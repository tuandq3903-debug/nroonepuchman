using System;

namespace Game6
{
	// Token: 0x0200002E RID: 46
	public abstract class Effect2
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void paint(mGraphics g)
		{
		}

		// Token: 0x040003DF RID: 991
		public static MyVector vEffect3 = new MyVector();

		// Token: 0x040003E0 RID: 992
		public static MyVector vEffect2 = new MyVector();

		// Token: 0x040003E1 RID: 993
		public static MyVector vRemoveEffect2 = new MyVector();

		// Token: 0x040003E2 RID: 994
		public static MyVector vEffect2Outside = new MyVector();

		// Token: 0x040003E3 RID: 995
		public static MyVector vAnimateEffect = new MyVector();

		// Token: 0x040003E4 RID: 996
		public static MyVector vEffectFeet = new MyVector();
	}
}
