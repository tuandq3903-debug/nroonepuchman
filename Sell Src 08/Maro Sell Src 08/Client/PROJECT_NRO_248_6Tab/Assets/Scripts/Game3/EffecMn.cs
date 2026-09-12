using System;

namespace Game3
{
	// Token: 0x020002B4 RID: 692
	public class EffecMn
	{
		// Token: 0x06001EC9 RID: 7881 RVA: 0x001E965C File Offset: 0x001E785C
		public static void addEff(Effect me)
		{
			EffecMn.vEff.addElement(me);
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x001E9669 File Offset: 0x001E7869
		public static void removeEff(int id)
		{
			if (EffecMn.getEffById(id) != null)
			{
				EffecMn.vEff.removeElement(EffecMn.getEffById(id));
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x001E9684 File Offset: 0x001E7884
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

		// Token: 0x06001ECC RID: 7884 RVA: 0x001E96C4 File Offset: 0x001E78C4
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

		// Token: 0x06001ECD RID: 7885 RVA: 0x001E9720 File Offset: 0x001E7920
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

		// Token: 0x06001ECE RID: 7886 RVA: 0x001E9778 File Offset: 0x001E7978
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

		// Token: 0x06001ECF RID: 7887 RVA: 0x001E97D0 File Offset: 0x001E79D0
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

		// Token: 0x06001ED0 RID: 7888 RVA: 0x001E9828 File Offset: 0x001E7A28
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

		// Token: 0x06001ED1 RID: 7889 RVA: 0x001E9880 File Offset: 0x001E7A80
		public static void update()
		{
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				((Effect)EffecMn.vEff.elementAt(i)).update();
			}
		}

		// Token: 0x04003B1A RID: 15130
		public static MyVector vEff = new MyVector();
	}
}
