package com.girlkun.models.npc;

import com.girlkun.consts.ConstMap;
import com.girlkun.models.map.bando.BanDoKhoBau;
import com.girlkun.models.map.bando.BanDoKhoBauService;
import com.girlkun.models.map.gas.Gas;
import com.girlkun.models.map.gas.GasService;
import com.girlkun.models.map.challenge.MartialCongressService;
import com.girlkun.services.*;
import com.girlkun.consts.ConstNpc;
import com.girlkun.consts.ConstPlayer;
import com.girlkun.consts.ConstTask;
import com.girlkun.data.DataGame;
import com.girlkun.database.GirlkunDB;
import com.girlkun.jdbc.daos.PlayerDAO;
import com.girlkun.models.boss.Boss;
import com.girlkun.models.boss.BossData;
import com.girlkun.models.boss.BossID;
import com.girlkun.models.boss.BossManager;
import com.girlkun.models.boss.BossesData;
import com.girlkun.models.boss.list_boss.NhanBan;
import com.girlkun.models.clan.Clan;
import com.girlkun.models.clan.ClanMember;

import java.util.HashMap;
import java.util.List;

import com.girlkun.services.func.ChangeMapService;
import com.girlkun.services.func.SummonDragon;

import static com.girlkun.services.func.SummonDragon.SHENRON_1_STAR_WISHES_1;
import static com.girlkun.services.func.SummonDragon.SHENRON_1_STAR_WISHES_2;
import static com.girlkun.services.func.SummonDragon.SHENRON_SAY;

import com.girlkun.models.player.Player;
import com.girlkun.models.item.Item;
import com.girlkun.models.item.Item.ItemOption;
import static com.girlkun.models.item.ItemTime.TEXT_DO_SAT;
import com.girlkun.models.map.ConDuongRanDoc.ConDuongRanDoc;
import com.girlkun.models.map.ConDuongRanDoc.ConDuongRanDocService;
import com.girlkun.models.map.GiaiCuuMiNuong.GiaiCuuMiNuongService;
import com.girlkun.models.map.Map;
import com.girlkun.models.map.Zone;
import com.girlkun.models.map.blackball.BlackBallWar;
import com.girlkun.models.map.MapMaBu.MapMaBu;
import com.girlkun.models.map.doanhtrai.DoanhTrai;
import com.girlkun.models.map.doanhtrai.DoanhTraiService;
import com.girlkun.models.map.gas.Gas;
import com.girlkun.models.map.gas.GasService;
import com.girlkun.models.map.gas.TopGasService;
import com.girlkun.models.map.nguhanhson.nguhs;
import com.girlkun.models.map.vodai.VoDai;
import com.girlkun.models.map.vodai.VoDaiManager;
import com.girlkun.models.map.vodai.VoDaiService;
import com.girlkun.models.player.Inventory;
import com.girlkun.models.player.NPoint;
import com.girlkun.models.matches.PVPService;
import com.girlkun.models.matches.pvp.DaiHoiVoThuat;
import com.girlkun.models.matches.pvp.DaiHoiVoThuatService;
import com.girlkun.models.shop.ShopServiceNew;
import com.girlkun.models.skill.Skill;
import com.girlkun.network.io.Message;
import com.girlkun.network.server.GirlkunSessionManager;
import com.girlkun.server.Client;
import com.girlkun.server.Controller;
import com.girlkun.server.Maintenance;
import com.girlkun.server.Manager;
import com.girlkun.server.ServerManager;
import com.girlkun.services.func.CombineServiceNew;
import com.girlkun.services.func.Input;
import com.girlkun.services.func.LuckyRound;
import com.girlkun.services.func.TopService;
import com.girlkun.utils.Logger;
import com.girlkun.utils.TimeUtil;
import com.girlkun.utils.Util;
import java.util.ArrayList;
import com.girlkun.services.func.ChonAiDay;
import static com.girlkun.services.func.CombineServiceNew.CHE_TAO_TRANG_BI_TS;
import com.girlkun.services.func.TaiXiu;
import com.kygui.ItemKyGui;
import com.kygui.ShopKyGuiService;
import com.kygui.ShopKyGuiManager;
import java.util.Random;
import java.util.Timer;
import java.util.TimerTask;
import java.util.logging.Level;

import java.util.logging.Level;

public class NpcFactory {

    private static final int COST_HD = 50000000;

    private static boolean nhanVang = false;
    private static boolean nhanDeTu = false;

    //playerid - object
    public static final java.util.Map<Long, Object> PLAYERID_OBJECT = new HashMap<Long, Object>();

    private NpcFactory() {

    }

    private static Npc trungLinhThu(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 104) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Đổi Trứng Linh thú cần:\b|7|X99 Hồn Linh Thú + 1 Tỷ vàng", "Đổi Trứng\nLinh thú", "Từ chối");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 104) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0: {
                                    Item honLinhThu = null;
                                    try {
                                        honLinhThu = InventoryServiceNew.gI().findItemBag(player, 2029);
                                    } catch (Exception e) {
//                                        throw new RuntimeException(e);
                                    }
                                    if (honLinhThu == null || honLinhThu.quantity < 99) {
                                        this.npcChat(player, "Bạn không đủ 99 Hồn Linh thú");
                                    } else if (player.inventory.gold < 1_000_000_000) {
                                        this.npcChat(player, "Bạn không đủ 1 Tỷ vàng");
                                    } else if (InventoryServiceNew.gI().getCountEmptyBag(player) == 0) {
                                        this.npcChat(player, "Hành trang của bạn không đủ chỗ trống");
                                    } else {
                                        player.inventory.gold -= 1_000_000_000;
                                        InventoryServiceNew.gI().subQuantityItemsBag(player, honLinhThu, 99);
                                        Service.gI().sendMoney(player);
                                        Item trungLinhThu = ItemService.gI().createNewItem((short) 2028);
                                        InventoryServiceNew.gI().addItemBag(player, trungLinhThu);
                                        InventoryServiceNew.gI().sendItemBags(player);
                                        this.npcChat(player, "Bạn nhận được 1 Trứng Linh thú");
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    private static Npc kyGui(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    createOtherMenu(player, 0, "Cửa hàng chúng tôi chuyên mua bán hàng hiệu, hàng độc, cảm ơn bạn đã ghé thăm.", "Hướng\ndẫn\nthêm", "Mua bán\nKý gửi", "Từ chối");
                }
            }

            @Override
            public void confirmMenu(Player pl, int select) {
                if (canOpenNpc(pl)) {
                    switch (select) {
                        case 0:
                            Service.getInstance().sendPopUpMultiLine(pl, tempId, avartar, "Cửa hàng chuyên nhận ký gửi mua bán vật phẩm\bChỉ với 5 hồng ngọc\bGiá trị ký gửi 10k-200Tr vàng hoặc 2-2k ngọc\bMột người bán, vạn người mua, mại dô, mại dô");
                            break;
                        case 1:
//                            ShopKyGuiService.gI().openShopKyGui(pl);
                            Service.gI().sendThongBaoOK(pl, "Đang Lỗi Không Mở");
                            break;

                    }
                }
            }
        };
    }

    private static Npc poTaGe(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 140) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Đa vũ trụ song song \b|7|Con muốn gọi con trong đa vũ trụ \b|1|Với giá 200tr vàng không?", "Gọi Boss\nNhân bản", "Từ chối");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 140) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0: {
                                    Boss oldBossClone = BossManager.gI().getBossById(Util.createIdBossClone((int) player.id));
                                    if (oldBossClone != null) {
                                        this.npcChat(player, "Nhà ngươi hãy tiêu diệt Boss lúc trước gọi ra đã, con boss đó đang ở khu " + oldBossClone.zone.zoneId);
                                    } else if (player.inventory.gold < 200_000_000) {
                                        this.npcChat(player, "Nhà ngươi không đủ 200 Triệu vàng ");
                                    } else {
                                        List<Skill> skillList = new ArrayList<>();
                                        for (byte i = 0; i < player.playerSkill.skills.size(); i++) {
                                            Skill skill = player.playerSkill.skills.get(i);
                                            if (skill.point > 0) {
                                                skillList.add(skill);
                                            }
                                        }
                                        int[][] skillTemp = new int[skillList.size()][3];
                                        for (byte i = 0; i < skillList.size(); i++) {
                                            Skill skill = skillList.get(i);
                                            if (skill.point > 0) {
                                                skillTemp[i][0] = skill.template.id;
                                                skillTemp[i][1] = skill.point;
                                                skillTemp[i][2] = skill.coolDown;
                                            }
                                        }
                                        BossData bossDataClone = new BossData(
                                                "Nhân Bản " + player.name,
                                                player.gender,
                                                new short[]{player.getHead(), player.getBody(), player.getLeg(), player.getFlagBag(), player.idAura, player.getEffFront()},
                                                player.nPoint.hpMax / 200,
                                                new long[]{player.nPoint.dame * 1000},
                                                new int[]{140},
                                                skillTemp,
                                                new String[]{"|-2|Boss nhân bản đã xuất hiện rồi"}, //text chat 1
                                                new String[]{"|-1|Ta sẽ chiếm lấy thân xác của ngươi hahaha!"}, //text chat 2
                                                new String[]{"|-1|Lần khác ta sẽ xử đẹp ngươi"}, //text chat 3
                                                60
                                        );

                                        try {
                                            new NhanBan(Util.createIdBossClone((int) player.id), bossDataClone, player.zone);
                                        } catch (Exception e) {
                                            e.printStackTrace();
                                        }
                                        //trừ vàng khi gọi boss
                                        player.inventory.gold -= 200_000_000;
                                        Service.gI().sendMoney(player);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc Giuma(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (this.mapId == 153) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU,
                            "Ngươi có muốn tiến vào map để up mảnh vỡ và mảnh hồn bông tai hay không ?", "Dồng Ý", "Quay Về", "Đóng");
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 153) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    if (player.getSession().player.nPoint.power >= 80000000000L) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 156, -1, 360);
                                    } else {
                                        this.npcChat(player, "Bạn chưa đủ 80 tỷ sức mạnh để vào");
                                    }
                                    break;
                                case 1:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 5, -1, 428);
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    private static Npc quyLaoKame(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            public void chatWithNpc(Player player) {
                String[] chat = {
                    "Phong Cách",
                    "Phong Cách",
                    "Phong Cách"
                };
                Timer timer = new Timer();
                timer.scheduleAtFixedRate(new TimerTask() {
                    int index = 0;

                    @Override
                    public void run() {
                        npcChat(player, chat[index]);
                        index = (index + 1) % chat.length;
                    }
                }, 10000, 10000);
            }

            @Override
            public void openBaseMenu(Player player) {
                chatWithNpc(player);
                if (canOpenNpc(player)) {
                    if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Chào con, con muốn ta giúp gì nào?\n",
                                "Nói Chuyện");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isBaseMenu()) {
                        switch (select) {
                            case 0:
                                this.createOtherMenu(player, 181, "Chào con, con muốn ta giúp gì nào?\n",
                                        "Nhiệm Vụ", "Kho báu dưới biển", "Chức Năng Bang Hội", "Bật Đồ Sát", "Đến Nghĩa Trang");
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == 181) {
                        switch (select) {
                            case 0:
                                Service.gI().sendThongBaoFromQuyLao(player, "|7|Nhiệm Vụ Của Con Hiện Tại:\n" + player.playerTask.taskMain.subTasks.get(player.playerTask.taskMain.index).name);
                                break;
                            case 1:
                                if (player.clan != null) {
                                    if (player.clan.banDoKhoBau != null) {
                                        this.createOtherMenu(player, ConstNpc.MENU_OPENED_DBKB,
                                                "Bang hội của con đang đi tìm kho báu dưới biển cấp độ "
                                                + player.clan.banDoKhoBau.level + "\nCon có muốn đi theo không?",
                                                "Đồng ý", "Từ chối");
                                    } else {

                                        this.createOtherMenu(player, ConstNpc.MENU_OPEN_DBKB,
                                                "Đây là bản đồ kho báu \nCác con cứ yên tâm lên đường\n"
                                                + "Ở đây có ta lo\nNhớ chọn cấp độ vừa sức mình nhé",
                                                "Chọn\ncấp độ", "Từ chối");
                                    }
                                } else {
                                    this.npcChat(player, "Con phải có bang hội ta mới có thể cho con đi");
                                }
                                break;
                            case 2:
                                this.createOtherMenu(player, ConstNpc.CHUC_NANG_BANG_HOI,
                                        "Ta có hỗ trợ những chức năng Bang hội, nhà ngươi cần gì?", "Giải tán\nBang", "Nâng cấp\nBang", "Quyên Góp\nĐiểm Capsule", "Lãnh địa\nBang", "Từ chối");
                                break;
                            case 3:
                                this.createOtherMenu(player, 182, "|7|Bạn Có Chắc Chắn Muốn Đổi 500Tr Vàng\n"
                                        + "100Tr Sức Mạnh Để Bật Đồ Sát?\n",
                                        "Đồng Ý", "Top Đồ Sát", "Đóng");
                                break;
                            case 4:
                                ChangeMapService.gI().changeMapBySpaceShip(player, 185, -1, 190);
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.CHUC_NANG_BANG_HOI) {
                        switch (select) {
                            case 0:
                                Clan clan = player.clan;
                                if (clan != null) {
                                    ClanMember cm = clan.getClanMember((int) player.id);
                                    if (cm != null) {
                                        if (!clan.isLeader(player)) {
                                            Service.gI().sendThongBao(player, "Yêu cầu phải là bang chủ!");
                                            break;
                                        }
                                        if (clan.members.size() > 1) {
                                            Service.gI().sendThongBao(player, "Yêu cầu bang hội chỉ còn một thành viên!");
                                            break;
                                        }
                                        NpcService.gI().createMenuConMeo(player, ConstNpc.CONFIRM_DISSOLUTION_CLAN, -1, "Bạn có chắc chắn muốn giải tán bang hội?\n( Yêu cầu sẽ không thể hoàn tác )",
                                                "Đồng ý", "Từ chối!");
                                        break;
                                    }
                                    break;
                                }
                                Service.gI().sendThongBao(player, "Yêu câu tham gia bang hội");
                                break;
                            case 1:
                                if (player.clan != null) {
                                    if (!player.clan.isLeader(player)) {
                                        Service.gI().sendThongBao(player, "Yêu cầu phải là bang chủ!");
                                        break;
                                    }
                                    if (player.clan.level >= 0 && player.clan.level <= 10) {
                                        this.createOtherMenu(player, ConstNpc.CHUC_NANG_BANG_HOI2,
                                                "Bạn có muốn Nâng cấp lên " + (player.clan.maxMember + 1) + " thành viên không?\n"
                                                + "Cần 2000 Capsule Bang\n"
                                                + "(Thu thập Capsule Bang bằng cách tiêu diệt quái tại Map Lãnh Địa Bang\n"
                                                + "cùng các thành viên khác)", "Nâng cấp\n(20K Ruby)", "Từ chối");
                                    } else {
                                        Service.gI().sendThongBao(player, "Bang của bạn đã đạt cấp tối đa!");
                                        break;
                                    }
                                    break;
                                } else if (player.clan == null) {
                                    Service.gI().sendThongBao(player, "Yêu câu tham gia bang hội");
                                    break;
                                }
                                break;
                            case 2:
                                if (player.clan == null) {
                                    Service.gI().sendThongBao(player, "Yêu câu tham gia bang hội");
                                    break;
                                }
                                Input.gI().DonateCsbang(player);
                                break;
                            case 3:
                                if (player.getSession().player.nPoint.power >= 80000000000L) {
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 153, -1, 432);
                                } else if (player.clan == null) {
                                    Service.gI().sendThongBao(player, "Yêu câu tham gia bang hội");
                                    break;
                                } else {
                                    this.npcChat(player, "Bạn chưa đủ 80 tỷ sức mạnh để vào");
                                }
                                break;
                            case 4:
                                if (player.getSession().player.nPoint.power >= 80000000000L) {
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 153, -1, 432);
                                } else if (player.clan == null) {
                                    Service.gI().sendThongBao(player, "Yêu câu tham gia bang hội");
                                    break;
                                } else {
                                    this.npcChat(player, "Bạn chưa đủ 80 tỷ sức mạnh để vào");
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.CHUC_NANG_BANG_HOI2) {
                        Clan clan = player.clan;
                        switch (select) {
                            case 0:
                                if (player.clan.capsuleClan >= 2000 && clan.isLeader(player) && player.inventory.ruby >= 20000) {
                                    player.clan.level += 1;
                                    player.clan.maxMember += 1;
                                    player.clan.capsuleClan -= 2000;
                                    player.inventory.subRuby(20000);
                                    player.clan.update();
                                    Service.gI().sendThongBao(player, "Yêu cầu nâng cấp bang hội thành công");
                                    break;
                                } else if (player.inventory.ruby < 20000) {
                                    Service.gI().sendThongBaoOK(player, "Bạn còn thiều " + (20000 - player.inventory.ruby) + " Hồng Ngọc");
                                    break;
                                } else if (player.clan.capsuleClan < 1000) {
                                    Service.gI().sendThongBaoOK(player, "Bang của bạn còn thiều " + (2000 - player.clan.capsuleClan) + " Capsule bang");
                                    break;
                                }
                        }
                    } else if (player.iDMark.getIndexMenu() == 182) {
                        switch (select) {
                            case 0:
                                if (player.getSession().player.nPoint.power < 80000000000L) {
                                    Service.gI().sendThongBao(player, "Cần Có Sức Mạnh Là 80 Tỉ");
                                } else if (player.getSession().player.inventory.gold < 500000000) {
                                    Service.gI().sendThongBao(player, "Cần 500tr Vàng");
                                } else {
                                    player.nPoint.power -= 100000000;
                                    player.getSession().player.inventory.gold -= 500000000;
                                    player.nPoint.teleport = true;
                                    player.name = player.name + "\n" + "[Đang Bật Đồ Sát]";
                                    Service.gI().player(player);
                                    Service.gI().Send_Caitrang(player);
                                    Service.gI().sendFlagBag(player);
                                    Zone zone = player.zone;
                                    ChangeMapService.gI().changeMap(player, zone, player.location.x, player.location.y);
                                    Service.gI().changeFlag(player, 8);
                                    PlayerService.gI().changeAndSendTypePK(player, ConstPlayer.PK_ALL);
                                    player.point_dosat++;
                                    new Thread(() -> {
                                        try {
                                            Thread.sleep(600000);
                                        } catch (Exception e) {
                                        }
                                        Client.gI().kickSession(player.getSession());
                                    }).start();
                                    ChatGlobalService.gI().chat(player, "Player " + player.name + " Đang Ở " + player.zone.map.mapName + " Khu " + player.zone.zoneId);
                                    ItemTimeService.gI().sendTextDoSat(player);
                                    break;
                                }
                                break;
                            case 1:
                                Service.gI().showListTop(player, Manager.topDoSat);
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPENED_DBKB) {
                        switch (select) {
                            case 0:
                                if (player.isAdmin() || player.nPoint.power >= BanDoKhoBau.POWER_CAN_GO_TO_DBKB) {
                                    ChangeMapService.gI().goToDBKB(player);
                                } else {
                                    this.npcChat(player, "Sức mạnh của con phải ít nhất phải đạt "
                                            + Util.numberToMoney(BanDoKhoBau.POWER_CAN_GO_TO_DBKB));
                                }
                                break;

                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPEN_DBKB) {
                        switch (select) {
                            case 0:
                                if (player.isAdmin() || player.nPoint.power >= BanDoKhoBau.POWER_CAN_GO_TO_DBKB) {
                                    Input.gI().createFormChooseLevelBDKB(player);
                                } else {
                                    this.npcChat(player, "Sức mạnh của con phải ít nhất phải đạt "
                                            + Util.numberToMoney(BanDoKhoBau.POWER_CAN_GO_TO_DBKB));
                                }
                                break;
                        }

                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_ACCEPT_GO_TO_BDKB) {
                        switch (select) {
                            case 0:
                                BanDoKhoBauService.gI().openBanDoKhoBau(player, Byte.parseByte(String.valueOf(PLAYERID_OBJECT.get(player.id))));
                                break;
                        }
                    }
                }

            }
//            }
        };
    }

    public static Npc truongLaoGuru(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                        super.openBaseMenu(player);
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {

                }
            }
        };
    }

    public static Npc vuaVegeta(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                        super.openBaseMenu(player);
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {

                }
            }
        };
    }

    public static Npc ongGohan_ongMoori_ongParagus(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Con cố gắng theo %1 học thành tài, đừng lo lắng cho ta.\n"
                                + "Con đang có: " + player.getSession().vnd + " VNĐ"
                                .replaceAll("%1", player.gender == ConstPlayer.TRAI_DAT ? "Quy lão Kamê"
                                        : player.gender == ConstPlayer.NAMEC ? "Trưởng lão Guru" : "Vua Vegeta"),
                                "Đổi mật khẩu", "Nhận ngọc xanh", "Kích hoạt\n Tài khoản", "Đổi Chúc Phúc");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isBaseMenu()) {
                        switch (select) {
                            case 0:
                                Input.gI().createFormChangePassword(player);
                                break;
                            case 1:
                                if (player.inventory.gem == 2000000) {
                                    this.npcChat(player, "Tham Lam");
                                    break;
                                }
                                player.inventory.gem = 2000000;
                                Service.getInstance().sendMoney(player);
                                Service.getInstance().sendThongBao(player, "Bạn vừa nhận được 2 triệu ngọc xanh");
                                break;
                            case 2:
                                this.createOtherMenu(player, 0, "|7|Kích Hoạt Tài Khoản\n"
                                        + "|2|Số tiền hiện tại : " + Util.format(player.getSession().vnd) + " VNĐ"
                                        + "\n|5|Trạng thái tài khoản : " + (player.getSession().actived == false ? "Chưa kích hoạt" : "Đã kích hoạt")
                                        + "\n|2|Trạng thái VIP : " + (player.vip == 1 ? "VIP" : player.vip == 2 ? "VIP2" : player.vip == 3 ? "VIP3" : player.vip == 4 ? "SVIP" : "Chưa Kích Hoạt")
                                        + (player.timevip > 0 ? "\n|5|Hạn còn : " + Util.msToThang(player.timevip) : ""), "Kích Hoạt\nVIP", "Điểm Danh", "Đóng");

                                break;
                            case 3:
                                this.createOtherMenu(player, 2010, "|2|Hãy đến Đảo Kame Gặp Ông Quy Lão\n"
                                        + "Để có thể bật chức năng Đồ Sát\n"
                                        + "Sau khi bật Đồ Sát con hãy tiêu diệt những player khác\n"
                                        + "Để có thể nhận được điểm Đồ Sát\n"
                                        + "Sau đó con hãy về đây\n"
                                        + "Ta sẽ tặng con những chúc phúc sau đây\n"
                                        + "|7|Điểm Đồ Sát hiện tại của con là: " + player.point_dosat,
                                        "Chúc Phúc\nSĐ", "Chúc Phúc\nHP", "Chúc Phúc\nMP");
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == 2010) {
                        switch (select) {
                            case 0:
                                if (player.point_dosat >= 20) {
                                    player.point_dosat -= 20;
                                    Item item = ItemService.gI().createNewItem((short) (1352));
                                    InventoryServiceNew.gI().addItemBag(player, item);
                                    Service.gI().sendThongBao(player, "Chúc Mừng Bạn Đổi Vật Phẩm Thành Công !");
                                } else {
                                    Service.gI().sendThongBao(player, "Không đủ điểm, bạn còn " + (20 - player.point_dosat) + " điểm nữa");
                                }
                                break;
                            case 1:
                                if (player.point_dosat >= 20) {
                                    player.point_dosat -= 20;
                                    Item item = ItemService.gI().createNewItem((short) (1353));
                                    InventoryServiceNew.gI().addItemBag(player, item);
                                    Service.gI().sendThongBao(player, "Chúc Mừng Bạn Đổi Vật Phẩm Thành Công !");
                                } else {
                                    Service.gI().sendThongBao(player, "Không đủ điểm, bạn còn " + (20 - player.point_dosat) + " điểm nữa");
                                }
                                break;
                            case 2:
                                if (player.point_dosat >= 20) {
                                    player.point_dosat -= 20;
                                    Item item = ItemService.gI().createNewItem((short) (1354));
                                    InventoryServiceNew.gI().addItemBag(player, item);
                                    Service.gI().sendThongBao(player, "Chúc Mừng Bạn Đổi Vật Phẩm Thành Công !");
                                } else {
                                    Service.gI().sendThongBao(player, "Không đủ điểm, bạn còn " + (20 - player.point_dosat) + " điểm nữa");
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == 0) {
                        switch (select) {
                            case 0:
                                this.createOtherMenu(player, 1, "|7|MUA THẺ VIP THÁNG\n"
                                        + "|2|Số tiền hiện tại : " + Util.format(player.getSession().vnd) + " VNĐ"
                                        + "\n|2|Trạng thái VIP : " + (player.vip == 1 ? "VIP" : player.vip == 2 ? "VIP2" : player.vip == 3 ? "VIP3" : player.vip == 4 ? "SVIP" : "Chưa Kích Hoạt")
                                        + (player.timevip > 0 ? "\n|5|Hạn còn : " + Util.msToThang(player.timevip) : ""), "Kích Hoạt\nVIP1\n20.000Đ", "Kích Hoạt\nVIP2\n30.000Đ", "Kích Hoạt\nVIP3\n50.000Đ", "Kích Hoạt\nSVIP\n70.000Đ", "Đóng");
                                break;
                            case 1:
                                if (player.diemdanh < 1) {
                                    int tv = 0;
                                    int hn = 0;
                                    switch (player.vip) {
                                        case 0:
                                            hn = 5000;
                                            break;
                                        case 1:
                                            tv = 10;
                                            hn = 10000;
                                            break;
                                        case 2:
                                            tv = 15;
                                            hn = 15000;
                                            break;
                                        case 3:
                                            tv = 20;
                                            hn = 20000;
                                            break;
                                        case 4:
                                            tv = 25;
                                            hn = 25000;
                                            break;
                                    }
                                    player.inventory.ruby += hn;
                                    Item thoivang = ItemService.gI().createNewItem((short) 457, tv);
                                    InventoryServiceNew.gI().addItemBag(player, thoivang);
                                    InventoryServiceNew.gI().sendItemBags(player);
                                    Service.gI().sendMoney(player);
                                    player.diemdanh++;
                                    Service.getInstance().sendThongBao(player, "|7|Điểm danh thành công!\nNhận được " + tv + " Thỏi vàng và " + Util.format(hn) + " Hồng ngọc");
                                } else {
                                    this.npcChat(player, "Hôm nay đã nhận rồi mà !!!");
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == 1) {
                        switch (select) {
                            case 0:
                                this.createOtherMenu(player, 2, "|7|VIP1\n"
                                        + "|2|Quyền lợi đi kèm\n"
                                        + "|5|Nhận 5 Thỏi Vàng/ngày"
                                        + "\nNhận 5.000 Hồng Ngọc/ngày"
                                        + "\nTăng 20% TNSM"
                                        + "\n|2|Số tiền hiện tại : " + Util.format(player.getSession().vnd) + " VNĐ"
                                        + (player.vip == 1 ? "\n|7|Trạng thái VIP : VIP1" : player.vip == 2 ? "\n|7|Trạng thái VIP : VIP2" : player.vip == 3 ? "\n|7|Trạng thái VIP : VIP3" : player.vip == 4 ? "\n|7|Trạng thái VIP : SVIP" : "")
                                        + (player.timevip > 0 ? "\nHạn còn : " + Util.msToThang(player.timevip) : ""), "Kích Hoạt", "Đóng");
                                break;
                            case 1:
                                this.createOtherMenu(player, 3, "|7|VIP2\n"
                                        + "|2|Quyền lợi đi kèm\n"
                                        + "|5|Nhận 8 Thỏi Vàng/ngày"
                                        + "\nNhận 10.000 Hồng Ngọc/ngày"
                                        + "\nTăng 20% TNSM"
                                        + "\n|2|Số tiền hiện tại : " + Util.format(player.getSession().vnd) + " VNĐ"
                                        + (player.vip == 1 ? "\n|7|Trạng thái VIP : VIP1" : player.vip == 2 ? "\n|7|Trạng thái VIP : VIP2" : player.vip == 3 ? "\n|7|Trạng thái VIP : VIP3" : player.vip == 4 ? "\n|7|Trạng thái VIP : SVIP" : "")
                                        + (player.timevip > 0 ? "\nHạn còn : " + Util.msToThang(player.timevip) : ""), "Kích Hoạt", "Đóng");
                                break;
                            case 2:
                                this.createOtherMenu(player, 4, "|7|VIP3\n"
                                        + "|2|Quyền lợi đi kèm\n"
                                        + "|5|Nhận 12 Thỏi Vàng/ngày"
                                        + "\nNhận 15.000 Hồng Ngọc/ngày"
                                        + "\nTăng 20% TNSM"
                                        + "\n|2|Số tiền hiện tại : " + Util.format(player.getSession().vnd) + " VNĐ"
                                        + (player.vip == 1 ? "\n|7|Trạng thái VIP : VIP1" : player.vip == 2 ? "\n|7|Trạng thái VIP : VIP2" : player.vip == 3 ? "\n|7|Trạng thái VIP : VIP3" : player.vip == 4 ? "\n|7|Trạng thái VIP : SVIP" : "")
                                        + (player.timevip > 0 ? "\nHạn còn : " + Util.msToThang(player.timevip) : ""), "Kích Hoạt", "Đóng");
                                break;
                            case 3:
                                this.createOtherMenu(player, 5, "|7|SVIP\n"
                                        + "|2|Quyền lợi đi kèm\n"
                                        + "|5|Nhận 15 Thỏi Vàng/ngày"
                                        + "\nNhận 20.000 Hồng Ngọc/ngày"
                                        + "\nTăng 20% TNSM"
                                        + "\n|2|Số tiền hiện tại : " + Util.format(player.getSession().vnd) + " VNĐ"
                                        + (player.vip == 1 ? "\n|7|Trạng thái VIP : VIP1" : player.vip == 2 ? "\n|7|Trạng thái VIP : VIP2" : player.vip == 3 ? "\n|7|Trạng thái VIP : VIP3" : player.vip == 4 ? "\n|7|Trạng thái VIP : SVIP" : "")
                                        + (player.timevip > 0 ? "\nHạn còn : " + Util.msToThang(player.timevip) : ""), "Kích Hoạt", "Đóng");
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == 2) {
                        switch (select) {
                            case 0:
                                if (player.vip >= 1) {
                                    this.npcChat(player, "|7|Bạn đang là thành viên " + (player.vip == 4 ? "SVIP" : "VIP" + player.vip) + " rồi");
                                    return;
                                }
                                if (player.getSession().vnd >= 20000) {
                                    player.vip = 1;
                                    player.timevip = (System.currentTimeMillis() + (1000 * 60 * 60 * 24 * 15)) + (1000 * 60 * 60 * 24 * 16);
                                    PlayerDAO.subvnd(player, 20000);
                                    Service.gI().sendMoney(player);
                                    this.npcChat(player, "|6|Đã mở thành công\n|7|VIP1");
                                } else {
                                    this.npcChat(player, "Bạn không đủ tiền");
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == 3) {
                        switch (select) {
                            case 0:
                                if (player.vip >= 2) {
                                    this.npcChat(player, "|7|Bạn đang là thành viên " + (player.vip == 4 ? "SVIP" : "VIP" + player.vip) + " rồi");
                                    return;
                                }
                                if (player.getSession().vnd >= 30000) {
                                    player.vip = 2;
                                    player.timevip = (System.currentTimeMillis() + (1000 * 60 * 60 * 24 * 15)) + (1000 * 60 * 60 * 24 * 16);
                                    PlayerDAO.subvnd(player, 30000);
                                    Service.gI().sendMoney(player);
                                    this.npcChat(player, "|6|Đã mở thành công\n|7|VIP2");
                                } else {
                                    this.npcChat(player, "Bạn không đủ tiền");
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == 4) {
                        switch (select) {
                            case 0:
                                if (player.vip >= 3) {
                                    this.npcChat(player, "|7|Bạn đang là thành viên " + (player.vip == 4 ? "SVIP" : "VIP" + player.vip) + " rồi");
                                    return;
                                }
                                if (player.getSession().vnd >= 50000) {
                                    player.vip = 3;
                                    player.timevip = (System.currentTimeMillis() + (1000 * 60 * 60 * 24 * 15)) + (1000 * 60 * 60 * 24 * 16);
                                    PlayerDAO.subvnd(player, 50000);
                                    Service.gI().sendMoney(player);
                                    this.npcChat(player, "|6|Đã mở thành công\n|7|VIP3");
                                } else {
                                    this.npcChat(player, "Bạn không đủ tiền");
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == 5) {
                        switch (select) {
                            case 0:
                                if (player.vip >= 4) {
                                    this.npcChat(player, "|7|Bạn đang là thành viên " + (player.vip == 4 ? "SVIP" : "VIP" + player.vip) + " rồi");
                                    return;
                                }
                                if (player.getSession().vnd >= 70000) {
                                    player.vip = 4;
                                    player.timevip = (System.currentTimeMillis() + (1000 * 60 * 60 * 24 * 15)) + (1000 * 60 * 60 * 24 * 16);
                                    PlayerDAO.subvnd(player, 70000);
                                    Service.gI().sendMoney(player);
                                    this.npcChat(player, "|6|Đã mở thành công\n|7|SVIP");
                                } else {
                                    this.npcChat(player, "Bạn không đủ tiền");
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.CONFIRM_ACTIVE) {
                        switch (select) {
                            case 0:
                                if (!player.getSession().actived) {
                                    if (player.getSession().vnd >= 10000) {
                                        player.getSession().actived = true;
                                        if (PlayerDAO.subvnd(player, 10000)) ;
                                        Item vangnemay = ItemService.gI().createNewItem((short) 457);
                                        vangnemay.quantity += 24;
                                        player.inventory.ruby += 10000;
                                        InventoryServiceNew.gI().addItemBag(player, vangnemay);
                                        InventoryServiceNew.gI().sendItemBags(player);
                                        Service.getInstance().sendMoney(player);
                                        Service.gI().sendThongBao(player, "|7|Kích hoạt thành công, bạn nhận được thêm 25 Thỏi Vàng và 10k Hồng Ngọc");
                                    } else {
                                        this.npcChat(player, "Không Đủ Tiền Mở Thành Viên...!");
                                    }
                                } else {
                                    this.npcChat(player, "Bạn đã mở thành viên rồi!");

                                }
                                break;
                            case 1:
                                this.npcChat(player, "Lần sau tiếp lúa cho ta nữa nha con!!!");
                                break;
                        }
                    }
                }
            }
        };
    }

    public static Npc bulmaQK(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Cậu cần trang bị gì cứ đến chỗ tôi nhé", "Cửa\nhàng");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isBaseMenu()) {
                        switch (select) {
                            case 0://Shop
                                if (player.gender == ConstPlayer.TRAI_DAT) {
                                    ShopServiceNew.gI().opendShop(player, "BUNMA", true);
                                } else {
                                    this.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Xin lỗi cưng, chị chỉ bán đồ cho người Trái Đất", "Đóng");
                                }
                                break;
                        }
                    }
                }
            }
        };
    }

    public static Npc dende(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                new Thread(() -> {
                    try {
                        while (true) {
                            Thread.sleep(5000);
                            new Thread(() -> {
                                try {
                                    Thread.sleep(1000);
                                    this.npcChat(player, "Bế Ngọc Rồng NM Về Đây Tao Gọi Rồng Cho");
                                } catch (Exception e) {
                                }
                            }).start();
                        }
                    } catch (Exception e) {
                    }
                }).start();
                if (canOpenNpc(player)) {
                    if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                        if (player.idNRNM != -1) {
                            if (player.zone.map.mapId == 7) {
                                this.createOtherMenu(player, 1, "Ồ, ngọc rồng namếc, bạn thật là may mắn\nnếu tìm đủ 7 viên sẽ được Rồng Thiêng Namếc ban cho điều ước", "Hướng\ndẫn\nGọi Rồng", "Gọi rồng", "Từ chối");
                            }
                        } else {
                            this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                    "Anh cần trang bị gì cứ đến chỗ em nhé", "Cửa\nhàng");
                        }
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isBaseMenu()) {
                        switch (select) {
                            case 0://Shop
                                if (player.gender == ConstPlayer.NAMEC) {
                                    ShopServiceNew.gI().opendShop(player, "DENDE", true);
                                } else {
                                    this.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Xin lỗi anh, em chỉ bán đồ cho dân tộc Namếc", "Đóng");
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == 1) {
                        if (player.zone.map.mapId == 7 && player.idNRNM != -1) {
                            if (player.idNRNM == 353) {
                                NgocRongNamecService.gI().tOpenNrNamec = System.currentTimeMillis() + 86400000;
                                NgocRongNamecService.gI().firstNrNamec = true;
                                NgocRongNamecService.gI().timeNrNamec = 0;
                                NgocRongNamecService.gI().doneDragonNamec();
                                NgocRongNamecService.gI().initNgocRongNamec((byte) 1);
                                NgocRongNamecService.gI().reInitNrNamec((long) 86399000);
                                SummonDragon.gI().summonNamec(player);
                            } else {
                                Service.gI().sendThongBao(player, "Anh phải có viên ngọc rồng Namếc 1 sao");
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc appule(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                new Thread(() -> {
                    try {
                        while (true) {
                            Thread.sleep(5000);
                            new Thread(() -> {
                                try {
                                    Thread.sleep(1000);
                                    this.npcChat(player, "Nhận Địt Nhau");
                                } catch (Exception e) {
                                }
                            }).start();
                        }
                    } catch (Exception e) {
                    }
                }).start();
                if (canOpenNpc(player)) {
                    if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Ngươi cần trang bị gì cứ đến chỗ ta nhé", "Cửa\nhàng");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isBaseMenu()) {
                        switch (select) {
                            case 0://Shop
                                if (player.gender == ConstPlayer.XAYDA) {
                                    ShopServiceNew.gI().opendShop(player, "APPULE", true);
                                } else {
                                    this.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Về hành tinh hạ đẳng của ngươi mà mua đồ cùi nhé. Tại đây ta chỉ bán đồ cho người Xayda thôi", "Đóng");
                                }
                                break;
                        }
                    }
                }
            }
        };
    }

    public static Npc drDrief(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player pl) {
                new Thread(() -> {
                    try {
                        while (true) {
                            Thread.sleep(5000);
                            new Thread(() -> {
                                try {
                                    Thread.sleep(1000);
                                    this.npcChat(pl, "Nhận Địt Nhau");
                                } catch (Exception e) {
                                }
                            }).start();
                        }
                    } catch (Exception e) {
                    }
                }).start();
                if (canOpenNpc(pl)) {
                    if (this.mapId == 84) {
                        this.createOtherMenu(pl, ConstNpc.BASE_MENU,
                                "Tàu Vũ Trụ của ta có thể đưa cậu đến hành tinh khác chỉ trong 3 giây. Cậu muốn đi đâu?",
                                pl.gender == ConstPlayer.TRAI_DAT ? "Đến\nTrái Đất" : pl.gender == ConstPlayer.NAMEC ? "Đến\nNamếc" : "Đến\nXayda");
                    } else if (!TaskService.gI().checkDoneTaskTalkNpc(pl, this)) {
                        if (pl.playerTask.taskMain.id == 7) {
                            NpcService.gI().createTutorial(pl, this.avartar, "Hãy lên đường cứu đứa bé nhà tôi\n"
                                    + "Chắc bây giờ nó đang sợ hãi lắm rồi");
                        } else {
                            this.createOtherMenu(pl, ConstNpc.BASE_MENU,
                                    "Tàu Vũ Trụ của ta có thể đưa cậu đến hành tinh khác chỉ trong 3 giây. Cậu muốn đi đâu?",
                                    "Đến\nNamếc", "Đến\nXayda", "Siêu thị");
                        }
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 84) {
                        ChangeMapService.gI().changeMapBySpaceShip(player, player.gender + 24, -1, -1);
                    } else if (player.iDMark.isBaseMenu()) {
                        switch (select) {
                            case 0:
                                ChangeMapService.gI().changeMapBySpaceShip(player, 25, -1, -1);
                                break;
                            case 1:
                                ChangeMapService.gI().changeMapBySpaceShip(player, 26, -1, -1);
                                break;
                            case 2:
                                ChangeMapService.gI().changeMapBySpaceShip(player, 84, -1, -1);
                                break;
                        }
                    }
                }
            }
        };
    }

    public static Npc cargo(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player pl) {
                new Thread(() -> {
                    try {
                        while (true) {
                            Thread.sleep(5000);
                            new Thread(() -> {
                                try {
                                    Thread.sleep(1000);
                                    this.npcChat(pl, "Nhận Địt Nhau");
                                } catch (Exception e) {
                                }
                            }).start();
                        }
                    } catch (Exception e) {
                    }
                }).start();
                if (canOpenNpc(pl)) {
                    if (!TaskService.gI().checkDoneTaskTalkNpc(pl, this)) {
                        if (pl.playerTask.taskMain.id == 7) {
                            NpcService.gI().createTutorial(pl, this.avartar, "Hãy lên đường cứu đứa bé nhà tôi\n"
                                    + "Chắc bây giờ nó đang sợ hãi lắm rồi");
                        } else {
                            this.createOtherMenu(pl, ConstNpc.BASE_MENU,
                                    "Tàu Vũ Trụ của ta có thể đưa cậu đến hành tinh khác chỉ trong 3 giây. Cậu muốn đi đâu?",
                                    "Đến\nTrái Đất", "Đến\nXayda", "Siêu thị");
                        }
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isBaseMenu()) {
                        switch (select) {
                            case 0:
                                ChangeMapService.gI().changeMapBySpaceShip(player, 24, -1, -1);
                                break;
                            case 1:
                                ChangeMapService.gI().changeMapBySpaceShip(player, 26, -1, -1);
                                break;
                            case 2:
                                ChangeMapService.gI().changeMapBySpaceShip(player, 84, -1, -1);
                                break;
                        }
                    }
                }
            }
        };
    }

    public static Npc cui(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            private final int COST_FIND_BOSS = 50000000;

            @Override
            public void openBaseMenu(Player pl) {
                if (canOpenNpc(pl)) {
                    if (!TaskService.gI().checkDoneTaskTalkNpc(pl, this)) {
                        if (pl.playerTask.taskMain.id == 7) {
                            NpcService.gI().createTutorial(pl, this.avartar, "Hãy lên đường cứu đứa bé nhà tôi\n"
                                    + "Chắc bây giờ nó đang bị địt rồi");
                        } else {
                            if (this.mapId == 19) {

                                int taskId = TaskService.gI().getIdTask(pl);
                                switch (taskId) {
                                    case ConstTask.TASK_19_0:
                                        this.createOtherMenu(pl, ConstNpc.MENU_FIND_KUKU,
                                                "Đội quân của Fide đang ở Thung lũng Nappa, ta sẽ đưa ngươi đến đó",
                                                "Đến chỗ\nKuku\n(" + Util.numberToMoney(COST_FIND_BOSS) + " vàng)", "Đến Cold", "Đến\nNappa", "Từ chối");
                                        break;
                                    case ConstTask.TASK_19_1:
                                        this.createOtherMenu(pl, ConstNpc.MENU_FIND_MAP_DAU_DINH,
                                                "Đội quân của Fide đang ở Thung lũng Nappa, ta sẽ đưa ngươi đến đó",
                                                "Đến chỗ\nMập đầu đinh\n(" + Util.numberToMoney(COST_FIND_BOSS) + " vàng)", "Đến Cold", "Đến\nNappa", "Từ chối");
                                        break;
                                    case ConstTask.TASK_19_2:
                                        this.createOtherMenu(pl, ConstNpc.MENU_FIND_RAMBO,
                                                "Đội quân của Fide đang ở Thung lũng Nappa, ta sẽ đưa ngươi đến đó",
                                                "Đến chỗ\nRambo\n(" + Util.numberToMoney(COST_FIND_BOSS) + " vàng)", "Đến Cold", "Đến\nNappa", "Từ chối");
                                        break;
                                    default:
                                        this.createOtherMenu(pl, ConstNpc.BASE_MENU,
                                                "Đội quân của Fide đang ở Thung lũng Nappa, ta sẽ đưa ngươi đến đó",
                                                "Đến Cold", "Đến\nNappa", "Từ chối");

                                        break;
                                }
                            } else if (this.mapId == 68) {
                                this.createOtherMenu(pl, ConstNpc.BASE_MENU,
                                        "Ngươi muốn về Thành Phố Vegeta", "Đồng ý", "Từ chối");
                            } else {
                                this.createOtherMenu(pl, ConstNpc.BASE_MENU,
                                        "Tàu vũ trụ Xayda sử dụng công nghệ mới nhất, "
                                        + "có thể đưa ngươi đi bất kỳ đâu, chỉ cần trả tiền là được.",
                                        "Đến\nTrái Đất", "Đến\nNamếc", "Siêu thị");
                            }
                        }
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 26) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 24, -1, -1);
                                    break;
                                case 1:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 25, -1, -1);
                                    break;
                                case 2:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 84, -1, -1);
                                    break;
                            }
                        }
                    }
                    if (this.mapId == 19) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    if (player.getSession().player.nPoint.power >= 80000000000L && player.playerTask.taskMain.id > 21) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 109, -1, 295);
                                    } //                                    break;
                                    else {
                                        Service.gI().sendThongBaoOK(player, "Làm Nhiệm Vụ Và Đạt 80 Tỷ Sức Mạnh");
                                    }
                                    break;
                                case 1:
                                    if (player.playerTask.taskMain.id >= 17) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 68, -1, 90);
                                    } else {
                                        Service.gI().sendThongBaoOK(player, "Làm Nhiệm Vụ Đi");
                                    }
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_FIND_KUKU) {
                            switch (select) {
                                case 0:
                                    Boss boss = BossManager.gI().getBossById(BossID.KUKU);
                                    if (boss != null && !boss.isDie()) {
                                        if (player.inventory.gold >= COST_FIND_BOSS) {
                                            Zone z = MapService.gI().getMapCanJoin(player, boss.zone.map.mapId, boss.zone.zoneId);
                                            if (z != null && z.getNumOfPlayers() < z.maxPlayer) {
                                                player.inventory.gold -= COST_FIND_BOSS;
                                                ChangeMapService.gI().changeMap(player, boss.zone, boss.location.x, boss.location.y);
                                                Service.gI().sendMoney(player);
                                            } else {
                                                Service.gI().sendThongBao(player, "Khu vực đang full.");
                                            }
                                        } else {
                                            Service.gI().sendThongBao(player, "Không đủ vàng, còn thiếu "
                                                    + Util.numberToMoney(COST_FIND_BOSS - player.inventory.gold) + " vàng");
                                        }
                                        break;
                                    }
                                    Service.gI().sendThongBao(player, "Chết rồi ba...");
                                    break;
                                case 1:
                                    if (player.getSession().player.nPoint.power >= 80000000000L && player.playerTask.taskMain.id > 21) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 109, -1, 295);
                                    } //                                    break;
                                    else {
                                        Service.gI().sendThongBaoOK(player, "Làm Nhiệm Vụ Và Đạt 80 Tỷ Sức Mạnh");
                                    }
                                    break;
                                case 2:
                                    if (player.playerTask.taskMain.id >= 17) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 68, -1, 90);
                                    } else {
                                        Service.gI().sendThongBaoOK(player, "Làm Nhiệm Vụ Đi");
                                    }
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_FIND_MAP_DAU_DINH) {
                            switch (select) {
                                case 0:
                                    Boss boss = BossManager.gI().getBossById(BossID.MAP_DAU_DINH);
                                    if (boss != null && !boss.isDie()) {
                                        if (player.inventory.gold >= COST_FIND_BOSS) {
                                            Zone z = MapService.gI().getMapCanJoin(player, boss.zone.map.mapId, boss.zone.zoneId);
                                            if (z != null && z.getNumOfPlayers() < z.maxPlayer) {
                                                player.inventory.gold -= COST_FIND_BOSS;
                                                ChangeMapService.gI().changeMap(player, boss.zone, boss.location.x, boss.location.y);
                                                Service.gI().sendMoney(player);
                                            } else {
                                                Service.gI().sendThongBao(player, "Khu vực đang full.");
                                            }
                                        } else {
                                            Service.gI().sendThongBao(player, "Không đủ vàng, còn thiếu "
                                                    + Util.numberToMoney(COST_FIND_BOSS - player.inventory.gold) + " vàng");
                                        }
                                        break;
                                    }
                                    Service.gI().sendThongBao(player, "Chết rồi ba...");
                                    break;
                                case 1:
                                    if (player.getSession().player.nPoint.power >= 80000000000L && player.playerTask.taskMain.id > 21) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 109, -1, 295);
                                    } //                                    break;
                                    else {
                                        Service.gI().sendThongBaoOK(player, "Làm Nhiệm Vụ Và Đạt 80 Tỷ Sức Mạnh");
                                    }
                                    break;
                                case 2:
                                    if (player.playerTask.taskMain.id >= 17) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 68, -1, 90);
                                    } else {
                                        Service.gI().sendThongBaoOK(player, "Làm Nhiệm Vụ Đi");
                                    }
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_FIND_RAMBO) {
                            switch (select) {
                                case 0:
                                    Boss boss = BossManager.gI().getBossById(BossID.RAMBO);
                                    if (boss != null && !boss.isDie()) {
                                        if (player.inventory.gold >= COST_FIND_BOSS) {
                                            Zone z = MapService.gI().getMapCanJoin(player, boss.zone.map.mapId, boss.zone.zoneId);
                                            if (z != null && z.getNumOfPlayers() < z.maxPlayer) {
                                                player.inventory.gold -= COST_FIND_BOSS;
                                                ChangeMapService.gI().changeMap(player, boss.zone, boss.location.x, boss.location.y);
                                                Service.gI().sendMoney(player);
                                            } else {
                                                Service.gI().sendThongBao(player, "Khu vực đang full.");
                                            }
                                        } else {
                                            Service.gI().sendThongBao(player, "Không đủ vàng, còn thiếu "
                                                    + Util.numberToMoney(COST_FIND_BOSS - player.inventory.gold) + " vàng");
                                        }
                                        break;
                                    }
                                    Service.gI().sendThongBao(player, "Chết rồi ba...");
                                    break;
                                case 1:
                                    if (player.getSession().player.nPoint.power >= 80000000000L && player.playerTask.taskMain.id > 21) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 109, -1, 295);
                                    } //                                    break;
                                    else {
                                        Service.gI().sendThongBaoOK(player, "Làm Nhiệm Vụ Và Đạt 80 Tỷ Sức Mạnh");
                                    }
                                    break;
                                case 2:
                                    if (player.playerTask.taskMain.id >= 17) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 68, -1, 90);
                                    } else {
                                        Service.gI().sendThongBaoOK(player, "Làm Nhiệm Vụ Đi");
                                    }
                                    break;
                            }
                        }
                    }
                    if (this.mapId == 68) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 19, -1, 1100);
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc miNuong(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    createOtherMenu(player, ConstNpc.MENU_JOIN_GIAI_CUU_MI_NUONG,
                            "Ta đang bị kẻ xấu lợi dụng kiểm soát bản thân\n"
                            + "Các chàng trai hãy cùng nhau nhanh chóng tập hợp lên đường giải cứu ta",
                            "Đang Update", "Từ chối");
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                int nPlSameClan = 0;
                for (Player pl : player.zone.getPlayers()) {
                    if (!pl.equals(player) && pl.clan != null
                            && pl.clan.equals(player.clan) && pl.location.x >= 1285
                            && pl.location.x <= 1645) {
                        nPlSameClan++;
                    }
                }
                if (canOpenNpc(player)) {
                    switch (player.iDMark.getIndexMenu()) {
                        case ConstNpc.MENU_JOIN_GIAI_CUU_MI_NUONG:
//                            if (select == 0) {
//                                if (player.clan == null) {
//                                    Service.gI().sendThongBao(player, "Yêu cầu gia nhập bang hội");
//                                    break;
//                                }
//                                if (player.clan.giaiCuuMiNuong != null) {
//                                    ChangeMapService.gI().changeMapInYard(player, 180, player.clan.giaiCuuMiNuong.id, 60);
//                                    break;
//                                } else if (nPlSameClan < 0) {
//                                    Service.gI().sendThongBao(player, "Yêu cầu tham gia cùng 2 đồng đội");
//                                    break;
//                                } else if (player.clanMember.getNumDateFromJoinTimeToToday() < 0) {
//                                    Service.gI().sendThongBao(player, "Yêu cầu tham gia bang hội trên 1 ngày");
//                                    break;
//                                } else if (player.clan.haveGoneGiaiCuuMiNuong) {
//                                    Service.gI().sendThongBaoOK(player, "Bang hội của ngươi đã tham gia vào lúc " + TimeUtil.formatTime(player.clan.lastTimeOpenGiaiCuuMiNuong, "HH:mm:ss") + "\nVui lòng tham gia vào ngày mai");
//                                    break;
//                                } else {
//                                    GiaiCuuMiNuongService.gI().openGiaiCuuMiNuong(player);
//                                }
//                            } else if (select == 1) {
//                                NpcService.gI().createTutorial(player, this.avartar, ConstNpc.HUONG_DAN_GIAI_CUU_MI_NUONG);
//                            }
                            break;
                    }
                }
            }
        };
    }

    public static Npc goHanZom(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 185) {
                        createOtherMenu(player, ConstNpc.BASE_MENU,
                                "|7|Chức Năng Đang Update!!\n",
                                "Quay Về");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 185) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 5, -1, 715);
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc hangNga(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5) {
                        createOtherMenu(player, ConstNpc.BASE_MENU,
                                "|7|Bạn Muốn Đi Đâu\n",
                                "Đến Cung Trăng", "Đóng");
                    } else if (this.mapId == 174) {
                        createOtherMenu(player, ConstNpc.BASE_MENU,
                                "|7|SỰ KIỆN TRUNG THU TẠI NGỌC RỒNG KURROKO\n"
                                + "|3|Các nguyên liệu cần thiết để làm bánh Trung Thu\n\n"
                                + "|2|- Nhân Gà Quay: 99 bột mì, 50 đậu xanh, 10 con gà quay\n"
                                + "|5|Tăng 5%SĐ,10% HP-KI(trong 10 phút)\n\n"
                                + "|2|- Nhân Trứng Muối: 99 bột mì, 50 đậu xanh, 10 trứng vịt muối\n"
                                + "|5|1 Trứng: Tăng 5% SĐ Đệ, 5% HP-KI(trong 10 phút)\n"
                                + "|5|2 Trứng: Tăng 5% SĐCM, 5%CM(trong 10 phút)\n\n"
                                + "|2|- Nhân Thập Cẩm: 299 bột mì, 99 đậu xanh, 30 con gà quay, 30 trứng vịt muối\n"
                                + "|5|Tăng 5% SĐ, 5% HP-KI, 5% SĐCM, 5% CM (trong 30 phút)\n\n"
                                + "|7|Phụ thu phí làm bánh là 1 tỷ vàng Và x99 Cà Rốt",
                                "Hướng Dẫn", "Làm Bánh", "Đổi Điểm", "Xem Top", "Quay Về", "Đóng");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    Item botMi;
                    Item dauXanh;
                    Item gaQuay;
                    Item trungVit;
                    Item caRot;
                    if (this.mapId == 5) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 174, -1, 182);
                                    break;
                            }
                        }
                    } else if (this.mapId == 174) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    createOtherMenu(player, -1,
                                            "|7|SỰ KIỆN TRUNG THU TẠI NGỌC RỒNG KURROKO\n"
                                            + "|2|Cách thức tìm nguyên liệu làm bánh Trung Thu\n"
                                            + "|4|- Gà quay nguyên con: Đánh các quái bay\n"
                                            + "- Trứng vịt muối: Đánh các quái dưới đất\n"
                                            + "- Đậu xanh, Bột mì: Đánh quái sên bên Tương Lai\n"
                                            + "Cà Rốt: Săn Thỏ Đại Ca\n\n"
                                            + "|5|Làm bánh để nhận điểm và đổi các phần quà hấp dẫn\n"
                                            + "|-1|- Bánh Gà Quay: Nhận 2 điểm sự kiện\n"
                                            + "|-1|- Bánh Trứng Muối: Nhận 2 điểm sự kiện\n"
                                            + "|-1|- Bánh Thập Cẩm: Nhận 5 điểm sự kiện\n\n"
                                            + "|7| - Quy đổi tiền 1.000đ nhận thêm  1 Điểm sự kiện",
                                            "Đóng");
                                    break;
                                case 1:
                                    createOtherMenu(player, 111,
                                            "|7|LÀM BÁNH TRUNG THU\n"
                                            + "|3|Các nguyên liệu cần thiết để làm bánh Trung Thu\n\n"
                                            + "|2|- Nhân Gà Quay: 99 bột mì, 50 đậu xanh, 10 con gà quay\n"
                                            + "|2|- Nhân Trứng Muối(1 Trứng): 99 bột mì, 50 đậu xanh, 10 trứng vịt muối\n"
                                            + "|2|- Nhân Trứng Muối(2 Trứng): 99 bột mì, 50 đậu xanh, 20 trứng vịt muối\n"
                                            + "|2|- Nhân Thập Cẩm: 299 bột mì, 99 đậu xanh, 30 con gà quay, 30 trứng vịt muối\n\n"
                                            + "|7|Phụ thu phí làm bánh là 1 tỷ vàng và x99 Thỏ Màu Các Loại",
                                            "Bánh Trung Thu Nhân Gà Quay", "Bánh Trung Thu Nhân Trứng Muối (1 Trứng)", "Bánh Trung Thu Nhân Trứng Muối (2 Trứng)", "Bánh Trung Thu Nhân Thập Cẩm", "Đóng");
                                    break;
                                case 2:
                                    createOtherMenu(player, 112,
                                            "|7|TÍCH ĐIỂM SỰ KIỆN TRUNG THU\n"
                                            + "|2|Mốc 1000 điểm\n"
                                            + "|4|15 hộp quà Trung Thu Ngẫu Nhiên (Cải Trang,VPDL,v.v)\n\n"
                                            + "|2|Mốc 500 điểm\n"
                                            + "|4|10 hộp quà Trung Thu Ngẫu Nhiên(Cải Trang,VPDL,v.v)\n\n"
                                            + "|2|Mốc 300 điểm\n"
                                            + "|4|5 hộp quà Trung Thu Ngẫu Nhiên (Cải Trang,VPDL,v.v)\n\n"
                                            + "|2|Mốc 150 điểm\n"
                                            + "|4|3 hộp quà Trung Thu Ngẫu Nhiên (Cải Trang,VPDL,v.v)\n\n"
                                            + "|2|Mốc 50 điểm\n"
                                            + "|4|1 hộp quà Trung Thu Ngẫu Nhiên (Cải Trang,VPDL,v.v)\n\n",
                                            "1000 điểm", "500 điểm", "300 điẻm", "150 điểm", "50 điểm", "Đóng");
                                    break;
                                case 3:
                                    Service.gI().showListTop(player, Manager.topTrungThu);
                                    break;
                                case 4:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 5, -1, 715);
                                    break;
                            }

                        } else if (player.iDMark.getIndexMenu() == 111) {
                            switch (select) {
                                case 0:
                                    botMi = InventoryServiceNew.gI().findItemBag(player, 888);
                                    dauXanh = InventoryServiceNew.gI().findItemBag(player, 889);
                                    gaQuay = InventoryServiceNew.gI().findItemBag(player, 887);
                                    caRot = InventoryServiceNew.gI().findItemBag(player, 462);
                                    if (botMi != null && botMi.quantity < 99) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (99 - botMi.quantity) + " Bột Mì.");
                                    } else if (botMi == null) {
                                        this.npcChat(player, "Bạn không có Bột Mì nào.");
                                    } else if (dauXanh != null && dauXanh.quantity < 50) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (50 - dauXanh.quantity) + " Đậu Xanh.");
                                    } else if (dauXanh == null) {
                                        this.npcChat(player, "Bạn không có Đậu Xanh nào.");
                                    } else if (gaQuay != null && gaQuay.quantity < 10) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (10 - gaQuay.quantity) + " Gà Quay.");
                                    } else if (gaQuay == null) {
                                        this.npcChat(player, "Bạn không có Gà Quay nào.");
                                    } else if (caRot != null && caRot.quantity < 99) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (99 - caRot.quantity) + " Cà Rốt.");
                                    } else if (caRot == null) {
                                        this.npcChat(player, "Bạn không có Cà Rốt nào.");
                                    } else {
                                        new Thread(() -> {
                                            int timeWait = 30;
                                            while (timeWait > 0) {
                                                try {
                                                    timeWait--;
                                                    this.npcChat(player, "Đang làm bánh\n|7|Thời gian còn lại: " + timeWait + ".");
                                                    Thread.sleep(1000);
                                                } catch (InterruptedException ex) {
                                                }
                                            }
                                            Item banhGaQuay = ItemService.gI().createNewItem((short) 465);
                                            botMi.quantity -= 99;
                                            dauXanh.quantity -= 50;
                                            gaQuay.quantity -= 10;
                                            caRot.quantity -= 99;
                                            player.inventory.gold -= 1_000_000_000;
                                            player.point_trungthu += 2;
                                            Service.gI().sendMoney(player);
                                            InventoryServiceNew.gI().addItemBag(player, banhGaQuay);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            this.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Đã làm bánh xong xong\n|7|Bạn đã nhận được " + banhGaQuay.template.name, "Nhận Ngay");
                                        }).start();
                                    }
                                    break;
                                case 1:
                                    botMi = InventoryServiceNew.gI().findItemBag(player, 888);
                                    dauXanh = InventoryServiceNew.gI().findItemBag(player, 889);
                                    trungVit = InventoryServiceNew.gI().findItemBag(player, 886);
                                    caRot = InventoryServiceNew.gI().findItemBag(player, 462);
                                    if (botMi != null && botMi.quantity < 99) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (99 - botMi.quantity) + " Bột Mì.");
                                    } else if (botMi == null) {
                                        this.npcChat(player, "Bạn không có Bột Mì nào.");
                                    } else if (dauXanh != null && dauXanh.quantity < 50) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (50 - dauXanh.quantity) + " Đậu Xanh.");
                                    } else if (dauXanh == null) {
                                        this.npcChat(player, "Bạn không có Đậu Xanh nào.");
                                    } else if (trungVit != null && trungVit.quantity < 10) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (10 - trungVit.quantity) + " Trứng Vịt.");
                                    } else if (trungVit == null) {
                                        this.npcChat(player, "Bạn không có Trứng Vịt nào.");
                                    } else if (caRot != null && caRot.quantity < 99) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (99 - caRot.quantity) + " Cà Rốt.");
                                    } else if (caRot == null) {
                                        this.npcChat(player, "Bạn không có Cà Rốt nào.");
                                    } else {
                                        new Thread(() -> {
                                            int timeWait = 40;
                                            while (timeWait > 0) {
                                                try {
                                                    timeWait--;
                                                    this.npcChat(player, "Đang làm bánh\n|7|Thời gian còn lại: " + timeWait + ".");
                                                    Thread.sleep(1000);
                                                } catch (InterruptedException ex) {
                                                }
                                            }
                                            Item banhVitMuoi1Trung = ItemService.gI().createNewItem((short) 465);
                                            botMi.quantity -= 99;
                                            dauXanh.quantity -= 50;
                                            trungVit.quantity -= 10;
                                            caRot.quantity -= 99;
                                            player.inventory.gold -= 1_000_000_000;
                                            player.point_trungthu += 2;
                                            Service.gI().sendMoney(player);
                                            InventoryServiceNew.gI().addItemBag(player, banhVitMuoi1Trung);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            this.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Đã làm bánh xong xong\n|7|Bạn đã nhận được " + banhVitMuoi1Trung.template.name, "Nhận Ngay");
                                        }).start();
                                    }
                                    break;
                                case 2:
                                    botMi = InventoryServiceNew.gI().findItemBag(player, 888);
                                    dauXanh = InventoryServiceNew.gI().findItemBag(player, 889);
                                    trungVit = InventoryServiceNew.gI().findItemBag(player, 886);
                                    caRot = InventoryServiceNew.gI().findItemBag(player, 462);
                                    if (botMi != null && botMi.quantity < 99) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (99 - botMi.quantity) + " Bột Mì.");
                                    } else if (botMi == null) {
                                        this.npcChat(player, "Bạn không có Bột Mì nào.");
                                    } else if (dauXanh != null && dauXanh.quantity < 50) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (50 - dauXanh.quantity) + " Đậu Xanh.");
                                    } else if (dauXanh == null) {
                                        this.npcChat(player, "Bạn không có Đậu Xanh nào.");
                                    } else if (trungVit != null && trungVit.quantity < 20) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (20 - trungVit.quantity) + " Trứng Vịt.");
                                    } else if (trungVit == null) {
                                        this.npcChat(player, "Bạn không có Trứng Vịt nào.");
                                    } else if (caRot != null && caRot.quantity < 99) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (99 - caRot.quantity) + " Cà Rốt.");
                                    } else if (caRot == null) {
                                        this.npcChat(player, "Bạn không có Cà Rốt nào.");
                                    } else {
                                        new Thread(() -> {
                                            int timeWait = 50;
                                            while (timeWait > 0) {
                                                try {
                                                    timeWait--;
                                                    this.npcChat(player, "Đang làm bánh\n|7|Thời gian còn lại: " + timeWait + ".");
                                                    Thread.sleep(1000);
                                                } catch (InterruptedException ex) {
                                                }
                                            }
                                            Item banhVitMuoi2Trung = ItemService.gI().createNewItem((short) 466);
                                            botMi.quantity -= 99;
                                            dauXanh.quantity -= 50;
                                            trungVit.quantity -= 20;
                                            caRot.quantity -= 99;
                                            player.inventory.gold -= 1_000_000_000;
                                            player.point_trungthu += 2;
                                            Service.gI().sendMoney(player);
                                            InventoryServiceNew.gI().addItemBag(player, banhVitMuoi2Trung);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            this.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Đã làm bánh xong xong\n|7|Bạn đã nhận được " + banhVitMuoi2Trung.template.name, "Nhận Ngay");
                                        }).start();
                                    }
                                    break;
                                case 3:
                                    botMi = InventoryServiceNew.gI().findItemBag(player, 888);
                                    dauXanh = InventoryServiceNew.gI().findItemBag(player, 889);
                                    trungVit = InventoryServiceNew.gI().findItemBag(player, 887);
                                    gaQuay = InventoryServiceNew.gI().findItemBag(player, 886);
                                    caRot = InventoryServiceNew.gI().findItemBag(player, 462);
                                    if (botMi != null && botMi.quantity < 299) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (299 - botMi.quantity) + " Bột Mì.");
                                    } else if (botMi == null) {
                                        this.npcChat(player, "Bạn không có Bột Mì nào.");
                                    } else if (dauXanh != null && dauXanh.quantity < 99) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (99 - dauXanh.quantity) + " Đậu Xanh.");
                                    } else if (dauXanh == null) {
                                        this.npcChat(player, "Bạn không có Đậu Xanh nào.");
                                    } else if (gaQuay != null && gaQuay.quantity < 30) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (30 - gaQuay.quantity) + " Gà Quay.");
                                    } else if (gaQuay == null) {
                                        this.npcChat(player, "Bạn không có Gà Quay nào.");
                                    } else if (trungVit != null && trungVit.quantity < 30) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (30 - trungVit.quantity) + " Trứng Vịt.");
                                    } else if (trungVit == null) {
                                        this.npcChat(player, "Bạn không có Trứng Vịt nào.");
                                    } else if (caRot != null && caRot.quantity < 99) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (99 - caRot.quantity) + " Cà Rốt.");
                                    } else if (caRot == null) {
                                        this.npcChat(player, "Bạn không có Cà Rốt nào.");
                                    } else {
                                        new Thread(() -> {
                                            int timeWait = 60;
                                            while (timeWait > 0) {
                                                try {
                                                    timeWait--;
                                                    this.npcChat(player, "Đang làm bánh\n|7|Thời gian còn lại: " + timeWait + ".");
                                                    Thread.sleep(1000);
                                                } catch (InterruptedException ex) {
                                                }
                                            }
                                            Item banhThapCam = ItemService.gI().createNewItem((short) 891);
                                            botMi.quantity -= 299;
                                            dauXanh.quantity -= 99;
                                            gaQuay.quantity -= 30;
                                            trungVit.quantity -= 30;
                                            caRot.quantity -= 99;
                                            player.inventory.gold -= 1_000_000_000;
                                            player.point_trungthu += 5;
                                            Service.gI().sendMoney(player);
                                            InventoryServiceNew.gI().addItemBag(player, banhThapCam);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            this.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Đã làm bánh xong xong\n|7|Bạn đã nhận được " + banhThapCam.template.name, "Nhận Ngay");
                                        }).start();
                                    }
                                    break;

                            }
                        } else if (player.iDMark.getIndexMenu() == 112) {
                            Item hopTrungThu;
                            Item conLan;
                            Item rongXanh;
                            switch (select) {
                                case 0:
                                    if (player.point_trungthu < 1000) {
                                        Service.gI().sendThongBao(player, "Bạn Không Đủ Điểm Bạn Còn Thiếu " + (1000 - player.point_trungthu) + " Nữa");
                                        return;
                                    }
                                    if (player.point_trungthu >= 1000) {
                                        hopTrungThu = ItemService.gI().createNewItem((short) 1512);
                                        conLan = ItemService.gI().createNewItem((short) 1236);
                                        rongXanh = ItemService.gI().createNewItem((short) 1237);
                                        hopTrungThu.quantity += 14;
                                        player.point_trungthu -= 999;
                                        hopTrungThu.itemOptions.add(new Item.ItemOption(30, 0));
                                        conLan.itemOptions.add(new Item.ItemOption(50, 20));
                                        conLan.itemOptions.add(new Item.ItemOption(77, 20));
                                        conLan.itemOptions.add(new Item.ItemOption(103, 20));
                                        if (Util.isTrue(97, 100)) {
                                            conLan.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 7)));
                                        }
                                        rongXanh.itemOptions.add(new Item.ItemOption(50, 20));
                                        rongXanh.itemOptions.add(new Item.ItemOption(77, 20));
                                        rongXanh.itemOptions.add(new Item.ItemOption(103, 20));
                                        if (Util.isTrue(97, 100)) {
                                            rongXanh.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 7)));
                                        }
                                        InventoryServiceNew.gI().addItemBag(player, hopTrungThu);
                                        InventoryServiceNew.gI().addItemBag(player, conLan);
                                        InventoryServiceNew.gI().addItemBag(player, rongXanh);
                                        Service.gI().sendThongBao(player, "Bạn Đã Nhận Được 15 " + hopTrungThu.template.name + " " + conLan.template.name + " Và " + rongXanh.template.name);
                                    }
                                    break;
                                case 1:
                                    if (player.point_trungthu < 500) {
                                        Service.gI().sendThongBao(player, "Bạn Không Đủ Điểm Bạn Còn Thiếu " + (500 - player.point_trungthu) + " Nữa");
                                        return;
                                    }
                                    if (player.point_trungthu >= 500) {
                                        hopTrungThu = ItemService.gI().createNewItem((short) 1512);
                                        conLan = ItemService.gI().createNewItem((short) 1236);
                                        rongXanh = ItemService.gI().createNewItem((short) 1237);
                                        hopTrungThu.quantity += 9;
                                        player.point_trungthu -= 499;
                                        conLan.itemOptions.add(new Item.ItemOption(50, 18));
                                        conLan.itemOptions.add(new Item.ItemOption(77, 18));
                                        conLan.itemOptions.add(new Item.ItemOption(103, 18));
                                        if (Util.isTrue(97, 100)) {
                                            conLan.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 7)));
                                        }
                                        rongXanh.itemOptions.add(new Item.ItemOption(50, 18));
                                        rongXanh.itemOptions.add(new Item.ItemOption(77, 18));
                                        rongXanh.itemOptions.add(new Item.ItemOption(103, 18));
                                        if (Util.isTrue(97, 100)) {
                                            rongXanh.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 7)));
                                        }
                                        hopTrungThu.itemOptions.add(new Item.ItemOption(30, 0));
                                        InventoryServiceNew.gI().addItemBag(player, hopTrungThu);
                                        InventoryServiceNew.gI().addItemBag(player, hopTrungThu);
                                        InventoryServiceNew.gI().addItemBag(player, conLan);
                                        InventoryServiceNew.gI().addItemBag(player, rongXanh);
                                        Service.gI().sendThongBao(player, "Bạn Đã Nhận Được 10 " + hopTrungThu.template.name + " " + conLan.template.name + " Và " + rongXanh.template.name);
                                    }
                                    break;
                                case 2:
                                    if (player.point_trungthu < 300) {
                                        Service.gI().sendThongBao(player, "Bạn Không Đủ Điểm Bạn Còn Thiếu " + (300 - player.point_trungthu) + " Nữa");
                                        return;
                                    }
                                    if (player.point_trungthu >= 300) {
                                        hopTrungThu = ItemService.gI().createNewItem((short) 1512);
                                        conLan = ItemService.gI().createNewItem((short) 1236);
                                        rongXanh = ItemService.gI().createNewItem((short) 1237);
                                        hopTrungThu.quantity += 4;
                                        player.point_trungthu -= 299;
                                        conLan.itemOptions.add(new Item.ItemOption(50, 15));
                                        conLan.itemOptions.add(new Item.ItemOption(77, 15));
                                        conLan.itemOptions.add(new Item.ItemOption(103, 15));
                                        if (Util.isTrue(97, 100)) {
                                            conLan.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 7)));
                                        }
                                        rongXanh.itemOptions.add(new Item.ItemOption(50, 15));
                                        rongXanh.itemOptions.add(new Item.ItemOption(77, 15));
                                        rongXanh.itemOptions.add(new Item.ItemOption(103, 15));
                                        if (Util.isTrue(97, 100)) {
                                            rongXanh.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 7)));
                                        }
                                        hopTrungThu.itemOptions.add(new Item.ItemOption(30, 0));
                                        InventoryServiceNew.gI().addItemBag(player, hopTrungThu);
                                        InventoryServiceNew.gI().addItemBag(player, conLan);
                                        InventoryServiceNew.gI().addItemBag(player, rongXanh);
                                        Service.gI().sendThongBao(player, "Bạn Đã Nhận Được 5 " + hopTrungThu.template.name + " " + conLan.template.name + " Và " + rongXanh.template.name);
                                    }
                                    break;
                                case 3:
                                    if (player.point_trungthu < 150) {
                                        Service.gI().sendThongBao(player, "Bạn Không Đủ Điểm Bạn Còn Thiếu " + (150 - player.point_trungthu) + " Nữa");
                                        return;
                                    }
                                    if (player.point_trungthu >= 150) {
                                        hopTrungThu = ItemService.gI().createNewItem((short) 1512);
                                        conLan = ItemService.gI().createNewItem((short) 1236);
                                        rongXanh = ItemService.gI().createNewItem((short) 1237);
                                        hopTrungThu.quantity += 2;
                                        player.point_trungthu -= 149;
                                        conLan.itemOptions.add(new Item.ItemOption(50, 13));
                                        conLan.itemOptions.add(new Item.ItemOption(77, 13));
                                        conLan.itemOptions.add(new Item.ItemOption(103, 13));
                                        if (Util.isTrue(97, 100)) {
                                            conLan.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 5)));
                                        }
                                        rongXanh.itemOptions.add(new Item.ItemOption(50, 13));
                                        rongXanh.itemOptions.add(new Item.ItemOption(77, 13));
                                        rongXanh.itemOptions.add(new Item.ItemOption(103, 13));
                                        if (Util.isTrue(97, 100)) {
                                            rongXanh.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 5)));
                                        }
                                        hopTrungThu.itemOptions.add(new Item.ItemOption(30, 0));
                                        InventoryServiceNew.gI().addItemBag(player, hopTrungThu);
                                        InventoryServiceNew.gI().addItemBag(player, conLan);
                                        InventoryServiceNew.gI().addItemBag(player, rongXanh);
                                        Service.gI().sendThongBao(player, "Bạn Đã Nhận Được 3 " + hopTrungThu.template.name + " " + conLan.template.name + " Và " + rongXanh.template.name);
                                    }
                                    break;
                                case 4:
                                    if (player.point_trungthu < 50) {
                                        Service.gI().sendThongBao(player, "Bạn Không Đủ Điểm Bạn Còn Thiếu " + (50 - player.point_trungthu) + " Nữa");
                                        return;
                                    }
                                    if (player.point_trungthu >= 50) {
                                        hopTrungThu = ItemService.gI().createNewItem((short) 1512);
                                        conLan = ItemService.gI().createNewItem((short) 1236);
                                        rongXanh = ItemService.gI().createNewItem((short) 1237);
                                        hopTrungThu.itemOptions.add(new Item.ItemOption(30, 0));
                                        player.point_trungthu -= 49;
                                        conLan.itemOptions.add(new Item.ItemOption(50, 10));
                                        conLan.itemOptions.add(new Item.ItemOption(77, 10));
                                        conLan.itemOptions.add(new Item.ItemOption(103, 10));
                                        if (Util.isTrue(97, 100)) {
                                            conLan.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 3)));
                                        }
                                        rongXanh.itemOptions.add(new Item.ItemOption(50, 10));
                                        rongXanh.itemOptions.add(new Item.ItemOption(77, 10));
                                        rongXanh.itemOptions.add(new Item.ItemOption(103, 10));
                                        if (Util.isTrue(97, 100)) {
                                            rongXanh.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 3)));
                                        }
                                        InventoryServiceNew.gI().addItemBag(player, hopTrungThu);
                                        InventoryServiceNew.gI().addItemBag(player, conLan);
                                        InventoryServiceNew.gI().addItemBag(player, rongXanh);
                                        Service.gI().sendThongBao(player, "Bạn Đã Nhận Được 1 " + hopTrungThu.template.name + " " + conLan.template.name + " Và " + rongXanh.template.name);
                                    }
                                    break;
                            }

                        }
                    }
                }
            }
        };
    }

    private static Npc gapThu(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5) {
                        this.createOtherMenu(player, 1234, "|7|- •⊹٭Ngọc Rồng One Puch Man٭⊹• -\n|2|MÁY GẮP THÚ NGỌC RỒNG ONE PUCH MAN, PET\nGẮP THÚ : 1 XU ONE PUCH/1 LƯỢT\n"
                                + "CHỌN CÁC TÙY CHỌN BÊN DƯỚI ĐỂ XEM THÊM THÔNG TIN CHI TIẾT\n|7|MỌI ITEM SẼ ĐƯỢC ĐẨY VÀO RƯƠNG PHỤ NẾU HÀNH TRANG ĐẦY!\n",
                                "Gắp Thú", "Bảng Xếp Hạng", "Rương Đồ", "Đóng");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5) {
                        if (player.iDMark.getIndexMenu() == 1234) {
                            switch (select) {
                                case 0:
                                    this.createOtherMenu(player, 12345, "|7|- •⊹٭Ngọc Rồng One Puch Man٭⊹• -\nGẮP THÚ : 5-10% CHỈ SỐ\n|3|GẮP X1 : GẮP THỦ CÔNG\nGẮP X10 : AUTO X10 LẦN GẮP\nGẮP X100 : AUTO X100 LẦN GẮP\n" + "|7|LƯU Ý : MỌI CHỈ SỐ ĐỀU RANDOM KHÔNG CÓ OPTION NHẤT ĐỊNH\nNẾU MUỐN NGƯNG AUTO GẤP CHỈ CẦN THOÁT GAME VÀ VÀO LẠI!",
                                            "Gắp x1", "Gắp x10", "Gắp x100", "Rương Đồ");
                                    break;
                                case 1:
                                    this.createOtherMenu(player, 123455, "|7|BẢNG XẾP HẠNG GẮP THÚ\n- •⊹٭Ngọc Rồng One Puch Man٭⊹• -",
                                            "Xem Top", "Nhận Quà Top");
                                    break;
                                case 2:
                                    this.createOtherMenu(player, ConstNpc.RUONG_PHU,
                                            "|1|Tình yêu như một dây đàn\n"
                                            + "Tình vừa được thì đàn đứt dây\n"
                                            + "Đứt dây này anh thay dây khác\n"
                                            + "Mất em rồi anh biết thay ai?",
                                            "Rương Phụ\n(" + (player.inventory.itemsBoxCrackBall.size()
                                            - InventoryServiceNew.gI().getCountEmptyListItem(player.inventory.itemsBoxCrackBall))
                                            + "/200)",
                                            "Xóa Hết\nRương Phụ", "Đóng");
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == 12345) {
                            switch (select) {
                                case 0:
                                    Item xuthuong = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259);
                                    if (xuthuong == null) {
                                        this.createOtherMenu(player, 12345, "|2|HẾT TIỀN!\n|7|CẦN TỐI THIỂU 1 XU GẮP THÚ, HÃY QUAY LẠI SAU!",
                                                "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                        break;
                                    }
                                    if (InventoryServiceNew.gI().getCountEmptyBag(player) == 0) {
                                        Service.gI().sendThongBao(player, "Hết chỗ trống rồi");
                                        return;
                                    }
                                    InventoryServiceNew.gI().subQuantityItem(player.inventory.itemsBag, xuthuong, 1);
                                    InventoryServiceNew.gI().sendItemBags(player);
                                    short[] bkt = {1275, 1276, 1277, 1278, 1279, 1280, 1281, 1282, 1283, 1284, 1285, 1286, 1287, 1288, 1289, 1290, 1292};
                                    Item gapt = Util.petviprandom(bkt[Util.nextInt(bkt.length)]);
                                    if (Util.isTrue(10, 100)) {
                                        player.point_gapthu += 1;
                                        InventoryServiceNew.gI().addItemBag(player, gapt);
                                        InventoryServiceNew.gI().sendItemBags(player);
                                        this.createOtherMenu(player, 12345, "|2|Bạn vừa gắp được : " + gapt.template.name + "\nSố xu còn : " + xuthuong.quantity + "\n|7|Chiến tiếp ngay!",
                                                "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                    } else {
                                        this.createOtherMenu(player, 12345, "|6|Gắp hụt rồi, bạn bỏ cuộc sao?" + "\nSố xu còn : " + xuthuong.quantity + "\n|7|Chiến tiếp ngay!",
                                                "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                    }
                                    break;
                                case 1:
                                    if (InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259) == null) {
                                        this.createOtherMenu(player, 12345, "|2|HẾT TIỀN!\n|7|CẦN TỐI THIỂU 1 XU GẮP THÚ, HÃY QUAY LẠI SAU!",
                                                "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                        break;
                                    }
                                    try {
                                        Service.gI().sendThongBao(player, "Tiến hành auto gắp x10 lần");
                                        int timex10 = 10;
                                        int count = 0;
                                        while (timex10 > 0) {
                                            timex10--;
                                            count++;
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259), 1);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            if (InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259) == null) {
                                                this.createOtherMenu(player, 12345, "|7|HẾT XU!\nSỐ LƯỢT ĐÃ GẮP : " + count,
                                                        "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                break;
                                            }
                                            if (1 + player.inventory.itemsBoxCrackBall.size() > 200) {
                                                this.createOtherMenu(player, 12345, "|7|DỪNG AUTO GẮP, RƯƠNG PHỤ ĐÃ ĐẦY!\n" + "|2|TỔNG LƯỢT GẮP : " + count + " LƯỢT" + "\n|7|VUI LÒNG LÀM TRỐNG RƯƠNG PHỤ!",
                                                        "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                break;
                                            }
                                            Thread.sleep(100);
                                            short[] bktt = {1351, 1352, 1353, 1354, 1355, 1356, 1357, 1358, 1359, 1360, 1608, 1609, 1610, 1561, 1397};
                                            Item gapx10 = Util.petviprandom(bktt[Util.nextInt(bktt.length)]);
                                            if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                                if (Util.isTrue(10, 100)) {
                                                    player.point_gapthu += 1;
                                                    InventoryServiceNew.gI().addItemBag(player, gapx10);
                                                    InventoryServiceNew.gI().sendItemBags(player);
                                                    this.createOtherMenu(player, 12345, "|7|ĐANG TIẾN HÀNH GẮP AUTO X10\nSỐ LƯỢT CÒN : " + timex10 + " LƯỢT\n" + "|2|Đã gắp được : " + gapx10.template.name + "\nSố xu còn : " + InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259).quantity + "\n|7|TỔNG ĐIỂM : " + player.point_gapthu + "\nNẾU HÀNH TRANG ĐẦY, ITEM SẼ ĐƯỢC THÊM VÀO RƯƠNG PHỤ",
                                                            "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                } else {
                                                    this.createOtherMenu(player, 12345, "|7|ĐANG TIẾN HÀNH GẮP AUTO X10\nSỐ LƯỢT CÒN : " + timex10 + " LƯỢT\n" + "|2|Gắp hụt rồi!" + "\nSố xu còn : " + InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259).quantity + "\n|7|TỔNG ĐIỂM : " + player.point_gapthu + "\nNẾU HÀNH TRANG ĐẦY, ITEM SẼ ĐƯỢC THÊM VÀO RƯƠNG PHỤ",
                                                            "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                }
                                            }
                                            if (InventoryServiceNew.gI().getCountEmptyBag(player) == 0) {
                                                if (Util.isTrue(2, 100)) {
                                                    player.inventory.itemsBoxCrackBall.add(ItemService.gI().createNewItem((short) 1259));
                                                }
                                                if (Util.isTrue(10, 100)) {
                                                    player.point_gapthu += 1;
                                                    player.inventory.itemsBoxCrackBall.add(gapx10);
                                                    this.createOtherMenu(player, 12345, "|7|HÀNH TRANG ĐÃ ĐẦY\nĐANG TIẾN HÀNH GẮP AUTO X10 VÀO RƯƠNG PHỤ\nSỐ LƯỢT CÒN : " + timex10 + " LƯỢT\n" + "|2|Đã gắp được : " + gapx10.template.name + "\nSố xu còn : " + InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259).quantity + "\n|7|TỔNG ĐIỂM : " + player.point_gapthu,
                                                            "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                } else {
                                                    this.createOtherMenu(player, 12345, "|7|HÀNH TRANG ĐÃ ĐẦY\nĐANG TIẾN HÀNH GẮP AUTO X10 VÀO RƯƠNG PHỤ\nSỐ LƯỢT CÒN : " + timex10 + " LƯỢT\n" + "|2|Gắp hụt rồi!" + "\nSố xu còn : " + InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259).quantity + "\n|7|TỔNG ĐIỂM : " + player.point_gapthu,
                                                            "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                }
                                            }
                                        }
                                    } catch (Exception e) {
                                    }
                                    break;
                                case 2:
                                    if (InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259) == null) {
                                        this.createOtherMenu(player, 12345, "|2|HẾT TIỀN!\n|7|CẦN TỐI THIỂU 1 XU GẮP THÚ, HÃY QUAY LẠI SAU!",
                                                "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                        break;
                                    }
                                    try {
                                        Service.gI().sendThongBao(player, "Tiến hành auto gắp x10 lần");
                                        int timex100 = 100;
                                        int count = 0;
                                        while (timex100 > 0) {
                                            timex100--;
                                            count++;
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259), 1);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            if (InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259) == null) {
                                                this.createOtherMenu(player, 12345, "|7|HẾT XU!\nSỐ LƯỢT ĐÃ GẮP : " + count,
                                                        "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                break;
                                            }
                                            if (1 + player.inventory.itemsBoxCrackBall.size() > 200) {
                                                this.createOtherMenu(player, 12345, "|7|DỪNG AUTO GẮP, RƯƠNG PHỤ ĐÃ ĐẦY!\n" + "|2|TỔNG LƯỢT GẮP : " + count + " LƯỢT" + "\n|7|VUI LÒNG LÀM TRỐNG RƯƠNG PHỤ!",
                                                        "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                break;
                                            }
                                            Thread.sleep(100);
                                            short[] bkttt = {1275, 1276, 1277, 1278, 1279, 1280, 1281, 1282, 1283, 1284, 1285, 1286, 1287, 1288, 1289, 1290, 1292};
                                            Item gapx100 = Util.petviprandom(bkttt[Util.nextInt(bkttt.length)]);
                                            if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                                if (Util.isTrue(10, 100)) {
                                                    player.point_gapthu += 1;
                                                    InventoryServiceNew.gI().addItemBag(player, gapx100);
                                                    InventoryServiceNew.gI().sendItemBags(player);
                                                    this.createOtherMenu(player, 12345, "|7|ĐANG TIẾN HÀNH GẮP AUTO X100\nSỐ LƯỢT CÒN : " + timex100 + " LƯỢT\n" + "|2|Đã gắp được : " + gapx100.template.name + "\nSố xu còn : " + InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259).quantity + "\n|7|TỔNG ĐIỂM : " + player.point_gapthu + "\nNẾU HÀNH TRANG ĐẦY, ITEM SẼ ĐƯỢC THÊM VÀO RƯƠNG PHỤ",
                                                            "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                } else {
                                                    this.createOtherMenu(player, 12345, "|7|ĐANG TIẾN HÀNH GẮP AUTO X100\nSỐ LƯỢT CÒN : " + timex100 + " LƯỢT\n" + "|2|Gắp hụt rồi!" + "\nSố xu còn : " + InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259).quantity + "\n|7|TỔNG ĐIỂM : " + player.point_gapthu + "\nNẾU HÀNH TRANG ĐẦY, ITEM SẼ ĐƯỢC THÊM VÀO RƯƠNG PHỤ",
                                                            "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                }
                                            }
                                            if (InventoryServiceNew.gI().getCountEmptyBag(player) == 0) {
                                                if (Util.isTrue(2, 100)) {
                                                    player.inventory.itemsBoxCrackBall.add(ItemService.gI().createNewItem((short) 1259));
                                                }
                                                if (Util.isTrue(10, 100)) {
                                                    player.point_gapthu += 1;
                                                    player.inventory.itemsBoxCrackBall.add(gapx100);
                                                    this.createOtherMenu(player, 12345, "|7|HÀNH TRANG ĐÃ ĐẦY\nĐANG TIẾN HÀNH GẮP AUTO X100 VÀO RƯƠNG PHỤ\nSỐ LƯỢT CÒN : " + timex100 + " LƯỢT\n" + "|2|Đã gắp được : " + gapx100.template.name + "\nSố xu còn : " + InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259).quantity + "\n|7|TỔNG ĐIỂM : " + player.point_gapthu,
                                                            "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                } else {
                                                    this.createOtherMenu(player, 12345, "|7|HÀNH TRANG ĐÃ ĐẦY\nĐANG TIẾN HÀNH GẮP AUTO X100 VÀO RƯƠNG PHỤ\nSỐ LƯỢT CÒN : " + timex100 + " LƯỢT\n" + "|2|Gắp hụt rồi!" + "\nSố xu còn : " + InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1259).quantity + "\n|7|TỔNG ĐIỂM : " + player.point_gapthu,
                                                            "Gắp X1", "Gắp X10", "Gắp X100", "Rương Đồ");
                                                }
                                            }
                                        }
                                    } catch (Exception e) {
                                    }
                                    break;
                                case 3:
                                    this.createOtherMenu(player, ConstNpc.RUONG_PHU,
                                            "|1|Tình yêu như một dây đàn\n"
                                            + "Tình vừa được thì đàn đứt dây\n"
                                            + "Đứt dây này anh thay dây khác\n"
                                            + "Mất em rồi anh biết thay ai?",
                                            "Rương Phụ\n(" + (player.inventory.itemsBoxCrackBall.size()
                                            - InventoryServiceNew.gI().getCountEmptyListItem(player.inventory.itemsBoxCrackBall))
                                            + "/200)",
                                            "Xóa Hết\nRương Phụ", "Đóng");
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.RUONG_PHU) {
                            switch (select) {
                                case 0:
                                    ShopServiceNew.gI().opendShop(player, "RUONG_PHU", true);
                                    break;
                                case 1:
                                    NpcService.gI().createMenuConMeo(player,
                                            ConstNpc.CONFIRM_REMOVE_ALL_ITEM_LUCKY_ROUND, this.avartar,
                                            "|3|Bạn chắc muốn xóa hết vật phẩm trong rương phụ?\n"
                                            + "|7|Sau khi xóa sẽ không thể khôi phục!",
                                            "Đồng ý", "Hủy bỏ");
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == 123455) {
                            switch (select) {
                                case 0:
                                    Service.gI().showListTop(player, Manager.topGapThu);
                                    break;
                                case 1:
                                    Service.gI().sendThongBao(player, "|7|Đang Update!!!");
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    private static Npc caPybaRa(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                createOtherMenu(player, 0, "\b|8|Trò chơi Tài Xỉu đang được diễn ra\n\n|6|Thử vận may của bạn với trò chơi Tài Xỉu! Đặt cược và dự đoán đúng"
                        + "\n kết quả, bạn sẽ được nhận thưởng lớn. Hãy tham gia ngay và\n cùng trải nghiệm sự hồi hộp, thú vị trong trò chơi này!"
                        + "\n\n|7|(Điều kiện tham gia : mở thành viên)\n\n|2|Đặt tối thiểu: 10 Thỏi Vàng\n Tối đa: 1.000 Thỏi Vàng"
                        + "\n\n|7| Lưu ý : Thoát game khi chốt Kết quả sẽ MẤT Tiền cược và Tiền thưởng", "Thể lệ", "Tham gia");
            }

            @Override
            public void confirmMenu(Player pl, int select) {
                if (canOpenNpc(pl)) {
                    String time = ((TaiXiu.gI().lastTimeEnd - System.currentTimeMillis()) / 1000) + " giây";
                    if (pl.iDMark.getIndexMenu() == 0) {
                        if (select == 0) {
                            createOtherMenu(pl, ConstNpc.IGNORE_MENU, "|5|Có 2 nhà cái Tài và Xĩu, bạn chỉ được chọn 1 nhà để tham gia"
                                    + "\n\n|6|Sau khi kết thúc thời gian đặt cược. Hệ thống sẽ tung xí ngầu để biết kết quả Tài Xỉu"
                                    + "\n\nNếu Tổng số 3 con xí ngầu <=10 : XỈU\nNếu Tổng số 3 con xí ngầu >10 : TÀI\nNếu 3 Xí ngầu cùng 1 số : TAM HOA (Nhà cái lụm hết)"
                                    + "\n\n|7|Lưu ý: Số Thỏi Vàng nhận được sẽ bị nhà cái lụm đi 20%. Trong quá trình diễn ra khi đặt cược nếu thoát game trong lúc phát thưởng phần quà sẽ bị HỦY", "Ok");
                        } else if (select == 1) {
                            if (TaiXiu.gI().baotri == false) {
                                if (pl.goldTai == 0 && pl.goldXiu == 0) {
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");
                                } else if (pl.goldTai > 0) {
                                    createOtherMenu(pl, 1, "\n|7|---NHÀ CÁI TÀI XỈU---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");
                                } else {
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");
                                }
                            } else {
                                if (pl.goldTai == 0 && pl.goldXiu == 0) {
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");
                                } else if (pl.goldTai > 0) {
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");
                                } else {
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n\nTổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng\n\n|5|Thời gian còn lại: " + time + "\n\n|7|Bạn đã cược Xỉu : " + Util.format(pl.goldXiu) + " Thỏi Vàng" + "\n\n|7|Hệ thống sắp bảo trì", "Cập nhập", "Đóng");
                                }
                            }
                        }
                    } else if (pl.iDMark.getIndexMenu() == 1) {
                        if (((TaiXiu.gI().lastTimeEnd - System.currentTimeMillis()) / 1000) > 0 && pl.goldTai == 0 && pl.goldXiu == 0 && TaiXiu.gI().baotri == false) {
                            switch (select) {
                                case 0:
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");
                                    break;
                                case 1:
                                    if (!pl.getSession().actived) {
                                        Service.gI().sendThongBao(pl, "Vui lòng kích hoạt tài khoản để sử dụng chức năng này");
                                    } else {
                                        Input.gI().TAI_taixiu(pl);
                                    }
                                    break;
                                case 2:
                                    if (!pl.getSession().actived) {
                                        Service.gI().sendThongBao(pl, "Vui lòng kích hoạt tài khoản để sử dụng chức năng này");
                                    } else {
                                        Input.gI().XIU_taixiu(pl);
                                    }
                                    break;
                            }
                        } else if (((TaiXiu.gI().lastTimeEnd - System.currentTimeMillis()) / 1000) > 0 && pl.goldTai > 0 && TaiXiu.gI().baotri == false) {
                            switch (select) {
                                case 0:
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");

                                    break;
                            }
                        } else if (((TaiXiu.gI().lastTimeEnd - System.currentTimeMillis()) / 1000) > 0 && pl.goldXiu > 0 && TaiXiu.gI().baotri == false) {
                            switch (select) {
                                case 0:
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");
                                    break;
                            }
                        } else if (((TaiXiu.gI().lastTimeEnd - System.currentTimeMillis()) / 1000) > 0 && pl.goldTai > 0 && TaiXiu.gI().baotri == true) {
                            switch (select) {
                                case 0:
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");

                                    break;
                            }
                        } else if (((TaiXiu.gI().lastTimeEnd - System.currentTimeMillis()) / 1000) > 0 && pl.goldXiu > 0 && TaiXiu.gI().baotri == true) {
                            switch (select) {
                                case 0:
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");

                                    break;
                            }
                        } else if (((TaiXiu.gI().lastTimeEnd - System.currentTimeMillis()) / 1000) > 0 && pl.goldXiu == 0 && pl.goldTai == 0 && TaiXiu.gI().baotri == true) {
                            switch (select) {
                                case 0:
                                    createOtherMenu(pl, 1, "\n|7|---BÁN NHÀ BÁN XE CHƠI TÀI XỈU ĐI CÁC ÔNG---\n\n|3|Kết quả kì trước:  " + TaiXiu.gI().x + " : " + TaiXiu.gI().y + " : " + TaiXiu.gI().z
                                            + "\n\n|6|Tổng nhà TÀI: " + Util.format(TaiXiu.gI().goldTai) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt TÀI: " + TaiXiu.gI().PlayersTai.size() + " người"
                                            + "\n\n|6|Tổng nhà XỈU: " + Util.format(TaiXiu.gI().goldXiu) + " Thỏi Vàng"
                                            + "\n|1|Tổng người đặt XỈU: " + TaiXiu.gI().PlayersXiu.size() + " người"
                                            + "\n\n|5|Thời gian còn lại: " + time, "Cập nhập", "Theo TÀI", "Theo XỈU", "Đóng");

                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    private static Npc xeMia(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 0 || this.mapId == 7 || this.mapId == 14) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "|7|Cư dân tìm khúc mía và nước đá\n"
                                + "Sau đó đến các xe nước mía ở đầu làng để xay nước mía\n"
                                + "|5|[CÔNG THỨC]\n"
                                + "|2|Nước Mía Khổng Lồ: 100 Cục đá, 50 khúc mía, 500 triệu vàng\n"
                                + "Nước Mía Ép Thơm: 200 Cục đá, 70 khúc mía, 500 triệu vàng\n"
                                + "Nước Mía Sầu Riêng: 300 Cục đá, 100 khúc mía, 500 triệu vàng\n"
                                + "Khúc Mía Săn Boss Mặt Trời mùa hè\n"
                                + "Cục Đá Có Thể Mua Tại Shop Sự Kiện Ở Đảo Kame\n"
                                + "|7|Nước Mía Khổng Lồ tăng 10%HP\n"
                                + "Nước Mía Ép Tơm tăng 10% HP, KI\n"
                                + "Nước Mía Sầu Riêng tăng 10%HP, KI, SĐ\n"
                                + "|-1|Úp cục đá tại all quái tỉ lệ 1%\n"
                                + "Khúc mía đổi hoặc mua tại shop\n"
                                + "Uống mỗi loại cốc nước mía sẽ được tăng chỉ số 15p\n"
                                + "chúc các bạn chơi game vui vẻ\n",
                                "Nước Mía\nKhổng Lồ\n(1 phút)", "Nước Mía\nÉp Thơm\n(3 phút)", "Nước Mía\nSầu Riêng\n(5 phút)", "Từ chối");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    Item cucDa;
                    Item khucMia;
                    Item thom;
                    Item sauRieng;
                    if (player.iDMark.isBaseMenu()) {
                        if (this.mapId == 0 || this.mapId == 7 || this.mapId == 14) {
                            switch (select) {
                                case 0:
                                    cucDa = InventoryServiceNew.gI().findItemBag(player, 1248);
                                    khucMia = InventoryServiceNew.gI().findItemBag(player, 1249);
                                    if (cucDa != null && cucDa.quantity < 100) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (100 - cucDa.quantity) + " cục đá.");
                                    } else if (cucDa == null) {
                                        this.npcChat(player, "Bạn không có cục đá nào.");
                                    } else if (khucMia != null && khucMia.quantity < 50) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (50 - khucMia.quantity) + " khúc mía.");
                                    } else if (khucMia == null) {
                                        this.npcChat(player, "Bạn không có khúc mía nào.");
                                    } else {
                                        new Thread(() -> {
                                            int timeWait = 60;
                                            while (timeWait > 0) {
                                                try {
                                                    timeWait--;
                                                    this.npcChat(player, "Đang xay nước mía\n|7|Thời gian còn lại: " + timeWait + ".");
                                                    Thread.sleep(1000);
                                                } catch (InterruptedException ex) {
                                                }
                                            }
                                            Item nuocMia = ItemService.gI().createNewItem((short) 1247);
                                            cucDa.quantity -= 100;
                                            khucMia.quantity -= 50;
                                            player.inventory.gold -= 500_000_000;
                                            Service.gI().sendMoney(player);
                                            InventoryServiceNew.gI().addItemBag(player, nuocMia);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            this.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Đã xay nước mía xong\n|7|Bạn đã nhận được " + nuocMia.template.name, "Nhận Ngay");
                                        }).start();
                                    }
                                    break;
                                case 1:
                                    cucDa = InventoryServiceNew.gI().findItemBag(player, 1248);
                                    khucMia = InventoryServiceNew.gI().findItemBag(player, 1249);
                                    thom = InventoryServiceNew.gI().findItemBag(player, 1258);
                                    if (cucDa != null && cucDa.quantity < 200) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (200 - cucDa.quantity) + " cục đá.");
                                    } else if (cucDa == null) {
                                        this.npcChat(player, "Bạn không có cục đá nào.");
                                    } else if (khucMia != null && khucMia.quantity < 70) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (70 - khucMia.quantity) + " khúc mía.");
                                    } else if (khucMia == null) {
                                        this.npcChat(player, "Bạn không có khúc mía nào.");
                                    } else if (thom != null && thom.quantity < 99) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (99 - thom.quantity) + " Thơm.");
                                    } else if (thom == null) {
                                        this.npcChat(player, "Bạn không có Túi Đựng Thơm nào.");
                                    } else {
                                        new Thread(() -> {
                                            int timeWait = 160;
                                            while (timeWait > 0) {
                                                try {
                                                    timeWait--;
                                                    this.npcChat(player, "Đang xay nước mía\n|7|Thời gian còn lại: " + timeWait + ".");
                                                    Thread.sleep(1000);
                                                } catch (InterruptedException ex) {
                                                }
                                            }
                                            Item nuocMia = ItemService.gI().createNewItem((short) 1246);
                                            cucDa.quantity -= 200;
                                            khucMia.quantity -= 70;
                                            thom.quantity -= 99;
                                            player.inventory.gold -= 500_000_000;
                                            Service.gI().sendMoney(player);
                                            InventoryServiceNew.gI().addItemBag(player, nuocMia);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            this.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Đã xay nước mía xong\n|7|Bạn đã nhận được " + nuocMia.template.name, "Nhận Ngay");
                                        }).start();
                                    }
                                    break;
                                case 2:
                                    cucDa = InventoryServiceNew.gI().findItemBag(player, 1248);
                                    khucMia = InventoryServiceNew.gI().findItemBag(player, 1249);
                                    sauRieng = InventoryServiceNew.gI().findItemBag(player, 1257);
                                    if (cucDa != null && cucDa.quantity < 300) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (300 - cucDa.quantity) + " cục đá.");
                                    } else if (cucDa == null) {
                                        this.npcChat(player, "Bạn không có cục đá nào.");
                                    } else if (khucMia != null && khucMia.quantity < 100) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (100 - khucMia.quantity) + " khúc mía.");
                                    } else if (khucMia == null) {
                                        this.npcChat(player, "Bạn không có khúc mía nào.");
                                    } else if (sauRieng != null && sauRieng.quantity < 99) {
                                        this.npcChat(player, "Bạn còn thiếu x" + (99 - sauRieng.quantity) + " Sầu Riêng.");
                                    } else if (sauRieng == null) {
                                        this.npcChat(player, "Bạn không có Miếng Sầu Riêng nào.");
                                    } else {
                                        new Thread(() -> {
                                            int timeWait = 300;
                                            while (timeWait > 0) {
                                                try {
                                                    timeWait--;
                                                    this.npcChat(player, "Đang xay nước mía\n|7|Thời gian còn lại: " + timeWait + ".");
                                                    Thread.sleep(1000);
                                                } catch (InterruptedException ex) {
                                                }
                                            }
                                            Item nuocMia = ItemService.gI().createNewItem((short) 1245);
                                            cucDa.quantity -= 300;
                                            khucMia.quantity -= 100;
                                            sauRieng.quantity -= 99;
                                            player.inventory.gold -= 500_000_000;
                                            Service.gI().sendMoney(player);
                                            InventoryServiceNew.gI().addItemBag(player, nuocMia);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            this.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Đã xay nước mía xong\n|7|Bạn đã nhận được " + nuocMia.template.name, "Nhận Ngay");
                                        }).start();
                                    }
                                    break;
                            }

                        }
                    }
                }
            }

        };
    }

    public static Npc santa(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    createOtherMenu(player, ConstNpc.BASE_MENU,
                            "Xin chào, ta có một số vật phẩm đặt biệt cậu có muốn xem không?",
                            "Cửa hàng", "Tiệm Hớt Tóc", "Shop Hồng Ngọc", "Shop Thỏi Vàng");
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {

                    if (this.mapId == 5 || this.mapId == 13 || this.mapId == 20) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0: //shop
                                    ShopServiceNew.gI().opendShop(player, "SANTA", false);
                                    break;
                                case 1:
                                    ShopServiceNew.gI().opendShop(player, "SANTA_HEAD", false);
                                    break;
                                case 2: //shop
                                    ShopServiceNew.gI().opendShop(player, "SHOP_VIP", false);
                                    break;
                                case 3: //shop
                                    ShopServiceNew.gI().opendShop(player, "SHOP_TV", false);
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc thodaika(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                new Thread(() -> {
                    try {
                        while (true) {
                            Thread.sleep(5000);
                            new Thread(() -> {
                                try {
                                    Thread.sleep(1000);
                                    this.npcChat(player, "Chơi Tài Xỉu Đi Mấy Cháu");
                                } catch (Exception e) {
                                }
                            }).start();
                        }
                    } catch (Exception e) {
                    }
                }).start();
                if (canOpenNpc(player)) {
                    createOtherMenu(player, ConstNpc.BASE_MENU,
                            "Bú Cu Bú Cu\nĐặt Tài Ra Xỉu\nĐặt Xỉu Ra Tài",
                            "Xỉu", "Tài");
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5) {

                        if (!player.getSession().actived) {
                            Service.gI().sendThongBao(player, "Vui lòng kích hoạt tài khoản để sử dụng chức năng này");

                        } else if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    Input.gI().TAI(player);
                                    break;
                                case 1:
                                    Input.gI().XIU(player);
                                    break;

//                                case 1:
//                            ShopKyGuiService.gI().openShopKyGui(player);
//                            break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc uron(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player pl) {
                if (canOpenNpc(pl)) {
                    ShopServiceNew.gI().opendShop(pl, "URON", false);
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {

                }
            }
        };
    }

    public static Npc baHatMit(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            String[] menuselect = new String[]{};

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Ngươi tìm ta có việc gì?",
                                "Ép sao\ntrang bị", "Pha lê\nhóa trang bị", "Pháp sư\ntrang bị", "Nâng Cấp\nLevel SKH", "Võ Đài\nBà Hạt Mít");
                    } else if (this.mapId == 121) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Ngươi tìm ta có việc gì?",
                                "Về đảo\nrùa");
                    } else if (this.mapId == 112) {
                        if (Util.isAfterMidnight(player.lastTimePKVoDaiSinhTu)) {
                            player.haveRewardVDST = false;
                            player.thoiVangVoDaiSinhTu = 0;
                        }
                        if (VoDaiManager.gI().getVDST(player.zone) != null) {
                            if (VoDaiManager.gI().getVDST(player.zone).getPlayer().equals(player)) {
                                this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                        "Ngươi muốn hủy đăng ký thi đấu võ đài?",
                                        "Nhận Quà", "Đồng ý\n" + player.thoiVangVoDaiSinhTu + " thỏi vàng", "Từ chối", "Về\nđảo rùa");
                                return;
                            }
                            this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                    "Ngươi muốn đăng ký thi đấu võ đài?\nnhiều phần thưởng giá trị đang đợi ngươi đó",
                                    "Nhận Quà", "Bình chọn", "Đồng ý\n" + player.thoiVangVoDaiSinhTu + " thỏi vàng", "Từ chối", "Về\nđảo rùa");
                            return;
                        }
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Ngươi muốn đăng ký thi đấu võ đài?\nnhiều phần thưởng giá trị đang đợi ngươi đó",
                                "Nhận Quà", "Đồng ý\n" + player.thoiVangVoDaiSinhTu + " thỏi vàng", "Từ chối", "Về\nđảo rùa");
                    } else {
                        if (player.chucNangNhanBua == 1) {
                            this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                    "Ngươi tìm ta có việc gì?",
                                    "Nhận Bùa Ngẫu Nhiên", "Cửa hàng\nBùa", "Nâng cấp\nVật phẩm",
                                    "Nâng cấp\nBông tai\nPorata", "Nhập\nNgọc Rồng", "Nâng Cấp\nChân Mệnh", "Sách Tuyệt Kỹ");
                        } else {
                            this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                    "Ngươi tìm ta có việc gì?",
                                    "Cửa hàng\nBùa", "Nâng cấp\nVật phẩm",
                                    "Nâng cấp\nBông tai\nPorata", "Nhập\nNgọc Rồng", "Nâng Cấp\nChân Mệnh", "Sách Tuyệt Kỹ");
                        }
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.EP_SAO_TRANG_BI);
                                    break;
                                case 1:
                                    CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.PHA_LE_HOA_TRANG_BI);
                                    break;
                                case 2:
                                    this.createOtherMenu(player, ConstNpc.PHAP_SU_TRANG_BI,
                                            "Pháp sư hóa trang bị sẽ được thêm chỉ số Pháp sư\n"
                                            + "Giải pháp sư sẽ bị xóa tất cả chỉ số Pháp sư\n"
                                            + "Ngươi muốn làm gì?", "Pháp sư\nhóa\nTrang bị", "Giải\nPháp sư\nTrang bị");
                                    break;
                                case 3:
                                    this.createOtherMenu(player, ConstNpc.NANG_CAP_LEVEL,
                                            "Ở Đây Ta Đổi Đồ Thần Ra Đá Nâng Cấp Và Nâng Cấp Level SKH\n"
                                            + "Ngươi Muốn Làm Gì?\n",
                                            "Nâng Cấp\nLevel SKH", "Phân Rã Đồ Thần");
                                    break;
                                case 4:
                                    ChangeMapService.gI().changeMapNonSpaceship(player, 112, 203, 408);
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.PHAP_SU_TRANG_BI) {
                            switch (select) {
                                case 0:
                                    CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.PS_HOA_TRANG_BI);
                                    break;
                                case 1:
                                    CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.TAY_PS_HOA_TRANG_BI);
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.NANG_CAP_LEVEL) {
                            switch (select) {
                                case 0:
                                    CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.NANG_CAP_LEVEL_SKH);
                                    break;
                                case 1:
                                    CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.PHAN_RA_DO_THAN_RA_DA_NANG_CAP);
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_START_COMBINE) {
                            switch (player.combineNew.typeCombine) {
                                case CombineServiceNew.EP_SAO_TRANG_BI:
                                case CombineServiceNew.PHA_LE_HOA_TRANG_BI:
                                case CombineServiceNew.PS_HOA_TRANG_BI:
                                case CombineServiceNew.TAY_PS_HOA_TRANG_BI:
                                case CombineServiceNew.NANG_CAP_LEVEL_SKH:
                                case CombineServiceNew.PHAN_RA_DO_THAN_RA_DA_NANG_CAP:
                                    CombineServiceNew.gI().startCombine(player, select);
                                    break;
                            }
                        }
                    } else if (this.mapId == 112) {
                        if (player.iDMark.isBaseMenu()) {
                            if (player.haveRewardVDST) {
                                switch (select) {
                                    case 0 -> {
                                        if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                            Item item = ItemService.gI().createNewItem((short) (Util.nextInt(705, 708)));
                                            item.itemOptions.add(new Item.ItemOption(93, 30));
                                            InventoryServiceNew.gI().addItemBag(player, item);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            Service.gI().sendThongBao(player, "Bạn nhận được " + item.template.name);
                                            player.haveRewardVDST = false;
                                        } else {
                                            Service.gI().sendThongBao(player, "Hành trang không còn chỗ trống, không thể nhặt thêm");
                                        }
                                    }
                                    case 1 -> {
                                        if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                            Item item = ItemService.gI().createNewItem((short) 585);
                                            item.itemOptions.add(new Item.ItemOption(93, 30));
                                            InventoryServiceNew.gI().addItemBag(player, item);
                                            InventoryServiceNew.gI().sendItemBags(player);
                                            Service.gI().sendThongBao(player, "Bạn nhận được " + item.template.name);
                                            player.haveRewardVDST = false;
                                        } else {
                                            Service.gI().sendThongBao(player, "Hành trang không còn chỗ trống, không thể nhặt thêm");
                                        }
                                    }
                                }
                                return;
                            }
                            if (VoDaiManager.gI().getVDST(player.zone) != null) {
                                if (VoDaiManager.gI().getVDST(player.zone).getPlayer().equals(player)) {
                                    switch (select) {
                                        case 0 -> {
                                        }
                                        case 1 ->
                                            this.npcChat("Không thể thực hiện");
                                        case 2 -> {
                                        }
                                        case 3 ->
                                            ChangeMapService.gI().changeMapBySpaceShip(player, 5, -1, 1156);
                                    }
                                    return;
                                }
                                switch (select) {
                                    case 0 -> {
                                    }
                                    case 1 ->
                                            this.createOtherMenu(player, ConstNpc.DAT_CUOC_HAT_MIT,
                                                "Phí bình chọn là 1 triệu vàng\nkhi trận đấu kết thúc\n90% tổng tiền bình chọn sẽ chia đều cho phe bình chọn chính xác",
                                                "Bình chọn cho " + VoDaiManager.gI().getVDST(player.zone).getPlayer().name + " (" + VoDaiManager.gI().getVDST(player.zone).getCuocPlayer() + ")",
                                                "Bình chọn cho hạt mít (" + VoDaiManager.gI().getVDST(player.zone).getCuocBaHatMit() + ")");
                                    case 2 ->
                                        VoDaiService.gI().startChallenge(player);
                                    case 3 -> {
                                    }
                                    case 4 ->
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 5, -1, 1156);
                                }
                                return;
                            }
                            switch (select) {
                                case 0 -> {
                                }
                                case 1 ->
                                    VoDaiService.gI().startChallenge(player);
                                case 2 -> {
                                }
                                case 3 ->
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 5, -1, 1156);
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.DAT_CUOC_HAT_MIT) {
                            if (VoDaiManager.gI().getVDST(player.zone) != null) {
                                switch (select) {
                                    case 0 -> {
                                        if (player.inventory.gold >= 1_000_000) {
                                            VoDai vdst = VoDaiManager.gI().getVDST(player.zone);
                                            vdst.setCuocPlayer(vdst.getCuocPlayer() + 1);
                                            vdst.addBinhChon(player);
                                            player.binhChonPlayer++;
                                            player.zoneBinhChon = player.zone;
                                            player.inventory.gold -= 1_000_000;
                                            Service.gI().sendMoney(player);
                                        } else {
                                            Service.gI().sendThongBao(player, "Bạn không đủ vàng, còn thiếu " + Util.numberToMoney(1_000_000 - player.inventory.gold) + " vàng nữa");
                                        }
                                    }
                                    case 1 -> {
                                        if (player.inventory.gold >= 1_000_000) {
                                            VoDai vdst = VoDaiManager.gI().getVDST(player.zone);
                                            vdst.setCuocBaHatMit(vdst.getCuocBaHatMit() + 1);
                                            vdst.addBinhChon(player);
                                            player.binhChonHatMit++;
                                            player.zoneBinhChon = player.zone;
                                            player.inventory.gold -= 1_000_000;
                                            Service.gI().sendMoney(player);
                                        } else {
                                            Service.gI().sendThongBao(player, "Bạn không đủ vàng, còn thiếu " + Util.numberToMoney(1_000_000 - player.inventory.gold) + " vàng nữa");
                                        }
                                    }
                                }
                            }
                        }
                    } else if (this.mapId == 42 || this.mapId == 43 || this.mapId == 44 || this.mapId == 84) {
                        if (player.iDMark.isBaseMenu()) {
                            if (player.chucNangNhanBua == 1) {
                                switch (select) {
                                    case 0: // Ngẫu nhiên bùa 1h
                                        if (player.chucNangNhanBua == 1) {
                                            int idItem = Util.nextInt(213, 219);
                                            player.charms.addTimeCharms(idItem, 60);
                                            Item bua = ItemService.gI().createNewItem((short) idItem);
                                            Service.getInstance().sendThongBao(player, "Bạn vừa nhận thưởng " + bua.getName());
                                            player.chucNangNhanBua = 0;
                                        } else {
                                            Service.getInstance().sendThongBao(player, "Hôm nay bạn đã nhận bùa miễn phí rồi!!!");
                                        }
                                        break;
                                    case 1: //shop bùa
                                        createOtherMenu(player, ConstNpc.MENU_OPTION_SHOP_BUA,
                                                "Bùa của ta rất lợi hại, nhìn ngươi yếu đuối thế này, chắc muốn mua bùa để "
                                                + "mạnh mẽ à, mua không ta bán cho, xài rồi lại thích cho mà xem.",
                                                "Bùa\n1 giờ", "Bùa\n8 giờ", "Bùa\n1 tháng", "Đóng");
                                        break;
                                    case 2:
                                        CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.NANG_CAP_VAT_PHAM);
                                        break;
                                    case 3: //nâng cấp bông tai
                                        createOtherMenu(player, 211,
                                                "Ngươi muốn nâng bông tai à",
                                                "Nâng Cấp Bông Tai", "Mở Chỉ Số Bông Tai", "Đóng");
                                        break;
                                    case 4:
                                        CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.NHAP_NGOC_RONG);
                                        break;
                                    case 5:
                                        CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.NANG_CAP_CHAN_MENH);
                                        break;
                                    case 6:
                                        createOtherMenu(player, ConstNpc.SACH_TUYET_KY, "Ta có thể giúp gì cho ngươi ?",
                                                "Đóng thành\nSách cũ",
                                                "Đổi Sách\nTuyệt kỹ",
                                                "Giám định\nSách",
                                                "Tẩy\nSách",
                                                "Nâng cấp\nSách\nTuyệt kỹ",
                                                "Hồi phục\nSách",
                                                "Phân rã\nSách");
                                        break;
                                }
                            } else {
                                switch (select) {
                                    case 0: //shop bùa
                                        createOtherMenu(player, ConstNpc.MENU_OPTION_SHOP_BUA,
                                                "Bùa của ta rất lợi hại, nhìn ngươi yếu đuối thế này, chắc muốn mua bùa để "
                                                + "mạnh mẽ à, mua không ta bán cho, xài rồi lại thích cho mà xem.",
                                                "Bùa\n1 giờ", "Bùa\n8 giờ", "Bùa\n1 tháng", "Đóng");
                                        break;
                                    case 1:
                                        CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.NANG_CAP_VAT_PHAM);
                                        break;
                                    case 2: //nâng cấp bông tai
                                        createOtherMenu(player, 211,
                                                "Ngươi muốn nâng bông tai à",
                                                "Nâng Cấp Bông Tai", "Mở Chỉ Số Bông Tai", "Đóng");
                                        break;
                                    case 3:
                                        CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.NHAP_NGOC_RONG);
                                        break;
                                    case 4:
                                        CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.NANG_CAP_CHAN_MENH);
                                        break;
                                    case 5:
                                        createOtherMenu(player, ConstNpc.SACH_TUYET_KY, "Ta có thể giúp gì cho ngươi ?",
                                                "Đóng thành\nSách cũ",
                                                "Đổi Sách\nTuyệt kỹ",
                                                "Giám định\nSách",
                                                "Tẩy\nSách",
                                                "Nâng cấp\nSách\nTuyệt kỹ",
                                                "Hồi phục\nSách",
                                                "Phân rã\nSách");
                                        break;
                                }
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.SACH_TUYET_KY) {
                            switch (select) {
                                case 0:
                                    Item trangSachCu = InventoryServiceNew.gI().findItemBag(player, 1272);
                                    Item cobonla = InventoryServiceNew.gI().findItemBag(player, 1273);
                                    Item biaSach = InventoryServiceNew.gI().findItemBag(player, 1268);
                                    int tile = (cobonla != null) ? 100 : 20;
                                    if ((trangSachCu != null && trangSachCu.quantity >= 9999) && (biaSach != null && biaSach.quantity >= 1)) {
                                        createOtherMenu(player, ConstNpc.DONG_THANH_SACH_CU,
                                                "|2|Chế tạo Cuốn sách cũ\n"
                                                + "|1|Trang sách cũ " + trangSachCu.quantity + "/9999\n"
                                                + "Bìa sách " + biaSach.quantity + "/1\n"
                                                + (cobonla != null ? "|7|Cỏ Bốn Lá: Có\n" : "|7|Cỏ Bốn Lá: Không\n")
                                                + "Khi có Cỏ Bốn Lá tỉ lệ Thành Công 100%\n"
                                                + "Tỉ lệ thành công: " + tile + "%\n"
                                                + "Thất bại mất 99 trang sách và 1 bìa sách", "Đồng ý", "Từ chối");
                                        break;
                                    } else {
                                        String NpcSay = "|2|Chế tạo Cuốn sách cũ\n";
                                        if (trangSachCu == null) {
                                            NpcSay += "|7|Trang sách cũ " + "0/9999\n";
                                        } else {
                                            NpcSay += "|1|Trang sách cũ " + trangSachCu.quantity + "/9999\n";
                                        }
                                        if (biaSach == null) {
                                            NpcSay += "|7|Bìa sách " + "0/1\n";
                                        } else {
                                            NpcSay += "|1|Bìa sách " + biaSach.quantity + "/1\n";
                                        }
                                        NpcSay += (cobonla != null ? "|7|Cỏ Bốn Lá: Có\n" : "|7|Cỏ Bốn Lá: Không\n");
                                        NpcSay += "|7|Tỉ lệ thành công: " + tile + "%\n";
                                        NpcSay += "|7|Thất bại mất 99 trang sách và 1 bìa sách";
                                        createOtherMenu(player, ConstNpc.DONG_THANH_SACH_CU_2,
                                                NpcSay, "Từ chối");
                                        break;
                                    }
                                case 1:
                                    Item cuonSachCu = InventoryServiceNew.gI().findItemBag(player, 1271);
                                    Item kimBam = InventoryServiceNew.gI().findItemBag(player, 1269);
                                    Item buabaove = InventoryServiceNew.gI().findItemBag(player, 1273);
                                    int successRate = (buabaove != null) ? 100 : 20;

                                    if ((cuonSachCu != null && cuonSachCu.quantity >= 10) && (kimBam != null && kimBam.quantity >= 1)) {
                                        createOtherMenu(player, ConstNpc.DOI_SACH_TUYET_KY,
                                                "|2|Đổi sách tuyệt kỹ 1\n"
                                                + "|1|Cuốn sách cũ " + cuonSachCu.quantity + "/10\n"
                                                + "Kìm bấm giấy " + kimBam.quantity + "/1\n"
                                                + (buabaove != null ? "|1|Cỏ Bốn LáCỏ Bốn Lá: Có\n" : "|7|Cỏ Bốn Lá: Không\n")
                                                + "Khi có Cỏ Bốn Lá tỉ lệ Thành Công 100%\n"
                                                + "Tỉ lệ thành công: " + successRate + "%\n", "Đồng ý", "Từ chối");
                                        break;
                                    } else {
                                        String NpcSay = "|2|Đổi sách Tuyệt kỹ 1\n";
                                        if (cuonSachCu == null) {
                                            NpcSay += "|7|Cuốn sách cũ " + "0/10\n";
                                        } else {
                                            NpcSay += "|1|Cuốn sách cũ " + cuonSachCu.quantity + "/10\n";
                                        }
                                        if (kimBam == null) {
                                            NpcSay += "|7|Kìm bấm giấy " + "0/1\n";
                                        } else {
                                            NpcSay += "|1|Kìm bấm giấy " + kimBam.quantity + "/1\n";
                                        }
                                        NpcSay += (buabaove != null ? "|1|Cỏ Bốn Lá: Có\n" : "|7|Cỏ Bốn Lá: Không\n");
                                        NpcSay += "|7|Tỉ lệ thành công: " + successRate + "%\n";
                                        createOtherMenu(player, ConstNpc.DOI_SACH_TUYET_KY_2,
                                                NpcSay, "Từ chối");
                                    }
                                    break;

                                case 2:// giám định sách
                                    CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.GIAM_DINH_SACH);
                                    break;
                                case 3:// tẩy sách
                                    CombineServiceNew.gI().openTabCombine(player,
                                            CombineServiceNew.TAY_SACH);
                                    break;
                                case 4:// nâng cấp sách
                                    CombineServiceNew.gI().openTabCombine(player,
                                            CombineServiceNew.NANG_CAP_SACH_TUYET_KY);
                                    break;
                                case 5:// phục hồi sách
                                    CombineServiceNew.gI().openTabCombine(player,
                                            CombineServiceNew.PHUC_HOI_SACH);
                                    break;
                                case 6:// phân rã sách
                                    CombineServiceNew.gI().openTabCombine(player,
                                            CombineServiceNew.PHAN_RA_SACH);
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.DOI_SACH_TUYET_KY) {
                            switch (select) {
                                case 0:
                                    Item cuonSachCu = InventoryServiceNew.gI().findItemBag(player, 1271);
                                    Item kimBam = InventoryServiceNew.gI().findItemBag(player, 1269);
                                    Item buabaove = InventoryServiceNew.gI().findItemBag(player, 1273);
                                    short baseValue = 1264;
                                    short genderModifier = (player.gender == 0) ? -2 : ((player.gender == 2) ? 2 : (short) 0);
                                    Item sachTuyetKy = ItemService.gI().createNewItem((short) (baseValue + genderModifier));
                                    boolean success = (buabaove != null) ? true : Util.isTrue(20, 100);

                                    if (success) {
                                        sachTuyetKy.itemOptions.add(new Item.ItemOption(217, 0));
                                        sachTuyetKy.itemOptions.add(new Item.ItemOption(21, 40));
                                        sachTuyetKy.itemOptions.add(new Item.ItemOption(30, 0));
                                        sachTuyetKy.itemOptions.add(new Item.ItemOption(87, 1));
                                        sachTuyetKy.itemOptions.add(new Item.ItemOption(214, 5));
                                        sachTuyetKy.itemOptions.add(new Item.ItemOption(215, 1000));
                                        try { // send effect success
                                            Message msg = new Message(-81);
                                            msg.writer().writeByte(0);
                                            msg.writer().writeUTF("test");
                                            msg.writer().writeUTF("test");
                                            msg.writer().writeShort(tempId);
                                            player.sendMessage(msg);
                                            msg.cleanup();
                                            msg = new Message(-81);
                                            msg.writer().writeByte(1);
                                            msg.writer().writeByte(2);
                                            msg.writer().writeByte(InventoryServiceNew.gI().getIndexBag(player, kimBam));
                                            msg.writer().writeByte(InventoryServiceNew.gI().getIndexBag(player, cuonSachCu));
                                            player.sendMessage(msg);
                                            msg.cleanup();
                                            msg = new Message(-81);
                                            msg.writer().writeByte(7);
                                            msg.writer().writeShort(sachTuyetKy.template.iconID);
                                            msg.writer().writeShort(-1);
                                            msg.writer().writeShort(-1);
                                            msg.writer().writeShort(-1);
                                            player.sendMessage(msg);
                                            msg.cleanup();
                                        } catch (Exception e) {
                                            System.out.println("lỗi 4");
                                        }
                                        InventoryServiceNew.gI().addItemBag(player, sachTuyetKy);
                                        InventoryServiceNew.gI().subQuantityItemsBag(player, cuonSachCu, 10);
                                        InventoryServiceNew.gI().subQuantityItemsBag(player, kimBam, 1);
                                        if (buabaove != null) {
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, buabaove, 1);
                                        }
                                        InventoryServiceNew.gI().sendItemBags(player);
                                        npcChat(player, "|7|Thành Công");
                                        return;
                                    } else {
                                        try { // send effect fail
                                            Message msg = new Message(-81);
                                            msg.writer().writeByte(0);
                                            msg.writer().writeUTF("test");
                                            msg.writer().writeUTF("test");
                                            msg.writer().writeShort(tempId);
                                            player.sendMessage(msg);
                                            msg.cleanup();
                                            msg = new Message(-81);
                                            msg.writer().writeByte(1);
                                            msg.writer().writeByte(2);
                                            msg.writer().writeByte(InventoryServiceNew.gI().getIndexBag(player, kimBam));
                                            msg.writer().writeByte(InventoryServiceNew.gI().getIndexBag(player, cuonSachCu));
                                            player.sendMessage(msg);
                                            msg.cleanup();
                                            msg = new Message(-81);
                                            msg.writer().writeByte(8);
                                            msg.writer().writeShort(-1);
                                            msg.writer().writeShort(-1);
                                            msg.writer().writeShort(-1);
                                            player.sendMessage(msg);
                                            msg.cleanup();
                                        } catch (Exception e) {
                                            System.out.println("lỗi 3");
                                        }
                                        InventoryServiceNew.gI().subQuantityItemsBag(player, cuonSachCu, 5);
                                        InventoryServiceNew.gI().subQuantityItemsBag(player, kimBam, 1);
                                        InventoryServiceNew.gI().sendItemBags(player);
                                        npcChat(player, "|7|Thất Bại");
                                    }
                                    return;

                                case 1:
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.DONG_THANH_SACH_CU) {
                            switch (select) {
                                case 0:

                                    Item trangSachCu = InventoryServiceNew.gI().findItemBag(player, 1272);
                                    Item biaSach = InventoryServiceNew.gI().findItemBag(player, 1268);
                                    Item cobonla = InventoryServiceNew.gI().findItemBag(player, 1273);
                                    Item cuonSachCu = ItemService.gI().createNewItem((short) 1271);
                                    boolean success = (cobonla != null) ? true : Util.isTrue(20, 100);
                                    if (success) {
                                        cuonSachCu.itemOptions.add(new Item.ItemOption(30, 0));
                                        try { // send effect susscess
                                            Message msg = new Message(-81);
                                            msg.writer().writeByte(0);
                                            msg.writer().writeUTF("test");
                                            msg.writer().writeUTF("test");
                                            msg.writer().writeShort(tempId);
                                            player.sendMessage(msg);
                                            msg.cleanup();

                                            msg = new Message(-81);
                                            msg.writer().writeByte(1);
                                            msg.writer().writeByte(2);
                                            msg.writer().writeByte(InventoryServiceNew.gI().getIndexBag(player, trangSachCu));
                                            msg.writer().writeByte(InventoryServiceNew.gI().getIndexBag(player, biaSach));
                                            player.sendMessage(msg);
                                            msg.cleanup();

                                            msg = new Message(-81);
                                            msg.writer().writeByte(7);
                                            msg.writer().writeShort(cuonSachCu.template.iconID);
                                            msg.writer().writeShort(-1);
                                            msg.writer().writeShort(-1);
                                            msg.writer().writeShort(-1);
                                            player.sendMessage(msg);
                                            msg.cleanup();

                                        } catch (Exception e) {
                                            System.out.println("lỗi 1");
                                        }
                                        InventoryServiceNew.gI().addItemList(player.inventory.itemsBag, cuonSachCu);
                                        InventoryServiceNew.gI().subQuantityItemsBag(player, trangSachCu, 9999);
                                        InventoryServiceNew.gI().subQuantityItemsBag(player, biaSach, 1);
                                        if (cobonla != null) {
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, cobonla, 1);
                                        }
                                        InventoryServiceNew.gI().sendItemBags(player);
                                        npcChat(player, "|7|Thành Công");
                                        return;
                                    } else {
                                        try { // send effect faile
                                            Message msg = new Message(-81);
                                            msg.writer().writeByte(0);
                                            msg.writer().writeUTF("test");
                                            msg.writer().writeUTF("test");
                                            msg.writer().writeShort(tempId);
                                            player.sendMessage(msg);
                                            msg.cleanup();
                                            msg = new Message(-81);
                                            msg.writer().writeByte(1);
                                            msg.writer().writeByte(2);
                                            msg.writer().writeByte(InventoryServiceNew.gI().getIndexBag(player, biaSach));
                                            msg.writer().writeByte(InventoryServiceNew.gI().getIndexBag(player, trangSachCu));
                                            player.sendMessage(msg);
                                            msg.cleanup();
                                            msg = new Message(-81);
                                            msg.writer().writeByte(8);
                                            msg.writer().writeShort(-1);
                                            msg.writer().writeShort(-1);
                                            msg.writer().writeShort(-1);
                                            player.sendMessage(msg);
                                            msg.cleanup();
                                        } catch (Exception e) {
                                            System.out.println("lỗi 2");
                                        }
                                        InventoryServiceNew.gI().subQuantityItemsBag(player, trangSachCu, 99);
                                        InventoryServiceNew.gI().subQuantityItemsBag(player, biaSach, 1);
                                        InventoryServiceNew.gI().sendItemBags(player);
                                        npcChat(player, "|7|Thất Bại");
                                    }
                                    return;
                                case 1:
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == 211) {
                            switch (select) {
                                case 0:
                                    CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.NANG_CAP_BONG_TAI);
                                    break;
                                case 1:
                                    CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.MO_CHI_SO_BONG_TAI);
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPTION_SHOP_BUA) {
                            switch (select) {
                                case 0:
                                    ShopServiceNew.gI().opendShop(player, "BUA_1H", true);
                                    break;
                                case 1:
                                    ShopServiceNew.gI().opendShop(player, "BUA_8H", true);
                                    break;
                                case 2:
                                    ShopServiceNew.gI().opendShop(player, "BUA_1M", true);
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_START_COMBINE) {
                            switch (player.combineNew.typeCombine) {
                                case CombineServiceNew.NANG_CAP_VAT_PHAM:
                                case CombineServiceNew.NANG_CAP_BONG_TAI:
                                case CombineServiceNew.LAM_PHEP_NHAP_DA:
                                case CombineServiceNew.NHAP_NGOC_RONG:
                                case CombineServiceNew.MO_CHI_SO_BONG_TAI:
                                case CombineServiceNew.NANG_CAP_CHAN_MENH:
                                case CombineServiceNew.GIAM_DINH_SACH:
                                case CombineServiceNew.TAY_SACH:
                                case CombineServiceNew.NANG_CAP_SACH_TUYET_KY:
                                case CombineServiceNew.PHUC_HOI_SACH:
                                case CombineServiceNew.PHAN_RA_SACH:

                                    if (select == 0) {
                                        CombineServiceNew.gI().startCombine(player, 0);
                                    }
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_PHAN_RA_DO_THAN_LINH) {
                            if (select == 0) {
                                CombineServiceNew.gI().startCombine(player, 0);
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_NANG_CAP_DO_TS) {
                            if (select == 0) {
                                CombineServiceNew.gI().startCombine(player, 0);
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_NANG_DOI_SKH_VIP) {
                            if (select == 0) {
                                CombineServiceNew.gI().startCombine(player, 0);
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc ruongDo(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    InventoryServiceNew.gI().sendItemBox(player);
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {

                }
            }
        };
    }

    public static Npc duongtank(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (mapId == 0) {
                        nguhs.gI().setTimeJoinnguhs();
                        long now = System.currentTimeMillis();
                        if (now > nguhs.TIME_OPEN_NHS && now < nguhs.TIME_CLOSE_NHS) {
                            this.createOtherMenu(player, 0, "|7|[ • MAP NGŨ HÀNH SON ĐÃ MỞ CỬA • ]\n"
                                    + "50 hồng ngọc 1 lần vào, tham gia ngay?\n"
                                    + "Bạn Cần Đạt Đủ 80 Tỷ Sức Mạnh và 50 Hồng Ngọc Để Có Thể Vào",
                                    "Chiến Ngay", "Đổi Chân Mệnh", "Đóng");
                        } else {
                            this.createOtherMenu(player, 0, "|7|Map Ngũ Hành Sơ đã mở cửa, 50 hồng ngọc 1 lần vào, tham gia ngay?", "Đổi Chân Mệnh", "Đóng");
                        }
                    }
                    if (mapId == 122) {
                        this.createOtherMenu(player, 0, "Bạn Muốn Quay Trở Lại Làng Aru?", "OK", "Từ chối");

                    }
                    if (mapId == 124) {
                        this.createOtherMenu(player, 0, "Xia xia thua phùa\b|7|Thí chủ đang có: " + player.NguHanhSonPoint + " điểm ngũ hành sơn\b|1|Thí chủ muốn đổi cải trang x4 chưởng ko?", "Âu kê", "Top Ngu Hanh Son", "No");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                Item daChanmenh;
                if (canOpenNpc(player)) {
                    if (mapId == 0) {
                        switch (select) {
                            case 0:
                                if (player.nPoint.power < 80000000000L) {
                                    Service.getInstance().sendThongBao(player, "Sức mạnh bạn không đủ để qua map!");
                                    return;
                                } else if (player.inventory.ruby < 50) {
                                    Service.getInstance().sendThongBao(player, "Phí vào là 50 hồng ngọc một lần bạn ey!\nBạn không đủ!");
                                    return;
                                } else {
                                    player.inventory.ruby -= 50;
                                    PlayerService.gI().sendInfoHpMpMoney(player);
                                    ChangeMapService.gI().changeMapInYard(player, 122, -1, -1);
                                }
                                break;
                            case 1:
                                daChanmenh = InventoryServiceNew.gI().findItemBag(player, 1241);
                                if (daChanmenh != null && daChanmenh.quantity < 99) {
                                    this.npcChat(player, "Bạn còn thiếu x" + (99 - daChanmenh.quantity) + " Đá Chân Mệnh.");
                                } else if (daChanmenh == null) {
                                    this.npcChat(player, "Bạn không có Đá Chân Mệnh.");
                                } else if (player.inventory.gold < 500_000_000) {
                                    this.npcChat(player, "Bạn còn thiếu x" + (Util.numberToMoney(500_000_000 - player.inventory.gold)) + " Vàng.");
                                } else {
                                    Item chanMenh = ItemService.gI().createNewItem((short) 1232);
                                    daChanmenh.quantity -= 99;
                                    player.inventory.gold -= 500_000_000;
                                    chanMenh.itemOptions.add(new Item.ItemOption(50, 1));
                                    chanMenh.itemOptions.add(new Item.ItemOption(77, 1));
                                    chanMenh.itemOptions.add(new Item.ItemOption(103, 1));
                                    Service.gI().sendMoney(player);
                                    InventoryServiceNew.gI().addItemBag(player, chanMenh);
                                    InventoryServiceNew.gI().sendItemBags(player);
                                    Service.gI().sendThongBao(player, "|7|Bạn đã nhận được " + chanMenh.template.name);
                                }
                                break;

                        }
                    } else if (mapId == 122) {
                        if (select == 0) {
                            ChangeMapService.gI().changeMapInYard(player, 0, -1, 469);
                        }
                    } else if (mapId == 124) {
                        if (select == 0) {
                            if (player.NguHanhSonPoint >= 500) {
                                player.NguHanhSonPoint -= 500;
                                Item item = ItemService.gI().createNewItem((short) (711));
                                item.itemOptions.add(new Item.ItemOption(49, 25));
                                item.itemOptions.add(new Item.ItemOption(77, 25));
                                item.itemOptions.add(new Item.ItemOption(103, 25));
                                item.itemOptions.add(new Item.ItemOption(207, 0));
                                item.itemOptions.add(new Item.ItemOption(33, 0));
                                InventoryServiceNew.gI().addItemBag(player, item);
                                Service.gI().sendThongBao(player, "Chúc Mừng Bạn Đổi Vật Phẩm Thành Công !");
                            } else {
                                Service.gI().sendThongBao(player, "Không đủ điểm, bạn còn " + (500 - player.pointPvp) + " điểm nữa");
                            }

                        }
                    }

                }
            }
        };
    }

    public static Npc dauThan(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    player.magicTree.openMenuTree();
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    switch (player.iDMark.getIndexMenu()) {
                        case ConstNpc.MAGIC_TREE_NON_UPGRADE_LEFT_PEA:
                            if (select == 0) {
                                player.magicTree.harvestPea();
                            } else if (select == 1) {
                                if (player.magicTree.level == 10) {
                                    player.magicTree.fastRespawnPea();
                                } else {
                                    player.magicTree.showConfirmUpgradeMagicTree();
                                }
                            } else if (select == 2) {
                                player.magicTree.fastRespawnPea();
                            }
                            break;
                        case ConstNpc.MAGIC_TREE_NON_UPGRADE_FULL_PEA:
                            if (select == 0) {
                                player.magicTree.harvestPea();
                            } else if (select == 1) {
                                player.magicTree.showConfirmUpgradeMagicTree();
                            }
                            break;
                        case ConstNpc.MAGIC_TREE_CONFIRM_UPGRADE:
                            if (select == 0) {
                                player.magicTree.upgradeMagicTree();
                            }
                            break;
                        case ConstNpc.MAGIC_TREE_UPGRADE:
                            if (select == 0) {
                                player.magicTree.fastUpgradeMagicTree();
                            } else if (select == 1) {
                                player.magicTree.showConfirmUnuppgradeMagicTree();
                            }
                            break;
                        case ConstNpc.MAGIC_TREE_CONFIRM_UNUPGRADE:
                            if (select == 0) {
                                player.magicTree.unupgradeMagicTree();
                            }
                            break;
                    }
                }
            }
        };
    }

    public static Npc calick(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            private final byte COUNT_CHANGE = 50;
            private int count;

            private void changeMap() {
                if (this.mapId != 102) {
                    count++;
                    if (this.count >= COUNT_CHANGE) {
                        count = 0;
                        this.map.npcs.remove(this);
                        Map map = MapService.gI().getMapForCalich();
                        this.mapId = map.mapId;
                        this.cx = Util.nextInt(100, map.mapWidth - 100);
                        this.cy = map.yPhysicInTop(this.cx, 0);
                        this.map = map;
                        this.map.npcs.add(this);
                    }
                }
            }

            @Override
            public void openBaseMenu(Player player) {
                player.iDMark.setIndexMenu(ConstNpc.BASE_MENU);
                if (TaskService.gI().getIdTask(player) < ConstTask.TASK_20_0) {
                    Service.gI().hideWaitDialog(player);
                    Service.gI().sendThongBao(player, "Không thể thực hiện");
                    return;
                }
                if (this.mapId != player.zone.map.mapId) {
                    Service.gI().sendThongBao(player, "Calích đã rời khỏi map!");
                    Service.gI().hideWaitDialog(player);
                    return;
                }

                if (this.mapId == 102) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU,
                            "Chào chú, cháu có thể giúp gì?",
                            "Kể\nChuyện", "Quay về\nQuá khứ");
                } else {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU,
                            "Chào chú, cháu có thể giúp gì?", "Kể\nChuyện", "Đi đến\nTương lai", "Từ chối");
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (this.mapId == 102) {
                    if (player.iDMark.isBaseMenu()) {
                        if (select == 0) {
                            //kể chuyện
                            NpcService.gI().createTutorial(player, this.avartar, ConstNpc.CALICK_KE_CHUYEN);
                        } else if (select == 1) {
                            //về quá khứ
                            ChangeMapService.gI().goToQuaKhu(player);
                        }
                    }
                } else if (player.iDMark.isBaseMenu()) {
                    if (select == 0) {
                        //kể chuyện
                        NpcService.gI().createTutorial(player, this.avartar, ConstNpc.CALICK_KE_CHUYEN);
                    } else if (select == 1) {
                        //đến tương lai
//                                    changeMap();
                        if (TaskService.gI().getIdTask(player) >= ConstTask.TASK_20_0) {
                            ChangeMapService.gI().goToTuongLai(player);
                        }
                    } else {
                        Service.gI().sendThongBao(player, "Không thể thực hiện");
                    }
                }
            }
        };
    }

    public static Npc jaco(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 24 || this.mapId == 25 || this.mapId == 26) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Gô Tên, Calich và Monaka đang gặp chuyện ở hành tinh Potaufeu \n Hãy đến đó ngay", "Đến \nPotaufeu");
                    } else if (this.mapId == 139) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Người muốn trở về?", "Quay về", "Từ chối");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 24 || this.mapId == 25 || this.mapId == 26) {
                        if (player.getSession().player.nPoint.power >= 800000000L) {

                            ChangeMapService.gI().goToPotaufeu(player);
                        } else {
                            this.npcChat(player, "Bạn chưa đủ 800tr sức mạnh để vào!");
                        }
                    } else if (this.mapId == 139) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                //về trạm vũ trụ
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 24 + player.gender, -1, -1);
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

//public static Npc Potage(int mapId, int status, int cx, int cy, int tempId, int avartar) {
//        return new Npc(mapId, status, cx, cy, tempId, avartar) {
//            @Override
//            public void openBaseMenu(Player player) {
//                if (canOpenNpc(player)) {
//                    if (this.mapId == 149) {
//                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
//                                "tét", "Gọi nhân bản");
//                    }
//                }
//            }
//            @Override
//            public void confirmMenu(Player player, int select) {
//                if (canOpenNpc(player)) {
//                   if (select == 0){
//                        BossManager.gI().createBoss(-214);
//                   }
//                }
//            }
//        };
//    }
    public static Npc npclytieunuong54(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    createOtherMenu(player, ConstNpc.BASE_MENU,
                            "Xin chào, ta có một số vật phẩm đặt biệt cậu có muốn xem không?",
                            "Săn Đệ Tử", "Quy đổi");
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5 || this.mapId == 13 || this.mapId == 20) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    Service.gI().sendThongBaoOK(player, "Broly xuất hiện ở các map ấy hãy tìm Broly để kích Super Broly như server Teamobi");
                                    break;
                                case 1:
                                    this.createOtherMenu(player, ConstNpc.QUY_DOI, "|7|Số tiền của bạn còn : " + player.getSession().coinBar + " VND\n"
                                            + "Tỉ lệ quy đổi là 1000VND = 4 thỏi vàng\n" + "1000VND = 2000 hồng ngọc\n Ví Dụ Có 10.000VND Thì Nhập Vào Là 10\nCứ Quy Đổi Kể Cả 1 Thỏi Vàng Là Được Kích Hoạt Tài Khoản\nQuy Đổi Lỗi Thì Quy Đổi Lại Lần 2", "Quy đổi\n Thỏi vàng", "Quy Đổi\nHồng Ngọc");
                                    break;
                            }
//                        }

                        } else if (player.iDMark.getIndexMenu() == ConstNpc.QUY_DOI) {
                            switch (select) {
                                case 0:
                                    Input.gI().createFormQDTV(player);
                                    break;
                                case 1:
                                    Input.gI().createFormQDHN(player);
                                    break;
                            }
                        }
                    }
                }
            }

        };
    }

    public static Npc thuongDe(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 177) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Con muốn làm gì nào", "Di Chuyển", "Quay số\nmay mắn");
                    }
                    if (this.mapId == 129) {
                        this.createOtherMenu(player, 0,
                                "Con muốn gì nào?", "Quay ve");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 177) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    this.createOtherMenu(player, 171,
                                            "Con muốn làm gì nào", "Đến Kaio", "Quay Về Tháp Karin");
                                    break;
                                case 1:
                                    this.createOtherMenu(player, ConstNpc.MENU_CHOOSE_LUCKY_ROUND,
                                            "Con muốn làm gì nào?", "Quay bằng\nvàng",
                                            "Rương phụ\n("
                                            + (player.inventory.itemsBoxCrackBall.size()
                                            - InventoryServiceNew.gI().getCountEmptyListItem(player.inventory.itemsBoxCrackBall))
                                            + " món)",
                                            "Xóa hết\ntrong rương", "Đóng");
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == 171) {
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 48, -1, 354);
                                    break;
                                case 1:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 176, -1, 345);
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_CHOOSE_LUCKY_ROUND) {
                            switch (select) {
                                case 0:
                                    LuckyRound.gI().openCrackBallUI(player, LuckyRound.USING_GOLD);
                                    break;
                                case 1:
                                    ShopServiceNew.gI().opendShop(player, "ITEMS_LUCKY_ROUND", true);
                                    break;
                                case 2:
                                    NpcService.gI().createMenuConMeo(player,
                                            ConstNpc.CONFIRM_REMOVE_ALL_ITEM_LUCKY_ROUND, this.avartar,
                                            "Con có chắc muốn xóa hết vật phẩm trong rương phụ? Sau khi xóa "
                                            + "sẽ không thể khôi phục!",
                                            "Đồng ý", "Hủy bỏ");
                                    break;
                            }
                        }
                    } else if (this.mapId == 129) {
                        switch (select) {
                            case 0:
                                ChangeMapService.gI().changeMapBySpaceShip(player, 0, -1, 354);
                                break;
                        }
                    }

                }
            }
        };
    }

    public static Npc thanVuTru(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 48) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Con muốn làm gì nào", "Di chuyển");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 48) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    this.createOtherMenu(player, ConstNpc.MENU_DI_CHUYEN,
                                            "Con muốn đi đâu?", "Về\nthần điện", "Thánh địa\nKaio", "Con\nđường\nrắn độc", "Từ chối");
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_DI_CHUYEN) {
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 177, -1, 429);
                                    break;
                                case 1:
                                    ChangeMapService.gI().changeMap(player, 50, -1, 318, 336);
                                    break;
                                case 2:
                                    if (player.clan != null) {
                                        if (player.clan.ConDuongRanDoc != null) {
                                            this.createOtherMenu(player, ConstNpc.MENU_OPENED_CDRD,
                                                    "Bang hội của con đang đi con đường rắn độc cấp độ "
                                                    + player.clan.ConDuongRanDoc.level + "\nCon có muốn đi theo không?",
                                                    "Đồng ý", "Từ chối");
                                        } else {
                                            this.createOtherMenu(player, ConstNpc.MENU_OPEN_CDRD,
                                                    "Đây là Con đường rắn độc \nCác con cứ yên tâm lên đường\n"
                                                    + "Ở đây có ta lo\nNhớ chọn cấp độ vừa sức mình nhé",
                                                    "Chọn\ncấp độ", "Từ chối");
                                        }
                                    } else {
                                        this.npcChat(player, "Con phải có bang hội ta mới có thể cho con đi");
                                    }
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPENED_CDRD) {
                            switch (select) {
                                case 0:
                                    if (player.nPoint.power >= ConDuongRanDoc.POWER_CAN_GO_TO_CDRD) {
                                        ChangeMapService.gI().goToCDRD(player);
                                    } else {
                                        Service.gI().sendThongBao(player, "Không đủ sức mạnh yêu cầu");
                                    }
                                    if (player.clan.haveGoneConDuongRanDoc) {
                                        createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                                "Bang hội của ngươi đã đi con đường rắn độc lúc " + TimeUtil.formatTime(player.clan.lastTimeOpenConDuongRanDoc, "HH:mm:ss") + " hôm nay. Người mở\n"
                                                + "(" + player.clan.playerOpenDoanhTrai + "). Hẹn ngươi quay lại vào ngày mai", "OK", "Hướng\ndẫn\nthêm");
                                        return;
                                    } else if (player.clanMember.getNumDateFromJoinTimeToToday() < 2) {
                                        Service.gI().sendThongBao(player, "Yêu cầu tham gia bang hội trên 2 ngày!");
                                    } else {
                                        this.npcChat(player, "Sức mạnh của con phải ít nhất phải đạt "
                                                + Util.numberToMoney(ConDuongRanDoc.POWER_CAN_GO_TO_CDRD));
                                    }
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPEN_CDRD) {
                            switch (select) {
                                case 0:
                                    if (player.isAdmin() || player.nPoint.power >= ConDuongRanDoc.POWER_CAN_GO_TO_CDRD) {
                                        Input.gI().createFormChooseLevelCDRD(player);
                                    } else {
                                        this.npcChat(player, "Sức mạnh của con phải ít nhất phải đạt "
                                                + Util.numberToMoney(ConDuongRanDoc.POWER_CAN_GO_TO_CDRD));
                                    }
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_ACCEPT_GO_TO_CDRD) {
                            switch (select) {
                                case 0:
                                    ConDuongRanDocService.gI().openConDuongRanDoc(player, Byte.parseByte(String.valueOf(PLAYERID_OBJECT.get(player.id))));
                                    break;
                            }
                        }
                    }
                }
            }

        };
    }

    public static Npc kibit(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 50) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Ta có thể giúp gì cho ngươi ?",
                                "Đến\nKaio", "Từ chối");
                    } else {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Ta có thể giúp gì cho ngươi ?",
                                "Từ chối");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 50) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMap(player, 48, -1, 354, 240);
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc osin(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 50) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Ta có thể giúp gì cho ngươi ?",
                                "Đến\nKaio", "Đến\nhành tinh\nBill", "Từ chối");
                    } else if (this.mapId == 154) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Ta có thể giúp gì cho ngươi ?",
                                "Về thánh địa", "Đến\nhành tinh\nngục tù", "Từ chối");
                    } else if (this.mapId == 155) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Ta có thể giúp gì cho ngươi ?",
                                "Quay về", "Từ chối");
                    } else if (this.mapId == 179) {
                        try {
                            MapMaBu.gI().setTimeJoinMapMaBu();
                            if (this.mapId == 179) {
                                long now = System.currentTimeMillis();
                                if (now > MapMaBu.TIME_OPEN_MABU && now < MapMaBu.TIME_CLOSE_MABU) {
                                    this.createOtherMenu(player, ConstNpc.MENU_OPEN_MMB, "Đại chiến Ma Bư đã mở, "
                                            + "ngươi có muốn tham gia không?",
                                            "Hướng dẫn\nthêm", "Tham gia", "Từ chối");
                                } else {
                                    this.createOtherMenu(player, ConstNpc.MENU_NOT_OPEN_MMB,
                                            "Ta có thể giúp gì cho ngươi?", "Hướng dẫn", "Từ chối");
                                }

                            }
                        } catch (Exception ex) {
                            Logger.error("Lỗi mở menu osin");
                        }

                    } else if (this.mapId >= 114 && this.mapId < 120 && this.mapId != 116) {
                        if (player.fightMabu.pointMabu >= player.fightMabu.POINT_MAX) {
                            this.createOtherMenu(player, ConstNpc.GO_UPSTAIRS_MENU, "Ta có thể giúp gì cho ngươi ?",
                                    "Lên Tầng!", "Quay về", "Từ chối");
                        } else {
                            this.createOtherMenu(player, ConstNpc.BASE_MENU, "Ta có thể giúp gì cho ngươi ?",
                                    "Quay về", "Từ chối");
                        }
                    } else if (this.mapId == 120) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Ta có thể giúp gì cho ngươi ?",
                                "Quay về", "Từ chối");
                    } else {
                        super.openBaseMenu(player);
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 50) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMap(player, 48, -1, 354, 240);
                                    break;
                                case 1:
                                    ChangeMapService.gI().changeMap(player, 154, -1, 200, 312);
                                    break;
                            }
                        }
                    } else if (this.mapId == 154) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    if (player.nPoint.power >= 80000000000L) {
                                        ChangeMapService.gI().changeMap(player, 50, -1, 318, 336);
                                    } else {
                                        Service.gI().sendThongBaoOK(player, "Yêu Cầu 80 Tỷ Sức Mạnh");
                                    }
                                    break;
                                case 1:
                                    if (player.nPoint.power >= 80000000000L) {
                                        ChangeMapService.gI().changeMap(player, 155, -1, 111, 792);
                                    } else {
                                        Service.gI().sendThongBaoOK(player, "Yêu Cầu 80 Tỷ Sức Mạnh");
                                    }
                                    break;
                            }
                        }
                    } else if (this.mapId == 155) {
                        if (player.iDMark.isBaseMenu()) {
                            if (select == 0) {
                                if (player.nPoint.power >= 80000000000L) {
                                    ChangeMapService.gI().changeMap(player, 154, -1, 200, 312);
                                } else {
                                    Service.gI().sendThongBaoOK(player, "Yêu Cầu 80 Tỷ Sức Mạnh");
                                }

                            }
                        }
                    } else if (this.mapId == 179) {
                        switch (player.iDMark.getIndexMenu()) {
                            case ConstNpc.MENU_REWARD_MMB:
                                break;
                            case ConstNpc.MENU_OPEN_MMB:
                                if (select == 0) {
                                    NpcService.gI().createTutorial(player, this.avartar, ConstNpc.HUONG_DAN_MAP_MA_BU);
                                } else if (select == 1) {
//                                    if (!player.getSession().actived) {
//                                        Service.gI().sendThongBao(player, "Vui lòng kích hoạt tài khoản để sử dụng chức năng này");
//                                    } else
                                    ChangeMapService.gI().changeMap(player, 114, -1, 318, 336);
                                }
                                break;
                            case ConstNpc.MENU_NOT_OPEN_BDW:
                                if (select == 0) {
                                    NpcService.gI().createTutorial(player, this.avartar, ConstNpc.HUONG_DAN_MAP_MA_BU);
                                }
                                break;
                        }
                    } else if (this.mapId >= 114 && this.mapId < 120 && this.mapId != 116) {
                        if (player.iDMark.getIndexMenu() == ConstNpc.GO_UPSTAIRS_MENU) {
                            if (select == 0) {
                                player.fightMabu.clear();
                                ChangeMapService.gI().changeMap(player, this.map.mapIdNextMabu((short) this.mapId), -1, this.cx, this.cy);
                            } else if (select == 1) {
                                ChangeMapService.gI().changeMapBySpaceShip(player, player.gender + 21, 0, -1);
                            }
                        } else {
                            if (select == 0) {
                                ChangeMapService.gI().changeMapBySpaceShip(player, player.gender + 21, 0, -1);
                            }
                        }
                    } else if (this.mapId == 120) {
                        if (player.iDMark.getIndexMenu() == ConstNpc.BASE_MENU) {
                            if (select == 0) {
                                ChangeMapService.gI().changeMapBySpaceShip(player, player.gender + 21, 0, -1);
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc linhCanh(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (player.clan == null) {
                        this.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Chỉ tiếp các bang hội, miễn tiếp khách vãng lai", "Đóng");
                        return;
                    }
                    if (player.clan.getMembers().size() < DoanhTrai.N_PLAYER_CLAN) {
                        this.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Bang hội phải có ít nhất 5 thành viên mới có thể mở", "Đóng");
                        return;
                    }
                    if (player.clan.doanhTrai != null) {
                        createOtherMenu(player, ConstNpc.MENU_JOIN_DOANH_TRAI,
                                "Bang hội của ngươi đang đánh trại độc nhãn\n"
                                + "Thời gian còn lại là "
                                + TimeUtil.getSecondLeft(player.clan.doanhTrai.getLastTimeOpen(), DoanhTrai.TIME_DOANH_TRAI / 1000)
                                + ". Ngươi có muốn tham gia không?",
                                "Tham gia", "Không", "Hướng\ndẫn\nthêm");
                        return;
                    }
                    int nPlSameClan = 0;
                    for (Player pl : player.zone.getPlayers()) {
                        if (!pl.equals(player) && pl.clan != null
                                && pl.clan.equals(player.clan) && pl.location.x >= 1285
                                && pl.location.x <= 1645) {
                            nPlSameClan++;
                        }
                    }
                    if (nPlSameClan < DoanhTrai.N_PLAYER_MAP) {
                        createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Ngươi phải có ít nhất " + DoanhTrai.N_PLAYER_MAP + " đồng đội cùng bang đứng gần mới có thể\nvào\n"
                                + "tuy nhiên ta khuyên ngươi nên đi cùng với 3-4 người để khỏi chết.\n"
                                + "Hahaha.", "OK", "Hướng\ndẫn\nthêm");
                        return;
                    }
                    if (player.clanMember.getNumDateFromJoinTimeToToday() < 1) {
                        createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Doanh trại chỉ cho phép những người ở trong bang trên 1 ngày. Hẹn ngươi quay lại vào lúc khác",
                                "OK", "Hướng\ndẫn\nthêm");
                        return;
                    }
                    if (player.clan.haveGoneDoanhTrai) {
                        createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Bang hội của ngươi đã đi trại lúc " + TimeUtil.formatTime(player.clan.lastTimeOpenDoanhTrai, "HH:mm:ss") + " hôm nay. Người mở\n"
                                + "(" + player.clan.playerOpenDoanhTrai + "). Hẹn ngươi quay lại vào ngày mai", "OK", "Hướng\ndẫn\nthêm");
                        return;
                    }
                    createOtherMenu(player, ConstNpc.MENU_JOIN_DOANH_TRAI,
                            "Hôm nay bang hội của ngươi chưa vào trại lần nào. Ngươi có muốn vào\n"
                            + "không?\nĐể vào, ta khuyên ngươi nên có 3-4 người cùng bang đi cùng",
                            "Vào\n(miễn phí)", "Không", "Hướng\ndẫn\nthêm");
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    switch (player.iDMark.getIndexMenu()) {
                        case ConstNpc.MENU_JOIN_DOANH_TRAI:
                            if (select == 0) {
                                DoanhTraiService.gI().joinDoanhTrai(player);
                            } else if (select == 2) {
                                NpcService.gI().createTutorial(player, this.avartar, ConstNpc.HUONG_DAN_DOANH_TRAI);
                            }
                            break;
                        case ConstNpc.IGNORE_MENU:
                            if (select == 1) {
                                NpcService.gI().createTutorial(player, this.avartar, ConstNpc.HUONG_DAN_DOANH_TRAI);
                            }
                            break;
                    }
                }
            }
        };
    }

    public static Npc quaTrung(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            private final int COST_AP_TRUNG_NHANH = 1000000000;

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == (21 + player.gender)) {
                        player.mabuEgg.sendMabuEgg();
                        if (player.mabuEgg.getSecondDone() != 0) {
                            this.createOtherMenu(player, ConstNpc.CAN_NOT_OPEN_EGG, "Bư bư bư...",
                                    "Hủy bỏ\ntrứng", "Ấp nhanh\n" + Util.numberToMoney(COST_AP_TRUNG_NHANH) + " vàng", "Đóng");
                        } else {
                            this.createOtherMenu(player, ConstNpc.CAN_OPEN_EGG, "Bư bư bư...", "Nở", "Hủy bỏ\ntrứng", "Đóng");
                        }
                    }
                    if (this.mapId == 7) {
                        player.billEgg.sendBillEgg();
                        if (player.billEgg.getSecondDone() != 0) {
                            this.createOtherMenu(player, ConstNpc.CAN_NOT_OPEN_EGG, "Bư bư bư...",
                                    "Hủy bỏ\ntrứng", "Ấp nhanh\n" + Util.numberToMoney(COST_AP_TRUNG_NHANH) + " vàng", "Đóng");
                        } else {
                            this.createOtherMenu(player, ConstNpc.CAN_OPEN_EGG, "Bư bư bư...", "Nở", "Hủy bỏ\ntrứng", "Đóng");
                        }
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == (21 + player.gender)) {
                        switch (player.iDMark.getIndexMenu()) {
                            case ConstNpc.CAN_NOT_OPEN_EGG:
                                if (select == 0) {
                                    this.createOtherMenu(player, ConstNpc.CONFIRM_DESTROY_EGG,
                                            "Bạn có chắc chắn muốn hủy bỏ trứng Mabư?", "Đồng ý", "Từ chối");
                                } else if (select == 1) {
                                    if (player.inventory.gold >= COST_AP_TRUNG_NHANH) {
                                        player.inventory.gold -= COST_AP_TRUNG_NHANH;
                                        player.mabuEgg.timeDone = 0;
                                        Service.gI().sendMoney(player);
                                        player.mabuEgg.sendMabuEgg();
                                    } else {
                                        Service.gI().sendThongBao(player,
                                                "Bạn không đủ vàng để thực hiện, còn thiếu "
                                                + Util.numberToMoney((COST_AP_TRUNG_NHANH - player.inventory.gold)) + " vàng");
                                    }
                                }
                                break;
                            case ConstNpc.CAN_OPEN_EGG:
                                switch (select) {
                                    case 0:
                                        this.createOtherMenu(player, ConstNpc.CONFIRM_OPEN_EGG,
                                                "Bạn có chắc chắn cho trứng nở?\n"
                                                + "Đệ tử của bạn sẽ được thay thế bằng đệ Mabư",
                                                "Đệ mabư\nTrái Đất", "Đệ mabư\nNamếc", "Đệ mabư\nXayda", "Từ chối");
                                        break;
                                    case 1:
                                        this.createOtherMenu(player, ConstNpc.CONFIRM_DESTROY_EGG,
                                                "Bạn có chắc chắn muốn hủy bỏ trứng Mabư?", "Đồng ý", "Từ chối");
                                        break;
                                }
                                break;
                            case ConstNpc.CONFIRM_OPEN_EGG:
                                switch (select) {
                                    case 0:
                                        player.mabuEgg.openEgg(ConstPlayer.TRAI_DAT);
                                        break;
                                    case 1:
                                        player.mabuEgg.openEgg(ConstPlayer.NAMEC);
                                        break;
                                    case 2:
                                        player.mabuEgg.openEgg(ConstPlayer.XAYDA);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ConstNpc.CONFIRM_DESTROY_EGG:
                                if (select == 0) {
                                    player.mabuEgg.destroyEgg();
                                }
                                break;
                        }
                    }
                    /*  if (this.mapId == 7) {
                        switch (player.iDMark.getIndexMenu()) {
                            case ConstNpc.CAN_NOT_OPEN_BILL:
                                if (select == 0) {
                                    this.createOtherMenu(player, ConstNpc.CONFIRM_DESTROY_BILL,
                                            "Bạn có chắc chắn muốn hủy bỏ trứng Mabư?", "Đồng ý", "Từ chối");
                                } else if (select == 1) {
                                    if (player.inventory.gold >= COST_AP_TRUNG_NHANH) {
                                        player.inventory.gold -= COST_AP_TRUNG_NHANH;
                                        player.billEgg.timeDone = 0;
                                        Service.gI().sendMoney(player);
                                        player.billEgg.sendBillEgg();
                                    } else {
                                        Service.gI().sendThongBao(player,
                                                "Bạn không đủ vàng để thực hiện, còn thiếu "
                                                + Util.numberToMoney((COST_AP_TRUNG_NHANH - player.inventory.gold)) + " vàng");
                                    }
                                }
                                break;
                            case ConstNpc.CAN_OPEN_EGG:
                                switch (select) {
                                    case 0:
                                        this.createOtherMenu(player, ConstNpc.CONFIRM_OPEN_BILL,
                                                "Bạn có chắc chắn cho trứng nở?\n"
                                                + "Đệ tử của bạn sẽ được thay thế bằng đệ Mabư",
                                                "Đệ mabư\nTrái Đất", "Đệ mabư\nNamếc", "Đệ mabư\nXayda", "Từ chối");
                                        break;
                                    case 1:
                                        this.createOtherMenu(player, ConstNpc.CONFIRM_DESTROY_BILL,
                                                "Bạn có chắc chắn muốn hủy bỏ trứng Mabư?", "Đồng ý", "Từ chối");
                                        break;
                                }
                                break;
                            case ConstNpc.CONFIRM_OPEN_BILL:
                                switch (select) {
                                    case 0:
                                        player.billEgg.openEgg(ConstPlayer.TRAI_DAT);
                                        break;
                                    case 1:
                                        player.billEgg.openEgg(ConstPlayer.NAMEC);
                                        break;
                                    case 2:
                                        player.billEgg.openEgg(ConstPlayer.XAYDA);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ConstNpc.CONFIRM_DESTROY_BILL:
                                if (select == 0) {
                                    player.billEgg.destroyEgg();
                                }
                                break;
                        }
                    }
                     */
                }
            }
        };
    }

    public static Npc duahau(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            private final int COST_AP_TRUNG_NHANH = 1000000000;

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {

                    if (this.mapId == 7 * player.gender) {
                        player.billEgg.sendBillEgg();
                        if (player.billEgg.getSecondDone() != 0) {
                            this.createOtherMenu(player, ConstNpc.CAN_NOT_OPEN_EGG, "Mang Đến Gặp Vua Hùng Để Được Những Món Quà Vô Giá...",
                                    "Thu Hoạch\nSớm" + Util.numberToMoney(COST_AP_TRUNG_NHANH) + " vàng", "Đóng");
                        } else {
                            this.createOtherMenu(player, ConstNpc.CAN_OPEN_EGG, "Mau thu hoạch nào...", "Thu Hoạch", "Đóng");
                        }
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {

                    if (this.mapId == 7 * player.gender) {
                        switch (player.iDMark.getIndexMenu()) {
                            case ConstNpc.CAN_NOT_OPEN_BILL:
                                if (select == 0) {
                                    if (player.inventory.gold >= COST_AP_TRUNG_NHANH) {
                                        player.inventory.gold -= COST_AP_TRUNG_NHANH;
                                        player.billEgg.timeDone = 0;
                                        Service.gI().sendMoney(player);
                                        player.billEgg.sendBillEgg();
                                    } else {
                                        Service.gI().sendThongBao(player,
                                                "Bạn không đủ vàng để thực hiện, còn thiếu "
                                                + Util.numberToMoney((COST_AP_TRUNG_NHANH - player.inventory.gold)) + " vàng");
                                    }
                                }
                                break;
                            case ConstNpc.CAN_OPEN_EGG:
                                switch (select) {
                                    case 0:
                                        this.createOtherMenu(player, ConstNpc.CONFIRM_OPEN_BILL,
                                                "ôi bạn ơi?\n"
                                                + "Chọn Một Trong Những Món Quà Giá Trị Nào",
                                                "Ngọc Rồng\nTorobo", "Dưa\nHấu", "Ngọc\nBội", "Từ chối");
                                        break;
                                }
                                break;
                            case ConstNpc.CONFIRM_OPEN_BILL:
                                switch (select) {
                                    case 0:
                                        ItemService.gI().openBoxtorobo(player);
                                        player.billEgg.destroyEgg();
//                                        player.billEgg.openEgg(ConstPlayer.TRAI_DAT);
                                        break;
                                    case 1:
                                        ItemService.gI().openBoxdua(player);
                                        player.billEgg.destroyEgg();
//                                if (player.inventory.ruby == 10000) {
//                                    this.npcChat(player, "Bú ít thôi con");
//                                    break;
//                                }
//                                player.inventory.ruby = 100;
//                                Service.gI().sendMoney(player);
//                                Service.gI().sendThongBao(player, "Bạn vừa nhận được 200K Hồng Ngọc");
                                        break;
                                    case 2://2072
                                        ItemService.gI().openBoxngocboi(player);
                                        player.billEgg.destroyEgg();
//                                        player.billEgg.openEgg(ConstPlayer.XAYDA);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case ConstNpc.CONFIRM_DESTROY_BILL:
                                if (select == 0) {
                                    player.billEgg.destroyEgg();
                                }
                                break;
                        }
                    }

                }
            }
        };
    }

    public static Npc quocVuong(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            @Override
            public void openBaseMenu(Player player) {
                this.createOtherMenu(player, ConstNpc.BASE_MENU,
                        "Con muốn nâng giới hạn sức mạnh cho bản thân hay đệ tử?",
                        "Bản thân", "Đệ tử", "Đóng");
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isBaseMenu()) {
                        switch (select) {
                            case 0:
                                if (player.nPoint.limitPower < NPoint.MAX_LIMIT) {
                                    this.createOtherMenu(player, ConstNpc.OPEN_POWER_MYSEFT,
                                            "Ta sẽ truền năng lượng giúp con mở giới hạn sức mạnh của bản thân lên "
                                            + Util.numberToMoney(player.nPoint.getPowerNextLimit()),
                                            "Nâng\ngiới hạn\nsức mạnh",
                                            "Nâng ngay\n" + Util.numberToMoney(OpenPowerService.COST_SPEED_OPEN_LIMIT_POWER) + " vàng", "Đóng");
                                } else {
                                    this.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                            "Sức mạnh của con đã đạt tới giới hạn",
                                            "Đóng");
                                }
                                break;
                            case 1:
                                if (player.pet != null) {
                                    if (player.pet.nPoint.limitPower < NPoint.MAX_LIMIT) {
                                        this.createOtherMenu(player, ConstNpc.OPEN_POWER_PET,
                                                "Ta sẽ truền năng lượng giúp con mở giới hạn sức mạnh của đệ tử lên "
                                                + Util.numberToMoney(player.pet.nPoint.getPowerNextLimit()),
                                                "Nâng ngay\n" + Util.numberToMoney(OpenPowerService.COST_SPEED_OPEN_LIMIT_POWER) + " vàng", "Đóng");
                                    } else {
                                        this.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                                "Sức mạnh của đệ con đã đạt tới giới hạn",
                                                "Đóng");
                                    }
                                } else {
                                    Service.gI().sendThongBao(player, "Không thể thực hiện");
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.OPEN_POWER_MYSEFT) {
                        switch (select) {
                            case 0:
                                OpenPowerService.gI().openPowerBasic(player);
                                break;
                            case 1:
                                if (player.inventory.gold >= OpenPowerService.COST_SPEED_OPEN_LIMIT_POWER) {
                                    if (OpenPowerService.gI().openPowerSpeed(player)) {
                                        player.inventory.gold -= OpenPowerService.COST_SPEED_OPEN_LIMIT_POWER;
                                        Service.gI().sendMoney(player);
                                    }
                                } else {
                                    Service.gI().sendThongBao(player,
                                            "Bạn không đủ vàng để mở, còn thiếu "
                                            + Util.numberToMoney((OpenPowerService.COST_SPEED_OPEN_LIMIT_POWER - player.inventory.gold)) + " vàng");
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.OPEN_POWER_PET) {
                        if (select == 0) {
                            if (player.inventory.gold >= OpenPowerService.COST_SPEED_OPEN_LIMIT_POWER) {
                                if (OpenPowerService.gI().openPowerSpeed(player.pet)) {
                                    player.inventory.gold -= OpenPowerService.COST_SPEED_OPEN_LIMIT_POWER;
                                    Service.gI().sendMoney(player);
                                }
                            } else {
                                Service.gI().sendThongBao(player,
                                        "Bạn không đủ vàng để mở, còn thiếu "
                                        + Util.numberToMoney((OpenPowerService.COST_SPEED_OPEN_LIMIT_POWER - player.inventory.gold)) + " vàng");
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc bulmaTL(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 102) {
                        if (player.getSession().player.playerTask.taskMain.id == 30) {
                            if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                                this.createOtherMenu(player, ConstNpc.BASE_MENU, "Cậu bé muốn mua gì nào?", "Cửa hàng", "Tới Trường Học", "Đóng");
                            }
                        } else {
                            if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                                this.createOtherMenu(player, ConstNpc.BASE_MENU, "Cậu bé muốn mua gì nào?", "Cửa hàng", "Đóng");
                            }
                        }
                    } else if (this.mapId == 184) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Cậu bé muốn gì nào?", "Quay Về", "Đóng");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 102) {
                        if (player.iDMark.isBaseMenu()) {
                            if (player.getSession().player.playerTask.taskMain.id == 30) {
                                if (select == 0) {
                                    ShopServiceNew.gI().opendShop(player, "BUNMA_FUTURE", true);
                                }
                                if (select == 1) {
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 184, -1, 331);
                                }
                            } else {
                                if (select == 0) {
                                    ShopServiceNew.gI().opendShop(player, "BUNMA_FUTURE", true);
                                }
                            }
                        }
                    } else if (this.mapId == 184) {
                        if (player.iDMark.isBaseMenu()) {
                            if (select == 0) {
                                ChangeMapService.gI().changeMapBySpaceShip(player, 102, -1, 350);
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc rongOmega(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    BlackBallWar.gI().setTime();
                    if (this.mapId == 24 || this.mapId == 25 || this.mapId == 26) {
                        try {
                            long now = System.currentTimeMillis();
                            if (now > BlackBallWar.TIME_OPEN && now < BlackBallWar.TIME_CLOSE) {
                                this.createOtherMenu(player, ConstNpc.MENU_OPEN_BDW, "Đường đến với ngọc rồng sao đen đã mở, "
                                        + "ngươi có muốn tham gia không?",
                                        "Hướng dẫn\nthêm", "Tham gia", "Từ chối");
                            } else {
                                String[] optionRewards = new String[7];
                                int index = 0;
                                for (int i = 0; i < 7; i++) {
                                    if (player.rewardBlackBall.timeOutOfDateReward[i] > System.currentTimeMillis()) {
                                        String quantily = player.rewardBlackBall.quantilyBlackBall[i] > 1 ? "x" + player.rewardBlackBall.quantilyBlackBall[i] + " " : "";
                                        optionRewards[index] = quantily + (i + 1) + " sao";
                                        index++;
                                    }
                                }
                                if (index != 0) {
                                    String[] options = new String[index + 1];
                                    for (int i = 0; i < index; i++) {
                                        options[i] = optionRewards[i];
                                    }
                                    options[options.length - 1] = "Từ chối";
                                    this.createOtherMenu(player, ConstNpc.MENU_REWARD_BDW, "Ngươi có một vài phần thưởng ngọc "
                                            + "rồng sao đen đây!",
                                            options);
                                } else {
                                    this.createOtherMenu(player, ConstNpc.MENU_NOT_OPEN_BDW,
                                            "Ta có thể giúp gì cho ngươi?", "Hướng dẫn", "Từ chối");
                                }
                            }
                        } catch (Exception ex) {
                            Logger.error("Lỗi mở menu rồng Omega");
                        }
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    switch (player.iDMark.getIndexMenu()) {
                        case ConstNpc.MENU_REWARD_BDW:
                            player.rewardBlackBall.getRewardSelect((byte) select);
                            break;
                        case ConstNpc.MENU_OPEN_BDW:
                            if (select == 0) {
                                NpcService.gI().createTutorial(player, this.avartar, ConstNpc.HUONG_DAN_BLACK_BALL_WAR);
                            } else if (select == 1) {
//                                if (!player.getSession().actived) {
//                                    Service.gI().sendThongBao(player, "Vui lòng kích hoạt tài khoản để sử dụng chức năng này");
//
//                                } else
                                player.iDMark.setTypeChangeMap(ConstMap.CHANGE_BLACK_BALL);
                                ChangeMapService.gI().openChangeMapTab(player);
                            }
                            break;
                        case ConstNpc.MENU_NOT_OPEN_BDW:
                            if (select == 0) {
                                NpcService.gI().createTutorial(player, this.avartar, ConstNpc.HUONG_DAN_BLACK_BALL_WAR);
                            }
                            break;
                    }
                }
            }

        };
    }

    public static Npc rong1_to_7s(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isHoldBlackBall()) {
                        this.createOtherMenu(player, ConstNpc.MENU_PHU_HP, "Ta có thể giúp gì cho ngươi?", "Phù hộ", "Từ chối");
                    } else {
                        if (BossManager.gI().existBossOnPlayer(player)
                                || player.zone.items.stream().anyMatch(itemMap -> ItemMapService.gI().isBlackBall(itemMap.itemTemplate.id))
                                || player.zone.getPlayers().stream().anyMatch(p -> p.iDMark.isHoldBlackBall())) {
                            this.createOtherMenu(player, ConstNpc.MENU_OPTION_GO_HOME, "Ta có thể giúp gì cho ngươi?", "Về nhà", "Từ chối");
                        } else {
                            this.createOtherMenu(player, ConstNpc.MENU_OPTION_GO_HOME, "Ta có thể giúp gì cho ngươi?", "Về nhà", "Từ chối", "Gọi BOSS");
                        }
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.getIndexMenu() == ConstNpc.MENU_PHU_HP) {
                        if (select == 0) {
                            this.createOtherMenu(player, ConstNpc.MENU_OPTION_PHU_HP,
                                    "Ta sẽ giúp ngươi tăng HP lên mức kinh hoàng, ngươi chọn đi",
                                    "x3 HP\n" + Util.numberToMoney(BlackBallWar.COST_X3) + " vàng",
                                    "x5 HP\n" + Util.numberToMoney(BlackBallWar.COST_X5) + " vàng",
                                    "x7 HP\n" + Util.numberToMoney(BlackBallWar.COST_X7) + " vàng",
                                    "Từ chối"
                            );
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPTION_GO_HOME) {
                        if (select == 0) {
                            ChangeMapService.gI().changeMapBySpaceShip(player, player.gender + 21, -1, 250);
                        } else if (select == 2) {
                            BossManager.gI().callBoss(player, mapId);
                        } else if (select == 1) {
                            this.npcChat(player, "Để ta xem ngươi trụ được bao lâu");
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPTION_PHU_HP) {
                        if (player.effectSkin.xHPKI > 1) {
                            Service.gI().sendThongBao(player, "Bạn đã được phù hộ rồi!");
                            return;
                        }
                        switch (select) {
                            case 0:
                                BlackBallWar.gI().xHPKI(player, BlackBallWar.X3);
                                break;
                            case 1:
                                BlackBallWar.gI().xHPKI(player, BlackBallWar.X5);
                                break;
                            case 2:
                                BlackBallWar.gI().xHPKI(player, BlackBallWar.X7);
                                break;
                            case 3:
                                this.npcChat(player, "Để ta xem ngươi trụ được bao lâu");
                                break;
                        }
                    }
                }
            }
        };
    }

    public static Npc npcThienSu64(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (this.mapId == 14) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU, "Ta sẽ dẫn cậu tới hành tinh Berrus với điều kiện\n 2. đạt 80 tỷ sức mạnh "
                            + "\n 3. chi phí vào cổng  50 triệu vàng", "Tới ngay", "Từ chối");
                }
                if (this.mapId == 7) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU, "Ta sẽ dẫn cậu tới hành tinh Berrus với điều kiện\n 2. đạt 80 tỷ sức mạnh "
                            + "\n 3. chi phí vào cổng  50 triệu vàng", "Tới ngay", "Từ chối");
                }
                if (this.mapId == 0) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU, "Ta sẽ dẫn cậu tới hành tinh Berrus với điều kiện\n 2. đạt 80 tỷ sức mạnh "
                            + "\n 3. chi phí vào cổng  50 triệu vàng", "Tới ngay", "Từ chối");
                }
                if (this.mapId == 146) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU, "Cậu không chịu nổi khi ở đây sao?\nCậu sẽ khó mà mạnh lên được", "Trốn về", "Ở lại");
                }
                if (this.mapId == 147) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU, "Cậu không chịu nổi khi ở đây sao?\nCậu sẽ khó mà mạnh lên được", "Trốn về", "Ở lại");
                }
                if (this.mapId == 148) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU, "Cậu không chịu nổi khi ở đây sao?\nCậu sẽ khó mà mạnh lên được", "Trốn về", "Ở lại");
                }
                if (this.mapId == 48) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU, "Đã tìm đủ nguyên liệu cho tôi chưa?\n Tôi sẽ giúp cậu mạnh lên kha khá đấy!", "Hướng Dẫn",
                            "Đổi Thức Ăn\nLấy Điểm", "Từ Chối");
                }
                if (this.mapId == 154) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU, "Đã tìm đủ nguyên liệu cho tôi chưa?\n Tôi sẽ giúp cậu mạnh lên kha khá đấy!",
                            "Chế Tạo trang bị thiên sứ", "Shop Thiên Sứ", "Đóng");
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isBaseMenu() && this.mapId == 7) {
                        if (select == 0) {
                            if (player.getSession().player.nPoint.power >= 80000000000L && player.inventory.gold > COST_HD) {
                                player.inventory.gold -= COST_HD;
                                Service.gI().sendMoney(player);
                                ChangeMapService.gI().changeMapBySpaceShip(player, 146, -1, 168);
                            } else {
                                this.npcChat(player, "Bạn chưa đủ điều kiện để vào");
                            }
                        }
                        if (select == 1) {
                        }
                    }
                    if (player.iDMark.isBaseMenu() && this.mapId == 14) {
                        if (select == 0) {
                            if (player.getSession().player.nPoint.power >= 80000000000L && player.inventory.gold > COST_HD) {
                                player.inventory.gold -= COST_HD;
                                Service.gI().sendMoney(player);
                                ChangeMapService.gI().changeMapBySpaceShip(player, 148, -1, 168);
                            } else {
                                this.npcChat(player, "Bạn chưa đủ điều kiện để vào");
                            }
                        }
                        if (select == 1) {
                        }
                    }
                    if (player.iDMark.isBaseMenu() && this.mapId == 0) {
                        if (select == 0) {
                            if (player.getSession().player.nPoint.power >= 80000000000L && player.inventory.gold > COST_HD) {
                                player.inventory.gold -= COST_HD;
                                Service.gI().sendMoney(player);
                                ChangeMapService.gI().changeMapBySpaceShip(player, 147, -1, 168);
                            } else {
                                this.npcChat(player, "Bạn chưa đủ điều kiện để vào");
                            }
                        }
                        if (select == 1) {
                        }
                    }
                    if (player.iDMark.isBaseMenu() && this.mapId == 147) {
                        if (select == 0) {
                            ChangeMapService.gI().changeMapBySpaceShip(player, 0, -1, 450);
                        }
                        if (select == 1) {
                        }
                    }
                    if (player.iDMark.isBaseMenu() && this.mapId == 148) {
                        if (select == 0) {
                            ChangeMapService.gI().changeMapBySpaceShip(player, 14, -1, 450);
                        }
                        if (select == 1) {
                        }
                    }
                    if (player.iDMark.isBaseMenu() && this.mapId == 146) {
                        if (select == 0) {
                            ChangeMapService.gI().changeMapBySpaceShip(player, 7, -1, 450);
                        }
                        if (select == 1) {
                        }

                    }
                    if (player.iDMark.isBaseMenu() && this.mapId == 48) {
                        if (select == 0) {
                            this.createOtherMenu(player, ConstNpc.BASE_MENU, "x99 Thức Ăn Được 1 Điểm");
                        }
                        if (select == 1) {
                            CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.DOI_DIEM);
                        }

                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_START_COMBINE) {
                        switch (player.combineNew.typeCombine) {
                            case CombineServiceNew.DOI_DIEM:

                                if (select == 0) {
                                    CombineServiceNew.gI().startCombine(player, 0);
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_PHAN_RA_DO_THAN_LINH) {
                        if (select == 0) {
                            CombineServiceNew.gI().startCombine(player, 0);
                        }

                    }
                    if (player.iDMark.isBaseMenu() && this.mapId == 154) {
                        if (select == 0) {
                            CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.CHE_TAO_TRANG_BI_TS);
                        }
                        if (select == 1) {
                            ShopServiceNew.gI().opendShop(player, "WHIS", true);
                        }

                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_START_COMBINE) {
                        switch (player.combineNew.typeCombine) {
                            case CombineServiceNew.CHE_TAO_TRANG_BI_TS:

                                if (select == 0) {
                                    CombineServiceNew.gI().startCombine(player, 0);
                                }
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_NANG_CAP_DO_TS) {
                        if (select == 0) {
                            CombineServiceNew.gI().startCombine(player, 0);
                        }

                    }
                }
            }

        };
    }

    public static Npc bill(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 48) {
                        createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Gặp Whis Để Đổi Thức Ăn Lấy Điểm Sau Đó Gặp Ta Để Mua Trang Bị Hủy Diệt",
                                "Điểm",
                                "Shop Hủy Diệt", "Đóng");
                    } else {
                        createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Gặp Whis Để Đổi Thức Ăn Lấy Điểm Sau Đó Gặp Ta Để Mua Trang Bị Hủy Diệt",
                                "Đóng");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isBaseMenu()) {
                        if (this.mapId == 48) {
                            switch (select) {
                                case 0:
                                    createOtherMenu(player, ConstNpc.IGNORE_MENU, "Mày Có " + player.inventory.coupon + " Điểm", "Đóng");
                                    break;
                                case 1:
                                    if (player.inventory.coupon == 0) {
                                        createOtherMenu(player, ConstNpc.IGNORE_MENU, "Ngươi Không Có Điểm Vui Lòng Đổi Điểm Bằng Thức Ăn", "Đóng");
                                    } else {
                                        ShopServiceNew.gI().opendShop(player, "BILL", false);
                                        break;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc boMong(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 178 || this.mapId == 84) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU,
                                "Xin chào, cậu muốn tôi giúp gì?", "Nhiệm vụ\nhàng ngày", "Nhiệm Vụ\nThành Tích",
                                "Gift_Code", "Từ chối");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 178 || this.mapId == 84) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    if (player.playerTask.sideTask.template != null) {
                                        String npcSay = "Nhiệm vụ hiện tại: " + player.playerTask.sideTask.getName() + " ("
                                                + player.playerTask.sideTask.getLevel() + ")"
                                                + "\nHiện tại đã hoàn thành: " + player.playerTask.sideTask.count + "/"
                                                + player.playerTask.sideTask.maxCount + " ("
                                                + player.playerTask.sideTask.getPercentProcess() + "%)\nSố nhiệm vụ còn lại trong ngày: "
                                                + player.playerTask.sideTask.leftTask + "/" + ConstTask.MAX_SIDE_TASK;
                                        this.createOtherMenu(player, ConstNpc.MENU_OPTION_PAY_SIDE_TASK,
                                                npcSay, "Trả nhiệm\nvụ", "Hủy nhiệm\nvụ");
                                    } else {
                                        this.createOtherMenu(player, ConstNpc.MENU_OPTION_LEVEL_SIDE_TASK,
                                                "Tôi có vài nhiệm vụ theo cấp bậc, "
                                                + "sức cậu có thể làm được cái nào?",
                                                "Dễ", "Bình thường", "Khó", "Siêu khó", "Địa ngục", "Từ chối");
                                    }
                                    break;
                                case 1:
                                    player.achievement.Show();
                                    break;
                                case 2:
                                    Input.gI().createFormGiftCode(player);
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPTION_LEVEL_SIDE_TASK) {
                            switch (select) {
                                case 0:
                                case 1:
                                case 2:
                                case 3:
                                case 4:
                                    TaskService.gI().changeSideTask(player, (byte) select);
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPTION_PAY_SIDE_TASK) {
                            switch (select) {
                                case 0:
                                    TaskService.gI().paySideTask(player);
                                    break;
                                case 1:
                                    TaskService.gI().removeSideTask(player);
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc karin(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 176) {
                        if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                            this.createOtherMenu(player, ConstNpc.BASE_MENU, "Hê Hê Hê ta là thần mèo Karin?", "Lên Thần Điện", "Xuống Rừng Karin", "Đóng");
                        }
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 176) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMap(player, 177, -1, 384, 384);
                                    break;
                                case 1:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 178, -1, 384);
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc vados(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    createOtherMenu(player, ConstNpc.BASE_MENU,
                            "|2|Ta Vừa Hắc Mắp Xêm Được Tóp Của Toàn Server\b|7|Người Muốn Xem Tóp Gì?",
                            "Tóp Sức Mạnh", "Top Nhiệm Vụ", "Đóng");
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    switch (this.mapId) {
                        case 5:
                            switch (player.iDMark.getIndexMenu()) {
                                case ConstNpc.BASE_MENU:
                                    if (select == 0) {
                                        Service.gI().showListTop(player, Manager.topSM);
                                        break;
                                    }
                                    if (select == 1) {
                                        Service.gI().showListTop(player, Manager.topNV);
                                        break;
                                    }
                                    break;
                            }
                            break;
                    }
                }
            }
        };
    }

    public static Npc gokuSSJ_1(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 80) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Xin chào, tôi có thể giúp gì cho cậu?", "Tới hành tinh\nYardart", "Từ chối");
                    } else if (this.mapId == 131) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Xin chào, tôi có thể giúp gì cho cậu?", "Quay về", "Từ chối");
                    } else {
                        super.openBaseMenu(player);
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    switch (player.iDMark.getIndexMenu()) {
                        case ConstNpc.BASE_MENU:
                            if (this.mapId == 131) {
                                if (select == 0) {
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 80, -1, 870);
                                }
                            }
                            if (this.mapId == 80) {
                                if (select == 0) {
                                    ChangeMapService.gI().changeMap(player, 131, -1, 901, 240);
                                }
                            }
                            break;
                    }
                }
            }
        };
    }

    public static Npc gokuSSJ_2(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    try {
                        Item biKiep = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 590);
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Vào các khung giờ chẵn trong ngày\n"
                                + "Khi luyện tập với Mộc nhân với chế độ bật Cờ sẽ đánh rơi Bí kíp\n"
                                + "Hãy cố găng tập luyện thu thập 9999 bí kíp rồi quay lại gặp ta nhé", "Nhận\nthưởng", "OK");

                    } catch (Exception ex) {
                        ex.printStackTrace();

                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    try {
                        Item biKiep = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 590);
                        if (select == 0) {
                            if (biKiep != null) {
                                if (biKiep.quantity >= 10000 && InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                    Item yardart = ItemService.gI().createNewItem((short) (player.gender + 592));
                                    yardart.itemOptions.add(new Item.ItemOption(47, 400));
                                    yardart.itemOptions.add(new Item.ItemOption(108, 10));
                                    InventoryServiceNew.gI().addItemBag(player, yardart);
                                    InventoryServiceNew.gI().subQuantityItemsBag(player, biKiep, 10000);
                                    InventoryServiceNew.gI().sendItemBags(player);
                                    Service.gI().sendThongBao(player, "Bạn vừa nhận được trang phục tộc Yardart");
                                } else if (biKiep.quantity < 10000) {
                                    Service.gI().sendThongBao(player, "Vui lòng sưu tầm đủ\n9999 bí kíp");
                                }
                            } else {
                                Service.gI().sendThongBao(player, "Vui lòng sưu tầm đủ\n9999 bí kíp");
                                return;
                            }
                        } else {
                            return;
                        }
                    } catch (Exception ex) {
                    }
                }
            }
        };
    }

    public static Npc khidaumoi(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (this.mapId == 14) {
                    this.createOtherMenu(player, ConstNpc.BASE_MENU,
                            "Bạn muốn nâng cấp khỉ ư?", "Nâng cấp\nkhỉ", "Shop của Khỉ", "Từ chối");
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 14) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0:
                                    this.createOtherMenu(player, 1,
                                            "|7|Cần Khỉ Lv1 hoặc 2,4,6 để nâng cấp lên lv8\b|2|Mỗi lần nâng cấp tiếp thì mỗi cấp cần thêm 10 đá ngũ sắc",
                                            "Khỉ\ncấp 2",
                                            "Khỉ\ncấp 4",
                                            "Khỉ\ncấp 6",
                                            "Khỉ\ncấp 8",
                                            "Từ chối");
                                    break;
                                case 1: //shop
                                    ShopServiceNew.gI().opendShop(player, "KHI", false);
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == 1) { // action đổi dồ húy diệt
                            switch (select) {
                                case 0: // trade
                                try {
                                    Item dns = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 674);
                                    Item klv1 = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1137);
                                    int soLuong = 0;
                                    if (dns != null) {
                                        soLuong = dns.quantity;
                                    }
                                    for (int i = 0; i < 12; i++) {
                                        Item klv = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1137 + i);

                                        if (InventoryServiceNew.gI().isExistItemBag(player, 1137 + i) && soLuong >= 20) {
                                            CombineServiceNew.gI().khilv2(player, 1138 + i);
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, dns, 20);
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, klv, 1);
                                            this.npcChat(player, "Upgrede Thành Công!");

                                            break;
                                        } else {
                                            this.npcChat(player, "Yêu cầu cần cái trang khỉ cấp 1 với 20 đá ngũ sắc");
                                        }

                                    }
                                } catch (Exception e) {

                                }
                                break;
                                case 1: // trade
                                try {
                                    Item dns = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 674);
                                    Item klv2 = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1138);
                                    int soLuong = 0;
                                    if (dns != null) {
                                        soLuong = dns.quantity;
                                    }
                                    for (int i = 0; i < 12; i++) {
                                        Item klv = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1138 + i);

                                        if (InventoryServiceNew.gI().isExistItemBag(player, 1138 + i) && soLuong >= 30) {
                                            CombineServiceNew.gI().khilv3(player, 1139 + i);
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, dns, 30);
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, klv, 1);
                                            this.npcChat(player, "Upgrede Thành Công!");

                                            break;
                                        } else {
                                            this.npcChat(player, "Yêu cầu cần cái trang khỉ cấp 2 với 30 đá ngũ sắc");
                                        }

                                    }
                                } catch (Exception e) {

                                }
                                break;
                                case 2: // trade
                                try {
                                    Item dns = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 674);
                                    Item klv2 = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1139);
                                    int soLuong = 0;
                                    if (dns != null) {
                                        soLuong = dns.quantity;
                                    }
                                    for (int i = 0; i < 12; i++) {
                                        Item klv = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1139 + i);

                                        if (InventoryServiceNew.gI().isExistItemBag(player, 1139 + i) && soLuong >= 40) {
                                            CombineServiceNew.gI().khilv4(player, 1140 + i);
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, dns, 40);
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, klv, 1);
                                            this.npcChat(player, "Upgrede Thành Công!");

                                            break;
                                        } else {
                                            this.npcChat(player, "Yêu cầu cần cái trang khỉ cấp 3 với 40 đá ngũ sắc");
                                        }

                                    }
                                } catch (Exception e) {

                                }
                                break;
                                case 3: // trade
                                try {
                                    Item dns = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 674);
                                    Item klv2 = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1140);
                                    int soLuong = 0;
                                    if (dns != null) {
                                        soLuong = dns.quantity;
                                    }
                                    for (int i = 0; i < 12; i++) {
                                        Item klv = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 1140 + i);

                                        if (InventoryServiceNew.gI().isExistItemBag(player, 1140 + i) && soLuong >= 50) {
                                            CombineServiceNew.gI().khilv5(player, 1136 + i);
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, dns, 50);
                                            InventoryServiceNew.gI().subQuantityItemsBag(player, klv, 1);
                                            this.npcChat(player, "Upgrede Thành Công!");

                                            break;
                                        } else {
                                            this.npcChat(player, "Yêu cầu cần cái trang khỉ cấp 3 với 50 đá ngũ sắc");
                                        }

                                    }
                                } catch (Exception e) {

                                }
                                break;

                                case 5: // canel
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc GhiDanh(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            String[] menuselect = new String[]{};

            @Override
            public void openBaseMenu(Player pl) {
                if (canOpenNpc(pl)) {
                    if (this.mapId == 179) {
                        createOtherMenu(pl, 0, DaiHoiVoThuatService.gI(DaiHoiVoThuat.gI().getDaiHoiNow()).Giai(pl), "Thông tin\nChi tiết", DaiHoiVoThuatService.gI(DaiHoiVoThuat.gI().getDaiHoiNow()).CanReg(pl) ? "Đăng ký" : "OK", "Đại Hội\nVõ Thuật\nLần thứ\n23");
                    } else if (this.mapId == 129) {
                        int goldchallenge = pl.goldChallenge;
                        if (pl.levelWoodChest == 0) {
                            menuselect = new String[]{"Thi đấu\n" + Util.numberToMoney(goldchallenge) + " vàng", "Về\nĐại Hội\nVõ Thuật"};
                        } else {
                            menuselect = new String[]{"Thi đấu\n" + Util.numberToMoney(goldchallenge) + " vàng", "Nhận thưởng\nRương cấp\n" + pl.levelWoodChest, "Về\nĐại Hội\nVõ Thuật"};
                        }
                        this.createOtherMenu(pl, ConstNpc.BASE_MENU, "Đại hội võ thuật lần thứ 23\nDiễn ra bất kể ngày đêm,ngày nghỉ ngày lễ\nPhần thưởng vô cùng quý giá\nNhanh chóng tham gia nào", menuselect, "Từ chối");

                    } else {
                        super.openBaseMenu(pl);
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 179) {
                        switch (select) {
                            case 0:
                                Service.gI().sendPopUpMultiLine(player, tempId, avartar, DaiHoiVoThuat.gI().Info());
                                break;
                            case 1:
                                if (DaiHoiVoThuatService.gI(DaiHoiVoThuat.gI().getDaiHoiNow()).CanReg(player)) {
                                    DaiHoiVoThuatService.gI(DaiHoiVoThuat.gI().getDaiHoiNow()).Reg(player);
                                }
                                break;
                            case 2:
                                ChangeMapService.gI().changeMapNonSpaceship(player, 129, 499, 360);
                                break;
                        }
                    } else if (this.mapId == 129) {
                        int goldchallenge = player.goldChallenge;
                        if (player.levelWoodChest == 0) {
                            switch (select) {
                                case 0:
                                    if (InventoryServiceNew.gI().finditemWoodChest(player)) {
                                        if (player.inventory.gold >= goldchallenge) {
                                            MartialCongressService.gI().startChallenge(player);
                                            player.inventory.gold -= (goldchallenge);
                                            PlayerService.gI().sendInfoHpMpMoney(player);
                                            player.goldChallenge += 2000000;
                                        } else {
                                            Service.getInstance().sendThongBao(player, "Không đủ vàng, còn thiếu " + Util.numberToMoney(goldchallenge - player.inventory.gold) + " vàng");
                                        }
                                    } else {
                                        Service.getInstance().sendThongBao(player, "Hãy mở rương báu vật trước");
                                    }
                                    break;
                                case 1:
                                    ChangeMapService.gI().changeMapNonSpaceship(player, 179, player.location.x, 432);
                                    break;
                            }
                        } else {
                            switch (select) {
                                case 0:
                                    if (InventoryServiceNew.gI().finditemWoodChest(player)) {
                                        if (player.inventory.gold >= goldchallenge) {
                                            MartialCongressService.gI().startChallenge(player);
                                            player.inventory.gold -= (goldchallenge);
                                            PlayerService.gI().sendInfoHpMpMoney(player);
                                            player.goldChallenge += 2000000;
                                        } else {
                                            Service.getInstance().sendThongBao(player, "Không đủ vàng, còn thiếu " + Util.numberToMoney(goldchallenge - player.inventory.gold) + " vàng");
                                        }
                                    } else {
                                        Service.getInstance().sendThongBao(player, "Hãy mở rương báu vật trước");
                                    }
                                    break;
                                case 1:
                                    if (!player.receivedWoodChest) {
                                        if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                            Item it = ItemService.gI().createNewItem((short) 570);
                                            it.itemOptions.add(new Item.ItemOption(72, player.levelWoodChest));
                                            it.itemOptions.add(new Item.ItemOption(30, 0));
                                            it.createTime = System.currentTimeMillis();
                                            InventoryServiceNew.gI().addItemBag(player, it);
                                            InventoryServiceNew.gI().sendItemBags(player);

                                            player.receivedWoodChest = true;
                                            player.levelWoodChest = 0;
                                            Service.getInstance().sendThongBao(player, "Bạn nhận được rương gỗ");
                                        } else {
                                            this.npcChat(player, "Hành trang đã đầy");
                                        }
                                    } else {
                                        Service.getInstance().sendThongBao(player, "Mỗi ngày chỉ có thể nhận rương báu 1 lần");
                                    }
                                    break;
                                case 2:
                                    ChangeMapService.gI().changeMapNonSpaceship(player, 52, player.location.x, 336);
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc unkonw(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5) {
                        this.createOtherMenu(player, 0,
                                "Éc éc Bạn muốn gì ở tôi :3?", "Đến Võ đài Unknow", "Võ Đài Siêu Cấp");

                    }
                    if (this.mapId == 112) {
                        this.createOtherMenu(player, 0,
                                "Bạn đang còn : " + player.pointPvp + " điểm PvP Point", "Về đảo Kame", "Đổi Cải trang sự kiên", "Top PVP");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5) {
                        if (player.iDMark.getIndexMenu() == 0) { // 
                            switch (select) {
                                case 0:
                                    if (player.getSession().player.nPoint.power >= 10000000000L) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 112, -1, 495);
                                        Service.gI().changeFlag(player, Util.nextInt(8));
                                    } else {
                                        this.npcChat(player, "Bạn cần 10 tỷ sức mạnh mới có thể vào");
                                    }
                                    break; // qua vo dai
                                case 1:
                                    if (player.getSession().player.nPoint.power >= 10000000000L) {
                                        ChangeMapService.gI().changeMapBySpaceShip(player, 145, -1, 495);
                                        Service.gI().changeFlag(player, Util.nextInt(8));
                                    } else {
                                        this.npcChat(player, "Bạn cần 10 tỷ sức mạnh mới có thể vào");
                                    }
                                    break; // qua vo dai

                            }
                        }
                    }

                    if (this.mapId == 112) {
                        if (player.iDMark.getIndexMenu() == 0) { // 
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 5, -1, 319);
                                    break; // ve dao kame
                                case 1:  // 
                                    this.createOtherMenu(player, 1,
                                            "Bạn có muốn đổi 500 điểm PVP lấy \n|6|Cải trang Mèo Kid Lân với tất cả chỉ số là 80%\n ", "Ok", "Không");
                                    // bat menu doi item
                                    break;

                                case 2:  // 
                                    Service.gI().showListTop(player, Manager.topPVP);
                                    // mo top pvp
                                    break;

                            }
                        }
                        if (player.iDMark.getIndexMenu() == 1) { // action doi item
                            switch (select) {
                                case 0: // trade
                                    if (player.pointPvp >= 500) {
                                        player.pointPvp -= 500;
                                        Item item = ItemService.gI().createNewItem((short) (1104));
                                        item.itemOptions.add(new Item.ItemOption(49, 30));
                                        item.itemOptions.add(new Item.ItemOption(77, 15));
                                        item.itemOptions.add(new Item.ItemOption(103, 20));
                                        item.itemOptions.add(new Item.ItemOption(207, 0));
                                        item.itemOptions.add(new Item.ItemOption(33, 0));
//                                      
                                        InventoryServiceNew.gI().addItemBag(player, item);
                                        Service.gI().sendThongBao(player, "Chúc Mừng Bạn Đổi Cải Trang Thành Công !");
                                    } else {
                                        Service.gI().sendThongBao(player, "Không đủ điểm bạn còn " + (500 - player.pointPvp) + " Điểm nữa");
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc monaito(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 7) {
                        this.createOtherMenu(player, 0,
                                "Chào bạn tôi sẽ đưa bạn đến hành tinh Cereal?", "Đồng ý", "Từ chối");
                    }
                    if (this.mapId == 170) {
                        this.createOtherMenu(player, 0,
                                "Ta ở đây để đưa con về", "Về Làng Mori", "Từ chối");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 7) {
                        if (player.iDMark.getIndexMenu() == 0) { // 
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 170, -1, 264);
                                    break; // den hanh tinh cereal
                            }
                        }
                    }
                    if (this.mapId == 170) {
                        if (player.iDMark.getIndexMenu() == 0) { // 
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 7, -1, 432);
                                    break; // quay ve

                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc granala(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {

            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "|1|Xin Chào,Sự Kiện 10/3 Đang Diễn Ra Các Cư Dân Có Thể Đổi Item Vip ở Đây nhé"
                                + "\n|3|Tách Ngọc Bội Lấy Điểm Sk"
                                + "\n|3|Đổi Công Thức Chế Tạo Đồ Thiên Sứ"
                                + "\n|3|Sử Dụng Ngọc Bội Để Đổi Random Item c2"
                                + "\n|3|Sử Dụng Điểm Sự Kiện Đổi Cải Trang Vip random Có Vĩnh Viễn"
                                + "\n|3|Thử Vận May Ra NGọc Rồng vip Tỉ Lệ Cao "
                                + "\n|6|Ngoài Ra Các Bạn Có Thể Trồng Dưa Hấu,Hãy Chat'duahau' để nhận dưa trồng", "Tách Ngọc Bội Lấy Điểm Sk", "Xem Điểm Sk", "Đổi Công Thức", "Đổi item Cấp 2", "Đổi Cải Trang", "Thử Vận May Ngọc Vip", "Tặng Dưa Hấu Cho Vua Hùng", "Từ chối");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 5) {
                        if (player.iDMark.isBaseMenu()) {
                            switch (select) {
                                case 0: //phân rã đồ thần linh
                                    CombineServiceNew.gI().openTabCombine(player, CombineServiceNew.PHAN_RA_DO_THAN_LINH);
                                    break;
                                case 1:
                                    this.createOtherMenu(player, ConstNpc.NAP_THE, "|2|Khó Đã Có Mbbank :3 \nNgươi đang có: " + player.inventory.coupon + " điểm sự kiện", "Đóng");
                                    break;
                                case 2:
                                    NpcService.gI().createMenuConMeo(player, ConstNpc.CONFIRM_DOI_DIEM_DUA, -1, "Đổi Công Thức Chế Tạo Đồ Thiên Sứ?\nTa Cần 200 điểm sự kiện đấy... ",
                                            "Đồng ý", "Từ chối");
                                    break;
                                case 3:
                                    NpcService.gI().createMenuConMeo(player, ConstNpc.CONFIRM_DOI_DIEM_ITEMC2, -1, "Ta Sẽ Cho Con Item siêu cấp ngẫu nhiên?\nTa Cần 100 Điểm Sự Kiện... ",
                                            "Đồng ý", "Từ chối");
                                    break;
                                case 4:
                                    NpcService.gI().createMenuConMeo(player, ConstNpc.CONFIRM_DOI_DIEM_CT, -1, "Cần 999 Điểm Sự Kiện Để Lấy Cải Trang Random \nCó Tỉ Lệ May Mắn Được Vĩnh Viễn...Thử Ngay Nào ",
                                            "Đồng ý", "Từ chối");
                                    break;
                                case 5:
                                    NpcService.gI().createMenuConMeo(player, ConstNpc.CONFIRM_DOI_ITEM_NR, -1, "Còn Thở Còn Gỡ Còn Điểm Còn Đổi ..?\nPhải giao cho ta 200 điểm sự kiện đấy...\nNếu May Mắn Sẽ Nhận Được Đồ Thiên Sứ jiren Và Nro Víp 1 Sao ",
                                            "Đồng ý", "Từ chối");
                                    break;
                                case 6:
                                    NpcService.gI().createMenuConMeo(player, ConstNpc.MENU_GIAO_BONG, -1, "Dưa Hấu Ngoài Biển Đã Bị Ngươi Cướp ..?\nHãy Giao Dưa Hấu Để Nhận x1 Rương kho Báu của Ta...\nCần 1 Quả Dưa... ",
                                            "Đồng ý", "Từ chối");
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_START_COMBINE) {
                            switch (player.combineNew.typeCombine) {
                                case CombineServiceNew.PHAN_RA_DO_THAN_LINH:
                                    if (select == 0) {
                                        CombineServiceNew.gI().startCombine(player, 0);
                                    }
                                    break;
                            }
                        } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_PHAN_RA_DO_THAN_LINH) {
                            if (select == 0) {
                                CombineServiceNew.gI().startCombine(player, 0);
                            }
                        }
                    }
                }
            }
        };
    }

    public static Npc mabu(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (this.mapId == 20) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Bạn Đã Bị Bư béo Nuốt Hãy Tìm nó để tiêu diệt<");

                        ChangeMapService.gI().changeMapBySpaceShip(player, 128, -1, 432);
                    } else {

                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Bạn Đã Bị Bư béo Nuốt Hãy Tìm nó để tiêu diệt?", "Sợ chưa để anh về nhe cuuu");
                    }
                    super.openBaseMenu(player);
                    if (this.mapId == 128) {
                        this.createOtherMenu(player, 0,
                                "Sao mi vẫn chưa bị tiêu hóa à", "Sợ chưa để anh về nhe cuuu", "Từ chối");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    switch (player.iDMark.getIndexMenu()) {
                        case ConstNpc.BASE_MENU:
                            if (this.mapId == 20) {
                                if (select == 0) {
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 5, -1, 870);
                                }
                            }
                            break;
                    }

                    if (this.mapId == 128) {
                        if (player.iDMark.getIndexMenu() == 0) { // 
                            switch (select) {
                                case 0:
                                    ChangeMapService.gI().changeMapBySpaceShip(player, 20, -1, 432);
                                    break; // quay ve

                            }
                        }
                    }
                }
            }
        };
    }

    private static Npc popo(int mapId, int status, int cx, int cy, int tempId, int avartar) {
        return new Npc(mapId, status, cx, cy, tempId, avartar) {
            @Override
            public void openBaseMenu(Player player) {
                if (canOpenNpc(player)) {
                    if (!TaskService.gI().checkDoneTaskTalkNpc(player, this)) {
                        this.createOtherMenu(player, ConstNpc.BASE_MENU, "Thượng đế vừa phát hiện 1 loại khí đang âm thầm\nhủy diệt mọi mầm sống trên Trái Đất,\nnó được gọi là Destron Gas.\nTa sẽ đưa các cậu đến nơi ấy, các cậu sẵn sàng chưa?",
                                "OK", "Top Gas", "Từ Chối");
                    }
                }
            }

            @Override
            public void confirmMenu(Player player, int select) {
                if (canOpenNpc(player)) {
                    if (player.iDMark.isBaseMenu()) {
                        switch (select) {
                            case 0:
                                if (player.clan != null) {
                                    if (player.clan.khiGas != null) {
                                        this.createOtherMenu(player, ConstNpc.MENU_OPENED_GAS,
                                                "Bang hội của con đang đi DesTroy Gas cấp độ "
                                                + player.clan.khiGas.level + "\nCon có muốn đi theo không?",
                                                "Đồng ý", "Từ chối");
                                    } else {
                                        this.createOtherMenu(player, ConstNpc.MENU_OPEN_GAS,
                                                "Khí Gas Huỷ Diệt đã chuẩn bị tiếp nhận các đợt tấn công của quái vật\n"
                                                + "các con hãy giúp chúng ta tiêu diệt quái vật \n"
                                                + "Ở đây có ta lo\nNhớ chọn cấp độ vừa sức mình nhé",
                                                "Chọn\ncấp độ", "Từ chối");
                                    }
                                } else {
                                    this.npcChat(player, "Con phải có bang hội ta mới có thể cho con đi");
                                }
                                break;
                            case 1:
                                TopGasService.SendTop(TopGasService.Sort(Manager.TopGas), player);
                                break;
                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPENED_GAS) {
                        switch (select) {
                            case 0:
                                if (player.isAdmin() || player.nPoint.power >= Gas.POWER_CAN_GO_TO_GAS) {
                                    ChangeMapService.gI().goToGas(player);
                                } else {
                                    this.npcChat(player, "Sức mạnh của con phải ít nhất phải đạt "
                                            + Util.numberToMoney(Gas.POWER_CAN_GO_TO_GAS));
                                }
                                break;

                        }
                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_OPEN_GAS) {
                        switch (select) {
                            case 0:
                                if (player.isAdmin() || player.nPoint.power >= Gas.POWER_CAN_GO_TO_GAS) {
                                    Input.gI().createFormChooseLevelGas(player);
                                } else {
                                    this.npcChat(player, "Sức mạnh của con phải ít nhất phải đạt "
                                            + Util.numberToMoney(Gas.POWER_CAN_GO_TO_GAS));
                                }
                                break;
                        }

                    } else if (player.iDMark.getIndexMenu() == ConstNpc.MENU_ACCPET_GO_TO_GAS) {
                        switch (select) {
                            case 0:
                                GasService.gI().openGas(player, Integer.parseInt(String.valueOf(PLAYERID_OBJECT.get(player.id))));
                                break;
                        }
                    }
                }
            }
        };
    }

    public static Npc createNPC(int mapId, int status, int cx, int cy, int tempId) {
        int avatar = Manager.NPC_TEMPLATES.get(tempId).avatar;
        try {
            switch (tempId) {
                case ConstNpc.UNKOWN:
                    return unkonw(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.GHI_DANH:
                    return GhiDanh(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.TRUNG_LINH_THU:
                    return trungLinhThu(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.POTAGE:
                    return poTaGe(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.QUY_LAO_KAME:
                    return quyLaoKame(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.POPO:
                    return popo(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.THO_DAI_CA:
                    return thodaika(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.TRUONG_LAO_GURU:
                    return truongLaoGuru(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.VUA_VEGETA:
                    return vuaVegeta(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.ONG_GOHAN:
                case ConstNpc.ONG_MOORI:
                case ConstNpc.ONG_PARAGUS:
                    return ongGohan_ongMoori_ongParagus(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.BUNMA:
                    return bulmaQK(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.DUA_HAU:
                    return duahau(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.DENDE:
                    return dende(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.APPULE:
                    return appule(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.DR_DRIEF:
                    return drDrief(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.CARGO:
                    return cargo(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.CUI:
                    return cui(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.SANTA:
                    return santa(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.URON:
                    return uron(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.BA_HAT_MIT:
                    return baHatMit(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.RUONG_DO:
                    return ruongDo(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.DAU_THAN:
                    return dauThan(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.CALICK:
                    return calick(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.JACO:
                    return jaco(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.THUONG_DE:
                    return thuongDe(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.CUA_HANG_KY_GUI:
                    return kyGui(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.Granola:
                    return granala(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.GIUMA_DAU_BO:
                    return Giuma(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.MABU:
                    return mabu(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.Monaito:
                    return monaito(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.VADOS:
                    return vados(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.KHI_DAU_MOI:
                    return khidaumoi(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.THAN_VU_TRU:
                    return thanVuTru(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.KIBIT:
                    return kibit(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.OSIN:
                    return osin(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.LY_TIEU_NUONG:
                    return npclytieunuong54(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.LINH_CANH:
                    return linhCanh(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.QUA_TRUNG:
                    return quaTrung(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.QUOC_VUONG:
                    return quocVuong(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.BUNMA_TL:
                    return bulmaTL(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.RONG_OMEGA:
                    return rongOmega(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.RONG_1S:
                case ConstNpc.RONG_2S:
                case ConstNpc.RONG_3S:
                case ConstNpc.RONG_4S:
                case ConstNpc.RONG_5S:
                case ConstNpc.RONG_6S:
                case ConstNpc.RONG_7S:
                    return rong1_to_7s(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.NPC_64:
                    return npcThienSu64(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.BILL:
                    return bill(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.BO_MONG:
                    return boMong(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.THAN_MEO_KARIN:
                    return karin(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.GOKU_SSJ:
                    return gokuSSJ_1(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.GOKU_SSJ_:
                    return gokuSSJ_2(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.DUONG_TANG:
                    return duongtank(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.XE_MIA:
                    return xeMia(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.CAPYBARA:
                    return caPybaRa(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.GAP_THU:
                    return gapThu(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.HANG_NGA:
                    return hangNga(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.MI_NUONG:
                    return miNuong(mapId, status, cx, cy, tempId, avatar);
                case ConstNpc.GOHAN_ZOM:
                    return goHanZom(mapId, status, cx, cy, tempId, avatar);
                default:
                    return new Npc(mapId, status, cx, cy, tempId, avatar) {
                        @Override
                        public void openBaseMenu(Player player) {
                            if (canOpenNpc(player)) {
                                super.openBaseMenu(player);
                            }
                        }

                        @Override
                        public void confirmMenu(Player player, int select) {
                            if (canOpenNpc(player)) {
//                                ShopService.gI().openShopNormal(player, this, ConstNpc.SHOP_BUNMA_TL_0, 0, player.gender);
                            }
                        }
                    };
            }
        } catch (Exception e) {
            Logger.logException(NpcFactory.class, e, "Lỗi load npc");
            return null;
        }
    }

    //girlbeo-mark
    public static void createNpcRongThieng() {
        Npc npc = new Npc(-1, -1, -1, -1, ConstNpc.RONG_THIENG, -1) {
            @Override
            public void confirmMenu(Player player, int select) {
                switch (player.iDMark.getIndexMenu()) {
                    case ConstNpc.IGNORE_MENU:

                        break;
                    case ConstNpc.SHENRON_CONFIRM:
                        if (select == 0) {
                            SummonDragon.gI().confirmWish();
                        } else if (select == 1) {
                            SummonDragon.gI().reOpenShenronWishes(player);
                        }
                        break;
                    case ConstNpc.SHENRON_1_1:
                        if (player.iDMark.getIndexMenu() == ConstNpc.SHENRON_1_1 && select == SHENRON_1_STAR_WISHES_1.length - 1) {
                            NpcService.gI().createMenuRongThieng(player, ConstNpc.SHENRON_1_2, SHENRON_SAY, SHENRON_1_STAR_WISHES_2);
                            break;
                        }
                    case ConstNpc.SHENRON_1_2:
                        if (player.iDMark.getIndexMenu() == ConstNpc.SHENRON_1_2 && select == SHENRON_1_STAR_WISHES_2.length - 1) {
                            NpcService.gI().createMenuRongThieng(player, ConstNpc.SHENRON_1_1, SHENRON_SAY, SHENRON_1_STAR_WISHES_1);
                            break;
                        }
                    default:
                        SummonDragon.gI().showConfirmShenron(player, player.iDMark.getIndexMenu(), (byte) select);
                        break;
                }
            }
        };
    }

    public static void createNpcConMeo() {
        Npc npc = new Npc(-1, -1, -1, -1, ConstNpc.CON_MEO, 351) {
            @Override
            public void confirmMenu(Player player, int select) {
                switch (player.iDMark.getIndexMenu()) {
                    case ConstNpc.IGNORE_MENU:

                        break;
                    case ConstNpc.MAKE_MATCH_PVP: //                        if (player.getSession().actived) 
                    {
                        if (Maintenance.isRunning) {
                            break;
                        }
                        PVPService.gI().sendInvitePVP(player, (byte) select);
                        break;
                    }
                    case ConstNpc.MAKE_FRIEND:
                        if (select == 0) {
                            Object playerId = PLAYERID_OBJECT.get(player.id);
                            if (playerId != null) {
                                FriendAndEnemyService.gI().acceptMakeFriend(player,
                                        Integer.parseInt(String.valueOf(playerId)));
                            }
                        }
                        break;
                    case ConstNpc.REVENGE:
                        if (select == 0) {
                            PVPService.gI().acceptRevenge(player);
                        }
                        break;
                    case ConstNpc.TUTORIAL_SUMMON_DRAGON:
                        if (select == 0) {
                            NpcService.gI().createTutorial(player, -1, SummonDragon.SUMMON_SHENRON_TUTORIAL);
                        }
                        break;
                    case ConstNpc.SUMMON_SHENRON:
                        if (select == 0) {
                            NpcService.gI().createTutorial(player, -1, SummonDragon.SUMMON_SHENRON_TUTORIAL);
                        } else if (select == 1) {
                            SummonDragon.gI().summonShenron(player);
                        }
                        break;
                    case ConstNpc.TUTORIAL_SUMMON_DRAGONTRB://TRB
                        if (select == 0) {
                            NpcService.gI().createTutorial(player, -1, SummonDragon.SUMMON_SHENRON_TRB);
                        }
                        break;
                    case ConstNpc.SUMMON_SHENRONTRB:
                        if (select == 0) {
                            NpcService.gI().createTutorial(player, -1, SummonDragon.SUMMON_SHENRON_TRB);
                        } else if (select == 1) {
                            SummonDragon.gI().summonShenronTRB(player);
                        }
                        break;
                    case ConstNpc.MENU_OPTION_USE_ITEM1105:
                        if (select == 0) {
                            IntrinsicService.gI().sattd(player);
                        } else if (select == 1) {
                            IntrinsicService.gI().satnm(player);
                        } else if (select == 2) {
                            IntrinsicService.gI().setxd(player);
                        }
                        break;
                    case ConstNpc.MENU_OPTION_USE_ITEM2000:
                    case ConstNpc.MENU_OPTION_USE_ITEM2001:
                    case ConstNpc.MENU_OPTION_USE_ITEM2002:
                        try {
                        ItemService.gI().OpenSKH(player, player.iDMark.getIndexMenu(), select);
                    } catch (Exception e) {
                        System.err.print("\nError at 216\n");
                        e.printStackTrace();
                        Logger.error("Lỗi mở hộp quà");
                    }
                    break;
                    case ConstNpc.MENU_OPTION_USE_ITEM2003:
                    case ConstNpc.MENU_OPTION_USE_ITEM2004:
                    case ConstNpc.MENU_OPTION_USE_ITEM2005:
                        try {
                        ItemService.gI().OpenDHD(player, player.iDMark.getIndexMenu(), select);
                    } catch (Exception e) {
                        Logger.error("Lỗi mở hộp quà");
                    }
                    break;
                    case ConstNpc.MENU_OPTION_USE_ITEM736:
                        try {
                        ItemService.gI().OpenDHD(player, player.iDMark.getIndexMenu(), select);
                    } catch (Exception e) {
                        Logger.error("Lỗi mở hộp quà");
                    }
                    break;
                    case ConstNpc.INTRINSIC:
                        if (select == 0) {
                            IntrinsicService.gI().showAllIntrinsic(player);
                        } else if (select == 1) {
                            IntrinsicService.gI().showConfirmOpen(player);
                        } else if (select == 2) {
                            IntrinsicService.gI().showConfirmOpenVip(player);
                        }
                        break;
                    case ConstNpc.CONFIRM_OPEN_INTRINSIC:
                        if (select == 0) {
                            IntrinsicService.gI().open(player);
                        }
                        break;
                    case ConstNpc.CONFIRM_OPEN_INTRINSIC_VIP:
                        if (select == 0) {
                            IntrinsicService.gI().openVip(player);
                        }
                        break;
                    case ConstNpc.CONFIRM_LEAVE_CLAN:
                        if (select == 0) {
                            ClanService.gI().leaveClan(player);
                        }
                        break;
                    case ConstNpc.CONFIRM_NHUONG_PC:
                        if (select == 0) {
                            ClanService.gI().phongPc(player, (int) PLAYERID_OBJECT.get(player.id));
                        }
                        break;
                    case ConstNpc.BAN_PLAYER:
                        if (select == 0) {
                            PlayerService.gI().banPlayer((Player) PLAYERID_OBJECT.get(player.id));
                            Service.gI().sendThongBao(player, "Ban người chơi " + ((Player) PLAYERID_OBJECT.get(player.id)).name + " thành công");
                        }
                        break;

                    case ConstNpc.BUFF_PET:
                        if (select == 0) {
                            Player pl = (Player) PLAYERID_OBJECT.get(player.id);
                            if (pl.pet == null) {
                                PetService.gI().createNormalPet(pl);
                                Service.gI().sendThongBao(player, "Phát đệ tử cho " + ((Player) PLAYERID_OBJECT.get(player.id)).name + " thành công");
                            }
                        }
                        break;
                    case ConstNpc.ACTIVE_PLAYER:
                        if (select == 0) {
                            PlayerService.gI().ActivePlayer((Player) PLAYERID_OBJECT.get(player.id));
                            Service.getInstance().sendThongBao(player, "Activated  " + ((Player) PLAYERID_OBJECT.get(player.id)).name + " thành công");
                        }
                        break;
                    case ConstNpc.TVMAX:
                        Item thoivangne = InventoryServiceNew.gI().findItem(player.inventory.itemsBag, 457);
                        switch (select) {
                            case 0:
                                if (thoivangne.quantity < 1) {
                                    Service.gI().sendThongBao(player,
                                            "Bạn không đủ 1 thỏi vàng");
                                } else if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                    player.inventory.gold += 500000000;
                                    Service.gI().sendThongBao(player, "Bạn vừa dùng thỏi vàng và nhận được 500tr vàng");
                                    InventoryServiceNew.gI().subQuantityItemsBag(player, thoivangne, 1);
                                    InventoryServiceNew.gI().sendItemBags(player);
                                    Service.getInstance().sendMoney(player);
                                } else {
                                    Service.gI().sendThongBao(player, "Hàng trang đã đầy");
                                }
                                break;
                            case 1:
                                if (thoivangne.quantity < 5) {
                                    Service.gI().sendThongBao(player,
                                            "Bạn không đủ 5 thỏi vàng");
                                } else if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                    player.inventory.gold += 2500000000L;
                                    Service.gI().sendThongBao(player, "Bạn vừa dùng thỏi vàng và nhận được 2 tỷ vàng");
                                    InventoryServiceNew.gI().subQuantityItemsBag(player, thoivangne, 5);
                                    InventoryServiceNew.gI().sendItemBags(player);
                                    Service.getInstance().sendMoney(player);
                                } else {
                                    Service.gI().sendThongBao(player, "Hàng trang đã đầy");
                                }
                                break;
                            case 2:
                                if (thoivangne.quantity < 10) {
                                    Service.gI().sendThongBao(player,
                                            "Bạn không đủ 10 thỏi vàng");
                                } else if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                    player.inventory.gold += 5000000000L;
                                    Service.gI().sendThongBao(player, "Bạn vừa dùng thỏi vàng và nhận được 5 tỷ vàng");
                                    InventoryServiceNew.gI().subQuantityItemsBag(player, thoivangne, 10);
                                    InventoryServiceNew.gI().sendItemBags(player);
                                    Service.getInstance().sendMoney(player);
                                } else {
                                    Service.gI().sendThongBao(player, "Hàng trang đã đầy");
                                }
                                break;
                            case 3:
                                if (thoivangne.quantity < 25) {
                                    Service.gI().sendThongBao(player,
                                            "Bạn không đủ 25 thỏi vàng");
                                } else if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                    player.inventory.gold += 12500000000L;
                                    Service.gI().sendThongBao(player, "Bạn vừa dùng thỏi vàng và nhận được 12 tỷ 5 vàng");
                                    InventoryServiceNew.gI().subQuantityItemsBag(player, thoivangne, 25);
                                    InventoryServiceNew.gI().sendItemBags(player);
                                    Service.getInstance().sendMoney(player);
                                } else {
                                    Service.gI().sendThongBao(player, "Hàng trang đã đầy");
                                }
                                break;
                            case 4:
                                if (thoivangne.quantity < 50) {
                                    Service.gI().sendThongBao(player,
                                            "Bạn không đủ 50 thỏi vàng");
                                } else if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                    player.inventory.gold += 25000000000L;
                                    Service.gI().sendThongBao(player, "Bạn vừa dùng thỏi vàng và nhận được 25 tỷ vàng");
                                    InventoryServiceNew.gI().subQuantityItemsBag(player, thoivangne, 50);
                                    InventoryServiceNew.gI().sendItemBags(player);
                                    Service.getInstance().sendMoney(player);
                                } else {
                                    Service.gI().sendThongBao(player, "Hàng trang đã đầy");
                                }
                                break;
                            case 5:
                                if (thoivangne.quantity < 100) {
                                    Service.gI().sendThongBao(player,
                                            "Bạn không đủ 100 thỏi vàng");
                                } else if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                                    player.inventory.gold += 50000000000L;
                                    Service.gI().sendThongBao(player,
                                            "Bạn vừa dùng thỏi vàng và nhận được 50 tỷ vàng");
                                    InventoryServiceNew.gI().subQuantityItemsBag(player, thoivangne, 100);
                                    InventoryServiceNew.gI().sendItemBags(player);
                                    Service.getInstance().sendMoney(player);
                                } else {
                                    Service.gI().sendThongBao(player, "Hàng trang đã đầy");
                                }
                                break;

                        }
                        break;
                    case ConstNpc.menu_detu:
                        switch (select) {
                            case 0:
                                Random randomtd = new Random();
                                int petType = randomtd.nextInt(3);
                                System.out.println("Player " + player.name + " Random Type Pet: " + petType);
                                switch (petType) {
                                    case 0:
                                        PetService.gI().changeMabuPet(player, player.pet.gender = 0);
                                        break;
                                    case 1:
                                        PetService.gI().changeBerusPet(player, player.pet.gender = 0);
                                        break;
                                    case 2:
                                        PetService.gI().changePicPet(player, player.pet.gender = 0);
                                        break;
                                }
                                break;
                            case 1:
                                Random randomnm = new Random();
                                int petTypenm = randomnm.nextInt(3);
                                System.out.println("Player " + player.name + " Random Type Pet: " + petTypenm);
                                switch (petTypenm) {
                                    case 0:
                                        PetService.gI().changeMabuPet(player, player.pet.gender = 1);
                                        break;
                                    case 1:
                                        PetService.gI().changeBerusPet(player, player.pet.gender = 1);
                                        break;
                                    case 2:
                                        PetService.gI().changePicPet(player, player.pet.gender = 1);
                                        break;
                                }
                                break;
                            case 2:
                                Random randomxd = new Random();
                                int petTypee = randomxd.nextInt(3);
                                System.out.println("Player " + player.name + " Random Type Pet: " + petTypee);
                                switch (petTypee) {
                                    case 0:
                                        PetService.gI().changeMabuPet(player, player.pet.gender = 2);
                                        break;
                                    case 1:
                                        PetService.gI().changeBerusPet(player, player.pet.gender = 2);
                                        break;
                                    case 2:
                                        PetService.gI().changePicPet(player, player.pet.gender = 2);
                                        break;
                                }
                                break;
                        }
                        break;
                    case ConstNpc.MENU_ADMIN:
                        switch (select) {
                            case 0:
                                this.createMenuConMeo(player, ConstNpc.ADMIN, -1, "|7| Admin Ngọc Rồng One Puch Man\b|2| Bùi Kim Trường\b|4| Người Đang Chơi: " + GirlkunSessionManager.gI().getSessions().size() + "\n" + "|8|Current thread: " + (Thread.activeCount()) + "\n",
                                        "Ngọc Rồng", "Đệ Tử", "Bảo Trì", "Tìm Kiếm\nPlayer", "Chat All", "Đóng");
                                break;
                            case 1:
                                this.createOtherMenu(player, ConstNpc.CALL_BOSS,
                                        "Chọn Boss?", "Full Cụm\nANDROID", "BLACK", "BROLY", "Cụm\nCell",
                                        "Cụm\nDoanh trại", "DOREMON", "FIDE", "FIDE\nBlack", "Cụm\nGINYU", "Cụm\nNAPPA", "Gắp Thú");
                                break;
                            case 2:
                                this.createOtherMenu(player, ConstNpc.BUFF_ITEM,
                                        "Buff Item", "Buff Item", "Item Option", "Buff Skh", "Buff Item Vip");
                                break;
                        }
                        break;
                    case ConstNpc.ADMIN:
                        switch (select) {
                            case 0:
                                for (int i = 14; i <= 20; i++) {
                                    Item itemm = ItemService.gI().createNewItem((short) i);
                                    InventoryServiceNew.gI().addItemBag(player, itemm);
                                }
                                InventoryServiceNew.gI().sendItemBags(player);
                                break;
                            case 1:
                                if (player.pet == null) {
                                    PetService.gI().createNormalPet(player);
                                } else {
                                    if (player.pet.typePet == 1) {
                                        PetService.gI().changePicPet(player);
                                    } else if (player.pet.typePet == 2) {
                                        PetService.gI().changeMabuPet(player);
                                    }
                                    PetService.gI().changeBerusPet(player);
                                }
                                break;
                            case 2:
                                if (player.isAdmin()) {
                                    System.out.println(player.name);
                                    Maintenance.gI().start(15);
                                    System.out.println(player.name);
                                }
                                break;
                            case 3:
                                Input.gI().createFormFindPlayer(player);
                                break;
                            case 4:
                                Input.gI().ChatAll(player);
                                break;

                        }
                        break;
                    case ConstNpc.BUFF_ITEM:
                        switch (select) {
                            case 0:
                                Input.gI().createFormSenditem(player);
                                break;
                            case 1:
                                Input.gI().createFormSenditem1(player);
                                break;
                            case 2:
                                Input.gI().createFormSenditemskh(player);
                                break;
                            case 3:
                                Input.gI().createFormSenditem2(player);
                                break;
                        }
                        break;
                    case ConstNpc.CALL_BOSS:
                        switch (select) {
                            case 0:
                                BossManager.gI().createBoss(BossID.ANDROID_13);
                                BossManager.gI().createBoss(BossID.ANDROID_14);
                                BossManager.gI().createBoss(BossID.ANDROID_15);
                                BossManager.gI().createBoss(BossID.ANDROID_19);
                                BossManager.gI().createBoss(BossID.DR_KORE);
                                BossManager.gI().createBoss(BossID.KING_KONG);
                                BossManager.gI().createBoss(BossID.PIC);
                                BossManager.gI().createBoss(BossID.POC);
                                break;
                            case 1:
                                BossManager.gI().createBoss(BossID.BLACK);
                                break;
                            case 2:
                                BossManager.gI().createBoss(BossID.BROLY);
                                break;
                            case 3:
                                BossManager.gI().createBoss(BossID.SIEU_BO_HUNG);
                                BossManager.gI().createBoss(BossID.XEN_BO_HUNG);
                                break;
                            case 4:
                                Service.getInstance().sendThongBao(player, "Không có boss");
                                break;
                            case 5:
                                BossManager.gI().createBoss(BossID.CHAIEN);
                                BossManager.gI().createBoss(BossID.XEKO);
                                BossManager.gI().createBoss(BossID.XUKA);
                                BossManager.gI().createBoss(BossID.NOBITA);
                                BossManager.gI().createBoss(BossID.DORAEMON);
                                break;
                            case 6:
                                BossManager.gI().createBoss(BossID.FIDE);
                                break;
                            case 7:
                                BossManager.gI().createBoss(BossID.FIDE_ROBOT);
                                BossManager.gI().createBoss(BossID.VUA_COLD);
                                break;
                            case 8:
                                BossManager.gI().createBoss(BossID.SO_1);
                                BossManager.gI().createBoss(BossID.SO_2);
                                BossManager.gI().createBoss(BossID.SO_3);
                                BossManager.gI().createBoss(BossID.SO_4);
                                BossManager.gI().createBoss(BossID.TIEU_DOI_TRUONG);
                                break;
                            case 9:
                                BossManager.gI().createBoss(BossID.KUKU);
                                BossManager.gI().createBoss(BossID.MAP_DAU_DINH);
                                BossManager.gI().createBoss(BossID.RAMBO);
                                break;
                            case 10:
                                BossManager.gI().createBoss(BossID.COOLER_GOLD);
                                BossManager.gI().createBoss(BossID.CUMBER);
                                BossManager.gI().createBoss(BossID.SONGOKU_TA_AC);
                                break;
                        }
                        break;
                    case ConstNpc.menutd:
                        switch (select) {
                            case 0: {// set songoku
                                try {
                                    ItemService.gI().setSongoku(player);
                                } catch (Exception ex) {
                                    java.util.logging.Logger.getLogger(NpcFactory.class.getName()).log(Level.SEVERE, null, ex);
                                }
                            }
                            break;
                            case 1:// set kaioken
                                try {
                                ItemService.gI().setKaioKen(player);
                            } catch (Exception e) {
                            }
                            break;
                            case 2:// set thenxin hang
                                   try {
                                ItemService.gI().setThenXinHang(player);
                            } catch (Exception e) {
                            }
                            break;
                        }
                        break;
                    case ConstNpc.menunm:
                        switch (select) {
                            case 0:
                                try {
                                ItemService.gI().setPicolo(player);
                            } catch (Exception e) {
                                System.err.print("\nError at 248\n");
                                e.printStackTrace();
                            }
                            break;
                            case 1:
                                try {
                                ItemService.gI().setLienHoan(player);
                            } catch (Exception e) {
                                System.err.print("\nError at 249\n");
                                e.printStackTrace();
                            }
                            break;
                            case 2:
                                try {
                                ItemService.gI().setPikkoroDaimao(player);
                            } catch (Exception e) {
                                System.err.print("\nError at 250\n");
                                e.printStackTrace();
                            }
                            break;
                        }
                        break;
                    case ConstNpc.menuxd:
                        switch (select) {
                            case 0:
                                try {
                                ItemService.gI().setKakarot(player);
                            } catch (Exception e) {
                                System.err.print("\nError at 251\n");
                                e.printStackTrace();
                            }
                            break;
                            case 1:
                                try {
                                ItemService.gI().setCadic(player);
                            } catch (Exception e) {
                                System.err.print("\nError at 252\n");
                                e.printStackTrace();
                            }
                            break;
                            case 2:
                                try {
                                ItemService.gI().setNappa(player);
                            } catch (Exception e) {
                                System.err.print("\nError at 253\n");
                                e.printStackTrace();
                            }
                            break;
                        }
                        break;
                    case ConstNpc.CONFIRM_DISSOLUTION_CLAN:
                        switch (select) {
                            case 0:
                                Clan clan = player.clan;
                                clan.deleteDB(clan.id);
                                Manager.CLANS.remove(clan);
                                player.clan = null;
                                player.clanMember = null;
                                ClanService.gI().sendMyClan(player);
                                ClanService.gI().sendClanId(player);
                                Service.gI().sendThongBao(player, "Đã giải tán bang hội.");
                                break;
                        }
                        break;
//                    case ConstNpc.CONFIRM_ACTIVE:
//                        switch (select) {
//                            case 0:
//                                if (player.getSession().goldBar >= 20) {
//                                    player.getSession().actived = true;
//                                    if (PlayerDAO.subGoldBar(player, 20)) {
//                                        Service.gI().sendThongBao(player, "Đã mở thành viên thành công!");
//                                        break;
//                                    } else {
//                                        this.npcChat(player, "Lỗi vui lòng báo admin...");
//                                    }
//                                }
////                                Service.gI().sendThongBao(player, "Bạn không có vàng\n Vui lòng NROGOD.COM để nạp thỏi vàng");
//                                break;
//                        }
//                        break;
                    case ConstNpc.CONFIRM_REMOVE_ALL_ITEM_LUCKY_ROUND:
                        if (select == 0) {
                            for (int i = 0; i < player.inventory.itemsBoxCrackBall.size(); i++) {
                                player.inventory.itemsBoxCrackBall.set(i, ItemService.gI().createItemNull());
                            }
                            player.inventory.itemsBoxCrackBall.clear();
                            Service.gI().sendThongBao(player, "Đã xóa hết vật phẩm trong rương");
                        }
                        break;
                    case ConstNpc.MENU_FIND_PLAYER:
                        Player p = (Player) PLAYERID_OBJECT.get(player.id);
                        if (p != null) {
                            switch (select) {
                                case 0:
                                    if (p.zone != null) {
                                        ChangeMapService.gI().changeMapYardrat(player, p.zone, p.location.x, p.location.y);
                                    }
                                    break;
                                case 1:
                                    if (p.zone != null) {
                                        ChangeMapService.gI().changeMap(p, player.zone, player.location.x, player.location.y);
                                    }
                                    break;
                                case 2:
                                    Input.gI().createFormChangeName(player, p);
                                    break;
                                case 3:
                                    String[] selects = new String[]{"Đồng ý", "Hủy"};
                                    NpcService.gI().createMenuConMeo(player, ConstNpc.BAN_PLAYER, -1,
                                            "Bạn có chắc chắn muốn ban " + p.name, selects, p);
                                    break;
                                case 4:
                                    Service.getInstance().sendThongBao(player, "Kich người chơi " + p.name + " thành công");
                                    Client.gI().getPlayers().remove(p);
                                    Client.gI().kickSession(p.getSession());
                                    break;
                                case 5:
                                    String[] selectss = new String[]{"Đồng ý", "Hủy"};
                                    NpcService.gI().createMenuConMeo(player, ConstNpc.ACTIVE_PLAYER, -1,
                                            "Mở Thành Viên Cho " + p.name + " ?", selectss, p);
                                    break;
                            }
                        }
                    case ConstNpc.MENU_EVENT:
                        switch (select) {
                            case 0:
                                Service.gI().sendThongBaoOK(player, "Điểm sự kiện: " + player.inventory.event + " ngon ngon...");
                                break;
                            case 1:
                                Service.gI().showListTop(player, Manager.topSK);
                                break;
                            case 2:
                                Service.gI().sendThongBao(player, "Sự kiện đã kết thúc...");
//                                NpcService.gI().createMenuConMeo(player, ConstNpc.MENU_GIAO_BONG, -1, "Người muốn giao bao nhiêu bông...",
//                                        "100 bông", "1000 bông", "10000 bông");
                                break;
                            case 3:
                                Service.gI().sendThongBao(player, "Sự kiện đã kết thúc...");
//                                NpcService.gI().createMenuConMeo(player, ConstNpc.CONFIRM_DOI_THUONG_SU_KIEN, -1, "Con có thực sự muốn đổi thưởng?\nPhải giao cho ta 3000 điểm sự kiện đấy... ",
//                                        "Đồng ý", "Từ chối");
                                break;

                        }
                        break;
//                    case ConstNpc.MENU_GIAO_BONG:
//                        ItemService.gI().giaobong(player, (int) Util.tinhLuyThua(10, select + 2));
//                        break;
                    case ConstNpc.CONFIRM_DOI_THUONG_SU_KIEN:
                        if (select == 0) {
                            ItemService.gI().openBoxVip(player);
                        }
                        break;
                    case ConstNpc.CONFIRM_DOI_DIEM_DUA:
                        if (select == 0) {
                            ItemService.gI().openBoxCongThuc(player);
                        }
                        break;
                    case ConstNpc.CONFIRM_DOI_DIEM_ITEMC2:
                        if (select == 0) {
                            ItemService.gI().openBoxitemc2(player);
                        }
                        break;
                    case ConstNpc.CONFIRM_DOI_ITEM_NR:
                        if (select == 0) {
                            ItemService.gI().openBoxitemnr(player);
                        }
                        break;
                    case ConstNpc.CONFIRM_DOI_DIEM_CT:
                        if (select == 0) {
                            ItemService.gI().openBoxCt(player);
                        }
                        break;
                    case ConstNpc.CONFIRM_TELE_NAMEC:
                        if (select == 0) {
                            NgocRongNamecService.gI().teleportToNrNamec(player);
                            player.inventory.subGemAndRuby(50);
                            Service.gI().sendMoney(player);
                        }
                        break;
                }
            }
        };
    }

}
