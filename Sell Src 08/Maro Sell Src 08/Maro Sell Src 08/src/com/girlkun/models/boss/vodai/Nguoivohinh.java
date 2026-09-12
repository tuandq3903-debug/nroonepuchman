package com.girlkun.models.boss.vodai;

import com.girlkun.models.boss.BossData;
import com.girlkun.models.boss.BossID;
import com.girlkun.models.boss.BossesData;
import com.girlkun.models.player.Player;
import com.girlkun.services.Service;
import com.girlkun.utils.Util;

/**
 * @author BTH sieu cap vippr0
 */
public class Nguoivohinh extends BossVD {

    private long lastTimeTanHinh;
    private boolean goToPlayer;

    public Nguoivohinh(Player player) throws Exception {
        super(BossID.NGUOIVOHINH, BossesData.NGUOIVOHINH);
        this.playerAtt = player;
        lastTimeTanHinh = System.currentTimeMillis();
    }

    @Override
    public void tanHinh() {
        if (Util.canDoWithTime(lastTimeTanHinh, 15000)) {
            lastTimeTanHinh = System.currentTimeMillis();
        }

        if (!Util.canDoWithTime(this.lastTimeTanHinh, 5000)) {
            Service.gI().setPos2(this, playerAtt.location.x + (Util.getOne(-1, 1) * Util.nextInt(20, 200)),
                    10000);
            goToPlayer = false;
        } else {
            if (!goToPlayer) {
                goToPlayer = true;
                goToPlayer(playerAtt, false);
            }
        }

    }
}
