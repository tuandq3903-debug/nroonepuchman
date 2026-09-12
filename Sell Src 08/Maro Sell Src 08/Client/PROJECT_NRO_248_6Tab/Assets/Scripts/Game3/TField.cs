using System;
using System.Threading;
using UnityEngine;

namespace Game3
{
	// Token: 0x02000347 RID: 839
	public class TField : IActionListener
	{
		// Token: 0x06002583 RID: 9603 RVA: 0x0024A7AC File Offset: 0x002489AC
		public TField()
		{
			this.text = string.Empty;
			this.init();
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x000034B9 File Offset: 0x000016B9
		public void doChangeToTextBox()
		{
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x0024A850 File Offset: 0x00248A50
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

		// Token: 0x06002586 RID: 9606 RVA: 0x0024A8A3 File Offset: 0x00248AA3
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

		// Token: 0x06002587 RID: 9607 RVA: 0x0024A8C9 File Offset: 0x00248AC9
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

		// Token: 0x06002588 RID: 9608 RVA: 0x0024A900 File Offset: 0x00248B00
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

		// Token: 0x06002589 RID: 9609 RVA: 0x0024A970 File Offset: 0x00248B70
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

		// Token: 0x0600258A RID: 9610 RVA: 0x0024A9EC File Offset: 0x00248BEC
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

		// Token: 0x0600258B RID: 9611 RVA: 0x0024AB6C File Offset: 0x00248D6C
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

		// Token: 0x0600258C RID: 9612 RVA: 0x0024ADD8 File Offset: 0x00248FD8
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

		// Token: 0x0600258D RID: 9613 RVA: 0x0024AFB8 File Offset: 0x002491B8
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

		// Token: 0x0600258E RID: 9614 RVA: 0x0024B0B4 File Offset: 0x002492B4
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

		// Token: 0x0600258F RID: 9615 RVA: 0x0024B234 File Offset: 0x00249434
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

		// Token: 0x06002590 RID: 9616 RVA: 0x0024B3A4 File Offset: 0x002495A4
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

		// Token: 0x06002591 RID: 9617 RVA: 0x0024B530 File Offset: 0x00249730
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

		// Token: 0x06002592 RID: 9618 RVA: 0x0024B72C File Offset: 0x0024992C
		private bool isFocused()
		{
			return this.isFocus;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0024B734 File Offset: 0x00249934
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

		// Token: 0x06002594 RID: 9620 RVA: 0x0024B7F0 File Offset: 0x002499F0
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

		// Token: 0x06002595 RID: 9621 RVA: 0x0024B944 File Offset: 0x00249B44
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

		// Token: 0x06002596 RID: 9622 RVA: 0x0024B9B8 File Offset: 0x00249BB8
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

		// Token: 0x06002597 RID: 9623 RVA: 0x0024BA24 File Offset: 0x00249C24
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

		// Token: 0x06002598 RID: 9624 RVA: 0x0024BB2A File Offset: 0x00249D2A
		public string getText()
		{
			return this.text;
		}

		// Token: 0x06002599 RID: 9625 RVA: 0x0024BB32 File Offset: 0x00249D32
		public void clearKb()
		{
			if (TField.kb != null)
			{
				TField.kb.text = string.Empty;
			}
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x0024BB4C File Offset: 0x00249D4C
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

		// Token: 0x0600259B RID: 9627 RVA: 0x0024BB9A File Offset: 0x00249D9A
		public void setMaxTextLenght(int maxTextLenght)
		{
			this.maxTextLenght = maxTextLenght;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0024BBA3 File Offset: 0x00249DA3
		public void setIputType(int iputType)
		{
			this.inputType = iputType;
			this.setMaxTextLenght(500);
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0024BBB7 File Offset: 0x00249DB7
		public void perform(int idAction, object p)
		{
			if (idAction == 1000)
			{
				this.clear();
			}
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0024BBC8 File Offset: 0x00249DC8
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

		// Token: 0x0600259F RID: 9631 RVA: 0x0024BDFC File Offset: 0x00249FFC
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

		// Token: 0x060025A0 RID: 9632 RVA: 0x0024BEF8 File Offset: 0x0024A0F8
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

		// Token: 0x04004884 RID: 18564
		public bool isFocus;

		// Token: 0x04004885 RID: 18565
		public int x;

		// Token: 0x04004886 RID: 18566
		public int y;

		// Token: 0x04004887 RID: 18567
		public int width;

		// Token: 0x04004888 RID: 18568
		public int height;

		// Token: 0x04004889 RID: 18569
		public bool lockArrow;

		// Token: 0x0400488A RID: 18570
		public bool justReturnFromTextBox;

		// Token: 0x0400488B RID: 18571
		public bool paintFocus = true;

		// Token: 0x0400488C RID: 18572
		public const sbyte KEY_LEFT = 14;

		// Token: 0x0400488D RID: 18573
		public const sbyte KEY_RIGHT = 15;

		// Token: 0x0400488E RID: 18574
		public const sbyte KEY_CLEAR = 19;

		// Token: 0x0400488F RID: 18575
		public static int typeXpeed = 2;

		// Token: 0x04004890 RID: 18576
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

		// Token: 0x04004891 RID: 18577
		private static int CARET_HEIGHT = 0;

		// Token: 0x04004892 RID: 18578
		private static readonly int CARET_WIDTH = 1;

		// Token: 0x04004893 RID: 18579
		private static readonly int CARET_SHOWING_TIME = 5;

		// Token: 0x04004894 RID: 18580
		private static readonly int TEXT_GAP_X = 4;

		// Token: 0x04004895 RID: 18581
		private static readonly int MAX_SHOW_CARET_COUNER = 10;

		// Token: 0x04004896 RID: 18582
		public static readonly int INPUT_TYPE_ANY = 0;

		// Token: 0x04004897 RID: 18583
		public static readonly int INPUT_TYPE_NUMERIC = 1;

		// Token: 0x04004898 RID: 18584
		public static readonly int INPUT_TYPE_PASSWORD = 2;

		// Token: 0x04004899 RID: 18585
		public static readonly int INPUT_ALPHA_NUMBER_ONLY = 3;

		// Token: 0x0400489A RID: 18586
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

		// Token: 0x0400489B RID: 18587
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

		// Token: 0x0400489C RID: 18588
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

		// Token: 0x0400489D RID: 18589
		private string text = string.Empty;

		// Token: 0x0400489E RID: 18590
		private string passwordText = string.Empty;

		// Token: 0x0400489F RID: 18591
		private string paintedText = string.Empty;

		// Token: 0x040048A0 RID: 18592
		private int caretPos;

		// Token: 0x040048A1 RID: 18593
		private int counter;

		// Token: 0x040048A2 RID: 18594
		private int maxTextLenght = 500;

		// Token: 0x040048A3 RID: 18595
		private int offsetX;

		// Token: 0x040048A4 RID: 18596
		private static int lastKey = -1984;

		// Token: 0x040048A5 RID: 18597
		private int keyInActiveState;

		// Token: 0x040048A6 RID: 18598
		private int indexOfActiveChar;

		// Token: 0x040048A7 RID: 18599
		private int showCaretCounter = TField.MAX_SHOW_CARET_COUNER;

		// Token: 0x040048A8 RID: 18600
		private int inputType = TField.INPUT_TYPE_ANY;

		// Token: 0x040048A9 RID: 18601
		public static bool isQwerty = true;

		// Token: 0x040048AA RID: 18602
		public static int typingModeAreaWidth;

		// Token: 0x040048AB RID: 18603
		public static int mode = 0;

		// Token: 0x040048AC RID: 18604
		public static long timeChangeMode;

		// Token: 0x040048AD RID: 18605
		public static readonly string[] modeNotify = new string[]
		{
			"abc",
			"Abc",
			"ABC",
			"123"
		};

		// Token: 0x040048AE RID: 18606
		public static readonly int NOKIA = 0;

		// Token: 0x040048AF RID: 18607
		public static readonly int MOTO = 1;

		// Token: 0x040048B0 RID: 18608
		public static readonly int ORTHER = 2;

		// Token: 0x040048B1 RID: 18609
		public static readonly int BB = 3;

		// Token: 0x040048B2 RID: 18610
		public static int changeModeKey = 11;

		// Token: 0x040048B3 RID: 18611
		public static readonly sbyte abc = 0;

		// Token: 0x040048B4 RID: 18612
		public static readonly sbyte Abc = 1;

		// Token: 0x040048B5 RID: 18613
		public static readonly sbyte ABC = 2;

		// Token: 0x040048B6 RID: 18614
		public static readonly sbyte number123 = 3;

		// Token: 0x040048B7 RID: 18615
		public static TField currentTField;

		// Token: 0x040048B8 RID: 18616
		public bool isTfield;

		// Token: 0x040048B9 RID: 18617
		public bool isPaintMouse = true;

		// Token: 0x040048BA RID: 18618
		public string name = string.Empty;

		// Token: 0x040048BB RID: 18619
		public string title = string.Empty;

		// Token: 0x040048BC RID: 18620
		public string strInfo;

		// Token: 0x040048BD RID: 18621
		public Command cmdClear;

		// Token: 0x040048BE RID: 18622
		public Command cmdDoneAction;

		// Token: 0x040048BF RID: 18623
		private mScreen parentScr;

		// Token: 0x040048C0 RID: 18624
		private int timeDelayKyCode;

		// Token: 0x040048C1 RID: 18625
		private int holdCount;

		// Token: 0x040048C2 RID: 18626
		public static int changeDau;

		// Token: 0x040048C3 RID: 18627
		private int indexDau = -1;

		// Token: 0x040048C4 RID: 18628
		private int indexTemplate;

		// Token: 0x040048C5 RID: 18629
		private int indexCong;

		// Token: 0x040048C6 RID: 18630
		private long timeDau;

		// Token: 0x040048C7 RID: 18631
		private static string printDau = "aáàảãạâấầẩẫậăắằẳẵặeéèẻẽẹêếềểễệiíìỉĩịoóòỏõọôốồổỗộơớờởỡợuúùủũụưứừửữựyýỳỷỹỵ";

		// Token: 0x040048C8 RID: 18632
		public static Image imgTf;

		// Token: 0x040048C9 RID: 18633
		public int timePutKeyClearAll;

		// Token: 0x040048CA RID: 18634
		public int timeClearFirt;

		// Token: 0x040048CB RID: 18635
		public bool isPaintCarret;

		// Token: 0x040048CC RID: 18636
		public bool showSubTextField = true;

		// Token: 0x040048CD RID: 18637
		public static TouchScreenKeyboard kb;

		// Token: 0x040048CE RID: 18638
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

		// Token: 0x040048CF RID: 18639
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

		// Token: 0x040048D0 RID: 18640
		private string telexBuffer = "";

		// Token: 0x040048D1 RID: 18641
		private const int TELEX_BUFFER_LENGTH = 5;
	}
}
