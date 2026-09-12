using System;

namespace Game4
{
	// Token: 0x02000228 RID: 552
	public class mResources
	{
		// Token: 0x0600186F RID: 6255 RVA: 0x00182BB2 File Offset: 0x00180DB2
		public static void loadLanguague(sbyte newLanguage)
		{
			mResources.language = newLanguage;
			T1.load();
			GameCanvas.isLoadRes = true;
		}

		// Token: 0x04003005 RID: 12293
		public static string confirmChangeServer = string.Empty;

		// Token: 0x04003006 RID: 12294
		public static string chooseDefaultsv = string.Empty;

		// Token: 0x04003007 RID: 12295
		public static string winLose = string.Empty;

		// Token: 0x04003008 RID: 12296
		public static string learnSkill = string.Empty;

		// Token: 0x04003009 RID: 12297
		public static string updSkill = string.Empty;

		// Token: 0x0400300A RID: 12298
		public static string proficiency = string.Empty;

		// Token: 0x0400300B RID: 12299
		public static string delacc = string.Empty;

		// Token: 0x0400300C RID: 12300
		public static string notiINAPP = string.Empty;

		// Token: 0x0400300D RID: 12301
		public static string notiRuby = string.Empty;

		// Token: 0x0400300E RID: 12302
		public static string equip = string.Empty;

		// Token: 0x0400300F RID: 12303
		public static string unlock = string.Empty;

		// Token: 0x04003010 RID: 12304
		public static string radaCard = string.Empty;

		// Token: 0x04003011 RID: 12305
		public static string not_enough_money_1 = string.Empty;

		// Token: 0x04003012 RID: 12306
		public static string napngoc = string.Empty;

		// Token: 0x04003013 RID: 12307
		public static string functionMaintain1 = string.Empty;

		// Token: 0x04003014 RID: 12308
		public static string tang;

		// Token: 0x04003015 RID: 12309
		public static string kquaVongQuay;

		// Token: 0x04003016 RID: 12310
		public static string useGem;

		// Token: 0x04003017 RID: 12311
		public static string autoFunction;

		// Token: 0x04003018 RID: 12312
		public static string choitiep;

		// Token: 0x04003019 RID: 12313
		public static string attack;

		// Token: 0x0400301A RID: 12314
		public static string defend;

		// Token: 0x0400301B RID: 12315
		public static string follow;

		// Token: 0x0400301C RID: 12316
		public static string status;

		// Token: 0x0400301D RID: 12317
		public static string gohome;

		// Token: 0x0400301E RID: 12318
		public static string pet;

		// Token: 0x0400301F RID: 12319
		public static string maychutathoacmatsong;

		// Token: 0x04003020 RID: 12320
		public static string cauhinhthap;

		// Token: 0x04003021 RID: 12321
		public static string cauhinhcao;

		// Token: 0x04003022 RID: 12322
		public static string combineSpell;

		// Token: 0x04003023 RID: 12323
		public static string combineFail;

		// Token: 0x04003024 RID: 12324
		public static string combineSuccess;

		// Token: 0x04003025 RID: 12325
		public static string turnOnAnalog;

		// Token: 0x04003026 RID: 12326
		public static string turnOffAnalog;

		// Token: 0x04003027 RID: 12327
		public static string analog;

		// Token: 0x04003028 RID: 12328
		public static string inventory_Pass;

		// Token: 0x04003029 RID: 12329
		public static string input_Inventory_Pass;

		// Token: 0x0400302A RID: 12330
		public static string input_Inventory_Pass_wrong = string.Empty;

		// Token: 0x0400302B RID: 12331
		public static string REGISTOPROTECT = string.Empty;

		// Token: 0x0400302C RID: 12332
		public static string turnOnSound = string.Empty;

		// Token: 0x0400302D RID: 12333
		public static string turnOffSound = string.Empty;

		// Token: 0x0400302E RID: 12334
		public static string REGISTERING = string.Empty;

		// Token: 0x0400302F RID: 12335
		public static string SENDINGMSG = string.Empty;

		// Token: 0x04003030 RID: 12336
		public static string SENTMSG = string.Empty;

		// Token: 0x04003031 RID: 12337
		public static string NOSENDMSG = string.Empty;

		// Token: 0x04003032 RID: 12338
		public static string sendMsgSuccess = string.Empty;

		// Token: 0x04003033 RID: 12339
		public static string cannotSendMsg = string.Empty;

		// Token: 0x04003034 RID: 12340
		public static string sendGuessMsgSuccess = string.Empty;

		// Token: 0x04003035 RID: 12341
		public static string sendMsgFail = string.Empty;

		// Token: 0x04003036 RID: 12342
		public static string ALERT_PRIVATE_PASS_1 = string.Empty;

		// Token: 0x04003037 RID: 12343
		public static string ALERT_PRIVATE_PASS_2 = string.Empty;

		// Token: 0x04003038 RID: 12344
		public static string INPUT_PRIVATE_PASS = string.Empty;

		// Token: 0x04003039 RID: 12345
		public static string change_account = string.Empty;

		// Token: 0x0400303A RID: 12346
		public static string alreadyHadAccount1 = string.Empty;

		// Token: 0x0400303B RID: 12347
		public static string alreadyHadAccount2 = string.Empty;

		// Token: 0x0400303C RID: 12348
		public static string userBlank = string.Empty;

		// Token: 0x0400303D RID: 12349
		public static string passwordBlank = string.Empty;

		// Token: 0x0400303E RID: 12350
		public static string accTooShort = string.Empty;

		// Token: 0x0400303F RID: 12351
		public static string phoneInvalid = string.Empty;

		// Token: 0x04003040 RID: 12352
		public static string emailInvalid = string.Empty;

		// Token: 0x04003041 RID: 12353
		public static string registerNewAcc = string.Empty;

		// Token: 0x04003042 RID: 12354
		public static string selectServer = string.Empty;

		// Token: 0x04003043 RID: 12355
		public static string selectServer2 = string.Empty;

		// Token: 0x04003044 RID: 12356
		public static string forgetPass = string.Empty;

		// Token: 0x04003045 RID: 12357
		public static string password = string.Empty;

		// Token: 0x04003046 RID: 12358
		public static string[] LOGINLABELS = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04003047 RID: 12359
		public static string msg = string.Empty;

		// Token: 0x04003048 RID: 12360
		public static string[] msgg = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04003049 RID: 12361
		public static string no_msg = string.Empty;

		// Token: 0x0400304A RID: 12362
		public static string cancelAccountProtection = string.Empty;

		// Token: 0x0400304B RID: 12363
		public static string plsCheckAcc = string.Empty;

		// Token: 0x0400304C RID: 12364
		public static string phone = string.Empty;

		// Token: 0x0400304D RID: 12365
		public static string email = string.Empty;

		// Token: 0x0400304E RID: 12366
		public static string acc = string.Empty;

		// Token: 0x0400304F RID: 12367
		public static string pwd = string.Empty;

		// Token: 0x04003050 RID: 12368
		public static string goToWebForPassword = string.Empty;

		// Token: 0x04003051 RID: 12369
		public static string dragon_ball = string.Empty;

		// Token: 0x04003052 RID: 12370
		public static string character = string.Empty;

		// Token: 0x04003053 RID: 12371
		public static string account = string.Empty;

		// Token: 0x04003054 RID: 12372
		public static string account_server = string.Empty;

		// Token: 0x04003055 RID: 12373
		public static string char_name_blank = string.Empty;

		// Token: 0x04003056 RID: 12374
		public static string char_name_short = string.Empty;

		// Token: 0x04003057 RID: 12375
		public static string char_name_long = string.Empty;

		// Token: 0x04003058 RID: 12376
		public static string changeNameChar = string.Empty;

		// Token: 0x04003059 RID: 12377
		public static string char_name = string.Empty;

		// Token: 0x0400305A RID: 12378
		public static string login = string.Empty;

		// Token: 0x0400305B RID: 12379
		public static string login2 = string.Empty;

		// Token: 0x0400305C RID: 12380
		public static string register = string.Empty;

		// Token: 0x0400305D RID: 12381
		public static string WAIT = string.Empty;

		// Token: 0x0400305E RID: 12382
		public static string PLEASEWAIT = string.Empty;

		// Token: 0x0400305F RID: 12383
		public static string CONNECTING = string.Empty;

		// Token: 0x04003060 RID: 12384
		public static string LOGGING = string.Empty;

		// Token: 0x04003061 RID: 12385
		public static string LOADING = string.Empty;

		// Token: 0x04003062 RID: 12386
		public static string downloading_data = string.Empty;

		// Token: 0x04003063 RID: 12387
		public static string select_server = string.Empty;

		// Token: 0x04003064 RID: 12388
		public static string pls_restart_game_error = string.Empty;

		// Token: 0x04003065 RID: 12389
		public static string pls_restart_game_error2 = string.Empty;

		// Token: 0x04003066 RID: 12390
		public static string lost_connection = string.Empty;

		// Token: 0x04003067 RID: 12391
		public static string check_3G = string.Empty;

		// Token: 0x04003068 RID: 12392
		public static string UPDATE = string.Empty;

		// Token: 0x04003069 RID: 12393
		public static string change_zone = string.Empty;

		// Token: 0x0400306A RID: 12394
		public static string select_zone = string.Empty;

		// Token: 0x0400306B RID: 12395
		public static string website = string.Empty;

		// Token: 0x0400306C RID: 12396
		public static string server = string.Empty;

		// Token: 0x0400306D RID: 12397
		public static string planet = string.Empty;

		// Token: 0x0400306E RID: 12398
		public static string[] MENUME = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400306F RID: 12399
		public static string[] MENUGENDER = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04003070 RID: 12400
		public static string[] CHAR_ORDER = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04003071 RID: 12401
		public static string[][] mainTab1 = new string[][]
		{
			new string[]
			{
				string.Empty,
				string.Empty
			},
			new string[]
			{
				string.Empty,
				string.Empty
			},
			new string[]
			{
				string.Empty,
				string.Empty
			},
			new string[]
			{
				string.Empty,
				string.Empty
			}
		};

		// Token: 0x04003072 RID: 12402
		public static string[][] mainTab2 = new string[][]
		{
			new string[]
			{
				string.Empty,
				string.Empty
			},
			new string[]
			{
				string.Empty,
				string.Empty
			},
			new string[]
			{
				string.Empty,
				string.Empty
			},
			new string[]
			{
				string.Empty,
				string.Empty
			},
			new string[]
			{
				string.Empty,
				string.Empty
			}
		};

		// Token: 0x04003073 RID: 12403
		public static string[][] petMainTab = new string[][]
		{
			new string[]
			{
				string.Empty,
				string.Empty
			},
			new string[]
			{
				string.Empty,
				string.Empty
			}
		};

		// Token: 0x04003074 RID: 12404
		public static string[][] petMainTab2 = new string[][]
		{
			new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty
			}
		};

		// Token: 0x04003075 RID: 12405
		public static string[] key_skill_qwerty = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04003076 RID: 12406
		public static string[] key_skill = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04003077 RID: 12407
		public static string SKILL_FAIL = string.Empty;

		// Token: 0x04003078 RID: 12408
		public static string HP_EMPTY = string.Empty;

		// Token: 0x04003079 RID: 12409
		public static string ZONE_HERE = string.Empty;

		// Token: 0x0400307A RID: 12410
		public static string[] DES_TASK = new string[]
		{
			" ",
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400307B RID: 12411
		public static string[] DIES = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400307C RID: 12412
		public static string[] SYNTHESIS = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400307D RID: 12413
		public static string[] tips = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400307E RID: 12414
		public static string TASK_INPUT_CLASS = string.Empty;

		// Token: 0x0400307F RID: 12415
		public static string SERI_NUM = string.Empty;

		// Token: 0x04003080 RID: 12416
		public static string CARD_CODE = string.Empty;

		// Token: 0x04003081 RID: 12417
		public static string pay_card = string.Empty;

		// Token: 0x04003082 RID: 12418
		public static string pay_card2 = string.Empty;

		// Token: 0x04003083 RID: 12419
		public static string serial_blank = string.Empty;

		// Token: 0x04003084 RID: 12420
		public static string card_code_blank = string.Empty;

		// Token: 0x04003085 RID: 12421
		public static string billion = string.Empty;

		// Token: 0x04003086 RID: 12422
		public static string million = string.Empty;

		// Token: 0x04003087 RID: 12423
		public static string MENU = string.Empty;

		// Token: 0x04003088 RID: 12424
		public static string CLOSE = string.Empty;

		// Token: 0x04003089 RID: 12425
		public static string ON = string.Empty;

		// Token: 0x0400308A RID: 12426
		public static string OFF = string.Empty;

		// Token: 0x0400308B RID: 12427
		public static string ENABLE = string.Empty;

		// Token: 0x0400308C RID: 12428
		public static string DELETE = string.Empty;

		// Token: 0x0400308D RID: 12429
		public static string VIEW = string.Empty;

		// Token: 0x0400308E RID: 12430
		public static string CONTINUE = string.Empty;

		// Token: 0x0400308F RID: 12431
		public static string NEXTSTEP = string.Empty;

		// Token: 0x04003090 RID: 12432
		public static string USE = string.Empty;

		// Token: 0x04003091 RID: 12433
		public static string SORT = string.Empty;

		// Token: 0x04003092 RID: 12434
		public static string YES = string.Empty;

		// Token: 0x04003093 RID: 12435
		public static string NO = string.Empty;

		// Token: 0x04003094 RID: 12436
		public static string EXIT = string.Empty;

		// Token: 0x04003095 RID: 12437
		public static string CHAT = string.Empty;

		// Token: 0x04003096 RID: 12438
		public static string REVENGE = string.Empty;

		// Token: 0x04003097 RID: 12439
		public static string OK = string.Empty;

		// Token: 0x04003098 RID: 12440
		public static string retry = string.Empty;

		// Token: 0x04003099 RID: 12441
		public static string uncheck = string.Empty;

		// Token: 0x0400309A RID: 12442
		public static string remember = string.Empty;

		// Token: 0x0400309B RID: 12443
		public static string ACCEPT = string.Empty;

		// Token: 0x0400309C RID: 12444
		public static string CANCEL = string.Empty;

		// Token: 0x0400309D RID: 12445
		public static string SELECT = string.Empty;

		// Token: 0x0400309E RID: 12446
		public static string enter = string.Empty;

		// Token: 0x0400309F RID: 12447
		public static string open_link = string.Empty;

		// Token: 0x040030A0 RID: 12448
		public static string DOYOUWANTEXIT = string.Empty;

		// Token: 0x040030A1 RID: 12449
		public static string NEWCHAR = string.Empty;

		// Token: 0x040030A2 RID: 12450
		public static string BACK = string.Empty;

		// Token: 0x040030A3 RID: 12451
		public static string LOCKED = string.Empty;

		// Token: 0x040030A4 RID: 12452
		public static string KILL = string.Empty;

		// Token: 0x040030A5 RID: 12453
		public static string KILLBOSS = string.Empty;

		// Token: 0x040030A6 RID: 12454
		public static string NOLOCK = string.Empty;

		// Token: 0x040030A7 RID: 12455
		public static string XU = string.Empty;

		// Token: 0x040030A8 RID: 12456
		public static string LUONG = string.Empty;

		// Token: 0x040030A9 RID: 12457
		public static string RUBY = string.Empty;

		// Token: 0x040030AA RID: 12458
		public static string PK_NOW = string.Empty;

		// Token: 0x040030AB RID: 12459
		public static string CUU_SAT = string.Empty;

		// Token: 0x040030AC RID: 12460
		public static string NOT_ENOUGH_MP = string.Empty;

		// Token: 0x040030AD RID: 12461
		public static string you_receive = string.Empty;

		// Token: 0x040030AE RID: 12462
		public static string MONTH = string.Empty;

		// Token: 0x040030AF RID: 12463
		public static string WEEK = string.Empty;

		// Token: 0x040030B0 RID: 12464
		public static string DAY = string.Empty;

		// Token: 0x040030B1 RID: 12465
		public static string HOUR = string.Empty;

		// Token: 0x040030B2 RID: 12466
		public static string SECOND = string.Empty;

		// Token: 0x040030B3 RID: 12467
		public static string MINUTE = string.Empty;

		// Token: 0x040030B4 RID: 12468
		public static string LEARN_SKILL = string.Empty;

		// Token: 0x040030B5 RID: 12469
		public static string rank = string.Empty;

		// Token: 0x040030B6 RID: 12470
		public static string active_point = string.Empty;

		// Token: 0x040030B7 RID: 12471
		public static string friend = string.Empty;

		// Token: 0x040030B8 RID: 12472
		public static string enemy = string.Empty;

		// Token: 0x040030B9 RID: 12473
		public static string no_friend = string.Empty;

		// Token: 0x040030BA RID: 12474
		public static string chat_world = string.Empty;

		// Token: 0x040030BB RID: 12475
		public static string change_flag = string.Empty;

		// Token: 0x040030BC RID: 12476
		public static string gameInfo = string.Empty;

		// Token: 0x040030BD RID: 12477
		public static string quayso = string.Empty;

		// Token: 0x040030BE RID: 12478
		public static string option = string.Empty;

		// Token: 0x040030BF RID: 12479
		public static string high = string.Empty;

		// Token: 0x040030C0 RID: 12480
		public static string medium = string.Empty;

		// Token: 0x040030C1 RID: 12481
		public static string low = string.Empty;

		// Token: 0x040030C2 RID: 12482
		public static string increase_vga = string.Empty;

		// Token: 0x040030C3 RID: 12483
		public static string decrease_vga = string.Empty;

		// Token: 0x040030C4 RID: 12484
		public static string serverchat_off = string.Empty;

		// Token: 0x040030C5 RID: 12485
		public static string serverchat_on = string.Empty;

		// Token: 0x040030C6 RID: 12486
		public static string x2Screen = string.Empty;

		// Token: 0x040030C7 RID: 12487
		public static string x1Screen = string.Empty;

		// Token: 0x040030C8 RID: 12488
		public static string changeSizeScreen = string.Empty;

		// Token: 0x040030C9 RID: 12489
		public static string aura_off = string.Empty;

		// Token: 0x040030CA RID: 12490
		public static string aura_on = string.Empty;

		// Token: 0x040030CB RID: 12491
		public static string aura_off_2 = string.Empty;

		// Token: 0x040030CC RID: 12492
		public static string aura_on_2 = string.Empty;

		// Token: 0x040030CD RID: 12493
		public static string hat_off = string.Empty;

		// Token: 0x040030CE RID: 12494
		public static string hat_on = string.Empty;

		// Token: 0x040030CF RID: 12495
		public static string chest = string.Empty;

		// Token: 0x040030D0 RID: 12496
		public static string[] chestt = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040030D1 RID: 12497
		public static string[] inventory = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040030D2 RID: 12498
		public static string[] combine = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040030D3 RID: 12499
		public static string[] mapp = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040030D4 RID: 12500
		public static string[] item_give = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040030D5 RID: 12501
		public static string[] item_receive = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040030D6 RID: 12502
		public static string[] zonee = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040030D7 RID: 12503
		public static string zone = string.Empty;

		// Token: 0x040030D8 RID: 12504
		public static string map = string.Empty;

		// Token: 0x040030D9 RID: 12505
		public static string item_receive2 = string.Empty;

		// Token: 0x040030DA RID: 12506
		public static string item = string.Empty;

		// Token: 0x040030DB RID: 12507
		public static string give_upper = string.Empty;

		// Token: 0x040030DC RID: 12508
		public static string receive_upper = string.Empty;

		// Token: 0x040030DD RID: 12509
		public static string receive_all = string.Empty;

		// Token: 0x040030DE RID: 12510
		public static string no_map = string.Empty;

		// Token: 0x040030DF RID: 12511
		public static string go_to_quest = string.Empty;

		// Token: 0x040030E0 RID: 12512
		public static string from_earth = string.Empty;

		// Token: 0x040030E1 RID: 12513
		public static string from_namec = string.Empty;

		// Token: 0x040030E2 RID: 12514
		public static string from_sayda = string.Empty;

		// Token: 0x040030E3 RID: 12515
		public static string expire = string.Empty;

		// Token: 0x040030E4 RID: 12516
		public static string pow_request = string.Empty;

		// Token: 0x040030E5 RID: 12517
		public static string your_pow = string.Empty;

		// Token: 0x040030E6 RID: 12518
		public static string used = string.Empty;

		// Token: 0x040030E7 RID: 12519
		public static string place = string.Empty;

		// Token: 0x040030E8 RID: 12520
		public static string FOREVER = string.Empty;

		// Token: 0x040030E9 RID: 12521
		public static string NOUPGRADE = string.Empty;

		// Token: 0x040030EA RID: 12522
		public static string NOTUPGRADE = string.Empty;

		// Token: 0x040030EB RID: 12523
		public static string UPGRADE = string.Empty;

		// Token: 0x040030EC RID: 12524
		public static string UPGRADING = string.Empty;

		// Token: 0x040030ED RID: 12525
		public static string make_shortcut = string.Empty;

		// Token: 0x040030EE RID: 12526
		public static string into_place = string.Empty;

		// Token: 0x040030EF RID: 12527
		public static string move_to_chest = string.Empty;

		// Token: 0x040030F0 RID: 12528
		public static string move_to_chest2 = string.Empty;

		// Token: 0x040030F1 RID: 12529
		public static string press_chat_querty = string.Empty;

		// Token: 0x040030F2 RID: 12530
		public static string press_chat = string.Empty;

		// Token: 0x040030F3 RID: 12531
		public static string saying = string.Empty;

		// Token: 0x040030F4 RID: 12532
		public static string miss = string.Empty;

		// Token: 0x040030F5 RID: 12533
		public static string donate = string.Empty;

		// Token: 0x040030F6 RID: 12534
		public static string receive = string.Empty;

		// Token: 0x040030F7 RID: 12535
		public static string press_twice = string.Empty;

		// Token: 0x040030F8 RID: 12536
		public static string can_harvest = string.Empty;

		// Token: 0x040030F9 RID: 12537
		public static string do_accept_qwerty = string.Empty;

		// Token: 0x040030FA RID: 12538
		public static string do_accept = string.Empty;

		// Token: 0x040030FB RID: 12539
		public static string plsRestartGame = string.Empty;

		// Token: 0x040030FC RID: 12540
		public static string is_online = string.Empty;

		// Token: 0x040030FD RID: 12541
		public static string is_offline = string.Empty;

		// Token: 0x040030FE RID: 12542
		public static string make_friend = string.Empty;

		// Token: 0x040030FF RID: 12543
		public static string chat_player = string.Empty;

		// Token: 0x04003100 RID: 12544
		public static string chat_with = string.Empty;

		// Token: 0x04003101 RID: 12545
		public static string clan_capsuledonate = string.Empty;

		// Token: 0x04003102 RID: 12546
		public static string clan_capsuleself = string.Empty;

		// Token: 0x04003103 RID: 12547
		public static string clan_point = string.Empty;

		// Token: 0x04003104 RID: 12548
		public static string give_pea = string.Empty;

		// Token: 0x04003105 RID: 12549
		public static string receive_pea = string.Empty;

		// Token: 0x04003106 RID: 12550
		public static string request_pea = string.Empty;

		// Token: 0x04003107 RID: 12551
		public static string time = string.Empty;

		// Token: 0x04003108 RID: 12552
		public static string received = string.Empty;

		// Token: 0x04003109 RID: 12553
		public static string power = string.Empty;

		// Token: 0x0400310A RID: 12554
		public static string join_date = string.Empty;

		// Token: 0x0400310B RID: 12555
		public static string clan_leader = string.Empty;

		// Token: 0x0400310C RID: 12556
		public static string clan_coleader = string.Empty;

		// Token: 0x0400310D RID: 12557
		public static string power_point = string.Empty;

		// Token: 0x0400310E RID: 12558
		public static string member = string.Empty;

		// Token: 0x0400310F RID: 12559
		public static string[] memberr = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04003110 RID: 12560
		public static string[] chatClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04003111 RID: 12561
		public static string[] leaveClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04003112 RID: 12562
		public static string[] createClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04003113 RID: 12563
		public static string[] findClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04003114 RID: 12564
		public static string[] khau_hieuu = new string[]
		{
			string.Empty
		};

		// Token: 0x04003115 RID: 12565
		public static string[] bieu_tuongg = new string[]
		{
			string.Empty
		};

		// Token: 0x04003116 RID: 12566
		public static string[] request_pea2 = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04003117 RID: 12567
		public static string level = string.Empty;

		// Token: 0x04003118 RID: 12568
		public static string clan_birthday = string.Empty;

		// Token: 0x04003119 RID: 12569
		public static string clan_list = string.Empty;

		// Token: 0x0400311A RID: 12570
		public static string create = string.Empty;

		// Token: 0x0400311B RID: 12571
		public static string find = string.Empty;

		// Token: 0x0400311C RID: 12572
		public static string leave = string.Empty;

		// Token: 0x0400311D RID: 12573
		public static string not_join_clan = string.Empty;

		// Token: 0x0400311E RID: 12574
		public static string[] clanEmpty = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400311F RID: 12575
		public static string input_clan_name = string.Empty;

		// Token: 0x04003120 RID: 12576
		public static string clan_name = string.Empty;

		// Token: 0x04003121 RID: 12577
		public static string chat_clan = string.Empty;

		// Token: 0x04003122 RID: 12578
		public static string input_clan_name_to_create = string.Empty;

		// Token: 0x04003123 RID: 12579
		public static string input_clan_slogan = string.Empty;

		// Token: 0x04003124 RID: 12580
		public static string do_u_want_join_clan = string.Empty;

		// Token: 0x04003125 RID: 12581
		public static string select_clan_icon = string.Empty;

		// Token: 0x04003126 RID: 12582
		public static string request_join_clan = string.Empty;

		// Token: 0x04003127 RID: 12583
		public static string view_clan_member = string.Empty;

		// Token: 0x04003128 RID: 12584
		public static string create_clan_co_leader = string.Empty;

		// Token: 0x04003129 RID: 12585
		public static string create_clan_leader = string.Empty;

		// Token: 0x0400312A RID: 12586
		public static string disable_clan_mastership = string.Empty;

		// Token: 0x0400312B RID: 12587
		public static string kick_clan_mem = string.Empty;

		// Token: 0x0400312C RID: 12588
		public static string clan_name_blank = string.Empty;

		// Token: 0x0400312D RID: 12589
		public static string clan_slogan_blank = string.Empty;

		// Token: 0x0400312E RID: 12590
		public static string cannot_find_clan = string.Empty;

		// Token: 0x0400312F RID: 12591
		public static string ago = string.Empty;

		// Token: 0x04003130 RID: 12592
		public static string findingClan = string.Empty;

		// Token: 0x04003131 RID: 12593
		public static string trade = string.Empty;

		// Token: 0x04003132 RID: 12594
		public static string not_lock_trade = string.Empty;

		// Token: 0x04003133 RID: 12595
		public static string not_lock_trade_upper = string.Empty;

		// Token: 0x04003134 RID: 12596
		public static string locked_trade = string.Empty;

		// Token: 0x04003135 RID: 12597
		public static string locked_trade_upper = string.Empty;

		// Token: 0x04003136 RID: 12598
		public static string lock_trade = string.Empty;

		// Token: 0x04003137 RID: 12599
		public static string wait_opp_lock_trade = string.Empty;

		// Token: 0x04003138 RID: 12600
		public static string press_done = string.Empty;

		// Token: 0x04003139 RID: 12601
		public static string THROW = string.Empty;

		// Token: 0x0400313A RID: 12602
		public static string SPLIT = string.Empty;

		// Token: 0x0400313B RID: 12603
		public static string done = string.Empty;

		// Token: 0x0400313C RID: 12604
		public static string opponent = string.Empty;

		// Token: 0x0400313D RID: 12605
		public static string you = string.Empty;

		// Token: 0x0400313E RID: 12606
		public static string mlock = string.Empty;

		// Token: 0x0400313F RID: 12607
		public static string money_trade = string.Empty;

		// Token: 0x04003140 RID: 12608
		public static string GETOUT = string.Empty;

		// Token: 0x04003141 RID: 12609
		public static string MOVEOUT = string.Empty;

		// Token: 0x04003142 RID: 12610
		public static string MOVEFORPET = string.Empty;

		// Token: 0x04003143 RID: 12611
		public static string GETOUTMONEY = string.Empty;

		// Token: 0x04003144 RID: 12612
		public static string GETINMONEY = string.Empty;

		// Token: 0x04003145 RID: 12613
		public static string SENDMONEY = string.Empty;

		// Token: 0x04003146 RID: 12614
		public static string GETIN = string.Empty;

		// Token: 0x04003147 RID: 12615
		public static string SALE = string.Empty;

		// Token: 0x04003148 RID: 12616
		public static string SALES = string.Empty;

		// Token: 0x04003149 RID: 12617
		public static string SALEALL = string.Empty;

		// Token: 0x0400314A RID: 12618
		public static string BUY = string.Empty;

		// Token: 0x0400314B RID: 12619
		public static string BUYS = string.Empty;

		// Token: 0x0400314C RID: 12620
		public static string input_money_to_trade = string.Empty;

		// Token: 0x0400314D RID: 12621
		public static string input_money = string.Empty;

		// Token: 0x0400314E RID: 12622
		public static string input_money_wrong = string.Empty;

		// Token: 0x0400314F RID: 12623
		public static string not_enough_money = string.Empty;

		// Token: 0x04003150 RID: 12624
		public static string input_quantity_to_trade = string.Empty;

		// Token: 0x04003151 RID: 12625
		public static string input_quantity = string.Empty;

		// Token: 0x04003152 RID: 12626
		public static string input_quantity_wrong = string.Empty;

		// Token: 0x04003153 RID: 12627
		public static string already_has_item = string.Empty;

		// Token: 0x04003154 RID: 12628
		public static string unlock_item_to_trade = string.Empty;

		// Token: 0x04003155 RID: 12629
		public static string root = string.Empty;

		// Token: 0x04003156 RID: 12630
		public static string need = string.Empty;

		// Token: 0x04003157 RID: 12631
		public static string need_upper = string.Empty;

		// Token: 0x04003158 RID: 12632
		public static string free = string.Empty;

		// Token: 0x04003159 RID: 12633
		public static string free1 = string.Empty;

		// Token: 0x0400315A RID: 12634
		public static string free2 = string.Empty;

		// Token: 0x0400315B RID: 12635
		public static string select_item = string.Empty;

		// Token: 0x0400315C RID: 12636
		public static string random = string.Empty;

		// Token: 0x0400315D RID: 12637
		public static string say_hello = string.Empty;

		// Token: 0x0400315E RID: 12638
		public static string say_wat_do_u_want_to_buy = string.Empty;

		// Token: 0x0400315F RID: 12639
		public static string say_wat_do_u_want_to_buy2 = string.Empty;

		// Token: 0x04003160 RID: 12640
		public static string do_u_sure_to_trade = string.Empty;

		// Token: 0x04003161 RID: 12641
		public static string learn_with = string.Empty;

		// Token: 0x04003162 RID: 12642
		public static string buy_with = string.Empty;

		// Token: 0x04003163 RID: 12643
		public static string can_not_do_when_die = string.Empty;

		// Token: 0x04003164 RID: 12644
		public static string use_for_combine = string.Empty;

		// Token: 0x04003165 RID: 12645
		public static string use_for_trade = string.Empty;

		// Token: 0x04003166 RID: 12646
		public static string not_enough_luong_world_channel = string.Empty;

		// Token: 0x04003167 RID: 12647
		public static string world_channel_5_luong = string.Empty;

		// Token: 0x04003168 RID: 12648
		public static string want_to_trade = string.Empty;

		// Token: 0x04003169 RID: 12649
		public static string hasJustUpgrade1 = string.Empty;

		// Token: 0x0400316A RID: 12650
		public static string hasJustUpgrade2 = string.Empty;

		// Token: 0x0400316B RID: 12651
		public static string potential_to_learn = string.Empty;

		// Token: 0x0400316C RID: 12652
		public static string potential_point = string.Empty;

		// Token: 0x0400316D RID: 12653
		public static string achievement_point = string.Empty;

		// Token: 0x0400316E RID: 12654
		public static string increase = string.Empty;

		// Token: 0x0400316F RID: 12655
		public static string increase_upper = string.Empty;

		// Token: 0x04003170 RID: 12656
		public static string not_enough_potential_point1 = string.Empty;

		// Token: 0x04003171 RID: 12657
		public static string not_enough_potential_point2 = string.Empty;

		// Token: 0x04003172 RID: 12658
		public static string use_potential_point_for1 = string.Empty;

		// Token: 0x04003173 RID: 12659
		public static string use_potential_point_for2 = string.Empty;

		// Token: 0x04003174 RID: 12660
		public static string for_HP = string.Empty;

		// Token: 0x04003175 RID: 12661
		public static string for_KI = string.Empty;

		// Token: 0x04003176 RID: 12662
		public static string for_hit_point = string.Empty;

		// Token: 0x04003177 RID: 12663
		public static string for_armor = string.Empty;

		// Token: 0x04003178 RID: 12664
		public static string for_crit = string.Empty;

		// Token: 0x04003179 RID: 12665
		public static string can_buy_from_Uron1 = string.Empty;

		// Token: 0x0400317A RID: 12666
		public static string can_buy_from_Uron2 = string.Empty;

		// Token: 0x0400317B RID: 12667
		public static string can_buy_from_Uron3 = string.Empty;

		// Token: 0x0400317C RID: 12668
		public static string HP = string.Empty;

		// Token: 0x0400317D RID: 12669
		public static string KI = string.Empty;

		// Token: 0x0400317E RID: 12670
		public static string hit_point = string.Empty;

		// Token: 0x0400317F RID: 12671
		public static string armor = string.Empty;

		// Token: 0x04003180 RID: 12672
		public static string vitality = string.Empty;

		// Token: 0x04003181 RID: 12673
		public static string critical = string.Empty;

		// Token: 0x04003182 RID: 12674
		public static string cap_do = string.Empty;

		// Token: 0x04003183 RID: 12675
		public static string KI_consume = string.Empty;

		// Token: 0x04003184 RID: 12676
		public static string cooldown = string.Empty;

		// Token: 0x04003185 RID: 12677
		public static string milisecond = string.Empty;

		// Token: 0x04003186 RID: 12678
		public static string max_level_reach = string.Empty;

		// Token: 0x04003187 RID: 12679
		public static string next_level_require = string.Empty;

		// Token: 0x04003188 RID: 12680
		public static string potential = string.Empty;

		// Token: 0x04003189 RID: 12681
		public static string potential2 = string.Empty;

		// Token: 0x0400318A RID: 12682
		public static string not_learn = string.Empty;

		// Token: 0x0400318B RID: 12683
		public static string learn_require = string.Empty;

		// Token: 0x0400318C RID: 12684
		public static string learn = string.Empty;

		// Token: 0x0400318D RID: 12685
		public static string to_gain_20hp = string.Empty;

		// Token: 0x0400318E RID: 12686
		public static string to_gain_20mp = string.Empty;

		// Token: 0x0400318F RID: 12687
		public static string to_gain_1pow = string.Empty;

		// Token: 0x04003190 RID: 12688
		public static string[][] hairStyleName = new string[][]
		{
			new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty
			},
			new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty
			},
			new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty
			}
		};

		// Token: 0x04003191 RID: 12689
		public static string hp_ki_full = string.Empty;

		// Token: 0x04003192 RID: 12690
		public static string quest_place = string.Empty;

		// Token: 0x04003193 RID: 12691
		public static string no_mission = string.Empty;

		// Token: 0x04003194 RID: 12692
		public static string reward_mission = string.Empty;

		// Token: 0x04003195 RID: 12693
		public static string achievement_mission = string.Empty;

		// Token: 0x04003196 RID: 12694
		public static string trangbi = string.Empty;

		// Token: 0x04003197 RID: 12695
		public static string wat_do_u_want = string.Empty;

		// Token: 0x04003198 RID: 12696
		public static string off = string.Empty;

		// Token: 0x04003199 RID: 12697
		public static string on = string.Empty;

		// Token: 0x0400319A RID: 12698
		public static string select_map = string.Empty;

		// Token: 0x0400319B RID: 12699
		public static string offPlease = string.Empty;

		// Token: 0x0400319C RID: 12700
		public static string onPlease = string.Empty;

		// Token: 0x0400319D RID: 12701
		public static sbyte language;

		// Token: 0x0400319E RID: 12702
		public static string choigame;

		// Token: 0x0400319F RID: 12703
		public static string no_enemy = string.Empty;

		// Token: 0x040031A0 RID: 12704
		public static string kigui;

		// Token: 0x040031A1 RID: 12705
		public static string kiguiXu;

		// Token: 0x040031A2 RID: 12706
		public static string kiguiLuong;

		// Token: 0x040031A3 RID: 12707
		public static string kiguiXuchat;

		// Token: 0x040031A4 RID: 12708
		public static string kiguiLuongchat;

		// Token: 0x040031A5 RID: 12709
		public static string huykigui;

		// Token: 0x040031A6 RID: 12710
		public static string nhantien;

		// Token: 0x040031A7 RID: 12711
		public static string dangban;

		// Token: 0x040031A8 RID: 12712
		public static string daban;

		// Token: 0x040031A9 RID: 12713
		public static string num;

		// Token: 0x040031AA RID: 12714
		public static string upTop;

		// Token: 0x040031AB RID: 12715
		public static string page;

		// Token: 0x040031AC RID: 12716
		public static string getDown;

		// Token: 0x040031AD RID: 12717
		public static string getUp;

		// Token: 0x040031AE RID: 12718
		public static string notYetSell;

		// Token: 0x040031AF RID: 12719
		public static string charger;

		// Token: 0x040031B0 RID: 12720
		public static string finishBomong;

		// Token: 0x040031B1 RID: 12721
		public static string note;

		// Token: 0x040031B2 RID: 12722
		public static string regNote;

		// Token: 0x040031B3 RID: 12723
		public static string remain;

		// Token: 0x040031B4 RID: 12724
		public static string faster;

		// Token: 0x040031B5 RID: 12725
		public static string fasterQuestion;

		// Token: 0x040031B6 RID: 12726
		public static string chuacotaikhoan;

		// Token: 0x040031B7 RID: 12727
		public static string taidulieudechoi;

		// Token: 0x040031B8 RID: 12728
		public static string huy;

		// Token: 0x040031B9 RID: 12729
		public static string taidulieu;

		// Token: 0x040031BA RID: 12730
		public static string xoadulieu;

		// Token: 0x040031BB RID: 12731
		public static string deletaDataNote;

		// Token: 0x040031BC RID: 12732
		public static string playNew;

		// Token: 0x040031BD RID: 12733
		public static string playAcc;

		// Token: 0x040031BE RID: 12734
		public static string vuilongnhapduthongtin;

		// Token: 0x040031BF RID: 12735
		public static string not_register_yet = string.Empty;

		// Token: 0x040031C0 RID: 12736
		public static string nhanngoc;

		// Token: 0x040031C1 RID: 12737
		public static string fusion;

		// Token: 0x040031C2 RID: 12738
		public static string sure_fusion;

		// Token: 0x040031C3 RID: 12739
		public static string fusionForever;

		// Token: 0x040031C4 RID: 12740
		public static string xinchucmung;

		// Token: 0x040031C5 RID: 12741
		public static string den;

		// Token: 0x040031C6 RID: 12742
		public static string nhatvatpham;
	}
}
