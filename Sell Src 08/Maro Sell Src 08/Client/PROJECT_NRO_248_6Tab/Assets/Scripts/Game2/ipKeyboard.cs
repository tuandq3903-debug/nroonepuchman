using System;
using UnityEngine;

namespace Game2
{
	// Token: 0x020003B2 RID: 946
	public class ipKeyboard
	{
		// Token: 0x06002A34 RID: 10804 RVA: 0x0029A6A8 File Offset: 0x002988A8
		public static void openKeyBoard(string caption, int type, string text, Command action)
		{
			ipKeyboard.act = action;
			TouchScreenKeyboardType t = (type == 0 || type == 2) ? TouchScreenKeyboardType.ASCIICapable : TouchScreenKeyboardType.NumberPad;
			TouchScreenKeyboard.hideInput = false;
			ipKeyboard.tk = TouchScreenKeyboard.Open(text, t, false, false, type == 2, false, caption);
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x0029A6E4 File Offset: 0x002988E4
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

		// Token: 0x04005183 RID: 20867
		private static TouchScreenKeyboard tk;

		// Token: 0x04005184 RID: 20868
		public static int TEXT;

		// Token: 0x04005185 RID: 20869
		public static int NUMBERIC = 1;

		// Token: 0x04005186 RID: 20870
		public static int PASS = 2;

		// Token: 0x04005187 RID: 20871
		private static Command act;
	}
}
