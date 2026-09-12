using System;

namespace Game4
{
	// Token: 0x02000204 RID: 516
	public class Item
	{
		// Token: 0x060016F0 RID: 5872 RVA: 0x00170612 File Offset: 0x0016E812
		public void getCompare()
		{
			this.compare = GameCanvas.panel.getCompare(this);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00170628 File Offset: 0x0016E828
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

		// Token: 0x060016F2 RID: 5874 RVA: 0x00170668 File Offset: 0x0016E868
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

		// Token: 0x060016F3 RID: 5875 RVA: 0x001707A4 File Offset: 0x0016E9A4
		public bool isTypeBody()
		{
			return (0 <= this.template.type && this.template.type < 6) || this.template.type == 32 || this.template.type == 35 || this.template.type == 11 || this.template.type == 23;
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0017080C File Offset: 0x0016EA0C
		public void setPartTemp(int headTemp, int bodyTemp, int legTemp, int bagTemp)
		{
			this.headTemp = headTemp;
			this.bodyTemp = bodyTemp;
			this.legTemp = legTemp;
			this.bagTemp = bagTemp;
		}

		// Token: 0x04002C8A RID: 11402
		public const int OPT_STAR = 34;

		// Token: 0x04002C8B RID: 11403
		public const int OPT_MOON = 35;

		// Token: 0x04002C8C RID: 11404
		public const int OPT_SUN = 36;

		// Token: 0x04002C8D RID: 11405
		public const int OPT_COLORNAME = 41;

		// Token: 0x04002C8E RID: 11406
		public const int OPT_LVITEM = 72;

		// Token: 0x04002C8F RID: 11407
		public const int OPT_STARSLOT = 102;

		// Token: 0x04002C90 RID: 11408
		public const int OPT_MAXSTARSLOT = 107;

		// Token: 0x04002C91 RID: 11409
		public const int TYPE_BODY_MIN = 0;

		// Token: 0x04002C92 RID: 11410
		public const int TYPE_BODY_MAX = 6;

		// Token: 0x04002C93 RID: 11411
		public const int TYPE_AO = 0;

		// Token: 0x04002C94 RID: 11412
		public const int TYPE_QUAN = 1;

		// Token: 0x04002C95 RID: 11413
		public const int TYPE_GANGTAY = 2;

		// Token: 0x04002C96 RID: 11414
		public const int TYPE_GIAY = 3;

		// Token: 0x04002C97 RID: 11415
		public const int TYPE_RADA = 4;

		// Token: 0x04002C98 RID: 11416
		public const int TYPE_HAIR = 5;

		// Token: 0x04002C99 RID: 11417
		public const int TYPE_DAUTHAN = 6;

		// Token: 0x04002C9A RID: 11418
		public const int TYPE_NGOCRONG = 12;

		// Token: 0x04002C9B RID: 11419
		public const int TYPE_SACH = 7;

		// Token: 0x04002C9C RID: 11420
		public const int TYPE_NHIEMVU = 8;

		// Token: 0x04002C9D RID: 11421
		public const int TYPE_GOLD = 9;

		// Token: 0x04002C9E RID: 11422
		public const int TYPE_DIAMOND = 10;

		// Token: 0x04002C9F RID: 11423
		public const int TYPE_BALO = 11;

		// Token: 0x04002CA0 RID: 11424
		public const int TYPE_MOUNT = 23;

		// Token: 0x04002CA1 RID: 11425
		public const int TYPE_MOUNT_VIP = 24;

		// Token: 0x04002CA2 RID: 11426
		public const int TYPE_DIAMOND_LOCK = 34;

		// Token: 0x04002CA3 RID: 11427
		public const int TYPE_TRAINSUIT = 32;

		// Token: 0x04002CA4 RID: 11428
		public const int TYPE_HAT = 35;

		// Token: 0x04002CA5 RID: 11429
		public const sbyte UI_WEAPON = 2;

		// Token: 0x04002CA6 RID: 11430
		public const sbyte UI_BAG = 3;

		// Token: 0x04002CA7 RID: 11431
		public const sbyte UI_BOX = 4;

		// Token: 0x04002CA8 RID: 11432
		public const sbyte UI_BODY = 5;

		// Token: 0x04002CA9 RID: 11433
		public const sbyte UI_STACK = 6;

		// Token: 0x04002CAA RID: 11434
		public const sbyte UI_STACK_LOCK = 7;

		// Token: 0x04002CAB RID: 11435
		public const sbyte UI_GROCERY = 8;

		// Token: 0x04002CAC RID: 11436
		public const sbyte UI_GROCERY_LOCK = 9;

		// Token: 0x04002CAD RID: 11437
		public const sbyte UI_UPGRADE = 10;

		// Token: 0x04002CAE RID: 11438
		public const sbyte UI_UPPEARL = 11;

		// Token: 0x04002CAF RID: 11439
		public const sbyte UI_UPPEARL_LOCK = 12;

		// Token: 0x04002CB0 RID: 11440
		public const sbyte UI_SPLIT = 13;

		// Token: 0x04002CB1 RID: 11441
		public const sbyte UI_STORE = 14;

		// Token: 0x04002CB2 RID: 11442
		public const sbyte UI_BOOK = 15;

		// Token: 0x04002CB3 RID: 11443
		public const sbyte UI_LIEN = 16;

		// Token: 0x04002CB4 RID: 11444
		public const sbyte UI_NHAN = 17;

		// Token: 0x04002CB5 RID: 11445
		public const sbyte UI_NGOCBOI = 18;

		// Token: 0x04002CB6 RID: 11446
		public const sbyte UI_PHU = 19;

		// Token: 0x04002CB7 RID: 11447
		public const sbyte UI_NONNAM = 20;

		// Token: 0x04002CB8 RID: 11448
		public const sbyte UI_NONNU = 21;

		// Token: 0x04002CB9 RID: 11449
		public const sbyte UI_AONAM = 22;

		// Token: 0x04002CBA RID: 11450
		public const sbyte UI_AONU = 23;

		// Token: 0x04002CBB RID: 11451
		public const sbyte UI_GANGTAYNAM = 24;

		// Token: 0x04002CBC RID: 11452
		public const sbyte UI_GANGTAYNU = 25;

		// Token: 0x04002CBD RID: 11453
		public const sbyte UI_QUANNAM = 26;

		// Token: 0x04002CBE RID: 11454
		public const sbyte UI_QUANNU = 27;

		// Token: 0x04002CBF RID: 11455
		public const sbyte UI_GIAYNAM = 28;

		// Token: 0x04002CC0 RID: 11456
		public const sbyte UI_GIAYNU = 29;

		// Token: 0x04002CC1 RID: 11457
		public const sbyte UI_TRADE = 30;

		// Token: 0x04002CC2 RID: 11458
		public const sbyte UI_UPGRADE_GOLD = 31;

		// Token: 0x04002CC3 RID: 11459
		public const sbyte UI_FASHION = 32;

		// Token: 0x04002CC4 RID: 11460
		public const sbyte UI_CONVERT = 33;

		// Token: 0x04002CC5 RID: 11461
		public ItemOption[] itemOption;

		// Token: 0x04002CC6 RID: 11462
		public ItemTemplate template;

		// Token: 0x04002CC7 RID: 11463
		public MyVector options;

		// Token: 0x04002CC8 RID: 11464
		public int itemId;

		// Token: 0x04002CC9 RID: 11465
		public int playerId;

		// Token: 0x04002CCA RID: 11466
		public bool isSelect;

		// Token: 0x04002CCB RID: 11467
		public int indexUI;

		// Token: 0x04002CCC RID: 11468
		public int quantity;

		// Token: 0x04002CCD RID: 11469
		public int quantilyToBuy;

		// Token: 0x04002CCE RID: 11470
		public long powerRequire;

		// Token: 0x04002CCF RID: 11471
		public bool isLock;

		// Token: 0x04002CD0 RID: 11472
		public int sys;

		// Token: 0x04002CD1 RID: 11473
		public int upgrade;

		// Token: 0x04002CD2 RID: 11474
		public int buyCoin;

		// Token: 0x04002CD3 RID: 11475
		public int buyCoinLock;

		// Token: 0x04002CD4 RID: 11476
		public int buyGold;

		// Token: 0x04002CD5 RID: 11477
		public int buyGoldLock;

		// Token: 0x04002CD6 RID: 11478
		public int saleCoinLock;

		// Token: 0x04002CD7 RID: 11479
		public int buySpec;

		// Token: 0x04002CD8 RID: 11480
		public int buyRuby;

		// Token: 0x04002CD9 RID: 11481
		public short iconSpec = -1;

		// Token: 0x04002CDA RID: 11482
		public sbyte buyType = -1;

		// Token: 0x04002CDB RID: 11483
		public int typeUI;

		// Token: 0x04002CDC RID: 11484
		public bool isExpires;

		// Token: 0x04002CDD RID: 11485
		public bool isBuySpec;

		// Token: 0x04002CDE RID: 11486
		public EffectCharPaint eff;

		// Token: 0x04002CDF RID: 11487
		public int indexEff;

		// Token: 0x04002CE0 RID: 11488
		public Image img;

		// Token: 0x04002CE1 RID: 11489
		public string info;

		// Token: 0x04002CE2 RID: 11490
		public string content;

		// Token: 0x04002CE3 RID: 11491
		public string reason = string.Empty;

		// Token: 0x04002CE4 RID: 11492
		public int compare;

		// Token: 0x04002CE5 RID: 11493
		public sbyte isMe;

		// Token: 0x04002CE6 RID: 11494
		public bool newItem;

		// Token: 0x04002CE7 RID: 11495
		public int headTemp = -1;

		// Token: 0x04002CE8 RID: 11496
		public int bodyTemp = -1;

		// Token: 0x04002CE9 RID: 11497
		public int legTemp = -1;

		// Token: 0x04002CEA RID: 11498
		public int bagTemp = -1;

		// Token: 0x04002CEB RID: 11499
		public int wpTemp = -1;

		// Token: 0x04002CEC RID: 11500
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

		// Token: 0x04002CED RID: 11501
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

		// Token: 0x04002CEE RID: 11502
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
