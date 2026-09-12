using System;

namespace Game4
{
	// Token: 0x020001FB RID: 507
	public class InfoItem
	{
		// Token: 0x060016D0 RID: 5840 RVA: 0x0016F25D File Offset: 0x0016D45D
		public InfoItem(string s)
		{
			this.f = mFont.tahoma_7_green2;
			this.s = s;
			this.speed = 20;
		}

		// Token: 0x04002C2B RID: 11307
		public string s;

		// Token: 0x04002C2C RID: 11308
		private mFont f;

		// Token: 0x04002C2D RID: 11309
		public int speed = 70;

		// Token: 0x04002C2E RID: 11310
		public Char charInfo;

		// Token: 0x04002C2F RID: 11311
		public bool isChatServer;

		// Token: 0x04002C30 RID: 11312
		public bool isOnline;

		// Token: 0x04002C31 RID: 11313
		public int timeCount;

		// Token: 0x04002C32 RID: 11314
		public int maxTime;

		// Token: 0x04002C33 RID: 11315
		public long last;

		// Token: 0x04002C34 RID: 11316
		public long curr;
	}
}
