using System;
using UnityEngine;

namespace Game6
{
	// Token: 0x0200006A RID: 106
	public class mFont
	{
		// Token: 0x06000428 RID: 1064 RVA: 0x0004B398 File Offset: 0x00049598
		public mFont(string strFont, string pathImage, string pathData, int space)
		{
			try
			{
				this.strFont = strFont;
				this.space = space;
				this.pathImage = pathImage;
				DataInputStream dataInputStream = null;
				this.reloadImage();
				try
				{
					dataInputStream = MyStream.readFile(pathData);
					this.fImages = new int[(int)dataInputStream.readShort()][];
					for (int i = 0; i < this.fImages.Length; i++)
					{
						this.fImages[i] = new int[4];
						this.fImages[i][0] = (int)dataInputStream.readShort();
						this.fImages[i][1] = (int)dataInputStream.readShort();
						this.fImages[i][2] = (int)dataInputStream.readShort();
						this.fImages[i][3] = (int)dataInputStream.readShort();
						this.setHeight(this.fImages[i][3]);
					}
					dataInputStream.close();
				}
				catch (Exception)
				{
					try
					{
						dataInputStream.close();
					}
					catch (Exception ex)
					{
						ex.StackTrace.ToString();
					}
				}
			}
			catch (Exception ex2)
			{
				ex2.StackTrace.ToString();
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0004B4D8 File Offset: 0x000496D8
		public mFont(sbyte id)
		{
			string text = "chelthm";
			if ((id > 0 && id < 10) || id == 19)
			{
				this.yAdd = 1;
				text = "barmeneb";
			}
			else if (id >= 10 && id <= 18)
			{
				text = "chelthm";
				this.yAdd = 2;
			}
			else if (id > 24)
			{
				text = "staccato";
			}
			this.id = id;
			text = "FontSys/x" + mGraphics.zoomLevel.ToString() + "/" + text;
			this.myFont = (Font)Resources.Load(text);
			if (id < 25)
			{
				this.color1 = this.setColorFont(id);
				this.color2 = this.setColorFont(id);
			}
			else
			{
				this.color1 = this.bigColor((int)id);
				this.color2 = this.bigColor((int)id);
			}
			this.wO = this.getWidthExactOf("o");
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0004B5E0 File Offset: 0x000497E0
		public static void init()
		{
			if (mGraphics.zoomLevel == 1)
			{
				mFont.tahoma_7b_red = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_red.png", "/myfont/tahoma_7b", 0);
				mFont.tahoma_7b_blue = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_blue.png", "/myfont/tahoma_7b", 0);
				mFont.tahoma_7b_white = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_white.png", "/myfont/tahoma_7b", 0);
				mFont.tahoma_7b_yellow = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_yellow.png", "/myfont/tahoma_7b", 0);
				mFont.tahoma_7b_yellowSmall = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_yellow.png", "/myfont/tahoma_7b", 0);
				mFont.tahoma_7b_dark = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_brown.png", "/myfont/tahoma_7b", 0);
				mFont.tahoma_7b_green2 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_green2.png", "/myfont/tahoma_7b", 0);
				mFont.tahoma_7b_green = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_green.png", "/myfont/tahoma_7b", 0);
				mFont.tahoma_7b_focus = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_focus.png", "/myfont/tahoma_7b", 0);
				mFont.tahoma_7b_unfocus = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_unfocus.png", "/myfont/tahoma_7b", 0);
				mFont.tahoma_7 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7.png", "/myfont/tahoma_7", 0);
				mFont.tahoma_7_blue1 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_blue1.png", "/myfont/tahoma_7", 0);
				mFont.tahoma_7_green2 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_green2.png", "/myfont/tahoma_7", 0);
				mFont.tahoma_7_yellow = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_yellow.png", "/myfont/tahoma_7", 0);
				mFont.tahoma_7_orange = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_orange.png", "/myfont/tahoma_7", 0);
				mFont.tahoma_7_grey = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_grey.png", "/myfont/tahoma_7", 0);
				mFont.tahoma_7_red = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_red.png", "/myfont/tahoma_7", 0);
				mFont.tahoma_7_blue = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_blue.png", "/myfont/tahoma_7", 0);
				mFont.tahoma_7_green = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_green.png", "/myfont/tahoma_7", 0);
				mFont.tahoma_7_white = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_white.png", "/myfont/tahoma_7", 0);
				mFont.tahoma_8b = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_8b.png", "/myfont/tahoma_8b", -1);
				mFont.number_yellow = new mFont(" 0123456789+-", "/myfont/number_yellow.png", "/myfont/number", 0);
				mFont.number_red = new mFont(" 0123456789+-", "/myfont/number_red.png", "/myfont/number", 0);
				mFont.number_green = new mFont(" 0123456789+-", "/myfont/number_green.png", "/myfont/number", 0);
				mFont.number_gray = new mFont(" 0123456789+-", "/myfont/number_gray.png", "/myfont/number", 0);
				mFont.number_orange = new mFont(" 0123456789+-", "/myfont/number_orange.png", "/myfont/number", 0);
				mFont.bigNumber_red = mFont.number_red;
				mFont.bigNumber_While = mFont.tahoma_7b_white;
				mFont.bigNumber_yellow = mFont.number_yellow;
				mFont.bigNumber_green = mFont.number_green;
				mFont.bigNumber_orange = mFont.number_orange;
				mFont.bigNumber_blue = mFont.tahoma_7_blue1;
				mFont.nameFontRed = mFont.tahoma_7_red;
				mFont.nameFontYellow = mFont.tahoma_7_yellow;
				mFont.nameFontGreen = mFont.tahoma_7_green;
				mFont.tahoma_7_greySmall = mFont.tahoma_7_grey;
				mFont.tahoma_7b_yellowSmall2 = mFont.tahoma_7_yellow;
				mFont.tahoma_7b_green2Small = mFont.tahoma_7b_green2;
				mFont.tahoma_7_whiteSmall = mFont.tahoma_7_white;
				mFont.tahoma_7b_greenSmall = mFont.tahoma_7b_green;
				mFont.tahoma_7_blue1Small = mFont.tahoma_7_blue1;
				return;
			}
			mFont.gI = new mFont(0);
			mFont.tahoma_7b_red = new mFont(1);
			mFont.tahoma_7b_blue = new mFont(2);
			mFont.tahoma_7b_white = new mFont(3);
			mFont.tahoma_7b_yellow = new mFont(4);
			mFont.tahoma_7b_yellowSmall = new mFont(4);
			mFont.tahoma_7b_dark = new mFont(5);
			mFont.tahoma_7b_green2 = new mFont(6);
			mFont.tahoma_7b_green = new mFont(7);
			mFont.tahoma_7b_focus = new mFont(8);
			mFont.tahoma_7b_unfocus = new mFont(9);
			mFont.tahoma_7 = new mFont(10);
			mFont.tahoma_7_blue1 = new mFont(11);
			mFont.tahoma_7_blue1Small = mFont.tahoma_7_blue1;
			mFont.tahoma_7_green2 = new mFont(12);
			mFont.tahoma_7_yellow = new mFont(13);
			mFont.tahoma_7_orange = new mFont(24);
			mFont.tahoma_7_grey = new mFont(14);
			mFont.tahoma_7_red = new mFont(15);
			mFont.tahoma_7_blue = new mFont(16);
			mFont.tahoma_7_green = new mFont(17);
			mFont.tahoma_7_white = new mFont(18);
			mFont.tahoma_8b = new mFont(19);
			mFont.number_yellow = new mFont(20);
			mFont.number_red = new mFont(21);
			mFont.number_green = new mFont(22);
			mFont.number_gray = new mFont(23);
			mFont.number_orange = new mFont(24);
			mFont.bigNumber_red = new mFont(25);
			mFont.bigNumber_yellow = new mFont(26);
			mFont.bigNumber_green = new mFont(27);
			mFont.bigNumber_While = new mFont(28);
			mFont.bigNumber_blue = new mFont(29);
			mFont.bigNumber_orange = new mFont(30);
			mFont.bigNumber_black = new mFont(31);
			mFont.nameFontRed = mFont.tahoma_7b_red;
			mFont.nameFontYellow = mFont.tahoma_7_yellow;
			mFont.nameFontGreen = mFont.tahoma_7_green;
			mFont.tahoma_7_greySmall = mFont.tahoma_7_grey;
			mFont.tahoma_7b_yellowSmall2 = mFont.tahoma_7_yellow;
			mFont.tahoma_7b_green2Small = mFont.tahoma_7b_green2;
			mFont.tahoma_7_whiteSmall = mFont.tahoma_7_white;
			mFont.tahoma_7b_greenSmall = mFont.tahoma_7b_green;
			mFont.yAddFont = 1;
			if (mGraphics.zoomLevel == 1)
			{
				mFont.yAddFont = -3;
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0004BB30 File Offset: 0x00049D30
		public void setHeight(int height)
		{
			this.height = height;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0004BB3C File Offset: 0x00049D3C
		public Color setColor(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float b = (float)num / 256f;
			float g = (float)num2 / 256f;
			return new Color(num3 / 256f, g, b);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0004BB88 File Offset: 0x00049D88
		public Color bigColor(int id)
		{
			return (new Color[]
			{
				Color.red,
				Color.yellow,
				Color.green,
				Color.white,
				this.setColor(40404),
				Color.red,
				Color.black
			})[id - 25];
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0004BBFE File Offset: 0x00049DFE
		public void setColorByID(int ID)
		{
			this.color1 = this.setColor(mFont.colorJava[ID]);
			this.color2 = this.setColor(mFont.colorJava[ID]);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0004BC28 File Offset: 0x00049E28
		public void setTypePaint(mGraphics g, string st, int x, int y, int align, sbyte idFont)
		{
			sbyte b = this.id;
			if (idFont > 0)
			{
				b = idFont;
			}
			x--;
			if (this.id > 24)
			{
				Color[] array = new Color[]
				{
					this.setColor(6029312),
					this.setColor(7169025),
					this.setColor(7680),
					this.setColor(0),
					this.setColor(9264),
					this.setColor(6029312)
				};
				this.color1 = array[(int)(this.id - 25)];
				this.color2 = array[(int)(this.id - 25)];
				this._drawString(g, st, x + 1, y, align);
				this._drawString(g, st, x - 1, y, align);
				this._drawString(g, st, x, y - 1, align);
				this._drawString(g, st, x, y + 1, align);
				this._drawString(g, st, x + 1, y + 1, align);
				this._drawString(g, st, x + 1, y - 1, align);
				this._drawString(g, st, x - 1, y - 1, align);
				this._drawString(g, st, x - 1, y + 1, align);
				this.color1 = this.bigColor((int)this.id);
				this.color2 = this.bigColor((int)this.id);
			}
			else
			{
				this.setColorByID((int)b);
			}
			this._drawString(g, st, x, y - this.yAdd, align);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0004BDB0 File Offset: 0x00049FB0
		public Color setColorFont(sbyte id)
		{
			return this.setColor(mFont.colorJava[(int)id]);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0004BDC0 File Offset: 0x00049FC0
		public void drawString(mGraphics g, string st, int x, int y, int align)
		{
			if (mGraphics.zoomLevel == 1)
			{
				int length = st.Length;
				int num5;
				if (align != 0)
				{
					if (align != 1)
					{
						num5 = x - (this.getWidth(st) >> 1);
					}
					else
					{
						num5 = x - this.getWidth(st);
					}
				}
				else
				{
					num5 = x;
				}
				int num = num5;
				for (int i = 0; i < length; i++)
				{
					int num2 = this.strFont.IndexOf(st[i].ToString() + string.Empty);
					if (num2 == -1)
					{
						num2 = 0;
					}
					if (num2 > -1)
					{
						int x2 = this.fImages[num2][0];
						int num3 = this.fImages[num2][1];
						int w = this.fImages[num2][2];
						int num4 = this.fImages[num2][3];
						if (num3 + num4 > this.imgFont.texture.height)
						{
							num3 -= this.imgFont.texture.height;
							x2 = this.imgFont.texture.width / 2;
						}
						g.drawRegion(this.imgFont, x2, num3, w, num4, 0, num, y, 20);
					}
					num += this.fImages[num2][2] + this.space;
				}
				return;
			}
			this.setTypePaint(g, st, x, y, align, 0);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0004BF03 File Offset: 0x0004A103
		public void drawStringBorder(mGraphics g, string st, int x, int y, int align)
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.drawString(g, st, x, y, align);
				return;
			}
			this.setTypePaint(g, st, x, y, align, 0);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0004BF29 File Offset: 0x0004A129
		public void drawStringBorder(mGraphics g, string st, int x, int y, int align, mFont font2)
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.drawString(g, st, x, y, align, font2);
				return;
			}
			this.drawStringBd(g, st, x, y, align, font2);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0004BF54 File Offset: 0x0004A154
		public void drawStringBd(mGraphics g, string st, int x, int y, int align, mFont font)
		{
			this.setTypePaint(g, st, x - 1, y - 1, align, font.id);
			this.setTypePaint(g, st, x - 1, y + 1, align, font.id);
			this.setTypePaint(g, st, x + 1, y - 1, align, font.id);
			this.setTypePaint(g, st, x + 1, y + 1, align, font.id);
			this.setTypePaint(g, st, x, y - 1, align, font.id);
			this.setTypePaint(g, st, x, y + 1, align, font.id);
			this.setTypePaint(g, st, x + 1, y, align, font.id);
			this.setTypePaint(g, st, x - 1, y, align, font.id);
			this.setTypePaint(g, st, x, y, align, 0);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0004C028 File Offset: 0x0004A228
		public void drawString(mGraphics g, string st, int x, int y, int align, mFont font)
		{
			if (mGraphics.zoomLevel == 1)
			{
				int length = st.Length;
				int num5;
				if (align != 0)
				{
					if (align != 1)
					{
						num5 = x - (this.getWidth(st) >> 1);
					}
					else
					{
						num5 = x - this.getWidth(st);
					}
				}
				else
				{
					num5 = x;
				}
				int num = num5;
				for (int i = 0; i < length; i++)
				{
					int num2 = this.strFont.IndexOf(st[i]);
					if (num2 == -1)
					{
						num2 = 0;
					}
					if (num2 > -1)
					{
						int x2 = this.fImages[num2][0];
						int num3 = this.fImages[num2][1];
						int w = this.fImages[num2][2];
						int num4 = this.fImages[num2][3];
						if (num3 + num4 > this.imgFont.texture.height)
						{
							num3 -= this.imgFont.texture.height;
							x2 = this.imgFont.texture.width / 2;
						}
						if (!GameCanvas.lowGraphic && font != null)
						{
							g.drawRegion(font.imgFont, x2, num3, w, num4, 0, num + 1, y, 20);
							g.drawRegion(font.imgFont, x2, num3, w, num4, 0, num, y + 1, 20);
						}
						g.drawRegion(this.imgFont, x2, num3, w, num4, 0, num, y, 20);
					}
					num += this.fImages[num2][2] + this.space;
				}
				return;
			}
			this.setTypePaint(g, st, x, y + 1, align, font.id);
			this.setTypePaint(g, st, x, y, align, 0);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0004C1B4 File Offset: 0x0004A3B4
		public MyVector splitFontVector(string src, int lineWidth)
		{
			MyVector myVector = new MyVector();
			string text = string.Empty;
			for (int i = 0; i < src.Length; i++)
			{
				if (src[i] == '\n' || src[i] == '\b')
				{
					myVector.addElement(text);
					text = string.Empty;
				}
				else
				{
					text += src[i].ToString();
					if (this.getWidth(text) > lineWidth)
					{
						int num = text.Length - 1;
						while (num >= 0 && text[num] != ' ')
						{
							num--;
						}
						if (num < 0)
						{
							num = text.Length - 1;
						}
						myVector.addElement(text.Substring(0, num));
						i = i - (text.Length - num) + 1;
						text = string.Empty;
					}
					if (i == src.Length - 1 && !text.Trim().Equals(string.Empty))
					{
						myVector.addElement(text);
					}
				}
			}
			return myVector;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0004C2A8 File Offset: 0x0004A4A8
		public string[] splitFontArray(string src, int lineWidth)
		{
			MyVector myVector = this.splitFontVector(src, lineWidth);
			string[] array = new string[myVector.size()];
			for (int i = 0; i < myVector.size(); i++)
			{
				array[i] = (string)myVector.elementAt(i);
			}
			return array;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0004C2EC File Offset: 0x0004A4EC
		public int getWidth(string s)
		{
			if (mGraphics.zoomLevel == 1)
			{
				int num = 0;
				for (int i = 0; i < s.Length; i++)
				{
					int num2 = this.strFont.IndexOf(s[i]);
					if (num2 == -1)
					{
						num2 = 0;
					}
					num += this.fImages[num2][2] + this.space;
				}
				return num;
			}
			return this.getWidthExactOf(s);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0004C34C File Offset: 0x0004A54C
		public int getWidthExactOf(string s)
		{
			int result;
			try
			{
				result = (int)new GUIStyle
				{
					font = this.myFont
				}.CalcSize(new GUIContent(s)).x / mGraphics.zoomLevel;
			}
			catch (Exception ex)
			{
				Cout.LogError(string.Concat(new string[]
				{
					"GET WIDTH OF ",
					s,
					" FAIL.\n",
					ex.Message,
					"\n",
					ex.StackTrace
				}));
				result = this.getWidthNotExactOf(s);
			}
			return result;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0004C3E0 File Offset: 0x0004A5E0
		public int getWidthNotExactOf(string s)
		{
			return s.Length * this.wO / mGraphics.zoomLevel;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0004C3F8 File Offset: 0x0004A5F8
		public int getHeight()
		{
			if (mGraphics.zoomLevel == 1)
			{
				return this.height;
			}
			if (this.height > 0)
			{
				return this.height / mGraphics.zoomLevel;
			}
			GUIStyle gUIStyle = new GUIStyle();
			gUIStyle.font = this.myFont;
			try
			{
				this.height = (int)gUIStyle.CalcSize(new GUIContent("Adg")).y + 2;
			}
			catch (Exception ex)
			{
				Cout.LogError("FAIL GET HEIGHT " + ex.StackTrace);
				this.height = 20;
			}
			return this.height / mGraphics.zoomLevel;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0004C498 File Offset: 0x0004A698
		public void _drawString(mGraphics g, string st, int x0, int y0, int align)
		{
			y0 += mFont.yAddFont;
			GUIStyle gUIStyle = new GUIStyle(GUI.skin.label);
			gUIStyle.font = this.myFont;
			float num = 0f;
			float num2 = 0f;
			switch (align)
			{
			case 0:
				num = (float)x0;
				num2 = (float)y0;
				gUIStyle.alignment = TextAnchor.UpperLeft;
				break;
			case 1:
				num = (float)(x0 - GameCanvas.w);
				num2 = (float)y0;
				gUIStyle.alignment = TextAnchor.UpperRight;
				break;
			case 2:
			case 3:
				num = (float)(x0 - GameCanvas.w / 2);
				num2 = (float)y0;
				gUIStyle.alignment = TextAnchor.UpperCenter;
				break;
			}
			gUIStyle.normal.textColor = this.color1;
			g.drawString(st, (int)num, (int)num2, gUIStyle);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0004C549 File Offset: 0x0004A749
		public void reloadImage()
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.imgFont = GameCanvas.loadImage(this.pathImage);
			}
		}

		// Token: 0x04000916 RID: 2326
		public static int LEFT = 0;

		// Token: 0x04000917 RID: 2327
		public static int RIGHT = 1;

		// Token: 0x04000918 RID: 2328
		public static int CENTER = 2;

		// Token: 0x04000919 RID: 2329
		public static int RED = 0;

		// Token: 0x0400091A RID: 2330
		public static int YELLOW = 1;

		// Token: 0x0400091B RID: 2331
		public static int GREEN = 2;

		// Token: 0x0400091C RID: 2332
		public static int FATAL = 3;

		// Token: 0x0400091D RID: 2333
		public static int MISS = 4;

		// Token: 0x0400091E RID: 2334
		public static int ORANGE = 5;

		// Token: 0x0400091F RID: 2335
		public static int ADDMONEY = 6;

		// Token: 0x04000920 RID: 2336
		public static int MISS_ME = 7;

		// Token: 0x04000921 RID: 2337
		public static int FATAL_ME = 8;

		// Token: 0x04000922 RID: 2338
		public static int HP = 9;

		// Token: 0x04000923 RID: 2339
		public static int MP = 10;

		// Token: 0x04000924 RID: 2340
		private int space;

		// Token: 0x04000925 RID: 2341
		private Image imgFont;

		// Token: 0x04000926 RID: 2342
		private string strFont;

		// Token: 0x04000927 RID: 2343
		private int[][] fImages;

		// Token: 0x04000928 RID: 2344
		public static int yAddFont;

		// Token: 0x04000929 RID: 2345
		public static int[] colorJava = new int[]
		{
			0,
			16711680,
			6520319,
			16777215,
			16755200,
			5449989,
			21285,
			52224,
			7386228,
			16771788,
			0,
			65535,
			21285,
			16776960,
			5592405,
			16742263,
			33023,
			8701737,
			15723503,
			7999781,
			16768815,
			14961237,
			4124899,
			4671303,
			16096312,
			16711680,
			16755200,
			52224,
			16777215,
			6520319,
			16096312
		};

		// Token: 0x0400092A RID: 2346
		public static mFont gI;

		// Token: 0x0400092B RID: 2347
		public static mFont tahoma_7b_red;

		// Token: 0x0400092C RID: 2348
		public static mFont tahoma_7b_blue;

		// Token: 0x0400092D RID: 2349
		public static mFont tahoma_7b_white;

		// Token: 0x0400092E RID: 2350
		public static mFont tahoma_7b_yellow;

		// Token: 0x0400092F RID: 2351
		public static mFont tahoma_7b_yellowSmall;

		// Token: 0x04000930 RID: 2352
		public static mFont tahoma_7b_dark;

		// Token: 0x04000931 RID: 2353
		public static mFont tahoma_7b_green2;

		// Token: 0x04000932 RID: 2354
		public static mFont tahoma_7b_green;

		// Token: 0x04000933 RID: 2355
		public static mFont tahoma_7b_focus;

		// Token: 0x04000934 RID: 2356
		public static mFont tahoma_7b_pink;

		// Token: 0x04000935 RID: 2357
		public static mFont tahoma_7b_unfocus;

		// Token: 0x04000936 RID: 2358
		public static mFont tahoma_7;

		// Token: 0x04000937 RID: 2359
		public static mFont tahoma_7_blue1;

		// Token: 0x04000938 RID: 2360
		public static mFont tahoma_7_blue1Small;

		// Token: 0x04000939 RID: 2361
		public static mFont tahoma_7_green2;

		// Token: 0x0400093A RID: 2362
		public static mFont tahoma_7_yellow;

		// Token: 0x0400093B RID: 2363
		public static mFont tahoma_7_orange;

		// Token: 0x0400093C RID: 2364
		public static mFont tahoma_7_grey;

		// Token: 0x0400093D RID: 2365
		public static mFont tahoma_7_red;

		// Token: 0x0400093E RID: 2366
		public static mFont tahoma_7_blue;

		// Token: 0x0400093F RID: 2367
		public static mFont tahoma_7_green;

		// Token: 0x04000940 RID: 2368
		public static mFont tahoma_7_white;

		// Token: 0x04000941 RID: 2369
		public static mFont tahoma_8b;

		// Token: 0x04000942 RID: 2370
		public static mFont number_yellow;

		// Token: 0x04000943 RID: 2371
		public static mFont number_red;

		// Token: 0x04000944 RID: 2372
		public static mFont number_green;

		// Token: 0x04000945 RID: 2373
		public static mFont number_gray;

		// Token: 0x04000946 RID: 2374
		public static mFont number_orange;

		// Token: 0x04000947 RID: 2375
		public static mFont bigNumber_red;

		// Token: 0x04000948 RID: 2376
		public static mFont bigNumber_While;

		// Token: 0x04000949 RID: 2377
		public static mFont bigNumber_yellow;

		// Token: 0x0400094A RID: 2378
		public static mFont bigNumber_green;

		// Token: 0x0400094B RID: 2379
		public static mFont bigNumber_orange;

		// Token: 0x0400094C RID: 2380
		public static mFont bigNumber_blue;

		// Token: 0x0400094D RID: 2381
		public static mFont bigNumber_black;

		// Token: 0x0400094E RID: 2382
		public static mFont nameFontRed;

		// Token: 0x0400094F RID: 2383
		public static mFont nameFontYellow;

		// Token: 0x04000950 RID: 2384
		public static mFont nameFontGreen;

		// Token: 0x04000951 RID: 2385
		public static mFont tahoma_7_greySmall;

		// Token: 0x04000952 RID: 2386
		public static mFont tahoma_7b_yellowSmall2;

		// Token: 0x04000953 RID: 2387
		public static mFont tahoma_7b_green2Small;

		// Token: 0x04000954 RID: 2388
		public static mFont tahoma_7_whiteSmall;

		// Token: 0x04000955 RID: 2389
		public static mFont tahoma_7b_greenSmall;

		// Token: 0x04000956 RID: 2390
		public Font myFont;

		// Token: 0x04000957 RID: 2391
		private int height;

		// Token: 0x04000958 RID: 2392
		private int wO;

		// Token: 0x04000959 RID: 2393
		public Color color1 = Color.white;

		// Token: 0x0400095A RID: 2394
		public Color color2 = Color.gray;

		// Token: 0x0400095B RID: 2395
		public sbyte id;

		// Token: 0x0400095C RID: 2396
		public int fstyle;

		// Token: 0x0400095D RID: 2397
		public string st1 = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";

		// Token: 0x0400095E RID: 2398
		public string st2 = "¸µ¶·¹¨¾»¼½Æ©ÊÇÈÉËÐÌÎÏÑªÕÒÓÔÖÝ×ØÜÞãßáâä«èåæçé¬íêëìîóïñòô­øõö÷ùýúûüþ®¸µ¶·¹¡¾»¼½Æ¢ÊÇÈÉËÐÌÎÏÑ£ÕÒÓÔÖÝ×ØÜÞãßáâä¤èåæçé¥íêëìîóïñòô¦øõö÷ùýúûüþ§";

		// Token: 0x0400095F RID: 2399
		public const string str = " 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW";

		// Token: 0x04000960 RID: 2400
		private int yAdd;

		// Token: 0x04000961 RID: 2401
		private string pathImage;
	}
}
