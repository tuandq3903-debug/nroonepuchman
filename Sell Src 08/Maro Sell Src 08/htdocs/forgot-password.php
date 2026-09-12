<?php
require_once 'connect.php';
require_once 'cauhinh.php';
require_once 'head.php';
require_once 'set.php';
?>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8">
    <title>Ngọc Rồng Hades</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" type="image/x-icon" href="assets/images/icon/icon.ico">


    <!-- bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <!-- icon -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <!-- jquery -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.1.min.js" integrity="sha256-o88AwQnZB+VDvE9tvIXrMQaPlFFSUTR+nldQm1LuPXQ=" crossorigin="anonymous"></script>
    <!-- mycss -->
    <link rel="stylesheet" href="css/mystyle.css">
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <!-- myjs -->
    <!--<script src="js/tet.js"></script>-->

    <!-- bootstrap -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
</head>

<div class="p-1 mt-1 alert alert-danger" style="border-radius: 7px; box-shadow: 0px 0px 5px black;">
        <div class="container pt-5 pb-5">
            <div class="row">
                <div class="col-lg-6 offset-lg-3">
                    <h4 class="text-center">QUÊN MẬT KHẨU</h4>
                    <?php
                    require 'cauhinh.php';
                    require 'vendor/autoload.php';
                    require 'vendor/phpmailer/phpmailer/src/Exception.php';
                    require 'vendor/phpmailer/phpmailer/src/PHPMailer.php';
                    require 'vendor/phpmailer/phpmailer/src/SMTP.php';
                    use PHPMailer\PHPMailer\PHPMailer;
                    use PHPMailer\PHPMailer\Exception;

                    function generateRandomPassword($length = 6)
                    {
                        $characters = '0123456789';
                        $password = '';

                        for ($i = 0; $i < $length; $i++) {
                            $password .= $characters[rand(0, strlen($characters) - 1)];
                        }

                        return $password;
                    }

                    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
                        $username = $_POST['username'];
                        $gmail = $_POST['gmail'];

                        // Thực hiện kết nối tới cơ sở dữ liệu MySQL
                        $conn = new mysqli("localhost", "root", "", "privates");
                        if ($conn->connect_error) {
                            die("Kết nối thất bại: " . $conn->connect_error);
                        }

                        // Tạo một mật khẩu mới ngẫu nhiên
                        $newPassword = generateRandomPassword();

                        // Cập nhật mật khẩu mới vào cơ sở dữ liệu
                        $updateQuery = "UPDATE account SET password = '$newPassword' WHERE username = '$username' AND gmail = '$gmail'";
                        if ($conn->query($updateQuery) === TRUE) {
                            // Tạo một đối tượng PHPMailer và cấu hình
                            $mail = new PHPMailer(true);
                            try {
                                // Cấu hình gửi email thông qua Gmail
                                $mail->SMTPDebug = 0;
                                $mail->isSMTP();
                                $mail->Host = 'smtp.gmail.com';
                                $mail->SMTPAuth = true;
                                $mail->Username = 'ngocrongdragonking@gmail.com'; // Thay đổi thành email của bạn
                                $mail->Password = 'erlthhyxknomaxgx'; // Thay đổi thành mật khẩu của bạn
                                $mail->SMTPSecure = 'tls';
                                $mail->Port = 587;

                                // Thiết lập thông tin email
                                $mail->setFrom('37quantran@gmail.com', 'Ngọc Rồng Universe'); // Thay đổi thành email của bạn và tên của bạn
                                $mail->addAddress($gmail);
                                $mail->Subject = '=?UTF-8?B?' . base64_encode('Quên Mật Khẩu - Ngọc Rồng Universe') . '?=';
                                $mail->Body = "Xin chào bạn,\n\nTài khoản $username đang thực hiện Quên mật khẩu.\n\nThông tin tài khoản của bạn:\n- Tài khoản: $username \n- Mật khẩu mới: $newPassword \n\nAdmin chân thành cảm ơn bạn đã tin tưởng và đồng hành cùng " . $_tenmaychu . "!\n\n" . $_tenmaychu;
                                $mail->CharSet = 'UTF-8';
                                $mail->Encoding = 'base64';

                                // Gửi thư
                                $mail->send();
                                echo "Email đã được gửi thành công đến địa chỉ: " . $gmail;
                            } catch (Exception $e) {
                                echo "Có lỗi xảy ra khi gửi email: " . $mail->ErrorInfo;
                            }
                        } else {
                            echo "Không tìm thấy thông tin trong cơ sở dữ liệu hoặc không thể cập nhật mật khẩu mới.";
                        }

                        // Đóng kết nối cơ sở dữ liệu
                        $conn->close();
                    }
                    ?>
                    <form id="form" method="POST">
                        <div class="form-group">
                            <label>Tài khoản:</label>
                            <input class="form-control" type="text" name="username" id="username"
                                placeholder="Nhập tên tài khoản">
                        </div>
                        <div class="form-group">
                            <label>Gmail:</label>
                            <input class="form-control" type="gmail" name="gmail" id="gmail"
                                placeholder="Nhập Gmail của bạn">
                        </div>
                        <div id="notify" class="text-danger pb-2 font-weight-bold"></div>
                        <button class="btn btn-main form-control" type="submit">XÁC NHẬN</button>
                    </form>
                    <br>
                    <div class="text-center">
                        <p>Bạn đã lấy lại tài khoản? <a href="/dang-nhap">Đăng nhập tại đây</a></p>
                    </div>
                </div>
            </div>
        </div>
	</div>
          <footer class="mt-1">


            <div class="text-center mt-1">
                <b style="font-size:13px;" class="text-white">Tham gia cộng đồng giao lưu game với chúng tớ.</b>
                <br>
                <a href="" target="_blank"><img src="assets/images/icon/findondiscord.png" style="max-width:100px" class="mt-1"></a>
                <a href="https://www.facebook.com/groups/ngocronghades" target="_blank"><img src="assets/images/icon/findonfb.png" style="max-width:100px" class="mt-1"></a>
            </div>
            <div class="text-center text-white">
                Trò chơi không có bản quyền chính thức, hãy cân nhắc kỹ trước khi tham gia.<br> Chơi quá 180 phút một ngày sẽ ảnh hưởng đến sức khỏe.
            </div>
        </footer>
    <script src="assets/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="asset/main.js"></script>
</body>

</html>