using System;
using System.Collections;
using Game1.Assets.src.e;
using UnityEngine;

namespace Game1
{
	// Token: 0x020004A3 RID: 1187
	public class mGraphics
	{
		// Token: 0x06003473 RID: 13427 RVA: 0x003359EC File Offset: 0x00333BEC
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

		// Token: 0x06003474 RID: 13428 RVA: 0x00335A3C File Offset: 0x00333C3C
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

		// Token: 0x06003475 RID: 13429 RVA: 0x00335A95 File Offset: 0x00333C95
		public int getTranslateX()
		{
			return this.translateX / mGraphics.zoomLevel;
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x00335AA3 File Offset: 0x00333CA3
		public int getTranslateY()
		{
			return this.translateY / mGraphics.zoomLevel + mGraphics.addYWhenOpenKeyBoard;
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x00335AB8 File Offset: 0x00333CB8
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

		// Token: 0x06003478 RID: 13432 RVA: 0x00335B26 File Offset: 0x00333D26
		public int getClipX()
		{
			return GameScr.cmx;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x00335B2D File Offset: 0x00333D2D
		public int getClipY()
		{
			return GameScr.cmy;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x00335B34 File Offset: 0x00333D34
		public int getClipWidth()
		{
			return GameScr.gW;
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x00335B3B File Offset: 0x00333D3B
		public int getClipHeight()
		{
			return GameScr.gH;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x00335B44 File Offset: 0x00333D44
		public void fillRect(int x, int y, int w, int h, int color, int alpha)
		{
			float alpha2 = 0.5f;
			this.setColor(color, alpha2);
			this.fillRect(x, y, w, h);
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x00335B6C File Offset: 0x00333D6C
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

		// Token: 0x0600347E RID: 13438 RVA: 0x00335DA4 File Offset: 0x00333FA4
		public void drawRect(int x, int y, int w, int h)
		{
			int num = 1;
			this.fillRect(x, y, w, num);
			this.fillRect(x, y, num, h);
			this.fillRect(x + w, y, num, h + 1);
			this.fillRect(x, y + h, w + 1, num);
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x00335DE8 File Offset: 0x00333FE8
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

		// Token: 0x06003480 RID: 13440 RVA: 0x00335FCC File Offset: 0x003341CC
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

		// Token: 0x06003481 RID: 13441 RVA: 0x00336178 File Offset: 0x00334378
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

		// Token: 0x06003482 RID: 13442 RVA: 0x003361D7 File Offset: 0x003343D7
		public void setColor(Color color)
		{
			this.b = color.b;
			this.g = color.g;
			this.r = color.r;
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x00336200 File Offset: 0x00334400
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

		// Token: 0x06003484 RID: 13444 RVA: 0x003362C8 File Offset: 0x003344C8
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

		// Token: 0x06003485 RID: 13445 RVA: 0x00336324 File Offset: 0x00334524
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

		// Token: 0x06003486 RID: 13446 RVA: 0x0033652C File Offset: 0x0033472C
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

		// Token: 0x06003487 RID: 13447 RVA: 0x00336590 File Offset: 0x00334790
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

		// Token: 0x06003488 RID: 13448 RVA: 0x003365F4 File Offset: 0x003347F4
		public void drawRegion(Image arg0, int x0, int y0, int w0, int h0, int arg5, int x, int y, int arg8, bool isClip)
		{
			this.drawRegion(arg0, x0, y0, w0, h0, arg5, x, y, arg8);
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x00336618 File Offset: 0x00334818
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

		// Token: 0x0600348A RID: 13450 RVA: 0x00336A2C File Offset: 0x00334C2C
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

		// Token: 0x0600348B RID: 13451 RVA: 0x00336E2C File Offset: 0x0033502C
		public void drawImage(Image image, int x, int y, int anchor)
		{
			if (image != null)
			{
				this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
			}
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x00336E58 File Offset: 0x00335058
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

		// Token: 0x0600348D RID: 13453 RVA: 0x00336F28 File Offset: 0x00335128
		public void drawImageFog(Image image, int x, int y, int anchor)
		{
			if (image != null)
			{
				this.drawRegion(image, 0, 0, image.texture.width, image.texture.height, 0, x, y, anchor);
			}
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x00336F5C File Offset: 0x0033515C
		public void drawImage(Image image, int x, int y)
		{
			if (image != null)
			{
				this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, mGraphics.TOP | mGraphics.LEFT);
			}
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x00336F90 File Offset: 0x00335190
		public void drawImage(Image image, float x, float y, int anchor)
		{
			if (image != null)
			{
				this.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), 0, x, y, anchor);
			}
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x00336FBA File Offset: 0x003351BA
		public void reset()
		{
			this.isClip = false;
			this.isTranslate = false;
			this.translateX = 0;
			this.translateY = 0;
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x00336FD8 File Offset: 0x003351D8
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

		// Token: 0x06003492 RID: 13458 RVA: 0x0033709C File Offset: 0x0033529C
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

		// Token: 0x06003493 RID: 13459 RVA: 0x00337103 File Offset: 0x00335303
		public static int getImageWidth(Image image)
		{
			return image.getWidth();
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x0033710B File Offset: 0x0033530B
		public static int getImageHeight(Image image)
		{
			return image.getHeight();
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x00337113 File Offset: 0x00335313
		public static bool isNotTranColor(Color color)
		{
			return !(color == Color.clear) && !(color == mGraphics.transParentColor);
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x00337134 File Offset: 0x00335334
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

		// Token: 0x06003497 RID: 13463 RVA: 0x003372E8 File Offset: 0x003354E8
		public static Color setColorObj(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float num4 = (float)num / 256f;
			float num5 = (float)num2 / 256f;
			return new Color(num3 / 256f, num5, num4);
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x00337331 File Offset: 0x00335531
		public void fillTrans(Image imgTrans, int x, int y, int w, int h)
		{
			this.setColor(0, 0.5f);
			this.fillRect(x * mGraphics.zoomLevel, y * mGraphics.zoomLevel, w * mGraphics.zoomLevel, h * mGraphics.zoomLevel);
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x00337364 File Offset: 0x00335564
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

		// Token: 0x0600349A RID: 13466 RVA: 0x00337458 File Offset: 0x00335658
		public static int getIntByColor(Color cl)
		{
			int num4 = (int)(cl.r * 255f);
			float num2 = cl.b * 255f;
			float num3 = cl.g * 255f;
			return (num4 & 255) << 16 | ((int)num3 & 255) << 8 | ((int)num2 & 255);
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x0032D60E File Offset: 0x0032B80E
		public static int getRealImageWidth(Image img)
		{
			return img.w;
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x0032D616 File Offset: 0x0032B816
		public static int getRealImageHeight(Image img)
		{
			return img.h;
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x003374A9 File Offset: 0x003356A9
		public void fillArg(int i, int j, int k, int l, int m, int n)
		{
			this.fillRect(i * mGraphics.zoomLevel, j * mGraphics.zoomLevel, k * mGraphics.zoomLevel, l * mGraphics.zoomLevel);
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x003374CE File Offset: 0x003356CE
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

		// Token: 0x0600349F RID: 13471 RVA: 0x00002378 File Offset: 0x00000578
		internal void drawRegion(Small img, int p1, int p2, int p3, int p4, int transform, int x, int y, int anchor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040065DD RID: 26077
		public static int HCENTER = 1;

		// Token: 0x040065DE RID: 26078
		public static int VCENTER = 2;

		// Token: 0x040065DF RID: 26079
		public static int LEFT = 4;

		// Token: 0x040065E0 RID: 26080
		public static int RIGHT = 8;

		// Token: 0x040065E1 RID: 26081
		public static int TOP = 16;

		// Token: 0x040065E2 RID: 26082
		public static int BOTTOM = 32;

		// Token: 0x040065E3 RID: 26083
		private float r;

		// Token: 0x040065E4 RID: 26084
		private float g;

		// Token: 0x040065E5 RID: 26085
		private float b;

		// Token: 0x040065E6 RID: 26086
		private float a;

		// Token: 0x040065E7 RID: 26087
		public int clipX;

		// Token: 0x040065E8 RID: 26088
		public int clipY;

		// Token: 0x040065E9 RID: 26089
		public int clipW;

		// Token: 0x040065EA RID: 26090
		public int clipH;

		// Token: 0x040065EB RID: 26091
		private bool isClip;

		// Token: 0x040065EC RID: 26092
		private bool isTranslate = true;

		// Token: 0x040065ED RID: 26093
		private int translateX;

		// Token: 0x040065EE RID: 26094
		private int translateY;

		// Token: 0x040065EF RID: 26095
		private float translateXf;

		// Token: 0x040065F0 RID: 26096
		private float translateYf;

		// Token: 0x040065F1 RID: 26097
		public static int zoomLevel = 1;

		// Token: 0x040065F2 RID: 26098
		public const int BASELINE = 64;

		// Token: 0x040065F3 RID: 26099
		public const int SOLID = 0;

		// Token: 0x040065F4 RID: 26100
		public const int DOTTED = 1;

		// Token: 0x040065F5 RID: 26101
		public const int TRANS_MIRROR = 2;

		// Token: 0x040065F6 RID: 26102
		public const int TRANS_MIRROR_ROT180 = 1;

		// Token: 0x040065F7 RID: 26103
		public const int TRANS_MIRROR_ROT270 = 4;

		// Token: 0x040065F8 RID: 26104
		public const int TRANS_MIRROR_ROT90 = 7;

		// Token: 0x040065F9 RID: 26105
		public const int TRANS_NONE = 0;

		// Token: 0x040065FA RID: 26106
		public const int TRANS_ROT180 = 3;

		// Token: 0x040065FB RID: 26107
		public const int TRANS_ROT270 = 6;

		// Token: 0x040065FC RID: 26108
		public const int TRANS_ROT90 = 5;

		// Token: 0x040065FD RID: 26109
		public static Hashtable cachedTextures = new Hashtable();

		// Token: 0x040065FE RID: 26110
		public static int addYWhenOpenKeyBoard;

		// Token: 0x040065FF RID: 26111
		private int clipTX;

		// Token: 0x04006600 RID: 26112
		private int clipTY;

		// Token: 0x04006601 RID: 26113
		private int currentBGColor;

		// Token: 0x04006602 RID: 26114
		private Vector2 pos = new Vector2(0f, 0f);

		// Token: 0x04006603 RID: 26115
		private Rect rect;

		// Token: 0x04006604 RID: 26116
		private Matrix4x4 matrixBackup;

		// Token: 0x04006605 RID: 26117
		private Vector2 pivot;

		// Token: 0x04006606 RID: 26118
		public Vector2 size = new Vector2(128f, 128f);

		// Token: 0x04006607 RID: 26119
		public Vector2 relativePosition = new Vector2(0f, 0f);

		// Token: 0x04006608 RID: 26120
		public Color clTrans;

		// Token: 0x04006609 RID: 26121
		public static Color transParentColor = new Color(1f, 1f, 1f, 0f);

		// Token: 0x0400660A RID: 26122
		private Material lineMaterial;

		// Token: 0x0400660B RID: 26123
		public float boderSize;

		// Token: 0x0400660C RID: 26124
		private float flipProgress;

		// Token: 0x0400660D RID: 26125
		public static bool isFlipping = false;

		// Token: 0x0400660E RID: 26126
		private float flipSpeed = 5f;
	}
}
