package com.girlkun.server;

import com.arriety.MaQuaTang.MaQuaTangManager;
import com.girlkun.database.GirlkunDB;

import java.net.ServerSocket;

import com.girlkun.jdbc.daos.HistoryTransactionDAO;
import com.girlkun.models.boss.BossManager;
import com.girlkun.models.item.Item;
import com.girlkun.models.map.challenge.MartialCongressManager;
import com.girlkun.models.map.vodai.VoDaiManager;
import com.girlkun.models.matches.pvp.DaiHoiVoThuat;
import com.girlkun.models.player.Player;
import com.girlkun.network.session.ISession;
import com.girlkun.network.example.MessageSendCollect;
import com.girlkun.network.server.GirlkunServer;
import com.girlkun.network.server.IServerClose;
import com.girlkun.network.server.ISessionAcceptHandler;
import com.girlkun.server.io.MyKeyHandler;
import com.girlkun.server.io.MySession;
import com.girlkun.services.ClanService;
import com.girlkun.services.InventoryServiceNew;
import com.girlkun.services.NgocRongNamecService;
import com.girlkun.services.Service;
import com.girlkun.services.func.ChonAiDay;
import com.girlkun.services.func.TaiXiu;
import com.girlkun.services.func.TopService;
import com.girlkun.utils.Logger;
import com.girlkun.utils.TimeUtil;
import com.girlkun.utils.Util;
import com.kygui.ShopKyGuiManager;
import java.io.IOException;

import java.util.*;
import java.util.concurrent.TimeUnit;
import java.util.logging.Level;

public class ServerManager {

    public int threadMap;
    public static String timeStart;
    public static final Map CLIENTS = new HashMap();
    public static String NAME = "Bùi Kim Trường";
    public static int PORT = 14445;
    private static ServerManager instance;
    public static ServerSocket listenSocket;
    public static boolean isRunning;

    public void init() {
        Manager.gI();
        try {
            if (Manager.LOCAL) {
                return;
            }
            GirlkunDB.executeUpdate("update account set last_time_login = '2000-01-01', "
                    + "last_time_logout = '2001-01-01'");
        } catch (Exception e) {
        }
        HistoryTransactionDAO.deleteHistory();
    }

    public static ServerManager gI() {
        if (instance == null) {
            instance = new ServerManager();
            instance.init();
        }
        return instance;
    }

    public static void main(String[] args) {
        timeStart = TimeUtil.getTimeNow("dd/MM/yyyy HH:mm:ss");
        ServerManager.gI().run();
    }

    public void run() {
        long delay = 500;
        isRunning = true;
        activeCommandLine();
        activeGame();
        activeServerSocket();

        Logger.log(Logger.GREEN, "Time start server: " + ServerManager.timeStart + "\n");

        new Thread(TaiXiu.gI(), "Thread TaiXiu").start();

        new Thread(AutoMaintenance.gI(), "Thread Bảo Trì Tự Động").start();

        new Thread(VoDaiManager.gI(), "Update Võ Đài Sinh Tử").start();

        new Thread(() -> {
            while (isRunning) {
                try {
                    long start = System.currentTimeMillis();
                    MartialCongressManager.gI().update();
                    long timeUpdate = System.currentTimeMillis() - start;
                    if (timeUpdate < 500) {
                        try {
                            TimeUnit.MILLISECONDS.sleep(500 - timeUpdate);
                        } catch (InterruptedException ex) {
                        }
                    }
                } catch (Exception e) {
                }
            }
        }, "Update dai hoi vo thuat").start();
        try {
            Thread.sleep(1000);
            BossManager.gI().loadBoss();
            Manager.MAPS.forEach(com.girlkun.models.map.Map::initBoss);
        } catch (InterruptedException ex) {
            java.util.logging.Logger.getLogger(BossManager.class.getName()).log(Level.SEVERE, null, ex);
        }

    }

    private void act() throws Exception {
        GirlkunServer.gI().init().setAcceptHandler(new ISessionAcceptHandler() {
            @Override
            public void sessionInit(ISession is) {
//                antiddos girlkun
                if (!canConnectWithIp(is.getIP())) {
                    is.disconnect();

                    return;
                }

                is = is.setMessageHandler(Controller.getInstance())
                        .setSendCollect(new MessageSendCollect())
                        .setKeyHandler(new MyKeyHandler())
                        .startCollect();
            }

            @Override
            public void sessionDisconnect(ISession session) {
                Client.gI().kickSession((MySession) session);
            }
        }).setTypeSessioClone(MySession.class)
                .setDoSomeThingWhenClose(new IServerClose() {
                    @Override
                    public void serverClose() {
                        System.out.println("Địt Nhau");
                        System.exit(0);
                    }
                })
                .start(PORT);

    }

    private void activeServerSocket() {
        if (true) {
            try {
                this.act();
            } catch (Exception e) {
            }
            return;
        }
//        try {
//            Logger.log(Logger.PURPLE, "Start server......... Current thread: " + Thread.activeCount() + "\n");
//            listenSocket = new ServerSocket(PORT);
//            while (isRunning) {
//                try {
//                    Socket sc = listenSocket.accept();
//                    String ip = (((InetSocketAddress) sc.getRemoteSocketAddress()).getAddress()).toString().replace("/", "");
//                    if (canConnectWithIp(ip)) {
//                        Session session = new Session(sc, ip);
//                        session.ipAddress = ip;
//                    } else {
//                        sc.close();
//                    }
//                } catch (Exception e) {
////                        Logger.logException(ServerManager.class, e);
//                }
//            }
//            listenSocket.close();
//        } catch (Exception e) {
//            Logger.logException(ServerManager.class, e, "Lỗi mở port");
//            System.exit(0);
//        }
    }

    private boolean canConnectWithIp(String ipAddress) {
        Object o = CLIENTS.get(ipAddress);
        if (o == null) {
            CLIENTS.put(ipAddress, 1);
            return true;
        } else {
            int n = Integer.parseInt(String.valueOf(o));
            if (n < Manager.MAX_PER_IP) {
                n++;
                CLIENTS.put(ipAddress, n);
                return true;
            } else if (n > Manager.MAX_PER_IP) {
                GirlkunServer.gI().stopConnect();
            } else {
//                
                return false;
            }
        }
        return false;
    }

    public void disconnect(MySession session) {
        Object o = CLIENTS.get(session.getIP());
        if (o != null) {
            int n = Integer.parseInt(String.valueOf(o));
            n--;
            if (n < 0) {
                n = 0;
            }
            CLIENTS.put(session.getIP(), n);
        }
    }

    private void activeCommandLine() {
        new Thread(() -> {
            Scanner sc = new Scanner(System.in);
            while (true) {
                String line = sc.nextLine();
                if (line.equals("1")) {
                    new Thread(() -> {
                        Client.gI().close();
                        ClanService.gI().close();
                        Logger.error("Save " + Manager.CLANS.size() + " bang");
                    }).start();
                    Maintenance.gI().start(1);

                } else if (line.equals("athread")) {
                    ServerNotify.gI().notify("debug server: " + Thread.activeCount());
                } else if (line.equals("online")) {
                    Logger.error("Số Player Online: " + Client.gI().getPlayers().size() + "\n");
//                } else if (line.equals("2")) {
//                    new Thread(() -> {
//                        Client.gI().close();
//                        ClanService.gI().close();
//                            Logger.error("Save " + Manager.CLANS.size() + " bang");
//                    }).start();
//                } else if (line.startsWith("3")) {
//                    new Thread(() -> {
//                        try {
//                            ClanService.gI().close();
//                            Logger.error("Save " + Manager.CLANS.size() + " bang");
//                        } catch (Exception e) {
//                            Logger.error("Lỗi save clan!...................................\n");
//                        }
//                    }).start();
                } else if (line.startsWith("a")) {
                    String a = line.replace("a ", "");
                    Service.gI().sendThongBaoAllPlayer(a);
                } else if (line.startsWith("qua")) {
//                    =1-1-1-1=1-1-1-1=
//                     =playerId-quantily-itemId-sql=optioneId-pagram=

                    try {
                        List<Item.ItemOption> ios = new ArrayList<>();
                        String[] pagram1 = line.split("=")[1].split("-");
                        String[] pagram2 = line.split("=")[2].split("-");
                        if (pagram1.length == 4 && pagram2.length % 2 == 0) {
                            Player p = Client.gI().getPlayer(Integer.parseInt(pagram1[0]));
                            if (p != null) {
                                for (int i = 0; i < pagram2.length; i += 2) {
                                    ios.add(new Item.ItemOption(Integer.parseInt(pagram2[i]), Integer.parseInt(pagram2[i + 1])));
                                }
                                Item i = Util.sendDo(Integer.parseInt(pagram1[2]), Integer.parseInt(pagram1[3]), ios);
                                i.quantity = Integer.parseInt(pagram1[1]);
                                InventoryServiceNew.gI().addItemBag(p, i);
                                InventoryServiceNew.gI().sendItemBags(p);
                                Service.gI().sendThongBao(p, "Admin trả đồ. anh em thông cảm nhé...");
                            } else {
                                System.out.println("Người chơi không online");
                            }
                        }
                    } catch (Exception e) {
                        System.out.println("Lỗi quà");
                    }
                }
            }
        }, "Active line").start();
    }

    private void activeGame() {
    }

    public void close(long delay) {
        GirlkunServer.gI().stopConnect();

        isRunning = false;
        try {
            ClanService.gI().close();
        } catch (Exception e) {
            Logger.error("Lỗi save clan!...................................\n");
        }
        Client.gI().close();

        Logger.success("SUCCESSFULLY MAINTENANCE!...................................\n");
        if (AutoMaintenance.isRunning) {
            AutoMaintenance.isRunning = false;
            try {
                String batchFilePath = "run.bat";
                AutoMaintenance.runBatchFile(batchFilePath);
            } catch (IOException e) {
            }
        }
        System.exit(0);
    }
}
