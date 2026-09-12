using System;

namespace Game3
{
	// Token: 0x020002DC RID: 732
	public class Item
	{
		// Token: 0x06002094 RID: 8340 RVA: 0x002056B6 File Offset: 0x002038B6
		public void getCompare()
		{
			this.compare = GameCanvas.panel.getCompare(this);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x002056CC File Offset: 0x002038CC
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

		// Token: 0x06002096 RID: 8342 RVA: 0x0020570C File Offset: 0x0020390C
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

		// Token: 0x06002097 RID: 8343 RVA: 0x00205848 File Offset: 0x00203A48
		public bool isTypeBody()
		{
			return (0 <= this.template.type && this.template.type < 6) || this.template.type == 32 || this.template.type == 35 || this.template.type == 11 || this.template.type == 23;
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x002058B0 File Offset: 0x00203AB0
		public void setPartTemp(int headTemp, int bodyTemp, int legTemp, int bagTemp)
		{
			this.headTemp = headTemp;
			this.bodyTemp = bodyTemp;
			this.legTemp = legTemp;
			this.bagTemp = bagTemp;
		}

		// Token: 0x04003F09 RID: 16137
		public const int OPT_STAR = 34;

		// Token: 0x04003F0A RID: 16138
		public const int OPT_MOON = 35;

		// Token: 0x04003F0B RID: 16139
		public const int OPT_SUN = 36;

		// Token: 0x04003F0C RID: 16140
		public const int OPT_COLORNAME = 41;

		// Token: 0x04003F0D RID: 16141
		public const int OPT_LVITEM = 72;

		// Token: 0x04003F0E RID: 16142
		public const int OPT_STARSLOT = 102;

		// Token: 0x04003F0F RID: 16143
		public const int OPT_MAXSTARSLOT = 107;

		// Token: 0x04003F10 RID: 16144
		public const int TYPE_BODY_MIN = 0;

		// Token: 0x04003F11 RID: 16145
		public const int TYPE_BODY_MAX = 6;

		// Token: 0x04003F12 RID: 16146
		public const int TYPE_AO = 0;

		// Token: 0x04003F13 RID: 16147
		public const int TYPE_QUAN = 1;

		// Token: 0x04003F14 RID: 16148
		public const int TYPE_GANGTAY = 2;

		// Token: 0x04003F15 RID: 16149
		public const int TYPE_GIAY = 3;

		// Token: 0x04003F16 RID: 16150
		public const int TYPE_RADA = 4;

		// Token: 0x04003F17 RID: 16151
		public const int TYPE_HAIR = 5;

		// Token: 0x04003F18 RID: 16152
		public const int TYPE_DAUTHAN = 6;

		// Token: 0x04003F19 RID: 16153
		public const int TYPE_NGOCRONG = 12;

		// Token: 0x04003F1A RID: 16154
		public const int TYPE_SACH = 7;

		// Token: 0x04003F1B RID: 16155
		public const int TYPE_NHIEMVU = 8;

		// Token: 0x04003F1C RID: 16156
		public const int TYPE_GOLD = 9;

		// Token: 0x04003F1D RID: 16157
		public const int TYPE_DIAMOND = 10;

		// Token: 0x04003F1E RID: 16158
		public const int TYPE_BALO = 11;

		// Token: 0x04003F1F RID: 16159
		public const int TYPE_MOUNT = 23;

		// Token: 0x04003F20 RID: 16160
		public const int TYPE_MOUNT_VIP = 24;

		// Token: 0x04003F21 RID: 16161
		public const int TYPE_DIAMOND_LOCK = 34;

		// Token: 0x04003F22 RID: 16162
		public const int TYPE_TRAINSUIT = 32;

		// Token: 0x04003F23 RID: 16163
		public const int TYPE_HAT = 35;

		// Token: 0x04003F24 RID: 16164
		public const sbyte UI_WEAPON = 2;

		// Token: 0x04003F25 RID: 16165
		public const sbyte UI_BAG = 3;

		// Token: 0x04003F26 RID: 16166
		public const sbyte UI_BOX = 4;

		// Token: 0x04003F27 RID: 16167
		public const sbyte UI_BODY = 5;

		// Token: 0x04003F28 RID: 16168
		public const sbyte UI_STACK = 6;

		// Token: 0x04003F29 RID: 16169
		public const sbyte UI_STACK_LOCK = 7;

		// Token: 0x04003F2A RID: 16170
		public const sbyte UI_GROCERY = 8;

		// Token: 0x04003F2B RID: 16171
		public const sbyte UI_GROCERY_LOCK = 9;

		// Token: 0x04003F2C RID: 16172
		public const sbyte UI_UPGRADE = 10;

		// Token: 0x04003F2D RID: 16173
		public const sbyte UI_UPPEARL = 11;

		// Token: 0x04003F2E RID: 16174
		public const sbyte UI_UPPEARL_LOCK = 12;

		// Token: 0x04003F2F RID: 16175
		public const sbyte UI_SPLIT = 13;

		// Token: 0x04003F30 RID: 16176
		public const sbyte UI_STORE = 14;

		// Token: 0x04003F31 RID: 16177
		public const sbyte UI_BOOK = 15;

		// Token: 0x04003F32 RID: 16178
		public const sbyte UI_LIEN = 16;

		// Token: 0x04003F33 RID: 16179
		public const sbyte UI_NHAN = 17;

		// Token: 0x04003F34 RID: 16180
		public const sbyte UI_NGOCBOI = 18;

		// Token: 0x04003F35 RID: 16181
		public const sbyte UI_PHU = 19;

		// Token: 0x04003F36 RID: 16182
		public const sbyte UI_NONNAM = 20;

		// Token: 0x04003F37 RID: 16183
		public const sbyte UI_NONNU = 21;

		// Token: 0x04003F38 RID: 16184
		public const sbyte UI_AONAM = 22;

		// Token: 0x04003F39 RID: 16185
		public const sbyte UI_AONU = 23;

		// Token: 0x04003F3A RID: 16186
		public const sbyte UI_GANGTAYNAM = 24;

		// Token: 0x04003F3B RID: 16187
		public const sbyte UI_GANGTAYNU = 25;

		// Token: 0x04003F3C RID: 16188
		public const sbyte UI_QUANNAM = 26;

		// Token: 0x04003F3D RID: 16189
		public const sbyte UI_QUANNU = 27;

		// Token: 0x04003F3E RID: 16190
		public const sbyte UI_GIAYNAM = 28;

		// Token: 0x04003F3F RID: 16191
		public const sbyte UI_GIAYNU = 29;

		// Token: 0x04003F40 RID: 16192
		public const sbyte UI_TRADE = 30;

		// Token: 0x04003F41 RID: 16193
		public const sbyte UI_UPGRADE_GOLD = 31;

		// Token: 0x04003F42 RID: 16194
		public const sbyte UI_FASHION = 32;

		// Token: 0x04003F43 RID: 16195
		public const sbyte UI_CONVERT = 33;

		// Token: 0x04003F44 RID: 16196
		public ItemOption[] itemOption;

		// Token: 0x04003F45 RID: 16197
		public ItemTemplate template;

		// Token: 0x04003F46 RID: 16198
		public MyVector options;

		// Token: 0x04003F47 RID: 16199
		public int itemId;

		// Token: 0x04003F48 RID: 16200
		public int playerId;

		// Token: 0x04003F49 RID: 16201
		public bool isSelect;

		// Token: 0x04003F4A RID: 16202
		public int indexUI;

		// Token: 0x04003F4B RID: 16203
		public int quantity;

		// Token: 0x04003F4C RID: 16204
		public int quantilyToBuy;

		// Token: 0x04003F4D RID: 16205
		public long powerRequire;

		// Token: 0x04003F4E RID: 16206
		public bool isLock;

		// Token: 0x04003F4F RID: 16207
		public int sys;

		// Token: 0x04003F50 RID: 16208
		public int upgrade;

		// Token: 0x04003F51 RID: 16209
		public int buyCoin;

		// Token: 0x04003F52 RID: 16210
		public int buyCoinLock;

		// Token: 0x04003F53 RID: 16211
		public int buyGold;

		// Token: 0x04003F54 RID: 16212
		public int buyGoldLock;

		// Token: 0x04003F55 RID: 16213
		public int saleCoinLock;

		// Token: 0x04003F56 RID: 16214
		public int buySpec;

		// Token: 0x04003F57 RID: 16215
		public int buyRuby;

		// Token: 0x04003F58 RID: 16216
		public short iconSpec = -1;

		// Token: 0x04003F59 RID: 16217
		public sbyte buyType = -1;

		// Token: 0x04003F5A RID: 16218
		public int typeUI;

		// Token: 0x04003F5B RID: 16219
		public bool isExpires;

		// Token: 0x04003F5C RID: 16220
		public bool isBuySpec;

		// Token: 0x04003F5D RID: 16221
		public EffectCharPaint eff;

		// Token: 0x04003F5E RID: 16222
		public int indexEff;

		// Token: 0x04003F5F RID: 16223
		public Image img;

		// Token: 0x04003F60 RID: 16224
		public string info;

		// Token: 0x04003F61 RID: 16225
		public string content;

		// Token: 0x04003F62 RID: 16226
		public string reason = string.Empty;

		// Token: 0x04003F63 RID: 16227
		public int compare;

		// Token: 0x04003F64 RID: 16228
		public sbyte isMe;

		// Token: 0x04003F65 RID: 16229
		public bool newItem;

		// Token: 0x04003F66 RID: 16230
		public int headTemp = -1;

		// Token: 0x04003F67 RID: 16231
		public int bodyTemp = -1;

		// Token: 0x04003F68 RID: 16232
		public int legTemp = -1;

		// Token: 0x04003F69 RID: 16233
		public int bagTemp = -1;

		// Token: 0x04003F6A RID: 16234
		public int wpTemp = -1;

		// Token: 0x04003F6B RID: 16235
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

		// Token: 0x04003F6C RID: 16236
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

		// Token: 0x04003F6D RID: 16237
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
