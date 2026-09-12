using System;

namespace Game4
{
	// Token: 0x0200023D RID: 573
	public class Part
	{
		// Token: 0x06001A06 RID: 6662 RVA: 0x001A490C File Offset: 0x001A2B0C
		public Part(int type)
		{
			this.type = type;
			if (type == 0)
			{
				this.pi = new PartImage[3];
			}
			if (type == 1)
			{
				this.pi = new PartImage[17];
			}
			if (type == 2)
			{
				this.pi = new PartImage[14];
			}
			if (type == 3)
			{
				this.pi = new PartImage[2];
			}
		}

		// Token: 0x0400339E RID: 13214
		public int type;

		// Token: 0x0400339F RID: 13215
		public PartImage[] pi;
	}
}
