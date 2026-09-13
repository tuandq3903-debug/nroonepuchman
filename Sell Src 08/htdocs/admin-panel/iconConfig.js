// Item Icons Configuration - Kết nối đến hình ảnh vật phẩm từ server
// Server lưu icons tại: data/girlkun/icon/x{zoomlevel}/{icon_id}.png

module.exports = {
    // Đường dẫn đến thư mục chứa icons (từ server game)
    iconPath: '../../../Maro Sell Src 08/data/girlkun/icon',
    
    // Các zoom levels mặc định
    zoomLevels: [1, 2, 3, 4],
    
    // URL gốc để serve icons (nếu dùng qua HTTP)
    // Thay đổi theo cấu hình server của bạn
    iconBaseUrl: '/icons',
    
    // Folder name mapping for icon storage
    zoomFolders: {
        1: 'x1',
        2: 'x2', 
        3: 'x3',
        4: 'x4'
    }
};
