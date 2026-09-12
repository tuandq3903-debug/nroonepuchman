using System;
using System.Text;

namespace Game4
{
	// Token: 0x02000232 RID: 562
	public class myWriter
	{
		// Token: 0x060018C9 RID: 6345 RVA: 0x00185A2F File Offset: 0x00183C2F
		public myWriter()
		{
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00185A52 File Offset: 0x00183C52
		public myWriter(int len)
		{
			this.buffer = new sbyte[len];
			this.lenght = len;
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00185A88 File Offset: 0x00183C88
		public void writeSByte(sbyte value)
		{
			this.checkLenght(0);
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00185AB8 File Offset: 0x00183CB8
		public void writeSByteUncheck(sbyte value)
		{
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00185ADE File Offset: 0x00183CDE
		public void writeByte(sbyte value)
		{
			this.writeSByte(value);
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00185AE7 File Offset: 0x00183CE7
		public void writeByte(int value)
		{
			this.writeSByte((sbyte)value);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00185AF1 File Offset: 0x00183CF1
		public void writeChar(char value)
		{
			this.writeSByte(0);
			this.writeSByte((sbyte)value);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00185B04 File Offset: 0x00183D04
		public void writeSByte(sbyte[] value)
		{
			this.checkLenght(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				this.writeSByteUncheck(value[i]);
			}
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00185B34 File Offset: 0x00183D34
		public void writeShort(short value)
		{
			this.checkLenght(2);
			for (int num = 1; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00185B64 File Offset: 0x00183D64
		public void writeShort(int value)
		{
			this.checkLenght(2);
			short num = (short)value;
			for (int num2 = 1; num2 >= 0; num2--)
			{
				this.writeSByteUncheck((sbyte)(num >> num2 * 8));
			}
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00185B98 File Offset: 0x00183D98
		public void writeInt(int value)
		{
			this.checkLenght(4);
			for (int num = 3; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00185BC7 File Offset: 0x00183DC7
		public void writeBoolean(bool value)
		{
			writeSByte((sbyte)(value ? 1 : 0));
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00185BD8 File Offset: 0x00183DD8
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

		// Token: 0x060018D6 RID: 6358 RVA: 0x00185C34 File Offset: 0x00183E34
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

		// Token: 0x060018D7 RID: 6359 RVA: 0x00185C6E File Offset: 0x00183E6E
		public void write(sbyte[] value)
		{
			this.writeSByte(value);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00185C78 File Offset: 0x00183E78
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

		// Token: 0x060018D9 RID: 6361 RVA: 0x00185CBC File Offset: 0x00183EBC
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

		// Token: 0x060018DA RID: 6362 RVA: 0x00185D2B File Offset: 0x00183F2B
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x040031E4 RID: 12772
		public sbyte[] buffer = new sbyte[2048];

		// Token: 0x040031E5 RID: 12773
		private int posWrite;

		// Token: 0x040031E6 RID: 12774
		private int lenght = 2048;
	}
}
