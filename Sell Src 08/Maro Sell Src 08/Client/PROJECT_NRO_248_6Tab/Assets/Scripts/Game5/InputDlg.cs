using System;

namespace Game5
{
	// Token: 0x02000127 RID: 295
	public class InputDlg : Dialog
	{
		// Token: 0x06000D47 RID: 3399 RVA: 0x000DB44C File Offset: 0x000D964C
		public void show(string info, Command ok, int type)
		{
			this.tfInput.setText(string.Empty);
			this.tfInput.setIputType(type);
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
			this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
			this.center = ok;
			this.show();
		}

		// Token: 0x040019ED RID: 6637
		protected string[] info;

		// Token: 0x040019EE RID: 6638
		public TField tfInput;

		// Token: 0x040019EF RID: 6639
		private int padLeft;
	}
}
