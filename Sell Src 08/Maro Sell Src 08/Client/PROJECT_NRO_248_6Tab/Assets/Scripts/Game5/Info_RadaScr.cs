using System;

namespace Game5
{
	// Token: 0x02000126 RID: 294
	public class Info_RadaScr
	{
		// Token: 0x06000D3B RID: 3387 RVA: 0x000DAE3C File Offset: 0x000D903C
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

		// Token: 0x06000D3C RID: 3388 RVA: 0x000DAEB1 File Offset: 0x000D90B1
		public void SetAmount(sbyte amount, sbyte max_amount)
		{
			this.amount = amount;
			this.max_amount = max_amount;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000DAEC1 File Offset: 0x000D90C1
		public void SetLevel(sbyte level)
		{
			this.level = level;
			this.addItemDetail();
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x000DAED0 File Offset: 0x000D90D0
		public void SetUse(sbyte isUse)
		{
			this.isUse = isUse;
			this.addItemDetail();
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x000DAEDF File Offset: 0x000D90DF
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

		// Token: 0x06000D40 RID: 3392 RVA: 0x000DAF04 File Offset: 0x000D9104
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

		// Token: 0x06000D41 RID: 3393 RVA: 0x000DAF44 File Offset: 0x000D9144
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

		// Token: 0x06000D42 RID: 3394 RVA: 0x000DB034 File Offset: 0x000D9234
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

		// Token: 0x06000D43 RID: 3395 RVA: 0x000DB22C File Offset: 0x000D942C
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

		// Token: 0x06000D44 RID: 3396 RVA: 0x000DB2C0 File Offset: 0x000D94C0
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

		// Token: 0x06000D45 RID: 3397 RVA: 0x000DB348 File Offset: 0x000D9548
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

		// Token: 0x040019DA RID: 6618
		public sbyte rank;

		// Token: 0x040019DB RID: 6619
		public sbyte amount;

		// Token: 0x040019DC RID: 6620
		public sbyte max_amount;

		// Token: 0x040019DD RID: 6621
		public sbyte typeMonster;

		// Token: 0x040019DE RID: 6622
		public int id;

		// Token: 0x040019DF RID: 6623
		public int no;

		// Token: 0x040019E0 RID: 6624
		public int idIcon;

		// Token: 0x040019E1 RID: 6625
		public string name;

		// Token: 0x040019E2 RID: 6626
		public string info;

		// Token: 0x040019E3 RID: 6627
		public sbyte level;

		// Token: 0x040019E4 RID: 6628
		public sbyte isUse;

		// Token: 0x040019E5 RID: 6629
		public Char charInfo;

		// Token: 0x040019E6 RID: 6630
		public Mob mobInfo;

		// Token: 0x040019E7 RID: 6631
		public ItemOption[] itemOption;

		// Token: 0x040019E8 RID: 6632
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

		// Token: 0x040019E9 RID: 6633
		private int count;

		// Token: 0x040019EA RID: 6634
		private long timeRequest;

		// Token: 0x040019EB RID: 6635
		public ChatPopup cp;

		// Token: 0x040019EC RID: 6636
		public MyVector eff = new MyVector(string.Empty);
	}
}
