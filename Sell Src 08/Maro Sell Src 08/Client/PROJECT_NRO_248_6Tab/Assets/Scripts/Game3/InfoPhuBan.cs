using System;

namespace Game3
{
	// Token: 0x020002D5 RID: 725
	public class InfoPhuBan
	{
		// Token: 0x0600207F RID: 8319 RVA: 0x00204E9C File Offset: 0x0020309C
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

		// Token: 0x06002080 RID: 8320 RVA: 0x00204F38 File Offset: 0x00203138
		public void updateTime(int type_PB, short timeSecond)
		{
			this.type_PB = type_PB;
			this.timeSecond = timeSecond;
			this.timeStart = GameCanvas.timeNow;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00204F53 File Offset: 0x00203153
		public void updatePoint(int type_PB, int pointTeam1, int pointTeam2)
		{
			this.type_PB = type_PB;
			this.pointTeam1 = pointTeam1;
			this.pointTeam2 = pointTeam2;
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00204F6A File Offset: 0x0020316A
		public void updateLife(int type_PB, int lifeTeam1, int lifeTeam2)
		{
			this.type_PB = type_PB;
			this.lifeTeam1 = lifeTeam1;
			this.lifeTeam2 = lifeTeam2;
		}

		// Token: 0x04003EC7 RID: 16071
		public int type_PB;

		// Token: 0x04003EC8 RID: 16072
		public int maxPoint;

		// Token: 0x04003EC9 RID: 16073
		public int pointTeam1;

		// Token: 0x04003ECA RID: 16074
		public int pointTeam2;

		// Token: 0x04003ECB RID: 16075
		public int color_1;

		// Token: 0x04003ECC RID: 16076
		public int color_2;

		// Token: 0x04003ECD RID: 16077
		public int maxLife = 1;

		// Token: 0x04003ECE RID: 16078
		public int lifeTeam1;

		// Token: 0x04003ECF RID: 16079
		public int lifeTeam2;

		// Token: 0x04003ED0 RID: 16080
		public string nameTeam1;

		// Token: 0x04003ED1 RID: 16081
		public string nameTeam2;

		// Token: 0x04003ED2 RID: 16082
		public short idmapPaint;

		// Token: 0x04003ED3 RID: 16083
		public short timeSecond;

		// Token: 0x04003ED4 RID: 16084
		public short maxtimeSecond = 1;

		// Token: 0x04003ED5 RID: 16085
		public byte owner;

		// Token: 0x04003ED6 RID: 16086
		public long timeStart;

		// Token: 0x04003ED7 RID: 16087
		public MyVector vecInfo = new MyVector("vecInfo chientruong");
	}
}
