using System;

namespace Game4
{
	// Token: 0x020001DC RID: 476
	public class EffecMn
	{
		// Token: 0x06001525 RID: 5413 RVA: 0x001545B8 File Offset: 0x001527B8
		public static void addEff(Effect me)
		{
			EffecMn.vEff.addElement(me);
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x001545C5 File Offset: 0x001527C5
		public static void removeEff(int id)
		{
			if (EffecMn.getEffById(id) != null)
			{
				EffecMn.vEff.removeElement(EffecMn.getEffById(id));
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x001545E0 File Offset: 0x001527E0
		public static Effect getEffById(int id)
		{
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				Effect effect = (Effect)EffecMn.vEff.elementAt(i);
				if (effect.effId == id)
				{
					return effect;
				}
			}
			return null;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00154620 File Offset: 0x00152820
		public static void paintBackGroundUnderLayer(mGraphics g, int x, int y, int layer)
		{
			if (ModFunc.GiamDungLuong)
			{
				return;
			}
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				if (((Effect)EffecMn.vEff.elementAt(i)).layer == -layer)
				{
					((Effect)EffecMn.vEff.elementAt(i)).paintUnderBackground(g, x, y);
				}
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0015467C File Offset: 0x0015287C
		public static void paintLayer1(mGraphics g)
		{
			if (ModFunc.GiamDungLuong)
			{
				return;
			}
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				if (((Effect)EffecMn.vEff.elementAt(i)).layer == 1)
				{
					((Effect)EffecMn.vEff.elementAt(i)).paint(g);
				}
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x001546D4 File Offset: 0x001528D4
		public static void paintLayer2(mGraphics g)
		{
			if (ModFunc.GiamDungLuong)
			{
				return;
			}
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				if (((Effect)EffecMn.vEff.elementAt(i)).layer == 2)
				{
					((Effect)EffecMn.vEff.elementAt(i)).paint(g);
				}
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0015472C File Offset: 0x0015292C
		public static void paintLayer3(mGraphics g)
		{
			if (ModFunc.GiamDungLuong)
			{
				return;
			}
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				if (((Effect)EffecMn.vEff.elementAt(i)).layer == 3)
				{
					((Effect)EffecMn.vEff.elementAt(i)).paint(g);
				}
			}
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00154784 File Offset: 0x00152984
		public static void paintLayer4(mGraphics g)
		{
			if (ModFunc.GiamDungLuong)
			{
				return;
			}
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				if (((Effect)EffecMn.vEff.elementAt(i)).layer == 4)
				{
					((Effect)EffecMn.vEff.elementAt(i)).paint(g);
				}
			}
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x001547DC File Offset: 0x001529DC
		public static void update()
		{
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				((Effect)EffecMn.vEff.elementAt(i)).update();
			}
		}

		// Token: 0x0400289B RID: 10395
		public static MyVector vEff = new MyVector();
	}
}
