<?php
include_once 'set.php';
include_once 'connect.php';
include('head.php');
if ($_login === null) {
    // Chưa đăng nhập, chuyển hướng đến trang khác bằng JavaScript
    echo '<script>window.location.href = "../dang-nhap";</script>';
    exit(); // Đảm bảo dừng thực thi code sau khi chuyển hướng
}
$_alert = '';
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
                <?php if ($_login === null) { ?>
                    <p>Bạn chưa đăng nhập? hãy đăng nhập để sử dụng chức năng này</p>
                <?php } else { ?>
                    <form method="POST" enctype="multipart/form-data">
                        <div class="form-group">
                            <label><span class="text-danger">*</span> Tiêu đề:</label>
                            <input class="form-control" type="text" name="tieude" id="tieude"
                                placeholder="Nhập tiêu đề bài viết" required>

                            <label><span class="text-danger">*</span> Nội dung:</label>
                            <textarea class="form-control" type="text" name="noidung" id="noidung"
                                placeholder="Nhập nội dung bài viết" required></textarea>
                            <?php
                            $query = "SELECT account.*, account.admin FROM account LEFT JOIN player ON player.account_id = account.id";
                            $result = mysqli_query($conn, $query);

                            if ($row = mysqli_fetch_assoc($result)) {
                                ?>
                                <label><span class="text-danger">*</span> Thể loại:</label>
                                <select class="form-control" name="theloai" id="theloai" required>
                                    <option value="0">Thường</option>
                                    <script>
                                        var isAdmin = <?php echo ($row['admin'] == 1) ? 'true' : 'false'; ?>;
                                        if (isAdmin) {
                                            var option1 = new Option("Thông Báo", "1");
                                            var option2 = new Option("Sự Kiện", "2");
                                            var option3 = new Option("Cập Nhật", "3");
                                            document.getElementById("theloai").add(option1);
                                            document.getElementById("theloai").add(option2);
                                            document.getElementById("theloai").add(option3);
                                        }
                                    </script>
                                </select>
                                <?php
                            }
                            ?>
                            <label>Chọn ảnh:</label>
                            <input class="form-control" type="file" name="image[]" id="image" multiple>

                            <div id="submit-error" class="alert alert-danger mt-2" style="display: none;"></div>
                        </div>
<br>
                        <button id="btn" class="btn btn-lg btn-dark btn-block" style="border-radius: 10px;width: 100%; height: 50px;" type="submit">ĐĂNG BÀI</button>
                    </form>
                    <?php
                    include_once 'set.php';

                    // Lấy dữ liệu từ form sử dụng phương thức POST
                    if ($_SERVER["REQUEST_METHOD"] == "POST") {
                        // Lấy giá trị của tiêu đề và nội dung bài viết
                        $tieude = htmlspecialchars($_POST["tieude"]);
                        $noidung = htmlspecialchars($_POST["noidung"]);
                        $theloai = intval($_POST["theloai"]);

                        if (isset($_POST['username'])) {
                            $_username = $_POST['username'];
                        }
                        $sql = "SELECT player.name FROM player INNER JOIN account ON account.id = player.account_id WHERE account.username='$_username'";
                        $result = mysqli_query($conn, $sql);
                        $row = mysqli_fetch_assoc($result);
                        $_name = $row['name'];

                        // Kiểm tra từ cấm trong tiêu đề và nội dung
                        $censoredWords = array(
                            'sex',
                            'địt',
                            'súc vật',
                            'sv',
                            'fuck',
                            'loz',
                            'lozz',
                            'lozzz',
                            'óc chó',
                            'ngu lồn',
                            'nguu lồn',
                            'nguu lồn',
                            'ngulon',
                            'nguu lonn',
                            'ngu lon',
                            'occho',
                            'ditmemay',
                            'dmm',
                            'dcm',
                            'địt cụ mày',
                            'địt con mẹ mày',
                            'fuck you',
                            'chịch',
                            'chịt',
                            'sẽ gầy'
                        );

                        foreach ($censoredWords as $word) {
                            if (stripos($tieude, $word) !== false || stripos($noidung, $word) !== false) {
                                echo "<span class='text-danger pb-2'>Thông Báo:</span> Tiêu đề hoặc nội dung chứa từ không cho phép.";
                                exit;
                            }
                        }

                        // Kiểm tra nếu có tệp tin ảnh được tải lên
                        if (isset($_FILES['image']) && !empty($_FILES['image']['name'][0])) {
                            $image_files = $_FILES['image'];
                            $total_files = count($image_files['name']);

                            $image_names = array(); // Mảng để lưu trữ tên tệp tin ảnh
                            $upload_directory = "uploads/"; // Thư mục lưu trữ ảnh
                
                            for ($i = 0; $i < $total_files; $i++) {
                                $image_filename = $image_files['name'][$i];
                                $image_tmp = $image_files['tmp_name'][$i];

                                $targetFile = $upload_directory . basename($image_filename);

                                // Di chuyển tệp tin ảnh vào thư mục lưu trữ
                                move_uploaded_file($image_tmp, $targetFile);

                                // Thêm tên tệp tin vào mảng
                                $image_names[] = basename($image_filename);
                            }

                            // Chuyển đổi mảng thành chuỗi JSON
                            $image_names_json = json_encode($image_names);

                            // Lưu dữ liệu (bao gồm username và danh sách tên tệp tin ảnh) vào cơ sở dữ liệu bằng câu lệnh INSERT INTO
                            $sql = "INSERT INTO posts (tieude, noidung, theloai, image, username) VALUES ('$tieude', '$noidung', '$theloai', '$image_names_json', '$_name')";
                        } else {
                            // Nếu không có tệp tin ảnh được tải lên, lưu dữ liệu (bao gồm username) vào cơ sở dữ liệu bằng câu lệnh INSERT INTO
                            $sql = "INSERT INTO posts (tieude, noidung, theloai, username) VALUES ('$tieude', '$noidung', '$theloai', '$_name')";
                        }

                        if (mysqli_query($conn, $sql)) {
                            // Lấy số điểm tích lũy hiện tại của người dùng
                            $sql_select = "SELECT a.tichdiem FROM account a INNER JOIN player p ON a.id = p.account_id WHERE p.name = '$_name'";
                            $result_select = mysqli_query($conn, $sql_select);
                            $row_select = mysqli_fetch_assoc($result_select);
                            $tichdiem = $row_select['tichdiem'];

                            // Cập nhật giá trị tichdiem trong bảng account
                            $sql_update = "UPDATE account SET tichdiem = ($tichdiem + 1) WHERE id = (SELECT account_id FROM player WHERE name = '$_name')";
                            mysqli_query($conn, $sql_update);

                            echo "<span class='text-danger pb-2'>Thông Báo:</span> Bài viết đã được đăng thành công.";
                        } else {
                            echo "Lỗi: " . $sql . "<br>" . mysqli_error($conn);
                        }
                    }

                    // Đóng kết nối với cơ sở dữ liệu
                    mysqli_close($conn);
                    ?>
                    <script>
                        const form = document.querySelector('form');
                        const submitBtn = form.querySelector('button[type="submit"]');
                        const submitError = form.querySelector('#submit-error');

                        form.addEventListener('submit', (event) => {
                            const titleLength = document.getElementById('tieude').value.trim().length;
                            const contentLength = document.getElementById('noidung').value.trim().length;

                            if (titleLength < 5 || contentLength < 5) {
                                event.preventDefault();

                                submitError.innerHTML = '<strong>Lỗi:</strong> Tiêu đề và nội dung phải có ít nhất 5 ký tự!';
                                submitError.style.display = 'block';
                                submitBtn.scrollIntoView({ behavior: 'smooth', block: 'start' });
                            }
                        });
                    </script>
                <?php } ?>
            </div>
        </div>
    </div>
    <script src=" assets/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="asset/main.js"></script>
</body><!-- Bootstrap core JavaScript -->

</html>
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
</main>
</body>