using System;

namespace Game5
{
	// Token: 0x02000180 RID: 384
	public class Skill
	{
		// Token: 0x0600119B RID: 4507 RVA: 0x0011B094 File Offset: 0x00119294
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

		// Token: 0x0600119C RID: 4508 RVA: 0x0011B114 File Offset: 0x00119314
		public string strTimeReplay()
		{
			if (this.coolDown % 1000 == 0)
			{
				return (this.coolDown / 1000).ToString() + string.Empty;
			}
			int num = this.coolDown % 1000;
			return (this.coolDown / 1000).ToString() + "." + ((num % 100 != 0) ? (num / 10) : (num / 100)).ToString();
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0011B194 File Offset: 0x00119394
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

		// Token: 0x0400227F RID: 8831
		public const sbyte ATT_STAND = 0;

		// Token: 0x04002280 RID: 8832
		public const sbyte ATT_FLY = 1;

		// Token: 0x04002281 RID: 8833
		public const sbyte SKILL_AUTO_USE = 0;

		// Token: 0x04002282 RID: 8834
		public const sbyte SKILL_CLICK_USE_ATTACK = 1;

		// Token: 0x04002283 RID: 8835
		public const sbyte SKILL_CLICK_USE_BUFF = 2;

		// Token: 0x04002284 RID: 8836
		public const sbyte SKILL_CLICK_NPC = 3;

		// Token: 0x04002285 RID: 8837
		public const sbyte SKILL_CLICK_LIVE = 4;

		// Token: 0x04002286 RID: 8838
		public SkillTemplate template;

		// Token: 0x04002287 RID: 8839
		public short skillId;

		// Token: 0x04002288 RID: 8840
		public int point;

		// Token: 0x04002289 RID: 8841
		public long powRequire;

		// Token: 0x0400228A RID: 8842
		public int coolDown;

		// Token: 0x0400228B RID: 8843
		public long lastTimeUseThisSkill;

		// Token: 0x0400228C RID: 8844
		public int dx;

		// Token: 0x0400228D RID: 8845
		public int dy;

		// Token: 0x0400228E RID: 8846
		public int maxFight;

		// Token: 0x0400228F RID: 8847
		public int manaUse;

		// Token: 0x04002290 RID: 8848
		public SkillOption[] options;

		// Token: 0x04002291 RID: 8849
		public bool paintCanNotUseSkill;

		// Token: 0x04002292 RID: 8850
		public short damage;

		// Token: 0x04002293 RID: 8851
		public string moreInfo;

		// Token: 0x04002294 RID: 8852
		public short price;

		// Token: 0x04002295 RID: 8853
		public short curExp;
	}
}
