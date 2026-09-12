using System;
using UnityEngine;

namespace Game5
{
	// Token: 0x02000101 RID: 257
	public class DataInputStream
	{
		// Token: 0x06000B68 RID: 2920 RVA: 0x000BF114 File Offset: 0x000BD314
		public DataInputStream(string filename)
		{
			TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
			this.r = new myReader(ArrayCast.cast(textAsset.bytes));
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000BF153 File Offset: 0x000BD353
		public DataInputStream(sbyte[] data)
		{
			this.r = new myReader(data);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000BF167 File Offset: 0x000BD367
		public static void update()
		{
			if (DataInputStream.status == 2)
			{
				DataInputStream.status = 1;
				DataInputStream.istemp = DataInputStream.__getResourceAsStream(DataInputStream.filenametemp);
				DataInputStream.status = 0;
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x000BF18C File Offset: 0x000BD38C
		public static DataInputStream getResourceAsStream(string filename)
		{
			return DataInputStream.__getResourceAsStream(filename);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000BF194 File Offset: 0x000BD394
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

		// Token: 0x06000B6D RID: 2925 RVA: 0x000BF1C0 File Offset: 0x000BD3C0
		public short readShort()
		{
			return this.r.readShort();
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000BF1CD File Offset: 0x000BD3CD
		public int read()
		{
			return (int)this.r.readUnsignedByte();
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000BF1DA File Offset: 0x000BD3DA
		public void read(ref sbyte[] data)
		{
			this.r.read(ref data);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000BF1E9 File Offset: 0x000BD3E9
		public void close()
		{
			this.r.Close();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x000BF1F6 File Offset: 0x000BD3F6
		public string readUTF()
		{
			return this.r.readUTF();
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x000BF203 File Offset: 0x000BD403
		public sbyte readByte()
		{
			return this.r.readByte();
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x000BF210 File Offset: 0x000BD410
		public int readUnsignedByte()
		{
			return (int)((byte)this.r.readByte());
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x000BF21E File Offset: 0x000BD41E
		public int available()
		{
			return this.r.available();
		}

		// Token: 0x04001613 RID: 5651
		public myReader r;

		// Token: 0x04001614 RID: 5652
		public static DataInputStream istemp;

		// Token: 0x04001615 RID: 5653
		private static int status;

		// Token: 0x04001616 RID: 5654
		private static string filenametemp;
	}
}
