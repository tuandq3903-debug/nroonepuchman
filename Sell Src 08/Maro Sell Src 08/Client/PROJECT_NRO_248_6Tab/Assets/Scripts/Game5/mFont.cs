using System;
using UnityEngine;

namespace Game5
{
	// Token: 0x02000142 RID: 322
	public class mFont
	{
		// Token: 0x06000DCC RID: 3532 RVA: 0x000E0514 File Offset: 0x000DE714
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

		// Token: 0x06000DCD RID: 3533 RVA: 0x000E0654 File Offset: 0x000DE854
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

		// Token: 0x06000DCE RID: 3534 RVA: 0x000E075C File Offset: 0x000DE95C
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

		// Token: 0x06000DCF RID: 3535 RVA: 0x000E0CAC File Offset: 0x000DEEAC
		public void setHeight(int height)
		{
			this.height = height;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000E0CB8 File Offset: 0x000DEEB8
		public Color setColor(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float b = (float)num / 256f;
			float g = (float)num2 / 256f;
			return new Color(num3 / 256f, g, b);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000E0D04 File Offset: 0x000DEF04
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

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000E0D7A File Offset: 0x000DEF7A
		public void setColorByID(int ID)
		{
			this.color1 = this.setColor(mFont.colorJava[ID]);
			this.color2 = this.setColor(mFont.colorJava[ID]);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000E0DA4 File Offset: 0x000DEFA4
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

		// Token: 0x06000DD4 RID: 3540 RVA: 0x000E0F2C File Offset: 0x000DF12C
		public Color setColorFont(sbyte id)
		{
			return this.setColor(mFont.colorJava[(int)id]);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x000E0F3C File Offset: 0x000DF13C
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

		// Token: 0x06000DD6 RID: 3542 RVA: 0x000E107F File Offset: 0x000DF27F
		public void drawStringBorder(mGraphics g, string st, int x, int y, int align)
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.drawString(g, st, x, y, align);
				return;
			}
			this.setTypePaint(g, st, x, y, align, 0);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x000E10A5 File Offset: 0x000DF2A5
		public void drawStringBorder(mGraphics g, string st, int x, int y, int align, mFont font2)
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.drawString(g, st, x, y, align, font2);
				return;
			}
			this.drawStringBd(g, st, x, y, align, font2);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000E10D0 File Offset: 0x000DF2D0
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

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000E11A4 File Offset: 0x000DF3A4
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

		// Token: 0x06000DDA RID: 3546 RVA: 0x000E1330 File Offset: 0x000DF530
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

		// Token: 0x06000DDB RID: 3547 RVA: 0x000E1424 File Offset: 0x000DF624
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

		// Token: 0x06000DDC RID: 3548 RVA: 0x000E1468 File Offset: 0x000DF668
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

		// Token: 0x06000DDD RID: 3549 RVA: 0x000E14C8 File Offset: 0x000DF6C8
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

		// Token: 0x06000DDE RID: 3550 RVA: 0x000E155C File Offset: 0x000DF75C
		public int getWidthNotExactOf(string s)
		{
			return s.Length * this.wO / mGraphics.zoomLevel;
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x000E1574 File Offset: 0x000DF774
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

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000E1614 File Offset: 0x000DF814
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

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000E16C5 File Offset: 0x000DF8C5
		public void reloadImage()
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.imgFont = GameCanvas.loadImage(this.pathImage);
			}
		}

		// Token: 0x04001B95 RID: 7061
		public static int LEFT = 0;

		// Token: 0x04001B96 RID: 7062
		public static int RIGHT = 1;

		// Token: 0x04001B97 RID: 7063
		public static int CENTER = 2;

		// Token: 0x04001B98 RID: 7064
		public static int RED = 0;

		// Token: 0x04001B99 RID: 7065
		public static int YELLOW = 1;

		// Token: 0x04001B9A RID: 7066
		public static int GREEN = 2;

		// Token: 0x04001B9B RID: 7067
		public static int FATAL = 3;

		// Token: 0x04001B9C RID: 7068
		public static int MISS = 4;

		// Token: 0x04001B9D RID: 7069
		public static int ORANGE = 5;

		// Token: 0x04001B9E RID: 7070
		public static int ADDMONEY = 6;

		// Token: 0x04001B9F RID: 7071
		public static int MISS_ME = 7;

		// Token: 0x04001BA0 RID: 7072
		public static int FATAL_ME = 8;

		// Token: 0x04001BA1 RID: 7073
		public static int HP = 9;

		// Token: 0x04001BA2 RID: 7074
		public static int MP = 10;

		// Token: 0x04001BA3 RID: 7075
		private int space;

		// Token: 0x04001BA4 RID: 7076
		private Image imgFont;

		// Token: 0x04001BA5 RID: 7077
		private string strFont;

		// Token: 0x04001BA6 RID: 7078
		private int[][] fImages;

		// Token: 0x04001BA7 RID: 7079
		public static int yAddFont;

		// Token: 0x04001BA8 RID: 7080
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

		// Token: 0x04001BA9 RID: 7081
		public static mFont gI;

		// Token: 0x04001BAA RID: 7082
		public static mFont tahoma_7b_red;

		// Token: 0x04001BAB RID: 7083
		public static mFont tahoma_7b_blue;

		// Token: 0x04001BAC RID: 7084
		public static mFont tahoma_7b_white;

		// Token: 0x04001BAD RID: 7085
		public static mFont tahoma_7b_yellow;

		// Token: 0x04001BAE RID: 7086
		public static mFont tahoma_7b_yellowSmall;

		// Token: 0x04001BAF RID: 7087
		public static mFont tahoma_7b_dark;

		// Token: 0x04001BB0 RID: 7088
		public static mFont tahoma_7b_green2;

		// Token: 0x04001BB1 RID: 7089
		public static mFont tahoma_7b_green;

		// Token: 0x04001BB2 RID: 7090
		public static mFont tahoma_7b_focus;

		// Token: 0x04001BB3 RID: 7091
		public static mFont tahoma_7b_pink;

		// Token: 0x04001BB4 RID: 7092
		public static mFont tahoma_7b_unfocus;

		// Token: 0x04001BB5 RID: 7093
		public static mFont tahoma_7;

		// Token: 0x04001BB6 RID: 7094
		public static mFont tahoma_7_blue1;

		// Token: 0x04001BB7 RID: 7095
		public static mFont tahoma_7_blue1Small;

		// Token: 0x04001BB8 RID: 7096
		public static mFont tahoma_7_green2;

		// Token: 0x04001BB9 RID: 7097
		public static mFont tahoma_7_yellow;

		// Token: 0x04001BBA RID: 7098
		public static mFont tahoma_7_orange;

		// Token: 0x04001BBB RID: 7099
		public static mFont tahoma_7_grey;

		// Token: 0x04001BBC RID: 7100
		public static mFont tahoma_7_red;

		// Token: 0x04001BBD RID: 7101
		public static mFont tahoma_7_blue;

		// Token: 0x04001BBE RID: 7102
		public static mFont tahoma_7_green;

		// Token: 0x04001BBF RID: 7103
		public static mFont tahoma_7_white;

		// Token: 0x04001BC0 RID: 7104
		public static mFont tahoma_8b;

		// Token: 0x04001BC1 RID: 7105
		public static mFont number_yellow;

		// Token: 0x04001BC2 RID: 7106
		public static mFont number_red;

		// Token: 0x04001BC3 RID: 7107
		public static mFont number_green;

		// Token: 0x04001BC4 RID: 7108
		public static mFont number_gray;

		// Token: 0x04001BC5 RID: 7109
		public static mFont number_orange;

		// Token: 0x04001BC6 RID: 7110
		public static mFont bigNumber_red;

		// Token: 0x04001BC7 RID: 7111
		public static mFont bigNumber_While;

		// Token: 0x04001BC8 RID: 7112
		public static mFont bigNumber_yellow;

		// Token: 0x04001BC9 RID: 7113
		public static mFont bigNumber_green;

		// Token: 0x04001BCA RID: 7114
		public static mFont bigNumber_orange;

		// Token: 0x04001BCB RID: 7115
		public static mFont bigNumber_blue;

		// Token: 0x04001BCC RID: 7116
		public static mFont bigNumber_black;

		// Token: 0x04001BCD RID: 7117
		public static mFont nameFontRed;

		// Token: 0x04001BCE RID: 7118
		public static mFont nameFontYellow;

		// Token: 0x04001BCF RID: 7119
		public static mFont nameFontGreen;

		// Token: 0x04001BD0 RID: 7120
		public static mFont tahoma_7_greySmall;

		// Token: 0x04001BD1 RID: 7121
		public static mFont tahoma_7b_yellowSmall2;

		// Token: 0x04001BD2 RID: 7122
		public static mFont tahoma_7b_green2Small;

		// Token: 0x04001BD3 RID: 7123
		public static mFont tahoma_7_whiteSmall;

		// Token: 0x04001BD4 RID: 7124
		public static mFont tahoma_7b_greenSmall;

		// Token: 0x04001BD5 RID: 7125
		public Font myFont;

		// Token: 0x04001BD6 RID: 7126
		private int height;

		// Token: 0x04001BD7 RID: 7127
		private int wO;

		// Token: 0x04001BD8 RID: 7128
		public Color color1 = Color.white;

		// Token: 0x04001BD9 RID: 7129
		public Color color2 = Color.gray;

		// Token: 0x04001BDA RID: 7130
		public sbyte id;

		// Token: 0x04001BDB RID: 7131
		public int fstyle;

		// Token: 0x04001BDC RID: 7132
		public string st1 = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";

		// Token: 0x04001BDD RID: 7133
		public string st2 = "¸µ¶·¹¨¾»¼½Æ©ÊÇÈÉËÐÌÎÏÑªÕÒÓÔÖÝ×ØÜÞãßáâä«èåæçé¬íêëìîóïñòô­øõö÷ùýúûüþ®¸µ¶·¹¡¾»¼½Æ¢ÊÇÈÉËÐÌÎÏÑ£ÕÒÓÔÖÝ×ØÜÞãßáâä¤èåæçé¥íêëìîóïñòô¦øõö÷ùýúûüþ§";

		// Token: 0x04001BDE RID: 7134
		public const string str = " 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW";

		// Token: 0x04001BDF RID: 7135
		private int yAdd;

		// Token: 0x04001BE0 RID: 7136
		private string pathImage;
	}
}
