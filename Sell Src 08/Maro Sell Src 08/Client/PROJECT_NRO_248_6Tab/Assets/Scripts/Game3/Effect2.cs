using System;

namespace Game3
{
	// Token: 0x020002B6 RID: 694
	public abstract class Effect2
	{
		// Token: 0x06001EDF RID: 7903 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void paint(mGraphics g)
		{
		}

		// Token: 0x04003B5C RID: 15196
		public static MyVector vEffect3 = new MyVector();

		// Token: 0x04003B5D RID: 15197
		public static MyVector vEffect2 = new MyVector();

		// Token: 0x04003B5E RID: 15198
		public static MyVector vRemoveEffect2 = new MyVector();

		// Token: 0x04003B5F RID: 15199
		public static MyVector vEffect2Outside = new MyVector();

		// Token: 0x04003B60 RID: 15200
		public static MyVector vAnimateEffect = new MyVector();

		// Token: 0x04003B61 RID: 15201
		public static MyVector vEffectFeet = new MyVector();
	}
}
