using System;
using System.Collections;
using Game4.Assets.src.e;
using UnityEngine;

namespace Game4
{
	// Token: 0x0200021B RID: 539
	public class mGraphics
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x00176800 File Offset: 0x00174A00
		private void cache(string key, Texture value)
		{
			if (mGraphics.cachedTextures.Count > 400)
			{
				mGraphics.cachedTextures.Clear();
			}
			if (value.width * value.height < GameCanvas.w * GameCanvas.h)
			{
				mGraphics.cachedTextures.Add(key, value);
			}
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00176850 File Offset: 0x00174A50
		public void translate(int tx, int ty)
		{
			tx *= mGraphics.zoomLevel;
			ty *= mGraphics.zoomLevel;
			this.translateX += tx;
			this.translateY += ty;
			this.isTranslate = true;
			if (this.translateX == 0 && this.translateY == 0)
			{
				this.isTranslate = false;
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x001768A9 File Offset: 0x00174AA9
		public int getTranslateX()
		{
			return this.translateX / mGraphics.zoomLevel;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x001768B7 File Offset: 0x00174AB7
		public int getTranslateY()
		{
			return this.translateY / mGraphics.zoomLevel + mGraphics.addYWhenOpenKeyBoard;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x001768CC File Offset: 0x00174ACC
		public void setClip(int x, int y, int w, int h)
		{
			x *= mGraphics.zoomLevel;
			y *= mGraphics.zoomLevel;
			w *= mGraphics.zoomLevel;
			h *= mGraphics.zoomLevel;
			this.clipTX = this.translateX;
			this.clipTY = this.translateY;
			this.clipX = x;
			this.clipY = y;
			this.clipW = w;
			this.clipH = h;
			this.isClip = true;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0017693A File Offset: 0x00174B3A
		public int getClipX()
		{
			return GameScr.cmx;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00176941 File Offset: 0x00174B41
		public int getClipY()
		{
			return GameScr.cmy;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00176948 File Offset: 0x00174B48
		public int getClipWidth()
		{
			return GameScr.gW;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0017694F File Offset: 0x00174B4F
		public int getClipHeight()
		{
			return GameScr.gH;
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00176958 File Offset: 0x00174B58
		public void fillRect(int x, int y, int w, int h, int color, int alpha)
		{
			float alpha2 = 0.5f;
			this.setColor(color, alpha2);
			this.fillRect(x, y, w, h);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00176980 File Offset: 0x00174B80
		public void drawLine(int x1, int y1, int x2, int y2)
		{
			x1 *= mGraphics.zoomLevel;
			y1 *= mGraphics.zoomLevel;
			x2 *= mGraphics.zoomLevel;
			y2 *= mGraphics.zoomLevel;
			if (y1 == y2)
			{
				if (x1 > x2)
				{
					int num9 = x2;
					x2 = x1;
					x1 = num9;
				}
				this.fillRect(x1, y1, x2 - x1, 1);
				return;
			}
			if (x1 == x2)
			{
				if (y1 > y2)
				{
					int num10 = y2;
					y2 = y1;
					y1 = num10;
				}
				this.fillRect(x1, y1, 1, y2 - y1);
				return;
			}
			if (this.isTranslate)
			{
				x1 += this.translateX;
				y1 += this.translateY;
				x2 += this.translateX;
				y2 += this.translateY;
			}
			string key = "dl" + this.r.ToString() + this.g.ToString() + this.b.ToString();
			Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
			if (texture2D == null)
			{
				texture2D = new Texture2D(1, 1);
				Color color = new Color(this.r, this.g, this.b);
				texture2D.SetPixel(0, 0, color);
				texture2D.Apply();
				this.cache(key, texture2D);
			}
			Vector2 vector = new Vector2((float)x1, (float)y1);
			Vector2 vector2 = new Vector2((float)x2, (float)y2) - vector;
			float num3 = 57.29578f * Mathf.Atan(vector2.y / vector2.x);
			if (vector2.x < 0f)
			{
				num3 += 180f;
			}
			int num4 = (int)Mathf.Ceil(0f);
			GUIUtility.RotateAroundPivot(num3, vector);
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			int num8 = 0;
			if (this.isClip)
			{
				num5 = this.clipX;
				num6 = this.clipY;
				num7 = this.clipW;
				num8 = this.clipH;
				if (this.isTranslate)
				{
					num5 += this.clipTX;
					num6 += this.clipTY;
				}
			}
			if (this.isClip)
			{
				GUI.BeginGroup(new Rect((float)num5, (float)num6, (float)num7, (float)num8));
			}
			Graphics.DrawTexture(new Rect(vector.x - (float)num5, vector.y - (float)num4 - (float)num6, vector2.magnitude, 1f), texture2D);
			if (this.isClip)
			{
				GUI.EndGroup();
			}
			GUIUtility.RotateAroundPivot(0f - num3, vector);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00176BB8 File Offset: 0x00174DB8
		public void drawRect(int x, int y, int w, int h)
		{
			int num = 1;
			this.fillRect(x, y, w, num);
			this.fillRect(x, y, num, h);
			this.fillRect(x + w, y, num, h + 1);
			this.fillRect(x, y + h, w + 1, num);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00176BFC File Offset: 0x00174DFC
		public void fillRect(int x, int y, int w, int h, int border)
		{
			border *= mGraphics.zoomLevel;
			float num = (float)(x * mGraphics.zoomLevel);
			float num2 = (float)(y * mGraphics.zoomLevel);
			w *= mGraphics.zoomLevel;
			h *= mGraphics.zoomLevel;
			if (this.isTranslate)
			{
				num += (float)this.translateX;
				num2 += (float)this.translateY;
			}
			int _ = 1;
			string key = string.Concat(new string[]
			{
				"fr",
				_.ToString(),
				_.ToString(),
				this.r.ToString(),
				this.g.ToString(),
				this.b.ToString(),
				this.a.ToString()
			});
			Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
			if (texture2D == null)
			{
				texture2D = new Texture2D(_, _);
				Color color = new Color(this.r, this.g, this.b, this.a);
				texture2D.SetPixel(0, 0, color);
				Image.setTextureQuality(texture2D);
				texture2D.Apply();
				this.cache(key, texture2D);
			}
			int num3 = 0;
			int num4 = 0;
			if (this.isClip)
			{
				num3 = this.clipX;
				num4 = this.clipY;
				int num5 = this.clipW;
				int num6 = this.clipH;
				if (this.isTranslate)
				{
					num3 += this.clipTX;
					num4 += this.clipTY;
				}
				GUI.BeginGroup(new Rect((float)num3, (float)num4, (float)num5, (float)num6));
			}
			GUI.DrawTexture(new Rect(num - (float)num3, num2 - (float)num4, (float)w, (float)h), texture2D, ScaleMode.StretchToFill, false, 0f, Color.white, new Vector4(this.boderSize, this.boderSize, this.boderSize, this.boderSize), new Vector4((float)border, (float)border, (float)border, (float)border));
			if (this.isClip)
			{
				GUI.EndGroup();
			}
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00176DE0 File Offset: 0x00174FE0
		public void fillRect(int x, int y, int w, int h)
		{
			x *= mGraphics.zoomLevel;
			y *= mGraphics.zoomLevel;
			w *= mGraphics.zoomLevel;
			h *= mGraphics.zoomLevel;
			if (w < 0 || h < 0)
			{
				return;
			}
			if (this.isTranslate)
			{
				x += this.translateX;
				y += this.translateY;
			}
			int num = 1;
			int num2 = 1;
			string key = string.Concat(new string[]
			{
				"fr",
				num.ToString(),
				num2.ToString(),
				this.r.ToString(),
				this.g.ToString(),
				this.b.ToString(),
				this.a.ToString()
			});
			Texture2D texture2D = (Texture2D)mGraphics.cachedTextures[key];
			if (texture2D == null)
			{
				texture2D = new Texture2D(num, num2);
				Color color = new Color(this.r, this.g, this.b, this.a);
				texture2D.SetPixel(0, 0, color);
				texture2D.Apply();
				this.cache(key, texture2D);
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			if (this.isClip)
			{
				num3 = this.clipX;
				num4 = this.clipY;
				num5 = this.clipW;
				num6 = this.clipH;
				if (this.isTranslate)
				{
					num3 += this.clipTX;
					num4 += this.clipTY;
				}
			}
			if (this.isClip)
			{
				GUI.BeginGroup(new Rect((float)num3, (float)num4, (float)num5, (float)num6));
			}
			GUI.DrawTexture(new Rect((float)(x - num3), (float)(y - num4), (float)w, (float)h), texture2D);
			if (this.isClip)
			{
				GUI.EndGroup();
			}
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00176F8C File Offset: 0x0017518C
		public void setColor(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			int num3 = rgb >> 16 & 255;
			this.b = (float)num / 256f;
			this.g = (float)num2 / 256f;
			this.r = (float)num3 / 256f;
			this.a = 255f;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00176FEB File Offset: 0x001751EB
		public void setColor(Color color)
		{
			this.b = color.b;
			this.g = color.g;
			this.r = color.r;
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00177014 File Offset: 0x00175214
		public void drawString(string s, int x, int y, GUIStyle style)
		{
			x *= mGraphics.zoomLevel;
			y *= mGraphics.zoomLevel;
			if (this.isTranslate)
			{
				x += this.translateX;
				y += this.translateY;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if (this.isClip)
			{
				num = this.clipX;
				num2 = this.clipY;
				num3 = this.clipW;
				num4 = this.clipH;
				if (this.isTranslate)
				{
					num += this.clipTX;
					num2 += this.clipTY;
				}
			}
			if (this.isClip)
			{
				GUI.BeginGroup(new Rect((float)num, (float)num2, (float)num3, (float)num4));
			}
			GUI.Label(new Rect((float)(x - num), (float)(y - num2), ScaleGUI.WIDTH, 100f), s, style);
			if (this.isClip)
			{
				GUI.EndGroup();
			}
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x001770DC File Offset: 0x001752DC
		public void setColor(int rgb, float alpha)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			int num3 = rgb >> 16 & 255;
			this.b = (float)num / 256f;
			this.g = (float)num2 / 256f;
			this.r = (float)num3 / 256f;
			this.a = alpha;
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00177138 File Offset: 0x00175338
		private void UpdatePos(int anchor)
		{
			Vector2 vector = new Vector2(0f, 0f);
			if (anchor <= 17)
			{
				if (anchor <= 6)
				{
					if (anchor != 3)
					{
						if (anchor == 6)
						{
							vector = new Vector2(0f, (float)(Screen.height / 2));
						}
					}
					else
					{
						vector = new Vector2(this.size.x / 2f, this.size.y / 2f);
					}
				}
				else if (anchor != 10)
				{
					if (anchor == 17)
					{
						vector = new Vector2((float)(Screen.width / 2), 0f);
					}
				}
				else
				{
					vector = new Vector2((float)Screen.width, (float)(Screen.height / 2));
				}
			}
			else if (anchor <= 24)
			{
				if (anchor != 20)
				{
					if (anchor == 24)
					{
						vector = new Vector2((float)Screen.width, 0f);
					}
				}
				else
				{
					vector = new Vector2(0f, 0f);
				}
			}
			else if (anchor != 33)
			{
				if (anchor != 36)
				{
					if (anchor == 40)
					{
						vector = new Vector2((float)Screen.width, (float)Screen.height);
					}
				}
				else
				{
					vector = new Vector2(0f, (float)Screen.height);
				}
			}
			else
			{
				vector = new Vector2((float)(Screen.width / 2), (float)Screen.height);
			}
			this.pos = vector + this.relativePosition;
			this.rect = new Rect(this.pos.x - this.size.x * 0.5f, this.pos.y - this.size.y * 0.5f, this.size.x, this.size.y);
			this.pivot = new Vector2(this.rect.xMin + this.rect.width * 0.5f, this.rect.yMin + this.rect.height * 0.5f);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00177340 File Offset: 0x00175540
		public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8)
		{
			if (arg0 != null)
			{
				x *= mGraphics.zoomLevel;
				y *= mGraphics.zoomLevel;
				x0 *= mGraphics.zoomLevel;
				y0 *= mGraphics.zoomLevel;
				w0 *= mGraphics.zoomLevel;
				h0 *= mGraphics.zoomLevel;
				this._drawRegion(arg0, (float)x0, (float)y0, w0, h0, arg5, x, y, arg8);
			}
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x001773A4 File Offset: 0x001755A4
		public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, float x, float y, int arg8)
		{
			if (arg0 != null)
			{
				x *= (float)mGraphics.zoomLevel;
				y *= (float)mGraphics.zoomLevel;
				x0 *= mGraphics.zoomLevel;
				y0 *= mGraphics.zoomLevel;
				w0 *= mGraphics.zoomLevel;
				h0 *= mGraphics.zoomLevel;
				this.__drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
			}
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00177408 File Offset: 0x00175608
		public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8, bool isClip)
		{
			this.drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0017742C File Offset: 0x0017562C
		public void __drawRegion(Image image, int x0, int y0, int w, int h, int transform, float x, float y, int anchor)
		{
			if (image == null)
			{
				return;
			}
			if (this.isTranslate)
			{
				x += (float)this.translateX;
				y += (float)this.translateY;
			}
			float num = (float)w;
			float num2 = (float)h;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 1f;
			float num8 = 0f;
			int num9 = 1;
			if ((anchor & mGraphics.HCENTER) == mGraphics.HCENTER)
			{
				num5 -= num / 2f;
			}
			if ((anchor & mGraphics.VCENTER) == mGraphics.VCENTER)
			{
				num6 -= num2 / 2f;
			}
			if ((anchor & mGraphics.RIGHT) == mGraphics.RIGHT)
			{
				num5 -= num;
			}
			if ((anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM)
			{
				num6 -= num2;
			}
			x += num5;
			y += num6;
			int num10 = 0;
			int num11 = 0;
			if (this.isClip)
			{
				num10 = this.clipX;
				int num12 = this.clipY;
				num11 = this.clipW;
				int num13 = this.clipH;
				if (this.isTranslate)
				{
					num10 += this.clipTX;
					num12 += this.clipTY;
				}
				Rect r = new Rect(x, y, (float)w, (float)h);
				Rect r2 = new Rect((float)num10, (float)num12, (float)num11, (float)num13);
				Rect rect = this.intersectRect(r, r2);
				if (rect.width <= 0f || rect.height <= 0f)
				{
					return;
				}
				num = rect.width;
				num2 = rect.height;
				num3 = rect.x - r.x;
				num4 = rect.y - r.y;
			}
			float num14 = 0f;
			float num15 = 0f;
			switch (transform)
			{
			case 1:
				num9 = -1;
				num15 += num2;
				break;
			case 2:
				num14 += num;
				num7 = -1f;
				if (this.isClip)
				{
					if ((float)num10 > x)
					{
						num8 = 0f - num3;
					}
					else if ((float)(num10 + num11) < x + (float)w)
					{
						num8 = 0f - ((float)(num10 + num11) - x - (float)w);
					}
				}
				break;
			case 3:
				num9 = -1;
				num15 += num2;
				num7 = -1f;
				num14 += num;
				break;
			}
			int num16 = 0;
			int num17 = 0;
			if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
			{
				this.matrixBackup = GUI.matrix;
				this.size = new Vector2((float)w, (float)h);
				this.relativePosition = new Vector2(x, y);
				this.UpdatePos(3);
				if (transform != 5)
				{
					if (transform == 6)
					{
						this.UpdatePos(3);
					}
				}
				else
				{
					this.size = new Vector2((float)w, (float)h);
					this.UpdatePos(3);
				}
				switch (transform)
				{
				case 4:
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					num14 += num;
					num7 = -1f;
					if (this.isClip)
					{
						if ((float)num10 > x)
						{
							num8 = 0f - num3;
						}
						else if ((float)(num10 + num11) < x + (float)w)
						{
							num8 = 0f - ((float)(num10 + num11) - x - (float)w);
						}
					}
					break;
				case 5:
					GUIUtility.RotateAroundPivot(90f, this.pivot);
					break;
				case 6:
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					break;
				case 7:
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					num9 = -1;
					num15 += num2;
					break;
				}
			}
			Graphics.DrawTexture(new Rect(x + num3 + num14 + (float)num16, y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), image.texture, new Rect(((float)x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - ((float)y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
			if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
			{
				GUI.matrix = this.matrixBackup;
			}
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00177840 File Offset: 0x00175A40
		public void _drawRegion(Image image, float x0, float y0, int w, int h, int transform, int x, int y, int anchor)
		{
			if (image == null)
			{
				return;
			}
			if (this.isTranslate)
			{
				x += this.translateX;
				y += this.translateY;
			}
			float num = (float)w;
			float num2 = (float)h;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 1f;
			float num8 = 0f;
			int num9 = 1;
			if ((anchor & mGraphics.HCENTER) == mGraphics.HCENTER)
			{
				num5 -= num / 2f;
			}
			if ((anchor & mGraphics.VCENTER) == mGraphics.VCENTER)
			{
				num6 -= num2 / 2f;
			}
			if ((anchor & mGraphics.RIGHT) == mGraphics.RIGHT)
			{
				num5 -= num;
			}
			if ((anchor & mGraphics.BOTTOM) == mGraphics.BOTTOM)
			{
				num6 -= num2;
			}
			x += (int)num5;
			y += (int)num6;
			int num10 = 0;
			int num11 = 0;
			if (this.isClip)
			{
				num10 = this.clipX;
				int num12 = this.clipY;
				num11 = this.clipW;
				int num13 = this.clipH;
				if (this.isTranslate)
				{
					num10 += this.clipTX;
					num12 += this.clipTY;
				}
				Rect r = new Rect((float)x, (float)y, (float)w, (float)h);
				Rect r2 = new Rect((float)num10, (float)num12, (float)num11, (float)num13);
				Rect rect = this.intersectRect(r, r2);
				if (rect.width <= 0f || rect.height <= 0f)
				{
					return;
				}
				num = rect.width;
				num2 = rect.height;
				num3 = rect.x - r.x;
				num4 = rect.y - r.y;
			}
			float num14 = 0f;
			float num15 = 0f;
			switch (transform)
			{
			case 1:
				num9 = -1;
				num15 += num2;
				break;
			case 2:
				num14 += num;
				num7 = -1f;
				if (this.isClip)
				{
					if (num10 > x)
					{
						num8 = 0f - num3;
					}
					else if (num10 + num11 < x + w)
					{
						num8 = (float)(-(float)(num10 + num11 - x - w));
					}
				}
				break;
			case 3:
				num9 = -1;
				num15 += num2;
				num7 = -1f;
				num14 += num;
				break;
			}
			int num16 = 0;
			int num17 = 0;
			if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
			{
				this.matrixBackup = GUI.matrix;
				this.size = new Vector2((float)w, (float)h);
				this.relativePosition = new Vector2((float)x, (float)y);
				this.UpdatePos(3);
				if (transform != 5)
				{
					if (transform == 6)
					{
						this.UpdatePos(3);
					}
				}
				else
				{
					this.size = new Vector2((float)w, (float)h);
					this.UpdatePos(3);
				}
				switch (transform)
				{
				case 4:
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					num14 += num;
					num7 = -1f;
					if (this.isClip)
					{
						if (num10 > x)
						{
							num8 = 0f - num3;
						}
						else if (num10 + num11 < x + w)
						{
							num8 = (float)(-(float)(num10 + num11 - x - w));
						}
					}
					break;
				case 5:
					GUIUtility.RotateAroundPivot(90f, this.pivot);
					break;
				case 6:
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					break;
				case 7:
					GUIUtility.RotateAroundPivot(270f, this.pivot);
					num9 = -1;
					num15 += num2;
					break;
				}
			}
			Graphics.DrawTexture(new Rect((float)x + num3 + num14 + (float)num16, (float)y + num4 + (float)num17 + num15, num * num7, num2 * (float)num9), image.texture, new Rect((x0 + num3 + num8) / (float)image.texture.width, ((float)image.texture.height - num2 - (y0 + num4)) / (float)image.texture.height, num / (float)image.texture.width, num2 / (float)image.texture.height), 0, 0, 0, 0);
			if (transform == 5 || transform == 6 || transform == 4 || transform == 7)
			{
				GUI.matrix = this.matrixBackup;
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00177C40 File Offset: 0x00175E40
		public void drawImage(Image image, int x, int y, int anchor)
		{
			if (image != null)
			{
				this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00177C6C File Offset: 0x00175E6C
		public void drawImageFlipped(Image image, float x, float y)
		{
			x *= (float)mGraphics.zoomLevel;
			y *= (float)mGraphics.zoomLevel;
			if (!mGraphics.isFlipping)
			{
				mGraphics.isFlipping = true;
				this.flipProgress = 0f;
			}
			else if (this.flipProgress < 1f)
			{
				this.flipProgress += Time.deltaTime * this.flipSpeed;
				if (this.flipProgress > 1f)
				{
					this.flipProgress = 1f;
				}
			}
			float width = Mathf.Lerp((float)image.getRealImageWidth(), (float)(-(float)image.getRealImageWidth()), this.flipProgress);
			float xOffset = this.flipProgress * (float)image.getRealImageWidth();
			GUI.DrawTexture(new Rect(x + (float)this.translateX + xOffset, y + (float)this.translateY, width, (float)image.getRealImageHeight()), image.texture);
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00177D3C File Offset: 0x00175F3C
		public void drawImageFog(Image image, int x, int y, int anchor)
		{
			if (image != null)
			{
				this.drawRegion(image, 0, 0, image.texture.width, image.texture.height, 0, x, y, anchor);
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00177D70 File Offset: 0x00175F70
		public void drawImage(Image image, int x, int y)
		{
			if (image != null)
			{
				this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, mGraphics.TOP | mGraphics.LEFT);
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00177DA4 File Offset: 0x00175FA4
		public void drawImage(Image image, float x, float y, int anchor)
		{
			if (image != null)
			{
				this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00177DCE File Offset: 0x00175FCE
		public void reset()
		{
			this.isClip = false;
			this.isTranslate = false;
			this.translateX = 0;
			this.translateY = 0;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00177DEC File Offset: 0x00175FEC
		public Rect intersectRect(Rect r1, Rect r2)
		{
			float num = r1.x;
			float num2 = r1.y;
			float x = r2.x;
			float y = r2.y;
			float num3 = num;
			num3 += r1.width;
			float num4 = num2;
			num4 += r1.height;
			float num5 = x;
			num5 += r2.width;
			float num6 = y;
			num6 += r2.height;
			if (num < x)
			{
				num = x;
			}
			if (num2 < y)
			{
				num2 = y;
			}
			if (num3 > num5)
			{
				num3 = num5;
			}
			if (num4 > num6)
			{
				num4 = num6;
			}
			num3 -= num;
			num4 -= num2;
			if (num3 < -30000f)
			{
				num3 = -30000f;
			}
			if (num4 < -30000f)
			{
				num4 = -30000f;
			}
			return new Rect(num, num2, (float)((int)num3), (float)((int)num4));
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00177EB0 File Offset: 0x001760B0
		public void drawImageScale(Image image, int x, int y, int w, int h, int tranform)
		{
			x *= mGraphics.zoomLevel;
			y *= mGraphics.zoomLevel;
			w *= mGraphics.zoomLevel;
			h *= mGraphics.zoomLevel;
			if (image != null)
			{
				Graphics.DrawTexture(new Rect((float)(x + this.translateX), (float)(y + this.translateY), (float)((tranform != 0) ? (-(float)w) : w), (float)h), image.texture);
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00177F17 File Offset: 0x00176117
		public static int getImageWidth(Image image)
		{
			return image.getWidth();
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00177F1F File Offset: 0x0017611F
		public static int getImageHeight(Image image)
		{
			return image.getHeight();
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00177F27 File Offset: 0x00176127
		public static bool isNotTranColor(Color color)
		{
			return !(color == Color.clear) && !(color == mGraphics.transParentColor);
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00177F48 File Offset: 0x00176148
		public static Image blend(Image img0, float level, int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num12 = (float)(rgb >> 16 & 255);
			float num3 = (float)num / 256f;
			float num4 = (float)num2 / 256f;
			float num5 = num12 / 256f;
			Color color3 = new Color(num5, num4, num3);
			Color[] pixels = img0.texture.GetPixels();
			float num6 = color3.r;
			float num7 = color3.g;
			float num8 = color3.b;
			for (int i = 0; i < pixels.Length; i++)
			{
				Color color2 = pixels[i];
				if (mGraphics.isNotTranColor(color2))
				{
					float num9 = (num6 - color2.r) * level + color2.r;
					float num10 = (num7 - color2.g) * level + color2.g;
					float num11 = (num8 - color2.b) * level + color2.b;
					if (num9 > 255f)
					{
						num9 = 255f;
					}
					if (num9 < 0f)
					{
						num9 = 0f;
					}
					if (num10 > 255f)
					{
						num10 = 255f;
					}
					if (num10 < 0f)
					{
						num10 = 0f;
					}
					if (num11 < 0f)
					{
						num11 = 0f;
					}
					if (num11 > 255f)
					{
						num11 = 255f;
					}
					pixels[i].r = num9;
					pixels[i].g = num10;
					pixels[i].b = num11;
				}
			}
			Image image = Image.createImage(img0.getRealImageWidth(), img0.getRealImageHeight());
			image.texture.SetPixels(pixels);
			Image.setTextureQuality(image.texture);
			image.texture.Apply();
			Cout.LogError2("BLEND ----------------------------------------------------");
			return image;
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x001780FC File Offset: 0x001762FC
		public static Color setColorObj(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float num4 = (float)num / 256f;
			float num5 = (float)num2 / 256f;
			return new Color(num3 / 256f, num5, num4);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00178145 File Offset: 0x00176345
		public void fillTrans(Image imgTrans, int x, int y, int w, int h)
		{
			this.setColor(0, 0.5f);
			this.fillRect(x * mGraphics.zoomLevel, y * mGraphics.zoomLevel, w * mGraphics.zoomLevel, h * mGraphics.zoomLevel);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00178178 File Offset: 0x00176378
		public static int blendColor(float level, int color, int colorBlend)
		{
			Color color3 = mGraphics.setColorObj(colorBlend);
			float num = color3.r * 255f;
			float num2 = color3.g * 255f;
			float num6 = color3.b * 255f;
			Color color2 = mGraphics.setColorObj(color);
			float num3 = (num + color2.r) * level + color2.r;
			float num4 = (num2 + color2.g) * level + color2.g;
			float num5 = (num6 + color2.b) * level + color2.b;
			if (num3 > 255f)
			{
				num3 = 255f;
			}
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			if (num4 > 255f)
			{
				num4 = 255f;
			}
			if (num4 < 0f)
			{
				num4 = 0f;
			}
			if (num5 < 0f)
			{
				num5 = 0f;
			}
			if (num5 > 255f)
			{
				num5 = 255f;
			}
			return (int)num5 & 255 + ((int)num4 << 8) & 255 + ((int)num3 << 16) & 255;
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0017826C File Offset: 0x0017646C
		public static int getIntByColor(Color cl)
		{
			int num4 = (int)(cl.r * 255f);
			float num2 = cl.b * 255f;
			float num3 = cl.g * 255f;
			return (num4 & 255) << 16 | ((int)num3 & 255) << 8 | ((int)num2 & 255);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0016E422 File Offset: 0x0016C622
		public static int getRealImageWidth(Image img)
		{
			return img.w;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0016E42A File Offset: 0x0016C62A
		public static int getRealImageHeight(Image img)
		{
			return img.h;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x001782BD File Offset: 0x001764BD
		public void fillArg(int i, int j, int k, int l, int m, int n)
		{
			this.fillRect(i * mGraphics.zoomLevel, j * mGraphics.zoomLevel, k * mGraphics.zoomLevel, l * mGraphics.zoomLevel);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x001782E2 File Offset: 0x001764E2
		public void CreateLineMaterial()
		{
			if (!this.lineMaterial)
			{
				this.lineMaterial = new Material(Shader.Find("Lines/Colored Blended"))
				{
					hideFlags = HideFlags.HideAndDontSave
				};
				this.lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
			}
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00002378 File Offset: 0x00000578
		internal void drawRegion(Small img, int p1, int p2, int p3, int p4, int transform, int x, int y, int anchor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002E60 RID: 11872
		public static int HCENTER = 1;

		// Token: 0x04002E61 RID: 11873
		public static int VCENTER = 2;

		// Token: 0x04002E62 RID: 11874
		public static int LEFT = 4;

		// Token: 0x04002E63 RID: 11875
		public static int RIGHT = 8;

		// Token: 0x04002E64 RID: 11876
		public static int TOP = 16;

		// Token: 0x04002E65 RID: 11877
		public static int BOTTOM = 32;

		// Token: 0x04002E66 RID: 11878
		private float r;

		// Token: 0x04002E67 RID: 11879
		private float g;

		// Token: 0x04002E68 RID: 11880
		private float b;

		// Token: 0x04002E69 RID: 11881
		private float a;

		// Token: 0x04002E6A RID: 11882
		public int clipX;

		// Token: 0x04002E6B RID: 11883
		public int clipY;

		// Token: 0x04002E6C RID: 11884
		public int clipW;

		// Token: 0x04002E6D RID: 11885
		public int clipH;

		// Token: 0x04002E6E RID: 11886
		private bool isClip;

		// Token: 0x04002E6F RID: 11887
		private bool isTranslate = true;

		// Token: 0x04002E70 RID: 11888
		private int translateX;

		// Token: 0x04002E71 RID: 11889
		private int translateY;

		// Token: 0x04002E72 RID: 11890
		private float translateXf;

		// Token: 0x04002E73 RID: 11891
		private float translateYf;

		// Token: 0x04002E74 RID: 11892
		public static int zoomLevel = 1;

		// Token: 0x04002E75 RID: 11893
		public const int BASELINE = 64;

		// Token: 0x04002E76 RID: 11894
		public const int SOLID = 0;

		// Token: 0x04002E77 RID: 11895
		public const int DOTTED = 1;

		// Token: 0x04002E78 RID: 11896
		public const int TRANS_MIRROR = 2;

		// Token: 0x04002E79 RID: 11897
		public const int TRANS_MIRROR_ROT180 = 1;

		// Token: 0x04002E7A RID: 11898
		public const int TRANS_MIRROR_ROT270 = 4;

		// Token: 0x04002E7B RID: 11899
		public const int TRANS_MIRROR_ROT90 = 7;

		// Token: 0x04002E7C RID: 11900
		public const int TRANS_NONE = 0;

		// Token: 0x04002E7D RID: 11901
		public const int TRANS_ROT180 = 3;

		// Token: 0x04002E7E RID: 11902
		public const int TRANS_ROT270 = 6;

		// Token: 0x04002E7F RID: 11903
		public const int TRANS_ROT90 = 5;

		// Token: 0x04002E80 RID: 11904
		public static Hashtable cachedTextures = new Hashtable();

		// Token: 0x04002E81 RID: 11905
		public static int addYWhenOpenKeyBoard;

		// Token: 0x04002E82 RID: 11906
		private int clipTX;

		// Token: 0x04002E83 RID: 11907
		private int clipTY;

		// Token: 0x04002E84 RID: 11908
		private int currentBGColor;

		// Token: 0x04002E85 RID: 11909
		private Vector2 pos = new Vector2(0f, 0f);

		// Token: 0x04002E86 RID: 11910
		private Rect rect;

		// Token: 0x04002E87 RID: 11911
		private Matrix4x4 matrixBackup;

		// Token: 0x04002E88 RID: 11912
		private Vector2 pivot;

		// Token: 0x04002E89 RID: 11913
		public Vector2 size = new Vector2(128f, 128f);

		// Token: 0x04002E8A RID: 11914
		public Vector2 relativePosition = new Vector2(0f, 0f);

		// Token: 0x04002E8B RID: 11915
		public Color clTrans;

		// Token: 0x04002E8C RID: 11916
		public static Color transParentColor = new Color(1f, 1f, 1f, 0f);

		// Token: 0x04002E8D RID: 11917
		private Material lineMaterial;

		// Token: 0x04002E8E RID: 11918
		public float boderSize;

		// Token: 0x04002E8F RID: 11919
		private float flipProgress;

		// Token: 0x04002E90 RID: 11920
		public static bool isFlipping = false;

		// Token: 0x04002E91 RID: 11921
		private float flipSpeed = 5f;
	}
}
