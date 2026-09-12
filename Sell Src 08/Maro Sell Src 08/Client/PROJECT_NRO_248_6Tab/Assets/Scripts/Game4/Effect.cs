using System;

namespace Game4
{
	// Token: 0x020001DD RID: 477
	public class Effect
	{
		// Token: 0x0600152F RID: 5423 RVA: 0x00154820 File Offset: 0x00152A20
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

		// Token: 0x06001530 RID: 5424 RVA: 0x001549C4 File Offset: 0x00152BC4
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

		// Token: 0x06001531 RID: 5425 RVA: 0x00154BF0 File Offset: 0x00152DF0
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

		// Token: 0x06001532 RID: 5426 RVA: 0x00154C38 File Offset: 0x00152E38
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

		// Token: 0x06001533 RID: 5427 RVA: 0x00154C84 File Offset: 0x00152E84
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

		// Token: 0x06001534 RID: 5428 RVA: 0x00154CC4 File Offset: 0x00152EC4
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

		// Token: 0x06001535 RID: 5429 RVA: 0x00154D01 File Offset: 0x00152F01
		public bool isPaintz()
		{
			return !ModFunc.GiamDungLuong && this.isPaint;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00154D18 File Offset: 0x00152F18
		public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
		{
			if (this.isPaintz() && Effect.getEffDataById(this.effId).img != null)
			{
				Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
			}
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00154D74 File Offset: 0x00152F74
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

		// Token: 0x06001538 RID: 5432 RVA: 0x00154E3C File Offset: 0x0015303C
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

		// Token: 0x06001539 RID: 5433 RVA: 0x00154EC0 File Offset: 0x001530C0
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

		// Token: 0x0400289C RID: 10396
		public int effId;

		// Token: 0x0400289D RID: 10397
		public int typeEff;

		// Token: 0x0400289E RID: 10398
		public int indexFrom;

		// Token: 0x0400289F RID: 10399
		public int indexTo;

		// Token: 0x040028A0 RID: 10400
		public bool isNearPlayer;

		// Token: 0x040028A1 RID: 10401
		public const int NEAR_PLAYER = 0;

		// Token: 0x040028A2 RID: 10402
		public const int LOOP_NORMAL = 1;

		// Token: 0x040028A3 RID: 10403
		public const int LOOP_TRANS = 2;

		// Token: 0x040028A4 RID: 10404
		public const int BACKGROUND = 3;

		// Token: 0x040028A5 RID: 10405
		public const int CHAR = 4;

		// Token: 0x040028A6 RID: 10406
		public const int CHAR_PET_EFF = 5;

		// Token: 0x040028A7 RID: 10407
		public const int FIRE_TD = 0;

		// Token: 0x040028A8 RID: 10408
		public const int BIRD = 1;

		// Token: 0x040028A9 RID: 10409
		public const int FIRE_NAMEK = 2;

		// Token: 0x040028AA RID: 10410
		public const int FIRE_SAYAI = 3;

		// Token: 0x040028AB RID: 10411
		public const int FROG = 5;

		// Token: 0x040028AC RID: 10412
		public const int CA = 4;

		// Token: 0x040028AD RID: 10413
		public const int ECH = 6;

		// Token: 0x040028AE RID: 10414
		public const int TACKE = 7;

		// Token: 0x040028AF RID: 10415
		public const int RAN = 8;

		// Token: 0x040028B0 RID: 10416
		public const int KHI = 9;

		// Token: 0x040028B1 RID: 10417
		public const int GACON = 10;

		// Token: 0x040028B2 RID: 10418
		public const int DANONG = 11;

		// Token: 0x040028B3 RID: 10419
		public const int DANBUOM = 12;

		// Token: 0x040028B4 RID: 10420
		public const int QUA = 13;

		// Token: 0x040028B5 RID: 10421
		public const int THIENTHACH = 14;

		// Token: 0x040028B6 RID: 10422
		public const int CAVOI = 15;

		// Token: 0x040028B7 RID: 10423
		public const int NAM = 16;

		// Token: 0x040028B8 RID: 10424
		public const int RONGTHAN = 17;

		// Token: 0x040028B9 RID: 10425
		public const int BUOMBAY = 26;

		// Token: 0x040028BA RID: 10426
		public const int KHUCGO = 27;

		// Token: 0x040028BB RID: 10427
		public const int DOIBAY = 28;

		// Token: 0x040028BC RID: 10428
		public const int CONMEO = 29;

		// Token: 0x040028BD RID: 10429
		public const int LUATAT = 30;

		// Token: 0x040028BE RID: 10430
		public const int ONGCONG = 31;

		// Token: 0x040028BF RID: 10431
		public const int KHANGIA1 = 42;

		// Token: 0x040028C0 RID: 10432
		public const int KHANGIA2 = 43;

		// Token: 0x040028C1 RID: 10433
		public const int KHANGIA3 = 44;

		// Token: 0x040028C2 RID: 10434
		public const int KHANGIA4 = 45;

		// Token: 0x040028C3 RID: 10435
		public const int KHANGIA5 = 46;

		// Token: 0x040028C4 RID: 10436
		public Char c;

		// Token: 0x040028C5 RID: 10437
		public int t;

		// Token: 0x040028C6 RID: 10438
		public int currFrame;

		// Token: 0x040028C7 RID: 10439
		public int x;

		// Token: 0x040028C8 RID: 10440
		public int y;

		// Token: 0x040028C9 RID: 10441
		public int loop;

		// Token: 0x040028CA RID: 10442
		public int tLoop;

		// Token: 0x040028CB RID: 10443
		public int tLoopCount;

		// Token: 0x040028CC RID: 10444
		private bool isPaint = true;

		// Token: 0x040028CD RID: 10445
		public int layer;

		// Token: 0x040028CE RID: 10446
		public int isStand;

		// Token: 0x040028CF RID: 10447
		public static MyVector vEffData = new MyVector();

		// Token: 0x040028D0 RID: 10448
		public int trans;

		// Token: 0x040028D1 RID: 10449
		public long timeExist;

		// Token: 0x040028D2 RID: 10450
		public static MyVector lastEff = new MyVector();

		// Token: 0x040028D3 RID: 10451
		public static MyVector newEff = new MyVector();

		// Token: 0x040028D4 RID: 10452
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

		// Token: 0x040028D5 RID: 10453
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

		// Token: 0x040028D6 RID: 10454
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

		// Token: 0x040028D7 RID: 10455
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

		// Token: 0x040028D8 RID: 10456
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

		// Token: 0x040028D9 RID: 10457
		private bool isGetTime;

		// Token: 0x040028DA RID: 10458
		private short[] data;

		// Token: 0x040028DB RID: 10459
		public int cLastStatusMe;

		// Token: 0x040028DC RID: 10460
		public long cur_time_cLastStatusMe;
	}
}
