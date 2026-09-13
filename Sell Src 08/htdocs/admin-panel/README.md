# NRO Admin Panel - Hệ Thống Quản Trị Server Game Ngọc Rồng

## Giới Thiệu
Đây là hệ thống Admin Panel dành cho server game Ngọc Rồng (NRO) của bạn. Panel kết nối trực tiếp đến database `nro onepuch` từ server source code (Maro Sell Src 08), cho phép quản lý toàn bộ các khía cạnh của server game bao gồm: tài khoản, nhân vật, vật phẩm, giftcode, boss, và cài đặt server.

## Database Connection
- **Database Name**: `nro onepuch`
- **Host**: localhost
- **User**: root
- **Tables Used**: account, player, giftcode, shop, history_gold, history_transaction, adminpanel, item_template

## Hình Ảnh Vật Phẩm (Icons)

Server game lưu hình ảnh vật phẩm tại thư mục:
```
Maro Sell Src 08/data/girlkun/icon/x{zoom_level}/{icon_id}.png
```

Admin Panel đã được cấu hình để serve các icons từ thư mục này:
- `/icons/x1/` - Icons zoom level 1 (mặc định)
- `/icons/x2/` - Icons zoom level 2
- `/icons/x3/` - Icons zoom level 3
- `/icons/x4/` - Icons zoom level 4

Mỗi vật phẩm trong `item_template` có `icon_id` để xác định hình ảnh.

**Lưu ý**: Nếu thư mục icon đang trống, bạn cần copy icons từ client vào đúng vị trí.

## Tính Năng

### Dashboard
- Tổng quan số liệu thống kê
- Top nhân vật mạnh nhất
- Thao tác nhanh

### Quản Lý Tài Khoản
- Xem danh sách tài khoản
- Tìm kiếm theo username
- Ban/Unban tài khoản
- Cập nhật quyền admin
- Chỉnh sửa thông tin tài chính (tổng nạp, coin)

### Quản Lý Nhân Vật
- Xem danh sách nhân vật
- Tìm kiếm theo tên
- Xem chi tiết chỉ số nhân vật
- Chỉnh sửa sức mạnh, tiềm năng
- Thêm/xóa vật phẩm trong hành trang
- Cập nhật vàng, ngọc

### Quản Lý Giftcode
- Tạo giftcode mới
- Thiết lập vật phẩm trong giftcode
- Bật/tắt giftcode
- Xóa giftcode

### Quản Lý Boss
- Xem danh sách boss
- Trạng thái sống/chết
- Triệu hồi boss
- Chỉnh sửa vị trí boss

### Quản Lý Vật Phẩm
- Xem danh sách tất cả vật phẩm từ `item_template`
- Hiển thị hình ảnh vật phẩm từ server game
- Tìm kiếm vật phẩm theo tên/ID
- Xem chi tiết vật phẩm (tên, loại, giá, yêu cầu)
- Phân loại theo loại vật phẩm (Áo, Quần, Găng, Ngọc Rồng, v.v.)

### Quản Lý Cửa Hàng
- (Đang phát triển)

### Lịch Sử Giao Dịch
- Lịch sử vàng
- Lịch sử giao dịch

### Cài Đặt Server
- Thông tin domain
- Link download các nền tảng
- Tỉ lệ EXP, drop
- Trạng thái server (hoạt động/bảo trì)

## Cài Đặt

### Yêu Cầu
- Node.js 16 trở lên
- MySQL/MariaDB
- Server game đang chạy

### Các Bước Cài Đặt

1. **Di chuyển vào thư mục admin-panel:**
```bash
cd "D:/nro08/nroonepuchman/Sell Src 08/htdocs/admin-panel"
```

2. **Cài đặt dependencies:**
```bash
npm install
```

3. **Cấu hình database:**
Mở file `config.js` và chỉnh sửa thông tin kết nối:
```javascript
database: {
    host: 'localhost',
    user: 'root',
    password: '', // Điền password MySQL của bạn
    database: 'nro onepuch', // Tên database game
    port: 3306
}
```

4. **Tạo tài khoản admin:**
Đảm bảo trong bảng `account` có tài khoản với `is_admin = 1`. Nếu chưa có, chạy SQL:
```sql
UPDATE account SET is_admin = 1 WHERE username = 'bkt';
```

5. **Khởi động server:**
```bash
npm start
```

6. **Truy cập Admin Panel:**
- Đăng nhập: http://localhost:3000/login.html
- Dashboard: http://localhost:3000/dashboard.html

## Cấu Trúc Project

```
admin-panel/
├── config.js           # Cấu hình database và server
├── server.js          # Backend API (Express.js)
├── package.json       # Dependencies
├── public/
│   ├── css/
│   │   └── style.css  # Styles chính
│   ├── js/            # JavaScript files
│   ├── images/        # Hình ảnh
│   ├── index.html     # Redirect page
│   ├── login.html     # Trang đăng nhập
│   └── dashboard.html  # Trang chính
└── README.md          # File này
```

## API Endpoints

### Authentication
- `POST /api/auth/login` - Đăng nhập
- `GET /api/auth/me` - Lấy thông tin admin hiện tại

### Dashboard
- `GET /api/dashboard/stats` - Thống kê tổng quan

### Accounts
- `GET /api/accounts` - Danh sách tài khoản
- `GET /api/accounts/:id` - Chi tiết tài khoản
- `PUT /api/accounts/:id` - Cập nhật tài khoản

### Players
- `GET /api/players` - Danh sách nhân vật
- `GET /api/players/:id` - Chi tiết nhân vật
- `PUT /api/players/:id/stats` - Cập nhật chỉ số
- `PUT /api/players/:id/currency` - Cập nhật vàng/ngọc
- `POST /api/players/:id/items` - Thêm vật phẩm
- `DELETE /api/players/:id/items/:index` - Xóa vật phẩm

### Giftcodes
- `GET /api/giftcodes` - Danh sách giftcode
- `POST /api/giftcodes` - Tạo giftcode mới
- `PUT /api/giftcodes/:id` - Cập nhật giftcode
- `DELETE /api/giftcodes/:id` - Xóa giftcode

### Items
- `GET /api/items/templates` - Danh sách vật phẩm (với icon URLs)
- `GET /api/items/:id` - Chi tiết vật phẩm
- `GET /api/items/icon/:iconId` - Lấy icon theo icon_id
- `GET /icons/x{zoom}/{id}.png` - Serve icons từ server game

### Settings
- `GET /api/settings` - Lấy cài đặt
- `PUT /api/settings` - Cập nhật cài đặt

## Bảo Mật

- Mật khẩu được mã hóa với bcrypt
- JWT token cho xác thực
- Session management
- Input validation

## Lưu Ý

1. **Kết nối Database**: Admin panel sử dụng chung database với game server
2. **Cổng mặc định**: 3000 (có thể thay đổi trong config.js)
3. **Đảm bảo server game đang chạy** khi sử dụng các tính năng real-time
4. **Backup database** trước khi thực hiện các thay đổi lớn

## Troubleshooting

### Lỗi kết nối database
- Kiểm tra thông tin trong `config.js`
- Đảm bảo MySQL đang chạy
- Kiểm tra firewall

### Lỗi đăng nhập
- Đảm bảo tài khoản có `is_admin = 1`
- Kiểm tra password đã được mã hóa đúng cách

### Lỗi CORS
- Server đã được cấu hình CORS, kiểm tra lại nếu frontend không load được

### Icons không hiển thị
- Kiểm tra thư mục `data/girlkun/icon` trong server game
- Copy thư mục `icon` từ client vào đúng vị trí
- Cấu trúc: `data/girlkun/icon/x1/{icon_id}.png`
- Icons sẽ hiển thị placeholder nếu không tìm thấy file

## License
Project này được tạo cho mục đích quản lý server game Ngọc Rồng của bạn.
