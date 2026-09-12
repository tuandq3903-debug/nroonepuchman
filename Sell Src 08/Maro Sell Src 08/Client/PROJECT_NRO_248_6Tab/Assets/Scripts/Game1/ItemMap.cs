using System;

namespace Game1
{
	// Token: 0x0200048F RID: 1167
	public class ItemMap : IMapObject
	{
		// Token: 0x060033E4 RID: 13284 RVA: 0x0032FB30 File Offset: 0x0032DD30
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

		// Token: 0x060033E5 RID: 13285 RVA: 0x0032FBB8 File Offset: 0x0032DDB8
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

		// Token: 0x060033E6 RID: 13286 RVA: 0x0032FCA6 File Offset: 0x0032DEA6
		public void setPoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.vx = xEnd - this.x >> 2;
			this.vy = yEnd - this.y >> 2;
			this.status = 2;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x0032FCE0 File Offset: 0x0032DEE0
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

		// Token: 0x060033E8 RID: 13288 RVA: 0x0032FE70 File Offset: 0x0032E070
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

		// Token: 0x060033E9 RID: 13289 RVA: 0x0033005C File Offset: 0x0032E25C
		private bool isAuraItem()
		{
			return this.template.type == 22;
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x00330070 File Offset: 0x0032E270
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

		// Token: 0x060033EB RID: 13291 RVA: 0x00330108 File Offset: 0x0032E308
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

		// Token: 0x060033EC RID: 13292 RVA: 0x00330184 File Offset: 0x0032E384
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

		// Token: 0x060033ED RID: 13293 RVA: 0x00330220 File Offset: 0x0032E420
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

		// Token: 0x060033EE RID: 13294 RVA: 0x003303A7 File Offset: 0x0032E5A7
		public int getX()
		{
			return this.x;
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x003303AF File Offset: 0x0032E5AF
		public int getY()
		{
			return this.y;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x00046F7B File Offset: 0x0004517B
		public int getH()
		{
			return 20;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x00046F7B File Offset: 0x0004517B
		public int getW()
		{
			return 20;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000034B9 File Offset: 0x000016B9
		public void stopMoving()
		{
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isInvisible()
		{
			return false;
		}

		// Token: 0x04006471 RID: 25713
		public int x;

		// Token: 0x04006472 RID: 25714
		public int y;

		// Token: 0x04006473 RID: 25715
		public int xEnd;

		// Token: 0x04006474 RID: 25716
		public int yEnd;

		// Token: 0x04006475 RID: 25717
		public int f;

		// Token: 0x04006476 RID: 25718
		public int vx;

		// Token: 0x04006477 RID: 25719
		public int vy;

		// Token: 0x04006478 RID: 25720
		public int playerId;

		// Token: 0x04006479 RID: 25721
		public int itemMapID;

		// Token: 0x0400647A RID: 25722
		public int IdCharMove;

		// Token: 0x0400647B RID: 25723
		public ItemTemplate template;

		// Token: 0x0400647C RID: 25724
		public sbyte status;

		// Token: 0x0400647D RID: 25725
		public bool isHintFocus;

		// Token: 0x0400647E RID: 25726
		public int rO;

		// Token: 0x0400647F RID: 25727
		public int xO;

		// Token: 0x04006480 RID: 25728
		public int yO;

		// Token: 0x04006481 RID: 25729
		public int angle;

		// Token: 0x04006482 RID: 25730
		public int iAngle;

		// Token: 0x04006483 RID: 25731
		public int iDot;

		// Token: 0x04006484 RID: 25732
		public int[] xArg;

		// Token: 0x04006485 RID: 25733
		public int[] yArg;

		// Token: 0x04006486 RID: 25734
		public int[] xDot;

		// Token: 0x04006487 RID: 25735
		public int[] yDot;

		// Token: 0x04006488 RID: 25736
		public int count;

		// Token: 0x04006489 RID: 25737
		public int countAura;

		// Token: 0x0400648A RID: 25738
		public int countAutoPick;

		// Token: 0x0400648B RID: 25739
		public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

		// Token: 0x0400648C RID: 25740
		public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

		// Token: 0x0400648D RID: 25741
		public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

		// Token: 0x0400648E RID: 25742
		public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
	}
}
