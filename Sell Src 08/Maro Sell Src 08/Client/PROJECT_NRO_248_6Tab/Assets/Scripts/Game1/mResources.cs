using System;

namespace Game1
{
	// Token: 0x020004B0 RID: 1200
	public class mResources
	{
		// Token: 0x0600355B RID: 13659 RVA: 0x00341D9E File Offset: 0x0033FF9E
		public static void loadLanguague(sbyte newLanguage)
		{
			mResources.language = newLanguage;
			T1.load();
			GameCanvas.isLoadRes = true;
		}

		// Token: 0x04006782 RID: 26498
		public static string confirmChangeServer = string.Empty;

		// Token: 0x04006783 RID: 26499
		public static string chooseDefaultsv = string.Empty;

		// Token: 0x04006784 RID: 26500
		public static string winLose = string.Empty;

		// Token: 0x04006785 RID: 26501
		public static string learnSkill = string.Empty;

		// Token: 0x04006786 RID: 26502
		public static string updSkill = string.Empty;

		// Token: 0x04006787 RID: 26503
		public static string proficiency = string.Empty;

		// Token: 0x04006788 RID: 26504
		public static string delacc = string.Empty;

		// Token: 0x04006789 RID: 26505
		public static string notiINAPP = string.Empty;

		// Token: 0x0400678A RID: 26506
		public static string notiRuby = string.Empty;

		// Token: 0x0400678B RID: 26507
		public static string equip = string.Empty;

		// Token: 0x0400678C RID: 26508
		public static string unlock = string.Empty;

		// Token: 0x0400678D RID: 26509
		public static string radaCard = string.Empty;

		// Token: 0x0400678E RID: 26510
		public static string not_enough_money_1 = string.Empty;

		// Token: 0x0400678F RID: 26511
		public static string napngoc = string.Empty;

		// Token: 0x04006790 RID: 26512
		public static string functionMaintain1 = string.Empty;

		// Token: 0x04006791 RID: 26513
		public static string tang;

		// Token: 0x04006792 RID: 26514
		public static string kquaVongQuay;

		// Token: 0x04006793 RID: 26515
		public static string useGem;

		// Token: 0x04006794 RID: 26516
		public static string autoFunction;

		// Token: 0x04006795 RID: 26517
		public static string choitiep;

		// Token: 0x04006796 RID: 26518
		public static string attack;

		// Token: 0x04006797 RID: 26519
		public static string defend;

		// Token: 0x04006798 RID: 26520
		public static string follow;

		// Token: 0x04006799 RID: 26521
		public static string status;

		// Token: 0x0400679A RID: 26522
		public static string gohome;

		// Token: 0x0400679B RID: 26523
		public static string pet;

		// Token: 0x0400679C RID: 26524
		public static string maychutathoacmatsong;

		// Token: 0x0400679D RID: 26525
		public static string cauhinhthap;

		// Token: 0x0400679E RID: 26526
		public static string cauhinhcao;

		// Token: 0x0400679F RID: 26527
		public static string combineSpell;

		// Token: 0x040067A0 RID: 26528
		public static string combineFail;

		// Token: 0x040067A1 RID: 26529
		public static string combineSuccess;

		// Token: 0x040067A2 RID: 26530
		public static string turnOnAnalog;

		// Token: 0x040067A3 RID: 26531
		public static string turnOffAnalog;

		// Token: 0x040067A4 RID: 26532
		public static string analog;

		// Token: 0x040067A5 RID: 26533
		public static string inventory_Pass;

		// Token: 0x040067A6 RID: 26534
		public static string input_Inventory_Pass;

		// Token: 0x040067A7 RID: 26535
		public static string input_Inventory_Pass_wrong = string.Empty;

		// Token: 0x040067A8 RID: 26536
		public static string REGISTOPROTECT = string.Empty;

		// Token: 0x040067A9 RID: 26537
		public static string turnOnSound = string.Empty;

		// Token: 0x040067AA RID: 26538
		public static string turnOffSound = string.Empty;

		// Token: 0x040067AB RID: 26539
		public static string REGISTERING = string.Empty;

		// Token: 0x040067AC RID: 26540
		public static string SENDINGMSG = string.Empty;

		// Token: 0x040067AD RID: 26541
		public static string SENTMSG = string.Empty;

		// Token: 0x040067AE RID: 26542
		public static string NOSENDMSG = string.Empty;

		// Token: 0x040067AF RID: 26543
		public static string sendMsgSuccess = string.Empty;

		// Token: 0x040067B0 RID: 26544
		public static string cannotSendMsg = string.Empty;

		// Token: 0x040067B1 RID: 26545
		public static string sendGuessMsgSuccess = string.Empty;

		// Token: 0x040067B2 RID: 26546
		public static string sendMsgFail = string.Empty;

		// Token: 0x040067B3 RID: 26547
		public static string ALERT_PRIVATE_PASS_1 = string.Empty;

		// Token: 0x040067B4 RID: 26548
		public static string ALERT_PRIVATE_PASS_2 = string.Empty;

		// Token: 0x040067B5 RID: 26549
		public static string INPUT_PRIVATE_PASS = string.Empty;

		// Token: 0x040067B6 RID: 26550
		public static string change_account = string.Empty;

		// Token: 0x040067B7 RID: 26551
		public static string alreadyHadAccount1 = string.Empty;

		// Token: 0x040067B8 RID: 26552
		public static string alreadyHadAccount2 = string.Empty;

		// Token: 0x040067B9 RID: 26553
		public static string userBlank = string.Empty;

		// Token: 0x040067BA RID: 26554
		public static string passwordBlank = string.Empty;

		// Token: 0x040067BB RID: 26555
		public static string accTooShort = string.Empty;

		// Token: 0x040067BC RID: 26556
		public static string phoneInvalid = string.Empty;

		// Token: 0x040067BD RID: 26557
		public static string emailInvalid = string.Empty;

		// Token: 0x040067BE RID: 26558
		public static string registerNewAcc = string.Empty;

		// Token: 0x040067BF RID: 26559
		public static string selectServer = string.Empty;

		// Token: 0x040067C0 RID: 26560
		public static string selectServer2 = string.Empty;

		// Token: 0x040067C1 RID: 26561
		public static string forgetPass = string.Empty;

		// Token: 0x040067C2 RID: 26562
		public static string password = string.Empty;

		// Token: 0x040067C3 RID: 26563
		public static string[] LOGINLABELS = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040067C4 RID: 26564
		public static string msg = string.Empty;

		// Token: 0x040067C5 RID: 26565
		public static string[] msgg = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040067C6 RID: 26566
		public static string no_msg = string.Empty;

		// Token: 0x040067C7 RID: 26567
		public static string cancelAccountProtection = string.Empty;

		// Token: 0x040067C8 RID: 26568
		public static string plsCheckAcc = string.Empty;

		// Token: 0x040067C9 RID: 26569
		public static string phone = string.Empty;

		// Token: 0x040067CA RID: 26570
		public static string email = string.Empty;

		// Token: 0x040067CB RID: 26571
		public static string acc = string.Empty;

		// Token: 0x040067CC RID: 26572
		public static string pwd = string.Empty;

		// Token: 0x040067CD RID: 26573
		public static string goToWebForPassword = string.Empty;

		// Token: 0x040067CE RID: 26574
		public static string dragon_ball = string.Empty;

		// Token: 0x040067CF RID: 26575
		public static string character = string.Empty;

		// Token: 0x040067D0 RID: 26576
		public static string account = string.Empty;

		// Token: 0x040067D1 RID: 26577
		public static string account_server = string.Empty;

		// Token: 0x040067D2 RID: 26578
		public static string char_name_blank = string.Empty;

		// Token: 0x040067D3 RID: 26579
		public static string char_name_short = string.Empty;

		// Token: 0x040067D4 RID: 26580
		public static string char_name_long = string.Empty;

		// Token: 0x040067D5 RID: 26581
		public static string changeNameChar = string.Empty;

		// Token: 0x040067D6 RID: 26582
		public static string char_name = string.Empty;

		// Token: 0x040067D7 RID: 26583
		public static string login = string.Empty;

		// Token: 0x040067D8 RID: 26584
		public static string login2 = string.Empty;

		// Token: 0x040067D9 RID: 26585
		public static string register = string.Empty;

		// Token: 0x040067DA RID: 26586
		public static string WAIT = string.Empty;

		// Token: 0x040067DB RID: 26587
		public static string PLEASEWAIT = string.Empty;

		// Token: 0x040067DC RID: 26588
		public static string CONNECTING = string.Empty;

		// Token: 0x040067DD RID: 26589
		public static string LOGGING = string.Empty;

		// Token: 0x040067DE RID: 26590
		public static string LOADING = string.Empty;

		// Token: 0x040067DF RID: 26591
		public static string downloading_data = string.Empty;

		// Token: 0x040067E0 RID: 26592
		public static string select_server = string.Empty;

		// Token: 0x040067E1 RID: 26593
		public static string pls_restart_game_error = string.Empty;

		// Token: 0x040067E2 RID: 26594
		public static string pls_restart_game_error2 = string.Empty;

		// Token: 0x040067E3 RID: 26595
		public static string lost_connection = string.Empty;

		// Token: 0x040067E4 RID: 26596
		public static string check_3G = string.Empty;

		// Token: 0x040067E5 RID: 26597
		public static string UPDATE = string.Empty;

		// Token: 0x040067E6 RID: 26598
		public static string change_zone = string.Empty;

		// Token: 0x040067E7 RID: 26599
		public static string select_zone = string.Empty;

		// Token: 0x040067E8 RID: 26600
		public static string website = string.Empty;

		// Token: 0x040067E9 RID: 26601
		public static string server = string.Empty;

		// Token: 0x040067EA RID: 26602
		public static string planet = string.Empty;

		// Token: 0x040067EB RID: 26603
		public static string[] MENUME = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x040067EC RID: 26604
		public static string[] MENUGENDER = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x040067ED RID: 26605
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

		// Token: 0x040067EE RID: 26606
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

		// Token: 0x040067EF RID: 26607
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

		// Token: 0x040067F0 RID: 26608
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

		// Token: 0x040067F1 RID: 26609
		public static string[][] petMainTab2 = new string[][]
		{
			new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty
			}
		};

		// Token: 0x040067F2 RID: 26610
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

		// Token: 0x040067F3 RID: 26611
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

		// Token: 0x040067F4 RID: 26612
		public static string SKILL_FAIL = string.Empty;

		// Token: 0x040067F5 RID: 26613
		public static string HP_EMPTY = string.Empty;

		// Token: 0x040067F6 RID: 26614
		public static string ZONE_HERE = string.Empty;

		// Token: 0x040067F7 RID: 26615
		public static string[] DES_TASK = new string[]
		{
			" ",
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x040067F8 RID: 26616
		public static string[] DIES = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x040067F9 RID: 26617
		public static string[] SYNTHESIS = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x040067FA RID: 26618
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

		// Token: 0x040067FB RID: 26619
		public static string TASK_INPUT_CLASS = string.Empty;

		// Token: 0x040067FC RID: 26620
		public static string SERI_NUM = string.Empty;

		// Token: 0x040067FD RID: 26621
		public static string CARD_CODE = string.Empty;

		// Token: 0x040067FE RID: 26622
		public static string pay_card = string.Empty;

		// Token: 0x040067FF RID: 26623
		public static string pay_card2 = string.Empty;

		// Token: 0x04006800 RID: 26624
		public static string serial_blank = string.Empty;

		// Token: 0x04006801 RID: 26625
		public static string card_code_blank = string.Empty;

		// Token: 0x04006802 RID: 26626
		public static string billion = string.Empty;

		// Token: 0x04006803 RID: 26627
		public static string million = string.Empty;

		// Token: 0x04006804 RID: 26628
		public static string MENU = string.Empty;

		// Token: 0x04006805 RID: 26629
		public static string CLOSE = string.Empty;

		// Token: 0x04006806 RID: 26630
		public static string ON = string.Empty;

		// Token: 0x04006807 RID: 26631
		public static string OFF = string.Empty;

		// Token: 0x04006808 RID: 26632
		public static string ENABLE = string.Empty;

		// Token: 0x04006809 RID: 26633
		public static string DELETE = string.Empty;

		// Token: 0x0400680A RID: 26634
		public static string VIEW = string.Empty;

		// Token: 0x0400680B RID: 26635
		public static string CONTINUE = string.Empty;

		// Token: 0x0400680C RID: 26636
		public static string NEXTSTEP = string.Empty;

		// Token: 0x0400680D RID: 26637
		public static string USE = string.Empty;

		// Token: 0x0400680E RID: 26638
		public static string SORT = string.Empty;

		// Token: 0x0400680F RID: 26639
		public static string YES = string.Empty;

		// Token: 0x04006810 RID: 26640
		public static string NO = string.Empty;

		// Token: 0x04006811 RID: 26641
		public static string EXIT = string.Empty;

		// Token: 0x04006812 RID: 26642
		public static string CHAT = string.Empty;

		// Token: 0x04006813 RID: 26643
		public static string REVENGE = string.Empty;

		// Token: 0x04006814 RID: 26644
		public static string OK = string.Empty;

		// Token: 0x04006815 RID: 26645
		public static string retry = string.Empty;

		// Token: 0x04006816 RID: 26646
		public static string uncheck = string.Empty;

		// Token: 0x04006817 RID: 26647
		public static string remember = string.Empty;

		// Token: 0x04006818 RID: 26648
		public static string ACCEPT = string.Empty;

		// Token: 0x04006819 RID: 26649
		public static string CANCEL = string.Empty;

		// Token: 0x0400681A RID: 26650
		public static string SELECT = string.Empty;

		// Token: 0x0400681B RID: 26651
		public static string enter = string.Empty;

		// Token: 0x0400681C RID: 26652
		public static string open_link = string.Empty;

		// Token: 0x0400681D RID: 26653
		public static string DOYOUWANTEXIT = string.Empty;

		// Token: 0x0400681E RID: 26654
		public static string NEWCHAR = string.Empty;

		// Token: 0x0400681F RID: 26655
		public static string BACK = string.Empty;

		// Token: 0x04006820 RID: 26656
		public static string LOCKED = string.Empty;

		// Token: 0x04006821 RID: 26657
		public static string KILL = string.Empty;

		// Token: 0x04006822 RID: 26658
		public static string KILLBOSS = string.Empty;

		// Token: 0x04006823 RID: 26659
		public static string NOLOCK = string.Empty;

		// Token: 0x04006824 RID: 26660
		public static string XU = string.Empty;

		// Token: 0x04006825 RID: 26661
		public static string LUONG = string.Empty;

		// Token: 0x04006826 RID: 26662
		public static string RUBY = string.Empty;

		// Token: 0x04006827 RID: 26663
		public static string PK_NOW = string.Empty;

		// Token: 0x04006828 RID: 26664
		public static string CUU_SAT = string.Empty;

		// Token: 0x04006829 RID: 26665
		public static string NOT_ENOUGH_MP = string.Empty;

		// Token: 0x0400682A RID: 26666
		public static string you_receive = string.Empty;

		// Token: 0x0400682B RID: 26667
		public static string MONTH = string.Empty;

		// Token: 0x0400682C RID: 26668
		public static string WEEK = string.Empty;

		// Token: 0x0400682D RID: 26669
		public static string DAY = string.Empty;

		// Token: 0x0400682E RID: 26670
		public static string HOUR = string.Empty;

		// Token: 0x0400682F RID: 26671
		public static string SECOND = string.Empty;

		// Token: 0x04006830 RID: 26672
		public static string MINUTE = string.Empty;

		// Token: 0x04006831 RID: 26673
		public static string LEARN_SKILL = string.Empty;

		// Token: 0x04006832 RID: 26674
		public static string rank = string.Empty;

		// Token: 0x04006833 RID: 26675
		public static string active_point = string.Empty;

		// Token: 0x04006834 RID: 26676
		public static string friend = string.Empty;

		// Token: 0x04006835 RID: 26677
		public static string enemy = string.Empty;

		// Token: 0x04006836 RID: 26678
		public static string no_friend = string.Empty;

		// Token: 0x04006837 RID: 26679
		public static string chat_world = string.Empty;

		// Token: 0x04006838 RID: 26680
		public static string change_flag = string.Empty;

		// Token: 0x04006839 RID: 26681
		public static string gameInfo = string.Empty;

		// Token: 0x0400683A RID: 26682
		public static string quayso = string.Empty;

		// Token: 0x0400683B RID: 26683
		public static string option = string.Empty;

		// Token: 0x0400683C RID: 26684
		public static string high = string.Empty;

		// Token: 0x0400683D RID: 26685
		public static string medium = string.Empty;

		// Token: 0x0400683E RID: 26686
		public static string low = string.Empty;

		// Token: 0x0400683F RID: 26687
		public static string increase_vga = string.Empty;

		// Token: 0x04006840 RID: 26688
		public static string decrease_vga = string.Empty;

		// Token: 0x04006841 RID: 26689
		public static string serverchat_off = string.Empty;

		// Token: 0x04006842 RID: 26690
		public static string serverchat_on = string.Empty;

		// Token: 0x04006843 RID: 26691
		public static string x2Screen = string.Empty;

		// Token: 0x04006844 RID: 26692
		public static string x1Screen = string.Empty;

		// Token: 0x04006845 RID: 26693
		public static string changeSizeScreen = string.Empty;

		// Token: 0x04006846 RID: 26694
		public static string aura_off = string.Empty;

		// Token: 0x04006847 RID: 26695
		public static string aura_on = string.Empty;

		// Token: 0x04006848 RID: 26696
		public static string aura_off_2 = string.Empty;

		// Token: 0x04006849 RID: 26697
		public static string aura_on_2 = string.Empty;

		// Token: 0x0400684A RID: 26698
		public static string hat_off = string.Empty;

		// Token: 0x0400684B RID: 26699
		public static string hat_on = string.Empty;

		// Token: 0x0400684C RID: 26700
		public static string chest = string.Empty;

		// Token: 0x0400684D RID: 26701
		public static string[] chestt = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x0400684E RID: 26702
		public static string[] inventory = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x0400684F RID: 26703
		public static string[] combine = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04006850 RID: 26704
		public static string[] mapp = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04006851 RID: 26705
		public static string[] item_give = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04006852 RID: 26706
		public static string[] item_receive = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04006853 RID: 26707
		public static string[] zonee = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04006854 RID: 26708
		public static string zone = string.Empty;

		// Token: 0x04006855 RID: 26709
		public static string map = string.Empty;

		// Token: 0x04006856 RID: 26710
		public static string item_receive2 = string.Empty;

		// Token: 0x04006857 RID: 26711
		public static string item = string.Empty;

		// Token: 0x04006858 RID: 26712
		public static string give_upper = string.Empty;

		// Token: 0x04006859 RID: 26713
		public static string receive_upper = string.Empty;

		// Token: 0x0400685A RID: 26714
		public static string receive_all = string.Empty;

		// Token: 0x0400685B RID: 26715
		public static string no_map = string.Empty;

		// Token: 0x0400685C RID: 26716
		public static string go_to_quest = string.Empty;

		// Token: 0x0400685D RID: 26717
		public static string from_earth = string.Empty;

		// Token: 0x0400685E RID: 26718
		public static string from_namec = string.Empty;

		// Token: 0x0400685F RID: 26719
		public static string from_sayda = string.Empty;

		// Token: 0x04006860 RID: 26720
		public static string expire = string.Empty;

		// Token: 0x04006861 RID: 26721
		public static string pow_request = string.Empty;

		// Token: 0x04006862 RID: 26722
		public static string your_pow = string.Empty;

		// Token: 0x04006863 RID: 26723
		public static string used = string.Empty;

		// Token: 0x04006864 RID: 26724
		public static string place = string.Empty;

		// Token: 0x04006865 RID: 26725
		public static string FOREVER = string.Empty;

		// Token: 0x04006866 RID: 26726
		public static string NOUPGRADE = string.Empty;

		// Token: 0x04006867 RID: 26727
		public static string NOTUPGRADE = string.Empty;

		// Token: 0x04006868 RID: 26728
		public static string UPGRADE = string.Empty;

		// Token: 0x04006869 RID: 26729
		public static string UPGRADING = string.Empty;

		// Token: 0x0400686A RID: 26730
		public static string make_shortcut = string.Empty;

		// Token: 0x0400686B RID: 26731
		public static string into_place = string.Empty;

		// Token: 0x0400686C RID: 26732
		public static string move_to_chest = string.Empty;

		// Token: 0x0400686D RID: 26733
		public static string move_to_chest2 = string.Empty;

		// Token: 0x0400686E RID: 26734
		public static string press_chat_querty = string.Empty;

		// Token: 0x0400686F RID: 26735
		public static string press_chat = string.Empty;

		// Token: 0x04006870 RID: 26736
		public static string saying = string.Empty;

		// Token: 0x04006871 RID: 26737
		public static string miss = string.Empty;

		// Token: 0x04006872 RID: 26738
		public static string donate = string.Empty;

		// Token: 0x04006873 RID: 26739
		public static string receive = string.Empty;

		// Token: 0x04006874 RID: 26740
		public static string press_twice = string.Empty;

		// Token: 0x04006875 RID: 26741
		public static string can_harvest = string.Empty;

		// Token: 0x04006876 RID: 26742
		public static string do_accept_qwerty = string.Empty;

		// Token: 0x04006877 RID: 26743
		public static string do_accept = string.Empty;

		// Token: 0x04006878 RID: 26744
		public static string plsRestartGame = string.Empty;

		// Token: 0x04006879 RID: 26745
		public static string is_online = string.Empty;

		// Token: 0x0400687A RID: 26746
		public static string is_offline = string.Empty;

		// Token: 0x0400687B RID: 26747
		public static string make_friend = string.Empty;

		// Token: 0x0400687C RID: 26748
		public static string chat_player = string.Empty;

		// Token: 0x0400687D RID: 26749
		public static string chat_with = string.Empty;

		// Token: 0x0400687E RID: 26750
		public static string clan_capsuledonate = string.Empty;

		// Token: 0x0400687F RID: 26751
		public static string clan_capsuleself = string.Empty;

		// Token: 0x04006880 RID: 26752
		public static string clan_point = string.Empty;

		// Token: 0x04006881 RID: 26753
		public static string give_pea = string.Empty;

		// Token: 0x04006882 RID: 26754
		public static string receive_pea = string.Empty;

		// Token: 0x04006883 RID: 26755
		public static string request_pea = string.Empty;

		// Token: 0x04006884 RID: 26756
		public static string time = string.Empty;

		// Token: 0x04006885 RID: 26757
		public static string received = string.Empty;

		// Token: 0x04006886 RID: 26758
		public static string power = string.Empty;

		// Token: 0x04006887 RID: 26759
		public static string join_date = string.Empty;

		// Token: 0x04006888 RID: 26760
		public static string clan_leader = string.Empty;

		// Token: 0x04006889 RID: 26761
		public static string clan_coleader = string.Empty;

		// Token: 0x0400688A RID: 26762
		public static string power_point = string.Empty;

		// Token: 0x0400688B RID: 26763
		public static string member = string.Empty;

		// Token: 0x0400688C RID: 26764
		public static string[] memberr = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x0400688D RID: 26765
		public static string[] chatClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x0400688E RID: 26766
		public static string[] leaveClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x0400688F RID: 26767
		public static string[] createClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04006890 RID: 26768
		public static string[] findClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04006891 RID: 26769
		public static string[] khau_hieuu = new string[]
		{
			string.Empty
		};

		// Token: 0x04006892 RID: 26770
		public static string[] bieu_tuongg = new string[]
		{
			string.Empty
		};

		// Token: 0x04006893 RID: 26771
		public static string[] request_pea2 = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04006894 RID: 26772
		public static string level = string.Empty;

		// Token: 0x04006895 RID: 26773
		public static string clan_birthday = string.Empty;

		// Token: 0x04006896 RID: 26774
		public static string clan_list = string.Empty;

		// Token: 0x04006897 RID: 26775
		public static string create = string.Empty;

		// Token: 0x04006898 RID: 26776
		public static string find = string.Empty;

		// Token: 0x04006899 RID: 26777
		public static string leave = string.Empty;

		// Token: 0x0400689A RID: 26778
		public static string not_join_clan = string.Empty;

		// Token: 0x0400689B RID: 26779
		public static string[] clanEmpty = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400689C RID: 26780
		public static string input_clan_name = string.Empty;

		// Token: 0x0400689D RID: 26781
		public static string clan_name = string.Empty;

		// Token: 0x0400689E RID: 26782
		public static string chat_clan = string.Empty;

		// Token: 0x0400689F RID: 26783
		public static string input_clan_name_to_create = string.Empty;

		// Token: 0x040068A0 RID: 26784
		public static string input_clan_slogan = string.Empty;

		// Token: 0x040068A1 RID: 26785
		public static string do_u_want_join_clan = string.Empty;

		// Token: 0x040068A2 RID: 26786
		public static string select_clan_icon = string.Empty;

		// Token: 0x040068A3 RID: 26787
		public static string request_join_clan = string.Empty;

		// Token: 0x040068A4 RID: 26788
		public static string view_clan_member = string.Empty;

		// Token: 0x040068A5 RID: 26789
		public static string create_clan_co_leader = string.Empty;

		// Token: 0x040068A6 RID: 26790
		public static string create_clan_leader = string.Empty;

		// Token: 0x040068A7 RID: 26791
		public static string disable_clan_mastership = string.Empty;

		// Token: 0x040068A8 RID: 26792
		public static string kick_clan_mem = string.Empty;

		// Token: 0x040068A9 RID: 26793
		public static string clan_name_blank = string.Empty;

		// Token: 0x040068AA RID: 26794
		public static string clan_slogan_blank = string.Empty;

		// Token: 0x040068AB RID: 26795
		public static string cannot_find_clan = string.Empty;

		// Token: 0x040068AC RID: 26796
		public static string ago = string.Empty;

		// Token: 0x040068AD RID: 26797
		public static string findingClan = string.Empty;

		// Token: 0x040068AE RID: 26798
		public static string trade = string.Empty;

		// Token: 0x040068AF RID: 26799
		public static string not_lock_trade = string.Empty;

		// Token: 0x040068B0 RID: 26800
		public static string not_lock_trade_upper = string.Empty;

		// Token: 0x040068B1 RID: 26801
		public static string locked_trade = string.Empty;

		// Token: 0x040068B2 RID: 26802
		public static string locked_trade_upper = string.Empty;

		// Token: 0x040068B3 RID: 26803
		public static string lock_trade = string.Empty;

		// Token: 0x040068B4 RID: 26804
		public static string wait_opp_lock_trade = string.Empty;

		// Token: 0x040068B5 RID: 26805
		public static string press_done = string.Empty;

		// Token: 0x040068B6 RID: 26806
		public static string THROW = string.Empty;

		// Token: 0x040068B7 RID: 26807
		public static string SPLIT = string.Empty;

		// Token: 0x040068B8 RID: 26808
		public static string done = string.Empty;

		// Token: 0x040068B9 RID: 26809
		public static string opponent = string.Empty;

		// Token: 0x040068BA RID: 26810
		public static string you = string.Empty;

		// Token: 0x040068BB RID: 26811
		public static string mlock = string.Empty;

		// Token: 0x040068BC RID: 26812
		public static string money_trade = string.Empty;

		// Token: 0x040068BD RID: 26813
		public static string GETOUT = string.Empty;

		// Token: 0x040068BE RID: 26814
		public static string MOVEOUT = string.Empty;

		// Token: 0x040068BF RID: 26815
		public static string MOVEFORPET = string.Empty;

		// Token: 0x040068C0 RID: 26816
		public static string GETOUTMONEY = string.Empty;

		// Token: 0x040068C1 RID: 26817
		public static string GETINMONEY = string.Empty;

		// Token: 0x040068C2 RID: 26818
		public static string SENDMONEY = string.Empty;

		// Token: 0x040068C3 RID: 26819
		public static string GETIN = string.Empty;

		// Token: 0x040068C4 RID: 26820
		public static string SALE = string.Empty;

		// Token: 0x040068C5 RID: 26821
		public static string SALES = string.Empty;

		// Token: 0x040068C6 RID: 26822
		public static string SALEALL = string.Empty;

		// Token: 0x040068C7 RID: 26823
		public static string BUY = string.Empty;

		// Token: 0x040068C8 RID: 26824
		public static string BUYS = string.Empty;

		// Token: 0x040068C9 RID: 26825
		public static string input_money_to_trade = string.Empty;

		// Token: 0x040068CA RID: 26826
		public static string input_money = string.Empty;

		// Token: 0x040068CB RID: 26827
		public static string input_money_wrong = string.Empty;

		// Token: 0x040068CC RID: 26828
		public static string not_enough_money = string.Empty;

		// Token: 0x040068CD RID: 26829
		public static string input_quantity_to_trade = string.Empty;

		// Token: 0x040068CE RID: 26830
		public static string input_quantity = string.Empty;

		// Token: 0x040068CF RID: 26831
		public static string input_quantity_wrong = string.Empty;

		// Token: 0x040068D0 RID: 26832
		public static string already_has_item = string.Empty;

		// Token: 0x040068D1 RID: 26833
		public static string unlock_item_to_trade = string.Empty;

		// Token: 0x040068D2 RID: 26834
		public static string root = string.Empty;

		// Token: 0x040068D3 RID: 26835
		public static string need = string.Empty;

		// Token: 0x040068D4 RID: 26836
		public static string need_upper = string.Empty;

		// Token: 0x040068D5 RID: 26837
		public static string free = string.Empty;

		// Token: 0x040068D6 RID: 26838
		public static string free1 = string.Empty;

		// Token: 0x040068D7 RID: 26839
		public static string free2 = string.Empty;

		// Token: 0x040068D8 RID: 26840
		public static string select_item = string.Empty;

		// Token: 0x040068D9 RID: 26841
		public static string random = string.Empty;

		// Token: 0x040068DA RID: 26842
		public static string say_hello = string.Empty;

		// Token: 0x040068DB RID: 26843
		public static string say_wat_do_u_want_to_buy = string.Empty;

		// Token: 0x040068DC RID: 26844
		public static string say_wat_do_u_want_to_buy2 = string.Empty;

		// Token: 0x040068DD RID: 26845
		public static string do_u_sure_to_trade = string.Empty;

		// Token: 0x040068DE RID: 26846
		public static string learn_with = string.Empty;

		// Token: 0x040068DF RID: 26847
		public static string buy_with = string.Empty;

		// Token: 0x040068E0 RID: 26848
		public static string can_not_do_when_die = string.Empty;

		// Token: 0x040068E1 RID: 26849
		public static string use_for_combine = string.Empty;

		// Token: 0x040068E2 RID: 26850
		public static string use_for_trade = string.Empty;

		// Token: 0x040068E3 RID: 26851
		public static string not_enough_luong_world_channel = string.Empty;

		// Token: 0x040068E4 RID: 26852
		public static string world_channel_5_luong = string.Empty;

		// Token: 0x040068E5 RID: 26853
		public static string want_to_trade = string.Empty;

		// Token: 0x040068E6 RID: 26854
		public static string hasJustUpgrade1 = string.Empty;

		// Token: 0x040068E7 RID: 26855
		public static string hasJustUpgrade2 = string.Empty;

		// Token: 0x040068E8 RID: 26856
		public static string potential_to_learn = string.Empty;

		// Token: 0x040068E9 RID: 26857
		public static string potential_point = string.Empty;

		// Token: 0x040068EA RID: 26858
		public static string achievement_point = string.Empty;

		// Token: 0x040068EB RID: 26859
		public static string increase = string.Empty;

		// Token: 0x040068EC RID: 26860
		public static string increase_upper = string.Empty;

		// Token: 0x040068ED RID: 26861
		public static string not_enough_potential_point1 = string.Empty;

		// Token: 0x040068EE RID: 26862
		public static string not_enough_potential_point2 = string.Empty;

		// Token: 0x040068EF RID: 26863
		public static string use_potential_point_for1 = string.Empty;

		// Token: 0x040068F0 RID: 26864
		public static string use_potential_point_for2 = string.Empty;

		// Token: 0x040068F1 RID: 26865
		public static string for_HP = string.Empty;

		// Token: 0x040068F2 RID: 26866
		public static string for_KI = string.Empty;

		// Token: 0x040068F3 RID: 26867
		public static string for_hit_point = string.Empty;

		// Token: 0x040068F4 RID: 26868
		public static string for_armor = string.Empty;

		// Token: 0x040068F5 RID: 26869
		public static string for_crit = string.Empty;

		// Token: 0x040068F6 RID: 26870
		public static string can_buy_from_Uron1 = string.Empty;

		// Token: 0x040068F7 RID: 26871
		public static string can_buy_from_Uron2 = string.Empty;

		// Token: 0x040068F8 RID: 26872
		public static string can_buy_from_Uron3 = string.Empty;

		// Token: 0x040068F9 RID: 26873
		public static string HP = string.Empty;

		// Token: 0x040068FA RID: 26874
		public static string KI = string.Empty;

		// Token: 0x040068FB RID: 26875
		public static string hit_point = string.Empty;

		// Token: 0x040068FC RID: 26876
		public static string armor = string.Empty;

		// Token: 0x040068FD RID: 26877
		public static string vitality = string.Empty;

		// Token: 0x040068FE RID: 26878
		public static string critical = string.Empty;

		// Token: 0x040068FF RID: 26879
		public static string cap_do = string.Empty;

		// Token: 0x04006900 RID: 26880
		public static string KI_consume = string.Empty;

		// Token: 0x04006901 RID: 26881
		public static string cooldown = string.Empty;

		// Token: 0x04006902 RID: 26882
		public static string milisecond = string.Empty;

		// Token: 0x04006903 RID: 26883
		public static string max_level_reach = string.Empty;

		// Token: 0x04006904 RID: 26884
		public static string next_level_require = string.Empty;

		// Token: 0x04006905 RID: 26885
		public static string potential = string.Empty;

		// Token: 0x04006906 RID: 26886
		public static string potential2 = string.Empty;

		// Token: 0x04006907 RID: 26887
		public static string not_learn = string.Empty;

		// Token: 0x04006908 RID: 26888
		public static string learn_require = string.Empty;

		// Token: 0x04006909 RID: 26889
		public static string learn = string.Empty;

		// Token: 0x0400690A RID: 26890
		public static string to_gain_20hp = string.Empty;

		// Token: 0x0400690B RID: 26891
		public static string to_gain_20mp = string.Empty;

		// Token: 0x0400690C RID: 26892
		public static string to_gain_1pow = string.Empty;

		// Token: 0x0400690D RID: 26893
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

		// Token: 0x0400690E RID: 26894
		public static string hp_ki_full = string.Empty;

		// Token: 0x0400690F RID: 26895
		public static string quest_place = string.Empty;

		// Token: 0x04006910 RID: 26896
		public static string no_mission = string.Empty;

		// Token: 0x04006911 RID: 26897
		public static string reward_mission = string.Empty;

		// Token: 0x04006912 RID: 26898
		public static string achievement_mission = string.Empty;

		// Token: 0x04006913 RID: 26899
		public static string trangbi = string.Empty;

		// Token: 0x04006914 RID: 26900
		public static string wat_do_u_want = string.Empty;

		// Token: 0x04006915 RID: 26901
		public static string off = string.Empty;

		// Token: 0x04006916 RID: 26902
		public static string on = string.Empty;

		// Token: 0x04006917 RID: 26903
		public static string select_map = string.Empty;

		// Token: 0x04006918 RID: 26904
		public static string offPlease = string.Empty;

		// Token: 0x04006919 RID: 26905
		public static string onPlease = string.Empty;

		// Token: 0x0400691A RID: 26906
		public static sbyte language;

		// Token: 0x0400691B RID: 26907
		public static string choigame;

		// Token: 0x0400691C RID: 26908
		public static string no_enemy = string.Empty;

		// Token: 0x0400691D RID: 26909
		public static string kigui;

		// Token: 0x0400691E RID: 26910
		public static string kiguiXu;

		// Token: 0x0400691F RID: 26911
		public static string kiguiLuong;

		// Token: 0x04006920 RID: 26912
		public static string kiguiXuchat;

		// Token: 0x04006921 RID: 26913
		public static string kiguiLuongchat;

		// Token: 0x04006922 RID: 26914
		public static string huykigui;

		// Token: 0x04006923 RID: 26915
		public static string nhantien;

		// Token: 0x04006924 RID: 26916
		public static string dangban;

		// Token: 0x04006925 RID: 26917
		public static string daban;

		// Token: 0x04006926 RID: 26918
		public static string num;

		// Token: 0x04006927 RID: 26919
		public static string upTop;

		// Token: 0x04006928 RID: 26920
		public static string page;

		// Token: 0x04006929 RID: 26921
		public static string getDown;

		// Token: 0x0400692A RID: 26922
		public static string getUp;

		// Token: 0x0400692B RID: 26923
		public static string notYetSell;

		// Token: 0x0400692C RID: 26924
		public static string charger;

		// Token: 0x0400692D RID: 26925
		public static string finishBomong;

		// Token: 0x0400692E RID: 26926
		public static string note;

		// Token: 0x0400692F RID: 26927
		public static string regNote;

		// Token: 0x04006930 RID: 26928
		public static string remain;

		// Token: 0x04006931 RID: 26929
		public static string faster;

		// Token: 0x04006932 RID: 26930
		public static string fasterQuestion;

		// Token: 0x04006933 RID: 26931
		public static string chuacotaikhoan;

		// Token: 0x04006934 RID: 26932
		public static string taidulieudechoi;

		// Token: 0x04006935 RID: 26933
		public static string huy;

		// Token: 0x04006936 RID: 26934
		public static string taidulieu;

		// Token: 0x04006937 RID: 26935
		public static string xoadulieu;

		// Token: 0x04006938 RID: 26936
		public static string deletaDataNote;

		// Token: 0x04006939 RID: 26937
		public static string playNew;

		// Token: 0x0400693A RID: 26938
		public static string playAcc;

		// Token: 0x0400693B RID: 26939
		public static string vuilongnhapduthongtin;

		// Token: 0x0400693C RID: 26940
		public static string not_register_yet = string.Empty;

		// Token: 0x0400693D RID: 26941
		public static string nhanngoc;

		// Token: 0x0400693E RID: 26942
		public static string fusion;

		// Token: 0x0400693F RID: 26943
		public static string sure_fusion;

		// Token: 0x04006940 RID: 26944
		public static string fusionForever;

		// Token: 0x04006941 RID: 26945
		public static string xinchucmung;

		// Token: 0x04006942 RID: 26946
		public static string den;

		// Token: 0x04006943 RID: 26947
		public static string nhatvatpham;
	}
}
