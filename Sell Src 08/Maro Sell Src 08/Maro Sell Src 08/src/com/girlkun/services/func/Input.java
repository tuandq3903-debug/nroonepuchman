package com.girlkun.services.func;

import com.girlkun.database.GirlkunDB;
import com.girlkun.consts.ConstNpc;
import com.girlkun.jdbc.daos.PlayerDAO;
import com.girlkun.models.item.Item;
import com.girlkun.models.map.Zone;
import com.girlkun.models.npc.Npc;
import com.girlkun.models.npc.NpcManager;
import com.girlkun.models.player.Inventory;
import com.girlkun.models.player.Player;
import com.girlkun.network.io.Message;
import com.girlkun.network.session.ISession;
import com.girlkun.result.GirlkunResultSet;
import com.girlkun.server.Client;
import com.girlkun.services.Service;
import com.girlkun.services.InventoryServiceNew;
import com.girlkun.services.ItemService;
import com.girlkun.services.NapThe;
//import com.girlkun.services.NapThe;
import com.girlkun.services.NpcService;
import com.girlkun.utils.Util;

import java.util.HashMap;
import java.util.Map;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.JSONValue;

public class Input {

    public static String LOAI_THE;
    public static String MENH_GIA;
    private static final Map<Integer, Object> PLAYER_ID_OBJECT = new HashMap<Integer, Object>();

    public static final int CHANGE_PASSWORD = 500;
    public static final int GIFT_CODE = 501;
    public static final int FIND_PLAYER = 502;
    public static final int CHANGE_NAME = 503;
    public static final int CHOOSE_LEVEL_GAS = 522;
    public static final int CHOOSE_LEVEL_CDRD = 560;
    public static final int CHOOSE_LEVEL_BDKB = 504;
    public static final int NAP_THE = 505;
    public static final int CHANGE_NAME_BY_ITEM = 506;
    public static final int GIVE_IT = 507;
    public static final int SEND_ITEM_OP = 512;
    public static final int SEND_ITEM_SKH = 556;
    public static final int SEND_ITEM_OP_VIP = 515;
    public static final int QUY_DOI_COIN = 508;
    public static final int QUY_DOI_HONG_NGOC = 509;
    public static final int CHATALL = 521;
    public static final int TAI = 510;
    public static final int XIU = 511;
    public static final byte NUMERIC = 0;
    public static final byte ANY = 1;
    public static final byte PASSWORD = 2;
    public static final int XIU_taixiu = 5164;
    public static final int TAI_taixiu = 5165;
    public static final int DONATE_CS = 523;

    private static Input intance;

    private Input() {

    }

    public static Input gI() {
        if (intance == null) {
            intance = new Input();
        }
        return intance;
    }

    public void doInput(Player player, Message msg) {
        try {
            String[] text = new String[msg.reader().readByte()];
            for (int i = 0; i < text.length; i++) {
                text[i] = msg.reader().readUTF();
            }
            switch (player.iDMark.getTypeInput()) {
                case DONATE_CS:
                    int csbang = Integer.parseInt(text[0]);
                    Item cscanhan = InventoryServiceNew.gI().findItemBag(player, 1261);
                    if (cscanhan == null && player.clanMember.memberPoint < 1) {
                        Service.gI().sendThongBao(player, "Số điểm capsule bản thân không đủ để thực hiện");
                        break;
                    }
                    InventoryServiceNew.gI().subQuantityItemsBag(player, cscanhan, csbang);
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    player.clanMember.memberPoint -= csbang;
                    player.clan.capsuleClan += csbang;
                    player.clanMember.clanPoint += csbang;
                    Service.gI().sendThongBao(player, "bạn đã quyên góp " + csbang + " điểm bang");
                    break;
                case TAI_taixiu:
                    int sotvxiu1 = Integer.valueOf(text[0]);
                    Item thoivang = InventoryServiceNew.gI().findItemBag(player, 457);
                    try {
                        if (sotvxiu1 >= 10 && sotvxiu1 <= 1000) {
                            if (thoivang != null && thoivang.quantity >= sotvxiu1) {
                                InventoryServiceNew.gI().subQuantityItemsBag(player, thoivang, sotvxiu1);
                                player.goldTai += sotvxiu1;
                                TaiXiu.gI().goldTai += sotvxiu1;
                                Service.gI().sendThongBao(player, "Bạn đã đặt " + Util.format(sotvxiu1) + " Thỏi Vàng vào TÀI");
                                TaiXiu.gI().addPlayerTai(player);
                                InventoryServiceNew.gI().sendItemBags(player);
                                Service.getInstance().sendMoney(player);
                                PlayerDAO.updatePlayer(player);
                            } else {
                                Service.gI().sendThongBao(player, "Bạn không đủ Thỏi Vàng để chơi.");
                            }
                        } else {
                            Service.gI().sendThongBao(player, "Cược ít nhất 10 - nhiều nhất 1.000 Thỏi Vàng");
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                        Service.gI().sendThongBao(player, "Lỗi.");
                    }
                    break;
                case XIU_taixiu:
                    int sotvxiu2 = Integer.valueOf(text[0]);
                    Item thoivang1 = InventoryServiceNew.gI().findItemBag(player, 457);
                    try {
                        if (sotvxiu2 >= 10 && sotvxiu2 <= 1000) {
                            if (thoivang1 != null && thoivang1.quantity >= sotvxiu2) {
                                InventoryServiceNew.gI().subQuantityItemsBag(player, thoivang1, sotvxiu2);
                                player.goldXiu += sotvxiu2;
                                TaiXiu.gI().goldXiu += sotvxiu2;
                                Service.gI().sendThongBao(player, "Bạn đã đặt " + Util.format(sotvxiu2) + " Thỏi Vàng vào XỈU");
                                TaiXiu.gI().addPlayerXiu(player);
                                InventoryServiceNew.gI().sendItemBags(player);
                                Service.getInstance().sendMoney(player);
                                PlayerDAO.updatePlayer(player);
                            } else {
                                Service.gI().sendThongBao(player, "Bạn không đủ Thỏi Vàng để chơi.");
                            }
                        } else {
                            Service.gI().sendThongBao(player, "Cược ít nhất 10 - nhiều nhất 1.000 Thỏi Vàng");
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                        Service.gI().sendThongBao(player, "Lỗi.");
                        System.out.println("nnnnn2  ");
                    }
                    break;
                case CHATALL:
                    String chat = text[0];
                    Service.gI().ChatAll(-1, "|7|[ - •⊹٭NGỌC RỒNG ONE PUCH MAN THÔNG BÁO٭⊹• - ]" + "\n"
                            + "|1|" + (player.isAdmin() ? "ADMIN : " : "") + chat + "\n");
                    break;
                case GIVE_IT:
                    if (player.isAdmin()) {
                        int idItemBuff = Integer.parseInt(text[1]);
                        int quantityItemBuff = Integer.parseInt(text[2]);
                        Player pBuffItem = Client.gI().getPlayer(text[0]);
                        if (pBuffItem != null) {
                            String txtBuff = "Buff to player: " + pBuffItem.name + "\b";
                            if (idItemBuff == -1) {
                                pBuffItem.inventory.gold = Math.min(pBuffItem.inventory.gold + (long) quantityItemBuff, (Inventory.LIMIT_GOLD + pBuffItem.limitgold));
                                txtBuff += quantityItemBuff + " vàng\b";
                                Service.getInstance().sendMoney(player);
                            } else if (idItemBuff == -2) {
                                pBuffItem.inventory.gem = Math.min(pBuffItem.inventory.gem + quantityItemBuff, 2000000000);
                                txtBuff += quantityItemBuff + " ngọc\b";
                                Service.getInstance().sendMoney(player);
                            } else if (idItemBuff == -3) {
                                pBuffItem.inventory.ruby = Math.min(pBuffItem.inventory.ruby + quantityItemBuff, 2000000000);
                                txtBuff += quantityItemBuff + " ngọc khóa\b";
                                Service.getInstance().sendMoney(player);
                            } else {
                                Item itemBuffTemplate = ItemService.gI().createNewItem((short) idItemBuff);
                                itemBuffTemplate.quantity = quantityItemBuff;
                                InventoryServiceNew.gI().addItemBag(pBuffItem, itemBuffTemplate);
                                InventoryServiceNew.gI().sendItemBags(pBuffItem);
                                txtBuff += "x" + quantityItemBuff + " " + itemBuffTemplate.template.name + "\b";
                            }
                            NpcService.gI().createTutorial(player, 24, txtBuff);
                            if (player.id != pBuffItem.id) {
                                NpcService.gI().createTutorial(pBuffItem, 24, txtBuff);
                            }
                        } else {
                            Service.getInstance().sendThongBao(player, "Player không online");
                        }
                        break;
                    }
                    break;
                case SEND_ITEM_OP:
                    if (player.isAdmin()) {
                        int idItemBuff = Integer.parseInt(text[1]);
                        int idOptionBuff = Integer.parseInt(text[2]);
                        int slOptionBuff = Integer.parseInt(text[3]);
                        int slItemBuff = Integer.parseInt(text[4]);
                        Player pBuffItem = Client.gI().getPlayer(text[0]);
                        if (pBuffItem != null) {
                            String txtBuff = "Buff to player: " + pBuffItem.name + "\b";
                            if (idItemBuff == -1) {
                                pBuffItem.inventory.gold = Math.min(pBuffItem.inventory.gold + (long) slItemBuff, Inventory.LIMIT_GOLD);
                                txtBuff += slItemBuff + " vàng\b";
                                Service.getInstance().sendMoney(player);
                            } else if (idItemBuff == -2) {
                                pBuffItem.inventory.gem = Math.min(pBuffItem.inventory.gem + slItemBuff, 2000000000);
                                txtBuff += slItemBuff + " ngọc\b";
                                Service.getInstance().sendMoney(player);
                            } else if (idItemBuff == -3) {
                                pBuffItem.inventory.ruby = Math.min(pBuffItem.inventory.ruby + slItemBuff, 2000000000);
                                txtBuff += slItemBuff + " ngọc khóa\b";
                                Service.getInstance().sendMoney(player);
                            } else {
                                Item itemBuffTemplate = ItemService.gI().createNewItem((short) idItemBuff);
                                itemBuffTemplate.itemOptions.add(new Item.ItemOption(idOptionBuff, slOptionBuff));
                                itemBuffTemplate.quantity = slItemBuff;
                                txtBuff += "x" + slItemBuff + " " + itemBuffTemplate.template.name + "\b";
                                InventoryServiceNew.gI().addItemBag(pBuffItem, itemBuffTemplate);
                                InventoryServiceNew.gI().sendItemBags(pBuffItem);
                            }
                            NpcService.gI().createTutorial(player, 24, txtBuff);
                            if (player.id != pBuffItem.id) {
                                NpcService.gI().createTutorial(player, 24, txtBuff);
                            }
                        } else {
                            Service.getInstance().sendThongBao(player, "Player không online");
                        }
                        break;
                    }
                    break;
                case SEND_ITEM_SKH:
                    if (player.isAdmin()) {
                        int idItemBuff = Integer.parseInt(text[1]);
                        int idOptionSKH = Integer.parseInt(text[2]);
                        int idOptionBuff = Integer.parseInt(text[3]);
                        int slOptionBuff = Integer.parseInt(text[4]);
                        int slItemBuff = Integer.parseInt(text[5]);
                        Player pBuffItem = Client.gI().getPlayer(text[0]);
                        if (pBuffItem != null) {
                            String txtBuff = "Buff to player: " + pBuffItem.name + "\b";
                            if (idItemBuff == -1) {
                                pBuffItem.inventory.gold = Math.min(pBuffItem.inventory.gold + (long) slItemBuff, Inventory.LIMIT_GOLD);
                                txtBuff += slItemBuff + " vàng\b";
                                Service.getInstance().sendMoney(player);
                            } else if (idItemBuff == -2) {
                                pBuffItem.inventory.gem = Math.min(pBuffItem.inventory.gem + slItemBuff, 2000000000);
                                txtBuff += slItemBuff + " ngọc\b";
                                Service.getInstance().sendMoney(player);
                            } else if (idItemBuff == -3) {
                                pBuffItem.inventory.ruby = Math.min(pBuffItem.inventory.ruby + slItemBuff, 2000000000);
                                txtBuff += slItemBuff + " ngọc khóa\b";
                                Service.getInstance().sendMoney(player);
                            } else {
                                Item itemBuffTemplate = ItemService.gI().createNewItem((short) idItemBuff);
                                itemBuffTemplate.itemOptions.add(new Item.ItemOption(idOptionSKH, 0));
                                if (idOptionSKH == 127) {
                                    itemBuffTemplate.itemOptions.add(new Item.ItemOption(139, 0));
                                } else if (idOptionSKH == 128) {
                                    itemBuffTemplate.itemOptions.add(new Item.ItemOption(140, 0));
                                } else if (idOptionSKH == 129) {
                                    itemBuffTemplate.itemOptions.add(new Item.ItemOption(141, 0));
                                } else if (idOptionSKH == 130) {
                                    itemBuffTemplate.itemOptions.add(new Item.ItemOption(142, 0));
                                } else if (idOptionSKH == 131) {
                                    itemBuffTemplate.itemOptions.add(new Item.ItemOption(143, 0));
                                } else if (idOptionSKH == 132) {
                                    itemBuffTemplate.itemOptions.add(new Item.ItemOption(144, 0));
                                } else if (idOptionSKH == 133) {
                                    itemBuffTemplate.itemOptions.add(new Item.ItemOption(136, 0));
                                } else if (idOptionSKH == 134) {
                                    itemBuffTemplate.itemOptions.add(new Item.ItemOption(137, 0));
                                } else if (idOptionSKH == 135) {
                                    itemBuffTemplate.itemOptions.add(new Item.ItemOption(138, 0));
                                }
                                itemBuffTemplate.itemOptions.add(new Item.ItemOption(30, 0));
                                itemBuffTemplate.itemOptions.add(new Item.ItemOption(idOptionBuff, slOptionBuff));
                                itemBuffTemplate.quantity = slItemBuff;
                                txtBuff += "x" + slItemBuff + " " + itemBuffTemplate.template.name + "\b";
                                InventoryServiceNew.gI().addItemBag(pBuffItem, itemBuffTemplate);
                                InventoryServiceNew.gI().sendItemBags(pBuffItem);
                            }
                            NpcService.gI().createTutorial(player, 24, txtBuff);
                            if (player.id != pBuffItem.id) {
                                NpcService.gI().createTutorial(player, 24, txtBuff);
                            }
                        } else {
                            Service.getInstance().sendThongBao(player, "Player không online");
                        }
                        break;

                    }
                    break;
                case SEND_ITEM_OP_VIP:
                    if (player.isAdmin()) {
                        Player pBuffItem = Client.gI().getPlayer(text[0]);
                        int idItemBuff = Integer.parseInt(text[1]);
                        String idOptionBuff = text[2].trim();

                        int slItemBuff = Integer.parseInt(text[3]);

                        try {
                            if (pBuffItem != null) {
                                String txtBuff = "Buff to player: " + pBuffItem.name + "\b";

                                Item itemBuffTemplate = ItemService.gI().createNewItem((short) idItemBuff, slItemBuff);
                                if (!idOptionBuff.isEmpty()) {
                                    String arr[] = idOptionBuff.split("v");
                                    for (int i = 0; i < arr.length; i++) {
                                        String arr2[] = arr[i].split("-");
                                        int idoption = Integer.parseInt(arr2[0].trim());
                                        int param = Integer.parseInt(arr2[1].trim());
                                        itemBuffTemplate.itemOptions.add(new Item.ItemOption(idoption, param));
                                    }

                                }
                                txtBuff += "x" + slItemBuff + " " + itemBuffTemplate.template.name + "\b";
                                InventoryServiceNew.gI().addItemBag(pBuffItem, itemBuffTemplate);
                                InventoryServiceNew.gI().sendItemBags(pBuffItem);
                                NpcService.gI().createTutorial(player, 24, txtBuff);
                                if (player.id != pBuffItem.id) {
                                    NpcService.gI().createTutorial(pBuffItem, 24, txtBuff);
                                }
                            } else {
                                Service.getInstance().sendThongBao(player, "Player không online");
                            }
                        } catch (Exception e) {
                            Service.getInstance().sendThongBao(player, "Đã có lỗi xảy ra vui lòng thử lại");
                        }

                    }
                    break;
                case CHANGE_PASSWORD:
                    Service.gI().changePassword(player, text[0], text[1], text[2]);
                    break;
                case GIFT_CODE: {
                    String textLevel = text[0];
                    Input.gI().addItemGiftCodeToPlayer(player, textLevel);
                    break;
                }
                case FIND_PLAYER:
                    Player pl = Client.gI().getPlayer(text[0]);
                    if (pl != null) {
                        NpcService.gI().createMenuConMeo(player, ConstNpc.MENU_FIND_PLAYER, -1, "Ngài muốn..?",
                                new String[]{"Đi tới\n" + pl.name, "Gọi " + pl.name + "\ntới đây", "Đổi tên", "Ban", "Kick", "Ấy Cho Nó"},
                                pl);
                    } else {
                        Service.gI().sendThongBao(player, "Người chơi không tồn tại hoặc đang offline");
                    }
                    break;
                case CHANGE_NAME: {
                    Player plChanged = (Player) PLAYER_ID_OBJECT.get((int) player.id);
                    if (plChanged != null) {
                        if (GirlkunDB.executeQuery("select * from player where name = ?", text[0]).next()) {
                            Service.gI().sendThongBao(player, "Tên nhân vật đã tồn tại");
                        } else {
                            plChanged.name = text[0];
                            GirlkunDB.executeUpdate("update player set name = ? where id = ?", plChanged.name, plChanged.id);
                            Service.gI().player(plChanged);
                            Service.gI().Send_Caitrang(plChanged);
                            Service.gI().sendFlagBag(plChanged);
                            Zone zone = plChanged.zone;
                            ChangeMapService.gI().changeMap(plChanged, zone, plChanged.location.x, plChanged.location.y);
                            Service.gI().sendThongBao(plChanged, "Chúc mừng bạn đã có cái tên mới đẹp đẽ hơn tên ban đầu");
                            Service.gI().sendThongBao(player, "Đổi tên người chơi thành công");
                        }
                    }
                }
                break;
                case CHANGE_NAME_BY_ITEM: {
                    if (player != null) {
                        if (GirlkunDB.executeQuery("select * from player where name = ?", text[0]).next()) {
                            Service.gI().sendThongBao(player, "Tên nhân vật đã tồn tại");
                            createFormChangeNameByItem(player);
                        } else {
                            Item theDoiTen = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 2006);
                            if (theDoiTen == null) {
                                Service.gI().sendThongBao(player, "Không tìm thấy thẻ đổi tên");
                            } else {
                                InventoryServiceNew.gI().subQuantityItemsBag(player, theDoiTen, 1);
                                player.name = text[0];
                                GirlkunDB.executeUpdate("update player set name = ? where id = ?", player.name, player.id);
                                Service.gI().player(player);
                                Service.gI().Send_Caitrang(player);
                                Service.gI().sendFlagBag(player);
                                Zone zone = player.zone;
                                ChangeMapService.gI().changeMap(player, zone, player.location.x, player.location.y);
                                Service.gI().sendThongBao(player, "Chúc mừng bạn đã có cái tên mới đẹp đẽ hơn tên ban đầu");
                            }
                        }
                    }
                }
                break;

                case TAI:
                    int sotvxiu = Integer.valueOf(text[0]);
                    if (sotvxiu <= 0) {
                        Service.getInstance().sendThongBaoOK(player, "?");
                    } else {
                        TransactionService.gI().cancelTrade(player);
                        Item tv2 = null;
                        for (Item item : player.inventory.itemsBag) {
                            if (item.isNotNullItem() && item.template.id == 457) {
                                tv2 = item;
                                break;
                            }
                        }
                        try {
                            if (tv2 != null && tv2.quantity >= sotvxiu) {
                                InventoryServiceNew.gI().subQuantityItemsBag(player, tv2, sotvxiu);
                                InventoryServiceNew.gI().sendItemBags(player);
                                int TimeSeconds = 10;
                                Service.gI().sendThongBao(player, "Chờ 10 giây để biết kết quả.");
                                while (TimeSeconds > 0) {
                                    TimeSeconds--;
                                    Thread.sleep(1000);
                                }
                                int x = Util.nextInt(1, 6);
                                int y = Util.nextInt(1, 6);
                                int z = Util.nextInt(1, 6);
                                int tong = (x + y + z);
                                if (4 <= (x + y + z) && (x + y + z) <= 10) {
                                    if (player != null) {
                                        Item tvthang = ItemService.gI().createNewItem((short) 457);
                                        tvthang.quantity = (int) Math.round(sotvxiu * 1.5);
                                        InventoryServiceNew.gI().addItemBag(player, tvthang);
                                        InventoryServiceNew.gI().sendItemBags(player);
                                        Service.gI().sendThongBaoOK(player, "Kết quả" + "\nSố hệ thống quay ra : " + x + " "
                                                + y + " " + z + "\nTổng là : " + tong + "\nBạn đã cược : " + sotvxiu
                                                + " thỏi vàng vào Xỉu" + "\nKết quả : Xỉu" + "\n\nVề bờ");
                                        return;
                                    }
                                } else if (x == y && x == z) {
                                    if (player != null) {
                                        Service.gI().sendThongBaoOK(player, "Kết quả" + "Số hệ thống quay ra : " + x + " " + y + " " + z + "\nTổng là : " + tong + "\nBạn đã cược : " + sotvxiu + " thỏi vàng vào Xỉu" + "\nKết quả : Tam hoa" + "\nCòn cái nịt.");
                                        return;
                                    }
                                } else if ((x + y + z) > 10) {
                                    if (player != null) {
                                        Service.gI().sendThongBaoOK(player, "Kết quả" + "\nSố hệ thống quay ra là :"
                                                + " " + x + " " + y + " " + z + "\nTổng là : " + tong + "\nBạn đã cược : "
                                                + sotvxiu + " thỏi vàng vào Xỉu" + "\nKết quả : Tài" + "\nCòn cái nịt.");
                                        return;
                                    }
                                }
                            } else {
                                Service.gI().sendThongBao(player, "Bạn không đủ thỏi vàng để chơi.");
                            }
                        } catch (Exception e) {
                            e.printStackTrace();
                            Service.gI().sendThongBao(player, "Lỗi.");
                        }
                    }
                case XIU:
                    int sotvtai = Integer.valueOf(text[0]);
                    if (sotvtai <= 0) {
                        Service.getInstance().sendThongBao(player, "?");
                    } else {
                        TransactionService.gI().cancelTrade(player);
                        Item tv1 = null;

                        for (Item item : player.inventory.itemsBag) {
                            if (item.isNotNullItem() && item.template.id == 457) {
                                tv1 = item;
                                break;
                            }
                        }
                        try {
                            if (tv1 != null && tv1.quantity >= sotvtai) {
                                InventoryServiceNew.gI().subQuantityItemsBag(player, tv1, sotvtai);
                                InventoryServiceNew.gI().sendItemBags(player);
                                int TimeSeconds = 10;
                                Service.gI().sendThongBao(player, "Chờ 10 giây để biết kết quả.");
                                while (TimeSeconds > 0) {
                                    TimeSeconds--;
                                    Thread.sleep(1000);
                                }
                                int x = Util.nextInt(1, 6);
                                int y = Util.nextInt(1, 6);
                                int z = Util.nextInt(1, 6);
                                int tong = (x + y + z);
                                if (4 <= (x + y + z) && (x + y + z) <= 10) {
                                    if (player != null) {
                                        Service.gI().sendThongBaoOK(player, "Kết quả" + "\nSố hệ thống quay ra là :"
                                                + " " + x + " " + y + " " + z + "\nTổng là : " + tong + "\nBạn đã cược : "
                                                + sotvtai + " thỏi vàng vào Tài" + "\nKết quả : Xỉu" + "\nMày Mất " + sotvtai + " Thòi Vàng");
                                        return;
                                    }
                                } else if (x == y && x == z) {
                                    if (player != null) {
                                        Service.gI().sendThongBaoOK(player, "Kết quả" + "Số hệ thống quay ra : " + x + " " + y + " " + z + "\nTổng là : " + tong + "\nBạn đã cược : " + sotvtai + " thỏi vàng vào Xỉu" + "\nKết quả : Tam hoa" + "\nCòn cái nịt.");
                                        return;
                                    }
                                } else if ((x + y + z) > 10) {

                                    if (player != null) {
                                        Item tvthang = ItemService.gI().createNewItem((short) 457);
                                        tvthang.quantity = (int) Math.round(sotvtai * 1.5);
                                        InventoryServiceNew.gI().addItemBag(player, tvthang);
                                        InventoryServiceNew.gI().sendItemBags(player);
                                        Service.gI().sendThongBaoOK(player, "Kết quả" + "\nSố hệ thống quay ra : " + x + " "
                                                + y + " " + z + "\nTổng là : " + tong + "\nBạn đã cược : " + sotvtai
                                                + " thỏi vàng vào Tài" + "\nKết quả : Tài" + "\n\nVề bờ");
                                        return;
                                    }
                                }
                            } else {
                                Service.gI().sendThongBao(player, "Bạn không đủ thỏi vàng để chơi.");
                            }
                        } catch (Exception e) {
                            e.printStackTrace();
                            Service.gI().sendThongBao(player, "Lỗi.");
                        }
                    }

                case CHOOSE_LEVEL_BDKB:
                    int level = Integer.parseInt(text[0]);
                    if (level >= 1 && level <= 110) {
                        Npc npc = NpcManager.getByIdAndMap(ConstNpc.QUY_LAO_KAME, player.zone.map.mapId);
                        if (npc != null) {
                            npc.createOtherMenu(player, ConstNpc.MENU_ACCEPT_GO_TO_BDKB,
                                    "Con có chắc chắn muốn tới bản đồ kho báu cấp độ " + level + "?",
                                    new String[]{"Đồng ý", "Từ chối"}, level);
                        }
                    } else {
                        Service.getInstance().sendThongBao(player, "Không thể thực hiện");
                    }
                    break;
                case CHOOSE_LEVEL_CDRD:
                    int level3 = Integer.parseInt(text[0]);
                    if (level3 >= 1 && level3 <= 110) {
                        Npc npc = NpcManager.getByIdAndMap(ConstNpc.THAN_VU_TRU, player.zone.map.mapId);
                        if (npc != null) {
                            npc.createOtherMenu(player, ConstNpc.MENU_ACCEPT_GO_TO_CDRD,
                                    "Con có chắc chắn muốn tới con đường rắn độc cấp độ " + level3 + "?",
                                    new String[]{"Đồng ý", "Từ chối"}, level3);
                        }
                    } else {
                        Service.gI().sendThongBao(player, "Không thể thực hiện");
                    }
                    break;
                case CHOOSE_LEVEL_GAS:
                    int levell = Integer.parseInt(text[0]);
                    if (levell >= 1 && levell <= 100) {
                        Npc npc = NpcManager.getByIdAndMap(ConstNpc.POPO, player.zone.map.mapId);
                        if (npc != null) {
                            npc.createOtherMenu(player, ConstNpc.MENU_ACCPET_GO_TO_GAS,
                                    "Con có chắc chắn muốn tới Khí gas huỷ diệt cấp độ " + levell + "?",
                                    new String[]{"Đồng ý, Let's Go", "Từ chối"}, levell);
                        }
                    } else {
                        Service.getInstance().sendThongBao(player, "Không thể thực hiện");
                    }
                    break;
                case QUY_DOI_HONG_NGOC: {
                    int ratioGold = 2000; // tỉ lệ đổi tv
                    int coinGold = 1000; // là cái loz
                    int goldTrade = Integer.parseInt(text[0]);
                    if (goldTrade <= 0 || goldTrade > 500) {
                        Service.getInstance().sendThongBao(player, "Quá giới hạn mỗi lần chỉ được 500");
                    } else if (player.getSession().coinBar >= goldTrade * coinGold) {
                        PlayerDAO.subcoinBar(player, goldTrade * coinGold);
                        short ruby = 861;
                        Item thoiVang = ItemService.gI().createNewItem(ruby, goldTrade * 2000);// x3
                        InventoryServiceNew.gI().addItemBag(player, thoiVang);
                        InventoryServiceNew.gI().sendItemBags(player);
                        Service.getInstance().sendThongBaoOK(player, "bạn nhận được " + goldTrade * ratioGold
                                + " " + thoiVang.template.name);
                        GirlkunDB.executeUpdate("update account set active = 1 where id = ? and username = ?",
                                player.getSession().userId, player.getSession().uu);
                        player.iDMark.setActive(true);
                        player.pointPvp += goldTrade;
                    } else {
                        Service.getInstance().sendThongBao(player, "Số tiền của bạn là " + player.getSession().coinBar + "không đủ để quy"
                                + "đổi" + goldTrade + " thỏi vàng" + " " + "bạn cần thêm " + (player.getSession().coinBar - goldTrade));

                    }
                    break;
                }
                case QUY_DOI_COIN:
                    int ratioGold = 4; // tỉ lệ đổi tv
                    int coinGold = 1000; // là cái loz
                    int goldTrade = Integer.parseInt(text[0]);
                    if (goldTrade <= 0 || goldTrade > 500) {
                        Service.getInstance().sendThongBao(player, "Quá giới hạn mỗi lần chỉ được 500");
                    } else if (player.getSession().coinBar >= goldTrade * coinGold) {
                        PlayerDAO.subcoinBar(player, goldTrade * coinGold);
                        Item thoiVang = ItemService.gI().createNewItem((short) 457, goldTrade * 4);// x3
                        InventoryServiceNew.gI().addItemBag(player, thoiVang);
                        InventoryServiceNew.gI().sendItemBags(player);
                        Service.getInstance().sendThongBaoOK(player, "bạn nhận được " + goldTrade * ratioGold
                                + " " + thoiVang.template.name);
                        GirlkunDB.executeUpdate("update account set active = 1 where id = ? and username = ?",
                                player.getSession().userId, player.getSession().uu);
                        player.iDMark.setActive(true);
                        player.pointPvp += goldTrade;

                    } else {
                        Service.getInstance().sendThongBao(player, "Số tiền của bạn là " + player.getSession().coinBar + "không đủ để quy"
                                + "đổi" + goldTrade + " thỏi vàng" + " " + "bạn cần thêm " + (player.getSession().coinBar - goldTrade));
                    }
                    break;
            }
        } catch (Exception e) {
        }
    }

    public void createForm(Player pl, int typeInput, String title, SubInput... subInputs) {
        pl.iDMark.setTypeInput(typeInput);
        Message msg;
        try {
            msg = new Message(-125);
            msg.writer().writeUTF(title);
            msg.writer().writeByte(subInputs.length);
            for (SubInput si : subInputs) {
                msg.writer().writeUTF(si.name);
                msg.writer().writeByte(si.typeInput);
            }
            pl.sendMessage(msg);
            msg.cleanup();
        } catch (Exception e) {
        }
    }

    public void DonateCsbang(Player pl) {
        createForm(pl, DONATE_CS, "Donate (Điểm Capsule Cá Nhân của bạn sẽ donate vào bang)", new SubInput("Nhập số lượng capsule muốn quyên góp", NUMERIC));
    }

    public void TAI_taixiu(Player pl) {
        createForm(pl, TAI_taixiu, "Chọn số thỏi vàng đặt Tài", new SubInput("Số Thỏi vàng cược", ANY));//????
    }

    public void XIU_taixiu(Player pl) {
        createForm(pl, XIU_taixiu, "Chọn số thỏi vàng đặt Xỉu", new SubInput("Số Thỏi vàng cược", ANY));//????
    }

    public void createForm(ISession session, int typeInput, String title, SubInput... subInputs) {
        Message msg;
        try {
            msg = new Message(-125);
            msg.writer().writeUTF(title);
            msg.writer().writeByte(subInputs.length);
            for (SubInput si : subInputs) {
                msg.writer().writeUTF(si.name);
                msg.writer().writeByte(si.typeInput);
            }
            session.sendMessage(msg);
            msg.cleanup();
        } catch (Exception e) {
        }
    }

    public void createFormChangePassword(Player pl) {
        createForm(pl, CHANGE_PASSWORD, "Quên Mật Khẩu", new SubInput("Nhập mật khẩu đã quên", PASSWORD),
                new SubInput("Mật khẩu mới", PASSWORD),
                new SubInput("Nhập lại mật khẩu mới", PASSWORD));
    }

    public void createFormSenditem(Player pl) {
        createForm(pl, GIVE_IT, "Quyền năng BKT",
                new SubInput("Name", ANY),
                new SubInput("ID Item", NUMERIC),
                new SubInput("Quantity", NUMERIC));
    }

    public void createFormSenditem1(Player pl) {
        createForm(pl, SEND_ITEM_OP, "SEND Vật Phẩm Option",
                new SubInput("Tên người chơi", ANY),
                new SubInput("ID Trang Bị", NUMERIC),
                new SubInput("ID Option", NUMERIC),
                new SubInput("Param", NUMERIC),
                new SubInput("Số lượng", NUMERIC));
    }

    public void createFormSenditemskh(Player pl) {
        createForm(pl, SEND_ITEM_SKH, "Buff SKH Option V2",
                new SubInput("Tên người chơi", ANY),
                new SubInput("ID Trang Bị", NUMERIC),
                new SubInput("ID Option SKH 127 > 135", NUMERIC),
                new SubInput("ID Option Bonus", NUMERIC),
                new SubInput("Param", NUMERIC),
                new SubInput("Số lượng", NUMERIC));
    }

    public void createFormSenditem2(Player pl) {
        createForm(pl, SEND_ITEM_OP_VIP, "BUFF VIP", new SubInput("Tên người chơi", ANY), new SubInput("Id Item", ANY), new SubInput("Chuỗi option vd : 50-20v30-1", ANY), new SubInput("Số lượng", ANY));
    }

    public void ChatAll(Player pl) {
        createForm(pl, CHATALL, "CHAT ALL PLAYER", new SubInput("Chat All", ANY));
    }

    public void createFormGiftCode(Player pl) {
        createForm(pl, GIFT_CODE, "Gift code : tanthu ", new SubInput("Gift-code", ANY));
    }

    public void createFormFindPlayer(Player pl) {
        createForm(pl, FIND_PLAYER, "Tìm kiếm người chơi", new SubInput("Tên người chơi", ANY));
    }

    public void TAI(Player pl) {
        createForm(pl, TAI, "Chọn số thỏi vàng đặt tài", new SubInput("Số thỏi vàng", ANY));
    }

    public void XIU(Player pl) {
        createForm(pl, XIU, "Chọn số thỏi vàng đặt xỉu", new SubInput("Số thỏi vàng", ANY));
    }

    public void createFormNapThe(Player pl, String loaiThe, String menhGia) {
        LOAI_THE = loaiThe;
        MENH_GIA = menhGia;
        createForm(pl, NAP_THE, "Nạp thẻ", new SubInput("Số Seri", ANY), new SubInput("Mã thẻ", ANY));
    }

    public void createFormQDTV(Player pl) {

        createForm(pl, QUY_DOI_COIN, "Quy đổi thỏi vàng, giới hạn đổi không quá 500 ", new SubInput("Nhập số lượng muốn đổi", NUMERIC));
    }

    public void createFormQDHN(Player pl) {

        createForm(pl, QUY_DOI_HONG_NGOC, "Quy đổi hồng ngọc", new SubInput("Nhập số lượng muốn đổi", NUMERIC));
    }

    public void createFormChangeName(Player pl, Player plChanged) {
        PLAYER_ID_OBJECT.put((int) pl.id, plChanged);
        createForm(pl, CHANGE_NAME, "Đổi tên " + plChanged.name, new SubInput("Tên mới", ANY));
    }

    public void createFormChangeNameByItem(Player pl) {
        createForm(pl, CHANGE_NAME_BY_ITEM, "Đổi tên " + pl.name, new SubInput("Tên mới", ANY));
    }

    public void createFormChooseLevelBDKB(Player pl) {
        createForm(pl, CHOOSE_LEVEL_BDKB, "Chọn cấp độ", new SubInput("Cấp độ (1-110)", NUMERIC));
    }

    public void createFormChooseLevelGas(Player pl) {
        createForm(pl, CHOOSE_LEVEL_GAS, "Chọn cấp độ", new SubInput("Cấp độ (1-110)", NUMERIC));
    }

    public void createFormChooseLevelCDRD(Player pl) {
        createForm(pl, CHOOSE_LEVEL_CDRD, "Chọn cấp độ", new SubInput("Cấp độ (1-110)", NUMERIC));
    }

    public static class SubInput {

        private String name;
        private byte typeInput;

        public SubInput(String name, byte typeInput) {
            this.name = name;
            this.typeInput = typeInput;
        }
    }
    public void addItemGiftCodeToPlayer(Player p, final String giftcode) {
        try {
            final GirlkunResultSet red = GirlkunDB.executeQuery("SELECT * FROM `giftcode` WHERE `code` LIKE '" + Util.strSQL(giftcode) + "' LIMIT 1;");
            if (red.first()) {
                String text = "Mã quà tặng" + ": " + giftcode + "\b- " + "Phần quà của bạn là:" + "\b";
                final byte type = red.getByte("type");
                int limit = red.getInt("limit");
                final boolean isDelete = red.getBoolean("Delete");
                final boolean isCheckbag = red.getBoolean("bagCount");
                final JSONArray listUser = (JSONArray) JSONValue.parseWithException(red.getString("listUser"));
                final JSONArray listItem = (JSONArray) JSONValue.parseWithException(red.getString("listItem"));
                final JSONArray option = (JSONArray) JSONValue.parseWithException(red.getString("itemoption"));
                boolean active = red.getBoolean("active");
                if (limit == 0) {
                    NpcService.gI().createTutorial(p, 24, "Số lượng mã quà tặng này đã hết.");
                } else if (active && !p.getSession().actived) {
                    Service.getInstance().sendThongBao(p, "Cần kích hoạt tài khoản để nhận mã quà tặng này");
                    return;
                } else {
                    if (type == 1) {
                        for (int i = 0; i < listUser.size(); ++i) {
                            final int playerId = Integer.parseInt(listUser.get(i).toString());
                            if (playerId == p.id) {
                                Service.gI().sendThongBaoOK(p, "Mỗi tài khoản chỉ được phép sử dụng mã quà tặng này 1 lần duy nhất.");
                                return;
                            }
                        }
                    }
                    if (isCheckbag && listItem.size() > InventoryServiceNew.gI().getCountEmptyBag(p)) {
                        NpcService.gI().createTutorial(p, 24, "Hành trang cần phải có ít nhất " + listItem.size() + " ô trống để nhận vật phẩm");
                    } else {
                        for (int i = 0; i < listItem.size(); ++i) {
                            final JSONObject item = (JSONObject) listItem.get(i);
                            final int idItem = Integer.parseInt(item.get("id").toString());
                            final int quantity = Integer.parseInt(item.get("quantity").toString());

                            if (idItem == -1) {
                                p.inventory.gold = Math.min(p.inventory.gold + (long) quantity, Inventory.LIMIT_GOLD);
                                text += quantity + " vàng\b";
                            } else if (idItem == -2) {
                                p.inventory.gem = Math.min(p.inventory.gem + quantity, 2000000000);
                                text += quantity + " ngọc\b";
                            } else if (idItem == -3) {
                                p.inventory.ruby = Math.min(p.inventory.ruby + quantity, 2000000000);
                                text += quantity + " ngọc khóa\b";
                            } else {
                                Item itemGiftTemplate = ItemService.gI().createNewItem((short) idItem);

                                itemGiftTemplate.quantity = quantity;
                                if (option != null) {
                                    for (int u = 0; u < option.size(); u++) {
                                        JSONObject jsonobject = (JSONObject) option.get(u);
                                        itemGiftTemplate.itemOptions.add(new Item.ItemOption(Integer.parseInt(jsonobject.get("id").toString()), Integer.parseInt(jsonobject.get("param").toString())));
                                    }

                                }
                                text += "x" + quantity + " " + itemGiftTemplate.template.name + "\b";
                                InventoryServiceNew.gI().addItemBag(p, itemGiftTemplate);
                                InventoryServiceNew.gI().sendItemBags(p);
                            }

                            if (i < listItem.size() - 1) {
                                text += "";
                            }
                        }
                        if (limit != -1) {
                            --limit;
                        }
                        listUser.add(p.id);
                        GirlkunDB.executeUpdate("UPDATE `giftcode` SET `limit` = " + limit + ", `listUser` = '" + listUser.toJSONString() + "' WHERE `code` LIKE '" + Util.strSQL(giftcode) + "';");
                        Service.gI().sendThongBaoOK(p, text);
                    }
                }
            } else {
                NpcService.gI().createTutorial(p, 24, "Mã quà tặng không tồn tại hoặc đã được sử dụng");
            }
        } catch (Exception e) {
            NpcService.gI().createTutorial(p, 24, "Có lỗi sảy ra  hãy báo ngay cho QTV để khắc phục.");
            e.printStackTrace();
        }
    }

}
