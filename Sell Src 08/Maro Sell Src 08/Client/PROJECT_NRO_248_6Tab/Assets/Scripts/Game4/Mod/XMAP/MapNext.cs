using System;

namespace Game4.Mod.XMAP
{
	// Token: 0x0200027B RID: 635
	public struct MapNext
	{
		// Token: 0x06001C4E RID: 7246 RVA: 0x001B9C52 File Offset: 0x001B7E52
		public MapNext(int mapID, TypeMapNext type, int[] info)
		{
			this.MapID = mapID;
			this.Type = type;
			this.Info = info;
		}

		// Token: 0x040036CC RID: 14028
		public int MapID;

		// Token: 0x040036CD RID: 14029
		public TypeMapNext Type;

		// Token: 0x040036CE RID: 14030
		public int[] Info;
	}
}
