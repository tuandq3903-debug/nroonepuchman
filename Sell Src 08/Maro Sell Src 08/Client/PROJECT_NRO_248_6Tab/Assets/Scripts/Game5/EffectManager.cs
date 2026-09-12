using System;

namespace Game5
{
	// Token: 0x0200010B RID: 267
	public class EffectManager : MyVector
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x000C11CC File Offset: 0x000BF3CC
		public void updateAll()
		{
			for (int num = base.size() - 1; num >= 0; num--)
			{
				Effect_End effect_End = (Effect_End)base.elementAt(num);
				if (effect_End != null)
				{
					effect_End.update();
					if (effect_End.isRemove)
					{
						base.removeElementAt(num);
					}
				}
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000C1211 File Offset: 0x000BF411
		public static void update()
		{
			EffectManager.hiEffects.updateAll();
			EffectManager.mid_2Effects.updateAll();
			EffectManager.midEffects.updateAll();
			EffectManager.lowEffects.updateAll();
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x000C123C File Offset: 0x000BF43C
		public void paintAll(mGraphics g)
		{
			for (int i = 0; i < base.size(); i++)
			{
				Effect_End effect_End = (Effect_End)base.elementAt(i);
				if (effect_End != null && !effect_End.isRemove)
				{
					((Effect_End)base.elementAt(i)).paint(g);
				}
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x000C1284 File Offset: 0x000BF484
		public void removeAll()
		{
			for (int num = base.size() - 1; num >= 0; num--)
			{
				Effect_End effect_End = (Effect_End)base.elementAt(num);
				if (effect_End != null)
				{
					effect_End.isRemove = true;
					base.removeElementAt(num);
				}
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000C12C2 File Offset: 0x000BF4C2
		public static void remove()
		{
			EffectManager.hiEffects.removeAll();
			EffectManager.lowEffects.removeAll();
			EffectManager.midEffects.removeAll();
			EffectManager.mid_2Effects.removeAll();
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000C12EC File Offset: 0x000BF4EC
		public static void addHiEffect(Effect_End eff)
		{
			EffectManager.hiEffects.addElement(eff);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000C12F9 File Offset: 0x000BF4F9
		public static void addMidEffects(Effect_End eff)
		{
			EffectManager.midEffects.addElement(eff);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000C1306 File Offset: 0x000BF506
		public static void addMid_2Effects(Effect_End eff)
		{
			EffectManager.mid_2Effects.addElement(eff);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000C1313 File Offset: 0x000BF513
		public static void addLowEffect(Effect_End eff)
		{
			EffectManager.lowEffects.addElement(eff);
		}

		// Token: 0x04001678 RID: 5752
		public static EffectManager lowEffects = new EffectManager();

		// Token: 0x04001679 RID: 5753
		public static EffectManager mid_2Effects = new EffectManager();

		// Token: 0x0400167A RID: 5754
		public static EffectManager midEffects = new EffectManager();

		// Token: 0x0400167B RID: 5755
		public static EffectManager hiEffects = new EffectManager();
	}
}
