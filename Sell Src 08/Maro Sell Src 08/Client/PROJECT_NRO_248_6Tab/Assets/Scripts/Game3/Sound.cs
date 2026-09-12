using System;
using System.Threading;
using UnityEngine;

namespace Game3
{
	// Token: 0x02000339 RID: 825
	public class Sound
	{
		// Token: 0x060024FE RID: 9470 RVA: 0x00245994 File Offset: 0x00243B94
		public static void init()
		{
			if (GameObject.Find("Audio Player 3") != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "Audio Player 3";
			gameObject.transform.position = Vector3.zero;
			gameObject.AddComponent<AudioListener>();
			Sound.SoundBGLoop = gameObject.AddComponent<AudioSource>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x002459EC File Offset: 0x00243BEC
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

		// Token: 0x06002500 RID: 9472 RVA: 0x00245A85 File Offset: 0x00243C85
		public static void playSound(int id, float volume)
		{
			Sound.play(id + Sound.l1, volume);
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x00245A94 File Offset: 0x00243C94
		public static void playSound1(int id, float volume)
		{
			Sound.play(id, volume);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00245A9D File Offset: 0x00243C9D
		public static void getAssetSoundFile(string fileName, int pos)
		{
			Sound.stop(pos);
			string empty = string.Empty;
			Sound.load(Main.res + fileName, pos);
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x00245ABC File Offset: 0x00243CBC
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

		// Token: 0x06002504 RID: 9476 RVA: 0x00245AF7 File Offset: 0x00243CF7
		public static void play(int id, float volume)
		{
			if (!Sound.isNotPlay && GameCanvas.isPlaySound)
			{
				Sound.start(volume, id);
			}
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x00245B0E File Offset: 0x00243D0E
		public static bool isPlayingSound()
		{
			return !(Sound.SoundRun == null) && Sound.SoundRun.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x00245B2E File Offset: 0x00243D2E
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

		// Token: 0x06002507 RID: 9479 RVA: 0x00245B58 File Offset: 0x00243D58
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

		// Token: 0x06002508 RID: 9480 RVA: 0x00245BDC File Offset: 0x00243DDC
		public static void sTopSoundBG(int id)
		{
			Sound.SoundBGLoop.GetComponent<AudioSource>().Stop();
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x00245BED File Offset: 0x00243DED
		public static bool isPlayingSoundBG(int id)
		{
			return !(Sound.SoundBGLoop == null) && Sound.SoundBGLoop.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x00245C0D File Offset: 0x00243E0D
		public static void load(string filename, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__load(filename, pos);
				return;
			}
			Sound._load(filename, pos);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x00245C34 File Offset: 0x00243E34
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

		// Token: 0x0600250C RID: 9484 RVA: 0x00245CDF File Offset: 0x00243EDF
		private static void __load(string filename, int pos)
		{
			Sound.music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
			GameObject.Find("Audio Player 3").AddComponent<AudioSource>();
			Sound.player[pos] = GameObject.Find("Audio Player 3");
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x00245D1E File Offset: 0x00243F1E
		public static void start(float volume, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__start(volume, pos);
				return;
			}
			Sound._start(volume, pos);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x00245D48 File Offset: 0x00243F48
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

		// Token: 0x0600250F RID: 9487 RVA: 0x00245DC2 File Offset: 0x00243FC2
		public static void __start(float volume, int pos)
		{
			if (!(Sound.player[pos] == null))
			{
				Sound.player[pos].GetComponent<AudioSource>().PlayOneShot(Sound.music[pos], volume);
			}
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00245DEC File Offset: 0x00243FEC
		public static void stop(int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__stop(pos);
				return;
			}
			Sound._stop(pos);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00245E14 File Offset: 0x00244014
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

		// Token: 0x06002512 RID: 9490 RVA: 0x00245E88 File Offset: 0x00244088
		public static void __stop(int pos)
		{
			if (Sound.player[pos] != null)
			{
				Sound.player[pos].GetComponent<AudioSource>().Stop();
			}
		}

		// Token: 0x040047C3 RID: 18371
		public static int status;

		// Token: 0x040047C4 RID: 18372
		public static int postem;

		// Token: 0x040047C5 RID: 18373
		private static string filenametemp;

		// Token: 0x040047C6 RID: 18374
		private static float volumetem;

		// Token: 0x040047C7 RID: 18375
		public static bool isSound = true;

		// Token: 0x040047C8 RID: 18376
		public static bool isNotPlay;

		// Token: 0x040047C9 RID: 18377
		public static bool stopAll;

		// Token: 0x040047CA RID: 18378
		public static AudioSource SoundRun;

		// Token: 0x040047CB RID: 18379
		public static AudioSource SoundBGLoop;

		// Token: 0x040047CC RID: 18380
		public static AudioClip[] music;

		// Token: 0x040047CD RID: 18381
		public static GameObject[] player;

		// Token: 0x040047CE RID: 18382
		public static sbyte MBClick = 1;

		// Token: 0x040047CF RID: 18383
		public static sbyte MTone = 2;

		// Token: 0x040047D0 RID: 18384
		public static sbyte MSanzu = 3;

		// Token: 0x040047D1 RID: 18385
		public static sbyte MChakumi = 4;

		// Token: 0x040047D2 RID: 18386
		public static sbyte MChai = 5;

		// Token: 0x040047D3 RID: 18387
		public static sbyte MOshin = 6;

		// Token: 0x040047D4 RID: 18388
		public static sbyte MEchigo = 7;

		// Token: 0x040047D5 RID: 18389
		public static sbyte MKojin = 8;

		// Token: 0x040047D6 RID: 18390
		public static sbyte MHaruna = 9;

		// Token: 0x040047D7 RID: 18391
		public static sbyte MHirosaki = 10;

		// Token: 0x040047D8 RID: 18392
		public static sbyte MOokaza = 11;

		// Token: 0x040047D9 RID: 18393
		public static sbyte MGiotuyet = 12;

		// Token: 0x040047DA RID: 18394
		public static sbyte MHangdong = 13;

		// Token: 0x040047DB RID: 18395
		public static sbyte MDeKeu = 14;

		// Token: 0x040047DC RID: 18396
		public static sbyte MChimKeu = 15;

		// Token: 0x040047DD RID: 18397
		public static sbyte MBuocChan = 16;

		// Token: 0x040047DE RID: 18398
		public static sbyte MNuocChay = 17;

		// Token: 0x040047DF RID: 18399
		public static sbyte MBomMau = 18;

		// Token: 0x040047E0 RID: 18400
		public static sbyte MKiemGo = 19;

		// Token: 0x040047E1 RID: 18401
		public static sbyte MKiem = 20;

		// Token: 0x040047E2 RID: 18402
		public static sbyte MTieu = 21;

		// Token: 0x040047E3 RID: 18403
		public static sbyte MKunai = 22;

		// Token: 0x040047E4 RID: 18404
		public static sbyte MCung = 23;

		// Token: 0x040047E5 RID: 18405
		public static sbyte MDao = 24;

		// Token: 0x040047E6 RID: 18406
		public static sbyte MQuat = 25;

		// Token: 0x040047E7 RID: 18407
		public static sbyte MCung2 = 26;

		// Token: 0x040047E8 RID: 18408
		public static sbyte MTieu2 = 27;

		// Token: 0x040047E9 RID: 18409
		public static sbyte MTieu3 = 28;

		// Token: 0x040047EA RID: 18410
		public static sbyte MKiem2 = 29;

		// Token: 0x040047EB RID: 18411
		public static sbyte MKiem3 = 30;

		// Token: 0x040047EC RID: 18412
		public static sbyte MDao2 = 31;

		// Token: 0x040047ED RID: 18413
		public static sbyte MDao3 = 32;

		// Token: 0x040047EE RID: 18414
		public static sbyte MCung3 = 33;

		// Token: 0x040047EF RID: 18415
		public static int l1;
	}
}
