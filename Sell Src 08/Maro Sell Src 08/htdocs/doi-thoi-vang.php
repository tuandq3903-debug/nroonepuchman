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
                <h4>ĐỔI THỎI VÀNG - TỰ ĐỘNG</h4>
                <?php if ($_login === null) { ?>
                    <p>Bạn chưa đăng nhập? hãy đăng nhập để sử dụng chức năng này</p>
                <?php } else { ?>
                    <span>- Máy chủ chưa hỗ trợ đổi trực tiếp khi đang trực tuyến trong máy chủ! </span>
                    <p>- Vui lòng thoát game tránh lỗi khi đổi nhé:3 </p>
                    <form method="POST">
                        <label for="">Tên tài khoản:</label>
                        <input type="text" value="<?php echo $_username; ?>" class="form-control"
                            readonly autocomplete="text"></input>
                        <br>
                        <label for="vnd_amount">Số Dư Cần Đổi:</label>
                        <select class="form-control form-control-alternative" name="vnd_amount" id="vnd_amount" required>
                            <option value="">Chọn Số Dư</option>
                            <option value="10000">10,000 VNĐ</option>
                            <option value="20000">20,000 VNĐ</option>
                            <option value="30000">30,000 VNĐ</option>
                            <option value="50000">50,000 VNĐ</option>
                            <option value="100000">100,000 VNĐ</option>
                            <option value="200000">200,000 VNĐ</option>
                            <option value="300000">300,000 VNĐ</option>
                            <option value="500000">500,000 VNĐ</option>
                            <option value="1000000">1,000,000 VNĐ</option>
                        </select>
                        <label>Số thỏi vàng sẽ nhận: <span class="font-weight-bold" id="gold">0</span> thỏi</label>
                        <br>
                        <button class="btn btn-main form-control" name="doithoivang" type="submit">Thực hiện</button>
                    </form>
                    <?php
                    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
                        if (isset($_POST['doithoivang'])) {
                            $account_id = $_SESSION['id'];
                            $vnd_amount = $_POST['vnd_amount'];

                            // Mệnh giá và số lượng thỏi vàng tương ứng
                            $options = array(
                                array("amount" => 10000, "quantity" => 25),
                                array("amount" => 20000, "quantity" => 60),
                                array("amount" => 30000, "quantity" => 90),
                                array("amount" => 50000, "quantity" => 160),
                                array("amount" => 100000, "quantity" => 360),
                                array("amount" => 200000, "quantity" => 670),
                                array("amount" => 500000, "quantity" => 1700),
                                array("amount" => 1000000, "quantity" => 3500),
                            );

                            $gold_quantity = 0;
                            foreach ($options as $option) {
                                if ($option["amount"] == $vnd_amount) {
                                    $gold_quantity = $option["quantity"];
                                    break;
                                }
                            }

                            if ($gold_quantity > 0) {
                                // Lấy dữ liệu tài khoản từ cơ sở dữ liệu
                                $account_query = "SELECT * FROM account WHERE id = $account_id LIMIT 1";
                                $account_result = mysqli_query($conn, $account_query);

                                if (mysqli_num_rows($account_result) > 0) {
                                    $account_data = mysqli_fetch_assoc($account_result);
                                    $current_vnd = $account_data['vnd'];

                                    // Kiểm tra hành trang đầy
                                    $player_query = "SELECT * FROM player WHERE account_id = $account_id LIMIT 1";
                                    $player_result = mysqli_query($conn, $player_query);

                                    if (mysqli_num_rows($player_result) > 0) {
                                        $player_data = mysqli_fetch_assoc($player_result);
                                        $_items_bag = $player_data['items_bag'];
                                        $replacement = "[457,$gold_quantity,\\\"[\\\\\\\\\\\"[73,1]\\\\\\\\\\\"]\\\"";
                                        $_items_bag = preg_replace('/\[-1,0,\\\"\[\]\\\"/', $replacement, $_items_bag, 1, $count);

                                        if ($count > 0) {
                                            // Kiểm tra số dư VND đủ để trừ
                                            if ($current_vnd >= $vnd_amount) {
                                                // Cập nhật số dư VND
                                                $new_vnd = $current_vnd - $vnd_amount;
                                                $update_query = "UPDATE account SET vnd = $new_vnd WHERE id = $account_id";
                                                mysqli_query($conn, $update_query);

                                                $escaped_items_bag = mysqli_real_escape_string($conn, $_items_bag);
                                                $update_query = "UPDATE player SET items_bag = '$escaped_items_bag' WHERE account_id = $account_id";
                                                mysqli_query($conn, $update_query);

                                                echo '<div class="text-danger pb-2 font-weight-bold">';
                                                echo "Đổi quà thành công! Nhận được $gold_quantity thỏi vàng.";
                                                echo '</div>';
                                            } else {
                                                echo '<div class="text-danger pb-2 font-weight-bold">';
                                                echo 'Số dư VND không đủ.';
                                                echo '</div>';
                                            }
                                        } else {
                                            echo '<div class="text-danger pb-2 font-weight-bold">';
                                            echo 'Hành trang đầy, không thể nhận quà.';
                                            echo '</div>';
                                        }
                                    } else {
                                        echo '<div class="text-danger pb-2 font-weight-bold">';
                                        echo 'Không tìm thấy dữ liệu người chơi.';
                                        echo '</div>';
                                    }
                                } else {
                                    echo '<div class="text-danger pb-2 font-weight-bold">';
                                    echo 'Không tìm thấy dữ liệu tài khoản.';
                                    echo '</div>';
                                }
                            } else {
                                echo '<div class="text-danger pb-2 font-weight-bold">';
                                echo 'Không tìm thấy món quà phù hợp.';
                                echo '</div>';
                            }
                        }
                    }
                    ?>
                    <script>
                        document.getElementById('vnd_amount').addEventListener('change', function () {
                            var vndAmount = parseInt(this.value);
                            var goldQuantity = 0;
                            var options = [
                                { amount: 10000, quantity: 25 },
                                { amount: 20000, quantity: 60 },
                                { amount: 30000, quantity: 90 },
                                { amount: 50000, quantity: 160 },
                                { amount: 100000, quantity: 360 },
                                { amount: 200000, quantity: 670 },
                                { amount: 500000, quantity: 1700 },
                                { amount: 1000000, quantity: 3500 }
                            ];

                            for (var i = 0; i < options.length; i++) {
                                if (options[i].amount === vndAmount) {
                                    goldQuantity = options[i].quantity;
                                    break;
                                }
                            }

                            document.getElementById('gold').textContent = goldQuantity;
                        });
                    </script>
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