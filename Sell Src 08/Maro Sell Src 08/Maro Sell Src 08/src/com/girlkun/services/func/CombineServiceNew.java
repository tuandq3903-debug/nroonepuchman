package com.girlkun.services.func;

import com.girlkun.consts.ConstNpc;
import com.girlkun.models.item.Item;
import com.girlkun.models.item.Item.ItemOption;
import com.girlkun.models.map.ItemMap;
import com.girlkun.models.npc.Npc;
import com.girlkun.models.npc.NpcManager;
import com.girlkun.models.player.Player;
import com.girlkun.server.Manager;
import com.girlkun.server.ServerNotify;
import com.girlkun.network.io.Message;
import com.girlkun.services.*;
import com.girlkun.utils.Logger;
import com.girlkun.utils.Util;

import java.util.*;
import java.util.stream.Collectors;

public class CombineServiceNew {

    private static final int COST_DOI_VE_DOI_DO_HUY_DIET = 500000000;
    private static final int COST_DAP_DO_KICH_HOAT = 500000000;
    private static final int COST_DOI_MANH_KICH_HOAT = 500000000;
    private static final int COST = 500000000;
    private static final int TIME_COMBINE = 1;
    private static final byte MAX_STAR_ITEM = 8;
    private static final byte MAX_LEVEL_ITEM = 8;
    private static final byte MAX_LEVEL_SKH = 2;
    private static final byte OPEN_TAB_COMBINE = 0;
    private static final byte REOPEN_TAB_COMBINE = 1;
    private static final byte COMBINE_SUCCESS = 2;
    private static final byte COMBINE_FAIL = 3;
    private static final byte COMBINE_CHANGE_OPTION = 4;
    private static final byte COMBINE_DRAGON_BALL = 5;
    public static final byte OPEN_ITEM = 6;
    public static final int EP_SAO_TRANG_BI = 500;
    public static final int PHA_LE_HOA_TRANG_BI = 501;
    public static final int NANG_CAP_VAT_PHAM = 510;
    public static final int NANG_CAP_BONG_TAI = 511;
    public static final int MO_CHI_SO_BONG_TAI = 519;
    public static final int NHAP_NGOC_RONG = 513;
    public static final int LAM_PHEP_NHAP_DA = 524;
    public static final int PHAN_RA_DO_THAN_LINH = 514;
    public static final int NANG_CAP_DO_TS = 515;
    public static final int NANG_CAP_SKH_VIP = 516;
    public static final int CHE_TAO_TRANG_BI_TS = 520;
    public static final int LUYEN_HOA_CHIEN_LINH = 521;
    public static final int EP_AN_TRANG_BI = 522;
    public static final int DOI_DIEM = 502;
    public static final int NGUYET_AN = 512;
    private static final int GOLD_MOCS_BONG_TAI = 500_000_000;
    private static final int Gem_MOCS_BONG_TAI = 500;
    private static final int RUBY_MOCS_BONG_TAI = 500;
    private static final int RATIO_BONG_TAI = 50;
    private static final int RATIO_NANG_CAP = 45;
    private static final int GOLD_BONG_TAI2 = 500_000_000;
    private static final int RUBY_BONG_TAI2 = 1_000;
    private static final int GEM_BONG_TAI2 = 1_000;
    public static final int NANG_CAP_CHAN_MENH = 5380;
    public static final int PS_HOA_TRANG_BI = 2150;
    public static final int TAY_PS_HOA_TRANG_BI = 2151;

    //--------Sách Tuyệt Kỹ
    public static final int GIAM_DINH_SACH = 1233;
    public static final int TAY_SACH = 1234;
    public static final int NANG_CAP_SACH_TUYET_KY = 1235;
    public static final int PHUC_HOI_SACH = 1236;
    public static final int PHAN_RA_SACH = 1237;
    //------------End

    public static final int NANG_CAP_LEVEL_SKH = 1238;
    public static final int PHAN_RA_DO_THAN_RA_DA_NANG_CAP = 1239;
    public static final int NANG_CAP_SKH_VIP_DHD = 1240;
        public static final int NANG_CAP_SKH_TL = 1241;
        public static final int NANG_CAP_SKH_HD = 1242;
        public static final int NANG_CAP_SKH_TS = 1243;

        private static final int[][] ITEM_DHD = {
            {650, 651, 657, 658, 656},
            {652, 653, 659, 660, 656},
            {654, 655, 661, 662, 656}
        };
        private static final int[][] ITEM_DTS = {
            {1048, 1051, 1054, 1057, 1060},
            {1049, 1052, 1055, 1058, 1061},
            {1050, 1053, 1056, 1059, 1062}
        };

        private static final int[][] TY_LE_SET_SKH = {
            {25, 35, 40},
            {25, 35, 40},
            {25, 35, 40}
        };
        private static final int[][][] TY_LE_PHAN_KHUC_SKH_VIP = {
            {{70, 25, 5}, {70, 25, 5}, {70, 25, 5}, {70, 25, 5}},
            {{70, 25, 5}, {70, 25, 5}, {70, 25, 5}, {70, 25, 5}},
            {{70, 25, 5}, {70, 25, 5}, {70, 25, 5}, {70, 25, 5}}
        };

    private final Npc baHatMit;
    private final Npc granala;
    private final Npc npsthiensu64;

    private static CombineServiceNew i;

    public CombineServiceNew() {
        this.baHatMit = NpcManager.getNpc(ConstNpc.BA_HAT_MIT);
        this.granala = NpcManager.getNpc(ConstNpc.Granola);
        this.npsthiensu64 = NpcManager.getNpc(ConstNpc.NPC_64);
    }

    public static CombineServiceNew gI() {
        if (i == null) {
            i = new CombineServiceNew();
        }
        return i;
    }

    /**
     * Mở tab đập đồ
     *
     * @param player
     * @param type kiểu đập đồ
     */
    public void openTabCombine(Player player, int type) {
        player.combineNew.setTypeCombine(type);
        Message msg;
        try {
            msg = new Message(-81);
            msg.writer().writeByte(OPEN_TAB_COMBINE);
            msg.writer().writeUTF(getTextInfoTabCombine(type));
            msg.writer().writeUTF(getTextTopTabCombine(type));
            if (player.iDMark.getNpcChose() != null) {
                msg.writer().writeShort(player.iDMark.getNpcChose().tempId);
            }
            player.sendMessage(msg);
            msg.cleanup();
        } catch (Exception e) {
        }
    }

    /**
     * Hiển thị thông tin đập đồ
     *
     * @param player
     */
    public void showInfoCombine(Player player, int[] index) {
        player.combineNew.clearItemCombine();
        if (index.length > 0) {
            for (int i = 0; i < index.length; i++) {
                player.combineNew.itemsCombine.add(player.inventory.itemsBag.get(index[i]));
            }
        }
        switch (player.combineNew.typeCombine) {
            case PHAN_RA_DO_THAN_RA_DA_NANG_CAP:
                if (player.combineNew.itemsCombine.size() == 0) {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Con hãy đưa ta đồ thần linh để phân rã", "Đóng");
                    return;
                }
                if (player.combineNew.itemsCombine.size() == 1) {
                    List<Integer> itemdov2 = new ArrayList<>(Arrays.asList(562, 564, 566));
                    int couponAdd = 0;
                    Item item = player.combineNew.itemsCombine.get(0);
                    if (item.isNotNullItem()) {
                        if (item.template.id >= 555 && item.template.id <= 567) {
                            couponAdd = itemdov2.stream().anyMatch(t -> t == item.template.id) ? 2 : item.template.id == 561 ? 3 : 1;
                        }
                    }
                    if (couponAdd == 0) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Ta chỉ có thể phân rã đồ thần linh thôi", "Đóng");
                        return;
                    }
                    String npcSay = "|2|Sau khi phân rã vật phẩm\n|7|"
                            + "Bạn sẽ nhận được : " + couponAdd + " Đá Ngũ Sắc\n"
                            + (500000000 > player.inventory.gold ? "|7|" : "|1|")
                            + "Cần " + Util.numberToMoney(50000000) + " vàng";

                    if (player.inventory.gold < 50000000) {
                        this.baHatMit.npcChat(player, "Con không đủ 50TR vàng");
                        return;
                    }
                    this.baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE,
                            npcSay, "Phân Rã\n" + Util.numberToMoney(50000000) + " vàng", "Từ chối");
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Ta chỉ có thể phân rã 1 lần 1 món đồ thần linh", "Đóng");
                }
                break;
            case NANG_CAP_LEVEL_SKH:
                if (player.combineNew.itemsCombine.size() == 1
                        && player.combineNew.itemsCombine.get(0).isDHD()) {
                    this.baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE,
                            "Đổi 1 món Hủy Diệt thành 1 món SKH cùng loại\n"
                            + "Set nhận được: ngẫu nhiên\n"
                            + "Không cần thêm nguyên liệu", "Nâng Cấp", "Từ chối");
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                            "Hãy chọn đúng 1 món đồ Hủy Diệt", "Đóng");
                }
                break;
            case TAY_PS_HOA_TRANG_BI:
                if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                    if (player.combineNew.itemsCombine.size() == 2) {
                        Item daHacHoa = null;
                        Item itemHacHoa = null;
                        for (Item item_ : player.combineNew.itemsCombine) {
                            if (item_.template.id == 1274) {
                                daHacHoa = item_;
                            } else if (item_.isTrangBiHacHoa()) {
                                itemHacHoa = item_;
                            }
                        }
                        if (daHacHoa == null) {
                            Service.getInstance().sendThongBaoOK(player, "Cần 1 trang bị đã Pháp Sư và 1 Bùa giải Pháp sư");
                            return;
                        }
                        if (itemHacHoa == null) {
                            Service.getInstance().sendThongBaoOK(player, "Cần 1 trang bị đã Pháp Sư và 1 Bùa giải Pháp sư");
                            return;
                        }

                        String npcSay = "Trang bị được Giải pháp sư \"" + itemHacHoa.Name() + "\"\n"
                                + "|0|Sau khi Giải pháp sư: Về trang bị thường"
                                + "\nTỉ lệ thành công: 100%\n"
                                + "|2|Cần 0 vàng";

                        this.baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE,
                                npcSay, "Nâng cấp", "Từ chối");
                    } else {
                        Service.getInstance().sendThongBaoOK(player, "Cần có trang bị Pháp sư hóa");
                    }
                } else {
                    Service.getInstance().sendThongBaoOK(player, "Không đủ hành trang");
                }

                break;
            case PS_HOA_TRANG_BI:
                if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                    if (player.combineNew.itemsCombine.size() == 2) {
                        Item daHacHoa = null;
                        Item itemHacHoa = null;
                        for (Item item_ : player.combineNew.itemsCombine) {

                            if (item_.template != null && item_.template.id == 2030) {
                                daHacHoa = item_;
                            } else if (item_.isTrangBiHacHoa()) {
                                itemHacHoa = item_;
                            }
                        }
                        if (daHacHoa == null) {
                            Service.getInstance().sendThongBaoOK(player, "Cần 1 ngọc Pháp sư và 1 trang bị có thể Pháp sư");
                            return;
                        }
                        if (itemHacHoa == null) {
                            Service.getInstance().sendThongBaoOK(player, "Cần 1 ngọc Pháp sư và 1 trang bị có thể Pháp sư");
                            return;
                        }
                        if (itemHacHoa != null) {
                            for (ItemOption itopt : itemHacHoa.itemOptions) {
                                if (itopt.optionTemplate.id == 223) {
                                    if (itopt.param >= 8) {
                                        Service.getInstance().sendThongBaoOK(player, "Trang bị đã Pháp Sư Hóa tới mức tối đa");
                                        return;
                                    }
                                }
                            }
                        }
                        String npcSay = "Trang bị được pháp sư hóa \"" + itemHacHoa.template.name + "\"";
//
                        npcSay += "\n|0|Sau khi Nâng cấp: +3% chỉ số pháp sư\n";

                        npcSay += "|2|Tỉ lệ thành công: 100%" + "\n";
                        if (player.combineNew.goldCombine <= player.inventory.gold) {
                            npcSay += "Cần 0 vàng";
                            baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                    "Nâng cấp", "Từ chối");
                        } else {
                            Service.getInstance().sendThongBaoOK(player, "Cần 1 đá Pháp sư và 1 trang bị có thể Pháp sư");
                        }
                    } else {
                        Service.getInstance().sendThongBaoOK(player, "Không đủ hành trang");
                    }
                }
                break;
            case GIAM_DINH_SACH:
                if (player.combineNew.itemsCombine.size() == 2) {
                    Item sachTuyetKy = null;
                    Item buaGiamDinh = null;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (issachTuyetKy(item)) {
                            sachTuyetKy = item;
                        } else if (item.template.id == 1270) {
                            buaGiamDinh = item;
                        }
                    }
                    if (sachTuyetKy != null && buaGiamDinh != null) {

                        String npcSay = "|1|" + sachTuyetKy.getName() + "\n";
                        npcSay += "|2|" + buaGiamDinh.getName() + " " + buaGiamDinh.quantity + "/1";
                        baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                "Giám định", "Từ chối");
                    } else {
                        Service.getInstance().sendThongBaoOK(player, "Cần Sách Tuyệt Kỹ và bùa giám định");
                        return;
                    }
                } else {
                    Service.getInstance().sendThongBaoOK(player, "Cần Sách Tuyệt Kỹ và bùa giám định");
                    return;
                }
                break;
            case TAY_SACH:
                if (player.combineNew.itemsCombine.size() == 1) {
                    Item sachTuyetKy = null;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (issachTuyetKy(item)) {
                            sachTuyetKy = item;
                        }
                    }
                    if (sachTuyetKy != null) {
                        String npcSay = "|2|Tẩy Sách Tuyệt Kỹ";
                        baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                "Đồng ý", "Từ chối");
                    } else {
                        Service.getInstance().sendThongBaoOK(player, "Cần Sách Tuyệt Kỹ để tẩy");
                        return;
                    }
                } else {
                    Service.getInstance().sendThongBaoOK(player, "Cần Sách Tuyệt Kỹ để tẩy");
                    return;
                }
                break;

            case NANG_CAP_SACH_TUYET_KY:
                if (player.combineNew.itemsCombine.size() == 2) {
                    Item sachTuyetKy = null;
                    Item kimBamGiay = null;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (issachTuyetKy(item) && (item.template.id == 1262 || item.template.id == 1264 || item.template.id == 1266)) {
                            sachTuyetKy = item;
                        } else if (item.template.id == 1269) {
                            kimBamGiay = item;
                        }
                    }
                    if (sachTuyetKy != null && kimBamGiay != null) {
                        String npcSay = "|2|Nâng cấp sách tuyệt kỹ\n";
                        npcSay += "Cần 10 Kìm bấm giấy\n"
                                + "Tỉ lệ thành công: 10%\n"
                                + "Nâng cấp thất bại sẽ mất 10 Kìm bấm giấy";
                        baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                "Nâng cấp", "Từ chối");
                    } else {
                        Service.getInstance().sendThongBaoOK(player, "Cần Sách Tuyệt Kỹ 1 và 10 Kìm bấm giấy.");
                        return;
                    }
                } else {
                    Service.getInstance().sendThongBaoOK(player, "Cần Sách Tuyệt Kỹ 1 và 10 Kìm bấm giấy.");
                    return;
                }
                break;
            case PHUC_HOI_SACH:
                if (player.combineNew.itemsCombine.size() == 1) {
                    Item sachTuyetKy = null;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (issachTuyetKy(item)) {
                            sachTuyetKy = item;
                        }
                    }
                    if (sachTuyetKy != null) {
                        String npcSay = "|2|Phục hồi " + sachTuyetKy.getName() + "\n"
                                + "Cần 10 cuốn sách cũ\n"
                                + "Phí phục hồi 10 triệu vàng";
                        baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                "Đồng ý", "Từ chối");
                    } else {
                        Service.getInstance().sendThongBaoOK(player, "Không tìm thấy vật phẩm");
                        return;
                    }
                } else {
                    Service.getInstance().sendThongBaoOK(player, "Không tìm thấy vật phẩm");
                    return;
                }
                break;
            case PHAN_RA_SACH:
                if (player.combineNew.itemsCombine.size() == 1) {
                    Item sachTuyetKy = null;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (issachTuyetKy(item)) {
                            sachTuyetKy = item;
                        }
                    }
                    if (sachTuyetKy != null) {
                        String npcSay = "|2|Phân rã sách\n"
                                + "Nhận lại 5 cuốn sách cũ\n"
                                + "Phí rã 10 triệu vàng";
                        baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                "Đồng ý", "Từ chối");
                    } else {
                        Service.getInstance().sendThongBaoOK(player, "Không tìm thấy vật phẩm");
                        return;
                    }
                } else {
                    Service.getInstance().sendThongBaoOK(player, "Không tìm thấy vật phẩm");
                    return;
                }
                break;
            case NANG_CAP_CHAN_MENH:
                if (player.combineNew.itemsCombine.size() == 2) {
                    Item bongTai = null;
                    Item manhVo = null;
                    int star = 0;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (item.template.id == 1241 && item.isNotNullItem()) {
                            manhVo = item;
                        } else if (item.template.id >= 1232 && item.template.id <= 1240) {
                            bongTai = item;
                            star = item.template.id - 1232;
                        }
                    }
                    if (bongTai != null && bongTai.template.id == 1240) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Chân Mệnh đã đạt cấp tối đa", "Đóng");
                        return;
                    }
                    player.combineNew.DiemNangcap = getDiemNangcapChanmenh(star);
                    player.combineNew.DaNangcap = getDaNangcapChanmenh(star);
                    player.combineNew.TileNangcap = getTiLeNangcapChanmenh(star);
                    if (bongTai != null && manhVo != null && (bongTai.template.id >= 1232 && bongTai.template.id < 1240)) {
                        String npcSay = bongTai.template.name + "\n|2|";
                        for (Item.ItemOption io : bongTai.itemOptions) {
                            npcSay += io.getOptionString() + "\n";
                        }
                        npcSay += "|7|Tỉ lệ thành công: " + player.combineNew.TileNangcap + "%" + "\n";
                        if (player.combineNew.DiemNangcap <= player.NguHanhSonPoint) {
                            npcSay += "|1|Cần " + Util.numberToMoney(player.combineNew.DiemNangcap) + " Điểm Ngũ Hành Sơn";
                            baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                    "Nâng cấp\ncần " + player.combineNew.DaNangcap + " Đá Chân Mệnh");
                        } else {
                            npcSay += "Còn thiếu " + Util.numberToMoney(player.combineNew.DiemNangcap - player.NguHanhSonPoint) + " Điểm Ngũ Hành Sơn";
                            baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, npcSay, "Đóng");
                        }
                    } else {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Cần 1 Chân Mệnh và Đá Chân Mệnh", "Đóng");
                    }
                } else if (player.combineNew.itemsCombine.size() == 3) {
                    Item bongTai = null;
                    Item manhVo = null;
                    Item tv = null;
                    int star = 0;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (item.template.id == 1241) {
                            manhVo = item;
                        } else if (item.template.id == 457) {
                            tv = item;
                        } else if (item.template.id >= 1232 && item.template.id <= 1240) {
                            bongTai = item;
                            star = item.template.id - 1232;
                        }
                    }
                    if (bongTai != null && bongTai.template.id == 1240) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Chân Mệnh đã đạt cấp tối đa", "Đóng");
                        return;
                    }
                    player.combineNew.DiemNangcap = getDiemNangcapChanmenh(star) - 5;
                    player.combineNew.DaNangcap = getDaNangcapChanmenh(star) - 5;
                    player.combineNew.TileNangcap = getTiLeNangcapChanmenh(star) + 5;
                    if (tv != null && tv.quantity >= 1 && bongTai != null && manhVo != null && manhVo.quantity >= player.combineNew.DaNangcap - 5 && (bongTai.template.id >= 1232 && bongTai.template.id < 1240)) {
                        String npcSay = bongTai.template.name + "\n|2|";
                        for (Item.ItemOption io : bongTai.itemOptions) {
                            npcSay += io.getOptionString() + "\n";
                        }
                        npcSay += "|7|Tỉ lệ thành công: " + player.combineNew.TileNangcap + "%" + "\n";
                        if (player.combineNew.DiemNangcap <= player.NguHanhSonPoint - 5) {
                            npcSay += "|1|Cần " + Util.numberToMoney(player.combineNew.DiemNangcap) + " Điểm Ngũ Hành Sơn";
                            baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                    "Nâng cấp\ncần " + player.combineNew.DaNangcap + " Đá Chân Mệnh");
                        } else {
                            npcSay += "Còn thiếu " + Util.numberToMoney(player.combineNew.DiemNangcap - player.NguHanhSonPoint) + " Điểm Ngũ Hành Sơn";
                            baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, npcSay, "Đóng");
                        }
                    } else {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Cần 1 Chân Mệnh và Đá Chân Mệnh", "Đóng");
                    }
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                            "Cần 1 Chân Mệnh và Đá Chân Mệnh", "Đóng");
                }
                break;
            case NANG_CAP_BONG_TAI:
                if (player.combineNew.itemsCombine.size() == 2) {
                    Item bongtai = null;
                    Item manhvobt = null;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (checkbongtai(item)) { // gán bông tai c1 hoặc c2
                            bongtai = item;
                        } else if (item.template.id == 933) { // gán mảnh vỡ bt
                            manhvobt = item;
                        }
                    }

                    if (bongtai != null && manhvobt != null) {
                        int level = 0;
                        for (ItemOption io : bongtai.itemOptions) {
                            if (io.optionTemplate.id == 72) {
                                level = io.param;
                                break;
                            }
                        }
                        if (level < 4) {
                            int lvbt = lvbt(bongtai);
                            int countmvbt = getcountmvbtnangbt(lvbt);
                            player.combineNew.goldCombine = getGoldnangbt(lvbt);
                            player.combineNew.gemCombine = getgemdnangbt(lvbt);
                            player.combineNew.ratioCombine = getRationangbt(lvbt);

                            String npcSay = "Bông tai Porata Cấp: " + lvbt + " \n|2|";
                            for (ItemOption io : bongtai.itemOptions) {
                                npcSay += io.getOptionString() + "\n";
                            }
                            npcSay += "|7|Tỉ lệ thành công: " + player.combineNew.ratioCombine + "%" + "\n";
                            if (manhvobt.quantity >= countmvbt) {
                                if (player.combineNew.goldCombine <= player.inventory.gold) {
                                    if (player.combineNew.gemCombine <= player.inventory.gem) {
                                        npcSay += "|1|Cần " + Util.numberToMoney(player.combineNew.goldCombine)
                                                + " vàng";
                                        baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                                "Nâng cấp\ncần " + player.combineNew.gemCombine + " ngọc");
                                    } else {
                                        npcSay += "Còn thiếu " + Util.numberToMoney(
                                                player.combineNew.gemCombine - player.inventory.gem) + " ngọc";
                                        baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, npcSay, "Đóng");
                                    }
                                } else {
                                    npcSay += "Còn thiếu "
                                            + Util.numberToMoney(player.combineNew.goldCombine - player.inventory.gold)
                                            + " vàng";
                                    baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, npcSay, "Đóng");
                                }
                            } else {
                                npcSay += "Còn thiếu " + Util.numberToMoney(countmvbt - manhvobt.quantity)
                                        + " Mảnh vỡ bông tai";
                                baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, npcSay, "Đóng");
                            }
                        } else {
                            this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                    "Đã đạt cấp tối đa!)))", "Đóng");
                        }
                    } else {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Cần 1 Bông tai Porata cấp 1 hoặc cấp 2 và Mảnh vỡ bông tai", "Đóng");
                    }
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                            "Cần 1 Bông tai Porata cấp 1 hoặc cấp 2 và Mảnh vỡ bông tai", "Đóng");
                }
                break;
            case MO_CHI_SO_BONG_TAI:
                if (player.combineNew.itemsCombine.size() == 3) {
                    Item bongTai = null;
                    Item manhHon = null;
                    Item daXanhLam = null;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (item.template.id == 921 || item.template.id == 1228 || item.template.id == 1229) {
                            bongTai = item;
                        } else if (item.template.id == 934) {
                            manhHon = item;
                        } else if (item.template.id == 935) {
                            daXanhLam = item;
                        }
                    }
                    if (bongTai != null && manhHon != null && daXanhLam != null && manhHon.quantity >= 99 && daXanhLam.quantity >= 1) {

                        player.combineNew.goldCombine = GOLD_MOCS_BONG_TAI;
                        player.combineNew.gemCombine = Gem_MOCS_BONG_TAI;
                        player.combineNew.ratioCombine = RATIO_NANG_CAP;

                        String npcSay = "Bông tai Porata cấp "
                                + (bongTai.template.id == 921 ? bongTai.template.id == 1228 ? bongTai.template.id == 1229 ? "2" : "3" : "4" : "1")
                                + " \n|2|";
                        for (ItemOption io : bongTai.itemOptions) {
                            npcSay += io.getOptionString() + "\n";
                        }
                        npcSay += "|7|Tỉ lệ thành công: " + player.combineNew.ratioCombine + "%" + "\n";
                        if (player.combineNew.goldCombine <= player.inventory.gold) {
                            if (player.combineNew.gemCombine <= player.inventory.gem) {
                                npcSay += "|1|Cần " + Util.numberToMoney(player.combineNew.goldCombine) + " vàng";
                                baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                        "Nâng cấp\ncần " + player.combineNew.gemCombine + " ngọc");
                            } else {
                                npcSay += "Còn thiếu "
                                        + Util.numberToMoney(player.combineNew.gemCombine - player.inventory.gem)
                                        + " ngọc";
                                baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, npcSay, "Đóng");
                            }
                        } else {
                            npcSay += "Còn thiếu "
                                    + Util.numberToMoney(player.combineNew.goldCombine - player.inventory.gold)
                                    + " vàng";
                            baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, npcSay, "Đóng");
                        }
                    } else {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Cần 1 Bông tai Porata cấp 2 hoặc 3, X99 Mảnh hồn bông tai và x1 Đá xanh lam", "Đóng");
                    }
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                            "Cần 1 Bông tai Porata cấp 2 hoặc 3, X99 Mảnh hồn bông tai và x1 Đá xanh lam", "Đóng");
                }

                break;
            case CHE_TAO_TRANG_BI_TS:
                if (player.combineNew.itemsCombine.size() == 0) {
                    this.npsthiensu64.createOtherMenu(player, ConstNpc.IGNORE_MENU, "ọc ọc", "Yes");
                    return;
                }
                if (player.combineNew.itemsCombine.size() >= 2 && player.combineNew.itemsCombine.size() < 5) {
                    if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isCongThucVip()).count() < 1) {
                        this.npsthiensu64.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu Công thức Vip", "Đóng");
                        return;
                    }
                    if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isManhTS() && item.quantity >= 999).count() < 1) {
                        this.npsthiensu64.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu Mảnh đồ thiên sứ", "Đóng");
                        return;
                    }
                    if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaNangCap()).count() < 1 || player.combineNew.itemsCombine.size() == 4 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaNangCap()).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu Đá nâng cấp", "Đóng");
                        return;
                    }
                    if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaMayMan()).count() < 1 || player.combineNew.itemsCombine.size() == 4 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaMayMan()).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu Đá may mắn", "Đóng");
                        return;
                    }
                    Item mTS = null, daNC = null, daMM = null;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (item.isNotNullItem()) {
                            if (item.isManhTS()) {
                                mTS = item;
                            } else if (item.isDaNangCap()) {
                                daNC = item;
                            } else if (item.isDaMayMan()) {
                                daMM = item;
                            }
                        }
                    }
                    int tilemacdinh = 35;
                    int tilenew = tilemacdinh;
                    if (daNC != null) {
                        tilenew += (daNC.template.id - 1073) * 10;
                    }

                    String npcSay = "|2|Chế tạo " + player.combineNew.itemsCombine.stream().filter(Item::isManhTS).findFirst().get().typeNameManh() + " Thiên sứ "
                            + player.combineNew.itemsCombine.stream().filter(Item::isCongThucVip).findFirst().get().typeHanhTinh() + "\n"
                            + "|7|Mảnh ghép " + mTS.quantity + "/999\n";
//                            + "|2|Đá nâng cấp " + player.combineNew.itemsCombine.stream().filter(Item::isDaNangCap).findFirst().get().typeDanangcap()
//                            + " (+" + (daNC.template.id - 1073) + "0% tỉ lệ thành công)\n"
//                            + "|2|Đá may mắn " + player.combineNew.itemsCombine.stream().filter(Item::isDaMayMan).findFirst().get().typeDaMayman()
//                            + " (+" + (daMM.template.id - 1078) + "0% tỉ lệ tối đa các chỉ số)\n"
//                            + "|2|Tỉ lệ thành công: " + tilenew + "%\n"
//                            + "|7|Phí nâng cấp: 500 triệu vàng";

                    if (daNC != null) {

                        npcSay += "|2|Đá nâng cấp " + player.combineNew.itemsCombine.stream().filter(Item::isDaNangCap).findFirst().get().typeDanangcap()
                                + " (+" + (daNC.template.id - 1073) + "0% tỉ lệ thành công)\n";
                    }
                    if (daMM != null) {
                        npcSay += "|2|Đá may mắn " + player.combineNew.itemsCombine.stream().filter(Item::isDaMayMan).findFirst().get().typeDaMayman()
                                + " (+" + (daMM.template.id - 1078) + "0% tỉ lệ tối đa các chỉ số)\n";
                    }
                    if (daNC != null) {
                        tilenew += (daNC.template.id - 1073) * 10;
                        npcSay += "|2|Tỉ lệ thành công: " + tilenew + "%\n";
                    } else {
                        npcSay += "|2|Tỉ lệ thành công: " + tilemacdinh + "%\n";
                    }
                    npcSay += "|7|Phí nâng cấp: 500 triệu vàng";
                    if (player.inventory.gold < 500000000) {
                        this.npsthiensu64.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Bạn không đủ vàng", "Đóng");
                        return;
                    }
                    this.npsthiensu64.createOtherMenu(player, ConstNpc.MENU_START_COMBINE,
                            npcSay, "Nâng cấp\n500 Tr vàng", "Từ chối");
                } else {
                    if (player.combineNew.itemsCombine.size() > 4) {
                        this.npsthiensu64.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Nguyên liệu không phù hợp", "Đóng");
                        return;
                    }
                    this.npsthiensu64.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Không đủ nguyên liệu, mời quay lại sau", "Đóng");
                }
                break;
//            case NANG_CAP_LINH_THU:
//                if (player.combineNew.itemsCombine.size() == 3) {
//                    Item linhthu = null;
//                    Item thangtinhthach = null;
//                    Item thucan = null;
//                    for (Item item : player.combineNew.itemsCombine) {
//                        if (item.template.id >= 2019 && item.template.id <= 2026) {
//                            linhthu = item;
//                        } else if (item.template.id == 2030) {
//                            thangtinhthach = item;
//                        } else if (item.template.id >= 663 && item.template.id <= 667) {
//                            thucan = item;
//                        }
//                    }
//                    if (linhthu != null && thangtinhthach != null && thucan != null && thangtinhthach.quantity >= 99) {
//
//                        player.combineNew.goldCombine = GOLD_BONG_TAI;
//                        player.combineNew.gemCombine = GEM_BONG_TAI;
//                        player.combineNew.ratioCombine = RATIO_NANG_CAP;
//
//                        String npcSay = "Linh Thú Siêu Cấp" + "\n|2|";
//                        for (Item.ItemOption io : linhthu.itemOptions) {
//                            npcSay += io.getOptionString() + "\n";
//                        }
//                        npcSay += "|7|Tỉ lệ thành công: " + player.combineNew.ratioCombine + "%" + "\n";
//                        if (player.combineNew.goldCombine <= player.inventory.gold ) {
//                            if (player.combineNew.gemCombine <= player.inventory.gem) {
//                            npcSay += "|1|Cần " + Util.numberToMoney(player.combineNew.goldCombine) + " vàng";
//                            baHatMit.createOtherMenu(player, ConstNpc.MENU_NANG_CAP_LINH_THU, npcSay,
//                                    "Nâng cấp\ncần " + player.combineNew.gemCombine + " ngọc");
//                        } else {
//                          
//                            npcSay += "\n Còn thiếu " + Util.numberToMoney(player.combineNew.gemCombine - player.inventory.gem) + " Ngọc";
//                            baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, npcSay, "Đóng");
//                        } }else {
//                            npcSay += "Còn thiếu " + Util.numberToMoney(player.combineNew.goldCombine - player.inventory.gold) + " vàng";
//                        
//                            baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, npcSay, "Đóng");
//                        }
//                        
//                    } else {
//                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
//                                "Cần 1 Linh Thú, X99 Đá Ma Thuât và 1 Thức Ăn", "Đóng");
//                    }
//                } else {
//                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
//                            "Cần 1 Linh Thú, X99 Đá Ma Thuât và 1 Thức Ăn", "Đóng");
//                }
//                break;    
            case EP_SAO_TRANG_BI:
                if (player.combineNew.itemsCombine.size() == 2) {
                    Item trangBi = null;
                    Item daPhaLe = null;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (isTrangBiPhaLeHoa(item)) {
                            trangBi = item;
                        } else if (isDaPhaLe(item)) {
                            daPhaLe = item;
                        }
                    }
                    int star = 0; //sao pha lê đã ép
                    int starEmpty = 0; //lỗ sao pha lê
                    if (trangBi != null && daPhaLe != null) {
                        for (Item.ItemOption io : trangBi.itemOptions) {
                            if (io.optionTemplate.id == 102) {
                                star = io.param;
                            } else if (io.optionTemplate.id == 107) {
                                starEmpty = io.param;
                            }
                        }
                        if (star < starEmpty) {
                            player.combineNew.gemCombine = getGemEpSao(star);
                            String npcSay = trangBi.template.name + "\n|2|";
                            for (Item.ItemOption io : trangBi.itemOptions) {
                                if (io.optionTemplate.id != 102) {
                                    npcSay += io.getOptionString() + "\n";
                                }
                            }
                            if (daPhaLe.template.type == 30) {
                                for (Item.ItemOption io : daPhaLe.itemOptions) {
                                    npcSay += "|7|" + io.getOptionString() + "\n";
                                }
                            } else {
                                npcSay += "|7|" + ItemService.gI().getItemOptionTemplate(getOptionDaPhaLe(daPhaLe)).name.replaceAll("#", getParamDaPhaLe(daPhaLe) + "") + "\n";
                            }
                            npcSay += "|1|Cần " + Util.numberToMoney(player.combineNew.gemCombine) + " ngọc";
                            baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                    "Nâng cấp\ncần " + player.combineNew.gemCombine + " ngọc");

                        } else {
                            this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                    "Cần 1 trang bị có lỗ sao pha lê và 1 loại đá pha lê để ép vào", "Đóng");
                        }
                    } else {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Cần 1 trang bị có lỗ sao pha lê và 1 loại đá pha lê để ép vào", "Đóng");
                    }
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                            "Cần 1 trang bị có lỗ sao pha lê và 1 loại đá pha lê để ép vào", "Đóng");
                }
                break;
            case PHA_LE_HOA_TRANG_BI:
                if (player.combineNew.itemsCombine.size() == 1) {
                    Item item = player.combineNew.itemsCombine.get(0);
                    if (isTrangBiPhaLeHoa(item)) {
                        int star = 0;
                        for (Item.ItemOption io : item.itemOptions) {
                            if (io.optionTemplate.id == 107) {
                                star = io.param;
                                break;
                            }
                        }
                        if (star < MAX_STAR_ITEM) {
                            player.combineNew.goldCombine = getGoldPhaLeHoa(star);
                            player.combineNew.gemCombine = getGemPhaLeHoa(star);
                            player.combineNew.ratioCombine = getRatioPhaLeHoa(star);

                            String npcSay = item.template.name + "\n|2|";
                            for (Item.ItemOption io : item.itemOptions) {
                                if (io.optionTemplate.id != 102) {
                                    npcSay += io.getOptionString() + "\n";
                                }
                            }
                            npcSay += "|7|Tỉ lệ thành công: " + player.combineNew.ratioCombine + "%" + "\n";
                            if (player.combineNew.goldCombine <= player.inventory.gold) {
                                npcSay += "|1|Cần " + Util.numberToMoney(player.combineNew.goldCombine) + " vàng";
                                baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                        "Nâng cấp\ncần " + player.combineNew.gemCombine + " ngọc", "Nâng Cấp Liên Tục 100 Lần");
                            } else {
                                npcSay += "Còn thiếu " + Util.numberToMoney(player.combineNew.goldCombine - player.inventory.gold) + " vàng";
                                baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, npcSay, "Đóng");
                            }
                        } else {
                            this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Vật phẩm đã đạt tối đa sao pha lê", "Đóng");
                        }
                    } else {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Vật phẩm này không thể đục lỗ", "Đóng");
                    }
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Hãy hãy chọn 1 vật phẩm để pha lê hóa", "Đóng");
                }
                break;
            case NHAP_NGOC_RONG:
                if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                    if (player.combineNew.itemsCombine.size() == 1) {
                        Item item = player.combineNew.itemsCombine.get(0);
                        if (item != null && item.isNotNullItem() && (item.template.id > 14 && item.template.id <= 20) && item.quantity >= 7) {
                            String npcSay = "|2|Con có muốn biến 7 " + item.template.name + " thành\n"
                                    + "1 viên " + ItemService.gI().getTemplate((short) (item.template.id - 1)).name + "\n"
                                    + "|7|Cần 7 " + item.template.name;
                            this.baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay, "Làm phép", "Từ chối");
                        } else {
                            this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Cần 7 viên ngọc rồng 2 sao trở lên", "Đóng");
                        }
                    } else {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Cần 7 viên ngọc rồng 2 sao trở lên", "Đóng");
                    }
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Hành trang cần ít nhất 1 chỗ trống", "Đóng");
                }
                break;
            case NANG_CAP_VAT_PHAM:
                if (player.combineNew.itemsCombine.size() >= 2 && player.combineNew.itemsCombine.size() < 4) {
                    if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type < 5).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu đồ nâng cấp", "Đóng");
                        break;
                    }
                    if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type == 14).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu đá nâng cấp", "Đóng");
                        break;
                    }
                    if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.id == 987).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu đồ nâng cấp", "Đóng");
                        break;
                    }
                    Item itemDo = null;
                    Item itemDNC = null;
                    Item itemDBV = null;
                    for (int j = 0; j < player.combineNew.itemsCombine.size(); j++) {
                        if (player.combineNew.itemsCombine.get(j).isNotNullItem()) {
                            if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.get(j).template.id == 987) {
                                itemDBV = player.combineNew.itemsCombine.get(j);
                                continue;
                            }
                            if (player.combineNew.itemsCombine.get(j).template.type < 5) {
                                itemDo = player.combineNew.itemsCombine.get(j);
                            } else {
                                itemDNC = player.combineNew.itemsCombine.get(j);
                            }
                        }
                    }
                    if (isCoupleItemNangCapCheck(itemDo, itemDNC)) {
                        int level = 0;
                        for (Item.ItemOption io : itemDo.itemOptions) {
                            if (io.optionTemplate.id == 72) {
                                level = io.param;
                                break;
                            }
                        }
                        if (level < MAX_LEVEL_ITEM) {
                            player.combineNew.goldCombine = getGoldNangCapDo(level);
                            player.combineNew.ratioCombine = (float) getTileNangCapDo(level);
                            player.combineNew.countDaNangCap = getCountDaNangCapDo(level);
                            player.combineNew.countDaBaoVe = (short) getCountDaBaoVe(level);
                            String npcSay = "|2|Hiện tại " + itemDo.template.name + " (+" + level + ")\n|0|";
                            for (Item.ItemOption io : itemDo.itemOptions) {
                                if (io.optionTemplate.id != 72) {
                                    npcSay += io.getOptionString() + "\n";
                                }
                            }
                            String option = null;
                            int param = 0;
                            for (Item.ItemOption io : itemDo.itemOptions) {
                                if (io.optionTemplate.id == 47
                                        || io.optionTemplate.id == 6
                                        || io.optionTemplate.id == 0
                                        || io.optionTemplate.id == 7
                                        || io.optionTemplate.id == 14
                                        || io.optionTemplate.id == 22
                                        || io.optionTemplate.id == 23) {
                                    option = io.optionTemplate.name;
                                    param = io.param + (io.param * 10 / 100);
                                    break;
                                }
                            }
                            npcSay += "|2|Sau khi nâng cấp (+" + (level + 1) + ")\n|7|"
                                    + option.replaceAll("#", String.valueOf(param))
                                    + "\n|7|Tỉ lệ thành công: " + player.combineNew.ratioCombine + "%\n"
                                    + (player.combineNew.countDaNangCap > itemDNC.quantity ? "|7|" : "|1|")
                                    + "Cần " + player.combineNew.countDaNangCap + " " + itemDNC.template.name
                                    + "\n" + (player.combineNew.goldCombine > player.inventory.gold ? "|7|" : "|1|")
                                    + "Cần " + Util.numberToMoney(player.combineNew.goldCombine) + " vàng";

                            String daNPC = player.combineNew.itemsCombine.size() == 3 && itemDBV != null ? String.format("\nCần tốn %s đá bảo vệ", player.combineNew.countDaBaoVe) : "";
                            if ((level == 2 || level == 4 || level == 6) && !(player.combineNew.itemsCombine.size() == 3 && itemDBV != null)) {
                                npcSay += "\nNếu thất bại sẽ rớt xuống (+" + (level - 1) + ")";
                            }
                            if (player.combineNew.countDaNangCap > itemDNC.quantity) {
                                this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                        npcSay, "Còn thiếu\n" + (player.combineNew.countDaNangCap - itemDNC.quantity) + " " + itemDNC.template.name);
                            } else if (player.combineNew.goldCombine > player.inventory.gold) {
                                this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                        npcSay, "Còn thiếu\n" + Util.numberToMoney((player.combineNew.goldCombine - player.inventory.gold)) + " vàng");
                            } else if (player.combineNew.itemsCombine.size() == 3 && Objects.nonNull(itemDBV) && itemDBV.quantity < player.combineNew.countDaBaoVe) {
                                this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                        npcSay, "Còn thiếu\n" + (player.combineNew.countDaBaoVe - itemDBV.quantity) + " đá bảo vệ");
                            } else {
                                this.baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE,
                                        npcSay, "Nâng cấp\n" + Util.numberToMoney(player.combineNew.goldCombine) + " vàng" + daNPC, "Từ chối");
                            }
                        } else {
                            this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Trang bị của ngươi đã đạt cấp tối đa", "Đóng");
                        }
                    } else {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Hãy chọn 1 trang bị và 1 loại đá nâng cấp", "Đóng");
                    }
                } else {
                    if (player.combineNew.itemsCombine.size() > 3) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Cất đi con ta không thèm", "Đóng");
                        break;
                    }
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Hãy chọn 1 trang bị và 1 loại đá nâng cấp", "Đóng");
                }
                break;
            case PHAN_RA_DO_THAN_LINH:
                if (player.combineNew.itemsCombine.size() == 0) {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Con hãy đưa ta đồ thần linh để phân rã", "Đóng");
                    return;
                }
                if (player.combineNew.itemsCombine.size() == 1) {
                    List<Integer> itemdov2 = new ArrayList<>(Arrays.asList(562, 564, 566));
                    int couponAdd = 0;
                    Item item = player.combineNew.itemsCombine.get(0);
                    if (item.isNotNullItem()) {
                        if (item.template.id >= 555 && item.template.id <= 567) {
                            couponAdd = itemdov2.stream().anyMatch(t -> t == item.template.id) ? 2 : item.template.id == 561 ? 3 : 1;
                        }
                    }
                    if (couponAdd == 0) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Ta chỉ có thể phân rã đồ thần linh thôi", "Đóng");
                        return;
                    }
                    String npcSay = "|2|Sau khi phân rã vật phẩm\n|7|"
                            + "Bạn sẽ nhận được : " + couponAdd + " đá ngũ sắc\n"
                            + (500000000 > player.inventory.gold ? "|7|" : "|1|")
                            + "Cần " + Util.numberToMoney(500000000) + " vàng";

                    if (player.inventory.gold < 500000000) {
                        this.baHatMit.npcChat(player, "Hết tiền rồi\nẢo ít thôi con");
                        return;
                    }
                    this.baHatMit.createOtherMenu(player, ConstNpc.MENU_PHAN_RA_DO_THAN_LINH,
                            npcSay, "Nâng cấp\n" + Util.numberToMoney(500000000) + " vàng", "Từ chối");
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Ta chỉ có thể phân rã 1 lần 1 món đồ thần linh", "Đóng");
                }
                break;
            case DOI_DIEM:
                if (player.combineNew.itemsCombine.size() == 0) {
                    this.npsthiensu64.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Con hãy đưa cho ta thức ăn", "Đóng");
                    return;
                }
                if (player.combineNew.itemsCombine.size() == 1) {
                    List<Integer> itemdov2 = new ArrayList<>(Arrays.asList(663, 664, 665, 666, 667));
                    int couponAdd = 0;
                    Item item = player.combineNew.itemsCombine.get(0);
                    if (item.isNotNullItem()) {
                        if (item.template.id >= 663 && item.template.id <= 667) {
                            couponAdd = itemdov2.stream().anyMatch(t -> t == item.template.id) ? 1 : item.template.id <= 667 ? 1 : 1;
                        }
                    }
                    if (couponAdd == 0) {
                        this.npsthiensu64.createOtherMenu(player, ConstNpc.IGNORE_MENU, "THỨC ĂN!!!!!!!!", "Đóng");
                        return;
                    }
                    String npcSay = "|2|Sau khi phân rã vật phẩm\n|7|"
                            + "Bạn sẽ nhận được : " + couponAdd + " điểm\n"
                            + (500000000 > player.inventory.gold ? "|7|" : "|1|")
                            + "Cần " + Util.numberToMoney(500000000) + " vàng";

                    if (player.inventory.gold < 500000000) {
                        this.npsthiensu64.npcChat(player, "Hết tiền rồi\nẢo ít thôi con");
                        return;
                    }
                    this.npsthiensu64.createOtherMenu(player, ConstNpc.MENU_PHAN_RA_DO_THAN_LINH,
                            npcSay, "Thức Ăn", "Từ chối");
                } else {
                    this.npsthiensu64.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Cái Đầu Buồi", "Đóng");
                }
                break;
            case NANG_CAP_DO_TS:
                if (player.combineNew.itemsCombine.size() == 0) {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "1 công thức, 1 đá nâng cấp, 999 mảnh thiên sứ, địt nhau", "Đóng");
                    return;
                }
                if (player.combineNew.itemsCombine.size() == 4) {
                    if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isCongThucVip()).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu Công Thức", "Đóng");
                        return;
                    }
                    if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaNangCap()).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu đồ hủy diệt", "Đóng");
                        return;
                    }
                    if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isManhTS() && item.quantity >= 999).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu mảnh thiên sứ", "Đóng");
                        return;
                    }

                    String npcSay = "|2|Con có muốn đổi các món nguyên liệu ?\n|7|"
                            + "Và nhận được " + player.combineNew.itemsCombine.stream().filter(Item::isManhTS).findFirst().get().typeNameManh() + " thiên sứ tương ứng\n"
                            + "|1|Cần " + Util.numberToMoney(COST) + " vàng";

                    if (player.inventory.gold < COST) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Hết tiền rồi\nẢo ít thôi con", "Đóng");
                        return;
                    }
                    this.baHatMit.createOtherMenu(player, ConstNpc.MENU_NANG_CAP_DO_TS,
                            npcSay, "Nâng cấp\n" + Util.numberToMoney(COST) + " vàng", "Từ chối");
                } else {
                    if (player.combineNew.itemsCombine.size() > 3) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Cất đi con ta không thèm", "Đóng");
                        return;
                    }
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Còn thiếu nguyên liệu để nâng cấp hãy quay lại sau", "Đóng");
                }
                break;
            case NANG_CAP_SKH_VIP_DHD:
                if (isValidInputNangCapSKHVIP(player)) {
                    this.baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE,
                            "1 món Hủy Diệt + 3 món Thần Linh khác nhau\n"
                            + "Kết quả: 1 món SKH VIP cùng loại, phân khúc ngẫu nhiên\n"
                            + "Tỉ lệ phân khúc: " + getVipTierRateText(player), "Nâng Cấp", "Từ chối");
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                            "Cần 1 món Hủy Diệt và 3 món Thần Linh khác nhau", "Đóng");
                }
                break;
            case NANG_CAP_SKH_TL:
            case NANG_CAP_SKH_HD:
            case NANG_CAP_SKH_TS:
                showInfoNangCapSKH(player, player.combineNew.typeCombine);
                break;
            case EP_AN_TRANG_BI:
                if (player.combineNew.itemsCombine.size() == 3) {
                    Item cailoz = null;
                    Item concak = null;
                    Item thoivang = null;
                    for (Item item : player.combineNew.itemsCombine) {
                        if (item.isSKH() || item.isTrangBiAn()) {
                            cailoz = item;
                        } else if (item.template.id == 674) {
                            concak = item;
                        } else if (item.template.id == 457) {
                            thoivang = item;
                        }
                    }
                    if (cailoz != null && concak != null && thoivang != null && concak.quantity >= 25) {

                        player.combineNew.goldCombine = 50000;
                        player.combineNew.gemCombine = 0;
                        player.combineNew.ratioCombine = 50;

                        String npcSay = "Trang Bị" + "\n" + player.combineNew.ratioCombine + "%\n5000 Hồng Ngọc";
                        for (Item.ItemOption io : cailoz.itemOptions) {
                            if (player.combineNew.goldCombine <= player.inventory.ruby) {
                                baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE, npcSay,
                                        "NÂNG CẤP!!!!");
                            } else {
                                this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                        "Cần 1 trang bị cá5;i đéo j cũng được, 50000 hồng ngọc, 25 đá ngũ sắc và 1 thỏi vàng", "Đóng");
                            }
                        }
                    } else {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                "Cần 1 trang bị kích hoạt hoặc ấn, 50000 hồng ngọc, 25 đá ngũ sắc và 1 thỏi vàng", "Đóng");
                    }
                } else {
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                            "Cần 1 trang bị kích hoạt hoặc ấn, 50000 hồng ngọc, 25 đá ngũ sắc và 1 thỏi vàng", "Đóng");
                }
                break;
            case NGUYET_AN:
                if (player.combineNew.itemsCombine.size() >= 2 && player.combineNew.itemsCombine.size() < 4) {
                    if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type < 5).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu đồ nâng cấp", "Đóng");
                        break;
                    }
                    if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type == 14).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu đá nâng cấp", "Đóng");
                        break;
                    }
                    if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.id == 987).count() < 1) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Thiếu đồ nâng cấp", "Đóng");
                        break;
                    }
                    Item itemDo = null;
                    Item itemDNC = null;
                    Item itemDBV = null;
                    for (int j = 0; j < player.combineNew.itemsCombine.size(); j++) {
                        if (player.combineNew.itemsCombine.get(j).isNotNullItem()) {
                            if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.get(j).template.id == 987) {
                                itemDBV = player.combineNew.itemsCombine.get(j);
                                continue;
                            }
                            if (player.combineNew.itemsCombine.get(j).template.type < 5) {
                                itemDo = player.combineNew.itemsCombine.get(j);
                            } else {
                                itemDNC = player.combineNew.itemsCombine.get(j);
                            }
                        }
                    }
                    if (isCoupleItemNangCapCheck(itemDo, itemDNC)) {
                        int level = 0;
                        for (Item.ItemOption io : itemDo.itemOptions) {
                            if (io.optionTemplate.id >= 34 && io.optionTemplate.id <= 36) {
                                level = io.param;
                                break;
                            }
                        }
                        if (level < 1) {
                            player.combineNew.goldCombine = 50000;
                            player.combineNew.ratioCombine = 50;
                            player.combineNew.countDaNangCap = 99;
                            player.combineNew.countDaBaoVe = (short) getCountDaBaoVe(level);
                            String npcSay = "|2|Hiện tại " + itemDo.template.name + " (+" + level + ")\n|0|";
                            for (Item.ItemOption io : itemDo.itemOptions) {
                                if (io.optionTemplate.id != 34 && io.optionTemplate.id != 36 && io.optionTemplate.id != 35 && io.optionTemplate.id != 127 && io.optionTemplate.id != 128 && io.optionTemplate.id != 129 && io.optionTemplate.id != 130 && io.optionTemplate.id != 131 && io.optionTemplate.id != 132 && io.optionTemplate.id != 133 && io.optionTemplate.id != 134 && io.optionTemplate.id != 135) {
                                    npcSay += io.getOptionString() + "\n";
                                }
                            }
//                            String option = null;
//                            int param = 0;
//                            for (Item.ItemOption io : itemDo.itemOptions) {
//                                if (io.optionTemplate.id == 47
//                                        || io.optionTemplate.id == 6
//                                        || io.optionTemplate.id == 0
//                                        || io.optionTemplate.id == 7
//                                        || io.optionTemplate.id == 14
//                                        || io.optionTemplate.id == 22
//                                        || io.optionTemplate.id == 23) {
//                                    option = io.optionTemplate.name;
//                                    param = io.param + (io.param * 0 / 100);
//                                    break;
//                                }
//                            }

                            npcSay += "|2|Nguyệt Ấn"
                                    + "\n|7|Tỉ lệ thành công: " + player.combineNew.ratioCombine + "%\n"
                                    + (player.combineNew.countDaNangCap > itemDNC.quantity ? "|7|" : "|1|")
                                    + "Cần " + player.combineNew.countDaNangCap + " " + itemDNC.template.name
                                    + "\n" + (player.combineNew.goldCombine > player.inventory.ruby ? "|7|" : "|1|")
                                    + "Cần " + Util.numberToMoney(50000) + " Hòng Ngọc";
//                            for (Item.ItemOption io : itemDo.itemOptions) {
                            String daNPC = player.combineNew.itemsCombine.size() == 3 && itemDBV != null ? String.format("\nCần tốn %s đá bảo vệ", player.combineNew.countDaBaoVe) : "";
                            if ((level == 2 || level == 4 || level == 6) && !(player.combineNew.itemsCombine.size() == 3 && itemDBV != null)) {
                                npcSay += "\nNếu thất bại sẽ rớt xuống (+" + (level - 1) + ")";
                                break;
                            }
//                            for (Item.ItemOption io : itemDo.itemOptions) {
//                            else if (io.optionTemplate.id >= 35 && io.optionTemplate.id <= 36) {
//                                    npcSay += "|2|Tinh Ấn"
//                                    + "\n|7|Đã Ép Ấn";
//                                    break;
//                                }
                            if (player.combineNew.countDaNangCap > itemDNC.quantity) {
                                this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                        npcSay, "Còn thiếu\n" + (player.combineNew.countDaNangCap - itemDNC.quantity) + " " + itemDNC.template.name);
                            } else if (player.combineNew.goldCombine > player.inventory.ruby) {
                                this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                        npcSay, "Còn thiếu\n" + Util.numberToMoney((25000 - player.inventory.ruby)) + " Hòng Ngọc");
                            } else if (player.combineNew.itemsCombine.size() == 3 && Objects.nonNull(itemDBV) && itemDBV.quantity < player.combineNew.countDaBaoVe) {
                                this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU,
                                        npcSay, "Còn thiếu\n" + (player.combineNew.countDaBaoVe - itemDBV.quantity) + " đá bảo vệ");
                            } else {
                                this.baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE,
                                        npcSay, "Nâng cấp\n" + Util.numberToMoney(player.combineNew.goldCombine) + " Hồng Ngọc" + daNPC, "Từ chối");
                            }
                        } else {
                            this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Trang bị này như cc tao đéo ép", "Đóng");
                        }
                    } else {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Hãy chọn 1 trang bị và 1 loại đá nâng cấp", "Đóng");
                    }
                } else {
                    if (player.combineNew.itemsCombine.size() > 3) {
                        this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Cất đi con ta không thèm", "Đóng");
                        break;
                    }
                    this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Hãy chọn 1 trang bị và 1 loại đá nâng cấp", "Đóng");
                }
                break;
        }
    }

    /**
     * Bắt đầu đập đồ - điều hướng từng loại đập đồ
     *
     * @param player
     */
    public void startCombine(Player player, int type) {
        switch (player.combineNew.typeCombine) {
            case EP_SAO_TRANG_BI:
                epSaoTrangBi(player);
                break;
            case PHA_LE_HOA_TRANG_BI:
                phaLeHoaTrangBi(player, type);
                break;
            case NHAP_NGOC_RONG:
                nhapNgocRong(player);
                break;

            case PHAN_RA_DO_THAN_LINH:
                phanradothanlinh(player);
                break;

            case NANG_CAP_DO_TS:
                openDTS(player);
                break;
            case CHE_TAO_TRANG_BI_TS:
                cheTaoDoTS(player);
                break;
            case NANG_CAP_SKH_VIP_DHD:
                nangCapSKHVIP(player);
                break;
            case NANG_CAP_SKH_TL:
            case NANG_CAP_SKH_HD:
            case NANG_CAP_SKH_TS:
                nangCapSKHTheoCongThuc(player, player.combineNew.typeCombine);
                break;
            case NANG_CAP_VAT_PHAM:
                nangCapVatPham(player);
                break;
            case NANG_CAP_BONG_TAI:
                nangCapBongTai(player);
                break;
            case MO_CHI_SO_BONG_TAI:
                moChiSoBongTai(player);
                break;
            case NANG_CAP_CHAN_MENH:
                nangCapChanMenh(player);
                break;
            case PS_HOA_TRANG_BI:
                psHoaTrangBi(player);
                break;
            case TAY_PS_HOA_TRANG_BI:
                tayHacHoaTrangBi(player);
                break;
            //Sách Tuyệt Kỹ
            case GIAM_DINH_SACH:
                giamDinhSach(player);
                break;
            case TAY_SACH:
                taySach(player);
                break;
            case NANG_CAP_SACH_TUYET_KY:
                nangCapSachTuyetKy(player);
                break;
            case PHUC_HOI_SACH:
                phucHoiSach(player);
                break;
            case PHAN_RA_SACH:
                phanRaSach(player);
                break;
            case NANG_CAP_LEVEL_SKH:
                doiDoHuyDietSangSKH(player);
                break;
            case NANG_CAP_SKH_VIP:
                nangCapSKHVIP(player);
                break;
            case PHAN_RA_DO_THAN_RA_DA_NANG_CAP:
                phanradothanradanangcap(player);
                break;
            //------------End
            case LUYEN_HOA_CHIEN_LINH:
                //
                break;
            case EP_AN_TRANG_BI:
                epantrangbi(player);
                break;
            case NGUYET_AN:
                nguyetan(player);
                break;
            case DOI_DIEM:
                doidiem(player);
                break;
        }

        player.iDMark.setIndexMenu(ConstNpc.IGNORE_MENU);
        player.combineNew.clearParamCombine();
        player.combineNew.lastTimeCombine = System.currentTimeMillis();

    }

    private void psHoaTrangBi(Player player) {

        if (player.combineNew.itemsCombine.size() != 2) {
            Service.getInstance().sendThongBao(player, "Thiếu nguyên liệu");
            return;
        }
        if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isTrangBiHacHoa()).count() != 1) {
            Service.getInstance().sendThongBao(player, "Thiếu trang bị pháp sư");
            return;
        }
        if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.id == 2030).count() != 1) {
            Service.getInstance().sendThongBao(player, "Thiếu đá pháp sư");
            return;
        }
        if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
            Item daHacHoa = player.combineNew.itemsCombine.stream().filter(item -> item.template.id == 2030).findFirst().get();
            Item trangBiHacHoa = player.combineNew.itemsCombine.stream().filter(Item::isTrangBiHacHoa).findFirst().get();
            if (daHacHoa == null) {
                Service.getInstance().sendThongBao(player, "Thiếu đá pháp sư");
                return;
            }
            if (trangBiHacHoa == null) {
                Service.getInstance().sendThongBao(player, "Thiếu trang bị pháp sư");
                return;
            }

            if (trangBiHacHoa != null) {
                for (ItemOption itopt : trangBiHacHoa.itemOptions) {
                    if (itopt.optionTemplate.id == 102) {
                        if (itopt.param >= 8) {
                            Service.getInstance().sendThongBao(player, "Trang bị đã đạt tới giới hạn pháp sư");
                            return;
                        }
                    }
                }
            }

            if (Util.isTrue(100, 100)) {
                sendEffectSuccessCombine(player);
                List<Integer> idOptionHacHoa = Arrays.asList(218, 219, 220, 221);
                int randomOption = idOptionHacHoa.get(Util.nextInt(0, 3));
                if (!trangBiHacHoa.haveOption(107)) {
                    trangBiHacHoa.itemOptions.add(new ItemOption(107, 1));
                    trangBiHacHoa.itemOptions.add(new ItemOption(222, 1));
                } else {
                    for (ItemOption itopt : trangBiHacHoa.itemOptions) {
                        if (itopt.optionTemplate.id == 107) {
                            itopt.param += 1;
                            break;
                        }
                    }
                }
                if (!trangBiHacHoa.haveOption(102)) {
                    trangBiHacHoa.itemOptions.add(new ItemOption(102, 1));
                } else {
                    for (ItemOption itopt : trangBiHacHoa.itemOptions) {
                        if (itopt.optionTemplate.id == 102) {
                            itopt.param += 1;
                            break;
                        }
                    }
                }
                if (!trangBiHacHoa.haveOption(randomOption)) {
                    trangBiHacHoa.itemOptions.add(new ItemOption(randomOption, 3));
                } else {
                    for (ItemOption itopt : trangBiHacHoa.itemOptions) {
                        if (itopt.optionTemplate.id == randomOption) {
                            itopt.param += 3;
                            break;
                        }
                    }
                }
//                Service.getInstance().sendThongBao(player, "Bạn đã nâng cấp thành công");
            } else {
                sendEffectFailCombine(player);
            }
            InventoryServiceNew.gI().subQuantityItemsBag(player, daHacHoa, 1);
            InventoryServiceNew.gI().sendItemBags(player);
            Service.getInstance().sendMoney(player);
//            player.combineNew.itemsCombine.clear();
//            reOpenItemCombine(player);
        } else {
            Service.getInstance().sendThongBao(player, "Bạn phải có ít nhất 1 ô trống hành trang");
        }
    }

    private void tayHacHoaTrangBi(Player player) {

        if (player.combineNew.itemsCombine.size() != 2) {
            Service.getInstance().sendThongBao(player, "Thiếu nguyên liệu");
            return;
        }
        if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isTrangBiHacHoa()).count() != 1) {
            Service.getInstance().sendThongBao(player, "Thiếu trang bị hắc hóa");
            return;
        }
        if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.id == 1274).count() != 1) {
            Service.getInstance().sendThongBao(player, "Thiếu đá pháp sư");
            return;
        }

        Item buagiaihachoa = player.combineNew.itemsCombine.stream().filter(item -> item.template.id == 1274).findFirst().get();
        Item trangBiHacHoa = player.combineNew.itemsCombine.stream().filter(Item::isTrangBiHacHoa).findFirst().get();
        if (buagiaihachoa == null) {
            Service.getInstance().sendThongBao(player, "Thiếu bùa giải pháp sư");
            return;
        }
        if (trangBiHacHoa == null) {
            Service.getInstance().sendThongBao(player, "Thiếu trang bị pháp sư");
            return;
        }

        if (Util.isTrue(100, 100)) {
            sendEffectSuccessCombine(player);
            List<Integer> idOptionHacHoa = Arrays.asList(218, 219, 220, 221);

            ItemOption option_222 = new ItemOption();
            ItemOption option_218 = new ItemOption();
            ItemOption option_219 = new ItemOption();
            ItemOption option_220 = new ItemOption();
            ItemOption option_221 = new ItemOption();
            ItemOption option_102 = new ItemOption();
            ItemOption option_107 = new ItemOption();

            for (ItemOption itopt : trangBiHacHoa.itemOptions) {
                if (itopt.optionTemplate.id == 222) {
                    option_222 = itopt;
                }
                if (itopt.optionTemplate.id == 218) {
                    option_218 = itopt;
                }
                if (itopt.optionTemplate.id == 219) {
                    option_219 = itopt;
                }
                if (itopt.optionTemplate.id == 220) {
                    option_220 = itopt;
                }
                if (itopt.optionTemplate.id == 221) {
                    option_221 = itopt;
                }
                if (itopt.optionTemplate.id == 102) {
                    option_102 = itopt;
                }
                if (itopt.optionTemplate.id == 107) {
                    option_107 = itopt;
                }
            }
            if (option_222 != null) {
                trangBiHacHoa.itemOptions.remove(option_222);
            }
            if (option_218 != null) {
                trangBiHacHoa.itemOptions.remove(option_218);
            }
            if (option_219 != null) {
                trangBiHacHoa.itemOptions.remove(option_219);
            }
            if (option_220 != null) {
                trangBiHacHoa.itemOptions.remove(option_220);
            }
            if (option_221 != null) {
                trangBiHacHoa.itemOptions.remove(option_221);
            }
            if (option_102 != null) {
                trangBiHacHoa.itemOptions.remove(option_102);
            }
            if (option_107 != null) {
                trangBiHacHoa.itemOptions.remove(option_107);
            }
//                Service.getInstance().sendThongBao(player, "Bạn đã tẩy thành công");
            InventoryServiceNew.gI().sendItemBags(player);
        } else {
            sendEffectFailCombine(player);
        }
        InventoryServiceNew.gI().subQuantityItemsBag(player, buagiaihachoa, 1);
        InventoryServiceNew.gI().sendItemBags(player);
        Service.getInstance().sendMoney(player);
        player.combineNew.itemsCombine.clear();
        reOpenItemCombine(player);
    }

    private void phanradothanradanangcap(Player player) {
        if (player.combineNew.itemsCombine.size() == 1) {
            player.inventory.gold -= 50000000;
            List<Integer> itemdov2 = new ArrayList<>(Arrays.asList(562, 564, 566));
            Item item = player.combineNew.itemsCombine.get(0);
            int couponAdd = itemdov2.stream().anyMatch(t -> t == item.template.id) ? 2 : item.template.id == 561 ? 3 : 1;
            sendEffectSuccessCombine(player);
            Item dangusac = ItemService.gI().createNewItem((short) 1342);
            Item dangusac1 = ItemService.gI().createNewItem((short) 1342);
            Item dangusac2 = ItemService.gI().createNewItem((short) 1342);
            InventoryServiceNew.gI().addItemBag(player, dangusac);
            InventoryServiceNew.gI().sendItemBags(player);
            if (item.template.id == 561) {
                InventoryServiceNew.gI().addItemBag(player, dangusac);
                InventoryServiceNew.gI().addItemBag(player, dangusac1);
                InventoryServiceNew.gI().addItemBag(player, dangusac2);
                InventoryServiceNew.gI().sendItemBags(player);
            } else if (item.template.id == 562 || item.template.id == 564 || item.template.id == 566) {
                InventoryServiceNew.gI().addItemBag(player, dangusac);
                InventoryServiceNew.gI().addItemBag(player, dangusac1);
                InventoryServiceNew.gI().sendItemBags(player);
            }
            Service.gI().sendThongBaoOK(player, "Bạn Nhận Được Đá Ngũ Sắc");
            InventoryServiceNew.gI().subQuantityItemsBag(player, item, 1);
            player.combineNew.itemsCombine.clear();
            InventoryServiceNew.gI().sendItemBags(player);
            Service.gI().sendMoney(player);
            reOpenItemCombine(player);
        }
    }

    private void doiDoHuyDietSangSKH(Player player) {
        if (player.combineNew.itemsCombine.size() != 1
                || !player.combineNew.itemsCombine.get(0).isDHD()) {
            Service.gI().sendThongBao(player, "Cần đúng 1 món đồ Hủy Diệt");
            return;
        }
        if (InventoryServiceNew.gI().getCountEmptyBag(player) <= 0) {
            Service.gI().sendThongBao(player, "Bạn phải có ít nhất 1 ô trống hành trang");
            return;
        }
        Item itemHuyDiet = player.combineNew.itemsCombine.get(0);
        int gender = itemHuyDiet.template.id <= 651 ? 0 : itemHuyDiet.template.id <= 653 ? 1 : 2;
        int[][] itemIds = {{0, 6, 21, 27, 12}, {1, 7, 22, 28, 12}, {2, 8, 23, 29, 12}};
        int type = itemHuyDiet.template.type;
        if (type < 0 || type >= itemIds[gender].length) {
            Service.gI().sendThongBao(player, "Không xác định được loại trang bị");
            return;
        }
        Item itemSKH = ItemService.gI().itemSKH(itemIds[gender][type], randomSKHIdTheoSet(gender));
        InventoryServiceNew.gI().subQuantityItemsBag(player, itemHuyDiet, 1);
        InventoryServiceNew.gI().addItemBag(player, itemSKH);
        InventoryServiceNew.gI().sendItemBags(player);
        Service.gI().sendThongBao(player, "Bạn đã nhận được " + itemSKH.template.name);
    }

    private boolean isValidInputNangCapSKHVIP(Player player) {
        if (player.combineNew.itemsCombine.size() != 4) {
            return false;
        }
        if (player.combineNew.itemsCombine.stream().filter(Item::isDHD).count() != 1
                || player.combineNew.itemsCombine.stream().filter(Item::isDTL).count() != 3) {
            return false;
        }
        List<Item> itemTL = player.combineNew.itemsCombine.stream()
                .filter(Item::isDTL).collect(Collectors.toList());
        return itemTL.stream().map(item -> item.template.id).distinct().count() == 3;
    }

    private boolean isValidInputNangCapSKH(Player player, int type) {
        if (player.combineNew.itemsCombine.size() != 4) {
            return false;
        }
        long dts = player.combineNew.itemsCombine.stream().filter(Item::isDTS).count();
        long dtl = player.combineNew.itemsCombine.stream().filter(Item::isDTL).count();
        long dhd = player.combineNew.itemsCombine.stream().filter(Item::isDHD).count();
        switch (type) {
            case NANG_CAP_SKH_TL:
                return dts == 1 && dhd == 3;
            case NANG_CAP_SKH_HD:
                return dtl == 1 && dhd == 3
                        && player.combineNew.itemsCombine.stream().anyMatch(item -> item.isDTL() && item.isSKH());
            case NANG_CAP_SKH_TS:
                List<Item> itemDTS = player.combineNew.itemsCombine.stream()
                        .filter(Item::isDTS).collect(Collectors.toList());
                return dhd == 1
                        && itemDTS.size() == 3
                        && player.combineNew.itemsCombine.stream().anyMatch(item -> item.isDHD() && item.isSKH())
                        && itemDTS.stream().map(item -> item.template.id).distinct().count() == 3;
            default:
                return false;
        }
    }

    private void showInfoNangCapSKH(Player player, int type) {
        if (!isValidInputNangCapSKH(player, type)) {
            String message = type == NANG_CAP_SKH_TL
                    ? "Cần 1 món Thiên Sứ và 3 món Hủy Diệt"
                    : type == NANG_CAP_SKH_HD
                    ? "Cần 1 món Thần Linh SKH và 3 món Hủy Diệt"
                    : "Cần 1 món Hủy Diệt SKH và 3 món Thiên Sứ khác nhau";
            this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, message, "Đóng");
            return;
        }
        String result = type == NANG_CAP_SKH_TL ? "Thần Linh SKH"
                : type == NANG_CAP_SKH_HD ? "Hủy Diệt SKH" : "Thiên Sứ SKH";
        this.baHatMit.createOtherMenu(player, ConstNpc.MENU_START_COMBINE,
                "Nguyên liệu hợp lệ\nKết quả: 1 món " + result + " cùng loại\nOption kích hoạt ngẫu nhiên",
                "Nâng Cấp", "Từ chối");
    }

    private void nangCapSKHTheoCongThuc(Player player, int type) {
        if (!isValidInputNangCapSKH(player, type)) {
            Service.gI().sendThongBao(player, "Nguyên liệu không hợp lệ");
            return;
        }
        if (InventoryServiceNew.gI().getCountEmptyBag(player) <= 0) {
            Service.gI().sendThongBao(player, "Bạn phải có ít nhất 1 ô trống hành trang");
            return;
        }
        Item source = player.combineNew.itemsCombine.stream()
                .filter(item -> type == NANG_CAP_SKH_TL ? item.isDTS()
                        : type == NANG_CAP_SKH_HD ? item.isDTL() && item.isSKH()
                        : item.isDHD() && item.isSKH())
                .findFirst().orElse(null);
        if (source == null) {
            Service.gI().sendThongBao(player, "Không xác định được trang bị gốc");
            return;
        }
        int gender = source.template.gender == 3 ? 2 : source.template.gender;
        int itemType = source.template.type;
        if (gender < 0 || gender >= ITEM_DHD.length || itemType < 0 || itemType > 4) {
            Service.gI().sendThongBao(player, "Không xác định được loại trang bị");
            return;
        }
        short resultId;
        if (type == NANG_CAP_SKH_TL) {
            resultId = itemType == 4 ? Manager.radaSKHVip[6]
                    : Manager.doSKHVip[gender][itemType][6];
        } else if (type == NANG_CAP_SKH_HD) {
            resultId = (short) ITEM_DHD[gender][itemType];
        } else {
            resultId = (short) ITEM_DTS[gender][itemType];
        }
        Item result = createRandomSKHItem(resultId, gender, type);
        if (result == null) {
            Service.gI().sendThongBao(player, "Không tạo được trang bị kết quả");
            return;
        }
        sendEffectSuccessCombine(player);
        player.combineNew.itemsCombine.forEach(item -> InventoryServiceNew.gI().subQuantityItemsBag(player, item, 1));
        InventoryServiceNew.gI().addItemBag(player, result);
        InventoryServiceNew.gI().sendItemBags(player);
        Service.gI().sendThongBao(player, "Bạn nhận được " + result.template.name + " SKH");
        player.combineNew.itemsCombine.clear();
        reOpenItemCombine(player);
    }

    private Item createRandomSKHItem(short itemId, int gender, int type) {
        int skhId = ItemService.gI().randomSKHId((byte) gender);
        Item item;
        if (new Item(itemId).isDTL()) {
            item = Util.ratiItemTL(itemId);
            item.itemOptions.removeIf(option -> option.optionTemplate.id == 21);
            item.itemOptions.add(new Item.ItemOption(skhId, 2));
            item.itemOptions.add(new Item.ItemOption(ItemService.gI().optionIdSKH(skhId),
                    ItemService.gI().skhOptionParam(ItemService.gI().optionIdSKH(skhId))));
            item.itemOptions.add(new Item.ItemOption(21, 15));
            item.itemOptions.add(new Item.ItemOption(30, 1));
        } else if (new Item(itemId).isDHD()) {
            item = ItemService.gI().randomCS_DHD(itemId, gender);
            item.itemOptions.add(new Item.ItemOption(skhId, 2));
            item.itemOptions.add(new Item.ItemOption(ItemService.gI().optionIdSKH(skhId),
                    ItemService.gI().skhOptionParam(ItemService.gI().optionIdSKH(skhId))));
        } else {
            item = ItemService.gI().itemSKH(itemId, skhId);
        }
        return item;
    }

    private int randomSKHIdTheoSet(int gender) {
        int safeGender = Math.max(0, Math.min(gender, TY_LE_SET_SKH.length - 1));
        int[] ratios = TY_LE_SET_SKH[safeGender];
        int value = Util.nextInt(1, Arrays.stream(ratios).sum());
        int accumulated = 0;
        int[][] options = {{128, 129, 127}, {130, 131, 132}, {133, 135, 134}};
        for (int set = 0; set < ratios.length; set++) {
            accumulated += ratios[set];
            if (value <= accumulated) {
                return options[safeGender][set];
            }
        }
        return options[safeGender][options[safeGender].length - 1];
    }

    private int randomPhanKhucSKHVIP(int gender, int type) {
        int safeGender = Math.max(0, Math.min(gender, TY_LE_PHAN_KHUC_SKH_VIP.length - 1));
        int safeType = Math.max(0, Math.min(type, TY_LE_PHAN_KHUC_SKH_VIP[safeGender].length - 1));
        int[] ratios = TY_LE_PHAN_KHUC_SKH_VIP[safeGender][safeType];
        int total = Arrays.stream(ratios).sum();
        int value = Util.nextInt(1, total);
        int accumulated = 0;
        for (int i = 0; i < ratios.length; i++) {
            accumulated += ratios[i];
            if (value <= accumulated) {
                return i;
            }
        }
        return ratios.length - 1;
    }

    private String getVipTierRateText(Player player) {
        Item item = player.combineNew.itemsCombine.stream().filter(Item::isDHD).findFirst().orElse(null);
        if (item == null) {
            return "70% / 25% / 5%";
        }
        int gender = item.template.gender == 3 ? 2 : item.template.gender;
        int type = Math.max(0, Math.min(item.template.type, TY_LE_PHAN_KHUC_SKH_VIP[0].length - 1));
        int[] ratios = TY_LE_PHAN_KHUC_SKH_VIP[Math.max(0, Math.min(gender, 2))][type];
        return ratios[0] + "% / " + ratios[1] + "% / " + ratios[2] + "%";
    }

    private void nangCapSKHVIP(Player player) {
        if (!isValidInputNangCapSKHVIP(player)) {
            Service.gI().sendThongBao(player, "Cần 1 món Hủy Diệt và 3 món Thần Linh khác nhau");
            return;
        }
        if (InventoryServiceNew.gI().getCountEmptyBag(player) <= 0) {
            Service.gI().sendThongBao(player, "Bạn phải có ít nhất 1 ô trống hành trang");
            return;
        }
        Item itemHuyDiet = player.combineNew.itemsCombine.stream().filter(Item::isDHD).findFirst().get();
        int gender = itemHuyDiet.template.gender == 3 ? 2 : itemHuyDiet.template.gender;
        int type = itemHuyDiet.template.type;
        int tier = randomPhanKhucSKHVIP(gender, type);
        short itemId;
        if (type == 4) {
            itemId = Manager.radaSKHVip[Math.min(tier + 3, Manager.radaSKHVip.length - 1)];
        } else if (gender >= 0 && gender < Manager.doSKHVip.length && type >= 0 && type < Manager.doSKHVip[gender].length) {
            short[] tierItems = Manager.doSKHVip[gender][type];
            int index = tier == 0 ? Util.nextInt(0, 2) : tier == 1 ? Util.nextInt(3, 4) : 5;
            itemId = tierItems[Math.min(index, tierItems.length - 1)];
        } else {
            Service.gI().sendThongBao(player, "Không xác định được phân khúc trang bị");
            return;
        }
        Item itemSKH = ItemService.gI().itemSKH(itemId, ItemService.gI().randomSKHId((byte) gender));
        player.combineNew.itemsCombine.forEach(item -> InventoryServiceNew.gI().subQuantityItemsBag(player, item, 1));
        InventoryServiceNew.gI().addItemBag(player, itemSKH);
        InventoryServiceNew.gI().sendItemBags(player);
        Service.gI().sendThongBao(player, "Bạn đã nhận được " + itemSKH.template.name + " SKH VIP");
    }

    private void nangCapLEVELSKH(Player player) {
        if (player.combineNew.itemsCombine.size() >= 2 && player.combineNew.itemsCombine.size() < 5) {
            if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type < 5).count() != 2) {

            }
            if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type == 14).count() != 1) {

            }
            if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDTL()).count() != 1) {
            }
            if (player.combineNew.itemsCombine.size() == 4 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.id == 987).count() != 1) {

            }
            Item itemDo = null;
            Item itemDNC = null;
            Item itemDBV = null;
            for (int j = 0; j < player.combineNew.itemsCombine.size(); j++) {
                if (player.combineNew.itemsCombine.get(j).isNotNullItem()) {
                    if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.get(j).template.id == 987) {
                        itemDBV = player.combineNew.itemsCombine.get(j);
                        continue;
                    }
                    if (player.combineNew.itemsCombine.get(j).template.type < 5) {
                        itemDo = player.combineNew.itemsCombine.get(j);
                    } else {
                        itemDNC = player.combineNew.itemsCombine.get(j);
                    }
                }
            }
            if (isCoupleItemNangCapSKHCheck(itemDo, itemDNC)) {
                int countDaNangCap = player.combineNew.countDaNangCap;
                int gold = player.combineNew.goldCombine;
                short countDaBaoVe = player.combineNew.countDaBaoVe;
                if (player.inventory.gold < gold) {
                    Service.getInstance().sendThongBao(player, "Không đủ vàng để thực hiện");
                    return;
                }
                if (itemDNC.quantity < countDaNangCap) {
                    return;
                }
                int level = 0;
                ItemOption optionLevel = null;

                for (ItemOption io : itemDo.itemOptions) {
                    if (io.optionTemplate.id == 223
                            || io.optionTemplate.id == 224
                            || io.optionTemplate.id == 225
                            || io.optionTemplate.id == 226
                            || io.optionTemplate.id == 227
                            || io.optionTemplate.id == 228
                            || io.optionTemplate.id == 229
                            || io.optionTemplate.id == 230
                            || io.optionTemplate.id == 231) {
                        level = io.param;
                        optionLevel = io;
                        break;
                    }
                }
                if (level == 4) {
                    Service.getInstance().sendThongBao(player, "Đã đạt cấp tối đa");
                    return;
                }
                if (level < MAX_LEVEL_SKH) {
                    player.inventory.gold -= gold;
                    ItemOption option = null;
                    for (ItemOption io : itemDo.itemOptions) {
                        if (io.optionTemplate.id == 232
                                || io.optionTemplate.id == 233
                                || io.optionTemplate.id == 234
                                || io.optionTemplate.id == 235
                                || io.optionTemplate.id == 236
                                || io.optionTemplate.id == 237
                                || io.optionTemplate.id == 238
                                || io.optionTemplate.id == 239
                                || io.optionTemplate.id == 240) {
                            option = io;
                        }

                    }
                    //thành công
                    if (Util.isTrue(player.combineNew.ratioCombine, 100) || player.isAdmin()) {
                        option.param += 20;
                        optionLevel.param++;
                        sendEffectSuccessCombine(player);
                        System.out.println("lỗi 6:");
                        //thất bại
                    } else {
                        if ((level == 2 || level == 4 || level == 6) && (player.combineNew.itemsCombine.size() != 3)) {
                            option.param -= 20;
                            optionLevel.param--;
                        }
                        sendEffectFailCombine(player);
                    }
                    List<Item> itemDTL = player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDTL() && !item.isSKHLEVEL()).collect(Collectors.toList());
                    InventoryServiceNew.gI().subQuantityItemsBag(player, itemDNC, player.combineNew.countDaNangCap);
                    itemDTL.forEach(i -> InventoryServiceNew.gI().subQuantityItemsBag(player, i, 1));
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.getInstance().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
    }

    private void giamDinhSach(Player player) {
        if (player.combineNew.itemsCombine.size() == 2) {

            Item sachTuyetKy = null;
            Item buaGiamDinh = null;
            for (Item item : player.combineNew.itemsCombine) {
                if (issachTuyetKy(item)) {
                    sachTuyetKy = item;
                } else if (item.template.id == 1270) {
                    buaGiamDinh = item;
                }
            }
            if (sachTuyetKy != null && buaGiamDinh != null) {
                Item sachTuyetKy_2 = ItemService.gI().createNewItem((short) sachTuyetKy.template.id);
                if (checkHaveOption(sachTuyetKy, 0, 217)) {
                    int tyle = new Random().nextInt(10);
                    if (tyle >= 0 && tyle <= 33) {
                        sachTuyetKy_2.itemOptions.add(new ItemOption(50, new Util().nextInt(5, 10)));
                    } else if (tyle > 33 && tyle <= 66) {
                        sachTuyetKy_2.itemOptions.add(new ItemOption(77, new Util().nextInt(10, 15)));
                    } else {
                        sachTuyetKy_2.itemOptions.add(new ItemOption(103, new Util().nextInt(10, 15)));
                    }
                    for (int i = 1; i < sachTuyetKy.itemOptions.size(); i++) {
                        sachTuyetKy_2.itemOptions.add(new ItemOption(sachTuyetKy.itemOptions.get(i).optionTemplate.id, sachTuyetKy.itemOptions.get(i).param));
                    }
                    sendEffectSuccessCombine(player);
                    InventoryServiceNew.gI().addItemBag(player, sachTuyetKy_2);
                    InventoryServiceNew.gI().subQuantityItemsBag(player, sachTuyetKy, 1);
                    InventoryServiceNew.gI().subQuantityItemsBag(player, buaGiamDinh, 1);
                    InventoryServiceNew.gI().sendItemBags(player);
                    reOpenItemCombine(player);
                } else {
                    Service.getInstance().sendThongBao(player, "Còn cái nịt mà giám");
                    return;
                }
            }
        }
    }

    private void nangCapSachTuyetKy(Player player) {
        if (player.combineNew.itemsCombine.size() == 2) {

            Item sachTuyetKy = null;
            Item kimBamGiay = null;

            for (Item item : player.combineNew.itemsCombine) {
                if (issachTuyetKy(item)) {
                    sachTuyetKy = item;
                } else if (item.template.id == 1269) {
                    kimBamGiay = item;
                }
            }
            Item sachTuyetKy_2 = ItemService.gI().createNewItem((short) ((short) sachTuyetKy.template.id + 1));
            if (sachTuyetKy != null && kimBamGiay != null) {
                if (kimBamGiay.quantity < 10) {
                    Service.getInstance().sendThongBao(player, "Không đủ Kìm bấm giấy mà đòi nâng cấp");
                    return;
                }
                if (checkHaveOption(sachTuyetKy, 0, 217)) {
                    Service.getInstance().sendThongBao(player, "Chưa giám định mà đòi nâng cấp");
                    return;
                }
                for (int i = 0; i < sachTuyetKy.itemOptions.size(); i++) {
                    sachTuyetKy_2.itemOptions.add(new ItemOption(sachTuyetKy.itemOptions.get(i).optionTemplate.id, sachTuyetKy.itemOptions.get(i).param));
                }
                sendEffectSuccessCombine(player);
                InventoryServiceNew.gI().addItemBag(player, sachTuyetKy_2);
                InventoryServiceNew.gI().subQuantityItemsBag(player, sachTuyetKy, 1);
                InventoryServiceNew.gI().subQuantityItemsBag(player, kimBamGiay, 10);
                InventoryServiceNew.gI().sendItemBags(player);
                reOpenItemCombine(player);

            }
        }
    }

    private void phucHoiSach(Player player) {
        if (player.combineNew.itemsCombine.size() == 1) {
            Item cuonSachCu = InventoryServiceNew.gI().findItemBag(player, (short) 1392);
            int goldPhanra = 10_000_000;
            Item sachTuyetKy = null;
            for (Item item : player.combineNew.itemsCombine) {
                if (issachTuyetKy(item)) {
                    sachTuyetKy = item;
                }
            }
            if (sachTuyetKy != null) {
                int doBen = 0;
                ItemOption optionLevel = null;
                for (ItemOption io : sachTuyetKy.itemOptions) {
                    if (io.optionTemplate.id == 215) {
                        doBen = io.param;
                        optionLevel = io;
                        break;
                    }
                }
                if (cuonSachCu == null) {
                    Service.getInstance().sendThongBaoOK(player, "Cần sách tuyệt kỹ và 10 cuốn sách cũ");
                    return;
                }
                if (cuonSachCu.quantity < 10) {
                    Service.getInstance().sendThongBaoOK(player, "Cần sách tuyệt kỹ và 10 cuốn sách cũ");
                    return;
                }
                if (player.inventory.gold < goldPhanra) {
                    Service.getInstance().sendThongBao(player, "Không có tiền mà đòi phục hồi à");
                    return;
                }
                if (doBen != 1000) {
                    for (int i = 0; i < sachTuyetKy.itemOptions.size(); i++) {
                        if (sachTuyetKy.itemOptions.get(i).optionTemplate.id == 215) {
                            sachTuyetKy.itemOptions.get(i).param = 1000;
                            break;
                        }
                    }
                    player.inventory.gold -= 10_000_000;
                    InventoryServiceNew.gI().subQuantityItemsBag(player, cuonSachCu, 10);
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.getInstance().sendMoney(player);
                    sendEffectSuccessCombine(player);
                    reOpenItemCombine(player);
                } else {
                    Service.getInstance().sendThongBao(player, "Còn dùng được phục hồi ăn cứt à");
                    return;
                }
            }
        }
    }

    private void phanRaSach(Player player) {
        if (player.combineNew.itemsCombine.size() == 1) {
            Item cuonSachCu = ItemService.gI().createNewItem((short) 1392, 5);
            int goldPhanra = 10_000_000;
            Item sachTuyetKy = null;
            for (Item item : player.combineNew.itemsCombine) {
                if (issachTuyetKy(item)) {
                    sachTuyetKy = item;
                }
            }
            if (sachTuyetKy != null) {
                int luotTay = 0;
                ItemOption optionLevel = null;
                for (ItemOption io : sachTuyetKy.itemOptions) {
                    if (io.optionTemplate.id == 214) {
                        luotTay = io.param;
                        optionLevel = io;
                        break;
                    }
                }
                if (player.inventory.gold < goldPhanra) {
                    Service.getInstance().sendThongBao(player, "Không có tiền mà đòi phân rã à");
                    return;
                }
                if (luotTay == 0) {

                    player.inventory.gold -= goldPhanra;
                    InventoryServiceNew.gI().subQuantityItemsBag(player, sachTuyetKy, 1);
                    InventoryServiceNew.gI().addItemBag(player, cuonSachCu);
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.getInstance().sendMoney(player);
                    sendEffectSuccessCombine(player);
                    reOpenItemCombine(player);

                } else {
                    Service.getInstance().sendThongBao(player, "Còn dùng được phân rã ăn cứt à");
                    return;
                }
            }
        }
    }

    private void taySach(Player player) {
        if (player.combineNew.itemsCombine.size() == 1) {
            Item sachTuyetKy = null;
            for (Item item : player.combineNew.itemsCombine) {
                if (issachTuyetKy(item)) {
                    sachTuyetKy = item;
                }
            }
            if (sachTuyetKy != null) {
                int luotTay = 0;
                ItemOption optionLevel = null;
                for (ItemOption io : sachTuyetKy.itemOptions) {
                    if (io.optionTemplate.id == 214) {
                        luotTay = io.param;
                        optionLevel = io;
                        break;
                    }
                }
                if (luotTay == 0) {
                    Service.getInstance().sendThongBao(player, "Còn cái nịt mà tẩy");
                    return;
                }
                Item sachTuyetKy_2 = ItemService.gI().createNewItem((short) sachTuyetKy.template.id);
                if (checkHaveOption(sachTuyetKy, 0, 217)) {
                    Service.getInstance().sendThongBao(player, "Còn cái nịt mà tẩy");
                    return;
                }
                int tyle = new Random().nextInt(10);
                for (int i = 1; i < sachTuyetKy.itemOptions.size(); i++) {
                    if (sachTuyetKy.itemOptions.get(i).optionTemplate.id == 214) {
                        sachTuyetKy.itemOptions.get(i).param -= 1;
                    }
                }
                sachTuyetKy_2.itemOptions.add(new ItemOption(217, 0));
                for (int i = 1; i < sachTuyetKy.itemOptions.size(); i++) {
                    sachTuyetKy_2.itemOptions.add(new ItemOption(sachTuyetKy.itemOptions.get(i).optionTemplate.id, sachTuyetKy.itemOptions.get(i).param));
                }
                sendEffectSuccessCombine(player);
                InventoryServiceNew.gI().addItemBag(player, sachTuyetKy_2);
                InventoryServiceNew.gI().subQuantityItemsBag(player, sachTuyetKy, 1);
                InventoryServiceNew.gI().sendItemBags(player);
                reOpenItemCombine(player);
            }
        }
    }

    public void GetTrangBiKichHoathuydiet(Player player, int id) {
        Item item = ItemService.gI().createNewItem((short) id);
        int[][] optionNormal = {{127, 128}, {130, 132}, {133, 135}};
        int[][] paramNormal = {{139, 140}, {142, 144}, {136, 138}};
        int[][] optionVIP = {{129}, {131}, {134}};
        int[][] paramVIP = {{141}, {143}, {137}};
        int random = Util.nextInt(optionNormal.length);
        int randomSkh = Util.nextInt(100);
        if (item.template.type == 0) {
            item.itemOptions.add(new ItemOption(47, Util.nextInt(1500, 2000)));
        }
        if (item.template.type == 1) {
            item.itemOptions.add(new ItemOption(22, Util.nextInt(100, 150)));
        }
        if (item.template.type == 2) {
            item.itemOptions.add(new ItemOption(0, Util.nextInt(9000, 11000)));
        }
        if (item.template.type == 3) {
            item.itemOptions.add(new ItemOption(23, Util.nextInt(90, 150)));
        }
        if (item.template.type == 4) {
            item.itemOptions.add(new ItemOption(14, Util.nextInt(15, 20)));
        }
        if (randomSkh <= 20) {//tile ra do kich hoat
            if (randomSkh <= 5) { // tile ra option vip
                item.itemOptions.add(new ItemOption(optionVIP[player.gender][0], 1));
                item.itemOptions.add(new ItemOption(paramVIP[player.gender][0], 1));
                item.itemOptions.add(new ItemOption(30, 0));
            } else {// 
                item.itemOptions.add(new ItemOption(optionNormal[player.gender][random], 1));
                item.itemOptions.add(new ItemOption(paramNormal[player.gender][random], 1));
                item.itemOptions.add(new ItemOption(30, 0));
            }
        }

        InventoryServiceNew.gI().addItemBag(player, item);
        InventoryServiceNew.gI().sendItemBags(player);
    }

    public void GetTrangBiKichHoatthiensu(Player player, int id) {
        Item item = ItemService.gI().createNewItem((short) id);
        int[][] optionNormal = {{127, 128}, {130, 132}, {133, 135}};
        int[][] paramNormal = {{139, 140}, {142, 144}, {136, 138}};
        int[][] optionVIP = {{129}, {131}, {134}};
        int[][] paramVIP = {{141}, {143}, {137}};
        int random = Util.nextInt(optionNormal.length);
        int randomSkh = Util.nextInt(100);
        if (item.template.type == 0) {
            item.itemOptions.add(new ItemOption(47, Util.nextInt(2000, 2500)));
        }
        if (item.template.type == 1) {
            item.itemOptions.add(new ItemOption(22, Util.nextInt(150, 200)));
        }
        if (item.template.type == 2) {
            item.itemOptions.add(new ItemOption(0, Util.nextInt(18000, 20000)));
        }
        if (item.template.type == 3) {
            item.itemOptions.add(new ItemOption(23, Util.nextInt(150, 200)));
        }
        if (item.template.type == 4) {
            item.itemOptions.add(new ItemOption(14, Util.nextInt(20, 25)));
        }
        if (randomSkh <= 20) {//tile ra do kich hoat
            if (randomSkh <= 5) { // tile ra option vip
                item.itemOptions.add(new ItemOption(optionVIP[player.gender][0], 1));
                item.itemOptions.add(new ItemOption(paramVIP[player.gender][0], 1));
                item.itemOptions.add(new ItemOption(30, 0));
            } else {// 
                item.itemOptions.add(new ItemOption(optionNormal[player.gender][random], 1));
                item.itemOptions.add(new ItemOption(paramNormal[player.gender][random], 1));
                item.itemOptions.add(new ItemOption(30, 0));
            }
        }

        InventoryServiceNew.gI().addItemBag(player, item);
        InventoryServiceNew.gI().sendItemBags(player);
    }

    public void khilv2(Player player, int id) {
        Item item = ItemService.gI().createNewItem((short) id);
        item.itemOptions.add(new ItemOption(50, 20));//sd
        item.itemOptions.add(new ItemOption(77, 20));//hp
        item.itemOptions.add(new ItemOption(103, 20));//ki
        item.itemOptions.add(new ItemOption(14, 20));//cm
        item.itemOptions.add(new ItemOption(5, 20));//sd cm
        item.itemOptions.add(new ItemOption(106, 0));
        item.itemOptions.add(new ItemOption(34, 0));
        InventoryServiceNew.gI().addItemBag(player, item);
        InventoryServiceNew.gI().sendItemBags(player);
    }

    public void khilv3(Player player, int id) {
        Item item = ItemService.gI().createNewItem((short) id);
        item.itemOptions.add(new ItemOption(50, 22));//sd
        item.itemOptions.add(new ItemOption(77, 22));//hp
        item.itemOptions.add(new ItemOption(103, 22));//ki
        item.itemOptions.add(new ItemOption(14, 22));//cm
        item.itemOptions.add(new ItemOption(5, 22));//sd cm
        item.itemOptions.add(new ItemOption(106, 0));
        item.itemOptions.add(new ItemOption(35, 0));
        InventoryServiceNew.gI().addItemBag(player, item);
        InventoryServiceNew.gI().sendItemBags(player);
    }

    public void khilv4(Player player, int id) {
        Item item = ItemService.gI().createNewItem((short) id);
        item.itemOptions.add(new ItemOption(50, 24));//sd
        item.itemOptions.add(new ItemOption(77, 24));//hp
        item.itemOptions.add(new ItemOption(103, 24));//ki
        item.itemOptions.add(new ItemOption(14, 24));//cm
        item.itemOptions.add(new ItemOption(5, 24));//sd cm
        item.itemOptions.add(new ItemOption(106, 0));
        item.itemOptions.add(new ItemOption(36, 0));
        InventoryServiceNew.gI().addItemBag(player, item);
        InventoryServiceNew.gI().sendItemBags(player);
    }

    public void khilv5(Player player, int id) {
        Item item = ItemService.gI().createNewItem((short) id);
        item.itemOptions.add(new ItemOption(50, 26));//sd
        item.itemOptions.add(new ItemOption(77, 26));//hp
        item.itemOptions.add(new ItemOption(103, 26));//ki
        item.itemOptions.add(new ItemOption(14, 26));//cm
        item.itemOptions.add(new ItemOption(5, 26));//sd cm
        item.itemOptions.add(new ItemOption(106, 0));
        item.itemOptions.add(new ItemOption(36, 0));
        InventoryServiceNew.gI().addItemBag(player, item);
        InventoryServiceNew.gI().sendItemBags(player);
    }

    private void doiKiemThan(Player player) {
        if (player.combineNew.itemsCombine.size() == 3) {
            Item keo = null, luoiKiem = null, chuoiKiem = null;
            for (Item it : player.combineNew.itemsCombine) {
                if (it.template.id == 2015) {
                    keo = it;
                } else if (it.template.id == 2016) {
                    chuoiKiem = it;
                } else if (it.template.id == 2017) {
                    luoiKiem = it;
                }
            }
            if (keo != null && keo.quantity >= 99 && luoiKiem != null && chuoiKiem != null) {
                if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                    sendEffectSuccessCombine(player);
                    Item item = ItemService.gI().createNewItem((short) 2018);
                    item.itemOptions.add(new Item.ItemOption(50, Util.nextInt(9, 15)));
                    item.itemOptions.add(new Item.ItemOption(77, Util.nextInt(8, 15)));
                    item.itemOptions.add(new Item.ItemOption(103, Util.nextInt(8, 15)));
                    if (Util.isTrue(80, 100)) {
                        item.itemOptions.add(new Item.ItemOption(93, Util.nextInt(1, 15)));
                    }
                    InventoryServiceNew.gI().addItemBag(player, item);

                    InventoryServiceNew.gI().subQuantityItemsBag(player, keo, 99);
                    InventoryServiceNew.gI().subQuantityItemsBag(player, luoiKiem, 1);
                    InventoryServiceNew.gI().subQuantityItemsBag(player, chuoiKiem, 1);

                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
    }

    private void doiChuoiKiem(Player player) {
        if (player.combineNew.itemsCombine.size() == 1) {
            Item manhNhua = player.combineNew.itemsCombine.get(0);
            if (manhNhua.template.id == 2014 && manhNhua.quantity >= 99) {
                if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                    sendEffectSuccessCombine(player);
                    Item item = ItemService.gI().createNewItem((short) 2016);
                    InventoryServiceNew.gI().addItemBag(player, item);

                    InventoryServiceNew.gI().subQuantityItemsBag(player, manhNhua, 99);

                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
    }

    private void doiLuoiKiem(Player player) {
        if (player.combineNew.itemsCombine.size() == 1) {
            Item manhSat = player.combineNew.itemsCombine.get(0);
            if (manhSat.template.id == 2013 && manhSat.quantity >= 99) {
                if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
                    sendEffectSuccessCombine(player);
                    Item item = ItemService.gI().createNewItem((short) 2017);
                    InventoryServiceNew.gI().addItemBag(player, item);
                    InventoryServiceNew.gI().subQuantityItemsBag(player, manhSat, 99);

                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
    }

    private void doiManhKichHoat(Player player) {
        if (player.combineNew.itemsCombine.size() == 2 || player.combineNew.itemsCombine.size() == 3) {
            Item nr1s = null, doThan = null, buaBaoVe = null;
            for (Item it : player.combineNew.itemsCombine) {
                if (it.template.id == 14) {
                    nr1s = it;
                } else if (it.template.id == 2010) {
                    buaBaoVe = it;
                } else if (it.template.id >= 555 && it.template.id <= 567) {
                    doThan = it;
                }
            }

            if (nr1s != null && doThan != null) {
                if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0
                        && player.inventory.gold >= COST_DOI_MANH_KICH_HOAT) {
                    player.inventory.gold -= COST_DOI_MANH_KICH_HOAT;
                    int tiLe = buaBaoVe != null ? 100 : 50;
                    if (Util.isTrue(tiLe, 100)) {
                        sendEffectSuccessCombine(player);
                        Item item = ItemService.gI().createNewItem((short) 2009);
                        item.itemOptions.add(new Item.ItemOption(30, 0));
                        InventoryServiceNew.gI().addItemBag(player, item);
                    } else {
                        sendEffectFailCombine(player);
                    }
                    InventoryServiceNew.gI().subQuantityItemsBag(player, nr1s, 1);
                    InventoryServiceNew.gI().subQuantityItemsBag(player, doThan, 1);
                    if (buaBaoVe != null) {
                        InventoryServiceNew.gI().subQuantityItemsBag(player, buaBaoVe, 1);
                    }
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            } else {
                this.baHatMit.createOtherMenu(player, ConstNpc.IGNORE_MENU, "Hãy chọn 1 trang bị thần linh và 1 viên ngọc rồng 1 sao", "Đóng");
            }
        }
    }

    private void phanradothanlinh(Player player) {
        if (player.combineNew.itemsCombine.size() == 1) {
            player.inventory.gold -= 20000000;
            List<Integer> itemdov2 = new ArrayList<>(Arrays.asList(562, 564, 566));
            Item item = player.combineNew.itemsCombine.get(0);
            int couponAdd = itemdov2.stream().anyMatch(t -> t == item.template.id) ? 2 : item.template.id == 561 ? 3 : 1;
            sendEffectSuccessCombine(player);
            Item dangusac = ItemService.gI().createNewItem((short) 674);
            Item dangusac1 = ItemService.gI().createNewItem((short) 674);
            Item dangusac2 = ItemService.gI().createNewItem((short) 674);
            InventoryServiceNew.gI().addItemBag(player, dangusac);
            InventoryServiceNew.gI().sendItemBags(player);
            if (item.template.id == 561) {
                InventoryServiceNew.gI().addItemBag(player, dangusac);
                InventoryServiceNew.gI().addItemBag(player, dangusac1);
                InventoryServiceNew.gI().addItemBag(player, dangusac2);
                InventoryServiceNew.gI().sendItemBags(player);
            } else if (item.template.id == 562 || item.template.id == 564 || item.template.id == 566) {
                InventoryServiceNew.gI().addItemBag(player, dangusac);
                InventoryServiceNew.gI().addItemBag(player, dangusac1);
                InventoryServiceNew.gI().sendItemBags(player);
            }
            Service.gI().sendThongBaoOK(player, "Mày Nhận Được Đá Ngũ Sắc");
            InventoryServiceNew.gI().subQuantityItemsBag(player, item, 1);
            player.combineNew.itemsCombine.clear();
            InventoryServiceNew.gI().sendItemBags(player);
            Service.gI().sendMoney(player);
            reOpenItemCombine(player);
        }
    }

    private void doidiem(Player player) {
        if (player.combineNew.itemsCombine.size() == 1) {
            player.inventory.gold -= 0;
            List<Integer> itemdov2 = new ArrayList<>(Arrays.asList(663, 664, 665, 666, 667));
            Item item = player.combineNew.itemsCombine.get(0);
            sendEffectSuccessCombine(player);
            if (item.quantity < 99) {
                Service.gI().sendThongBaoOK(player, "Đéo Đủ Thức Ăn");
            } else if (item.quantity >= 99) {
                InventoryServiceNew.gI().sendItemBags(player);
                player.inventory.coupon += 1;
                Service.gI().sendThongBaoOK(player, "Bú 1 Điểm");
                InventoryServiceNew.gI().subQuantityItemsBag(player, item, 99);
                player.combineNew.itemsCombine.clear();
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendMoney(player);
                reOpenItemCombine(player);
            }
        }
    }

    public void openDTS(Player player) {
        //check sl đồ tl, đồ hd
        // new update 2 mon huy diet + 1 mon than linh(skh theo style) +  5 manh bat ki
        if (player.combineNew.itemsCombine.size() != 4) {
            Service.gI().sendThongBao(player, "Thiếu đồ");
            return;
        }
        if (player.inventory.gold < COST) {
            Service.gI().sendThongBao(player, "Ảo ít thôi con...");
            return;
        }
        if (InventoryServiceNew.gI().getCountEmptyBag(player) < 1) {
            Service.gI().sendThongBao(player, "Bạn phải có ít nhất 1 ô trống hành trang");
            return;
        }
        Item itemTL = player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaNangCap()).findFirst().get();
        List<Item> itemHDs = player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isCongThucVip()).collect(Collectors.toList());
        Item itemManh = player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isManhTS() && item.quantity >= 999).findFirst().get();

        player.inventory.gold -= COST;
        sendEffectSuccessCombine(player);
        short[][] itemIds = {{1048, 1051, 1054, 1057, 1060}, {1049, 1052, 1055, 1058, 1061}, {1050, 1053, 1056, 1059, 1062}}; // thứ tự td - 0,nm - 1, xd - 2

        Item itemTS = ItemService.gI().DoThienSu(itemIds[player.gender > 2 ? player.gender : player.gender][itemManh.typeIdManh()], player.gender);
        InventoryServiceNew.gI().addItemBag(player, itemTS);

        InventoryServiceNew.gI().subQuantityItemsBag(player, itemTL, 1);
        InventoryServiceNew.gI().subQuantityItemsBag(player, itemManh, 999);
        itemHDs.forEach(item -> InventoryServiceNew.gI().subQuantityItemsBag(player, item, 1));
        InventoryServiceNew.gI().sendItemBags(player);
        Service.gI().sendMoney(player);
        Service.gI().sendThongBao(player, "Bạn đã nhận được " + itemTS.template.name);
        player.combineNew.itemsCombine.clear();
        reOpenItemCombine(player);
    }

    public void cheTaoDoTS(Player player) {
        // Công thức vip + x999 Mảnh thiên sứ + đá nâng cấp + đá may mắn
        if (player.combineNew.itemsCombine.size() < 2 || player.combineNew.itemsCombine.size() > 4) {
            Service.getInstance().sendThongBao(player, "Thiếu vật phẩm, vui lòng thêm vào");
            return;
        }
        if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isCongThucVip()).count() != 1) {
            Service.getInstance().sendThongBao(player, "Thiếu Công thức Vip");
            return;
        }
        if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isManhTS() && item.quantity >= 999).count() != 1) {
            Service.getInstance().sendThongBao(player, "Thiếu Mảnh thiên sứ");
            return;
        }
        if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaNangCap()).count() != 1 || player.combineNew.itemsCombine.size() == 4 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaNangCap()).count() != 1) {
            Service.getInstance().sendThongBao(player, "Thiếu Đá nâng cấp");
            return;
        }
        if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaMayMan()).count() != 1 || player.combineNew.itemsCombine.size() == 4 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaMayMan()).count() != 1) {
            Service.getInstance().sendThongBao(player, "Thiếu Đá may mắn");
            return;
        }
        Item mTS = null, daNC = null, daMM = null, CtVip = null;
        for (Item item : player.combineNew.itemsCombine) {
            if (item.isNotNullItem()) {
                if (item.isManhTS()) {
                    mTS = item;
                } else if (item.isDaNangCap()) {
                    daNC = item;
                } else if (item.isDaMayMan()) {
                    daMM = item;
                } else if (item.isCongThucVip()) {
                    CtVip = item;
                }
            }
        }
        if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {//check chỗ trống hành trang
            if (player.inventory.gold < 500000000) {
                Service.getInstance().sendThongBao(player, "Không đủ vàng để thực hiện");
                return;
            }
            player.inventory.gold -= 500000000;

            int tilemacdinh = 35;
            int tileLucky = 20;
            if (daNC != null) {
                tilemacdinh += (daNC.template.id - 1073) * 10;
            } else {
                tilemacdinh = tilemacdinh;
            }
            if (daMM != null) {
                tileLucky += tileLucky * (daMM.template.id - 1078) * 10 / 100;
            } else {
                tileLucky = tileLucky;
            }

            if (Util.nextInt(0, 100) < tilemacdinh) {
                Item itemCtVip = player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isCongThucVip()).findFirst().get();
                if (daNC != null) {
                    Item itemDaNangC = player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaNangCap()).findFirst().get();
                }
                if (daMM != null) {
                    Item itemDaMayM = player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDaMayMan()).findFirst().get();
                }
                Item itemManh = player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isManhTS() && item.quantity >= 999).findFirst().get();

                tilemacdinh = Util.nextInt(0, 50);
                if (tilemacdinh == 49) {
                    tilemacdinh = 20;
                } else if (tilemacdinh == 48 || tilemacdinh == 47) {
                    tilemacdinh = 19;
                } else if (tilemacdinh == 46 || tilemacdinh == 45) {
                    tilemacdinh = 18;
                } else if (tilemacdinh == 44 || tilemacdinh == 43) {
                    tilemacdinh = 17;
                } else if (tilemacdinh == 42 || tilemacdinh == 41) {
                    tilemacdinh = 16;
                } else if (tilemacdinh == 40 || tilemacdinh == 39) {
                    tilemacdinh = 15;
                } else if (tilemacdinh == 38 || tilemacdinh == 37) {
                    tilemacdinh = 14;
                } else if (tilemacdinh == 36 || tilemacdinh == 35) {
                    tilemacdinh = 13;
                } else if (tilemacdinh == 34 || tilemacdinh == 33) {
                    tilemacdinh = 12;
                } else if (tilemacdinh == 32 || tilemacdinh == 31) {
                    tilemacdinh = 11;
                } else if (tilemacdinh == 30 || tilemacdinh == 29) {
                    tilemacdinh = 10;
                } else if (tilemacdinh <= 28 || tilemacdinh >= 26) {
                    tilemacdinh = 9;
                } else if (tilemacdinh <= 25 || tilemacdinh >= 23) {
                    tilemacdinh = 8;
                } else if (tilemacdinh <= 22 || tilemacdinh >= 20) {
                    tilemacdinh = 7;
                } else if (tilemacdinh <= 19 || tilemacdinh >= 17) {
                    tilemacdinh = 6;
                } else if (tilemacdinh <= 16 || tilemacdinh >= 14) {
                    tilemacdinh = 5;
                } else if (tilemacdinh <= 13 || tilemacdinh >= 11) {
                    tilemacdinh = 4;
                } else if (tilemacdinh <= 10 || tilemacdinh >= 8) {
                    tilemacdinh = 3;
                } else if (tilemacdinh <= 7 || tilemacdinh >= 5) {
                    tilemacdinh = 2;
                } else if (tilemacdinh <= 4 || tilemacdinh >= 2) {
                    tilemacdinh = 1;
                } else if (tilemacdinh <= 1) {
                    tilemacdinh = 0;
                }
                short[][] itemIds = {{1048, 1051, 1054, 1057, 1060}, {1049, 1052, 1055, 1058, 1061}, {1050, 1053, 1056, 1059, 1062}}; // thứ tự td - 0,nm - 1, xd - 2

                Item itemTS = ItemService.gI().DoThienSu(itemIds[itemCtVip.template.gender > 2 ? player.gender : itemCtVip.template.gender][itemManh.typeIdManh()], itemCtVip.template.gender);

                tilemacdinh += 10;

                if (tilemacdinh > 0) {
                    for (byte i = 0; i < itemTS.itemOptions.size(); i++) {
                        if (itemTS.itemOptions.get(i).optionTemplate.id != 21 && itemTS.itemOptions.get(i).optionTemplate.id != 30) {
                            itemTS.itemOptions.get(i).param += (itemTS.itemOptions.get(i).param * tilemacdinh / 100);
                        }
                    }
                }
                tilemacdinh = Util.nextInt(0, 100);

                if (tilemacdinh <= tileLucky) {
                    if (tilemacdinh >= (tileLucky - 3)) {
                        tileLucky = 3;
                    } else if (tilemacdinh <= (tileLucky - 4) && tilemacdinh >= (tileLucky - 10)) {
                        tileLucky = 2;
                    } else {
                        tileLucky = 1;
                    }
                    itemTS.itemOptions.add(new Item.ItemOption(15, tileLucky));
                    ArrayList<Integer> listOptionBonus = new ArrayList<>();
                    listOptionBonus.add(50);
                    listOptionBonus.add(77);
                    listOptionBonus.add(103);
                    listOptionBonus.add(98);
                    listOptionBonus.add(99);
                    for (int i = 0; i < tileLucky; i++) {
                        tilemacdinh = Util.nextInt(0, listOptionBonus.size());
                        itemTS.itemOptions.add(new ItemOption(listOptionBonus.get(tilemacdinh), Util.nextInt(1, 5)));
                        listOptionBonus.remove(tilemacdinh);
                    }
                }

                InventoryServiceNew.gI().addItemBag(player, itemTS);
                sendEffectSuccessCombine(player);
            } else {
                sendEffectFailCombine(player);
            }
            if (mTS != null && daMM != null && daNC != null && CtVip != null) {
                InventoryServiceNew.gI().subQuantityItemsBag(player, CtVip, 1);
                InventoryServiceNew.gI().subQuantityItemsBag(player, daNC, 1);
                InventoryServiceNew.gI().subQuantityItemsBag(player, mTS, 999);
                InventoryServiceNew.gI().subQuantityItemsBag(player, daMM, 1);
            } else if (CtVip != null && mTS != null) {
                InventoryServiceNew.gI().subQuantityItemsBag(player, CtVip, 1);
                InventoryServiceNew.gI().subQuantityItemsBag(player, mTS, 999);
            } else if (CtVip != null && mTS != null && daNC != null) {
                InventoryServiceNew.gI().subQuantityItemsBag(player, CtVip, 1);
                InventoryServiceNew.gI().subQuantityItemsBag(player, mTS, 999);
                InventoryServiceNew.gI().subQuantityItemsBag(player, daNC, 1);
            } else if (CtVip != null && mTS != null && daMM != null) {
                InventoryServiceNew.gI().subQuantityItemsBag(player, CtVip, 1);
                InventoryServiceNew.gI().subQuantityItemsBag(player, mTS, 999);
                InventoryServiceNew.gI().subQuantityItemsBag(player, daMM, 1);
            }

            InventoryServiceNew.gI().sendItemBags(player);
            Service.getInstance().sendMoney(player);
            reOpenItemCombine(player);
        } else {
            Service.getInstance().sendThongBao(player, "Bạn phải có ít nhất 1 ô trống hành trang");
        }
    }

    public void openSKHVIP(Player player) {
        // 1 thiên sứ + 2 món kích hoạt -- món đầu kh làm gốc
        if (player.combineNew.itemsCombine.size() != 3) {
            Service.gI().sendThongBao(player, "Thiếu nguyên liệu");
            return;
        }
        if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isDTS()).count() != 1) {
            Service.gI().sendThongBao(player, "Thiếu đồ thiên sứ");
            return;
        }
        if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isSKH()).count() != 2) {
            Service.gI().sendThongBao(player, "Thiếu đồ kích hoạt");
            return;
        }
        if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
            if (player.inventory.gold < 1) {
                Service.gI().sendThongBao(player, "Con cần thêm vàng để đổi...");
                return;
            }
            player.inventory.gold -= COST;
            Item itemTS = player.combineNew.itemsCombine.stream().filter(Item::isDTS).findFirst().get();
            List<Item> itemSKH = player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.isSKH()).collect(Collectors.toList());
            CombineServiceNew.gI().sendEffectOpenItem(player, itemTS.template.iconID, itemTS.template.iconID);
            short itemId;
            if (itemTS.template.gender == 3 || itemTS.template.type == 4) {
                itemId = Manager.radaSKHVip[Util.nextInt(0, 5)];
                if (player.getSession().bdPlayer > 0 && Util.isTrue(1, (int) (100 / player.getSession().bdPlayer))) {
                    itemId = Manager.radaSKHVip[6];
                }
            } else {
                itemId = Manager.doSKHVip[itemTS.template.gender][itemTS.template.type][Util.nextInt(0, 5)];
                if (player.getSession().bdPlayer > 0 && Util.isTrue(1, (int) (100 / player.getSession().bdPlayer))) {
                    itemId = Manager.doSKHVip[itemTS.template.gender][itemTS.template.type][6];
                }
            }
            int skhId = ItemService.gI().randomSKHId(itemTS.template.gender);
            Item item;
            if (new Item(itemId).isDTL()) {
                item = Util.ratiItemTL(itemId);
                item.itemOptions.add(new Item.ItemOption(skhId, 2));
                int optionId = ItemService.gI().optionIdSKH(skhId);
                item.itemOptions.add(new Item.ItemOption(optionId, ItemService.gI().skhOptionParam(optionId)));
                item.itemOptions.remove(item.itemOptions.stream().filter(itemOption -> itemOption.optionTemplate.id == 21).findFirst().get());
                item.itemOptions.add(new Item.ItemOption(21, 15));
                item.itemOptions.add(new Item.ItemOption(30, 1));
            } else {
                item = ItemService.gI().itemSKH(itemId, skhId);
            }
            InventoryServiceNew.gI().addItemBag(player, item);
            InventoryServiceNew.gI().subQuantityItemsBag(player, itemTS, 1);
            itemSKH.forEach(i -> InventoryServiceNew.gI().subQuantityItemsBag(player, i, 1));
            InventoryServiceNew.gI().sendItemBags(player);
            Service.gI().sendMoney(player);
            player.combineNew.itemsCombine.clear();
            reOpenItemCombine(player);
        } else {
            Service.gI().sendThongBao(player, "Bạn phải có ít nhất 1 ô trống hành trang");
        }
    }

    private void dapDoKichHoat(Player player) {
        if (player.combineNew.itemsCombine.size() == 1 || player.combineNew.itemsCombine.size() == 2) {
            Item dhd = null, dtl = null;
            for (Item item : player.combineNew.itemsCombine) {
                if (item.isNotNullItem()) {
                    if (item.template.id >= 650 && item.template.id <= 662) {
                        dhd = item;
                    } else if (item.template.id >= 555 && item.template.id <= 567) {
                        dtl = item;
                    }
                }
            }
            if (dhd != null) {
                if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0 //check chỗ trống hành trang
                        && player.inventory.gold >= COST_DAP_DO_KICH_HOAT) {
                    player.inventory.gold -= COST_DAP_DO_KICH_HOAT;
                    int tiLe = dtl != null ? 100 : 50;
                    if (Util.isTrue(tiLe, 100)) {
                        sendEffectSuccessCombine(player);
                        Item item = ItemService.gI().createNewItem((short) getTempIdItemC0(dhd.template.gender, dhd.template.type));
                        RewardService.gI().initBaseOptionClothes(item.template.id, item.template.type, item.itemOptions);
                        RewardService.gI().initActivationOption(item.template.gender < 3 ? item.template.gender : player.gender, item.template.type, item.itemOptions);
                        InventoryServiceNew.gI().addItemBag(player, item);
                    } else {
                        sendEffectFailCombine(player);
                    }
                    InventoryServiceNew.gI().subQuantityItemsBag(player, dhd, 1);
                    if (dtl != null) {
                        InventoryServiceNew.gI().subQuantityItemsBag(player, dtl, 1);
                    }
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
    }

    private void doiVeHuyDiet(Player player) {
        if (player.combineNew.itemsCombine.size() == 1) {
            Item item = player.combineNew.itemsCombine.get(0);
            if (item.isNotNullItem() && item.template.id >= 555 && item.template.id <= 567) {
                if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0
                        && player.inventory.gold >= COST_DOI_VE_DOI_DO_HUY_DIET) {
                    player.inventory.gold -= COST_DOI_VE_DOI_DO_HUY_DIET;
                    Item ticket = ItemService.gI().createNewItem((short) (2001 + item.template.type));
                    ticket.itemOptions.add(new Item.ItemOption(30, 0));
                    InventoryServiceNew.gI().subQuantityItemsBag(player, item, 1);
                    InventoryServiceNew.gI().addItemBag(player, ticket);
                    sendEffectOpenItem(player, item.template.iconID, ticket.template.iconID);

                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
    }

    private void nangCapChanMenh(Player player) {
        if (player.combineNew.itemsCombine.size() == 2) {
            int diem = player.combineNew.DiemNangcap;
            if (player.NguHanhSonPoint < diem) {
                Service.gI().sendThongBao(player, "Không đủ Điểm Ngũ Hành Sơn để thực hiện");
                return;
            }
            Item chanmenh = null;
            Item dahoangkim = null;
            int capbac = 0;
            for (Item item : player.combineNew.itemsCombine) {
                if (item.template.id == 1241) {
                    dahoangkim = item;
                } else if (item.template.id >= 1232 && item.template.id < 1240) {
                    chanmenh = item;
                    capbac = item.template.id - 1231;
                }
            }
            int soluongda = player.combineNew.DaNangcap;
            if (dahoangkim != null && dahoangkim.quantity >= soluongda) {
                if (chanmenh != null && (chanmenh.template.id >= 1232 && chanmenh.template.id < 1240)) {
                    player.NguHanhSonPoint -= diem;
                    if (Util.isTrue(player.combineNew.TileNangcap, 100)) {
                        InventoryServiceNew.gI().subQuantityItemsBag(player, dahoangkim, soluongda);
                        sendEffectSuccessCombine(player);
                        switch (capbac) {
                            case 1:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 2));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 2));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 2));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 2));
                                break;
                            case 2:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 3));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 3));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 3));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 3:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 4));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 4));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 4));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 4:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 5));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 5));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 5));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 5:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 6));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 6));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 6));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 6:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 7));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 7));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 7));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 7:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 8:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                        }
                    } else {
                        InventoryServiceNew.gI().subQuantityItemsBag(player, dahoangkim, soluongda);
                        sendEffectFailCombine(player);
                    }
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            } else {
                Service.gI().sendThongBao(player, "Không đủ Đá Chân Mệnh để thực hiện");
            }
        } else if (player.combineNew.itemsCombine.size() == 3) {
            int diem = player.combineNew.DiemNangcap - 5;
            if (player.NguHanhSonPoint < diem) {
                Service.gI().sendThongBao(player, "Không đủ Điểm Ngũ Hành Sơn để thực hiện");
                return;
            }
            Item chanmenh = null;
            Item dahoangkim = null;
            Item tv = null;
            int capbac = 0;
            for (Item item : player.combineNew.itemsCombine) {
                if (item.template.id == 1241) {
                    dahoangkim = item;
                } else if (item.template.id == 457) {
                    tv = item;
                } else if (item.template.id >= 1232 && item.template.id < 1240) {
                    chanmenh = item;
                    capbac = item.template.id - 1231;
                }
            }
            int soluongda = player.combineNew.DaNangcap - 5;
            if (dahoangkim != null && dahoangkim.quantity >= soluongda && tv != null && tv.quantity >= 1) {
                if (chanmenh != null && (chanmenh.template.id >= 1232 && chanmenh.template.id < 1240)) {
                    player.NguHanhSonPoint -= diem;
                    if (Util.isTrue(player.combineNew.TileNangcap, 100)) {
                        InventoryServiceNew.gI().subQuantityItemsBag(player, dahoangkim, soluongda);
                        InventoryServiceNew.gI().subQuantityItemsBag(player, tv, 1);
                        sendEffectSuccessCombine(player);
                        switch (capbac) {
                            case 1:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 2));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 2));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 2));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 2));
                                break;
                            case 2:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 3));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 3));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 3));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 3:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 4));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 4));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 4));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 4:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 5));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 5));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 5));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 5:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 6));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 6));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 6));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 6:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 7));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 7));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 7));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 7:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                            case 8:
                                chanmenh.template = ItemService.gI().getTemplate(chanmenh.template.id + 1);
                                chanmenh.itemOptions.clear();
                                chanmenh.itemOptions.add(new Item.ItemOption(50, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(77, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(103, 8));
                                chanmenh.itemOptions.add(new Item.ItemOption(30, 1));
                                break;
                        }
                    } else {
                        InventoryServiceNew.gI().subQuantityItemsBag(player, dahoangkim, soluongda);
                        InventoryServiceNew.gI().subQuantityItemsBag(player, tv, 1);
                        sendEffectFailCombine(player);
                    }
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            } else {
                Service.gI().sendThongBao(player, "Không đủ Đá Chân Mệnh để thực hiện");
            }
        }
    }

    private void nangCapBongTai(Player player) {
        if (player.combineNew.itemsCombine.size() == 2) {
            int gold = player.combineNew.goldCombine;
            if (player.inventory.gold < gold) {
                Service.getInstance().sendThongBao(player, "Không đủ vàng để thực hiện");
                return;
            }
            int gem = player.combineNew.gemCombine;
            if (player.inventory.gem < gem) {
                Service.getInstance().sendThongBao(player, "Không đủ ngọc để thực hiện");
                return;
            }
            Item bongtai = null;
            Item manhvobt = null;
            for (Item item : player.combineNew.itemsCombine) {
                if (checkbongtai(item)) {// Kiểm tra có bt c1 hoặc c2 không , id 451 và 921
                    bongtai = item;
                } else if (item.template.id == 933) {// check có mảnh vỡ bông tai và gán vào
                    manhvobt = item;
                }
            }
            if (bongtai != null && manhvobt != null) {
                int level = 0;
                for (ItemOption io : bongtai.itemOptions) {
                    if (io.optionTemplate.id == 72) {
                        level = io.param;
                        break;
                    }
                }
                if (level < 4) {
                    int lvbt = lvbt(bongtai);
                    int countmvbt = getcountmvbtnangbt(lvbt);
                    if (countmvbt > manhvobt.quantity) {
                        Service.getInstance().sendThongBao(player, "Không đủ Mảnh vỡ bông tai");
                        return;
                    }
                    player.inventory.gold -= gold;
                    player.inventory.gem -= gem;
                    InventoryServiceNew.gI().subQuantityItemsBag(player, manhvobt, countmvbt);
                    if (Util.isTrue(player.combineNew.ratioCombine, 100)) {
                        bongtai.template = ItemService.gI().getTemplate(getidbtsaukhilencap(lvbt));
                        bongtai.itemOptions.clear();
                        bongtai.itemOptions.add(new ItemOption(72, lvbt + 1));
                        sendEffectSuccessCombine(player);
                    } else {
                        sendEffectFailCombine(player);
                    }
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.getInstance().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
    }

    private short getidbtsaukhilencap(int lvbtcu) {
        switch (lvbtcu) {
            case 1:
                return 921;
            case 2:
                return 1228;
            case 3:
                return 1229;

        }
        return 0;
    }

    private void moChiSoBongTai(Player player) {
        if (player.combineNew.itemsCombine.size() == 3) {
            int gold = player.combineNew.goldCombine;
            if (player.inventory.gold < gold) {
                Service.getInstance().sendThongBao(player, "Không đủ vàng để thực hiện");
                return;
            }
            int gem = player.combineNew.gemCombine;
            if (player.inventory.gem < gem) {
                Service.getInstance().sendThongBao(player, "Không đủ ngọc để thực hiện");
                return;
            }
            Item bongTai = null;
            Item manhHon = null;
            Item daXanhLam = null;
            for (Item item : player.combineNew.itemsCombine) {
                if (item.template.id == 921 || item.template.id == 1228 || item.template.id == 1229) {
                    bongTai = item;
                } else if (item.template.id == 934) {
                    manhHon = item;
                } else if (item.template.id == 935) {
                    daXanhLam = item;
                }
            }
            if (bongTai != null && daXanhLam != null && manhHon.quantity >= 99 && daXanhLam.quantity >= 1) {
                player.inventory.gold -= gold;
                player.inventory.gem -= gem;
                InventoryServiceNew.gI().subQuantityItemsBag(player, manhHon, 99);
                InventoryServiceNew.gI().subQuantityItemsBag(player, daXanhLam, 1);
                if (Util.isTrue(player.combineNew.ratioCombine, 100)) {
                    bongTai.itemOptions.clear();
                    if (bongTai.template.id == 921) {
                        bongTai.itemOptions.add(new ItemOption(72, 2));
                        int rdUp = Util.nextInt(0, 8);
                        switch (rdUp) {
                            case 0:
                                bongTai.itemOptions.add(new ItemOption(50, 5));
                                break;
                            case 1:
                                bongTai.itemOptions.add(new ItemOption(77, 5));
                                break;
                            case 2:
                                bongTai.itemOptions.add(new ItemOption(103, 5));
                                break;
                            case 3:
                                bongTai.itemOptions.add(new ItemOption(108, 5));
                                break;
                            case 4:
                                bongTai.itemOptions.add(new ItemOption(94, 5));
                                break;
                            case 5:
                                bongTai.itemOptions.add(new ItemOption(14, 5));
                                break;
                            case 6:
                                bongTai.itemOptions.add(new ItemOption(80, 5));
                                break;
                            case 7:
                                bongTai.itemOptions.add(new ItemOption(81, 5));
                                break;
                            case 8:
                                bongTai.itemOptions.add(new ItemOption(101, 5));
                                break;
                        }
                    } else if (bongTai.template.id == 1228) {
                        bongTai.itemOptions.add(new ItemOption(72, 3));
                        int rdUp1 = Util.nextInt(0, 8);
                        switch (rdUp1) {
                            case 0:
                                bongTai.itemOptions.add(new ItemOption(50, Util.nextInt(5, 10)));
                                break;
                            case 1:
                                bongTai.itemOptions.add(new ItemOption(77, Util.nextInt(5, 10)));
                                break;
                            case 2:
                                bongTai.itemOptions.add(new ItemOption(103, Util.nextInt(5, 10)));
                                break;
                            case 3:
                                bongTai.itemOptions.add(new ItemOption(108, Util.nextInt(5, 10)));
                                break;
                            case 4:
                                bongTai.itemOptions.add(new ItemOption(94, Util.nextInt(5, 10)));
                                break;
                            case 5:
                                bongTai.itemOptions.add(new ItemOption(14, Util.nextInt(5, 10)));
                                break;
                            case 6:
                                bongTai.itemOptions.add(new ItemOption(80, Util.nextInt(5, 10)));
                                break;
                            case 7:
                                bongTai.itemOptions.add(new ItemOption(81, Util.nextInt(5, 10)));
                                break;
                            case 8:
                                bongTai.itemOptions.add(new ItemOption(101, Util.nextInt(5, 10)));
                                ;
                                break;
                        }
                    } else if (bongTai.template.id == 1229) {
                        bongTai.itemOptions.add(new ItemOption(72, 3));
                        int rdUp1 = Util.nextInt(0, 8);
                        switch (rdUp1) {
                            case 0:
                                bongTai.itemOptions.add(new ItemOption(50, Util.nextInt(5, 15)));
                                break;
                            case 1:
                                bongTai.itemOptions.add(new ItemOption(77, Util.nextInt(5, 15)));
                                break;
                            case 2:
                                bongTai.itemOptions.add(new ItemOption(103, Util.nextInt(5, 15)));
                                break;
                            case 3:
                                bongTai.itemOptions.add(new ItemOption(108, Util.nextInt(5, 15)));
                                break;
                            case 4:
                                bongTai.itemOptions.add(new ItemOption(94, Util.nextInt(5, 15)));
                                break;
                            case 5:
                                bongTai.itemOptions.add(new ItemOption(14, Util.nextInt(5, 15)));
                                break;
                            case 6:
                                bongTai.itemOptions.add(new ItemOption(80, Util.nextInt(5, 15)));
                                break;
                            case 7:
                                bongTai.itemOptions.add(new ItemOption(81, Util.nextInt(5, 15)));
                                break;
                            case 8:
                                bongTai.itemOptions.add(new ItemOption(101, Util.nextInt(5, 15)));
                                ;
                                break;
                        }
                    }
                    sendEffectSuccessCombine(player);
                } else {
                    sendEffectFailCombine(player);
                }
                InventoryServiceNew.gI().sendItemBags(player);
                Service.getInstance().sendMoney(player);
                reOpenItemCombine(player);
            }
        }
    }

    private void moChiSolinhthu(Player player) {
        if (player.combineNew.itemsCombine.size() == 3) {
            int gold = player.combineNew.goldCombine;
            if (player.inventory.gold < gold) {
                Service.gI().sendThongBao(player, "Không đủ vàng để thực hiện");
                return;
            }
            int gem = player.combineNew.gemCombine;
            if (player.inventory.gem < gem) {
                Service.gI().sendThongBao(player, "Không đủ ngọc để thực hiện");
                return;
            }
            Item linhthu = null;
            Item damathuat = null;
            Item thucan = null;
            for (Item item : player.combineNew.itemsCombine) {
                if (item.template.id >= 2019 && item.template.id <= 2026) {
                    linhthu = item;
                } else if (item.template.id == 2030) {
                    damathuat = item;
                } else if (item.template.id >= 663 && item.template.id <= 667) {
                    thucan = item;
                }
            }
            if (linhthu != null && damathuat != null && damathuat.quantity >= 99) {
                player.inventory.gold -= gold;
                player.inventory.gem -= gem;
                InventoryServiceNew.gI().subQuantityItemsBag(player, damathuat, 99);
                InventoryServiceNew.gI().subQuantityItemsBag(player, thucan, 1);
                if (Util.isTrue(player.combineNew.ratioCombine, 100)) {
                    linhthu.itemOptions.clear();
                    linhthu.itemOptions.add(new Item.ItemOption(72, 2));
                    int rdUp = Util.nextInt(0, 7);
                    if (rdUp == 0) {
                        linhthu.itemOptions.add(new Item.ItemOption(50, Util.nextInt(5, 25)));
                    } else if (rdUp == 1) {
                        linhthu.itemOptions.add(new Item.ItemOption(77, Util.nextInt(5, 25)));
                    } else if (rdUp == 2) {
                        linhthu.itemOptions.add(new Item.ItemOption(103, Util.nextInt(5, 25)));
                    } else if (rdUp == 3) {
                        linhthu.itemOptions.add(new Item.ItemOption(108, Util.nextInt(5, 25)));
                    } else if (rdUp == 4) {
                        linhthu.itemOptions.add(new Item.ItemOption(94, Util.nextInt(5, 15)));
                    } else if (rdUp == 5) {
                        linhthu.itemOptions.add(new Item.ItemOption(14, Util.nextInt(5, 15)));
                    } else if (rdUp == 6) {
                        linhthu.itemOptions.add(new Item.ItemOption(80, Util.nextInt(5, 25)));
                    } else if (rdUp == 7) {
                        linhthu.itemOptions.add(new Item.ItemOption(81, Util.nextInt(5, 25)));
                    }
                    sendEffectSuccessCombine(player);
                } else {
                    sendEffectFailCombine(player);
                }
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendMoney(player);
                reOpenItemCombine(player);
            }
        }
    }

    private void epSaoTrangBi(Player player) {
        if (player.combineNew.itemsCombine.size() == 2) {
            int gem = player.combineNew.gemCombine;
            if (player.inventory.gem < gem) {
                Service.gI().sendThongBao(player, "Không đủ ngọc để thực hiện");
                return;
            }
            Item trangBi = null;
            Item daPhaLe = null;
            for (Item item : player.combineNew.itemsCombine) {
                if (isTrangBiPhaLeHoa(item)) {
                    trangBi = item;
                } else if (isDaPhaLe(item)) {
                    daPhaLe = item;
                }
            }
            int star = 0; //sao pha lê đã ép
            int starEmpty = 0; //lỗ sao pha lê
            if (trangBi != null && daPhaLe != null) {
                Item.ItemOption optionStar = null;
                for (Item.ItemOption io : trangBi.itemOptions) {
                    if (io.optionTemplate.id == 102) {
                        star = io.param;
                        optionStar = io;
                    } else if (io.optionTemplate.id == 107) {
                        starEmpty = io.param;
                    }
                }
                if (star < starEmpty) {
                    player.inventory.gem -= gem;
                    int optionId = getOptionDaPhaLe(daPhaLe);
                    int param = getParamDaPhaLe(daPhaLe);
                    Item.ItemOption option = null;
                    for (Item.ItemOption io : trangBi.itemOptions) {
                        if (io.optionTemplate.id == optionId) {
                            option = io;
                            break;
                        }
                    }
                    if (option != null) {
                        option.param += param;
                    } else {
                        trangBi.itemOptions.add(new Item.ItemOption(optionId, param));
                    }
                    if (optionStar != null) {
                        optionStar.param++;
                    } else {
                        trangBi.itemOptions.add(new Item.ItemOption(102, 1));
                    }

                    InventoryServiceNew.gI().subQuantityItemsBag(player, daPhaLe, 1);
                    sendEffectSuccessCombine(player);
                }
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendMoney(player);
                reOpenItemCombine(player);
            }
        }
    }

    private void phaLeHoaTrangBi(Player player, int type) {
        if (type == 0) {
            if (!player.combineNew.itemsCombine.isEmpty()) {
                long gold = player.combineNew.goldCombine;
                int gem = player.combineNew.gemCombine;
                if (player.inventory.gold < gold) {
                    Service.gI().sendThongBao(player, "Không đủ vàng để thực hiện");
                    return;
                } else if (player.inventory.gem < gem) {
                    Service.gI().sendThongBao(player, "Không đủ ngọc để thực hiện");
                    return;
                }
                Item item = player.combineNew.itemsCombine.get(0);
                if (isTrangBiPhaLeHoa(item)) {
                    int star = 0;
                    Item.ItemOption optionStar = null;
                    for (Item.ItemOption io : item.itemOptions) {
                        if (io.optionTemplate.id == 107) {
                            star = io.param;
                            optionStar = io;
                            break;
                        }
                    }
                    if (star < MAX_STAR_ITEM) {
                        player.inventory.gold -= gold;
                        player.inventory.gem -= gem;
                        byte ratio = (optionStar != null && optionStar.param > 6) ? (byte) 3 : 1;
                        if (Util.isTrue(player.combineNew.ratioCombine, 100 * ratio)) {
                            if (optionStar == null) {
                                item.itemOptions.add(new Item.ItemOption(107, 1));
                            } else {
                                optionStar.param++;
                            }
                            sendEffectSuccessCombine(player);
//                            if (optionStar != null && optionStar.param >= 7) {
//                                ServerNotify.gI().notify("Chúc mừng " + player.name + " vừa pha lê hóa "
//                                        + "thành công " + item.template.name + " lên " + optionStar.param + " sao pha lê");
//                            }
                        } else {
                            sendEffectFailCombine(player);
                        }
                    }
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
        if (type == 1) {
            if (player.combineNew.itemsCombine.isEmpty()) {
                return;
            }
            long gold = player.combineNew.goldCombine;
            int gem = player.combineNew.gemCombine;
            if (player.inventory.gold < gold) {
                Service.gI().sendThongBao(player, "Không đủ vàng để thực hiện");
                return;
            } else if (player.inventory.gem < gem) {
                Service.gI().sendThongBao(player, "Không đủ ngọc để thực hiện");
                return;
            }
            Item item = player.combineNew.itemsCombine.get(0);
            if (!isTrangBiPhaLeHoa(item)) {
                return;
            }
            int star = 0;
            Item.ItemOption optionStar = null;
            for (Item.ItemOption io : item.itemOptions) {
                if (io.optionTemplate.id == 107) {
                    star = io.param;
                    optionStar = io;
                    break;
                }
            }
            boolean flag = false;
            for (int i = 0; i < 100; i++) {
                gold = player.combineNew.goldCombine;
                gem = player.combineNew.gemCombine;
                if (player.inventory.gold < gold) {
                    Service.gI().sendThongBao(player, "Không đủ vàng để thực hiện");
                    break;
                } else if (player.inventory.gem < gem) {
                    Service.gI().sendThongBao(player, "Không đủ ngọc để thực hiện");
                    break;
                }
                if (star < MAX_STAR_ITEM) {
                    player.inventory.gold -= gold;
                    player.inventory.gem -= gem;
                    byte ratio = (optionStar != null && optionStar.param > 6) ? (byte) 3 : 1;
                    if (Util.isTrue(player.combineNew.ratioCombine, 100 * ratio)) {
                        if (optionStar == null) {
                            item.itemOptions.add(new Item.ItemOption(107, 1));
                        } else {
                            optionStar.param++;
                        }
                        sendEffectSuccessCombine(player);
                        flag = true;
                        if (optionStar != null && optionStar.param >= 7) {
//                            ServerNotify.gI().notify("Chúc mừng " + player.name + " vừa pha lê hóa "
//                                    + "thành công " + item.template.name + " lên " + optionStar.param + " sao pha lê");
                        }
                        Service.gI().sendThongBao(player, "Nâng cấp thành công sau " + (i + 1) + " lần");
                        break;
                    }
                }
            }
            if (!flag) {
                sendEffectFailCombine(player);
            }
            InventoryServiceNew.gI().sendItemBags(player);
            Service.gI().sendMoney(player);
            reOpenItemCombine(player);
        }
    }

    private void nhapNgocRong(Player player) {
        if (InventoryServiceNew.gI().getCountEmptyBag(player) > 0) {
            if (!player.combineNew.itemsCombine.isEmpty()) {
                Item item = player.combineNew.itemsCombine.get(0);
                if (item != null && item.isNotNullItem() && (item.template.id > 14 && item.template.id <= 20) && item.quantity >= 7) {
                    Item nr = ItemService.gI().createNewItem((short) (item.template.id - 1));
                    InventoryServiceNew.gI().addItemBag(player, nr);
                    InventoryServiceNew.gI().subQuantityItemsBag(player, item, 7);
                    InventoryServiceNew.gI().sendItemBags(player);
                    reOpenItemCombine(player);
//                    sendEffectCombineDB(player, item.template.iconID);
                }
            }
        }
    }

    private void nangCapVatPham(Player player) {
        if (player.combineNew.itemsCombine.size() >= 2 && player.combineNew.itemsCombine.size() < 4) {
            if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type < 5).count() != 1) {
                return;
            }
            if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type == 14).count() != 1) {
                return;
            }
            if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.id == 987).count() != 1) {
                return;//admin
            }
            Item itemDo = null;
            Item itemDNC = null;
            Item itemDBV = null;
            for (int j = 0; j < player.combineNew.itemsCombine.size(); j++) {
                if (player.combineNew.itemsCombine.get(j).isNotNullItem()) {
                    if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.get(j).template.id == 987) {
                        itemDBV = player.combineNew.itemsCombine.get(j);
                        continue;
                    }
                    if (player.combineNew.itemsCombine.get(j).template.type < 5) {
                        itemDo = player.combineNew.itemsCombine.get(j);
                    } else {
                        itemDNC = player.combineNew.itemsCombine.get(j);
                    }
                }
            }
            if (isCoupleItemNangCapCheck(itemDo, itemDNC)) {
                int countDaNangCap = player.combineNew.countDaNangCap;
                int gold = player.combineNew.goldCombine;
                short countDaBaoVe = player.combineNew.countDaBaoVe;
                if (player.inventory.gold < gold) {
                    Service.gI().sendThongBao(player, "Không đủ vàng để thực hiện");
                    return;
                }

                if (itemDNC.quantity < countDaNangCap) {
                    return;
                }
                if (player.combineNew.itemsCombine.size() == 3) {
                    if (Objects.isNull(itemDBV)) {
                        return;
                    }
                    if (itemDBV.quantity < countDaBaoVe) {
                        return;
                    }
                }

                int level = 0;
                Item.ItemOption optionLevel = null;
                for (Item.ItemOption io : itemDo.itemOptions) {
                    if (io.optionTemplate.id == 72) {
                        level = io.param;
                        optionLevel = io;
                        break;
                    }
                }
                if (level < MAX_LEVEL_ITEM) {
                    player.inventory.gold -= gold;
                    Item.ItemOption option = null;
                    Item.ItemOption option2 = null;
                    for (Item.ItemOption io : itemDo.itemOptions) {
                        if (io.optionTemplate.id == 47
                                || io.optionTemplate.id == 6
                                || io.optionTemplate.id == 0
                                || io.optionTemplate.id == 7
                                || io.optionTemplate.id == 14
                                || io.optionTemplate.id == 22
                                || io.optionTemplate.id == 23) {
                            option = io;
                        } else if (io.optionTemplate.id == 27
                                || io.optionTemplate.id == 28) {
                            option2 = io;
                        }
                    }
                    if (Util.isTrue(player.combineNew.ratioCombine, 100)) {
                        option.param += (option.param * 10 / 100);
                        if (option2 != null) {
                            option2.param += (option2.param * 10 / 100);
                        }
                        if (optionLevel == null) {
                            itemDo.itemOptions.add(new Item.ItemOption(72, 1));
                        } else {
                            optionLevel.param++;
                        }
//                        if (optionLevel != null && optionLevel.param >= 5) {
//                            ServerNotify.gI().notify("Chúc mừng " + player.name + " vừa nâng cấp "
//                                    + "thành công " + trangBi.template.name + " lên +" + optionLevel.param);
//                        }
                        sendEffectSuccessCombine(player);
                    } else {
                        if ((level == 2 || level == 4 || level == 6) && (player.combineNew.itemsCombine.size() != 3)) {
                            option.param -= (option.param * 10 / 100);
                            if (option2 != null) {
                                option2.param -= (option2.param * 10 / 100);
                            }
                            optionLevel.param--;
                        }
                        sendEffectFailCombine(player);
                    }
                    if (player.combineNew.itemsCombine.size() == 3) {
                        InventoryServiceNew.gI().subQuantityItemsBag(player, itemDBV, countDaBaoVe);
                    }
                    InventoryServiceNew.gI().subQuantityItemsBag(player, itemDNC, player.combineNew.countDaNangCap);
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
    }

    private void epantrangbi(Player player) {
        if (player.combineNew.itemsCombine.size() == 3) {
            int gold = 5000;
            if (player.inventory.ruby < gold) {
                Service.gI().sendThongBao(player, "Không đủ vàng để thực hiện");
                return;
            }

            Item cailoz = null;
            Item concac = null;
            Item thoivang = null;
            for (Item item : player.combineNew.itemsCombine) {
                if (item.template.type < 5) {
                    cailoz = item;
                } else if (item.template.id == 674) {
                    concac = item;
                } else if (item.template.id == 457) {
                    thoivang = item;
                }
            }
            if (cailoz != null && concac != null && concac.quantity >= 25) {
                player.inventory.ruby -= 50000;
                player.inventory.gem -= 0;
                InventoryServiceNew.gI().subQuantityItemsBag(player, concac, 25);
                InventoryServiceNew.gI().subQuantityItemsBag(player, thoivang, 1);
                if (Util.isTrue(100, 100)) {
                    cailoz.itemOptions.clear();
                    cailoz.itemOptions.add(new Item.ItemOption(72, 10));
                    cailoz.itemOptions.add(new Item.ItemOption(107, Util.nextInt(3, 6)));
                    cailoz.itemOptions.add(new Item.ItemOption(0, Util.nextInt(5000, 8000)));
                    cailoz.itemOptions.add(new Item.ItemOption(48, Util.nextInt(30000, 60000)));
                    cailoz.itemOptions.add(new Item.ItemOption(21, Util.nextInt(40, 60)));
                    cailoz.itemOptions.add(new Item.ItemOption(30, 0));

                    int rdUp = Util.nextInt(0, 2);
                    if (rdUp == 0) {
                        cailoz.itemOptions.add(new Item.ItemOption(34, 1));
                    } else if (rdUp == 1) {
                        cailoz.itemOptions.add(new Item.ItemOption(35, 1));
                    } else if (rdUp == 2) {
                        cailoz.itemOptions.add(new Item.ItemOption(36, 1));
                    }
                    sendEffectSuccessCombine(player);
                } else {
                    sendEffectFailCombine(player);
                }
                InventoryServiceNew.gI().sendItemBags(player);
                Service.gI().sendMoney(player);
                reOpenItemCombine(player);
            }
        }
    }

    private void nguyetan(Player player) {
        if (player.combineNew.itemsCombine.size() >= 2 && player.combineNew.itemsCombine.size() < 4) {
            if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type < 5).count() != 1) {
                return;
            }
            if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type == 14).count() != 1) {
                return;
            }
            if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.id == 987).count() != 1) {
                return;//admin
            }
            Item itemDo = null;
            Item itemDNC = null;
            Item itemDBV = null;
            for (int j = 0; j < player.combineNew.itemsCombine.size(); j++) {
                if (player.combineNew.itemsCombine.get(j).isNotNullItem()) {
                    if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.get(j).template.id == 987) {
                        itemDBV = player.combineNew.itemsCombine.get(j);
                        continue;
                    }
                    if (player.combineNew.itemsCombine.get(j).template.type < 5) {
                        itemDo = player.combineNew.itemsCombine.get(j);
                    } else {
                        itemDNC = player.combineNew.itemsCombine.get(j);
                    }
                }
            }
            if (isCoupleItemNangCapCheck(itemDo, itemDNC)) {
                int countDaNangCap = player.combineNew.countDaNangCap;
                int gold = 25000;
                short countDaBaoVe = player.combineNew.countDaBaoVe;
                if (player.inventory.ruby < gold) {
                    Service.gI().sendThongBao(player, "Không đủ ngọc để thực hiện");
                    return;
                }

                if (itemDNC.quantity < countDaNangCap) {
                    return;
                }
                if (player.combineNew.itemsCombine.size() == 3) {
                    if (Objects.isNull(itemDBV)) {
                        return;
                    }
                    if (itemDBV.quantity < countDaBaoVe) {
                        return;
                    }
                }

                int level = 0;
                Item.ItemOption optionLevel = null;
                for (Item.ItemOption io : itemDo.itemOptions) {
                    if (io.optionTemplate.id == 35) {
//                        player.inventory.ruby -= gold;
                        level = io.param;
                        optionLevel = io;
                        break;
                    }

                }
                if (level <= 2) {

                    Item.ItemOption option = null;
                    Item.ItemOption option2 = null;
                    for (Item.ItemOption io : itemDo.itemOptions) {
                        if (io.optionTemplate.id == 47
                                || io.optionTemplate.id == 6
                                || io.optionTemplate.id == 0
                                || io.optionTemplate.id == 7
                                || io.optionTemplate.id == 14
                                || io.optionTemplate.id == 22
                                || io.optionTemplate.id == 23) {
                            option = io;
                        } else if (io.optionTemplate.id == 27
                                || io.optionTemplate.id == 28) {
                            option2 = io;
                        }
                    }
                    if (Util.isTrue(player.combineNew.ratioCombine, 100)) {
                        option.param += (option.param * 10 / 100);
                        if (option2 != null) {
                            option2.param += (option2.param * 10 / 100);
                        }
                        if (optionLevel == null) {
                            itemDo.itemOptions.add(new Item.ItemOption(Util.nextInt(35, 35), 1));
                        } else {
                            optionLevel.param++;
                        }
//                        if (optionLevel != null && optionLevel.param >= 5) {
//                            ServerNotify.gI().notify("Chúc mừng " + player.name + " vừa nâng cấp "
//                                    + "thành công " + trangBi.template.name + " lên +" + optionLevel.param);
//                        }
                        sendEffectSuccessCombine(player);
                    } else {
                        if ((level == 2 || level == 4 || level == 6) && (player.combineNew.itemsCombine.size() != 3)) {
                            option.param -= (option.param * 10 / 100);
                            if (option2 != null) {
                                option2.param -= (option2.param * 10 / 100);
                            }
                            optionLevel.param--;
                        }
                        sendEffectFailCombine(player);
                    }
                    if (player.combineNew.itemsCombine.size() == 3) {
                        InventoryServiceNew.gI().subQuantityItemsBag(player, itemDBV, countDaBaoVe);
                    }
                    player.inventory.ruby -= gold;
                    InventoryServiceNew.gI().subQuantityItemsBag(player, itemDNC, player.combineNew.countDaNangCap);
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
    }

    private void nhatan(Player player) {
        if (player.combineNew.itemsCombine.size() >= 2 && player.combineNew.itemsCombine.size() < 4) {
            if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type < 5).count() != 1) {
                return;
            }
            if (player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.type == 14).count() != 1) {
                return;
            }
            if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.stream().filter(item -> item.isNotNullItem() && item.template.id == 987).count() != 1) {
                return;//admin
            }
            Item itemDo = null;
            Item itemDNC = null;
            Item itemDBV = null;
            for (int j = 0; j < player.combineNew.itemsCombine.size(); j++) {
                if (player.combineNew.itemsCombine.get(j).isNotNullItem()) {
                    if (player.combineNew.itemsCombine.size() == 3 && player.combineNew.itemsCombine.get(j).template.id == 987) {
                        itemDBV = player.combineNew.itemsCombine.get(j);
                        continue;
                    }
                    if (player.combineNew.itemsCombine.get(j).template.type < 5) {
                        itemDo = player.combineNew.itemsCombine.get(j);
                    } else {
                        itemDNC = player.combineNew.itemsCombine.get(j);
                    }
                }
            }
            if (isCoupleItemNangCapCheck(itemDo, itemDNC)) {
                int countDaNangCap = player.combineNew.countDaNangCap;
                int gold = 25000;
                short countDaBaoVe = player.combineNew.countDaBaoVe;
                if (player.inventory.ruby < gold) {
                    Service.gI().sendThongBao(player, "Không đủ ngọc để thực hiện");
                    return;
                }

                if (itemDNC.quantity < countDaNangCap) {
                    return;
                }
                if (player.combineNew.itemsCombine.size() == 3) {
                    if (Objects.isNull(itemDBV)) {
                        return;
                    }
                    if (itemDBV.quantity < countDaBaoVe) {
                        return;
                    }
                }

                int level = 0;
                Item.ItemOption optionLevel = null;
                for (Item.ItemOption io : itemDo.itemOptions) {
                    if (io.optionTemplate.id == 36) {
//                        player.inventory.ruby -= gold;
                        level = io.param;
                        optionLevel = io;
                        break;
                    }

                }
                if (level <= 2) {

                    Item.ItemOption option = null;
                    Item.ItemOption option2 = null;
                    for (Item.ItemOption io : itemDo.itemOptions) {
                        if (io.optionTemplate.id == 47
                                || io.optionTemplate.id == 6
                                || io.optionTemplate.id == 0
                                || io.optionTemplate.id == 7
                                || io.optionTemplate.id == 14
                                || io.optionTemplate.id == 22
                                || io.optionTemplate.id == 23) {
                            option = io;
                        } else if (io.optionTemplate.id == 27
                                || io.optionTemplate.id == 28) {
                            option2 = io;
                        }
                    }
                    if (Util.isTrue(player.combineNew.ratioCombine, 100)) {
                        option.param += (option.param * 10 / 100);
                        if (option2 != null) {
                            option2.param += (option2.param * 10 / 100);
                        }
                        if (optionLevel == null) {
                            itemDo.itemOptions.add(new Item.ItemOption(Util.nextInt(36, 36), 1));
                        } else {
                            optionLevel.param++;
                        }
//                        if (optionLevel != null && optionLevel.param >= 5) {
//                            ServerNotify.gI().notify("Chúc mừng " + player.name + " vừa nâng cấp "
//                                    + "thành công " + trangBi.template.name + " lên +" + optionLevel.param);
//                        }
                        sendEffectSuccessCombine(player);
                    } else {
                        if ((level == 2 || level == 4 || level == 6) && (player.combineNew.itemsCombine.size() != 3)) {
                            option.param -= (option.param * 10 / 100);
                            if (option2 != null) {
                                option2.param -= (option2.param * 10 / 100);
                            }
                            optionLevel.param--;
                        }
                        sendEffectFailCombine(player);
                    }
                    if (player.combineNew.itemsCombine.size() == 3) {
                        InventoryServiceNew.gI().subQuantityItemsBag(player, itemDBV, countDaBaoVe);
                    }
                    player.inventory.ruby -= gold;
                    InventoryServiceNew.gI().subQuantityItemsBag(player, itemDNC, player.combineNew.countDaNangCap);
                    InventoryServiceNew.gI().sendItemBags(player);
                    Service.gI().sendMoney(player);
                    reOpenItemCombine(player);
                }
            }
        }
    }

    //--------------------------------------------------------------------------
    /**
     * r
     * Hiệu ứng mở item
     *
     * @param player
     */
    public void sendEffectOpenItem(Player player, short icon1, short icon2) {
        Message msg;
        try {
            msg = new Message(-81);
            msg.writer().writeByte(OPEN_ITEM);
            msg.writer().writeShort(icon1);
            msg.writer().writeShort(icon2);
            player.sendMessage(msg);
            msg.cleanup();
        } catch (Exception e) {
        }
    }

    /**
     * Hiệu ứng đập đồ thành công
     *
     * @param player
     */
    private void sendEffectSuccessCombine(Player player) {
        Message msg;
        try {
            msg = new Message(-81);
            msg.writer().writeByte(COMBINE_SUCCESS);
            player.sendMessage(msg);
            msg.cleanup();
        } catch (Exception e) {
        }
    }

    /**
     * Hiệu ứng đập đồ thất bại
     *
     * @param player
     */
    private void sendEffectFailCombine(Player player) {
        Message msg;
        try {
            msg = new Message(-81);
            msg.writer().writeByte(COMBINE_FAIL);
            player.sendMessage(msg);
            msg.cleanup();
        } catch (Exception e) {
        }
    }

    /**
     * Gửi lại danh sách đồ trong tab combine
     *
     * @param player
     */
    private void reOpenItemCombine(Player player) {
        Message msg;
        try {
            msg = new Message(-81);
            msg.writer().writeByte(REOPEN_TAB_COMBINE);
            msg.writer().writeByte(player.combineNew.itemsCombine.size());
            for (Item it : player.combineNew.itemsCombine) {
                for (int j = 0; j < player.inventory.itemsBag.size(); j++) {
                    if (it == player.inventory.itemsBag.get(j)) {
                        msg.writer().writeByte(j);
                    }
                }
            }
            player.sendMessage(msg);
            msg.cleanup();
        } catch (Exception e) {
        }
    }

    /**
     * Hiệu ứng ghép ngọc rồng
     *
     * @param player
     * @param icon
     */
    private void sendEffectCombineDB(Player player, short icon) {
        Message msg;
        try {
            msg = new Message(-81);
            msg.writer().writeByte(COMBINE_DRAGON_BALL);
            msg.writer().writeShort(icon);
            player.sendMessage(msg);
            msg.cleanup();
        } catch (Exception e) {
        }
    }

    //--------------------------------------------------------------------------Ratio, cost combine
    private int getGoldPhaLeHoa(int star) {
        switch (star) {
            case 0:
                return 5000000;
            case 1:
                return 20000000;
            case 2:
                return 35000000;
            case 3:
                return 50000000;
            case 4:
                return 75000000;
            case 5:
                return 90000000;
            case 6:
                return 115000000;
            case 7:
                return 150000000;
            case 8:
                return 200000000;
            case 9:
                return 300000000;
            case 10:
                return 450000000;
            case 11:
                return 500000000;
            case 12:
                return 1000000000;
        }
        return 0;
    }

    private float getRatioPhaLeHoa(int star) { //tile dap do chi hat mit
        switch (star) {
            case 0:
                return 100f;
            case 1:
                return 100f;
            case 2:
                return 180f;
            case 3:
                return 160f;
            case 4:
                return 140f;
            case 5:
                return 110f;
            case 6:
                return 311f;
            case 7:
                return 110.7f;
            case 8:
                return 110.4f;
            case 9:
                return 1f;
            case 10:
                return 0.9f;
            case 11:
                return 0.8f;
            case 12:
                return 0.8f;
        }

        return 0;
    }

    private int getGemPhaLeHoa(int star) {
        switch (star) {
            case 0:
                return 10;
            case 1:
                return 20;
            case 2:
                return 30;
            case 3:
                return 40;
            case 4:
                return 50;
            case 5:
                return 60;
            case 6:
                return 70;
            case 7:
                return 80;
            case 8:
                return 50;
            case 9:
                return 50;
            case 10:
                return 50;
            case 11:
                return 50;
            case 12:
                return 50;
        }
        return 0;
    }

    private int getGemEpSao(int star) {
        switch (star) {
            case 0:
                return 1;
            case 1:
                return 2;
            case 2:
                return 5;
            case 3:
                return 10;
            case 4:
                return 25;
            case 5:
                return 50;
            case 6:
                return 100;
        }
        return 0;
    }

    private double getTileNangCapSKH(int level) {
        switch (level) {
            case 0:
                return 100;
            case 1:
                return 80;
            case 2:
                return 50;
            case 3:
                return 10;
            case 4:
                return 5;
            case 5:
                return 20;
            case 6:
                return 10;
            case 7:
                return 5;

        }
        return 0;
    }

    private int getCountDaNangCapSKH(int level) {
        switch (level) {
            case 0:
                return 10;
            case 1:
                return 5;
            case 2:
                return 10;
            case 3:
                return 3;
            case 4:
                return 50;
            case 5:
                return 60;
            case 6:
                return 70;
            case 7:
                return 60;
            case 8:
                return 80;
        }
        return 0;
    }

    private boolean isCoupleItemNangCapSKHCheck(Item trangBi, Item daNangCap) {
        if (trangBi != null && daNangCap != null) {
            if (trangBi.template.type == 0 && trangBi.isSKHLEVEL() && daNangCap.template.id == 1342) {
                return true;
            } else if (trangBi.template.type == 1 && trangBi.isSKHLEVEL() && daNangCap.template.id == 1342) {
                return true;
            } else if (trangBi.template.type == 2 && trangBi.isSKHLEVEL() && daNangCap.template.id == 1342) {
                return true;
            } else if (trangBi.template.type == 3 && trangBi.isSKHLEVEL() && daNangCap.template.id == 1342) {
                return true;
            } else if (trangBi.template.type == 4 && trangBi.isSKHLEVEL() && daNangCap.template.id == 1342) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    private double getTileNangCapDo(int level) {
        switch (level) {
            case 0:
                return 80;
            case 1:
                return 50;
            case 2:
                return 20;
            case 3:
                return 10;
            case 4:
                return 7;
            case 5:
                return 5;
            case 6:
                return 1;
            case 7: // 7 sao
                return 1;
            case 8:
                return 1;
            case 9:
                return 1;
            case 10: // 7 sao
                return 0.3;
            case 11: // 7 sao
                return 1;
            case 12: // 7 sao
                return 1;
        }
        return 0;
    }

    private int getCountDaNangCapDo(int level) {
        switch (level) {
            case 0:
                return 3;
            case 1:
                return 7;
            case 2:
                return 11;
            case 3:
                return 17;
            case 4:
                return 23;
            case 5:
                return 35;
            case 6:
                return 50;
            case 7:
                return 70;
        }
        return 0;
    }

    private int getCountDaBaoVe(int level) {
        return level + 1;
    }

    private int getGoldNangCapDo(int level) {
        switch (level) {
            case 0:
                return 10000;
            case 1:
                return 70000;
            case 2:
                return 300000;
            case 3:
                return 1500000;
            case 4:
                return 7000000;
            case 5:
                return 23000000;
            case 6:
                return 100000000;
            case 7:
                return 250000000;
        }
        return 0;
    }

    private float getRationangbt(int lvbt) { // tỉ lệ nâng cấp bông tai c1 và c2
        switch (lvbt) {
            case 1:
                return 70f;
            case 2:
                return 50f;
            case 3:
                return 30f;
        }
        return 0;
    }

    private int getGoldnangbt(int lvbt) {
        return GOLD_BONG_TAI2 + 200000000 * lvbt;
    }

    private int getgemdnangbt(int lvbt) {
        return GEM_BONG_TAI2;
    }

    private int getRubydnangbt(int lvbt) {
        return RUBY_BONG_TAI2 + 2000 * lvbt;
    }

    private int getcountmvbtnangbt(int lvbt) {// so luong mảnh vỡ bông tai cần nâng cấp
        switch (lvbt) {
            case 1:
                return 99;
            case 2:
                return 999;
            case 3:
                return 9999;
        }
        return 0;
    }

    private boolean checkbongtai(Item item) {
        if (item.template.id == 454 || item.template.id == 921 || item.template.id == 1228 || item.template.id == 1229) {
            return true;
        }
        return false;
    }

    private int lvbt(Item bongtai) {
        switch (bongtai.template.id) {
            case 454:
                return 1;
            case 921:
                return 2;
            case 1228:
                return 3;
            case 1229:
                return 4;
        }

        return 0;

    }

    private int getDiemNangcapChanmenh(int star) {
        switch (star) {
            case 0:
                return 10;
            case 1:
                return 10;
            case 2:
                return 10;
            case 3:
                return 10;
            case 4:
                return 10;
            case 5:
                return 10;
            case 6:
                return 10;
            case 7:
                return 10;
        }
        return 0;
    }

    private int getDaNangcapChanmenh(int star) {
        switch (star) {
            case 0:
                return 20;
            case 1:
                return 20;
            case 2:
                return 20;
            case 3:
                return 20;
            case 4:
                return 20;
            case 5:
                return 20;
            case 6:
                return 20;
            case 7:
                return 20;
        }
        return 0;
    }

    private float getTiLeNangcapChanmenh(int star) {
        switch (star) {
            case 0:
                return 40;
            case 1:
                return 30;
            case 2:
                return 25;
            case 3:
                return 10;
            case 4:
                return 8;
            case 5:
                return 5f;
            case 6:
                return 3f;
            case 7:
                return 3f;
        }
        return 0;
    }

    //--------------------------------------------------------------------------check
    private boolean isCoupleItemNangCap(Item item1, Item item2) {
        Item trangBi = null;
        Item daNangCap = null;
        if (item1 != null && item1.isNotNullItem()) {
            if (item1.template.type < 5) {
                trangBi = item1;
            } else if (item1.template.type == 14) {
                daNangCap = item1;
            }
        }
        if (item2 != null && item2.isNotNullItem()) {
            if (item2.template.type < 5) {
                trangBi = item2;
            } else if (item2.template.type == 14) {
                daNangCap = item2;
            }
        }
        if (trangBi != null && daNangCap != null) {
            if (trangBi.template.type == 0 && daNangCap.template.id == 223) {
                return true;
            } else if (trangBi.template.type == 1 && daNangCap.template.id == 222) {
                return true;
            } else if (trangBi.template.type == 2 && daNangCap.template.id == 224) {
                return true;
            } else if (trangBi.template.type == 3 && daNangCap.template.id == 221) {
                return true;
            } else if (trangBi.template.type == 4 && daNangCap.template.id == 220) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    private boolean issachTuyetKy(Item item) {
        if (item != null && item.isNotNullItem()) {
            if (item.template.type == 77) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    private boolean checkHaveOption(Item item, int viTriOption, int idOption) {
        if (item != null && item.isNotNullItem()) {
            if (item.itemOptions.get(viTriOption).optionTemplate.id == idOption) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    private boolean isCoupleItemNangCapCheck(Item trangBi, Item daNangCap) {
        if (trangBi != null && daNangCap != null) {
            if (trangBi.template.type == 0 && daNangCap.template.id == 223) {
                return true;
            } else if (trangBi.template.type == 1 && daNangCap.template.id == 222) {
                return true;
            } else if (trangBi.template.type == 2 && daNangCap.template.id == 224) {
                return true;
            } else if (trangBi.template.type == 3 && daNangCap.template.id == 221) {
                return true;
            } else if (trangBi.template.type == 4 && daNangCap.template.id == 220) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    private boolean isDaPhaLe(Item item) {
        return item != null && (item.template.type == 30 || (item.template.id >= 14 && item.template.id <= 20));
    }

    private boolean isTrangBiPhaLeHoa(Item item) {
        if (item != null && item.isNotNullItem()) {
            if (item.template.type <= 5 || item.template.type == 32) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    private int getParamDaPhaLe(Item daPhaLe) {
        if (daPhaLe.template.type == 30) {
            return daPhaLe.itemOptions.get(0).param;
        }
        switch (daPhaLe.template.id) {
            case 20:
                return 5; // +5%hp
            case 19:
                return 5; // +5%ki
            case 18:
                return 5; // +5%hp/30s
            case 17:
                return 5; // +5%ki/30s
            case 16:
                return 3; // +3%sđ
            case 15:
                return 2; // +2%giáp
            case 14:
                return 1; // +5%né đòn
            default:
                return -1;
        }
    }

    private int getOptionDaPhaLe(Item daPhaLe) {
        if (daPhaLe.template.type == 30) {
            return daPhaLe.itemOptions.get(0).optionTemplate.id;
        }
        switch (daPhaLe.template.id) {
            case 20:
                return 77;
            case 19:
                return 103;
            case 18:
                return 80;
            case 17:
                return 81;
            case 16:
                return 50;
            case 15:
                return 94;
            case 14:
                return 108;
            default:
                return -1;
        }
    }

    /**
     * Trả về id item c0
     *
     * @param gender
     * @param type
     * @return
     */
    private int getTempIdItemC0(int gender, int type) {
        if (type == 4) {
            return 12;
        }
        switch (gender) {
            case 0:
                switch (type) {
                    case 0:
                        return 0;
                    case 1:
                        return 6;
                    case 2:
                        return 21;
                    case 3:
                        return 27;
                }
                break;
            case 1:
                switch (type) {
                    case 0:
                        return 1;
                    case 1:
                        return 7;
                    case 2:
                        return 22;
                    case 3:
                        return 28;
                }
                break;
            case 2:
                switch (type) {
                    case 0:
                        return 2;
                    case 1:
                        return 8;
                    case 2:
                        return 23;
                    case 3:
                        return 29;
                }
                break;
        }
        return -1;
    }

    //Trả về tên đồ c0
    private String getNameItemC0(int gender, int type) {
        if (type == 4) {
            return "Rada cấp 1";
        }
        switch (gender) {
            case 0:
                switch (type) {
                    case 0:
                        return "Áo vải 3 lỗ";
                    case 1:
                        return "Quần vải đen";
                    case 2:
                        return "Găng thun đen";
                    case 3:
                        return "Giầy nhựa";
                }
                break;
            case 1:
                switch (type) {
                    case 0:
                        return "Áo sợi len";
                    case 1:
                        return "Quần sợi len";
                    case 2:
                        return "Găng sợi len";
                    case 3:
                        return "Giầy sợi len";
                }
                break;
            case 2:
                switch (type) {
                    case 0:
                        return "Áo vải thô";
                    case 1:
                        return "Quần vải thô";
                    case 2:
                        return "Găng vải thô";
                    case 3:
                        return "Giầy vải thô";
                }
                break;
        }
        return "";
    }

    //--------------------------------------------------------------------------Text tab combine
    private String getTextTopTabCombine(int type) {
        switch (type) {
            case EP_SAO_TRANG_BI:
                return "Ta sẽ phù phép\ncho trang bị của ngươi\ntrở lên mạnh mẽ";
            case PHA_LE_HOA_TRANG_BI:
                return "Ta sẽ phù phép\ncho trang bị của ngươi\ntrở thành trang bị pha lê";
            case NHAP_NGOC_RONG:
                return "Ta sẽ phù phép\ncho 7 viên Ngọc Rồng\nthành 1 viên Ngọc Rồng cấp cao";
            case NANG_CAP_VAT_PHAM:
                return "Ta sẽ phù phép cho trang bị của ngươi trở lên mạnh mẽ";
            case PHAN_RA_DO_THAN_LINH:
                return "Ta sẽ phân rã \n  ngọc của người thành điểm!";
            case PHAN_RA_DO_THAN_RA_DA_NANG_CAP:
                return "Ta sẽ phân rã \n  đồ thần của người thành đá nâng SKH!";
            case NANG_CAP_DO_TS:
                return "Ta sẽ nâng cấp \n  trang bị của người thành\n đồ thiên sứ!";
            case NANG_CAP_SKH_VIP:
                return "Thiên sứ nhờ ta nâng cấp \n  trang bị của người thành\n SKH VIP!";
            case NANG_CAP_SKH_VIP_DHD:
                return "Hủy diệt và Thần Linh nhờ ta nâng cấp\ntrang bị thành SKH VIP cao cấp!";
            case NANG_CAP_SKH_TL:
                return "Thiên Sứ và Hủy Diệt nhờ ta nâng cấp\ntrang bị thành Thần Linh SKH!";
            case NANG_CAP_SKH_HD:
                return "Thần Linh SKH và Hủy Diệt nhờ ta nâng cấp\ntrang bị thành Hủy Diệt SKH!";
            case NANG_CAP_SKH_TS:
                return "Hủy Diệt SKH và Thiên Sứ nhờ ta nâng cấp\ntrang bị thành Thiên Sứ SKH!";
            case NANG_CAP_BONG_TAI:
                return "Ta sẽ phù phép\ncho bông tai Porata của ngươi\ntiến hóa thêm 1 cấp";
            case MO_CHI_SO_BONG_TAI:
                return "Ta sẽ phù phép\ncho bông tai Porata của ngươi\ncó 1 chỉ số ngẫu nhiên";
            case NANG_CAP_CHAN_MENH:
                return "Ta sẽ Nâng cấp\nChân Mệnh của ngươi\ncao hơn một bậc";
            case CHE_TAO_TRANG_BI_TS:
                return "Chế tạo\ntrang bị thiên sứ";
            case LUYEN_HOA_CHIEN_LINH:
                return "Ta sẽ cùng người luyện hóa\nchiến linh";
            case EP_AN_TRANG_BI:
                return "Ép Ấn Trang Bị\nKhông Nhận Địt Nhau";
            case NGUYET_AN:
                return "Ép Ấn Trang Bị\nKhông Nhận Địt Nhau";
            case DOI_DIEM:
                return "Thức Ăn";
            case GIAM_DINH_SACH:
                return "Ta sẽ phù phép\ngiám định sách đó cho ngươi";
            case TAY_SACH:
                return "Ta sẽ phù phép\ntẩy sách đó cho ngươi";
            case NANG_CAP_SACH_TUYET_KY:
                return "Ta sẽ phù phép\nnâng cấp Sách Tuyệt Kỹ cho ngươi";
            case PHUC_HOI_SACH:
                return "Ta sẽ phù phép\nphục hồi sách cho ngươi";
            case PHAN_RA_SACH:
                return "Ta sẽ phù phép\nphân rã sách cho ngươi";
            case PS_HOA_TRANG_BI:
                return "Ta sẽ phù phép\ncho trang bị của ngươi\ntrở thành trang bị pháp sư";
            case TAY_PS_HOA_TRANG_BI:
                return "Ta sẽ Giải pháp sư\nncho trang bị của ngươi\nntrở thành trang bị thường";
            default:
                return "";
        }
    }

    private String getTextInfoTabCombine(int type) {
        switch (type) {
            case EP_SAO_TRANG_BI:
                return "Chọn trang bị\n(Áo, quần, găng, giày hoặc rađa) có ô đặt sao pha lê\nChọn loại sao pha lê\n"
                        + "Sau đó chọn 'Nâng cấp'";
            case PHA_LE_HOA_TRANG_BI:
                return "Chọn trang bị\n(Áo, quần, găng, giày hoặc rađa)\nSau đó chọn 'Nâng cấp'";
            case NHAP_NGOC_RONG:
                return "Vào hành trang\nChọn 7 viên ngọc cùng sao\nSau đó chọn 'Làm phép'";
            case NANG_CAP_VAT_PHAM:
                return "vào hành trang\nChọn trang bị\n(Áo, quần, găng, giày hoặc rađa)\nChọn loại đá để nâng cấp\n"
                        + "Sau đó chọn 'Nâng cấp'";
            case PHAN_RA_DO_THAN_LINH:
                return "vào hành trang\nChọn item\n(Đồ Thần)\nChọn Đồ Thần Rồi Tách\n"
                        + "Sau đó chọn 'Tách'";
            case PHAN_RA_DO_THAN_RA_DA_NANG_CAP:
                return "vào hành trang\nChọn item\n(Đồ Thần)\nChọn Đồ Thần Rồi Tách\n"
                        + "Sau đó chọn 'Tách'";
            case NANG_CAP_DO_TS:
                return "vào hành trang\nChọn 2 trang bị hủy diệt bất kì\nkèm 1 món đồ thần linh\n và 5 mảnh thiên sứ\n "
                        + "sẽ cho ra đồ thiên sứ từ 0-15% chỉ số"
                        + "Sau đó chọn 'Nâng Cấp'";
            case NANG_CAP_SKH_VIP:
                return "vào hành trang\nChọn 1 trang bị thiên sứ bất kì\nChọn tiếp ngẫu nhiên 2 món SKH thường \n "
                        + " đồ SKH VIP sẽ cùng loại \n với đồ thiên sứ!"
                        + "Chỉ cần chọn 'Nâng Cấp'";
            case NANG_CAP_BONG_TAI:
                return "Vào hành trang\nChọn bông tai Porata\nChọn mảnh bông tai để nâng cấp\n99 mảnh cho bông tai cho cấp 2\n999 mảnh cho bông tai cho cấp 3\n9999 mảnh cho bông tai cho cấp 4\nSau đó chọn 'Nâng cấp'";
            case MO_CHI_SO_BONG_TAI:
                return "Vào hành trang\nChọn bông tai Porata\nChọn mảnh hồn bông tai số lượng 99 cái\nvà đá xanh lam để nâng cấp\nSau đó chọn 'Nâng cấp'";
            case NANG_CAP_CHAN_MENH:
                return "Vào hành trang\nChọn Chân mệnh muốn nâng cấp\nChọn Đá Chân Mệnh\n"
                        + "Sau đó chọn 'Nâng cấp'\n"
                        + "Nếu cho thêm thỏi vàng vào sẽ giúp giảm số lượng Đá Chân Mệnh và tăng thêm tỉ lệ\n\n"
                        + "Lưu ý: Khi Nâng cấp Thành công sẽ tăng thêm % chỉ số của cấp trước đó";
            case NANG_CAP_LEVEL_SKH:
                return "Đổi 1 món Hủy Diệt thành 1 món SKH cùng loại"
                    + "\nSet SKH nhận được là ngẫu nhiên"
                    + "\nChỉ cần chọn 'Nâng Cấp'";
            case NANG_CAP_SKH_VIP_DHD:
                return "Chọn 1 món Hủy Diệt và 3 món Thần Linh khác nhau"
                    + "\nKết quả cùng loại, ở phân khúc cao hơn"
                    + "\nTỷ lệ từng phân khúc được cấu hình riêng theo set";
            case NANG_CAP_SKH_TL:
                return "Chọn 1 món Thiên Sứ và 3 món Hủy Diệt"
                    + "\nKết quả: 1 món Thần Linh SKH cùng loại";
            case NANG_CAP_SKH_HD:
                return "Chọn 1 món Thần Linh SKH và 3 món Hủy Diệt"
                    + "\nKết quả: 1 món Hủy Diệt SKH cùng loại";
            case NANG_CAP_SKH_TS:
                return "Chọn 1 món Hủy Diệt SKH và 3 món Thiên Sứ khác nhau"
                    + "\nKết quả: 1 món Thiên Sứ SKH cùng loại";
            case CHE_TAO_TRANG_BI_TS:
                return "Cần 1 công thức vip\nMảnh trang bị tương ứng\n"
                        + "Số Lượng\n999"
                        + "Có thể thêm\nĐá nâng cấp (tùy chọn) để tăng tỉ lệ chế tạo\n"
                        + "Đá may mắn (tùy chọn) để tăng tỉ lệ các chỉ số cơ bản và chỉ số ẩn\n"
                        + "Sau đó chọn 'Nâng cấp'";
            case EP_AN_TRANG_BI:
                return "Vào hành trang\nChọn Trang Bị\nChọn Đá Ngũ Sắc Số Lượng 25 \nThêm 1 Thỏi Vàng Vào\nSau đó chọn 'Nâng cấp'";
            case NGUYET_AN:
                return "Vào hành trang\nChọn Trang Bị\nChọn Đá Nâng Cấp Số Lượng 99 \nMấy Viên Kiểu Titan Ruby Các Thứ Ấy\nSau đó chọn 'Nâng cấp'\nCHÚ Ý KHÔNG DÙNG\nTRANG BỊ KÍCH HOẠT";
            case DOI_DIEM:
                return "Vào hành trang\nChọn x99 Thức Ăn\nSau đó chọn 'Nâng cấp'";
            case GIAM_DINH_SACH:
                return "Vào hành trang chọn\n1 sách cần giám định";
            case TAY_SACH:
                return "Vào hành trang chọn\n1 sách cần tẩy";
            case NANG_CAP_SACH_TUYET_KY:
                return "Vào hành trang chọn\nSách Tuyệt Kỹ 1 cần nâng cấp và 10 Kìm bấm giấy";
            case PHUC_HOI_SACH:
                return "Vào hành trang chọn\nCác Sách Tuyệt Kỹ cần phục hồi";
            case PHAN_RA_SACH:
                return "Vào hành trang chọn\n1 sách cần phân rã";
            case PS_HOA_TRANG_BI:
                return "Chọn trang bị\n(Linh Thú, Và Vật Phẩm Đeo Lưng)\nSau đó chọn 'Nâng cấp'";
            case TAY_PS_HOA_TRANG_BI:
                return "Chọn trang bị\n(Linh Thú, Và Vật Phẩm Đeo Lưng)\nSau đó chọn 'Nâng cấp'";
            default:
                return "";
        }
    }

}
