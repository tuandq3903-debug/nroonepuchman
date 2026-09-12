using System;

namespace Game1
{
	// Token: 0x02000464 RID: 1124
	public class EffecMn
	{
		// Token: 0x06003211 RID: 12817 RVA: 0x003137A4 File Offset: 0x003119A4
		public static void addEff(Effect me)
		{
			EffecMn.vEff.addElement(me);
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x003137B1 File Offset: 0x003119B1
		public static void removeEff(int id)
		{
			if (EffecMn.getEffById(id) != null)
			{
				EffecMn.vEff.removeElement(EffecMn.getEffById(id));
			}
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x003137CC File Offset: 0x003119CC
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

		// Token: 0x06003214 RID: 12820 RVA: 0x0031380C File Offset: 0x00311A0C
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

		// Token: 0x06003215 RID: 12821 RVA: 0x00313868 File Offset: 0x00311A68
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

		// Token: 0x06003216 RID: 12822 RVA: 0x003138C0 File Offset: 0x00311AC0
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

		// Token: 0x06003217 RID: 12823 RVA: 0x00313918 File Offset: 0x00311B18
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

		// Token: 0x06003218 RID: 12824 RVA: 0x00313970 File Offset: 0x00311B70
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

		// Token: 0x06003219 RID: 12825 RVA: 0x003139C8 File Offset: 0x00311BC8
		public static void update()
		{
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				((Effect)EffecMn.vEff.elementAt(i)).update();
			}
		}

		// Token: 0x04006018 RID: 24600
		public static MyVector vEff = new MyVector();
	}
}
