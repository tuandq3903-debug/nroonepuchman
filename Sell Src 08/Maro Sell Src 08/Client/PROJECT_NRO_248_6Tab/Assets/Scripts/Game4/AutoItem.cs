using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Game4
{
	// Token: 0x020001BD RID: 445
	internal class AutoItem : IChatable
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0012CBDA File Offset: 0x0012ADDA
		// (set) Token: 0x06001367 RID: 4967 RVA: 0x0012CBE2 File Offset: 0x0012ADE2
		internal int ID { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0012CBEB File Offset: 0x0012ADEB
		// (set) Token: 0x06001369 RID: 4969 RVA: 0x0012CBF3 File Offset: 0x0012ADF3
		internal int Quantity { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0012CBFC File Offset: 0x0012ADFC
		// (set) Token: 0x0600136B RID: 4971 RVA: 0x0012CC04 File Offset: 0x0012AE04
		internal int timeDelay { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600136C RID: 4972 RVA: 0x0012CC0D File Offset: 0x0012AE0D
		// (set) Token: 0x0600136D RID: 4973 RVA: 0x0012CC15 File Offset: 0x0012AE15
		internal sbyte indexUI { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600136E RID: 4974 RVA: 0x0012CC1E File Offset: 0x0012AE1E
		// (set) Token: 0x0600136F RID: 4975 RVA: 0x0012CC26 File Offset: 0x0012AE26
		internal bool isGold { get; set; }

		// Token: 0x17000025 RID: 37
		// (set) Token: 0x06001370 RID: 4976 RVA: 0x0012CC2F File Offset: 0x0012AE2F
		internal bool isGem
		{
			[CompilerGenerated]
			set
			{
				
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06001371 RID: 4977 RVA: 0x0012CC38 File Offset: 0x0012AE38
		// (set) Token: 0x06001372 RID: 4978 RVA: 0x0012CC40 File Offset: 0x0012AE40
		internal long lastTimeUseItem { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x0012CC49 File Offset: 0x0012AE49
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x0012CC51 File Offset: 0x0012AE51
		internal long lastTimeBuy { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0012CC5A File Offset: 0x0012AE5A
		// (set) Token: 0x06001376 RID: 4982 RVA: 0x0012CC61 File Offset: 0x0012AE61
		internal static AutoItem itemToAuto { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0012CC69 File Offset: 0x0012AE69
		// (set) Token: 0x06001378 RID: 4984 RVA: 0x0012CC70 File Offset: 0x0012AE70
		internal static AutoItem instance { get; set; }

		// Token: 0x06001379 RID: 4985 RVA: 0x0012CC78 File Offset: 0x0012AE78
		internal static AutoItem gI()
		{
			AutoItem result;
			if ((result = AutoItem.instance) == null)
			{
				result = (AutoItem.instance = new AutoItem());
			}
			return result;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0000237F File Offset: 0x0000057F
		internal AutoItem()
		{
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0012CC8E File Offset: 0x0012AE8E
		internal AutoItem(int ID, sbyte indexUI, int timeDelay, bool isGold = false, bool isGem = false)
		{
			this.ID = ID;
			this.indexUI = indexUI;
			this.timeDelay = timeDelay;
			this.isGold = isGold;
			this.isGem = isGem;
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0012CCBB File Offset: 0x0012AEBB
		internal static void Update()
		{
			AutoItem.AutoUse();
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0012CCC4 File Offset: 0x0012AEC4
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

		// Token: 0x0600137E RID: 4990 RVA: 0x0012CDC4 File Offset: 0x0012AFC4
		

		// Token: 0x0600137F RID: 4991 RVA: 0x0012CDFC File Offset: 0x0012AFFC
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

		// Token: 0x06001380 RID: 4992 RVA: 0x0012CE78 File Offset: 0x0012B078
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
					
				}
				else
				{
					GameCanvas.startOKDlg("Invaild Value!");
				}
			}
			this.onCancelChat();
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0012CF44 File Offset: 0x0012B144
		internal static void ResetTF(ChatTextField tf)
		{
			tf.strChat = "Chat";
			tf.tfChat.name = "chat";
			tf.to = "";
			tf.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			tf.isShow = false;
			tf.parentScreen = GameScr.gI();
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0012CF99 File Offset: 0x0012B199
		public void onCancelChat()
		{
			AutoItem.ResetTF(ChatTextField.gI());
		}

		// Token: 0x0400254B RID: 9547
		internal static List<AutoItem> listItemUses = new List<AutoItem>();
	}
}
