using System;
using System.Threading;
using UnityEngine;

namespace Game5
{
	// Token: 0x02000189 RID: 393
	public class Sound
	{
		// Token: 0x060011B6 RID: 4534 RVA: 0x0011B84C File Offset: 0x00119A4C
		public static void init()
		{
			if (GameObject.Find("Audio Player 5") != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "Audio Player 5";
			gameObject.transform.position = Vector3.zero;
			gameObject.AddComponent<AudioListener>();
			Sound.SoundBGLoop = gameObject.AddComponent<AudioSource>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0011B8A4 File Offset: 0x00119AA4
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

		// Token: 0x060011B8 RID: 4536 RVA: 0x0011B93D File Offset: 0x00119B3D
		public static void playSound(int id, float volume)
		{
			Sound.play(id + Sound.l1, volume);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0011B94C File Offset: 0x00119B4C
		public static void playSound1(int id, float volume)
		{
			Sound.play(id, volume);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0011B955 File Offset: 0x00119B55
		public static void getAssetSoundFile(string fileName, int pos)
		{
			Sound.stop(pos);
			string empty = string.Empty;
			Sound.load(Main.res + fileName, pos);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0011B974 File Offset: 0x00119B74
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

		// Token: 0x060011BC RID: 4540 RVA: 0x0011B9AF File Offset: 0x00119BAF
		public static void play(int id, float volume)
		{
			if (!Sound.isNotPlay && GameCanvas.isPlaySound)
			{
				Sound.start(volume, id);
			}
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0011B9C6 File Offset: 0x00119BC6
		public static bool isPlayingSound()
		{
			return !(Sound.SoundRun == null) && Sound.SoundRun.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0011B9E6 File Offset: 0x00119BE6
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

		// Token: 0x060011BF RID: 4543 RVA: 0x0011BA10 File Offset: 0x00119C10
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

		// Token: 0x060011C0 RID: 4544 RVA: 0x0011BA94 File Offset: 0x00119C94
		public static void sTopSoundBG(int id)
		{
			Sound.SoundBGLoop.GetComponent<AudioSource>().Stop();
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0011BAA5 File Offset: 0x00119CA5
		public static bool isPlayingSoundBG(int id)
		{
			return !(Sound.SoundBGLoop == null) && Sound.SoundBGLoop.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0011BAC5 File Offset: 0x00119CC5
		public static void load(string filename, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__load(filename, pos);
				return;
			}
			Sound._load(filename, pos);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0011BAEC File Offset: 0x00119CEC
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

		// Token: 0x060011C4 RID: 4548 RVA: 0x0011BB97 File Offset: 0x00119D97
		private static void __load(string filename, int pos)
		{
			Sound.music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
			GameObject.Find("Audio Player 5").AddComponent<AudioSource>();
			Sound.player[pos] = GameObject.Find("Audio Player 5");
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0011BBD6 File Offset: 0x00119DD6
		public static void start(float volume, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__start(volume, pos);
				return;
			}
			Sound._start(volume, pos);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0011BC00 File Offset: 0x00119E00
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

		// Token: 0x060011C7 RID: 4551 RVA: 0x0011BC7A File Offset: 0x00119E7A
		public static void __start(float volume, int pos)
		{
			if (!(Sound.player[pos] == null))
			{
				Sound.player[pos].GetComponent<AudioSource>().PlayOneShot(Sound.music[pos], volume);
			}
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0011BCA4 File Offset: 0x00119EA4
		public static void stop(int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__stop(pos);
				return;
			}
			Sound._stop(pos);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0011BCCC File Offset: 0x00119ECC
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

		// Token: 0x060011CA RID: 4554 RVA: 0x0011BD40 File Offset: 0x00119F40
		public static void __stop(int pos)
		{
			if (Sound.player[pos] != null)
			{
				Sound.player[pos].GetComponent<AudioSource>().Stop();
			}
		}

		// Token: 0x040022C5 RID: 8901
		public static int status;

		// Token: 0x040022C6 RID: 8902
		public static int postem;

		// Token: 0x040022C7 RID: 8903
		private static string filenametemp;

		// Token: 0x040022C8 RID: 8904
		private static float volumetem;

		// Token: 0x040022C9 RID: 8905
		public static bool isSound = true;

		// Token: 0x040022CA RID: 8906
		public static bool isNotPlay;

		// Token: 0x040022CB RID: 8907
		public static bool stopAll;

		// Token: 0x040022CC RID: 8908
		public static AudioSource SoundRun;

		// Token: 0x040022CD RID: 8909
		public static AudioSource SoundBGLoop;

		// Token: 0x040022CE RID: 8910
		public static AudioClip[] music;

		// Token: 0x040022CF RID: 8911
		public static GameObject[] player;

		// Token: 0x040022D0 RID: 8912
		public static sbyte MBClick = 1;

		// Token: 0x040022D1 RID: 8913
		public static sbyte MTone = 2;

		// Token: 0x040022D2 RID: 8914
		public static sbyte MSanzu = 3;

		// Token: 0x040022D3 RID: 8915
		public static sbyte MChakumi = 4;

		// Token: 0x040022D4 RID: 8916
		public static sbyte MChai = 5;

		// Token: 0x040022D5 RID: 8917
		public static sbyte MOshin = 6;

		// Token: 0x040022D6 RID: 8918
		public static sbyte MEchigo = 7;

		// Token: 0x040022D7 RID: 8919
		public static sbyte MKojin = 8;

		// Token: 0x040022D8 RID: 8920
		public static sbyte MHaruna = 9;

		// Token: 0x040022D9 RID: 8921
		public static sbyte MHirosaki = 10;

		// Token: 0x040022DA RID: 8922
		public static sbyte MOokaza = 11;

		// Token: 0x040022DB RID: 8923
		public static sbyte MGiotuyet = 12;

		// Token: 0x040022DC RID: 8924
		public static sbyte MHangdong = 13;

		// Token: 0x040022DD RID: 8925
		public static sbyte MDeKeu = 14;

		// Token: 0x040022DE RID: 8926
		public static sbyte MChimKeu = 15;

		// Token: 0x040022DF RID: 8927
		public static sbyte MBuocChan = 16;

		// Token: 0x040022E0 RID: 8928
		public static sbyte MNuocChay = 17;

		// Token: 0x040022E1 RID: 8929
		public static sbyte MBomMau = 18;

		// Token: 0x040022E2 RID: 8930
		public static sbyte MKiemGo = 19;

		// Token: 0x040022E3 RID: 8931
		public static sbyte MKiem = 20;

		// Token: 0x040022E4 RID: 8932
		public static sbyte MTieu = 21;

		// Token: 0x040022E5 RID: 8933
		public static sbyte MKunai = 22;

		// Token: 0x040022E6 RID: 8934
		public static sbyte MCung = 23;

		// Token: 0x040022E7 RID: 8935
		public static sbyte MDao = 24;

		// Token: 0x040022E8 RID: 8936
		public static sbyte MQuat = 25;

		// Token: 0x040022E9 RID: 8937
		public static sbyte MCung2 = 26;

		// Token: 0x040022EA RID: 8938
		public static sbyte MTieu2 = 27;

		// Token: 0x040022EB RID: 8939
		public static sbyte MTieu3 = 28;

		// Token: 0x040022EC RID: 8940
		public static sbyte MKiem2 = 29;

		// Token: 0x040022ED RID: 8941
		public static sbyte MKiem3 = 30;

		// Token: 0x040022EE RID: 8942
		public static sbyte MDao2 = 31;

		// Token: 0x040022EF RID: 8943
		public static sbyte MDao3 = 32;

		// Token: 0x040022F0 RID: 8944
		public static sbyte MCung3 = 33;

		// Token: 0x040022F1 RID: 8945
		public static int l1;
	}
}
