using System;

namespace Game3
{
	// Token: 0x02000302 RID: 770
	public class MsgDlg : Dialog
	{
		// Token: 0x0600221E RID: 8734 RVA: 0x002194B7 File Offset: 0x002176B7
		public MsgDlg()
		{
			this.padLeft = 35;
			if (GameCanvas.w <= 176)
			{
				this.padLeft = 10;
			}
			if (GameCanvas.w > 320)
			{
				this.padLeft = 80;
			}
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x002194F7 File Offset: 0x002176F7
		public void pleasewait()
		{
			this.setInfo(mResources.PLEASEWAIT, null, null, null);
			GameCanvas.currentDialog = this;
			this.time = mSystem.currentTimeMillis() + 5000L;
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0021951F File Offset: 0x0021771F
		public override void show()
		{
			GameCanvas.currentDialog = this;
			this.time = -1L;
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x00219530 File Offset: 0x00217730
		public void setInfo(string info)
		{
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
			this.h = 80;
			if (this.info.Length >= 5)
			{
				this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
			}
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x00219590 File Offset: 0x00217790
		public void setInfo(string info, Command left, Command center, Command right)
		{
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
			this.left = left;
			this.center = center;
			this.right = right;
			this.h = 80;
			if (this.info.Length >= 5)
			{
				this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
			}
			if (GameCanvas.isTouch)
			{
				if (left != null)
				{
					this.left.x = GameCanvas.w / 2 - 68 - 5;
					this.left.y = GameCanvas.h - 50;
				}
				if (right != null)
				{
					this.right.x = GameCanvas.w / 2 + 5;
					this.right.y = GameCanvas.h - 50;
				}
				if (center != null)
				{
					this.center.x = GameCanvas.w / 2 - 35;
					this.center.y = GameCanvas.h - 50;
				}
			}
			this.isWait = false;
			this.time = -1L;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x002196A0 File Offset: 0x002178A0
		public override void paint(mGraphics g)
		{
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (!LoginScr.isContinueToLogin)
			{
				int num = GameCanvas.h - this.h - 38;
				int w = GameCanvas.w - this.padLeft * 2;
				GameCanvas.paintz.paintPopUp(this.padLeft, num, w, this.h, g);
				int num2 = num + (this.h - this.info.Length * mFont.tahoma_8b.getHeight()) / 2 - 2;
				if (this.isWait)
				{
					num2 += 8;
					GameCanvas.paintShukiren(GameCanvas.hw, num2 - 12, g);
				}
				int num3 = 0;
				int num4 = num2;
				while (num3 < this.info.Length)
				{
					mFont.tahoma_7b_dark.drawString(g, this.info[num3], GameCanvas.hw, num4, 2);
					num3++;
					num4 += mFont.tahoma_8b.getHeight();
				}
				base.paint(g);
			}
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00219784 File Offset: 0x00217984
		public override void update()
		{
			base.update();
			if (this.time != -1L && mSystem.currentTimeMillis() > this.time)
			{
				GameCanvas.endDlg();
			}
		}

		// Token: 0x04004450 RID: 17488
		public string[] info;

		// Token: 0x04004451 RID: 17489
		public bool isWait;

		// Token: 0x04004452 RID: 17490
		private int h;

		// Token: 0x04004453 RID: 17491
		private int padLeft;

		// Token: 0x04004454 RID: 17492
		private long time = -1L;
	}
}
