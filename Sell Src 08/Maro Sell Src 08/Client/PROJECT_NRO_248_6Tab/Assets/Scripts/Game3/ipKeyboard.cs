using System;
using UnityEngine;

namespace Game3
{
	// Token: 0x020002DA RID: 730
	public class ipKeyboard
	{
		// Token: 0x06002090 RID: 8336 RVA: 0x00205604 File Offset: 0x00203804
		public static void openKeyBoard(string caption, int type, string text, Command action)
		{
			ipKeyboard.act = action;
			TouchScreenKeyboardType t = (type == 0 || type == 2) ? TouchScreenKeyboardType.ASCIICapable : TouchScreenKeyboardType.NumberPad;
			TouchScreenKeyboard.hideInput = false;
			ipKeyboard.tk = TouchScreenKeyboard.Open(text, t, false, false, type == 2, false, caption);
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x00205640 File Offset: 0x00203840
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

		// Token: 0x04003F04 RID: 16132
		private static TouchScreenKeyboard tk;

		// Token: 0x04003F05 RID: 16133
		public static int TEXT;

		// Token: 0x04003F06 RID: 16134
		public static int NUMBERIC = 1;

		// Token: 0x04003F07 RID: 16135
		public static int PASS = 2;

		// Token: 0x04003F08 RID: 16136
		private static Command act;
	}
}
