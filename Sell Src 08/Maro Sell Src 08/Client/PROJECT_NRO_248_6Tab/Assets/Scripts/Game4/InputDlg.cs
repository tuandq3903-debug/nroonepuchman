using System;

namespace Game4
{
	// Token: 0x020001FF RID: 511
	public class InputDlg : Dialog
	{
		// Token: 0x060016EB RID: 5867 RVA: 0x001704F0 File Offset: 0x0016E6F0
		public void show(string info, Command ok, int type)
		{
			this.tfInput.setText(string.Empty);
			this.tfInput.setIputType(type);
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
			this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
			this.center = ok;
			this.show();
		}

		// Token: 0x04002C6C RID: 11372
		protected string[] info;

		// Token: 0x04002C6D RID: 11373
		public TField tfInput;

		// Token: 0x04002C6E RID: 11374
		private int padLeft;
	}
}
