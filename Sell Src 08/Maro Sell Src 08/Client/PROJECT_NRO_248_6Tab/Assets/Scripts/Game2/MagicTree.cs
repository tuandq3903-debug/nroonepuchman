using System;

namespace Game2
{
	// Token: 0x020003C2 RID: 962
	public class MagicTree : Npc, IActionListener
	{
		// Token: 0x06002A82 RID: 10882 RVA: 0x0029D6E8 File Offset: 0x0029B8E8
		public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId) : base(npcId, status, cx, cy, templateId, iconId)
		{
			this.p = new PopUp(string.Empty, 0, 0);
			this.p.command = new Command(null, this, 1, null);
			PopUp.addPopUp(this.p);
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x0029D738 File Offset: 0x0029B938
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

		// Token: 0x06002A84 RID: 10884 RVA: 0x0029D984 File Offset: 0x0029BB84
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

		// Token: 0x06002A85 RID: 10885 RVA: 0x0029DCB3 File Offset: 0x0029BEB3
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				Service.gI().magicTree(1);
			}
		}

		// Token: 0x0400528D RID: 21133
		public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

		// Token: 0x0400528E RID: 21134
		public int id;

		// Token: 0x0400528F RID: 21135
		public int level;

		// Token: 0x04005290 RID: 21136
		public int x;

		// Token: 0x04005291 RID: 21137
		public int y;

		// Token: 0x04005292 RID: 21138
		public int currPeas;

		// Token: 0x04005293 RID: 21139
		public int remainPeas;

		// Token: 0x04005294 RID: 21140
		public int maxPeas;

		// Token: 0x04005295 RID: 21141
		public new string strInfo;

		// Token: 0x04005296 RID: 21142
		public string name;

		// Token: 0x04005297 RID: 21143
		public int timeToRecieve;

		// Token: 0x04005298 RID: 21144
		public bool isUpdate;

		// Token: 0x04005299 RID: 21145
		public int[] peaPostionX;

		// Token: 0x0400529A RID: 21146
		public int[] peaPostionY;

		// Token: 0x0400529B RID: 21147
		private int num;

		// Token: 0x0400529C RID: 21148
		public PopUp p;

		// Token: 0x0400529D RID: 21149
		public bool isUpdateTree;

		// Token: 0x0400529E RID: 21150
		public new static bool isPaint = true;

		// Token: 0x0400529F RID: 21151
		public bool isPeasEffect;

		// Token: 0x040052A0 RID: 21152
		public new int seconds;

		// Token: 0x040052A1 RID: 21153
		public new long last;

		// Token: 0x040052A2 RID: 21154
		public new long cur;

		// Token: 0x040052A3 RID: 21155
		private bool waitToUpdate;

		// Token: 0x040052A4 RID: 21156
		private int delay;
	}
}
