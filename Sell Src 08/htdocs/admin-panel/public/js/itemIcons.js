// Item Icons Data - Map icon_id to image URLs
// This file is loaded on the frontend to get item icon information

const ItemIcons = {
    // Base URL for icons
    baseUrl: '/icons',
    
    // Get icon URL for a specific zoom level
    getIconUrl: function(iconId, zoom = 1) {
        if (!iconId || iconId < 0) return this.getPlaceholder();
        return `${this.baseUrl}/x${zoom}/${iconId}.png`;
    },
    
    // Get 1x zoom icon
    getIcon: function(iconId) {
        return this.getIconUrl(iconId, 1);
    },
    
    // Get 2x zoom icon
    getIcon2x: function(iconId) {
        return this.getIconUrl(iconId, 2);
    },
    
    // Get 4x zoom icon
    getIcon4x: function(iconId) {
        return this.getIconUrl(iconId, 4);
    },
    
    // Get placeholder image
    getPlaceholder: function() {
        return 'data:image/svg+xml,' + encodeURIComponent(`
            <svg xmlns="http://www.w3.org/2000/svg" width="48" height="48" viewBox="0 0 48 48">
                <rect fill="#ddd" width="48" height="48"/>
                <text x="24" y="28" font-size="12" fill="#999" text-anchor="middle">?</text>
            </svg>
        `);
    },
    
    // Create item icon element
    createIconElement: function(iconId, options = {}) {
        const {
            size = 48,
            zoom = 1,
            className = '',
            alt = 'Item',
            showQuantity = false,
            quantity = 0
        } = options;
        
        const url = this.getIconUrl(iconId, zoom);
        const placeholder = this.getPlaceholder();
        
        return `
            <div class="item-icon ${className}" style="position: relative; width: ${size}px; height: ${size}px;">
                <img src="${url}" 
                     alt="${alt}" 
                     style="width: 100%; height: 100%; object-fit: contain;"
                     onerror="this.src='${placeholder}'"
                     loading="lazy">
                ${showQuantity && quantity > 0 ? `
                    <span style="position: absolute; bottom: -2px; right: -2px; 
                                 background: #333; color: #fff; font-size: 10px; 
                                 padding: 1px 4px; border-radius: 3px; font-weight: bold;">
                        ${quantity > 999 ? '999+' : quantity}
                    </span>
                ` : ''}
            </div>
        `;
    },
    
    // Item type names (from item_template TYPE column)
    typeNames: {
        0: 'Áo',
        1: 'Quần',
        2: 'Găng',
        3: 'Giày',
        4: 'Rada',
        5: 'Phù',
        6: 'Đậu thần',
        7: 'Sách',
        8: 'Nhiệm vụ',
        9: 'Vàng',
        10: 'Ngọc',
        11: 'Ngọc khóa',
        12: 'Ngọc Rồng',
        13: 'Bánh',
        14: 'Thảo dược',
        15: 'Truyền thuyết',
        16: 'Càn khôn',
        17: 'Thần linh giáp',
        18: 'Ngọc rồng PTC',
        19: 'Cỏ 4 lá',
        20: 'Bùa',
        21: 'Đá nâng cấp',
        22: 'Đá PCL',
        23: 'Set PTC',
        24: 'Bảo hộ PTC',
        25: 'Đá dính PTC',
        26: 'Đá xanh lá',
        27: 'Thức ăn',
        28: 'Bùa HP',
        29: 'Bùa KI',
        30: 'Bùa SD',
        31: 'Đá may mắn',
        32: 'Đá EXP',
        33: 'Đá 6 sao',
        34: 'Đá khảm',
        35: 'Bùa 6 sao',
        36: 'Item đặc biệt',
        37: 'Đá trible',
        38: 'Hành tinh',
        39: 'Vật phẩm hiếm'
    },
    
    // Get item type name
    getTypeName: function(typeId) {
        return this.typeNames[typeId] || `Loại ${typeId}`;
    },
    
    // Item type colors
    typeColors: {
        0: '#3498db',   // Áo - Blue
        1: '#e67e22',   // Quần - Orange
        2: '#9b59b6',   // Găng - Purple
        3: '#1abc9c',   // Giày - Teal
        4: '#f1c40f',   // Rada - Yellow
        5: '#e74c3c',   // Phù - Red
        6: '#27ae60',   // Đậu thần - Green
        7: '#34495e',   // Sách - Dark Gray
        8: '#95a5a6',   // Nhiệm vụ - Gray
        9: '#f39c12',   // Vàng - Gold
        10: '#3498db',  // Ngọc - Blue
        12: '#9b59b6',  // Ngọc Rồng - Purple
        14: '#2ecc71',  // Thảo dược - Light Green
        15: '#e74c3c',  // Truyền thuyết - Red
        16: '#f39c12',  // Càn khôn - Gold
        17: '#3498db',  // Thần linh giáp - Blue
        18: '#9b59b6', // Ngọc rồng PTC - Purple
        21: '#3498db',  // Đá nâng cấp - Blue
        22: '#1abc9c',  // Đá PCL - Teal
        27: '#e67e22',  // Thức ăn - Orange
    },
    
    // Get type color
    getTypeColor: function(typeId) {
        return this.typeColors[typeId] || '#95a5a6';
    }
};

// Export for use in other scripts
if (typeof module !== 'undefined' && module.exports) {
    module.exports = ItemIcons;
}
