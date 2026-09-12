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


    <style>
        th,
        td {
            white-space: nowrap;
            padding: 2px 4px !important;
            font-size: 11px;
        }
    </style>
    <div class="p-1 mt-1 alert alert-danger" style="border-radius: 7px; box-shadow: 0px 0px 5px black;">
    <div class="container color-forum pt-2">
        <div class="row">
            <div class="col">
                <h6 class="text-center">LỊCH SỬ NẠP THẺ</h6>
                <table class="table table-borderless text-center">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>TÀI KHOẢN</th>
                            <th>MỆNH GIÁ</th>
                            <th>LOẠI THẺ</th>
                            <th>TRẠNG THÁI</th>
                            <th>THỜI GIAN</th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php
                        if (isset($_username)) {
                            $stmt = $conn->prepare("SELECT * FROM trans_log WHERE name = ? ORDER BY id DESC LIMIT 10");
                            $stmt->bind_param("s", $_username);
                            $stmt->execute();
                            $result = $stmt->get_result();

                            if ($result->num_rows > 0) {
                                echo '<tbody>';

                                while ($row = $result->fetch_assoc()) {
                                    $status = '';

                                    switch ($row['status']) {
                                        case 1:
                                            $status = '<span>Thành Công</span>';
                                            break;
                                        case 2:
                                            $status = '<span>Thất Bại</span>';
                                            break;
                                        case 3:
                                            $status = '<span>Sai Mệnh Giá</span>';
                                            break;
                                        default:
                                            $status = '<span>Chờ Duyệt</span>';
                                    }

                                    echo '<tr>
                                    <td>' . $row['id'] . '</td>
                                    <td>' . $row['name'] . '</td>
                                    <td>' . number_format($row['amount']) . 'đ</td>
                                    <td>' . $row['type'] . '</td>
                                    <td>' . $status . '</td>
                                    <td>' . $row['date'] . '</td>
                                    </tr>';
                                }

                                echo '</tbody>';
                            } else {
                                echo '<tbody>
                                <tr>
                                   <td colspan="6" align="center"><span style="font-size:100%;"><< Lịch Sử Trống >></span></td>
                                </tr>
                               </tbody>';
                            }
                        } else {
                            echo 'Chưa có tên người dùng được cung cấp.';
                        }
                        ?>
                    </tbody>
                </table>
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