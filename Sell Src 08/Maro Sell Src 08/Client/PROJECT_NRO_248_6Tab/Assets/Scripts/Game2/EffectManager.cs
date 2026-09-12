using System;

namespace Game2
{
	// Token: 0x02000393 RID: 915
	public class EffectManager : MyVector
	{
		// Token: 0x06002897 RID: 10391 RVA: 0x002803B8 File Offset: 0x0027E5B8
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

		// Token: 0x06002898 RID: 10392 RVA: 0x002803FD File Offset: 0x0027E5FD
		public static void update()
		{
			EffectManager.hiEffects.updateAll();
			EffectManager.mid_2Effects.updateAll();
			EffectManager.midEffects.updateAll();
			EffectManager.lowEffects.updateAll();
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x00280428 File Offset: 0x0027E628
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

		// Token: 0x0600289A RID: 10394 RVA: 0x00280470 File Offset: 0x0027E670
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

		// Token: 0x0600289B RID: 10395 RVA: 0x002804AE File Offset: 0x0027E6AE
		public static void remove()
		{
			EffectManager.hiEffects.removeAll();
			EffectManager.lowEffects.removeAll();
			EffectManager.midEffects.removeAll();
			EffectManager.mid_2Effects.removeAll();
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x002804D8 File Offset: 0x0027E6D8
		public static void addHiEffect(Effect_End eff)
		{
			EffectManager.hiEffects.addElement(eff);
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x002804E5 File Offset: 0x0027E6E5
		public static void addMidEffects(Effect_End eff)
		{
			EffectManager.midEffects.addElement(eff);
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x002804F2 File Offset: 0x0027E6F2
		public static void addMid_2Effects(Effect_End eff)
		{
			EffectManager.mid_2Effects.addElement(eff);
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x002804FF File Offset: 0x0027E6FF
		public static void addLowEffect(Effect_End eff)
		{
			EffectManager.lowEffects.addElement(eff);
		}

		// Token: 0x04004DF5 RID: 19957
		public static EffectManager lowEffects = new EffectManager();

		// Token: 0x04004DF6 RID: 19958
		public static EffectManager mid_2Effects = new EffectManager();

		// Token: 0x04004DF7 RID: 19959
		public static EffectManager midEffects = new EffectManager();

		// Token: 0x04004DF8 RID: 19960
		public static EffectManager hiEffects = new EffectManager();
	}
}
