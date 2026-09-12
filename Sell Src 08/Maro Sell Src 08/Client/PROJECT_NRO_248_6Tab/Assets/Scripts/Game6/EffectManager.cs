using System;

namespace Game6
{
	// Token: 0x02000033 RID: 51
	public class EffectManager : MyVector
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0002BFC0 File Offset: 0x0002A1C0
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

		// Token: 0x06000208 RID: 520 RVA: 0x0002C005 File Offset: 0x0002A205
		public static void update()
		{
			EffectManager.hiEffects.updateAll();
			EffectManager.mid_2Effects.updateAll();
			EffectManager.midEffects.updateAll();
			EffectManager.lowEffects.updateAll();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0002C030 File Offset: 0x0002A230
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

		// Token: 0x0600020A RID: 522 RVA: 0x0002C078 File Offset: 0x0002A278
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

		// Token: 0x0600020B RID: 523 RVA: 0x0002C0B6 File Offset: 0x0002A2B6
		public static void remove()
		{
			EffectManager.hiEffects.removeAll();
			EffectManager.lowEffects.removeAll();
			EffectManager.midEffects.removeAll();
			EffectManager.mid_2Effects.removeAll();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0002C0E0 File Offset: 0x0002A2E0
		public static void addHiEffect(Effect_End eff)
		{
			EffectManager.hiEffects.addElement(eff);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0002C0ED File Offset: 0x0002A2ED
		public static void addMidEffects(Effect_End eff)
		{
			EffectManager.midEffects.addElement(eff);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0002C0FA File Offset: 0x0002A2FA
		public static void addMid_2Effects(Effect_End eff)
		{
			EffectManager.mid_2Effects.addElement(eff);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0002C107 File Offset: 0x0002A307
		public static void addLowEffect(Effect_End eff)
		{
			EffectManager.lowEffects.addElement(eff);
		}

		// Token: 0x040003F9 RID: 1017
		public static EffectManager lowEffects = new EffectManager();

		// Token: 0x040003FA RID: 1018
		public static EffectManager mid_2Effects = new EffectManager();

		// Token: 0x040003FB RID: 1019
		public static EffectManager midEffects = new EffectManager();

		// Token: 0x040003FC RID: 1020
		public static EffectManager hiEffects = new EffectManager();
	}
}
