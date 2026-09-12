using System;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

namespace Game5
{
	// Token: 0x0200013B RID: 315
	public class Main : MonoBehaviour
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x000DEAF0 File Offset: 0x000DCCF0
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

		// Token: 0x06000D9C RID: 3484 RVA: 0x00049A3A File Offset: 0x00047C3A
		private void SetInit()
		{
			base.enabled = true;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00049A43 File Offset: 0x00047C43
		private void OnHideUnity(bool isGameShown)
		{
			if (!isGameShown)
			{
				Time.timeScale = 0f;
				return;
			}
			Time.timeScale = 1f;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000DEBE4 File Offset: 0x000DCDE4
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
			else if (TabManagement.tab == TabType.Tab5)
			{
				this.checkInput();
			}
			Session_ME.update();
			Session_ME2.update();
			if (TabManagement.tab == TabType.Tab5 && Event.current.type.Equals(EventType.Repaint) && this.paintCount <= this.updateCount)
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

		// Token: 0x06000D9F RID: 3487 RVA: 0x000DECE4 File Offset: 0x000DCEE4
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

		// Token: 0x06000DA0 RID: 3488 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void setBackupIcloud(string path)
		{
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000DEE30 File Offset: 0x000DD030
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

		// Token: 0x06000DA2 RID: 3490 RVA: 0x000DEE7E File Offset: 0x000DD07E
		public void doClearRMS()
		{
			if (Main.isPC && Rms.loadRMSInt("lastZoomlevel") != mGraphics.zoomLevel)
			{
				Rms.clearAll();
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				Rms.saveRMSInt("levelScreenKN", this.level);
			}
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000DEEBC File Offset: 0x000DD0BC
		public static void closeKeyBoard()
		{
			if (TouchScreenKeyboard.visible)
			{
				TField.kb.active = false;
				TField.kb = null;
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000DEED8 File Offset: 0x000DD0D8
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

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000DEFB8 File Offset: 0x000DD1B8
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

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000DF225 File Offset: 0x000DD425
		private void Awake()
		{
			if (Main.main != null)
			{
				TabManagement.tab = TabType.Tab5;
				TabController.updateCaption();
				UnityEngine.Object.Destroy(base.gameObject);
				SoundMn.gI().loadSound(TileMap.mapID);
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x000DF265 File Offset: 0x000DD465
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

		// Token: 0x06000DA8 RID: 3496 RVA: 0x000DF298 File Offset: 0x000DD498
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

		// Token: 0x06000DA9 RID: 3497 RVA: 0x000DF2E7 File Offset: 0x000DD4E7
		public static void exit()
		{
			if (Main.isPC)
			{
				Main.main.OnApplicationQuit();
				return;
			}
			Main.a = 0;
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x000DF301 File Offset: 0x000DD501
		public static bool detectCompactDevice()
		{
			return iPhoneSettings.generation != iPhoneGeneration.iPhone && iPhoneSettings.generation != iPhoneGeneration.iPhone3G && iPhoneSettings.generation != iPhoneGeneration.iPodTouch1Gen && iPhoneSettings.generation != iPhoneGeneration.iPodTouch2Gen;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000DF326 File Offset: 0x000DD526
		public static bool checkCanSendSMS()
		{
			return iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen;
		}

		// Token: 0x04001B28 RID: 6952
		public static Main main;

		// Token: 0x04001B29 RID: 6953
		public static mGraphics g;

		// Token: 0x04001B2A RID: 6954
		public static GameMidlet midlet;

		// Token: 0x04001B2B RID: 6955
		public static string res = "res";

		// Token: 0x04001B2C RID: 6956
		public static string mainThreadName;

		// Token: 0x04001B2D RID: 6957
		public static bool started;

		// Token: 0x04001B2E RID: 6958
		public static bool isIpod;

		// Token: 0x04001B2F RID: 6959
		public static bool isIphone4;

		// Token: 0x04001B30 RID: 6960
		public static bool isPC;

		// Token: 0x04001B31 RID: 6961
		public static bool isWindowsPhone;

		// Token: 0x04001B32 RID: 6962
		public static bool isIPhone;

		// Token: 0x04001B33 RID: 6963
		public static bool IphoneVersionApp;

		// Token: 0x04001B34 RID: 6964
		public static string IMEI;

		// Token: 0x04001B35 RID: 6965
		public static int versionIp;

		// Token: 0x04001B36 RID: 6966
		public static int numberQuit = 1;

		// Token: 0x04001B37 RID: 6967
		public static int typeClient = 4;

		// Token: 0x04001B38 RID: 6968
		public const sbyte PC_VERSION = 4;

		// Token: 0x04001B39 RID: 6969
		public const sbyte IP_APPSTORE = 5;

		// Token: 0x04001B3A RID: 6970
		public const sbyte WINDOWSPHONE = 6;

		// Token: 0x04001B3B RID: 6971
		private int level;

		// Token: 0x04001B3C RID: 6972
		public const sbyte IP_JB = 3;

		// Token: 0x04001B3D RID: 6973
		private int updateCount;

		// Token: 0x04001B3E RID: 6974
		private int paintCount;

		// Token: 0x04001B3F RID: 6975
		private int count;

		// Token: 0x04001B40 RID: 6976
		private int fps;

		// Token: 0x04001B41 RID: 6977
		private int max;

		// Token: 0x04001B42 RID: 6978
		private int up;

		// Token: 0x04001B43 RID: 6979
		private int upmax;

		// Token: 0x04001B44 RID: 6980
		private long timefps;

		// Token: 0x04001B45 RID: 6981
		private long timeup;

		// Token: 0x04001B46 RID: 6982
		private bool isRun;

		// Token: 0x04001B47 RID: 6983
		public static int waitTick;

		// Token: 0x04001B48 RID: 6984
		public static int f;

		// Token: 0x04001B49 RID: 6985
		public static bool isResume;

		// Token: 0x04001B4A RID: 6986
		public static bool isMiniApp = true;

		// Token: 0x04001B4B RID: 6987
		public static bool isQuitApp;

		// Token: 0x04001B4C RID: 6988
		private Vector2 lastMousePos;

		// Token: 0x04001B4D RID: 6989
		public static int a = 1;

		// Token: 0x04001B4E RID: 6990
		public static bool isCompactDevice = true;
	}
}
