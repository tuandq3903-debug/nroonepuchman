using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Game2
{
	// Token: 0x0200036D RID: 877
	internal class AutoItem : IChatable
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x00256D22 File Offset: 0x00254F22
		// (set) Token: 0x060026AF RID: 9903 RVA: 0x00256D2A File Offset: 0x00254F2A
		internal int ID { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060026B0 RID: 9904 RVA: 0x00256D33 File Offset: 0x00254F33
		// (set) Token: 0x060026B1 RID: 9905 RVA: 0x00256D3B File Offset: 0x00254F3B
		internal int Quantity { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x00256D44 File Offset: 0x00254F44
		// (set) Token: 0x060026B3 RID: 9907 RVA: 0x00256D4C File Offset: 0x00254F4C
		internal int timeDelay { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x00256D55 File Offset: 0x00254F55
		// (set) Token: 0x060026B5 RID: 9909 RVA: 0x00256D5D File Offset: 0x00254F5D
		internal sbyte indexUI { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x00256D66 File Offset: 0x00254F66
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x00256D6E File Offset: 0x00254F6E
		internal bool isGold { get; set; }

		// Token: 0x17000043 RID: 67
		// (set) Token: 0x060026B8 RID: 9912 RVA: 0x00256D77 File Offset: 0x00254F77
		internal bool isGem
		{
			[CompilerGenerated]
			set
			{
				// this.<isGem>k__BackingField = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x00256D80 File Offset: 0x00254F80
		// (set) Token: 0x060026BA RID: 9914 RVA: 0x00256D88 File Offset: 0x00254F88
		internal long lastTimeUseItem { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x00256D91 File Offset: 0x00254F91
		// (set) Token: 0x060026BC RID: 9916 RVA: 0x00256D99 File Offset: 0x00254F99
		internal long lastTimeBuy { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x00256DA2 File Offset: 0x00254FA2
		// (set) Token: 0x060026BE RID: 9918 RVA: 0x00256DA9 File Offset: 0x00254FA9
		internal static AutoItem itemToAuto { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x00256DB1 File Offset: 0x00254FB1
		// (set) Token: 0x060026C0 RID: 9920 RVA: 0x00256DB8 File Offset: 0x00254FB8
		internal static AutoItem instance { get; set; }

		// Token: 0x060026C1 RID: 9921 RVA: 0x00256DC0 File Offset: 0x00254FC0
		internal static AutoItem gI()
		{
			AutoItem result;
			if ((result = AutoItem.instance) == null)
			{
				result = (AutoItem.instance = new AutoItem());
			}
			return result;
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x0000237F File Offset: 0x0000057F
		internal AutoItem()
		{
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x00256DD6 File Offset: 0x00254FD6
		internal AutoItem(int ID, sbyte indexUI, int timeDelay, bool isGold = false, bool isGem = false)
		{
			this.ID = ID;
			this.indexUI = indexUI;
			this.timeDelay = timeDelay;
			this.isGold = isGold;
			this.isGem = isGem;
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x00256E03 File Offset: 0x00255003
		internal static void Update()
		{
			AutoItem.AutoUse();
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x00256E0C File Offset: 0x0025500C
		private static void AutoUse()
		{
			if (AutoItem.listItemUses.Count == 0 || GameCanvas.gameTick % 20 != 0)
			{
				return;
			}
			AutoItem item = AutoItem.listItemUses.FirstOrDefault((AutoItem i) => mSystem.currentTimeMillis() - i.lastTimeUseItem > (long)(i.timeDelay * 1000));
			if (item != null)
			{
				Item currentItem = Char.myCharz().arrItemBag.FirstOrDefault((Item i) => i != null && i.indexUI == (int)item.indexUI);
				if (currentItem != null && (int)currentItem.template.id == item.ID)
				{
					item.lastTimeUseItem = mSystem.currentTimeMillis();
					Service.gI().useItem(0, 1, item.indexUI, -1);
					return;
				}
				AutoItem.listItemUses.Remove(item);
				GameScr.info1.addInfo("Đã ngưng sử dụng " + ItemTemplates.get((short)item.ID).name, 0);
			}
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x00256F0C File Offset: 0x0025510C
		private static void AutoBuy(AutoItem item)
		{
			// 	AutoItem.<AutoBuy>d__46 <AutoBuy>d__;
			// 	<AutoBuy>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			// 	<AutoBuy>d__.item = item;
			// 	<AutoBuy>d__.<>1__state = -1;
			// 	<AutoBuy>d__.<>t__builder.Start<AutoItem.<AutoBuy>d__46>(ref <AutoBuy>d__);
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x00256F44 File Offset: 0x00255144
		internal void addItemBuys(AutoItem item)
		{
			GameCanvas.panel.hideNow();
			AutoItem.itemToAuto = item;
			string itemName = ItemTemplates.get((short)item.ID).name;
			ChatTextField.gI().strChat = "Auto Mua " + itemName;
			ChatTextField.gI().tfChat.name = "Nhập số lượng";
			ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			ChatTextField.gI().startChat(this, string.Empty);
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x00256FC0 File Offset: 0x002551C0
		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().strChat.StartsWith("Auto Sử Dụng"))
			{
				int time;
				if (int.TryParse(text, out time))
				{
					GameScr.info1.addInfo(string.Format("Auto: {0} [{1} giây]", ItemTemplates.get((short)this.ID).name, time), 0);
					AutoItem.listItemUses.Add(new AutoItem(this.ID, this.indexUI, time, false, false));
				}
				else
				{
					GameCanvas.startOKDlg("Invaild Value!");
				}
			}
			else if (ChatTextField.gI().strChat.StartsWith("Auto Mua"))
			{
				int quantity;
				if (int.TryParse(text, out quantity))
				{
					AutoItem.itemToAuto.Quantity = quantity;
					AutoItem.AutoBuy(AutoItem.itemToAuto);
				}
				else
				{
					GameCanvas.startOKDlg("Invaild Value!");
				}
			}
			this.onCancelChat();
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x0025708C File Offset: 0x0025528C
		internal static void ResetTF(ChatTextField tf)
		{
			tf.strChat = "Chat";
			tf.tfChat.name = "chat";
			tf.to = "";
			tf.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			tf.isShow = false;
			tf.parentScreen = GameScr.gI();
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x002570E1 File Offset: 0x002552E1
		public void onCancelChat()
		{
			AutoItem.ResetTF(ChatTextField.gI());
		}

		// Token: 0x04004A49 RID: 19017
		internal static List<AutoItem> listItemUses = new List<AutoItem>();
	}
}
