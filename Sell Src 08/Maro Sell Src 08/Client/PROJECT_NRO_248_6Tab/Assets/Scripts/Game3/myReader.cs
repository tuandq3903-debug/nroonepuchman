using System;
using System.Text;

namespace Game3
{
	// Token: 0x02000307 RID: 775
	public class myReader
	{
		// Token: 0x0600224A RID: 8778 RVA: 0x0021A6B3 File Offset: 0x002188B3
		public myReader(sbyte[] data)
		{
			this.buffer = data;
		}

		// Token: 0x0600224B RID: 8779 RVA: 0x0021A6C4 File Offset: 0x002188C4
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

		// Token: 0x0600224C RID: 8780 RVA: 0x0021A712 File Offset: 0x00218912
		public sbyte readByte()
		{
			return this.readSByte();
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0021A71A File Offset: 0x0021891A
		public void mark(int readlimit)
		{
			this.posMark = this.posRead;
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x0021A728 File Offset: 0x00218928
		public void reset()
		{
			this.posRead = this.posMark;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0021A736 File Offset: 0x00218936
		public byte readUnsignedByte()
		{
			return myReader.convertSbyteToByte(this.readSByte());
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0021A744 File Offset: 0x00218944
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

		// Token: 0x06002251 RID: 8785 RVA: 0x0021A788 File Offset: 0x00218988
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

		// Token: 0x06002252 RID: 8786 RVA: 0x0021A7CC File Offset: 0x002189CC
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

		// Token: 0x06002253 RID: 8787 RVA: 0x0021A810 File Offset: 0x00218A10
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

		// Token: 0x06002254 RID: 8788 RVA: 0x0021A853 File Offset: 0x00218A53
		public bool readBool()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x0021A853 File Offset: 0x00218A53
		public bool readBoolean()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0021A860 File Offset: 0x00218A60
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

		// Token: 0x06002257 RID: 8791 RVA: 0x0021A8A0 File Offset: 0x00218AA0
		public string readUTF()
		{
			return this.readStringUTF();
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x0021A8A8 File Offset: 0x00218AA8
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

		// Token: 0x06002259 RID: 8793 RVA: 0x0021A8F0 File Offset: 0x00218AF0
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

		// Token: 0x0600225A RID: 8794 RVA: 0x0021A92F File Offset: 0x00218B2F
		public int available()
		{
			return this.buffer.Length - this.posRead;
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0021A940 File Offset: 0x00218B40
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x0021A94C File Offset: 0x00218B4C
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

		// Token: 0x0400445F RID: 17503
		public sbyte[] buffer;

		// Token: 0x04004460 RID: 17504
		private int posRead;

		// Token: 0x04004461 RID: 17505
		private int posMark;
	}
}
