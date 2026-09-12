using System;
using System.Text;

namespace Game1
{
	// Token: 0x020004BA RID: 1210
	public class myWriter
	{
		// Token: 0x060035B5 RID: 13749 RVA: 0x00344C1B File Offset: 0x00342E1B
		public myWriter()
		{
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x00344C3E File Offset: 0x00342E3E
		public myWriter(int len)
		{
			this.buffer = new sbyte[len];
			this.lenght = len;
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x00344C74 File Offset: 0x00342E74
		public void writeSByte(sbyte value)
		{
			this.checkLenght(0);
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x00344CA4 File Offset: 0x00342EA4
		public void writeSByteUncheck(sbyte value)
		{
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x00344CCA File Offset: 0x00342ECA
		public void writeByte(sbyte value)
		{
			this.writeSByte(value);
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x00344CD3 File Offset: 0x00342ED3
		public void writeByte(int value)
		{
			this.writeSByte((sbyte)value);
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x00344CDD File Offset: 0x00342EDD
		public void writeChar(char value)
		{
			this.writeSByte(0);
			this.writeSByte((sbyte)value);
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x00344CF0 File Offset: 0x00342EF0
		public void writeSByte(sbyte[] value)
		{
			this.checkLenght(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				this.writeSByteUncheck(value[i]);
			}
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x00344D20 File Offset: 0x00342F20
		public void writeShort(short value)
		{
			this.checkLenght(2);
			for (int num = 1; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x00344D50 File Offset: 0x00342F50
		public void writeShort(int value)
		{
			this.checkLenght(2);
			short num = (short)value;
			for (int num2 = 1; num2 >= 0; num2--)
			{
				this.writeSByteUncheck((sbyte)(num >> num2 * 8));
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x00344D84 File Offset: 0x00342F84
		public void writeInt(int value)
		{
			this.checkLenght(4);
			for (int num = 3; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x00344DB3 File Offset: 0x00342FB3
		public void writeBoolean(bool value)
		{
			writeSByte((sbyte)(value ? 1 : 0));
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x00344DC4 File Offset: 0x00342FC4
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

		// Token: 0x060035C2 RID: 13762 RVA: 0x00344E20 File Offset: 0x00343020
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

		// Token: 0x060035C3 RID: 13763 RVA: 0x00344E5A File Offset: 0x0034305A
		public void write(sbyte[] value)
		{
			this.writeSByte(value);
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x00344E64 File Offset: 0x00343064
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

		// Token: 0x060035C5 RID: 13765 RVA: 0x00344EA8 File Offset: 0x003430A8
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

		// Token: 0x060035C6 RID: 13766 RVA: 0x00344F17 File Offset: 0x00343117
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x04006961 RID: 26977
		public sbyte[] buffer = new sbyte[2048];

		// Token: 0x04006962 RID: 26978
		private int posWrite;

		// Token: 0x04006963 RID: 26979
		private int lenght = 2048;
	}
}
