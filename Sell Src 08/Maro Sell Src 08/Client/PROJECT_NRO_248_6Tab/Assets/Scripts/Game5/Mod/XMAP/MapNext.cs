using System;

namespace Game5.Mod.XMAP
{
	// Token: 0x020001A3 RID: 419
	public struct MapNext
	{
		// Token: 0x060012AA RID: 4778 RVA: 0x00124BAE File Offset: 0x00122DAE
		public MapNext(int mapID, TypeMapNext type, int[] info)
		{
			this.MapID = mapID;
			this.Type = type;
			this.Info = info;
		}

		// Token: 0x0400244D RID: 9293
		public int MapID;

		// Token: 0x0400244E RID: 9294
		public TypeMapNext Type;

		// Token: 0x0400244F RID: 9295
		public int[] Info;
	}
}
