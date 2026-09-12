using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Game3
{
	// Token: 0x02000295 RID: 661
	internal class AutoItem : IChatable
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x001C1C7E File Offset: 0x001BFE7E
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x001C1C86 File Offset: 0x001BFE86
		internal int ID { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x001C1C8F File Offset: 0x001BFE8F
		// (set) Token: 0x06001D0D RID: 7437 RVA: 0x001C1C97 File Offset: 0x001BFE97
		internal int Quantity { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x001C1CA0 File Offset: 0x001BFEA0
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x001C1CA8 File Offset: 0x001BFEA8
		internal int timeDelay { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x001C1CB1 File Offset: 0x001BFEB1
		// (set) Token: 0x06001D11 RID: 7441 RVA: 0x001C1CB9 File Offset: 0x001BFEB9
		internal sbyte indexUI { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x001C1CC2 File Offset: 0x001BFEC2
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x001C1CCA File Offset: 0x001BFECA
		internal bool isGold { get; set; }

		// Token: 0x17000034 RID: 52
		// (set) Token: 0x06001D14 RID: 7444 RVA: 0x001C1CD3 File Offset: 0x001BFED3
		internal bool isGem
		{
			[CompilerGenerated]
			set
			{
				// this.< isGem > k__BackingField = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x001C1CDC File Offset: 0x001BFEDC
		// (set) Token: 0x06001D16 RID: 7446 RVA: 0x001C1CE4 File Offset: 0x001BFEE4
		internal long lastTimeUseItem { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x001C1CED File Offset: 0x001BFEED
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x001C1CF5 File Offset: 0x001BFEF5
		internal long lastTimeBuy { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x001C1CFE File Offset: 0x001BFEFE
		// (set) Token: 0x06001D1A RID: 7450 RVA: 0x001C1D05 File Offset: 0x001BFF05
		internal static AutoItem itemToAuto { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x001C1D0D File Offset: 0x001BFF0D
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x001C1D14 File Offset: 0x001BFF14
		internal static AutoItem instance { get; set; }

		// Token: 0x06001D1D RID: 7453 RVA: 0x001C1D1C File Offset: 0x001BFF1C
		internal static AutoItem gI()
		{
			AutoItem result;
			if ((result = AutoItem.instance) == null)
			{
				result = (AutoItem.instance = new AutoItem());
			}
			return result;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x0000237F File Offset: 0x0000057F
		internal AutoItem()
		{
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x001C1D32 File Offset: 0x001BFF32
		internal AutoItem(int ID, sbyte indexUI, int timeDelay, bool isGold = false, bool isGem = false)
		{
			this.ID = ID;
			this.indexUI = indexUI;
			this.timeDelay = timeDelay;
			this.isGold = isGold;
			this.isGem = isGem;
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x001C1D5F File Offset: 0x001BFF5F
		internal static void Update()
		{
			AutoItem.AutoUse();
		}

		// Token: 0x06001D21 RID: 7457 RVA: 0x001C1D68 File Offset: 0x001BFF68
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

		// Token: 0x06001D22 RID: 7458 RVA: 0x001C1E68 File Offset: 0x001C0068
		private static void AutoBuy(AutoItem item)
		{
			// 	AutoItem.<AutoBuy>d__46 <AutoBuy>d__;
			// 	<AutoBuy>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			// 	<AutoBuy>d__.item = item;
			// 	<AutoBuy>d__.<>1__state = -1;
			// 	<AutoBuy>d__.<>t__builder.Start<AutoItem.<AutoBuy>d__46>(ref <AutoBuy>d__);
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x001C1EA0 File Offset: 0x001C00A0
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

		// Token: 0x06001D24 RID: 7460 RVA: 0x001C1F1C File Offset: 0x001C011C
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

		// Token: 0x06001D25 RID: 7461 RVA: 0x001C1FE8 File Offset: 0x001C01E8
		internal static void ResetTF(ChatTextField tf)
		{
			tf.strChat = "Chat";
			tf.tfChat.name = "chat";
			tf.to = "";
			tf.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			tf.isShow = false;
			tf.parentScreen = GameScr.gI();
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x001C203D File Offset: 0x001C023D
		public void onCancelChat()
		{
			AutoItem.ResetTF(ChatTextField.gI());
		}

		// Token: 0x040037CA RID: 14282
		internal static List<AutoItem> listItemUses = new List<AutoItem>();
	}
}
