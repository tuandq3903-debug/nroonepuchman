using System;

namespace Game2
{
	// Token: 0x0200038E RID: 910
	public abstract class Effect2
	{
		// Token: 0x06002883 RID: 10371 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void update()
		{
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000034B9 File Offset: 0x000016B9
		public virtual void paint(mGraphics g)
		{
		}

		// Token: 0x04004DDB RID: 19931
		public static MyVector vEffect3 = new MyVector();

		// Token: 0x04004DDC RID: 19932
		public static MyVector vEffect2 = new MyVector();

		// Token: 0x04004DDD RID: 19933
		public static MyVector vRemoveEffect2 = new MyVector();

		// Token: 0x04004DDE RID: 19934
		public static MyVector vEffect2Outside = new MyVector();

		// Token: 0x04004DDF RID: 19935
		public static MyVector vAnimateEffect = new MyVector();

		// Token: 0x04004DE0 RID: 19936
		public static MyVector vEffectFeet = new MyVector();
	}
}
