using System;

namespace Game6
{
	// Token: 0x0200004F RID: 79
	public class InputDlg : Dialog
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x000462A0 File Offset: 0x000444A0
		public void show(string info, Command ok, int type)
		{
			this.tfInput.setText(string.Empty);
			this.tfInput.setIputType(type);
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
			this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
			this.center = ok;
			this.show();
		}

		// Token: 0x0400076E RID: 1902
		protected string[] info;

		// Token: 0x0400076F RID: 1903
		public TField tfInput;

		// Token: 0x04000770 RID: 1904
		private int padLeft;
	}
}
