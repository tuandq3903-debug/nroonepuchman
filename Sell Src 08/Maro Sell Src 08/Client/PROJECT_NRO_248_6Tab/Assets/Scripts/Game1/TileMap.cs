using System;

namespace Game1
{
	// Token: 0x020004F8 RID: 1272
	public class TileMap
	{
		// Token: 0x060038EA RID: 14570 RVA: 0x003764C6 File Offset: 0x003746C6
		public static void loadBg()
		{
			TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
			if (mGraphics.zoomLevel != 1 && !Main.isIpod && !Main.isIphone4)
			{
				TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
			}
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x003764FC File Offset: 0x003746FC
		public static bool isVoDaiMap()
		{
			return TileMap.mapID == 51 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 129 || TileMap.mapID == 130;
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x00376548 File Offset: 0x00374748
		public static bool isTrainingMap()
		{
			return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x00376568 File Offset: 0x00374768
		public static bool mapPhuBang()
		{
			return GameScr.phuban_Info != null && TileMap.mapID == (int)GameScr.phuban_Info.idmapPaint;
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x00376588 File Offset: 0x00374788
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

		// Token: 0x060038EF RID: 14575 RVA: 0x003765C8 File Offset: 0x003747C8
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

		// Token: 0x060038F0 RID: 14576 RVA: 0x003765F8 File Offset: 0x003747F8
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

		// Token: 0x060038F1 RID: 14577 RVA: 0x003766A4 File Offset: 0x003748A4
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

		// Token: 0x060038F2 RID: 14578 RVA: 0x00376724 File Offset: 0x00374924
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

		// Token: 0x060038F3 RID: 14579 RVA: 0x0037675C File Offset: 0x0037495C
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

		// Token: 0x060038F4 RID: 14580 RVA: 0x00376814 File Offset: 0x00374A14
		public static bool isInAirMap()
		{
			return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x00376834 File Offset: 0x00374A34
		public static bool isDoubleMap()
		{
			return TileMap.isMapDouble || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x003768F8 File Offset: 0x00374AF8
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

		// Token: 0x060038F7 RID: 14583 RVA: 0x00376B90 File Offset: 0x00374D90
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

		// Token: 0x060038F8 RID: 14584 RVA: 0x00376BFC File Offset: 0x00374DFC
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

		// Token: 0x060038F9 RID: 14585 RVA: 0x00376C48 File Offset: 0x00374E48
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

		// Token: 0x060038FA RID: 14586 RVA: 0x00376F6C File Offset: 0x0037516C
		public static bool isWaterEff()
		{
			return TileMap.mapID != 54 && TileMap.mapID != 55 && TileMap.mapID != 56 && TileMap.mapID != 57 && TileMap.mapID != 138 && TileMap.mapID != 167;
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x00376FB8 File Offset: 0x003751B8
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

		// Token: 0x060038FC RID: 14588 RVA: 0x0037712C File Offset: 0x0037532C
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

		// Token: 0x060038FD RID: 14589 RVA: 0x003771B0 File Offset: 0x003753B0
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

		// Token: 0x060038FE RID: 14590 RVA: 0x003771EC File Offset: 0x003753EC
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

		// Token: 0x060038FF RID: 14591 RVA: 0x00377234 File Offset: 0x00375434
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

		// Token: 0x06003900 RID: 14592 RVA: 0x0037727C File Offset: 0x0037547C
		public static void setTileTypeAtPixel(int px, int py, int t)
		{
			TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x003772A2 File Offset: 0x003754A2
		public static void killTileTypeAt(int px, int py, int t)
		{
			TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x003772C9 File Offset: 0x003754C9
		public static int tileYofPixel(int py)
		{
			return py / (int)TileMap.size * (int)TileMap.size;
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x003772C9 File Offset: 0x003754C9
		public static int tileXofPixel(int px)
		{
			return px / (int)TileMap.size * (int)TileMap.size;
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x003772D8 File Offset: 0x003754D8
		public static void loadMainTile()
		{
			if (TileMap.lastTileID != TileMap.tileID)
			{
				TileMap.getTile();
				TileMap.lastTileID = TileMap.tileID;
			}
		}

		// Token: 0x04006DD0 RID: 28112
		public static int tmw;

		// Token: 0x04006DD1 RID: 28113
		public static int tmh;

		// Token: 0x04006DD2 RID: 28114
		public static int pxw;

		// Token: 0x04006DD3 RID: 28115
		public static int pxh;

		// Token: 0x04006DD4 RID: 28116
		public static int tileID;

		// Token: 0x04006DD5 RID: 28117
		public static int lastTileID = -1;

		// Token: 0x04006DD6 RID: 28118
		public static int[] maps;

		// Token: 0x04006DD7 RID: 28119
		public static int[] types;

		// Token: 0x04006DD8 RID: 28120
		public static Image[] imgTile;

		// Token: 0x04006DD9 RID: 28121
		public static Image imgWaterfall;

		// Token: 0x04006DDA RID: 28122
		public static Image imgTopWaterfall;

		// Token: 0x04006DDB RID: 28123
		public static Image imgWaterflow;

		// Token: 0x04006DDC RID: 28124
		public static Image imgWaterlowN;

		// Token: 0x04006DDD RID: 28125
		public static Image imgWaterlowN2;

		// Token: 0x04006DDE RID: 28126
		public static sbyte size = 24;

		// Token: 0x04006DDF RID: 28127
		private static int bx;

		// Token: 0x04006DE0 RID: 28128
		private static int dbx;

		// Token: 0x04006DE1 RID: 28129
		private static int fx;

		// Token: 0x04006DE2 RID: 28130
		private static int dfx;

		// Token: 0x04006DE3 RID: 28131
		public static bool isMapDouble = false;

		// Token: 0x04006DE4 RID: 28132
		public static string mapName = string.Empty;

		// Token: 0x04006DE5 RID: 28133
		public static sbyte versionMap = 1;

		// Token: 0x04006DE6 RID: 28134
		public static int mapID;

		// Token: 0x04006DE7 RID: 28135
		public static int lastBgID = -1;

		// Token: 0x04006DE8 RID: 28136
		public static int zoneID;

		// Token: 0x04006DE9 RID: 28137
		public static int bgID;

		// Token: 0x04006DEA RID: 28138
		public static int bgType;

		// Token: 0x04006DEB RID: 28139
		public static int lastType = -1;

		// Token: 0x04006DEC RID: 28140
		public static int typeMap;

		// Token: 0x04006DED RID: 28141
		public static sbyte planetID;

		// Token: 0x04006DEE RID: 28142
		public static sbyte lastPlanetId = -1;

		// Token: 0x04006DEF RID: 28143
		public static MyVector vGo = new MyVector();

		// Token: 0x04006DF0 RID: 28144
		public static MyVector vItemBg = new MyVector();

		// Token: 0x04006DF1 RID: 28145
		public static MyVector vCurrItem = new MyVector();

		// Token: 0x04006DF2 RID: 28146
		public static string[] mapNames;

		// Token: 0x04006DF3 RID: 28147
		public static sbyte MAP_NORMAL = 0;

		// Token: 0x04006DF4 RID: 28148
		public static Image bong;

		// Token: 0x04006DF5 RID: 28149
		public static Image[] bgItem = new Image[8];

		// Token: 0x04006DF6 RID: 28150
		public static MyVector vObject = new MyVector();

		// Token: 0x04006DF7 RID: 28151
		public static int[] offlineId = new int[]
		{
			21,
			22,
			23,
			39,
			40,
			41
		};

		// Token: 0x04006DF8 RID: 28152
		public static int[] highterId = new int[]
		{
			21,
			22,
			23,
			24,
			25,
			26
		};

		// Token: 0x04006DF9 RID: 28153
		public static int[] toOfflineId = new int[]
		{
			0,
			7,
			14
		};

		// Token: 0x04006DFA RID: 28154
		public static int[][] tileType;

		// Token: 0x04006DFB RID: 28155
		public static int[][][] tileIndex;

		// Token: 0x04006DFC RID: 28156
		public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

		// Token: 0x04006DFD RID: 28157
		public static int sizeMiniMap = 2;

		// Token: 0x04006DFE RID: 28158
		public static int gssx;

		// Token: 0x04006DFF RID: 28159
		public static int gssxe;

		// Token: 0x04006E00 RID: 28160
		public static int gssy;

		// Token: 0x04006E01 RID: 28161
		public static int gssye;

		// Token: 0x04006E02 RID: 28162
		public static int countx;

		// Token: 0x04006E03 RID: 28163
		public static int county;

		// Token: 0x04006E04 RID: 28164
		private static int[] colorMini = new int[]
		{
			5257738,
			8807192
		};

		// Token: 0x04006E05 RID: 28165
		public static int yWater = 0;
	}
}
