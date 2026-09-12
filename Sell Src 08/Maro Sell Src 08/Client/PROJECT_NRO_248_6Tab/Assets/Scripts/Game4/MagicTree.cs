using System;

namespace Game4
{
	// Token: 0x02000212 RID: 530
	public class MagicTree : Npc, IActionListener
	{
		// Token: 0x0600173A RID: 5946 RVA: 0x001735A0 File Offset: 0x001717A0
		public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId) : base(npcId, status, cx, cy, templateId, iconId)
		{
			this.p = new PopUp(string.Empty, 0, 0);
			this.p.command = new Command(null, this, 1, null);
			PopUp.addPopUp(this.p);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x001735F0 File Offset: 0x001717F0
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

		// Token: 0x0600173C RID: 5948 RVA: 0x0017383C File Offset: 0x00171A3C
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

		// Token: 0x0600173D RID: 5949 RVA: 0x00173B6B File Offset: 0x00171D6B
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				Service.gI().magicTree(1);
			}
		}

		// Token: 0x04002D8F RID: 11663
		public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

		// Token: 0x04002D90 RID: 11664
		public int id;

		// Token: 0x04002D91 RID: 11665
		public int level;

		// Token: 0x04002D92 RID: 11666
		public int x;

		// Token: 0x04002D93 RID: 11667
		public int y;

		// Token: 0x04002D94 RID: 11668
		public int currPeas;

		// Token: 0x04002D95 RID: 11669
		public int remainPeas;

		// Token: 0x04002D96 RID: 11670
		public int maxPeas;

		// Token: 0x04002D97 RID: 11671
		public new string strInfo;

		// Token: 0x04002D98 RID: 11672
		public string name;

		// Token: 0x04002D99 RID: 11673
		public int timeToRecieve;

		// Token: 0x04002D9A RID: 11674
		public bool isUpdate;

		// Token: 0x04002D9B RID: 11675
		public int[] peaPostionX;

		// Token: 0x04002D9C RID: 11676
		public int[] peaPostionY;

		// Token: 0x04002D9D RID: 11677
		private int num;

		// Token: 0x04002D9E RID: 11678
		public PopUp p;

		// Token: 0x04002D9F RID: 11679
		public bool isUpdateTree;

		// Token: 0x04002DA0 RID: 11680
		public new static bool isPaint = true;

		// Token: 0x04002DA1 RID: 11681
		public bool isPeasEffect;

		// Token: 0x04002DA2 RID: 11682
		public new int seconds;

		// Token: 0x04002DA3 RID: 11683
		public new long last;

		// Token: 0x04002DA4 RID: 11684
		public new long cur;

		// Token: 0x04002DA5 RID: 11685
		private bool waitToUpdate;

		// Token: 0x04002DA6 RID: 11686
		private int delay;
	}
}
