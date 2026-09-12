using System;

namespace Game4
{
	// Token: 0x020001FE RID: 510
	public class Info_RadaScr
	{
		// Token: 0x060016DF RID: 5855 RVA: 0x0016FEE0 File Offset: 0x0016E0E0
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

		// Token: 0x060016E0 RID: 5856 RVA: 0x0016FF55 File Offset: 0x0016E155
		public void SetAmount(sbyte amount, sbyte max_amount)
		{
			this.amount = amount;
			this.max_amount = max_amount;
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0016FF65 File Offset: 0x0016E165
		public void SetLevel(sbyte level)
		{
			this.level = level;
			this.addItemDetail();
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x0016FF74 File Offset: 0x0016E174
		public void SetUse(sbyte isUse)
		{
			this.isUse = isUse;
			this.addItemDetail();
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x0016FF83 File Offset: 0x0016E183
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

		// Token: 0x060016E4 RID: 5860 RVA: 0x0016FFA8 File Offset: 0x0016E1A8
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

		// Token: 0x060016E5 RID: 5861 RVA: 0x0016FFE8 File Offset: 0x0016E1E8
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

		// Token: 0x060016E6 RID: 5862 RVA: 0x001700D8 File Offset: 0x0016E2D8
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

		// Token: 0x060016E7 RID: 5863 RVA: 0x001702D0 File Offset: 0x0016E4D0
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

		// Token: 0x060016E8 RID: 5864 RVA: 0x00170364 File Offset: 0x0016E564
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

		// Token: 0x060016E9 RID: 5865 RVA: 0x001703EC File Offset: 0x0016E5EC
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

		// Token: 0x04002C59 RID: 11353
		public sbyte rank;

		// Token: 0x04002C5A RID: 11354
		public sbyte amount;

		// Token: 0x04002C5B RID: 11355
		public sbyte max_amount;

		// Token: 0x04002C5C RID: 11356
		public sbyte typeMonster;

		// Token: 0x04002C5D RID: 11357
		public int id;

		// Token: 0x04002C5E RID: 11358
		public int no;

		// Token: 0x04002C5F RID: 11359
		public int idIcon;

		// Token: 0x04002C60 RID: 11360
		public string name;

		// Token: 0x04002C61 RID: 11361
		public string info;

		// Token: 0x04002C62 RID: 11362
		public sbyte level;

		// Token: 0x04002C63 RID: 11363
		public sbyte isUse;

		// Token: 0x04002C64 RID: 11364
		public Char charInfo;

		// Token: 0x04002C65 RID: 11365
		public Mob mobInfo;

		// Token: 0x04002C66 RID: 11366
		public ItemOption[] itemOption;

		// Token: 0x04002C67 RID: 11367
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

		// Token: 0x04002C68 RID: 11368
		private int count;

		// Token: 0x04002C69 RID: 11369
		private long timeRequest;

		// Token: 0x04002C6A RID: 11370
		public ChatPopup cp;

		// Token: 0x04002C6B RID: 11371
		public MyVector eff = new MyVector(string.Empty);
	}
}
