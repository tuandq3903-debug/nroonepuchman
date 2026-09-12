using System;
using System.Threading;
using UnityEngine;

namespace Game1
{
	// Token: 0x020004E9 RID: 1257
	public class Sound
	{
		// Token: 0x06003846 RID: 14406 RVA: 0x0036FADC File Offset: 0x0036DCDC
		public static void init()
		{
			if (GameObject.Find("Audio Player 1") != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "Audio Player 1";
			gameObject.transform.position = Vector3.zero;
			gameObject.AddComponent<AudioListener>();
			Sound.SoundBGLoop = gameObject.AddComponent<AudioSource>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}

		// Token: 0x06003847 RID: 14407 RVA: 0x0036FB34 File Offset: 0x0036DD34
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

		// Token: 0x06003848 RID: 14408 RVA: 0x0036FBCD File Offset: 0x0036DDCD
		public static void playSound(int id, float volume)
		{
			Sound.play(id + Sound.l1, volume);
		}

		// Token: 0x06003849 RID: 14409 RVA: 0x0036FBDC File Offset: 0x0036DDDC
		public static void playSound1(int id, float volume)
		{
			Sound.play(id, volume);
		}

		// Token: 0x0600384A RID: 14410 RVA: 0x0036FBE5 File Offset: 0x0036DDE5
		public static void getAssetSoundFile(string fileName, int pos)
		{
			Sound.stop(pos);
			string empty = string.Empty;
			Sound.load(Main.res + fileName, pos);
		}

		// Token: 0x0600384B RID: 14411 RVA: 0x0036FC04 File Offset: 0x0036DE04
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

		// Token: 0x0600384C RID: 14412 RVA: 0x0036FC3F File Offset: 0x0036DE3F
		public static void play(int id, float volume)
		{
			if (!Sound.isNotPlay && GameCanvas.isPlaySound)
			{
				Sound.start(volume, id);
			}
		}

		// Token: 0x0600384D RID: 14413 RVA: 0x0036FC56 File Offset: 0x0036DE56
		public static bool isPlayingSound()
		{
			return !(Sound.SoundRun == null) && Sound.SoundRun.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x0600384E RID: 14414 RVA: 0x0036FC76 File Offset: 0x0036DE76
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

		// Token: 0x0600384F RID: 14415 RVA: 0x0036FCA0 File Offset: 0x0036DEA0
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

		// Token: 0x06003850 RID: 14416 RVA: 0x0036FD24 File Offset: 0x0036DF24
		public static void sTopSoundBG(int id)
		{
			Sound.SoundBGLoop.GetComponent<AudioSource>().Stop();
		}

		// Token: 0x06003851 RID: 14417 RVA: 0x0036FD35 File Offset: 0x0036DF35
		public static bool isPlayingSoundBG(int id)
		{
			return !(Sound.SoundBGLoop == null) && Sound.SoundBGLoop.GetComponent<AudioSource>().isPlaying;
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x0036FD55 File Offset: 0x0036DF55
		public static void load(string filename, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__load(filename, pos);
				return;
			}
			Sound._load(filename, pos);
		}

		// Token: 0x06003853 RID: 14419 RVA: 0x0036FD7C File Offset: 0x0036DF7C
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

		// Token: 0x06003854 RID: 14420 RVA: 0x0036FE27 File Offset: 0x0036E027
		private static void __load(string filename, int pos)
		{
			Sound.music[pos] = (AudioClip)Resources.Load(filename, typeof(AudioClip));
			GameObject.Find("Audio Player 1").AddComponent<AudioSource>();
			Sound.player[pos] = GameObject.Find("Audio Player 1");
		}

		// Token: 0x06003855 RID: 14421 RVA: 0x0036FE66 File Offset: 0x0036E066
		public static void start(float volume, int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__start(volume, pos);
				return;
			}
			Sound._start(volume, pos);
		}

		// Token: 0x06003856 RID: 14422 RVA: 0x0036FE90 File Offset: 0x0036E090
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

		// Token: 0x06003857 RID: 14423 RVA: 0x0036FF0A File Offset: 0x0036E10A
		public static void __start(float volume, int pos)
		{
			if (!(Sound.player[pos] == null))
			{
				Sound.player[pos].GetComponent<AudioSource>().PlayOneShot(Sound.music[pos], volume);
			}
		}

		// Token: 0x06003858 RID: 14424 RVA: 0x0036FF34 File Offset: 0x0036E134
		public static void stop(int pos)
		{
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				Sound.__stop(pos);
				return;
			}
			Sound._stop(pos);
		}

		// Token: 0x06003859 RID: 14425 RVA: 0x0036FF5C File Offset: 0x0036E15C
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

		// Token: 0x0600385A RID: 14426 RVA: 0x0036FFD0 File Offset: 0x0036E1D0
		public static void __stop(int pos)
		{
			if (Sound.player[pos] != null)
			{
				Sound.player[pos].GetComponent<AudioSource>().Stop();
			}
		}

		// Token: 0x04006CC1 RID: 27841
		public static int status;

		// Token: 0x04006CC2 RID: 27842
		public static int postem;

		// Token: 0x04006CC3 RID: 27843
		private static string filenametemp;

		// Token: 0x04006CC4 RID: 27844
		private static float volumetem;

		// Token: 0x04006CC5 RID: 27845
		public static bool isSound = true;

		// Token: 0x04006CC6 RID: 27846
		public static bool isNotPlay;

		// Token: 0x04006CC7 RID: 27847
		public static bool stopAll;

		// Token: 0x04006CC8 RID: 27848
		public static AudioSource SoundRun;

		// Token: 0x04006CC9 RID: 27849
		public static AudioSource SoundBGLoop;

		// Token: 0x04006CCA RID: 27850
		public static AudioClip[] music;

		// Token: 0x04006CCB RID: 27851
		public static GameObject[] player;

		// Token: 0x04006CCC RID: 27852
		public static sbyte MBClick = 1;

		// Token: 0x04006CCD RID: 27853
		public static sbyte MTone = 2;

		// Token: 0x04006CCE RID: 27854
		public static sbyte MSanzu = 3;

		// Token: 0x04006CCF RID: 27855
		public static sbyte MChakumi = 4;

		// Token: 0x04006CD0 RID: 27856
		public static sbyte MChai = 5;

		// Token: 0x04006CD1 RID: 27857
		public static sbyte MOshin = 6;

		// Token: 0x04006CD2 RID: 27858
		public static sbyte MEchigo = 7;

		// Token: 0x04006CD3 RID: 27859
		public static sbyte MKojin = 8;

		// Token: 0x04006CD4 RID: 27860
		public static sbyte MHaruna = 9;

		// Token: 0x04006CD5 RID: 27861
		public static sbyte MHirosaki = 10;

		// Token: 0x04006CD6 RID: 27862
		public static sbyte MOokaza = 11;

		// Token: 0x04006CD7 RID: 27863
		public static sbyte MGiotuyet = 12;

		// Token: 0x04006CD8 RID: 27864
		public static sbyte MHangdong = 13;

		// Token: 0x04006CD9 RID: 27865
		public static sbyte MDeKeu = 14;

		// Token: 0x04006CDA RID: 27866
		public static sbyte MChimKeu = 15;

		// Token: 0x04006CDB RID: 27867
		public static sbyte MBuocChan = 16;

		// Token: 0x04006CDC RID: 27868
		public static sbyte MNuocChay = 17;

		// Token: 0x04006CDD RID: 27869
		public static sbyte MBomMau = 18;

		// Token: 0x04006CDE RID: 27870
		public static sbyte MKiemGo = 19;

		// Token: 0x04006CDF RID: 27871
		public static sbyte MKiem = 20;

		// Token: 0x04006CE0 RID: 27872
		public static sbyte MTieu = 21;

		// Token: 0x04006CE1 RID: 27873
		public static sbyte MKunai = 22;

		// Token: 0x04006CE2 RID: 27874
		public static sbyte MCung = 23;

		// Token: 0x04006CE3 RID: 27875
		public static sbyte MDao = 24;

		// Token: 0x04006CE4 RID: 27876
		public static sbyte MQuat = 25;

		// Token: 0x04006CE5 RID: 27877
		public static sbyte MCung2 = 26;

		// Token: 0x04006CE6 RID: 27878
		public static sbyte MTieu2 = 27;

		// Token: 0x04006CE7 RID: 27879
		public static sbyte MTieu3 = 28;

		// Token: 0x04006CE8 RID: 27880
		public static sbyte MKiem2 = 29;

		// Token: 0x04006CE9 RID: 27881
		public static sbyte MKiem3 = 30;

		// Token: 0x04006CEA RID: 27882
		public static sbyte MDao2 = 31;

		// Token: 0x04006CEB RID: 27883
		public static sbyte MDao3 = 32;

		// Token: 0x04006CEC RID: 27884
		public static sbyte MCung3 = 33;

		// Token: 0x04006CED RID: 27885
		public static int l1;
	}
}
