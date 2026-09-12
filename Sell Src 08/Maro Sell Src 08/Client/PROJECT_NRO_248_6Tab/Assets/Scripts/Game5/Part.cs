using System;

namespace Game5
{
	// Token: 0x02000165 RID: 357
	public class Part
	{
		// Token: 0x06001062 RID: 4194 RVA: 0x0010F868 File Offset: 0x0010DA68
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

		// Token: 0x0400211F RID: 8479
		public int type;

		// Token: 0x04002120 RID: 8480
		public PartImage[] pi;
	}
}
