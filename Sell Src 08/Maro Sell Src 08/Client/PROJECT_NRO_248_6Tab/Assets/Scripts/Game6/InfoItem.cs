using System;

namespace Game6
{
	// Token: 0x0200004B RID: 75
	public class InfoItem
	{
		// Token: 0x06000388 RID: 904 RVA: 0x0004500D File Offset: 0x0004320D
		public InfoItem(string s)
		{
			this.f = mFont.tahoma_7_green2;
			this.s = s;
			this.speed = 20;
		}

		// Token: 0x0400072D RID: 1837
		public string s;

		// Token: 0x0400072E RID: 1838
		private mFont f;

		// Token: 0x0400072F RID: 1839
		public int speed = 70;

		// Token: 0x04000730 RID: 1840
		public Char charInfo;

		// Token: 0x04000731 RID: 1841
		public bool isChatServer;

		// Token: 0x04000732 RID: 1842
		public bool isOnline;

		// Token: 0x04000733 RID: 1843
		public int timeCount;

		// Token: 0x04000734 RID: 1844
		public int maxTime;

		// Token: 0x04000735 RID: 1845
		public long last;

		// Token: 0x04000736 RID: 1846
		public long curr;
	}
}
