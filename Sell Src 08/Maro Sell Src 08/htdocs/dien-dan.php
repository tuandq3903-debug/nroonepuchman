<?php
// 
include_once 'connect.php';
include_once 'set.php';
include('head.php');
?>

<!DOCTYPE html>
<html>



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
                                "value": "#d3077d"
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


    <div class="p-1 mt-1 alert alert-danger" style="border-radius: 7px; box-shadow: 0px 0px 5px black;">
	<div class="alert alert-danger" style="border-radius: 7px;">
	<div class="p-1">
        
          
               
                
                 <div class="post-section" style="margin-bottom: 20px;">
            <div class="post-box" style="border-radius: 7px; padding: 8px; margin-bottom: 8px; border: 1px solid #00CC66;">
                <div class="post-item d-flex align-items-center my-2 justify-content-between">
                    <div class="post-image">
                        <img src="/image/avatar20.png" alt="htth-su-kien-gio-to-hung-vuong" width="50px" height="50px" class="avatar">
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


    <script src="assets/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/main.js"></script>
</body><!-- Bootstrap core JavaScript -->

</html>