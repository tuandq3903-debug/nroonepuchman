using System;
using System.Text;

namespace Game2
{
	// Token: 0x020003E2 RID: 994
	public class myWriter
	{
		// Token: 0x06002C11 RID: 11281 RVA: 0x002AFB77 File Offset: 0x002ADD77
		public myWriter()
		{
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x002AFB9A File Offset: 0x002ADD9A
		public myWriter(int len)
		{
			this.buffer = new sbyte[len];
			this.lenght = len;
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x002AFBD0 File Offset: 0x002ADDD0
		public void writeSByte(sbyte value)
		{
			this.checkLenght(0);
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x002AFC00 File Offset: 0x002ADE00
		public void writeSByteUncheck(sbyte value)
		{
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x002AFC26 File Offset: 0x002ADE26
		public void writeByte(sbyte value)
		{
			this.writeSByte(value);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x002AFC2F File Offset: 0x002ADE2F
		public void writeByte(int value)
		{
			this.writeSByte((sbyte)value);
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x002AFC39 File Offset: 0x002ADE39
		public void writeChar(char value)
		{
			this.writeSByte(0);
			this.writeSByte((sbyte)value);
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x002AFC4C File Offset: 0x002ADE4C
		public void writeSByte(sbyte[] value)
		{
			this.checkLenght(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				this.writeSByteUncheck(value[i]);
			}
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x002AFC7C File Offset: 0x002ADE7C
		public void writeShort(short value)
		{
			this.checkLenght(2);
			for (int num = 1; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x002AFCAC File Offset: 0x002ADEAC
		public void writeShort(int value)
		{
			this.checkLenght(2);
			short num = (short)value;
			for (int num2 = 1; num2 >= 0; num2--)
			{
				this.writeSByteUncheck((sbyte)(num >> num2 * 8));
			}
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x002AFCE0 File Offset: 0x002ADEE0
		public void writeInt(int value)
		{
			this.checkLenght(4);
			for (int num = 3; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x002AFD0F File Offset: 0x002ADF0F
		public void writeBoolean(bool value)
		{
			writeSByte((sbyte)(value ? 1 : 0));
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x002AFD20 File Offset: 0x002ADF20
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

		// Token: 0x06002C1E RID: 11294 RVA: 0x002AFD7C File Offset: 0x002ADF7C
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

		// Token: 0x06002C1F RID: 11295 RVA: 0x002AFDB6 File Offset: 0x002ADFB6
		public void write(sbyte[] value)
		{
			this.writeSByte(value);
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x002AFDC0 File Offset: 0x002ADFC0
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

		// Token: 0x06002C21 RID: 11297 RVA: 0x002AFE04 File Offset: 0x002AE004
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

		// Token: 0x06002C22 RID: 11298 RVA: 0x002AFE73 File Offset: 0x002AE073
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x040056E2 RID: 22242
		public sbyte[] buffer = new sbyte[2048];

		// Token: 0x040056E3 RID: 22243
		private int posWrite;

		// Token: 0x040056E4 RID: 22244
		private int lenght = 2048;
	}
}
