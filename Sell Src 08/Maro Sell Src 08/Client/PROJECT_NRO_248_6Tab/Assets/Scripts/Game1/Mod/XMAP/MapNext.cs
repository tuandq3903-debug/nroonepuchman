using System;

namespace Game1.Mod.XMAP
{
	// Token: 0x02000503 RID: 1283
	public struct MapNext
	{
		// Token: 0x0600393A RID: 14650 RVA: 0x00378E3E File Offset: 0x0037703E
		public MapNext(int mapID, TypeMapNext type, int[] info)
		{
			this.MapID = mapID;
			this.Type = type;
			this.Info = info;
		}

		// Token: 0x04006E49 RID: 28233
		public int MapID;

		// Token: 0x04006E4A RID: 28234
		public TypeMapNext Type;

		// Token: 0x04006E4B RID: 28235
		public int[] Info;
	}
}
