using System;

namespace Game3
{
	// Token: 0x020002EA RID: 746
	public class MagicTree : Npc, IActionListener
	{
		// Token: 0x060020DE RID: 8414 RVA: 0x00208644 File Offset: 0x00206844
		public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId) : base(npcId, status, cx, cy, templateId, iconId)
		{
			this.p = new PopUp(string.Empty, 0, 0);
			this.p.command = new Command(null, this, 1, null);
			PopUp.addPopUp(this.p);
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00208694 File Offset: 0x00206894
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

		// Token: 0x060020E0 RID: 8416 RVA: 0x002088E0 File Offset: 0x00206AE0
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

		// Token: 0x060020E1 RID: 8417 RVA: 0x00208C0F File Offset: 0x00206E0F
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				Service.gI().magicTree(1);
			}
		}

		// Token: 0x0400400E RID: 16398
		public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

		// Token: 0x0400400F RID: 16399
		public int id;

		// Token: 0x04004010 RID: 16400
		public int level;

		// Token: 0x04004011 RID: 16401
		public int x;

		// Token: 0x04004012 RID: 16402
		public int y;

		// Token: 0x04004013 RID: 16403
		public int currPeas;

		// Token: 0x04004014 RID: 16404
		public int remainPeas;

		// Token: 0x04004015 RID: 16405
		public int maxPeas;

		// Token: 0x04004016 RID: 16406
		public new string strInfo;

		// Token: 0x04004017 RID: 16407
		public string name;

		// Token: 0x04004018 RID: 16408
		public int timeToRecieve;

		// Token: 0x04004019 RID: 16409
		public bool isUpdate;

		// Token: 0x0400401A RID: 16410
		public int[] peaPostionX;

		// Token: 0x0400401B RID: 16411
		public int[] peaPostionY;

		// Token: 0x0400401C RID: 16412
		private int num;

		// Token: 0x0400401D RID: 16413
		public PopUp p;

		// Token: 0x0400401E RID: 16414
		public bool isUpdateTree;

		// Token: 0x0400401F RID: 16415
		public new static bool isPaint = true;

		// Token: 0x04004020 RID: 16416
		public bool isPeasEffect;

		// Token: 0x04004021 RID: 16417
		public new int seconds;

		// Token: 0x04004022 RID: 16418
		public new long last;

		// Token: 0x04004023 RID: 16419
		public new long cur;

		// Token: 0x04004024 RID: 16420
		private bool waitToUpdate;

		// Token: 0x04004025 RID: 16421
		private int delay;
	}
}
