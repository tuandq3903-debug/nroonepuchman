using System;

namespace Game5.Assets.src.g
{
	// Token: 0x020001B0 RID: 432
	internal class Mabu : Char
	{
		// Token: 0x0600132E RID: 4910 RVA: 0x001289BE File Offset: 0x00126BBE
		public Mabu()
		{
			this.getData1();
			this.getData2();
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x001289DC File Offset: 0x00126BDC
		public void eat(int id)
		{
			this.effEat = new Effect(105, this.cx, this.cy + 20, 2, 1, -1);
			EffecMn.addEff(this.effEat);
			if (id == Char.myCharz().charID)
			{
				this.focus = Char.myCharz();
				return;
			}
			this.focus = GameScr.findCharInMap(id);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00128A38 File Offset: 0x00126C38
		public new void checkFrameTick(int[] array)
		{
			if (this.skillID == 0)
			{
				if (this.tick == 11)
				{
					this.addFoot = true;
					EffecMn.addEff(new Effect(19, this.cx, this.cy + 20, 2, 1, -1));
				}
				if (this.tick >= array.Length - 1)
				{
					this.skillID = 2;
					return;
				}
			}
			if (this.skillID == 1 && this.tick == array.Length - 1)
			{
				this.skillID = 3;
				this.cy -= 15;
				return;
			}
			this.tick++;
			if (this.tick > array.Length - 1)
			{
				this.tick = 0;
			}
			this.frame = array[this.tick];
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00128AF0 File Offset: 0x00126CF0
		public void getData1()
		{
			Mabu.data1 = null;
			Mabu.data1 = new EffectData();
			string patch = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				102.ToString(),
				"/data"
			});
			try
			{
				Mabu.data1.readData2(patch);
				Mabu.data1.img = GameCanvas.loadImage("/effectdata/" + 102.ToString() + "/img.png");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00128B94 File Offset: 0x00126D94
		public void setSkill(sbyte id, short x, short y, Char[] charHit, int[] damageHit)
		{
			this.skillID = id;
			this.xTo = (int)x;
			this.yTo = (int)y;
			this.lastDir = this.cdir;
			this.cdir = ((this.xTo > this.cx) ? 1 : -1);
			this.charAttack = charHit;
			this.damageAttack = damageHit;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00128BEC File Offset: 0x00126DEC
		public void getData2()
		{
			Mabu.data2 = null;
			Mabu.data2 = new EffectData();
			string patch = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				103.ToString(),
				"/data"
			});
			try
			{
				Mabu.data2.readData2(patch);
				Mabu.data2.img = GameCanvas.loadImage("/effectdata/" + 103.ToString() + "/img.png");
				Res.outz("read xong data");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00128C9C File Offset: 0x00126E9C
		public override void update()
		{
			if (this.focus != null)
			{
				if (this.effEat.t >= 30)
				{
					this.effEat.x += (this.cx - this.effEat.x) / 4;
					this.effEat.y += (this.cy - this.effEat.y) / 4;
					this.focus.cx = this.effEat.x;
					this.focus.cy = this.effEat.y;
					this.focus.isMabuHold = true;
				}
				else
				{
					this.effEat.trans = ((this.effEat.x > this.focus.cx) ? 1 : 0);
					this.effEat.x += (this.focus.cx - this.effEat.x) / 3;
					this.effEat.y += (this.focus.cy - this.effEat.y) / 3;
				}
			}
			if (this.skillID != -1)
			{
				if (this.skillID == 0 && this.addFoot && GameCanvas.gameTick % 2 == 0)
				{
					this.dx += ((this.xTo <= this.cx) ? -30 : 30);
					EffecMn.addEff(new Effect(103, this.cx + this.dx, this.cy + 20, 2, 1, -1)
					{
						trans = ((this.xTo <= this.cx) ? 1 : 0)
					});
					if ((this.cdir == 1 && this.cx + this.dx >= this.xTo) || (this.cdir == -1 && this.cx + this.dx <= this.xTo))
					{
						this.addFoot = false;
						this.skillID = -1;
						this.dx = 0;
						this.tick = 0;
						this.cdir = this.lastDir;
						for (int i = 0; i < this.charAttack.Length; i++)
						{
							this.charAttack[i].doInjure((long)this.damageAttack[i], 0L, false, false);
						}
					}
				}
				if (this.skillID != 3)
				{
					return;
				}
				this.xTo = this.charAttack[this.pIndex].cx;
				this.yTo = this.charAttack[this.pIndex].cy;
				this.cx += (this.xTo - this.cx) / 3;
				this.cy += (this.yTo - this.cy) / 3;
				if (GameCanvas.gameTick % 5 == 0)
				{
					EffecMn.addEff(new Effect(19, this.cx, this.cy, 2, 1, -1));
				}
				if (Res.abs(this.cx - this.xTo) <= 20 && Res.abs(this.cy - this.yTo) <= 20)
				{
					this.cx = this.xTo;
					this.cy = this.yTo;
					this.charAttack[this.pIndex].doInjure((long)this.damageAttack[this.pIndex], 0L, false, false);
					this.pIndex++;
					if (this.pIndex == this.charAttack.Length)
					{
						this.skillID = -1;
						this.pIndex = 0;
						return;
					}
				}
			}
			else
			{
				base.update();
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0012901C File Offset: 0x0012721C
		public override void paint(mGraphics g)
		{
			if (this.skillID != -1)
			{
				base.paintShadow(g);
				g.translate(0, GameCanvas.transY);
				this.checkFrameTick(Mabu.skills[(int)this.skillID]);
				if (this.skillID == 0 || this.skillID == 1)
				{
					Mabu.data1.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				else
				{
					Mabu.data2.paintFrame(g, this.frame, this.cx, this.cy + this.fy, (this.cdir != 1) ? 1 : 0, 2);
				}
				g.translate(0, -GameCanvas.transY);
				return;
			}
			base.paint(g);
		}

		// Token: 0x040024C9 RID: 9417
		public static EffectData data1;

		// Token: 0x040024CA RID: 9418
		public static EffectData data2;

		// Token: 0x040024CB RID: 9419
		private new int tick;

		// Token: 0x040024CC RID: 9420
		private int lastDir;

		// Token: 0x040024CD RID: 9421
		private bool addFoot;

		// Token: 0x040024CE RID: 9422
		private Effect effEat;

		// Token: 0x040024CF RID: 9423
		private new Char focus;

		// Token: 0x040024D0 RID: 9424
		public int xTo;

		// Token: 0x040024D1 RID: 9425
		public int yTo;

		// Token: 0x040024D2 RID: 9426
		private Char[] charAttack;

		// Token: 0x040024D3 RID: 9427
		private int[] damageAttack;

		// Token: 0x040024D4 RID: 9428
		private int dx;

		// Token: 0x040024D5 RID: 9429
		public static int[] skill1 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5,
			5
		};

		// Token: 0x040024D6 RID: 9430
		public static int[] skill2 = new int[]
		{
			0,
			0,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			9,
			9,
			9,
			10,
			10
		};

		// Token: 0x040024D7 RID: 9431
		public static int[] skill3 = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			2,
			3,
			3,
			4,
			4,
			5,
			5,
			6,
			6,
			7,
			7,
			8,
			8,
			9,
			9,
			10,
			10,
			11,
			11,
			12,
			12
		};

		// Token: 0x040024D8 RID: 9432
		public static int[] skill4 = new int[]
		{
			13,
			13,
			14,
			14,
			15,
			15,
			16,
			16
		};

		// Token: 0x040024D9 RID: 9433
		public static int[][] skills = new int[][]
		{
			Mabu.skill1,
			Mabu.skill2,
			Mabu.skill3,
			Mabu.skill4
		};

		// Token: 0x040024DA RID: 9434
		public sbyte skillID = -1;

		// Token: 0x040024DB RID: 9435
		private int frame;

		// Token: 0x040024DC RID: 9436
		private int pIndex;
	}
}
