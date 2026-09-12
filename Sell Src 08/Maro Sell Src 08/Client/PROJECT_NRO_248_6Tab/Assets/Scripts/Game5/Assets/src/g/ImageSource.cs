using System;

namespace Game5.Assets.src.g
{
	// Token: 0x020001AF RID: 431
	internal class ImageSource
	{
		// Token: 0x0600132A RID: 4906 RVA: 0x00128822 File Offset: 0x00126A22
		public ImageSource(string ID, sbyte version)
		{
			this.id = ID;
			this.version = version;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00128838 File Offset: 0x00126A38
		public static void checkRMS()
		{
			MyVector myVector = new MyVector();
			sbyte[] array = Rms.loadRMS("ImageSource");
			if (array == null)
			{
				Service.gI().imageSource(myVector);
				return;
			}
			ImageSource.vRms = new MyVector();
			DataInputStream dataInputStream = new DataInputStream(array);
			if (dataInputStream == null)
			{
				return;
			}
			try
			{
				short num = dataInputStream.readShort();
				string[] array2 = new string[(int)num];
				sbyte[] array3 = new sbyte[(int)num];
				for (int i = 0; i < (int)num; i++)
				{
					array2[i] = dataInputStream.readUTF();
					array3[i] = dataInputStream.readByte();
					ImageSource.vRms.addElement(new ImageSource(array2[i], array3[i]));
				}
				dataInputStream.close();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			Service.gI().imageSource(myVector);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00128904 File Offset: 0x00126B04
		public static void saveRMS()
		{
			DataOutputStream dataOutputStream = new DataOutputStream();
			try
			{
				dataOutputStream.writeShort((short)ImageSource.vSource.size());
				for (int i = 0; i < ImageSource.vSource.size(); i++)
				{
					dataOutputStream.writeUTF(((ImageSource)ImageSource.vSource.elementAt(i)).id);
					dataOutputStream.writeByte(((ImageSource)ImageSource.vSource.elementAt(i)).version);
				}
				Rms.saveRMS("ImageSource", dataOutputStream.toByteArray());
				dataOutputStream.close();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
		}

		// Token: 0x040024C5 RID: 9413
		public sbyte version;

		// Token: 0x040024C6 RID: 9414
		public string id;

		// Token: 0x040024C7 RID: 9415
		public static MyVector vSource = new MyVector();

		// Token: 0x040024C8 RID: 9416
		public static MyVector vRms = new MyVector();
	}
}
