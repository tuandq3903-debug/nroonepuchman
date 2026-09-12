using System;
using System.Threading;
using UnityEngine;

namespace Game6
{
	// Token: 0x020000BF RID: 191
	public class TField : IActionListener
	{
		// Token: 0x06000897 RID: 2199 RVA: 0x0008B54C File Offset: 0x0008974C
		public TField()
		{
			this.text = string.Empty;
			this.init();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000034B9 File Offset: 0x000016B9
		public void doChangeToTextBox()
		{
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0008B5F0 File Offset: 0x000897F0
		public void init()
		{
			TField.CARET_HEIGHT = mScreen.ITEM_HEIGHT + 1;
			this.cmdClear = new Command(mResources.DELETE, this, 1000, null);
			if (Main.isPC)
			{
				TField.typeXpeed = 0;
			}
			if (TField.imgTf == null)
			{
				TField.imgTf = GameCanvas.loadImage("/mainImage/myTexture2dtf.png");
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0008B643 File Offset: 0x00089843
		public void clearKeyWhenPutText(int keyCode)
		{
			if (keyCode == -8 && this.timeDelayKyCode <= 0)
			{
				if (this.timeDelayKyCode <= 0)
				{
					this.timeDelayKyCode = 1;
				}
				this.clear();
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0008B669 File Offset: 0x00089869
		public void clearAllText()
		{
			this.text = string.Empty;
			if (TField.kb != null)
			{
				TField.kb.text = string.Empty;
			}
			this.caretPos = 0;
			this.setOffset(0);
			this.setPasswordTest();
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0008B6A0 File Offset: 0x000898A0
		public void clear()
		{
			if (this.caretPos > 0 && this.text.Length > 0)
			{
				this.text = this.text.Substring(0, this.caretPos - 1);
				this.caretPos--;
				this.setOffset(0);
				this.setPasswordTest();
				if (TField.kb != null)
				{
					TField.kb.text = this.text;
				}
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0008B710 File Offset: 0x00089910
		public void clearAll()
		{
			if (this.caretPos > 0 && this.text.Length > 0)
			{
				this.text = this.text.Substring(0, this.text.Length - 1);
				this.caretPos--;
				this.setOffset();
				this.setPasswordTest();
				this.setFocusWithKb(true);
				if (TField.kb != null)
				{
					TField.kb.text = string.Empty;
				}
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0008B78C File Offset: 0x0008998C
		public void setOffset()
		{
			if (this.paintedText != null && mFont.tahoma_8b != null)
			{
				if (this.inputType == TField.INPUT_TYPE_PASSWORD)
				{
					this.paintedText = this.passwordText;
				}
				else
				{
					this.paintedText = this.text;
				}
				if (this.offsetX < 0 && mFont.tahoma_8b.getWidth(this.paintedText) + this.offsetX < this.width - TField.TEXT_GAP_X - 13 - TField.typingModeAreaWidth)
				{
					this.offsetX = this.width - 10 - TField.typingModeAreaWidth - mFont.tahoma_8b.getWidth(this.paintedText);
				}
				if (this.offsetX + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) <= 0)
				{
					this.offsetX = -mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos));
					this.offsetX += 40;
				}
				else if (this.offsetX + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) >= this.width - 12 - TField.typingModeAreaWidth)
				{
					this.offsetX = this.width - 10 - TField.typingModeAreaWidth - mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos)) - 2 * TField.TEXT_GAP_X;
				}
				if (this.offsetX > 0)
				{
					this.offsetX = 0;
				}
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0008B90C File Offset: 0x00089B0C
		private void keyPressedAny(int keyCode)
		{
			string[] array = (this.inputType != TField.INPUT_TYPE_PASSWORD && this.inputType != TField.INPUT_ALPHA_NUMBER_ONLY) ? TField.print : TField.printA;
			if (keyCode == TField.lastKey)
			{
				this.indexOfActiveChar = (this.indexOfActiveChar + 1) % array[keyCode - 48].Length;
				char c = array[keyCode - 48][this.indexOfActiveChar];
				object obj = (TField.mode == 0) ? char.ToLower(c) : ((TField.mode == 1) ? char.ToUpper(c) : ((TField.mode != 2) ? array[keyCode - 48][array[keyCode - 48].Length - 1] : char.ToUpper(c)));
				string text = this.text.Substring(0, this.caretPos - 1) + ((obj != null) ? obj.ToString() : null);
				if (this.caretPos < this.text.Length)
				{
					text += this.text.Substring(this.caretPos, this.text.Length);
				}
				this.text = text;
				this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
				this.setPasswordTest();
			}
			else if (this.text.Length < this.maxTextLenght)
			{
				if (TField.mode == 1 && TField.lastKey != -1984)
				{
					TField.mode = 0;
				}
				this.indexOfActiveChar = 0;
				char c2 = array[keyCode - 48][this.indexOfActiveChar];
				object obj2 = (TField.mode == 0) ? char.ToLower(c2) : ((TField.mode == 1) ? char.ToUpper(c2) : ((TField.mode != 2) ? array[keyCode - 48][array[keyCode - 48].Length - 1] : char.ToUpper(c2)));
				string text2 = this.text.Substring(0, this.caretPos) + ((obj2 != null) ? obj2.ToString() : null);
				if (this.caretPos < this.text.Length)
				{
					text2 += this.text.Substring(this.caretPos, this.text.Length);
				}
				this.text = text2;
				this.keyInActiveState = TField.MAX_TIME_TO_CONFIRM_KEY[TField.typeXpeed];
				this.caretPos++;
				this.setPasswordTest();
				this.setOffset();
			}
			TField.lastKey = keyCode;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0008BB78 File Offset: 0x00089D78
		private void keyPressedAscii(int keyCode)
		{
			if ((keyCode >= 33 && keyCode <= 47) || (keyCode >= 58 && keyCode <= 64) || (keyCode >= 91 && keyCode <= 96) || (keyCode >= 123 && keyCode <= 126))
			{
				string text = this.text.Substring(0, this.caretPos) + ((char)keyCode).ToString();
				if (this.caretPos < this.text.Length)
				{
					text += this.text.Substring(this.caretPos, this.text.Length - this.caretPos);
				}
				this.text = text;
				this.caretPos++;
				this.setPasswordTest();
				this.setOffset(0);
				if (TField.kb != null)
				{
					TField.kb.text = this.text;
					return;
				}
			}
			else
			{
				if ((this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY) && (keyCode < 48 || keyCode > 57) && (keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 122))
				{
					return;
				}
				if (this.text.Length < this.maxTextLenght)
				{
					char c = (char)keyCode;
					string text2 = this.text.Substring(0, this.caretPos) + c.ToString();
					if (this.caretPos < this.text.Length)
					{
						text2 += this.text.Substring(this.caretPos, this.text.Length - this.caretPos);
					}
					this.text = text2;
					this.caretPos++;
					this.setPasswordTest();
					this.setOffset(0);
					if (ModFunc.isVietnamese && Main.isPC && this.inputType == TField.INPUT_TYPE_ANY)
					{
						this.processTelex(c);
					}
				}
				if (TField.kb != null)
				{
					TField.kb.text = this.text;
				}
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0008BD58 File Offset: 0x00089F58
		private void setDau()
		{
			this.timeDau = (long)(Environment.TickCount / 100);
			if (this.indexDau == -1)
			{
				for (int num = this.caretPos; num > 0; num--)
				{
					char c = this.text[num - 1];
					for (int i = 0; i < TField.printDau.Length; i++)
					{
						char c2 = TField.printDau[i];
						if (c == c2)
						{
							this.indexTemplate = i;
							this.indexCong = 0;
							this.indexDau = num - 1;
							return;
						}
					}
				}
				this.indexDau = -1;
				return;
			}
			this.indexCong++;
			if (this.indexCong >= 6)
			{
				this.indexCong = 0;
			}
			string text = this.text.Substring(0, this.indexDau);
			string text2 = this.text.Substring(this.indexDau + 1);
			string text3 = TField.printDau.Substring(this.indexTemplate + this.indexCong, 1);
			this.text = text + text3 + text2;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0008BE54 File Offset: 0x0008A054
		public bool keyPressed(int keyCode)
		{
			if (Main.isPC && keyCode == -8)
			{
				this.clearKeyWhenPutText(-8);
				return true;
			}
			if (keyCode == 8 || keyCode == -8 || keyCode == 204)
			{
				this.clear();
				return true;
			}
			if (TField.isQwerty && keyCode >= 32)
			{
				this.keyPressedAscii(keyCode);
				return false;
			}
			if (keyCode == TField.changeDau && this.inputType == TField.INPUT_TYPE_ANY)
			{
				this.setDau();
				return false;
			}
			if (keyCode == 42)
			{
				keyCode = 58;
			}
			if (keyCode == 35)
			{
				keyCode = 59;
			}
			if (keyCode >= 48 && keyCode <= 59)
			{
				if (this.inputType == TField.INPUT_TYPE_ANY || this.inputType == TField.INPUT_TYPE_PASSWORD || this.inputType == TField.INPUT_ALPHA_NUMBER_ONLY)
				{
					this.keyPressedAny(keyCode);
				}
				else if (this.inputType == TField.INPUT_TYPE_NUMERIC)
				{
					this.keyPressedAscii(keyCode);
					this.keyInActiveState = 1;
				}
			}
			else
			{
				this.indexOfActiveChar = 0;
				TField.lastKey = -1984;
				if (keyCode == 14 && !this.lockArrow)
				{
					if (this.caretPos > 0)
					{
						this.caretPos--;
						this.setOffset(0);
						this.showCaretCounter = TField.MAX_SHOW_CARET_COUNER;
						return false;
					}
				}
				else if (keyCode == 15 && !this.lockArrow)
				{
					if (this.caretPos < this.text.Length)
					{
						this.caretPos++;
						this.setOffset(0);
						this.showCaretCounter = TField.MAX_SHOW_CARET_COUNER;
						return false;
					}
				}
				else
				{
					if (keyCode == 19)
					{
						this.clear();
						return false;
					}
					TField.lastKey = keyCode;
				}
			}
			return true;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0008BFD4 File Offset: 0x0008A1D4
		public void setOffset(int index)
		{
			if (this.inputType == TField.INPUT_TYPE_PASSWORD)
			{
				this.paintedText = this.passwordText;
			}
			else
			{
				this.paintedText = this.text;
			}
			int num = mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos));
			if (index != -1)
			{
				if (index != 1)
				{
					this.offsetX = -(num - (this.width - 12));
				}
				else if (num + this.offsetX > this.width - 25 && this.caretPos < this.paintedText.Length && this.caretPos > 0)
				{
					this.offsetX -= mFont.tahoma_8b.getWidth(this.paintedText.Substring(this.caretPos - 1, 1));
				}
			}
			else if (num + this.offsetX < 15 && this.caretPos > 0 && this.caretPos < this.paintedText.Length)
			{
				this.offsetX += mFont.tahoma_8b.getWidth(this.paintedText.Substring(this.caretPos, 1));
			}
			if (this.offsetX > 0)
			{
				this.offsetX = 0;
				return;
			}
			if (this.offsetX < 0)
			{
				int num2 = mFont.tahoma_8b.getWidth(this.paintedText) - (this.width - 12);
				if (this.offsetX < -num2)
				{
					this.offsetX = -num2;
				}
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0008C144 File Offset: 0x0008A344
		public void paintInputTf(mGraphics g, bool iss, int x, int y, int w, int h, int xText, int yText, string text, string info)
		{
			g.setColor(0);
			if (iss)
			{
				g.drawRegion(TField.imgTf, 0, 81, 29, 27, 0, x, y, 0);
				g.drawRegion(TField.imgTf, 0, 135, 29, 27, 0, x + w - 29, y, 0);
				g.drawRegion(TField.imgTf, 0, 108, 29, 27, 0, x + w - 58, y, 0);
				for (int i = 0; i < (w - 58) / 29; i++)
				{
					g.drawRegion(TField.imgTf, 0, 108, 29, 27, 0, x + 29 + i * 29, y, 0);
				}
			}
			else
			{
				g.drawRegion(TField.imgTf, 0, 0, 29, 27, 0, x, y, 0);
				g.drawRegion(TField.imgTf, 0, 54, 29, 27, 0, x + w - 29, y, 0);
				g.drawRegion(TField.imgTf, 0, 27, 29, 27, 0, x + w - 58, y, 0);
				for (int j = 0; j < (w - 58) / 29; j++)
				{
					g.drawRegion(TField.imgTf, 0, 27, 29, 27, 0, x + 29 + j * 29, y, 0);
				}
			}
			g.setClip(x + 3, y + 1, w - 4, h);
			if (text != null && !text.Equals(string.Empty))
			{
				mFont.tahoma_8b.drawString(g, text, xText, yText, 0);
				return;
			}
			if (info != null)
			{
				if (iss)
				{
					mFont.tahoma_7b_focus.drawString(g, info, xText, yText, 0);
					return;
				}
				mFont.tahoma_7b_unfocus.drawString(g, info, xText, yText, 0);
			}
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0008C2D0 File Offset: 0x0008A4D0
		public void paint(mGraphics g)
		{
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			bool flag = this.isFocused();
			if (this.inputType == TField.INPUT_TYPE_PASSWORD)
			{
				this.paintedText = this.passwordText;
			}
			else
			{
				this.paintedText = this.text;
			}
			this.paintInputTf(g, flag, this.x, this.y - 1, this.width, this.height + 5, TField.TEXT_GAP_X + this.offsetX + this.x + 1, this.y + (this.height - mFont.tahoma_8b.getHeight()) / 2 + 2, this.paintedText, this.name);
			g.setClip(this.x + 3, this.y + 1, this.width - 4, this.height - 2);
			g.setColor(0);
			if (flag && this.isPaintMouse && this.isPaintCarret)
			{
				if (this.keyInActiveState == 0 && (this.showCaretCounter > 0 || this.counter / TField.CARET_SHOWING_TIME % 4 == 0))
				{
					g.setColor(7999781);
					g.fillRect(TField.TEXT_GAP_X + 1 + this.offsetX + this.x + mFont.tahoma_8b.getWidth(this.paintedText.Substring(0, this.caretPos) + "a") - TField.CARET_WIDTH - mFont.tahoma_8b.getWidth("a"), this.y + (this.height - TField.CARET_HEIGHT) / 2 + 5, TField.CARET_WIDTH, TField.CARET_HEIGHT);
				}
				GameCanvas.resetTrans(g);
				if (this.text != null && this.text.Length > 0 && GameCanvas.isTouch)
				{
					g.drawImage(GameCanvas.imgClear, this.x + this.width - 13, this.y + this.height / 2 + 3, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0008C4CC File Offset: 0x0008A6CC
		private bool isFocused()
		{
			return this.isFocus;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0008C4D4 File Offset: 0x0008A6D4
		private void setPasswordTest()
		{
			if (this.inputType == TField.INPUT_TYPE_PASSWORD)
			{
				this.passwordText = string.Empty;
				for (int i = 0; i < this.text.Length; i++)
				{
					this.passwordText += "*";
				}
				if (this.keyInActiveState > 0 && this.caretPos > 0)
				{
					this.passwordText = this.passwordText.Substring(0, this.caretPos - 1) + this.text[this.caretPos - 1].ToString() + this.passwordText.Substring(this.caretPos, this.passwordText.Length);
				}
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0008C590 File Offset: 0x0008A790
		public void update()
		{
			if (ModFunc.isOpenAccMAnager)
			{
				return;
			}
			this.isPaintCarret = true;
			if (Main.isPC)
			{
				if (this.timeDelayKyCode > 0)
				{
					this.timeDelayKyCode--;
				}
				if (this.timeDelayKyCode <= 0)
				{
					this.timeDelayKyCode = 0;
				}
			}
			if (TField.kb != null && TField.currentTField == this)
			{
				if (TField.kb.text.Length < 40 && this.isFocus)
				{
					this.setText(TField.kb.text);
				}
				if (TField.kb.status == TouchScreenKeyboard.Status.Done && this.cmdDoneAction != null)
				{
					this.cmdDoneAction.performAction();
				}
			}
			this.counter++;
			if (this.keyInActiveState > 0)
			{
				this.keyInActiveState--;
				if (this.keyInActiveState == 0)
				{
					this.indexOfActiveChar = 0;
					if (TField.mode == 1 && TField.lastKey != TField.changeModeKey && this.isFocus)
					{
						TField.mode = 0;
					}
					TField.lastKey = -1984;
					this.setPasswordTest();
				}
			}
			if (this.showCaretCounter > 0)
			{
				this.showCaretCounter--;
			}
			if (GameCanvas.isPointerJustRelease)
			{
				this.setTextBox();
			}
			if (this.indexDau != -1 && (long)(Environment.TickCount / 100) - this.timeDau > 5L)
			{
				this.indexDau = -1;
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0008C6E4 File Offset: 0x0008A8E4
		public void setTextBox()
		{
			if (GameCanvas.isPointerHoldIn(this.x + this.width - 20, this.y, 40, this.height))
			{
				this.clearAllText();
				this.isFocus = true;
				return;
			}
			if (GameCanvas.isPointerHoldIn(this.x, this.y, this.width - 20, this.height))
			{
				this.setFocusWithKb(true);
				return;
			}
			this.setFocus(false);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0008C758 File Offset: 0x0008A958
		public void setFocus(bool isFocus)
		{
			if (this.isFocus != isFocus)
			{
				TField.mode = 0;
			}
			TField.lastKey = -1984;
			TField.timeChangeMode = (long)((int)(DateTime.Now.Ticks / 1000L));
			this.isFocus = isFocus;
			if (isFocus)
			{
				TField.currentTField = this;
				if (TField.kb != null)
				{
					TField.kb.text = TField.currentTField.text;
				}
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0008C7C4 File Offset: 0x0008A9C4
		public void setFocusWithKb(bool isFocus)
		{
			if (this.isFocus != isFocus)
			{
				TField.mode = 0;
			}
			TField.lastKey = -1984;
			TField.timeChangeMode = (long)((int)(DateTime.Now.Ticks / 1000L));
			this.isFocus = isFocus;
			if (isFocus)
			{
				TField.currentTField = this;
			}
			else if (TField.currentTField == this)
			{
				TField.currentTField = null;
			}
			if (!(Thread.CurrentThread.Name == Main.mainThreadName) || TField.currentTField == null)
			{
				return;
			}
			this.isFocus = true;
			TouchScreenKeyboard.hideInput = !TField.currentTField.showSubTextField;
			TouchScreenKeyboardType t = TouchScreenKeyboardType.ASCIICapable;
			if (this.inputType == TField.INPUT_TYPE_NUMERIC)
			{
				t = TouchScreenKeyboardType.NumberPad;
			}
			bool type = false;
			if (this.inputType == TField.INPUT_TYPE_PASSWORD)
			{
				type = true;
			}
			if (!Main.isPC || Main.isIPhone)
			{
				TField.kb = TouchScreenKeyboard.Open(TField.currentTField.text, t, false, false, type, false, TField.currentTField.name);
				if (TField.kb != null)
				{
					TField.kb.text = TField.currentTField.text;
				}
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0008C8CA File Offset: 0x0008AACA
		public string getText()
		{
			return this.text;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0008C8D2 File Offset: 0x0008AAD2
		public void clearKb()
		{
			if (TField.kb != null)
			{
				TField.kb.text = string.Empty;
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0008C8EC File Offset: 0x0008AAEC
		public void setText(string text)
		{
			if (text != null)
			{
				TField.lastKey = -1984;
				this.keyInActiveState = 0;
				this.indexOfActiveChar = 0;
				this.text = text;
				this.paintedText = text;
				this.setPasswordTest();
				this.caretPos = text.Length;
				this.setOffset();
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0008C93A File Offset: 0x0008AB3A
		public void setMaxTextLenght(int maxTextLenght)
		{
			this.maxTextLenght = maxTextLenght;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0008C943 File Offset: 0x0008AB43
		public void setIputType(int iputType)
		{
			this.inputType = iputType;
			this.setMaxTextLenght(500);
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0008C957 File Offset: 0x0008AB57
		public void perform(int idAction, object p)
		{
			if (idAction == 1000)
			{
				this.clear();
			}
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0008C968 File Offset: 0x0008AB68
		private void processTelex(char c)
		{
			this.telexBuffer += c.ToString();
			if (this.telexBuffer.Length > 5)
			{
				this.telexBuffer = this.telexBuffer.Substring(this.telexBuffer.Length - 5);
			}
			string[] array = TField.telexMap;
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split('|', StringSplitOptions.None);
				string input = array2[0];
				string output = array2[1];
				if (this.telexBuffer.EndsWith(input))
				{
					int pos = this.caretPos - input.Length;
					if (pos >= 0)
					{
						string before = this.text.Substring(0, pos);
						string after = (this.caretPos < this.text.Length) ? this.text.Substring(this.caretPos) : "";
						if (input.Length == 1 && "sfrxj".Contains(input))
						{
							if (pos > 0)
							{
								char prevChar = this.text[pos - 1];
								char newChar = this.addMark(prevChar, output[0]);
								if (newChar == prevChar)
								{
									this.text = before + input + after;
									this.caretPos = pos + 1;
								}
								else
								{
									this.text = before.Substring(0, before.Length - 1) + newChar.ToString() + after;
									this.caretPos = pos;
								}
								this.telexBuffer = "";
								this.setPasswordTest();
								this.setOffset();
								return;
							}
						}
						else
						{
							if (pos > 0 && input.Length == 2 && "âăêôơư".Contains(this.text[pos - 1].ToString()))
							{
								this.text = before + input[1].ToString() + after;
								this.caretPos = pos + 1;
								this.telexBuffer = "";
								this.setPasswordTest();
								this.setOffset();
								return;
							}
							this.text = before + output + after;
							this.caretPos = pos + 1;
							this.telexBuffer = "";
							this.setPasswordTest();
							this.setOffset();
							return;
						}
					}
				}
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0008CB9C File Offset: 0x0008AD9C
		private char addMark(char c, char mark)
		{
			if ("aáàảãạâấầẩẫậăắằẳẵặeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ".IndexOf(c) < 0)
			{
				return c;
			}
			char baseVowel = c;
			int markType = this.getMarkType(mark);
			if ("âăêôơư".Contains(baseVowel.ToString()))
			{
				foreach (string specialMark in new string[]
				{
					"âấầẩẫậ",
					"ăắằẳẵặ",
					"êếềểễệ",
					"ôốồổỗộ",
					"ơớờởỡợ",
					"ưứừửữự"
				})
				{
					if (specialMark[0] == baseVowel)
					{
						return specialMark[markType + 1];
					}
				}
			}
			foreach (string markSet in new string[]
			{
				"aáàảãạ",
				"eéèẻẽẹ",
				"iíìỉĩị",
				"oóòỏõọ",
				"uúùủũụ",
				"yýỳỷỹỵ"
			})
			{
				if (markSet.Contains(baseVowel))
				{
					return markSet[markType + 1];
				}
			}
			return c;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0008CC98 File Offset: 0x0008AE98
		private int getMarkType(char mark)
		{
			switch (mark)
			{
			case '̀':
				return 1;
			case '́':
				return 0;
			case '̂':
				break;
			case '̃':
				return 3;
			default:
				if (mark == '̉')
				{
					return 2;
				}
				if (mark == '̣')
				{
					return 4;
				}
				break;
			}
			return 0;
		}

		// Token: 0x04001107 RID: 4359
		public bool isFocus;

		// Token: 0x04001108 RID: 4360
		public int x;

		// Token: 0x04001109 RID: 4361
		public int y;

		// Token: 0x0400110A RID: 4362
		public int width;

		// Token: 0x0400110B RID: 4363
		public int height;

		// Token: 0x0400110C RID: 4364
		public bool lockArrow;

		// Token: 0x0400110D RID: 4365
		public bool justReturnFromTextBox;

		// Token: 0x0400110E RID: 4366
		public bool paintFocus = true;

		// Token: 0x0400110F RID: 4367
		public const sbyte KEY_LEFT = 14;

		// Token: 0x04001110 RID: 4368
		public const sbyte KEY_RIGHT = 15;

		// Token: 0x04001111 RID: 4369
		public const sbyte KEY_CLEAR = 19;

		// Token: 0x04001112 RID: 4370
		public static int typeXpeed = 2;

		// Token: 0x04001113 RID: 4371
		private static readonly int[] MAX_TIME_TO_CONFIRM_KEY = new int[]
		{
			30,
			14,
			11,
			9,
			6,
			4,
			2
		};

		// Token: 0x04001114 RID: 4372
		private static int CARET_HEIGHT = 0;

		// Token: 0x04001115 RID: 4373
		private static readonly int CARET_WIDTH = 1;

		// Token: 0x04001116 RID: 4374
		private static readonly int CARET_SHOWING_TIME = 5;

		// Token: 0x04001117 RID: 4375
		private static readonly int TEXT_GAP_X = 4;

		// Token: 0x04001118 RID: 4376
		private static readonly int MAX_SHOW_CARET_COUNER = 10;

		// Token: 0x04001119 RID: 4377
		public static readonly int INPUT_TYPE_ANY = 0;

		// Token: 0x0400111A RID: 4378
		public static readonly int INPUT_TYPE_NUMERIC = 1;

		// Token: 0x0400111B RID: 4379
		public static readonly int INPUT_TYPE_PASSWORD = 2;

		// Token: 0x0400111C RID: 4380
		public static readonly int INPUT_ALPHA_NUMBER_ONLY = 3;

		// Token: 0x0400111D RID: 4381
		private static string[] print = new string[]
		{
			" 0",
			".,@?!_1\"/$-():*+<=>;%&~#%^&*{}[];'/1",
			"abc2áàảãạâấầẩẫậăắằẳẵặ2",
			"def3đéèẻẽẹêếềểễệ3",
			"ghi4íìỉĩị4",
			"jkl5",
			"mno6óòỏõọôốồổỗộơớờởỡợ6",
			"pqrs7",
			"tuv8úùủũụưứừửữự8",
			"wxyz9ýỳỷỹỵ9",
			"*",
			"#"
		};

		// Token: 0x0400111E RID: 4382
		private static string[] printA = new string[]
		{
			"0",
			"1",
			"abc2",
			"def3",
			"ghi4",
			"jkl5",
			"mno6",
			"pqrs7",
			"tuv8",
			"wxyz9",
			"0",
			"0"
		};

		// Token: 0x0400111F RID: 4383
		private static string[] printBB = new string[]
		{
			" 0",
			"er1",
			"ty2",
			"ui3",
			"df4",
			"gh5",
			"jk6",
			"cv7",
			"bn8",
			"m9",
			"0",
			"0",
			"qw!",
			"as?",
			"zx",
			"op.",
			"l,"
		};

		// Token: 0x04001120 RID: 4384
		private string text = string.Empty;

		// Token: 0x04001121 RID: 4385
		private string passwordText = string.Empty;

		// Token: 0x04001122 RID: 4386
		private string paintedText = string.Empty;

		// Token: 0x04001123 RID: 4387
		private int caretPos;

		// Token: 0x04001124 RID: 4388
		private int counter;

		// Token: 0x04001125 RID: 4389
		private int maxTextLenght = 500;

		// Token: 0x04001126 RID: 4390
		private int offsetX;

		// Token: 0x04001127 RID: 4391
		private static int lastKey = -1984;

		// Token: 0x04001128 RID: 4392
		private int keyInActiveState;

		// Token: 0x04001129 RID: 4393
		private int indexOfActiveChar;

		// Token: 0x0400112A RID: 4394
		private int showCaretCounter = TField.MAX_SHOW_CARET_COUNER;

		// Token: 0x0400112B RID: 4395
		private int inputType = TField.INPUT_TYPE_ANY;

		// Token: 0x0400112C RID: 4396
		public static bool isQwerty = true;

		// Token: 0x0400112D RID: 4397
		public static int typingModeAreaWidth;

		// Token: 0x0400112E RID: 4398
		public static int mode = 0;

		// Token: 0x0400112F RID: 4399
		public static long timeChangeMode;

		// Token: 0x04001130 RID: 4400
		public static readonly string[] modeNotify = new string[]
		{
			"abc",
			"Abc",
			"ABC",
			"123"
		};

		// Token: 0x04001131 RID: 4401
		public static readonly int NOKIA = 0;

		// Token: 0x04001132 RID: 4402
		public static readonly int MOTO = 1;

		// Token: 0x04001133 RID: 4403
		public static readonly int ORTHER = 2;

		// Token: 0x04001134 RID: 4404
		public static readonly int BB = 3;

		// Token: 0x04001135 RID: 4405
		public static int changeModeKey = 11;

		// Token: 0x04001136 RID: 4406
		public static readonly sbyte abc = 0;

		// Token: 0x04001137 RID: 4407
		public static readonly sbyte Abc = 1;

		// Token: 0x04001138 RID: 4408
		public static readonly sbyte ABC = 2;

		// Token: 0x04001139 RID: 4409
		public static readonly sbyte number123 = 3;

		// Token: 0x0400113A RID: 4410
		public static TField currentTField;

		// Token: 0x0400113B RID: 4411
		public bool isTfield;

		// Token: 0x0400113C RID: 4412
		public bool isPaintMouse = true;

		// Token: 0x0400113D RID: 4413
		public string name = string.Empty;

		// Token: 0x0400113E RID: 4414
		public string title = string.Empty;

		// Token: 0x0400113F RID: 4415
		public string strInfo;

		// Token: 0x04001140 RID: 4416
		public Command cmdClear;

		// Token: 0x04001141 RID: 4417
		public Command cmdDoneAction;

		// Token: 0x04001142 RID: 4418
		private mScreen parentScr;

		// Token: 0x04001143 RID: 4419
		private int timeDelayKyCode;

		// Token: 0x04001144 RID: 4420
		private int holdCount;

		// Token: 0x04001145 RID: 4421
		public static int changeDau;

		// Token: 0x04001146 RID: 4422
		private int indexDau = -1;

		// Token: 0x04001147 RID: 4423
		private int indexTemplate;

		// Token: 0x04001148 RID: 4424
		private int indexCong;

		// Token: 0x04001149 RID: 4425
		private long timeDau;

		// Token: 0x0400114A RID: 4426
		private static string printDau = "aáàảãạâấầẩẫậăắằẳẵặeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ";

		// Token: 0x0400114B RID: 4427
		public static Image imgTf;

		// Token: 0x0400114C RID: 4428
		public int timePutKeyClearAll;

		// Token: 0x0400114D RID: 4429
		public int timeClearFirt;

		// Token: 0x0400114E RID: 4430
		public bool isPaintCarret;

		// Token: 0x0400114F RID: 4431
		public bool showSubTextField = true;

		// Token: 0x04001150 RID: 4432
		public static TouchScreenKeyboard kb;

		// Token: 0x04001151 RID: 4433
		public static int[][] BBKEY = new int[][]
		{
			new int[]
			{
				32,
				48
			},
			new int[]
			{
				49,
				69
			},
			new int[]
			{
				50,
				84
			},
			new int[]
			{
				51,
				85
			},
			new int[]
			{
				52,
				68
			},
			new int[]
			{
				53,
				71
			},
			new int[]
			{
				54,
				74
			},
			new int[]
			{
				55,
				67
			},
			new int[]
			{
				56,
				66
			},
			new int[]
			{
				57,
				77
			},
			new int[]
			{
				42,
				128
			},
			new int[]
			{
				35,
				137
			},
			new int[]
			{
				33,
				113
			},
			new int[]
			{
				63,
				97
			},
			new int[]
			{
				64,
				121,
				122
			},
			new int[]
			{
				46,
				111
			},
			new int[]
			{
				44,
				108
			}
		};

		// Token: 0x04001152 RID: 4434
		private static string[] telexMap = new string[]
		{
			"aw|ă",
			"aa|â",
			"dd|đ",
			"ow|ơ",
			"oo|ô",
			"ee|ê",
			"uw|ư",
			"w|ư",
			"s|́",
			"f|̀",
			"r|̉",
			"x|̃",
			"j|̣"
		};

		// Token: 0x04001153 RID: 4435
		private string telexBuffer = "";

		// Token: 0x04001154 RID: 4436
		private const int TELEX_BUFFER_LENGTH = 5;
	}
}
