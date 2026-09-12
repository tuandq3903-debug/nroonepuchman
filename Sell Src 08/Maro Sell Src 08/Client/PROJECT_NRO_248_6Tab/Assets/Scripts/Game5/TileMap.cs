using System;

namespace Game5
{
	// Token: 0x02000198 RID: 408
	public class TileMap
	{
		// Token: 0x0600125A RID: 4698 RVA: 0x00122236 File Offset: 0x00120436
		public static void loadBg()
		{
			TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
			if (mGraphics.zoomLevel != 1 && !Main.isIpod && !Main.isIphone4)
			{
				TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0012226C File Offset: 0x0012046C
		public static bool isVoDaiMap()
		{
			return TileMap.mapID == 51 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 129 || TileMap.mapID == 130;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x001222B8 File Offset: 0x001204B8
		public static bool isTrainingMap()
		{
			return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x001222D8 File Offset: 0x001204D8
		public static bool mapPhuBang()
		{
			return GameScr.phuban_Info != null && TileMap.mapID == (int)GameScr.phuban_Info.idmapPaint;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x001222F8 File Offset: 0x001204F8
		public static BgItem getBIById(int id)
		{
			for (int i = 0; i < TileMap.vItemBg.size(); i++)
			{
				BgItem bgItem = (BgItem)TileMap.vItemBg.elementAt(i);
				if (bgItem.id == id)
				{
					return bgItem;
				}
			}
			return null;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00122338 File Offset: 0x00120538
		public static bool isOfflineMap()
		{
			for (int i = 0; i < TileMap.offlineId.Length; i++)
			{
				if (TileMap.mapID == TileMap.offlineId[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00122368 File Offset: 0x00120568
		public static bool isExistMoreOne(int id)
		{
			if (id == 156 || id == 330 || id == 345 || id == 334)
			{
				return false;
			}
			if (TileMap.mapID == 54 || TileMap.mapID == 55 || TileMap.mapID == 56 || TileMap.mapID == 57 || TileMap.mapID == 58 || TileMap.mapID == 59 || TileMap.mapID == 103)
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < TileMap.vCurrItem.size(); i++)
			{
				if (((BgItem)TileMap.vCurrItem.elementAt(i)).id == id)
				{
					num++;
				}
			}
			return num > 2;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00122414 File Offset: 0x00120614
		public static void loadTileImage()
		{
			if (TileMap.imgWaterfall == null)
			{
				TileMap.imgWaterfall = GameCanvas.loadImageRMS("/tWater/wtf.png");
			}
			if (TileMap.imgTopWaterfall == null)
			{
				TileMap.imgTopWaterfall = GameCanvas.loadImageRMS("/tWater/twtf.png");
			}
			if (TileMap.imgWaterflow == null)
			{
				TileMap.imgWaterflow = GameCanvas.loadImageRMS("/tWater/wts.png");
			}
			if (TileMap.imgWaterlowN == null)
			{
				TileMap.imgWaterlowN = GameCanvas.loadImageRMS("/tWater/wtsN.png");
			}
			if (TileMap.imgWaterlowN2 == null)
			{
				TileMap.imgWaterlowN2 = GameCanvas.loadImageRMS("/tWater/wtsN2.png");
			}
			mSystem.gcc();
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00122494 File Offset: 0x00120694
		public static void setTile(int index, int[] mapsArr, int type)
		{
			for (int i = 0; i < mapsArr.Length; i++)
			{
				if (TileMap.maps[index] == mapsArr[i])
				{
					TileMap.types[index] |= type;
					return;
				}
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x001224CC File Offset: 0x001206CC
		public static void loadMap(int tileId)
		{
			TileMap.pxh = TileMap.tmh * (int)TileMap.size;
			TileMap.pxw = TileMap.tmw * (int)TileMap.size;
			Res.outz("load tile ID= " + TileMap.tileID.ToString());
			int num = tileId - 1;
			try
			{
				for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
				{
					for (int j = 0; j < TileMap.tileType[num].Length; j++)
					{
						TileMap.setTile(i, TileMap.tileIndex[num][j], TileMap.tileType[num][j]);
					}
				}
			}
			catch (Exception)
			{
				Cout.println("Error Load Map");
				GameMidlet.instance.exit();
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00122584 File Offset: 0x00120784
		public static bool isInAirMap()
		{
			return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x001225A4 File Offset: 0x001207A4
		public static bool isDoubleMap()
		{
			return TileMap.isMapDouble || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00122668 File Offset: 0x00120868
		public static void getTile()
		{
			if (Main.typeClient == 3 || Main.typeClient == 5)
			{
				if (mGraphics.zoomLevel == 1)
				{
					TileMap.imgTile = new Image[1];
					TileMap.imgTile[0] = GameCanvas.loadImage("/t/" + TileMap.tileID.ToString() + ".png");
					return;
				}
				TileMap.imgTile = new Image[100];
				for (int i = 0; i < TileMap.imgTile.Length; i++)
				{
					TileMap.imgTile[i] = GameCanvas.loadImage(string.Concat(new string[]
					{
						"/t/",
						TileMap.tileID.ToString(),
						"/",
						(i + 1).ToString(),
						".png"
					}));
				}
				return;
			}
			else
			{
				if (mGraphics.zoomLevel == 1)
				{
					if (TileMap.imgTile != null)
					{
						for (int j = 0; j < TileMap.imgTile.Length; j++)
						{
							if (TileMap.imgTile[j] != null)
							{
								TileMap.imgTile[j].texture = null;
								TileMap.imgTile[j] = null;
							}
						}
						mSystem.gcc();
					}
					TileMap.imgTile = new Image[100];
					string empty = string.Empty;
					for (int k = 0; k < TileMap.imgTile.Length; k++)
					{
						empty = ((k >= 9) ? ("/t/" + TileMap.tileID.ToString() + "/t_" + (k + 1).ToString()) : ("/t/" + TileMap.tileID.ToString() + "/t_0" + (k + 1).ToString()));
						TileMap.imgTile[k] = GameCanvas.loadImage(empty);
					}
					return;
				}
				Image image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID.ToString() + "$1.png");
				if (image != null)
				{
					Rms.DeleteStorage("x" + mGraphics.zoomLevel.ToString() + "t" + TileMap.tileID.ToString());
					TileMap.imgTile = new Image[100];
					for (int l = 0; l < TileMap.imgTile.Length; l++)
					{
						TileMap.imgTile[l] = GameCanvas.loadImageRMS(string.Concat(new string[]
						{
							"/t/",
							TileMap.tileID.ToString(),
							"$",
							(l + 1).ToString(),
							".png"
						}));
					}
					return;
				}
				image = GameCanvas.loadImageRMS("/t/" + TileMap.tileID.ToString() + ".png");
				if (image != null)
				{
					Rms.DeleteStorage("$");
					TileMap.imgTile = new Image[1];
					TileMap.imgTile[0] = image;
				}
				return;
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00122900 File Offset: 0x00120B00
		public static void paintTile(mGraphics g, int frame, int indexX, int indexY)
		{
			if (TileMap.imgTile != null)
			{
				if (TileMap.imgTile.Length == 1)
				{
					g.drawRegion(TileMap.imgTile[0], 0, frame * (int)TileMap.size, (int)TileMap.size, (int)TileMap.size, 0, indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
					return;
				}
				g.drawImage(TileMap.imgTile[frame], indexX * (int)TileMap.size, indexY * (int)TileMap.size, 0);
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0012296C File Offset: 0x00120B6C
		public static void paintTile(mGraphics g, int frame, int x, int y, int w, int h)
		{
			if (TileMap.imgTile != null)
			{
				if (TileMap.imgTile.Length == 1)
				{
					g.drawRegion(TileMap.imgTile[0], 0, frame * w, w, w, 0, x, y, 0);
					return;
				}
				g.drawImage(TileMap.imgTile[frame], x, y, 0);
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x001229B8 File Offset: 0x00120BB8
		public static void paintTilemap(mGraphics g)
		{
			if (ModFunc.GiamDungLuong || Char.isLoadingMap)
			{
				return;
			}
			GameScr.gI().paintBgItem(g, 1);
			for (int i = 0; i < GameScr.vItemMap.size(); i++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(i)).paintAuraItemEff(g);
			}
			for (int j = GameScr.gssx; j < GameScr.gssxe; j++)
			{
				for (int k = GameScr.gssy; k < GameScr.gssye; k++)
				{
					if (j != 0 && j != TileMap.tmw - 1)
					{
						int num = TileMap.maps[k * TileMap.tmw + j] - 1;
						if ((TileMap.tileTypeAt(j, k) & 256) != 256)
						{
							if ((TileMap.tileTypeAt(j, k) & 32) == 32)
							{
								g.drawRegion(TileMap.imgWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
							}
							else if ((TileMap.tileTypeAt(j, k) & 128) == 128)
							{
								g.drawRegion(TileMap.imgTopWaterfall, 0, 24 * (GameCanvas.gameTick % 8 >> 1), 24, 24, 0, j * (int)TileMap.size, k * (int)TileMap.size, 0);
							}
							else if (TileMap.tileID != 13 || num == -1)
							{
								if (TileMap.tileID == 2 && (TileMap.tileTypeAt(j, k) & 512) == 512 && num != -1)
								{
									TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
									TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
								}
								int num5 = TileMap.tileID;
								if ((TileMap.tileTypeAt(j, k) & 16) == 16)
								{
									TileMap.bx = j * (int)TileMap.size - GameScr.cmx;
									TileMap.dbx = TileMap.bx - GameScr.gW2;
									TileMap.dfx = (int)(TileMap.size - 2) * TileMap.dbx / (int)TileMap.size;
									TileMap.fx = TileMap.dfx + GameScr.gW2;
									TileMap.paintTile(g, num, TileMap.fx + GameScr.cmx, k * (int)TileMap.size, 24, 24);
								}
								else if ((TileMap.tileTypeAt(j, k) & 512) == 512)
								{
									if (num != -1)
									{
										TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size, 24, 1);
										TileMap.paintTile(g, num, j * (int)TileMap.size, k * (int)TileMap.size + 1, 24, 24);
									}
								}
								else if (num != -1)
								{
									TileMap.paintTile(g, num, j, k);
								}
							}
						}
					}
				}
			}
			if (GameScr.cmx < 24)
			{
				for (int l = GameScr.gssy; l < GameScr.gssye; l++)
				{
					int num2 = TileMap.maps[l * TileMap.tmw + 1] - 1;
					if (num2 != -1)
					{
						TileMap.paintTile(g, num2, 0, l);
					}
				}
			}
			if (GameScr.cmx <= GameScr.cmxLim)
			{
				return;
			}
			int num3 = TileMap.tmw - 2;
			for (int m = GameScr.gssy; m < GameScr.gssye; m++)
			{
				int num4 = TileMap.maps[m * TileMap.tmw + num3] - 1;
				if (num4 != -1)
				{
					TileMap.paintTile(g, num4, num3 + 1, m);
				}
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00122CDC File Offset: 0x00120EDC
		public static bool isWaterEff()
		{
			return TileMap.mapID != 54 && TileMap.mapID != 55 && TileMap.mapID != 56 && TileMap.mapID != 57 && TileMap.mapID != 138 && TileMap.mapID != 167;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00122D28 File Offset: 0x00120F28
		public static void paintOutTilemap(mGraphics g)
		{
			if (GameCanvas.lowGraphic)
			{
				return;
			}
			int num = 0;
			for (int i = GameScr.gssx; i < GameScr.gssxe; i++)
			{
				for (int j = GameScr.gssy; j < GameScr.gssye; j++)
				{
					num++;
					if ((TileMap.tileTypeAt(i, j) & 64) == 64)
					{
						Image image = (TileMap.tileID == 5) ? TileMap.imgWaterlowN : ((TileMap.tileID != 8) ? TileMap.imgWaterflow : TileMap.imgWaterlowN2);
						if (!TileMap.isWaterEff())
						{
							g.drawRegion(image, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 1, 0);
							g.drawRegion(image, 0, 0, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 3, 0);
						}
						g.drawRegion(image, 0, (GameCanvas.gameTick % 8 >> 2) * 24, 24, 24, 0, i * (int)TileMap.size, j * (int)TileMap.size - 12, 0);
						if (TileMap.yWater == 0 && TileMap.isWaterEff())
						{
							TileMap.yWater = j * (int)TileMap.size - 12;
							int color = 16777215;
							if (GameCanvas.typeBg == 2)
							{
								color = 10871287;
							}
							else if (GameCanvas.typeBg == 4)
							{
								color = 8111470;
							}
							else if (GameCanvas.typeBg == 7)
							{
								color = 5693125;
							}
							else if (GameCanvas.typeBg == 19)
							{
								color = 16711680;
							}
							BackgroudEffect.addWater(color, TileMap.yWater + 15);
						}
					}
				}
			}
			BackgroudEffect.paintWaterAll(g);
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00122E9C File Offset: 0x0012109C
		public static void loadMapFromResource(int mapID)
		{
			DataInputStream dataInputStream = MyStream.readFile("/mymap/" + mapID.ToString());
			TileMap.tmw = (int)((ushort)dataInputStream.read());
			TileMap.tmh = (int)((ushort)dataInputStream.read());
			TileMap.maps = new int[dataInputStream.available()];
			for (int i = 0; i < TileMap.tmw * TileMap.tmh; i++)
			{
				TileMap.maps[i] = (int)((ushort)dataInputStream.read());
			}
			TileMap.types = new int[TileMap.maps.Length];
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00122F20 File Offset: 0x00121120
		public static int tileTypeAt(int x, int y)
		{
			int result;
			try
			{
				result = TileMap.types[y * TileMap.tmw + x];
			}
			catch (Exception)
			{
				result = 1000;
			}
			return result;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00122F5C File Offset: 0x0012115C
		public static int tileTypeAtPixel(int px, int py)
		{
			int result;
			try
			{
				result = TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size];
			}
			catch (Exception)
			{
				result = 1000;
			}
			return result;
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00122FA4 File Offset: 0x001211A4
		public static bool tileTypeAt(int px, int py, int t)
		{
			bool result;
			try
			{
				result = ((TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] & t) == t);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00122FEC File Offset: 0x001211EC
		public static void setTileTypeAtPixel(int px, int py, int t)
		{
			TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00123012 File Offset: 0x00121212
		public static void killTileTypeAt(int px, int py, int t)
		{
			TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00123039 File Offset: 0x00121239
		public static int tileYofPixel(int py)
		{
			return py / (int)TileMap.size * (int)TileMap.size;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00123039 File Offset: 0x00121239
		public static int tileXofPixel(int px)
		{
			return px / (int)TileMap.size * (int)TileMap.size;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00123048 File Offset: 0x00121248
		public static void loadMainTile()
		{
			if (TileMap.lastTileID != TileMap.tileID)
			{
				TileMap.getTile();
				TileMap.lastTileID = TileMap.tileID;
			}
		}

		// Token: 0x040023D4 RID: 9172
		public static int tmw;

		// Token: 0x040023D5 RID: 9173
		public static int tmh;

		// Token: 0x040023D6 RID: 9174
		public static int pxw;

		// Token: 0x040023D7 RID: 9175
		public static int pxh;

		// Token: 0x040023D8 RID: 9176
		public static int tileID;

		// Token: 0x040023D9 RID: 9177
		public static int lastTileID = -1;

		// Token: 0x040023DA RID: 9178
		public static int[] maps;

		// Token: 0x040023DB RID: 9179
		public static int[] types;

		// Token: 0x040023DC RID: 9180
		public static Image[] imgTile;

		// Token: 0x040023DD RID: 9181
		public static Image imgWaterfall;

		// Token: 0x040023DE RID: 9182
		public static Image imgTopWaterfall;

		// Token: 0x040023DF RID: 9183
		public static Image imgWaterflow;

		// Token: 0x040023E0 RID: 9184
		public static Image imgWaterlowN;

		// Token: 0x040023E1 RID: 9185
		public static Image imgWaterlowN2;

		// Token: 0x040023E2 RID: 9186
		public static sbyte size = 24;

		// Token: 0x040023E3 RID: 9187
		private static int bx;

		// Token: 0x040023E4 RID: 9188
		private static int dbx;

		// Token: 0x040023E5 RID: 9189
		private static int fx;

		// Token: 0x040023E6 RID: 9190
		private static int dfx;

		// Token: 0x040023E7 RID: 9191
		public static bool isMapDouble = false;

		// Token: 0x040023E8 RID: 9192
		public static string mapName = string.Empty;

		// Token: 0x040023E9 RID: 9193
		public static sbyte versionMap = 1;

		// Token: 0x040023EA RID: 9194
		public static int mapID;

		// Token: 0x040023EB RID: 9195
		public static int lastBgID = -1;

		// Token: 0x040023EC RID: 9196
		public static int zoneID;

		// Token: 0x040023ED RID: 9197
		public static int bgID;

		// Token: 0x040023EE RID: 9198
		public static int bgType;

		// Token: 0x040023EF RID: 9199
		public static int lastType = -1;

		// Token: 0x040023F0 RID: 9200
		public static int typeMap;

		// Token: 0x040023F1 RID: 9201
		public static sbyte planetID;

		// Token: 0x040023F2 RID: 9202
		public static sbyte lastPlanetId = -1;

		// Token: 0x040023F3 RID: 9203
		public static MyVector vGo = new MyVector();

		// Token: 0x040023F4 RID: 9204
		public static MyVector vItemBg = new MyVector();

		// Token: 0x040023F5 RID: 9205
		public static MyVector vCurrItem = new MyVector();

		// Token: 0x040023F6 RID: 9206
		public static string[] mapNames;

		// Token: 0x040023F7 RID: 9207
		public static sbyte MAP_NORMAL = 0;

		// Token: 0x040023F8 RID: 9208
		public static Image bong;

		// Token: 0x040023F9 RID: 9209
		public static Image[] bgItem = new Image[8];

		// Token: 0x040023FA RID: 9210
		public static MyVector vObject = new MyVector();

		// Token: 0x040023FB RID: 9211
		public static int[] offlineId = new int[]
		{
			21,
			22,
			23,
			39,
			40,
			41
		};

		// Token: 0x040023FC RID: 9212
		public static int[] highterId = new int[]
		{
			21,
			22,
			23,
			24,
			25,
			26
		};

		// Token: 0x040023FD RID: 9213
		public static int[] toOfflineId = new int[]
		{
			0,
			7,
			14
		};

		// Token: 0x040023FE RID: 9214
		public static int[][] tileType;

		// Token: 0x040023FF RID: 9215
		public static int[][][] tileIndex;

		// Token: 0x04002400 RID: 9216
		public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

		// Token: 0x04002401 RID: 9217
		public static int sizeMiniMap = 2;

		// Token: 0x04002402 RID: 9218
		public static int gssx;

		// Token: 0x04002403 RID: 9219
		public static int gssxe;

		// Token: 0x04002404 RID: 9220
		public static int gssy;

		// Token: 0x04002405 RID: 9221
		public static int gssye;

		// Token: 0x04002406 RID: 9222
		public static int countx;

		// Token: 0x04002407 RID: 9223
		public static int county;

		// Token: 0x04002408 RID: 9224
		private static int[] colorMini = new int[]
		{
			5257738,
			8807192
		};

		// Token: 0x04002409 RID: 9225
		public static int yWater = 0;
	}
}
