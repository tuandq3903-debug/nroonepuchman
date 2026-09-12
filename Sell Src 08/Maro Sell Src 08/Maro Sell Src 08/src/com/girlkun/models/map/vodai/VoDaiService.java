package com.girlkun.models.map.vodai;

import com.girlkun.consts.ConstNpc;
import com.girlkun.models.map.Map;
import com.girlkun.models.map.Zone;
import com.girlkun.models.npc.Npc;
import com.girlkun.models.npc.NpcManager;
import com.girlkun.models.player.Player;
import com.girlkun.network.io.Message;
import com.girlkun.services.InventoryServiceNew;
import com.girlkun.services.MapService;
import com.girlkun.services.Service;
import com.girlkun.services.func.ChangeMapService;
import com.girlkun.utils.Util;
import static com.girlkun.utils.Util.setTimeout;
import java.io.IOException;

/**
 * @author Bùi Kim Trường
 */
public class VoDaiService {

    private static VoDaiService i;

    public static VoDaiService gI() {
        if (i == null) {
            i = new VoDaiService();
        }
        return i;
    }

    public void startChallenge(Player player) {
        Zone zone = getMapChallenge(112);
        if (zone != null) {
            if (InventoryServiceNew.gI().findItemBag(player, 457) != null && InventoryServiceNew.gI().findItemBag(player, 457).quantity >= player.thoiVangVoDaiSinhTu) {
                InventoryServiceNew.gI().subQuantityItemsBag(player, InventoryServiceNew.gI().findItemBag(player, 457), player.thoiVangVoDaiSinhTu);
                InventoryServiceNew.gI().sendItemBags(player);
                player.thoiVangVoDaiSinhTu += 100;
                player.lastTimePKVoDaiSinhTu = System.currentTimeMillis();
            } else {
                Service.gI().sendThongBao(player, "Bạn không có đủ thỏi vàng!");
                return;
            }
            if (!zone.equals(player.zone)) {
                ChangeMapService.gI().changeMap(player, zone, player.location.x, 408);
            }
            setTimeout(() -> {
                Npc baHatMit = NpcManager.getNpc(ConstNpc.BA_HAT_MIT);
                VoDai vdst = new VoDai();
                vdst.setPlayer(player);
                vdst.setNpc(baHatMit);
                vdst.setRound(0);
                vdst.toTheNextRound();
                vdst.setZone(zone);
                vdst.setTimeTotal(0);
                VoDaiManager.gI().add(vdst);
                baHatMit.npcChat(player, "Số thứ tự của ngươi là 1 chuẩn bị thi đấu nhé.");
                Service.gI().releaseCooldownSkill(player);
                player.isPKDHVT = true;
                player.lastTimePKDHVT23 = System.currentTimeMillis();
                vdst.endChallenge = false;
            }, 500);
        } else {
        }
    }

    public void sendTypePK(Player player, Player boss) {
        Message msg;
        try {
            msg = Service.gI().messageSubCommand((byte) 35);
            msg.writer().writeInt((int) boss.id);
            msg.writer().writeByte(3);
            player.sendMessage(msg);
            msg.cleanup();
        } catch (IOException e) {
        }
    }

    public Zone getMapChallenge(int mapId) {
        Map map = MapService.gI().getMapById(mapId);
        Zone zone = null;
        try {
            if (map != null) {
                int zoneId = 0;
                while (zoneId < map.zones.size()) {
                    Zone zonez = map.zones.get(zoneId);
                    if (VoDaiManager.gI().getVDST(zonez) == null) {
                        zone = zonez;
                        break;
                    }
                    zoneId++;
                }
            }
        } catch (Exception e) {
        }
        return zone;
    }
}
