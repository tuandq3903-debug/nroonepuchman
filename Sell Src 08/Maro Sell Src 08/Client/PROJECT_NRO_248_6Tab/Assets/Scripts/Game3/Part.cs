using System;

namespace Game3
{
	// Token: 0x02000315 RID: 789
	public class Part
	{
		// Token: 0x060023AA RID: 9130 RVA: 0x002399B0 File Offset: 0x00237BB0
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

		// Token: 0x0400461D RID: 17949
		public int type;

		// Token: 0x0400461E RID: 17950
		public PartImage[] pi;
	}
}
