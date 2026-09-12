using System;
using System.Threading;
using UnityEngine;

namespace Game2
{
	// Token: 0x02000411 RID: 1041
	public class Sound
	{
		// Token: 0x06002EA2 RID: 11938 RVA: 0x002DAA38 File Offset: 0x002D8C38
		public static void init()
		{
			if (GameObject.Find("Audio Player 2") != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "Audio Player 2";
			gameObject.transform.position = Vector3.zero;
			gameObject.AddComponent<AudioListener>();
			Sound.SoundBGLoop = gameObject.AddComponent<AudioSource>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x002DAA90 File Offset: 0x002D8C90
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

		// Token: 0x06002EA4 RID: 11940 RVA: 0x002DAB29 File Offset: 0x002D8D29
		public static void playSound(int id, float volume)
		{
			Sound.play(id + Sound.l1, volume);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x002DAB38 File Offset: 0x002D8D38
		public static void playSound1(int id, float volume)
		{
			Sound.play(id, volume);
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x002DAB41 File Offset: 0x002D8D41
		public static void getAssetSoundFile(string fileName, int pos)
		{
			Sound.stop(pos);
			string empty = string.Empty;
			Sound.load(Main.res + fileName, pos);
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x002DAB60 File Offset: 0x002D8D60
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

		// Token: 0x06002EA8 RID: 11944 RVA: 0x002DAB9B File Offset: 0x002D8D9B
		public static void play(int id, float volume)
		{
			if (!Sound.isNotPlay && GameCanvas.isPlaySound)
			{
				Sound.start(volume, id);
			}
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x002DABB2 File Offset: 0x002D8DB2
		public static bool isPlayingSound()
		{
			return !(Sound.SoundRun == null) && Sound.SoundRun.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x002DABD2 File Offset: 0x002D8DD2
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

		// Token: 0x06002EAB RID: 11947 RVA: 0x002DABFC File Offset: 0x002D8DFC
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

		// Token: 0x06002EAC RID: 11948 RVA: 0x002DAC80 File Offset: 0x002D8E80
		public static void sTopSoundBG(int id)
		{
			Sound.SoundBGLoop.GetComponent<AudioSource>().Stop();
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x002DAC91 File Offset: 0x002D8E91
		public static bool isPlayingSoundBG(int id)
		{
			return !(Sound.SoundBGLoop == null) && Sound.SoundBGLoop.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x002DACB1 File Offset: 0x002D8EB1
		public static void load(string filename, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__load(filename, pos);
				return;
			}
			Sound._load(filename, pos);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x002DACD8 File Offset: 0x002D8ED8
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

		// Token: 0x06002EB0 RID: 11952 RVA: 0x002DAD83 File Offset: 0x002D8F83
		private static void __load(string filename, int pos)
		{
			Sound.music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
			GameObject.Find("Audio Player 2").AddComponent<AudioSource>();
			Sound.player[pos] = GameObject.Find("Audio Player 2");
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x002DADC2 File Offset: 0x002D8FC2
		public static void start(float volume, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__start(volume, pos);
				return;
			}
			Sound._start(volume, pos);
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x002DADEC File Offset: 0x002D8FEC
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

		// Token: 0x06002EB3 RID: 11955 RVA: 0x002DAE66 File Offset: 0x002D9066
		public static void __start(float volume, int pos)
		{
			if (!(Sound.player[pos] == null))
			{
				Sound.player[pos].GetComponent<AudioSource>().PlayOneShot(Sound.music[pos], volume);
			}
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x002DAE90 File Offset: 0x002D9090
		public static void stop(int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__stop(pos);
				return;
			}
			Sound._stop(pos);
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x002DAEB8 File Offset: 0x002D90B8
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

		// Token: 0x06002EB6 RID: 11958 RVA: 0x002DAF2C File Offset: 0x002D912C
		public static void __stop(int pos)
		{
			if (Sound.player[pos] != null)
			{
				Sound.player[pos].GetComponent<AudioSource>().Stop();
			}
		}

		// Token: 0x04005A42 RID: 23106
		public static int status;

		// Token: 0x04005A43 RID: 23107
		public static int postem;

		// Token: 0x04005A44 RID: 23108
		private static string filenametemp;

		// Token: 0x04005A45 RID: 23109
		private static float volumetem;

		// Token: 0x04005A46 RID: 23110
		public static bool isSound = true;

		// Token: 0x04005A47 RID: 23111
		public static bool isNotPlay;

		// Token: 0x04005A48 RID: 23112
		public static bool stopAll;

		// Token: 0x04005A49 RID: 23113
		public static AudioSource SoundRun;

		// Token: 0x04005A4A RID: 23114
		public static AudioSource SoundBGLoop;

		// Token: 0x04005A4B RID: 23115
		public static AudioClip[] music;

		// Token: 0x04005A4C RID: 23116
		public static GameObject[] player;

		// Token: 0x04005A4D RID: 23117
		public static sbyte MBClick = 1;

		// Token: 0x04005A4E RID: 23118
		public static sbyte MTone = 2;

		// Token: 0x04005A4F RID: 23119
		public static sbyte MSanzu = 3;

		// Token: 0x04005A50 RID: 23120
		public static sbyte MChakumi = 4;

		// Token: 0x04005A51 RID: 23121
		public static sbyte MChai = 5;

		// Token: 0x04005A52 RID: 23122
		public static sbyte MOshin = 6;

		// Token: 0x04005A53 RID: 23123
		public static sbyte MEchigo = 7;

		// Token: 0x04005A54 RID: 23124
		public static sbyte MKojin = 8;

		// Token: 0x04005A55 RID: 23125
		public static sbyte MHaruna = 9;

		// Token: 0x04005A56 RID: 23126
		public static sbyte MHirosaki = 10;

		// Token: 0x04005A57 RID: 23127
		public static sbyte MOokaza = 11;

		// Token: 0x04005A58 RID: 23128
		public static sbyte MGiotuyet = 12;

		// Token: 0x04005A59 RID: 23129
		public static sbyte MHangdong = 13;

		// Token: 0x04005A5A RID: 23130
		public static sbyte MDeKeu = 14;

		// Token: 0x04005A5B RID: 23131
		public static sbyte MChimKeu = 15;

		// Token: 0x04005A5C RID: 23132
		public static sbyte MBuocChan = 16;

		// Token: 0x04005A5D RID: 23133
		public static sbyte MNuocChay = 17;

		// Token: 0x04005A5E RID: 23134
		public static sbyte MBomMau = 18;

		// Token: 0x04005A5F RID: 23135
		public static sbyte MKiemGo = 19;

		// Token: 0x04005A60 RID: 23136
		public static sbyte MKiem = 20;

		// Token: 0x04005A61 RID: 23137
		public static sbyte MTieu = 21;

		// Token: 0x04005A62 RID: 23138
		public static sbyte MKunai = 22;

		// Token: 0x04005A63 RID: 23139
		public static sbyte MCung = 23;

		// Token: 0x04005A64 RID: 23140
		public static sbyte MDao = 24;

		// Token: 0x04005A65 RID: 23141
		public static sbyte MQuat = 25;

		// Token: 0x04005A66 RID: 23142
		public static sbyte MCung2 = 26;

		// Token: 0x04005A67 RID: 23143
		public static sbyte MTieu2 = 27;

		// Token: 0x04005A68 RID: 23144
		public static sbyte MTieu3 = 28;

		// Token: 0x04005A69 RID: 23145
		public static sbyte MKiem2 = 29;

		// Token: 0x04005A6A RID: 23146
		public static sbyte MKiem3 = 30;

		// Token: 0x04005A6B RID: 23147
		public static sbyte MDao2 = 31;

		// Token: 0x04005A6C RID: 23148
		public static sbyte MDao3 = 32;

		// Token: 0x04005A6D RID: 23149
		public static sbyte MCung3 = 33;

		// Token: 0x04005A6E RID: 23150
		public static int l1;
	}
}
