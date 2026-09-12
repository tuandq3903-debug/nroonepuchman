<?php
include_once 'set.php';
include_once 'connect.php';
include('head.php');
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
                <h4>ĐỔI MẬT KHẨU</h4>
                <?php if ($_login === null) { ?>
                    <p>Bạn chưa đăng nhập? hãy đăng nhập để sử dụng chức năng này</p>
                <?php } else { ?>
                    <?php
                    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
                        // Lấy mật khẩu hiện tại của người dùng từ cơ sở dữ liệu
                        $stmt = $conn->prepare("SELECT password, mkc2 FROM account WHERE username=?");
                        $stmt->bind_param("s", $_username);
                        $stmt->execute();
                        $result = $stmt->get_result();
                        $row = $result->fetch_assoc();
                        $matkhaucu = $row['password'];
                        $mkc2 = $row['mkc2'];

                        $matKhauHienTai = $_POST['password'] ?? '';
                        $matKhauMoi = $_POST['new_password'] ?? '';
                        $xacNhanMatKhauMoi = $_POST['new_password_confirmation'] ?? '';

                        if (!empty($matkhaucu) && !empty($mkc2)) {
                            $matKhauCap2 = isset($_POST['passwordcap2']) ? $_POST['passwordcap2'] : '';

                            if (!empty($matKhauHienTai) && !empty($matKhauMoi) && !empty($xacNhanMatKhauMoi) && !empty($matKhauCap2)) {
                                // Xác thực mật khẩu hiện tại
                                if ($matKhauHienTai !== $matkhaucu) {
                                    echo "<div class='alert alert-danger'>Sai mật khẩu hiện tại</div>";
                                } elseif ($matKhauCap2 !== $mkc2) {
                                    echo "<div class='alert alert-danger'>Sai mật khẩu cấp 2</div>";
                                } elseif ($matKhauMoi === $matKhauHienTai) {
                                    echo "<div class='alert alert-danger'>Mật khẩu mới không được giống mật khẩu hiện tại</div>";
                                } elseif ($matKhauMoi !== $xacNhanMatKhauMoi) {
                                    echo "<div class='alert alert-danger'>Mật khẩu mới không giống nhau</div>";
                                } else {
                                    // Cập nhật mật khẩu mới vào cơ sở dữ liệu
                                    $stmt = $conn->prepare("UPDATE account SET password=? WHERE username=?");
                                    $stmt->bind_param("ss", $matKhauMoi, $_username);

                                    if ($stmt->execute()) {
                                        echo "<div class='alert alert-success'>Cập nhật mật khẩu mới thành công</div>";
                                        $matkhaucu = $matKhauMoi; // Cập nhật giá trị của biến matkhaucu sau khi cập nhật thành công
                                    } else {
                                        echo "<div class='alert alert-danger'>Lỗi khi cập nhật mật khẩu mới</div>";
                                    }
                                }
                            } else {
                                echo "<div class='alert alert-danger'>Vui lòng điền đầy đủ thông tin trong form</div>";
                            }
                        } else {
                            if (!empty($matKhauHienTai) && !empty($matKhauMoi) && !empty($xacNhanMatKhauMoi)) {
                                // Kiểm tra mật khẩu hiện tại
                                if ($matKhauHienTai !== $matkhaucu) {
                                    echo "<div class='alert alert-danger'>Sai mật khẩu hiện tại</div>";
                                } else {
                                    // Tạo mật khẩu mới
                                    $stmt = $conn->prepare("UPDATE account SET password=? WHERE username=?");
                                    $stmt->bind_param("ss", $matKhauMoi, $_username);

                                    if ($stmt->execute()) {
                                        echo "<div class='alert alert-success'>Tạo mật khẩu mới thành công</div>";
                                        $matkhaucu = $matKhauMoi; // Cập nhật giá trị của biến matkhaucu sau khi tạo thành công
                                    } else {
                                        echo "<div class='alert alert-danger'>Lỗi khi tạo mật khẩu mới</div>";
                                    }
                                }
                            } else {
                                echo "<div class='alert alert-danger'>Vui lòng điền đầy đủ thông tin trong form</div>";
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
                                <label class="font-weight-bold">Mật Khẩu Cấp 2:</label>
                                <input type="password" class="form-control" name="passwordcap2" id="passwordcap2"
                                    placeholder="Mật khẩu cấp 2" required autocomplete="passwordcap2">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Mật Khẩu Mới:</label>
                                <input type="password" class="form-control" name="new_password" id="new_password"
                                    placeholder="Mật khẩu mới" required autocomplete="new_password">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Xác Nhận Mật Khẩu Mới:</label>
                                <input type="password" class="form-control" name="new_password_confirmation"
                                    id="new_password_confirmation" placeholder="Xác nhận mật khẩu mới" required
                                    autocomplete="new_password_confirmation">
                            </div>
                            <button class="btn btn-sm btn-main form-control" type="submit">Thực hiện</button>
                        </form>
                    <?php } else { ?>
                        <form method="POST">
                            <div class="mb-3">
                                <label class="font-weight-bold">Mật Khẩu Hiện Tại:</label>
                                <input type="password" class="form-control" name="password" id="password"
                                    placeholder="Mật khẩu hiện tại" required autocomplete="password">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Mật Khẩu Mới:</label>
                                <input type="password" class="form-control" name="new_password" id="new_password"
                                    placeholder="Mật khẩu mới" required autocomplete="new_password">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Nhập Lại Mật Khẩu Mới:</label>
                                <input type="password" class="form-control" name="new_password_confirmation"
                                    id="new_password_confirmation" placeholder="Xác nhận mật khẩu mới" required
                                    autocomplete="new_password_confirmation">
                            </div>
                            <button class="btn btn-sm btn-main form-control" type="submit">Thực hiện</button>
                        </form>
                    <?php }
                } ?>
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