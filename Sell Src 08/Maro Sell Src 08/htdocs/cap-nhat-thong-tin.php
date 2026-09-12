<?php
include 'head.php';
if ($_login === null) {
    // Chưa đăng nhập, chuyển hướng đến trang khác bằng JavaScript
    echo '<script>window.location.href = "../dang-nhap";</script>';
    exit(); // Đảm bảo dừng thực thi code sau khi chuyển hướng
}


?>


    <div class="p-1 mt-1 alert alert-danger" style="border-radius: 7px; box-shadow: 0px 0px 5px black;">
    <div class="container pt-5 pb-5" id="pageHeader">
        <div class="row pb-2 pt-2">
            <div class="col-lg-6">
                <?php if ($_login === null) { ?>
                    <p>Bạn chưa đăng nhập? hãy đăng nhập để sử dụng chức năng này</p>
                <?php } else { ?>
                    <?php
                    $stmt = $conn->prepare("SELECT password, gmail FROM account WHERE username=?");
                    $stmt->bind_param("s", $_username);
                    $stmt->execute();
                    $result = $stmt->get_result();
                    $row = $result->fetch_assoc();
                    $primaryPassword = $row['password'];
                    $primaryGmail = $row['gmail'];

                    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
                        $password = $_POST['password'] ?? '';
                        $newGmail = $_POST['new_gmail'] ?? '';
                        $newGmailConfirm = $_POST['new_gmail_confirm'] ?? '';

                        if (!empty($primaryGmail)) {
                            $oldGmail = isset($_POST['old_gmail']) ? $_POST['old_gmail'] : '';

                            if (!empty($password) && !empty($newGmail) && !empty($newGmailConfirm) && !empty($oldGmail)) {
                                // Kiểm tra xem mật khẩu hiện tại nhập vào có giống với mật khẩu trong database không.
                                // Nếu sai, in ra thông báo lỗi.
                                if ($password !== $primaryPassword) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Sai mật khẩu hiện tại</div>";
                                } elseif ($oldGmail !== $primaryGmail) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Sai Gmail hiện tại</div>";
                                } elseif ($newGmail === $primaryGmail) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Gmail mới không được giống với Gmail hiện tại</div>";
                                } elseif ($newGmail !== $newGmailConfirm) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Gmail mới không giống nhau</div>";
                                } elseif (!filter_var($newGmail, FILTER_VALIDATE_EMAIL) || substr($newGmail, -10) !== "@gmail.com") {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Vui lòng nhập địa chỉ email Gmail (ví dụ: example@gmail.com)</div>";
                                } elseif (!filter_var($newGmailConfirm, FILTER_VALIDATE_EMAIL) || substr($newGmailConfirm, -10) !== "@gmail.com") {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Vui lòng nhập địa chỉ email Gmail (ví dụ: example@gmail.com)</div>";
                                } else {
                                    // Cập nhật Gmail mới lên database
                                    $stmt = $conn->prepare("UPDATE account SET gmail=? WHERE username=?");
                                    $stmt->bind_param("ss", $newGmail, $_username);

                                    if ($stmt->execute()) {
                                        echo "<div class='text-danger pb-2 font-weight-bold'>Cập nhật Gmail thành công</div>";
                                    } else {
                                        echo "<div class='text-danger pb-2 font-weight-bold'>Lỗi khi cập nhật Gmail</div>";
                                    }
                                }
                            } else {
                                echo "<div class='text-danger pb-2 font-weight-bold'>Vui lòng điền đầy đủ thông tin trong form</div>";
                            }
                        } else {
                            if (!empty($password) && !empty($newGmail) && !empty($newGmailConfirm)) {
                                if ($password !== $primaryPassword) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Sai mật khẩu hiện tại</div>";
                                } elseif ($newGmail !== $newGmailConfirm) {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Gmail không giống nhau</div>";
                                } elseif (!filter_var($newGmail, FILTER_VALIDATE_EMAIL) || substr($newGmail, -10) !== "@gmail.com") {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Vui lòng nhập địa chỉ email Gmail (ví dụ: example@gmail.com)</div>";
                                } elseif (!filter_var($newGmailConfirm, FILTER_VALIDATE_EMAIL) || substr($newGmailConfirm, -10) !== "@gmail.com") {
                                    echo "<div class='text-danger pb-2 font-weight-bold'>Vui lòng nhập địa chỉ email Gmail (ví dụ: example@gmail.com)</div>";
                                } else {
                                    // Cập nhật Gmail lên database
                                    $stmt = $conn->prepare("UPDATE account SET gmail=? WHERE username=?");
                                    $stmt->bind_param("ss", $newGmail, $_username);

                                    if ($stmt->execute()) {
                                        echo "<div class='text-danger pb-2 font-weight-bold'>Cập nhật Gmail thành công</div>";
                                    } else {
                                        echo "<div class='text-danger pb-2 font-weight-bold'>Lỗi khi cập nhật Gmail</div>";
                                    }
                                }
                            } else {
                                echo "<div class='text-danger pb-2 font-weight-bold'>Vui lòng điền đầy đủ thông tin trong form</div>";
                            }
                        }
                    }

                    if (!empty($primaryGmail)) {
                        ?>
                        <p>Tài khoản của bạn đã được liên kết Gmail</p>
                        <!-- Trang HTML -->
                        <div id="remaining-time"></div>

                        <script>
                            // Sử dụng JavaScript và AJAX để gửi yêu cầu đến máy chủ và cập nhật nội dung của vùng hiển thị kết quả
                            function updateRemainingTime() {
                                var xhttp = new XMLHttpRequest();
                                xhttp.onreadystatechange = function () {
                                    if (this.readyState === 4 && this.status === 200) {
                                        // Nhận phản hồi từ máy chủ và cập nhật nội dung của vùng hiển thị kết quả
                                        document.getElementById("remaining-time").innerHTML = this.responseText;
                                    }
                                };
                                xhttp.open("GET", "./gmail/time.php", true); // Thay đổi đường dẫn đến tệp PHP xử lý
                                xhttp.send();
                            }

                            // Tự động cập nhật thời gian mỗi giây
                            setInterval(updateRemainingTime, 1000);
                        </script>

                        <div>Gmail liên kết: <span class="font-weight-bold">
                                <?php echo $primaryGmail; ?>
                            </span><i style="color:green" class="fa-regular fa-badge-check"></i></div>
                    <?php } else { ?>
                        <?php
                        // Lấy thông báo và lớp thông báo từ tham số truy vấn
                        $message = $_GET['message'] ?? '';
                        $messageClass = $_GET['messageClass'] ?? '';

                        // Hiển thị thông báo và lớp thông báo
                        if ($message && $messageClass) {
                            echo '<div class="' . $messageClass . '">' . $message . '</div>';
                        }
                        ?>

                        <form method="POST">
                            <div class="mb-3">
                                <label class="font-weight-bold">Mật khẩu hiện tại:</label>
                                <input type="password" class="form-control" name="password" id="password"
                                    placeholder="Mật khẩu hiện tại" required autocomplete="password">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Gmail mới:</label>
                                <input type="text" class="form-control" name="new_gmail" id="new_gmail" placeholder="Gmail mới"
                                    required autocomplete="new_gmail">
                            </div>
                            <div class="mb-3">
                                <label class="font-weight-bold">Xác nhận Gmail mới:</label>
                                <input type="text" class="form-control" name="new_gmail_confirm" id="new_gmail_confirm"
                                    placeholder="Xác nhận Gmail mới" required autocomplete="new_gmail_confirm">
                            </div>
                            <button class="btn btn-main form-control" type="submit">Thực hiện</button>
                        </form>
                        <br>
                        <br>
                    <?php }
                    ?>
                    <div id="notification"></div>
                </div>
                <div class="col-lg-6 htop ">
                    <br>
                    <br>
                    <h6> THÔNG TIN VỀ CẬP NHẬT THÔNG TIN</h6>
                    1. Thông tin chung
                    <br>
                    - Cập nhật Gmail
                    <br>
                    - Dùng để lấy lại thông tin khi quên
                    <br>
                    - Có thể dùng hoặc không dùng
                    <br>
                    - Có thể đổi được gmail mới
                    <br>
                    - Nhấn vào nút HỦY GMAIL HIỆN TẠI là nó sẽ gửi gmail nha :3
                    <br>
                    <br>
                    2. Hủy gmail hiện tại
                    <br>
                    - Gmail sẽ được huỷ luôn nếu như bạn xác nhận
                    <br>
                    - Vẫn có thể bật lại sau khi Hủy
                    <br>
                    <br>

                    <?php if (!empty($primaryGmail)) { ?>
                        <div class="mt-2 mb-2">
                            <?php if (!empty($primaryGmail)) { ?>
                                <div class="mt-2 mb-2">
                                    <a class="btn btn-lg btn-dark btn-block" style="border-radius: 10px;width: 100%; height: 50px;" href="#" id="sendEmailLink">
                                        <i class="fa fa-ban text-danger"></i> HỦY GMAIL HIỆN TẠI
                                    </a>

                                    <script>
                                        document.getElementById('sendEmailLink').addEventListener('click', function (event) {
                                            event.preventDefault();

                                            var xhr = new XMLHttpRequest();
                                            xhr.open('POST', 'gmail/guithu.php', true);
                                            xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
                                            xhr.send();

                                            xhr.onreadystatechange = function () {
                                                if (xhr.readyState === XMLHttpRequest.DONE) {
                                                    if (xhr.status === 200) {
                                                        var response = xhr.responseText;
                                                        if (response === "success") {
                                                            alert("Gửi gmail thành công");
                                                            updateRemainingTime(); // Cập nhật thời gian sau khi gửi gmail thành công
                                                        } else {
                                                            console.error(response);
                                                        }
                                                    } else {
                                                        console.error(xhr.statusText);
                                                    }
                                                }
                                            };
                                        });
                                    </script>
                                </div>
                            <?php } ?>
                        </div>
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