using System;

namespace Game1
{
	// Token: 0x0200049A RID: 1178
	public class MagicTree : Npc, IActionListener
	{
		// Token: 0x06003426 RID: 13350 RVA: 0x0033278C File Offset: 0x0033098C
		public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId) : base(npcId, status, cx, cy, templateId, iconId)
		{
			this.p = new PopUp(string.Empty, 0, 0);
			this.p.command = new Command(null, this, 1, null);
			PopUp.addPopUp(this.p);
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x003327DC File Offset: 0x003309DC
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

		// Token: 0x06003428 RID: 13352 RVA: 0x00332A28 File Offset: 0x00330C28
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

		// Token: 0x06003429 RID: 13353 RVA: 0x00332D57 File Offset: 0x00330F57
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				Service.gI().magicTree(1);
			}
		}

		// Token: 0x0400650C RID: 25868
		public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

		// Token: 0x0400650D RID: 25869
		public int id;

		// Token: 0x0400650E RID: 25870
		public int level;

		// Token: 0x0400650F RID: 25871
		public int x;

		// Token: 0x04006510 RID: 25872
		public int y;

		// Token: 0x04006511 RID: 25873
		public int currPeas;

		// Token: 0x04006512 RID: 25874
		public int remainPeas;

		// Token: 0x04006513 RID: 25875
		public int maxPeas;

		// Token: 0x04006514 RID: 25876
		public new string strInfo;

		// Token: 0x04006515 RID: 25877
		public string name;

		// Token: 0x04006516 RID: 25878
		public int timeToRecieve;

		// Token: 0x04006517 RID: 25879
		public bool isUpdate;

		// Token: 0x04006518 RID: 25880
		public int[] peaPostionX;

		// Token: 0x04006519 RID: 25881
		public int[] peaPostionY;

		// Token: 0x0400651A RID: 25882
		private int num;

		// Token: 0x0400651B RID: 25883
		public PopUp p;

		// Token: 0x0400651C RID: 25884
		public bool isUpdateTree;

		// Token: 0x0400651D RID: 25885
		public new static bool isPaint = true;

		// Token: 0x0400651E RID: 25886
		public bool isPeasEffect;

		// Token: 0x0400651F RID: 25887
		public new int seconds;

		// Token: 0x04006520 RID: 25888
		public new long last;

		// Token: 0x04006521 RID: 25889
		public new long cur;

		// Token: 0x04006522 RID: 25890
		private bool waitToUpdate;

		// Token: 0x04006523 RID: 25891
		private int delay;
	}
}
