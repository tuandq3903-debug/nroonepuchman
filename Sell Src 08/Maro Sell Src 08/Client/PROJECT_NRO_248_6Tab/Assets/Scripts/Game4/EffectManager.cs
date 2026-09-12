using System;

namespace Game4
{
	// Token: 0x020001E3 RID: 483
	public class EffectManager : MyVector
	{
		// Token: 0x0600154F RID: 5455 RVA: 0x00156270 File Offset: 0x00154470
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

		// Token: 0x06001550 RID: 5456 RVA: 0x001562B5 File Offset: 0x001544B5
		public static void update()
		{
			EffectManager.hiEffects.updateAll();
			EffectManager.mid_2Effects.updateAll();
			EffectManager.midEffects.updateAll();
			EffectManager.lowEffects.updateAll();
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x001562E0 File Offset: 0x001544E0
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

		// Token: 0x06001552 RID: 5458 RVA: 0x00156328 File Offset: 0x00154528
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

		// Token: 0x06001553 RID: 5459 RVA: 0x00156366 File Offset: 0x00154566
		public static void remove()
		{
			EffectManager.hiEffects.removeAll();
			EffectManager.lowEffects.removeAll();
			EffectManager.midEffects.removeAll();
			EffectManager.mid_2Effects.removeAll();
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00156390 File Offset: 0x00154590
		public static void addHiEffect(Effect_End eff)
		{
			EffectManager.hiEffects.addElement(eff);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0015639D File Offset: 0x0015459D
		public static void addMidEffects(Effect_End eff)
		{
			EffectManager.midEffects.addElement(eff);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x001563AA File Offset: 0x001545AA
		public static void addMid_2Effects(Effect_End eff)
		{
			EffectManager.mid_2Effects.addElement(eff);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x001563B7 File Offset: 0x001545B7
		public static void addLowEffect(Effect_End eff)
		{
			EffectManager.lowEffects.addElement(eff);
		}

		// Token: 0x040028F7 RID: 10487
		public static EffectManager lowEffects = new EffectManager();

		// Token: 0x040028F8 RID: 10488
		public static EffectManager mid_2Effects = new EffectManager();

		// Token: 0x040028F9 RID: 10489
		public static EffectManager midEffects = new EffectManager();

		// Token: 0x040028FA RID: 10490
		public static EffectManager hiEffects = new EffectManager();
	}
}
