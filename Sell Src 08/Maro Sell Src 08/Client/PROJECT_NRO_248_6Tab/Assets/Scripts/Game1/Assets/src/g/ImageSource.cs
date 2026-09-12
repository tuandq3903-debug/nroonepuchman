using System;

namespace Game1.Assets.src.g
{
	// Token: 0x0200050F RID: 1295
	internal class ImageSource
	{
		// Token: 0x060039BA RID: 14778 RVA: 0x0037CAB2 File Offset: 0x0037ACB2
		public ImageSource(string ID, sbyte version)
		{
			this.id = ID;
			this.version = version;
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x0037CAC8 File Offset: 0x0037ACC8
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

		// Token: 0x060039BC RID: 14780 RVA: 0x0037CB94 File Offset: 0x0037AD94
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

		// Token: 0x04006EC1 RID: 28353
		public sbyte version;

		// Token: 0x04006EC2 RID: 28354
		public string id;

		// Token: 0x04006EC3 RID: 28355
		public static MyVector vSource = new MyVector();

		// Token: 0x04006EC4 RID: 28356
		public static MyVector vRms = new MyVector();
	}
}
