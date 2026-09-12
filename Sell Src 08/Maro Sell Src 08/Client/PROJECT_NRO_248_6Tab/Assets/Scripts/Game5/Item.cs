using System;

namespace Game5
{
	// Token: 0x0200012C RID: 300
	public class Item
	{
		// Token: 0x06000D4C RID: 3404 RVA: 0x000DB56E File Offset: 0x000D976E
		public void getCompare()
		{
			this.compare = GameCanvas.panel.getCompare(this);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000DB584 File Offset: 0x000D9784
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

		// Token: 0x06000D4E RID: 3406 RVA: 0x000DB5C4 File Offset: 0x000D97C4
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

		// Token: 0x06000D4F RID: 3407 RVA: 0x000DB700 File Offset: 0x000D9900
		public bool isTypeBody()
		{
			return (0 <= this.template.type && this.template.type < 6) || this.template.type == 32 || this.template.type == 35 || this.template.type == 11 || this.template.type == 23;
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000DB768 File Offset: 0x000D9968
		public void setPartTemp(int headTemp, int bodyTemp, int legTemp, int bagTemp)
		{
			this.headTemp = headTemp;
			this.bodyTemp = bodyTemp;
			this.legTemp = legTemp;
			this.bagTemp = bagTemp;
		}

		// Token: 0x04001A0B RID: 6667
		public const int OPT_STAR = 34;

		// Token: 0x04001A0C RID: 6668
		public const int OPT_MOON = 35;

		// Token: 0x04001A0D RID: 6669
		public const int OPT_SUN = 36;

		// Token: 0x04001A0E RID: 6670
		public const int OPT_COLORNAME = 41;

		// Token: 0x04001A0F RID: 6671
		public const int OPT_LVITEM = 72;

		// Token: 0x04001A10 RID: 6672
		public const int OPT_STARSLOT = 102;

		// Token: 0x04001A11 RID: 6673
		public const int OPT_MAXSTARSLOT = 107;

		// Token: 0x04001A12 RID: 6674
		public const int TYPE_BODY_MIN = 0;

		// Token: 0x04001A13 RID: 6675
		public const int TYPE_BODY_MAX = 6;

		// Token: 0x04001A14 RID: 6676
		public const int TYPE_AO = 0;

		// Token: 0x04001A15 RID: 6677
		public const int TYPE_QUAN = 1;

		// Token: 0x04001A16 RID: 6678
		public const int TYPE_GANGTAY = 2;

		// Token: 0x04001A17 RID: 6679
		public const int TYPE_GIAY = 3;

		// Token: 0x04001A18 RID: 6680
		public const int TYPE_RADA = 4;

		// Token: 0x04001A19 RID: 6681
		public const int TYPE_HAIR = 5;

		// Token: 0x04001A1A RID: 6682
		public const int TYPE_DAUTHAN = 6;

		// Token: 0x04001A1B RID: 6683
		public const int TYPE_NGOCRONG = 12;

		// Token: 0x04001A1C RID: 6684
		public const int TYPE_SACH = 7;

		// Token: 0x04001A1D RID: 6685
		public const int TYPE_NHIEMVU = 8;

		// Token: 0x04001A1E RID: 6686
		public const int TYPE_GOLD = 9;

		// Token: 0x04001A1F RID: 6687
		public const int TYPE_DIAMOND = 10;

		// Token: 0x04001A20 RID: 6688
		public const int TYPE_BALO = 11;

		// Token: 0x04001A21 RID: 6689
		public const int TYPE_MOUNT = 23;

		// Token: 0x04001A22 RID: 6690
		public const int TYPE_MOUNT_VIP = 24;

		// Token: 0x04001A23 RID: 6691
		public const int TYPE_DIAMOND_LOCK = 34;

		// Token: 0x04001A24 RID: 6692
		public const int TYPE_TRAINSUIT = 32;

		// Token: 0x04001A25 RID: 6693
		public const int TYPE_HAT = 35;

		// Token: 0x04001A26 RID: 6694
		public const sbyte UI_WEAPON = 2;

		// Token: 0x04001A27 RID: 6695
		public const sbyte UI_BAG = 3;

		// Token: 0x04001A28 RID: 6696
		public const sbyte UI_BOX = 4;

		// Token: 0x04001A29 RID: 6697
		public const sbyte UI_BODY = 5;

		// Token: 0x04001A2A RID: 6698
		public const sbyte UI_STACK = 6;

		// Token: 0x04001A2B RID: 6699
		public const sbyte UI_STACK_LOCK = 7;

		// Token: 0x04001A2C RID: 6700
		public const sbyte UI_GROCERY = 8;

		// Token: 0x04001A2D RID: 6701
		public const sbyte UI_GROCERY_LOCK = 9;

		// Token: 0x04001A2E RID: 6702
		public const sbyte UI_UPGRADE = 10;

		// Token: 0x04001A2F RID: 6703
		public const sbyte UI_UPPEARL = 11;

		// Token: 0x04001A30 RID: 6704
		public const sbyte UI_UPPEARL_LOCK = 12;

		// Token: 0x04001A31 RID: 6705
		public const sbyte UI_SPLIT = 13;

		// Token: 0x04001A32 RID: 6706
		public const sbyte UI_STORE = 14;

		// Token: 0x04001A33 RID: 6707
		public const sbyte UI_BOOK = 15;

		// Token: 0x04001A34 RID: 6708
		public const sbyte UI_LIEN = 16;

		// Token: 0x04001A35 RID: 6709
		public const sbyte UI_NHAN = 17;

		// Token: 0x04001A36 RID: 6710
		public const sbyte UI_NGOCBOI = 18;

		// Token: 0x04001A37 RID: 6711
		public const sbyte UI_PHU = 19;

		// Token: 0x04001A38 RID: 6712
		public const sbyte UI_NONNAM = 20;

		// Token: 0x04001A39 RID: 6713
		public const sbyte UI_NONNU = 21;

		// Token: 0x04001A3A RID: 6714
		public const sbyte UI_AONAM = 22;

		// Token: 0x04001A3B RID: 6715
		public const sbyte UI_AONU = 23;

		// Token: 0x04001A3C RID: 6716
		public const sbyte UI_GANGTAYNAM = 24;

		// Token: 0x04001A3D RID: 6717
		public const sbyte UI_GANGTAYNU = 25;

		// Token: 0x04001A3E RID: 6718
		public const sbyte UI_QUANNAM = 26;

		// Token: 0x04001A3F RID: 6719
		public const sbyte UI_QUANNU = 27;

		// Token: 0x04001A40 RID: 6720
		public const sbyte UI_GIAYNAM = 28;

		// Token: 0x04001A41 RID: 6721
		public const sbyte UI_GIAYNU = 29;

		// Token: 0x04001A42 RID: 6722
		public const sbyte UI_TRADE = 30;

		// Token: 0x04001A43 RID: 6723
		public const sbyte UI_UPGRADE_GOLD = 31;

		// Token: 0x04001A44 RID: 6724
		public const sbyte UI_FASHION = 32;

		// Token: 0x04001A45 RID: 6725
		public const sbyte UI_CONVERT = 33;

		// Token: 0x04001A46 RID: 6726
		public ItemOption[] itemOption;

		// Token: 0x04001A47 RID: 6727
		public ItemTemplate template;

		// Token: 0x04001A48 RID: 6728
		public MyVector options;

		// Token: 0x04001A49 RID: 6729
		public int itemId;

		// Token: 0x04001A4A RID: 6730
		public int playerId;

		// Token: 0x04001A4B RID: 6731
		public bool isSelect;

		// Token: 0x04001A4C RID: 6732
		public int indexUI;

		// Token: 0x04001A4D RID: 6733
		public int quantity;

		// Token: 0x04001A4E RID: 6734
		public int quantilyToBuy;

		// Token: 0x04001A4F RID: 6735
		public long powerRequire;

		// Token: 0x04001A50 RID: 6736
		public bool isLock;

		// Token: 0x04001A51 RID: 6737
		public int sys;

		// Token: 0x04001A52 RID: 6738
		public int upgrade;

		// Token: 0x04001A53 RID: 6739
		public int buyCoin;

		// Token: 0x04001A54 RID: 6740
		public int buyCoinLock;

		// Token: 0x04001A55 RID: 6741
		public int buyGold;

		// Token: 0x04001A56 RID: 6742
		public int buyGoldLock;

		// Token: 0x04001A57 RID: 6743
		public int saleCoinLock;

		// Token: 0x04001A58 RID: 6744
		public int buySpec;

		// Token: 0x04001A59 RID: 6745
		public int buyRuby;

		// Token: 0x04001A5A RID: 6746
		public short iconSpec = -1;

		// Token: 0x04001A5B RID: 6747
		public sbyte buyType = -1;

		// Token: 0x04001A5C RID: 6748
		public int typeUI;

		// Token: 0x04001A5D RID: 6749
		public bool isExpires;

		// Token: 0x04001A5E RID: 6750
		public bool isBuySpec;

		// Token: 0x04001A5F RID: 6751
		public EffectCharPaint eff;

		// Token: 0x04001A60 RID: 6752
		public int indexEff;

		// Token: 0x04001A61 RID: 6753
		public Image img;

		// Token: 0x04001A62 RID: 6754
		public string info;

		// Token: 0x04001A63 RID: 6755
		public string content;

		// Token: 0x04001A64 RID: 6756
		public string reason = string.Empty;

		// Token: 0x04001A65 RID: 6757
		public int compare;

		// Token: 0x04001A66 RID: 6758
		public sbyte isMe;

		// Token: 0x04001A67 RID: 6759
		public bool newItem;

		// Token: 0x04001A68 RID: 6760
		public int headTemp = -1;

		// Token: 0x04001A69 RID: 6761
		public int bodyTemp = -1;

		// Token: 0x04001A6A RID: 6762
		public int legTemp = -1;

		// Token: 0x04001A6B RID: 6763
		public int bagTemp = -1;

		// Token: 0x04001A6C RID: 6764
		public int wpTemp = -1;

		// Token: 0x04001A6D RID: 6765
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

		// Token: 0x04001A6E RID: 6766
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

		// Token: 0x04001A6F RID: 6767
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
