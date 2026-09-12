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
    <title>Ngọc Rồng RUN</title>
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
                <h4>MỞ THÀNH VIÊN</h4>
                <?php
                $mysqli = new mysqli("localhost", "root", "", "nro");

                if ($mysqli->connect_errno) {
                    echo "Failed to connect to MySQL: (" . $mysqli->connect_errno . ") " . $mysqli->connect_error;
                    exit();
                }
                // Check if account is already activated
                if ($_status == '1') {
                    $_alert = '<div class="text-danger pb-2 font-weight-bold">Tài khoản của bạn đã được kích hoạt!</div>';
                }
                // Check if account is not activated and balance is insufficient
                elseif (($_status == '0' || $_status == '-1') && $_coin < 20000) {
                    $_alert = '<div class="text-danger pb-2 font-weight-bold">Bạn không đủ 20.000 KCoin. Vui lòng nạp thêm tiền vào tài khoản để ' . ($_status == '0' ? 'kích hoạt nhé!' : 'mở lại tài khoản!</div>');
                }
                // Activate or unlock account
                elseif (($_status == '0' || $_status == '-1') && $_coin >= 20000) {
                    $coin = $_coin - 20000;
                    $stmt = $mysqli->prepare('UPDATE account SET active = 1, vnd = ? WHERE username = ?');
                    $stmt->bind_param('is', $coin, $_username);
                    if ($stmt->execute() && $stmt->affected_rows > 0) {
                        $_alert = '<div class="text-danger pb-2 font-weight-bold">Kích hoạt tài khoản thành công. Bây giờ bạn đã có thể đăng nhập vào game!</div>';
                        if ($_status == '-1') {
                            $_alert = '<div class="text-danger pb-2 font-weight-bold">Mở khóa tài khoản thành công. Bây giờ bạn đã có thể đăng nhập vào game!</div>';
                        }
                    } else {
                        $_alert = '<div class="text-danger pb-2 font-weight-bold">Có lỗi gì đó xảy ra. Vui lòng liên hệ Admin!</div>';
                    }
                }
                ?>
                <form id="form" method="POST">
                    <div> Thông tin mở thành viên:<br>- Mở thành viên với chỉ <strong>20.000 VNĐ</strong>. <img
                            src="image/hot.gif"><br>- Được miễn phí <strong>GiftCode Thành viên</strong>. <img
                            src="image/hot.gif"><br>- Tận hưởng trọn vẹn các tính năng. <img src="image/hot.gif"><br>-
                        Xây dựng, ủng hộ NgocRongLight.com hoạt động. </div>
                    <div id="notify" class="text-danger pb-2 font-weight-bold"></div>
                    <?php if (isset($_POST['submit']))
                        echo $_alert; ?>
                    <button class="btn btn-main form-control" id="btn" type="submit" name="submit">MỞ NGAY</button>
                </form>

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