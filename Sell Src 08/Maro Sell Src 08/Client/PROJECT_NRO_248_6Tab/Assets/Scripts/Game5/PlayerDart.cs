using System;

namespace Game5
{
	// Token: 0x02000168 RID: 360
	public class PlayerDart
	{
		// Token: 0x06001065 RID: 4197 RVA: 0x0010F8C4 File Offset: 0x0010DAC4
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

		// Token: 0x06001066 RID: 4198 RVA: 0x0010F95D File Offset: 0x0010DB5D
		public void setAngle(int angle)
		{
			this.angle = angle;
			this.vx = this.va * Res.cos(angle) >> 10;
			this.vy = this.va * Res.sin(angle) >> 10;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0010F994 File Offset: 0x0010DB94
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

		// Token: 0x06001068 RID: 4200 RVA: 0x0010FD10 File Offset: 0x0010DF10
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

		// Token: 0x06001069 RID: 4201 RVA: 0x0010FE1C File Offset: 0x0010E01C
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

		// Token: 0x04002126 RID: 8486
		public Char charBelong;

		// Token: 0x04002127 RID: 8487
		public DartInfo info;

		// Token: 0x04002128 RID: 8488
		public MyVector darts = new MyVector();

		// Token: 0x04002129 RID: 8489
		public int angle;

		// Token: 0x0400212A RID: 8490
		public int vx;

		// Token: 0x0400212B RID: 8491
		public int vy;

		// Token: 0x0400212C RID: 8492
		public int va;

		// Token: 0x0400212D RID: 8493
		public int x;

		// Token: 0x0400212E RID: 8494
		public int y;

		// Token: 0x0400212F RID: 8495
		public int z;

		// Token: 0x04002130 RID: 8496
		private int life;

		// Token: 0x04002131 RID: 8497
		private int dx;

		// Token: 0x04002132 RID: 8498
		private int dy;

		// Token: 0x04002133 RID: 8499
		public bool isActive = true;

		// Token: 0x04002134 RID: 8500
		public bool isSpeedUp;

		// Token: 0x04002135 RID: 8501
		public SkillPaint skillPaint;
	}
}
