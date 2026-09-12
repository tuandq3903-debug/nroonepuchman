using System;

namespace Game2
{
	// Token: 0x020003DA RID: 986
	public class MsgDlg : Dialog
	{
		// Token: 0x06002BC2 RID: 11202 RVA: 0x002AE55B File Offset: 0x002AC75B
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

		// Token: 0x06002BC3 RID: 11203 RVA: 0x002AE59B File Offset: 0x002AC79B
		public void pleasewait()
		{
			this.setInfo(mResources.PLEASEWAIT, null, null, null);
			GameCanvas.currentDialog = this;
			this.time = mSystem.currentTimeMillis() + 5000L;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x002AE5C3 File Offset: 0x002AC7C3
		public override void show()
		{
			GameCanvas.currentDialog = this;
			this.time = -1L;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x002AE5D4 File Offset: 0x002AC7D4
		public void setInfo(string info)
		{
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - (this.padLeft * 2 + 20));
			this.h = 80;
			if (this.info.Length >= 5)
			{
				this.h = this.info.Length * mFont.tahoma_8b.getHeight() + 20;
			}
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x002AE634 File Offset: 0x002AC834
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

		// Token: 0x06002BC7 RID: 11207 RVA: 0x002AE744 File Offset: 0x002AC944
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

		// Token: 0x06002BC8 RID: 11208 RVA: 0x002AE828 File Offset: 0x002ACA28
		public override void update()
		{
			base.update();
			if (this.time != -1L && mSystem.currentTimeMillis() > this.time)
			{
				GameCanvas.endDlg();
			}
		}

		// Token: 0x040056CF RID: 22223
		public string[] info;

		// Token: 0x040056D0 RID: 22224
		public bool isWait;

		// Token: 0x040056D1 RID: 22225
		private int h;

		// Token: 0x040056D2 RID: 22226
		private int padLeft;

		// Token: 0x040056D3 RID: 22227
		private long time = -1L;
	}
}
