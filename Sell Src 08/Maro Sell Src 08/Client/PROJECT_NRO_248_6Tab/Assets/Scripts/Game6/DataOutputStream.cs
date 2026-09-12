using System;

namespace Game6
{
	// Token: 0x0200002A RID: 42
	public class DataOutputStream
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x0002A01F File Offset: 0x0002821F
		public DataOutputStream()
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0002A032 File Offset: 0x00028232
		public DataOutputStream(int len)
		{
			this.w = new myWriter(len);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0002A051 File Offset: 0x00028251
		public void writeShort(short i)
		{
			this.w.writeShort(i);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0002A05F File Offset: 0x0002825F
		public sbyte[] toByteArray()
		{
			return this.w.getData();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0002A06C File Offset: 0x0002826C
		public void close()
		{
			this.w.Close();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0002A079 File Offset: 0x00028279
		public void writeByte(sbyte b)
		{
			this.w.writeByte(b);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0002A087 File Offset: 0x00028287
		public void writeUTF(string name)
		{
			this.w.writeUTF(name);
		}

		// Token: 0x04000398 RID: 920
		private myWriter w = new myWriter();
	}
}
