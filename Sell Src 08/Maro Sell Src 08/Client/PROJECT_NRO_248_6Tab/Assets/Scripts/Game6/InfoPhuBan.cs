using System;

namespace Game6
{
	// Token: 0x0200004D RID: 77
	public class InfoPhuBan
	{
		// Token: 0x06000393 RID: 915 RVA: 0x00045BA8 File Offset: 0x00043DA8
		public InfoPhuBan(int type_PB, short idmapPaint, string nameTeam1, string nameTeam2, int maxPoint, short timeSecond)
		{
			this.type_PB = type_PB;
			this.idmapPaint = idmapPaint;
			this.nameTeam1 = nameTeam1;
			this.nameTeam2 = nameTeam2;
			this.timeSecond = timeSecond;
			this.timeStart = GameCanvas.timeNow;
			this.maxPoint = maxPoint;
			if (this.maxPoint <= 0)
			{
				this.maxPoint = 1;
			}
			this.pointTeam1 = 0;
			this.pointTeam2 = 0;
			this.owner = 0;
			this.color_1 = 4;
			this.color_2 = 6;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00045C44 File Offset: 0x00043E44
		public void updateTime(int type_PB, short timeSecond)
		{
			this.type_PB = type_PB;
			this.timeSecond = timeSecond;
			this.timeStart = GameCanvas.timeNow;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00045C5F File Offset: 0x00043E5F
		public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
		{
			this.type_PB = type_PB;
			this.pointTeam1 = pointTeam1;
			this.pointTeam2 = pointTeam2;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00045C76 File Offset: 0x00043E76
		public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
		{
			this.type_PB = type_PB;
			this.lifeTeam1 = lifeTeam1;
			this.lifeTeam2 = lifeTeam2;
		}

		// Token: 0x0400074A RID: 1866
		public int type_PB;

		// Token: 0x0400074B RID: 1867
		public int maxPoint;

		// Token: 0x0400074C RID: 1868
		public int pointTeam1;

		// Token: 0x0400074D RID: 1869
		public int pointTeam2;

		// Token: 0x0400074E RID: 1870
		public int color_1;

		// Token: 0x0400074F RID: 1871
		public int color_2;

		// Token: 0x04000750 RID: 1872
		public int maxLife = 1;

		// Token: 0x04000751 RID: 1873
		public int lifeTeam1;

		// Token: 0x04000752 RID: 1874
		public int lifeTeam2;

		// Token: 0x04000753 RID: 1875
		public string nameTeam1;

		// Token: 0x04000754 RID: 1876
		public string nameTeam2;

		// Token: 0x04000755 RID: 1877
		public short idmapPaint;

		// Token: 0x04000756 RID: 1878
		public short timeSecond;

		// Token: 0x04000757 RID: 1879
		public short maxtimeSecond = 1;

		// Token: 0x04000758 RID: 1880
		public byte owner;

		// Token: 0x04000759 RID: 1881
		public long timeStart;

		// Token: 0x0400075A RID: 1882
		public MyVector vecInfo = new MyVector("vecInfo chientruong");
	}
}
