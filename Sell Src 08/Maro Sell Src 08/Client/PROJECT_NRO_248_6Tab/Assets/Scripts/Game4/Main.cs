using System;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

namespace Game4
{
	// Token: 0x02000213 RID: 531
	public class Main : MonoBehaviour
	{
		// Token: 0x0600173F RID: 5951 RVA: 0x00173B94 File Offset: 0x00171D94
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

		// Token: 0x06001740 RID: 5952 RVA: 0x00049A3A File Offset: 0x00047C3A
		private void SetInit()
		{
			base.enabled = true;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00049A43 File Offset: 0x00047C43
		private void OnHideUnity(bool isGameShown)
		{
			if (!isGameShown)
			{
				Time.timeScale = 0f;
				return;
			}
			Time.timeScale = 1f;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x00173C88 File Offset: 0x00171E88
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
			else if (TabManagement.tab == TabType.Tab4)
			{
				this.checkInput();
			}
			Session_ME.update();
			Session_ME2.update();
			if (TabManagement.tab == TabType.Tab4 && Event.current.type.Equals(EventType.Repaint) && this.paintCount <= this.updateCount)
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

		// Token: 0x06001743 RID: 5955 RVA: 0x00173D88 File Offset: 0x00171F88
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

		// Token: 0x06001744 RID: 5956 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void setBackupIcloud(string path)
		{
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00173ED4 File Offset: 0x001720D4
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

		// Token: 0x06001746 RID: 5958 RVA: 0x00173F22 File Offset: 0x00172122
		public void doClearRMS()
		{
			if (Main.isPC && Rms.loadRMSInt("lastZoomlevel") != mGraphics.zoomLevel)
			{
				Rms.clearAll();
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				Rms.saveRMSInt("levelScreenKN", this.level);
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00173F60 File Offset: 0x00172160
		public static void closeKeyBoard()
		{
			if (TouchScreenKeyboard.visible)
			{
				TField.kb.active = false;
				TField.kb = null;
			}
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00173F7C File Offset: 0x0017217C
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

		// Token: 0x06001749 RID: 5961 RVA: 0x0017405C File Offset: 0x0017225C
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

		// Token: 0x0600174A RID: 5962 RVA: 0x001742C9 File Offset: 0x001724C9
		private void Awake()
		{
			if (Main.main != null)
			{
				TabManagement.tab = TabType.Tab4;
				TabController.updateCaption();
				UnityEngine.Object.Destroy(base.gameObject);
				SoundMn.gI().loadSound(TileMap.mapID);
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00174309 File Offset: 0x00172509
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

		// Token: 0x0600174C RID: 5964 RVA: 0x0017433C File Offset: 0x0017253C
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

		// Token: 0x0600174D RID: 5965 RVA: 0x0017438B File Offset: 0x0017258B
		public static void exit()
		{
			if (Main.isPC)
			{
				Main.main.OnApplicationQuit();
				return;
			}
			Main.a = 0;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x001743A5 File Offset: 0x001725A5
		public static bool detectCompactDevice()
		{
			return iPhoneSettings.generation != iPhoneGeneration.iPhone && iPhoneSettings.generation != iPhoneGeneration.iPhone3G && iPhoneSettings.generation != iPhoneGeneration.iPodTouch1Gen && iPhoneSettings.generation != iPhoneGeneration.iPodTouch2Gen;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x001743CA File Offset: 0x001725CA
		public static bool checkCanSendSMS()
		{
			return iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen;
		}

		// Token: 0x04002DA7 RID: 11687
		public static Main main;

		// Token: 0x04002DA8 RID: 11688
		public static mGraphics g;

		// Token: 0x04002DA9 RID: 11689
		public static GameMidlet midlet;

		// Token: 0x04002DAA RID: 11690
		public static string res = "res";

		// Token: 0x04002DAB RID: 11691
		public static string mainThreadName;

		// Token: 0x04002DAC RID: 11692
		public static bool started;

		// Token: 0x04002DAD RID: 11693
		public static bool isIpod;

		// Token: 0x04002DAE RID: 11694
		public static bool isIphone4;

		// Token: 0x04002DAF RID: 11695
		public static bool isPC;

		// Token: 0x04002DB0 RID: 11696
		public static bool isWindowsPhone;

		// Token: 0x04002DB1 RID: 11697
		public static bool isIPhone;

		// Token: 0x04002DB2 RID: 11698
		public static bool IphoneVersionApp;

		// Token: 0x04002DB3 RID: 11699
		public static string IMEI;

		// Token: 0x04002DB4 RID: 11700
		public static int versionIp;

		// Token: 0x04002DB5 RID: 11701
		public static int numberQuit = 1;

		// Token: 0x04002DB6 RID: 11702
		public static int typeClient = 4;

		// Token: 0x04002DB7 RID: 11703
		public const sbyte PC_VERSION = 4;

		// Token: 0x04002DB8 RID: 11704
		public const sbyte IP_APPSTORE = 5;

		// Token: 0x04002DB9 RID: 11705
		public const sbyte WINDOWSPHONE = 6;

		// Token: 0x04002DBA RID: 11706
		private int level;

		// Token: 0x04002DBB RID: 11707
		public const sbyte IP_JB = 3;

		// Token: 0x04002DBC RID: 11708
		private int updateCount;

		// Token: 0x04002DBD RID: 11709
		private int paintCount;

		// Token: 0x04002DBE RID: 11710
		private int count;

		// Token: 0x04002DBF RID: 11711
		private int fps;

		// Token: 0x04002DC0 RID: 11712
		private int max;

		// Token: 0x04002DC1 RID: 11713
		private int up;

		// Token: 0x04002DC2 RID: 11714
		private int upmax;

		// Token: 0x04002DC3 RID: 11715
		private long timefps;

		// Token: 0x04002DC4 RID: 11716
		private long timeup;

		// Token: 0x04002DC5 RID: 11717
		private bool isRun;

		// Token: 0x04002DC6 RID: 11718
		public static int waitTick;

		// Token: 0x04002DC7 RID: 11719
		public static int f;

		// Token: 0x04002DC8 RID: 11720
		public static bool isResume;

		// Token: 0x04002DC9 RID: 11721
		public static bool isMiniApp = true;

		// Token: 0x04002DCA RID: 11722
		public static bool isQuitApp;

		// Token: 0x04002DCB RID: 11723
		private Vector2 lastMousePos;

		// Token: 0x04002DCC RID: 11724
		public static int a = 1;

		// Token: 0x04002DCD RID: 11725
		public static bool isCompactDevice = true;
	}
}
