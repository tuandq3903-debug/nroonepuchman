using System;
using System.Threading;
using UnityEngine;

namespace Game4
{
	// Token: 0x0200026F RID: 623
	public class TField : IActionListener
	{
		// Token: 0x06001BDF RID: 7135 RVA: 0x001B5708 File Offset: 0x001B3908
		public TField()
		{
			this.text = string.Empty;
			this.init();
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000034B9 File Offset: 0x000016B9
		public void doChangeToTextBox()
		{
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x001B57AC File Offset: 0x001B39AC
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

		// Token: 0x06001BE2 RID: 7138 RVA: 0x001B57FF File Offset: 0x001B39FF
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

		// Token: 0x06001BE3 RID: 7139 RVA: 0x001B5825 File Offset: 0x001B3A25
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

		// Token: 0x06001BE4 RID: 7140 RVA: 0x001B585C File Offset: 0x001B3A5C
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

		// Token: 0x06001BE5 RID: 7141 RVA: 0x001B58CC File Offset: 0x001B3ACC
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

		// Token: 0x06001BE6 RID: 7142 RVA: 0x001B5948 File Offset: 0x001B3B48
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

		// Token: 0x06001BE7 RID: 7143 RVA: 0x001B5AC8 File Offset: 0x001B3CC8
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

		// Token: 0x06001BE8 RID: 7144 RVA: 0x001B5D34 File Offset: 0x001B3F34
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

		// Token: 0x06001BE9 RID: 7145 RVA: 0x001B5F14 File Offset: 0x001B4114
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

		// Token: 0x06001BEA RID: 7146 RVA: 0x001B6010 File Offset: 0x001B4210
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

		// Token: 0x06001BEB RID: 7147 RVA: 0x001B6190 File Offset: 0x001B4390
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

		// Token: 0x06001BEC RID: 7148 RVA: 0x001B6300 File Offset: 0x001B4500
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

		// Token: 0x06001BED RID: 7149 RVA: 0x001B648C File Offset: 0x001B468C
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

		// Token: 0x06001BEE RID: 7150 RVA: 0x001B6688 File Offset: 0x001B4888
		private bool isFocused()
		{
			return this.isFocus;
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x001B6690 File Offset: 0x001B4890
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

		// Token: 0x06001BF0 RID: 7152 RVA: 0x001B674C File Offset: 0x001B494C
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

		// Token: 0x06001BF1 RID: 7153 RVA: 0x001B68A0 File Offset: 0x001B4AA0
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

		// Token: 0x06001BF2 RID: 7154 RVA: 0x001B6914 File Offset: 0x001B4B14
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

		// Token: 0x06001BF3 RID: 7155 RVA: 0x001B6980 File Offset: 0x001B4B80
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

		// Token: 0x06001BF4 RID: 7156 RVA: 0x001B6A86 File Offset: 0x001B4C86
		public string getText()
		{
			return this.text;
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x001B6A8E File Offset: 0x001B4C8E
		public void clearKb()
		{
			if (TField.kb != null)
			{
				TField.kb.text = string.Empty;
			}
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x001B6AA8 File Offset: 0x001B4CA8
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

		// Token: 0x06001BF7 RID: 7159 RVA: 0x001B6AF6 File Offset: 0x001B4CF6
		public void setMaxTextLenght(int maxTextLenght)
		{
			this.maxTextLenght = maxTextLenght;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x001B6AFF File Offset: 0x001B4CFF
		public void setIputType(int iputType)
		{
			this.inputType = iputType;
			this.setMaxTextLenght(500);
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x001B6B13 File Offset: 0x001B4D13
		public void perform(int idAction, object p)
		{
			if (idAction == 1000)
			{
				this.clear();
			}
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x001B6B24 File Offset: 0x001B4D24
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

		// Token: 0x06001BFB RID: 7163 RVA: 0x001B6D58 File Offset: 0x001B4F58
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

		// Token: 0x06001BFC RID: 7164 RVA: 0x001B6E54 File Offset: 0x001B5054
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

		// Token: 0x04003605 RID: 13829
		public bool isFocus;

		// Token: 0x04003606 RID: 13830
		public int x;

		// Token: 0x04003607 RID: 13831
		public int y;

		// Token: 0x04003608 RID: 13832
		public int width;

		// Token: 0x04003609 RID: 13833
		public int height;

		// Token: 0x0400360A RID: 13834
		public bool lockArrow;

		// Token: 0x0400360B RID: 13835
		public bool justReturnFromTextBox;

		// Token: 0x0400360C RID: 13836
		public bool paintFocus = true;

		// Token: 0x0400360D RID: 13837
		public const sbyte KEY_LEFT = 14;

		// Token: 0x0400360E RID: 13838
		public const sbyte KEY_RIGHT = 15;

		// Token: 0x0400360F RID: 13839
		public const sbyte KEY_CLEAR = 19;

		// Token: 0x04003610 RID: 13840
		public static int typeXpeed = 2;

		// Token: 0x04003611 RID: 13841
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

		// Token: 0x04003612 RID: 13842
		private static int CARET_HEIGHT = 0;

		// Token: 0x04003613 RID: 13843
		private static readonly int CARET_WIDTH = 1;

		// Token: 0x04003614 RID: 13844
		private static readonly int CARET_SHOWING_TIME = 5;

		// Token: 0x04003615 RID: 13845
		private static readonly int TEXT_GAP_X = 4;

		// Token: 0x04003616 RID: 13846
		private static readonly int MAX_SHOW_CARET_COUNER = 10;

		// Token: 0x04003617 RID: 13847
		public static readonly int INPUT_TYPE_ANY = 0;

		// Token: 0x04003618 RID: 13848
		public static readonly int INPUT_TYPE_NUMERIC = 1;

		// Token: 0x04003619 RID: 13849
		public static readonly int INPUT_TYPE_PASSWORD = 2;

		// Token: 0x0400361A RID: 13850
		public static readonly int INPUT_ALPHA_NUMBER_ONLY = 3;

		// Token: 0x0400361B RID: 13851
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

		// Token: 0x0400361C RID: 13852
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

		// Token: 0x0400361D RID: 13853
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

		// Token: 0x0400361E RID: 13854
		private string text = string.Empty;

		// Token: 0x0400361F RID: 13855
		private string passwordText = string.Empty;

		// Token: 0x04003620 RID: 13856
		private string paintedText = string.Empty;

		// Token: 0x04003621 RID: 13857
		private int caretPos;

		// Token: 0x04003622 RID: 13858
		private int counter;

		// Token: 0x04003623 RID: 13859
		private int maxTextLenght = 500;

		// Token: 0x04003624 RID: 13860
		private int offsetX;

		// Token: 0x04003625 RID: 13861
		private static int lastKey = -1984;

		// Token: 0x04003626 RID: 13862
		private int keyInActiveState;

		// Token: 0x04003627 RID: 13863
		private int indexOfActiveChar;

		// Token: 0x04003628 RID: 13864
		private int showCaretCounter = TField.MAX_SHOW_CARET_COUNER;

		// Token: 0x04003629 RID: 13865
		private int inputType = TField.INPUT_TYPE_ANY;

		// Token: 0x0400362A RID: 13866
		public static bool isQwerty = true;

		// Token: 0x0400362B RID: 13867
		public static int typingModeAreaWidth;

		// Token: 0x0400362C RID: 13868
		public static int mode = 0;

		// Token: 0x0400362D RID: 13869
		public static long timeChangeMode;

		// Token: 0x0400362E RID: 13870
		public static readonly string[] modeNotify = new string[]
		{
			"abc",
			"Abc",
			"ABC",
			"123"
		};

		// Token: 0x0400362F RID: 13871
		public static readonly int NOKIA = 0;

		// Token: 0x04003630 RID: 13872
		public static readonly int MOTO = 1;

		// Token: 0x04003631 RID: 13873
		public static readonly int ORTHER = 2;

		// Token: 0x04003632 RID: 13874
		public static readonly int BB = 3;

		// Token: 0x04003633 RID: 13875
		public static int changeModeKey = 11;

		// Token: 0x04003634 RID: 13876
		public static readonly sbyte abc = 0;

		// Token: 0x04003635 RID: 13877
		public static readonly sbyte Abc = 1;

		// Token: 0x04003636 RID: 13878
		public static readonly sbyte ABC = 2;

		// Token: 0x04003637 RID: 13879
		public static readonly sbyte number123 = 3;

		// Token: 0x04003638 RID: 13880
		public static TField currentTField;

		// Token: 0x04003639 RID: 13881
		public bool isTfield;

		// Token: 0x0400363A RID: 13882
		public bool isPaintMouse = true;

		// Token: 0x0400363B RID: 13883
		public string name = string.Empty;

		// Token: 0x0400363C RID: 13884
		public string title = string.Empty;

		// Token: 0x0400363D RID: 13885
		public string strInfo;

		// Token: 0x0400363E RID: 13886
		public Command cmdClear;

		// Token: 0x0400363F RID: 13887
		public Command cmdDoneAction;

		// Token: 0x04003640 RID: 13888
		private mScreen parentScr;

		// Token: 0x04003641 RID: 13889
		private int timeDelayKyCode;

		// Token: 0x04003642 RID: 13890
		private int holdCount;

		// Token: 0x04003643 RID: 13891
		public static int changeDau;

		// Token: 0x04003644 RID: 13892
		private int indexDau = -1;

		// Token: 0x04003645 RID: 13893
		private int indexTemplate;

		// Token: 0x04003646 RID: 13894
		private int indexCong;

		// Token: 0x04003647 RID: 13895
		private long timeDau;

		// Token: 0x04003648 RID: 13896
		private static string printDau = "aáàảãạâấầẩẫậăắằẳẵặeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ";

		// Token: 0x04003649 RID: 13897
		public static Image imgTf;

		// Token: 0x0400364A RID: 13898
		public int timePutKeyClearAll;

		// Token: 0x0400364B RID: 13899
		public int timeClearFirt;

		// Token: 0x0400364C RID: 13900
		public bool isPaintCarret;

		// Token: 0x0400364D RID: 13901
		public bool showSubTextField = true;

		// Token: 0x0400364E RID: 13902
		public static TouchScreenKeyboard kb;

		// Token: 0x0400364F RID: 13903
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

		// Token: 0x04003650 RID: 13904
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

		// Token: 0x04003651 RID: 13905
		private string telexBuffer = "";

		// Token: 0x04003652 RID: 13906
		private const int TELEX_BUFFER_LENGTH = 5;
	}
}
