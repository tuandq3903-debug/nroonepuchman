using System;
using System.Text;

namespace Game6
{
	// Token: 0x0200007F RID: 127
	public class myReader
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0005B403 File Offset: 0x00059603
		public myReader(sbyte[] data)
		{
			this.buffer = data;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0005B414 File Offset: 0x00059614
		public sbyte readSByte()
		{
			if (this.posRead < this.buffer.Length)
			{
				sbyte[] array = this.buffer;
				int num = this.posRead;
				this.posRead = num + 1;
				return array[num];
			}
			this.posRead = this.buffer.Length;
			throw new Exception(" loi doc sbyte eof ");
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0005B462 File Offset: 0x00059662
		public sbyte readByte()
		{
			return this.readSByte();
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0005B46A File Offset: 0x0005966A
		public void mark(int readlimit)
		{
			this.posMark = this.posRead;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0005B478 File Offset: 0x00059678
		public void reset()
		{
			this.posRead = this.posMark;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0005B486 File Offset: 0x00059686
		public byte readUnsignedByte()
		{
			return myReader.convertSbyteToByte(this.readSByte());
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0005B494 File Offset: 0x00059694
		public short readShort()
		{
			short num = 0;
			for (int i = 0; i < 2; i++)
			{
				num = (short)(num << 8);
				short num2 = num;
				short num3 = 255;
				sbyte[] array = this.buffer;
				int num4 = this.posRead;
				this.posRead = num4 + 1;
				num = (short)(num2 | (num3 & array[num4]));
			}
			return num;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0005B4D8 File Offset: 0x000596D8
		public ushort readUnsignedShort()
		{
			ushort num = 0;
			for (int i = 0; i < 2; i++)
			{
				num = (ushort)(num << 8);
				ushort num2 = num;
				ushort num3 = 255;
				sbyte[] array = this.buffer;
				int num4 = this.posRead;
				this.posRead = num4 + 1;
				num = (ushort)(num2 | (num3 & array[num4]));
			}
			return num;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0005B51C File Offset: 0x0005971C
		public int readInt()
		{
			int num = 0;
			for (int i = 0; i < 4; i++)
			{
				num <<= 8;
				int num2 = num;
				int num3 = 255;
				sbyte[] array = this.buffer;
				int num4 = this.posRead;
				this.posRead = num4 + 1;
				num = (num2 | (num3 & array[num4]));
			}
			return num;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0005B560 File Offset: 0x00059760
		public long readLong()
		{
			long num = 0L;
			for (int i = 0; i < 8; i++)
			{
				num <<= 8;
				long num2 = num;
				long num3 = 255L;
				sbyte[] array = this.buffer;
				int num4 = this.posRead;
				this.posRead = num4 + 1;
				num = (num2 | (num3 & array[num4]));
			}
			return num;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0005B5A3 File Offset: 0x000597A3
		public bool readBool()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0005B5A3 File Offset: 0x000597A3
		public bool readBoolean()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0005B5B0 File Offset: 0x000597B0
		public string readStringUTF()
		{
			short num = this.readShort();
			byte[] array = new byte[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				array[i] = myReader.convertSbyteToByte(this.readSByte());
			}
			return new UTF8Encoding().GetString(array);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0005B5F0 File Offset: 0x000597F0
		public string readUTF()
		{
			return this.readStringUTF();
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0005B5F8 File Offset: 0x000597F8
		public int read(ref sbyte[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = this.readSByte();
				if (this.posRead > this.buffer.Length)
				{
					return -1;
				}
				num++;
			}
			return num;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0005B640 File Offset: 0x00059840
		public void readFully(ref sbyte[] data)
		{
			if (data != null && data.Length + this.posRead <= this.buffer.Length)
			{
				for (int i = 0; i < data.Length; i++)
				{
					data[i] = this.readSByte();
				}
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0005B67F File Offset: 0x0005987F
		public int available()
		{
			return this.buffer.Length - this.posRead;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0005B690 File Offset: 0x00059890
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0005B69C File Offset: 0x0005989C
		public void read(ref sbyte[] data, int arg1, int arg2)
		{
			if (data == null)
			{
				return;
			}
			for (int i = 0; i < arg2; i++)
			{
				data[i + arg1] = this.readSByte();
				if (this.posRead > this.buffer.Length)
				{
					break;
				}
			}
		}

		// Token: 0x04000CE2 RID: 3298
		public sbyte[] buffer;

		// Token: 0x04000CE3 RID: 3299
		private int posRead;

		// Token: 0x04000CE4 RID: 3300
		private int posMark;
	}
}
