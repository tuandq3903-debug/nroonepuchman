using System;
using UnityEngine;

namespace Game3
{
	// Token: 0x020002B1 RID: 689
	public class DataInputStream
	{
		// Token: 0x06001EB0 RID: 7856 RVA: 0x001E925C File Offset: 0x001E745C
		public DataInputStream(string filename)
		{
			TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
			this.r = new myReader(ArrayCast.cast(textAsset.bytes));
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x001E929B File Offset: 0x001E749B
		public DataInputStream(sbyte[] data)
		{
			this.r = new myReader(data);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x001E92AF File Offset: 0x001E74AF
		public static void update()
		{
			if (DataInputStream.status == 2)
			{
				DataInputStream.status = 1;
				DataInputStream.istemp = DataInputStream.__getResourceAsStream(DataInputStream.filenametemp);
				DataInputStream.status = 0;
			}
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x001E92D4 File Offset: 0x001E74D4
		public static DataInputStream getResourceAsStream(string filename)
		{
			return DataInputStream.__getResourceAsStream(filename);
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x001E92DC File Offset: 0x001E74DC
		private static DataInputStream __getResourceAsStream(string filename)
		{
			DataInputStream result;
			try
			{
				result = new DataInputStream(filename);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x001E9308 File Offset: 0x001E7508
		public short readShort()
		{
			return this.r.readShort();
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x001E9315 File Offset: 0x001E7515
		public int read()
		{
			return (int)this.r.readUnsignedByte();
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x001E9322 File Offset: 0x001E7522
		public void read(ref sbyte[] data)
		{
			this.r.read(ref data);
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x001E9331 File Offset: 0x001E7531
		public void close()
		{
			this.r.Close();
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x001E933E File Offset: 0x001E753E
		public string readUTF()
		{
			return this.r.readUTF();
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x001E934B File Offset: 0x001E754B
		public sbyte readByte()
		{
			return this.r.readByte();
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x001E9358 File Offset: 0x001E7558
		public int readUnsignedByte()
		{
			return (int)((byte)this.r.readByte());
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x001E9366 File Offset: 0x001E7566
		public int available()
		{
			return this.r.available();
		}

		// Token: 0x04003B11 RID: 15121
		public myReader r;

		// Token: 0x04003B12 RID: 15122
		public static DataInputStream istemp;

		// Token: 0x04003B13 RID: 15123
		private static int status;

		// Token: 0x04003B14 RID: 15124
		private static string filenametemp;
	}
}
