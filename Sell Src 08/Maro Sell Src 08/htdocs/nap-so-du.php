<?php
include_once 'set.php';
include_once 'connect.php';


$_alert = null;
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
    <div class="container pt-3 pb-5">
        <div class="row">
            <div class="col-lg-6 offset-lg-3">
                <div class="text-center pb-3"> <a href="history" class="text-dark"><i
                            class="fas fa-hand-point-right"></i>
                        Xem tình trạng nạp thẻ <i class="fas fa-hand-point-left"></i></a> </div>
                <h4>NẠP SỐ DƯ</h4>
                <?php if ($_login === null) { ?>
                    <p>Bạn chưa đăng nhập? hãy đăng nhập để sử dụng chức năng này</p>
                <?php } else { ?>
                    <script type="text/javascript">
                        new WOW().init();
                    </script>
                    <form method="POST" action="" id="myform">
                        <tbody>
                            <label>Tài Khoản: </label><br>
                            <input type="text" class="form-control form-control-alternative"
                                style="background-color: #DCDCDC; font-weight: bold; color: #696969" name="username" value="<?php echo $_username; ?>
" readonly required>

                            <label>Loại thẻ:</label>
                            <select class="form-control form-control-alternative" name="card_type" required>
                                <option value="">Chọn loại thẻ</option>
                                <?php
                                $cdurl = curl_init("https://thesieutoc.net/card_info.php");
                                curl_setopt($cdurl, CURLOPT_FAILONERROR, true);
                                curl_setopt($cdurl, CURLOPT_FOLLOWLOCATION, true);
                                curl_setopt($cdurl, CURLOPT_RETURNTRANSFER, true);
                                curl_setopt($cdurl, CURLOPT_CAINFO, __DIR__ . '/api/curl-ca-bundle.crt');
                                curl_setopt($cdurl, CURLOPT_CAPATH, __DIR__ . '/api/curl-ca-bundle.crt');
                                $obj = json_decode(curl_exec($cdurl), true);
                                curl_close($cdurl);
                                $length = count($obj);
                                for ($i = 0; $i < $length; $i++) {
                                    if ($obj[$i]['status'] == 1) {
                                        echo '<option value="' . $obj[$i]['name'] . '">' . $obj[$i]['name'] . ' (' . $obj[$i]['chietkhau'] . '%)</option> ';
                                    }
                                }
                                ?>
                            </select>
                            <label>Mệnh giá:</label>
                            <select class="form-control form-control-alternative" name="card_amount" required>
                                <option value="">Sai mệnh giá mất thẻ!</option>
                                <option value="10000">10.000</option>
                                <option value="20000">20.000</option>
                                <option value="30000">30.000 </option>
                                <option value="50000">50.000</option>
                                <option value="100000">100.000</option>
                                <option value="200000">200.000</option>
                                <option value="300000">300.000</option>
                                <option value="500000">500.000</option>
                                <option value="1000000">1.000.000</option>
                            </select>
                            <label>Số seri:</label>
                            <input type="text" class="form-control form-control-alternative" name="serial" required />
                            <label>Mã thẻ:</label>
                            <input type="text" class="form-control form-control-alternative" name="pin" required /><br>
                            <button type="submit" class="btn btn-main form-control" name="submit">NẠP NGAY</button>

                        </tbody>
                    </form>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            var lastSubmitTime = 0;
                            $("#myform").submit(function (e) {
                                var now = new Date().getTime();
                                if (now - lastSubmitTime < 5000) {
                                    Swal.fire({
                                        title: 'Thông báo',
                                        text: 'Vui lòng đợi ít nhất 5 giây trước khi nạp tiếp',
                                        icon: 'error'
                                    });
                                    return false;
                                }
                                lastSubmitTime = now;

                                $("#status").html("");
                                e.preventDefault();
                                $.ajax({
                                    url: "./ajax/card.php",
                                    type: 'post',
                                    data: $("#myform").serialize(),
                                    success: function (data) {
                                        $("#status").html(data);
                                        document.getElementById("myform").reset();
                                        $("#load_hs").load("./history.php");
                                    }
                                });
                            });
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
    <div id="status"></div>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/crypto-js/4.0.0/crypto-js.min.js"></script>
    <!-- Code made in tui 127.0.0.1 -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"
        integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous">
        </script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"
        integrity="sha384-smHYKdLADwkXOn1EmN1qk/HfnUcbVRZyYmZ4qpPea6sjB/pTJ0euyQp0Mk8ck+5T" crossorigin="anonymous">
        </script>

    <script src="assets/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/main.js"></script>
</body><!-- Bootstrap core JavaScript -->

</html>