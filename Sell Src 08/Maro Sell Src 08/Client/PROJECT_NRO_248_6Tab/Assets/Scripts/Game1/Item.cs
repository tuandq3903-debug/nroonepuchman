using System;

namespace Game1
{
	// Token: 0x0200048C RID: 1164
	public class Item
	{
		// Token: 0x060033DC RID: 13276 RVA: 0x0032F7FE File Offset: 0x0032D9FE
		public void getCompare()
		{
			this.compare = GameCanvas.panel.getCompare(this);
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x0032F814 File Offset: 0x0032DA14
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

		// Token: 0x060033DE RID: 13278 RVA: 0x0032F854 File Offset: 0x0032DA54
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

		// Token: 0x060033DF RID: 13279 RVA: 0x0032F990 File Offset: 0x0032DB90
		public bool isTypeBody()
		{
			return (0 <= this.template.type && this.template.type < 6) || this.template.type == 32 || this.template.type == 35 || this.template.type == 11 || this.template.type == 23;
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x0032F9F8 File Offset: 0x0032DBF8
		public void setPartTemp(int headTemp, int bodyTemp, int legTemp, int bagTemp)
		{
			this.headTemp = headTemp;
			this.bodyTemp = bodyTemp;
			this.legTemp = legTemp;
			this.bagTemp = bagTemp;
		}

		// Token: 0x04006407 RID: 25607
		public const int OPT_STAR = 34;

		// Token: 0x04006408 RID: 25608
		public const int OPT_MOON = 35;

		// Token: 0x04006409 RID: 25609
		public const int OPT_SUN = 36;

		// Token: 0x0400640A RID: 25610
		public const int OPT_COLORNAME = 41;

		// Token: 0x0400640B RID: 25611
		public const int OPT_LVITEM = 72;

		// Token: 0x0400640C RID: 25612
		public const int OPT_STARSLOT = 102;

		// Token: 0x0400640D RID: 25613
		public const int OPT_MAXSTARSLOT = 107;

		// Token: 0x0400640E RID: 25614
		public const int TYPE_BODY_MIN = 0;

		// Token: 0x0400640F RID: 25615
		public const int TYPE_BODY_MAX = 6;

		// Token: 0x04006410 RID: 25616
		public const int TYPE_AO = 0;

		// Token: 0x04006411 RID: 25617
		public const int TYPE_QUAN = 1;

		// Token: 0x04006412 RID: 25618
		public const int TYPE_GANGTAY = 2;

		// Token: 0x04006413 RID: 25619
		public const int TYPE_GIAY = 3;

		// Token: 0x04006414 RID: 25620
		public const int TYPE_RADA = 4;

		// Token: 0x04006415 RID: 25621
		public const int TYPE_HAIR = 5;

		// Token: 0x04006416 RID: 25622
		public const int TYPE_DAUTHAN = 6;

		// Token: 0x04006417 RID: 25623
		public const int TYPE_NGOCRONG = 12;

		// Token: 0x04006418 RID: 25624
		public const int TYPE_SACH = 7;

		// Token: 0x04006419 RID: 25625
		public const int TYPE_NHIEMVU = 8;

		// Token: 0x0400641A RID: 25626
		public const int TYPE_GOLD = 9;

		// Token: 0x0400641B RID: 25627
		public const int TYPE_DIAMOND = 10;

		// Token: 0x0400641C RID: 25628
		public const int TYPE_BALO = 11;

		// Token: 0x0400641D RID: 25629
		public const int TYPE_MOUNT = 23;

		// Token: 0x0400641E RID: 25630
		public const int TYPE_MOUNT_VIP = 24;

		// Token: 0x0400641F RID: 25631
		public const int TYPE_DIAMOND_LOCK = 34;

		// Token: 0x04006420 RID: 25632
		public const int TYPE_TRAINSUIT = 32;

		// Token: 0x04006421 RID: 25633
		public const int TYPE_HAT = 35;

		// Token: 0x04006422 RID: 25634
		public const sbyte UI_WEAPON = 2;

		// Token: 0x04006423 RID: 25635
		public const sbyte UI_BAG = 3;

		// Token: 0x04006424 RID: 25636
		public const sbyte UI_BOX = 4;

		// Token: 0x04006425 RID: 25637
		public const sbyte UI_BODY = 5;

		// Token: 0x04006426 RID: 25638
		public const sbyte UI_STACK = 6;

		// Token: 0x04006427 RID: 25639
		public const sbyte UI_STACK_LOCK = 7;

		// Token: 0x04006428 RID: 25640
		public const sbyte UI_GROCERY = 8;

		// Token: 0x04006429 RID: 25641
		public const sbyte UI_GROCERY_LOCK = 9;

		// Token: 0x0400642A RID: 25642
		public const sbyte UI_UPGRADE = 10;

		// Token: 0x0400642B RID: 25643
		public const sbyte UI_UPPEARL = 11;

		// Token: 0x0400642C RID: 25644
		public const sbyte UI_UPPEARL_LOCK = 12;

		// Token: 0x0400642D RID: 25645
		public const sbyte UI_SPLIT = 13;

		// Token: 0x0400642E RID: 25646
		public const sbyte UI_STORE = 14;

		// Token: 0x0400642F RID: 25647
		public const sbyte UI_BOOK = 15;

		// Token: 0x04006430 RID: 25648
		public const sbyte UI_LIEN = 16;

		// Token: 0x04006431 RID: 25649
		public const sbyte UI_NHAN = 17;

		// Token: 0x04006432 RID: 25650
		public const sbyte UI_NGOCBOI = 18;

		// Token: 0x04006433 RID: 25651
		public const sbyte UI_PHU = 19;

		// Token: 0x04006434 RID: 25652
		public const sbyte UI_NONNAM = 20;

		// Token: 0x04006435 RID: 25653
		public const sbyte UI_NONNU = 21;

		// Token: 0x04006436 RID: 25654
		public const sbyte UI_AONAM = 22;

		// Token: 0x04006437 RID: 25655
		public const sbyte UI_AONU = 23;

		// Token: 0x04006438 RID: 25656
		public const sbyte UI_GANGTAYNAM = 24;

		// Token: 0x04006439 RID: 25657
		public const sbyte UI_GANGTAYNU = 25;

		// Token: 0x0400643A RID: 25658
		public const sbyte UI_QUANNAM = 26;

		// Token: 0x0400643B RID: 25659
		public const sbyte UI_QUANNU = 27;

		// Token: 0x0400643C RID: 25660
		public const sbyte UI_GIAYNAM = 28;

		// Token: 0x0400643D RID: 25661
		public const sbyte UI_GIAYNU = 29;

		// Token: 0x0400643E RID: 25662
		public const sbyte UI_TRADE = 30;

		// Token: 0x0400643F RID: 25663
		public const sbyte UI_UPGRADE_GOLD = 31;

		// Token: 0x04006440 RID: 25664
		public const sbyte UI_FASHION = 32;

		// Token: 0x04006441 RID: 25665
		public const sbyte UI_CONVERT = 33;

		// Token: 0x04006442 RID: 25666
		public ItemOption[] itemOption;

		// Token: 0x04006443 RID: 25667
		public ItemTemplate template;

		// Token: 0x04006444 RID: 25668
		public MyVector options;

		// Token: 0x04006445 RID: 25669
		public int itemId;

		// Token: 0x04006446 RID: 25670
		public int playerId;

		// Token: 0x04006447 RID: 25671
		public bool isSelect;

		// Token: 0x04006448 RID: 25672
		public int indexUI;

		// Token: 0x04006449 RID: 25673
		public int quantity;

		// Token: 0x0400644A RID: 25674
		public int quantilyToBuy;

		// Token: 0x0400644B RID: 25675
		public long powerRequire;

		// Token: 0x0400644C RID: 25676
		public bool isLock;

		// Token: 0x0400644D RID: 25677
		public int sys;

		// Token: 0x0400644E RID: 25678
		public int upgrade;

		// Token: 0x0400644F RID: 25679
		public int buyCoin;

		// Token: 0x04006450 RID: 25680
		public int buyCoinLock;

		// Token: 0x04006451 RID: 25681
		public int buyGold;

		// Token: 0x04006452 RID: 25682
		public int buyGoldLock;

		// Token: 0x04006453 RID: 25683
		public int saleCoinLock;

		// Token: 0x04006454 RID: 25684
		public int buySpec;

		// Token: 0x04006455 RID: 25685
		public int buyRuby;

		// Token: 0x04006456 RID: 25686
		public short iconSpec = -1;

		// Token: 0x04006457 RID: 25687
		public sbyte buyType = -1;

		// Token: 0x04006458 RID: 25688
		public int typeUI;

		// Token: 0x04006459 RID: 25689
		public bool isExpires;

		// Token: 0x0400645A RID: 25690
		public bool isBuySpec;

		// Token: 0x0400645B RID: 25691
		public EffectCharPaint eff;

		// Token: 0x0400645C RID: 25692
		public int indexEff;

		// Token: 0x0400645D RID: 25693
		public Image img;

		// Token: 0x0400645E RID: 25694
		public string info;

		// Token: 0x0400645F RID: 25695
		public string content;

		// Token: 0x04006460 RID: 25696
		public string reason = string.Empty;

		// Token: 0x04006461 RID: 25697
		public int compare;

		// Token: 0x04006462 RID: 25698
		public sbyte isMe;

		// Token: 0x04006463 RID: 25699
		public bool newItem;

		// Token: 0x04006464 RID: 25700
		public int headTemp = -1;

		// Token: 0x04006465 RID: 25701
		public int bodyTemp = -1;

		// Token: 0x04006466 RID: 25702
		public int legTemp = -1;

		// Token: 0x04006467 RID: 25703
		public int bagTemp = -1;

		// Token: 0x04006468 RID: 25704
		public int wpTemp = -1;

		// Token: 0x04006469 RID: 25705
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

		// Token: 0x0400646A RID: 25706
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

		// Token: 0x0400646B RID: 25707
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
