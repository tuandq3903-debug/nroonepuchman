using System;

namespace Game1
{
	// Token: 0x02000483 RID: 1155
	public class InfoItem
	{
		// Token: 0x060033BC RID: 13244 RVA: 0x0032E449 File Offset: 0x0032C649
		public InfoItem(string s)
		{
			this.f = mFont.tahoma_7_green2;
			this.s = s;
			this.speed = 20;
		}

		// Token: 0x040063A8 RID: 25512
		public string s;

		// Token: 0x040063A9 RID: 25513
		private mFont f;

		// Token: 0x040063AA RID: 25514
		public int speed = 70;

		// Token: 0x040063AB RID: 25515
		public Char charInfo;

		// Token: 0x040063AC RID: 25516
		public bool isChatServer;

		// Token: 0x040063AD RID: 25517
		public bool isOnline;

		// Token: 0x040063AE RID: 25518
		public int timeCount;

		// Token: 0x040063AF RID: 25519
		public int maxTime;

		// Token: 0x040063B0 RID: 25520
		public long last;

		// Token: 0x040063B1 RID: 25521
		public long curr;
	}
}
