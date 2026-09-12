using System;

namespace Game1
{
	// Token: 0x020004C8 RID: 1224
	public class PlayerDart
	{
		// Token: 0x060036F5 RID: 14069 RVA: 0x00363B54 File Offset: 0x00361D54
		public PlayerDart(Char charBelong, int dartType, SkillPaint sp, int x, int y)
		{
			this.skillPaint = sp;
			this.charBelong = charBelong;
			this.info = GameScr.darts[dartType];
			this.va = this.info.va;
			this.x = x;
			this.y = y;
			IMapObject mapObject3;
			if (charBelong.mobFocus == null)
			{
				IMapObject mapObject2 = charBelong.charFocus;
				mapObject3 = mapObject2;
			}
			else
			{
				IMapObject mapObject2 = charBelong.mobFocus;
				mapObject3 = mapObject2;
			}
			IMapObject mapObject = mapObject3;
			this.setAngle(Res.angle(mapObject.getX() - x, mapObject.getY() - y));
		}

		// Token: 0x060036F6 RID: 14070 RVA: 0x00363BED File Offset: 0x00361DED
		public void setAngle(int angle)
		{
			this.angle = angle;
			this.vx = this.va * Res.cos(angle) >> 10;
			this.vy = this.va * Res.sin(angle) >> 10;
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x00363C24 File Offset: 0x00361E24
		public void update()
		{
			if (!this.isActive)
			{
				return;
			}
			if (this.charBelong.mobFocus == null && this.charBelong.charFocus == null)
			{
				this.endMe();
				return;
			}
			IMapObject mapObject3;
			if (this.charBelong.mobFocus == null)
			{
				IMapObject mapObject2 = this.charBelong.charFocus;
				mapObject3 = mapObject2;
			}
			else
			{
				IMapObject mapObject2 = this.charBelong.mobFocus;
				mapObject3 = mapObject2;
			}
			IMapObject mapObject = (IMapObject)mapObject3;
			for (int i = 0; i < (int)this.info.nUpdate; i++)
			{
				if (this.info.tail.Length != 0)
				{
					this.darts.addElement(new SmallDart(this.x, this.y));
				}
				int num = (this.charBelong.getX() <= mapObject.getX()) ? -10 : 10;
				this.dx = mapObject.getX() + num - this.x;
				this.dy = mapObject.getY() - mapObject.getH() / 2 - this.y;
				this.life++;
				if (Res.abs(this.dx) < 20 && Res.abs(this.dy) < 20)
				{
					if (this.charBelong.charFocus != null && this.charBelong.charFocus.me)
					{
						this.charBelong.charFocus.doInjure(this.charBelong.charFocus.damHP, 0L, this.charBelong.charFocus.isCrit, this.charBelong.charFocus.isMob);
					}
					this.endMe();
					return;
				}
				int num2 = Res.angle(this.dx, this.dy);
				if (Math.abs(num2 - this.angle) < 90 || this.dx * this.dx + this.dy * this.dy > 4096)
				{
					if (Math.abs(num2 - this.angle) < 15)
					{
						this.angle = num2;
					}
					else if ((num2 - this.angle >= 0 && num2 - this.angle < 180) || num2 - this.angle < -180)
					{
						this.angle = Res.fixangle(this.angle + 15);
					}
					else
					{
						this.angle = Res.fixangle(this.angle - 15);
					}
				}
				if (!this.isSpeedUp && this.va < 8192)
				{
					this.va += 1024;
				}
				this.vx = this.va * Res.cos(this.angle) >> 10;
				this.vy = this.va * Res.sin(this.angle) >> 10;
				this.dx += this.vx;
				int num3 = this.dx >> 10;
				this.x += num3;
				this.dx &= 1023;
				this.dy += this.vy;
				int num4 = this.dy >> 10;
				this.y += num4;
				this.dy &= 1023;
			}
			for (int j = 0; j < this.darts.size(); j++)
			{
				SmallDart smallDart = (SmallDart)this.darts.elementAt(j);
				smallDart.index++;
				if (smallDart.index >= this.info.tail.Length)
				{
					this.darts.removeElementAt(j);
				}
			}
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x00363FA0 File Offset: 0x003621A0
		private void endMe()
		{
			if (!this.charBelong.isUseSkillAfterCharge && this.x >= GameScr.cmx && this.x <= GameScr.cmx + GameCanvas.w)
			{
				SoundMn.gI().explode_1();
			}
			this.charBelong.setAttack();
			if (this.charBelong.me)
			{
				this.charBelong.saveLoadPreviousSkill();
			}
			if (this.charBelong.isUseSkillAfterCharge)
			{
				this.charBelong.isUseSkillAfterCharge = false;
				if (this.charBelong.isLockMove && this.charBelong.me && this.charBelong.statusMe != 14 && this.charBelong.statusMe != 5)
				{
					this.charBelong.isLockMove = false;
				}
				GameScr.gI().activeSuperPower(this.x, this.y);
			}
			this.charBelong.dart = null;
			this.charBelong.isCreateDark = false;
			this.charBelong.skillPaint = null;
			this.charBelong.skillPaintRandomPaint = null;
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x003640AC File Offset: 0x003622AC
		public void paint(mGraphics g)
		{
			if (!this.isActive)
			{
				return;
			}
			int num = MonsterDart.findDirIndexFromAngle(360 - this.angle);
			int num2 = (int)MonsterDart.FRAME[num];
			int transform = MonsterDart.TRANSFORM[num];
			for (int i = this.darts.size() / 2; i < this.darts.size(); i++)
			{
				SmallDart smallDart = (SmallDart)this.darts.elementAt(i);
				SmallImage.drawSmallImage(g, (int)this.info.tailBorder[smallDart.index], smallDart.x, smallDart.y, 0, 3);
			}
			int num3 = GameCanvas.gameTick % this.info.headBorder.Length;
			SmallImage.drawSmallImage(g, (int)this.info.headBorder[num3][num2], this.x, this.y, transform, 3);
			for (int j = 0; j < this.darts.size(); j++)
			{
				SmallDart smallDart2 = (SmallDart)this.darts.elementAt(j);
				SmallImage.drawSmallImage(g, (int)this.info.tail[smallDart2.index], smallDart2.x, smallDart2.y, 0, 3);
			}
			SmallImage.drawSmallImage(g, (int)this.info.head[num3][num2], this.x, this.y, transform, 3);
			for (int k = 0; k < this.darts.size(); k++)
			{
				SmallDart smallDart3 = (SmallDart)this.darts.elementAt(k);
				if (Res.abs(MonsterDart.r.nextInt(100)) < (int)this.info.xdPercent)
				{
					SmallImage.drawSmallImage(g, (int)((GameCanvas.gameTick % 2 != 0) ? this.info.xd2[smallDart3.index] : this.info.xd1[smallDart3.index]), smallDart3.x, smallDart3.y, 0, 3);
				}
			}
			g.setColor(16711680);
		}

		// Token: 0x04006B22 RID: 27426
		public Char charBelong;

		// Token: 0x04006B23 RID: 27427
		public DartInfo info;

		// Token: 0x04006B24 RID: 27428
		public MyVector darts = new MyVector();

		// Token: 0x04006B25 RID: 27429
		public int angle;

		// Token: 0x04006B26 RID: 27430
		public int vx;

		// Token: 0x04006B27 RID: 27431
		public int vy;

		// Token: 0x04006B28 RID: 27432
		public int va;

		// Token: 0x04006B29 RID: 27433
		public int x;

		// Token: 0x04006B2A RID: 27434
		public int y;

		// Token: 0x04006B2B RID: 27435
		public int z;

		// Token: 0x04006B2C RID: 27436
		private int life;

		// Token: 0x04006B2D RID: 27437
		private int dx;

		// Token: 0x04006B2E RID: 27438
		private int dy;

		// Token: 0x04006B2F RID: 27439
		public bool isActive = true;

		// Token: 0x04006B30 RID: 27440
		public bool isSpeedUp;

		// Token: 0x04006B31 RID: 27441
		public SkillPaint skillPaint;
	}
}
