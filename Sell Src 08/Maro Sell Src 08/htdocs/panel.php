<?php
include_once 'set.php';
include_once 'connect.php';
include_once 'head.php';
if ($_login === null) {
    // Chưa đăng nhập, chuyển hướng đến trang khác bằng JavaScript
    echo '<script>window.location.href = "../dang-nhap";</script>';
    exit(); // Đảm bảo dừng thực thi code sau khi chuyển hướng
}

// Rest of the code
$_active = (isset($_active)) ? $_active : null;
$_tcoin = (isset($_tcoin)) ? $_tcoin : 0;


$id_user = "SELECT COUNT(id) AS id FROM account";
$user = mysqli_query($conn, $id_user);

if ($user) {
    $row_user = mysqli_fetch_assoc($user);
    $id = $row_user['id'];
} else {
    echo "Lỗi truy vấn SQL: " . mysqli_error($conn);
    exit;
}

$result = _select("COUNT(*) as ban", "account", "ban = 1");
$row = _fetch($result);
$_tongban = $row["ban"];

$result2 = _select("COUNT(*) as active", "account", "active = 1");
$row = _fetch($result2);
$_tongactive = $row["active"];

$sql = "SELECT COUNT(*) AS recaf FROM account WHERE recaf IS NOT NULL";
$result = mysqli_query($conn, $sql);
$row = mysqli_fetch_assoc($result);
$_recaf = $row['recaf'];

function get_user_ip()
{
    if (!empty($_SERVER['HTTP_X_FORWARDED_FOR'])) {
        $addr = explode(",", $_SERVER['HTTP_X_FORWARDED_FOR']);
        return trim($addr[0]);
    } else {
        return $_SERVER['REMOTE_ADDR'];
    }
}

?>
<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8">
    <title>Ngọc Rồng Hades</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" type="image/x-icon" href="../assets/images/icon/icon.ico">


    <!-- bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <!-- icon -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <!-- jquery -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.1.min.js" integrity="sha256-o88AwQnZB+VDvE9tvIXrMQaPlFFSUTR+nldQm1LuPXQ=" crossorigin="anonymous"></script>
    <!-- mycss -->
    <link rel="stylesheet" href="../css/mystyle.css">
    <script src="//cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <!-- myjs -->
    <!--<script src="js/tet.js"></script>-->

    <!-- bootstrap -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
</head>

<div class="p-1 mt-1 alert alert-danger" style="border-radius: 7px; box-shadow: 0px 0px 5px black;">
    
    <?php if ($_login === null) { ?>
        <p>Bạn chưa đăng nhập? Hãy đăng nhập để sử dụng chức năng này.</p>
    <?php } else { ?>
        <div class="container pt-5 pb-5" id="pageHeader">
            <h4>MENU ADMIN</h4>
            <div class="row pb-2 pt-2">
                <div class="col-lg-6 text-left">
                    <p>Tổng tài khoản:
                        <?php echo $id; ?>
                    </p>
                    <p>Thành viên:
                        <?php echo $_tongactive; ?>
                    </p>
                    <p>Tài khoản vi phạm:
                        <?php echo $_tongban; ?>
                    </p>
                </div>
                <div class="col-lg-6">
                    <?php
                    $month = date('m');
                    $query = "SELECT SUM(amount) AS tongtiennap FROM trans_log WHERE status = 1 AND MONTH(date) = $month";
                    $result = mysqli_query($conn, $query);
                    $row = mysqli_fetch_assoc($result);
                    $tongtienthangnay = $row['tongtiennap'];

                    $tienthangnay = number_format($tongtienthangnay);

                    $query = "SELECT name, SUM(amount) AS topnap FROM trans_log WHERE status = 1 AND MONTH(date) = $month GROUP BY name ORDER BY topnap DESC LIMIT 3";
                    $result = mysqli_query($conn, $query);

                    if (mysqli_num_rows($result) > 0) {
                        echo "<p class='mb-2'>Danh Sách Top Nạp Thẻ Tháng:</p>";
                        echo "<ol class='list-unstyled'>";

                        $count = 1; // Biến đếm
                
                        while ($row = mysqli_fetch_assoc($result)) {
                            $name = $row['name'];
                            $topnap = $row['topnap'];

                            $tinhtopnap = number_format($topnap);

                            echo "<li class='mb-1'>TOP $count: $name - Tổng nạp: <span class='amount'>$tinhtopnap VNĐ</span></li> ";

                            $count++; // Tăng biến đếm sau mỗi lần lặp
                        }
                        echo "</ol><hr>";
                        echo "<span class='mb-3'>- Tổng doanh thu tháng này: </span>$tienthangnay VNĐ";
                    } else {
                        echo "<p>Không có tài khoản nạp vào tháng này.</p>";
                    }
    }
    ?>

            </div>
        </div>
    </div>

    <div class="container pt-5 pb-5 mt-5">
        <div class="row">
            <div class="col-lg-6 offset-lg-3">
                <div class="text-center mb-2">
                    
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
    <script src="../assets/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../assets/main.js"></script>
</body><!-- Bootstrap core JavaScript -->

</html>