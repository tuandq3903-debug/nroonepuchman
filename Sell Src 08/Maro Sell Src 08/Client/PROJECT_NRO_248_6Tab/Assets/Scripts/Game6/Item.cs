using System;

namespace Game6
{
	// Token: 0x02000054 RID: 84
	public class Item
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x000463C2 File Offset: 0x000445C2
		public void getCompare()
		{
			this.compare = GameCanvas.panel.getCompare(this);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000463D8 File Offset: 0x000445D8
		public bool isHaveOption(int id)
		{
			for (int i = 0; i < this.itemOption.Length; i++)
			{
				ItemOption itemOption = this.itemOption[i];
				if (itemOption != null && itemOption.optionTemplate.id == id)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00046418 File Offset: 0x00044618
		public Item clone()
		{
			Item item = new Item();
			item.template = this.template;
			if (this.options != null)
			{
				item.options = new MyVector();
				for (int i = 0; i < this.options.size(); i++)
				{
					ItemOption itemOption = new ItemOption();
					itemOption.optionTemplate = ((ItemOption)this.options.elementAt(i)).optionTemplate;
					itemOption.param = ((ItemOption)this.options.elementAt(i)).param;
					item.options.addElement(itemOption);
				}
			}
			item.itemId = this.itemId;
			item.playerId = this.playerId;
			item.indexUI = this.indexUI;
			item.quantity = this.quantity;
			item.isLock = this.isLock;
			item.sys = this.sys;
			item.upgrade = this.upgrade;
			item.buyCoin = this.buyCoin;
			item.buyCoinLock = this.buyCoinLock;
			item.buyGold = this.buyGold;
			item.buyGoldLock = this.buyGoldLock;
			item.saleCoinLock = this.saleCoinLock;
			item.typeUI = this.typeUI;
			item.isExpires = this.isExpires;
			return item;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00046554 File Offset: 0x00044754
		public bool isTypeBody()
		{
			return (0 <= this.template.type && this.template.type < 6) || this.template.type == 32 || this.template.type == 35 || this.template.type == 11 || this.template.type == 23;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000465BC File Offset: 0x000447BC
		public void setPartTemp(int headTemp, int bodyTemp, int legTemp, int bagTemp)
		{
			this.headTemp = headTemp;
			this.bodyTemp = bodyTemp;
			this.legTemp = legTemp;
			this.bagTemp = bagTemp;
		}

		// Token: 0x0400078C RID: 1932
		public const int OPT_STAR = 34;

		// Token: 0x0400078D RID: 1933
		public const int OPT_MOON = 35;

		// Token: 0x0400078E RID: 1934
		public const int OPT_SUN = 36;

		// Token: 0x0400078F RID: 1935
		public const int OPT_COLORNAME = 41;

		// Token: 0x04000790 RID: 1936
		public const int OPT_LVITEM = 72;

		// Token: 0x04000791 RID: 1937
		public const int OPT_STARSLOT = 102;

		// Token: 0x04000792 RID: 1938
		public const int OPT_MAXSTARSLOT = 107;

		// Token: 0x04000793 RID: 1939
		public const int TYPE_BODY_MIN = 0;

		// Token: 0x04000794 RID: 1940
		public const int TYPE_BODY_MAX = 6;

		// Token: 0x04000795 RID: 1941
		public const int TYPE_AO = 0;

		// Token: 0x04000796 RID: 1942
		public const int TYPE_QUAN = 1;

		// Token: 0x04000797 RID: 1943
		public const int TYPE_GANGTAY = 2;

		// Token: 0x04000798 RID: 1944
		public const int TYPE_GIAY = 3;

		// Token: 0x04000799 RID: 1945
		public const int TYPE_RADA = 4;

		// Token: 0x0400079A RID: 1946
		public const int TYPE_HAIR = 5;

		// Token: 0x0400079B RID: 1947
		public const int TYPE_DAUTHAN = 6;

		// Token: 0x0400079C RID: 1948
		public const int TYPE_NGOCRONG = 12;

		// Token: 0x0400079D RID: 1949
		public const int TYPE_SACH = 7;

		// Token: 0x0400079E RID: 1950
		public const int TYPE_NHIEMVU = 8;

		// Token: 0x0400079F RID: 1951
		public const int TYPE_GOLD = 9;

		// Token: 0x040007A0 RID: 1952
		public const int TYPE_DIAMOND = 10;

		// Token: 0x040007A1 RID: 1953
		public const int TYPE_BALO = 11;

		// Token: 0x040007A2 RID: 1954
		public const int TYPE_MOUNT = 23;

		// Token: 0x040007A3 RID: 1955
		public const int TYPE_MOUNT_VIP = 24;

		// Token: 0x040007A4 RID: 1956
		public const int TYPE_DIAMOND_LOCK = 34;

		// Token: 0x040007A5 RID: 1957
		public const int TYPE_TRAINSUIT = 32;

		// Token: 0x040007A6 RID: 1958
		public const int TYPE_HAT = 35;

		// Token: 0x040007A7 RID: 1959
		public const sbyte UI_WEAPON = 2;

		// Token: 0x040007A8 RID: 1960
		public const sbyte UI_BAG = 3;

		// Token: 0x040007A9 RID: 1961
		public const sbyte UI_BOX = 4;

		// Token: 0x040007AA RID: 1962
		public const sbyte UI_BODY = 5;

		// Token: 0x040007AB RID: 1963
		public const sbyte UI_STACK = 6;

		// Token: 0x040007AC RID: 1964
		public const sbyte UI_STACK_LOCK = 7;

		// Token: 0x040007AD RID: 1965
		public const sbyte UI_GROCERY = 8;

		// Token: 0x040007AE RID: 1966
		public const sbyte UI_GROCERY_LOCK = 9;

		// Token: 0x040007AF RID: 1967
		public const sbyte UI_UPGRADE = 10;

		// Token: 0x040007B0 RID: 1968
		public const sbyte UI_UPPEARL = 11;

		// Token: 0x040007B1 RID: 1969
		public const sbyte UI_UPPEARL_LOCK = 12;

		// Token: 0x040007B2 RID: 1970
		public const sbyte UI_SPLIT = 13;

		// Token: 0x040007B3 RID: 1971
		public const sbyte UI_STORE = 14;

		// Token: 0x040007B4 RID: 1972
		public const sbyte UI_BOOK = 15;

		// Token: 0x040007B5 RID: 1973
		public const sbyte UI_LIEN = 16;

		// Token: 0x040007B6 RID: 1974
		public const sbyte UI_NHAN = 17;

		// Token: 0x040007B7 RID: 1975
		public const sbyte UI_NGOCBOI = 18;

		// Token: 0x040007B8 RID: 1976
		public const sbyte UI_PHU = 19;

		// Token: 0x040007B9 RID: 1977
		public const sbyte UI_NONNAM = 20;

		// Token: 0x040007BA RID: 1978
		public const sbyte UI_NONNU = 21;

		// Token: 0x040007BB RID: 1979
		public const sbyte UI_AONAM = 22;

		// Token: 0x040007BC RID: 1980
		public const sbyte UI_AONU = 23;

		// Token: 0x040007BD RID: 1981
		public const sbyte UI_GANGTAYNAM = 24;

		// Token: 0x040007BE RID: 1982
		public const sbyte UI_GANGTAYNU = 25;

		// Token: 0x040007BF RID: 1983
		public const sbyte UI_QUANNAM = 26;

		// Token: 0x040007C0 RID: 1984
		public const sbyte UI_QUANNU = 27;

		// Token: 0x040007C1 RID: 1985
		public const sbyte UI_GIAYNAM = 28;

		// Token: 0x040007C2 RID: 1986
		public const sbyte UI_GIAYNU = 29;

		// Token: 0x040007C3 RID: 1987
		public const sbyte UI_TRADE = 30;

		// Token: 0x040007C4 RID: 1988
		public const sbyte UI_UPGRADE_GOLD = 31;

		// Token: 0x040007C5 RID: 1989
		public const sbyte UI_FASHION = 32;

		// Token: 0x040007C6 RID: 1990
		public const sbyte UI_CONVERT = 33;

		// Token: 0x040007C7 RID: 1991
		public ItemOption[] itemOption;

		// Token: 0x040007C8 RID: 1992
		public ItemTemplate template;

		// Token: 0x040007C9 RID: 1993
		public MyVector options;

		// Token: 0x040007CA RID: 1994
		public int itemId;

		// Token: 0x040007CB RID: 1995
		public int playerId;

		// Token: 0x040007CC RID: 1996
		public bool isSelect;

		// Token: 0x040007CD RID: 1997
		public int indexUI;

		// Token: 0x040007CE RID: 1998
		public int quantity;

		// Token: 0x040007CF RID: 1999
		public int quantilyToBuy;

		// Token: 0x040007D0 RID: 2000
		public long powerRequire;

		// Token: 0x040007D1 RID: 2001
		public bool isLock;

		// Token: 0x040007D2 RID: 2002
		public int sys;

		// Token: 0x040007D3 RID: 2003
		public int upgrade;

		// Token: 0x040007D4 RID: 2004
		public int buyCoin;

		// Token: 0x040007D5 RID: 2005
		public int buyCoinLock;

		// Token: 0x040007D6 RID: 2006
		public int buyGold;

		// Token: 0x040007D7 RID: 2007
		public int buyGoldLock;

		// Token: 0x040007D8 RID: 2008
		public int saleCoinLock;

		// Token: 0x040007D9 RID: 2009
		public int buySpec;

		// Token: 0x040007DA RID: 2010
		public int buyRuby;

		// Token: 0x040007DB RID: 2011
		public short iconSpec = -1;

		// Token: 0x040007DC RID: 2012
		public sbyte buyType = -1;

		// Token: 0x040007DD RID: 2013
		public int typeUI;

		// Token: 0x040007DE RID: 2014
		public bool isExpires;

		// Token: 0x040007DF RID: 2015
		public bool isBuySpec;

		// Token: 0x040007E0 RID: 2016
		public EffectCharPaint eff;

		// Token: 0x040007E1 RID: 2017
		public int indexEff;

		// Token: 0x040007E2 RID: 2018
		public Image img;

		// Token: 0x040007E3 RID: 2019
		public string info;

		// Token: 0x040007E4 RID: 2020
		public string content;

		// Token: 0x040007E5 RID: 2021
		public string reason = string.Empty;

		// Token: 0x040007E6 RID: 2022
		public int compare;

		// Token: 0x040007E7 RID: 2023
		public sbyte isMe;

		// Token: 0x040007E8 RID: 2024
		public bool newItem;

		// Token: 0x040007E9 RID: 2025
		public int headTemp = -1;

		// Token: 0x040007EA RID: 2026
		public int bodyTemp = -1;

		// Token: 0x040007EB RID: 2027
		public int legTemp = -1;

		// Token: 0x040007EC RID: 2028
		public int bagTemp = -1;

		// Token: 0x040007ED RID: 2029
		public int wpTemp = -1;

		// Token: 0x040007EE RID: 2030
		private int[] color = new int[]
		{
			0,
			0,
			0,
			0,
			600841,
			600841,
			667658,
			667658,
			3346944,
			3346688,
			4199680,
			5052928,
			3276851,
			3932211,
			4587571,
			5046280,
			6684682,
			3359744
		};

		// Token: 0x040007EF RID: 2031
		private int[][] colorBorder = new int[][]
		{
			new int[]
			{
				18687,
				16869,
				15052,
				13235,
				11161,
				9344
			},
			new int[]
			{
				45824,
				39168,
				32768,
				26112,
				19712,
				13056
			},
			new int[]
			{
				16744192,
				15037184,
				13395456,
				11753728,
				10046464,
				8404992
			},
			new int[]
			{
				13500671,
				12058853,
				10682572,
				9371827,
				7995545,
				6684800
			},
			new int[]
			{
				16711705,
				15007767,
				13369364,
				11730962,
				10027023,
				8388621
			}
		};

		// Token: 0x040007F0 RID: 2032
		private int[] size = new int[]
		{
			2,
			1,
			1,
			1,
			1,
			1
		};
	}
}
