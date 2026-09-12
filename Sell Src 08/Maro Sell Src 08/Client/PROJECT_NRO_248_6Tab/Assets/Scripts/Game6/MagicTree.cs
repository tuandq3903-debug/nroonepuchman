using System;

namespace Game6
{
	// Token: 0x02000062 RID: 98
	public class MagicTree : Npc, IActionListener
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x00049354 File Offset: 0x00047554
		public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId) : base(npcId, status, cx, cy, templateId, iconId)
		{
			this.p = new PopUp(string.Empty, 0, 0);
			this.p.command = new Command(null, this, 1, null);
			PopUp.addPopUp(this.p);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000493A4 File Offset: 0x000475A4
		public override void paint(mGraphics g)
		{
			if (this.id == 0)
			{
				return;
			}
			SmallImage.drawSmallImage(g, this.id, this.cx, this.cy, 0, StaticObj.BOTTOM_HCENTER);
			if (Char.myCharz().npcFocus != null && Char.myCharz().npcFocus.Equals(this))
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 1, mGraphics.BOTTOM | mGraphics.HCENTER);
				if (this.name != null)
				{
					mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 20, mFont.CENTER, mFont.tahoma_7_grey);
				}
			}
			else if (this.name != null)
			{
				mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 17, mFont.CENTER, mFont.tahoma_7_grey);
			}
			try
			{
				for (int i = 0; i < this.currPeas; i++)
				{
					g.drawImage(MagicTree.pea, this.cx + this.peaPostionX[i] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[i] - SmallImage.smallImg[this.id][4], 0);
				}
			}
			catch (Exception)
			{
			}
			if (this.indexEffTask < 0 || this.effTask == null || this.cTypePk != 0)
			{
				return;
			}
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx + SmallImage.smallImg[this.id][3] / 2 + 5, this.cy - 15 + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			if (GameCanvas.gameTick % 2 == 0)
			{
				this.indexEffTask++;
				if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
				{
					this.indexEffTask = 0;
				}
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000495F0 File Offset: 0x000477F0
		public override void update()
		{
			this.p.isPaint = MagicTree.isPaint;
			this.cur = mSystem.currentTimeMillis();
			if (this.cur - this.last >= 1000L)
			{
				this.seconds--;
				this.last = this.cur;
				if (this.seconds < 0)
				{
					this.seconds = 0;
				}
			}
			if (!this.isUpdate)
			{
				if (this.currPeas < this.maxPeas && this.seconds == 0)
				{
					this.waitToUpdate = true;
				}
			}
			else if (this.seconds == 0)
			{
				this.isUpdate = false;
				this.waitToUpdate = true;
			}
			if (this.waitToUpdate)
			{
				this.delay++;
				if (this.delay == 20)
				{
					this.delay = 0;
					this.waitToUpdate = false;
					Service.gI().getMagicTree(2);
				}
			}
			this.num = ((this.peaPostionX != null) ? (this.peaPostionX.Length * this.currPeas / this.maxPeas) : 0);
			if (this.isUpdateTree)
			{
				this.isUpdateTree = false;
				if ((this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate) || this.isPeasEffect)
				{
					this.p.updateXYWH(new string[]
					{
						this.isUpdate ? mResources.UPGRADING : (this.currPeas.ToString() + "/" + this.maxPeas.ToString()),
						NinjaUtil.getTime(this.seconds)
					}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
				}
				else if (this.currPeas == this.maxPeas && !this.isUpdate)
				{
					this.p.updateXYWH(new string[]
					{
						mResources.can_harvest,
						this.currPeas.ToString() + "/" + this.maxPeas.ToString()
					}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
				}
			}
			if ((this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate))
			{
				this.p.says[this.p.says.Length - 1] = NinjaUtil.getTime(this.seconds);
			}
			if (this.isPeasEffect)
			{
				this.p.isPaint = false;
				ServerEffect.addServerEffect(98, this.cx + this.peaPostionX[this.currPeas - 1] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[this.currPeas - 1] - SmallImage.smallImg[this.id][4], 1);
				this.currPeas--;
				if (GameCanvas.gameTick % 2 == 0)
				{
					SoundMn.gI().HP_MPup();
				}
				if (this.currPeas == this.remainPeas)
				{
					this.p.isPaint = true;
					this.isUpdateTree = true;
					this.isPeasEffect = false;
				}
			}
			base.update();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0004991F File Offset: 0x00047B1F
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				Service.gI().magicTree(1);
			}
		}

		// Token: 0x04000891 RID: 2193
		public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

		// Token: 0x04000892 RID: 2194
		public int id;

		// Token: 0x04000893 RID: 2195
		public int level;

		// Token: 0x04000894 RID: 2196
		public int x;

		// Token: 0x04000895 RID: 2197
		public int y;

		// Token: 0x04000896 RID: 2198
		public int currPeas;

		// Token: 0x04000897 RID: 2199
		public int remainPeas;

		// Token: 0x04000898 RID: 2200
		public int maxPeas;

		// Token: 0x04000899 RID: 2201
		public new string strInfo;

		// Token: 0x0400089A RID: 2202
		public string name;

		// Token: 0x0400089B RID: 2203
		public int timeToRecieve;

		// Token: 0x0400089C RID: 2204
		public bool isUpdate;

		// Token: 0x0400089D RID: 2205
		public int[] peaPostionX;

		// Token: 0x0400089E RID: 2206
		public int[] peaPostionY;

		// Token: 0x0400089F RID: 2207
		private int num;

		// Token: 0x040008A0 RID: 2208
		public PopUp p;

		// Token: 0x040008A1 RID: 2209
		public bool isUpdateTree;

		// Token: 0x040008A2 RID: 2210
		public new static bool isPaint = true;

		// Token: 0x040008A3 RID: 2211
		public bool isPeasEffect;

		// Token: 0x040008A4 RID: 2212
		public new int seconds;

		// Token: 0x040008A5 RID: 2213
		public new long last;

		// Token: 0x040008A6 RID: 2214
		public new long cur;

		// Token: 0x040008A7 RID: 2215
		private bool waitToUpdate;

		// Token: 0x040008A8 RID: 2216
		private int delay;
	}
}
