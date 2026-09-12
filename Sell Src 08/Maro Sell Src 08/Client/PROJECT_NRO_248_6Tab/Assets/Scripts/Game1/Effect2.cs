using System;

namespace Game1
{
	// Token: 0x02000466 RID: 1126
	public abstract class Effect2
	{
		// Token: 0x06003227 RID: 12839 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void paint(mGraphics g)
		{
		}

		// Token: 0x0400605A RID: 24666
		public static MyVector vEffect3 = new MyVector();

		// Token: 0x0400605B RID: 24667
		public static MyVector vEffect2 = new MyVector();

		// Token: 0x0400605C RID: 24668
		public static MyVector vRemoveEffect2 = new MyVector();

		// Token: 0x0400605D RID: 24669
		public static MyVector vEffect2Outside = new MyVector();

		// Token: 0x0400605E RID: 24670
		public static MyVector vAnimateEffect = new MyVector();

		// Token: 0x0400605F RID: 24671
		public static MyVector vEffectFeet = new MyVector();
	}
}
