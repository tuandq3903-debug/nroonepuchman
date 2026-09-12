using System;
using UnityEngine;

namespace Game2
{
	// Token: 0x02000389 RID: 905
	public class DataInputStream
	{
		// Token: 0x06002854 RID: 10324 RVA: 0x0027E300 File Offset: 0x0027C500
		public DataInputStream(string filename)
		{
			TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
			this.r = new myReader(ArrayCast.cast(textAsset.bytes));
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x0027E33F File Offset: 0x0027C53F
		public DataInputStream(sbyte[] data)
		{
			this.r = new myReader(data);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x0027E353 File Offset: 0x0027C553
		public static void update()
		{
			if (DataInputStream.status == 2)
			{
				DataInputStream.status = 1;
				DataInputStream.istemp = DataInputStream.__getResourceAsStream(DataInputStream.filenametemp);
				DataInputStream.status = 0;
			}
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x0027E378 File Offset: 0x0027C578
		public static DataInputStream getResourceAsStream(string filename)
		{
			return DataInputStream.__getResourceAsStream(filename);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x0027E380 File Offset: 0x0027C580
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

		// Token: 0x06002859 RID: 10329 RVA: 0x0027E3AC File Offset: 0x0027C5AC
		public short readShort()
		{
			return this.r.readShort();
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x0027E3B9 File Offset: 0x0027C5B9
		public int read()
		{
			return (int)this.r.readUnsignedByte();
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x0027E3C6 File Offset: 0x0027C5C6
		public void read(ref sbyte[] data)
		{
			this.r.read(ref data);
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x0027E3D5 File Offset: 0x0027C5D5
		public void close()
		{
			this.r.Close();
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x0027E3E2 File Offset: 0x0027C5E2
		public string readUTF()
		{
			return this.r.readUTF();
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x0027E3EF File Offset: 0x0027C5EF
		public sbyte readByte()
		{
			return this.r.readByte();
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0027E3FC File Offset: 0x0027C5FC
		public int readUnsignedByte()
		{
			return (int)((byte)this.r.readByte());
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x0027E40A File Offset: 0x0027C60A
		public int available()
		{
			return this.r.available();
		}

		// Token: 0x04004D90 RID: 19856
		public myReader r;

		// Token: 0x04004D91 RID: 19857
		public static DataInputStream istemp;

		// Token: 0x04004D92 RID: 19858
		private static int status;

		// Token: 0x04004D93 RID: 19859
		private static string filenametemp;
	}
}
