<?php
include_once 'set.php';
include_once 'connect.php';
include('head.php');
?>
<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Trang Chủ Chính Thức - Ngọc Rồng Online</title>
    <meta name="description" content="">
    <meta name="author" content="">
   
</head>

<body>
    <style>
    /* Add borders to the table header and data cells */
    .table th, .table td {
        border: 1px solid #ccc; /* Light gray border */
        padding: 8px;
        text-align: center; /* Center the text */
    }

    /* Optionally add background color to the headers */
    .table th {
        background-color: #00CC66; /* Light gray background for the header */
    }
</style>
    
  <div class="container color-forum pt-2" style="background-color: #00CC66; border-radius: 15px;">
    <div class="row">
        <div class="col">
            <h6 class="text-center" style="color: #dc3545;">BẢNG XẾP HẠNG ĐUA TOP NGỌC RỒNG ONLINE</h6>
            <table class="table table-borderless text-center" style="background-color: #ffffff; border-radius: 8px; overflow: hidden;">
                <thead style="background-color: #FFCCFF;">
                    <tr>
                        <th>#</th>
                        <th>Nhân vật</th>
                        <th>Sức Mạnh</th>
                        <th>Đệ Tử</th>
                        <th>Hành Tinh</th>
                        <th>Tổng</th>
                    </tr>
                </thead>
                <tbody>
                    <?php
                    $countTop = 1;
                    $data = mysqli_query($conn, "SELECT name, gender, 
                        CASE 
                            WHEN gender = 1 THEN CAST(JSON_UNQUOTE(JSON_EXTRACT(data_point, '$[1]')) AS SIGNED)
                            WHEN gender = 2 THEN CAST(JSON_UNQUOTE(JSON_EXTRACT(data_point, '$[1]')) AS SIGNED)
                            ELSE CAST(JSON_UNQUOTE(JSON_EXTRACT(data_point, '$[1]')) AS SIGNED)
                        END AS second_value,
                        SUBSTRING_INDEX(SUBSTRING_INDEX(JSON_UNQUOTE(JSON_EXTRACT(pet, '$[1]')), ',', 2), ',', -1) AS detu_sm,
                        CAST(JSON_UNQUOTE(JSON_EXTRACT(data_point, '$[1]')) AS SIGNED) + CAST(COALESCE(SUBSTRING_INDEX(SUBSTRING_INDEX(JSON_UNQUOTE(JSON_EXTRACT(pet, '$[1]')), ',', 2), ',', -1), '0') AS SIGNED) AS tongdiem
                    FROM player
                    ORDER BY tongdiem DESC
                    LIMIT 10;");
                    if (mysqli_num_rows($data) > 0) {
                        while ($row = mysqli_fetch_array($data)) {
                            ?>
                            <tr class="top_<?php echo $countTop; ?>" style="background-color: <?php echo ($countTop % 2 == 0) ? '#f8f9fa' : '#ffffff'; ?>;">
                                <td>
                                    <?php echo $countTop++; ?>
                                </td>
                                <td>
                                    <?php echo htmlspecialchars($row['name']); ?>
                                </td>
                                <td>
                                    <?php
                                    $value = $row['second_value'];

                                    if ($value != '') {
                                        if ($value > 1000000000) {
                                            echo number_format($value / 1000000000, 1, '.', '') . ' tỷ';
                                        } elseif ($value > 1000000) {
                                            echo number_format($value / 1000000, 1, '.', '') . ' Triệu';
                                        } elseif ($value >= 1000) {
                                            echo number_format($value / 1000, 1, '.', '') . ' k';
                                        } else {
                                            echo number_format($value, 0, ',', '');
                                        }
                                    } else {
                                        echo 'Không có chỉ số sức mạnh';
                                    }
                                    ?>
                                </td>
                                <td>
                                    <?php
                                    $value = $row['detu_sm'];

                                    if ($value != '') {
                                        if ($value > 1000000000) {
                                            echo number_format($value / 1000000000, 1, '.', '') . ' tỷ';
                                        } elseif ($value > 1000000) {
                                            echo number_format($value / 1000000, 1, '.', '') . ' Triệu';
                                        } elseif ($value >= 1000) {
                                            echo number_format($value / 1000, 1, '.', '') . ' k';
                                        } else {
                                            echo number_format($value, 0, ',', '');
                                        }
                                    } else {
                                        echo 'Không đệ tử';
                                    }
                                    ?>
                                </td>
                                <td>
                                    <?php
                                    if ($row['gender'] == 0) {
                                        echo "Trái đất";
                                    } elseif ($row['gender'] == 1) {
                                        echo "Namec";
                                    } elseif ($row['gender'] == 2) {
                                        echo "Xayda";
                                    }
                                    ?>
                                </td>
                                <td>
                                    <?php
                                    $total = $row['tongdiem'];

                                    if ($total > 1000000000) {
                                        echo number_format($total / 1000000000, 1, '.', '') . ' tỷ';
                                    } elseif ($total > 1000000) {
                                        echo number_format($total / 1000000, 1, '.', '') . ' Triệu';
                                    } elseif ($total >= 1000) {
                                        echo number_format($total / 1000, 1, '.', '') . ' k';
                                    } else {
                                        echo number_format($total, 0, ',', '');
                                    }
                                    ?>
                                </td>
                            </tr>
                            <?php
                        }
                    } else {
                        echo 'Máy Chủ 1 chưa có thông kê bảng xếp hạng!';
                    }
                    ?>
                </tbody>
            </table>
        </div>
    </div>
</div>

<script>
    // Cập nhật tự động sau mỗi 3 giây
    setInterval(function () {
        $.ajax({
            url: location.href, // URL hiện tại
            success: function (result) {
                var leaderboardTable = $(result).find('#leaderboard-table'); // Tìm bảng xếp hạng trong HTML mới nhận được
                $('#leaderboard-table').html(leaderboardTable.html()); // Cập nhật HTML của bảng xếp hạng
            }
        });
    }, 3000);
</script>

<div class="text-right">
    <small>Cập nhật lúc:
        <?php echo date('H:i d/m/Y'); ?>
    </small>
</div>

          
        
    <div class="border-secondary border-top"></div>
    <div class="container pt-4 pb-4 text-white">
        <div class="row">
            <div class="col">
                <div class="text-center">
                    <div style="font-size: 13px" class="text-dark">
                        <small>IP:
                            <?php echo $_IP; ?>
                        </small><br>
                        <small>Desgin By NROZ</small><br>
                        <small>2023© Ngọc Rồng Online</small>
                    </div>
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