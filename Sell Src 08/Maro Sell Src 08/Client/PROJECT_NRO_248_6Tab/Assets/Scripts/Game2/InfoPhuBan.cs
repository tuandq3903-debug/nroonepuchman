using System;

namespace Game2
{
	// Token: 0x020003AD RID: 941
	public class InfoPhuBan
	{
		// Token: 0x06002A23 RID: 10787 RVA: 0x00299F40 File Offset: 0x00298140
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

		// Token: 0x06002A24 RID: 10788 RVA: 0x00299FDC File Offset: 0x002981DC
		public void updateTime(int type_PB, short timeSecond)
		{
			this.type_PB = type_PB;
			this.timeSecond = timeSecond;
			this.timeStart = GameCanvas.timeNow;
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x00299FF7 File Offset: 0x002981F7
		public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
		{
			this.type_PB = type_PB;
			this.pointTeam1 = pointTeam1;
			this.pointTeam2 = pointTeam2;
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x0029A00E File Offset: 0x0029820E
		public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
		{
			this.type_PB = type_PB;
			this.lifeTeam1 = lifeTeam1;
			this.lifeTeam2 = lifeTeam2;
		}

		// Token: 0x04005146 RID: 20806
		public int type_PB;

		// Token: 0x04005147 RID: 20807
		public int maxPoint;

		// Token: 0x04005148 RID: 20808
		public int pointTeam1;

		// Token: 0x04005149 RID: 20809
		public int pointTeam2;

		// Token: 0x0400514A RID: 20810
		public int color_1;

		// Token: 0x0400514B RID: 20811
		public int color_2;

		// Token: 0x0400514C RID: 20812
		public int maxLife = 1;

		// Token: 0x0400514D RID: 20813
		public int lifeTeam1;

		// Token: 0x0400514E RID: 20814
		public int lifeTeam2;

		// Token: 0x0400514F RID: 20815
		public string nameTeam1;

		// Token: 0x04005150 RID: 20816
		public string nameTeam2;

		// Token: 0x04005151 RID: 20817
		public short idmapPaint;

		// Token: 0x04005152 RID: 20818
		public short timeSecond;

		// Token: 0x04005153 RID: 20819
		public short maxtimeSecond = 1;

		// Token: 0x04005154 RID: 20820
		public byte owner;

		// Token: 0x04005155 RID: 20821
		public long timeStart;

		// Token: 0x04005156 RID: 20822
		public MyVector vecInfo = new MyVector("vecInfo chientruong");
	}
}
