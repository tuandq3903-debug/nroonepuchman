using System;
using System.Text;

namespace Game5
{
	// Token: 0x02000157 RID: 343
	public class myReader
	{
		// Token: 0x06000F02 RID: 3842 RVA: 0x000F056B File Offset: 0x000EE76B
		public myReader(sbyte[] data)
		{
			this.buffer = data;
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000F057C File Offset: 0x000EE77C
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

		// Token: 0x06000F04 RID: 3844 RVA: 0x000F05CA File Offset: 0x000EE7CA
		public sbyte readByte()
		{
			return this.readSByte();
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x000F05D2 File Offset: 0x000EE7D2
		public void mark(int readlimit)
		{
			this.posMark = this.posRead;
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x000F05E0 File Offset: 0x000EE7E0
		public void reset()
		{
			this.posRead = this.posMark;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x000F05EE File Offset: 0x000EE7EE
		public byte readUnsignedByte()
		{
			return myReader.convertSbyteToByte(this.readSByte());
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000F05FC File Offset: 0x000EE7FC
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

		// Token: 0x06000F09 RID: 3849 RVA: 0x000F0640 File Offset: 0x000EE840
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

		// Token: 0x06000F0A RID: 3850 RVA: 0x000F0684 File Offset: 0x000EE884
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

		// Token: 0x06000F0B RID: 3851 RVA: 0x000F06C8 File Offset: 0x000EE8C8
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

		// Token: 0x06000F0C RID: 3852 RVA: 0x000F070B File Offset: 0x000EE90B
		public bool readBool()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000F070B File Offset: 0x000EE90B
		public bool readBoolean()
		{
			return this.readSByte() > 0;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x000F0718 File Offset: 0x000EE918
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

		// Token: 0x06000F0F RID: 3855 RVA: 0x000F0758 File Offset: 0x000EE958
		public string readUTF()
		{
			return this.readStringUTF();
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000F0760 File Offset: 0x000EE960
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

		// Token: 0x06000F11 RID: 3857 RVA: 0x000F07A8 File Offset: 0x000EE9A8
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

		// Token: 0x06000F12 RID: 3858 RVA: 0x000F07E7 File Offset: 0x000EE9E7
		public int available()
		{
			return this.buffer.Length - this.posRead;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00043E19 File Offset: 0x00042019
		public static byte convertSbyteToByte(sbyte var)
		{
			if (var > 0)
			{
				return (byte)var;
			}
			return (byte)((int)var + 256);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000F07F8 File Offset: 0x000EE9F8
		public void Close()
		{
			this.buffer = null;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000F0804 File Offset: 0x000EEA04
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

		// Token: 0x04001F61 RID: 8033
		public sbyte[] buffer;

		// Token: 0x04001F62 RID: 8034
		private int posRead;

		// Token: 0x04001F63 RID: 8035
		private int posMark;
	}
}
