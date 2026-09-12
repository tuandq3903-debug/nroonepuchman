<?php
include_once 'set.php';
include_once 'connect.php';
include 'head.php';
if ($_login === null) {
    // Chưa đăng nhập, chuyển hướng đến trang khác bằng JavaScript
    echo '<script>window.location.href = "../dang-nhap";</script>';
    exit(); // Đảm bảo dừng thực thi code sau khi chuyển hướng
}


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
                    <h4>BẢO MẬT CẤP 2 - BẢO VỆ TÀI KHOẢN</h4>
                    <?php
                    $stmt = $conn->prepare("SELECT password, mkc2 FROM account WHERE username=?");
                    $stmt->bind_param("s", $_username);
                    $stmt->execute();
                    $result = $stmt->get_result();
                    $row = $result->fetch_assoc();
                    $primaryPassword = $row['password'];
                    $mkc2 = $row['mkc2'];

                    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
                        $password = $_POST['password'] ?? '';
                        $new_passwordcap2 = $_POST['new_passwordcap2'] ?? '';
                        $new_passwordcap2xacnhan = $_POST['new_passwordcap2xacnhan'] ?? '';

                        if (!empty($mkc2)) {
                            $old_passwordcap2 = isset($_POST['old_passwordcap2']) ? $_POST['old_passwordcap2'] : '';

                            if (!empty($password) && !empty($new_passwordcap2) && !empty($new_passwordcap2xacnhan) && !empty($old_passwordcap2)) {
                                // Kiểm tra xem mật khẩu hiện tại nhập vào có giống với mật khẩu trong database không.
                                // Nếu sai, in ra thông báo lỗi.
                                if ($password !== $primaryPassword) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Sai mật khẩu hiện tại</div>";
                                } elseif ($old_passwordcap2 !== $mkc2) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Sai mật khẩu cấp 2 hiện tại</div>";
                                } elseif ($new_passwordcap2 === $password) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Mật khẩu cấp 2 không được giống mật khẩu hiện tại</div>";
                                } elseif ($new_passwordcap2 !== $new_passwordcap2xacnhan) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Mật khẩu cấp 2 không giống nhau</div>";
                                } elseif ($password === $new_passwordcap2) { // Kiểm tra mật khẩu hiện tại có trùng với mật khẩu mới hay không.
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Mật khẩu cấp 2 phải khác với mật khẩu hiện tại</div>";
                                } else {
                                    // Cập nhật mật khẩu cấp 2 lên database
                                    $stmt = $conn->prepare("UPDATE account SET mkc2=? WHERE username=?");
                                    $stmt->bind_param("ss", $new_passwordcap2, $_username);

                                    if ($stmt->execute()) {
                                        echo "<div class='text-danger pb-2 font-weight-bold'>Cập nhật mật khẩu cấp 2 thành công</div>";
                                    } else {
                                        echo "<div class='text-danger pb-2 font-weight-bold'>Lỗi khi cập nhật mật khẩu cấp 2</div>";
                                    }
                                }
                            } else {
                                echo "<div class='text-danger pb-2 font-weight-bold'>Vui lòng điền đầy đủ thông tin trong form</div>";
                            }
                        } else {
                            if (!empty($password) && !empty($new_passwordcap2) && !empty($new_passwordcap2xacnhan)) {
                                if ($password !== $primaryPassword) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Sai mật khẩu hiện tại</div>";
                                } elseif ($new_passwordcap2 === $password) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Mật khẩu cấp 2 không được giống mật khẩu hiện tại</div>";
                                } elseif ($new_passwordcap2 !== $new_passwordcap2xacnhan) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Mật khẩu cấp 2 không giống nhau</div>";
                                } else {
                                    $stmt = $conn->prepare("UPDATE account SET mkc2=? WHERE username=?");
                                    $stmt->bind_param("ss", $new_passwordcap2, $_username);

                                    if ($stmt->execute()) {
                                        echo "<div class='text-danger pb-2 font-weight-bold'>Cập nhật mật khẩu cấp 2 thành công</div>";
                                    } else {
                                        echo "<div class='text-danger pb-2 font-weight-bold'>Lỗi khi cập nhật mật khẩu cấp 2</div>";
                                    }
                                }
                            } else {
                                echo "<div class='text-danger pb-2 font-weight-bold'>Vui lòng điền đầy đủ thông tin trong form</div>";
                            }
                        }
                    }

                    if (!empty($mkc2)) {
                        ?>
                        <form method="POST">
                            <div class="mb-3">
                                <label class="font-weight-bold">Mật Khẩu hiện tại:</label>
                                <input type="password" class="form-control" name="password" id="password"
                                    placeholder="Mật khẩu hiện tại" required autocomplete="password">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Mật Khẩu Cấp 2 Hiện Tại:</label>
                                <input type="password" class="form-control" name="old_passwordcap2" id="old_passwordcap2"
                                    placeholder="Mật khẩu cấp 2 hiện tại" required autocomplete="old-passwordcap2">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Mật Khẩu Cấp 2 Mới:</label>
                                <input type="password" class="form-control" name="new_passwordcap2" id="new_passwordcap2"
                                    placeholder="Mật khẩu cấp 2 mới" required autocomplete="new-passwordcap2">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Xác Nhận Mật Khẩu Cấp 2:</label>
                                <input type="password" class="form-control" name="new_passwordcap2xacnhan"
                                    id="new_passwordcap2xacnhan" placeholder="Xác nhận mật khẩu cấp 2 mới" required
                                    autocomplete="new-passwordcap2xacnhan">
                            </div>
                            <button class="btn btn-main form-control" type="submit">Thực hiện</button>
                        </form>
                    <?php } else { ?>
                        <form method="POST">
                            <div class="mb-3">
                                <label class="font-weight-bold">Mật Khẩu Hiện Tại:</label>
                                <input type="password" class="form-control" name="password" id="password"
                                    placeholder="Mật khẩu hiện tại" required autocomplete="password">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Mật Khẩu Cấp 2:</label>
                                <input type="password" class="form-control" name="new_passwordcap2" id="new_passwordcap2"
                                    placeholder="Mật khẩu cấp 2" required autocomplete="new-passwordcap2">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Nhập Lại Mật Khẩu Cấp 2:</label>
                                <input type="password" class="form-control" name="new_passwordcap2xacnhan"
                                    id="new_passwordcap2xacnhan" placeholder="Xác nhận mật khẩu cấp 2" required
                                    autocomplete="new-passwordcap2xacnhan">
                            </div>
                            <button class="btn btn-main form-control" type="submit">Thực hiện</button>
                        </form>
                    <?php } ?>
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
    <script src="assets/main.js"></script>
</body><!-- Bootstrap core JavaScript -->

</html>