using System;

namespace Game6
{
	// Token: 0x0200007A RID: 122
	public class MsgDlg : Dialog
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x0005A1F3 File Offset: 0x000583F3
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

		// Token: 0x06000533 RID: 1331 RVA: 0x0005A233 File Offset: 0x00058433
		public void pleasewait()
		{
			this.setInfo(mResources.PLEASEWAIT, null, null, null);
			GameCanvas.currentDialog = this;
			this.time = mSystem.currentTimeMillis() + 5000L;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0005A25B File Offset: 0x0005845B
		public override void show()
		{
			GameCanvas.currentDialog = this;
			this.time = -1L;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0005A26C File Offset: 0x0005846C
		public void setInfo(string info)
		{
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
			this.h = 80;
			if (this.info.Length >= 5)
			{
				this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0005A2CC File Offset: 0x000584CC
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

		// Token: 0x06000537 RID: 1335 RVA: 0x0005A3DC File Offset: 0x000585DC
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

		// Token: 0x06000538 RID: 1336 RVA: 0x0005A4C0 File Offset: 0x000586C0
		public override void update()
		{
			base.update();
			if (this.time != -1L && mSystem.currentTimeMillis() > this.time)
			{
				GameCanvas.endDlg();
			}
		}

		// Token: 0x04000CD3 RID: 3283
		public string[] info;

		// Token: 0x04000CD4 RID: 3284
		public bool isWait;

		// Token: 0x04000CD5 RID: 3285
		private int h;

		// Token: 0x04000CD6 RID: 3286
		private int padLeft;

		// Token: 0x04000CD7 RID: 3287
		private long time = -1L;
	}
}
