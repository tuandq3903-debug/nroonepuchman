using System;

namespace Game4
{
	// Token: 0x020001CF RID: 463
	public class ClanImage
	{
		// Token: 0x060014A9 RID: 5289 RVA: 0x0014159C File Offset: 0x0013F79C
		public static void addClanImage(ClanImage cm)
		{
			Service.gI().clanImage((sbyte)cm.ID);
			ClanImage.vClanImage.addElement(cm);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x001415BC File Offset: 0x0013F7BC
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

		// Token: 0x060014AB RID: 5291 RVA: 0x001415FC File Offset: 0x0013F7FC
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

		// Token: 0x04002805 RID: 10245
		public int ID;

		// Token: 0x04002806 RID: 10246
		public string name;

		// Token: 0x04002807 RID: 10247
		public int[] idImage;

		// Token: 0x04002808 RID: 10248
		public int xu;

		// Token: 0x04002809 RID: 10249
		public int luong;

		// Token: 0x0400280A RID: 10250
		public static MyVector vClanImage = new MyVector();

		// Token: 0x0400280B RID: 10251
		public static MyHashTable idImages = new MyHashTable();
	}
}
