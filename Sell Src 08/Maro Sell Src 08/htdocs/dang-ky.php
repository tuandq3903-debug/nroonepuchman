<?php
session_start(); // Khởi tạo session

require_once 'connect.php'; // Kết nối cơ sở dữ liệu
require_once 'set.php'; // Các thiết lập khác

$_alert = '';

// Hàm tạo mã CAPTCHA
function generateCaptcha($length = 5) {
    $characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';  // Bao gồm chữ cái và số
    $captcha = '';
    for ($i = 0; $i < $length; $i++) {
        $captcha .= $characters[rand(0, strlen($characters) - 1)];
    }
    return $captcha;
}

// Kiểm tra CAPTCHA khi người dùng gửi form
if ($_SERVER["REQUEST_METHOD"] == "POST" && !isset($_POST['refresh_captcha'])) {
    $username = trim($_POST["username"]);
    $password = trim($_POST["password"]);
    $confirm_password = trim($_POST["confirm_password"]);
    $user_captcha = trim($_POST["captcha"]); // Nhập CAPTCHA của người dùng

    // Kiểm tra CAPTCHA
    if ($user_captcha != $_SESSION['captcha_code']) {
        $_alert = "<div class='text-danger pb-2 font-weight-bold'>Mã CAPTCHA không chính xác. Vui lòng thử lại.</div>";
    } else {
        // Kiểm tra tính hợp lệ của username và password
        if (empty($username) || empty($password) || empty($confirm_password)) {
            $_alert = "<div class='text-danger pb-2 font-weight-bold'>Vui lòng điền đầy đủ thông tin.</div>";
        } elseif ($password !== $confirm_password) {
            $_alert = "<div class='text-danger pb-2 font-weight-bold'>Mật khẩu và mật khẩu nhập lại không khớp.</div>";
        } elseif (strlen($password) < 5) {
            $_alert = "<div class='text-danger pb-2 font-weight-bold'>Mật khẩu phải có ít nhất 5 ký tự.</div>";
        } else {
            // Truy vấn để kiểm tra tài khoản đã tồn tại chưa
            $stmt = $conn->prepare("SELECT * FROM account WHERE username=?");
            $stmt->bind_param("s", $username);
            $stmt->execute();
            $result = $stmt->get_result();

            if ($result->num_rows > 0) {
                $_alert = "<div class='text-danger pb-2 font-weight-bold'>Tài khoản đã tồn tại.</div>";
            } else {
                // Đăng ký tài khoản
                $stmt = $conn->prepare("INSERT INTO account (username, password) VALUES (?, ?)");
                $stmt->bind_param("ss", $username, $password); // Lưu mật khẩu thẳng
                if ($stmt->execute()) {
                    $_alert = '<div class="text-white pb-2 font-weight-bold" id="success-alert" style="color: white;">Đăng kí thành công, Vui Lòng Chờ ...</div>';
                    
                    // Chuyển hướng sau khi đăng ký thành công
                    echo '<script>
                            document.getElementById("success-alert").classList.add("show"); // Hiển thị thông báo thành công
                            setTimeout(function() {
                                window.location.href = "dang-nhap.php"; // Chuyển hướng sau 5 giây
                            }, 5000);
                          </script>';
                } else {
                    $_alert = '<div class="text-danger pb-2 font-weight-bold">Đăng ký thất bại.</div>';
                }
            }
        }
    }

    // Đóng kết nối
    $conn->close();
}

// Tạo mã CAPTCHA và lưu vào session
if (!isset($_SESSION['captcha_code']) || isset($_POST['refresh_captcha'])) {
    $_SESSION['captcha_code'] = generateCaptcha(); // Lưu mã CAPTCHA mới vào session
}
?>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $_title; ?></title>
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css">
    <style>
        body {
            background-image: url('https://imgur.com/LsKmxtU.png');
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }

        .register-container {
            border-radius: 10px;
            background-color: rgba(45, 190, 96, 0.9);
            padding: 30px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
        }

        .footer {
            font-size: 13px;
            color: #444;
            text-align: center;
        }

        .footer small {
            color: #888;
        }

        .btn-secondary {
            background-color: #000000;
            border-color: #6c757d;
        }

        .btn-main {
            background-color: #007bff;
            border-color: #007bff;
        }

        .btn-main:hover, .btn-secondary:hover {
            opacity: 0.8;
        }

        /* CSS cho thông báo thành công */
        #success-alert {
            position: fixed;
            top: 10px;
            right: 10px;
            background-color: #28a745; /* Green for success */
            color: white; /* Chữ trắng */
            padding: 10px;
            border-radius: 5px;
            font-weight: bold;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            display: none; /* Hide by default */
            z-index: 9999;
        }

        /* Hiển thị thông báo khi có sự kiện */
        #success-alert.show {
            display: block;
        }
    </style>
</head>

<body>
    <div class="container pt-5 pb-5">
        <div class="row justify-content-center">
            <div class="col-lg-6">
                <div class="register-container">
                    <h4 class="text-danger mb-4">ĐĂNG KÝ TÀI KHOẢN NRORUN</h4>
                    <form id="form" method="POST">
                        <div class="form-group">
                            <label for="username">Tài khoản:</label>
                            <input class="form-control" type="text" name="username" id="username" placeholder="Nhập tài khoản">
                        </div>
                        <div class="form-group">
                            <label for="password">Mật khẩu:</label>
                            <input class="form-control" type="password" name="password" id="password" placeholder="Nhập mật khẩu">
                        </div>
                        <div class="form-group">
                            <label for="confirm_password">Nhập lại mật khẩu:</label>
                            <input class="form-control" type="password" name="confirm_password" id="confirm_password" placeholder="Nhập lại mật khẩu">
                        </div>

                        <?php if (!empty($recafcode)) { ?>
                            <div class="form-group">
                                <label for="recaf">Mã giới thiệu:</label>
                                <input type="text" class="form-control" id="recaf" name="recaf" value="<?php echo $recafcode; ?>" placeholder="Nhập mã giới thiệu (nếu có)">
                            </div>
                        <?php } ?>

                        <!-- Hiển thị CAPTCHA -->
                        <div class="form-group">
                            <label for="captcha">Mã CAPTCHA:</label>
                            <input type="text" class="form-control" id="captcha" name="captcha" placeholder="Nhập mã CAPTCHA" required>
                            <small>Captcha: <strong id="captcha-text"><?php echo $_SESSION['captcha_code']; ?></strong></small>
                        </div>

                        <div class="form-check form-group">
                            <label class="form-check-label">
                                <input class="form-check-input" type="checkbox" name="accept" id="accept" checked=""/>
                                Đồng ý <a href="dieu-khoan" target="_blank">Điều khoản sử dụng</a>
                            </label>
                        </div>

                        <?php if (!empty($_alert)) {
                            echo $_alert;
                        } ?>

                       <div id="notify" class="text-danger pb-2 font-weight-bold"></div>
<div class="button-container">
    <button class="btn btn-main" type="submit">ĐĂNG KÝ</button>
</div>

<!-- Add your CSS below -->
<style>
    .button-container {
        display: flex;
        justify-content: center; /* Center the button horizontally */
        margin-top: 20px; /* Optional: Add some space above the button */
    }

    .btn-main {
        width: auto; /* Button width adjusts to content */
        padding: 10px 20px; /* Adjust padding to make the button more compact */
        display: inline-block; /* Ensure the button stays inline and not full-width */
        font-size: 16px; /* Adjust font size if needed */
        background-color: #007bff; /* Change button background color */
        color: white; /* Text color */
        border: 2px solid #007bff; /* Border color */
        border-radius: 25px; /* Rounded corners */
        transition: all 0.3s ease; /* Smooth transition for hover effect */
    }

    /* Hover effect for button */
    .btn-main:hover {
        background-color: #0056b3; /* Darker background color on hover */
        border-color: #0056b3; /* Darker border color on hover */
    }
</style>


                    <!-- Nút đăng nhập -->
                    <div class="mt-3">
                        <a href="dang-nhap" class="btn btn-secondary form-control">ĐÃ CÓ TÀI KHOẢN? ĐĂNG NHẬP</a>
                    </div>
                </div>
            </div>
        </div>
    

    <div class="footer mt-4">
        <small>IP: <?php echo $_IP; ?></small><br>
        <small>Design By NRORUN</small><br>
        <small>2023© Ngọc Rồng online</small>
    </div>

     <script src="assets/bootstrap/js/bootstrap.bundle.min.js"></script>

    <!-- JavaScript cho thông báo thành công và chuyển hướng -->
    <?php if (isset($_alert) && strpos($_alert, 'Đăng kí thành công') !== false): ?>
        <script>
            // Hiển thị thông báo thành công và thực hiện chuyển hướng sau 5 giây
            document.getElementById("success-alert").classList.add("show"); // Hiển thị thông báo thành công
            setTimeout(function() {
                window.location.href = "dang-nhap.php"; // Chuyển hướng sau 5 giây
            }, 5000);
        </script>
    <?php endif; ?>
</body>

</html>
