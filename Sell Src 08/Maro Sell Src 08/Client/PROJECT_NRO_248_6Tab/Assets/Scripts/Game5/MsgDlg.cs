using System;

namespace Game5
{
	// Token: 0x02000152 RID: 338
	public class MsgDlg : Dialog
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x000EF36F File Offset: 0x000ED56F
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

		// Token: 0x06000ED7 RID: 3799 RVA: 0x000EF3AF File Offset: 0x000ED5AF
		public void pleasewait()
		{
			this.setInfo(mResources.PLEASEWAIT, null, null, null);
			GameCanvas.currentDialog = this;
			this.time = mSystem.currentTimeMillis() + 5000L;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000EF3D7 File Offset: 0x000ED5D7
		public override void show()
		{
			GameCanvas.currentDialog = this;
			this.time = -1L;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000EF3E8 File Offset: 0x000ED5E8
		public void setInfo(string info)
		{
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
			this.h = 80;
			if (this.info.Length >= 5)
			{
				this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000EF448 File Offset: 0x000ED648
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

		// Token: 0x06000EDB RID: 3803 RVA: 0x000EF558 File Offset: 0x000ED758
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

		// Token: 0x06000EDC RID: 3804 RVA: 0x000EF63C File Offset: 0x000ED83C
		public override void update()
		{
			base.update();
			if (this.time != -1L && mSystem.currentTimeMillis() > this.time)
			{
				GameCanvas.endDlg();
			}
		}

		// Token: 0x04001F52 RID: 8018
		public string[] info;

		// Token: 0x04001F53 RID: 8019
		public bool isWait;

		// Token: 0x04001F54 RID: 8020
		private int h;

		// Token: 0x04001F55 RID: 8021
		private int padLeft;

		// Token: 0x04001F56 RID: 8022
		private long time = -1L;
	}
}
