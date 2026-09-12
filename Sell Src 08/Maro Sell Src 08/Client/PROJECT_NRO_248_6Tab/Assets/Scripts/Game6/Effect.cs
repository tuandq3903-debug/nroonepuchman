using System;

namespace Game6
{
	// Token: 0x0200002D RID: 45
	public class Effect
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0002A570 File Offset: 0x00028770
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

		// Token: 0x060001E8 RID: 488 RVA: 0x0002A714 File Offset: 0x00028914
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

		// Token: 0x060001E9 RID: 489 RVA: 0x0002A940 File Offset: 0x00028B40
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

		// Token: 0x060001EA RID: 490 RVA: 0x0002A988 File Offset: 0x00028B88
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

		// Token: 0x060001EB RID: 491 RVA: 0x0002A9D4 File Offset: 0x00028BD4
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

		// Token: 0x060001EC RID: 492 RVA: 0x0002AA14 File Offset: 0x00028C14
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

		// Token: 0x060001ED RID: 493 RVA: 0x0002AA51 File Offset: 0x00028C51
		public bool isPaintz()
		{
			return !ModFunc.GiamDungLuong && this.isPaint;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0002AA68 File Offset: 0x00028C68
		public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
		{
			if (this.isPaintz() && Effect.getEffDataById(this.effId).img != null)
			{
				Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0002AAC4 File Offset: 0x00028CC4
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

		// Token: 0x060001F0 RID: 496 RVA: 0x0002AB8C File Offset: 0x00028D8C
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

		// Token: 0x060001F1 RID: 497 RVA: 0x0002AC10 File Offset: 0x00028E10
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

		// Token: 0x0400039E RID: 926
		public int effId;

		// Token: 0x0400039F RID: 927
		public int typeEff;

		// Token: 0x040003A0 RID: 928
		public int indexFrom;

		// Token: 0x040003A1 RID: 929
		public int indexTo;

		// Token: 0x040003A2 RID: 930
		public bool isNearPlayer;

		// Token: 0x040003A3 RID: 931
		public const int NEAR_PLAYER = 0;

		// Token: 0x040003A4 RID: 932
		public const int LOOP_NORMAL = 1;

		// Token: 0x040003A5 RID: 933
		public const int LOOP_TRANS = 2;

		// Token: 0x040003A6 RID: 934
		public const int BACKGROUND = 3;

		// Token: 0x040003A7 RID: 935
		public const int CHAR = 4;

		// Token: 0x040003A8 RID: 936
		public const int CHAR_PET_EFF = 5;

		// Token: 0x040003A9 RID: 937
		public const int FIRE_TD = 0;

		// Token: 0x040003AA RID: 938
		public const int BIRD = 1;

		// Token: 0x040003AB RID: 939
		public const int FIRE_NAMEK = 2;

		// Token: 0x040003AC RID: 940
		public const int FIRE_SAYAI = 3;

		// Token: 0x040003AD RID: 941
		public const int FROG = 5;

		// Token: 0x040003AE RID: 942
		public const int CA = 4;

		// Token: 0x040003AF RID: 943
		public const int ECH = 6;

		// Token: 0x040003B0 RID: 944
		public const int TACKE = 7;

		// Token: 0x040003B1 RID: 945
		public const int RAN = 8;

		// Token: 0x040003B2 RID: 946
		public const int KHI = 9;

		// Token: 0x040003B3 RID: 947
		public const int GACON = 10;

		// Token: 0x040003B4 RID: 948
		public const int DANONG = 11;

		// Token: 0x040003B5 RID: 949
		public const int DANBUOM = 12;

		// Token: 0x040003B6 RID: 950
		public const int QUA = 13;

		// Token: 0x040003B7 RID: 951
		public const int THIENTHACH = 14;

		// Token: 0x040003B8 RID: 952
		public const int CAVOI = 15;

		// Token: 0x040003B9 RID: 953
		public const int NAM = 16;

		// Token: 0x040003BA RID: 954
		public const int RONGTHAN = 17;

		// Token: 0x040003BB RID: 955
		public const int BUOMBAY = 26;

		// Token: 0x040003BC RID: 956
		public const int KHUCGO = 27;

		// Token: 0x040003BD RID: 957
		public const int DOIBAY = 28;

		// Token: 0x040003BE RID: 958
		public const int CONMEO = 29;

		// Token: 0x040003BF RID: 959
		public const int LUATAT = 30;

		// Token: 0x040003C0 RID: 960
		public const int ONGCONG = 31;

		// Token: 0x040003C1 RID: 961
		public const int KHANGIA1 = 42;

		// Token: 0x040003C2 RID: 962
		public const int KHANGIA2 = 43;

		// Token: 0x040003C3 RID: 963
		public const int KHANGIA3 = 44;

		// Token: 0x040003C4 RID: 964
		public const int KHANGIA4 = 45;

		// Token: 0x040003C5 RID: 965
		public const int KHANGIA5 = 46;

		// Token: 0x040003C6 RID: 966
		public Char c;

		// Token: 0x040003C7 RID: 967
		public int t;

		// Token: 0x040003C8 RID: 968
		public int currFrame;

		// Token: 0x040003C9 RID: 969
		public int x;

		// Token: 0x040003CA RID: 970
		public int y;

		// Token: 0x040003CB RID: 971
		public int loop;

		// Token: 0x040003CC RID: 972
		public int tLoop;

		// Token: 0x040003CD RID: 973
		public int tLoopCount;

		// Token: 0x040003CE RID: 974
		private bool isPaint = true;

		// Token: 0x040003CF RID: 975
		public int layer;

		// Token: 0x040003D0 RID: 976
		public int isStand;

		// Token: 0x040003D1 RID: 977
		public static MyVector vEffData = new MyVector();

		// Token: 0x040003D2 RID: 978
		public int trans;

		// Token: 0x040003D3 RID: 979
		public long timeExist;

		// Token: 0x040003D4 RID: 980
		public static MyVector lastEff = new MyVector();

		// Token: 0x040003D5 RID: 981
		public static MyVector newEff = new MyVector();

		// Token: 0x040003D6 RID: 982
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

		// Token: 0x040003D7 RID: 983
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

		// Token: 0x040003D8 RID: 984
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

		// Token: 0x040003D9 RID: 985
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

		// Token: 0x040003DA RID: 986
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

		// Token: 0x040003DB RID: 987
		private bool isGetTime;

		// Token: 0x040003DC RID: 988
		private short[] data;

		// Token: 0x040003DD RID: 989
		public int cLastStatusMe;

		// Token: 0x040003DE RID: 990
		public long cur_time_cLastStatusMe;
	}
}
