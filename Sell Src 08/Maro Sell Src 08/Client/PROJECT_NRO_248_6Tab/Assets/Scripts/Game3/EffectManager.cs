using System;

namespace Game3
{
	// Token: 0x020002BB RID: 699
	public class EffectManager : MyVector
	{
		// Token: 0x06001EF3 RID: 7923 RVA: 0x001EB314 File Offset: 0x001E9514
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

		// Token: 0x06001EF4 RID: 7924 RVA: 0x001EB359 File Offset: 0x001E9559
		public static void update()
		{
			EffectManager.hiEffects.updateAll();
			EffectManager.mid_2Effects.updateAll();
			EffectManager.midEffects.updateAll();
			EffectManager.lowEffects.updateAll();
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x001EB384 File Offset: 0x001E9584
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

		// Token: 0x06001EF6 RID: 7926 RVA: 0x001EB3CC File Offset: 0x001E95CC
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

		// Token: 0x06001EF7 RID: 7927 RVA: 0x001EB40A File Offset: 0x001E960A
		public static void remove()
		{
			EffectManager.hiEffects.removeAll();
			EffectManager.lowEffects.removeAll();
			EffectManager.midEffects.removeAll();
			EffectManager.mid_2Effects.removeAll();
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x001EB434 File Offset: 0x001E9634
		public static void addHiEffect(Effect_End eff)
		{
			EffectManager.hiEffects.addElement(eff);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x001EB441 File Offset: 0x001E9641
		public static void addMidEffects(Effect_End eff)
		{
			EffectManager.midEffects.addElement(eff);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x001EB44E File Offset: 0x001E964E
		public static void addMid_2Effects(Effect_End eff)
		{
			EffectManager.mid_2Effects.addElement(eff);
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x001EB45B File Offset: 0x001E965B
		public static void addLowEffect(Effect_End eff)
		{
			EffectManager.lowEffects.addElement(eff);
		}

		// Token: 0x04003B76 RID: 15222
		public static EffectManager lowEffects = new EffectManager();

		// Token: 0x04003B77 RID: 15223
		public static EffectManager mid_2Effects = new EffectManager();

		// Token: 0x04003B78 RID: 15224
		public static EffectManager midEffects = new EffectManager();

		// Token: 0x04003B79 RID: 15225
		public static EffectManager hiEffects = new EffectManager();
	}
}
