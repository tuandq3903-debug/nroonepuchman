using System;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000052 RID: 82
	public class ipKeyboard
	{
		// Token: 0x060003A4 RID: 932 RVA: 0x00046310 File Offset: 0x00044510
		public static void openKeyBoard(string caption, int type, string text, Command action)
		{
			ipKeyboard.act = action;
			TouchScreenKeyboardType t = (type == 0 || type == 2) ? TouchScreenKeyboardType.ASCIICapable : TouchScreenKeyboardType.NumberPad;
			TouchScreenKeyboard.hideInput = false;
			ipKeyboard.tk = TouchScreenKeyboard.Open(text, t, false, false, type == 2, false, caption);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0004634C File Offset: 0x0004454C
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

		// Token: 0x04000787 RID: 1927
		private static TouchScreenKeyboard tk;

		// Token: 0x04000788 RID: 1928
		public static int TEXT;

		// Token: 0x04000789 RID: 1929
		public static int NUMBERIC = 1;

		// Token: 0x0400078A RID: 1930
		public static int PASS = 2;

		// Token: 0x0400078B RID: 1931
		private static Command act;
	}
}
