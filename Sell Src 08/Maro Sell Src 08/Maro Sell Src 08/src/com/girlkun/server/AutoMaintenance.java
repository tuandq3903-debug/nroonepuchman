package com.girlkun.server;

import com.girlkun.server.Maintenance;
import com.girlkun.utils.Logger;
import java.io.IOException;
import static java.time.LocalDate.now;
import java.time.LocalTime;

public class AutoMaintenance extends Thread {

    public static boolean AutoMaintenance = true;
    public static final int hours = 2;
    public static final int mins = 2;
    private static AutoMaintenance instance;
    public static boolean isRunning;

    public static AutoMaintenance gI() {
        if (instance == null) {
            instance = new AutoMaintenance();
        }
        return instance;
    }

    @Override
    public void run() {
        while (!Maintenance.isRunning && !isRunning) {
            try {
                if (AutoMaintenance) {
                    LocalTime currentTime = LocalTime.now();
                    if (currentTime.getHour() == hours && currentTime.getMinute() == mins) {
                        Logger.log(Logger.PURPLE, "Đang Trong Quá Trình Tiến Hành Bảo Trì Tự Động\n");
                        Maintenance.gI().start(15);
                        isRunning = true;
                        AutoMaintenance = false;

                    }
                }
                Thread.sleep(1000);

            } catch (Exception e) {
            }
        }
    }

    public static void runBatchFile(String batchFilePath) throws IOException {
        ProcessBuilder processBuilder = new ProcessBuilder("cmd", "/c", "start", batchFilePath);
        Process process = processBuilder.start();
        try {
            process.waitFor();
        } catch (Exception e) {
        }
    }
}
