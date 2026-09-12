using System;

namespace Game1
{
	// Token: 0x02000485 RID: 1157
	public class InfoPhuBan
	{
		// Token: 0x060033C7 RID: 13255 RVA: 0x0032EFE4 File Offset: 0x0032D1E4
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

		// Token: 0x060033C8 RID: 13256 RVA: 0x0032F080 File Offset: 0x0032D280
		public void updateTime(int type_PB, short timeSecond)
		{
			this.type_PB = type_PB;
			this.timeSecond = timeSecond;
			this.timeStart = GameCanvas.timeNow;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x0032F09B File Offset: 0x0032D29B
		public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
		{
			this.type_PB = type_PB;
			this.pointTeam1 = pointTeam1;
			this.pointTeam2 = pointTeam2;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x0032F0B2 File Offset: 0x0032D2B2
		public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
		{
			this.type_PB = type_PB;
			this.lifeTeam1 = lifeTeam1;
			this.lifeTeam2 = lifeTeam2;
		}

		// Token: 0x040063C5 RID: 25541
		public int type_PB;

		// Token: 0x040063C6 RID: 25542
		public int maxPoint;

		// Token: 0x040063C7 RID: 25543
		public int pointTeam1;

		// Token: 0x040063C8 RID: 25544
		public int pointTeam2;

		// Token: 0x040063C9 RID: 25545
		public int color_1;

		// Token: 0x040063CA RID: 25546
		public int color_2;

		// Token: 0x040063CB RID: 25547
		public int maxLife = 1;

		// Token: 0x040063CC RID: 25548
		public int lifeTeam1;

		// Token: 0x040063CD RID: 25549
		public int lifeTeam2;

		// Token: 0x040063CE RID: 25550
		public string nameTeam1;

		// Token: 0x040063CF RID: 25551
		public string nameTeam2;

		// Token: 0x040063D0 RID: 25552
		public short idmapPaint;

		// Token: 0x040063D1 RID: 25553
		public short timeSecond;

		// Token: 0x040063D2 RID: 25554
		public short maxtimeSecond = 1;

		// Token: 0x040063D3 RID: 25555
		public byte owner;

		// Token: 0x040063D4 RID: 25556
		public long timeStart;

		// Token: 0x040063D5 RID: 25557
		public MyVector vecInfo = new MyVector("vecInfo chientruong");
	}
}
