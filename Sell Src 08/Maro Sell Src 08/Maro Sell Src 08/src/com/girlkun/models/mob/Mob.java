package com.girlkun.models.mob;

import com.girlkun.consts.ConstMap;
import com.girlkun.consts.ConstMob;
import com.girlkun.consts.ConstPlayer;
import com.girlkun.consts.ConstTask;
import com.girlkun.models.item.Item;
import com.girlkun.models.map.ItemMap;

import java.util.List;

import com.girlkun.models.map.Zone;
import com.girlkun.models.player.Location;
import com.girlkun.models.player.Pet;
import com.girlkun.models.player.Player;
import com.girlkun.models.reward.ItemMobReward;
import com.girlkun.models.reward.MobReward;
import com.girlkun.network.io.Message;
import com.girlkun.server.Maintenance;
import com.girlkun.server.Manager;
import com.girlkun.server.ServerManager;
import com.girlkun.services.*;
import com.girlkun.utils.Util;

import java.util.ArrayList;
import java.util.Calendar;

public class Mob {

    public int id;
    public Zone zone;
    public int tempId;
    public String name;
    public byte level;

    public MobPoint point;
    public MobEffectSkill effectSkill;
    public Location location;

    public byte pDame;
    public int pTiemNang;
    private long maxTiemNang;

    public long lastTimeDie;
    public int lvMob = 0;
    public int status = 5;

    public Mob(Mob mob) {
        this.point = new MobPoint(this);
        this.effectSkill = new MobEffectSkill(this);
        this.location = new Location();
        this.id = mob.id;
        this.tempId = mob.tempId;
        this.level = mob.level;
        this.point.setHpFull(mob.point.getHpFull());
        this.point.sethp(this.point.getHpFull());
        this.location.x = mob.location.x;
        this.location.y = mob.location.y;
        this.pDame = mob.pDame;
        this.pTiemNang = mob.pTiemNang;
        this.setTiemNang();
    }

    public Mob() {
        this.point = new MobPoint(this);
        this.effectSkill = new MobEffectSkill(this);
        this.location = new Location();
    }

    public static void initMobBanDoKhoBau(Mob mob, byte level) {
        mob.point.dame = level * 3250 * mob.level * 4;
        mob.point.maxHp = level * 10000 * mob.level * 2 + level * 7263 * mob.tempId;
    }

    public static void initMopbKhiGas(Mob mob, int level) {
        mob.point.maxHp = 20000000 * level;
        mob.point.dame = 10000 * level;
    }

    public static void initMobConDuongRanDoc(Mob mob, byte level) {
        mob.point.dame = level * 3250 * mob.level * 4;
        mob.point.maxHp = level * 12472 * mob.level * 2 + level * 7263 * mob.tempId;
    }

    public static void hoiSinhMob(Mob mob) {
        mob.point.hp = mob.point.maxHp;
        mob.setTiemNang();
        Message msg;
        try {
            msg = new Message(-13);
            msg.writer().writeByte(mob.id);
            msg.writer().writeByte(mob.tempId);
            msg.writer().writeByte(0); //level mob
            msg.writer().writeLong((mob.point.hp));
            Service.getInstance().sendMessAllPlayerInMap(mob.zone, msg);
            msg.cleanup();
        } catch (Exception e) {
        }
    }

    public void setTiemNang() {
        this.maxTiemNang = (long) this.point.getHpFull() * (this.pTiemNang + Util.nextInt(-2, 2)) / 100;
    }

    private long lastTimeAttackPlayer;

    public boolean isDie() {
        return this.point.gethp() <= 0;
    }

    public boolean isSieuQuai() {
        return this.lvMob > 0;
    }

    public synchronized void injured(Player plAtt, long damage, boolean dieWhenHpFull) {
        if (!this.isDie()) {
            if (damage >= this.point.hp) {
                damage = this.point.hp;
            }
            if (!dieWhenHpFull) {
                if (this.point.hp == this.point.maxHp && damage >= this.point.hp) {
                    damage = this.point.hp - 1;
                }
                if (this.tempId == 0 && damage > 10) {
                    damage = 10;
                }
            }
            this.point.hp -= damage;
            if (this.isDie()) {
                this.status = 0;
                this.sendMobDieAffterAttacked(plAtt, damage);
                TaskService.gI().checkDoneTaskKillMob(plAtt, this);
                TaskService.gI().checkDoneSideTaskKillMob(plAtt, this);
                this.lastTimeDie = System.currentTimeMillis();
                if (this.id == 13) {
                    this.zone.isbulon13Alive = false;
                }
                if (this.id == 14) {
                    this.zone.isbulon14Alive = false;
                }
                if (this.isSieuQuai()) {
                    plAtt.achievement.plusCount(12);
                }
                if (this.tempId == 0) {
                    plAtt.achievement.plusCount(7);
                }
            } else {
                this.sendMobStillAliveAffterAttacked(damage, plAtt != null ? plAtt.nPoint.isCrit : false);
            }
            if (plAtt != null) {
                Service.gI().addSMTN(plAtt, (byte) 2, getTiemNangForPlayer(plAtt, damage), true);
            }
        }
    }

    public long getTiemNangForPlayer(Player pl, long dame) {
        int levelPlayer = Service.gI().getCurrLevel(pl);
        int n = levelPlayer - this.level;
        long pDameHit = dame * 100 / point.getHpFull();
        long tiemNang = pDameHit * maxTiemNang / 100;
        if (tiemNang <= 0) {
            tiemNang = 1;
        }
        if (n >= 0) {
            for (int i = 0; i < n; i++) {
                long sub = tiemNang * 10 / 100;
                if (sub <= 0) {
                    sub = 1;
                }
                tiemNang -= sub;
            }
        } else {
            for (int i = 0; i < -n; i++) {
                long add = tiemNang * 10 / 100;
                if (add <= 0) {
                    add = 1;
                }
                tiemNang += add;
            }
        }
        if (tiemNang <= 0) {
            tiemNang = 1;
        }
        tiemNang = (int) pl.nPoint.calSucManhTiemNang(tiemNang);
        if (pl.zone.map.mapId == 122 || pl.zone.map.mapId == 123 || pl.zone.map.mapId == 124) {
            tiemNang *= 20;
        }
        return tiemNang;
    }

    public boolean FindChar(int CharID) {
        List<Player> players = this.zone.getPlayers();
        for (int i = 0; i < players.size(); i++) {
            Player pl = players.get(i);
            if (pl != null && pl.id == CharID) {
                return true;
            }
        }
        return false;
    }

    public void update() {
        if (this.tempId == 71) {
            try {
                Message msg = new Message(102);
                msg.writer().writeByte(5);
                msg.writer().writeShort(this.zone.getPlayers().get(0).location.x);
                Service.gI().sendMessAllPlayerInMap(zone, msg);
                msg.cleanup();
            } catch (Exception e) {
            }
        }
        if (this.isDie() && !Maintenance.isRunning) {
            switch (zone.map.type) {
                case ConstMap.MAP_DOANH_TRAI:
                    if (this.tempId == 22 && this.zone.map.mapId == 59 && FindChar(-2_147_479_965)) {
                        if (Util.canDoWithTime(lastTimeDie, 10000)) {
                            if (this.id == 13) {
                                this.zone.isbulon13Alive = true;
                            }
                            if (this.id == 14) {
                                this.zone.isbulon14Alive = true;
                            }
                            this.hoiSinh();
                            this.sendMobHoiSinh();
                        }

                    }
                    break;
                case ConstMap.MAP_CON_DUONG_RAN_DOC:
                    break;
                case ConstMap.MAP_BAN_DO_KHO_BAU:
                    break;
                case ConstMap.MAP_KHI_GAS:
                    break;
                default:
                    if (Util.canDoWithTime(lastTimeDie, 5000)) {
                        this.hoiSinh();
                        this.sendMobHoiSinh();
                    }
            }
        }
        effectSkill.update();
        attackPlayer();
    }

    private void attackPlayer() {
        if (!isDie() && !effectSkill.isHaveEffectSkill() && !(tempId == 0) && Util.canDoWithTime(lastTimeAttackPlayer, 2000)) {
            Player pl = getPlayerCanAttack();
            if (pl != null) {
//                MobService.gI().mobAttackPlayer(this, pl);
                this.mobAttackPlayer(pl);
            }
            this.lastTimeAttackPlayer = System.currentTimeMillis();
        }
    }

    private Player getPlayerCanAttack() {
        int distance = 100;
        Player plAttack = null;
        try {
            List<Player> players = this.zone.getNotBosses();
            for (Player pl : players) {
                if (!pl.isDie() && !pl.isBoss && !pl.effectSkin.isVoHinh) {
                    int dis = Util.getDistance(pl, this);
                    if (dis <= distance) {
                        plAttack = pl;
                        distance = dis;
                    }
                }
            }
        } catch (Exception e) {

        }
        return plAttack;
    }

    //**************************************************************************
    private void mobAttackPlayer(Player player) {
        long dameMob = this.point.getDameAttack();
        if (player.charms.tdDaTrau > System.currentTimeMillis()) {
            dameMob /= 2;
        }
        long dame = player.injured(null, dameMob, false, true);
        this.sendMobAttackMe(player, dame);
        this.sendMobAttackPlayer(player);
    }

    private void sendMobAttackMe(Player player, long dame) {
        if (!player.isPet && !player.isNewPet) {
            Message msg;
            try {
                msg = new Message(-11);
                msg.writer().writeByte(this.id);
                if (player.getSession() != null && player.getSession().version == 999) {
                    msg.writer().writeLong(dame); //dame
                } else {
                    msg.writer().writeInt(Util.fromLongtoInt(dame)); //dame
                }
                player.sendMessage(msg);
                msg.cleanup();
            } catch (Exception e) {
            }
        }
    }

    private void sendMobAttackPlayer(Player player) {
        Message msg;
        try {
            for (Player plMap : this.zone.getPlayers()) {
                msg = new Message(-10);
                msg.writer().writeByte(this.id);
                msg.writer().writeInt((int) player.id);
                if (player.getSession() != null && player.getSession().version == 999) {
                    msg.writer().writeLong(player.nPoint.hp);
                } else {
                    msg.writer().writeInt(Util.fromLongtoInt(player.nPoint.hp));
                }
                Service.gI().sendMessAnotherNotMeInMap(player, msg);
                msg.cleanup();
            }
        } catch (Exception e) {
        }
    }

    public void hoiSinh() {
        this.status = 5;
        this.point.hp = this.point.maxHp;
        this.setTiemNang();
    }

    public void sendMobHoiSinh() {
        Message msg;
        try {
            for (Player plMap : this.zone.getPlayers()) {
                msg = new Message(-13);
                msg.writer().writeByte(this.id);
                msg.writer().writeByte(this.tempId);
                msg.writer().writeByte(lvMob);
                if (plMap.getSession() != null && plMap.getSession().version == 999) {
                    msg.writer().writeLong(point.hp);
                } else {
                    msg.writer().writeInt(Util.fromLongtoInt(point.hp));
                }
                Service.gI().sendMessAllPlayerInMap(this.zone, msg);
                msg.cleanup();
            }
        } catch (Exception e) {
        }
    }

    private boolean isMobBay() {
        return this.tempId == ConstMob.PHI_LONG || this.tempId == ConstMob.PHI_LONG_ME
                || this.tempId == ConstMob.QUY_BAY || this.tempId == ConstMob.QUY_BAY_ME
                || this.tempId == ConstMob.QUY_DAU_TO || this.tempId == ConstMob.THAN_LAN_BAY
                || this.tempId == ConstMob.THAN_LAN_ME || this.tempId == ConstMob.KHONG_TAC
                || this.tempId == ConstMob.TAMBOURINE || this.tempId == ConstMob.ALIEN
                || this.tempId == ConstMob.QUY_DIA_NGUC || this.tempId == ConstMob.THAN_LAN_XANH
                || this.tempId == ConstMob.DOI_DA_XANH || this.tempId == ConstMob.QUY_CHIM
                || this.tempId == ConstMob.TABURINE_DO || this.tempId == ConstMob.DA_XANH
                || this.tempId == ConstMob.ARBEE;
    }

    private boolean isXenCon() {
        return this.tempId >= ConstMob.XEN_CON_CAP_1 && this.tempId <= ConstMob.XEN_CON_CAP_8;
    }

    //**************************************************************************
    private void sendMobDieAffterAttacked(Player plKill, long dameHit) {
        Message msg;
        try {
            for (Player plMap : this.zone.getPlayers()) {
                msg = new Message(-12);
                msg.writer().writeByte(this.id);
                if (plMap.getSession() != null && plMap.getSession().version == 999) {
                    msg.writer().writeLong(dameHit);
                } else {
                    msg.writer().writeInt(Util.fromLongtoInt(dameHit));
                }
                msg.writer().writeBoolean(plKill.nPoint.isCrit); // crit
                List<ItemMap> items = mobReward(plKill, this.dropItemTask(plKill), msg);
                Service.gI().sendMessAllPlayerInMap(this.zone, msg);
                msg.cleanup();
                hutItem(plKill, items);
            }
        } catch (Exception e) {
        }
    }

    private void hutItem(Player player, List<ItemMap> items) {
        if (!player.isPet && !player.isNewPet) {
            if (player.charms.tdThuHut > System.currentTimeMillis()) {
                for (ItemMap item : items) {
                    if (item.itemTemplate.id != 590) {
                        ItemMapService.gI().pickItem(player, item.itemMapId, true);
                    }
                }
            }
        } else {
            if (((Pet) player).master.charms.tdThuHut > System.currentTimeMillis()) {
                for (ItemMap item : items) {
                    if (item.itemTemplate.id != 590) {
                        ItemMapService.gI().pickItem(((Pet) player).master, item.itemMapId, true);
                    }
                }
            }
        }
    }

    private List<ItemMap> mobReward(Player player, ItemMap itemTask, Message msg) {
//        nplayer
        List<ItemMap> itemReward = new ArrayList<>();
        try {
            if ((!player.isPet && player.getSession().actived && player.setClothes.setDHD == 5)) {
                byte random = 0;
                if (Util.isTrue(5, 100)) {
                    random = 1;

                    Item i = Manager.RUBY_REWARDS.get(Util.nextInt(0, Manager.RUBY_REWARDS.size() - 1));
                    i.quantity = random;
                    InventoryServiceNew.gI().addItemBag(player, i);
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendThongBao(player, "Mày Nhận Được" + i.template.name);
                }
            }

            itemReward = this.getItemMobReward(player, this.location.x + Util.nextInt(-10, 10),
                    this.zone.map.yPhysicInTop(this.location.x, this.location.y));
            if (itemTask != null) {
                itemReward.add(itemTask);
            }
            msg.writer().writeByte(itemReward.size()); //sl item roi
            for (ItemMap itemMap : itemReward) {
                msg.writer().writeShort(itemMap.itemMapId);// itemmapid
                msg.writer().writeShort(itemMap.itemTemplate.id); // id item
                msg.writer().writeShort(itemMap.x); // xend item
                msg.writer().writeShort(itemMap.y); // yend item
                msg.writer().writeInt((int) itemMap.playerId); // id nhan nat
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return itemReward;
    }

    public List<ItemMap> getItemMobReward(Player player, int x, int yEnd) {
        List<ItemMap> list = new ArrayList<>();
        MobReward mobReward = Manager.MOB_REWARDS.get(this.tempId);
        if (mobReward == null) {
            return list;

        }
        final Calendar rightNow = Calendar.getInstance();
        int hour = rightNow.get(11);
        List<ItemMobReward> items = mobReward.getItemReward();
        List<ItemMobReward> golds = mobReward.getGoldReward();
        if (!items.isEmpty()) {
            ItemMobReward item = items.get(Util.nextInt(0, items.size() - 1));
            ItemMap itemMap = item.getItemMap(zone, player, x, yEnd);
            if (itemMap != null) {
                list.add(itemMap);
            }
        }
        if (!golds.isEmpty()) {
            ItemMobReward gold = golds.get(Util.nextInt(0, golds.size() - 1));
            ItemMap itemMap = gold.getItemMap(zone, player, x, yEnd);
            if (itemMap != null) {
                list.add(itemMap);
            }
        }
        if (player.itemTime.isUseMayDo && Util.isTrue(21, 100) && this.tempId > 57 && this.tempId < 66) {
            list.add(new ItemMap(zone, 380, 1, x, player.location.y, player.id));
            if (Util.isTrue(1, 100) && this.tempId > 57 && this.tempId < 66) {    //up bí kíp
                list.add(new ItemMap(zone, Util.nextInt(1099, 1102), 1, x, player.location.y, player.id));
            }
        }// vat phẩm rơi khi user maaáy dò adu hoa r o day ti code choa
        if (player.itemTime.isUseMayDo2 && Util.isTrue(1, 100) && this.tempId > 1 && this.tempId < 81) {
            list.add(new ItemMap(zone, 2036, 1, x, player.location.y, player.id));// cai nay sua sau nha
        }
        if (player.cFlag >= 1 && Util.isTrue(100, 100) && this.tempId == 0 && hour != 1 && hour != 3 && hour != 5 && hour != 7 && hour != 9 && hour != 11 && hour != 13 && hour != 15 && hour != 17 && hour != 19 && hour != 21 && hour != 23) {    //up bí kíp
            list.add(new ItemMap(zone, 590, 1, x, player.location.y, player.id));// cai nay sua sau nha
            if (Util.isTrue(50, 100) && this.tempId == 0) {    //up bí kíp
                list.add(new ItemMap(zone, 590, 1, x, player.location.y, player.id));
                if (Util.isTrue(50, 100) && this.tempId == 0) {    //up bí kíp
                    list.add(new ItemMap(zone, 590, 1, x, player.location.y, player.id));
                    if (Util.isTrue(50, 100) && this.tempId == 0) {    //up bí kíp
                        list.add(new ItemMap(zone, 590, 1, x, player.location.y, player.id));
                    }
                }
            }
        }
        if (this.tempId > 0 && this.zone.map.mapId >= 156 && this.zone.map.mapId <= 159 && player.fusion.typeFusion == ConstPlayer.HOP_THE_PORATA2) {
            if (Util.isTrue(10, 100)) {    //up bí kíp
                list.add(new ItemMap(zone, 2076, 1, x, player.location.y, player.id));
            }
        }
        if (this.tempId > 0 && this.zone.map.mapId >= 156 && this.zone.map.mapId <= 159) {
            if (Util.isTrue(10, 100)) {    //up bí kíp
                list.add(new ItemMap(zone, 933, 1, x, player.location.y, player.id));
            }
        }
        if (this.tempId > 0 && this.zone.map.mapId >= 156 && this.zone.map.mapId <= 159) {
            if (Util.isTrue(10, 100)) {    //up bí kíp
                list.add(new ItemMap(zone, 934, 1, x, player.location.y, player.id));
            }
        }
        if (this.tempId > 0 && this.zone.map.mapId >= 156 && this.zone.map.mapId <= 159 && player.fusion.typeFusion == ConstPlayer.HOP_THE_PORATA3) {
            if (Util.isTrue(10, 100)) {    //up bí kíp
                list.add(new ItemMap(zone, 2077, 1, x, player.location.y, player.id));
            }
        }
        if (this.tempId > 0 && this.zone.map.mapId >= 156 && this.zone.map.mapId <= 159 && player.fusion.typeFusion == ConstPlayer.HOP_THE_PORATA3) {
            if (Util.isTrue(10, 100)) {    //up bí kíp
                list.add(new ItemMap(zone, 2036, 1, x, player.location.y, player.id));
            }
        }
        if (this.tempId > 0 && this.zone.map.mapId >= 156 && this.zone.map.mapId <= 159 && player.fusion.typeFusion == ConstPlayer.HOP_THE_PORATA4) {
            if (Util.isTrue(10, 100)) {    //up bí kíp
                list.add(new ItemMap(zone, 2036, 1, x, player.location.y, player.id));
            }
        }
        if (player.setClothes.setGod() && this.zone.map.mapId >= 105 && this.zone.map.mapId <= 111) {
            if (Util.isTrue(10, 100)) {    //up bí kíp
                list.add(new ItemMap(zone, Util.nextInt(663, 667), 1, x, player.location.y, player.id));
            }
        }
        if (player.setClothes.setGod14() && this.zone.map.mapId == 155) {
            if (Util.isTrue(5, 100)) {    //up bí kíp
                list.add(new ItemMap(zone, Util.nextInt(1066, 1070), 1, x, player.location.y, player.id));
            }
        }
        Item item = player.inventory.itemsBody.get(1);
        if (this.zone.map.mapId > 0) {
            if (item.isNotNullItem()) {
                if (item.template.id == 691) {
                    if (Util.isTrue(10, 100)) {    //up bí kíp
                        list.add(new ItemMap(zone, Util.nextInt(695, 698), 1, x, player.location.y, player.id));
                    }
                } else if (item.template.id != 691 && item.template.id != 692 && item.template.id != 693) {
                    if (Util.isTrue(0, 1)) {
                        list.add(new ItemMap(zone, 76, 1, x, player.location.y, player.id));
                    }
                }
            }
        }
        if (this.zone.map.mapId >= 0) {
            if (item.isNotNullItem()) {
                if (item.template.id == 692) {
                    if (Util.isTrue(10, 100)) {    //up bí kíp
                        list.add(new ItemMap(zone, Util.nextInt(695, 698), 1, x, player.location.y, player.id));
                    }
                } else if (item.template.id != 691 && item.template.id != 692 && item.template.id != 693) {
                    if (Util.isTrue(0, 1)) {
                        list.add(new ItemMap(zone, 76, 1, x, player.location.y, player.id));
                    }
                }
            }
        }
        if (this.zone.map.mapId > 0) {
            if (item.isNotNullItem()) {
                if (item.template.id == 693) {
                    if (Util.isTrue(10, 100)) {    //up bí kíp
                        list.add(new ItemMap(zone, Util.nextInt(695, 698), 1, x, player.location.y, player.id));
                    }
                } else if (item.template.id != 691 && item.template.id != 692 && item.template.id != 693) {
                    if (Util.isTrue(0, 1)) {
                        list.add(new ItemMap(zone, 76, 1, x, player.location.y, player.id));
                    }
                }
            }
        }
        //Roi Do Than Cold
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Quanthanlinh = ItemService.gI().createNewItem((short) (556));
                Quanthanlinh.itemOptions.add(new Item.ItemOption(22, Util.nextInt(55, 65)));
                Quanthanlinh.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Quanthanlinh);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Quanthanlinh.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Quanthanlinhxd = ItemService.gI().createNewItem((short) (560));
                Quanthanlinhxd.itemOptions.add(new Item.ItemOption(22, Util.nextInt(45, 55)));
                Quanthanlinhxd.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Quanthanlinhxd);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Quanthanlinhxd.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Quanthanlinhnm = ItemService.gI().createNewItem((short) (558));
                Quanthanlinhnm.itemOptions.add(new Item.ItemOption(22, Util.nextInt(50, 60)));
                Quanthanlinhnm.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Quanthanlinhnm);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Quanthanlinhnm.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Aothanlinh = ItemService.gI().createNewItem((short) (555));
                Aothanlinh.itemOptions.add(new Item.ItemOption(47, Util.nextInt(500, 600)));
                Aothanlinh.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Aothanlinh);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Aothanlinh.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Aothanlinhxd = ItemService.gI().createNewItem((short) (559));
                Aothanlinhxd.itemOptions.add(new Item.ItemOption(47, Util.nextInt(600, 700)));
                Aothanlinhxd.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Aothanlinhxd);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Aothanlinhxd.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Aothanlinhnm = ItemService.gI().createNewItem((short) (562));
                Aothanlinhnm.itemOptions.add(new Item.ItemOption(47, Util.nextInt(400, 550)));
                Aothanlinhnm.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Aothanlinhnm);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Aothanlinhnm.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Gangthanlinh = ItemService.gI().createNewItem((short) (568));
                Gangthanlinh.itemOptions.add(new Item.ItemOption(0, Util.nextInt(6000, 7000)));
                Gangthanlinh.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Gangthanlinh);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Gangthanlinh.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Gangthanlinhxd = ItemService.gI().createNewItem((short) (566));
                Gangthanlinhxd.itemOptions.add(new Item.ItemOption(0, Util.nextInt(6500, 7500)));
                Gangthanlinhxd.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Gangthanlinhxd);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Gangthanlinhxd.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Gangthanlinhnm = ItemService.gI().createNewItem((short) (564));
                Gangthanlinhnm.itemOptions.add(new Item.ItemOption(0, Util.nextInt(5500, 6500)));
                Gangthanlinhnm.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Gangthanlinhnm);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Gangthanlinhnm.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Giaythanlinh = ItemService.gI().createNewItem((short) (563));
                Giaythanlinh.itemOptions.add(new Item.ItemOption(23, Util.nextInt(50, 60)));
                Giaythanlinh.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Giaythanlinh);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Giaythanlinh.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Giaythanlinhxd = ItemService.gI().createNewItem((short) (567));
                Giaythanlinhxd.itemOptions.add(new Item.ItemOption(23, Util.nextInt(55, 65)));
                Giaythanlinhxd.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Giaythanlinhxd);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Giaythanlinhxd.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Giaythanlinhnm = ItemService.gI().createNewItem((short) (565));
                Giaythanlinhnm.itemOptions.add(new Item.ItemOption(23, Util.nextInt(65, 75)));
                Giaythanlinhnm.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Giaythanlinhnm);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Giaythanlinhnm.template.name);
            }
        }
        if (this.zone.map.mapId >= 105 && this.zone.map.mapId <= 110) {
            if (Util.isTrue(1, 200)) {
                Item Giaythanlinhnm = ItemService.gI().createNewItem((short) (561));
                Giaythanlinhnm.itemOptions.add(new Item.ItemOption(14, Util.nextInt(13, 16)));
                Giaythanlinhnm.itemOptions.add(new Item.ItemOption(21, Util.nextInt(15, 17)));
                InventoryServiceNew.gI().addItemBag(player, Giaythanlinhnm);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + Giaythanlinhnm.template.name);
            }
        }
        if (this.zone.map.mapId >= 122 && this.zone.map.mapId <= 124) {
            if (player.clan != null) {
                if (Util.isTrue(5, 1000)) {
                    Item daChanMenh = ItemService.gI().createNewItem((short) (1241));
                    InventoryServiceNew.gI().addItemBag(player, daChanMenh);
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendThongBao(player, "Bạn vừa nhận được " + daChanMenh.template.name);
                }
            } else {
                Service.gI().sendThongBao(player, "Bạn Cần Phải Có Clan Mới Có Thể Up Đá Chân Mệnh");
            }
        }
        if (this.zone.map.mapId >= 122 && this.zone.map.mapId <= 124) {
            if (player.clan != null) {
                if (Util.isTrue(5, 100)) {
                    int random = Util.nextInt(1, 3);
                    player.NguHanhSonPoint += random;
                    Service.gI().sendThongBao(player, "Bạn vừa nhận được " + random + " Điểm Ngũ Hành Sơn");
                }
            } else {
                Service.gI().sendThongBao(player, "Bạn Cần Phải Có Clan Mới Có Thể Up Đá Chân Mệnh");
            }
        }
        if (player.isPl()) {
            int mapId = player.zone.map.mapId;
            if (!player.isBoss && (mapId == 1 || mapId == 2 || mapId == 3 || mapId == 15 || mapId == 16 || mapId == 17 || mapId == 8 || mapId == 9 || mapId == 11)) {
                if (Util.isTrue(1, 1000)) {
                    int[][] itemKH = {{0, 6, 21, 27, 12}, {1, 7, 22, 28, 12}, {2, 8, 23, 29, 12}}; //td, 
                    int skhId = ItemService.gI().randomSKHLV(player.gender);
                    ItemMap it = ItemService.gI().itemMapSKHLV(zone, itemKH[player.gender][Util.nextInt(0, 4)], 1, this.location.x, this.location.y, player.id, skhId);
                    list.add(it);
                }
            }
        }
        if (Util.isTrue(1, 100)) {
            if (this.isMobBay()) {
                list.add(new ItemMap(this.zone, 886, 1, x, player.location.y, player.id));
            } else {
                list.add(new ItemMap(this.zone, 887, 1, x, player.location.y, player.id));
            }
        }
        if (Util.isTrue(10, 100)) {
            if (this.isXenCon()) {
                list.add(new ItemMap(this.zone, Util.nextInt(888, 889), 1, x, player.location.y, player.id));
            }
        }
        if (this.zone.map.mapId >= 156 && this.zone.map.mapId <= 159) {
            if (Util.isTrue(5, 500)) {
                Item capsulebang = ItemService.gI().createNewItem((short) (1261));
                InventoryServiceNew.gI().addItemBag(player, capsulebang);
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendThongBao(player, "Bạn vừa nhận được " + capsulebang.template.name);
            }
        }
        return list;
    }

    private ItemMap dropItemTask(Player player) {
        ItemMap itemMap = null;
        switch (this.tempId) {
            case ConstMob.KHUNG_LONG:
            case ConstMob.LON_LOI:
            case ConstMob.QUY_DAT:
                if (TaskService.gI().getIdTask(player) == ConstTask.TASK_2_0) {
                    itemMap = new ItemMap(this.zone, 73, 1, this.location.x, this.location.y, player.id);
                }
                break;
        }
        if (itemMap != null) {
            return itemMap;
        }
        return null;
    }

    private void sendMobStillAliveAffterAttacked(long dameHit, boolean crit) {
        Message msg;
        try {
            for (Player plMap : this.zone.getPlayers()) {
                msg = new Message(-9);
                msg.writer().writeByte(this.id);
                if (plMap.getSession() != null && plMap.getSession().version == 999) {
                    msg.writer().writeLong(this.point.gethp());
                    msg.writer().writeLong(dameHit);
                } else {
                    msg.writer().writeInt(Util.fromLongtoInt(this.point.gethp()));
                    msg.writer().writeInt(Util.fromLongtoInt(dameHit));
                }
                msg.writer().writeBoolean(crit); // chí mạng
                msg.writer().writeInt(-1);
                Service.gI().sendMessAllPlayerInMap(this.zone, msg);
                msg.cleanup();
            }
        } catch (Exception e) {
        }
    }
}
