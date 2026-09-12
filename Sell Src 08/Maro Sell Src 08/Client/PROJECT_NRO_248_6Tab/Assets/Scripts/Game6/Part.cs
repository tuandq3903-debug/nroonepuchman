using System;

namespace Game6
{
	// Token: 0x0200008D RID: 141
	public class Part
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x0007A710 File Offset: 0x00078910
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

		// Token: 0x04000EA0 RID: 3744
		public int type;

		// Token: 0x04000EA1 RID: 3745
		public PartImage[] pi;
	}
}
