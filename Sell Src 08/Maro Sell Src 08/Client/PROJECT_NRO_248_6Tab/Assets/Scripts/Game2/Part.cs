using System;

namespace Game2
{
	// Token: 0x020003ED RID: 1005
	public class Part
	{
		// Token: 0x06002D4E RID: 11598 RVA: 0x002CEA54 File Offset: 0x002CCC54
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

		// Token: 0x0400589C RID: 22684
		public int type;

		// Token: 0x0400589D RID: 22685
		public PartImage[] pi;
	}
}
