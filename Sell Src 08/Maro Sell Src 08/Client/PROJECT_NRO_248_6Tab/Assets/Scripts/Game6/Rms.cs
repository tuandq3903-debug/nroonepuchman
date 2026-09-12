using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000098 RID: 152
	public class Rms
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0007D9AF File Offset: 0x0007BBAF
		public static void saveRMS(string filename, sbyte[] data)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Rms.__saveRMS(filename, data);
				return;
			}
			Rms._saveRMS(filename, data);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0007D9D6 File Offset: 0x0007BBD6
		public static sbyte[] loadRMS(string filename)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				return Rms.__loadRMS(filename);
			}
			return Rms._loadRMS(filename);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0007D9FC File Offset: 0x0007BBFC
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

		// Token: 0x06000710 RID: 1808 RVA: 0x0007DA4C File Offset: 0x0007BC4C
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

		// Token: 0x06000711 RID: 1809 RVA: 0x0007DA98 File Offset: 0x0007BC98
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

		// Token: 0x06000712 RID: 1810 RVA: 0x0007DB10 File Offset: 0x0007BD10
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

		// Token: 0x06000713 RID: 1811 RVA: 0x0007DB8C File Offset: 0x0007BD8C
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

		// Token: 0x06000714 RID: 1812 RVA: 0x0007DBE0 File Offset: 0x0007BDE0
		public static int loadRMSInt(string file)
		{
			sbyte[] array = Rms.loadRMS(file);
			if (array == null)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0007DBFC File Offset: 0x0007BDFC
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

		// Token: 0x06000716 RID: 1814 RVA: 0x0007DC30 File Offset: 0x0007BE30
		public static string GetiPhoneDocumentsPath()
		{
			return Application.persistentDataPath;
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0007DC37 File Offset: 0x0007BE37
		private static void __saveRMS(string filename, sbyte[] data)
		{
			string text = Rms.GetiPhoneDocumentsPath() + "/" + filename;
			FileStream fileStream = new FileStream(text, FileMode.Create);
			fileStream.Write(ArrayCast.cast(data), 0, data.Length);
			fileStream.Flush();
			fileStream.Close();
			Main.setBackupIcloud(text);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0007DC70 File Offset: 0x0007BE70
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

		// Token: 0x06000719 RID: 1817 RVA: 0x0007DCD8 File Offset: 0x0007BED8
		public static void clearAll()
		{
			Cout.LogError3("clean rms");
			FileInfo[] files = new DirectoryInfo(Rms.GetiPhoneDocumentsPath() + "/").GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0007DD20 File Offset: 0x0007BF20
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

		// Token: 0x04000F59 RID: 3929
		public static int status;

		// Token: 0x04000F5A RID: 3930
		public static sbyte[] data;

		// Token: 0x04000F5B RID: 3931
		public static string filename;
	}
}
