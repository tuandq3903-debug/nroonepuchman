using System;

namespace Game2
{
	// Token: 0x0200038C RID: 908
	public class EffecMn
	{
		// Token: 0x0600286D RID: 10349 RVA: 0x0027E700 File Offset: 0x0027C900
		public static void addEff(Effect me)
		{
			EffecMn.vEff.addElement(me);
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x0027E70D File Offset: 0x0027C90D
		public static void removeEff(int id)
		{
			if (EffecMn.getEffById(id) != null)
			{
				EffecMn.vEff.removeElement(EffecMn.getEffById(id));
			}
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0027E728 File Offset: 0x0027C928
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

		// Token: 0x06002870 RID: 10352 RVA: 0x0027E768 File Offset: 0x0027C968
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

		// Token: 0x06002871 RID: 10353 RVA: 0x0027E7C4 File Offset: 0x0027C9C4
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

		// Token: 0x06002872 RID: 10354 RVA: 0x0027E81C File Offset: 0x0027CA1C
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

		// Token: 0x06002873 RID: 10355 RVA: 0x0027E874 File Offset: 0x0027CA74
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

		// Token: 0x06002874 RID: 10356 RVA: 0x0027E8CC File Offset: 0x0027CACC
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

		// Token: 0x06002875 RID: 10357 RVA: 0x0027E924 File Offset: 0x0027CB24
		public static void update()
		{
			for (int i = 0; i < EffecMn.vEff.size(); i++)
			{
				((Effect)EffecMn.vEff.elementAt(i)).update();
			}
		}

		// Token: 0x04004D99 RID: 19865
		public static MyVector vEff = new MyVector();
	}
}
