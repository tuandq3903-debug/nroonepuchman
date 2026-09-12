using System;
using System.Net.NetworkInformation;
using System.Threading;
using UnityEngine;

namespace Game3
{
	// Token: 0x020002EB RID: 747
	public class Main : MonoBehaviour
	{
		// Token: 0x060020E3 RID: 8419 RVA: 0x00208C38 File Offset: 0x00206E38
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

		// Token: 0x060020E4 RID: 8420 RVA: 0x00049A3A File Offset: 0x00047C3A
		private void SetInit()
		{
			base.enabled = true;
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x00049A43 File Offset: 0x00047C43
		private void OnHideUnity(bool isGameShown)
		{
			if (!isGameShown)
			{
				Time.timeScale = 0f;
				return;
			}
			Time.timeScale = 1f;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x00208D2C File Offset: 0x00206F2C
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
			else if (TabManagement.tab == TabType.Tab3)
			{
				this.checkInput();
			}
			Session_ME.update();
			Session_ME2.update();
			if (TabManagement.tab == TabType.Tab3 && Event.current.type.Equals(EventType.Repaint) && this.paintCount <= this.updateCount)
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

		// Token: 0x060020E7 RID: 8423 RVA: 0x00208E2C File Offset: 0x0020702C
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

		// Token: 0x060020E8 RID: 8424 RVA: 0x000034B9 File Offset: 0x000016B9
		public static void setBackupIcloud(string path)
		{
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x00208F78 File Offset: 0x00207178
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

		// Token: 0x060020EA RID: 8426 RVA: 0x00208FC6 File Offset: 0x002071C6
		public void doClearRMS()
		{
			if (Main.isPC && Rms.loadRMSInt("lastZoomlevel") != mGraphics.zoomLevel)
			{
				Rms.clearAll();
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				Rms.saveRMSInt("levelScreenKN", this.level);
			}
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x00209004 File Offset: 0x00207204
		public static void closeKeyBoard()
		{
			if (TouchScreenKeyboard.visible)
			{
				TField.kb.active = false;
				TField.kb = null;
			}
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x00209020 File Offset: 0x00207220
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

		// Token: 0x060020ED RID: 8429 RVA: 0x00209100 File Offset: 0x00207300
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

		// Token: 0x060020EE RID: 8430 RVA: 0x0020936D File Offset: 0x0020756D
		private void Awake()
		{
			if (Main.main != null)
			{
				TabManagement.tab = TabType.Tab3;
				TabController.updateCaption();
				UnityEngine.Object.Destroy(base.gameObject);
				SoundMn.gI().loadSound(TileMap.mapID);
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x002093AD File Offset: 0x002075AD
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

		// Token: 0x060020F0 RID: 8432 RVA: 0x002093E0 File Offset: 0x002075E0
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

		// Token: 0x060020F1 RID: 8433 RVA: 0x0020942F File Offset: 0x0020762F
		public static void exit()
		{
			if (Main.isPC)
			{
				Main.main.OnApplicationQuit();
				return;
			}
			Main.a = 0;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00209449 File Offset: 0x00207649
		public static bool detectCompactDevice()
		{
			return iPhoneSettings.generation != iPhoneGeneration.iPhone && iPhoneSettings.generation != iPhoneGeneration.iPhone3G && iPhoneSettings.generation != iPhoneGeneration.iPodTouch1Gen && iPhoneSettings.generation != iPhoneGeneration.iPodTouch2Gen;
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0020946E File Offset: 0x0020766E
		public static bool checkCanSendSMS()
		{
			return iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen;
		}

		// Token: 0x04004026 RID: 16422
		public static Main main;

		// Token: 0x04004027 RID: 16423
		public static mGraphics g;

		// Token: 0x04004028 RID: 16424
		public static GameMidlet midlet;

		// Token: 0x04004029 RID: 16425
		public static string res = "res";

		// Token: 0x0400402A RID: 16426
		public static string mainThreadName;

		// Token: 0x0400402B RID: 16427
		public static bool started;

		// Token: 0x0400402C RID: 16428
		public static bool isIpod;

		// Token: 0x0400402D RID: 16429
		public static bool isIphone4;

		// Token: 0x0400402E RID: 16430
		public static bool isPC;

		// Token: 0x0400402F RID: 16431
		public static bool isWindowsPhone;

		// Token: 0x04004030 RID: 16432
		public static bool isIPhone;

		// Token: 0x04004031 RID: 16433
		public static bool IphoneVersionApp;

		// Token: 0x04004032 RID: 16434
		public static string IMEI;

		// Token: 0x04004033 RID: 16435
		public static int versionIp;

		// Token: 0x04004034 RID: 16436
		public static int numberQuit = 1;

		// Token: 0x04004035 RID: 16437
		public static int typeClient = 4;

		// Token: 0x04004036 RID: 16438
		public const sbyte PC_VERSION = 4;

		// Token: 0x04004037 RID: 16439
		public const sbyte IP_APPSTORE = 5;

		// Token: 0x04004038 RID: 16440
		public const sbyte WINDOWSPHONE = 6;

		// Token: 0x04004039 RID: 16441
		private int level;

		// Token: 0x0400403A RID: 16442
		public const sbyte IP_JB = 3;

		// Token: 0x0400403B RID: 16443
		private int updateCount;

		// Token: 0x0400403C RID: 16444
		private int paintCount;

		// Token: 0x0400403D RID: 16445
		private int count;

		// Token: 0x0400403E RID: 16446
		private int fps;

		// Token: 0x0400403F RID: 16447
		private int max;

		// Token: 0x04004040 RID: 16448
		private int up;

		// Token: 0x04004041 RID: 16449
		private int upmax;

		// Token: 0x04004042 RID: 16450
		private long timefps;

		// Token: 0x04004043 RID: 16451
		private long timeup;

		// Token: 0x04004044 RID: 16452
		private bool isRun;

		// Token: 0x04004045 RID: 16453
		public static int waitTick;

		// Token: 0x04004046 RID: 16454
		public static int f;

		// Token: 0x04004047 RID: 16455
		public static bool isResume;

		// Token: 0x04004048 RID: 16456
		public static bool isMiniApp = true;

		// Token: 0x04004049 RID: 16457
		public static bool isQuitApp;

		// Token: 0x0400404A RID: 16458
		private Vector2 lastMousePos;

		// Token: 0x0400404B RID: 16459
		public static int a = 1;

		// Token: 0x0400404C RID: 16460
		public static bool isCompactDevice = true;
	}
}
