using System;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

namespace Game6
{
	// Token: 0x02000063 RID: 99
	public class Main : MonoBehaviour
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x00049948 File Offset: 0x00047B48
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

		// Token: 0x060003F8 RID: 1016 RVA: 0x00049A3A File Offset: 0x00047C3A
		private void SetInit()
		{
			base.enabled = true;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00049A43 File Offset: 0x00047C43
		private void OnHideUnity(bool isGameShown)
		{
			if (!isGameShown)
			{
				Time.timeScale = 0f;
				return;
			}
			Time.timeScale = 1f;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00049A60 File Offset: 0x00047C60
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
			else if (TabManagement.tab == TabType.Tab6)
			{
				this.checkInput();
			}
			Session_ME.update();
			Session_ME2.update();
			if (TabManagement.tab == TabType.Tab6 && Event.current.type.Equals(EventType.Repaint) && this.paintCount <= this.updateCount)
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

		// Token: 0x060003FB RID: 1019 RVA: 0x00049B60 File Offset: 0x00047D60
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

		// Token: 0x060003FC RID: 1020 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void setBackupIcloud(string path)
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00049CAC File Offset: 0x00047EAC
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

		// Token: 0x060003FE RID: 1022 RVA: 0x00049CFA File Offset: 0x00047EFA
		public void doClearRMS()
		{
			if (Main.isPC && Rms.loadRMSInt("lastZoomlevel") != mGraphics.zoomLevel)
			{
				Rms.clearAll();
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				Rms.saveRMSInt("levelScreenKN", this.level);
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00049D38 File Offset: 0x00047F38
		public static void closeKeyBoard()
		{
			if (TouchScreenKeyboard.visible)
			{
				TField.kb.active = false;
				TField.kb = null;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00049D54 File Offset: 0x00047F54
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

		// Token: 0x06000401 RID: 1025 RVA: 0x00049E34 File Offset: 0x00048034
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

		// Token: 0x06000402 RID: 1026 RVA: 0x0004A0A1 File Offset: 0x000482A1
		private void Awake()
		{
			if (Main.main != null)
			{
				TabManagement.tab = TabType.Tab6;
				TabController.updateCaption();
				UnityEngine.Object.Destroy(base.gameObject);
				SoundMn.gI().loadSound(TileMap.mapID);
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0004A0E1 File Offset: 0x000482E1
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

		// Token: 0x06000404 RID: 1028 RVA: 0x0004A114 File Offset: 0x00048314
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

		// Token: 0x06000405 RID: 1029 RVA: 0x0004A163 File Offset: 0x00048363
		public static void exit()
		{
			if (Main.isPC)
			{
				Main.main.OnApplicationQuit();
				return;
			}
			Main.a = 0;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0004A17D File Offset: 0x0004837D
		public static bool detectCompactDevice()
		{
			return iPhoneSettings.generation != iPhoneGeneration.iPhone && iPhoneSettings.generation != iPhoneGeneration.iPhone3G && iPhoneSettings.generation != iPhoneGeneration.iPodTouch1Gen && iPhoneSettings.generation != iPhoneGeneration.iPodTouch2Gen;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0004A1A2 File Offset: 0x000483A2
		public static bool checkCanSendSMS()
		{
			return iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen;
		}

		// Token: 0x040008A9 RID: 2217
		public static Main main;

		// Token: 0x040008AA RID: 2218
		public static mGraphics g;

		// Token: 0x040008AB RID: 2219
		public static GameMidlet midlet;

		// Token: 0x040008AC RID: 2220
		public static string res = "res";

		// Token: 0x040008AD RID: 2221
		public static string mainThreadName;

		// Token: 0x040008AE RID: 2222
		public static bool started;

		// Token: 0x040008AF RID: 2223
		public static bool isIpod;

		// Token: 0x040008B0 RID: 2224
		public static bool isIphone4;

		// Token: 0x040008B1 RID: 2225
		public static bool isPC;

		// Token: 0x040008B2 RID: 2226
		public static bool isWindowsPhone;

		// Token: 0x040008B3 RID: 2227
		public static bool isIPhone;

		// Token: 0x040008B4 RID: 2228
		public static bool IphoneVersionApp;

		// Token: 0x040008B5 RID: 2229
		public static string IMEI;

		// Token: 0x040008B6 RID: 2230
		public static int versionIp;

		// Token: 0x040008B7 RID: 2231
		public static int numberQuit = 1;

		// Token: 0x040008B8 RID: 2232
		public static int typeClient = 4;

		// Token: 0x040008B9 RID: 2233
		public const sbyte PC_VERSION = 4;

		// Token: 0x040008BA RID: 2234
		public const sbyte IP_APPSTORE = 5;

		// Token: 0x040008BB RID: 2235
		public const sbyte WINDOWSPHONE = 6;

		// Token: 0x040008BC RID: 2236
		private int level;

		// Token: 0x040008BD RID: 2237
		public const sbyte IP_JB = 3;

		// Token: 0x040008BE RID: 2238
		private int updateCount;

		// Token: 0x040008BF RID: 2239
		private int paintCount;

		// Token: 0x040008C0 RID: 2240
		private int count;

		// Token: 0x040008C1 RID: 2241
		private int fps;

		// Token: 0x040008C2 RID: 2242
		private int max;

		// Token: 0x040008C3 RID: 2243
		private int up;

		// Token: 0x040008C4 RID: 2244
		private int upmax;

		// Token: 0x040008C5 RID: 2245
		private long timefps;

		// Token: 0x040008C6 RID: 2246
		private long timeup;

		// Token: 0x040008C7 RID: 2247
		private bool isRun;

		// Token: 0x040008C8 RID: 2248
		public static int waitTick;

		// Token: 0x040008C9 RID: 2249
		public static int f;

		// Token: 0x040008CA RID: 2250
		public static bool isResume;

		// Token: 0x040008CB RID: 2251
		public static bool isMiniApp = true;

		// Token: 0x040008CC RID: 2252
		public static bool isQuitApp;

		// Token: 0x040008CD RID: 2253
		private Vector2 lastMousePos;

		// Token: 0x040008CE RID: 2254
		public static int a = 1;

		// Token: 0x040008CF RID: 2255
		public static bool isCompactDevice = true;
	}
}
