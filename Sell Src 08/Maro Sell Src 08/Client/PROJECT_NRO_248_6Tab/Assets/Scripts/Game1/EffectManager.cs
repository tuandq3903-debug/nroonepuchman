using System;

namespace Game1
{
	// Token: 0x0200046B RID: 1131
	public class EffectManager : MyVector
	{
		// Token: 0x0600323B RID: 12859 RVA: 0x0031545C File Offset: 0x0031365C
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

		// Token: 0x0600323C RID: 12860 RVA: 0x003154A1 File Offset: 0x003136A1
		public static void update()
		{
			EffectManager.hiEffects.updateAll();
			EffectManager.mid_2Effects.updateAll();
			EffectManager.midEffects.updateAll();
			EffectManager.lowEffects.updateAll();
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x003154CC File Offset: 0x003136CC
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

		// Token: 0x0600323E RID: 12862 RVA: 0x00315514 File Offset: 0x00313714
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

		// Token: 0x0600323F RID: 12863 RVA: 0x00315552 File Offset: 0x00313752
		public static void remove()
		{
			EffectManager.hiEffects.removeAll();
			EffectManager.lowEffects.removeAll();
			EffectManager.midEffects.removeAll();
			EffectManager.mid_2Effects.removeAll();
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x0031557C File Offset: 0x0031377C
		public static void addHiEffect(Effect_End eff)
		{
			EffectManager.hiEffects.addElement(eff);
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x00315589 File Offset: 0x00313789
		public static void addMidEffects(Effect_End eff)
		{
			EffectManager.midEffects.addElement(eff);
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x00315596 File Offset: 0x00313796
		public static void addMid_2Effects(Effect_End eff)
		{
			EffectManager.mid_2Effects.addElement(eff);
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x003155A3 File Offset: 0x003137A3
		public static void addLowEffect(Effect_End eff)
		{
			EffectManager.lowEffects.addElement(eff);
		}

		// Token: 0x04006074 RID: 24692
		public static EffectManager lowEffects = new EffectManager();

		// Token: 0x04006075 RID: 24693
		public static EffectManager mid_2Effects = new EffectManager();

		// Token: 0x04006076 RID: 24694
		public static EffectManager midEffects = new EffectManager();

		// Token: 0x04006077 RID: 24695
		public static EffectManager hiEffects = new EffectManager();
	}
}
