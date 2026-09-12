using System;
using UnityEngine;

namespace Game1
{
	// Token: 0x02000461 RID: 1121
	public class DataInputStream
	{
		// Token: 0x060031F8 RID: 12792 RVA: 0x003133A4 File Offset: 0x003115A4
		public DataInputStream(string filename)
		{
			TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
			this.r = new myReader(ArrayCast.cast(textAsset.bytes));
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x003133E3 File Offset: 0x003115E3
		public DataInputStream(sbyte[] data)
		{
			this.r = new myReader(data);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x003133F7 File Offset: 0x003115F7
		public static void update()
		{
			if (DataInputStream.status == 2)
			{
				DataInputStream.status = 1;
				DataInputStream.istemp = DataInputStream.__getResourceAsStream(DataInputStream.filenametemp);
				DataInputStream.status = 0;
			}
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x0031341C File Offset: 0x0031161C
		public static DataInputStream getResourceAsStream(string filename)
		{
			return DataInputStream.__getResourceAsStream(filename);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x00313424 File Offset: 0x00311624
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

		// Token: 0x060031FD RID: 12797 RVA: 0x00313450 File Offset: 0x00311650
		public short readShort()
		{
			return this.r.readShort();
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x0031345D File Offset: 0x0031165D
		public int read()
		{
			return (int)this.r.readUnsignedByte();
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x0031346A File Offset: 0x0031166A
		public void read(ref sbyte[] data)
		{
			this.r.read(ref data);
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x00313479 File Offset: 0x00311679
		public void close()
		{
			this.r.Close();
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x00313486 File Offset: 0x00311686
		public string readUTF()
		{
			return this.r.readUTF();
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x00313493 File Offset: 0x00311693
		public sbyte readByte()
		{
			return this.r.readByte();
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x003134A0 File Offset: 0x003116A0
		public int readUnsignedByte()
		{
			return (int)((byte)this.r.readByte());
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x003134AE File Offset: 0x003116AE
		public int available()
		{
			return this.r.available();
		}

		// Token: 0x0400600F RID: 24591
		public myReader r;

		// Token: 0x04006010 RID: 24592
		public static DataInputStream istemp;

		// Token: 0x04006011 RID: 24593
		private static int status;

		// Token: 0x04006012 RID: 24594
		private static string filenametemp;
	}
}
