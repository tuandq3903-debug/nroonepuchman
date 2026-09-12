using System;

namespace Game3.Mod.XMAP
{
	// Token: 0x02000353 RID: 851
	public struct MapNext
	{
		// Token: 0x060025F2 RID: 9714 RVA: 0x0024ECF6 File Offset: 0x0024CEF6
		public MapNext(int mapID, TypeMapNext type, int[] info)
		{
			this.MapID = mapID;
			this.Type = type;
			this.Info = info;
		}

		// Token: 0x0400494B RID: 18763
		public int MapID;

		// Token: 0x0400494C RID: 18764
		public TypeMapNext Type;

		// Token: 0x0400494D RID: 18765
		public int[] Info;
	}
}
