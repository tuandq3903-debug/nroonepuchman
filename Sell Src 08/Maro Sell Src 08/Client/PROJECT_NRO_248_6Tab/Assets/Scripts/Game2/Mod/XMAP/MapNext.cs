using System;

namespace Game2.Mod.XMAP
{
	// Token: 0x0200042B RID: 1067
	public struct MapNext
	{
		// Token: 0x06002F96 RID: 12182 RVA: 0x002E3D9A File Offset: 0x002E1F9A
		public MapNext(int mapID, TypeMapNext type, int[] info)
		{
			this.MapID = mapID;
			this.Type = type;
			this.Info = info;
		}

		// Token: 0x04005BCA RID: 23498
		public int MapID;

		// Token: 0x04005BCB RID: 23499
		public TypeMapNext Type;

		// Token: 0x04005BCC RID: 23500
		public int[] Info;
	}
}
