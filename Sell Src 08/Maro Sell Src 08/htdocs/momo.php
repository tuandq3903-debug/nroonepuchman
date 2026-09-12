<?php
ob_clean(); // clear output buffer
header("Location: nap-momo"); // redirect to "nap-momo.php"
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

// Lấy dữ liệu từ API Momo
$curl = curl_init();
$dataPost = array(
    "type" => "history",
    "token" => "0e018bee18b411e14753b8-31e5-0189-7db3-c004ed3faf86",
);
curl_setopt_array($curl, array(
    CURLOPT_URL => 'https://api.buiduyanh.com/api/momo/history/0e018bee18b411e14753b8-31e5-0189-7db3-c004ed3faf86',
    CURLOPT_RETURNTRANSFER => true,
    CURLOPT_ENCODING => '',
    CURLOPT_MAXREDIRS => 10,
    CURLOPT_TIMEOUT => 0,
    CURLOPT_FOLLOWLOCATION => true,
    CURLOPT_HTTP_VERSION => CURL_HTTP_VERSION_1_1,
    CURLOPT_CUSTOMREQUEST => 'POST',
    CURLOPT_POSTFIELDS => $dataPost,
)
);

$response = curl_exec($curl);
curl_close($curl);

$result = json_decode($response, true);
$tranList = $result['tranList'];
$count = count($tranList);

for ($x = 0; $x < $count; $x++) {
    $tranId = $tranList[$x]['tranId'];
    $partnerId = $tranList[$x]['partnerId'];
    $partnerName = $tranList[$x]['partnerName'];
    $io = $tranList[$x]['io'];
    $amount = $tranList[$x]['amount'];
    $comment = $tranList[$x]['comment'];

    $checkQuery = "SELECT * FROM `momo` WHERE `tranId`='$tranId'";
    $checkResult = mysqli_query($conn, $checkQuery);

    if (mysqli_num_rows($checkResult) > 0) {
        continue;
    } else {
        $updateQuery = "UPDATE `account` SET vnd = vnd + $amount, tongnap = tongnap + $amount WHERE username = '$comment'";
        mysqli_query($conn, $updateQuery);

        $insertQuery = "INSERT INTO `momo` (`tranId`, `partnerId`, `partnerName`, `io`, `amount`, `comment`) 
                        VALUES ('$tranId', '$partnerId', '$partnerName', '$io', $amount, '$comment')";
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