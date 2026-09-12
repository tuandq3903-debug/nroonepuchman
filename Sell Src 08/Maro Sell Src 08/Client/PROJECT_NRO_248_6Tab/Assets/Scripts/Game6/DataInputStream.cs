using System;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000029 RID: 41
	public class DataInputStream
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00029F08 File Offset: 0x00028108
		public DataInputStream(string filename)
		{
			TextAsset textAsset = (TextAsset)Resources.Load(filename, typeof(TextAsset));
			this.r = new myReader(ArrayCast.cast(textAsset.bytes));
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00029F47 File Offset: 0x00028147
		public DataInputStream(sbyte[] data)
		{
			this.r = new myReader(data);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00029F5B File Offset: 0x0002815B
		public static void update()
		{
			if (DataInputStream.status == 2)
			{
				DataInputStream.status = 1;
				DataInputStream.istemp = DataInputStream.__getResourceAsStream(DataInputStream.filenametemp);
				DataInputStream.status = 0;
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00029F80 File Offset: 0x00028180
		public static DataInputStream getResourceAsStream(string filename)
		{
			return DataInputStream.__getResourceAsStream(filename);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00029F88 File Offset: 0x00028188
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

		// Token: 0x060001C9 RID: 457 RVA: 0x00029FB4 File Offset: 0x000281B4
		public short readShort()
		{
			return this.r.readShort();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00029FC1 File Offset: 0x000281C1
		public int read()
		{
			return (int)this.r.readUnsignedByte();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00029FCE File Offset: 0x000281CE
		public void read(ref sbyte[] data)
		{
			this.r.read(ref data);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00029FDD File Offset: 0x000281DD
		public void close()
		{
			this.r.Close();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00029FEA File Offset: 0x000281EA
		public string readUTF()
		{
			return this.r.readUTF();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00029FF7 File Offset: 0x000281F7
		public sbyte readByte()
		{
			return this.r.readByte();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0002A004 File Offset: 0x00028204
		public int readUnsignedByte()
		{
			return (int)((byte)this.r.readByte());
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0002A012 File Offset: 0x00028212
		public int available()
		{
			return this.r.available();
		}

		// Token: 0x04000394 RID: 916
		public myReader r;

		// Token: 0x04000395 RID: 917
		public static DataInputStream istemp;

		// Token: 0x04000396 RID: 918
		private static int status;

		// Token: 0x04000397 RID: 919
		private static string filenametemp;
	}
}
