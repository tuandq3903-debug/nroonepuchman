using System;
using System.Threading;
using UnityEngine;

namespace Game4
{
	// Token: 0x02000261 RID: 609
	public class Sound
	{
		// Token: 0x06001B5A RID: 7002 RVA: 0x001B08F0 File Offset: 0x001AEAF0
		public static void init()
		{
			if (GameObject.Find("Audio Player 4") != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "Audio Player 4";
			gameObject.transform.position = Vector3.zero;
			gameObject.AddComponent<AudioListener>();
			Sound.SoundBGLoop = gameObject.AddComponent<AudioSource>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x001B0948 File Offset: 0x001AEB48
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

		// Token: 0x06001B5C RID: 7004 RVA: 0x001B09E1 File Offset: 0x001AEBE1
		public static void playSound(int id, float volume)
		{
			Sound.play(id + Sound.l1, volume);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x001B09F0 File Offset: 0x001AEBF0
		public static void playSound1(int id, float volume)
		{
			Sound.play(id, volume);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x001B09F9 File Offset: 0x001AEBF9
		public static void getAssetSoundFile(string fileName, int pos)
		{
			Sound.stop(pos);
			string empty = string.Empty;
			Sound.load(Main.res + fileName, pos);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x001B0A18 File Offset: 0x001AEC18
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

		// Token: 0x06001B60 RID: 7008 RVA: 0x001B0A53 File Offset: 0x001AEC53
		public static void play(int id, float volume)
		{
			if (!Sound.isNotPlay && GameCanvas.isPlaySound)
			{
				Sound.start(volume, id);
			}
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x001B0A6A File Offset: 0x001AEC6A
		public static bool isPlayingSound()
		{
			return !(Sound.SoundRun == null) && Sound.SoundRun.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x001B0A8A File Offset: 0x001AEC8A
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

		// Token: 0x06001B63 RID: 7011 RVA: 0x001B0AB4 File Offset: 0x001AECB4
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

		// Token: 0x06001B64 RID: 7012 RVA: 0x001B0B38 File Offset: 0x001AED38
		public static void sTopSoundBG(int id)
		{
			Sound.SoundBGLoop.GetComponent<AudioSource>().Stop();
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x001B0B49 File Offset: 0x001AED49
		public static bool isPlayingSoundBG(int id)
		{
			return !(Sound.SoundBGLoop == null) && Sound.SoundBGLoop.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x001B0B69 File Offset: 0x001AED69
		public static void load(string filename, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__load(filename, pos);
				return;
			}
			Sound._load(filename, pos);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x001B0B90 File Offset: 0x001AED90
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

		// Token: 0x06001B68 RID: 7016 RVA: 0x001B0C3B File Offset: 0x001AEE3B
		private static void __load(string filename, int pos)
		{
			Sound.music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
			GameObject.Find("Audio Player 4").AddComponent<AudioSource>();
			Sound.player[pos] = GameObject.Find("Audio Player 4");
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x001B0C7A File Offset: 0x001AEE7A
		public static void start(float volume, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__start(volume, pos);
				return;
			}
			Sound._start(volume, pos);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x001B0CA4 File Offset: 0x001AEEA4
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

		// Token: 0x06001B6B RID: 7019 RVA: 0x001B0D1E File Offset: 0x001AEF1E
		public static void __start(float volume, int pos)
		{
			if (!(Sound.player[pos] == null))
			{
				Sound.player[pos].GetComponent<AudioSource>().PlayOneShot(Sound.music[pos], volume);
			}
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x001B0D48 File Offset: 0x001AEF48
		public static void stop(int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__stop(pos);
				return;
			}
			Sound._stop(pos);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x001B0D70 File Offset: 0x001AEF70
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

		// Token: 0x06001B6E RID: 7022 RVA: 0x001B0DE4 File Offset: 0x001AEFE4
		public static void __stop(int pos)
		{
			if (Sound.player[pos] != null)
			{
				Sound.player[pos].GetComponent<AudioSource>().Stop();
			}
		}

		// Token: 0x04003544 RID: 13636
		public static int status;

		// Token: 0x04003545 RID: 13637
		public static int postem;

		// Token: 0x04003546 RID: 13638
		private static string filenametemp;

		// Token: 0x04003547 RID: 13639
		private static float volumetem;

		// Token: 0x04003548 RID: 13640
		public static bool isSound = true;

		// Token: 0x04003549 RID: 13641
		public static bool isNotPlay;

		// Token: 0x0400354A RID: 13642
		public static bool stopAll;

		// Token: 0x0400354B RID: 13643
		public static AudioSource SoundRun;

		// Token: 0x0400354C RID: 13644
		public static AudioSource SoundBGLoop;

		// Token: 0x0400354D RID: 13645
		public static AudioClip[] music;

		// Token: 0x0400354E RID: 13646
		public static GameObject[] player;

		// Token: 0x0400354F RID: 13647
		public static sbyte MBClick = 1;

		// Token: 0x04003550 RID: 13648
		public static sbyte MTone = 2;

		// Token: 0x04003551 RID: 13649
		public static sbyte MSanzu = 3;

		// Token: 0x04003552 RID: 13650
		public static sbyte MChakumi = 4;

		// Token: 0x04003553 RID: 13651
		public static sbyte MChai = 5;

		// Token: 0x04003554 RID: 13652
		public static sbyte MOshin = 6;

		// Token: 0x04003555 RID: 13653
		public static sbyte MEchigo = 7;

		// Token: 0x04003556 RID: 13654
		public static sbyte MKojin = 8;

		// Token: 0x04003557 RID: 13655
		public static sbyte MHaruna = 9;

		// Token: 0x04003558 RID: 13656
		public static sbyte MHirosaki = 10;

		// Token: 0x04003559 RID: 13657
		public static sbyte MOokaza = 11;

		// Token: 0x0400355A RID: 13658
		public static sbyte MGiotuyet = 12;

		// Token: 0x0400355B RID: 13659
		public static sbyte MHangdong = 13;

		// Token: 0x0400355C RID: 13660
		public static sbyte MDeKeu = 14;

		// Token: 0x0400355D RID: 13661
		public static sbyte MChimKeu = 15;

		// Token: 0x0400355E RID: 13662
		public static sbyte MBuocChan = 16;

		// Token: 0x0400355F RID: 13663
		public static sbyte MNuocChay = 17;

		// Token: 0x04003560 RID: 13664
		public static sbyte MBomMau = 18;

		// Token: 0x04003561 RID: 13665
		public static sbyte MKiemGo = 19;

		// Token: 0x04003562 RID: 13666
		public static sbyte MKiem = 20;

		// Token: 0x04003563 RID: 13667
		public static sbyte MTieu = 21;

		// Token: 0x04003564 RID: 13668
		public static sbyte MKunai = 22;

		// Token: 0x04003565 RID: 13669
		public static sbyte MCung = 23;

		// Token: 0x04003566 RID: 13670
		public static sbyte MDao = 24;

		// Token: 0x04003567 RID: 13671
		public static sbyte MQuat = 25;

		// Token: 0x04003568 RID: 13672
		public static sbyte MCung2 = 26;

		// Token: 0x04003569 RID: 13673
		public static sbyte MTieu2 = 27;

		// Token: 0x0400356A RID: 13674
		public static sbyte MTieu3 = 28;

		// Token: 0x0400356B RID: 13675
		public static sbyte MKiem2 = 29;

		// Token: 0x0400356C RID: 13676
		public static sbyte MKiem3 = 30;

		// Token: 0x0400356D RID: 13677
		public static sbyte MDao2 = 31;

		// Token: 0x0400356E RID: 13678
		public static sbyte MDao3 = 32;

		// Token: 0x0400356F RID: 13679
		public static sbyte MCung3 = 33;

		// Token: 0x04003570 RID: 13680
		public static int l1;
	}
}
