using System;

namespace Game2
{
	// Token: 0x020003AE RID: 942
	public class Info_RadaScr
	{
		// Token: 0x06002A27 RID: 10791 RVA: 0x0029A028 File Offset: 0x00298228
		public void SetInfo(int id, int no, int idIcon, sbyte rank, sbyte typeMonster, short templateId, string name, string info, Char charInfo, ItemOption[] itemOption)
		{
			this.id = id;
			this.no = no;
			this.idIcon = idIcon;
			this.rank = rank;
			this.typeMonster = typeMonster;
			if (templateId != -1)
			{
				this.mobInfo = new Mob();
				this.mobInfo.templateId = (int)templateId;
			}
			this.name = name;
			this.info = info;
			this.charInfo = charInfo;
			this.itemOption = itemOption;
			this.addItemDetail();
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x0029A09D File Offset: 0x0029829D
		public void SetAmount(sbyte amount, sbyte max_amount)
		{
			this.amount = amount;
			this.max_amount = max_amount;
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x0029A0AD File Offset: 0x002982AD
		public void SetLevel(sbyte level)
		{
			this.level = level;
			this.addItemDetail();
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x0029A0BC File Offset: 0x002982BC
		public void SetUse(sbyte isUse)
		{
			this.isUse = isUse;
			this.addItemDetail();
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x0029A0CB File Offset: 0x002982CB
		public static Char SetCharInfo(int head, int body, int leg, int bag)
		{
			return new Char
			{
				head = head,
				body = body,
				leg = leg,
				bag = bag
			};
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x0029A0F0 File Offset: 0x002982F0
		public static Info_RadaScr GetInfo(MyVector vec, int id)
		{
			if (vec != null)
			{
				for (int i = 0; i < vec.size(); i++)
				{
					Info_RadaScr info_RadaScr = (Info_RadaScr)vec.elementAt(i);
					if (info_RadaScr != null && info_RadaScr.id == id)
					{
						return info_RadaScr;
					}
				}
			}
			return null;
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x0029A130 File Offset: 0x00298330
		public void paintInfo(mGraphics g, int x, int y)
		{
			this.count++;
			if (this.count > this.f.Length - 1)
			{
				this.count = 0;
			}
			if (this.typeMonster == 0)
			{
				if (Mob.arrMobTemplate[this.mobInfo.templateId] != null)
				{
					if (Mob.arrMobTemplate[this.mobInfo.templateId].data != null)
					{
						Mob.arrMobTemplate[this.mobInfo.templateId].data.paintFrame(g, this.f[this.count], x, y, 0, 0);
						return;
					}
					if (this.timeRequest - GameCanvas.timeNow < 0L)
					{
						this.timeRequest = GameCanvas.timeNow + 1500L;
						this.mobInfo.getData();
						return;
					}
				}
			}
			else if (this.charInfo != null)
			{
				this.charInfo.paintCharBody(g, x, y, 1, this.f[this.count], true);
			}
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x0029A220 File Offset: 0x00298420
		public void addItemDetail()
		{
			this.cp = new ChatPopup();
			string empty = string.Empty;
			string empty2 = string.Empty;
			empty2 = empty2 + "\n|6|" + this.info;
			empty2 += "\n--";
			if (this.itemOption != null)
			{
				int num = 0;
				bool flag = true;
				while (flag)
				{
					int num2 = 0;
					for (int i = 0; i < this.itemOption.Length; i++)
					{
						empty = this.itemOption[i].getOptionString();
						if (!empty.Equals(string.Empty) && num == (int)this.itemOption[i].activeCard)
						{
							num2++;
							break;
						}
					}
					if (num2 == 0)
					{
						break;
					}
					if (num == 0)
					{
						empty2 = empty2 + "\n|6|2|--" + mResources.unlock + "--";
					}
					else
					{
						string text = empty2;
						empty2 = string.Concat(new string[]
						{
							text,
							"\n|6|2|--",
							mResources.equip,
							" Lv.",
							num.ToString(),
							"--"
						});
					}
					for (int j = 0; j < this.itemOption.Length; j++)
					{
						empty = this.itemOption[j].getOptionString();
						if (!empty.Equals(string.Empty) && num == (int)this.itemOption[j].activeCard)
						{
							string text2 = "1";
							if (this.level == 0)
							{
								text2 = "2";
							}
							else if (this.itemOption[j].activeCard != 0)
							{
								if (this.isUse == 0)
								{
									text2 = "2";
								}
								else if (this.level < this.itemOption[j].activeCard)
								{
									text2 = "2";
								}
							}
							string text3 = empty2;
							empty2 = string.Concat(new string[]
							{
								text3,
								"\n|",
								text2,
								"|1|",
								empty
							});
						}
					}
					if (num2 != 0)
					{
						num++;
					}
				}
			}
			this.popUpDetailInit(this.cp, empty2);
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x0029A418 File Offset: 0x00298618
		public void popUpDetailInit(ChatPopup cp, string chat)
		{
			cp.sayWidth = RadarScr.wText;
			cp.cx = RadarScr.xText;
			cp.says = mFont.tahoma_7.splitFontArray(chat, cp.sayWidth - 8);
			cp.delay = 10000000;
			cp.c = null;
			cp.ch = cp.says.Length * 12;
			cp.cy = RadarScr.yText;
			cp.strY = 10;
			cp.lim = cp.ch - RadarScr.hText;
			if (cp.lim < 0)
			{
				cp.lim = 0;
			}
		}

		// Token: 0x06002A30 RID: 10800 RVA: 0x0029A4AC File Offset: 0x002986AC
		public void SetEff()
		{
			if (this.amount == this.max_amount && this.eff.size() == 0)
			{
				int num = Res.random(1, 5);
				for (int i = 0; i < num; i++)
				{
					Position position = new Position();
					position.x = Res.random(5, 25);
					position.y = Res.random(5, 25);
					position.v = i * Res.random(0, 8);
					position.w = 0;
					position.anchor = -1;
					this.eff.addElement(position);
				}
			}
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x0029A534 File Offset: 0x00298734
		public void paintEff(mGraphics g, int x, int y)
		{
			this.SetEff();
			for (int i = 0; i < this.eff.size(); i++)
			{
				Position position = (Position)this.eff.elementAt(i);
				if (position != null)
				{
					if (position.w < position.v)
					{
						position.w++;
					}
					if (position.w >= position.v)
					{
						position.anchor = GameCanvas.gameTick / 3 % (RadarScr.fraEff.nFrame + 1);
						if (position.anchor >= RadarScr.fraEff.nFrame)
						{
							this.eff.removeElementAt(i);
							i--;
						}
						else
						{
							RadarScr.fraEff.drawFrame(position.anchor, x + position.x, y + position.y, 0, 3, g);
						}
					}
				}
			}
		}

		// Token: 0x04005157 RID: 20823
		public sbyte rank;

		// Token: 0x04005158 RID: 20824
		public sbyte amount;

		// Token: 0x04005159 RID: 20825
		public sbyte max_amount;

		// Token: 0x0400515A RID: 20826
		public sbyte typeMonster;

		// Token: 0x0400515B RID: 20827
		public int id;

		// Token: 0x0400515C RID: 20828
		public int no;

		// Token: 0x0400515D RID: 20829
		public int idIcon;

		// Token: 0x0400515E RID: 20830
		public string name;

		// Token: 0x0400515F RID: 20831
		public string info;

		// Token: 0x04005160 RID: 20832
		public sbyte level;

		// Token: 0x04005161 RID: 20833
		public sbyte isUse;

		// Token: 0x04005162 RID: 20834
		public Char charInfo;

		// Token: 0x04005163 RID: 20835
		public Mob mobInfo;

		// Token: 0x04005164 RID: 20836
		public ItemOption[] itemOption;

		// Token: 0x04005165 RID: 20837
		private int[] f = new int[]
		{
			0,
			0,
			0,
			0,
			0,
			1,
			1,
			1,
			1,
			1
		};

		// Token: 0x04005166 RID: 20838
		private int count;

		// Token: 0x04005167 RID: 20839
		private long timeRequest;

		// Token: 0x04005168 RID: 20840
		public ChatPopup cp;

		// Token: 0x04005169 RID: 20841
		public MyVector eff = new MyVector(string.Empty);
	}
}
