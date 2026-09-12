using System;

namespace Game6
{
	// Token: 0x020000C0 RID: 192
	public class TileMap
	{
		// Token: 0x060008B6 RID: 2230 RVA: 0x0008D11E File Offset: 0x0008B31E
		public static void loadBg()
		{
			TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
			if (mGraphics.zoomLevel != 1 && !Main.isIpod && !Main.isIphone4)
			{
				TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
			}
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0008D154 File Offset: 0x0008B354
		public static bool isVoDaiMap()
		{
			return TileMap.mapID == 51 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 129 || TileMap.mapID == 130;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0008D1A0 File Offset: 0x0008B3A0
		public static bool isTrainingMap()
		{
			return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0008D1C0 File Offset: 0x0008B3C0
		public static bool mapPhuBang()
		{
			return GameScr.phuban_Info != null && TileMap.mapID == (int)GameScr.phuban_Info.idmapPaint;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0008D1E0 File Offset: 0x0008B3E0
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

		// Token: 0x060008BB RID: 2235 RVA: 0x0008D220 File Offset: 0x0008B420
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

		// Token: 0x060008BC RID: 2236 RVA: 0x0008D250 File Offset: 0x0008B450
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

		// Token: 0x060008BD RID: 2237 RVA: 0x0008D2FC File Offset: 0x0008B4FC
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

		// Token: 0x060008BE RID: 2238 RVA: 0x0008D37C File Offset: 0x0008B57C
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

		// Token: 0x060008BF RID: 2239 RVA: 0x0008D3B4 File Offset: 0x0008B5B4
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

		// Token: 0x060008C0 RID: 2240 RVA: 0x0008D46C File Offset: 0x0008B66C
		public static bool isInAirMap()
		{
			return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0008D48C File Offset: 0x0008B68C
		public static bool isDoubleMap()
		{
			return TileMap.isMapDouble || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0008D550 File Offset: 0x0008B750
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

		// Token: 0x060008C3 RID: 2243 RVA: 0x0008D7E8 File Offset: 0x0008B9E8
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

		// Token: 0x060008C4 RID: 2244 RVA: 0x0008D854 File Offset: 0x0008BA54
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

		// Token: 0x060008C5 RID: 2245 RVA: 0x0008D8A0 File Offset: 0x0008BAA0
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

		// Token: 0x060008C6 RID: 2246 RVA: 0x0008DBC4 File Offset: 0x0008BDC4
		public static bool isWaterEff()
		{
			return TileMap.mapID != 54 && TileMap.mapID != 55 && TileMap.mapID != 56 && TileMap.mapID != 57 && TileMap.mapID != 138 && TileMap.mapID != 167;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0008DC10 File Offset: 0x0008BE10
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

		// Token: 0x060008C8 RID: 2248 RVA: 0x0008DD84 File Offset: 0x0008BF84
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

		// Token: 0x060008C9 RID: 2249 RVA: 0x0008DE08 File Offset: 0x0008C008
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

		// Token: 0x060008CA RID: 2250 RVA: 0x0008DE44 File Offset: 0x0008C044
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

		// Token: 0x060008CB RID: 2251 RVA: 0x0008DE8C File Offset: 0x0008C08C
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

		// Token: 0x060008CC RID: 2252 RVA: 0x0008DED4 File Offset: 0x0008C0D4
		public static void setTileTypeAtPixel(int px, int py, int t)
		{
			TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0008DEFA File Offset: 0x0008C0FA
		public static void killTileTypeAt(int px, int py, int t)
		{
			TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0008DF21 File Offset: 0x0008C121
		public static int tileYofPixel(int py)
		{
			return py / (int)TileMap.size * (int)TileMap.size;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0008DF21 File Offset: 0x0008C121
		public static int tileXofPixel(int px)
		{
			return px / (int)TileMap.size * (int)TileMap.size;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0008DF30 File Offset: 0x0008C130
		public static void loadMainTile()
		{
			if (TileMap.lastTileID != TileMap.tileID)
			{
				TileMap.getTile();
				TileMap.lastTileID = TileMap.tileID;
			}
		}

		// Token: 0x04001155 RID: 4437
		public static int tmw;

		// Token: 0x04001156 RID: 4438
		public static int tmh;

		// Token: 0x04001157 RID: 4439
		public static int pxw;

		// Token: 0x04001158 RID: 4440
		public static int pxh;

		// Token: 0x04001159 RID: 4441
		public static int tileID;

		// Token: 0x0400115A RID: 4442
		public static int lastTileID = -1;

		// Token: 0x0400115B RID: 4443
		public static int[] maps;

		// Token: 0x0400115C RID: 4444
		public static int[] types;

		// Token: 0x0400115D RID: 4445
		public static Image[] imgTile;

		// Token: 0x0400115E RID: 4446
		public static Image imgWaterfall;

		// Token: 0x0400115F RID: 4447
		public static Image imgTopWaterfall;

		// Token: 0x04001160 RID: 4448
		public static Image imgWaterflow;

		// Token: 0x04001161 RID: 4449
		public static Image imgWaterlowN;

		// Token: 0x04001162 RID: 4450
		public static Image imgWaterlowN2;

		// Token: 0x04001163 RID: 4451
		public static sbyte size = 24;

		// Token: 0x04001164 RID: 4452
		private static int bx;

		// Token: 0x04001165 RID: 4453
		private static int dbx;

		// Token: 0x04001166 RID: 4454
		private static int fx;

		// Token: 0x04001167 RID: 4455
		private static int dfx;

		// Token: 0x04001168 RID: 4456
		public static bool isMapDouble = false;

		// Token: 0x04001169 RID: 4457
		public static string mapName = string.Empty;

		// Token: 0x0400116A RID: 4458
		public static sbyte versionMap = 1;

		// Token: 0x0400116B RID: 4459
		public static int mapID;

		// Token: 0x0400116C RID: 4460
		public static int lastBgID = -1;

		// Token: 0x0400116D RID: 4461
		public static int zoneID;

		// Token: 0x0400116E RID: 4462
		public static int bgID;

		// Token: 0x0400116F RID: 4463
		public static int bgType;

		// Token: 0x04001170 RID: 4464
		public static int lastType = -1;

		// Token: 0x04001171 RID: 4465
		public static int typeMap;

		// Token: 0x04001172 RID: 4466
		public static sbyte planetID;

		// Token: 0x04001173 RID: 4467
		public static sbyte lastPlanetId = -1;

		// Token: 0x04001174 RID: 4468
		public static MyVector vGo = new MyVector();

		// Token: 0x04001175 RID: 4469
		public static MyVector vItemBg = new MyVector();

		// Token: 0x04001176 RID: 4470
		public static MyVector vCurrItem = new MyVector();

		// Token: 0x04001177 RID: 4471
		public static string[] mapNames;

		// Token: 0x04001178 RID: 4472
		public static sbyte MAP_NORMAL = 0;

		// Token: 0x04001179 RID: 4473
		public static Image bong;

		// Token: 0x0400117A RID: 4474
		public static Image[] bgItem = new Image[8];

		// Token: 0x0400117B RID: 4475
		public static MyVector vObject = new MyVector();

		// Token: 0x0400117C RID: 4476
		public static int[] offlineId = new int[]
		{
			21,
			22,
			23,
			39,
			40,
			41
		};

		// Token: 0x0400117D RID: 4477
		public static int[] highterId = new int[]
		{
			21,
			22,
			23,
			24,
			25,
			26
		};

		// Token: 0x0400117E RID: 4478
		public static int[] toOfflineId = new int[]
		{
			0,
			7,
			14
		};

		// Token: 0x0400117F RID: 4479
		public static int[][] tileType;

		// Token: 0x04001180 RID: 4480
		public static int[][][] tileIndex;

		// Token: 0x04001181 RID: 4481
		public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

		// Token: 0x04001182 RID: 4482
		public static int sizeMiniMap = 2;

		// Token: 0x04001183 RID: 4483
		public static int gssx;

		// Token: 0x04001184 RID: 4484
		public static int gssxe;

		// Token: 0x04001185 RID: 4485
		public static int gssy;

		// Token: 0x04001186 RID: 4486
		public static int gssye;

		// Token: 0x04001187 RID: 4487
		public static int countx;

		// Token: 0x04001188 RID: 4488
		public static int county;

		// Token: 0x04001189 RID: 4489
		private static int[] colorMini = new int[]
		{
			5257738,
			8807192
		};

		// Token: 0x0400118A RID: 4490
		public static int yWater = 0;
	}
}
