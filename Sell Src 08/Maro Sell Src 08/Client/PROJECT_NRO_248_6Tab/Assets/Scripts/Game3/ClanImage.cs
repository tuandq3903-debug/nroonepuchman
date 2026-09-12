using System;

namespace Game3
{
	// Token: 0x020002A7 RID: 679
	public class ClanImage
	{
		// Token: 0x06001E4D RID: 7757 RVA: 0x001D6640 File Offset: 0x001D4840
		public static void addClanImage(ClanImage cm)
		{
			Service.gI().clanImage((sbyte)cm.ID);
			ClanImage.vClanImage.addElement(cm);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x001D6660 File Offset: 0x001D4860
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

		// Token: 0x06001E4F RID: 7759 RVA: 0x001D66A0 File Offset: 0x001D48A0
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

		// Token: 0x04003A84 RID: 14980
		public int ID;

		// Token: 0x04003A85 RID: 14981
		public string name;

		// Token: 0x04003A86 RID: 14982
		public int[] idImage;

		// Token: 0x04003A87 RID: 14983
		public int xu;

		// Token: 0x04003A88 RID: 14984
		public int luong;

		// Token: 0x04003A89 RID: 14985
		public static MyVector vClanImage = new MyVector();

		// Token: 0x04003A8A RID: 14986
		public static MyHashTable idImages = new MyHashTable();
	}
}
