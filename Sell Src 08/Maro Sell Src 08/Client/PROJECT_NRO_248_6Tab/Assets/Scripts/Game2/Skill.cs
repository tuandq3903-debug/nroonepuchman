using System;

namespace Game2
{
	// Token: 0x02000408 RID: 1032
	public class Skill
	{
		// Token: 0x06002E87 RID: 11911 RVA: 0x002DA280 File Offset: 0x002D8480
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

		// Token: 0x06002E88 RID: 11912 RVA: 0x002DA300 File Offset: 0x002D8500
		public string strTimeReplay()
		{
			if (this.coolDown % 1000 == 0)
			{
				return (this.coolDown / 1000).ToString() + string.Empty;
			}
			int num = this.coolDown % 1000;
			return (this.coolDown / 1000).ToString() + "." + ((num % 100 != 0) ? (num / 10) : (num / 100)).ToString();
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x002DA380 File Offset: 0x002D8580
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

		// Token: 0x040059FC RID: 23036
		public const sbyte ATT_STAND = 0;

		// Token: 0x040059FD RID: 23037
		public const sbyte ATT_FLY = 1;

		// Token: 0x040059FE RID: 23038
		public const sbyte SKILL_AUTO_USE = 0;

		// Token: 0x040059FF RID: 23039
		public const sbyte SKILL_CLICK_USE_ATTACK = 1;

		// Token: 0x04005A00 RID: 23040
		public const sbyte SKILL_CLICK_USE_BUFF = 2;

		// Token: 0x04005A01 RID: 23041
		public const sbyte SKILL_CLICK_NPC = 3;

		// Token: 0x04005A02 RID: 23042
		public const sbyte SKILL_CLICK_LIVE = 4;

		// Token: 0x04005A03 RID: 23043
		public SkillTemplate template;

		// Token: 0x04005A04 RID: 23044
		public short skillId;

		// Token: 0x04005A05 RID: 23045
		public int point;

		// Token: 0x04005A06 RID: 23046
		public long powRequire;

		// Token: 0x04005A07 RID: 23047
		public int coolDown;

		// Token: 0x04005A08 RID: 23048
		public long lastTimeUseThisSkill;

		// Token: 0x04005A09 RID: 23049
		public int dx;

		// Token: 0x04005A0A RID: 23050
		public int dy;

		// Token: 0x04005A0B RID: 23051
		public int maxFight;

		// Token: 0x04005A0C RID: 23052
		public int manaUse;

		// Token: 0x04005A0D RID: 23053
		public SkillOption[] options;

		// Token: 0x04005A0E RID: 23054
		public bool paintCanNotUseSkill;

		// Token: 0x04005A0F RID: 23055
		public short damage;

		// Token: 0x04005A10 RID: 23056
		public string moreInfo;

		// Token: 0x04005A11 RID: 23057
		public short price;

		// Token: 0x04005A12 RID: 23058
		public short curExp;
	}
}
