<?php
include_once 'set.php';

$_title = "Ngọc Rồng Z - Đăng Nhập";

// Check if the session is not already started
if (session_status() == PHP_SESSION_NONE) {
    session_start();
}

if ($_login == null) {
    if (isset($_POST['username'])) {
        $username = htmlspecialchars(trim($_POST['username']));
        $password = htmlspecialchars(trim($_POST['password']));
        $captcha_input = htmlspecialchars(trim($_POST['captcha'])); // Get captcha input

        // Check if captcha is valid
        if ($captcha_input != $_SESSION['captcha_code']) {
            $_alert = '<div class="text-danger pb-2 font-weight-bold">Mã xác minh không chính xác!</div>';
        } else {
            // Check if username and password are valid
            if (!ctype_alnum($username)) {
                $_alert = '<div class="text-danger pb-2 font-weight-bold">Tên đăng nhập chỉ được chứa kí tự và số!</div>';
            } else {
                $select = _fetch(_select("*", 'account', "username='$username'"));

                if ($select != null && $select['password'] == $password) {
                    // Check if the account has a character created
                    $account_id = $select['id'];
                    $result = _fetch(_select("*", 'player', "`account_id`='$account_id'"));

                    if ($result != null) {
                        $_SESSION['account'] = $username;
                        $_SESSION['id'] = $select['id'];
                        header('Location: /dien-dan');
                        exit; // Always call exit after header
                    } else {
                        $_alert = '<div class="text-danger pb-2 font-weight-bold">Tài khoản này chưa tạo nhân vật!</div>';
                    }
                } else {
                    $_alert = '<div class="text-danger pb-2 font-weight-bold">Tên đăng nhập hoặc mật khẩu không hợp lệ, vui lòng kiểm tra lại!</div>';
                }
            }
        }

    } elseif (isset($_POST['submit'])) {
        $_alert = '<div class="text-danger pb-2 font-weight-bold">Vui lòng nhập tên đăng nhập, mật khẩu và mã xác minh!</div>';
    }
} else {
    header("Location: /");
    exit;
}

// Generate a random CAPTCHA string
function generateCaptcha($length = 5) {
    $characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    $captcha = '';
    for ($i = 0; $i < $length; $i++) {
        $captcha .= $characters[rand(0, strlen($characters) - 1)];
    }
    return $captcha;
}

// Generate CAPTCHA and store it in session
$_SESSION['captcha_code'] = generateCaptcha();
?>


<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $_title; ?></title>
    <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css">
    <style>
        /* Set background image */
        body {
            background-image: url('https://imgur.com/LsKmxtU.png'); /* Đặt đường dẫn đến hình ảnh của bạn */
            background-size: cover; /* Phủ toàn bộ trang */
            background-position: center; /* Căn giữa hình ảnh */
            background-repeat: no-repeat; /* Không lặp lại hình ảnh */
            background-attachment: fixed; /* Hình ảnh sẽ không di chuyển khi scroll */
        }

        .login-container {
            border-radius: 15px;
            background-color: rgba(255, 175, 76, 0.9); /* Màu nền bán trong suốt để dễ đọc */
            padding: 20px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }

        .login-form {
            background-color: #2dbe60;
            border-radius: 15px;
            padding: 20px;
        }

        .login-form input,
        .login-form button {
            border-radius: 8px;
        }

        .footer {
            font-size: 13px;
            color: #444;
            text-align: center;
        }

        .footer small {
            color: #888;
        }

        .alert {
            margin-top: 10px;
        }

        .back-link {
            display: inline-block;
            padding: 10px 20px;
            background-color: #2dbe60;
            color: #fff;
            font-size: 16px;
            border-radius: 8px;
            text-align: center;
            text-decoration: none;
            margin-bottom: 20px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            transition: background-color 0.3s ease;
        }

        .back-link:hover {
            background-color: #E8D3E3;
            text-decoration: none;
        }

        .back-link:active {
            background-color: #E8D3E3;
        }
    </style>
</head>

<body>
  <div class="container mt-5">
    <!-- Added Back to Home Link styled as button -->
    <div class="d-flex justify-content-start mb-3">
        <a href="/" class="back-link home-link">
            <i class="fas fa-home"></i> Trở lại trang chủ
        </a>
        <a href="dang-ky" class="back-link register-link">
            <i class="fas fa-register_tick_function"></i> Đăng Ký Tài Khoản
        </a>
    </div>

    

<!-- Add your CSS below -->
<style>
    .back-link {
        text-decoration: none;
        font-size: 16px;
        font-weight: bold;
        padding: 10px 20px; /* Add padding to make the link look like a button */
        border: 2px solid white; /* Add a white border */
        border-radius: 25px; /* Round the corners */
        color: white; /* White text color */
        display: inline-flex;
        align-items: center;
        margin-right: 20px; /* Space between links */
        transition: all 0.3s ease; /* Smooth transition effect */
    }

    .back-link i {
        margin-right: 8px; /* Space between icon and text */
    }

    /* Hover effect for both links */
    .back-link:hover {
        background-color: rgba(255, 255, 255, 0.2); /* Light background on hover */
        color: white; /* Keep text white */
        border-color: #ff5733; /* Change border color on hover */
    }

    /* Flashing color effect for both links */
    .home-link, .register-link {
        animation: flashColors 1.5s infinite alternate;
    }

    

    /* Scrolling text effect */
    .scrolling-text {
        font-size: 18px;
        color: #ffffff;
        font-weight: bold;
        white-space: nowrap; /* Prevents text from wrapping */
        overflow: hidden; /* Hides text that is outside the container */
        width: 100%; /* Ensures full width */
        animation: scrollText 10s linear infinite; /* Controls the scroll speed and direction */
    }

    @keyframes scrollText {
        0% {
            transform: translateX(100%); /* Starts from the right */
        }
        100% {
            transform: translateX(-100%); /* Ends on the left */
        }
    }
</style>


        <div class="login-container">
            <div class="login-form">
                <h4 class="text-white text-center">ĐĂNG NHẬP</h4>
                <form method="POST">
                    <div class="form-group">
                        <label class="text-white">Tài khoản:</label>
                        <input class="form-control" type="text" name="username" id="username" placeholder="Nhập tài khoản">
                    </div>
                    <div class="form-group">
                        <label class="text-white">Mật khẩu:</label>
                        <input class="form-control" type="password" name="password" id="password" placeholder="Nhập mật khẩu">
                    </div>
                    <div class="form-check form-group">
                        <label class="form-check-label text-white">
                            <input class="form-check-input" type="checkbox" name="accept" id="accept" checked="true">
                            Ghi nhớ đăng nhập
                        </label>
                    </div>

                    <!-- CAPTCHA Section -->
                  <div class="form-group d-flex align-items-center justify-content-end">
    <label class="text-white mr-3"></label>
    
    <!-- Adjusting the CAPTCHA container to sit next to the input and align to the right -->
    <div class="form-control" style="text-align: center; font-size: 12px; background-color: #E8D3E3; width: 80px; padding: 5px; margin-right: 10px;">
        <?php echo $_SESSION['captcha_code']; ?>
    </div>

    <!-- CAPTCHA input field -->
    <input class="form-control" type="text" name="captcha" id="captcha" placeholder="Nhập mã xác minh" style="font-size: 16px; padding: 10px;">
</div>


 
                      

                    <?php if (!empty($_alert)) {
                        echo $_alert;
                    } ?>

                  <div class="form-group text-center">
    <div id="notify" class="text-danger pb-1 font-weight-bold" style="font-size: 10px; padding: 3px 0;"></div>
    <button class="btn btn-dark" type="submit" style="font-size: 12px; padding: 6px 12px; width: auto;">ĐĂNG NHẬP</button>
	
</div>

                </form>
            </div>

          <style>
    .footer small {
        font-weight: bold;
        color: black; /* Set text color to black */
    }
</style>

<div class="footer mt-4">
    <small>IP: <?php echo $_IP; ?></small><br>
    <small>Design By NRORUN</small><br>
    <small>2023© Ngọc Rồng online</small>
</div>


        </div>
    </div>

    <script src="assets/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="asset/main.js"></script>
</body>

</html>
