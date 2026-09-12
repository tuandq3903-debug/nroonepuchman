using System;

namespace Game6
{
	// Token: 0x02000088 RID: 136
	public class Npc : Char
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x0005CE54 File Offset: 0x0005B054
		public Npc(int npcId, int status, int cx, int cy, int templateId, int avatar)
		{
			this.isShadown = true;
			this.npcId = npcId;
			this.avatar = avatar;
			this.cx = cx;
			this.cy = cy;
			this.xSd = cx;
			this.ySd = cy;
			this.statusMe = status;
			if (npcId != -1)
			{
				this.template = Npc.arrNpcTemplate[templateId];
			}
			if (templateId == 23 || templateId == 42)
			{
				this.ch = 45;
			}
			if (templateId == 51)
			{
				this.isShadown = false;
				this.duaHauIndex = status;
			}
			if (this.template != null)
			{
				if (this.template.name == null)
				{
					this.template.name = string.Empty;
				}
				this.template.name = Res.changeString(this.template.name);
			}
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0005CF2C File Offset: 0x0005B12C
		public void setStatus(sbyte s, int sc)
		{
			this.duaHauIndex = (int)s;
			this.last = (this.cur = mSystem.currentTimeMillis());
			this.seconds = sc;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0005CF5C File Offset: 0x0005B15C
		public static void clearEffTask()
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				npc.effTask = null;
				npc.indexEffTask = -1;
			}
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0005CF9C File Offset: 0x0005B19C
		public override void update()
		{
			if (this.template.npcTemplateId == 51)
			{
				this.cur = mSystem.currentTimeMillis();
				if (this.cur - this.last >= 1000L)
				{
					this.seconds--;
					this.last = this.cur;
					if (this.seconds < 0)
					{
						this.seconds = 0;
					}
				}
			}
			if (this.isShadown)
			{
				base.updateShadown();
			}
			if (this.effTask == null)
			{
				sbyte[] array = new sbyte[]
				{
					-1,
					9,
					9,
					10,
					10,
					11,
					11
				};
				if (Char.myCharz().ctaskId >= 9 && Char.myCharz().ctaskId <= 10 && Char.myCharz().nClass.classId > 0 && (int)array[Char.myCharz().nClass.classId] == this.template.npcTemplateId)
				{
					if (Char.myCharz().taskMaint == null)
					{
						this.effTask = GameScr.efs[57];
						this.indexEffTask = 0;
					}
					else if (Char.myCharz().taskMaint != null && Char.myCharz().taskMaint.index + 1 == Char.myCharz().taskMaint.subNames.Length)
					{
						this.effTask = GameScr.efs[62];
						this.indexEffTask = 0;
					}
				}
				else
				{
					sbyte taskNpcId = GameScr.getTaskNpcId();
					if (Char.myCharz().taskMaint == null && (int)taskNpcId == this.template.npcTemplateId)
					{
						this.indexEffTask = 0;
					}
					else if (Char.myCharz().taskMaint != null && (int)taskNpcId == this.template.npcTemplateId)
					{
						if (Char.myCharz().taskMaint.index + 1 == Char.myCharz().taskMaint.subNames.Length)
						{
							this.effTask = GameScr.efs[98];
						}
						else
						{
							this.effTask = GameScr.efs[98];
						}
						this.indexEffTask = 0;
					}
				}
			}
			base.update();
			if (TileMap.mapID != 51)
			{
				return;
			}
			if (this.cx > Char.myCharz().cx)
			{
				this.cdir = -1;
			}
			else
			{
				this.cdir = 1;
			}
			if (this.template.npcTemplateId % 2 == 0)
			{
				if (this.cf == 1)
				{
					this.cf = 0;
					return;
				}
				this.cf = 1;
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0005D1E0 File Offset: 0x0005B3E0
		public void PaintAuraBehind(mGraphics g)
		{
			if (!Char.isPaintAura && this.idAura > -1 && !GameCanvas.panel.isShow)
			{
				FrameImage fraImage = mSystem.getFraImage("aura_" + this.idAura.ToString() + "_0");
				if (fraImage != null)
				{
					fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, this.cx, this.cy, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				}
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x0005D268 File Offset: 0x0005B468
		public void PaintAuraFront(mGraphics g)
		{
			if (Char.isPaintAura && this.idAura > -1 && !GameCanvas.panel.isShow && !GameCanvas.lowGraphic)
			{
				FrameImage fraImage = mSystem.getFraImage("aura_" + this.idAura.ToString() + "_1");
				if (fraImage != null)
				{
					fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, this.cx, this.cy + 2, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				}
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0005D2F8 File Offset: 0x0005B4F8
		public override void paint(mGraphics g)
		{
			if (Char.isLoadingMap || this.isHide || !base.isPaint() || this.statusMe == 15)
			{
				return;
			}
			if (this.cTypePk != 0)
			{
				base.paint(g);
				return;
			}
			if (this.template == null)
			{
				return;
			}
			if (this.template.npcTemplateId != 4 && this.template.npcTemplateId != 51 && this.template.npcTemplateId != 50)
			{
				g.drawImage(TileMap.bong, this.cx, this.cy, 3);
			}
			if (this.template.npcTemplateId == 3)
			{
				SmallImage.drawSmallImage(g, 265, this.cx, this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				if (Char.myCharz().npcFocus != null && Char.myCharz().npcFocus.Equals(this) && ChatPopup.currChatPopup == null)
				{
					g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch + 4, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				this.dyEff = 60;
			}
			else if (this.template.npcTemplateId != 4)
			{
				if (this.template.npcTemplateId == 50 || this.template.npcTemplateId == 51)
				{
					if (this.duahau != null)
					{
						if (this.template.npcTemplateId == 50 && Npc.mabuEff)
						{
							Npc.tMabuEff++;
							if (GameCanvas.gameTick % 3 == 0)
							{
								EffecMn.addEff(new Effect(19, this.cx + Res.random(-50, 50), this.cy, 2, 1, -1));
							}
							if (GameCanvas.gameTick % 15 == 0)
							{
								EffecMn.addEff(new Effect(18, this.cx + Res.random(-5, 5), this.cy + Res.random(-90, 0), 2, 1, -1));
							}
							if (Npc.tMabuEff == 100)
							{
								GameScr.gI().activeSuperPower(this.cx, this.cy);
							}
							if (Npc.tMabuEff == 110)
							{
								Npc.mabuEff = false;
								this.template.npcTemplateId = 4;
							}
						}
						int num = 0;
						if (SmallImage.imgNew[this.duahau[this.duaHauIndex]] != null && SmallImage.imgNew[this.duahau[this.duaHauIndex]].img != null)
						{
							num = mGraphics.getImageHeight(SmallImage.imgNew[this.duahau[this.duaHauIndex]].img);
						}
						SmallImage.drawSmallImage(g, this.duahau[this.duaHauIndex], this.cx + Res.random(-1, 1), this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
						if (Char.myCharz().npcFocus != null && Char.myCharz().npcFocus.Equals(this))
						{
							if (ChatPopup.currChatPopup == null)
							{
								g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - 9 + 16 - num, mGraphics.BOTTOM | mGraphics.HCENTER);
							}
							mFont.tahoma_7b_white.drawString(g, NinjaUtil.getTime(this.seconds), this.cx, this.cy - this.ch - 16 - mFont.tahoma_7.getHeight() - 20 - num + 16, mFont.CENTER, mFont.tahoma_7b_dark);
						}
						else
						{
							mFont.tahoma_7b_white.drawString(g, NinjaUtil.getTime(this.seconds), this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight() - 20 - num + 16, mFont.CENTER, mFont.tahoma_7b_dark);
						}
					}
				}
				else if (this.template.npcTemplateId == 6)
				{
					SmallImage.drawSmallImage(g, 545, this.cx, this.cy + 5, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					if (Char.myCharz().npcFocus != null && Char.myCharz().npcFocus.Equals(this) && ChatPopup.currChatPopup == null)
					{
						g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - this.ch - 9, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					mFont.tahoma_7b_white.drawString(g, TileMap.zoneID.ToString() + string.Empty, this.cx, this.cy - this.ch + 19 - mFont.tahoma_7.getHeight(), mFont.CENTER);
				}
				else
				{
					this.PaintAuraBehind(g);
					this.PaintAuraFront(g);
					int headId = this.template.headId;
					int legId = this.template.legId;
					int bodyId = this.template.bodyId;
					Part part = GameScr.parts[headId];
					Part part2 = GameScr.parts[legId];
					Part part3 = GameScr.parts[bodyId];
					if (this.cdir == 1)
					{
						SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[this.cf][0][0]].id, this.cx + Char.CharInfo[this.cf][0][1] + (int)part.pi[Char.CharInfo[this.cf][0][0]].dx, this.cy - Char.CharInfo[this.cf][0][2] + (int)part.pi[Char.CharInfo[this.cf][0][0]].dy, 0, 0);
						SmallImage.drawSmallImage(g, (int)part2.pi[Char.CharInfo[this.cf][1][0]].id, this.cx + Char.CharInfo[this.cf][1][1] + (int)part2.pi[Char.CharInfo[this.cf][1][0]].dx, this.cy - Char.CharInfo[this.cf][1][2] + (int)part2.pi[Char.CharInfo[this.cf][1][0]].dy, 0, 0);
						SmallImage.drawSmallImage(g, (int)part3.pi[Char.CharInfo[this.cf][2][0]].id, this.cx + Char.CharInfo[this.cf][2][1] + (int)part3.pi[Char.CharInfo[this.cf][2][0]].dx, this.cy - Char.CharInfo[this.cf][2][2] + (int)part3.pi[Char.CharInfo[this.cf][2][0]].dy, 0, 0);
					}
					else
					{
						SmallImage.drawSmallImage(g, (int)part.pi[Char.CharInfo[this.cf][0][0]].id, this.cx - Char.CharInfo[this.cf][0][1] - (int)part.pi[Char.CharInfo[this.cf][0][0]].dx, this.cy - Char.CharInfo[this.cf][0][2] + (int)part.pi[Char.CharInfo[this.cf][0][0]].dy, 2, 24);
						SmallImage.drawSmallImage(g, (int)part2.pi[Char.CharInfo[this.cf][1][0]].id, this.cx - Char.CharInfo[this.cf][1][1] - (int)part2.pi[Char.CharInfo[this.cf][1][0]].dx, this.cy - Char.CharInfo[this.cf][1][2] + (int)part2.pi[Char.CharInfo[this.cf][1][0]].dy, 2, 24);
						SmallImage.drawSmallImage(g, (int)part3.pi[Char.CharInfo[this.cf][2][0]].id, this.cx - Char.CharInfo[this.cf][2][1] - (int)part3.pi[Char.CharInfo[this.cf][2][0]].dx, this.cy - Char.CharInfo[this.cf][2][2] + (int)part3.pi[Char.CharInfo[this.cf][2][0]].dy, 2, 24);
					}
					if (TileMap.mapID != 51)
					{
						int num2 = 15;
						if (this.template.npcTemplateId == 47)
						{
							num2 = 47;
						}
						if (Char.myCharz().npcFocus != null && Char.myCharz().npcFocus.Equals(this) && ChatPopup.currChatPopup == null)
						{
							int num3 = 0;
							int num4 = 0;
							if (Char.myCharz().npcFocus.template.npcTemplateId == 28 || Char.myCharz().npcFocus.template.npcTemplateId == 41)
							{
								num3 = 3;
								num4 = -12;
							}
							g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx + num3, this.cy - this.ch - (num2 - 8) + num4, mGraphics.BOTTOM | mGraphics.HCENTER);
						}
					}
					this.dyEff = 65;
				}
			}
			if (this.indexEffTask < 0 || this.effTask == null || this.cTypePk != 0)
			{
				return;
			}
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy + this.effTask.arrEfInfo[this.indexEffTask].dy - this.dyEff, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			if (GameCanvas.gameTick % 2 == 0)
			{
				this.indexEffTask++;
				if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
				{
					this.indexEffTask = 0;
				}
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x0005DCB4 File Offset: 0x0005BEB4
		public new void paintName(mGraphics g)
		{
			if (Char.isLoadingMap || this.isHide || !base.isPaint() || this.statusMe == 15 || this.template == null)
			{
				return;
			}
			string npcName = this.template.name;
			if (ModFunc.isShowID)
			{
				npcName = "[" + this.template.npcTemplateId.ToString() + "] " + npcName;
			}
			if (this.template.npcTemplateId == 3)
			{
				if (Char.myCharz().npcFocus != null && Char.myCharz().npcFocus.Equals(this))
				{
					mFont.tahoma_7_yellow.drawStringBorder(g, npcName, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
				}
				else
				{
					mFont.tahoma_7_yellow.drawStringBorder(g, npcName, this.cx, this.cy - this.ch - 3 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
				}
				this.dyEff = 60;
				return;
			}
			if (this.template.npcTemplateId == 4)
			{
				return;
			}
			if (this.template.npcTemplateId == 50 || this.template.npcTemplateId == 51)
			{
				if (this.duahau != null)
				{
					int num = 0;
					if (SmallImage.imgNew[this.duahau[this.duaHauIndex]] != null && SmallImage.imgNew[this.duahau[this.duaHauIndex]].img != null)
					{
						num = mGraphics.getImageHeight(SmallImage.imgNew[this.duahau[this.duaHauIndex]].img);
					}
					if (Char.myCharz().npcFocus != null && Char.myCharz().npcFocus.Equals(this))
					{
						mFont.tahoma_7_yellow.drawStringBorder(g, npcName, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - num, mFont.CENTER, mFont.tahoma_7_grey);
						return;
					}
					mFont.tahoma_7_yellow.drawStringBorder(g, npcName, this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight() - num + 16, mFont.CENTER, mFont.tahoma_7_grey);
				}
				return;
			}
			if (this.template.npcTemplateId != 6)
			{
				if (TileMap.mapID != 51)
				{
					int num2 = 15;
					if (this.template.npcTemplateId == 47)
					{
						num2 = 47;
					}
					if (Char.myCharz().npcFocus != null && Char.myCharz().npcFocus.Equals(this))
					{
						if (TileMap.mapID != 113)
						{
							int num3 = 0;
							int num4 = 0;
							if (Char.myCharz().npcFocus.template.npcTemplateId == 28 || Char.myCharz().npcFocus.template.npcTemplateId == 41)
							{
								num3 = 3;
								num4 = -12;
							}
							mFont.tahoma_7_yellow.drawStringBorder(g, npcName, this.cx + num3, this.cy - this.ch - mFont.tahoma_7.getHeight() - num2 + num4, mFont.CENTER, mFont.tahoma_7_grey);
						}
					}
					else
					{
						num2 = 8;
						if (this.template.npcTemplateId == 47)
						{
							num2 = 40;
						}
						if (TileMap.mapID != 113)
						{
							int num5 = 0;
							int num6 = 0;
							if (this.template.npcTemplateId == 28 || this.template.npcTemplateId == 41)
							{
								num5 = 3;
								num6 = -12;
							}
							mFont.tahoma_7_yellow.drawStringBorder(g, npcName, this.cx + num5, this.cy - this.ch - num2 - mFont.tahoma_7.getHeight() + num6, mFont.CENTER, mFont.tahoma_7_grey);
						}
					}
				}
				this.dyEff = 65;
				return;
			}
			if (Char.myCharz().npcFocus != null && Char.myCharz().npcFocus.Equals(this))
			{
				mFont.tahoma_7_yellow.drawStringBorder(g, npcName, this.cx, this.cy - this.ch - mFont.tahoma_7.getHeight() - 16, mFont.CENTER, mFont.tahoma_7_grey);
				return;
			}
			mFont.tahoma_7_yellow.drawStringBorder(g, npcName, this.cx, this.cy - this.ch - 8 - mFont.tahoma_7.getHeight(), mFont.CENTER, mFont.tahoma_7_grey);
		}

		// Token: 0x04000D06 RID: 3334
		public const sbyte BINH_KHI = 0;

		// Token: 0x04000D07 RID: 3335
		public const sbyte PHONG_CU = 1;

		// Token: 0x04000D08 RID: 3336
		public const sbyte TRANG_SUC = 2;

		// Token: 0x04000D09 RID: 3337
		public const sbyte DUOC_PHAM = 3;

		// Token: 0x04000D0A RID: 3338
		public const sbyte TAP_HOA = 4;

		// Token: 0x04000D0B RID: 3339
		public const sbyte THU_KHO = 5;

		// Token: 0x04000D0C RID: 3340
		public const sbyte DA_LUYEN = 6;

		// Token: 0x04000D0D RID: 3341
		public NpcTemplate template;

		// Token: 0x04000D0E RID: 3342
		public int npcId;

		// Token: 0x04000D0F RID: 3343
		public bool isFocus = true;

		// Token: 0x04000D10 RID: 3344
		public static NpcTemplate[] arrNpcTemplate;

		// Token: 0x04000D11 RID: 3345
		public int sys;

		// Token: 0x04000D12 RID: 3346
		public new bool isHide;

		// Token: 0x04000D13 RID: 3347
		private int duaHauIndex;

		// Token: 0x04000D14 RID: 3348
		private int dyEff;

		// Token: 0x04000D15 RID: 3349
		public static bool mabuEff;

		// Token: 0x04000D16 RID: 3350
		public static int tMabuEff;

		// Token: 0x04000D17 RID: 3351
		private static int[] shock_x = new int[]
		{
			1,
			-1,
			1,
			-1
		};

		// Token: 0x04000D18 RID: 3352
		private static int[] shock_y = new int[]
		{
			1,
			-1,
			-1,
			1
		};

		// Token: 0x04000D19 RID: 3353
		public static int shock_scr;

		// Token: 0x04000D1A RID: 3354
		public int[] duahau;

		// Token: 0x04000D1B RID: 3355
		public new int seconds;

		// Token: 0x04000D1C RID: 3356
		public new long last;

		// Token: 0x04000D1D RID: 3357
		public new long cur;

		// Token: 0x04000D1E RID: 3358
		public int idItem;

		// Token: 0x04000D1F RID: 3359
		public short idAura = -1;
	}
}
