using System;

namespace Game4
{
	// Token: 0x02000258 RID: 600
	public class Skill
	{
		// Token: 0x06001B3F RID: 6975 RVA: 0x001B0138 File Offset: 0x001AE338
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

		// Token: 0x06001B40 RID: 6976 RVA: 0x001B01B8 File Offset: 0x001AE3B8
		public string strTimeReplay()
		{
			if (this.coolDown % 1000 == 0)
			{
				return (this.coolDown / 1000).ToString() + string.Empty;
			}
			int num = this.coolDown % 1000;
			return (this.coolDown / 1000).ToString() + "." + ((num % 100 != 0) ? (num / 10) : (num / 100)).ToString();
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x001B0238 File Offset: 0x001AE438
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

		// Token: 0x040034FE RID: 13566
		public const sbyte ATT_STAND = 0;

		// Token: 0x040034FF RID: 13567
		public const sbyte ATT_FLY = 1;

		// Token: 0x04003500 RID: 13568
		public const sbyte SKILL_AUTO_USE = 0;

		// Token: 0x04003501 RID: 13569
		public const sbyte SKILL_CLICK_USE_ATTACK = 1;

		// Token: 0x04003502 RID: 13570
		public const sbyte SKILL_CLICK_USE_BUFF = 2;

		// Token: 0x04003503 RID: 13571
		public const sbyte SKILL_CLICK_NPC = 3;

		// Token: 0x04003504 RID: 13572
		public const sbyte SKILL_CLICK_LIVE = 4;

		// Token: 0x04003505 RID: 13573
		public SkillTemplate template;

		// Token: 0x04003506 RID: 13574
		public short skillId;

		// Token: 0x04003507 RID: 13575
		public int point;

		// Token: 0x04003508 RID: 13576
		public long powRequire;

		// Token: 0x04003509 RID: 13577
		public int coolDown;

		// Token: 0x0400350A RID: 13578
		public long lastTimeUseThisSkill;

		// Token: 0x0400350B RID: 13579
		public int dx;

		// Token: 0x0400350C RID: 13580
		public int dy;

		// Token: 0x0400350D RID: 13581
		public int maxFight;

		// Token: 0x0400350E RID: 13582
		public int manaUse;

		// Token: 0x0400350F RID: 13583
		public SkillOption[] options;

		// Token: 0x04003510 RID: 13584
		public bool paintCanNotUseSkill;

		// Token: 0x04003511 RID: 13585
		public short damage;

		// Token: 0x04003512 RID: 13586
		public string moreInfo;

		// Token: 0x04003513 RID: 13587
		public short price;

		// Token: 0x04003514 RID: 13588
		public short curExp;
	}
}
