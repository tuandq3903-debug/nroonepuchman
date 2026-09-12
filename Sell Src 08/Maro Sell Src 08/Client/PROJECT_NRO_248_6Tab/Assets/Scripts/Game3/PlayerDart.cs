using System;

namespace Game3
{
	// Token: 0x02000318 RID: 792
	public class PlayerDart
	{
		// Token: 0x060023AD RID: 9133 RVA: 0x00239A0C File Offset: 0x00237C0C
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

		// Token: 0x060023AE RID: 9134 RVA: 0x00239AA5 File Offset: 0x00237CA5
		public void setAngle(int angle)
		{
			this.angle = angle;
			this.vx = this.va * Res.cos(angle) >> 10;
			this.vy = this.va * Res.sin(angle) >> 10;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x00239ADC File Offset: 0x00237CDC
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

		// Token: 0x060023B0 RID: 9136 RVA: 0x00239E58 File Offset: 0x00238058
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

		// Token: 0x060023B1 RID: 9137 RVA: 0x00239F64 File Offset: 0x00238164
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

		// Token: 0x04004624 RID: 17956
		public Char charBelong;

		// Token: 0x04004625 RID: 17957
		public DartInfo info;

		// Token: 0x04004626 RID: 17958
		public MyVector darts = new MyVector();

		// Token: 0x04004627 RID: 17959
		public int angle;

		// Token: 0x04004628 RID: 17960
		public int vx;

		// Token: 0x04004629 RID: 17961
		public int vy;

		// Token: 0x0400462A RID: 17962
		public int va;

		// Token: 0x0400462B RID: 17963
		public int x;

		// Token: 0x0400462C RID: 17964
		public int y;

		// Token: 0x0400462D RID: 17965
		public int z;

		// Token: 0x0400462E RID: 17966
		private int life;

		// Token: 0x0400462F RID: 17967
		private int dx;

		// Token: 0x04004630 RID: 17968
		private int dy;

		// Token: 0x04004631 RID: 17969
		public bool isActive = true;

		// Token: 0x04004632 RID: 17970
		public bool isSpeedUp;

		// Token: 0x04004633 RID: 17971
		public SkillPaint skillPaint;
	}
}
