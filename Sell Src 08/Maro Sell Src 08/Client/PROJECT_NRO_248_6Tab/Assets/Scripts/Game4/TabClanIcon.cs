using System;

namespace Game4
{
	// Token: 0x02000266 RID: 614
	public class TabClanIcon : IActionListener
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x001B3CB4 File Offset: 0x001B1EB4
		public TabClanIcon()
		{
			this.left = new Command(mResources.SELECT, this, 1, null);
			this.right = new Command(mResources.CLOSE, this, 2, null);
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x001B3D08 File Offset: 0x001B1F08
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

		// Token: 0x06001BB3 RID: 7091 RVA: 0x001B3EA8 File Offset: 0x001B20A8
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

		// Token: 0x06001BB4 RID: 7092 RVA: 0x001B3ED1 File Offset: 0x001B20D1
		public void hide()
		{
			this.cmtoX = this.x + this.w;
			SmallImage.clearHastable();
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x000034B9 File Offset: 0x000016B9
		public void paintPeans(mGraphics g)
		{
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x001B3EEC File Offset: 0x001B20EC
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

		// Token: 0x06001BB7 RID: 7095 RVA: 0x001B41D6 File Offset: 0x001B23D6
		public void paint(mGraphics g)
		{
			if (!this.isRequest)
			{
				this.paintIcon(g);
				return;
			}
			this.paintPeans(g);
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x001B41F0 File Offset: 0x001B23F0
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

		// Token: 0x06001BB9 RID: 7097 RVA: 0x001B42C4 File Offset: 0x001B24C4
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

		// Token: 0x06001BBA RID: 7098 RVA: 0x001B447C File Offset: 0x001B267C
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

		// Token: 0x040035B0 RID: 13744
		private int x;

		// Token: 0x040035B1 RID: 13745
		private int y;

		// Token: 0x040035B2 RID: 13746
		private int w;

		// Token: 0x040035B3 RID: 13747
		private int h;

		// Token: 0x040035B4 RID: 13748
		private Command left;

		// Token: 0x040035B5 RID: 13749
		private Command right;

		// Token: 0x040035B6 RID: 13750
		private Command center;

		// Token: 0x040035B7 RID: 13751
		private int WIDTH = 24;

		// Token: 0x040035B8 RID: 13752
		public int nItem;

		// Token: 0x040035B9 RID: 13753
		private int disStart = 50;

		// Token: 0x040035BA RID: 13754
		public static Scroll scrMain;

		// Token: 0x040035BB RID: 13755
		public int cmtoX;

		// Token: 0x040035BC RID: 13756
		public int cmx;

		// Token: 0x040035BD RID: 13757
		public int cmvx;

		// Token: 0x040035BE RID: 13758
		public int cmdx;

		// Token: 0x040035BF RID: 13759
		public bool isShow;

		// Token: 0x040035C0 RID: 13760
		public bool isGetName;

		// Token: 0x040035C1 RID: 13761
		public string text;

		// Token: 0x040035C2 RID: 13762
		private bool isRequest;

		// Token: 0x040035C3 RID: 13763
		private bool isUpdate;

		// Token: 0x040035C4 RID: 13764
		public MyVector vItems = new MyVector();

		// Token: 0x040035C5 RID: 13765
		private int msgID;

		// Token: 0x040035C6 RID: 13766
		private int select;

		// Token: 0x040035C7 RID: 13767
		private int lastSelect;

		// Token: 0x040035C8 RID: 13768
		private ScrollResult sr;
	}
}
