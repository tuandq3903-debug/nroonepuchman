using System;

namespace Game4
{
	// Token: 0x02000207 RID: 519
	public class ItemMap : IMapObject
	{
		// Token: 0x060016F8 RID: 5880 RVA: 0x00170944 File Offset: 0x0016EB44
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

		// Token: 0x060016F9 RID: 5881 RVA: 0x001709CC File Offset: 0x0016EBCC
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

		// Token: 0x060016FA RID: 5882 RVA: 0x00170ABA File Offset: 0x0016ECBA
		public void setPoint(int xEnd, int yEnd)
		{
			this.xEnd = xEnd;
			this.yEnd = yEnd;
			this.vx = xEnd - this.x >> 2;
			this.vy = yEnd - this.y >> 2;
			this.status = 2;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00170AF4 File Offset: 0x0016ECF4
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

		// Token: 0x060016FC RID: 5884 RVA: 0x00170C84 File Offset: 0x0016EE84
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

		// Token: 0x060016FD RID: 5885 RVA: 0x00170E70 File Offset: 0x0016F070
		private bool isAuraItem()
		{
			return this.template.type == 22;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00170E84 File Offset: 0x0016F084
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

		// Token: 0x060016FF RID: 5887 RVA: 0x00170F1C File Offset: 0x0016F11C
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

		// Token: 0x06001700 RID: 5888 RVA: 0x00170F98 File Offset: 0x0016F198
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

		// Token: 0x06001701 RID: 5889 RVA: 0x00171034 File Offset: 0x0016F234
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

		// Token: 0x06001702 RID: 5890 RVA: 0x001711BB File Offset: 0x0016F3BB
		public int getX()
		{
			return this.x;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x001711C3 File Offset: 0x0016F3C3
		public int getY()
		{
			return this.y;
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00046F7B File Offset: 0x0004517B
		public int getH()
		{
			return 20;
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00046F7B File Offset: 0x0004517B
		public int getW()
		{
			return 20;
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000034B9 File Offset: 0x000016B9
		public void stopMoving()
		{
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0001269B File Offset: 0x0001089B
		public bool isInvisible()
		{
			return false;
		}

		// Token: 0x04002CF4 RID: 11508
		public int x;

		// Token: 0x04002CF5 RID: 11509
		public int y;

		// Token: 0x04002CF6 RID: 11510
		public int xEnd;

		// Token: 0x04002CF7 RID: 11511
		public int yEnd;

		// Token: 0x04002CF8 RID: 11512
		public int f;

		// Token: 0x04002CF9 RID: 11513
		public int vx;

		// Token: 0x04002CFA RID: 11514
		public int vy;

		// Token: 0x04002CFB RID: 11515
		public int playerId;

		// Token: 0x04002CFC RID: 11516
		public int itemMapID;

		// Token: 0x04002CFD RID: 11517
		public int IdCharMove;

		// Token: 0x04002CFE RID: 11518
		public ItemTemplate template;

		// Token: 0x04002CFF RID: 11519
		public sbyte status;

		// Token: 0x04002D00 RID: 11520
		public bool isHintFocus;

		// Token: 0x04002D01 RID: 11521
		public int rO;

		// Token: 0x04002D02 RID: 11522
		public int xO;

		// Token: 0x04002D03 RID: 11523
		public int yO;

		// Token: 0x04002D04 RID: 11524
		public int angle;

		// Token: 0x04002D05 RID: 11525
		public int iAngle;

		// Token: 0x04002D06 RID: 11526
		public int iDot;

		// Token: 0x04002D07 RID: 11527
		public int[] xArg;

		// Token: 0x04002D08 RID: 11528
		public int[] yArg;

		// Token: 0x04002D09 RID: 11529
		public int[] xDot;

		// Token: 0x04002D0A RID: 11530
		public int[] yDot;

		// Token: 0x04002D0B RID: 11531
		public int count;

		// Token: 0x04002D0C RID: 11532
		public int countAura;

		// Token: 0x04002D0D RID: 11533
		public int countAutoPick;

		// Token: 0x04002D0E RID: 11534
		public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

		// Token: 0x04002D0F RID: 11535
		public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

		// Token: 0x04002D10 RID: 11536
		public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

		// Token: 0x04002D11 RID: 11537
		public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
	}
}
