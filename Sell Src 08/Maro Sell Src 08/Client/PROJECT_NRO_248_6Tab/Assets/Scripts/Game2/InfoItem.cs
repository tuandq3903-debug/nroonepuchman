using System;

namespace Game2
{
	// Token: 0x020003AB RID: 939
	public class InfoItem
	{
		// Token: 0x06002A18 RID: 10776 RVA: 0x002993A5 File Offset: 0x002975A5
		public InfoItem(string s)
		{
			this.f = mFont.tahoma_7_green2;
			this.s = s;
			this.speed = 20;
		}

		// Token: 0x04005129 RID: 20777
		public string s;

		// Token: 0x0400512A RID: 20778
		private mFont f;

		// Token: 0x0400512B RID: 20779
		public int speed = 70;

		// Token: 0x0400512C RID: 20780
		public Char charInfo;

		// Token: 0x0400512D RID: 20781
		public bool isChatServer;

		// Token: 0x0400512E RID: 20782
		public bool isOnline;

		// Token: 0x0400512F RID: 20783
		public int timeCount;

		// Token: 0x04005130 RID: 20784
		public int maxTime;

		// Token: 0x04005131 RID: 20785
		public long last;

		// Token: 0x04005132 RID: 20786
		public long curr;
	}
}
