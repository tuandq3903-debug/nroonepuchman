using System;

namespace Game1
{
	// Token: 0x02000487 RID: 1159
	public class InputDlg : Dialog
	{
		// Token: 0x060033D7 RID: 13271 RVA: 0x0032F6DC File Offset: 0x0032D8DC
		public void show(string info, Command ok, int type)
		{
			this.tfInput.setText(string.Empty);
			this.tfInput.setIputType(type);
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
			this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
			this.center = ok;
			this.show();
		}

		// Token: 0x040063E9 RID: 25577
		protected string[] info;

		// Token: 0x040063EA RID: 25578
		public TField tfInput;

		// Token: 0x040063EB RID: 25579
		private int padLeft;
	}
}
