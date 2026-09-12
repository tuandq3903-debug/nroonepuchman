using System;
using System.Text;

namespace Game2
{
	// Token: 0x020003DF RID: 991
	public class myReader
	{
		// Token: 0x06002BEE RID: 11246 RVA: 0x002AF757 File Offset: 0x002AD957
		public myReader(sbyte[] data)
		{
			this.buffer = data;
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x002AF768 File Offset: 0x002AD968
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

		// Token: 0x06002BF0 RID: 11248 RVA: 0x002AF7B6 File Offset: 0x002AD9B6
		public sbyte readByte()
		{
			return this.readSByte();
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x002AF7BE File Offset: 0x002AD9BE
		public void mark(int readlimit)
		{
			this.posMark = this.posRead;
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x002AF7CC File Offset: 0x002AD9CC
		public void reset()
		{
			this.posRead = this.posMark;
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x002AF7DA File Offset: 0x002AD9DA
		public byte readUnsignedByte()
		{
			return myReader.convertSbyteToByte(this.readSByte());
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x002AF7E8 File Offset: 0x002AD9E8
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

		// Token: 0x06002BF5 RID: 11253 RVA: 0x002AF82C File Offset: 0x002ADA2C
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

		// Token: 0x06002BF6 RID: 11254 RVA: 0x002AF870 File Offset: 0x002ADA70
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

		// Token: 0x06002BF7 RID: 11255 RVA: 0x002AF8B4 File Offset: 0x002ADAB4
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

		// Token: 0x06002BF8 RID: 11256 RVA: 0x002AF8F7 File Offset: 0x002ADAF7
		public bool readBool()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x002AF8F7 File Offset: 0x002ADAF7
		public bool readBoolean()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x002AF904 File Offset: 0x002ADB04
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

		// Token: 0x06002BFB RID: 11259 RVA: 0x002AF944 File Offset: 0x002ADB44
		public string readUTF()
		{
			return this.readStringUTF();
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x002AF94C File Offset: 0x002ADB4C
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

		// Token: 0x06002BFD RID: 11261 RVA: 0x002AF994 File Offset: 0x002ADB94
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

		// Token: 0x06002BFE RID: 11262 RVA: 0x002AF9D3 File Offset: 0x002ADBD3
		public int available()
		{
			return this.buffer.Length - this.posRead;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x002AF9E4 File Offset: 0x002ADBE4
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x002AF9F0 File Offset: 0x002ADBF0
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

		// Token: 0x040056DE RID: 22238
		public sbyte[] buffer;

		// Token: 0x040056DF RID: 22239
		private int posRead;

		// Token: 0x040056E0 RID: 22240
		private int posMark;
	}
}
