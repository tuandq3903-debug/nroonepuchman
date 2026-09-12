using System;
using System.Collections;
using UnityEngine;

namespace Game3
{
	// Token: 0x020002D0 RID: 720
	public class ImgByName
	{
		// Token: 0x06002061 RID: 8289 RVA: 0x00203540 File Offset: 0x00201740
		public static void SetImage(string name, Image img, sbyte nFrame)
		{
			ImgByName.hashImagePath.put(string.Empty + name, new MainImage(img, nFrame));
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00203560 File Offset: 0x00201760
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

		// Token: 0x06002063 RID: 8291 RVA: 0x0020360C File Offset: 0x0020180C
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

		// Token: 0x06002064 RID: 8292 RVA: 0x00203680 File Offset: 0x00201880
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

		// Token: 0x06002065 RID: 8293 RVA: 0x0020372C File Offset: 0x0020192C
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

		// Token: 0x04003E87 RID: 16007
		public static MyHashTable hashImagePath = new MyHashTable();
	}
}
