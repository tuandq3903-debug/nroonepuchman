using System;
using UnityEngine;

namespace Game5
{
	// Token: 0x0200012A RID: 298
	public class ipKeyboard
	{
		// Token: 0x06000D48 RID: 3400 RVA: 0x000DB4BC File Offset: 0x000D96BC
		public static void openKeyBoard(string caption, int type, string text, Command action)
		{
			ipKeyboard.act = action;
			TouchScreenKeyboardType t = (type == 0 || type == 2) ? TouchScreenKeyboardType.ASCIICapable : TouchScreenKeyboardType.NumberPad;
			TouchScreenKeyboard.hideInput = false;
			ipKeyboard.tk = TouchScreenKeyboard.Open(text, t, false, false, type == 2, false, caption);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000DB4F8 File Offset: 0x000D96F8
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

		// Token: 0x04001A06 RID: 6662
		private static TouchScreenKeyboard tk;

		// Token: 0x04001A07 RID: 6663
		public static int TEXT;

		// Token: 0x04001A08 RID: 6664
		public static int NUMBERIC = 1;

		// Token: 0x04001A09 RID: 6665
		public static int PASS = 2;

		// Token: 0x04001A0A RID: 6666
		private static Command act;
	}
}
