using System;

namespace Game1
{
	// Token: 0x02000469 RID: 1129
	public class EffectData
	{
		// Token: 0x0600322E RID: 12846 RVA: 0x003146C0 File Offset: 0x003128C0
		public ImageInfo getImageInfo(sbyte id)
		{
			for (int i = 0; i < this.imgInfo.Length; i++)
			{
				if (this.imgInfo[i].ID == (int)id)
				{
					return this.imgInfo[i];
				}
			}
			return null;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x003146FA File Offset: 0x003128FA
		public short[] get()
		{
			return this.arrFrame;
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00314702 File Offset: 0x00312902
		public short[] get(int index)
		{
			if (index >= this.anim_data.Length)
			{
				index = 0;
			}
			if (this.anim_data[index] == null)
			{
				return new short[1];
			}
			return this.anim_data[index];
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x0031472C File Offset: 0x0031292C
		public void readData(string patch)
		{
			DataInputStream dataInputStream = null;
			try
			{
				dataInputStream = MyStream.readFile(patch);
			}
			catch (Exception)
			{
				return;
			}
			this.readData(dataInputStream.r);
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x00314764 File Offset: 0x00312964
		public void readData2(string patch)
		{
			DataInputStream dataInputStream = null;
			try
			{
				dataInputStream = MyStream.readFile(patch);
			}
			catch (Exception)
			{
				return;
			}
			this.readEffect(dataInputStream.r);
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x0031479C File Offset: 0x0031299C
		public void readEffect(myReader msg)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			try
			{
				sbyte b = msg.readByte();
				Res.outz("size IMG==========" + b.ToString());
				this.imgInfo = new ImageInfo[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					this.imgInfo[i] = new ImageInfo();
					this.imgInfo[i].ID = (int)msg.readByte();
					this.imgInfo[i].x0 = (int)msg.readUnsignedByte();
					this.imgInfo[i].y0 = (int)msg.readUnsignedByte();
					this.imgInfo[i].w = (int)msg.readUnsignedByte();
					this.imgInfo[i].h = (int)msg.readUnsignedByte();
				}
				short num5 = msg.readShort();
				this.frame = new Frame[(int)num5];
				for (int j = 0; j < this.frame.Length; j++)
				{
					this.frame[j] = new Frame();
					sbyte b2 = msg.readByte();
					this.frame[j].dx = new short[(int)b2];
					this.frame[j].dy = new short[(int)b2];
					this.frame[j].idImg = new sbyte[(int)b2];
					for (int k = 0; k < (int)b2; k++)
					{
						this.frame[j].dx[k] = msg.readShort();
						this.frame[j].dy[k] = msg.readShort();
						this.frame[j].idImg[k] = msg.readByte();
						if (j == 0)
						{
							if (num > (int)this.frame[j].dx[k])
							{
								num = (int)this.frame[j].dx[k];
							}
							if (num2 > (int)this.frame[j].dy[k])
							{
								num2 = (int)this.frame[j].dy[k];
							}
							if (num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w)
							{
								num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
							}
							if (num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h)
							{
								num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
							}
							this.width = num3 - num;
							this.height = num4 - num2;
						}
					}
				}
				this.arrFrame = new short[(int)msg.readShort()];
				for (int l = 0; l < this.arrFrame.Length; l++)
				{
					this.arrFrame[l] = msg.readShort();
				}
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
				Res.outz("1");
			}
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x00314AD8 File Offset: 0x00312CD8
		public void readData(myReader iss)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			try
			{
				sbyte b = iss.readByte();
				this.imgInfo = new ImageInfo[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					this.imgInfo[i] = new ImageInfo();
					this.imgInfo[i].ID = (int)iss.readByte();
					this.imgInfo[i].x0 = (int)iss.readUnsignedByte();
					this.imgInfo[i].y0 = (int)iss.readUnsignedByte();
					this.imgInfo[i].w = (int)iss.readUnsignedByte();
					this.imgInfo[i].h = (int)iss.readUnsignedByte();
				}
				short num5 = iss.readShort();
				this.frame = new Frame[(int)num5];
				for (int j = 0; j < (int)num5; j++)
				{
					this.frame[j] = new Frame();
					sbyte b2 = iss.readByte();
					this.frame[j].dx = new short[(int)b2];
					this.frame[j].dy = new short[(int)b2];
					this.frame[j].idImg = new sbyte[(int)b2];
					for (int k = 0; k < (int)b2; k++)
					{
						this.frame[j].dx[k] = iss.readShort();
						this.frame[j].dy[k] = iss.readShort();
						this.frame[j].idImg[k] = iss.readByte();
						if (j == 0)
						{
							if (num > (int)this.frame[j].dx[k])
							{
								num = (int)this.frame[j].dx[k];
							}
							if (num2 > (int)this.frame[j].dy[k])
							{
								num2 = (int)this.frame[j].dy[k];
							}
							if (num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w)
							{
								num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
							}
							if (num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h)
							{
								num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
							}
							this.width = num3 - num;
							this.height = num4 - num2;
						}
					}
				}
				short num6 = iss.readShort();
				this.arrFrame = new short[(int)num6];
				if (this.ID >= 201)
				{
					short num7 = 0;
					short[] array = new short[(int)num6];
					int num8 = 0;
					string text = string.Empty;
					bool flag = false;
					for (int l = 0; l < (int)num6; l++)
					{
						short num9 = iss.readShort();
						text = text + num9.ToString() + ",";
						this.arrFrame[l] = num9;
						if (num9 + 500 >= 500)
						{
							array[num8++] = num9;
							flag = true;
						}
						else
						{
							num7 = (short)Res.abs((int)(num9 + 500));
							this.anim_data[(int)num7] = new short[num8];
							Array.Copy(array, 0, this.anim_data[(int)num7], 0, num8);
							num8 = 0;
						}
					}
					if (!flag)
					{
						this.anim_data[0] = new short[num8];
						Array.Copy(array, 0, this.anim_data[(int)num7], 0, num8);
					}
					else
					{
						for (int m = 0; m < 16; m++)
						{
							if (this.anim_data[m] == null)
							{
								this.anim_data[m] = this.anim_data[2];
							}
						}
					}
				}
				else
				{
					for (int n = 0; n < (int)num6; n++)
					{
						this.arrFrame[n] = iss.readShort();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x00314EFC File Offset: 0x003130FC
		public void readData(sbyte[] data)
		{
			myReader iss = new myReader(data);
			this.readData(iss);
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x00314F18 File Offset: 0x00313118
		public void readDataNewBoss(sbyte[] data, sbyte typeread)
		{
			myReader msg = new myReader(data);
			this.readMobNew(msg, typeread);
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x00314F34 File Offset: 0x00313134
		public void paintFrame(mGraphics g, int f, int x, int y, int trans, int layer)
		{
			if (this.frame == null || this.frame.Length == 0)
			{
				return;
			}
			Frame frame = this.frame[f];
			for (int i = 0; i < frame.dx.Length; i++)
			{
				ImageInfo imageInfo = this.getImageInfo(frame.idImg[i]);
				try
				{
					switch (trans)
					{
					case -1:
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 0, x + (int)frame.dx[i], y + (int)frame.dy[i], 0);
						break;
					case 0:
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 0, x + (int)frame.dx[i], y + (int)frame.dy[i] - ((layer < 4 && layer > 0) ? GameCanvas.transY : 0), 0);
						break;
					case 1:
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 2, x - (int)frame.dx[i], y + (int)frame.dy[i] - ((layer < 4 && layer > 0) ? GameCanvas.transY : 0), StaticObj.TOP_RIGHT);
						break;
					case 2:
						g.drawRegion(this.img, imageInfo.x0, imageInfo.y0, imageInfo.w, imageInfo.h, 7, x - (int)frame.dx[i], y + (int)frame.dy[i] - ((layer < 4 && layer > 0) ? GameCanvas.transY : 0), StaticObj.VCENTER_HCENTER);
						break;
					}
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x003150FC File Offset: 0x003132FC
		public void readMobNew(myReader msg, sbyte typeread)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			try
			{
				sbyte b = msg.readByte();
				this.imgInfo = new ImageInfo[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					this.imgInfo[i] = new ImageInfo();
					this.imgInfo[i].ID = (int)msg.readByte();
					if (typeread == 1)
					{
						this.imgInfo[i].x0 = (int)msg.readUnsignedByte();
						this.imgInfo[i].y0 = (int)msg.readUnsignedByte();
					}
					else
					{
						this.imgInfo[i].x0 = (int)msg.readShort();
						this.imgInfo[i].y0 = (int)msg.readShort();
					}
					this.imgInfo[i].w = (int)msg.readUnsignedByte();
					this.imgInfo[i].h = (int)msg.readUnsignedByte();
				}
				short num5 = msg.readShort();
				this.frame = new Frame[(int)num5];
				for (int j = 0; j < this.frame.Length; j++)
				{
					this.frame[j] = new Frame();
					sbyte b2 = msg.readByte();
					this.frame[j].dx = new short[(int)b2];
					this.frame[j].dy = new short[(int)b2];
					this.frame[j].idImg = new sbyte[(int)b2];
					for (int k = 0; k < (int)b2; k++)
					{
						this.frame[j].dx[k] = msg.readShort();
						this.frame[j].dy[k] = msg.readShort();
						this.frame[j].idImg[k] = msg.readByte();
						if (j == 0)
						{
							if (num > (int)this.frame[j].dx[k])
							{
								num = (int)this.frame[j].dx[k];
							}
							if (num2 > (int)this.frame[j].dy[k])
							{
								num2 = (int)this.frame[j].dy[k];
							}
							if (num3 < (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w)
							{
								num3 = (int)this.frame[j].dx[k] + this.imgInfo[(int)this.frame[j].idImg[k]].w;
							}
							if (num4 < (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h)
							{
								num4 = (int)this.frame[j].dy[k] + this.imgInfo[(int)this.frame[j].idImg[k]].h;
							}
							this.width = num3 - num;
							this.height = num4 - num2;
						}
					}
				}
				this.arrFrame = new short[(int)msg.readShort()];
				for (int l = 0; l < this.arrFrame.Length; l++)
				{
					this.arrFrame[l] = msg.readShort();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04006068 RID: 24680
		public Image img;

		// Token: 0x04006069 RID: 24681
		public ImageInfo[] imgInfo;

		// Token: 0x0400606A RID: 24682
		public Frame[] frame;

		// Token: 0x0400606B RID: 24683
		public short[] arrFrame;

		// Token: 0x0400606C RID: 24684
		public short[][] anim_data = new short[16][];

		// Token: 0x0400606D RID: 24685
		public int ID;

		// Token: 0x0400606E RID: 24686
		public int typeData;

		// Token: 0x0400606F RID: 24687
		public int width;

		// Token: 0x04006070 RID: 24688
		public int height;
	}
}
