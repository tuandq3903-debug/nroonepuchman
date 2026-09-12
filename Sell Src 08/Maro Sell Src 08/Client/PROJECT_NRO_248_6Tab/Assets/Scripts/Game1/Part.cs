using System;

namespace Game1
{
	// Token: 0x020004C5 RID: 1221
	public class Part
	{
		// Token: 0x060036F2 RID: 14066 RVA: 0x00363AF8 File Offset: 0x00361CF8
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

		// Token: 0x04006B1B RID: 27419
		public int type;

		// Token: 0x04006B1C RID: 27420
		public PartImage[] pi;
	}
}
