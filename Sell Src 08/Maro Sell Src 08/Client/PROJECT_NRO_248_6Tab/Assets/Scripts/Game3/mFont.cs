using System;
using UnityEngine;

namespace Game3
{
	// Token: 0x020002F2 RID: 754
	public class mFont
	{
		// Token: 0x06002114 RID: 8468 RVA: 0x0020A65C File Offset: 0x0020885C
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

		// Token: 0x06002115 RID: 8469 RVA: 0x0020A79C File Offset: 0x0020899C
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

		// Token: 0x06002116 RID: 8470 RVA: 0x0020A8A4 File Offset: 0x00208AA4
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

		// Token: 0x06002117 RID: 8471 RVA: 0x0020ADF4 File Offset: 0x00208FF4
		public void setHeight(int height)
		{
			this.height = height;
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x0020AE00 File Offset: 0x00209000
		public Color setColor(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float b = (float)num / 256f;
			float g = (float)num2 / 256f;
			return new Color(num3 / 256f, g, b);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0020AE4C File Offset: 0x0020904C
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

		// Token: 0x0600211A RID: 8474 RVA: 0x0020AEC2 File Offset: 0x002090C2
		public void setColorByID(int ID)
		{
			this.color1 = this.setColor(mFont.colorJava[ID]);
			this.color2 = this.setColor(mFont.colorJava[ID]);
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x0020AEEC File Offset: 0x002090EC
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

		// Token: 0x0600211C RID: 8476 RVA: 0x0020B074 File Offset: 0x00209274
		public Color setColorFont(sbyte id)
		{
			return this.setColor(mFont.colorJava[(int)id]);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0020B084 File Offset: 0x00209284
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

		// Token: 0x0600211E RID: 8478 RVA: 0x0020B1C7 File Offset: 0x002093C7
		public void drawStringBorder(mGraphics g, string st, int x, int y, int align)
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.drawString(g, st, x, y, align);
				return;
			}
			this.setTypePaint(g, st, x, y, align, 0);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0020B1ED File Offset: 0x002093ED
		public void drawStringBorder(mGraphics g, string st, int x, int y, int align, mFont font2)
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.drawString(g, st, x, y, align, font2);
				return;
			}
			this.drawStringBd(g, st, x, y, align, font2);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0020B218 File Offset: 0x00209418
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

		// Token: 0x06002121 RID: 8481 RVA: 0x0020B2EC File Offset: 0x002094EC
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

		// Token: 0x06002122 RID: 8482 RVA: 0x0020B478 File Offset: 0x00209678
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

		// Token: 0x06002123 RID: 8483 RVA: 0x0020B56C File Offset: 0x0020976C
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

		// Token: 0x06002124 RID: 8484 RVA: 0x0020B5B0 File Offset: 0x002097B0
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

		// Token: 0x06002125 RID: 8485 RVA: 0x0020B610 File Offset: 0x00209810
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

		// Token: 0x06002126 RID: 8486 RVA: 0x0020B6A4 File Offset: 0x002098A4
		public int getWidthNotExactOf(string s)
		{
			return s.Length * this.wO / mGraphics.zoomLevel;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0020B6BC File Offset: 0x002098BC
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

		// Token: 0x06002128 RID: 8488 RVA: 0x0020B75C File Offset: 0x0020995C
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

		// Token: 0x06002129 RID: 8489 RVA: 0x0020B80D File Offset: 0x00209A0D
		public void reloadImage()
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.imgFont = GameCanvas.loadImage(this.pathImage);
			}
		}

		// Token: 0x04004093 RID: 16531
		public static int LEFT = 0;

		// Token: 0x04004094 RID: 16532
		public static int RIGHT = 1;

		// Token: 0x04004095 RID: 16533
		public static int CENTER = 2;

		// Token: 0x04004096 RID: 16534
		public static int RED = 0;

		// Token: 0x04004097 RID: 16535
		public static int YELLOW = 1;

		// Token: 0x04004098 RID: 16536
		public static int GREEN = 2;

		// Token: 0x04004099 RID: 16537
		public static int FATAL = 3;

		// Token: 0x0400409A RID: 16538
		public static int MISS = 4;

		// Token: 0x0400409B RID: 16539
		public static int ORANGE = 5;

		// Token: 0x0400409C RID: 16540
		public static int ADDMONEY = 6;

		// Token: 0x0400409D RID: 16541
		public static int MISS_ME = 7;

		// Token: 0x0400409E RID: 16542
		public static int FATAL_ME = 8;

		// Token: 0x0400409F RID: 16543
		public static int HP = 9;

		// Token: 0x040040A0 RID: 16544
		public static int MP = 10;

		// Token: 0x040040A1 RID: 16545
		private int space;

		// Token: 0x040040A2 RID: 16546
		private Image imgFont;

		// Token: 0x040040A3 RID: 16547
		private string strFont;

		// Token: 0x040040A4 RID: 16548
		private int[][] fImages;

		// Token: 0x040040A5 RID: 16549
		public static int yAddFont;

		// Token: 0x040040A6 RID: 16550
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

		// Token: 0x040040A7 RID: 16551
		public static mFont gI;

		// Token: 0x040040A8 RID: 16552
		public static mFont tahoma_7b_red;

		// Token: 0x040040A9 RID: 16553
		public static mFont tahoma_7b_blue;

		// Token: 0x040040AA RID: 16554
		public static mFont tahoma_7b_white;

		// Token: 0x040040AB RID: 16555
		public static mFont tahoma_7b_yellow;

		// Token: 0x040040AC RID: 16556
		public static mFont tahoma_7b_yellowSmall;

		// Token: 0x040040AD RID: 16557
		public static mFont tahoma_7b_dark;

		// Token: 0x040040AE RID: 16558
		public static mFont tahoma_7b_green2;

		// Token: 0x040040AF RID: 16559
		public static mFont tahoma_7b_green;

		// Token: 0x040040B0 RID: 16560
		public static mFont tahoma_7b_focus;

		// Token: 0x040040B1 RID: 16561
		public static mFont tahoma_7b_pink;

		// Token: 0x040040B2 RID: 16562
		public static mFont tahoma_7b_unfocus;

		// Token: 0x040040B3 RID: 16563
		public static mFont tahoma_7;

		// Token: 0x040040B4 RID: 16564
		public static mFont tahoma_7_blue1;

		// Token: 0x040040B5 RID: 16565
		public static mFont tahoma_7_blue1Small;

		// Token: 0x040040B6 RID: 16566
		public static mFont tahoma_7_green2;

		// Token: 0x040040B7 RID: 16567
		public static mFont tahoma_7_yellow;

		// Token: 0x040040B8 RID: 16568
		public static mFont tahoma_7_orange;

		// Token: 0x040040B9 RID: 16569
		public static mFont tahoma_7_grey;

		// Token: 0x040040BA RID: 16570
		public static mFont tahoma_7_red;

		// Token: 0x040040BB RID: 16571
		public static mFont tahoma_7_blue;

		// Token: 0x040040BC RID: 16572
		public static mFont tahoma_7_green;

		// Token: 0x040040BD RID: 16573
		public static mFont tahoma_7_white;

		// Token: 0x040040BE RID: 16574
		public static mFont tahoma_8b;

		// Token: 0x040040BF RID: 16575
		public static mFont number_yellow;

		// Token: 0x040040C0 RID: 16576
		public static mFont number_red;

		// Token: 0x040040C1 RID: 16577
		public static mFont number_green;

		// Token: 0x040040C2 RID: 16578
		public static mFont number_gray;

		// Token: 0x040040C3 RID: 16579
		public static mFont number_orange;

		// Token: 0x040040C4 RID: 16580
		public static mFont bigNumber_red;

		// Token: 0x040040C5 RID: 16581
		public static mFont bigNumber_While;

		// Token: 0x040040C6 RID: 16582
		public static mFont bigNumber_yellow;

		// Token: 0x040040C7 RID: 16583
		public static mFont bigNumber_green;

		// Token: 0x040040C8 RID: 16584
		public static mFont bigNumber_orange;

		// Token: 0x040040C9 RID: 16585
		public static mFont bigNumber_blue;

		// Token: 0x040040CA RID: 16586
		public static mFont bigNumber_black;

		// Token: 0x040040CB RID: 16587
		public static mFont nameFontRed;

		// Token: 0x040040CC RID: 16588
		public static mFont nameFontYellow;

		// Token: 0x040040CD RID: 16589
		public static mFont nameFontGreen;

		// Token: 0x040040CE RID: 16590
		public static mFont tahoma_7_greySmall;

		// Token: 0x040040CF RID: 16591
		public static mFont tahoma_7b_yellowSmall2;

		// Token: 0x040040D0 RID: 16592
		public static mFont tahoma_7b_green2Small;

		// Token: 0x040040D1 RID: 16593
		public static mFont tahoma_7_whiteSmall;

		// Token: 0x040040D2 RID: 16594
		public static mFont tahoma_7b_greenSmall;

		// Token: 0x040040D3 RID: 16595
		public Font myFont;

		// Token: 0x040040D4 RID: 16596
		private int height;

		// Token: 0x040040D5 RID: 16597
		private int wO;

		// Token: 0x040040D6 RID: 16598
		public Color color1 = Color.white;

		// Token: 0x040040D7 RID: 16599
		public Color color2 = Color.gray;

		// Token: 0x040040D8 RID: 16600
		public sbyte id;

		// Token: 0x040040D9 RID: 16601
		public int fstyle;

		// Token: 0x040040DA RID: 16602
		public string st1 = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";

		// Token: 0x040040DB RID: 16603
		public string st2 = "¸µ¶·¹¨¾»¼½Æ©ÊÇÈÉËÐÌÎÏÑªÕÒÓÔÖÝ×ØÜÞãßáâä«èåæçé¬íêëìîóïñòô­øõö÷ùýúûüþ®¸µ¶·¹¡¾»¼½Æ¢ÊÇÈÉËÐÌÎÏÑ£ÕÒÓÔÖÝ×ØÜÞãßáâä¤èåæçé¥íêëìîóïñòô¦øõö÷ùýúûüþ§";

		// Token: 0x040040DC RID: 16604
		public const string str = " 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW";

		// Token: 0x040040DD RID: 16605
		private int yAdd;

		// Token: 0x040040DE RID: 16606
		private string pathImage;
	}
}
