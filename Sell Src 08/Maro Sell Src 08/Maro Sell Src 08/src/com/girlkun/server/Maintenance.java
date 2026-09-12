package com.girlkun.server;

import com.girlkun.services.Service;
import com.girlkun.utils.Logger;


public class Maintenance extends Thread {

    public static boolean isRunning = false;
    public boolean canUseCode;
    private static Maintenance i;

    private int min;
    private int time;

    private Maintenance() {

    }

    public static Maintenance gI() {
        if (i == null) {
            i = new Maintenance();
        }
        return i;
    }

   public void start(int min) {
        if (!isRunning) {
            isRunning = true;
            this.time = min;
            this.start();
        }
    }

    @Override
    public void run() {
        while (this.min > 0) {
            this.min--;
            Service.gI().sendThongBaoAllPlayer("Hệ thống sẽ bảo trì sau " + min
                    + " giây nữa, vui lòng thoát game để tránh mất vật phẩm");
            try {
                Thread.sleep(1000);
            } catch (Exception e) {
            }
          
        }
        Logger.error("BEGIN MAINTENANCE...............................\n");
        ServerManager.gI().close(100);
    }

}
