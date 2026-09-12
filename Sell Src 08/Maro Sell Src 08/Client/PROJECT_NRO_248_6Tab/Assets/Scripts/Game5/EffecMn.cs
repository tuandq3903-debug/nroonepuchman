using System;

namespace Game5
{
	// Token: 0x02000104 RID: 260
	public class EffecMn
	{
		// Token: 0x06000B81 RID: 2945 RVA: 0x000BF514 File Offset: 0x000BD714
		public static void addEff(Effect me)
		{
			EffecMn.vEff.addElement(me);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000BF521 File Offset: 0x000BD721
		public static void removeEff(int id)
		{
			if (EffecMn.getEffById(id) != null)
			{
				EffecMn.vEff.removeElement(EffecMn.getEffById(id));
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000BF53C File Offset: 0x000BD73C
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

		// Token: 0x06000B84 RID: 2948 RVA: 0x000BF57C File Offset: 0x000BD77C
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

		// Token: 0x06000B85 RID: 2949 RVA: 0x000BF5D8 File Offset: 0x000BD7D8
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

		// Token: 0x06000B86 RID: 2950 RVA: 0x000BF630 File Offset: 0x000BD830
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

		// Token: 0x06000B87 RID: 2951 RVA: 0x000BF688 File Offset: 0x000BD888
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

		// Token: 0x06000B88 RID: 2952 RVA: 0x000BF6E0 File Offset: 0x000BD8E0
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

		// Token: 0x06000B89 RID: 2953 RVA: 0x000BF738 File Offset: 0x000BD938
		public static void update()
		{
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				((Effect)EffecMn.vEff.elementAt(i)).update();
			}
		}

		// Token: 0x0400161C RID: 5660
		public static MyVector vEff = new MyVector();
	}
}
