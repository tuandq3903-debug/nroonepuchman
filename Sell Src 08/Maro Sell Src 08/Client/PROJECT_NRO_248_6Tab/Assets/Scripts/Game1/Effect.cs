using System;

namespace Game1
{
	// Token: 0x02000465 RID: 1125
	public class Effect
	{
		// Token: 0x0600321B RID: 12827 RVA: 0x00313A0C File Offset: 0x00311C0C
		public Effect(int id, Char c, int layer, int loop, int loopCount, sbyte isStand)
		{
			this.c = c;
			this.effId = id;
			this.layer = layer;
			this.loop = loop;
			this.tLoop = loopCount;
			this.isStand = (int)isStand;
			if (Effect.getEffDataById(id) == null)
			{
				EffectData effectData = new EffectData
				{
					ID = id
				};
				if (id >= 42 && id <= 46)
				{
					id = 106;
				}
				string text = string.Concat(new string[]
				{
					"/x",
					mGraphics.zoomLevel.ToString(),
					"/effectdata/",
					id.ToString(),
					"/data"
				});
				if (MyStream.readFile(text) != null)
				{
					if (id > 100 && id < 200)
					{
						effectData.readData2(text);
					}
					else
					{
						effectData.readData(text);
					}
					effectData.img = GameCanvas.loadImage("/effectdata/" + id.ToString() + "/img.png");
				}
				else
				{
					Service.gI().getEffData((short)id);
				}
				Effect.addEffData(effectData);
			}
			this.indexFrom = -1;
			this.indexTo = -1;
			this.trans = -1;
			this.typeEff = 4;
			if (id == 78)
			{
				this.typeEff = 5;
			}
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x00313BB0 File Offset: 0x00311DB0
		public Effect(int id, int x, int y, int layer, int loop, int loopCount)
		{
			this.x = x;
			this.y = y;
			this.effId = id;
			this.layer = layer;
			this.loop = loop;
			this.tLoop = loopCount;
			if (Effect.getEffDataById(id) == null)
			{
				EffectData effectData = new EffectData
				{
					ID = id
				};
				if (id >= 42 && id <= 46)
				{
					id = 106;
				}
				string text = string.Concat(new string[]
				{
					"/x",
					mGraphics.zoomLevel.ToString(),
					"/effectdata/",
					id.ToString(),
					"/data"
				});
				if (MyStream.readFile(text) != null)
				{
					if (id > 100 && id < 200)
					{
						effectData.readData2(text);
					}
					else
					{
						effectData.readData(text);
					}
					effectData.img = GameCanvas.loadImage("/effectdata/" + id.ToString() + "/img.png");
				}
				else
				{
					Service.gI().getEffData((short)id);
				}
				Effect.addEffData(effectData);
				if (Effect.lastEff.size() > 20)
				{
					Effect.removeEffData(int.Parse((string)Effect.lastEff.elementAt(0)));
					Effect.lastEff.removeElementAt(0);
				}
				Effect.lastEff.addElement(this.effId.ToString() + string.Empty);
			}
			this.indexFrom = -1;
			this.indexTo = -1;
			if (id == 78)
			{
				this.typeEff = 5;
			}
			else
			{
				this.typeEff = 1;
			}
			if (!Effect.isExistNewEff(this.effId.ToString() + string.Empty))
			{
				Effect.newEff.addElement(this.effId.ToString() + string.Empty);
			}
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x00313DDC File Offset: 0x00311FDC
		public static void removeEffData(int id)
		{
			for (int i = 0; i < Effect.vEffData.size(); i++)
			{
				EffectData effectData = (EffectData)Effect.vEffData.elementAt(i);
				if (effectData.ID == id)
				{
					Effect.vEffData.removeElement(effectData);
					return;
				}
			}
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x00313E24 File Offset: 0x00312024
		public static void addEffData(EffectData eff)
		{
			Effect.vEffData.addElement(eff);
			if (TileMap.mapID != 130 && Effect.vEffData.size() > 10)
			{
				for (int i = 0; i < 5; i++)
				{
					Effect.vEffData.removeElementAt(0);
				}
			}
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x00313E70 File Offset: 0x00312070
		public static EffectData getEffDataById(int id)
		{
			for (int i = 0; i < Effect.vEffData.size(); i++)
			{
				EffectData effectData = (EffectData)Effect.vEffData.elementAt(i);
				if (effectData.ID == id)
				{
					return effectData;
				}
			}
			return null;
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x00313EB0 File Offset: 0x003120B0
		public static bool isExistNewEff(string id)
		{
			for (int i = 0; i < Effect.newEff.size(); i++)
			{
				if (((string)Effect.newEff.elementAt(i)).Equals(id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x00313EED File Offset: 0x003120ED
		public bool isPaintz()
		{
			return !ModFunc.GiamDungLuong && this.isPaint;
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x00313F04 File Offset: 0x00312104
		public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
		{
			if (this.isPaintz() && Effect.getEffDataById(this.effId).img != null)
			{
				Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
			}
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x00313F60 File Offset: 0x00312160
		public void getFrameKhangia()
		{
			if (this.effId == 42)
			{
				this.currFrame = this.khangia1[this.t];
			}
			if (this.effId == 43)
			{
				this.currFrame = this.khangia2[this.t];
			}
			if (this.effId == 44)
			{
				this.currFrame = this.khangia3[this.t];
			}
			if (this.effId == 45)
			{
				this.currFrame = this.khangia4[this.t];
			}
			if (this.effId == 46)
			{
				this.currFrame = this.khangia5[this.t];
			}
			this.t++;
			if (this.t > this.khangia1.Length - 1)
			{
				this.t = 0;
			}
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x00314028 File Offset: 0x00312228
		public void paint(mGraphics g)
		{
			if (ModFunc.GiamDungLuong || !this.isPaint || Effect.getEffDataById(this.effId) == null || Effect.getEffDataById(this.effId).img == null)
			{
				return;
			}
			try
			{
				Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x, this.y, this.trans, this.layer);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x003140AC File Offset: 0x003122AC
		public void update()
		{
			try
			{
				if (this.effId >= 42 && this.effId <= 46)
				{
					this.getFrameKhangia();
				}
				else if (Effect.getEffDataById(this.effId) != null && Effect.getEffDataById(this.effId).img != null)
				{
					if (this.typeEff == 5)
					{
						this.data = Effect.getEffDataById(this.effId).get(this.c.statusMe);
					}
					else
					{
						this.data = Effect.getEffDataById(this.effId).get();
					}
					if (this.data != null)
					{
						if (!this.isGetTime)
						{
							this.isGetTime = true;
							int num = this.data.Length - 1;
							if (num > 0 && this.typeEff != 1)
							{
								this.t = Res.random(0, num);
							}
							if (this.typeEff == 0)
							{
								this.t = Res.random(this.indexFrom, this.indexTo);
							}
						}
						switch (this.typeEff)
						{
						case 0:
							if (Res.inRect(this.x - 50, this.y - 50, 100, 100, Char.myCharz().cx, Char.myCharz().cy) && this.t > this.indexFrom && this.t < this.indexTo)
							{
								if (this.t < this.indexTo)
								{
									this.t = this.indexTo;
								}
								this.isNearPlayer = true;
							}
							if (!this.isNearPlayer)
							{
								this.t++;
								if (this.t == this.indexTo)
								{
									this.t = this.indexFrom;
								}
							}
							else if (this.t < this.data.Length)
							{
								this.t++;
							}
							break;
						case 1:
						case 3:
							if (this.t < this.data.Length)
							{
								this.t++;
							}
							break;
						case 2:
							if (this.t < this.data.Length)
							{
								this.t++;
							}
							this.tLoopCount++;
							if (this.tLoopCount == this.tLoop)
							{
								this.tLoopCount = 0;
								this.trans = Res.random(0, 2);
							}
							break;
						case 4:
							this.x = this.c.cx;
							this.y = this.c.cy;
							if (this.t < this.data.Length)
							{
								this.t++;
							}
							break;
						case 5:
							this.trans = ((this.c.cdir != 1) ? 1 : 0);
							if (this.c.cdir == 1)
							{
								this.x = this.c.cx - 15;
							}
							else
							{
								this.x = this.c.cx + 15;
							}
							if (this.c.isMonkey == 0)
							{
								this.y = this.c.cy - 25;
							}
							else
							{
								this.y = this.c.cy - 35;
							}
							if (this.t < this.data.Length)
							{
								this.t++;
							}
							break;
						}
						if (this.t == this.data.Length / 2 && (this.effId == 62 || this.effId == 63 || this.effId == 64 || this.effId == 65))
						{
							SoundMn.playSound(this.x, this.y, SoundMn.FIREWORK, SoundMn.volume);
						}
						if (this.t <= this.data.Length - 1)
						{
							this.currFrame = (int)this.data[this.t];
						}
					}
					if (this.t >= this.data.Length - 1)
					{
						if (this.typeEff == 0 || this.typeEff == 3)
						{
							this.isPaint = false;
						}
						if (this.tLoop == -1)
						{
							EffecMn.vEff.removeElement(this);
						}
						if (this.typeEff == 2)
						{
							this.t = 0;
						}
						else
						{
							if (this.typeEff == 1 && this.loop == 1)
							{
								this.isPaint = false;
							}
							if (this.typeEff == 4 || this.typeEff == 5)
							{
								if (this.loop == -1)
								{
									this.t = 0;
								}
								else
								{
									this.tLoopCount++;
									if (this.tLoopCount == this.tLoop)
									{
										this.tLoopCount = 0;
										this.loop--;
										this.t = 0;
										if (this.loop == 0)
										{
											this.c.removeEffChar(0, this.effId);
										}
									}
								}
							}
							else
							{
								this.isNearPlayer = false;
								if (this.loop == -1)
								{
									this.tLoopCount++;
									this.t = 0;
									if (this.tLoopCount == this.tLoop)
									{
										this.tLoopCount = 0;
										if (this.tLoop > 1)
										{
											this.trans = Res.random(0, 2);
										}
									}
								}
								else
								{
									this.tLoopCount++;
									this.t = 0;
									if (this.tLoopCount == this.tLoop)
									{
										this.tLoopCount = 0;
										this.loop--;
										if (this.loop == 0)
										{
											EffecMn.vEff.removeElement(this);
										}
									}
								}
							}
						}
					}
					else
					{
						this.isPaint = true;
					}
				}
			}
			catch (Exception)
			{
				EffecMn.vEff.removeElement(this);
			}
		}

		// Token: 0x04006019 RID: 24601
		public int effId;

		// Token: 0x0400601A RID: 24602
		public int typeEff;

		// Token: 0x0400601B RID: 24603
		public int indexFrom;

		// Token: 0x0400601C RID: 24604
		public int indexTo;

		// Token: 0x0400601D RID: 24605
		public bool isNearPlayer;

		// Token: 0x0400601E RID: 24606
		public const int NEAR_PLAYER = 0;

		// Token: 0x0400601F RID: 24607
		public const int LOOP_NORMAL = 1;

		// Token: 0x04006020 RID: 24608
		public const int LOOP_TRANS = 2;

		// Token: 0x04006021 RID: 24609
		public const int BACKGROUND = 3;

		// Token: 0x04006022 RID: 24610
		public const int CHAR = 4;

		// Token: 0x04006023 RID: 24611
		public const int CHAR_PET_EFF = 5;

		// Token: 0x04006024 RID: 24612
		public const int FIRE_TD = 0;

		// Token: 0x04006025 RID: 24613
		public const int BIRD = 1;

		// Token: 0x04006026 RID: 24614
		public const int FIRE_NAMEK = 2;

		// Token: 0x04006027 RID: 24615
		public const int FIRE_SAYAI = 3;

		// Token: 0x04006028 RID: 24616
		public const int FROG = 5;

		// Token: 0x04006029 RID: 24617
		public const int CA = 4;

		// Token: 0x0400602A RID: 24618
		public const int ECH = 6;

		// Token: 0x0400602B RID: 24619
		public const int TACKE = 7;

		// Token: 0x0400602C RID: 24620
		public const int RAN = 8;

		// Token: 0x0400602D RID: 24621
		public const int KHI = 9;

		// Token: 0x0400602E RID: 24622
		public const int GACON = 10;

		// Token: 0x0400602F RID: 24623
		public const int DANONG = 11;

		// Token: 0x04006030 RID: 24624
		public const int DANBUOM = 12;

		// Token: 0x04006031 RID: 24625
		public const int QUA = 13;

		// Token: 0x04006032 RID: 24626
		public const int THIENTHACH = 14;

		// Token: 0x04006033 RID: 24627
		public const int CAVOI = 15;

		// Token: 0x04006034 RID: 24628
		public const int NAM = 16;

		// Token: 0x04006035 RID: 24629
		public const int RONGTHAN = 17;

		// Token: 0x04006036 RID: 24630
		public const int BUOMBAY = 26;

		// Token: 0x04006037 RID: 24631
		public const int KHUCGO = 27;

		// Token: 0x04006038 RID: 24632
		public const int DOIBAY = 28;

		// Token: 0x04006039 RID: 24633
		public const int CONMEO = 29;

		// Token: 0x0400603A RID: 24634
		public const int LUATAT = 30;

		// Token: 0x0400603B RID: 24635
		public const int ONGCONG = 31;

		// Token: 0x0400603C RID: 24636
		public const int KHANGIA1 = 42;

		// Token: 0x0400603D RID: 24637
		public const int KHANGIA2 = 43;

		// Token: 0x0400603E RID: 24638
		public const int KHANGIA3 = 44;

		// Token: 0x0400603F RID: 24639
		public const int KHANGIA4 = 45;

		// Token: 0x04006040 RID: 24640
		public const int KHANGIA5 = 46;

		// Token: 0x04006041 RID: 24641
		public Char c;

		// Token: 0x04006042 RID: 24642
		public int t;

		// Token: 0x04006043 RID: 24643
		public int currFrame;

		// Token: 0x04006044 RID: 24644
		public int x;

		// Token: 0x04006045 RID: 24645
		public int y;

		// Token: 0x04006046 RID: 24646
		public int loop;

		// Token: 0x04006047 RID: 24647
		public int tLoop;

		// Token: 0x04006048 RID: 24648
		public int tLoopCount;

		// Token: 0x04006049 RID: 24649
		private bool isPaint = true;

		// Token: 0x0400604A RID: 24650
		public int layer;

		// Token: 0x0400604B RID: 24651
		public int isStand;

		// Token: 0x0400604C RID: 24652
		public static MyVector vEffData = new MyVector();

		// Token: 0x0400604D RID: 24653
		public int trans;

		// Token: 0x0400604E RID: 24654
		public long timeExist;

		// Token: 0x0400604F RID: 24655
		public static MyVector lastEff = new MyVector();

		// Token: 0x04006050 RID: 24656
		public static MyVector newEff = new MyVector();

		// Token: 0x04006051 RID: 24657
		private int[] khangia1 = new int[]
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

		// Token: 0x04006052 RID: 24658
		private int[] khangia2 = new int[]
		{
			2,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			3
		};

		// Token: 0x04006053 RID: 24659
		private int[] khangia3 = new int[]
		{
			4,
			4,
			4,
			4,
			4,
			5,
			5,
			5,
			5,
			5
		};

		// Token: 0x04006054 RID: 24660
		private int[] khangia4 = new int[]
		{
			6,
			6,
			6,
			6,
			6,
			7,
			7,
			7,
			7,
			7
		};

		// Token: 0x04006055 RID: 24661
		private int[] khangia5 = new int[]
		{
			8,
			8,
			8,
			8,
			8,
			9,
			9,
			9,
			9,
			9
		};

		// Token: 0x04006056 RID: 24662
		private bool isGetTime;

		// Token: 0x04006057 RID: 24663
		private short[] data;

		// Token: 0x04006058 RID: 24664
		public int cLastStatusMe;

		// Token: 0x04006059 RID: 24665
		public long cur_time_cLastStatusMe;
	}
}
