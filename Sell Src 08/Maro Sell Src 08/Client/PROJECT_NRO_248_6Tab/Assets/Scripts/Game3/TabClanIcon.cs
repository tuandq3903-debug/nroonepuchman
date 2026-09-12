using System;

namespace Game3
{
	// Token: 0x0200033E RID: 830
	public class TabClanIcon : IActionListener
	{
		// Token: 0x06002555 RID: 9557 RVA: 0x00248D58 File Offset: 0x00246F58
		public TabClanIcon()
		{
			this.left = new Command(mResources.SELECT, this, 1, null);
			this.right = new Command(mResources.CLOSE, this, 2, null);
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x00248DAC File Offset: 0x00246FAC
		public void init()
		{
			if (this.isGetName)
			{
				this.w = 170;
				this.h = 118;
				this.x = GameCanvas.w / 2 - this.w / 2;
				this.y = GameCanvas.h / 2 - this.h / 2;
			}
			else
			{
				this.w = 170;
				this.h = 170;
				this.x = GameCanvas.w / 2 - this.w / 2;
				this.y = GameCanvas.h / 2 - this.h / 2;
				if (GameCanvas.h < 240)
				{
					this.y -= 10;
				}
			}
			this.cmx = this.x;
			this.cmtoX = 0;
			if (!this.isRequest)
			{
				this.nItem = ClanImage.vClanImage.size();
			}
			else
			{
				this.nItem = this.vItems.size();
			}
			if (GameCanvas.isTouch)
			{
				this.left.x = this.x;
				this.left.y = this.y + this.h + 5;
				this.right.x = this.x + this.w - 68;
				this.right.y = this.y + this.h + 5;
			}
			TabClanIcon.scrMain = new Scroll();
			TabClanIcon.scrMain.setStyle(this.nItem, this.WIDTH, this.x, this.y + this.disStart, this.w, this.h - this.disStart, true, 1);
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00248F4C File Offset: 0x0024714C
		public void show(bool isGetName)
		{
			if (Char.myCharz().clan != null)
			{
				this.isUpdate = true;
			}
			this.isShow = true;
			this.isGetName = isGetName;
			this.init();
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00248F75 File Offset: 0x00247175
		public void hide()
		{
			this.cmtoX = this.x + this.w;
			SmallImage.clearHastable();
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000034B9 File Offset: 0x000016B9
		public void paintPeans(mGraphics g)
		{
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00248F90 File Offset: 0x00247190
		public void paintIcon(mGraphics g)
		{
			g.translate(-this.cmx, 0);
			PopUp.paintPopUp(g, this.x, this.y - 17, this.w, this.h + 17, -1, true);
			mFont.tahoma_7b_dark.drawString(g, mResources.select_clan_icon, this.x + this.w / 2, this.y - 7, 2);
			if (this.lastSelect >= 0 && this.lastSelect <= ClanImage.vClanImage.size() - 1)
			{
				ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect);
				if (clanImage.idImage != null)
				{
					Char.myCharz().paintBag(g, clanImage.idImage, GameCanvas.w / 2, this.y + 45, 1, false);
				}
			}
			Char.myCharz().paintCharBody(g, GameCanvas.w / 2, this.y + 45, 1, Char.myCharz().cf, false);
			g.setClip(this.x, this.y + this.disStart, this.w, this.h - this.disStart - 10);
			if (TabClanIcon.scrMain != null)
			{
				g.translate(0, -TabClanIcon.scrMain.cmy);
			}
			for (int i = 0; i < this.nItem; i++)
			{
				int num = this.x + 10;
				int num2 = this.y + i * this.WIDTH + this.disStart;
				if (num2 + this.WIDTH - ((TabClanIcon.scrMain != null) ? TabClanIcon.scrMain.cmy : 0) >= this.y + this.disStart && num2 - ((TabClanIcon.scrMain != null) ? TabClanIcon.scrMain.cmy : 0) <= this.y + this.disStart + this.h)
				{
					ClanImage clanImage2 = (ClanImage)ClanImage.vClanImage.elementAt(i);
					mFont mFont2 = mFont.tahoma_7_grey;
					if (i == this.lastSelect)
					{
						mFont2 = mFont.tahoma_7_blue;
					}
					if (clanImage2.name != null)
					{
						mFont2.drawString(g, clanImage2.name, num + 20, num2, 0);
					}
					if (clanImage2.xu > 0)
					{
						mFont2.drawString(g, clanImage2.xu.ToString() + " " + mResources.XU, num + this.w - 20, num2, mFont.RIGHT);
					}
					else if (clanImage2.luong > 0)
					{
						mFont2.drawString(g, clanImage2.luong.ToString() + " " + mResources.LUONG, num + this.w - 20, num2, mFont.RIGHT);
					}
					if (clanImage2.idImage != null)
					{
						SmallImage.drawSmallImage(g, clanImage2.idImage[0], num, num2, 0, 0);
					}
				}
			}
			g.translate(0, -g.getTranslateY());
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			GameCanvas.paintz.paintCmdBar(g, this.left, this.center, this.right);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x0024927A File Offset: 0x0024747A
		public void paint(mGraphics g)
		{
			if (!this.isRequest)
			{
				this.paintIcon(g);
				return;
			}
			this.paintPeans(g);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00249294 File Offset: 0x00247494
		public void update()
		{
			if (TabClanIcon.scrMain != null)
			{
				TabClanIcon.scrMain.updatecm();
			}
			if (this.cmx != this.cmtoX)
			{
				this.cmvx = this.cmtoX - this.cmx << 2;
				this.cmdx += this.cmvx;
				this.cmx += this.cmdx >> 3;
				this.cmdx &= 15;
			}
			if (Math.abs(this.cmtoX - this.cmx) < 10)
			{
				this.cmx = this.cmtoX;
			}
			if (this.cmx >= this.x + this.w - 10 && this.cmtoX >= this.x + this.w - 10)
			{
				this.isShow = false;
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00249368 File Offset: 0x00247568
		public void updateKey()
		{
			if (this.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.left)))
			{
				this.left.performAction();
			}
			if (this.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.right)))
			{
				this.right.performAction();
			}
			if (this.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.center)))
			{
				this.center.performAction();
			}
			if (!this.isGetName)
			{
				if (TabClanIcon.scrMain == null)
				{
					return;
				}
				if (GameCanvas.isTouch)
				{
					TabClanIcon.scrMain.updateKey();
					this.select = TabClanIcon.scrMain.selectedItem;
				}
				if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = false;
					this.select--;
					if (this.select < 0)
					{
						this.select = this.nItem - 1;
					}
					TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
				}
				if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
					this.select++;
					if (this.select > this.nItem - 1)
					{
						this.select = 0;
					}
					TabClanIcon.scrMain.moveTo(this.select * TabClanIcon.scrMain.ITEM_SIZE);
				}
				if (this.select != -1)
				{
					this.lastSelect = this.select;
				}
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00249520 File Offset: 0x00247720
		public void perform(int idAction, object p)
		{
			if (idAction == 2)
			{
				this.hide();
			}
			if (idAction != 1 || this.isGetName)
			{
				return;
			}
			if (!this.isRequest)
			{
				if (this.lastSelect >= 0)
				{
					this.hide();
					if (Char.myCharz().clan == null)
					{
						Service.gI().getClan(2, (sbyte)((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, this.text);
						return;
					}
					Service.gI().getClan(4, (sbyte)((ClanImage)ClanImage.vClanImage.elementAt(this.lastSelect)).ID, string.Empty);
					return;
				}
			}
			else if (this.lastSelect >= 0)
			{
				Item item = (Item)this.vItems.elementAt(this.select);
			}
		}

		// Token: 0x0400482F RID: 18479
		private int x;

		// Token: 0x04004830 RID: 18480
		private int y;

		// Token: 0x04004831 RID: 18481
		private int w;

		// Token: 0x04004832 RID: 18482
		private int h;

		// Token: 0x04004833 RID: 18483
		private Command left;

		// Token: 0x04004834 RID: 18484
		private Command right;

		// Token: 0x04004835 RID: 18485
		private Command center;

		// Token: 0x04004836 RID: 18486
		private int WIDTH = 24;

		// Token: 0x04004837 RID: 18487
		public int nItem;

		// Token: 0x04004838 RID: 18488
		private int disStart = 50;

		// Token: 0x04004839 RID: 18489
		public static Scroll scrMain;

		// Token: 0x0400483A RID: 18490
		public int cmtoX;

		// Token: 0x0400483B RID: 18491
		public int cmx;

		// Token: 0x0400483C RID: 18492
		public int cmvx;

		// Token: 0x0400483D RID: 18493
		public int cmdx;

		// Token: 0x0400483E RID: 18494
		public bool isShow;

		// Token: 0x0400483F RID: 18495
		public bool isGetName;

		// Token: 0x04004840 RID: 18496
		public string text;

		// Token: 0x04004841 RID: 18497
		private bool isRequest;

		// Token: 0x04004842 RID: 18498
		private bool isUpdate;

		// Token: 0x04004843 RID: 18499
		public MyVector vItems = new MyVector();

		// Token: 0x04004844 RID: 18500
		private int msgID;

		// Token: 0x04004845 RID: 18501
		private int select;

		// Token: 0x04004846 RID: 18502
		private int lastSelect;

		// Token: 0x04004847 RID: 18503
		private ScrollResult sr;
	}
}
