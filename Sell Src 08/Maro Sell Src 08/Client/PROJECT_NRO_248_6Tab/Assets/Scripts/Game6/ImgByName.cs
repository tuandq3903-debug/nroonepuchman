using System;
using System.Collections;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000048 RID: 72
	public class ImgByName
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0004424C File Offset: 0x0004244C
		public static void SetImage(string name, Image img, sbyte nFrame)
		{
			ImgByName.hashImagePath.put(string.Empty + name, new MainImage(img, nFrame));
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0004426C File Offset: 0x0004246C
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

		// Token: 0x06000377 RID: 887 RVA: 0x00044318 File Offset: 0x00042518
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

		// Token: 0x06000378 RID: 888 RVA: 0x0004438C File Offset: 0x0004258C
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

		// Token: 0x06000379 RID: 889 RVA: 0x00044438 File Offset: 0x00042638
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

		// Token: 0x0400070A RID: 1802
		public static MyHashTable hashImagePath = new MyHashTable();
	}
}
