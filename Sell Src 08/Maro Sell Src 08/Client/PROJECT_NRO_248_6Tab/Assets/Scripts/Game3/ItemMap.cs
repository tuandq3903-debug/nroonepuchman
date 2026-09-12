using System;

namespace Game3
{
	// Token: 0x020002DF RID: 735
	public class ItemMap : IMapObject
	{
		// Token: 0x0600209C RID: 8348 RVA: 0x002059E8 File Offset: 0x00203BE8
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

		// Token: 0x0600209D RID: 8349 RVA: 0x00205A70 File Offset: 0x00203C70
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

		// Token: 0x0600209E RID: 8350 RVA: 0x00205B5E File Offset: 0x00203D5E
		public void setPoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.vx = xEnd - this.x >> 2;
			this.vy = yEnd - this.y >> 2;
			this.status = 2;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00205B98 File Offset: 0x00203D98
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

		// Token: 0x060020A0 RID: 8352 RVA: 0x00205D28 File Offset: 0x00203F28
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

		// Token: 0x060020A1 RID: 8353 RVA: 0x00205F14 File Offset: 0x00204114
		private bool isAuraItem()
		{
			return this.template.type == 22;
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00205F28 File Offset: 0x00204128
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

		// Token: 0x060020A3 RID: 8355 RVA: 0x00205FC0 File Offset: 0x002041C0
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

		// Token: 0x060020A4 RID: 8356 RVA: 0x0020603C File Offset: 0x0020423C
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

		// Token: 0x060020A5 RID: 8357 RVA: 0x002060D8 File Offset: 0x002042D8
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

		// Token: 0x060020A6 RID: 8358 RVA: 0x0020625F File Offset: 0x0020445F
		public int getX()
		{
			return this.x;
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00206267 File Offset: 0x00204467
		public int getY()
		{
			return this.y;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x00046F7B File Offset: 0x0004517B
		public int getH()
		{
			return 20;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x00046F7B File Offset: 0x0004517B
		public int getW()
		{
			return 20;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x000034B9 File Offset: 0x000016B9
		public void stopMoving()
		{
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isInvisible()
		{
			return false;
		}

		// Token: 0x04003F73 RID: 16243
		public int x;

		// Token: 0x04003F74 RID: 16244
		public int y;

		// Token: 0x04003F75 RID: 16245
		public int xEnd;

		// Token: 0x04003F76 RID: 16246
		public int yEnd;

		// Token: 0x04003F77 RID: 16247
		public int f;

		// Token: 0x04003F78 RID: 16248
		public int vx;

		// Token: 0x04003F79 RID: 16249
		public int vy;

		// Token: 0x04003F7A RID: 16250
		public int playerId;

		// Token: 0x04003F7B RID: 16251
		public int itemMapID;

		// Token: 0x04003F7C RID: 16252
		public int IdCharMove;

		// Token: 0x04003F7D RID: 16253
		public ItemTemplate template;

		// Token: 0x04003F7E RID: 16254
		public sbyte status;

		// Token: 0x04003F7F RID: 16255
		public bool isHintFocus;

		// Token: 0x04003F80 RID: 16256
		public int rO;

		// Token: 0x04003F81 RID: 16257
		public int xO;

		// Token: 0x04003F82 RID: 16258
		public int yO;

		// Token: 0x04003F83 RID: 16259
		public int angle;

		// Token: 0x04003F84 RID: 16260
		public int iAngle;

		// Token: 0x04003F85 RID: 16261
		public int iDot;

		// Token: 0x04003F86 RID: 16262
		public int[] xArg;

		// Token: 0x04003F87 RID: 16263
		public int[] yArg;

		// Token: 0x04003F88 RID: 16264
		public int[] xDot;

		// Token: 0x04003F89 RID: 16265
		public int[] yDot;

		// Token: 0x04003F8A RID: 16266
		public int count;

		// Token: 0x04003F8B RID: 16267
		public int countAura;

		// Token: 0x04003F8C RID: 16268
		public int countAutoPick;

		// Token: 0x04003F8D RID: 16269
		public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

		// Token: 0x04003F8E RID: 16270
		public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

		// Token: 0x04003F8F RID: 16271
		public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

		// Token: 0x04003F90 RID: 16272
		public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
	}
}
