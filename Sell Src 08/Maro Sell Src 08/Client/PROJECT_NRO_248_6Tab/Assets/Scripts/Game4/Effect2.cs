using System;

namespace Game4
{
	// Token: 0x020001DE RID: 478
	public abstract class Effect2
	{
		// Token: 0x0600153B RID: 5435 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void paint(mGraphics g)
		{
		}

		// Token: 0x040028DD RID: 10461
		public static MyVector vEffect3 = new MyVector();

		// Token: 0x040028DE RID: 10462
		public static MyVector vEffect2 = new MyVector();

		// Token: 0x040028DF RID: 10463
		public static MyVector vRemoveEffect2 = new MyVector();

		// Token: 0x040028E0 RID: 10464
		public static MyVector vEffect2Outside = new MyVector();

		// Token: 0x040028E1 RID: 10465
		public static MyVector vAnimateEffect = new MyVector();

		// Token: 0x040028E2 RID: 10466
		public static MyVector vEffectFeet = new MyVector();
	}
}
