using System;
using System.Threading;
using UnityEngine;

namespace Game6
{
	// Token: 0x020000B1 RID: 177
	public class Sound
	{
		// Token: 0x06000812 RID: 2066 RVA: 0x00086740 File Offset: 0x00084940
		public static void init()
		{
			if (GameObject.Find("Audio Player 6") != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "Audio Player 6";
			gameObject.transform.position = Vector3.zero;
			gameObject.AddComponent<AudioListener>();
			Sound.SoundBGLoop = gameObject.AddComponent<AudioSource>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00086798 File Offset: 0x00084998
		public static void init(int[] musicID, int[] sID)
		{
			if (Sound.player == null && Sound.music == null)
			{
				Sound.init();
				Sound.l1 = musicID.Length;
				Sound.player = new GameObject[musicID.Length + sID.Length];
				Sound.music = new AudioClip[musicID.Length + sID.Length];
				for (int i = 0; i < Sound.player.Length; i++)
				{
					Sound.getAssetSoundFile((i >= Sound.l1) ? ("/sound/" + (i - Sound.l1).ToString()) : ("/music/" + i.ToString()), i);
				}
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00086831 File Offset: 0x00084A31
		public static void playSound(int id, float volume)
		{
			Sound.play(id + Sound.l1, volume);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00086840 File Offset: 0x00084A40
		public static void playSound1(int id, float volume)
		{
			Sound.play(id, volume);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00086849 File Offset: 0x00084A49
		public static void getAssetSoundFile(string fileName, int pos)
		{
			Sound.stop(pos);
			string empty = string.Empty;
			Sound.load(Main.res + fileName, pos);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00086868 File Offset: 0x00084A68
		public static void stopAllz()
		{
			for (int i = 0; i < Sound.music.Length; i++)
			{
				Sound.stop(i);
			}
			for (int j = 0; j < Sound.l1; j++)
			{
				Sound.sTopSoundBG(j);
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000868A3 File Offset: 0x00084AA3
		public static void play(int id, float volume)
		{
			if (!Sound.isNotPlay && GameCanvas.isPlaySound)
			{
				Sound.start(volume, id);
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000868BA File Offset: 0x00084ABA
		public static bool isPlayingSound()
		{
			return !(Sound.SoundRun == null) && Sound.SoundRun.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000868DA File Offset: 0x00084ADA
		public static void playMus(int type, float vl, bool loop)
		{
			if (!Sound.isNotPlay)
			{
				vl -= 0.3f;
				if (vl <= 0f)
				{
					vl = 0.01f;
				}
				Sound.playSoundBGLoop(type, vl);
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00086904 File Offset: 0x00084B04
		public static void playSoundBGLoop(int id, float volume)
		{
			if (GameCanvas.isPlaySound)
			{
				if (id == SoundMn.AIR_SHIP)
				{
					Sound.playSound1(id, volume + 0.2f);
					return;
				}
				if (!(Sound.SoundBGLoop == null) && !Sound.isPlayingSoundBG(id))
				{
					Sound.SoundBGLoop.GetComponent<AudioSource>().loop = true;
					Sound.SoundBGLoop.GetComponent<AudioSource>().clip = Sound.music[id];
					Sound.SoundBGLoop.GetComponent<AudioSource>().volume = volume;
					Sound.SoundBGLoop.GetComponent<AudioSource>().Play();
				}
			}
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00086988 File Offset: 0x00084B88
		public static void sTopSoundBG(int id)
		{
			Sound.SoundBGLoop.GetComponent<AudioSource>().Stop();
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00086999 File Offset: 0x00084B99
		public static bool isPlayingSoundBG(int id)
		{
			return !(Sound.SoundBGLoop == null) && Sound.SoundBGLoop.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x000869B9 File Offset: 0x00084BB9
		public static void load(string filename, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__load(filename, pos);
				return;
			}
			Sound._load(filename, pos);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000869E0 File Offset: 0x00084BE0
		private static void _load(string filename, int pos)
		{
			if (Sound.status != 0)
			{
				Cout.LogError("CANNOT LOAD AUDIO " + filename + " WHEN LOADING " + Sound.filenametemp);
				return;
			}
			Sound.filenametemp = filename;
			Sound.postem = pos;
			Sound.status = 2;
			int i;
			for (i = 0; i < 100; i++)
			{
				Thread.Sleep(5);
				if (Sound.status == 0)
				{
					break;
				}
			}
			if (i == 100)
			{
				Cout.LogError("TOO LONG FOR LOAD AUDIO " + filename);
				return;
			}
			Cout.Log(string.Concat(new string[]
			{
				"Load Audio ",
				filename,
				" done in ",
				(i * 5).ToString(),
				"ms"
			}));
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00086A8B File Offset: 0x00084C8B
		private static void __load(string filename, int pos)
		{
			Sound.music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
			GameObject.Find("Audio Player 6").AddComponent<AudioSource>();
			Sound.player[pos] = GameObject.Find("Audio Player 6");
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00086ACA File Offset: 0x00084CCA
		public static void start(float volume, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__start(volume, pos);
				return;
			}
			Sound._start(volume, pos);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00086AF4 File Offset: 0x00084CF4
		public static void _start(float volume, int pos)
		{
			if (Sound.status != 0)
			{
				Debug.LogError("CANNOT START AUDIO WHEN STARTING");
				return;
			}
			Sound.volumetem = volume;
			Sound.postem = pos;
			Sound.status = 3;
			int i;
			for (i = 0; i < 100; i++)
			{
				Thread.Sleep(5);
				if (Sound.status == 0)
				{
					break;
				}
			}
			if (i == 100)
			{
				Debug.LogError("TOO LONG FOR START AUDIO");
				return;
			}
			Debug.Log("Start Audio done in " + (i * 5).ToString() + "ms");
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00086B6E File Offset: 0x00084D6E
		public static void __start(float volume, int pos)
		{
			if (!(Sound.player[pos] == null))
			{
				Sound.player[pos].GetComponent<AudioSource>().PlayOneShot(Sound.music[pos], volume);
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00086B98 File Offset: 0x00084D98
		public static void stop(int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__stop(pos);
				return;
			}
			Sound._stop(pos);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00086BC0 File Offset: 0x00084DC0
		public static void _stop(int pos)
		{
			if (Sound.status != 0)
			{
				Debug.LogError("CANNOT STOP AUDIO WHEN STOPPING");
				return;
			}
			Sound.postem = pos;
			Sound.status = 4;
			int i;
			for (i = 0; i < 100; i++)
			{
				Thread.Sleep(5);
				if (Sound.status == 0)
				{
					break;
				}
			}
			if (i == 100)
			{
				Debug.LogError("TOO LONG FOR STOP AUDIO");
				return;
			}
			Debug.Log("Stop Audio done in " + (i * 5).ToString() + "ms");
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00086C34 File Offset: 0x00084E34
		public static void __stop(int pos)
		{
			if (Sound.player[pos] != null)
			{
				Sound.player[pos].GetComponent<AudioSource>().Stop();
			}
		}

		// Token: 0x04001046 RID: 4166
		public static int status;

		// Token: 0x04001047 RID: 4167
		public static int postem;

		// Token: 0x04001048 RID: 4168
		private static string filenametemp;

		// Token: 0x04001049 RID: 4169
		private static float volumetem;

		// Token: 0x0400104A RID: 4170
		public static bool isSound = true;

		// Token: 0x0400104B RID: 4171
		public static bool isNotPlay;

		// Token: 0x0400104C RID: 4172
		public static bool stopAll;

		// Token: 0x0400104D RID: 4173
		public static AudioSource SoundRun;

		// Token: 0x0400104E RID: 4174
		public static AudioSource SoundBGLoop;

		// Token: 0x0400104F RID: 4175
		public static AudioClip[] music;

		// Token: 0x04001050 RID: 4176
		public static GameObject[] player;

		// Token: 0x04001051 RID: 4177
		public static sbyte MBClick = 1;

		// Token: 0x04001052 RID: 4178
		public static sbyte MTone = 2;

		// Token: 0x04001053 RID: 4179
		public static sbyte MSanzu = 3;

		// Token: 0x04001054 RID: 4180
		public static sbyte MChakumi = 4;

		// Token: 0x04001055 RID: 4181
		public static sbyte MChai = 5;

		// Token: 0x04001056 RID: 4182
		public static sbyte MOshin = 6;

		// Token: 0x04001057 RID: 4183
		public static sbyte MEchigo = 7;

		// Token: 0x04001058 RID: 4184
		public static sbyte MKojin = 8;

		// Token: 0x04001059 RID: 4185
		public static sbyte MHaruna = 9;

		// Token: 0x0400105A RID: 4186
		public static sbyte MHirosaki = 10;

		// Token: 0x0400105B RID: 4187
		public static sbyte MOokaza = 11;

		// Token: 0x0400105C RID: 4188
		public static sbyte MGiotuyet = 12;

		// Token: 0x0400105D RID: 4189
		public static sbyte MHangdong = 13;

		// Token: 0x0400105E RID: 4190
		public static sbyte MDeKeu = 14;

		// Token: 0x0400105F RID: 4191
		public static sbyte MChimKeu = 15;

		// Token: 0x04001060 RID: 4192
		public static sbyte MBuocChan = 16;

		// Token: 0x04001061 RID: 4193
		public static sbyte MNuocChay = 17;

		// Token: 0x04001062 RID: 4194
		public static sbyte MBomMau = 18;

		// Token: 0x04001063 RID: 4195
		public static sbyte MKiemGo = 19;

		// Token: 0x04001064 RID: 4196
		public static sbyte MKiem = 20;

		// Token: 0x04001065 RID: 4197
		public static sbyte MTieu = 21;

		// Token: 0x04001066 RID: 4198
		public static sbyte MKunai = 22;

		// Token: 0x04001067 RID: 4199
		public static sbyte MCung = 23;

		// Token: 0x04001068 RID: 4200
		public static sbyte MDao = 24;

		// Token: 0x04001069 RID: 4201
		public static sbyte MQuat = 25;

		// Token: 0x0400106A RID: 4202
		public static sbyte MCung2 = 26;

		// Token: 0x0400106B RID: 4203
		public static sbyte MTieu2 = 27;

		// Token: 0x0400106C RID: 4204
		public static sbyte MTieu3 = 28;

		// Token: 0x0400106D RID: 4205
		public static sbyte MKiem2 = 29;

		// Token: 0x0400106E RID: 4206
		public static sbyte MKiem3 = 30;

		// Token: 0x0400106F RID: 4207
		public static sbyte MDao2 = 31;

		// Token: 0x04001070 RID: 4208
		public static sbyte MDao3 = 32;

		// Token: 0x04001071 RID: 4209
		public static sbyte MCung3 = 33;

		// Token: 0x04001072 RID: 4210
		public static int l1;
	}
}
