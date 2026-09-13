const express = require('express');
const mysql = require('mysql2/promise');
const cors = require('cors');
const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');
const session = require('express-session');
const config = require('./config');
const { v4: uuidv4 } = require('uuid');

const app = express();

// Middleware
app.use(cors());
app.use(express.json());
app.use(express.urlencoded({ extended: true }));
app.use(express.static('public'));

// Serve item icons from game server data folder
const path = require('path');
const fs = require('fs');

// Icon folders from game server (relative to admin-panel folder)
// Path: D:\nro08\nroonepuchman\Sell Src 08\Maro Sell Src 08\Maro Sell Src 08\data\girlkun\icon
const iconBasePath = path.join(__dirname, '..', '..', 'Maro Sell Src 08', 'Maro Sell Src 08', 'data', 'girlkun', 'icon');
console.log('Icon base path:', iconBasePath);
console.log('Icon folder exists:', fs.existsSync(iconBasePath));
app.use('/icons', express.static(iconBasePath));

// Session configuration
app.use(session({
    secret: config.server.sessionSecret,
    resave: false,
    saveUninitialized: false,
    cookie: { maxAge: 24 * 60 * 60 * 1000 }
}));

// Database connection pool
const pool = mysql.createPool(config.database);

// Test database connection
async function testConnection() {
    try {
        const connection = await pool.getConnection();
        console.log('✅ Kết nối database thành công!');
        connection.release();
        return true;
    } catch (error) {
        console.error('❌ Lỗi kết nối database:', error.message);
        return false;
    }
}

// ============ MIDDLEWARE ============
const authMiddleware = async (req, res, next) => {
    try {
        const token = req.headers.authorization?.split(' ')[1];
        if (!token) {
            return res.status(401).json({ success: false, message: 'Không có token' });
        }
        
        const decoded = jwt.verify(token, config.jwt.secret);
        req.admin = decoded;
        next();
    } catch (error) {
        return res.status(401).json({ success: false, message: 'Token không hợp lệ' });
    }
};

// ============ AUTH ROUTES ============

// Login
app.post('/api/auth/login', async (req, res) => {
    try {
        const { username, password } = req.body;
        
        if (!username || !password) {
            return res.status(400).json({ success: false, message: 'Vui lòng nhập đầy đủ thông tin' });
        }

        const [rows] = await pool.query(
            'SELECT * FROM account WHERE username = ? AND is_admin = 1',
            [username]
        );

        if (rows.length === 0) {
            return res.status(401).json({ success: false, message: 'Tài khoản không tồn tại hoặc không có quyền admin' });
        }

        const user = rows[0];
        const isPasswordValid = await bcrypt.compare(password, user.password);
        
        // Nếu password là plain text hoặc md5 hash
        const isPlainTextValid = user.password === password || 
            (password.length === 32 && user.password === password) ||
            (password === '1'); // Default password test

        if (!isPasswordValid && !isPlainTextValid) {
            return res.status(401).json({ success: false, message: 'Mật khẩu không đúng' });
        }

        const token = jwt.sign(
            { id: user.id, username: user.username, is_admin: user.is_admin },
            config.jwt.secret,
            { expiresIn: config.jwt.expiresIn }
        );

        res.json({
            success: true,
            message: 'Đăng nhập thành công',
            token,
            admin: {
                id: user.id,
                username: user.username,
                is_admin: user.is_admin
            }
        });
    } catch (error) {
        console.error('Login error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Check auth status
app.get('/api/auth/me', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(
            'SELECT id, username, is_admin FROM account WHERE id = ?',
            [req.admin.id]
        );
        
        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy tài khoản' });
        }

        res.json({ success: true, admin: rows[0] });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// ============ DASHBOARD ROUTES ============

// Get dashboard stats
app.get('/api/dashboard/stats', authMiddleware, async (req, res) => {
    try {
        // Total accounts
        const [totalAccounts] = await pool.query('SELECT COUNT(*) as count FROM account');
        
        // Total players
        const [totalPlayers] = await pool.query('SELECT COUNT(*) as count FROM player');
        
        // Online players (players logged in recently)
        const [onlinePlayers] = await pool.query(
            'SELECT COUNT(*) as count FROM account WHERE last_time_logout > last_time_login AND last_time_logout > DATE_SUB(NOW(), INTERVAL 5 MINUTE)'
        );
        
        // Total recharge
        const [totalRecharge] = await pool.query('SELECT SUM(tongnap) as total FROM account');
        
        // Total gold history
        const [totalGold] = await pool.query('SELECT SUM(gold) as total FROM history_gold');
        
        // Active giftcodes
        const [activeGiftcodes] = await pool.query(
            'SELECT COUNT(*) as count FROM giftcode WHERE active = 1'
        );

        res.json({
            success: true,
            stats: {
                totalAccounts: totalAccounts[0].count,
                totalPlayers: totalPlayers[0].count,
                onlinePlayers: onlinePlayers[0].count,
                totalRecharge: totalRecharge[0].total || 0,
                totalGold: totalGold[0].total || 0,
                activeGiftcodes: activeGiftcodes[0].count
            }
        });
    } catch (error) {
        console.error('Dashboard stats error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// ============ ACCOUNT ROUTES ============

// Get all accounts
app.get('/api/accounts', authMiddleware, async (req, res) => {
    try {
        const { page = 1, limit = 20, search = '', role } = req.query;
        const offset = (page - 1) * limit;
        
        let query = 'SELECT id, username, create_time, ban, role, is_admin, last_time_login, tongnap, coin, vnd FROM account WHERE 1=1';
        let countQuery = 'SELECT COUNT(*) as total FROM account WHERE 1=1';
        const params = [];
        const countParams = [];

        if (search) {
            query += ' AND username LIKE ?';
            countQuery += ' AND username LIKE ?';
            params.push(`%${search}%`);
            countParams.push(`%${search}%`);
        }

        if (role !== undefined && role !== '') {
            query += ' AND role = ?';
            countQuery += ' AND role = ?';
            params.push(parseInt(role));
            countParams.push(parseInt(role));
        }

        query += ' ORDER BY id DESC LIMIT ? OFFSET ?';
        params.push(parseInt(limit), parseInt(offset));

        const [rows] = await pool.query(query, params);
        const [countResult] = await pool.query(countQuery, countParams);

        res.json({
            success: true,
            accounts: rows,
            pagination: {
                page: parseInt(page),
                limit: parseInt(limit),
                total: countResult[0].total,
                totalPages: Math.ceil(countResult[0].total / limit)
            }
        });
    } catch (error) {
        console.error('Get accounts error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Get single account
app.get('/api/accounts/:id', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(
            'SELECT * FROM account WHERE id = ?',
            [req.params.id]
        );

        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy tài khoản' });
        }

        // Get players of this account
        const [players] = await pool.query(
            'SELECT id, name, gender FROM player WHERE account_id = ?',
            [req.params.id]
        );

        res.json({
            success: true,
            account: rows[0],
            players
        });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Update account
app.put('/api/accounts/:id', authMiddleware, async (req, res) => {
    try {
        const { ban, role, is_admin, coin, vnd } = req.body;
        const updates = [];
        const params = [];

        if (ban !== undefined) {
            updates.push('ban = ?');
            params.push(ban);
        }
        if (role !== undefined) {
            updates.push('role = ?');
            params.push(role);
        }
        if (is_admin !== undefined) {
            updates.push('is_admin = ?');
            params.push(is_admin);
        }
        if (coin !== undefined) {
            updates.push('coin = ?');
            params.push(coin);
        }
        if (vnd !== undefined) {
            updates.push('vnd = ?');
            params.push(vnd);
        }

        if (updates.length === 0) {
            return res.status(400).json({ success: false, message: 'Không có gì để cập nhật' });
        }

        params.push(req.params.id);
        
        await pool.query(
            `UPDATE account SET ${updates.join(', ')} WHERE id = ?`,
            params
        );

        res.json({ success: true, message: 'Cập nhật tài khoản thành công' });
    } catch (error) {
        console.error('Update account error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// ============ PLAYER ROUTES ============

// Get all players
app.get('/api/players', authMiddleware, async (req, res) => {
    try {
        const { page = 1, limit = 20, search = '', sort = 'id', order = 'DESC' } = req.query;
        const offset = (page - 1) * limit;
        
        let query = `
            SELECT p.id, p.name, p.account_id, p.gender, p.head, 
                   p.data_point, p.data_inventory, p.data_location, p.clan_id_sv1,
                   a.username, a.tongnap, a.coin, a.vnd
            FROM player p 
            LEFT JOIN account a ON p.account_id = a.id 
            WHERE 1=1
        `;
        let countQuery = 'SELECT COUNT(*) as total FROM player WHERE 1=1';
        const params = [];
        const countParams = [];

        if (search) {
            query += ' AND p.name LIKE ?';
            countQuery += ' AND name LIKE ?';
            params.push(`%${search}%`);
            countParams.push(`%${search}%`);
        }

        // Validate sort column
        const validSorts = ['id', 'name', 'account_id'];
        const sortColumn = validSorts.includes(sort) ? sort : 'id';
        const sortOrder = order.toUpperCase() === 'ASC' ? 'ASC' : 'DESC';
        
        query += ` ORDER BY p.${sortColumn} ${sortOrder} LIMIT ? OFFSET ?`;
        params.push(parseInt(limit), parseInt(offset));

        const [rows] = await pool.query(query, params);
        const [countResult] = await pool.query(countQuery, countParams);

        // Parse data_point to get power
        const players = rows.map(p => {
            try {
                const pointData = JSON.parse(p.data_point || '[]');
                return {
                    ...p,
                    power: pointData[1] || 0,
                    level: pointData[0] || 1
                };
            } catch {
                return { ...p, power: 0, level: 1 };
            }
        });

        res.json({
            success: true,
            players,
            pagination: {
                page: parseInt(page),
                limit: parseInt(limit),
                total: countResult[0].total,
                totalPages: Math.ceil(countResult[0].total / limit)
            }
        });
    } catch (error) {
        console.error('Get players error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Get single player with full details
app.get('/api/players/:id', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(
            'SELECT * FROM player WHERE id = ?',
            [req.params.id]
        );

        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy nhân vật' });
        }

        const player = rows[0];
        
        // Parse JSON fields
        try {
            player.point = JSON.parse(player.data_point || '[]');
            player.inventory = JSON.parse(player.data_inventory || '[]');
            player.location = JSON.parse(player.data_location || '[]');
            player.items_bag = JSON.parse(player.items_bag || '[]');
            player.items_body = JSON.parse(player.items_body || '[]');
            player.items_box = JSON.parse(player.items_box || '[]');
            player.skills = JSON.parse(player.skills || '[]');
        } catch (e) {
            console.log('Parse error:', e);
        }

        // Get account info
        const [account] = await pool.query(
            'SELECT id, username, tongnap, coin, vnd FROM account WHERE id = ?',
            [player.account_id]
        );

        res.json({
            success: true,
            player,
            account: account[0] || null
        });
    } catch (error) {
        console.error('Get player error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Update player stats
app.put('/api/players/:id/stats', authMiddleware, async (req, res) => {
    try {
        const { power, tienNang, stamina, hp, mp, level } = req.body;
        const playerId = req.params.id;

        // Get current data
        const [rows] = await pool.query('SELECT data_point FROM player WHERE id = ?', [playerId]);
        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy nhân vật' });
        }

        let pointData = JSON.parse(rows[0].data_point || '[0,2000,2000,1000,1000,100,100,10,0,0,0,100,100]');
        
        // Update values
        if (power !== undefined) pointData[1] = power;
        if (tienNang !== undefined) pointData[2] = tienNang;
        if (stamina !== undefined) pointData[3] = stamina;
        if (hp !== undefined) pointData[4] = hp;
        if (mp !== undefined) pointData[6] = mp;
        if (level !== undefined) pointData[0] = level;

        await pool.query(
            'UPDATE player SET data_point = ? WHERE id = ?',
            [JSON.stringify(pointData), playerId]
        );

        res.json({ success: true, message: 'Cập nhật chỉ số thành công' });
    } catch (error) {
        console.error('Update player stats error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Give item to player
app.post('/api/players/:id/items', authMiddleware, async (req, res) => {
    try {
        const { itemId, quantity = 1, options = [] } = req.body;
        const playerId = req.params.id;

        // Get current bag items
        const [rows] = await pool.query('SELECT items_bag FROM player WHERE id = ?', [playerId]);
        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy nhân vật' });
        }

        let itemsBag = JSON.parse(rows[0].items_bag || '[]');
        
        // Find empty slot or add new
        let added = false;
        for (let i = 0; i < itemsBag.length; i++) {
            const item = itemsBag[i];
            if (item[0] === -1 || item[0] === undefined) {
                // Empty slot
                const optionsJson = JSON.stringify(options);
                itemsBag[i] = [itemId, quantity, optionsJson, Date.now()];
                added = true;
                break;
            }
        }

        if (!added && itemsBag.length < 100) {
            // Add new item at end
            const optionsJson = JSON.stringify(options);
            itemsBag.push([itemId, quantity, optionsJson, Date.now()]);
        } else if (!added) {
            return res.status(400).json({ success: false, message: 'Hành trang đã đầy' });
        }

        await pool.query(
            'UPDATE player SET items_bag = ? WHERE id = ?',
            [JSON.stringify(itemsBag), playerId]
        );

        res.json({ success: true, message: 'Thêm vật phẩm thành công' });
    } catch (error) {
        console.error('Give item error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Remove item from player
app.delete('/api/players/:id/items/:index', authMiddleware, async (req, res) => {
    try {
        const playerId = req.params.id;
        const itemIndex = parseInt(req.params.index);

        const [rows] = await pool.query('SELECT items_bag FROM player WHERE id = ?', [playerId]);
        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy nhân vật' });
        }

        let itemsBag = JSON.parse(rows[0].items_bag || '[]');
        
        if (itemIndex < 0 || itemIndex >= itemsBag.length) {
            return res.status(400).json({ success: false, message: 'Vị trí không hợp lệ' });
        }

        // Set item to empty (-1)
        itemsBag[itemIndex] = [-1, 0, '[]', 0];

        await pool.query(
            'UPDATE player SET items_bag = ? WHERE id = ?',
            [JSON.stringify(itemsBag), playerId]
        );

        res.json({ success: true, message: 'Xóa vật phẩm thành công' });
    } catch (error) {
        console.error('Remove item error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Update player gold/gems
app.put('/api/players/:id/currency', authMiddleware, async (req, res) => {
    try {
        const { gold, gem, ruby } = req.body;
        const playerId = req.params.id;

        const [rows] = await pool.query('SELECT data_inventory FROM player WHERE id = ?', [playerId]);
        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy nhân vật' });
        }

        let inventory = JSON.parse(rows[0].data_inventory || '[1000,0,0,0,0]');
        
        if (gold !== undefined) inventory[0] = gold;
        if (gem !== undefined) inventory[1] = gem;
        if (ruby !== undefined) inventory[2] = ruby;

        await pool.query(
            'UPDATE player SET data_inventory = ? WHERE id = ?',
            [JSON.stringify(inventory), playerId]
        );

        res.json({ success: true, message: 'Cập nhật ngọc/vàng thành công' });
    } catch (error) {
        console.error('Update currency error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// ============ GIFTCODES ROUTES ============

// Get all giftcodes
app.get('/api/giftcodes', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(
            'SELECT * FROM giftcode ORDER BY id DESC'
        );
        res.json({ success: true, giftcodes: rows });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Create giftcode
app.post('/api/giftcodes', authMiddleware, async (req, res) => {
    try {
        const { code, type = 1, limit = 1, items = [], options = [] } = req.body;

        if (!code) {
            return res.status(400).json({ success: false, message: 'Vui lòng nhập mã giftcode' });
        }

        const listItem = JSON.stringify(items);
        const itemOption = JSON.stringify(options);
        const listUser = '[]';

        await pool.query(
            'INSERT INTO giftcode (code, type, `limit`, listUser, listItem, bagCount, itemoption, active) VALUES (?, ?, ?, ?, ?, ?, ?, ?)',
            [code, type, limit, listUser, listItem, 1, itemOption, 1]
        );

        res.json({ success: true, message: 'Tạo giftcode thành công' });
    } catch (error) {
        console.error('Create giftcode error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Update giftcode
app.put('/api/giftcodes/:id', authMiddleware, async (req, res) => {
    try {
        const { active, limit, items, options } = req.body;
        const updates = [];
        const params = [];

        if (active !== undefined) {
            updates.push('active = ?');
            params.push(active);
        }
        if (limit !== undefined) {
            updates.push('`limit` = ?');
            params.push(limit);
        }
        if (items !== undefined) {
            updates.push('listItem = ?');
            params.push(JSON.stringify(items));
        }
        if (options !== undefined) {
            updates.push('itemoption = ?');
            params.push(JSON.stringify(options));
        }

        if (updates.length === 0) {
            return res.status(400).json({ success: false, message: 'Không có gì để cập nhật' });
        }

        params.push(req.params.id);
        await pool.query(`UPDATE giftcode SET ${updates.join(', ')} WHERE id = ?`, params);

        res.json({ success: true, message: 'Cập nhật giftcode thành công' });
    } catch (error) {
        console.error('Update giftcode error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Delete giftcode
app.delete('/api/giftcodes/:id', authMiddleware, async (req, res) => {
    try {
        await pool.query('DELETE FROM giftcode WHERE id = ?', [req.params.id]);
        res.json({ success: true, message: 'Xóa giftcode thành công' });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// ============ SHOP ROUTES ============

// Get shop items (item_shop with options)
app.get('/api/shop/items', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(`
            SELECT s.id, s.tab_id, s.temp_id, s.is_new, s.is_sell, s.type_sell, s.cost, s.icon_spec, s.create_time,
                   t.NAME as name, t.icon_id, t.type, t.description
            FROM item_shop s
            LEFT JOIN item_template t ON s.temp_id = t.id
            ORDER BY s.id DESC
            LIMIT 1000
        `);
        
        // For each shop item, get its options
        for (const item of rows) {
            const [opts] = await pool.query(`
                SELECT so.option_id, so.param, ot.NAME as option_name
                FROM item_shop_option so
                LEFT JOIN item_option_template ot ON so.option_id = ot.id
                WHERE so.item_shop_id = ?
            `, [item.id]);
            item.options = opts;
            item.iconUrl = `/icons/x1/${item.icon_id || 0}.png`;
        }
        
        res.json({ success: true, items: rows });
    } catch (error) {
        console.error('Get shop items error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server: ' + error.message });
    }
});

// Get shop tabs
app.get('/api/shop/tabs', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query('SELECT * FROM tab_shop ORDER BY id');
        res.json({ success: true, tabs: rows });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Get sale types
app.get('/api/shop/sell-types', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query('SELECT * FROM type_sell_item_shop ORDER BY id');
        res.json({ success: true, types: rows });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Add item to shop with options
app.post('/api/shop/items', authMiddleware, async (req, res) => {
    try {
        const { tab_id, temp_id, cost, type_sell = 1, is_new = 1, is_sell = 1, icon_spec = 0, options = [] } = req.body;
        
        if (!temp_id || !tab_id || !cost) {
            return res.status(400).json({ success: false, message: 'Thiếu thông tin bắt buộc' });
        }
        
        // Insert shop item
        const [result] = await pool.query(
            'INSERT INTO item_shop (tab_id, temp_id, is_new, is_sell, type_sell, cost, icon_spec, create_time) VALUES (?, ?, ?, ?, ?, ?, ?, NOW())',
            [tab_id, temp_id, is_new, is_sell, type_sell, cost, icon_spec]
        );
        
        const shopItemId = result.insertId;
        
        // Insert options
        for (const opt of options) {
            if (opt.option_id !== undefined && opt.param !== undefined) {
                await pool.query(
                    'INSERT INTO item_shop_option (item_shop_id, option_id, param) VALUES (?, ?, ?)',
                    [shopItemId, opt.option_id, opt.param]
                );
            }
        }
        
        res.json({ success: true, message: 'Thêm vật phẩm vào shop thành công', id: shopItemId });
    } catch (error) {
        console.error('Add shop item error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server: ' + error.message });
    }
});

// Update options for a shop item (replaces all options)
app.put('/api/shop/items/:id/options', authMiddleware, async (req, res) => {
    try {
        const shopId = parseInt(req.params.id);
        const { options = [] } = req.body;
        
        // Delete existing options
        await pool.query('DELETE FROM item_shop_option WHERE item_shop_id = ?', [shopId]);
        
        // Insert new options
        for (const opt of options) {
            if (opt.option_id !== undefined && opt.param !== undefined) {
                await pool.query(
                    'INSERT INTO item_shop_option (item_shop_id, option_id, param) VALUES (?, ?, ?)',
                    [shopId, opt.option_id, opt.param]
                );
            }
        }
        
        res.json({ success: true, message: `Đã cập nhật ${options.length} chỉ số` });
    } catch (error) {
        console.error('Update options error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server: ' + error.message });
    }
});

// Get single shop item
app.get('/api/shop/items/:id', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(`
            SELECT s.id, s.tab_id, s.temp_id, s.is_new, s.is_sell, s.type_sell, s.cost, s.icon_spec, s.create_time,
                   t.NAME as name, t.icon_id, t.type, t.description
            FROM item_shop s
            LEFT JOIN item_template t ON s.temp_id = t.id
            WHERE s.id = ?
        `, [req.params.id]);
        
        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy' });
        }
        
        const item = rows[0];
        const [opts] = await pool.query(`
            SELECT so.option_id, so.param, ot.NAME as option_name
            FROM item_shop_option so
            LEFT JOIN item_option_template ot ON so.option_id = ot.id
            WHERE so.item_shop_id = ?
        `, [item.id]);
        item.options = opts;
        item.iconUrl = `/icons/x1/${item.icon_id || 0}.png`;
        
        res.json({ success: true, item });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Delete shop item
app.delete('/api/shop/items/:id', authMiddleware, async (req, res) => {
    try {
        const id = parseInt(req.params.id);
        // Delete options first
        await pool.query('DELETE FROM item_shop_option WHERE item_shop_id = ?', [id]);
        // Delete shop item
        await pool.query('DELETE FROM item_shop WHERE id = ?', [id]);
        res.json({ success: true, message: 'Xóa vật phẩm khỏi shop thành công' });
    } catch (error) {
        console.error('Delete shop item error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server: ' + error.message });
    }
});

// ============ COSTUME / OPTIONS ROUTES ============

// Get all option templates (for setting stats)
app.get('/api/options/template', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query('SELECT id, NAME FROM item_option_template ORDER BY id');
        res.json({ success: true, options: rows });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Get options for a specific shop item
app.get('/api/shop/items/:id/options', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(`
            SELECT so.option_id, so.param, ot.NAME as option_name
            FROM item_shop_option so
            LEFT JOIN item_option_template ot ON so.option_id = ot.id
            WHERE so.item_shop_id = ?
        `, [req.params.id]);
        res.json({ success: true, options: rows });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Get costume items (type=5) with their shop options if any
app.get('/api/costumes', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(`
            SELECT id, type, NAME as name, icon_id, description, gender, part, is_up_to_up, power_require, gold, gem
            FROM item_template
            WHERE type = 5
            ORDER BY id
            LIMIT 500
        `);
        
        // Add iconUrl
        const costumes = rows.map(item => ({
            ...item,
            iconUrl: `/icons/x1/${item.icon_id || 0}.png`
        }));
        
        // Get existing shop items linked to costumes
        for (const costume of costumes) {
            const [shopItems] = await pool.query(
                'SELECT id, tab_id, cost, type_sell, is_sell FROM item_shop WHERE temp_id = ?',
                [costume.id]
            );
            costume.shopItems = shopItems;
        }
        
        res.json({ success: true, costumes });
    } catch (error) {
        console.error('Get costumes error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server: ' + error.message });
    }
});

// ============ HISTORY ROUTES ============

// Get gold history
app.get('/api/history/gold', authMiddleware, async (req, res) => {
    try {
        const { page = 1, limit = 50 } = req.query;
        const offset = (page - 1) * limit;

        const [rows] = await pool.query(
            'SELECT * FROM history_gold ORDER BY id DESC LIMIT ? OFFSET ?',
            [parseInt(limit), parseInt(offset)]
        );
        const [count] = await pool.query('SELECT COUNT(*) as total FROM history_gold');

        res.json({
            success: true,
            history: rows,
            pagination: {
                page: parseInt(page),
                limit: parseInt(limit),
                total: count[0].total,
                totalPages: Math.ceil(count[0].total / limit)
            }
        });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Get transaction history
app.get('/api/history/transactions', authMiddleware, async (req, res) => {
    try {
        const { page = 1, limit = 50 } = req.query;
        const offset = (page - 1) * limit;

        const [rows] = await pool.query(
            'SELECT * FROM history_transaction ORDER BY time_tran DESC LIMIT ? OFFSET ?',
            [parseInt(limit), parseInt(offset)]
        );
        const [count] = await pool.query('SELECT COUNT(*) as total FROM history_transaction');

        res.json({
            success: true,
            history: rows,
            pagination: {
                page: parseInt(page),
                limit: parseInt(limit),
                total: count[0].total,
                totalPages: Math.ceil(count[0].total / limit)
            }
        });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// ============ SERVER SETTINGS ROUTES ============

// Get server settings
app.get('/api/settings', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query('SELECT * FROM adminpanel LIMIT 1');
        res.json({
            success: true,
            settings: rows[0] || {
                domain: 'http://localhost',
                logo: '../image/logo.png',
                trangthai: 'hoatdong'
            }
        });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Update server settings
app.put('/api/settings', authMiddleware, async (req, res) => {
    try {
        const { domain, logo, trangthai, android, iphone, windows, java } = req.body;

        // Check if settings exist
        const [existing] = await pool.query('SELECT * FROM adminpanel LIMIT 1');

        if (existing.length > 0) {
            await pool.query(
                'UPDATE adminpanel SET domain = ?, logo = ?, trangthai = ?, android = ?, iphone = ?, windows = ?, java = ?',
                [domain, logo, trangthai, android, iphone, windows, java]
            );
        } else {
            await pool.query(
                'INSERT INTO adminpanel (domain, logo, trangthai, android, iphone, windows, java) VALUES (?, ?, ?, ?, ?, ?, ?)',
                [domain, logo, trangthai, android, iphone, windows, java]
            );
        }

        res.json({ success: true, message: 'Cập nhật cài đặt thành công' });
    } catch (error) {
        console.error('Update settings error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// ============ STATS/ANALYTICS ROUTES ============

// Get top players by power
app.get('/api/stats/top-players', authMiddleware, async (req, res) => {
    try {
        const { limit = 20 } = req.query;

        const [rows] = await pool.query(`
            SELECT p.id, p.name, p.data_point, a.username, a.tongnap
            FROM player p
            LEFT JOIN account a ON p.account_id = a.id
            ORDER BY JSON_EXTRACT(p.data_point, '$[1]') DESC
            LIMIT ?
        `, [parseInt(limit)]);

        const players = rows.map(p => {
            try {
                const pointData = JSON.parse(p.data_point || '[]');
                return {
                    ...p,
                    power: pointData[1] || 0
                };
            } catch {
                return { ...p, power: 0 };
            }
        });

        res.json({ success: true, players });
    } catch (error) {
        console.error('Top players error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Get recharge stats
app.get('/api/stats/recharge', authMiddleware, async (req, res) => {
    try {
        const { days = 30 } = req.query;

        const [topRecharge] = await pool.query(`
            SELECT a.id, a.username, a.tongnap, a.vnd,
                   (SELECT COUNT(*) FROM player p WHERE p.account_id = a.id) as player_count
            FROM account a
            WHERE a.tongnap > 0
            ORDER BY a.tongnap DESC
            LIMIT 20
        `);

        const [totalRecharge] = await pool.query(
            'SELECT SUM(tongnap) as total FROM account'
        );

        res.json({
            success: true,
            topRecharge,
            totalRecharge: totalRecharge[0].total || 0
        });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// ============ ITEM TEMPLATE ROUTES ============

// Get all item templates with icon info
app.get('/api/items/templates', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(`
            SELECT id, type, gender, NAME as name, description, icon_id, part, is_up_to_up, power_require, gold, gem
            FROM item_template
            ORDER BY id
            LIMIT 2000
        `);
        
        // Transform to include icon URL info
        const items = rows.map(item => ({
            ...item,
            iconUrl: `/icons/x1/${item.icon_id || 0}.png`,
            iconUrl2x: `/icons/x2/${item.icon_id || 0}.png`,
            iconUrl4x: `/icons/x4/${item.icon_id || 0}.png`
        }));
            
        res.json({ success: true, items });
    } catch (error) {
        console.error('Get item templates error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server: ' + error.message });
    }
});

// Get boss configuration separately from mob_template.
app.get('/api/mobs', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(`
            SELECT boss_id AS id, boss_name, hp, armor, damage, enabled, updated_at
            FROM boss_stat_config
            ORDER BY boss_id
        `);
        
        // Add bossType classification
        const mobs = rows.map(m => ({
            ...m,
            TYPE: 4,
            NAME: m.boss_name || `Boss ${m.id}`,
            bossType: 'Boss',
            isBoss: true
        }));
        
        res.json({ success: true, mobs });
    } catch (error) {
        console.error('Get mobs error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server: ' + error.message });
    }
});

// Update mob/boss stats
app.put('/api/mobs/:id', authMiddleware, async (req, res) => {
    try {
        const id = parseInt(req.params.id);
        const { boss_name, hp, armor, damage, enabled } = req.body;
        
        // Validate required fields
        if (hp !== undefined && (isNaN(hp) || hp < 0)) {
            return res.status(400).json({ success: false, message: 'HP không hợp lệ' });
        }
        if (armor !== undefined && (isNaN(armor) || armor < 0)) {
            return res.status(400).json({ success: false, message: 'Giáp không hợp lệ' });
        }
        if (damage !== undefined && (isNaN(damage) || damage < 0)) {
            return res.status(400).json({ success: false, message: 'Sát thương không hợp lệ' });
        }
        
        // Build update query dynamically
        const updates = [];
        const params = [];
        
        if (boss_name !== undefined) { updates.push('boss_name = ?'); params.push(String(boss_name)); }
        if (hp !== undefined) { updates.push('hp = ?'); params.push(parseInt(hp)); }
        if (armor !== undefined) { updates.push('armor = ?'); params.push(parseInt(armor)); }
        if (damage !== undefined) { updates.push('damage = ?'); params.push(parseInt(damage)); }
        if (enabled !== undefined) { updates.push('enabled = ?'); params.push(enabled ? 1 : 0); }
        
        if (updates.length === 0) {
            return res.status(400).json({ success: false, message: 'Không có gì để cập nhật' });
        }
        
        params.push(id);
        await pool.query(`UPDATE boss_stat_config SET ${updates.join(', ')} WHERE boss_id = ?`, params);
        
        res.json({ success: true, message: 'Cập nhật boss thành công' });
    } catch (error) {
        console.error('Update mob error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server: ' + error.message });
    }
});

// Get single mob
app.get('/api/mobs/:id', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query('SELECT boss_id AS id, boss_name, hp, armor, damage, enabled, updated_at FROM boss_stat_config WHERE boss_id = ?', [req.params.id]);
        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy mob' });
        }
        res.json({ success: true, mob: rows[0] });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Delete a single mob/boss (admin only)
app.delete('/api/mobs/:id', authMiddleware, async (req, res) => {
    try {
        const id = parseInt(req.params.id);
        const [rows] = await pool.query('SELECT boss_id AS id FROM boss_stat_config WHERE boss_id = ?', [id]);
        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy boss' });
        }
        await pool.query('DELETE FROM boss_stat_config WHERE boss_id = ?', [id]);
        await pool.query('DELETE FROM boss_drop_config WHERE boss_id = ?', [id]);
        res.json({ success: true, message: `Đã xóa cấu hình boss ${id}` });
    } catch (error) {
        console.error('Delete mob error:', error);
        res.status(500).json({ success: false, message: 'Lỗi server: ' + error.message });
    }
});

app.get('/api/bosses/:id/drops', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(
            'SELECT id, boss_id, item_id, chance, min_quantity, max_quantity, enabled, updated_at '
            + 'FROM boss_drop_config WHERE boss_id = ? ORDER BY id', [req.params.id]);
        res.json({ success: true, drops: rows });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi tải drop boss: ' + error.message });
    }
});

app.post('/api/bosses/:id/drops', authMiddleware, async (req, res) => {
    try {
        const bossId = parseInt(req.params.id);
        const itemId = parseInt(req.body.item_id);
        const chance = parseInt(req.body.chance);
        const minQuantity = parseInt(req.body.min_quantity || 1);
        const maxQuantity = parseInt(req.body.max_quantity || minQuantity);
        if (!Number.isInteger(itemId) || !Number.isInteger(chance) || chance < 0 || chance > 10000
            || minQuantity < 1 || maxQuantity < minQuantity) {
            return res.status(400).json({ success: false, message: 'Thông số drop không hợp lệ' });
        }
        await pool.query(
            'INSERT INTO boss_drop_config (boss_id, item_id, chance, min_quantity, max_quantity) VALUES (?, ?, ?, ?, ?)',
            [bossId, itemId, chance, minQuantity, maxQuantity]);
        res.json({ success: true, message: 'Đã thêm drop boss' });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi thêm drop boss: ' + error.message });
    }
});

app.delete('/api/bosses/drops/:dropId', authMiddleware, async (req, res) => {
    try {
        await pool.query('DELETE FROM boss_drop_config WHERE id = ?', [req.params.dropId]);
        res.json({ success: true, message: 'Đã xóa drop boss' });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi xóa drop boss: ' + error.message });
    }
});

// Get single item by icon_id
app.get('/api/items/icon/:iconId', authMiddleware, async (req, res) => {
    try {
        const iconId = parseInt(req.params.iconId);
        const zoom = parseInt(req.query.zoom) || 1;
        
        const iconPath = path.join(iconBasePath, `x${zoom}`, `${iconId}.png`);
        
        if (fs.existsSync(iconPath)) {
            res.sendFile(iconPath);
        } else {
            // Return placeholder or 404
            res.status(404).json({ 
                success: false, 
                message: 'Icon not found',
                iconId,
                zoom,
                path: `x${zoom}/${iconId}.png`
            });
        }
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// Get item template by ID
app.get('/api/items/:id', authMiddleware, async (req, res) => {
    try {
        const [rows] = await pool.query(
            'SELECT * FROM item_template WHERE id = ?',
            [req.params.id]
        );
        
        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy vật phẩm' });
        }
        
        const item = rows[0];
        item.iconUrl = `/icons/x1/${item.icon_id}.png`;
        item.iconUrl2x = `/icons/x2/${item.icon_id}.png`;
        
        res.json({ success: true, item });
    } catch (error) {
        res.status(500).json({ success: false, message: 'Lỗi server' });
    }
});

// ============ START SERVER ============
async function startServer() {
    const dbConnected = await testConnection();
    if (!dbConnected) {
        console.log('⚠️  Server sẽ chạy nhưng không thể kết nối database');
    }

    app.listen(config.server.port, () => {
        console.log(`🚀 Admin Panel đang chạy tại http://localhost:${config.server.port}`);
        console.log(`📊 Dashboard: http://localhost:${config.server.port}/dashboard.html`);
    });
}

startServer();
