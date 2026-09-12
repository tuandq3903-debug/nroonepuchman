using System;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

namespace Game2
{
	// Token: 0x020003C3 RID: 963
	public class Main : MonoBehaviour
	{
		// Token: 0x06002A87 RID: 10887 RVA: 0x0029DCDC File Offset: 0x0029BEDC
		private void Start()
		{
			if (Main.started)
			{
				return;
			}
			if (Thread.CurrentThread.Name != "Main")
			{
				Thread.CurrentThread.Name = "Main";
			}
			Main.mainThreadName = Thread.CurrentThread.Name;
			Main.isPC = (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer);
			Main.isIPhone = (Main.IphoneVersionApp = (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android));
			Main.started = true;
			if (Main.isPC && !Main.isIPhone)
			{
				this.level = Rms.loadRMSInt("levelScreenKN");
				if (this.level == 1)
				{
					Screen.SetResolution(1024, 600, false);
				}
				else
				{
					Screen.SetResolution(1366, 768, false);
				}
			}
			else if (Main.isIPhone)
			{
				Screen.fullScreen = true;
				GameCanvas.isTouch = true;
			}
			ModFunc.GI().LoadGame();
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x00049A3A File Offset: 0x00047C3A
		private void SetInit()
		{
			base.enabled = true;
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x00049A43 File Offset: 0x00047C43
		private void OnHideUnity(bool isGameShown)
		{
			if (!isGameShown)
			{
				Time.timeScale = 0f;
				return;
			}
			Time.timeScale = 1f;
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x0029DDD0 File Offset: 0x0029BFD0
		private void OnGUI()
		{
			if (this.count < 10)
			{
				return;
			}
			if (this.fps == 0)
			{
				this.timefps = mSystem.currentTimeMillis();
			}
			else if (mSystem.currentTimeMillis() - this.timefps > 1000L)
			{
				this.max = this.fps;
				this.fps = 0;
				this.timefps = mSystem.currentTimeMillis();
			}
			this.fps++;
			if (TabManagement.SyncTab)
			{
				this.checkInput();
			}
			else if (TabManagement.tab == TabType.Tab2)
			{
				this.checkInput();
			}
			Session_ME.update();
			Session_ME2.update();
			if (TabManagement.tab == TabType.Tab2 && Event.current.type.Equals(EventType.Repaint) && this.paintCount <= this.updateCount)
			{
				if (GameMidlet.gameCanvas != null)
				{
					GameMidlet.gameCanvas.paint(Main.g);
				}
				this.paintCount++;
				if (Main.g != null)
				{
					Main.g.reset();
				}
			}
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0029DED0 File Offset: 0x0029C0D0
		public void setsizeChange()
		{
			if (!this.isRun)
			{
				Screen.orientation = ScreenOrientation.AutoRotation;
				Application.runInBackground = true;
				base.useGUILayout = false;
				Main.isCompactDevice = Main.detectCompactDevice();
				if (Main.main == null)
				{
					Main.main = this;
				}
				this.isRun = true;
				ScaleGUI.initScaleGUI();
				if (Main.isPC)
				{
					Main.IMEI = SystemInfo.deviceUniqueIdentifier;
				}
				else
				{
					Main.IMEI = this.GetMacAddress();
				}
				Main.isPC = true;
				if (Main.isPC && !Main.isIPhone)
				{
					Screen.fullScreen = false;
				}
				if (Main.isIPhone && !Main.isPC)
				{
					Screen.fullScreen = true;
				}
				if (Main.isPC)
				{
					Main.typeClient = 4;
				}
				if (Main.isWindowsPhone)
				{
					Main.typeClient = 6;
				}
				if (Main.isIPhone || Main.IphoneVersionApp)
				{
					Main.typeClient = 4;
				}
				if (iPhoneSettings.generation == iPhoneGeneration.iPodTouch4Gen)
				{
					Main.isIpod = true;
				}
				if (iPhoneSettings.generation == iPhoneGeneration.iPhone4)
				{
					Main.isIphone4 = true;
				}
				Main.g = new mGraphics();
				Main.midlet = new GameMidlet();
				TileMap.loadBg();
				Paint.loadbg();
				PopUp.loadBg();
				GameScr.loadBg();
				InfoMe.gI().loadCharId();
				Panel.loadBg();
				Menu.loadBg();
				TabCommand.loadBG();
				Key.mapKeyPC();
				SoundMn.gI().loadSound(TileMap.mapID);
				Main.g.CreateLineMaterial();
			}
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void setBackupIcloud(string path)
		{
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x0029E01C File Offset: 0x0029C21C
		public string GetMacAddress()
		{
			string empty = string.Empty;
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			for (int i = 0; i < allNetworkInterfaces.Length; i++)
			{
				PhysicalAddress physicalAddress = allNetworkInterfaces[i].GetPhysicalAddress();
				if (physicalAddress.ToString() != string.Empty)
				{
					return physicalAddress.ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x0029E06A File Offset: 0x0029C26A
		public void doClearRMS()
		{
			if (Main.isPC && Rms.loadRMSInt("lastZoomlevel") != mGraphics.zoomLevel)
			{
				Rms.clearAll();
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				Rms.saveRMSInt("levelScreenKN", this.level);
			}
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x0029E0A8 File Offset: 0x0029C2A8
		public static void closeKeyBoard()
		{
			if (TouchScreenKeyboard.visible)
			{
				TField.kb.active = false;
				TField.kb = null;
			}
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x0029E0C4 File Offset: 0x0029C2C4
		[Obsolete]
		private void FixedUpdate()
		{
			Rms.update();
			this.count++;
			if (this.count >= 10)
			{
				if (this.up == 0)
				{
					this.timeup = mSystem.currentTimeMillis();
				}
				else if (mSystem.currentTimeMillis() - this.timeup > 1000L)
				{
					this.upmax = this.up;
					this.up = 0;
					this.timeup = mSystem.currentTimeMillis();
				}
				this.up++;
				this.setsizeChange();
				this.updateCount++;
				ipKeyboard.update();
				if (GameMidlet.gameCanvas != null)
				{
					GameMidlet.gameCanvas.update();
				}
				Image.update();
				DataInputStream.update();
				Main.f++;
				if (Main.f > 8)
				{
					Main.f = 0;
				}
				if (!Main.isPC)
				{
					int num = 1 / Main.a;
				}
			}
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x0029E1A4 File Offset: 0x0029C3A4
		private void checkInput()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 mousePosition = Input.mousePosition;
				GameMidlet.gameCanvas.pointerPressed((int)(mousePosition.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
				this.lastMousePos.x = mousePosition.x / (float)mGraphics.zoomLevel;
				this.lastMousePos.y = mousePosition.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
			}
			if (Input.GetMouseButton(0))
			{
				Vector3 mousePosition2 = Input.mousePosition;
				GameMidlet.gameCanvas.pointerDragged((int)(mousePosition2.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition2.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
				this.lastMousePos.x = mousePosition2.x / (float)mGraphics.zoomLevel;
				this.lastMousePos.y = mousePosition2.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
			}
			if (Input.GetMouseButtonUp(0))
			{
				Vector3 mousePosition3 = Input.mousePosition;
				this.lastMousePos.x = mousePosition3.x / (float)mGraphics.zoomLevel;
				this.lastMousePos.y = mousePosition3.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
				GameMidlet.gameCanvas.pointerReleased((int)(mousePosition3.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition3.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			}
			if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
			{
				int num = MyKeyMap.map(Event.current.keyCode);
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					KeyCode keyCode = Event.current.keyCode;
					if (keyCode != KeyCode.Minus)
					{
						if (keyCode == KeyCode.Alpha2)
						{
							num = 64;
						}
					}
					else
					{
						num = 95;
					}
				}
				if (num != 0)
				{
					GameMidlet.gameCanvas.keyPressedz(num);
				}
			}
			if (Event.current.type == EventType.KeyUp)
			{
				int num2 = MyKeyMap.map(Event.current.keyCode);
				if (num2 != 0)
				{
					GameMidlet.gameCanvas.keyReleasedz(num2);
				}
			}
			if (Main.isPC)
			{
				GameMidlet.gameCanvas.scrollMouse((int)(Input.GetAxis("Mouse ScrollWheel") * 10f));
				int x3 = (int)Input.mousePosition.x;
				float y = Input.mousePosition.y;
				int x2 = x3 / mGraphics.zoomLevel;
				int y2 = (Screen.height - (int)y) / mGraphics.zoomLevel;
				GameMidlet.gameCanvas.pointerMouse(x2, y2);
			}
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x0029E411 File Offset: 0x0029C611
		private void Awake()
		{
			if (Main.main != null)
			{
				TabManagement.tab = TabType.Tab2;
				TabController.updateCaption();
				UnityEngine.Object.Destroy(base.gameObject);
				SoundMn.gI().loadSound(TileMap.mapID);
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x0029E451 File Offset: 0x0029C651
		private void OnApplicationQuit()
		{
			Debug.LogWarning("APP QUIT");
			GameCanvas.bRun = false;
			Session_ME.gI().close();
			Session_ME2.gI().close();
			if (Main.isPC)
			{
				Application.Quit();
			}
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x0029E484 File Offset: 0x0029C684
		private void OnApplicationPause(bool paused)
		{
			Main.isResume = false;
			if (paused)
			{
				if (GameCanvas.isWaiting())
				{
					Main.isQuitApp = true;
				}
			}
			else
			{
				Main.isResume = true;
			}
			if (TouchScreenKeyboard.visible)
			{
				TField.kb.active = false;
				TField.kb = null;
			}
			if (Main.isQuitApp)
			{
				Application.Quit();
			}
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x0029E4D3 File Offset: 0x0029C6D3
		public static void exit()
		{
			if (Main.isPC)
			{
				Main.main.OnApplicationQuit();
				return;
			}
			Main.a = 0;
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x0029E4ED File Offset: 0x0029C6ED
		public static bool detectCompactDevice()
		{
			return iPhoneSettings.generation != iPhoneGeneration.iPhone && iPhoneSettings.generation != iPhoneGeneration.iPhone3G && iPhoneSettings.generation != iPhoneGeneration.iPodTouch1Gen && iPhoneSettings.generation != iPhoneGeneration.iPodTouch2Gen;
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x0029E512 File Offset: 0x0029C712
		public static bool checkCanSendSMS()
		{
			return iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen;
		}

		// Token: 0x040052A5 RID: 21157
		public static Main main;

		// Token: 0x040052A6 RID: 21158
		public static mGraphics g;

		// Token: 0x040052A7 RID: 21159
		public static GameMidlet midlet;

		// Token: 0x040052A8 RID: 21160
		public static string res = "res";

		// Token: 0x040052A9 RID: 21161
		public static string mainThreadName;

		// Token: 0x040052AA RID: 21162
		public static bool started;

		// Token: 0x040052AB RID: 21163
		public static bool isIpod;

		// Token: 0x040052AC RID: 21164
		public static bool isIphone4;

		// Token: 0x040052AD RID: 21165
		public static bool isPC;

		// Token: 0x040052AE RID: 21166
		public static bool isWindowsPhone;

		// Token: 0x040052AF RID: 21167
		public static bool isIPhone;

		// Token: 0x040052B0 RID: 21168
		public static bool IphoneVersionApp;

		// Token: 0x040052B1 RID: 21169
		public static string IMEI;

		// Token: 0x040052B2 RID: 21170
		public static int versionIp;

		// Token: 0x040052B3 RID: 21171
		public static int numberQuit = 1;

		// Token: 0x040052B4 RID: 21172
		public static int typeClient = 4;

		// Token: 0x040052B5 RID: 21173
		public const sbyte PC_VERSION = 4;

		// Token: 0x040052B6 RID: 21174
		public const sbyte IP_APPSTORE = 5;

		// Token: 0x040052B7 RID: 21175
		public const sbyte WINDOWSPHONE = 6;

		// Token: 0x040052B8 RID: 21176
		private int level;

		// Token: 0x040052B9 RID: 21177
		public const sbyte IP_JB = 3;

		// Token: 0x040052BA RID: 21178
		private int updateCount;

		// Token: 0x040052BB RID: 21179
		private int paintCount;

		// Token: 0x040052BC RID: 21180
		private int count;

		// Token: 0x040052BD RID: 21181
		private int fps;

		// Token: 0x040052BE RID: 21182
		private int max;

		// Token: 0x040052BF RID: 21183
		private int up;

		// Token: 0x040052C0 RID: 21184
		private int upmax;

		// Token: 0x040052C1 RID: 21185
		private long timefps;

		// Token: 0x040052C2 RID: 21186
		private long timeup;

		// Token: 0x040052C3 RID: 21187
		private bool isRun;

		// Token: 0x040052C4 RID: 21188
		public static int waitTick;

		// Token: 0x040052C5 RID: 21189
		public static int f;

		// Token: 0x040052C6 RID: 21190
		public static bool isResume;

		// Token: 0x040052C7 RID: 21191
		public static bool isMiniApp = true;

		// Token: 0x040052C8 RID: 21192
		public static bool isQuitApp;

		// Token: 0x040052C9 RID: 21193
		private Vector2 lastMousePos;

		// Token: 0x040052CA RID: 21194
		public static int a = 1;

		// Token: 0x040052CB RID: 21195
		public static bool isCompactDevice = true;
	}
}
