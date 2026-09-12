using System;

namespace Game1
{
	// Token: 0x02000486 RID: 1158
	public class Info_RadaScr
	{
		// Token: 0x060033CB RID: 13259 RVA: 0x0032F0CC File Offset: 0x0032D2CC
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

		// Token: 0x060033CC RID: 13260 RVA: 0x0032F141 File Offset: 0x0032D341
		public void SetAmount(sbyte amount, sbyte max_amount)
		{
			this.amount = amount;
			this.max_amount = max_amount;
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x0032F151 File Offset: 0x0032D351
		public void SetLevel(sbyte level)
		{
			this.level = level;
			this.addItemDetail();
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x0032F160 File Offset: 0x0032D360
		public void SetUse(sbyte isUse)
		{
			this.isUse = isUse;
			this.addItemDetail();
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x0032F16F File Offset: 0x0032D36F
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

		// Token: 0x060033D0 RID: 13264 RVA: 0x0032F194 File Offset: 0x0032D394
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

		// Token: 0x060033D1 RID: 13265 RVA: 0x0032F1D4 File Offset: 0x0032D3D4
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

		// Token: 0x060033D2 RID: 13266 RVA: 0x0032F2C4 File Offset: 0x0032D4C4
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

		// Token: 0x060033D3 RID: 13267 RVA: 0x0032F4BC File Offset: 0x0032D6BC
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

		// Token: 0x060033D4 RID: 13268 RVA: 0x0032F550 File Offset: 0x0032D750
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

		// Token: 0x060033D5 RID: 13269 RVA: 0x0032F5D8 File Offset: 0x0032D7D8
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

		// Token: 0x040063D6 RID: 25558
		public sbyte rank;

		// Token: 0x040063D7 RID: 25559
		public sbyte amount;

		// Token: 0x040063D8 RID: 25560
		public sbyte max_amount;

		// Token: 0x040063D9 RID: 25561
		public sbyte typeMonster;

		// Token: 0x040063DA RID: 25562
		public int id;

		// Token: 0x040063DB RID: 25563
		public int no;

		// Token: 0x040063DC RID: 25564
		public int idIcon;

		// Token: 0x040063DD RID: 25565
		public string name;

		// Token: 0x040063DE RID: 25566
		public string info;

		// Token: 0x040063DF RID: 25567
		public sbyte level;

		// Token: 0x040063E0 RID: 25568
		public sbyte isUse;

		// Token: 0x040063E1 RID: 25569
		public Char charInfo;

		// Token: 0x040063E2 RID: 25570
		public Mob mobInfo;

		// Token: 0x040063E3 RID: 25571
		public ItemOption[] itemOption;

		// Token: 0x040063E4 RID: 25572
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

		// Token: 0x040063E5 RID: 25573
		private int count;

		// Token: 0x040063E6 RID: 25574
		private long timeRequest;

		// Token: 0x040063E7 RID: 25575
		public ChatPopup cp;

		// Token: 0x040063E8 RID: 25576
		public MyVector eff = new MyVector(string.Empty);
	}
}
