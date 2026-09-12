using System;

namespace Game4
{
	// Token: 0x020001FD RID: 509
	public class InfoPhuBan
	{
		// Token: 0x060016DB RID: 5851 RVA: 0x0016FDF8 File Offset: 0x0016DFF8
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

		// Token: 0x060016DC RID: 5852 RVA: 0x0016FE94 File Offset: 0x0016E094
		public void updateTime(int type_PB, short timeSecond)
		{
			this.type_PB = type_PB;
			this.timeSecond = timeSecond;
			this.timeStart = GameCanvas.timeNow;
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0016FEAF File Offset: 0x0016E0AF
		public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
		{
			this.type_PB = type_PB;
			this.pointTeam1 = pointTeam1;
			this.pointTeam2 = pointTeam2;
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0016FEC6 File Offset: 0x0016E0C6
		public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
		{
			this.type_PB = type_PB;
			this.lifeTeam1 = lifeTeam1;
			this.lifeTeam2 = lifeTeam2;
		}

		// Token: 0x04002C48 RID: 11336
		public int type_PB;

		// Token: 0x04002C49 RID: 11337
		public int maxPoint;

		// Token: 0x04002C4A RID: 11338
		public int pointTeam1;

		// Token: 0x04002C4B RID: 11339
		public int pointTeam2;

		// Token: 0x04002C4C RID: 11340
		public int color_1;

		// Token: 0x04002C4D RID: 11341
		public int color_2;

		// Token: 0x04002C4E RID: 11342
		public int maxLife = 1;

		// Token: 0x04002C4F RID: 11343
		public int lifeTeam1;

		// Token: 0x04002C50 RID: 11344
		public int lifeTeam2;

		// Token: 0x04002C51 RID: 11345
		public string nameTeam1;

		// Token: 0x04002C52 RID: 11346
		public string nameTeam2;

		// Token: 0x04002C53 RID: 11347
		public short idmapPaint;

		// Token: 0x04002C54 RID: 11348
		public short timeSecond;

		// Token: 0x04002C55 RID: 11349
		public short maxtimeSecond = 1;

		// Token: 0x04002C56 RID: 11350
		public byte owner;

		// Token: 0x04002C57 RID: 11351
		public long timeStart;

		// Token: 0x04002C58 RID: 11352
		public MyVector vecInfo = new MyVector("vecInfo chientruong");
	}
}
