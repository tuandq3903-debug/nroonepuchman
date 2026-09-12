using System;
using System.Text;

namespace Game5
{
	// Token: 0x0200015A RID: 346
	public class myWriter
	{
		// Token: 0x06000F25 RID: 3877 RVA: 0x000F098B File Offset: 0x000EEB8B
		public myWriter()
		{
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000F09AE File Offset: 0x000EEBAE
		public myWriter(int len)
		{
			this.buffer = new sbyte[len];
			this.lenght = len;
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x000F09E4 File Offset: 0x000EEBE4
		public void writeSByte(sbyte value)
		{
			this.checkLenght(0);
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000F0A14 File Offset: 0x000EEC14
		public void writeSByteUncheck(sbyte value)
		{
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x000F0A3A File Offset: 0x000EEC3A
		public void writeByte(sbyte value)
		{
			this.writeSByte(value);
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x000F0A43 File Offset: 0x000EEC43
		public void writeByte(int value)
		{
			this.writeSByte((sbyte)value);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x000F0A4D File Offset: 0x000EEC4D
		public void writeChar(char value)
		{
			this.writeSByte(0);
			this.writeSByte((sbyte)value);
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x000F0A60 File Offset: 0x000EEC60
		public void writeSByte(sbyte[] value)
		{
			this.checkLenght(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				this.writeSByteUncheck(value[i]);
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x000F0A90 File Offset: 0x000EEC90
		public void writeShort(short value)
		{
			this.checkLenght(2);
			for (int num = 1; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x000F0AC0 File Offset: 0x000EECC0
		public void writeShort(int value)
		{
			this.checkLenght(2);
			short num = (short)value;
			for (int num2 = 1; num2 >= 0; num2--)
			{
				this.writeSByteUncheck((sbyte)(num >> num2 * 8));
			}
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000F0AF4 File Offset: 0x000EECF4
		public void writeInt(int value)
		{
			this.checkLenght(4);
			for (int num = 3; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000F0B23 File Offset: 0x000EED23
		public void writeBoolean(bool value)
		{
			writeSByte((sbyte)(value ? 1 : 0));
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000F0B34 File Offset: 0x000EED34
		public void writeUTF(string value)
		{
			Encoding unicode = Encoding.Unicode;
			Encoding encoding = Encoding.GetEncoding(65001);
			byte[] bytes = unicode.GetBytes(value);
			byte[] array = Encoding.Convert(unicode, encoding, bytes);
			this.writeShort((short)array.Length);
			this.checkLenght(array.Length);
			foreach (sbyte value2 in array)
			{
				this.writeSByteUncheck(value2);
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x000F0B90 File Offset: 0x000EED90
		public void write(ref sbyte[] data, int arg1, int arg2)
		{
			if (data == null)
			{
				return;
			}
			for (int i = 0; i < arg2; i++)
			{
				this.writeSByte(data[i + arg1]);
				if (this.posWrite > this.buffer.Length)
				{
					break;
				}
			}
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x000F0BCA File Offset: 0x000EEDCA
		public void write(sbyte[] value)
		{
			this.writeSByte(value);
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000F0BD4 File Offset: 0x000EEDD4
		public sbyte[] getData()
		{
			if (this.posWrite <= 0)
			{
				return null;
			}
			sbyte[] array = new sbyte[this.posWrite];
			for (int i = 0; i < this.posWrite; i++)
			{
				array[i] = this.buffer[i];
			}
			return array;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x000F0C18 File Offset: 0x000EEE18
		public void checkLenght(int ltemp)
		{
			if (this.posWrite + ltemp > this.lenght)
			{
				sbyte[] array = new sbyte[this.lenght + 1024 + ltemp];
				for (int i = 0; i < this.lenght; i++)
				{
					array[i] = this.buffer[i];
				}
				this.buffer = null;
				this.buffer = array;
				this.lenght += 1024 + ltemp;
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x000F0C87 File Offset: 0x000EEE87
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x04001F65 RID: 8037
		public sbyte[] buffer = new sbyte[2048];

		// Token: 0x04001F66 RID: 8038
		private int posWrite;

		// Token: 0x04001F67 RID: 8039
		private int lenght = 2048;
	}
}
