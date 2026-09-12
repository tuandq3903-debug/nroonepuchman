package com.girlkun.models.player;

import com.girlkun.models.item.Item;

public class SetClothes {

    private Player player;
    private boolean huydietClothers;

    public SetClothes(Player player) {
        this.player = player;
    }

    public byte songoku;
    public byte thienXinHang;
    public byte kirin;

    public byte ocTieu;
    public byte pikkoroDaimao;
    public byte picolo;

    public byte kakarot;
    public byte cadic;
    public byte nappa;
    public byte TinhAn;
    public byte NhatAn;
    public byte NguyetAn;

    public byte worldcup;
    public byte setDHD;

    public boolean godClothes;
    public int ctHaiTac = -1;

    public void setup() {
        setDefault();
        setupSKT();
        this.godClothes = true;
        for (int i = 0; i < 5; i++) {
            Item item = this.player.inventory.itemsBody.get(i);
            if (item.isNotNullItem()) {
                if (item.template.id > 567 || item.template.id < 555) {
                    this.godClothes = false;
                    break;
                }
            } else {
                this.godClothes = false;
                break;
            }
        }
        Item ct = this.player.inventory.itemsBody.get(5);
        if (ct.isNotNullItem()) {
            switch (ct.template.id) {
                case 618:
                case 619:
                case 620:
                case 621:
                case 622:
                case 623:
                case 624:
                case 626:
                case 627:
                    this.ctHaiTac = ct.template.id;
                    break;

            }
        }
    }

    public int level;

    private void setupSKT() {
        int levelDefault = 0;
        for (int i = 0; i < 5; i++) {
            Item item = this.player.inventory.itemsBody.get(i);
            if (item.isNotNullItem()) {
                boolean isActSet = false;
                for (Item.ItemOption io : item.itemOptions) {
                    switch (io.optionTemplate.id) {
                        case 129:
                        case 141:
                        case 225:
                        case 237:
                            if (level == 0) {
                                level = io.param;
                            }
                            if (io.param != level) {
                                continue;
                            }
                            songoku++;
                            isActSet = true;
                            break;
                        case 127:
                        case 139:
                        case 223:
                        case 235:
                            if (level == 0) {
                                level = io.param;
                            }
                            if (io.param != level) {
                                continue;
                            }
                            thienXinHang++;
                            isActSet = true;
                            break;
                        case 128:
                        case 140:
                        case 224:
                        case 236:
                            if (level == 0) {
                                level = io.param;
                            }
                            if (io.param != level) {
                                continue;
                            }
                            kirin++;
                            isActSet = true;
                            break;
                        case 131:
                        case 143:
                        case 227:
                        case 239:
                            if (level == 0) {
                                level = io.param;
                            }
                            if (io.param != level) {
                                continue;
                            }
                            ocTieu++;
                            isActSet = true;
                            break;
                        case 132:
                        case 144:
                        case 228:
                        case 240:
                            if (level == 0) {
                                level = io.param;
                            }
                            if (io.param != level) {
                                continue;
                            }
                            pikkoroDaimao++;
                            isActSet = true;
                            break;
                        case 130:
                        case 142:
                        case 226:
                        case 238:
                            if (level == 0) {
                                level = io.param;
                            }
                            if (io.param != level) {
                                continue;
                            }
                            picolo++;
                            isActSet = true;
                            break;
                        case 135:
                        case 138:
                        case 231:
                        case 234:
                            if (level == 0) {
                                level = io.param;
                            }
                            if (io.param != level) {
                                continue;
                            }
                            nappa++;
                            isActSet = true;
                            break;
                        case 133:
                        case 136:
                        case 229:
                        case 232:
                            isActSet = true;
                            kakarot++;
                            break;
                        case 134:
                        case 137:
                        case 230:
                        case 233:
                            isActSet = true;
                            cadic++;
                            break;
                        case 34:
                            if (io.param > 0) {
                                TinhAn++;
                            }
                            break;
                        case 35:
                            if (io.param > 0) {
                                NguyetAn++;
                            }
                            break;
                        case 36:
                            if (io.param > 0) {
                                NhatAn++;
                            }
                            break;
                        case 21:
                            if (io.param == 80) {
                                setDHD++;
                            }
                            break;
                    }

                    if (isActSet) {
                        break;
                    }
                }
            } else {
                break;
            }
        }
    }

    private void setan() {
        for (int i = 6; i > 6; i++) {
            Item item = this.player.inventory.itemsBody.get(i);
            if (item.isNotNullItem()) {
                boolean isActSet = false;
                for (Item.ItemOption io : item.itemOptions) {
                    switch (io.optionTemplate.id) {
                        case 34:
                            if (io.param > 0) {
                                TinhAn++;
                            }
                            break;
//                        case 190:
                        case 35:
                            if (io.param > 0) {
                                NguyetAn++;
                            }
                            break;
//                        case 191:
                        case 36:
                            if (io.param > 0) {
                                NhatAn++;
                            }
                            break;
                        case 21:
                            if (io.param == 80) {
                                setDHD++;
                            }
                            break;
                    }

                    if (isActSet) {
                        break;
                    }
                }
            } else {
                break;
            }
        }
    }

    //checksetthanlinh
    public boolean setGod() {
        for (int i = 0; i < 6; i++) {
            Item item = this.player.inventory.itemsBody.get(i);
            if (item.isNotNullItem()) {
                if (item.template.id >= 555 && item.template.id <= 567) {
                    i++;
                } else if (i == 5) {
                    this.godClothes = true;
                    break;
                }
            } else {
                this.godClothes = false;
                break;
            }
        }
        return this.godClothes ? true : false;
    }

    // check set huy diet
    public boolean setGod14() {
        for (int i = 0; i < 6; i++) {
            Item item = this.player.inventory.itemsBody.get(i);
            if (item.isNotNullItem()) {
                if (item.template.id >= 650 && item.template.id <= 663) {
                    i++;
                } else if (i == 5) {
                    this.huydietClothers = true;
                    break;
                }
            } else {
                this.huydietClothers = false;
                break;
            }
        }
        return this.huydietClothers ? true : false;
    }

    private void setDefault() {
        this.songoku = 0;
        this.thienXinHang = 0;
        this.kirin = 0;
        this.ocTieu = 0;
        this.pikkoroDaimao = 0;
        this.picolo = 0;
        this.kakarot = 0;
        this.cadic = 0;
        this.nappa = 0;
        this.setDHD = 0;
        this.worldcup = 0;
        this.NhatAn = 0;
        this.TinhAn = 0;
        this.NguyetAn = 0;
        this.level = 0;
        this.godClothes = false;
        this.ctHaiTac = -1;
    }

    public void dispose() {
        this.player = null;
    }
}
