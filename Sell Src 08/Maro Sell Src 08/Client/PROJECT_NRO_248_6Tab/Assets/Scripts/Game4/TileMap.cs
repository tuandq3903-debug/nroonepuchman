using System;

namespace Game4
{
	// Token: 0x02000270 RID: 624
	public class TileMap
	{
		// Token: 0x06001BFE RID: 7166 RVA: 0x001B72DA File Offset: 0x001B54DA
		public static void loadBg()
		{
			TileMap.bong = GameCanvas.loadImage("/mainImage/myTexture2dbong.png");
			if (mGraphics.zoomLevel != 1 && !Main.isIpod && !Main.isIphone4)
			{
				TileMap.imgLight = GameCanvas.loadImage("/bg/light.png");
			}
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x001B7310 File Offset: 0x001B5510
		public static bool isVoDaiMap()
		{
			return TileMap.mapID == 51 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 129 || TileMap.mapID == 130;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x001B735C File Offset: 0x001B555C
		public static bool isTrainingMap()
		{
			return TileMap.mapID == 39 || TileMap.mapID == 40 || TileMap.mapID == 41;
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x001B737C File Offset: 0x001B557C
		public static bool mapPhuBang()
		{
			return GameScr.phuban_Info != null && TileMap.mapID == (int)GameScr.phuban_Info.idmapPaint;
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x001B739C File Offset: 0x001B559C
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

		// Token: 0x06001C03 RID: 7171 RVA: 0x001B73DC File Offset: 0x001B55DC
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

		// Token: 0x06001C04 RID: 7172 RVA: 0x001B740C File Offset: 0x001B560C
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

		// Token: 0x06001C05 RID: 7173 RVA: 0x001B74B8 File Offset: 0x001B56B8
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

		// Token: 0x06001C06 RID: 7174 RVA: 0x001B7538 File Offset: 0x001B5738
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

		// Token: 0x06001C07 RID: 7175 RVA: 0x001B7570 File Offset: 0x001B5770
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

		// Token: 0x06001C08 RID: 7176 RVA: 0x001B7628 File Offset: 0x001B5828
		public static bool isInAirMap()
		{
			return TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x001B7648 File Offset: 0x001B5848
		public static bool isDoubleMap()
		{
			return TileMap.isMapDouble || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 48 || TileMap.mapID == 51 || TileMap.mapID == 52 || TileMap.mapID == 103 || TileMap.mapID == 112 || TileMap.mapID == 113 || TileMap.mapID == 115 || TileMap.mapID == 117 || TileMap.mapID == 118 || TileMap.mapID == 119 || TileMap.mapID == 120 || TileMap.mapID == 121 || TileMap.mapID == 125 || TileMap.mapID == 129 || TileMap.mapID == 130;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x001B770C File Offset: 0x001B590C
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

		// Token: 0x06001C0B RID: 7179 RVA: 0x001B79A4 File Offset: 0x001B5BA4
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

		// Token: 0x06001C0C RID: 7180 RVA: 0x001B7A10 File Offset: 0x001B5C10
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

		// Token: 0x06001C0D RID: 7181 RVA: 0x001B7A5C File Offset: 0x001B5C5C
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

		// Token: 0x06001C0E RID: 7182 RVA: 0x001B7D80 File Offset: 0x001B5F80
		public static bool isWaterEff()
		{
			return TileMap.mapID != 54 && TileMap.mapID != 55 && TileMap.mapID != 56 && TileMap.mapID != 57 && TileMap.mapID != 138 && TileMap.mapID != 167;
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x001B7DCC File Offset: 0x001B5FCC
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

		// Token: 0x06001C10 RID: 7184 RVA: 0x001B7F40 File Offset: 0x001B6140
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

		// Token: 0x06001C11 RID: 7185 RVA: 0x001B7FC4 File Offset: 0x001B61C4
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

		// Token: 0x06001C12 RID: 7186 RVA: 0x001B8000 File Offset: 0x001B6200
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

		// Token: 0x06001C13 RID: 7187 RVA: 0x001B8048 File Offset: 0x001B6248
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

		// Token: 0x06001C14 RID: 7188 RVA: 0x001B8090 File Offset: 0x001B6290
		public static void setTileTypeAtPixel(int px, int py, int t)
		{
			TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] |= t;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x001B80B6 File Offset: 0x001B62B6
		public static void killTileTypeAt(int px, int py, int t)
		{
			TileMap.types[py / (int)TileMap.size * TileMap.tmw + px / (int)TileMap.size] &= ~t;
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x001B80DD File Offset: 0x001B62DD
		public static int tileYofPixel(int py)
		{
			return py / (int)TileMap.size * (int)TileMap.size;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x001B80DD File Offset: 0x001B62DD
		public static int tileXofPixel(int px)
		{
			return px / (int)TileMap.size * (int)TileMap.size;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x001B80EC File Offset: 0x001B62EC
		public static void loadMainTile()
		{
			if (TileMap.lastTileID != TileMap.tileID)
			{
				TileMap.getTile();
				TileMap.lastTileID = TileMap.tileID;
			}
		}

		// Token: 0x04003653 RID: 13907
		public static int tmw;

		// Token: 0x04003654 RID: 13908
		public static int tmh;

		// Token: 0x04003655 RID: 13909
		public static int pxw;

		// Token: 0x04003656 RID: 13910
		public static int pxh;

		// Token: 0x04003657 RID: 13911
		public static int tileID;

		// Token: 0x04003658 RID: 13912
		public static int lastTileID = -1;

		// Token: 0x04003659 RID: 13913
		public static int[] maps;

		// Token: 0x0400365A RID: 13914
		public static int[] types;

		// Token: 0x0400365B RID: 13915
		public static Image[] imgTile;

		// Token: 0x0400365C RID: 13916
		public static Image imgWaterfall;

		// Token: 0x0400365D RID: 13917
		public static Image imgTopWaterfall;

		// Token: 0x0400365E RID: 13918
		public static Image imgWaterflow;

		// Token: 0x0400365F RID: 13919
		public static Image imgWaterlowN;

		// Token: 0x04003660 RID: 13920
		public static Image imgWaterlowN2;

		// Token: 0x04003661 RID: 13921
		public static sbyte size = 24;

		// Token: 0x04003662 RID: 13922
		private static int bx;

		// Token: 0x04003663 RID: 13923
		private static int dbx;

		// Token: 0x04003664 RID: 13924
		private static int fx;

		// Token: 0x04003665 RID: 13925
		private static int dfx;

		// Token: 0x04003666 RID: 13926
		public static bool isMapDouble = false;

		// Token: 0x04003667 RID: 13927
		public static string mapName = string.Empty;

		// Token: 0x04003668 RID: 13928
		public static sbyte versionMap = 1;

		// Token: 0x04003669 RID: 13929
		public static int mapID;

		// Token: 0x0400366A RID: 13930
		public static int lastBgID = -1;

		// Token: 0x0400366B RID: 13931
		public static int zoneID;

		// Token: 0x0400366C RID: 13932
		public static int bgID;

		// Token: 0x0400366D RID: 13933
		public static int bgType;

		// Token: 0x0400366E RID: 13934
		public static int lastType = -1;

		// Token: 0x0400366F RID: 13935
		public static int typeMap;

		// Token: 0x04003670 RID: 13936
		public static sbyte planetID;

		// Token: 0x04003671 RID: 13937
		public static sbyte lastPlanetId = -1;

		// Token: 0x04003672 RID: 13938
		public static MyVector vGo = new MyVector();

		// Token: 0x04003673 RID: 13939
		public static MyVector vItemBg = new MyVector();

		// Token: 0x04003674 RID: 13940
		public static MyVector vCurrItem = new MyVector();

		// Token: 0x04003675 RID: 13941
		public static string[] mapNames;

		// Token: 0x04003676 RID: 13942
		public static sbyte MAP_NORMAL = 0;

		// Token: 0x04003677 RID: 13943
		public static Image bong;

		// Token: 0x04003678 RID: 13944
		public static Image[] bgItem = new Image[8];

		// Token: 0x04003679 RID: 13945
		public static MyVector vObject = new MyVector();

		// Token: 0x0400367A RID: 13946
		public static int[] offlineId = new int[]
		{
			21,
			22,
			23,
			39,
			40,
			41
		};

		// Token: 0x0400367B RID: 13947
		public static int[] highterId = new int[]
		{
			21,
			22,
			23,
			24,
			25,
			26
		};

		// Token: 0x0400367C RID: 13948
		public static int[] toOfflineId = new int[]
		{
			0,
			7,
			14
		};

		// Token: 0x0400367D RID: 13949
		public static int[][] tileType;

		// Token: 0x0400367E RID: 13950
		public static int[][][] tileIndex;

		// Token: 0x0400367F RID: 13951
		public static Image imgLight = GameCanvas.loadImage("/bg/light.png");

		// Token: 0x04003680 RID: 13952
		public static int sizeMiniMap = 2;

		// Token: 0x04003681 RID: 13953
		public static int gssx;

		// Token: 0x04003682 RID: 13954
		public static int gssxe;

		// Token: 0x04003683 RID: 13955
		public static int gssy;

		// Token: 0x04003684 RID: 13956
		public static int gssye;

		// Token: 0x04003685 RID: 13957
		public static int countx;

		// Token: 0x04003686 RID: 13958
		public static int county;

		// Token: 0x04003687 RID: 13959
		private static int[] colorMini = new int[]
		{
			5257738,
			8807192
		};

		// Token: 0x04003688 RID: 13960
		public static int yWater = 0;
	}
}
