using System;
using System.Collections;
using UnityEngine;

namespace Game5
{
	// Token: 0x02000120 RID: 288
	public class ImgByName
	{
		// Token: 0x06000D19 RID: 3353 RVA: 0x000D93F8 File Offset: 0x000D75F8
		public static void SetImage(string name, Image img, sbyte nFrame)
		{
			ImgByName.hashImagePath.put(string.Empty + name, new MainImage(img, nFrame));
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000D9418 File Offset: 0x000D7618
		public static MainImage getImagePath(string nameImg, MyHashTable hash)
		{
			MainImage mainImage = (MainImage)hash.get(string.Empty + nameImg);
			if (mainImage == null)
			{
				mainImage = new MainImage();
				MainImage fromRms = ImgByName.getFromRms(nameImg);
				if (fromRms != null)
				{
					mainImage.img = fromRms.img;
					mainImage.nFrame = fromRms.nFrame;
				}
				hash.put(string.Empty + nameImg, mainImage);
			}
			mainImage.count = GameCanvas.timeNow / 1000L;
			if (mainImage.img == null)
			{
				mainImage.timeImageNull--;
				if (mainImage.timeImageNull <= 0)
				{
					Service.gI().getImgByName(nameImg);
					mainImage.timeImageNull = 200;
				}
			}
			return mainImage;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000D94C4 File Offset: 0x000D76C4
		public static MainImage getFromRms(string nameImg)
		{
			string filename = mGraphics.zoomLevel.ToString() + "ImgByName_" + nameImg;
			MainImage result = null;
			sbyte[] array = Rms.loadRMS(filename);
			if (array == null)
			{
				return result;
			}
			MainImage result2;
			try
			{
				result = new MainImage();
				result.nFrame = array[0];
				result.img = Image.createImage(array, 1, array.Length - 1);
				Image img = result.img;
				result2 = result;
			}
			catch (Exception)
			{
				result2 = null;
			}
			return result2;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x000D9538 File Offset: 0x000D7738
		public static void saveRMS(string nameImg, sbyte nFrame, sbyte[] data)
		{
			string text = mGraphics.zoomLevel.ToString() + "ImgByName_" + nameImg;
			DataOutputStream dataOutputStream = new DataOutputStream(data.Length + 1);
			int i = 0;
			try
			{
				dataOutputStream.writeByte(nFrame);
				for (i = 0; i < data.Length; i++)
				{
					dataOutputStream.writeByte(data[i]);
				}
				Rms.saveRMS(text, dataOutputStream.toByteArray());
				dataOutputStream.close();
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new string[]
				{
					i.ToString(),
					">>Errr save rms: ",
					text,
					"  ",
					ex.ToString()
				}));
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000D95E4 File Offset: 0x000D77E4
		public static void checkDelHash(MyHashTable hash, int minute, bool isTrue)
		{
			MyVector myVector = new MyVector("checkDelHash");
			if (isTrue)
			{
				hash.clear();
				return;
			}
			IDictionaryEnumerator enumerator = hash.GetEnumerator();
			while (enumerator.MoveNext())
			{
				MainImage mainImage = (MainImage)enumerator.Value;
				if (GameCanvas.timeNow / 1000L - mainImage.count > (long)(minute * 60))
				{
					string o = (string)enumerator.Key;
					myVector.addElement(o);
				}
			}
			for (int i = 0; i < myVector.size(); i++)
			{
				hash.remove((string)myVector.elementAt(i));
			}
		}

		// Token: 0x04001989 RID: 6537
		public static MyHashTable hashImagePath = new MyHashTable();
	}
}
