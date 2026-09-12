using System;

namespace Game1
{
	// Token: 0x020004E0 RID: 1248
	public class Skill
	{
		// Token: 0x0600382B RID: 14379 RVA: 0x0036F324 File Offset: 0x0036D524
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

		// Token: 0x0600382C RID: 14380 RVA: 0x0036F3A4 File Offset: 0x0036D5A4
		public string strTimeReplay()
		{
			if (this.coolDown % 1000 == 0)
			{
				return (this.coolDown / 1000).ToString() + string.Empty;
			}
			int num = this.coolDown % 1000;
			return (this.coolDown / 1000).ToString() + "." + ((num % 100 != 0) ? (num / 10) : (num / 100)).ToString();
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x0036F424 File Offset: 0x0036D624
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

		// Token: 0x04006C7B RID: 27771
		public const sbyte ATT_STAND = 0;

		// Token: 0x04006C7C RID: 27772
		public const sbyte ATT_FLY = 1;

		// Token: 0x04006C7D RID: 27773
		public const sbyte SKILL_AUTO_USE = 0;

		// Token: 0x04006C7E RID: 27774
		public const sbyte SKILL_CLICK_USE_ATTACK = 1;

		// Token: 0x04006C7F RID: 27775
		public const sbyte SKILL_CLICK_USE_BUFF = 2;

		// Token: 0x04006C80 RID: 27776
		public const sbyte SKILL_CLICK_NPC = 3;

		// Token: 0x04006C81 RID: 27777
		public const sbyte SKILL_CLICK_LIVE = 4;

		// Token: 0x04006C82 RID: 27778
		public SkillTemplate template;

		// Token: 0x04006C83 RID: 27779
		public short skillId;

		// Token: 0x04006C84 RID: 27780
		public int point;

		// Token: 0x04006C85 RID: 27781
		public long powRequire;

		// Token: 0x04006C86 RID: 27782
		public int coolDown;

		// Token: 0x04006C87 RID: 27783
		public long lastTimeUseThisSkill;

		// Token: 0x04006C88 RID: 27784
		public int dx;

		// Token: 0x04006C89 RID: 27785
		public int dy;

		// Token: 0x04006C8A RID: 27786
		public int maxFight;

		// Token: 0x04006C8B RID: 27787
		public int manaUse;

		// Token: 0x04006C8C RID: 27788
		public SkillOption[] options;

		// Token: 0x04006C8D RID: 27789
		public bool paintCanNotUseSkill;

		// Token: 0x04006C8E RID: 27790
		public short damage;

		// Token: 0x04006C8F RID: 27791
		public string moreInfo;

		// Token: 0x04006C90 RID: 27792
		public short price;

		// Token: 0x04006C91 RID: 27793
		public short curExp;
	}
}
