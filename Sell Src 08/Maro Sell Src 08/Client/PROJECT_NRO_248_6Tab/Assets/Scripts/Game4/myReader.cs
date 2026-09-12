using System;
using System.Text;

namespace Game4
{
	// Token: 0x0200022F RID: 559
	public class myReader
	{
		// Token: 0x060018A6 RID: 6310 RVA: 0x0018560F File Offset: 0x0018380F
		public myReader(sbyte[] data)
		{
			this.buffer = data;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00185620 File Offset: 0x00183820
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

		// Token: 0x060018A8 RID: 6312 RVA: 0x0018566E File Offset: 0x0018386E
		public sbyte readByte()
		{
			return this.readSByte();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00185676 File Offset: 0x00183876
		public void mark(int readlimit)
		{
			this.posMark = this.posRead;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00185684 File Offset: 0x00183884
		public void reset()
		{
			this.posRead = this.posMark;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00185692 File Offset: 0x00183892
		public byte readUnsignedByte()
		{
			return myReader.convertSbyteToByte(this.readSByte());
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x001856A0 File Offset: 0x001838A0
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

		// Token: 0x060018AD RID: 6317 RVA: 0x001856E4 File Offset: 0x001838E4
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

		// Token: 0x060018AE RID: 6318 RVA: 0x00185728 File Offset: 0x00183928
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

		// Token: 0x060018AF RID: 6319 RVA: 0x0018576C File Offset: 0x0018396C
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

		// Token: 0x060018B0 RID: 6320 RVA: 0x001857AF File Offset: 0x001839AF
		public bool readBool()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x001857AF File Offset: 0x001839AF
		public bool readBoolean()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x001857BC File Offset: 0x001839BC
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

		// Token: 0x060018B3 RID: 6323 RVA: 0x001857FC File Offset: 0x001839FC
		public string readUTF()
		{
			return this.readStringUTF();
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00185804 File Offset: 0x00183A04
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

		// Token: 0x060018B5 RID: 6325 RVA: 0x0018584C File Offset: 0x00183A4C
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

		// Token: 0x060018B6 RID: 6326 RVA: 0x0018588B File Offset: 0x00183A8B
		public int available()
		{
			return this.buffer.Length - this.posRead;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0018589C File Offset: 0x00183A9C
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x001858A8 File Offset: 0x00183AA8
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

		// Token: 0x040031E0 RID: 12768
		public sbyte[] buffer;

		// Token: 0x040031E1 RID: 12769
		private int posRead;

		// Token: 0x040031E2 RID: 12770
		private int posMark;
	}
}
