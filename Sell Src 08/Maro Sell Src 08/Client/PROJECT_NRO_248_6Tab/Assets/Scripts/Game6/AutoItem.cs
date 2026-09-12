using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Game6
{
	// Token: 0x0200000D RID: 13
	internal class AutoItem : IChatable
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002946 File Offset: 0x00000B46
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000294E File Offset: 0x00000B4E
		internal int ID { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002957 File Offset: 0x00000B57
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000295F File Offset: 0x00000B5F
		internal int Quantity { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002968 File Offset: 0x00000B68
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002970 File Offset: 0x00000B70
		internal int timeDelay { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002979 File Offset: 0x00000B79
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002981 File Offset: 0x00000B81
		internal sbyte indexUI { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000298A File Offset: 0x00000B8A
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002992 File Offset: 0x00000B92
		internal bool isGold { get; set; }

		// Token: 0x17000007 RID: 7
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000299B File Offset: 0x00000B9B
		internal bool isGem
		{
			[CompilerGenerated]
			set
			{
				// this.<isGem>k__BackingField = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000029A4 File Offset: 0x00000BA4
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000029AC File Offset: 0x00000BAC
		internal long lastTimeUseItem { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000029B5 File Offset: 0x00000BB5
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000029BD File Offset: 0x00000BBD
		internal long lastTimeBuy { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000029C6 File Offset: 0x00000BC6
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000029CD File Offset: 0x00000BCD
		internal static AutoItem itemToAuto { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000029D5 File Offset: 0x00000BD5
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000029DC File Offset: 0x00000BDC
		internal static AutoItem instance { get; set; }

		// Token: 0x06000031 RID: 49 RVA: 0x000029E4 File Offset: 0x00000BE4
		internal static AutoItem gI()
		{
			AutoItem result;
			if ((result = AutoItem.instance) == null)
			{
				result = (AutoItem.instance = new AutoItem());
			}
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000237F File Offset: 0x0000057F
		internal AutoItem()
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029FA File Offset: 0x00000BFA
		internal AutoItem(int ID, sbyte indexUI, int timeDelay, bool isGold = false, bool isGem = false)
		{
			this.ID = ID;
			this.indexUI = indexUI;
			this.timeDelay = timeDelay;
			this.isGold = isGold;
			this.isGem = isGem;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A27 File Offset: 0x00000C27
		internal static void Update()
		{
			AutoItem.AutoUse();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A30 File Offset: 0x00000C30
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

		// Token: 0x06000036 RID: 54 RVA: 0x00002B30 File Offset: 0x00000D30
		private static void AutoBuy(AutoItem item)
		{
			// AutoItem.<AutoBuy>d__46 <AutoBuy>d__;
			// <AutoBuy>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			// <AutoBuy>d__.item = item;
			// <AutoBuy>d__.<>1__state = -1;
			// <AutoBuy>d__.<>t__builder.Start<AutoItem.<AutoBuy>d__46>(ref <AutoBuy>d__);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B68 File Offset: 0x00000D68
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

		// Token: 0x06000038 RID: 56 RVA: 0x00002BE4 File Offset: 0x00000DE4
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

		// Token: 0x06000039 RID: 57 RVA: 0x00002CB0 File Offset: 0x00000EB0
		internal static void ResetTF(ChatTextField tf)
		{
			tf.strChat = "Chat";
			tf.tfChat.name = "chat";
			tf.to = "";
			tf.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			tf.isShow = false;
			tf.parentScreen = GameScr.gI();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D05 File Offset: 0x00000F05
		public void onCancelChat()
		{
			AutoItem.ResetTF(ChatTextField.gI());
		}

		// Token: 0x0400004E RID: 78
		internal static List<AutoItem> listItemUses = new List<AutoItem>();
	}
}
