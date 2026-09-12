using System;
using System.Text;

namespace Game6
{
	// Token: 0x02000082 RID: 130
	public class myWriter
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x0005B823 File Offset: 0x00059A23
		public myWriter()
		{
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0005B846 File Offset: 0x00059A46
		public myWriter(int len)
		{
			this.buffer = new sbyte[len];
			this.lenght = len;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0005B87C File Offset: 0x00059A7C
		public void writeSByte(sbyte value)
		{
			this.checkLenght(0);
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0005B8AC File Offset: 0x00059AAC
		public void writeSByteUncheck(sbyte value)
		{
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0005B8D2 File Offset: 0x00059AD2
		public void writeByte(sbyte value)
		{
			this.writeSByte(value);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0005B8DB File Offset: 0x00059ADB
		public void writeByte(int value)
		{
			this.writeSByte((sbyte)value);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0005B8E5 File Offset: 0x00059AE5
		public void writeChar(char value)
		{
			this.writeSByte(0);
			this.writeSByte((sbyte)value);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0005B8F8 File Offset: 0x00059AF8
		public void writeSByte(sbyte[] value)
		{
			this.checkLenght(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				this.writeSByteUncheck(value[i]);
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0005B928 File Offset: 0x00059B28
		public void writeShort(short value)
		{
			this.checkLenght(2);
			for (int num = 1; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0005B958 File Offset: 0x00059B58
		public void writeShort(int value)
		{
			this.checkLenght(2);
			short num = (short)value;
			for (int num2 = 1; num2 >= 0; num2--)
			{
				this.writeSByteUncheck((sbyte)(num >> num2 * 8));
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0005B98C File Offset: 0x00059B8C
		public void writeInt(int value)
		{
			this.checkLenght(4);
			for (int num = 3; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0005B9BB File Offset: 0x00059BBB
		public void writeBoolean(bool value)
		{
			writeSByte((sbyte)(value ? 1 : 0));
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0005B9CC File Offset: 0x00059BCC
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

		// Token: 0x0600058E RID: 1422 RVA: 0x0005BA28 File Offset: 0x00059C28
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

		// Token: 0x0600058F RID: 1423 RVA: 0x0005BA62 File Offset: 0x00059C62
		public void write(sbyte[] value)
		{
			this.writeSByte(value);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0005BA6C File Offset: 0x00059C6C
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

		// Token: 0x06000591 RID: 1425 RVA: 0x0005BAB0 File Offset: 0x00059CB0
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

		// Token: 0x06000592 RID: 1426 RVA: 0x0005BB1F File Offset: 0x00059D1F
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x04000CE6 RID: 3302
		public sbyte[] buffer = new sbyte[2048];

		// Token: 0x04000CE7 RID: 3303
		private int posWrite;

		// Token: 0x04000CE8 RID: 3304
		private int lenght = 2048;
	}
}
