using System;
using UnityEngine;

namespace Game4
{
	// Token: 0x0200021A RID: 538
	public class mFont
	{
		// Token: 0x06001770 RID: 6000 RVA: 0x001755B8 File Offset: 0x001737B8
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

		// Token: 0x06001771 RID: 6001 RVA: 0x001756F8 File Offset: 0x001738F8
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

		// Token: 0x06001772 RID: 6002 RVA: 0x00175800 File Offset: 0x00173A00
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

		// Token: 0x06001773 RID: 6003 RVA: 0x00175D50 File Offset: 0x00173F50
		public void setHeight(int height)
		{
			this.height = height;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00175D5C File Offset: 0x00173F5C
		public Color setColor(int rgb)
		{
			int num = rgb & 255;
			int num2 = rgb >> 8 & 255;
			float num3 = (float)(rgb >> 16 & 255);
			float b = (float)num / 256f;
			float g = (float)num2 / 256f;
			return new Color(num3 / 256f, g, b);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00175DA8 File Offset: 0x00173FA8
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

		// Token: 0x06001776 RID: 6006 RVA: 0x00175E1E File Offset: 0x0017401E
		public void setColorByID(int ID)
		{
			this.color1 = this.setColor(mFont.colorJava[ID]);
			this.color2 = this.setColor(mFont.colorJava[ID]);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00175E48 File Offset: 0x00174048
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

		// Token: 0x06001778 RID: 6008 RVA: 0x00175FD0 File Offset: 0x001741D0
		public Color setColorFont(sbyte id)
		{
			return this.setColor(mFont.colorJava[(int)id]);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00175FE0 File Offset: 0x001741E0
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

		// Token: 0x0600177A RID: 6010 RVA: 0x00176123 File Offset: 0x00174323
		public void drawStringBorder(mGraphics g, string st, int x, int y, int align)
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.drawString(g, st, x, y, align);
				return;
			}
			this.setTypePaint(g, st, x, y, align, 0);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00176149 File Offset: 0x00174349
		public void drawStringBorder(mGraphics g, string st, int x, int y, int align, mFont font2)
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.drawString(g, st, x, y, align, font2);
				return;
			}
			this.drawStringBd(g, st, x, y, align, font2);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00176174 File Offset: 0x00174374
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

		// Token: 0x0600177D RID: 6013 RVA: 0x00176248 File Offset: 0x00174448
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

		// Token: 0x0600177E RID: 6014 RVA: 0x001763D4 File Offset: 0x001745D4
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

		// Token: 0x0600177F RID: 6015 RVA: 0x001764C8 File Offset: 0x001746C8
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

		// Token: 0x06001780 RID: 6016 RVA: 0x0017650C File Offset: 0x0017470C
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

		// Token: 0x06001781 RID: 6017 RVA: 0x0017656C File Offset: 0x0017476C
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

		// Token: 0x06001782 RID: 6018 RVA: 0x00176600 File Offset: 0x00174800
		public int getWidthNotExactOf(string s)
		{
			return s.Length * this.wO / mGraphics.zoomLevel;
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00176618 File Offset: 0x00174818
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

		// Token: 0x06001784 RID: 6020 RVA: 0x001766B8 File Offset: 0x001748B8
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

		// Token: 0x06001785 RID: 6021 RVA: 0x00176769 File Offset: 0x00174969
		public void reloadImage()
		{
			if (mGraphics.zoomLevel == 1)
			{
				this.imgFont = GameCanvas.loadImage(this.pathImage);
			}
		}

		// Token: 0x04002E14 RID: 11796
		public static int LEFT = 0;

		// Token: 0x04002E15 RID: 11797
		public static int RIGHT = 1;

		// Token: 0x04002E16 RID: 11798
		public static int CENTER = 2;

		// Token: 0x04002E17 RID: 11799
		public static int RED = 0;

		// Token: 0x04002E18 RID: 11800
		public static int YELLOW = 1;

		// Token: 0x04002E19 RID: 11801
		public static int GREEN = 2;

		// Token: 0x04002E1A RID: 11802
		public static int FATAL = 3;

		// Token: 0x04002E1B RID: 11803
		public static int MISS = 4;

		// Token: 0x04002E1C RID: 11804
		public static int ORANGE = 5;

		// Token: 0x04002E1D RID: 11805
		public static int ADDMONEY = 6;

		// Token: 0x04002E1E RID: 11806
		public static int MISS_ME = 7;

		// Token: 0x04002E1F RID: 11807
		public static int FATAL_ME = 8;

		// Token: 0x04002E20 RID: 11808
		public static int HP = 9;

		// Token: 0x04002E21 RID: 11809
		public static int MP = 10;

		// Token: 0x04002E22 RID: 11810
		private int space;

		// Token: 0x04002E23 RID: 11811
		private Image imgFont;

		// Token: 0x04002E24 RID: 11812
		private string strFont;

		// Token: 0x04002E25 RID: 11813
		private int[][] fImages;

		// Token: 0x04002E26 RID: 11814
		public static int yAddFont;

		// Token: 0x04002E27 RID: 11815
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

		// Token: 0x04002E28 RID: 11816
		public static mFont gI;

		// Token: 0x04002E29 RID: 11817
		public static mFont tahoma_7b_red;

		// Token: 0x04002E2A RID: 11818
		public static mFont tahoma_7b_blue;

		// Token: 0x04002E2B RID: 11819
		public static mFont tahoma_7b_white;

		// Token: 0x04002E2C RID: 11820
		public static mFont tahoma_7b_yellow;

		// Token: 0x04002E2D RID: 11821
		public static mFont tahoma_7b_yellowSmall;

		// Token: 0x04002E2E RID: 11822
		public static mFont tahoma_7b_dark;

		// Token: 0x04002E2F RID: 11823
		public static mFont tahoma_7b_green2;

		// Token: 0x04002E30 RID: 11824
		public static mFont tahoma_7b_green;

		// Token: 0x04002E31 RID: 11825
		public static mFont tahoma_7b_focus;

		// Token: 0x04002E32 RID: 11826
		public static mFont tahoma_7b_pink;

		// Token: 0x04002E33 RID: 11827
		public static mFont tahoma_7b_unfocus;

		// Token: 0x04002E34 RID: 11828
		public static mFont tahoma_7;

		// Token: 0x04002E35 RID: 11829
		public static mFont tahoma_7_blue1;

		// Token: 0x04002E36 RID: 11830
		public static mFont tahoma_7_blue1Small;

		// Token: 0x04002E37 RID: 11831
		public static mFont tahoma_7_green2;

		// Token: 0x04002E38 RID: 11832
		public static mFont tahoma_7_yellow;

		// Token: 0x04002E39 RID: 11833
		public static mFont tahoma_7_orange;

		// Token: 0x04002E3A RID: 11834
		public static mFont tahoma_7_grey;

		// Token: 0x04002E3B RID: 11835
		public static mFont tahoma_7_red;

		// Token: 0x04002E3C RID: 11836
		public static mFont tahoma_7_blue;

		// Token: 0x04002E3D RID: 11837
		public static mFont tahoma_7_green;

		// Token: 0x04002E3E RID: 11838
		public static mFont tahoma_7_white;

		// Token: 0x04002E3F RID: 11839
		public static mFont tahoma_8b;

		// Token: 0x04002E40 RID: 11840
		public static mFont number_yellow;

		// Token: 0x04002E41 RID: 11841
		public static mFont number_red;

		// Token: 0x04002E42 RID: 11842
		public static mFont number_green;

		// Token: 0x04002E43 RID: 11843
		public static mFont number_gray;

		// Token: 0x04002E44 RID: 11844
		public static mFont number_orange;

		// Token: 0x04002E45 RID: 11845
		public static mFont bigNumber_red;

		// Token: 0x04002E46 RID: 11846
		public static mFont bigNumber_While;

		// Token: 0x04002E47 RID: 11847
		public static mFont bigNumber_yellow;

		// Token: 0x04002E48 RID: 11848
		public static mFont bigNumber_green;

		// Token: 0x04002E49 RID: 11849
		public static mFont bigNumber_orange;

		// Token: 0x04002E4A RID: 11850
		public static mFont bigNumber_blue;

		// Token: 0x04002E4B RID: 11851
		public static mFont bigNumber_black;

		// Token: 0x04002E4C RID: 11852
		public static mFont nameFontRed;

		// Token: 0x04002E4D RID: 11853
		public static mFont nameFontYellow;

		// Token: 0x04002E4E RID: 11854
		public static mFont nameFontGreen;

		// Token: 0x04002E4F RID: 11855
		public static mFont tahoma_7_greySmall;

		// Token: 0x04002E50 RID: 11856
		public static mFont tahoma_7b_yellowSmall2;

		// Token: 0x04002E51 RID: 11857
		public static mFont tahoma_7b_green2Small;

		// Token: 0x04002E52 RID: 11858
		public static mFont tahoma_7_whiteSmall;

		// Token: 0x04002E53 RID: 11859
		public static mFont tahoma_7b_greenSmall;

		// Token: 0x04002E54 RID: 11860
		public Font myFont;

		// Token: 0x04002E55 RID: 11861
		private int height;

		// Token: 0x04002E56 RID: 11862
		private int wO;

		// Token: 0x04002E57 RID: 11863
		public Color color1 = Color.white;

		// Token: 0x04002E58 RID: 11864
		public Color color2 = Color.gray;

		// Token: 0x04002E59 RID: 11865
		public sbyte id;

		// Token: 0x04002E5A RID: 11866
		public int fstyle;

		// Token: 0x04002E5B RID: 11867
		public string st1 = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";

		// Token: 0x04002E5C RID: 11868
		public string st2 = "¸µ¶·¹¨¾»¼½Æ©ÊÇÈÉËÐÌÎÏÑªÕÒÓÔÖÝ×ØÜÞãßáâä«èåæçé¬íêëìîóïñòô­øõö÷ùýúûüþ®¸µ¶·¹¡¾»¼½Æ¢ÊÇÈÉËÐÌÎÏÑ£ÕÒÓÔÖÝ×ØÜÞãßáâä¤èåæçé¥íêëìîóïñòô¦øõö÷ùýúûüþ§";

		// Token: 0x04002E5D RID: 11869
		public const string str = " 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW";

		// Token: 0x04002E5E RID: 11870
		private int yAdd;

		// Token: 0x04002E5F RID: 11871
		private string pathImage;
	}
}
