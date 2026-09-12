using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Game1
{
	// Token: 0x02000445 RID: 1093
	internal class AutoItem : IChatable
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x002EBDC6 File Offset: 0x002E9FC6
		// (set) Token: 0x06003053 RID: 12371 RVA: 0x002EBDCE File Offset: 0x002E9FCE
		internal int ID { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x002EBDD7 File Offset: 0x002E9FD7
		// (set) Token: 0x06003055 RID: 12373 RVA: 0x002EBDDF File Offset: 0x002E9FDF
		internal int Quantity { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x002EBDE8 File Offset: 0x002E9FE8
		// (set) Token: 0x06003057 RID: 12375 RVA: 0x002EBDF0 File Offset: 0x002E9FF0
		internal int timeDelay { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x002EBDF9 File Offset: 0x002E9FF9
		// (set) Token: 0x06003059 RID: 12377 RVA: 0x002EBE01 File Offset: 0x002EA001
		internal sbyte indexUI { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600305A RID: 12378 RVA: 0x002EBE0A File Offset: 0x002EA00A
		// (set) Token: 0x0600305B RID: 12379 RVA: 0x002EBE12 File Offset: 0x002EA012
		internal bool isGold { get; set; }

		// Token: 0x17000052 RID: 82
		// (set) Token: 0x0600305C RID: 12380 RVA: 0x002EBE1B File Offset: 0x002EA01B
		internal bool isGem
		{
			[CompilerGenerated]
			set
			{
				// this.<isGem>k__BackingField = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x002EBE24 File Offset: 0x002EA024
		// (set) Token: 0x0600305E RID: 12382 RVA: 0x002EBE2C File Offset: 0x002EA02C
		internal long lastTimeUseItem { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x002EBE35 File Offset: 0x002EA035
		// (set) Token: 0x06003060 RID: 12384 RVA: 0x002EBE3D File Offset: 0x002EA03D
		internal long lastTimeBuy { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x002EBE46 File Offset: 0x002EA046
		// (set) Token: 0x06003062 RID: 12386 RVA: 0x002EBE4D File Offset: 0x002EA04D
		internal static AutoItem itemToAuto { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x002EBE55 File Offset: 0x002EA055
		// (set) Token: 0x06003064 RID: 12388 RVA: 0x002EBE5C File Offset: 0x002EA05C
		internal static AutoItem instance { get; set; }

		// Token: 0x06003065 RID: 12389 RVA: 0x002EBE64 File Offset: 0x002EA064
		internal static AutoItem gI()
		{
			AutoItem result;
			if ((result = AutoItem.instance) == null)
			{
				result = (AutoItem.instance = new AutoItem());
			}
			return result;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x0000237F File Offset: 0x0000057F
		internal AutoItem()
		{
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x002EBE7A File Offset: 0x002EA07A
		internal AutoItem(int ID, sbyte indexUI, int timeDelay, bool isGold = false, bool isGem = false)
		{
			this.ID = ID;
			this.indexUI = indexUI;
			this.timeDelay = timeDelay;
			this.isGold = isGold;
			this.isGem = isGem;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x002EBEA7 File Offset: 0x002EA0A7
		internal static void Update()
		{
			AutoItem.AutoUse();
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x002EBEB0 File Offset: 0x002EA0B0
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

		// Token: 0x0600306A RID: 12394 RVA: 0x002EBFB0 File Offset: 0x002EA1B0
		private static void AutoBuy(AutoItem item)
		{
			// AutoItem.<AutoBuy>d__46 <AutoBuy>d__;
			// <AutoBuy>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			// <AutoBuy>d__.item = item;
			// <AutoBuy>d__.<>1__state = -1;
			// <AutoBuy>d__.<>t__builder.Start<AutoItem.<AutoBuy>d__46>(ref <AutoBuy>d__);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x002EBFE8 File Offset: 0x002EA1E8
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

		// Token: 0x0600306C RID: 12396 RVA: 0x002EC064 File Offset: 0x002EA264
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

		// Token: 0x0600306D RID: 12397 RVA: 0x002EC130 File Offset: 0x002EA330
		internal static void ResetTF(ChatTextField tf)
		{
			tf.strChat = "Chat";
			tf.tfChat.name = "chat";
			tf.to = "";
			tf.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			tf.isShow = false;
			tf.parentScreen = GameScr.gI();
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x002EC185 File Offset: 0x002EA385
		public void onCancelChat()
		{
			AutoItem.ResetTF(ChatTextField.gI());
		}

		// Token: 0x04005CC8 RID: 23752
		internal static List<AutoItem> listItemUses = new List<AutoItem>();
	}
}
