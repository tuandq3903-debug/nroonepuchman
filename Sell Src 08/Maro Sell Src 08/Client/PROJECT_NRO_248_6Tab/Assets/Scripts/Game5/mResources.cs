using System;

namespace Game5
{
	// Token: 0x02000150 RID: 336
	public class mResources
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x000EDB0E File Offset: 0x000EBD0E
		public static void loadLanguague(sbyte newLanguage)
		{
			mResources.language = newLanguage;
			T1.load();
			GameCanvas.isLoadRes = true;
		}

		// Token: 0x04001D86 RID: 7558
		public static string confirmChangeServer = string.Empty;

		// Token: 0x04001D87 RID: 7559
		public static string chooseDefaultsv = string.Empty;

		// Token: 0x04001D88 RID: 7560
		public static string winLose = string.Empty;

		// Token: 0x04001D89 RID: 7561
		public static string learnSkill = string.Empty;

		// Token: 0x04001D8A RID: 7562
		public static string updSkill = string.Empty;

		// Token: 0x04001D8B RID: 7563
		public static string proficiency = string.Empty;

		// Token: 0x04001D8C RID: 7564
		public static string delacc = string.Empty;

		// Token: 0x04001D8D RID: 7565
		public static string notiINAPP = string.Empty;

		// Token: 0x04001D8E RID: 7566
		public static string notiRuby = string.Empty;

		// Token: 0x04001D8F RID: 7567
		public static string equip = string.Empty;

		// Token: 0x04001D90 RID: 7568
		public static string unlock = string.Empty;

		// Token: 0x04001D91 RID: 7569
		public static string radaCard = string.Empty;

		// Token: 0x04001D92 RID: 7570
		public static string not_enough_money_1 = string.Empty;

		// Token: 0x04001D93 RID: 7571
		public static string napngoc = string.Empty;

		// Token: 0x04001D94 RID: 7572
		public static string functionMaintain1 = string.Empty;

		// Token: 0x04001D95 RID: 7573
		public static string tang;

		// Token: 0x04001D96 RID: 7574
		public static string kquaVongQuay;

		// Token: 0x04001D97 RID: 7575
		public static string useGem;

		// Token: 0x04001D98 RID: 7576
		public static string autoFunction;

		// Token: 0x04001D99 RID: 7577
		public static string choitiep;

		// Token: 0x04001D9A RID: 7578
		public static string attack;

		// Token: 0x04001D9B RID: 7579
		public static string defend;

		// Token: 0x04001D9C RID: 7580
		public static string follow;

		// Token: 0x04001D9D RID: 7581
		public static string status;

		// Token: 0x04001D9E RID: 7582
		public static string gohome;

		// Token: 0x04001D9F RID: 7583
		public static string pet;

		// Token: 0x04001DA0 RID: 7584
		public static string maychutathoacmatsong;

		// Token: 0x04001DA1 RID: 7585
		public static string cauhinhthap;

		// Token: 0x04001DA2 RID: 7586
		public static string cauhinhcao;

		// Token: 0x04001DA3 RID: 7587
		public static string combineSpell;

		// Token: 0x04001DA4 RID: 7588
		public static string combineFail;

		// Token: 0x04001DA5 RID: 7589
		public static string combineSuccess;

		// Token: 0x04001DA6 RID: 7590
		public static string turnOnAnalog;

		// Token: 0x04001DA7 RID: 7591
		public static string turnOffAnalog;

		// Token: 0x04001DA8 RID: 7592
		public static string analog;

		// Token: 0x04001DA9 RID: 7593
		public static string inventory_Pass;

		// Token: 0x04001DAA RID: 7594
		public static string input_Inventory_Pass;

		// Token: 0x04001DAB RID: 7595
		public static string input_Inventory_Pass_wrong = string.Empty;

		// Token: 0x04001DAC RID: 7596
		public static string REGISTOPROTECT = string.Empty;

		// Token: 0x04001DAD RID: 7597
		public static string turnOnSound = string.Empty;

		// Token: 0x04001DAE RID: 7598
		public static string turnOffSound = string.Empty;

		// Token: 0x04001DAF RID: 7599
		public static string REGISTERING = string.Empty;

		// Token: 0x04001DB0 RID: 7600
		public static string SENDINGMSG = string.Empty;

		// Token: 0x04001DB1 RID: 7601
		public static string SENTMSG = string.Empty;

		// Token: 0x04001DB2 RID: 7602
		public static string NOSENDMSG = string.Empty;

		// Token: 0x04001DB3 RID: 7603
		public static string sendMsgSuccess = string.Empty;

		// Token: 0x04001DB4 RID: 7604
		public static string cannotSendMsg = string.Empty;

		// Token: 0x04001DB5 RID: 7605
		public static string sendGuessMsgSuccess = string.Empty;

		// Token: 0x04001DB6 RID: 7606
		public static string sendMsgFail = string.Empty;

		// Token: 0x04001DB7 RID: 7607
		public static string ALERT_PRIVATE_PASS_1 = string.Empty;

		// Token: 0x04001DB8 RID: 7608
		public static string ALERT_PRIVATE_PASS_2 = string.Empty;

		// Token: 0x04001DB9 RID: 7609
		public static string INPUT_PRIVATE_PASS = string.Empty;

		// Token: 0x04001DBA RID: 7610
		public static string change_account = string.Empty;

		// Token: 0x04001DBB RID: 7611
		public static string alreadyHadAccount1 = string.Empty;

		// Token: 0x04001DBC RID: 7612
		public static string alreadyHadAccount2 = string.Empty;

		// Token: 0x04001DBD RID: 7613
		public static string userBlank = string.Empty;

		// Token: 0x04001DBE RID: 7614
		public static string passwordBlank = string.Empty;

		// Token: 0x04001DBF RID: 7615
		public static string accTooShort = string.Empty;

		// Token: 0x04001DC0 RID: 7616
		public static string phoneInvalid = string.Empty;

		// Token: 0x04001DC1 RID: 7617
		public static string emailInvalid = string.Empty;

		// Token: 0x04001DC2 RID: 7618
		public static string registerNewAcc = string.Empty;

		// Token: 0x04001DC3 RID: 7619
		public static string selectServer = string.Empty;

		// Token: 0x04001DC4 RID: 7620
		public static string selectServer2 = string.Empty;

		// Token: 0x04001DC5 RID: 7621
		public static string forgetPass = string.Empty;

		// Token: 0x04001DC6 RID: 7622
		public static string password = string.Empty;

		// Token: 0x04001DC7 RID: 7623
		public static string[] LOGINLABELS = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001DC8 RID: 7624
		public static string msg = string.Empty;

		// Token: 0x04001DC9 RID: 7625
		public static string[] msgg = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001DCA RID: 7626
		public static string no_msg = string.Empty;

		// Token: 0x04001DCB RID: 7627
		public static string cancelAccountProtection = string.Empty;

		// Token: 0x04001DCC RID: 7628
		public static string plsCheckAcc = string.Empty;

		// Token: 0x04001DCD RID: 7629
		public static string phone = string.Empty;

		// Token: 0x04001DCE RID: 7630
		public static string email = string.Empty;

		// Token: 0x04001DCF RID: 7631
		public static string acc = string.Empty;

		// Token: 0x04001DD0 RID: 7632
		public static string pwd = string.Empty;

		// Token: 0x04001DD1 RID: 7633
		public static string goToWebForPassword = string.Empty;

		// Token: 0x04001DD2 RID: 7634
		public static string dragon_ball = string.Empty;

		// Token: 0x04001DD3 RID: 7635
		public static string character = string.Empty;

		// Token: 0x04001DD4 RID: 7636
		public static string account = string.Empty;

		// Token: 0x04001DD5 RID: 7637
		public static string account_server = string.Empty;

		// Token: 0x04001DD6 RID: 7638
		public static string char_name_blank = string.Empty;

		// Token: 0x04001DD7 RID: 7639
		public static string char_name_short = string.Empty;

		// Token: 0x04001DD8 RID: 7640
		public static string char_name_long = string.Empty;

		// Token: 0x04001DD9 RID: 7641
		public static string changeNameChar = string.Empty;

		// Token: 0x04001DDA RID: 7642
		public static string char_name = string.Empty;

		// Token: 0x04001DDB RID: 7643
		public static string login = string.Empty;

		// Token: 0x04001DDC RID: 7644
		public static string login2 = string.Empty;

		// Token: 0x04001DDD RID: 7645
		public static string register = string.Empty;

		// Token: 0x04001DDE RID: 7646
		public static string WAIT = string.Empty;

		// Token: 0x04001DDF RID: 7647
		public static string PLEASEWAIT = string.Empty;

		// Token: 0x04001DE0 RID: 7648
		public static string CONNECTING = string.Empty;

		// Token: 0x04001DE1 RID: 7649
		public static string LOGGING = string.Empty;

		// Token: 0x04001DE2 RID: 7650
		public static string LOADING = string.Empty;

		// Token: 0x04001DE3 RID: 7651
		public static string downloading_data = string.Empty;

		// Token: 0x04001DE4 RID: 7652
		public static string select_server = string.Empty;

		// Token: 0x04001DE5 RID: 7653
		public static string pls_restart_game_error = string.Empty;

		// Token: 0x04001DE6 RID: 7654
		public static string pls_restart_game_error2 = string.Empty;

		// Token: 0x04001DE7 RID: 7655
		public static string lost_connection = string.Empty;

		// Token: 0x04001DE8 RID: 7656
		public static string check_3G = string.Empty;

		// Token: 0x04001DE9 RID: 7657
		public static string UPDATE = string.Empty;

		// Token: 0x04001DEA RID: 7658
		public static string change_zone = string.Empty;

		// Token: 0x04001DEB RID: 7659
		public static string select_zone = string.Empty;

		// Token: 0x04001DEC RID: 7660
		public static string website = string.Empty;

		// Token: 0x04001DED RID: 7661
		public static string server = string.Empty;

		// Token: 0x04001DEE RID: 7662
		public static string planet = string.Empty;

		// Token: 0x04001DEF RID: 7663
		public static string[] MENUME = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04001DF0 RID: 7664
		public static string[] MENUGENDER = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04001DF1 RID: 7665
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

		// Token: 0x04001DF2 RID: 7666
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

		// Token: 0x04001DF3 RID: 7667
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

		// Token: 0x04001DF4 RID: 7668
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

		// Token: 0x04001DF5 RID: 7669
		public static string[][] petMainTab2 = new string[][]
		{
			new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty
			}
		};

		// Token: 0x04001DF6 RID: 7670
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

		// Token: 0x04001DF7 RID: 7671
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

		// Token: 0x04001DF8 RID: 7672
		public static string SKILL_FAIL = string.Empty;

		// Token: 0x04001DF9 RID: 7673
		public static string HP_EMPTY = string.Empty;

		// Token: 0x04001DFA RID: 7674
		public static string ZONE_HERE = string.Empty;

		// Token: 0x04001DFB RID: 7675
		public static string[] DES_TASK = new string[]
		{
			" ",
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04001DFC RID: 7676
		public static string[] DIES = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04001DFD RID: 7677
		public static string[] SYNTHESIS = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04001DFE RID: 7678
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

		// Token: 0x04001DFF RID: 7679
		public static string TASK_INPUT_CLASS = string.Empty;

		// Token: 0x04001E00 RID: 7680
		public static string SERI_NUM = string.Empty;

		// Token: 0x04001E01 RID: 7681
		public static string CARD_CODE = string.Empty;

		// Token: 0x04001E02 RID: 7682
		public static string pay_card = string.Empty;

		// Token: 0x04001E03 RID: 7683
		public static string pay_card2 = string.Empty;

		// Token: 0x04001E04 RID: 7684
		public static string serial_blank = string.Empty;

		// Token: 0x04001E05 RID: 7685
		public static string card_code_blank = string.Empty;

		// Token: 0x04001E06 RID: 7686
		public static string billion = string.Empty;

		// Token: 0x04001E07 RID: 7687
		public static string million = string.Empty;

		// Token: 0x04001E08 RID: 7688
		public static string MENU = string.Empty;

		// Token: 0x04001E09 RID: 7689
		public static string CLOSE = string.Empty;

		// Token: 0x04001E0A RID: 7690
		public static string ON = string.Empty;

		// Token: 0x04001E0B RID: 7691
		public static string OFF = string.Empty;

		// Token: 0x04001E0C RID: 7692
		public static string ENABLE = string.Empty;

		// Token: 0x04001E0D RID: 7693
		public static string DELETE = string.Empty;

		// Token: 0x04001E0E RID: 7694
		public static string VIEW = string.Empty;

		// Token: 0x04001E0F RID: 7695
		public static string CONTINUE = string.Empty;

		// Token: 0x04001E10 RID: 7696
		public static string NEXTSTEP = string.Empty;

		// Token: 0x04001E11 RID: 7697
		public static string USE = string.Empty;

		// Token: 0x04001E12 RID: 7698
		public static string SORT = string.Empty;

		// Token: 0x04001E13 RID: 7699
		public static string YES = string.Empty;

		// Token: 0x04001E14 RID: 7700
		public static string NO = string.Empty;

		// Token: 0x04001E15 RID: 7701
		public static string EXIT = string.Empty;

		// Token: 0x04001E16 RID: 7702
		public static string CHAT = string.Empty;

		// Token: 0x04001E17 RID: 7703
		public static string REVENGE = string.Empty;

		// Token: 0x04001E18 RID: 7704
		public static string OK = string.Empty;

		// Token: 0x04001E19 RID: 7705
		public static string retry = string.Empty;

		// Token: 0x04001E1A RID: 7706
		public static string uncheck = string.Empty;

		// Token: 0x04001E1B RID: 7707
		public static string remember = string.Empty;

		// Token: 0x04001E1C RID: 7708
		public static string ACCEPT = string.Empty;

		// Token: 0x04001E1D RID: 7709
		public static string CANCEL = string.Empty;

		// Token: 0x04001E1E RID: 7710
		public static string SELECT = string.Empty;

		// Token: 0x04001E1F RID: 7711
		public static string enter = string.Empty;

		// Token: 0x04001E20 RID: 7712
		public static string open_link = string.Empty;

		// Token: 0x04001E21 RID: 7713
		public static string DOYOUWANTEXIT = string.Empty;

		// Token: 0x04001E22 RID: 7714
		public static string NEWCHAR = string.Empty;

		// Token: 0x04001E23 RID: 7715
		public static string BACK = string.Empty;

		// Token: 0x04001E24 RID: 7716
		public static string LOCKED = string.Empty;

		// Token: 0x04001E25 RID: 7717
		public static string KILL = string.Empty;

		// Token: 0x04001E26 RID: 7718
		public static string KILLBOSS = string.Empty;

		// Token: 0x04001E27 RID: 7719
		public static string NOLOCK = string.Empty;

		// Token: 0x04001E28 RID: 7720
		public static string XU = string.Empty;

		// Token: 0x04001E29 RID: 7721
		public static string LUONG = string.Empty;

		// Token: 0x04001E2A RID: 7722
		public static string RUBY = string.Empty;

		// Token: 0x04001E2B RID: 7723
		public static string PK_NOW = string.Empty;

		// Token: 0x04001E2C RID: 7724
		public static string CUU_SAT = string.Empty;

		// Token: 0x04001E2D RID: 7725
		public static string NOT_ENOUGH_MP = string.Empty;

		// Token: 0x04001E2E RID: 7726
		public static string you_receive = string.Empty;

		// Token: 0x04001E2F RID: 7727
		public static string MONTH = string.Empty;

		// Token: 0x04001E30 RID: 7728
		public static string WEEK = string.Empty;

		// Token: 0x04001E31 RID: 7729
		public static string DAY = string.Empty;

		// Token: 0x04001E32 RID: 7730
		public static string HOUR = string.Empty;

		// Token: 0x04001E33 RID: 7731
		public static string SECOND = string.Empty;

		// Token: 0x04001E34 RID: 7732
		public static string MINUTE = string.Empty;

		// Token: 0x04001E35 RID: 7733
		public static string LEARN_SKILL = string.Empty;

		// Token: 0x04001E36 RID: 7734
		public static string rank = string.Empty;

		// Token: 0x04001E37 RID: 7735
		public static string active_point = string.Empty;

		// Token: 0x04001E38 RID: 7736
		public static string friend = string.Empty;

		// Token: 0x04001E39 RID: 7737
		public static string enemy = string.Empty;

		// Token: 0x04001E3A RID: 7738
		public static string no_friend = string.Empty;

		// Token: 0x04001E3B RID: 7739
		public static string chat_world = string.Empty;

		// Token: 0x04001E3C RID: 7740
		public static string change_flag = string.Empty;

		// Token: 0x04001E3D RID: 7741
		public static string gameInfo = string.Empty;

		// Token: 0x04001E3E RID: 7742
		public static string quayso = string.Empty;

		// Token: 0x04001E3F RID: 7743
		public static string option = string.Empty;

		// Token: 0x04001E40 RID: 7744
		public static string high = string.Empty;

		// Token: 0x04001E41 RID: 7745
		public static string medium = string.Empty;

		// Token: 0x04001E42 RID: 7746
		public static string low = string.Empty;

		// Token: 0x04001E43 RID: 7747
		public static string increase_vga = string.Empty;

		// Token: 0x04001E44 RID: 7748
		public static string decrease_vga = string.Empty;

		// Token: 0x04001E45 RID: 7749
		public static string serverchat_off = string.Empty;

		// Token: 0x04001E46 RID: 7750
		public static string serverchat_on = string.Empty;

		// Token: 0x04001E47 RID: 7751
		public static string x2Screen = string.Empty;

		// Token: 0x04001E48 RID: 7752
		public static string x1Screen = string.Empty;

		// Token: 0x04001E49 RID: 7753
		public static string changeSizeScreen = string.Empty;

		// Token: 0x04001E4A RID: 7754
		public static string aura_off = string.Empty;

		// Token: 0x04001E4B RID: 7755
		public static string aura_on = string.Empty;

		// Token: 0x04001E4C RID: 7756
		public static string aura_off_2 = string.Empty;

		// Token: 0x04001E4D RID: 7757
		public static string aura_on_2 = string.Empty;

		// Token: 0x04001E4E RID: 7758
		public static string hat_off = string.Empty;

		// Token: 0x04001E4F RID: 7759
		public static string hat_on = string.Empty;

		// Token: 0x04001E50 RID: 7760
		public static string chest = string.Empty;

		// Token: 0x04001E51 RID: 7761
		public static string[] chestt = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E52 RID: 7762
		public static string[] inventory = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E53 RID: 7763
		public static string[] combine = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E54 RID: 7764
		public static string[] mapp = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E55 RID: 7765
		public static string[] item_give = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E56 RID: 7766
		public static string[] item_receive = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E57 RID: 7767
		public static string[] zonee = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E58 RID: 7768
		public static string zone = string.Empty;

		// Token: 0x04001E59 RID: 7769
		public static string map = string.Empty;

		// Token: 0x04001E5A RID: 7770
		public static string item_receive2 = string.Empty;

		// Token: 0x04001E5B RID: 7771
		public static string item = string.Empty;

		// Token: 0x04001E5C RID: 7772
		public static string give_upper = string.Empty;

		// Token: 0x04001E5D RID: 7773
		public static string receive_upper = string.Empty;

		// Token: 0x04001E5E RID: 7774
		public static string receive_all = string.Empty;

		// Token: 0x04001E5F RID: 7775
		public static string no_map = string.Empty;

		// Token: 0x04001E60 RID: 7776
		public static string go_to_quest = string.Empty;

		// Token: 0x04001E61 RID: 7777
		public static string from_earth = string.Empty;

		// Token: 0x04001E62 RID: 7778
		public static string from_namec = string.Empty;

		// Token: 0x04001E63 RID: 7779
		public static string from_sayda = string.Empty;

		// Token: 0x04001E64 RID: 7780
		public static string expire = string.Empty;

		// Token: 0x04001E65 RID: 7781
		public static string pow_request = string.Empty;

		// Token: 0x04001E66 RID: 7782
		public static string your_pow = string.Empty;

		// Token: 0x04001E67 RID: 7783
		public static string used = string.Empty;

		// Token: 0x04001E68 RID: 7784
		public static string place = string.Empty;

		// Token: 0x04001E69 RID: 7785
		public static string FOREVER = string.Empty;

		// Token: 0x04001E6A RID: 7786
		public static string NOUPGRADE = string.Empty;

		// Token: 0x04001E6B RID: 7787
		public static string NOTUPGRADE = string.Empty;

		// Token: 0x04001E6C RID: 7788
		public static string UPGRADE = string.Empty;

		// Token: 0x04001E6D RID: 7789
		public static string UPGRADING = string.Empty;

		// Token: 0x04001E6E RID: 7790
		public static string make_shortcut = string.Empty;

		// Token: 0x04001E6F RID: 7791
		public static string into_place = string.Empty;

		// Token: 0x04001E70 RID: 7792
		public static string move_to_chest = string.Empty;

		// Token: 0x04001E71 RID: 7793
		public static string move_to_chest2 = string.Empty;

		// Token: 0x04001E72 RID: 7794
		public static string press_chat_querty = string.Empty;

		// Token: 0x04001E73 RID: 7795
		public static string press_chat = string.Empty;

		// Token: 0x04001E74 RID: 7796
		public static string saying = string.Empty;

		// Token: 0x04001E75 RID: 7797
		public static string miss = string.Empty;

		// Token: 0x04001E76 RID: 7798
		public static string donate = string.Empty;

		// Token: 0x04001E77 RID: 7799
		public static string receive = string.Empty;

		// Token: 0x04001E78 RID: 7800
		public static string press_twice = string.Empty;

		// Token: 0x04001E79 RID: 7801
		public static string can_harvest = string.Empty;

		// Token: 0x04001E7A RID: 7802
		public static string do_accept_qwerty = string.Empty;

		// Token: 0x04001E7B RID: 7803
		public static string do_accept = string.Empty;

		// Token: 0x04001E7C RID: 7804
		public static string plsRestartGame = string.Empty;

		// Token: 0x04001E7D RID: 7805
		public static string is_online = string.Empty;

		// Token: 0x04001E7E RID: 7806
		public static string is_offline = string.Empty;

		// Token: 0x04001E7F RID: 7807
		public static string make_friend = string.Empty;

		// Token: 0x04001E80 RID: 7808
		public static string chat_player = string.Empty;

		// Token: 0x04001E81 RID: 7809
		public static string chat_with = string.Empty;

		// Token: 0x04001E82 RID: 7810
		public static string clan_capsuledonate = string.Empty;

		// Token: 0x04001E83 RID: 7811
		public static string clan_capsuleself = string.Empty;

		// Token: 0x04001E84 RID: 7812
		public static string clan_point = string.Empty;

		// Token: 0x04001E85 RID: 7813
		public static string give_pea = string.Empty;

		// Token: 0x04001E86 RID: 7814
		public static string receive_pea = string.Empty;

		// Token: 0x04001E87 RID: 7815
		public static string request_pea = string.Empty;

		// Token: 0x04001E88 RID: 7816
		public static string time = string.Empty;

		// Token: 0x04001E89 RID: 7817
		public static string received = string.Empty;

		// Token: 0x04001E8A RID: 7818
		public static string power = string.Empty;

		// Token: 0x04001E8B RID: 7819
		public static string join_date = string.Empty;

		// Token: 0x04001E8C RID: 7820
		public static string clan_leader = string.Empty;

		// Token: 0x04001E8D RID: 7821
		public static string clan_coleader = string.Empty;

		// Token: 0x04001E8E RID: 7822
		public static string power_point = string.Empty;

		// Token: 0x04001E8F RID: 7823
		public static string member = string.Empty;

		// Token: 0x04001E90 RID: 7824
		public static string[] memberr = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E91 RID: 7825
		public static string[] chatClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E92 RID: 7826
		public static string[] leaveClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E93 RID: 7827
		public static string[] createClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E94 RID: 7828
		public static string[] findClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E95 RID: 7829
		public static string[] khau_hieuu = new string[]
		{
			string.Empty
		};

		// Token: 0x04001E96 RID: 7830
		public static string[] bieu_tuongg = new string[]
		{
			string.Empty
		};

		// Token: 0x04001E97 RID: 7831
		public static string[] request_pea2 = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04001E98 RID: 7832
		public static string level = string.Empty;

		// Token: 0x04001E99 RID: 7833
		public static string clan_birthday = string.Empty;

		// Token: 0x04001E9A RID: 7834
		public static string clan_list = string.Empty;

		// Token: 0x04001E9B RID: 7835
		public static string create = string.Empty;

		// Token: 0x04001E9C RID: 7836
		public static string find = string.Empty;

		// Token: 0x04001E9D RID: 7837
		public static string leave = string.Empty;

		// Token: 0x04001E9E RID: 7838
		public static string not_join_clan = string.Empty;

		// Token: 0x04001E9F RID: 7839
		public static string[] clanEmpty = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04001EA0 RID: 7840
		public static string input_clan_name = string.Empty;

		// Token: 0x04001EA1 RID: 7841
		public static string clan_name = string.Empty;

		// Token: 0x04001EA2 RID: 7842
		public static string chat_clan = string.Empty;

		// Token: 0x04001EA3 RID: 7843
		public static string input_clan_name_to_create = string.Empty;

		// Token: 0x04001EA4 RID: 7844
		public static string input_clan_slogan = string.Empty;

		// Token: 0x04001EA5 RID: 7845
		public static string do_u_want_join_clan = string.Empty;

		// Token: 0x04001EA6 RID: 7846
		public static string select_clan_icon = string.Empty;

		// Token: 0x04001EA7 RID: 7847
		public static string request_join_clan = string.Empty;

		// Token: 0x04001EA8 RID: 7848
		public static string view_clan_member = string.Empty;

		// Token: 0x04001EA9 RID: 7849
		public static string create_clan_co_leader = string.Empty;

		// Token: 0x04001EAA RID: 7850
		public static string create_clan_leader = string.Empty;

		// Token: 0x04001EAB RID: 7851
		public static string disable_clan_mastership = string.Empty;

		// Token: 0x04001EAC RID: 7852
		public static string kick_clan_mem = string.Empty;

		// Token: 0x04001EAD RID: 7853
		public static string clan_name_blank = string.Empty;

		// Token: 0x04001EAE RID: 7854
		public static string clan_slogan_blank = string.Empty;

		// Token: 0x04001EAF RID: 7855
		public static string cannot_find_clan = string.Empty;

		// Token: 0x04001EB0 RID: 7856
		public static string ago = string.Empty;

		// Token: 0x04001EB1 RID: 7857
		public static string findingClan = string.Empty;

		// Token: 0x04001EB2 RID: 7858
		public static string trade = string.Empty;

		// Token: 0x04001EB3 RID: 7859
		public static string not_lock_trade = string.Empty;

		// Token: 0x04001EB4 RID: 7860
		public static string not_lock_trade_upper = string.Empty;

		// Token: 0x04001EB5 RID: 7861
		public static string locked_trade = string.Empty;

		// Token: 0x04001EB6 RID: 7862
		public static string locked_trade_upper = string.Empty;

		// Token: 0x04001EB7 RID: 7863
		public static string lock_trade = string.Empty;

		// Token: 0x04001EB8 RID: 7864
		public static string wait_opp_lock_trade = string.Empty;

		// Token: 0x04001EB9 RID: 7865
		public static string press_done = string.Empty;

		// Token: 0x04001EBA RID: 7866
		public static string THROW = string.Empty;

		// Token: 0x04001EBB RID: 7867
		public static string SPLIT = string.Empty;

		// Token: 0x04001EBC RID: 7868
		public static string done = string.Empty;

		// Token: 0x04001EBD RID: 7869
		public static string opponent = string.Empty;

		// Token: 0x04001EBE RID: 7870
		public static string you = string.Empty;

		// Token: 0x04001EBF RID: 7871
		public static string mlock = string.Empty;

		// Token: 0x04001EC0 RID: 7872
		public static string money_trade = string.Empty;

		// Token: 0x04001EC1 RID: 7873
		public static string GETOUT = string.Empty;

		// Token: 0x04001EC2 RID: 7874
		public static string MOVEOUT = string.Empty;

		// Token: 0x04001EC3 RID: 7875
		public static string MOVEFORPET = string.Empty;

		// Token: 0x04001EC4 RID: 7876
		public static string GETOUTMONEY = string.Empty;

		// Token: 0x04001EC5 RID: 7877
		public static string GETINMONEY = string.Empty;

		// Token: 0x04001EC6 RID: 7878
		public static string SENDMONEY = string.Empty;

		// Token: 0x04001EC7 RID: 7879
		public static string GETIN = string.Empty;

		// Token: 0x04001EC8 RID: 7880
		public static string SALE = string.Empty;

		// Token: 0x04001EC9 RID: 7881
		public static string SALES = string.Empty;

		// Token: 0x04001ECA RID: 7882
		public static string SALEALL = string.Empty;

		// Token: 0x04001ECB RID: 7883
		public static string BUY = string.Empty;

		// Token: 0x04001ECC RID: 7884
		public static string BUYS = string.Empty;

		// Token: 0x04001ECD RID: 7885
		public static string input_money_to_trade = string.Empty;

		// Token: 0x04001ECE RID: 7886
		public static string input_money = string.Empty;

		// Token: 0x04001ECF RID: 7887
		public static string input_money_wrong = string.Empty;

		// Token: 0x04001ED0 RID: 7888
		public static string not_enough_money = string.Empty;

		// Token: 0x04001ED1 RID: 7889
		public static string input_quantity_to_trade = string.Empty;

		// Token: 0x04001ED2 RID: 7890
		public static string input_quantity = string.Empty;

		// Token: 0x04001ED3 RID: 7891
		public static string input_quantity_wrong = string.Empty;

		// Token: 0x04001ED4 RID: 7892
		public static string already_has_item = string.Empty;

		// Token: 0x04001ED5 RID: 7893
		public static string unlock_item_to_trade = string.Empty;

		// Token: 0x04001ED6 RID: 7894
		public static string root = string.Empty;

		// Token: 0x04001ED7 RID: 7895
		public static string need = string.Empty;

		// Token: 0x04001ED8 RID: 7896
		public static string need_upper = string.Empty;

		// Token: 0x04001ED9 RID: 7897
		public static string free = string.Empty;

		// Token: 0x04001EDA RID: 7898
		public static string free1 = string.Empty;

		// Token: 0x04001EDB RID: 7899
		public static string free2 = string.Empty;

		// Token: 0x04001EDC RID: 7900
		public static string select_item = string.Empty;

		// Token: 0x04001EDD RID: 7901
		public static string random = string.Empty;

		// Token: 0x04001EDE RID: 7902
		public static string say_hello = string.Empty;

		// Token: 0x04001EDF RID: 7903
		public static string say_wat_do_u_want_to_buy = string.Empty;

		// Token: 0x04001EE0 RID: 7904
		public static string say_wat_do_u_want_to_buy2 = string.Empty;

		// Token: 0x04001EE1 RID: 7905
		public static string do_u_sure_to_trade = string.Empty;

		// Token: 0x04001EE2 RID: 7906
		public static string learn_with = string.Empty;

		// Token: 0x04001EE3 RID: 7907
		public static string buy_with = string.Empty;

		// Token: 0x04001EE4 RID: 7908
		public static string can_not_do_when_die = string.Empty;

		// Token: 0x04001EE5 RID: 7909
		public static string use_for_combine = string.Empty;

		// Token: 0x04001EE6 RID: 7910
		public static string use_for_trade = string.Empty;

		// Token: 0x04001EE7 RID: 7911
		public static string not_enough_luong_world_channel = string.Empty;

		// Token: 0x04001EE8 RID: 7912
		public static string world_channel_5_luong = string.Empty;

		// Token: 0x04001EE9 RID: 7913
		public static string want_to_trade = string.Empty;

		// Token: 0x04001EEA RID: 7914
		public static string hasJustUpgrade1 = string.Empty;

		// Token: 0x04001EEB RID: 7915
		public static string hasJustUpgrade2 = string.Empty;

		// Token: 0x04001EEC RID: 7916
		public static string potential_to_learn = string.Empty;

		// Token: 0x04001EED RID: 7917
		public static string potential_point = string.Empty;

		// Token: 0x04001EEE RID: 7918
		public static string achievement_point = string.Empty;

		// Token: 0x04001EEF RID: 7919
		public static string increase = string.Empty;

		// Token: 0x04001EF0 RID: 7920
		public static string increase_upper = string.Empty;

		// Token: 0x04001EF1 RID: 7921
		public static string not_enough_potential_point1 = string.Empty;

		// Token: 0x04001EF2 RID: 7922
		public static string not_enough_potential_point2 = string.Empty;

		// Token: 0x04001EF3 RID: 7923
		public static string use_potential_point_for1 = string.Empty;

		// Token: 0x04001EF4 RID: 7924
		public static string use_potential_point_for2 = string.Empty;

		// Token: 0x04001EF5 RID: 7925
		public static string for_HP = string.Empty;

		// Token: 0x04001EF6 RID: 7926
		public static string for_KI = string.Empty;

		// Token: 0x04001EF7 RID: 7927
		public static string for_hit_point = string.Empty;

		// Token: 0x04001EF8 RID: 7928
		public static string for_armor = string.Empty;

		// Token: 0x04001EF9 RID: 7929
		public static string for_crit = string.Empty;

		// Token: 0x04001EFA RID: 7930
		public static string can_buy_from_Uron1 = string.Empty;

		// Token: 0x04001EFB RID: 7931
		public static string can_buy_from_Uron2 = string.Empty;

		// Token: 0x04001EFC RID: 7932
		public static string can_buy_from_Uron3 = string.Empty;

		// Token: 0x04001EFD RID: 7933
		public static string HP = string.Empty;

		// Token: 0x04001EFE RID: 7934
		public static string KI = string.Empty;

		// Token: 0x04001EFF RID: 7935
		public static string hit_point = string.Empty;

		// Token: 0x04001F00 RID: 7936
		public static string armor = string.Empty;

		// Token: 0x04001F01 RID: 7937
		public static string vitality = string.Empty;

		// Token: 0x04001F02 RID: 7938
		public static string critical = string.Empty;

		// Token: 0x04001F03 RID: 7939
		public static string cap_do = string.Empty;

		// Token: 0x04001F04 RID: 7940
		public static string KI_consume = string.Empty;

		// Token: 0x04001F05 RID: 7941
		public static string cooldown = string.Empty;

		// Token: 0x04001F06 RID: 7942
		public static string milisecond = string.Empty;

		// Token: 0x04001F07 RID: 7943
		public static string max_level_reach = string.Empty;

		// Token: 0x04001F08 RID: 7944
		public static string next_level_require = string.Empty;

		// Token: 0x04001F09 RID: 7945
		public static string potential = string.Empty;

		// Token: 0x04001F0A RID: 7946
		public static string potential2 = string.Empty;

		// Token: 0x04001F0B RID: 7947
		public static string not_learn = string.Empty;

		// Token: 0x04001F0C RID: 7948
		public static string learn_require = string.Empty;

		// Token: 0x04001F0D RID: 7949
		public static string learn = string.Empty;

		// Token: 0x04001F0E RID: 7950
		public static string to_gain_20hp = string.Empty;

		// Token: 0x04001F0F RID: 7951
		public static string to_gain_20mp = string.Empty;

		// Token: 0x04001F10 RID: 7952
		public static string to_gain_1pow = string.Empty;

		// Token: 0x04001F11 RID: 7953
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

		// Token: 0x04001F12 RID: 7954
		public static string hp_ki_full = string.Empty;

		// Token: 0x04001F13 RID: 7955
		public static string quest_place = string.Empty;

		// Token: 0x04001F14 RID: 7956
		public static string no_mission = string.Empty;

		// Token: 0x04001F15 RID: 7957
		public static string reward_mission = string.Empty;

		// Token: 0x04001F16 RID: 7958
		public static string achievement_mission = string.Empty;

		// Token: 0x04001F17 RID: 7959
		public static string trangbi = string.Empty;

		// Token: 0x04001F18 RID: 7960
		public static string wat_do_u_want = string.Empty;

		// Token: 0x04001F19 RID: 7961
		public static string off = string.Empty;

		// Token: 0x04001F1A RID: 7962
		public static string on = string.Empty;

		// Token: 0x04001F1B RID: 7963
		public static string select_map = string.Empty;

		// Token: 0x04001F1C RID: 7964
		public static string offPlease = string.Empty;

		// Token: 0x04001F1D RID: 7965
		public static string onPlease = string.Empty;

		// Token: 0x04001F1E RID: 7966
		public static sbyte language;

		// Token: 0x04001F1F RID: 7967
		public static string choigame;

		// Token: 0x04001F20 RID: 7968
		public static string no_enemy = string.Empty;

		// Token: 0x04001F21 RID: 7969
		public static string kigui;

		// Token: 0x04001F22 RID: 7970
		public static string kiguiXu;

		// Token: 0x04001F23 RID: 7971
		public static string kiguiLuong;

		// Token: 0x04001F24 RID: 7972
		public static string kiguiXuchat;

		// Token: 0x04001F25 RID: 7973
		public static string kiguiLuongchat;

		// Token: 0x04001F26 RID: 7974
		public static string huykigui;

		// Token: 0x04001F27 RID: 7975
		public static string nhantien;

		// Token: 0x04001F28 RID: 7976
		public static string dangban;

		// Token: 0x04001F29 RID: 7977
		public static string daban;

		// Token: 0x04001F2A RID: 7978
		public static string num;

		// Token: 0x04001F2B RID: 7979
		public static string upTop;

		// Token: 0x04001F2C RID: 7980
		public static string page;

		// Token: 0x04001F2D RID: 7981
		public static string getDown;

		// Token: 0x04001F2E RID: 7982
		public static string getUp;

		// Token: 0x04001F2F RID: 7983
		public static string notYetSell;

		// Token: 0x04001F30 RID: 7984
		public static string charger;

		// Token: 0x04001F31 RID: 7985
		public static string finishBomong;

		// Token: 0x04001F32 RID: 7986
		public static string note;

		// Token: 0x04001F33 RID: 7987
		public static string regNote;

		// Token: 0x04001F34 RID: 7988
		public static string remain;

		// Token: 0x04001F35 RID: 7989
		public static string faster;

		// Token: 0x04001F36 RID: 7990
		public static string fasterQuestion;

		// Token: 0x04001F37 RID: 7991
		public static string chuacotaikhoan;

		// Token: 0x04001F38 RID: 7992
		public static string taidulieudechoi;

		// Token: 0x04001F39 RID: 7993
		public static string huy;

		// Token: 0x04001F3A RID: 7994
		public static string taidulieu;

		// Token: 0x04001F3B RID: 7995
		public static string xoadulieu;

		// Token: 0x04001F3C RID: 7996
		public static string deletaDataNote;

		// Token: 0x04001F3D RID: 7997
		public static string playNew;

		// Token: 0x04001F3E RID: 7998
		public static string playAcc;

		// Token: 0x04001F3F RID: 7999
		public static string vuilongnhapduthongtin;

		// Token: 0x04001F40 RID: 8000
		public static string not_register_yet = string.Empty;

		// Token: 0x04001F41 RID: 8001
		public static string nhanngoc;

		// Token: 0x04001F42 RID: 8002
		public static string fusion;

		// Token: 0x04001F43 RID: 8003
		public static string sure_fusion;

		// Token: 0x04001F44 RID: 8004
		public static string fusionForever;

		// Token: 0x04001F45 RID: 8005
		public static string xinchucmung;

		// Token: 0x04001F46 RID: 8006
		public static string den;

		// Token: 0x04001F47 RID: 8007
		public static string nhatvatpham;
	}
}
