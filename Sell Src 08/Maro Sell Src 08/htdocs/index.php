<?php 
include 'connect.php';
include 'set.php';
include 'head.php';
?>
<!DOCTYPE html>
<html>


    <meta charset="utf-8">
    <title>Ngọc Rồng NRO RUN</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" type="image/x-icon" href="assets/images/icon/icon.ico">

    <!-- You can add your custom styles here -->
    <style>
        /* Custom background color */
        .main-container {
            background-color: #f0f8ff; /* Light blue background for the whole page */
        }

        .post-box {
            background-color: #FFCCFF; /* White background for post boxes */
        }

        .alert-danger {
            background-color: #00CC66; /* Light red background for alert box */
            border: 2px solid Black; /* Light red border */
        }

        .card-title {
            color: #dc3545; /* Red color for card title */
        }

        .text-blue {
            color: #007bff; /* Custom blue color for links */
        }

        .post-section {
            background-color: #00CC66; /* Light cyan background for each post section */
        }

        /* You can change the background color for the footer or other sections as well */
        footer {
            background-color: #00cc66; /* Example: Green footer */
        }
		
  .avatar {
      transition: transform 0.5s ease;
  }
  .avatar:hover {
      transform: rotate(360deg);
  }
 /* Tạo hiệu ứng bay lên cho trái tim */
@keyframes heartFlyUp {
    0% {
        transform: translateY(0); /* Vị trí ban đầu */
        opacity: 1; /* Độ mờ ban đầu */
    }
    50% {
        transform: translateY(-20px); /* Di chuyển lên 20px */
        opacity: 0.8; /* Mờ đi một chút */
    }
    100% {
        transform: translateY(-50px); /* Di chuyển lên cao 50px */
        opacity: 0; /* Trái tim sẽ mờ dần */
    }
}

/* Khi hover vào chữ "Hướng dẫn Chơi Game", trái tim sẽ bay lên */
a:hover + .ant-list-item-meta .content .heart-effect {
    animation: heartFlyUp 1s ease-out forwards; /* 1s là thời gian, ease-out giúp hiệu ứng mượt mà */
    position: relative; /* Để trái tim có thể di chuyển */
}

/* Đảm bảo trái tim không bị di chuyển trước khi hover */
.heart-effect {
    position: relative; /* Để hiệu ứng di chuyển có thể áp dụng */
}

   @keyframes marquee {
        0% { transform: translateX(100%); }
        100% { transform: translateX(-100%); }
    }

    @keyframes colorChange {
        0% { color: red; }
        20% { color: orange; }
        40% { color: yellow; }
        60% { color: #FFCCFF; }
        80% { color: #00EEEE; }
        100% { color: purple; }
    }

    .marquee-container {
        font-size: 18px; /* Điều chỉnh kích thước font chữ */
        font-weight: bold; /* Làm chữ đậm */
    }

    .marquee {
        display: inline-block;
        animation: marquee 10s linear infinite, colorChange 1s step-end infinite;
    </style>


<body class="main-container">

 <!-- Dòng chữ chạy -->
        <div class="marquee-container" style="overflow: hidden; white-space: nowrap; width: 100%; margin-bottom: 15px;">
            <div class="marquee" style="display: inline-block; animation: marquee 10s linear infinite, colorChange 1s step-end infinite;">
                <strong>SERVER NGỌC RỒNG RUN CHÚC CÁC BẠN CHƠI GAME VUI VẺ ❤️</strong>
            </div>
        </div>
<div class="p-3 mt-1 alert alert-danger" style="border-radius: 7px; box-shadow: 0px 0px 5px black; border: 2px solid #00CC66;">
    <div class="p-3">
        <div class="card-title h5">Bài viết mới</div>
        <hr>

      

        <!-- Các phần khác của bài viết -->
        <div class="post-section" style="margin-bottom: 20px;">
            <div class="post-box" style="border-radius: 7px; padding: 8px; margin-bottom: 8px; border: 1px solid #00CC66;">
                <div class="post-item d-flex align-items-center my-2 justify-content-between">
                    <div class="post-image">
                        <img src="/image/61.gif" alt="htth-su-kien-gio-to-hung-vuong" width="50px" height="50px" class="avatar">
                    </div>
                    <div class="ant-list-item-meta" style="flex-grow: 1; display: flex; align-items: center;">
                        <div>
                            <a class="fw-bold text-Black" href="bang-xep-hang" style="text-decoration: none;">
                                Bảng Xếp Hạng 
                                <i class="fas fa-check-circle" style="color: red; font-size: 1.2em; position: relative; top: 2px;"></i>
                            </a>

                            <small style="display: block; color: #6c757d;">
                                Đăng bởi <strong style="color: #BB0000;">ADMIN</strong>
                                <i class="fas fa-heart" style="color: red;"></i>
                            </small>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="post-section" style="margin-bottom: 20px;">
            <div class="post-box" style="border-radius: 7px; padding: 10px; border: 1px solid #00CC66;">
                <div class="post-item d-flex align-items-center my-2 justify-content-between">
                    <div class="post-image">
                        <img src="/image/19.gif" alt="hd" width="50px" height="50px" class="avatar">
                    </div>
                    <div class="ant-list-item-meta" style="flex-grow: 1; display: flex; align-items: center;">
                        <div class="content">
                            <a class="fw-bold text-blue" href="thongbao" style="text-decoration: none;">
                                Hướng dẫn Chơi Game
                            </a>
                            <i class="fas fa-check-circle" style="color: red; font-size: 1.2em; position: relative; top: 2px;"></i>
                            <small style="display: block; color: #6c757d;">
                                Đăng bởi <strong style="color: #BB0000;">ADMIN</strong>
                                <i class="fas fa-heart heart-effect" style="color: red;"></i>
                            </small>
                        </div>
                    </div>
                </div>
            </div>
        </div>
   
<style>
    @keyframes marquee {
        0% { transform: translateX(100%); }
        100% { transform: translateX(-100%); }
    }

    @keyframes colorChange {
        0% { color: red; }
        20% { color: orange; }
        40% { color: yellow; }
        60% { color: green; }
        80% { color: blue; }
        100% { color: purple; }
    }

    .marquee-container {
        font-size: 18px; /* Điều chỉnh kích thước font chữ */
        font-weight: bold; /* Làm chữ đậm */
    }

    .marquee {
        display: inline-block;
        animation: marquee 10s linear infinite, colorChange 1s step-end infinite;
    }
</style>

  





            <h5><b>SƠ LƯỢC VỀ GAME</b></h5>
            <hr>
           
            <p>Ngọc Rồng Run là trò chơi trực tuyến đa nền tảng. Bạn có thể chơi được trên mọi nền tảng từ máy tính PC Windows, iPhone, các dòng máy chạy hệ điều hành Android, Windows Phone đến các cả bản Java chạy trên S40, S60 cũ của Nokia. Với chất
                lượng cao và tốc độ mượt mà trên các loại đường truyền mạng ADSL, 3G, GPRS.
            </p>
            <p>Trò chơi thích hợp với mọi lứa tuổi. Điều khiển trực tiếp nhân vật rất dễ dàng trên màn hình cảm ứng. Khi chơi trên PC bạn chỉ cần dùng chuột, hoặc linh hoạt điều khiển nhân vật với bàn phím cứng điện thoại Nokia S40, S60.</p>
            <br>
            	
             <p class="text-center text-danger">Cơ Bản</p>
    <br>
    <p style="text-align:center">
        <img alt="" src="../ngocrongonline.com/gif/gif_maphongba.gif" loading="lazy">
        <img alt="" src="../ngocrongonline.com/gif/gif_gif_Saiyain.gif" loading="lazy">
        <img alt="" src="../ngocrongonline.com/gif/gif_supber_kame.gif" loading="lazy">
    </p>

    <p class="text-center text-danger">VIP</p>
    <br>
    <p style="text-align:center">
        <img alt="" src="../ngocrongonline.com/gif/gif_maphongba_VIP.gif" loading="lazy">
        <img alt="" src="../ngocrongonline.com/gif/gif_gif_Saiyain_VIP.gif" loading="lazy">
        <img alt="" src="../ngocrongonline.com/gif/gif_supber_kame_VIP.gif" loading="lazy">
    </p>

<div class="border-secondary border-top"></div>
<div class="container pt-4 pb-4 text-white">
    <div class="row">
        <div class="col">
            <div class="text-center">
                <div style="font-size: 18px; font-weight: bold; font-family: 'Noto Sans CJK', 'Microsoft YaHei', sans-serif;" class="text-dark">
                    <small>IP:
                        <?php echo $_IP; ?>
                    </small><br>
                    <small>Desgin By NRORUN</small><br>
                    <small></small>
                </div>
            </div>
        </div>
    </div>
</div>


    <script type="text/javascript">
        $(window).on('load', function() {
            $('#notify').modal('show');
        });

      
    </script>

</body>
</html>
