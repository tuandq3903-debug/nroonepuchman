using System;

namespace Game6
{
	// Token: 0x0200001F RID: 31
	public class ClanImage
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0001732C File Offset: 0x0001552C
		public static void addClanImage(ClanImage cm)
		{
			Service.gI().clanImage((sbyte)cm.ID);
			ClanImage.vClanImage.addElement(cm);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0001734C File Offset: 0x0001554C
		public static ClanImage getClanImage(short ID)
		{
			for (int i = 0; i < ClanImage.vClanImage.size(); i++)
			{
				ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
				if (clanImage.ID == (int)ID)
				{
					return clanImage;
				}
			}
			return null;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0001738C File Offset: 0x0001558C
		public static bool isExistClanImage(int ID)
		{
			for (int i = 0; i < ClanImage.vClanImage.size(); i++)
			{
				if (((ClanImage)ClanImage.vClanImage.elementAt(i)).ID == ID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000308 RID: 776
		public int ID;

		// Token: 0x04000309 RID: 777
		public string name;

		// Token: 0x0400030A RID: 778
		public int[] idImage;

		// Token: 0x0400030B RID: 779
		public int xu;

		// Token: 0x0400030C RID: 780
		public int luong;

		// Token: 0x0400030D RID: 781
		public static MyVector vClanImage = new MyVector();

		// Token: 0x0400030E RID: 782
		public static MyHashTable idImages = new MyHashTable();
	}
}
