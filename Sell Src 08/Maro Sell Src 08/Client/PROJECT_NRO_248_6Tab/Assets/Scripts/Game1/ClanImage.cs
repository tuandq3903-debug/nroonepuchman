using System;

namespace Game1
{
	// Token: 0x02000457 RID: 1111
	public class ClanImage
	{
		// Token: 0x06003195 RID: 12693 RVA: 0x00300788 File Offset: 0x002FE988
		public static void addClanImage(ClanImage cm)
		{
			Service.gI().clanImage((sbyte)cm.ID);
			ClanImage.vClanImage.addElement(cm);
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x003007A8 File Offset: 0x002FE9A8
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

		// Token: 0x06003197 RID: 12695 RVA: 0x003007E8 File Offset: 0x002FE9E8
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

		// Token: 0x04005F82 RID: 24450
		public int ID;

		// Token: 0x04005F83 RID: 24451
		public string name;

		// Token: 0x04005F84 RID: 24452
		public int[] idImage;

		// Token: 0x04005F85 RID: 24453
		public int xu;

		// Token: 0x04005F86 RID: 24454
		public int luong;

		// Token: 0x04005F87 RID: 24455
		public static MyVector vClanImage = new MyVector();

		// Token: 0x04005F88 RID: 24456
		public static MyHashTable idImages = new MyHashTable();
	}
}
