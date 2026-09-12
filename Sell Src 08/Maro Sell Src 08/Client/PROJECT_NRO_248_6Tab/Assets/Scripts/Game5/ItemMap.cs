using System;

namespace Game5
{
	// Token: 0x0200012F RID: 303
	public class ItemMap : IMapObject
	{
		// Token: 0x06000D54 RID: 3412 RVA: 0x000DB8A0 File Offset: 0x000D9AA0
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

		// Token: 0x06000D55 RID: 3413 RVA: 0x000DB928 File Offset: 0x000D9B28
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

		// Token: 0x06000D56 RID: 3414 RVA: 0x000DBA16 File Offset: 0x000D9C16
		public void setPoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.vx = xEnd - this.x >> 2;
			this.vy = yEnd - this.y >> 2;
			this.status = 2;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000DBA50 File Offset: 0x000D9C50
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

		// Token: 0x06000D58 RID: 3416 RVA: 0x000DBBE0 File Offset: 0x000D9DE0
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

		// Token: 0x06000D59 RID: 3417 RVA: 0x000DBDCC File Offset: 0x000D9FCC
		private bool isAuraItem()
		{
			return this.template.type == 22;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000DBDE0 File Offset: 0x000D9FE0
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

		// Token: 0x06000D5B RID: 3419 RVA: 0x000DBE78 File Offset: 0x000DA078
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

		// Token: 0x06000D5C RID: 3420 RVA: 0x000DBEF4 File Offset: 0x000DA0F4
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

		// Token: 0x06000D5D RID: 3421 RVA: 0x000DBF90 File Offset: 0x000DA190
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

		// Token: 0x06000D5E RID: 3422 RVA: 0x000DC117 File Offset: 0x000DA317
		public int getX()
		{
			return this.x;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x000DC11F File Offset: 0x000DA31F
		public int getY()
		{
			return this.y;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x00046F7B File Offset: 0x0004517B
		public int getH()
		{
			return 20;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00046F7B File Offset: 0x0004517B
		public int getW()
		{
			return 20;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000034B9 File Offset: 0x000016B9
		public void stopMoving()
		{
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isInvisible()
		{
			return false;
		}

		// Token: 0x04001A75 RID: 6773
		public int x;

		// Token: 0x04001A76 RID: 6774
		public int y;

		// Token: 0x04001A77 RID: 6775
		public int xEnd;

		// Token: 0x04001A78 RID: 6776
		public int yEnd;

		// Token: 0x04001A79 RID: 6777
		public int f;

		// Token: 0x04001A7A RID: 6778
		public int vx;

		// Token: 0x04001A7B RID: 6779
		public int vy;

		// Token: 0x04001A7C RID: 6780
		public int playerId;

		// Token: 0x04001A7D RID: 6781
		public int itemMapID;

		// Token: 0x04001A7E RID: 6782
		public int IdCharMove;

		// Token: 0x04001A7F RID: 6783
		public ItemTemplate template;

		// Token: 0x04001A80 RID: 6784
		public sbyte status;

		// Token: 0x04001A81 RID: 6785
		public bool isHintFocus;

		// Token: 0x04001A82 RID: 6786
		public int rO;

		// Token: 0x04001A83 RID: 6787
		public int xO;

		// Token: 0x04001A84 RID: 6788
		public int yO;

		// Token: 0x04001A85 RID: 6789
		public int angle;

		// Token: 0x04001A86 RID: 6790
		public int iAngle;

		// Token: 0x04001A87 RID: 6791
		public int iDot;

		// Token: 0x04001A88 RID: 6792
		public int[] xArg;

		// Token: 0x04001A89 RID: 6793
		public int[] yArg;

		// Token: 0x04001A8A RID: 6794
		public int[] xDot;

		// Token: 0x04001A8B RID: 6795
		public int[] yDot;

		// Token: 0x04001A8C RID: 6796
		public int count;

		// Token: 0x04001A8D RID: 6797
		public int countAura;

		// Token: 0x04001A8E RID: 6798
		public int countAutoPick;

		// Token: 0x04001A8F RID: 6799
		public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

		// Token: 0x04001A90 RID: 6800
		public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

		// Token: 0x04001A91 RID: 6801
		public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

		// Token: 0x04001A92 RID: 6802
		public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
	}
}
