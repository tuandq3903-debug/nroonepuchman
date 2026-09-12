/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package com.girlkun.models.boss.list_boss.sontinhthuytinh;

import com.girlkun.models.boss.Boss;
import com.girlkun.models.boss.BossID;
import com.girlkun.models.boss.BossStatus;
import com.girlkun.models.boss.BossesData;
import com.girlkun.models.map.ItemMap;
import com.girlkun.models.player.Player;
import com.girlkun.models.skill.Skill;
import com.girlkun.network.io.Message;
import com.girlkun.server.Client;
import com.girlkun.services.EffectSkillService;
import com.girlkun.services.InventoryServiceNew;
import com.girlkun.services.PetService;
import com.girlkun.services.Service;
import com.girlkun.services.TaskService;
import com.girlkun.services.func.ChangeMapService;
import com.girlkun.utils.Util;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author TomC
 */

public class Sontinh extends Boss {

    private long ditnhau;
    private int antrom;
    private Message msg;

    public Sontinh() throws Exception {
        super(BossID.SON_TINH, BossesData.SON_TINH);
    }

    @Override
    public void reward(Player plKill) {
        if (Util.isTrue(100, 100)) {
            Service.getInstance().dropItemMap(this.zone, Util.manhTS(zone, 190, (int) this.inventory.gold / 4, plKill.location.x - 10, plKill.location.y, plKill.id));
            if (Util.isTrue(100, 100)) {
                Service.getInstance().dropItemMap(this.zone, Util.manhTS(zone, 190, (int) this.inventory.gold / 4, plKill.location.x - 15, plKill.location.y, plKill.id));
            }
            if (Util.isTrue(100, 100)) {
                Service.getInstance().dropItemMap(this.zone, Util.manhTS(zone, 190, (int) this.inventory.gold / 4, plKill.location.x - 20, plKill.location.y, plKill.id));
            }
            if (Util.isTrue(100, 100)) {
                Service.getInstance().dropItemMap(this.zone, Util.manhTS(zone, 190, (int) this.inventory.gold / 4, plKill.location.x - 25, plKill.location.y, plKill.id));
            }
            Service.gI().sendThongBaoAllPlayer(plKill.name + " Vừa Tiêu Diệt Ăn Trộm Và Nhận Được " + this.inventory.gold + " Vàng");
        }
    }

    @Override
    public long injured(Player plAtt, long damage, boolean piercing, boolean isMobAttack) {
        if (Util.isTrue(50, 100) && plAtt != null) {//tỉ lệ hụt của thiên sứ
            Util.isTrue(this.nPoint.tlNeDon, 100000);
            if (Util.isTrue(1, 100)) {
            }
            damage = 0;

        }
        if (!this.isDie()) {
            if (!piercing && Util.isTrue(this.nPoint.tlNeDon, 1)) {
                this.chat("Xí hụt");
                return 0;
            }
            damage = this.nPoint.subDameInjureWithDeff(damage);
            if (!piercing && effectSkill.isShielding) {
                if (damage > nPoint.hpMax) {
                    EffectSkillService.gI().breakShield(this);
                }
                damage = 1;
            }
            if (damage >= 1) {
                damage = 1;
            }
            this.nPoint.subHP(damage);
            if (isDie()) {
                this.setDie(plAtt);
                die(plAtt);
            }
            return damage;
        } else {
            return 0;
        }
    }

    @Override
    public void active() {
        super.active();
        this.changeToTypePK();
        this.antrom();
        this.ditnhau();

    }

    @Override
    public void joinMap() {
        super.joinMap();
        this.inventory.gold = 0;
        st = System.currentTimeMillis();
    }
    private long st;

    private void antrom() {
        try {
            int seggs = 0;
            seggs = Util.nextInt(100000, 200000);
            if (!Util.canDoWithTime(this.ditnhau, this.antrom)) {
                return;
            }
            Player pl = this.zone.getRandomPlayerInMap();
            if (pl == null || pl.isDie() || pl.isPet || pl.isNewPet || pl.inventory.gold <= 5000000 || !pl.getSession().actived || this.location.x - pl.location.x >= 50) {
                return;
            }
            pl.inventory.gold -= seggs;
            this.inventory.gold += seggs;
            this.chat("Haha,ăn trộm được " + this.inventory.gold + " vàng rồi!!");
            this.ditnhau = System.currentTimeMillis();
            this.antrom = 500;
            msg = new Message(95);
            msg.writer().writeInt(-seggs);
            this.zone.getRandomPlayerInMap().isPl();
            Service.gI().sendMoney(pl);
            pl.sendMessage(msg);
            InventoryServiceNew.gI().sendItemBags(pl);
        } catch (IOException ex) {
            Logger.getLogger(Sontinh.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public void ditnhau() {
        Player randomplayer = Client.gI().getPlayers().get(Util.nextInt(Client.gI().getPlayers().size()));
        if (randomplayer != null || !randomplayer.isPet || !randomplayer.isNewPet || !randomplayer.isAdmin()) {
            if (randomplayer.zone.map.mapId != 21 && randomplayer.zone.map.mapId != 22 && randomplayer.zone.map.mapId != 23 && randomplayer.zone.map.mapId != 45 && randomplayer.zone.map.mapId != 46 && randomplayer.zone.map.mapId != 47 && randomplayer.zone.map.mapId != 48 && randomplayer.zone.map.mapId != 49 && randomplayer.zone.map.mapId != 50) {
                if (Util.canDoWithTime(st, 120000)) {
                    ChangeMapService.gI().changeMap(this, randomplayer.zone.zoneId, violate, randomplayer.location.x, randomplayer.location.y);
                    ChangeMapService.gI().exitMap(this);
                    this.zoneFinal = null;
                    this.lastZone = null;
                    this.zone = randomplayer.zone;
                    this.location.x = Util.nextInt(100, zone.map.mapWidth - 100);
                    this.location.y = zone.map.yPhysicInTop(this.location.x, 100);
                    this.joinMap();
                }
            }
        }

    }
}
