<?php
require_once 'set.php';
include 'connect.php';
?>
<html>
<head>

    <meta charset="utf-8">
    <title>Ngọc Rồng Run</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="icon" type="image/x-icon" href="assets/images/icon/icon.ico">
	<link rel="stylesheet" type="text/css" href="/css/sweetalert.css">
	<link rel="stylesheet" type="text/css" href="/css/hit.css">
	<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.5.0/css/all.css">
	 <script src="https://sweetalert.js.org/assets/sweetalert/sweetalert.min.js"></script>
	  <link href="https://fonts.googleapis.com/css?family=Lato:300,400,400i,700,700i" rel="stylesheet">
    <link href="https://fonts.googleapis.com/css?family=Inconsolata" rel="stylesheet">
	<script type="text/javascript" src="/assets/js/sweetalert.js"></script>


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
	<script src="https://cdn.jsdelivr.net/npm/sweetalert2@9"></script>

    <!-- bootstrap -->
	
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
	
</head>

<script>
    function confirmLogout() {
        Swal.fire({
            title: 'Bạn có chắc chắn muốn đăng xuất?',
            text: "",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Đồng ý',
            cancelButtonText: 'Đóng'
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire({
                    title: 'Thành công',
                    text: '',
                    icon: 'success',
                    timer: 1000,
                    showConfirmButton: false
                }
                ).then(() => {
                    window.location = '/logout.php'
                });
            }
        })
    }
</script> 
<style>
	#snowflakeContainer{position:absolute;left:0px;top:0px;}
	.snowflake{padding-left:15px;font-size:14px;line-height:24px;position:fixed;color:#ebebeb;user-select:none;z-index:1000;-moz-user-select:none;-ms-user-select:none;-khtml-user-select:none;-webkit-user-select:none;-webkit-touch-callout:none;}
	.snowflake:hover {cursor:default}
</style>
<div id='snowflakeContainer'>
<p class='snowflake'>☘️</p>
</div>
<script style='text/javascript'>
	//<![CDATA[
	var requestAnimationFrame=window.requestAnimationFrame||window.mozRequestAnimationFrame||window.webkitRequestAnimationFrame||window.msRequestAnimationFrame;var transforms=["transform","msTransform","webkitTransform","mozTransform","oTransform"];var transformProperty=getSupportedPropertyName(transforms);var snowflakes=[];var browserWidth;var browserHeight;var numberOfSnowflakes=25;var resetPosition=false;function setup(){window.addEventListener("DOMContentLoaded",generateSnowflakes,false);window.addEventListener("resize",setResetFlag,false)}setup();function getSupportedPropertyName(b){for(var a=0;a<b.length;a++){if(typeof document.body.style[b[a]]!="undefined"){return b[a]}}return null}function Snowflake(b,a,d,e,c){this.element=b;this.radius=a;this.speed=d;this.xPos=e;this.yPos=c;this.counter=0;this.sign=Math.random()<0.5?1:-1;this.element.style.opacity=0.5+Math.random();this.element.style.fontSize=4+Math.random()*30+"px"}Snowflake.prototype.update=function(){this.counter+=this.speed/5000;this.xPos+=this.sign*this.speed*Math.cos(this.counter)/40;this.yPos+=Math.sin(this.counter)/40+this.speed/30;setTranslate3DTransform(this.element,Math.round(this.xPos),Math.round(this.yPos));if(this.yPos>browserHeight){this.yPos=-50}};function setTranslate3DTransform(a,c,b){var d="translate3d("+c+"px, "+b+"px, 0)";a.style[transformProperty]=d}function generateSnowflakes(){var b=document.querySelector(".snowflake");var h=b.parentNode;browserWidth=document.documentElement.clientWidth;browserHeight=document.documentElement.clientHeight;for(var d=0;d<numberOfSnowflakes;d++){var j=b.cloneNode(true);h.appendChild(j);var e=getPosition(50,browserWidth);var a=getPosition(50,browserHeight);var c=5+Math.random()*40;var g=4+Math.random()*10;var f=new Snowflake(j,g,c,e,a);snowflakes.push(f)}h.removeChild(b);moveSnowflakes()}function moveSnowflakes(){for(var b=0;b<snowflakes.length;b++){var a=snowflakes[b];a.update()}if(resetPosition){browserWidth=document.documentElement.clientWidth;browserHeight=document.documentElement.clientHeight;for(var b=0;b<snowflakes.length;b++){var a=snowflakes[b];a.xPos=getPosition(50,browserWidth);a.yPos=getPosition(50,browserHeight)}resetPosition=false}requestAnimationFrame(moveSnowflakes)}function getPosition(b,a){return Math.round(-1*b+Math.random()*(a+2*b))}function setResetFlag(a){resetPosition=true};
	//]]>
</script>

<body class="body-bg">
<a href=”javascript:PopUp(‘http://veoclub.net/irc/’,’Pager’,’700′,’500′,’1′,’1′,’1′,’1′)”><font color=”red”><b></b></font></a>
    <!--<embed src="assets/musics/aoanh.mp3" loop="true" autostart="true" width="2" height="0">-->

    <div class="container-md p-1 col-sm-12 col-lg-6" style="background: #a69eff91; border-radius: 7px; border: 1px solid #d3087d; box-shadow: 0 0 15px #ED7AE7;">



        <style>
            #snow {
                position: fixed;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                pointer-events: none;
                z-index: -100;
            }
			



/* Tạo hiệu ứng nhấp nháy nhiều màu */
@keyframes blinkColors {
    0% { background-color: #ff3366; }
    20% { background-color: #33cc33; }
    40% { background-color: #3399ff; }
    60% { background-color: #ffcc00; }
    80% { background-color: #9933cc; }
    100% { background-color: #ff3366; }
}

.animated-btn {
    animation: blinkColors 1.5s infinite;
    transition: background-color 0.3s ease;
}

.animated-btn:hover {
    background-color: #ff66b3; /* Màu nền khi hover */
}




        </style>

        <div id="snow"></div>

        <script>
            document.addEventListener('DOMContentLoaded', function() {
                var script = document.createElement('script');
                script.src = 'https://cdn.jsdelivr.net/particles.js/2.0.0/particles.min.js';
                script.onload = function() {
                    particlesJS("snow", {
                        "particles": {
                            "number": {
                                "value": 75,
                                "density": {
                                    "enable": true,
                                    "value_area": 400
                                }
                            },
                            "color": {
                                "value": "#FFCCFF"
                            },
                            "opacity": {
                                "value": 1,
                                "random": true,
                                "anim": {
                                    "enable": false
                                }
                            },
                            "size": {
                                "value": 3,
                                "random": true,
                                "anim": {
                                    "enable": true
                                }
                            },
                            "line_linked": {
                                "enable": true
                            },
                            "move": {
                                "enable": true,
                                "speed": 1,
                                "direction": "top",
                                "random": true,
                                "straight": false,
                                "out_mode": "out",
                                "bounce": false,
                                "attract": {
                                    "enable": true,
                                    "rotateX": 300,
                                    "rotateY": 1200
                                }
                            }
                        },
                        "interactivity": {
                            "events": {
                                "onhover": {
                                    "enable": false
                                },
                                "onclick": {
                                    "enable": false
                                },
                                "resize": false
                            }
                        },
                        "retina_detect": true
                    });
                }
                document.head.append(script);
            });
        </script>


        <!-- header -->
        <div style="background: #00CC66; border-radius: 7px; box-shadow: 0px 2px 5px black;" class="pb-1">
            <!-- logo -->
            <div class="p-xs">
                <a href="index.php">
            <img src="image/logo.png" 
                 style="display: block;margin-left: auto;margin-right: auto;max-width: 350px;">
        </a>
            </div>

            <!-- navbar -->
           
            <div>


            </div>


     <div class="text-center mt-2">
    <a href="download/Nrorun.rar" target="_blank" class="btn btn-magick text-white m-1 animated-btn" style="border-radius: 10px;">
        <i class="fa fa-windows"></i> PC Mod
    </a>












                <a href="download/Nrorunun.apk" target="_blank"  class="btn btn-magick text-white m-1 animated-btn" style="border-radius: 10px;">
            <i class="fa fa-android"></i> APK (bản mod)
        </a>



                <a href="download/Nrorun.jar" target="_blank"  class="btn btn-magick text-white m-1 animated-btn" style="border-radius: 10px;">
            <i class="fa fa-apple"></i> java (jar)
        </a>


                <a href="download/Nrorun.ipa" target="_blank"  class="btn btn-magick text-white m-1 animated-btn" style="border-radius: 10px;">
            <i class="fa fa-apple"></i> IOS (ipa)
        </a>
        
        <a href="https://zalo.me/g/ryipvr696" target="_blank"  class="btn btn-magick text-white m-1 animated-btn" style="border-radius: 10px;">
            <i class="fa fa-apple"></i> Zalo
        </a>
        

            </div>


        </div>


        <!--body-->
		<?php if ($_login === null) { ?>
        <div class="col text-center mt-2">
		 <a href="index" class="btn btn-info m-1 text-dark" style="border-radius: 10px; font-weight: bold;">
    <i class="fa fa-home"></i>Trang Chủ
</a>
           <a href="dang-nhap" class="btn btn-info m-1 text-dark" style="border-radius: 10px; font-weight: bold;">
    <i class="fa fa-sign-in"></i>Đăng nhập
</a>

            <a href="dang-ky" class="btn btn-info m-1 text-dark" style="border-radius: 10px; font-weight: bold;">
    <i class="fa fa-user-plus"></i>Đăng Ký
</a>
           
           


    
    <?php } else {
        if ($_admin == 1) { // Kiểm tra quyền truy cập
            ?>
            
            <div class="container color-main pt-3 pb-4">
                <div class="text-center">
                    <?php
                    $query = "SELECT player.name, player.gender, account.admin FROM player LEFT JOIN account ON player.account_id = account.id";
                    $result = mysqli_query($conn, $query);
                    $prev_name = "";

                    while ($row = mysqli_fetch_assoc($result)) {
                        // Check if the username is submitted via POST method
                        if (isset($_POST['username'])) {
                            $_username = $_POST['username'];
                        }
                        $sql = "SELECT player.name, player.gender, account.admin, account.tichdiem FROM player INNER JOIN account ON account.id = player.account_id WHERE account.username='$_username'";
                        $result = mysqli_query($conn, $sql);
                        $row = mysqli_fetch_assoc($result);

                        // Hiển thị ảnh đại diện và tên người dùng
                        if (isset($row['gender'])) {
                            $gender = $row['gender'];
                            $tichdiem = $row['tichdiem'];
                            $admin = $row['admin'];
                            $avatar_url = "";

                            if ($admin == 1) {
                                if ($gender == 1) {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar18.png";
                                    } else {
                                        $avatar_url = "../image/avatar10.png";
                                    }
                                } elseif ($gender == 0) {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar19.png";
                                    } else {
                                        $avatar_url = "../image/avatar11.png";
                                    }
                                } else {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar20.png";
                                    } else {
                                        $avatar_url = "../image/avatar12.png";
                                    }
                                }
                            } else {
                                if ($gender == 1) {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar15.png";
                                    } else {
                                        $avatar_url = "../image/avatar1.png";
                                    }
                                } elseif ($gender == 0) {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar16.png";
                                    } else {
                                        $avatar_url = "../image/avatar2.png";
                                    }
                                } else {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar17.png";
                                    } else {
                                        $avatar_url = "../image/avatar0.png";
                                    }
                                }
                            }

                            $color = "";
                            if ($tichdiem >= 500) {
                                $danh_hieu = "(Chuyên Gia)";
                                $color = "#800000"; // sets color to red
                            } elseif ($tichdiem >= 300) {
                                $danh_hieu = "(Hỏi Đáp)";
                                $color = "#A0522D"; // sets color to yellow
                            } elseif ($tichdiem >= 200) {
                                $danh_hieu = "(Người Bắt Chuyện)";
                                $color = "#6A5ACD";
                            } else {
                                $danh_hieu = "";
                                $color = "";
                            }

                            if (isset($row['admin']) && $row['admin'] == 1) {
                                $name_str = '<span class="text-danger font-weight-bold">' . $row['name'] . '</span><br>';
                                $name_str .= '<span class="text-danger pt-1 mb-0">(Admin)</span>';
                            } else {
                                $name_str = '<p class="text-main font-weight-bold pt-1 mb-0">' . $row['name'] . '</p>';
                                if ($danh_hieu !== "") {
                                    $name_str .= '<div style="font-size: 9px; padding-top: 5px"><span style="color:' . $color . ' !important">' . $danh_hieu . '</span></div>';
                                }
                            }

                            echo '<div><img src="' . $avatar_url . '" alt="Avatar" style="width: 50px"></div>';
                            echo $name_str;
                        }
                        ?>
                        <p class="text-main font-weight-bold pt-1 mb-0">
                        </p>
                        <p class="pt-0" style="color:white">Số dư:
                            <?php echo number_format($_coin, 0, ',') ?> VNĐ
                        </p>
                    </div>
                    <div class="text-center mb-2">
                        <a class="btn btn-action m-1 text-white" style="border-radius: 10px;" href="panel">Panel Admin</a>
                        </div>
                    <div class="text-center mb-2">
                        <a class="btn btn-action m-1 text-white" style="border-radius: 10px;" href="../nap-so-du">Nạp số dư</a>
                        <a class="btn btn-action m-1 text-white" style="border-radius: 10px;" href="../doi-thoi-vang">Đổi thỏi vàng</a>
                    </div>
                    <div class="row mb-3">
                        <div class="col text-center">
                            <div class="btn-group"> <button type="button"
                                    data-toggle="dropdown"
                                    class="btn btn-action m-1 text-white  dropdown-toggle" style="border-radius: 10px;"> <i class="fa fa-lock"></i> Bảo mật </button>
                                <div class="dropdown-menu">
                                    <a class="dropdown-item" href="../doi-mat-khau">Đổi mật khẩu</a>
                                    <a class="dropdown-item" href="../pass2">Mật khẩu cấp 2</a>
                                    <a class="dropdown-item" href="../cap-nhat-thong-tin">Cập Nhật Tài Khoản</a>
                                </div>
                            </div>
                            <a class="btn btn-action m-1 text-white" style="border-radius: 10px;" onclick="confirmLogout()"> <i class="fa fa-sign-out"></i>Thoát</a>
                        </div>
                    </div>
                </div>
            <?php }
        } else { ?>
            
            <div class="container color-main pt-3 pb-4">
                <div class="text-center">
                    <?php
                    $query = "SELECT player.name, player.gender, account.admin FROM player LEFT JOIN account ON player.account_id = account.id";
                    $result = mysqli_query($conn, $query);
                    $prev_name = "";

                    while ($row = mysqli_fetch_assoc($result)) {
                        // Check if the username is submitted via POST method
                        if (isset($_POST['username'])) {
                            $_username = $_POST['username'];
                        }
                        $sql = "SELECT player.name, player.gender, account.admin, account.tichdiem FROM player INNER JOIN account ON account.id = player.account_id WHERE account.username='$_username'";
                        $result = mysqli_query($conn, $sql);
                        $row = mysqli_fetch_assoc($result);

                        // Hiển thị ảnh đại diện và tên người dùng
                        if (isset($row['gender'])) {
                            $gender = $row['gender'];
                            $tichdiem = $row['tichdiem'];
                            $admin = $row['admin'];
                            $avatar_url = "";

                            if ($admin == 1) {
                                if ($gender == 1) {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar18.png";
                                    } else {
                                        $avatar_url = "../image/avatar10.png";
                                    }
                                } elseif ($gender == 0) {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar19.png";
                                    } else {
                                        $avatar_url = "../image/avatar11.png";
                                    }
                                } else {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar20.png";
                                    } else {
                                        $avatar_url = "../image/avatar12.png";
                                    }
                                }
                            } else {
                                if ($gender == 1) {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar15.png";
                                    } else {
                                        $avatar_url = "../image/avatar1.png";
                                    }
                                } elseif ($gender == 0) {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar16.png";
                                    } else {
                                        $avatar_url = "../image/avatar2.png";
                                    }
                                } else {
                                    if (date('m') == 12 && date('d') == 25) {
                                        $avatar_url = "../image/avatar17.png";
                                    } else {
                                        $avatar_url = "../image/avatar0.png";
                                    }
                                }
                            }
                            $color = "";
                            if ($tichdiem >= 200) {
                                $danh_hieu = "(Chuyên Gia)";
                                $color = "#800000"; // sets color to red
                            } elseif ($tichdiem >= 100) {
                                $danh_hieu = "(Hỏi Đáp)";
                                $color = "#A0522D"; // sets color to yellow
                            } elseif ($tichdiem >= 35) {
                                $danh_hieu = "(Người Bắt Chuyện)";
                                $color = "#6A5ACD";
                            } else {
                                $danh_hieu = "";
                                $color = "";
                            }

                            if (isset($row['admin']) && $row['admin'] == 1) {
                                $name_str = '<span class="text-danger font-weight-bold">' . $row['name'] . '</span><br>';
                                $name_str .= '<span class="text-danger pt-1 mb-0">(Admin)</span>';
                            } else {
                                $name_str = '<p class="text-main font-weight-bold pt-1 mb-0">' . $row['name'] . '</p>';
                                if ($danh_hieu !== "") {
                                    $name_str .= '<div style="font-size: 9px; padding-top: 5px"><span style="color:' . $color . ' !important">' . $danh_hieu . '</span></div>';
                                }
                            }

                            echo '<div><img src="' . $avatar_url . '" alt="Avatar" style="width: 50px"></div>';
                            echo $name_str;
                        }
                    }
                    ?>
                    <p class="text-main font-weight-bold pt-1 mb-0">
                    </p>
                    <p class="pt-0">Số dư:
                        <?php echo number_format($_coin, 0, ',') ?> VNĐ
                    </p>
                </div>
                <div class="text-center mb-2">
                    <a class="btn btn-action m-1 text-white" style="border-radius: 10px;" href="../nap-so-du">Nạp số dư</a>
               
                    <a class="btn btn-action m-1 text-white" style="border-radius: 10px;" href="../doi-thoi-vang">Đổi thỏi vàng</a>
                </div>
                <div class="row mb-3">
                    <div class="col text-center">
                       <div class="btn-group"> <button type="button"
                                    data-toggle="dropdown"
                                    class="btn btn-action m-1 text-white  dropdown-toggle" style="border-radius: 10px;"> <i class="fa fa-lock"></i> Bảo mật </button>
                                <div class="dropdown-menu">
                                <a class="dropdown-item" href="../doi-mat-khau">Đổi mật khẩu</a>
                                <a class="dropdown-item" href="../pass2">Mật khẩu cấp 2</a>
                                <a class="dropdown-item" href="../cap-nhat-thong-tin">Cập Nhật Tài Khoản</a>
                            </div>
                        </div>
                        <a class="btn btn-action m-1 text-white" style="border-radius: 10px;" href="../logout">Thoát</a>
                    </div>
                </div>
            </div>
            <?php
        }
    }
    ?>
    
    <style>
        #snow {
            position: fixed;
            top: 0;
            left: 0;
            z-index: -1;
            width: 100%;
            height: 100%;
            pointer-events: none;
        }

        body {
            background: #b3afaf url(../image/background.jpg);
            background-size: 1920px 970px;
            background-repeat: no-repeat;
            background-attachment: fixed;
        }
    </style>

    <body>
        <div id="snow"><canvas class="particles-js-canvas-el" width="860" height="912"
                style="width: 100%; height: 100%;"></canvas></div>