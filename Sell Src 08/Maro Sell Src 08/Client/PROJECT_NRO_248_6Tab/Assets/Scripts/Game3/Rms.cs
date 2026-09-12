using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Game3
{
	// Token: 0x02000320 RID: 800
	public class Rms
	{
		// Token: 0x060023F9 RID: 9209 RVA: 0x0023CC0F File Offset: 0x0023AE0F
		public static void saveRMS(string filename, sbyte[] data)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Rms.__saveRMS(filename, data);
				return;
			}
			Rms._saveRMS(filename, data);
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x0023CC36 File Offset: 0x0023AE36
		public static sbyte[] loadRMS(string filename)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				return Rms.__loadRMS(filename);
			}
			return Rms._loadRMS(filename);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x0023CC5C File Offset: 0x0023AE5C
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

		// Token: 0x060023FC RID: 9212 RVA: 0x0023CCAC File Offset: 0x0023AEAC
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

		// Token: 0x060023FD RID: 9213 RVA: 0x0023CCF8 File Offset: 0x0023AEF8
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

		// Token: 0x060023FE RID: 9214 RVA: 0x0023CD70 File Offset: 0x0023AF70
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

		// Token: 0x060023FF RID: 9215 RVA: 0x0023CDEC File Offset: 0x0023AFEC
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

		// Token: 0x06002400 RID: 9216 RVA: 0x0023CE40 File Offset: 0x0023B040
		public static int loadRMSInt(string file)
		{
			sbyte[] array = Rms.loadRMS(file);
			if (array == null)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x0023CE5C File Offset: 0x0023B05C
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

		// Token: 0x06002402 RID: 9218 RVA: 0x0007DC30 File Offset: 0x0007BE30
		public static string GetiPhoneDocumentsPath()
		{
			return Application.persistentDataPath;
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x0023CE90 File Offset: 0x0023B090
		private static void __saveRMS(string filename, sbyte[] data)
		{
			string text = Rms.GetiPhoneDocumentsPath() + "/" + filename;
			FileStream fileStream = new FileStream(text, FileMode.Create);
			fileStream.Write(ArrayCast.cast(data), 0, data.Length);
			fileStream.Flush();
			fileStream.Close();
			Main.setBackupIcloud(text);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x0023CECC File Offset: 0x0023B0CC
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

		// Token: 0x06002405 RID: 9221 RVA: 0x0023CF34 File Offset: 0x0023B134
		public static void clearAll()
		{
			Cout.LogError3("clean rms");
			FileInfo[] files = new DirectoryInfo(Rms.GetiPhoneDocumentsPath() + "/").GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0023CF7C File Offset: 0x0023B17C
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

		// Token: 0x040046D6 RID: 18134
		public static int status;

		// Token: 0x040046D7 RID: 18135
		public static sbyte[] data;

		// Token: 0x040046D8 RID: 18136
		public static string filename;
	}
}
