using System;

namespace Game5
{
	// Token: 0x02000123 RID: 291
	public class InfoItem
	{
		// Token: 0x06000D2C RID: 3372 RVA: 0x000DA1B9 File Offset: 0x000D83B9
		public InfoItem(string s)
		{
			this.f = mFont.tahoma_7_green2;
			this.s = s;
			this.speed = 20;
		}

		// Token: 0x040019AC RID: 6572
		public string s;

		// Token: 0x040019AD RID: 6573
		private mFont f;

		// Token: 0x040019AE RID: 6574
		public int speed = 70;

		// Token: 0x040019AF RID: 6575
		public Char charInfo;

		// Token: 0x040019B0 RID: 6576
		public bool isChatServer;

		// Token: 0x040019B1 RID: 6577
		public bool isOnline;

		// Token: 0x040019B2 RID: 6578
		public int timeCount;

		// Token: 0x040019B3 RID: 6579
		public int maxTime;

		// Token: 0x040019B4 RID: 6580
		public long last;

		// Token: 0x040019B5 RID: 6581
		public long curr;
	}
}
