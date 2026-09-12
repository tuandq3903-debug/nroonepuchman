using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Game5
{
	// Token: 0x020000E5 RID: 229
	internal class AutoItem : IChatable
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00097B36 File Offset: 0x00095D36
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00097B3E File Offset: 0x00095D3E
		internal int ID { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00097B47 File Offset: 0x00095D47
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00097B4F File Offset: 0x00095D4F
		internal int Quantity { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00097B58 File Offset: 0x00095D58
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x00097B60 File Offset: 0x00095D60
		internal int timeDelay { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00097B69 File Offset: 0x00095D69
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x00097B71 File Offset: 0x00095D71
		internal sbyte indexUI { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00097B7A File Offset: 0x00095D7A
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x00097B82 File Offset: 0x00095D82
		internal bool isGold { get; set; }

		// Token: 0x17000016 RID: 22
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00097B8B File Offset: 0x00095D8B
		internal bool isGem
		{
			[CompilerGenerated]
			set
			{
				// this.<isGem>k__BackingField = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00097B94 File Offset: 0x00095D94
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x00097B9C File Offset: 0x00095D9C
		internal long lastTimeUseItem { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00097BA5 File Offset: 0x00095DA5
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x00097BAD File Offset: 0x00095DAD
		internal long lastTimeBuy { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00097BB6 File Offset: 0x00095DB6
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00097BBD File Offset: 0x00095DBD
		internal static AutoItem itemToAuto { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00097BC5 File Offset: 0x00095DC5
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x00097BCC File Offset: 0x00095DCC
		internal static AutoItem instance { get; set; }

		// Token: 0x060009D5 RID: 2517 RVA: 0x00097BD4 File Offset: 0x00095DD4
		internal static AutoItem gI()
		{
			AutoItem result;
			if ((result = AutoItem.instance) == null)
			{
				result = (AutoItem.instance = new AutoItem());
			}
			return result;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0000237F File Offset: 0x0000057F
		internal AutoItem()
		{
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00097BEA File Offset: 0x00095DEA
		internal AutoItem(int ID, sbyte indexUI, int timeDelay, bool isGold = false, bool isGem = false)
		{
			this.ID = ID;
			this.indexUI = indexUI;
			this.timeDelay = timeDelay;
			this.isGold = isGold;
			this.isGem = isGem;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00097C17 File Offset: 0x00095E17
		internal static void Update()
		{
			AutoItem.AutoUse();
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00097C20 File Offset: 0x00095E20
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

		// Token: 0x060009DA RID: 2522 RVA: 0x00097D20 File Offset: 0x00095F20
		private static void AutoBuy(AutoItem item)
		{
			// AutoItem.<AutoBuy>d__46 <AutoBuy>d__;
			// <AutoBuy>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			// <AutoBuy>d__.item = item;
			// <AutoBuy>d__.<>1__state = -1;
			// <AutoBuy>d__.<>t__builder.Start<AutoItem.<AutoBuy>d__46>(ref <AutoBuy>d__);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x00097D58 File Offset: 0x00095F58
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

		// Token: 0x060009DC RID: 2524 RVA: 0x00097DD4 File Offset: 0x00095FD4
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

		// Token: 0x060009DD RID: 2525 RVA: 0x00097EA0 File Offset: 0x000960A0
		internal static void ResetTF(ChatTextField tf)
		{
			tf.strChat = "Chat";
			tf.tfChat.name = "chat";
			tf.to = "";
			tf.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			tf.isShow = false;
			tf.parentScreen = GameScr.gI();
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x00097EF5 File Offset: 0x000960F5
		public void onCancelChat()
		{
			AutoItem.ResetTF(ChatTextField.gI());
		}

		// Token: 0x040012CC RID: 4812
		internal static List<AutoItem> listItemUses = new List<AutoItem>();
	}
}
