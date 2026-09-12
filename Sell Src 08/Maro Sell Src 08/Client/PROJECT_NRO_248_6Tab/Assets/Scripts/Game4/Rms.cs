using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Game4
{
	// Token: 0x02000248 RID: 584
	public class Rms
	{
		// Token: 0x06001A55 RID: 6741 RVA: 0x001A7B6B File Offset: 0x001A5D6B
		public static void saveRMS(string filename, sbyte[] data)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Rms.__saveRMS(filename, data);
				return;
			}
			Rms._saveRMS(filename, data);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x001A7B92 File Offset: 0x001A5D92
		public static sbyte[] loadRMS(string filename)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				return Rms.__loadRMS(filename);
			}
			return Rms._loadRMS(filename);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x001A7BB8 File Offset: 0x001A5DB8
		public static string loadRMSString(string fileName)
		{
			sbyte[] array = Rms.loadRMS(fileName);
			if (array == null)
			{
				return null;
			}
			DataInputStream dataInputStream = new DataInputStream(array);
			try
			{
				string result = dataInputStream.readUTF();
				dataInputStream.close();
				return result;
			}
			catch (Exception ex)
			{
				Cout.println(ex.StackTrace);
			}
			return null;
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x001A7C08 File Offset: 0x001A5E08
		public static void saveRMSString(string filename, string data)
		{
			DataOutputStream dataOutputStream = new DataOutputStream();
			try
			{
				dataOutputStream.writeUTF(data);
				Rms.saveRMS(filename, dataOutputStream.toByteArray());
				dataOutputStream.close();
			}
			catch (Exception ex)
			{
				Cout.println(ex.StackTrace);
			}
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x001A7C54 File Offset: 0x001A5E54
		private static void _saveRMS(string filename, sbyte[] data)
		{
			if (Rms.status != 0)
			{
				Debug.LogError("Cannot save RMS " + filename + " because current is saving " + Rms.filename);
				return;
			}
			Rms.filename = filename;
			Rms.data = data;
			Rms.status = 2;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				if (Rms.status == 0)
				{
					break;
				}
			}
			if (i == 500)
			{
				Debug.LogError("TOO LONG TO SAVE RMS " + filename);
			}
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x001A7CCC File Offset: 0x001A5ECC
		private static sbyte[] _loadRMS(string filename)
		{
			if (Rms.status != 0)
			{
				Debug.LogError("Cannot load RMS " + filename + " because current is loading " + Rms.filename);
				return null;
			}
			Rms.filename = filename;
			Rms.data = null;
			Rms.status = 3;
			int i;
			for (i = 0; i < 500; i++)
			{
				Thread.Sleep(5);
				if (Rms.status == 0)
				{
					break;
				}
			}
			if (i == 500)
			{
				Debug.LogError("TOO LONG TO LOAD RMS " + filename);
			}
			return Rms.data;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x001A7D48 File Offset: 0x001A5F48
		public static void update()
		{
			if (Rms.status == 2)
			{
				Rms.status = 1;
				Rms.__saveRMS(Rms.filename, Rms.data);
				Rms.status = 0;
				return;
			}
			if (Rms.status == 3)
			{
				Rms.status = 1;
				Rms.data = Rms.__loadRMS(Rms.filename);
				Rms.status = 0;
			}
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x001A7D9C File Offset: 0x001A5F9C
		public static int loadRMSInt(string file)
		{
			sbyte[] array = Rms.loadRMS(file);
			if (array == null)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x001A7DB8 File Offset: 0x001A5FB8
		public static void saveRMSInt(string file, int x)
		{
			try
			{
				Rms.saveRMS(file, new sbyte[]
				{
					(sbyte)x
				});
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0007DC30 File Offset: 0x0007BE30
		public static string GetiPhoneDocumentsPath()
		{
			return Application.persistentDataPath;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x001A7DEC File Offset: 0x001A5FEC
		private static void __saveRMS(string filename, sbyte[] data)
		{
			string text = Rms.GetiPhoneDocumentsPath() + "/" + filename;
			FileStream fileStream = new FileStream(text, FileMode.Create);
			fileStream.Write(ArrayCast.cast(data), 0, data.Length);
			fileStream.Flush();
			fileStream.Close();
			Main.setBackupIcloud(text);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x001A7E28 File Offset: 0x001A6028
		private static sbyte[] __loadRMS(string filename)
		{
			sbyte[] result;
			try
			{
				FileStream fileStream = new FileStream(Rms.GetiPhoneDocumentsPath() + "/" + filename, FileMode.Open);
				byte[] array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
				ArrayCast.cast(array);
				result = ArrayCast.cast(array);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x001A7E90 File Offset: 0x001A6090
		public static void clearAll()
		{
			Cout.LogError3("clean rms");
			FileInfo[] files = new DirectoryInfo(Rms.GetiPhoneDocumentsPath() + "/").GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x001A7ED8 File Offset: 0x001A60D8
		public static void DeleteStorage(string path)
		{
			try
			{
				File.Delete(Rms.GetiPhoneDocumentsPath() + "/" + path);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04003457 RID: 13399
		public static int status;

		// Token: 0x04003458 RID: 13400
		public static sbyte[] data;

		// Token: 0x04003459 RID: 13401
		public static string filename;
	}
}
