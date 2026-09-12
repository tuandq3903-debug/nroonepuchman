using System;

namespace Game3
{
	// Token: 0x020002D7 RID: 727
	public class InputDlg : Dialog
	{
		// Token: 0x0600208F RID: 8335 RVA: 0x00205594 File Offset: 0x00203794
		public void show(string info, Command ok, int type)
		{
			this.tfInput.setText(string.Empty);
			this.tfInput.setIputType(type);
			this.info = mFont.tahoma_8b.splitFontArray(info, GameCanvas.w - this.padLeft * 2);
			this.left = new Command(mResources.CLOSE, GameCanvas.gI(), 8882, null);
			this.center = ok;
			this.show();
		}

		// Token: 0x04003EEB RID: 16107
		protected string[] info;

		// Token: 0x04003EEC RID: 16108
		public TField tfInput;

		// Token: 0x04003EED RID: 16109
		private int padLeft;
	}
}
