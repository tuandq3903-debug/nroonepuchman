using System;
using UnityEngine;

namespace Game1
{
	// Token: 0x0200048A RID: 1162
	public class ipKeyboard
	{
		// Token: 0x060033D8 RID: 13272 RVA: 0x0032F74C File Offset: 0x0032D94C
		public static void openKeyBoard(string caption, int type, string text, Command action)
		{
			ipKeyboard.act = action;
			TouchScreenKeyboardType t = (type == 0 || type == 2) ? TouchScreenKeyboardType.ASCIICapable : TouchScreenKeyboardType.NumberPad;
			TouchScreenKeyboard.hideInput = false;
			ipKeyboard.tk = TouchScreenKeyboard.Open(text, t, false, false, type == 2, false, caption);
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x0032F788 File Offset: 0x0032D988
		public static void update()
		{
			try
			{
				if (ipKeyboard.tk != null && ipKeyboard.tk.done)
				{
					if (ipKeyboard.act != null)
					{
						ipKeyboard.act.perform(ipKeyboard.tk.text);
					}
					ipKeyboard.tk.text = string.Empty;
					ipKeyboard.tk = null;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04006402 RID: 25602
		private static TouchScreenKeyboard tk;

		// Token: 0x04006403 RID: 25603
		public static int TEXT;

		// Token: 0x04006404 RID: 25604
		public static int NUMBERIC = 1;

		// Token: 0x04006405 RID: 25605
		public static int PASS = 2;

		// Token: 0x04006406 RID: 25606
		private static Command act;
	}
}
