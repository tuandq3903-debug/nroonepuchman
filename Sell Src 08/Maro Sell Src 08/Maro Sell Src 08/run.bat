@ECHO OFF
cd /d "%~dp0"
if not exist "dist\Maro_Sell_Src_08.jar" (
	echo Khong tim thay dist\Maro_Sell_Src_08.jar.
	echo Hay build project truoc bang NetBeans: Clean and Build.
	pause
	exit /b 1
)
java -server -Dfile.encoding=UTF-8 -Xms1000M -Xmx1000M -cp "dist\Maro_Sell_Src_08.jar;lib\*" com.girlkun.server.ServerManager
PAUSE