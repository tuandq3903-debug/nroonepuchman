using System;
using UnityEngine;

namespace Game4
{
	// Token: 0x020001D9 RID: 473
	public class DataInputStream
	{
		// Token: 0x0600150C RID: 5388 RVA: 0x001541B8 File Offset: 0x001523B8
		public DataInputStream(string filename)
		{
			TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
			this.r = new myReader(ArrayCast.cast(textAsset.bytes));
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x001541F7 File Offset: 0x001523F7
		public DataInputStream(sbyte[] data)
		{
			this.r = new myReader(data);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0015420B File Offset: 0x0015240B
		public static void update()
		{
			if (DataInputStream.status == 2)
			{
				DataInputStream.status = 1;
				DataInputStream.istemp = DataInputStream.__getResourceAsStream(DataInputStream.filenametemp);
				DataInputStream.status = 0;
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00154230 File Offset: 0x00152430
		public static DataInputStream getResourceAsStream(string filename)
		{
			return DataInputStream.__getResourceAsStream(filename);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00154238 File Offset: 0x00152438
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

		// Token: 0x06001511 RID: 5393 RVA: 0x00154264 File Offset: 0x00152464
		public short readShort()
		{
			return this.r.readShort();
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00154271 File Offset: 0x00152471
		public int read()
		{
			return (int)this.r.readUnsignedByte();
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0015427E File Offset: 0x0015247E
		public void read(ref sbyte[] data)
		{
			this.r.read(ref data);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0015428D File Offset: 0x0015248D
		public void close()
		{
			this.r.Close();
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0015429A File Offset: 0x0015249A
		public string readUTF()
		{
			return this.r.readUTF();
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x001542A7 File Offset: 0x001524A7
		public sbyte readByte()
		{
			return this.r.readByte();
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x001542B4 File Offset: 0x001524B4
		public int readUnsignedByte()
		{
			return (int)((byte)this.r.readByte());
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x001542C2 File Offset: 0x001524C2
		public int available()
		{
			return this.r.available();
		}

		// Token: 0x04002892 RID: 10386
		public myReader r;

		// Token: 0x04002893 RID: 10387
		public static DataInputStream istemp;

		// Token: 0x04002894 RID: 10388
		private static int status;

		// Token: 0x04002895 RID: 10389
		private static string filenametemp;
	}
}
