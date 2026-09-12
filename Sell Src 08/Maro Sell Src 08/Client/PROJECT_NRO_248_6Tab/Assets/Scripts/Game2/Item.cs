using System;

namespace Game2
{
	// Token: 0x020003B4 RID: 948
	public class Item
	{
		// Token: 0x06002A38 RID: 10808 RVA: 0x0029A75A File Offset: 0x0029895A
		public void getCompare()
		{
			this.compare = GameCanvas.panel.getCompare(this);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x0029A770 File Offset: 0x00298970
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

		// Token: 0x06002A3A RID: 10810 RVA: 0x0029A7B0 File Offset: 0x002989B0
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

		// Token: 0x06002A3B RID: 10811 RVA: 0x0029A8EC File Offset: 0x00298AEC
		public bool isTypeBody()
		{
			return (0 <= this.template.type && this.template.type < 6) || this.template.type == 32 || this.template.type == 35 || this.template.type == 11 || this.template.type == 23;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x0029A954 File Offset: 0x00298B54
		public void setPartTemp(int headTemp, int bodyTemp, int legTemp, int bagTemp)
		{
			this.headTemp = headTemp;
			this.bodyTemp = bodyTemp;
			this.legTemp = legTemp;
			this.bagTemp = bagTemp;
		}

		// Token: 0x04005188 RID: 20872
		public const int OPT_STAR = 34;

		// Token: 0x04005189 RID: 20873
		public const int OPT_MOON = 35;

		// Token: 0x0400518A RID: 20874
		public const int OPT_SUN = 36;

		// Token: 0x0400518B RID: 20875
		public const int OPT_COLORNAME = 41;

		// Token: 0x0400518C RID: 20876
		public const int OPT_LVITEM = 72;

		// Token: 0x0400518D RID: 20877
		public const int OPT_STARSLOT = 102;

		// Token: 0x0400518E RID: 20878
		public const int OPT_MAXSTARSLOT = 107;

		// Token: 0x0400518F RID: 20879
		public const int TYPE_BODY_MIN = 0;

		// Token: 0x04005190 RID: 20880
		public const int TYPE_BODY_MAX = 6;

		// Token: 0x04005191 RID: 20881
		public const int TYPE_AO = 0;

		// Token: 0x04005192 RID: 20882
		public const int TYPE_QUAN = 1;

		// Token: 0x04005193 RID: 20883
		public const int TYPE_GANGTAY = 2;

		// Token: 0x04005194 RID: 20884
		public const int TYPE_GIAY = 3;

		// Token: 0x04005195 RID: 20885
		public const int TYPE_RADA = 4;

		// Token: 0x04005196 RID: 20886
		public const int TYPE_HAIR = 5;

		// Token: 0x04005197 RID: 20887
		public const int TYPE_DAUTHAN = 6;

		// Token: 0x04005198 RID: 20888
		public const int TYPE_NGOCRONG = 12;

		// Token: 0x04005199 RID: 20889
		public const int TYPE_SACH = 7;

		// Token: 0x0400519A RID: 20890
		public const int TYPE_NHIEMVU = 8;

		// Token: 0x0400519B RID: 20891
		public const int TYPE_GOLD = 9;

		// Token: 0x0400519C RID: 20892
		public const int TYPE_DIAMOND = 10;

		// Token: 0x0400519D RID: 20893
		public const int TYPE_BALO = 11;

		// Token: 0x0400519E RID: 20894
		public const int TYPE_MOUNT = 23;

		// Token: 0x0400519F RID: 20895
		public const int TYPE_MOUNT_VIP = 24;

		// Token: 0x040051A0 RID: 20896
		public const int TYPE_DIAMOND_LOCK = 34;

		// Token: 0x040051A1 RID: 20897
		public const int TYPE_TRAINSUIT = 32;

		// Token: 0x040051A2 RID: 20898
		public const int TYPE_HAT = 35;

		// Token: 0x040051A3 RID: 20899
		public const sbyte UI_WEAPON = 2;

		// Token: 0x040051A4 RID: 20900
		public const sbyte UI_BAG = 3;

		// Token: 0x040051A5 RID: 20901
		public const sbyte UI_BOX = 4;

		// Token: 0x040051A6 RID: 20902
		public const sbyte UI_BODY = 5;

		// Token: 0x040051A7 RID: 20903
		public const sbyte UI_STACK = 6;

		// Token: 0x040051A8 RID: 20904
		public const sbyte UI_STACK_LOCK = 7;

		// Token: 0x040051A9 RID: 20905
		public const sbyte UI_GROCERY = 8;

		// Token: 0x040051AA RID: 20906
		public const sbyte UI_GROCERY_LOCK = 9;

		// Token: 0x040051AB RID: 20907
		public const sbyte UI_UPGRADE = 10;

		// Token: 0x040051AC RID: 20908
		public const sbyte UI_UPPEARL = 11;

		// Token: 0x040051AD RID: 20909
		public const sbyte UI_UPPEARL_LOCK = 12;

		// Token: 0x040051AE RID: 20910
		public const sbyte UI_SPLIT = 13;

		// Token: 0x040051AF RID: 20911
		public const sbyte UI_STORE = 14;

		// Token: 0x040051B0 RID: 20912
		public const sbyte UI_BOOK = 15;

		// Token: 0x040051B1 RID: 20913
		public const sbyte UI_LIEN = 16;

		// Token: 0x040051B2 RID: 20914
		public const sbyte UI_NHAN = 17;

		// Token: 0x040051B3 RID: 20915
		public const sbyte UI_NGOCBOI = 18;

		// Token: 0x040051B4 RID: 20916
		public const sbyte UI_PHU = 19;

		// Token: 0x040051B5 RID: 20917
		public const sbyte UI_NONNAM = 20;

		// Token: 0x040051B6 RID: 20918
		public const sbyte UI_NONNU = 21;

		// Token: 0x040051B7 RID: 20919
		public const sbyte UI_AONAM = 22;

		// Token: 0x040051B8 RID: 20920
		public const sbyte UI_AONU = 23;

		// Token: 0x040051B9 RID: 20921
		public const sbyte UI_GANGTAYNAM = 24;

		// Token: 0x040051BA RID: 20922
		public const sbyte UI_GANGTAYNU = 25;

		// Token: 0x040051BB RID: 20923
		public const sbyte UI_QUANNAM = 26;

		// Token: 0x040051BC RID: 20924
		public const sbyte UI_QUANNU = 27;

		// Token: 0x040051BD RID: 20925
		public const sbyte UI_GIAYNAM = 28;

		// Token: 0x040051BE RID: 20926
		public const sbyte UI_GIAYNU = 29;

		// Token: 0x040051BF RID: 20927
		public const sbyte UI_TRADE = 30;

		// Token: 0x040051C0 RID: 20928
		public const sbyte UI_UPGRADE_GOLD = 31;

		// Token: 0x040051C1 RID: 20929
		public const sbyte UI_FASHION = 32;

		// Token: 0x040051C2 RID: 20930
		public const sbyte UI_CONVERT = 33;

		// Token: 0x040051C3 RID: 20931
		public ItemOption[] itemOption;

		// Token: 0x040051C4 RID: 20932
		public ItemTemplate template;

		// Token: 0x040051C5 RID: 20933
		public MyVector options;

		// Token: 0x040051C6 RID: 20934
		public int itemId;

		// Token: 0x040051C7 RID: 20935
		public int playerId;

		// Token: 0x040051C8 RID: 20936
		public bool isSelect;

		// Token: 0x040051C9 RID: 20937
		public int indexUI;

		// Token: 0x040051CA RID: 20938
		public int quantity;

		// Token: 0x040051CB RID: 20939
		public int quantilyToBuy;

		// Token: 0x040051CC RID: 20940
		public long powerRequire;

		// Token: 0x040051CD RID: 20941
		public bool isLock;

		// Token: 0x040051CE RID: 20942
		public int sys;

		// Token: 0x040051CF RID: 20943
		public int upgrade;

		// Token: 0x040051D0 RID: 20944
		public int buyCoin;

		// Token: 0x040051D1 RID: 20945
		public int buyCoinLock;

		// Token: 0x040051D2 RID: 20946
		public int buyGold;

		// Token: 0x040051D3 RID: 20947
		public int buyGoldLock;

		// Token: 0x040051D4 RID: 20948
		public int saleCoinLock;

		// Token: 0x040051D5 RID: 20949
		public int buySpec;

		// Token: 0x040051D6 RID: 20950
		public int buyRuby;

		// Token: 0x040051D7 RID: 20951
		public short iconSpec = -1;

		// Token: 0x040051D8 RID: 20952
		public sbyte buyType = -1;

		// Token: 0x040051D9 RID: 20953
		public int typeUI;

		// Token: 0x040051DA RID: 20954
		public bool isExpires;

		// Token: 0x040051DB RID: 20955
		public bool isBuySpec;

		// Token: 0x040051DC RID: 20956
		public EffectCharPaint eff;

		// Token: 0x040051DD RID: 20957
		public int indexEff;

		// Token: 0x040051DE RID: 20958
		public Image img;

		// Token: 0x040051DF RID: 20959
		public string info;

		// Token: 0x040051E0 RID: 20960
		public string content;

		// Token: 0x040051E1 RID: 20961
		public string reason = string.Empty;

		// Token: 0x040051E2 RID: 20962
		public int compare;

		// Token: 0x040051E3 RID: 20963
		public sbyte isMe;

		// Token: 0x040051E4 RID: 20964
		public bool newItem;

		// Token: 0x040051E5 RID: 20965
		public int headTemp = -1;

		// Token: 0x040051E6 RID: 20966
		public int bodyTemp = -1;

		// Token: 0x040051E7 RID: 20967
		public int legTemp = -1;

		// Token: 0x040051E8 RID: 20968
		public int bagTemp = -1;

		// Token: 0x040051E9 RID: 20969
		public int wpTemp = -1;

		// Token: 0x040051EA RID: 20970
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

		// Token: 0x040051EB RID: 20971
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

		// Token: 0x040051EC RID: 20972
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
