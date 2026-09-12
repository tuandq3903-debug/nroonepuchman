using System;

namespace Game6
{
	// Token: 0x0200004E RID: 78
	public class Info_RadaScr
	{
		// Token: 0x06000397 RID: 919 RVA: 0x00045C90 File Offset: 0x00043E90
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

		// Token: 0x06000398 RID: 920 RVA: 0x00045D05 File Offset: 0x00043F05
		public void SetAmount(sbyte amount, sbyte max_amount)
		{
			this.amount = amount;
			this.max_amount = max_amount;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00045D15 File Offset: 0x00043F15
		public void SetLevel(sbyte level)
		{
			this.level = level;
			this.addItemDetail();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00045D24 File Offset: 0x00043F24
		public void SetUse(sbyte isUse)
		{
			this.isUse = isUse;
			this.addItemDetail();
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00045D33 File Offset: 0x00043F33
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

		// Token: 0x0600039C RID: 924 RVA: 0x00045D58 File Offset: 0x00043F58
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

		// Token: 0x0600039D RID: 925 RVA: 0x00045D98 File Offset: 0x00043F98
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

		// Token: 0x0600039E RID: 926 RVA: 0x00045E88 File Offset: 0x00044088
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

		// Token: 0x0600039F RID: 927 RVA: 0x00046080 File Offset: 0x00044280
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

		// Token: 0x060003A0 RID: 928 RVA: 0x00046114 File Offset: 0x00044314
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

		// Token: 0x060003A1 RID: 929 RVA: 0x0004619C File Offset: 0x0004439C
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

		// Token: 0x0400075B RID: 1883
		public sbyte rank;

		// Token: 0x0400075C RID: 1884
		public sbyte amount;

		// Token: 0x0400075D RID: 1885
		public sbyte max_amount;

		// Token: 0x0400075E RID: 1886
		public sbyte typeMonster;

		// Token: 0x0400075F RID: 1887
		public int id;

		// Token: 0x04000760 RID: 1888
		public int no;

		// Token: 0x04000761 RID: 1889
		public int idIcon;

		// Token: 0x04000762 RID: 1890
		public string name;

		// Token: 0x04000763 RID: 1891
		public string info;

		// Token: 0x04000764 RID: 1892
		public sbyte level;

		// Token: 0x04000765 RID: 1893
		public sbyte isUse;

		// Token: 0x04000766 RID: 1894
		public Char charInfo;

		// Token: 0x04000767 RID: 1895
		public Mob mobInfo;

		// Token: 0x04000768 RID: 1896
		public ItemOption[] itemOption;

		// Token: 0x04000769 RID: 1897
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

		// Token: 0x0400076A RID: 1898
		private int count;

		// Token: 0x0400076B RID: 1899
		private long timeRequest;

		// Token: 0x0400076C RID: 1900
		public ChatPopup cp;

		// Token: 0x0400076D RID: 1901
		public MyVector eff = new MyVector(string.Empty);
	}
}
