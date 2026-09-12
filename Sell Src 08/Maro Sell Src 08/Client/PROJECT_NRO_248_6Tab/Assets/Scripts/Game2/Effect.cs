using System;

namespace Game2
{
	// Token: 0x0200038D RID: 909
	public class Effect
	{
		// Token: 0x06002877 RID: 10359 RVA: 0x0027E968 File Offset: 0x0027CB68
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

		// Token: 0x06002878 RID: 10360 RVA: 0x0027EB0C File Offset: 0x0027CD0C
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

		// Token: 0x06002879 RID: 10361 RVA: 0x0027ED38 File Offset: 0x0027CF38
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

		// Token: 0x0600287A RID: 10362 RVA: 0x0027ED80 File Offset: 0x0027CF80
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

		// Token: 0x0600287B RID: 10363 RVA: 0x0027EDCC File Offset: 0x0027CFCC
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

		// Token: 0x0600287C RID: 10364 RVA: 0x0027EE0C File Offset: 0x0027D00C
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

		// Token: 0x0600287D RID: 10365 RVA: 0x0027EE49 File Offset: 0x0027D049
		public bool isPaintz()
		{
			return !ModFunc.GiamDungLuong && this.isPaint;
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x0027EE60 File Offset: 0x0027D060
		public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
		{
			if (this.isPaintz() && Effect.getEffDataById(this.effId).img != null)
			{
				Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
			}
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x0027EEBC File Offset: 0x0027D0BC
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

		// Token: 0x06002880 RID: 10368 RVA: 0x0027EF84 File Offset: 0x0027D184
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

		// Token: 0x06002881 RID: 10369 RVA: 0x0027F008 File Offset: 0x0027D208
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

		// Token: 0x04004D9A RID: 19866
		public int effId;

		// Token: 0x04004D9B RID: 19867
		public int typeEff;

		// Token: 0x04004D9C RID: 19868
		public int indexFrom;

		// Token: 0x04004D9D RID: 19869
		public int indexTo;

		// Token: 0x04004D9E RID: 19870
		public bool isNearPlayer;

		// Token: 0x04004D9F RID: 19871
		public const int NEAR_PLAYER = 0;

		// Token: 0x04004DA0 RID: 19872
		public const int LOOP_NORMAL = 1;

		// Token: 0x04004DA1 RID: 19873
		public const int LOOP_TRANS = 2;

		// Token: 0x04004DA2 RID: 19874
		public const int BACKGROUND = 3;

		// Token: 0x04004DA3 RID: 19875
		public const int CHAR = 4;

		// Token: 0x04004DA4 RID: 19876
		public const int CHAR_PET_EFF = 5;

		// Token: 0x04004DA5 RID: 19877
		public const int FIRE_TD = 0;

		// Token: 0x04004DA6 RID: 19878
		public const int BIRD = 1;

		// Token: 0x04004DA7 RID: 19879
		public const int FIRE_NAMEK = 2;

		// Token: 0x04004DA8 RID: 19880
		public const int FIRE_SAYAI = 3;

		// Token: 0x04004DA9 RID: 19881
		public const int FROG = 5;

		// Token: 0x04004DAA RID: 19882
		public const int CA = 4;

		// Token: 0x04004DAB RID: 19883
		public const int ECH = 6;

		// Token: 0x04004DAC RID: 19884
		public const int TACKE = 7;

		// Token: 0x04004DAD RID: 19885
		public const int RAN = 8;

		// Token: 0x04004DAE RID: 19886
		public const int KHI = 9;

		// Token: 0x04004DAF RID: 19887
		public const int GACON = 10;

		// Token: 0x04004DB0 RID: 19888
		public const int DANONG = 11;

		// Token: 0x04004DB1 RID: 19889
		public const int DANBUOM = 12;

		// Token: 0x04004DB2 RID: 19890
		public const int QUA = 13;

		// Token: 0x04004DB3 RID: 19891
		public const int THIENTHACH = 14;

		// Token: 0x04004DB4 RID: 19892
		public const int CAVOI = 15;

		// Token: 0x04004DB5 RID: 19893
		public const int NAM = 16;

		// Token: 0x04004DB6 RID: 19894
		public const int RONGTHAN = 17;

		// Token: 0x04004DB7 RID: 19895
		public const int BUOMBAY = 26;

		// Token: 0x04004DB8 RID: 19896
		public const int KHUCGO = 27;

		// Token: 0x04004DB9 RID: 19897
		public const int DOIBAY = 28;

		// Token: 0x04004DBA RID: 19898
		public const int CONMEO = 29;

		// Token: 0x04004DBB RID: 19899
		public const int LUATAT = 30;

		// Token: 0x04004DBC RID: 19900
		public const int ONGCONG = 31;

		// Token: 0x04004DBD RID: 19901
		public const int KHANGIA1 = 42;

		// Token: 0x04004DBE RID: 19902
		public const int KHANGIA2 = 43;

		// Token: 0x04004DBF RID: 19903
		public const int KHANGIA3 = 44;

		// Token: 0x04004DC0 RID: 19904
		public const int KHANGIA4 = 45;

		// Token: 0x04004DC1 RID: 19905
		public const int KHANGIA5 = 46;

		// Token: 0x04004DC2 RID: 19906
		public Char c;

		// Token: 0x04004DC3 RID: 19907
		public int t;

		// Token: 0x04004DC4 RID: 19908
		public int currFrame;

		// Token: 0x04004DC5 RID: 19909
		public int x;

		// Token: 0x04004DC6 RID: 19910
		public int y;

		// Token: 0x04004DC7 RID: 19911
		public int loop;

		// Token: 0x04004DC8 RID: 19912
		public int tLoop;

		// Token: 0x04004DC9 RID: 19913
		public int tLoopCount;

		// Token: 0x04004DCA RID: 19914
		private bool isPaint = true;

		// Token: 0x04004DCB RID: 19915
		public int layer;

		// Token: 0x04004DCC RID: 19916
		public int isStand;

		// Token: 0x04004DCD RID: 19917
		public static MyVector vEffData = new MyVector();

		// Token: 0x04004DCE RID: 19918
		public int trans;

		// Token: 0x04004DCF RID: 19919
		public long timeExist;

		// Token: 0x04004DD0 RID: 19920
		public static MyVector lastEff = new MyVector();

		// Token: 0x04004DD1 RID: 19921
		public static MyVector newEff = new MyVector();

		// Token: 0x04004DD2 RID: 19922
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

		// Token: 0x04004DD3 RID: 19923
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

		// Token: 0x04004DD4 RID: 19924
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

		// Token: 0x04004DD5 RID: 19925
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

		// Token: 0x04004DD6 RID: 19926
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

		// Token: 0x04004DD7 RID: 19927
		private bool isGetTime;

		// Token: 0x04004DD8 RID: 19928
		private short[] data;

		// Token: 0x04004DD9 RID: 19929
		public int cLastStatusMe;

		// Token: 0x04004DDA RID: 19930
		public long cur_time_cLastStatusMe;
	}
}
