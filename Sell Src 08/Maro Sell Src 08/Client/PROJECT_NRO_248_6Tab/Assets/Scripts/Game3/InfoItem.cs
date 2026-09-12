using System;

namespace Game3
{
	// Token: 0x020002D3 RID: 723
	public class InfoItem
	{
		// Token: 0x06002074 RID: 8308 RVA: 0x00204301 File Offset: 0x00202501
		public InfoItem(string s)
		{
			this.f = mFont.tahoma_7_green2;
			this.s = s;
			this.speed = 20;
		}

		// Token: 0x04003EAA RID: 16042
		public string s;

		// Token: 0x04003EAB RID: 16043
		private mFont f;

		// Token: 0x04003EAC RID: 16044
		public int speed = 70;

		// Token: 0x04003EAD RID: 16045
		public Char charInfo;

		// Token: 0x04003EAE RID: 16046
		public bool isChatServer;

		// Token: 0x04003EAF RID: 16047
		public bool isOnline;

		// Token: 0x04003EB0 RID: 16048
		public int timeCount;

		// Token: 0x04003EB1 RID: 16049
		public int maxTime;

		// Token: 0x04003EB2 RID: 16050
		public long last;

		// Token: 0x04003EB3 RID: 16051
		public long curr;
	}
}
