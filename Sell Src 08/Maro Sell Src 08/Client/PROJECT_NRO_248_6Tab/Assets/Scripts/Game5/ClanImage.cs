using System;

namespace Game5
{
	// Token: 0x020000F7 RID: 247
	public class ClanImage
	{
		// Token: 0x06000B05 RID: 2821 RVA: 0x000AC4F8 File Offset: 0x000AA6F8
		public static void addClanImage(ClanImage cm)
		{
			Service.gI().clanImage((sbyte)cm.ID);
			ClanImage.vClanImage.addElement(cm);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000AC518 File Offset: 0x000AA718
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

		// Token: 0x06000B07 RID: 2823 RVA: 0x000AC558 File Offset: 0x000AA758
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

		// Token: 0x04001586 RID: 5510
		public int ID;

		// Token: 0x04001587 RID: 5511
		public string name;

		// Token: 0x04001588 RID: 5512
		public int[] idImage;

		// Token: 0x04001589 RID: 5513
		public int xu;

		// Token: 0x0400158A RID: 5514
		public int luong;

		// Token: 0x0400158B RID: 5515
		public static MyVector vClanImage = new MyVector();

		// Token: 0x0400158C RID: 5516
		public static MyHashTable idImages = new MyHashTable();
	}
}
