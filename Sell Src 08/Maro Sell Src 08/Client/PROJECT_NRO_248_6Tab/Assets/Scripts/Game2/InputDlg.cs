using System;

namespace Game2
{
	// Token: 0x020003AF RID: 943
	public class InputDlg : Dialog
	{
		// Token: 0x06002A33 RID: 10803 RVA: 0x0029A638 File Offset: 0x00298838
		public void show(string info, Command ok, int type)
		{
			this.tfInput.setText(string.Empty);
			this.tfInput.setIputType(type);
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
			this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
			this.center = ok;
			this.show();
		}

		// Token: 0x0400516A RID: 20842
		protected string[] info;

		// Token: 0x0400516B RID: 20843
		public TField tfInput;

		// Token: 0x0400516C RID: 20844
		private int padLeft;
	}
}
