@ECHO OFF
cd /d "%~dp0"

echo Dang build source Java...
set "JAVA_HOME=C:\Program Files\Java\jdk-21.0.11"
if not exist "%JAVA_HOME%\bin\java.exe" (
	echo Khong tim thay JDK 21 tai: %JAVA_HOME%
	pause
	exit /b 1
)
set "PATH=%JAVA_HOME%\bin;%PATH%"
set "ANT_HOME=C:\Users\Admin\Downloads\apache-ant-1.10.18-bin\apache-ant-1.10.18"
if not exist "%ANT_HOME%\bin\ant.bat" (
	echo Khong tim thay Apache Ant tai: %ANT_HOME%
	pause
	exit /b 1
)
call "%ANT_HOME%\bin\ant.bat" clean jar
if errorlevel 1 (
	echo Build that bai. Server khong duoc khoi dong.
	pause
	exit /b 1
)

if not exist "dist\Maro_Sell_Src_08.jar" (
	echo Khong tim thay dist\Maro_Sell_Src_08.jar.
	echo Hay build project truoc bang NetBeans: Clean and Build.
	pause
	exit /b 1
)
java -server -Dfile.encoding=UTF-8 -Xms1000M -Xmx1000M -cp "dist\Maro_Sell_Src_08.jar;lib\*" com.girlkun.server.ServerManager
PAUSE