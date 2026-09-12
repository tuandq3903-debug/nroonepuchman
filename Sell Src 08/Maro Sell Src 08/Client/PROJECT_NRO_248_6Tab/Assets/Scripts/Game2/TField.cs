using System;
using System.Threading;
using UnityEngine;

namespace Game2
{
	// Token: 0x0200041F RID: 1055
	public class TField : IActionListener
	{
		// Token: 0x06002F27 RID: 12071 RVA: 0x002DF850 File Offset: 0x002DDA50
		public TField()
		{
			this.text = string.Empty;
			this.init();
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000034B9 File Offset: 0x000016B9
		public void doChangeToTextBox()
		{
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x002DF8F4 File Offset: 0x002DDAF4
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

		// Token: 0x06002F2A RID: 12074 RVA: 0x002DF947 File Offset: 0x002DDB47
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

		// Token: 0x06002F2B RID: 12075 RVA: 0x002DF96D File Offset: 0x002DDB6D
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

		// Token: 0x06002F2C RID: 12076 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
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

		// Token: 0x06002F2D RID: 12077 RVA: 0x002DFA14 File Offset: 0x002DDC14
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

		// Token: 0x06002F2E RID: 12078 RVA: 0x002DFA90 File Offset: 0x002DDC90
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

		// Token: 0x06002F2F RID: 12079 RVA: 0x002DFC10 File Offset: 0x002DDE10
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

		// Token: 0x06002F30 RID: 12080 RVA: 0x002DFE7C File Offset: 0x002DE07C
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

		// Token: 0x06002F31 RID: 12081 RVA: 0x002E005C File Offset: 0x002DE25C
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

		// Token: 0x06002F32 RID: 12082 RVA: 0x002E0158 File Offset: 0x002DE358
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

		// Token: 0x06002F33 RID: 12083 RVA: 0x002E02D8 File Offset: 0x002DE4D8
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

		// Token: 0x06002F34 RID: 12084 RVA: 0x002E0448 File Offset: 0x002DE648
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

		// Token: 0x06002F35 RID: 12085 RVA: 0x002E05D4 File Offset: 0x002DE7D4
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

		// Token: 0x06002F36 RID: 12086 RVA: 0x002E07D0 File Offset: 0x002DE9D0
		private bool isFocused()
		{
			return this.isFocus;
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x002E07D8 File Offset: 0x002DE9D8
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

		// Token: 0x06002F38 RID: 12088 RVA: 0x002E0894 File Offset: 0x002DEA94
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

		// Token: 0x06002F39 RID: 12089 RVA: 0x002E09E8 File Offset: 0x002DEBE8
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

		// Token: 0x06002F3A RID: 12090 RVA: 0x002E0A5C File Offset: 0x002DEC5C
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

		// Token: 0x06002F3B RID: 12091 RVA: 0x002E0AC8 File Offset: 0x002DECC8
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

		// Token: 0x06002F3C RID: 12092 RVA: 0x002E0BCE File Offset: 0x002DEDCE
		public string getText()
		{
			return this.text;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x002E0BD6 File Offset: 0x002DEDD6
		public void clearKb()
		{
			if (TField.kb != null)
			{
				TField.kb.text = string.Empty;
			}
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x002E0BF0 File Offset: 0x002DEDF0
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

		// Token: 0x06002F3F RID: 12095 RVA: 0x002E0C3E File Offset: 0x002DEE3E
		public void setMaxTextLenght(int maxTextLenght)
		{
			this.maxTextLenght = maxTextLenght;
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x002E0C47 File Offset: 0x002DEE47
		public void setIputType(int iputType)
		{
			this.inputType = iputType;
			this.setMaxTextLenght(500);
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x002E0C5B File Offset: 0x002DEE5B
		public void perform(int idAction, object p)
		{
			if (idAction == 1000)
			{
				this.clear();
			}
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x002E0C6C File Offset: 0x002DEE6C
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

		// Token: 0x06002F43 RID: 12099 RVA: 0x002E0EA0 File Offset: 0x002DF0A0
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

		// Token: 0x06002F44 RID: 12100 RVA: 0x002E0F9C File Offset: 0x002DF19C
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

		// Token: 0x04005B03 RID: 23299
		public bool isFocus;

		// Token: 0x04005B04 RID: 23300
		public int x;

		// Token: 0x04005B05 RID: 23301
		public int y;

		// Token: 0x04005B06 RID: 23302
		public int width;

		// Token: 0x04005B07 RID: 23303
		public int height;

		// Token: 0x04005B08 RID: 23304
		public bool lockArrow;

		// Token: 0x04005B09 RID: 23305
		public bool justReturnFromTextBox;

		// Token: 0x04005B0A RID: 23306
		public bool paintFocus = true;

		// Token: 0x04005B0B RID: 23307
		public const sbyte KEY_LEFT = 14;

		// Token: 0x04005B0C RID: 23308
		public const sbyte KEY_RIGHT = 15;

		// Token: 0x04005B0D RID: 23309
		public const sbyte KEY_CLEAR = 19;

		// Token: 0x04005B0E RID: 23310
		public static int typeXpeed = 2;

		// Token: 0x04005B0F RID: 23311
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

		// Token: 0x04005B10 RID: 23312
		private static int CARET_HEIGHT = 0;

		// Token: 0x04005B11 RID: 23313
		private static readonly int CARET_WIDTH = 1;

		// Token: 0x04005B12 RID: 23314
		private static readonly int CARET_SHOWING_TIME = 5;

		// Token: 0x04005B13 RID: 23315
		private static readonly int TEXT_GAP_X = 4;

		// Token: 0x04005B14 RID: 23316
		private static readonly int MAX_SHOW_CARET_COUNER = 10;

		// Token: 0x04005B15 RID: 23317
		public static readonly int INPUT_TYPE_ANY = 0;

		// Token: 0x04005B16 RID: 23318
		public static readonly int INPUT_TYPE_NUMERIC = 1;

		// Token: 0x04005B17 RID: 23319
		public static readonly int INPUT_TYPE_PASSWORD = 2;

		// Token: 0x04005B18 RID: 23320
		public static readonly int INPUT_ALPHA_NUMBER_ONLY = 3;

		// Token: 0x04005B19 RID: 23321
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

		// Token: 0x04005B1A RID: 23322
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

		// Token: 0x04005B1B RID: 23323
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

		// Token: 0x04005B1C RID: 23324
		private string text = string.Empty;

		// Token: 0x04005B1D RID: 23325
		private string passwordText = string.Empty;

		// Token: 0x04005B1E RID: 23326
		private string paintedText = string.Empty;

		// Token: 0x04005B1F RID: 23327
		private int caretPos;

		// Token: 0x04005B20 RID: 23328
		private int counter;

		// Token: 0x04005B21 RID: 23329
		private int maxTextLenght = 500;

		// Token: 0x04005B22 RID: 23330
		private int offsetX;

		// Token: 0x04005B23 RID: 23331
		private static int lastKey = -1984;

		// Token: 0x04005B24 RID: 23332
		private int keyInActiveState;

		// Token: 0x04005B25 RID: 23333
		private int indexOfActiveChar;

		// Token: 0x04005B26 RID: 23334
		private int showCaretCounter = TField.MAX_SHOW_CARET_COUNER;

		// Token: 0x04005B27 RID: 23335
		private int inputType = TField.INPUT_TYPE_ANY;

		// Token: 0x04005B28 RID: 23336
		public static bool isQwerty = true;

		// Token: 0x04005B29 RID: 23337
		public static int typingModeAreaWidth;

		// Token: 0x04005B2A RID: 23338
		public static int mode = 0;

		// Token: 0x04005B2B RID: 23339
		public static long timeChangeMode;

		// Token: 0x04005B2C RID: 23340
		public static readonly string[] modeNotify = new string[]
		{
			"abc",
			"Abc",
			"ABC",
			"123"
		};

		// Token: 0x04005B2D RID: 23341
		public static readonly int NOKIA = 0;

		// Token: 0x04005B2E RID: 23342
		public static readonly int MOTO = 1;

		// Token: 0x04005B2F RID: 23343
		public static readonly int ORTHER = 2;

		// Token: 0x04005B30 RID: 23344
		public static readonly int BB = 3;

		// Token: 0x04005B31 RID: 23345
		public static int changeModeKey = 11;

		// Token: 0x04005B32 RID: 23346
		public static readonly sbyte abc = 0;

		// Token: 0x04005B33 RID: 23347
		public static readonly sbyte Abc = 1;

		// Token: 0x04005B34 RID: 23348
		public static readonly sbyte ABC = 2;

		// Token: 0x04005B35 RID: 23349
		public static readonly sbyte number123 = 3;

		// Token: 0x04005B36 RID: 23350
		public static TField currentTField;

		// Token: 0x04005B37 RID: 23351
		public bool isTfield;

		// Token: 0x04005B38 RID: 23352
		public bool isPaintMouse = true;

		// Token: 0x04005B39 RID: 23353
		public string name = string.Empty;

		// Token: 0x04005B3A RID: 23354
		public string title = string.Empty;

		// Token: 0x04005B3B RID: 23355
		public string strInfo;

		// Token: 0x04005B3C RID: 23356
		public Command cmdClear;

		// Token: 0x04005B3D RID: 23357
		public Command cmdDoneAction;

		// Token: 0x04005B3E RID: 23358
		private mScreen parentScr;

		// Token: 0x04005B3F RID: 23359
		private int timeDelayKyCode;

		// Token: 0x04005B40 RID: 23360
		private int holdCount;

		// Token: 0x04005B41 RID: 23361
		public static int changeDau;

		// Token: 0x04005B42 RID: 23362
		private int indexDau = -1;

		// Token: 0x04005B43 RID: 23363
		private int indexTemplate;

		// Token: 0x04005B44 RID: 23364
		private int indexCong;

		// Token: 0x04005B45 RID: 23365
		private long timeDau;

		// Token: 0x04005B46 RID: 23366
		private static string printDau = "aáàảãạâấầẩẫậăắằẳẵặeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ";

		// Token: 0x04005B47 RID: 23367
		public static Image imgTf;

		// Token: 0x04005B48 RID: 23368
		public int timePutKeyClearAll;

		// Token: 0x04005B49 RID: 23369
		public int timeClearFirt;

		// Token: 0x04005B4A RID: 23370
		public bool isPaintCarret;

		// Token: 0x04005B4B RID: 23371
		public bool showSubTextField = true;

		// Token: 0x04005B4C RID: 23372
		public static TouchScreenKeyboard kb;

		// Token: 0x04005B4D RID: 23373
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

		// Token: 0x04005B4E RID: 23374
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

		// Token: 0x04005B4F RID: 23375
		private string telexBuffer = "";

		// Token: 0x04005B50 RID: 23376
		private const int TELEX_BUFFER_LENGTH = 5;
	}
}
