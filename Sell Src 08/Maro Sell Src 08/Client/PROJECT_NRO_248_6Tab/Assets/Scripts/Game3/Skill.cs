using System;

namespace Game3
{
	// Token: 0x02000330 RID: 816
	public class Skill
	{
		// Token: 0x060024E3 RID: 9443 RVA: 0x002451DC File Offset: 0x002433DC
		public string strCurExp()
		{
			if (this.curExp / 10 >= 100)
			{
				return "MAX";
			}
			if (this.curExp % 10 == 0)
			{
				return ((int)(this.curExp / 10)).ToString() + "%";
			}
			int num = (int)(this.curExp % 10);
			return ((int)(this.curExp / 10)).ToString() + "." + (num % 10).ToString() + "%";
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0024525C File Offset: 0x0024345C
		public string strTimeReplay()
		{
			if (this.coolDown % 1000 == 0)
			{
				return (this.coolDown / 1000).ToString() + string.Empty;
			}
			int num = this.coolDown % 1000;
			return (this.coolDown / 1000).ToString() + "." + ((num % 100 != 0) ? (num / 10) : (num / 100)).ToString();
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x002452DC File Offset: 0x002434DC
		public void paint(int x, int y, mGraphics g)
		{
			SmallImage.drawSmallImage(g, this.template.iconId, x, y, 0, StaticObj.VCENTER_HCENTER);
			long num2 = mSystem.currentTimeMillis() - this.lastTimeUseThisSkill;
			if (num2 < (long)this.coolDown)
			{
				g.setColor(2721889, 0.7f);
				if (this.paintCanNotUseSkill && GameCanvas.gameTick % 6 > 2)
				{
					g.setColor(876862);
				}
				int num3 = (int)(num2 * 20L / (long)this.coolDown);
				g.fillRect(x - 10, y - 10 + num3, 20, 20 - num3);
				return;
			}
			this.paintCanNotUseSkill = false;
		}

		// Token: 0x0400477D RID: 18301
		public const sbyte ATT_STAND = 0;

		// Token: 0x0400477E RID: 18302
		public const sbyte ATT_FLY = 1;

		// Token: 0x0400477F RID: 18303
		public const sbyte SKILL_AUTO_USE = 0;

		// Token: 0x04004780 RID: 18304
		public const sbyte SKILL_CLICK_USE_ATTACK = 1;

		// Token: 0x04004781 RID: 18305
		public const sbyte SKILL_CLICK_USE_BUFF = 2;

		// Token: 0x04004782 RID: 18306
		public const sbyte SKILL_CLICK_NPC = 3;

		// Token: 0x04004783 RID: 18307
		public const sbyte SKILL_CLICK_LIVE = 4;

		// Token: 0x04004784 RID: 18308
		public SkillTemplate template;

		// Token: 0x04004785 RID: 18309
		public short skillId;

		// Token: 0x04004786 RID: 18310
		public int point;

		// Token: 0x04004787 RID: 18311
		public long powRequire;

		// Token: 0x04004788 RID: 18312
		public int coolDown;

		// Token: 0x04004789 RID: 18313
		public long lastTimeUseThisSkill;

		// Token: 0x0400478A RID: 18314
		public int dx;

		// Token: 0x0400478B RID: 18315
		public int dy;

		// Token: 0x0400478C RID: 18316
		public int maxFight;

		// Token: 0x0400478D RID: 18317
		public int manaUse;

		// Token: 0x0400478E RID: 18318
		public SkillOption[] options;

		// Token: 0x0400478F RID: 18319
		public bool paintCanNotUseSkill;

		// Token: 0x04004790 RID: 18320
		public short damage;

		// Token: 0x04004791 RID: 18321
		public string moreInfo;

		// Token: 0x04004792 RID: 18322
		public short price;

		// Token: 0x04004793 RID: 18323
		public short curExp;
	}
}
