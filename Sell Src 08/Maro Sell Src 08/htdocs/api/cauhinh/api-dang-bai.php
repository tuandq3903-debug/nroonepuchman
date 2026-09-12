 <?php
                    include_once '../../set.php';
					include_once '../../connect.php';




$response = array(); // Initialize the response array
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
                                $response['type'] = 'error';
        $response['message'] = '<span class="text-danger pb-2">Thông Báo:</span> Tiêu đề hoặc nội dung chứa từ không cho phép.';
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

                           $response['type'] = 'success';
                           $response['message'] = '<span class="text-danger pb-2">Thông Báo:</span> Bài viết đã được đăng thành công.';
                        } else {
                             $response['type'] = 'error';
                             $response['message'] = '<span class="text-danger pb-2">Thông Báo:</span> Đã có lỗi xảy ra khi đăng bài.';
                        }
                    }

                    // Đóng kết nối với cơ sở dữ liệu
                    mysqli_close($conn);
					// Convert the response array to JSON and echo it
echo json_encode($response);
exit;
                    ?>