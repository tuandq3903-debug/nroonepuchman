using System;

namespace Game2
{
	// Token: 0x0200037F RID: 895
	public class ClanImage
	{
		// Token: 0x060027F1 RID: 10225 RVA: 0x0026B6E4 File Offset: 0x002698E4
		public static void addClanImage(ClanImage cm)
		{
			Service.gI().clanImage((sbyte)cm.ID);
			ClanImage.vClanImage.addElement(cm);
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x0026B704 File Offset: 0x00269904
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

		// Token: 0x060027F3 RID: 10227 RVA: 0x0026B744 File Offset: 0x00269944
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

		// Token: 0x04004D03 RID: 19715
		public int ID;

		// Token: 0x04004D04 RID: 19716
		public string name;

		// Token: 0x04004D05 RID: 19717
		public int[] idImage;

		// Token: 0x04004D06 RID: 19718
		public int xu;

		// Token: 0x04004D07 RID: 19719
		public int luong;

		// Token: 0x04004D08 RID: 19720
		public static MyVector vClanImage = new MyVector();

		// Token: 0x04004D09 RID: 19721
		public static MyHashTable idImages = new MyHashTable();
	}
}
