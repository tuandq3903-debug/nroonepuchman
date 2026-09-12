using System;
using UnityEngine;

namespace Game4
{
	// Token: 0x02000202 RID: 514
	public class ipKeyboard
	{
		// Token: 0x060016EC RID: 5868 RVA: 0x00170560 File Offset: 0x0016E760
		public static void openKeyBoard(string caption, int type, string text, Command action)
		{
			ipKeyboard.act = action;
			TouchScreenKeyboardType t = (type == 0 || type == 2) ? TouchScreenKeyboardType.ASCIICapable : TouchScreenKeyboardType.NumberPad;
			TouchScreenKeyboard.hideInput = false;
			ipKeyboard.tk = TouchScreenKeyboard.Open(text, t, false, false, type == 2, false, caption);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0017059C File Offset: 0x0016E79C
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

		// Token: 0x04002C85 RID: 11397
		private static TouchScreenKeyboard tk;

		// Token: 0x04002C86 RID: 11398
		public static int TEXT;

		// Token: 0x04002C87 RID: 11399
		public static int NUMBERIC = 1;

		// Token: 0x04002C88 RID: 11400
		public static int PASS = 2;

		// Token: 0x04002C89 RID: 11401
		private static Command act;
	}
}
