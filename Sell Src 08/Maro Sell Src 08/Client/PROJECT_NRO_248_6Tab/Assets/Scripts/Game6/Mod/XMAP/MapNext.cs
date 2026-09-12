using System;

namespace Game6.Mod.XMAP
{
	// Token: 0x020000CB RID: 203
	public struct MapNext
	{
		// Token: 0x06000906 RID: 2310 RVA: 0x0008FABA File Offset: 0x0008DCBA
		public MapNext(int mapID, TypeMapNext type, int[] info)
		{
			this.MapID = mapID;
			this.Type = type;
			this.Info = info;
		}

		// Token: 0x040011CE RID: 4558
		public int MapID;

		// Token: 0x040011CF RID: 4559
		public TypeMapNext Type;

		// Token: 0x040011D0 RID: 4560
		public int[] Info;
	}
}
