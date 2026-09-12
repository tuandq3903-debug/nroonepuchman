using System;

namespace Game1
{
	// Token: 0x020004B2 RID: 1202
	public class MsgDlg : Dialog
	{
		// Token: 0x06003566 RID: 13670 RVA: 0x003435FF File Offset: 0x003417FF
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

		// Token: 0x06003567 RID: 13671 RVA: 0x0034363F File Offset: 0x0034183F
		public void pleasewait()
		{
			this.setInfo(mResources.PLEASEWAIT, null, null, null);
			GameCanvas.currentDialog = this;
			this.time = mSystem.currentTimeMillis() + 5000L;
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x00343667 File Offset: 0x00341867
		public override void show()
		{
			GameCanvas.currentDialog = this;
			this.time = -1L;
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x00343678 File Offset: 0x00341878
		public void setInfo(string info)
		{
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
			this.h = 80;
			if (this.info.Length >= 5)
			{
				this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
			}
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x003436D8 File Offset: 0x003418D8
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

		// Token: 0x0600356B RID: 13675 RVA: 0x003437E8 File Offset: 0x003419E8
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

		// Token: 0x0600356C RID: 13676 RVA: 0x003438CC File Offset: 0x00341ACC
		public override void update()
		{
			base.update();
			if (this.time != -1L && mSystem.currentTimeMillis() > this.time)
			{
				GameCanvas.endDlg();
			}
		}

		// Token: 0x0400694E RID: 26958
		public string[] info;

		// Token: 0x0400694F RID: 26959
		public bool isWait;

		// Token: 0x04006950 RID: 26960
		private int h;

		// Token: 0x04006951 RID: 26961
		private int padLeft;

		// Token: 0x04006952 RID: 26962
		private long time = -1L;
	}
}
