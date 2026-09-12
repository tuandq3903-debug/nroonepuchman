<?php
// Không cần gọi session_start() ở đây nữa nếu đã gọi trong dang-nhap.php
// session_start(); 

// Tạo chuỗi CAPTCHA ngẫu nhiên (6 ký tự, bao gồm cả chữ cái và số)
$characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
$captcha_string = '';
for ($i = 0; $i < 6; $i++) {
    $captcha_string .= $characters[rand(0, strlen($characters) - 1)];
}

// Lưu CAPTCHA vào session để kiểm tra khi người dùng gửi form
session_start();  // Đảm bảo session_start() chỉ gọi một lần, ở một nơi duy nhất (nếu chưa gọi trong dang-nhap.php)
$_SESSION['captcha'] = $captcha_string;

// Tạo hình ảnh CAPTCHA
$image = imagecreatetruecolor(200, 50); // Kích thước 200x50
$background_color = imagecolorallocate($image, 255, 255, 255); // Màu nền trắng
$text_color = imagecolorallocate($image, 0, 0, 0); // Màu chữ đen
$line_color = imagecolorallocate($image, 64, 64, 64); // Màu đường kẻ

// Fill background
imagefill($image, 0, 0, $background_color);

// Vẽ vài đường kẻ ngẫu nhiên cho CAPTCHA thêm phần khó
for ($i = 0; $i < 5; $i++) {
    imageline($image, rand(0, 200), rand(0, 50), rand(0, 200), rand(0, 50), $line_color);
}

// Vẽ chuỗi CAPTCHA lên hình ảnh
imagettftext($image, 20, rand(-10, 10), rand(30, 120), rand(30, 40), $text_color, 'path_to_your_font.ttf', $captcha_string);

// Tạo header để hiển thị hình ảnh trong trình duyệt
header('Content-Type: image/png');
imagepng($image);
imagedestroy($image);
?>
