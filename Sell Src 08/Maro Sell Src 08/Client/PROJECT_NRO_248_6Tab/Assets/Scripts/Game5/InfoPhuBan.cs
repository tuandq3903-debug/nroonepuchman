using System;

namespace Game5
{
	// Token: 0x02000125 RID: 293
	public class InfoPhuBan
	{
		// Token: 0x06000D37 RID: 3383 RVA: 0x000DAD54 File Offset: 0x000D8F54
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

		// Token: 0x06000D38 RID: 3384 RVA: 0x000DADF0 File Offset: 0x000D8FF0
		public void updateTime(int type_PB, short timeSecond)
		{
			this.type_PB = type_PB;
			this.timeSecond = timeSecond;
			this.timeStart = GameCanvas.timeNow;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000DAE0B File Offset: 0x000D900B
		public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
		{
			this.type_PB = type_PB;
			this.pointTeam1 = pointTeam1;
			this.pointTeam2 = pointTeam2;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x000DAE22 File Offset: 0x000D9022
		public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
		{
			this.type_PB = type_PB;
			this.lifeTeam1 = lifeTeam1;
			this.lifeTeam2 = lifeTeam2;
		}

		// Token: 0x040019C9 RID: 6601
		public int type_PB;

		// Token: 0x040019CA RID: 6602
		public int maxPoint;

		// Token: 0x040019CB RID: 6603
		public int pointTeam1;

		// Token: 0x040019CC RID: 6604
		public int pointTeam2;

		// Token: 0x040019CD RID: 6605
		public int color_1;

		// Token: 0x040019CE RID: 6606
		public int color_2;

		// Token: 0x040019CF RID: 6607
		public int maxLife = 1;

		// Token: 0x040019D0 RID: 6608
		public int lifeTeam1;

		// Token: 0x040019D1 RID: 6609
		public int lifeTeam2;

		// Token: 0x040019D2 RID: 6610
		public string nameTeam1;

		// Token: 0x040019D3 RID: 6611
		public string nameTeam2;

		// Token: 0x040019D4 RID: 6612
		public short idmapPaint;

		// Token: 0x040019D5 RID: 6613
		public short timeSecond;

		// Token: 0x040019D6 RID: 6614
		public short maxtimeSecond = 1;

		// Token: 0x040019D7 RID: 6615
		public byte owner;

		// Token: 0x040019D8 RID: 6616
		public long timeStart;

		// Token: 0x040019D9 RID: 6617
		public MyVector vecInfo = new MyVector("vecInfo chientruong");
	}
}
