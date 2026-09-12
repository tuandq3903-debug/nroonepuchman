using System;
using System.Text;

namespace Game3
{
	// Token: 0x0200030A RID: 778
	public class myWriter
	{
		// Token: 0x0600226D RID: 8813 RVA: 0x0021AAD3 File Offset: 0x00218CD3
		public myWriter()
		{
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0021AAF6 File Offset: 0x00218CF6
		public myWriter(int len)
		{
			this.buffer = new sbyte[len];
			this.lenght = len;
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0021AB2C File Offset: 0x00218D2C
		public void writeSByte(sbyte value)
		{
			this.checkLenght(0);
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0021AB5C File Offset: 0x00218D5C
		public void writeSByteUncheck(sbyte value)
		{
			sbyte[] array = this.buffer;
			int num = this.posWrite;
			this.posWrite = num + 1;
			array[num] = value;
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x0021AB82 File Offset: 0x00218D82
		public void writeByte(sbyte value)
		{
			this.writeSByte(value);
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x0021AB8B File Offset: 0x00218D8B
		public void writeByte(int value)
		{
			this.writeSByte((sbyte)value);
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x0021AB95 File Offset: 0x00218D95
		public void writeChar(char value)
		{
			this.writeSByte(0);
			this.writeSByte((sbyte)value);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0021ABA8 File Offset: 0x00218DA8
		public void writeSByte(sbyte[] value)
		{
			this.checkLenght(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				this.writeSByteUncheck(value[i]);
			}
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x0021ABD8 File Offset: 0x00218DD8
		public void writeShort(short value)
		{
			this.checkLenght(2);
			for (int num = 1; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0021AC08 File Offset: 0x00218E08
		public void writeShort(int value)
		{
			this.checkLenght(2);
			short num = (short)value;
			for (int num2 = 1; num2 >= 0; num2--)
			{
				this.writeSByteUncheck((sbyte)(num >> num2 * 8));
			}
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0021AC3C File Offset: 0x00218E3C
		public void writeInt(int value)
		{
			this.checkLenght(4);
			for (int num = 3; num >= 0; num--)
			{
				this.writeSByteUncheck((sbyte)(value >> num * 8));
			}
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x0021AC6B File Offset: 0x00218E6B
		public void writeBoolean(bool value)
		{
			writeSByte((sbyte)(value ? 1 : 0));
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x0021AC7C File Offset: 0x00218E7C
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

		// Token: 0x0600227A RID: 8826 RVA: 0x0021ACD8 File Offset: 0x00218ED8
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

		// Token: 0x0600227B RID: 8827 RVA: 0x0021AD12 File Offset: 0x00218F12
		public void write(sbyte[] value)
		{
			this.writeSByte(value);
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x0021AD1C File Offset: 0x00218F1C
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

		// Token: 0x0600227D RID: 8829 RVA: 0x0021AD60 File Offset: 0x00218F60
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

		// Token: 0x0600227E RID: 8830 RVA: 0x0021ADCF File Offset: 0x00218FCF
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x04004463 RID: 17507
		public sbyte[] buffer = new sbyte[2048];

		// Token: 0x04004464 RID: 17508
		private int posWrite;

		// Token: 0x04004465 RID: 17509
		private int lenght = 2048;
	}
}
