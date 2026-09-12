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
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Trang Chủ Chính Thức - Ngọc Rồng Light</title>
    <meta name="description" content="">
    <meta name="author" content="">
    <base href="">
    <meta name="description"
        content="Website chính thức của Chú Bé Rồng Online – Game Bay Vien Ngoc Rong Mobile nhập vai trực tuyến trên máy tính và điện thoại về Game 7 Viên Ngọc Rồng hấp dẫn nhất hiện nay!">
    <meta name="keywords"
        content="Chú Bé Rồng Online,ngoc rong mobile, game ngoc rong, game 7 vien ngoc rong, game bay vien ngoc rong">
    <meta name="twitter:card" content="summary">
    <meta name="twitter:title"
        content="Website chính thức của Chú Bé Rồng Online – Game Bay Vien Ngoc Rong Mobile nhập vai trực tuyến trên máy tính và điện thoại về Game 7 Viên Ngọc Rồng hấp dẫn nhất hiện nay!">
    <meta name="twitter:description"
        content="Website chính thức của Chú Bé Rồng Online – Game Bay Vien Ngoc Rong Mobile nhập vai trực tuyến trên máy tính và điện thoại về Game 7 Viên Ngọc Rồng hấp dẫn nhất hiện nay!">
    <meta name="twitter:image" content="image/logo.png">
    <meta name="twitter:image:width" content="200">
    <meta name="twitter:image:height" content="200">
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/fontawesome-free/css/all.min.css" rel="stylesheet" type="text/css">
    <script src="assets/jquery/jquery.min.js"></script>
    <script src="assets/notify/notify.js"></script>
    <link rel="icon" href="image/icon.png?v=99">
    <link href="assets/main.css" rel="stylesheet">
</head>


    <div class="container color-forum pt-1 pb-1">
        <div class="row">
            <div class="col"> <a href="dien-dan" style="color: white">Quay lại diễn đàn</a> </div>
        </div>
    </div>
    <div class="container pt-5 pb-5">
        <div class="row">
            <div class="col-lg-6 offset-lg-3">
                <?php if ($_login === null) { ?>
                    <p>Bạn chưa đăng nhập? Hãy đăng nhập để dùng được chức năng này</p>
                <?php } else { ?>
                    <h4>Nạp Tiền - Mbbank</h4>
                    <div>Thông tin nạp Mbbank:<br>
                        <br>
                        <p><strong>- Tên Tài Khoản:</strong>
                            <?php echo $_taikhoanmm; ?>
                        </p>
                        <p> <strong>- Ngân Hàng:</strong>
                            <?php echo $_mbbank; ?>
                        </p>
                        <p><strong>- Số Tài Khoản:</strong>
                            <?php echo $_phonemomo; ?>
                        </p>
                        <p><strong>- Nội Dung:</strong>
                            <?php echo $_username ?>
                        </p>
                        <br>
                        - Xây dựng, ủng hộ nro hoạt động.
                        <br>- Momo Bao Tri Cong Nap.
                    </div>

                    <br>
                    <a class="btn btn-sm btn-main form-control" style="border-radius:10px" href="mbbank">Xác
                        nhận đã chuyển khoản</a>
                    <br>
                    <p><i>Khi chuyển tiền xong nhấn xác nhận đã chuyển khoản để xác thực
                            giao dịch
                            nhé!.</i>
                    </p>
                    <p><i>Khi xác thực xong làm mới trang sau 1 - 3 phút để cập nhật
                            KCoin.</i>
                    </p>
                    </small>
                <?php } ?>
            </div>
        </div>
    </div>
    <div class=" border-secondary border-top">
    </div>
    <div class="container pt-4 pb-4 text-white">
        <div class="row">
            <div class="col">
                <div class="text-center">
                    <div style="font-size: 13px" class="text-dark">
                        <?php
                        echo $_group;
                        echo $_fanpage;
                        echo $_copyright;
                        ?>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    <script src="assets/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/main.js"></script>
</body><!-- Bootstrap core JavaScript -->

</html>