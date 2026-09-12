using System;

namespace Game4
{
	// Token: 0x020001E1 RID: 481
	public class EffectData
	{
		// Token: 0x06001542 RID: 5442 RVA: 0x001554D4 File Offset: 0x001536D4
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

		// Token: 0x06001543 RID: 5443 RVA: 0x0015550E File Offset: 0x0015370E
		public short[] get()
		{
			return this.arrFrame;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00155516 File Offset: 0x00153716
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

		// Token: 0x06001545 RID: 5445 RVA: 0x00155540 File Offset: 0x00153740
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

		// Token: 0x06001546 RID: 5446 RVA: 0x00155578 File Offset: 0x00153778
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

		// Token: 0x06001547 RID: 5447 RVA: 0x001555B0 File Offset: 0x001537B0
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

		// Token: 0x06001548 RID: 5448 RVA: 0x001558EC File Offset: 0x00153AEC
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

		// Token: 0x06001549 RID: 5449 RVA: 0x00155D10 File Offset: 0x00153F10
		public void readData(sbyte[] data)
		{
			myReader iss = new myReader(data);
			this.readData(iss);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00155D2C File Offset: 0x00153F2C
		public void readDataNewBoss(sbyte[] data, sbyte typeread)
		{
			myReader msg = new myReader(data);
			this.readMobNew(msg, typeread);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00155D48 File Offset: 0x00153F48
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

		// Token: 0x0600154C RID: 5452 RVA: 0x00155F10 File Offset: 0x00154110
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

		// Token: 0x040028EB RID: 10475
		public Image img;

		// Token: 0x040028EC RID: 10476
		public ImageInfo[] imgInfo;

		// Token: 0x040028ED RID: 10477
		public Frame[] frame;

		// Token: 0x040028EE RID: 10478
		public short[] arrFrame;

		// Token: 0x040028EF RID: 10479
		public short[][] anim_data = new short[16][];

		// Token: 0x040028F0 RID: 10480
		public int ID;

		// Token: 0x040028F1 RID: 10481
		public int typeData;

		// Token: 0x040028F2 RID: 10482
		public int width;

		// Token: 0x040028F3 RID: 10483
		public int height;
	}
}
