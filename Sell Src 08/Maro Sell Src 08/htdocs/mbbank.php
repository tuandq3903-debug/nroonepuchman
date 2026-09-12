<?php
ob_clean(); // clear output buffer
header("Location: nap-mbbank"); // redirect to "nap-mbbank.php"
error_reporting(0);

// Thực hiện kết nối tới cơ sở dữ liệu
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "admin";

$conn = mysqli_connect($servername, $username, $password, $dbname);

if (!$conn) {
    die("Kết nối đến cơ sở dữ liệu thất bại: " . mysqli_connect_error());
}

// Lấy dữ liệu từ API Mbbank
$url = "https://api.buiduyanh.com/api/mbbank/history/ĐIỀN_TOKEN_CỦA_BẠN";
$ch = curl_init();
curl_setopt($ch, CURLOPT_URL, $url);
curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
$data = curl_exec($ch);
curl_close($ch);

$response = json_decode($data, true);
$tranList = $response['tranList'];
$count = count($tranList);

for ($x = 0; $x < $count; $x++) {
    $tranId = $tranList[$x]['tranId'];
    $io = $tranList[$x]['io'];
    $amount = $tranList[$x]['amount'];
    $comment = $tranList[$x]['comment'];

    $checkQuery = "SELECT * FROM `mbbank` WHERE `tranId`='$tranId'";
    $checkResult = mysqli_query($conn, $checkQuery);

    if (mysqli_num_rows($checkResult) > 0) {
        continue;
    } else {
        $updateQuery = "UPDATE `account` SET vnd = vnd + $amount, tongnap = tongnap + $amount WHERE username = '$comment'";
        mysqli_query($conn, $updateQuery);

        $insertQuery = "INSERT INTO `mbbank` (`tranId`, `io`, `amount`, `comment`) 
                        VALUES ('$tranId', '$io', '$amount', '$comment')";
        mysqli_query($conn, $insertQuery);
    }
}

mysqli_close($conn);
?>

<script>
    window.alert("Load lại trang sau khi đã chuyển khoản đúng nội dung !");
    window.alert("Hệ thống xử lý tự động từ 1-10 phút, sau 10 phút nếu không thấy cộng coin vui lòng liên hệ admin để xử lý !");
</script>

<?php
exit(); // exit script
?>