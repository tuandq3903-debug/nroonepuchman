using System;
using System.Collections;
using UnityEngine;

namespace Game6
{
	// Token: 0x0200007D RID: 125
	public class MyKeyMap
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x0005AE84 File Offset: 0x00059084
		static MyKeyMap()
		{
			MyKeyMap.h.Add(KeyCode.A, 97);
			MyKeyMap.h.Add(KeyCode.B, 98);
			MyKeyMap.h.Add(KeyCode.C, 99);
			MyKeyMap.h.Add(KeyCode.D, 100);
			MyKeyMap.h.Add(KeyCode.E, 101);
			MyKeyMap.h.Add(KeyCode.F, 102);
			MyKeyMap.h.Add(KeyCode.G, 103);
			MyKeyMap.h.Add(KeyCode.H, 104);
			MyKeyMap.h.Add(KeyCode.I, 105);
			MyKeyMap.h.Add(KeyCode.J, 106);
			MyKeyMap.h.Add(KeyCode.K, 107);
			MyKeyMap.h.Add(KeyCode.L, 108);
			MyKeyMap.h.Add(KeyCode.M, 109);
			MyKeyMap.h.Add(KeyCode.N, 110);
			MyKeyMap.h.Add(KeyCode.O, 111);
			MyKeyMap.h.Add(KeyCode.P, 112);
			MyKeyMap.h.Add(KeyCode.Q, 113);
			MyKeyMap.h.Add(KeyCode.R, 114);
			MyKeyMap.h.Add(KeyCode.S, 115);
			MyKeyMap.h.Add(KeyCode.T, 116);
			MyKeyMap.h.Add(KeyCode.U, 117);
			MyKeyMap.h.Add(KeyCode.V, 118);
			MyKeyMap.h.Add(KeyCode.W, 119);
			MyKeyMap.h.Add(KeyCode.X, 120);
			MyKeyMap.h.Add(KeyCode.Y, 121);
			MyKeyMap.h.Add(KeyCode.Z, 122);
			MyKeyMap.h.Add(KeyCode.Alpha0, 48);
			MyKeyMap.h.Add(KeyCode.Alpha1, 49);
			MyKeyMap.h.Add(KeyCode.Alpha2, 50);
			MyKeyMap.h.Add(KeyCode.Alpha3, 51);
			MyKeyMap.h.Add(KeyCode.Alpha4, 52);
			MyKeyMap.h.Add(KeyCode.Alpha5, 53);
			MyKeyMap.h.Add(KeyCode.Alpha6, 54);
			MyKeyMap.h.Add(KeyCode.Alpha7, 55);
			MyKeyMap.h.Add(KeyCode.Alpha8, 56);
			MyKeyMap.h.Add(KeyCode.Alpha9, 57);
			MyKeyMap.h.Add(KeyCode.Space, 32);
			MyKeyMap.h.Add(KeyCode.F1, -21);
			MyKeyMap.h.Add(KeyCode.F2, -22);
			MyKeyMap.h.Add(KeyCode.Equals, -25);
			MyKeyMap.h.Add(KeyCode.Minus, 45);
			MyKeyMap.h.Add(KeyCode.F3, -23);
			MyKeyMap.h.Add(KeyCode.UpArrow, -1);
			MyKeyMap.h.Add(KeyCode.DownArrow, -2);
			MyKeyMap.h.Add(KeyCode.LeftArrow, -3);
			MyKeyMap.h.Add(KeyCode.RightArrow, -4);
			MyKeyMap.h.Add(KeyCode.Backspace, -8);
			MyKeyMap.h.Add(KeyCode.Return, -5);
			MyKeyMap.h.Add(KeyCode.Period, 46);
			MyKeyMap.h.Add(KeyCode.At, 64);
			MyKeyMap.h.Add(KeyCode.Tab, -26);
			MyKeyMap.h.Add(KeyCode.KeypadEnter, -5);
			MyKeyMap.h.Add(KeyCode.Comma, 44);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0005B3AC File Offset: 0x000595AC
		public static int map(KeyCode k)
		{
			object obj = MyKeyMap.h[k];
			if (obj == null)
			{
				return 0;
			}
			return (int)obj;
		}

		// Token: 0x04000CE0 RID: 3296
		private static Hashtable h = new Hashtable();
	}
}
