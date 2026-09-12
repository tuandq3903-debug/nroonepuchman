using System;

namespace Game5
{
	// Token: 0x02000105 RID: 261
	public class Effect
	{
		// Token: 0x06000B8B RID: 2955 RVA: 0x000BF77C File Offset: 0x000BD97C
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

		// Token: 0x06000B8C RID: 2956 RVA: 0x000BF920 File Offset: 0x000BDB20
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

		// Token: 0x06000B8D RID: 2957 RVA: 0x000BFB4C File Offset: 0x000BDD4C
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

		// Token: 0x06000B8E RID: 2958 RVA: 0x000BFB94 File Offset: 0x000BDD94
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

		// Token: 0x06000B8F RID: 2959 RVA: 0x000BFBE0 File Offset: 0x000BDDE0
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

		// Token: 0x06000B90 RID: 2960 RVA: 0x000BFC20 File Offset: 0x000BDE20
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

		// Token: 0x06000B91 RID: 2961 RVA: 0x000BFC5D File Offset: 0x000BDE5D
		public bool isPaintz()
		{
			return !ModFunc.GiamDungLuong && this.isPaint;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000BFC74 File Offset: 0x000BDE74
		public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
		{
			if (this.isPaintz() && Effect.getEffDataById(this.effId).img != null)
			{
				Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
			}
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000BFCD0 File Offset: 0x000BDED0
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

		// Token: 0x06000B94 RID: 2964 RVA: 0x000BFD98 File Offset: 0x000BDF98
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

		// Token: 0x06000B95 RID: 2965 RVA: 0x000BFE1C File Offset: 0x000BE01C
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

		// Token: 0x0400161D RID: 5661
		public int effId;

		// Token: 0x0400161E RID: 5662
		public int typeEff;

		// Token: 0x0400161F RID: 5663
		public int indexFrom;

		// Token: 0x04001620 RID: 5664
		public int indexTo;

		// Token: 0x04001621 RID: 5665
		public bool isNearPlayer;

		// Token: 0x04001622 RID: 5666
		public const int NEAR_PLAYER = 0;

		// Token: 0x04001623 RID: 5667
		public const int LOOP_NORMAL = 1;

		// Token: 0x04001624 RID: 5668
		public const int LOOP_TRANS = 2;

		// Token: 0x04001625 RID: 5669
		public const int BACKGROUND = 3;

		// Token: 0x04001626 RID: 5670
		public const int CHAR = 4;

		// Token: 0x04001627 RID: 5671
		public const int CHAR_PET_EFF = 5;

		// Token: 0x04001628 RID: 5672
		public const int FIRE_TD = 0;

		// Token: 0x04001629 RID: 5673
		public const int BIRD = 1;

		// Token: 0x0400162A RID: 5674
		public const int FIRE_NAMEK = 2;

		// Token: 0x0400162B RID: 5675
		public const int FIRE_SAYAI = 3;

		// Token: 0x0400162C RID: 5676
		public const int FROG = 5;

		// Token: 0x0400162D RID: 5677
		public const int CA = 4;

		// Token: 0x0400162E RID: 5678
		public const int ECH = 6;

		// Token: 0x0400162F RID: 5679
		public const int TACKE = 7;

		// Token: 0x04001630 RID: 5680
		public const int RAN = 8;

		// Token: 0x04001631 RID: 5681
		public const int KHI = 9;

		// Token: 0x04001632 RID: 5682
		public const int GACON = 10;

		// Token: 0x04001633 RID: 5683
		public const int DANONG = 11;

		// Token: 0x04001634 RID: 5684
		public const int DANBUOM = 12;

		// Token: 0x04001635 RID: 5685
		public const int QUA = 13;

		// Token: 0x04001636 RID: 5686
		public const int THIENTHACH = 14;

		// Token: 0x04001637 RID: 5687
		public const int CAVOI = 15;

		// Token: 0x04001638 RID: 5688
		public const int NAM = 16;

		// Token: 0x04001639 RID: 5689
		public const int RONGTHAN = 17;

		// Token: 0x0400163A RID: 5690
		public const int BUOMBAY = 26;

		// Token: 0x0400163B RID: 5691
		public const int KHUCGO = 27;

		// Token: 0x0400163C RID: 5692
		public const int DOIBAY = 28;

		// Token: 0x0400163D RID: 5693
		public const int CONMEO = 29;

		// Token: 0x0400163E RID: 5694
		public const int LUATAT = 30;

		// Token: 0x0400163F RID: 5695
		public const int ONGCONG = 31;

		// Token: 0x04001640 RID: 5696
		public const int KHANGIA1 = 42;

		// Token: 0x04001641 RID: 5697
		public const int KHANGIA2 = 43;

		// Token: 0x04001642 RID: 5698
		public const int KHANGIA3 = 44;

		// Token: 0x04001643 RID: 5699
		public const int KHANGIA4 = 45;

		// Token: 0x04001644 RID: 5700
		public const int KHANGIA5 = 46;

		// Token: 0x04001645 RID: 5701
		public Char c;

		// Token: 0x04001646 RID: 5702
		public int t;

		// Token: 0x04001647 RID: 5703
		public int currFrame;

		// Token: 0x04001648 RID: 5704
		public int x;

		// Token: 0x04001649 RID: 5705
		public int y;

		// Token: 0x0400164A RID: 5706
		public int loop;

		// Token: 0x0400164B RID: 5707
		public int tLoop;

		// Token: 0x0400164C RID: 5708
		public int tLoopCount;

		// Token: 0x0400164D RID: 5709
		private bool isPaint = true;

		// Token: 0x0400164E RID: 5710
		public int layer;

		// Token: 0x0400164F RID: 5711
		public int isStand;

		// Token: 0x04001650 RID: 5712
		public static MyVector vEffData = new MyVector();

		// Token: 0x04001651 RID: 5713
		public int trans;

		// Token: 0x04001652 RID: 5714
		public long timeExist;

		// Token: 0x04001653 RID: 5715
		public static MyVector lastEff = new MyVector();

		// Token: 0x04001654 RID: 5716
		public static MyVector newEff = new MyVector();

		// Token: 0x04001655 RID: 5717
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

		// Token: 0x04001656 RID: 5718
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

		// Token: 0x04001657 RID: 5719
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

		// Token: 0x04001658 RID: 5720
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

		// Token: 0x04001659 RID: 5721
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

		// Token: 0x0400165A RID: 5722
		private bool isGetTime;

		// Token: 0x0400165B RID: 5723
		private short[] data;

		// Token: 0x0400165C RID: 5724
		public int cLastStatusMe;

		// Token: 0x0400165D RID: 5725
		public long cur_time_cLastStatusMe;
	}
}
