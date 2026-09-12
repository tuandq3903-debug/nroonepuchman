using System;

namespace Game6
{
	// Token: 0x02000057 RID: 87
	public class ItemMap : IMapObject
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x000466F4 File Offset: 0x000448F4
		public ItemMap(short itemMapID, short itemTemplateID, int x, int y, int xEnd, int yEnd)
		{
			this.itemMapID = (int)itemMapID;
			this.template = ItemTemplates.get(itemTemplateID);
			this.x = xEnd;
			this.y = y;
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.vx = xEnd - x >> 2;
			this.vy = 5;
			Res.outz("playerid=  " + this.playerId.ToString() + " myid= " + Char.myCharz().charID.ToString());
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0004677C File Offset: 0x0004497C
		public ItemMap(int playerId, short itemMapID, short itemTemplateID, int x, int y, short r)
		{
			Res.outz(string.Concat(new string[]
			{
				"item map item= ",
				itemMapID.ToString(),
				" template= ",
				itemTemplateID.ToString(),
				" x= ",
				x.ToString(),
				" y= ",
				y.ToString()
			}));
			this.itemMapID = (int)itemMapID;
			this.template = ItemTemplates.get(itemTemplateID);
			Res.outz("playerid=  " + playerId.ToString() + " myid= " + Char.myCharz().charID.ToString());
			this.x = (this.xEnd = x);
			this.y = (this.yEnd = y);
			this.status = 1;
			this.playerId = playerId;
			if (this.isAuraItem())
			{
				this.rO = (int)r;
				this.setAuraItem();
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0004686A File Offset: 0x00044A6A
		public void setPoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.vx = xEnd - this.x >> 2;
			this.vy = yEnd - this.y >> 2;
			this.status = 2;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000468A4 File Offset: 0x00044AA4
		public void update()
		{
			if (this.status == 2 && this.x == this.xEnd && this.y == this.yEnd)
			{
				GameScr.vItemMap.removeElement(this);
				if (Char.myCharz().itemFocus != null && Char.myCharz().itemFocus.Equals(this))
				{
					Char.myCharz().itemFocus = null;
				}
				return;
			}
			if (this.status > 0)
			{
				if (this.vx == 0)
				{
					this.x = this.xEnd;
				}
				if (this.vy == 0)
				{
					this.y = this.yEnd;
				}
				if (this.x != this.xEnd)
				{
					this.x += this.vx;
					if ((this.vx > 0 && this.x > this.xEnd) || (this.vx < 0 && this.x < this.xEnd))
					{
						this.x = this.xEnd;
					}
				}
				if (this.y != this.yEnd)
				{
					this.y += this.vy;
					if ((this.vy > 0 && this.y > this.yEnd) || (this.vy < 0 && this.y < this.yEnd))
					{
						this.y = this.yEnd;
					}
				}
			}
			else
			{
				this.status -= 4;
				if (this.status < -12)
				{
					this.y -= 12;
					this.status = 1;
				}
			}
			if (this.isAuraItem())
			{
				this.updateAuraItemEff();
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00046A34 File Offset: 0x00044C34
		public void paint(mGraphics g)
		{
			if (!this.isAuraItem())
			{
				if (!this.isAuraItem())
				{
					if (GameCanvas.gameTick % 4 == 0)
					{
						g.drawImage(ItemMap.imageFlare, this.x, this.y + (int)this.status + 13, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					if (this.status <= 0)
					{
						SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + (int)this.status + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					if (Char.myCharz().itemFocus != null && Char.myCharz().itemFocus.Equals(this) && this.status != 2)
					{
						g.drawRegion(Mob.imgHP, 0, 24, 9, 6, 0, this.x, this.y - 17, 3);
					}
				}
				return;
			}
			g.drawImage(TileMap.bong, this.x + 3, this.y, mGraphics.VCENTER | mGraphics.HCENTER);
			if (this.status <= 0)
			{
				if (this.countAura < 10)
				{
					g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
					return;
				}
				g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
			else
			{
				if (this.countAura < 10)
				{
					g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
					return;
				}
				g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00046C20 File Offset: 0x00044E20
		private bool isAuraItem()
		{
			return this.template.type == 22;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00046C34 File Offset: 0x00044E34
		private void setAuraItem()
		{
			this.xO = this.x;
			this.yO = this.y;
			this.iDot = 120;
			this.angle = 0;
			if (!GameCanvas.lowGraphic)
			{
				this.iAngle = 360 / this.iDot;
				this.xArg = new int[this.iDot];
				this.yArg = new int[this.iDot];
				this.xDot = new int[this.iDot];
				this.yDot = new int[this.iDot];
				this.setDotPosition();
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00046CCC File Offset: 0x00044ECC
		private void updateAuraItemEff()
		{
			this.count++;
			this.countAura++;
			if (this.countAura >= 40)
			{
				this.countAura = 0;
			}
			if (this.count >= this.iDot)
			{
				this.count = 0;
			}
			if (this.count % 10 == 0 && !GameCanvas.lowGraphic)
			{
				ServerEffect.addServerEffect(114, this.x - 5, this.y - 30, 1);
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00046D48 File Offset: 0x00044F48
		public void paintAuraItemEff(mGraphics g)
		{
			if (GameCanvas.lowGraphic || !this.isAuraItem())
			{
				return;
			}
			for (int i = 0; i < this.yArg.Length; i++)
			{
				if (this.count == i)
				{
					if (this.countAura <= 20)
					{
						g.drawImage(ItemMap.imageAuraItem3, this.xDot[i], this.yDot[i] + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else
					{
						SmallImage.drawSmallImage(g, (int)this.template.iconID, this.xDot[i], this.yDot[i] + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00046DE4 File Offset: 0x00044FE4
		private void setDotPosition()
		{
			if (GameCanvas.lowGraphic)
			{
				return;
			}
			for (int i = 0; i < this.yArg.Length; i++)
			{
				this.yArg[i] = Res.abs(this.rO * Res.sin(this.angle) / 1024);
				this.xArg[i] = Res.abs(this.rO * Res.cos(this.angle) / 1024);
				if (this.angle < 90)
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else if (this.angle >= 90 && this.angle < 180)
				{
					this.xDot[i] = this.xO - this.xArg[i];
					this.yDot[i] = this.yO - this.yArg[i];
				}
				else if (this.angle >= 180 && this.angle < 270)
				{
					this.xDot[i] = this.xO - this.xArg[i];
					this.yDot[i] = this.yO + this.yArg[i];
				}
				else
				{
					this.xDot[i] = this.xO + this.xArg[i];
					this.yDot[i] = this.yO + this.yArg[i];
				}
				this.angle += this.iAngle;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00046F6B File Offset: 0x0004516B
		public int getX()
		{
			return this.x;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00046F73 File Offset: 0x00045173
		public int getY()
		{
			return this.y;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00046F7B File Offset: 0x0004517B
		public int getH()
		{
			return 20;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00046F7B File Offset: 0x0004517B
		public int getW()
		{
			return 20;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000034B9 File Offset: 0x000016B9
		public void stopMoving()
		{
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isInvisible()
		{
			return false;
		}

		// Token: 0x040007F6 RID: 2038
		public int x;

		// Token: 0x040007F7 RID: 2039
		public int y;

		// Token: 0x040007F8 RID: 2040
		public int xEnd;

		// Token: 0x040007F9 RID: 2041
		public int yEnd;

		// Token: 0x040007FA RID: 2042
		public int f;

		// Token: 0x040007FB RID: 2043
		public int vx;

		// Token: 0x040007FC RID: 2044
		public int vy;

		// Token: 0x040007FD RID: 2045
		public int playerId;

		// Token: 0x040007FE RID: 2046
		public int itemMapID;

		// Token: 0x040007FF RID: 2047
		public int IdCharMove;

		// Token: 0x04000800 RID: 2048
		public ItemTemplate template;

		// Token: 0x04000801 RID: 2049
		public sbyte status;

		// Token: 0x04000802 RID: 2050
		public bool isHintFocus;

		// Token: 0x04000803 RID: 2051
		public int rO;

		// Token: 0x04000804 RID: 2052
		public int xO;

		// Token: 0x04000805 RID: 2053
		public int yO;

		// Token: 0x04000806 RID: 2054
		public int angle;

		// Token: 0x04000807 RID: 2055
		public int iAngle;

		// Token: 0x04000808 RID: 2056
		public int iDot;

		// Token: 0x04000809 RID: 2057
		public int[] xArg;

		// Token: 0x0400080A RID: 2058
		public int[] yArg;

		// Token: 0x0400080B RID: 2059
		public int[] xDot;

		// Token: 0x0400080C RID: 2060
		public int[] yDot;

		// Token: 0x0400080D RID: 2061
		public int count;

		// Token: 0x0400080E RID: 2062
		public int countAura;

		// Token: 0x0400080F RID: 2063
		public int countAutoPick;

		// Token: 0x04000810 RID: 2064
		public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

		// Token: 0x04000811 RID: 2065
		public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

		// Token: 0x04000812 RID: 2066
		public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

		// Token: 0x04000813 RID: 2067
		public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
	}
}
