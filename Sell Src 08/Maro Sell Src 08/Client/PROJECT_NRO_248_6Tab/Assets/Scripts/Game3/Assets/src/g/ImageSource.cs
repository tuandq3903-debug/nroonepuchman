using System;

namespace Game3.Assets.src.g
{
	// Token: 0x0200035F RID: 863
	internal class ImageSource
	{
		// Token: 0x06002672 RID: 9842 RVA: 0x0025296A File Offset: 0x00250B6A
		public ImageSource(string ID, sbyte version)
		{
			this.id = ID;
			this.version = version;
		}

		// Token: 0x06002673 RID: 9843 RVA: 0x00252980 File Offset: 0x00250B80
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

		// Token: 0x06002674 RID: 9844 RVA: 0x00252A4C File Offset: 0x00250C4C
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

		// Token: 0x040049C3 RID: 18883
		public sbyte version;

		// Token: 0x040049C4 RID: 18884
		public string id;

		// Token: 0x040049C5 RID: 18885
		public static MyVector vSource = new MyVector();

		// Token: 0x040049C6 RID: 18886
		public static MyVector vRms = new MyVector();
	}
}
