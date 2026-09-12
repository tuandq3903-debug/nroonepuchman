using System;

namespace Game2
{
	// Token: 0x02000420 RID: 1056
	public class TileMap
	{
		// Token: 0x06002F46 RID: 12102 RVA: 0x002E1422 File Offset: 0x002DF622
		public static void loadBg()
		{
			TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
			if (mGraphics.zoomLevel != 1 && !Main.isIpod && !Main.isIphone4)
			{
				TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
			}
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x002E1458 File Offset: 0x002DF658
		public static bool isVoDaiMap()
		{
			return TileMap.mapID == 51 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 129 || TileMap.mapID == 130;
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x002E14A4 File Offset: 0x002DF6A4
		public static bool isTrainingMap()
		{
			return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x002E14C4 File Offset: 0x002DF6C4
		public static bool mapPhuBang()
		{
			return GameScr.phuban_Info != null && TileMap.mapID == (int)GameScr.phuban_Info.idmapPaint;
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x002E14E4 File Offset: 0x002DF6E4
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

		// Token: 0x06002F4B RID: 12107 RVA: 0x002E1524 File Offset: 0x002DF724
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

		// Token: 0x06002F4C RID: 12108 RVA: 0x002E1554 File Offset: 0x002DF754
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

		// Token: 0x06002F4D RID: 12109 RVA: 0x002E1600 File Offset: 0x002DF800
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

		// Token: 0x06002F4E RID: 12110 RVA: 0x002E1680 File Offset: 0x002DF880
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

		// Token: 0x06002F4F RID: 12111 RVA: 0x002E16B8 File Offset: 0x002DF8B8
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

		// Token: 0x06002F50 RID: 12112 RVA: 0x002E1770 File Offset: 0x002DF970
		public static bool isInAirMap()
		{
			return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x002E1790 File Offset: 0x002DF990
		public static bool isDoubleMap()
		{
			return TileMap.isMapDouble || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x002E1854 File Offset: 0x002DFA54
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

		// Token: 0x06002F53 RID: 12115 RVA: 0x002E1AEC File Offset: 0x002DFCEC
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

		// Token: 0x06002F54 RID: 12116 RVA: 0x002E1B58 File Offset: 0x002DFD58
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

		// Token: 0x06002F55 RID: 12117 RVA: 0x002E1BA4 File Offset: 0x002DFDA4
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

		// Token: 0x06002F56 RID: 12118 RVA: 0x002E1EC8 File Offset: 0x002E00C8
		public static bool isWaterEff()
		{
			return TileMap.mapID != 54 && TileMap.mapID != 55 && TileMap.mapID != 56 && TileMap.mapID != 57 && TileMap.mapID != 138 && TileMap.mapID != 167;
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x002E1F14 File Offset: 0x002E0114
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

		// Token: 0x06002F58 RID: 12120 RVA: 0x002E2088 File Offset: 0x002E0288
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

		// Token: 0x06002F59 RID: 12121 RVA: 0x002E210C File Offset: 0x002E030C
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

		// Token: 0x06002F5A RID: 12122 RVA: 0x002E2148 File Offset: 0x002E0348
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

		// Token: 0x06002F5B RID: 12123 RVA: 0x002E2190 File Offset: 0x002E0390
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

		// Token: 0x06002F5C RID: 12124 RVA: 0x002E21D8 File Offset: 0x002E03D8
		public static void setTileTypeAtPixel(int px, int py, int t)
		{
			TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x002E21FE File Offset: 0x002E03FE
		public static void killTileTypeAt(int px, int py, int t)
		{
			TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x002E2225 File Offset: 0x002E0425
		public static int tileYofPixel(int py)
		{
			return py / (int)TileMap.size * (int)TileMap.size;
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x002E2225 File Offset: 0x002E0425
		public static int tileXofPixel(int px)
		{
			return px / (int)TileMap.size * (int)TileMap.size;
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x002E2234 File Offset: 0x002E0434
		public static void loadMainTile()
		{
			if (TileMap.lastTileID != TileMap.tileID)
			{
				TileMap.getTile();
				TileMap.lastTileID = TileMap.tileID;
			}
		}

		// Token: 0x04005B51 RID: 23377
		public static int tmw;

		// Token: 0x04005B52 RID: 23378
		public static int tmh;

		// Token: 0x04005B53 RID: 23379
		public static int pxw;

		// Token: 0x04005B54 RID: 23380
		public static int pxh;

		// Token: 0x04005B55 RID: 23381
		public static int tileID;

		// Token: 0x04005B56 RID: 23382
		public static int lastTileID = -1;

		// Token: 0x04005B57 RID: 23383
		public static int[] maps;

		// Token: 0x04005B58 RID: 23384
		public static int[] types;

		// Token: 0x04005B59 RID: 23385
		public static Image[] imgTile;

		// Token: 0x04005B5A RID: 23386
		public static Image imgWaterfall;

		// Token: 0x04005B5B RID: 23387
		public static Image imgTopWaterfall;

		// Token: 0x04005B5C RID: 23388
		public static Image imgWaterflow;

		// Token: 0x04005B5D RID: 23389
		public static Image imgWaterlowN;

		// Token: 0x04005B5E RID: 23390
		public static Image imgWaterlowN2;

		// Token: 0x04005B5F RID: 23391
		public static sbyte size = 24;

		// Token: 0x04005B60 RID: 23392
		private static int bx;

		// Token: 0x04005B61 RID: 23393
		private static int dbx;

		// Token: 0x04005B62 RID: 23394
		private static int fx;

		// Token: 0x04005B63 RID: 23395
		private static int dfx;

		// Token: 0x04005B64 RID: 23396
		public static bool isMapDouble = false;

		// Token: 0x04005B65 RID: 23397
		public static string mapName = string.Empty;

		// Token: 0x04005B66 RID: 23398
		public static sbyte versionMap = 1;

		// Token: 0x04005B67 RID: 23399
		public static int mapID;

		// Token: 0x04005B68 RID: 23400
		public static int lastBgID = -1;

		// Token: 0x04005B69 RID: 23401
		public static int zoneID;

		// Token: 0x04005B6A RID: 23402
		public static int bgID;

		// Token: 0x04005B6B RID: 23403
		public static int bgType;

		// Token: 0x04005B6C RID: 23404
		public static int lastType = -1;

		// Token: 0x04005B6D RID: 23405
		public static int typeMap;

		// Token: 0x04005B6E RID: 23406
		public static sbyte planetID;

		// Token: 0x04005B6F RID: 23407
		public static sbyte lastPlanetId = -1;

		// Token: 0x04005B70 RID: 23408
		public static MyVector vGo = new MyVector();

		// Token: 0x04005B71 RID: 23409
		public static MyVector vItemBg = new MyVector();

		// Token: 0x04005B72 RID: 23410
		public static MyVector vCurrItem = new MyVector();

		// Token: 0x04005B73 RID: 23411
		public static string[] mapNames;

		// Token: 0x04005B74 RID: 23412
		public static sbyte MAP_NORMAL = 0;

		// Token: 0x04005B75 RID: 23413
		public static Image bong;

		// Token: 0x04005B76 RID: 23414
		public static Image[] bgItem = new Image[8];

		// Token: 0x04005B77 RID: 23415
		public static MyVector vObject = new MyVector();

		// Token: 0x04005B78 RID: 23416
		public static int[] offlineId = new int[]
		{
			21,
			22,
			23,
			39,
			40,
			41
		};

		// Token: 0x04005B79 RID: 23417
		public static int[] highterId = new int[]
		{
			21,
			22,
			23,
			24,
			25,
			26
		};

		// Token: 0x04005B7A RID: 23418
		public static int[] toOfflineId = new int[]
		{
			0,
			7,
			14
		};

		// Token: 0x04005B7B RID: 23419
		public static int[][] tileType;

		// Token: 0x04005B7C RID: 23420
		public static int[][][] tileIndex;

		// Token: 0x04005B7D RID: 23421
		public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

		// Token: 0x04005B7E RID: 23422
		public static int sizeMiniMap = 2;

		// Token: 0x04005B7F RID: 23423
		public static int gssx;

		// Token: 0x04005B80 RID: 23424
		public static int gssxe;

		// Token: 0x04005B81 RID: 23425
		public static int gssy;

		// Token: 0x04005B82 RID: 23426
		public static int gssye;

		// Token: 0x04005B83 RID: 23427
		public static int countx;

		// Token: 0x04005B84 RID: 23428
		public static int county;

		// Token: 0x04005B85 RID: 23429
		private static int[] colorMini = new int[]
		{
			5257738,
			8807192
		};

		// Token: 0x04005B86 RID: 23430
		public static int yWater = 0;
	}
}
