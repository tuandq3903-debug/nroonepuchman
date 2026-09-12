using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Game2
{
	// Token: 0x020003F8 RID: 1016
	public class Rms
	{
		// Token: 0x06002D9D RID: 11677 RVA: 0x002D1CB3 File Offset: 0x002CFEB3
		public static void saveRMS(string filename, sbyte[] data)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Rms.__saveRMS(filename, data);
				return;
			}
			Rms._saveRMS(filename, data);
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x002D1CDA File Offset: 0x002CFEDA
		public static sbyte[] loadRMS(string filename)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				return Rms.__loadRMS(filename);
			}
			return Rms._loadRMS(filename);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x002D1D00 File Offset: 0x002CFF00
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

		// Token: 0x06002DA0 RID: 11680 RVA: 0x002D1D50 File Offset: 0x002CFF50
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

		// Token: 0x06002DA1 RID: 11681 RVA: 0x002D1D9C File Offset: 0x002CFF9C
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

		// Token: 0x06002DA2 RID: 11682 RVA: 0x002D1E14 File Offset: 0x002D0014
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

		// Token: 0x06002DA3 RID: 11683 RVA: 0x002D1E90 File Offset: 0x002D0090
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

		// Token: 0x06002DA4 RID: 11684 RVA: 0x002D1EE4 File Offset: 0x002D00E4
		public static int loadRMSInt(string file)
		{
			sbyte[] array = Rms.loadRMS(file);
			if (array == null)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x002D1F00 File Offset: 0x002D0100
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

		// Token: 0x06002DA6 RID: 11686 RVA: 0x0007DC30 File Offset: 0x0007BE30
		public static string GetiPhoneDocumentsPath()
		{
			return Application.persistentDataPath;
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x002D1F34 File Offset: 0x002D0134
		private static void __saveRMS(string filename, sbyte[] data)
		{
			string text = Rms.GetiPhoneDocumentsPath() + "/" + filename;
			FileStream fileStream = new FileStream(text, FileMode.Create);
			fileStream.Write(ArrayCast.cast(data), 0, data.Length);
			fileStream.Flush();
			fileStream.Close();
			Main.setBackupIcloud(text);
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x002D1F70 File Offset: 0x002D0170
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

		// Token: 0x06002DA9 RID: 11689 RVA: 0x002D1FD8 File Offset: 0x002D01D8
		public static void clearAll()
		{
			Cout.LogError3("clean rms");
			FileInfo[] files = new DirectoryInfo(Rms.GetiPhoneDocumentsPath() + "/").GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x002D2020 File Offset: 0x002D0220
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

		// Token: 0x04005955 RID: 22869
		public static int status;

		// Token: 0x04005956 RID: 22870
		public static sbyte[] data;

		// Token: 0x04005957 RID: 22871
		public static string filename;
	}
}
