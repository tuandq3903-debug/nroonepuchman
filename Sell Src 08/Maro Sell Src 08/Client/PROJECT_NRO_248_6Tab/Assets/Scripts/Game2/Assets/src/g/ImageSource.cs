using System;

namespace Game2.Assets.src.g
{
	// Token: 0x02000437 RID: 1079
	internal class ImageSource
	{
		// Token: 0x06003016 RID: 12310 RVA: 0x002E7A0E File Offset: 0x002E5C0E
		public ImageSource(string ID, sbyte version)
		{
			this.id = ID;
			this.version = version;
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x002E7A24 File Offset: 0x002E5C24
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

		// Token: 0x06003018 RID: 12312 RVA: 0x002E7AF0 File Offset: 0x002E5CF0
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

		// Token: 0x04005C42 RID: 23618
		public sbyte version;

		// Token: 0x04005C43 RID: 23619
		public string id;

		// Token: 0x04005C44 RID: 23620
		public static MyVector vSource = new MyVector();

		// Token: 0x04005C45 RID: 23621
		public static MyVector vRms = new MyVector();
	}
}
