using System;

namespace Game6
{
	// Token: 0x020000A8 RID: 168
	public class Skill
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x00085F88 File Offset: 0x00084188
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

		// Token: 0x060007F8 RID: 2040 RVA: 0x00086008 File Offset: 0x00084208
		public string strTimeReplay()
		{
			if (this.coolDown % 1000 == 0)
			{
				return (this.coolDown / 1000).ToString() + string.Empty;
			}
			int num = this.coolDown % 1000;
			return (this.coolDown / 1000).ToString() + "." + ((num % 100 != 0) ? (num / 10) : (num / 100)).ToString();
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00086088 File Offset: 0x00084288
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

		// Token: 0x04001000 RID: 4096
		public const sbyte ATT_STAND = 0;

		// Token: 0x04001001 RID: 4097
		public const sbyte ATT_FLY = 1;

		// Token: 0x04001002 RID: 4098
		public const sbyte SKILL_AUTO_USE = 0;

		// Token: 0x04001003 RID: 4099
		public const sbyte SKILL_CLICK_USE_ATTACK = 1;

		// Token: 0x04001004 RID: 4100
		public const sbyte SKILL_CLICK_USE_BUFF = 2;

		// Token: 0x04001005 RID: 4101
		public const sbyte SKILL_CLICK_NPC = 3;

		// Token: 0x04001006 RID: 4102
		public const sbyte SKILL_CLICK_LIVE = 4;

		// Token: 0x04001007 RID: 4103
		public SkillTemplate template;

		// Token: 0x04001008 RID: 4104
		public short skillId;

		// Token: 0x04001009 RID: 4105
		public int point;

		// Token: 0x0400100A RID: 4106
		public long powRequire;

		// Token: 0x0400100B RID: 4107
		public int coolDown;

		// Token: 0x0400100C RID: 4108
		public long lastTimeUseThisSkill;

		// Token: 0x0400100D RID: 4109
		public int dx;

		// Token: 0x0400100E RID: 4110
		public int dy;

		// Token: 0x0400100F RID: 4111
		public int maxFight;

		// Token: 0x04001010 RID: 4112
		public int manaUse;

		// Token: 0x04001011 RID: 4113
		public SkillOption[] options;

		// Token: 0x04001012 RID: 4114
		public bool paintCanNotUseSkill;

		// Token: 0x04001013 RID: 4115
		public short damage;

		// Token: 0x04001014 RID: 4116
		public string moreInfo;

		// Token: 0x04001015 RID: 4117
		public short price;

		// Token: 0x04001016 RID: 4118
		public short curExp;
	}
}
