using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Game1
{
	// Token: 0x020004D0 RID: 1232
	public class Rms
	{
		// Token: 0x06003741 RID: 14145 RVA: 0x00366D57 File Offset: 0x00364F57
		public static void saveRMS(string filename, sbyte[] data)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Rms.__saveRMS(filename, data);
				return;
			}
			Rms._saveRMS(filename, data);
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x00366D7E File Offset: 0x00364F7E
		public static sbyte[] loadRMS(string filename)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				return Rms.__loadRMS(filename);
			}
			return Rms._loadRMS(filename);
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x00366DA4 File Offset: 0x00364FA4
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

		// Token: 0x06003744 RID: 14148 RVA: 0x00366DF4 File Offset: 0x00364FF4
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

		// Token: 0x06003745 RID: 14149 RVA: 0x00366E40 File Offset: 0x00365040
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

		// Token: 0x06003746 RID: 14150 RVA: 0x00366EB8 File Offset: 0x003650B8
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

		// Token: 0x06003747 RID: 14151 RVA: 0x00366F34 File Offset: 0x00365134
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

		// Token: 0x06003748 RID: 14152 RVA: 0x00366F88 File Offset: 0x00365188
		public static int loadRMSInt(string file)
		{
			sbyte[] array = Rms.loadRMS(file);
			if (array == null)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x00366FA4 File Offset: 0x003651A4
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

		// Token: 0x0600374A RID: 14154 RVA: 0x0007DC30 File Offset: 0x0007BE30
		public static string GetiPhoneDocumentsPath()
		{
			return Application.persistentDataPath;
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x00366FD8 File Offset: 0x003651D8
		private static void __saveRMS(string filename, sbyte[] data)
		{
			string text = Rms.GetiPhoneDocumentsPath() + "/" + filename;
			FileStream fileStream = new FileStream(text, FileMode.Create);
			fileStream.Write(ArrayCast.cast(data), 0, data.Length);
			fileStream.Flush();
			fileStream.Close();
			Main.setBackupIcloud(text);
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x00367014 File Offset: 0x00365214
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

		// Token: 0x0600374D RID: 14157 RVA: 0x0036707C File Offset: 0x0036527C
		public static void clearAll()
		{
			Cout.LogError3("clean rms");
			FileInfo[] files = new DirectoryInfo(Rms.GetiPhoneDocumentsPath() + "/").GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
		}

		// Token: 0x0600374E RID: 14158 RVA: 0x003670C4 File Offset: 0x003652C4
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

		// Token: 0x04006BD4 RID: 27604
		public static int status;

		// Token: 0x04006BD5 RID: 27605
		public static sbyte[] data;

		// Token: 0x04006BD6 RID: 27606
		public static string filename;
	}
}
