using System;

namespace Game3
{
	// Token: 0x020002B5 RID: 693
	public class Effect
	{
		// Token: 0x06001ED3 RID: 7891 RVA: 0x001E98C4 File Offset: 0x001E7AC4
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

		// Token: 0x06001ED4 RID: 7892 RVA: 0x001E9A68 File Offset: 0x001E7C68
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

		// Token: 0x06001ED5 RID: 7893 RVA: 0x001E9C94 File Offset: 0x001E7E94
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

		// Token: 0x06001ED6 RID: 7894 RVA: 0x001E9CDC File Offset: 0x001E7EDC
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

		// Token: 0x06001ED7 RID: 7895 RVA: 0x001E9D28 File Offset: 0x001E7F28
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

		// Token: 0x06001ED8 RID: 7896 RVA: 0x001E9D68 File Offset: 0x001E7F68
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

		// Token: 0x06001ED9 RID: 7897 RVA: 0x001E9DA5 File Offset: 0x001E7FA5
		public bool isPaintz()
		{
			return !ModFunc.GiamDungLuong && this.isPaint;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x001E9DBC File Offset: 0x001E7FBC
		public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
		{
			if (this.isPaintz() && Effect.getEffDataById(this.effId).img != null)
			{
				Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
			}
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x001E9E18 File Offset: 0x001E8018
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

		// Token: 0x06001EDC RID: 7900 RVA: 0x001E9EE0 File Offset: 0x001E80E0
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

		// Token: 0x06001EDD RID: 7901 RVA: 0x001E9F64 File Offset: 0x001E8164
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

		// Token: 0x04003B1B RID: 15131
		public int effId;

		// Token: 0x04003B1C RID: 15132
		public int typeEff;

		// Token: 0x04003B1D RID: 15133
		public int indexFrom;

		// Token: 0x04003B1E RID: 15134
		public int indexTo;

		// Token: 0x04003B1F RID: 15135
		public bool isNearPlayer;

		// Token: 0x04003B20 RID: 15136
		public const int NEAR_PLAYER = 0;

		// Token: 0x04003B21 RID: 15137
		public const int LOOP_NORMAL = 1;

		// Token: 0x04003B22 RID: 15138
		public const int LOOP_TRANS = 2;

		// Token: 0x04003B23 RID: 15139
		public const int BACKGROUND = 3;

		// Token: 0x04003B24 RID: 15140
		public const int CHAR = 4;

		// Token: 0x04003B25 RID: 15141
		public const int CHAR_PET_EFF = 5;

		// Token: 0x04003B26 RID: 15142
		public const int FIRE_TD = 0;

		// Token: 0x04003B27 RID: 15143
		public const int BIRD = 1;

		// Token: 0x04003B28 RID: 15144
		public const int FIRE_NAMEK = 2;

		// Token: 0x04003B29 RID: 15145
		public const int FIRE_SAYAI = 3;

		// Token: 0x04003B2A RID: 15146
		public const int FROG = 5;

		// Token: 0x04003B2B RID: 15147
		public const int CA = 4;

		// Token: 0x04003B2C RID: 15148
		public const int ECH = 6;

		// Token: 0x04003B2D RID: 15149
		public const int TACKE = 7;

		// Token: 0x04003B2E RID: 15150
		public const int RAN = 8;

		// Token: 0x04003B2F RID: 15151
		public const int KHI = 9;

		// Token: 0x04003B30 RID: 15152
		public const int GACON = 10;

		// Token: 0x04003B31 RID: 15153
		public const int DANONG = 11;

		// Token: 0x04003B32 RID: 15154
		public const int DANBUOM = 12;

		// Token: 0x04003B33 RID: 15155
		public const int QUA = 13;

		// Token: 0x04003B34 RID: 15156
		public const int THIENTHACH = 14;

		// Token: 0x04003B35 RID: 15157
		public const int CAVOI = 15;

		// Token: 0x04003B36 RID: 15158
		public const int NAM = 16;

		// Token: 0x04003B37 RID: 15159
		public const int RONGTHAN = 17;

		// Token: 0x04003B38 RID: 15160
		public const int BUOMBAY = 26;

		// Token: 0x04003B39 RID: 15161
		public const int KHUCGO = 27;

		// Token: 0x04003B3A RID: 15162
		public const int DOIBAY = 28;

		// Token: 0x04003B3B RID: 15163
		public const int CONMEO = 29;

		// Token: 0x04003B3C RID: 15164
		public const int LUATAT = 30;

		// Token: 0x04003B3D RID: 15165
		public const int ONGCONG = 31;

		// Token: 0x04003B3E RID: 15166
		public const int KHANGIA1 = 42;

		// Token: 0x04003B3F RID: 15167
		public const int KHANGIA2 = 43;

		// Token: 0x04003B40 RID: 15168
		public const int KHANGIA3 = 44;

		// Token: 0x04003B41 RID: 15169
		public const int KHANGIA4 = 45;

		// Token: 0x04003B42 RID: 15170
		public const int KHANGIA5 = 46;

		// Token: 0x04003B43 RID: 15171
		public Char c;

		// Token: 0x04003B44 RID: 15172
		public int t;

		// Token: 0x04003B45 RID: 15173
		public int currFrame;

		// Token: 0x04003B46 RID: 15174
		public int x;

		// Token: 0x04003B47 RID: 15175
		public int y;

		// Token: 0x04003B48 RID: 15176
		public int loop;

		// Token: 0x04003B49 RID: 15177
		public int tLoop;

		// Token: 0x04003B4A RID: 15178
		public int tLoopCount;

		// Token: 0x04003B4B RID: 15179
		private bool isPaint = true;

		// Token: 0x04003B4C RID: 15180
		public int layer;

		// Token: 0x04003B4D RID: 15181
		public int isStand;

		// Token: 0x04003B4E RID: 15182
		public static MyVector vEffData = new MyVector();

		// Token: 0x04003B4F RID: 15183
		public int trans;

		// Token: 0x04003B50 RID: 15184
		public long timeExist;

		// Token: 0x04003B51 RID: 15185
		public static MyVector lastEff = new MyVector();

		// Token: 0x04003B52 RID: 15186
		public static MyVector newEff = new MyVector();

		// Token: 0x04003B53 RID: 15187
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

		// Token: 0x04003B54 RID: 15188
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

		// Token: 0x04003B55 RID: 15189
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

		// Token: 0x04003B56 RID: 15190
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

		// Token: 0x04003B57 RID: 15191
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

		// Token: 0x04003B58 RID: 15192
		private bool isGetTime;

		// Token: 0x04003B59 RID: 15193
		private short[] data;

		// Token: 0x04003B5A RID: 15194
		public int cLastStatusMe;

		// Token: 0x04003B5B RID: 15195
		public long cur_time_cLastStatusMe;
	}
}
