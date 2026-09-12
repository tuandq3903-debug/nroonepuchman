using System;

namespace Game5
{
	// Token: 0x02000106 RID: 262
	public abstract class Effect2
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void paint(mGraphics g)
		{
		}

		// Token: 0x0400165E RID: 5726
		public static MyVector vEffect3 = new MyVector();

		// Token: 0x0400165F RID: 5727
		public static MyVector vEffect2 = new MyVector();

		// Token: 0x04001660 RID: 5728
		public static MyVector vRemoveEffect2 = new MyVector();

		// Token: 0x04001661 RID: 5729
		public static MyVector vEffect2Outside = new MyVector();

		// Token: 0x04001662 RID: 5730
		public static MyVector vAnimateEffect = new MyVector();

		// Token: 0x04001663 RID: 5731
		public static MyVector vEffectFeet = new MyVector();
	}
}
