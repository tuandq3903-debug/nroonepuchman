using System;
using System.Text;

namespace Game1
{
	// Token: 0x020004B7 RID: 1207
	public class myReader
	{
		// Token: 0x06003592 RID: 13714 RVA: 0x003447FB File Offset: 0x003429FB
		public myReader(sbyte[] data)
		{
			this.buffer = data;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x0034480C File Offset: 0x00342A0C
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

		// Token: 0x06003594 RID: 13716 RVA: 0x0034485A File Offset: 0x00342A5A
		public sbyte readByte()
		{
			return this.readSByte();
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x00344862 File Offset: 0x00342A62
		public void mark(int readlimit)
		{
			this.posMark = this.posRead;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x00344870 File Offset: 0x00342A70
		public void reset()
		{
			this.posRead = this.posMark;
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x0034487E File Offset: 0x00342A7E
		public byte readUnsignedByte()
		{
			return myReader.convertSbyteToByte(this.readSByte());
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x0034488C File Offset: 0x00342A8C
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

		// Token: 0x06003599 RID: 13721 RVA: 0x003448D0 File Offset: 0x00342AD0
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

		// Token: 0x0600359A RID: 13722 RVA: 0x00344914 File Offset: 0x00342B14
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

		// Token: 0x0600359B RID: 13723 RVA: 0x00344958 File Offset: 0x00342B58
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

		// Token: 0x0600359C RID: 13724 RVA: 0x0034499B File Offset: 0x00342B9B
		public bool readBool()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x0034499B File Offset: 0x00342B9B
		public bool readBoolean()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x003449A8 File Offset: 0x00342BA8
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

		// Token: 0x0600359F RID: 13727 RVA: 0x003449E8 File Offset: 0x00342BE8
		public string readUTF()
		{
			return this.readStringUTF();
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x003449F0 File Offset: 0x00342BF0
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

		// Token: 0x060035A1 RID: 13729 RVA: 0x00344A38 File Offset: 0x00342C38
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

		// Token: 0x060035A2 RID: 13730 RVA: 0x00344A77 File Offset: 0x00342C77
		public int available()
		{
			return this.buffer.Length - this.posRead;
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x00344A88 File Offset: 0x00342C88
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x00344A94 File Offset: 0x00342C94
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

		// Token: 0x0400695D RID: 26973
		public sbyte[] buffer;

		// Token: 0x0400695E RID: 26974
		private int posRead;

		// Token: 0x0400695F RID: 26975
		private int posMark;
	}
}
