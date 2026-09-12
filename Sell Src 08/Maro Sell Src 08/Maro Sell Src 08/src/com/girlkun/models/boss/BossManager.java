package com.girlkun.models.boss;

import com.girlkun.jdbc.daos.GodGK;
import com.girlkun.models.boss.list_boss.BLACK.*;
import com.girlkun.models.boss.list_boss.BatGioi;
import com.girlkun.models.boss.list_boss.Cooler.Cooler;
import com.girlkun.models.boss.list_boss.HuyDiet.Champa;
import com.girlkun.models.boss.list_boss.HuyDiet.ThanHuyDiet;
import com.girlkun.models.boss.list_boss.HuyDiet.ThienSuWhis;
import com.girlkun.models.boss.list_boss.HuyDiet.Vados;
import com.girlkun.models.boss.list_boss.NgucTu.CoolerGold;
import com.girlkun.models.boss.list_boss.Doraemon.Doraemon;
import com.girlkun.models.boss.list_boss.FideBack.Kingcold;
import com.girlkun.models.boss.list_boss.Mabu;
import com.girlkun.models.boss.list_boss.SuperXen;
import com.girlkun.models.boss.list_boss.NgucTu.Cumber;
import com.girlkun.models.boss.list_boss.cell.Xencon;
import com.girlkun.models.boss.list_boss.android.*;
import com.girlkun.models.boss.list_boss.cell.SieuBoHung;
import com.girlkun.models.boss.list_boss.cell.XenBoHung;
import com.girlkun.models.boss.list_boss.doanh_trai.TrungUyTrang;
import com.girlkun.models.boss.list_boss.Broly.Broly;
import com.girlkun.models.boss.list_boss.Doraemon.Nobita;
import com.girlkun.models.boss.list_boss.Doraemon.Xeko;
import com.girlkun.models.boss.list_boss.Doraemon.Xuka;
import com.girlkun.models.boss.list_boss.FideBack.FideRobot;
import com.girlkun.models.boss.list_boss.NgucTu.SongokuTaAc;
import com.girlkun.models.boss.list_boss.fide.Fide;
import com.girlkun.models.boss.list_boss.Doraemon.Chaien;
import com.girlkun.models.boss.list_boss.Halloween.BoXuong;
import com.girlkun.models.boss.list_boss.Halloween.DoiNhi;
import com.girlkun.models.boss.list_boss.NRD.Rong1Sao;
import com.girlkun.models.boss.list_boss.NRD.Rong2Sao;
import com.girlkun.models.boss.list_boss.NRD.Rong3Sao;
import com.girlkun.models.boss.list_boss.NRD.Rong4Sao;
import com.girlkun.models.boss.list_boss.NRD.Rong5Sao;
import com.girlkun.models.boss.list_boss.NRD.Rong6Sao;
import com.girlkun.models.boss.list_boss.NRD.Rong7Sao;
import com.girlkun.models.boss.list_boss.Mabu12h.MabuBoss;
import com.girlkun.models.boss.list_boss.Mabu12h.BuiBui;
import com.girlkun.models.boss.list_boss.Mabu12h.BuiBui2;
import com.girlkun.models.boss.list_boss.Mabu12h.Drabura;
import com.girlkun.models.boss.list_boss.Mabu12h.Drabura2;
import com.girlkun.models.boss.list_boss.Mabu12h.Yacon;
import com.girlkun.models.boss.list_boss.ThoDaiCa;
import com.girlkun.models.boss.list_boss.ginyu.So1;
import com.girlkun.models.boss.list_boss.ginyu.So2;
import com.girlkun.models.boss.list_boss.ginyu.So3;
import com.girlkun.models.boss.list_boss.ginyu.So4;
import com.girlkun.models.boss.list_boss.ginyu.Tieudoitruong;
import com.girlkun.models.boss.list_boss.kami.cumberBlack;
import com.girlkun.models.boss.list_boss.kami.cumberYellow;
import com.girlkun.models.boss.list_boss.kami.kamiLoc;
import com.girlkun.models.boss.list_boss.kami.kamiRin;
import com.girlkun.models.boss.list_boss.kami.kamiSooMe;
import com.girlkun.models.boss.list_boss.nappa.*;
import com.girlkun.models.boss.list_boss.sontinhthuytinh.Sontinh;
import com.girlkun.models.boss.list_boss.sontinhthuytinh.Thuytinh;
import com.girlkun.models.player.Player;
import com.girlkun.network.io.Message;
import com.girlkun.server.ServerManager;
import com.girlkun.services.ItemMapService;
import com.girlkun.services.MapService;
import com.girlkun.services.Service;
import com.girlkun.services.func.ChangeMapService;
import com.girlkun.utils.Util;
import java.io.IOException;
import java.text.Normalizer;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;

public class BossManager implements Runnable {

    private static BossManager I;
    public static final byte ratioReward = 2;

    public static BossManager gI() {
        if (BossManager.I == null) {
            BossManager.I = new BossManager();
        }
        return BossManager.I;
    }

    private BossManager() {
        this.bosses = new ArrayList<>();
    }

    private boolean loadedBoss;
    private final List<Boss> bosses;

    public void addBoss(Boss boss) {
        this.bosses.add(boss);
    }

    public void removeBoss(Boss boss) {
        this.bosses.remove(boss);
    }

    public void loadBoss() {
        if (this.loadedBoss) {
            return;
        }
        try {
            this.createBoss(BossID.KAMILOC);
            this.createBoss(BossID.CUMBERBLACK);
            this.createBoss(BossID.SUPER_XEN);
            this.createBoss(BossID.SO_4);
            this.createBoss(BossID.KING_KONG);
            this.createBoss(BossID.SONGOKU_TA_AC);
            this.createBoss(BossID.CUMBER);
            this.createBoss(BossID.COOLER_GOLD);
            this.createBoss(BossID.XEN_BO_HUNG);
            this.createBoss(BossID.SIEU_BO_HUNG);
            this.createBoss(BossID.XEN_CON_1);
            this.createBoss(BossID.XEN_CON_1);
            this.createBoss(BossID.XEN_CON_1);
            this.createBoss(BossID.XEN_CON_1);
            this.createBoss(BossID.THIEN_SU_VADOS);
            this.createBoss(BossID.THIEN_SU_WHIS);
            this.createBoss(BossID.THIEN_SU_VADOS);
            this.createBoss(BossID.THIEN_SU_WHIS);
            this.createBoss(BossID.THIEN_SU_VADOS);
            this.createBoss(BossID.THIEN_SU_WHIS);
            this.createBoss(BossID.THIEN_SU_VADOS);
            this.createBoss(BossID.THIEN_SU_WHIS);
            this.createBoss(BossID.DORAEMON);
            this.createBoss(BossID.BLACK);
            this.createBoss(BossID.BLACK2);
            this.createBoss(BossID.ZAMASMAX);
            this.createBoss(BossID.BLACK);
            this.createBoss(BossID.KUKU);
            this.createBoss(BossID.MAP_DAU_DINH);
            this.createBoss(BossID.RAMBO);
            this.createBoss(BossID.FIDE);
            this.createBoss(BossID.ANDROID_19);
            this.createBoss(BossID.ANDROID_14);
            this.createBoss(BossID.MABU);
            this.createBoss(BossID.THO_DAI_CA);
            this.createBoss(BossID.BO_XUONG);
            this.createBoss(BossID.BAT_GIOI);
            this.createBoss(BossID.DOI_NHI);
            for (int i = 1; i <= 5; i++) {
                this.createBoss(BossID.BROLY_THUONG + i);
            }

        } catch (Exception ex) {
            ex.printStackTrace();
        }
        this.loadedBoss = true;
        new Thread(BossManager.I, "Update boss").start();
    }

    public Boss createBoss(int bossID) {
        try {
            if (bossID <= BossID.BROLY_THUONG + 5 && bossID >= BossID.BROLY_THUONG) {

                return new Broly(MapService.gI().getZone(Util.randomMapBossBroly()), 500, 10000);
            }
            switch (bossID) {
                case BossID.BAT_GIOI:
                    return new BatGioi();
                case BossID.THO_DAI_CA:
                    return new ThoDaiCa();
                case BossID.CUMBERYELLOW:
                    return new cumberYellow();
                case BossID.CUMBERBLACK:
                    return new cumberBlack();
                case BossID.KAMIRIN:
                    return new kamiRin();
                case BossID.KAMILOC:
                    return new kamiLoc();
                case BossID.KAMI_SOOME:
                    return new kamiSooMe();
                case BossID.KUKU:
                    return new Kuku();
                case BossID.MAP_DAU_DINH:
                    return new MapDauDinh();
                case BossID.RAMBO:
                    return new Rambo();
                case BossID.DRABURA:
                    return new Drabura();
                case BossID.DRABURA_2:
                    return new Drabura2();
                case BossID.BUI_BUI:
                    return new BuiBui();
                case BossID.BUI_BUI_2:
                    return new BuiBui2();
                case BossID.YA_CON:
                    return new Yacon();
                case BossID.MABU_12H:
                    return new MabuBoss();
                case BossID.Rong_1Sao:
                    return new Rong1Sao();
                case BossID.Rong_2Sao:
                    return new Rong2Sao();
                case BossID.Rong_3Sao:
                    return new Rong3Sao();
                case BossID.Rong_4Sao:
                    return new Rong4Sao();
                case BossID.Rong_5Sao:
                    return new Rong5Sao();
                case BossID.Rong_6Sao:
                    return new Rong6Sao();
                case BossID.Rong_7Sao:
                    return new Rong7Sao();
                case BossID.FIDE:
                    return new Fide();
                case BossID.DR_KORE:
                    return new DrKore();
                case BossID.ANDROID_19:
                    return new Android19();
                case BossID.ANDROID_13:
                    return new Android13();
                case BossID.ANDROID_14:
                    return new Android14();
                case BossID.ANDROID_15:
                    return new Android15();
                case BossID.SUPER_XEN:
                    return new SuperXen();
                case BossID.SUPER_ANDROID_17:
                    return new SuperAndroid17();
                case BossID.PIC:
                    return new Pic();
                case BossID.POC:
                    return new Poc();
                case BossID.KING_KONG:
                    return new KingKong();
                case BossID.XEN_BO_HUNG:
                    return new XenBoHung();
                case BossID.SIEU_BO_HUNG:
                    return new SieuBoHung();
                case BossID.TRUNG_UY_TRANG:
                    return new TrungUyTrang();
                case BossID.XUKA:
                    return new Xuka();
                case BossID.NOBITA:
                    return new Nobita();
                case BossID.XEKO:
                    return new Xeko();
                case BossID.CHAIEN:
                    return new Chaien();
                case BossID.DORAEMON:
                    return new Doraemon();
                case BossID.VUA_COLD:
                    return new Kingcold();
                case BossID.FIDE_ROBOT:
                    return new FideRobot();
                case BossID.COOLER:
                    return new Cooler();
                case BossID.ZAMASMAX:
                    return new ZamasMax();
                case BossID.ZAMASZIN:
                    return new ZamasKaio();
                case BossID.BLACK2:
                    return new SuperBlack2();
                case BossID.BLACK1:
                    return new BlackGokuTl();
                case BossID.BLACK:
                    return new Black();
                case BossID.BLACK3:
                    return new BlackGokuBase();
                case BossID.XEN_CON_1:
                    return new Xencon();
                case BossID.MABU:
                    return new Mabu();
                case BossID.TIEU_DOI_TRUONG:
                    return new Tieudoitruong();
                case BossID.SO_4:
                    return new So4();
                case BossID.SO_3:
                    return new So3();
                case BossID.SO_2:
                    return new So2();
                case BossID.SO_1:
                    return new So1();
                case BossID.COOLER_GOLD:
                    return new CoolerGold();
                case BossID.CUMBER:
                    return new Cumber();
                case BossID.THAN_HUY_DIET_CHAMPA:
                    return new Champa();
                case BossID.THIEN_SU_VADOS:
                    return new Vados();
                case BossID.THAN_HUY_DIET:
                    return new ThanHuyDiet();
                case BossID.THIEN_SU_WHIS:
                    return new ThienSuWhis();
                case BossID.SONGOKU_TA_AC:
                    return new SongokuTaAc();
                case BossID.BO_XUONG:
                    return new BoXuong();
                case BossID.DOI_NHI:
                    return new DoiNhi();

                default:
                    return null;
            }
        } catch (Exception e) {
            return null;
        }
    }

    public boolean existBossOnPlayer(Player player) {
        return player.zone.getBosses().size() > 0;
    }

    public void teleBoss(Player pl, Message _msg) {
        if (_msg != null) {
            try {
                int id = _msg.reader().readInt();
                Boss b = getBossById(id);
                if (b == null) {
                    Player player = GodGK.loadById(id);
                    if (player != null && player.zone != null) {
                        ChangeMapService.gI().changeMapYardrat(pl, player.zone, player.location.x, player.location.y);
                        return;
                    } else {
                        Service.gI().sendThongBao(pl, "Nó trốn rồi");
                        return;
                    }
                }
                if (!b.isDie() && b.zone != null) {
                    ChangeMapService.gI().changeMapYardrat(pl, b.zone, b.location.x, b.location.y);
                } else {
                    Service.gI().sendThongBao(pl, "Boss Hẹo Rồi");
                }
            } catch (IOException e) {
                System.out.println("Loi tele boss");
                e.printStackTrace();
            }
        }
    }

    public void summonBoss(Player pl, Message _msg) {
        if (!pl.getSession().isAdmin) {
            Service.gI().sendThongBao(pl, "Chỉ dành cho Admin");
            return;
        }
        if (_msg != null) {
            try {
                int id = _msg.reader().readInt();
                Boss b = getBossById(id);
                if (b != null && b.zone != null) {
                    ChangeMapService.gI().changeMapYardrat(b, pl.zone, pl.location.x, pl.location.y);
                    return;
                }
                if (b == null) {
                    Player player = GodGK.loadById(id);
                    if (player != null && player.zone != null) {
                        ChangeMapService.gI().changeMapYardrat(player, pl.zone, pl.location.x, pl.location.y);
                    } else {
                        Service.gI().sendThongBao(pl, "Nó trốn rồi");
                    }
                }
            } catch (IOException e) {
                System.out.println("Loi summon boss");
            }
        }
    }

    public void showListBoss(Player player) {
        if (!player.isAdmin()) {
            return;
        }
        Message msg;
        try {
            msg = new Message(-96);
            msg.writer().writeByte(1);
            msg.writer().writeUTF("Boss");
            msg.writer().writeByte(
                    (int) bosses.stream()
                            .filter(boss -> !MapService.gI().isMapMaBu(boss.data[0].getMapJoin()[0])
                            && !MapService.gI().isMapDoanhTrai(boss.data[0].getMapJoin()[0])
                            && !MapService.gI().isMapBlackBallWar(boss.data[0].getMapJoin()[0])
                            && !MapService.gI().isMapKhiGas(boss.data[0].getMapJoin()[0])
                            && !MapService.gI().isMapGiaiCuuMiNuong(boss.data[0].getMapJoin()[0]))
                            .count());
            for (int i = 0; i < bosses.size(); i++) {
                Boss boss = this.bosses.get(i);
                if (MapService.gI().isMapMaBu(boss.data[0].getMapJoin()[0])
                        || MapService.gI().isMapBlackBallWar(boss.data[0].getMapJoin()[0])
                        || MapService.gI().isMapBanDoKhoBau(boss.data[0].getMapJoin()[0])
                        || MapService.gI().isMapDoanhTrai(boss.data[0].getMapJoin()[0])) {
                    continue;
                }
                msg.writer().writeInt((int) boss.id);
                msg.writer().writeInt((int) boss.id);
                msg.writer().writeShort(boss.data[0].getOutfit()[0]);
                msg.writer().writeShort(-1);
                msg.writer().writeShort(boss.data[0].getOutfit()[1]);
                msg.writer().writeShort(boss.data[0].getOutfit()[2]);
                msg.writer().writeUTF(boss.data[0].getName());
                if (boss.zone != null) {
                    msg.writer().writeUTF("Sống");
                    msg.writer().writeUTF("Thông Tin Boss\n" + "|7|Map : " + boss.zone.map.mapName + "(" + boss.zone.map.mapId + ") \nZone: " + boss.zone.zoneId + "\nHP: " + Util.powerToString(boss.nPoint.hp) + "\nDame: " + Util.powerToString(boss.nPoint.dame));
                } else {
                    msg.writer().writeUTF("Chết");
                    msg.writer().writeUTF("Boss Respawn\n|7|Time to Reset : " + (boss.secondsRest <= 0 ? "BossAppear" : boss.secondsRest + " giây"));
                }
            }
            player.sendMessage(msg);
            msg.cleanup();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void dobossmember(Player player) {
        Message msg;
        try {
            msg = new Message(-96);
            msg.writer().writeByte(0);
            msg.writer().writeUTF("Boss");
            msg.writer().writeByte((int) bosses.stream().filter(boss -> !MapService.gI().isMapMaBu(boss.data[0].getMapJoin()[0])
                    && !MapService.gI().isMapDoanhTrai(boss.data[0].getMapJoin()[0])
                    && !MapService.gI().isMapBanDoKhoBau(boss.data[0].getMapJoin()[0])
                    && !MapService.gI().isMapKhiGas(boss.data[0].getMapJoin()[0])
                    && !MapService.gI().isMapBlackBallWar(boss.data[0].getMapJoin()[0])).count());
            for (int i = 0; i < bosses.size(); i++) {
                Boss boss = this.bosses.get(i);
                if (MapService.gI().isMapMaBu(boss.data[0].getMapJoin()[0])
                        || MapService.gI().isMapBlackBallWar(boss.data[0].getMapJoin()[0])
                        || MapService.gI().isMapDoanhTrai(boss.data[0].getMapJoin()[0])
                        || MapService.gI().isMapBanDoKhoBau(boss.data[0].getMapJoin()[0])
                        || MapService.gI().isMapKhiGas(boss.data[0].getMapJoin()[0])) {
                    continue;
                }
                msg.writer().writeInt((int) boss.id);
                msg.writer().writeInt((int) boss.id);
                msg.writer().writeShort(boss.data[0].getOutfit()[0]);
                msg.writer().writeShort(-1);
                msg.writer().writeShort(boss.data[0].getOutfit()[1]);
                msg.writer().writeShort(boss.data[0].getOutfit()[2]);
                msg.writer().writeUTF(boss.data[0].getName());
                if (boss.zone != null) {
                    msg.writer().writeUTF("Sống");
                    msg.writer().writeUTF("Thông Tin Boss\n" + "|7|Map : " + boss.zone.map.mapName + "(" + boss.zone.map.mapId + ")" + "\nHP: " + Util.powerToString(boss.nPoint.hp) + "\nDame: " + Util.powerToString(boss.nPoint.dame));
                } else {
                    msg.writer().writeUTF("Chết");
                    msg.writer().writeUTF("Boss Respawn\n|7|Time to Reset : " + (boss.secondsRest <= 0 ? "BossAppear" : boss.secondsRest + " giây"));
                }
            }
            player.sendMessage(msg);
            msg.cleanup();
        } catch (Exception e) {
            e.printStackTrace();
            System.err.println("An error occurred: " + e.getMessage());
        }
    }

    public synchronized void callBoss(Player player, int mapId) {
        try {
            if (BossManager.gI().existBossOnPlayer(player)
                    || player.zone.items.stream().anyMatch(itemMap -> ItemMapService.gI().isBlackBall(itemMap.itemTemplate.id))
                    || player.zone.getPlayers().stream().anyMatch(p -> p.iDMark.isHoldBlackBall())) {
                return;
            }
            Boss k = null;
            switch (mapId) {
                case 85:
                    k = BossManager.gI().createBoss(BossID.Rong_1Sao);
                    break;
                case 86:
                    k = BossManager.gI().createBoss(BossID.Rong_2Sao);
                    break;
                case 87:
                    k = BossManager.gI().createBoss(BossID.Rong_3Sao);
                    break;
                case 88:
                    k = BossManager.gI().createBoss(BossID.Rong_4Sao);
                    break;
                case 89:
                    k = BossManager.gI().createBoss(BossID.Rong_5Sao);
                    break;
                case 90:
                    k = BossManager.gI().createBoss(BossID.Rong_6Sao);
                    break;
                case 91:
                    k = BossManager.gI().createBoss(BossID.Rong_7Sao);
                    break;
            }
            if (k != null) {
                k.currentLevel = 0;
                k.joinMapByZone(player);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public Boss getBossById(int bossId) {
        return BossManager.gI().bosses.stream().filter(boss -> boss.id == bossId && !boss.isDie()).findFirst().orElse(null);
    }

    public static String covertString(String value) {
        String temp = Normalizer.normalize(value, Normalizer.Form.NFD);
        try {
            Pattern pattern = Pattern.compile("\\p{InCombiningDiacriticalMarks}+");
            return pattern.matcher(temp).replaceAll("");
        } catch (Exception ex) {
            return temp;
        }
    }

    public Boss getBossByName(String name) {
        try {
            for (Boss boss : this.bosses) {
                if (boss.currentLevel > 0) {
                    if (covertString(boss.data[0].getName()).equalsIgnoreCase(covertString(name))) {
                        return boss;
                    }
                }
                if (boss.name == null) {
                    continue;
                }
                if (covertString(boss.name).equalsIgnoreCase(covertString(name))) {
                    return boss;
                }
            }
        } catch (Exception e) {
        }
        return null;
    }

    @Override
    public void run() {
        while (ServerManager.isRunning) {
            try {
                long st = System.currentTimeMillis();
                for (Boss boss : this.bosses) {
                    boss.update();
                }
                Thread.sleep(150 - (System.currentTimeMillis() - st));
            } catch (Exception ignored) {
            }

        }
    }
}
