using System;

namespace Game2
{
	// Token: 0x020003D8 RID: 984
	public class mResources
	{
		// Token: 0x06002BB7 RID: 11191 RVA: 0x002ACCFA File Offset: 0x002AAEFA
		public static void loadLanguague(sbyte newLanguage)
		{
			mResources.language = newLanguage;
			T1.load();
			GameCanvas.isLoadRes = true;
		}

		// Token: 0x04005503 RID: 21763
		public static string confirmChangeServer = string.Empty;

		// Token: 0x04005504 RID: 21764
		public static string chooseDefaultsv = string.Empty;

		// Token: 0x04005505 RID: 21765
		public static string winLose = string.Empty;

		// Token: 0x04005506 RID: 21766
		public static string learnSkill = string.Empty;

		// Token: 0x04005507 RID: 21767
		public static string updSkill = string.Empty;

		// Token: 0x04005508 RID: 21768
		public static string proficiency = string.Empty;

		// Token: 0x04005509 RID: 21769
		public static string delacc = string.Empty;

		// Token: 0x0400550A RID: 21770
		public static string notiINAPP = string.Empty;

		// Token: 0x0400550B RID: 21771
		public static string notiRuby = string.Empty;

		// Token: 0x0400550C RID: 21772
		public static string equip = string.Empty;

		// Token: 0x0400550D RID: 21773
		public static string unlock = string.Empty;

		// Token: 0x0400550E RID: 21774
		public static string radaCard = string.Empty;

		// Token: 0x0400550F RID: 21775
		public static string not_enough_money_1 = string.Empty;

		// Token: 0x04005510 RID: 21776
		public static string napngoc = string.Empty;

		// Token: 0x04005511 RID: 21777
		public static string functionMaintain1 = string.Empty;

		// Token: 0x04005512 RID: 21778
		public static string tang;

		// Token: 0x04005513 RID: 21779
		public static string kquaVongQuay;

		// Token: 0x04005514 RID: 21780
		public static string useGem;

		// Token: 0x04005515 RID: 21781
		public static string autoFunction;

		// Token: 0x04005516 RID: 21782
		public static string choitiep;

		// Token: 0x04005517 RID: 21783
		public static string attack;

		// Token: 0x04005518 RID: 21784
		public static string defend;

		// Token: 0x04005519 RID: 21785
		public static string follow;

		// Token: 0x0400551A RID: 21786
		public static string status;

		// Token: 0x0400551B RID: 21787
		public static string gohome;

		// Token: 0x0400551C RID: 21788
		public static string pet;

		// Token: 0x0400551D RID: 21789
		public static string maychutathoacmatsong;

		// Token: 0x0400551E RID: 21790
		public static string cauhinhthap;

		// Token: 0x0400551F RID: 21791
		public static string cauhinhcao;

		// Token: 0x04005520 RID: 21792
		public static string combineSpell;

		// Token: 0x04005521 RID: 21793
		public static string combineFail;

		// Token: 0x04005522 RID: 21794
		public static string combineSuccess;

		// Token: 0x04005523 RID: 21795
		public static string turnOnAnalog;

		// Token: 0x04005524 RID: 21796
		public static string turnOffAnalog;

		// Token: 0x04005525 RID: 21797
		public static string analog;

		// Token: 0x04005526 RID: 21798
		public static string inventory_Pass;

		// Token: 0x04005527 RID: 21799
		public static string input_Inventory_Pass;

		// Token: 0x04005528 RID: 21800
		public static string input_Inventory_Pass_wrong = string.Empty;

		// Token: 0x04005529 RID: 21801
		public static string REGISTOPROTECT = string.Empty;

		// Token: 0x0400552A RID: 21802
		public static string turnOnSound = string.Empty;

		// Token: 0x0400552B RID: 21803
		public static string turnOffSound = string.Empty;

		// Token: 0x0400552C RID: 21804
		public static string REGISTERING = string.Empty;

		// Token: 0x0400552D RID: 21805
		public static string SENDINGMSG = string.Empty;

		// Token: 0x0400552E RID: 21806
		public static string SENTMSG = string.Empty;

		// Token: 0x0400552F RID: 21807
		public static string NOSENDMSG = string.Empty;

		// Token: 0x04005530 RID: 21808
		public static string sendMsgSuccess = string.Empty;

		// Token: 0x04005531 RID: 21809
		public static string cannotSendMsg = string.Empty;

		// Token: 0x04005532 RID: 21810
		public static string sendGuessMsgSuccess = string.Empty;

		// Token: 0x04005533 RID: 21811
		public static string sendMsgFail = string.Empty;

		// Token: 0x04005534 RID: 21812
		public static string ALERT_PRIVATE_PASS_1 = string.Empty;

		// Token: 0x04005535 RID: 21813
		public static string ALERT_PRIVATE_PASS_2 = string.Empty;

		// Token: 0x04005536 RID: 21814
		public static string INPUT_PRIVATE_PASS = string.Empty;

		// Token: 0x04005537 RID: 21815
		public static string change_account = string.Empty;

		// Token: 0x04005538 RID: 21816
		public static string alreadyHadAccount1 = string.Empty;

		// Token: 0x04005539 RID: 21817
		public static string alreadyHadAccount2 = string.Empty;

		// Token: 0x0400553A RID: 21818
		public static string userBlank = string.Empty;

		// Token: 0x0400553B RID: 21819
		public static string passwordBlank = string.Empty;

		// Token: 0x0400553C RID: 21820
		public static string accTooShort = string.Empty;

		// Token: 0x0400553D RID: 21821
		public static string phoneInvalid = string.Empty;

		// Token: 0x0400553E RID: 21822
		public static string emailInvalid = string.Empty;

		// Token: 0x0400553F RID: 21823
		public static string registerNewAcc = string.Empty;

		// Token: 0x04005540 RID: 21824
		public static string selectServer = string.Empty;

		// Token: 0x04005541 RID: 21825
		public static string selectServer2 = string.Empty;

		// Token: 0x04005542 RID: 21826
		public static string forgetPass = string.Empty;

		// Token: 0x04005543 RID: 21827
		public static string password = string.Empty;

		// Token: 0x04005544 RID: 21828
		public static string[] LOGINLABELS = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04005545 RID: 21829
		public static string msg = string.Empty;

		// Token: 0x04005546 RID: 21830
		public static string[] msgg = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04005547 RID: 21831
		public static string no_msg = string.Empty;

		// Token: 0x04005548 RID: 21832
		public static string cancelAccountProtection = string.Empty;

		// Token: 0x04005549 RID: 21833
		public static string plsCheckAcc = string.Empty;

		// Token: 0x0400554A RID: 21834
		public static string phone = string.Empty;

		// Token: 0x0400554B RID: 21835
		public static string email = string.Empty;

		// Token: 0x0400554C RID: 21836
		public static string acc = string.Empty;

		// Token: 0x0400554D RID: 21837
		public static string pwd = string.Empty;

		// Token: 0x0400554E RID: 21838
		public static string goToWebForPassword = string.Empty;

		// Token: 0x0400554F RID: 21839
		public static string dragon_ball = string.Empty;

		// Token: 0x04005550 RID: 21840
		public static string character = string.Empty;

		// Token: 0x04005551 RID: 21841
		public static string account = string.Empty;

		// Token: 0x04005552 RID: 21842
		public static string account_server = string.Empty;

		// Token: 0x04005553 RID: 21843
		public static string char_name_blank = string.Empty;

		// Token: 0x04005554 RID: 21844
		public static string char_name_short = string.Empty;

		// Token: 0x04005555 RID: 21845
		public static string char_name_long = string.Empty;

		// Token: 0x04005556 RID: 21846
		public static string changeNameChar = string.Empty;

		// Token: 0x04005557 RID: 21847
		public static string char_name = string.Empty;

		// Token: 0x04005558 RID: 21848
		public static string login = string.Empty;

		// Token: 0x04005559 RID: 21849
		public static string login2 = string.Empty;

		// Token: 0x0400555A RID: 21850
		public static string register = string.Empty;

		// Token: 0x0400555B RID: 21851
		public static string WAIT = string.Empty;

		// Token: 0x0400555C RID: 21852
		public static string PLEASEWAIT = string.Empty;

		// Token: 0x0400555D RID: 21853
		public static string CONNECTING = string.Empty;

		// Token: 0x0400555E RID: 21854
		public static string LOGGING = string.Empty;

		// Token: 0x0400555F RID: 21855
		public static string LOADING = string.Empty;

		// Token: 0x04005560 RID: 21856
		public static string downloading_data = string.Empty;

		// Token: 0x04005561 RID: 21857
		public static string select_server = string.Empty;

		// Token: 0x04005562 RID: 21858
		public static string pls_restart_game_error = string.Empty;

		// Token: 0x04005563 RID: 21859
		public static string pls_restart_game_error2 = string.Empty;

		// Token: 0x04005564 RID: 21860
		public static string lost_connection = string.Empty;

		// Token: 0x04005565 RID: 21861
		public static string check_3G = string.Empty;

		// Token: 0x04005566 RID: 21862
		public static string UPDATE = string.Empty;

		// Token: 0x04005567 RID: 21863
		public static string change_zone = string.Empty;

		// Token: 0x04005568 RID: 21864
		public static string select_zone = string.Empty;

		// Token: 0x04005569 RID: 21865
		public static string website = string.Empty;

		// Token: 0x0400556A RID: 21866
		public static string server = string.Empty;

		// Token: 0x0400556B RID: 21867
		public static string planet = string.Empty;

		// Token: 0x0400556C RID: 21868
		public static string[] MENUME = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400556D RID: 21869
		public static string[] MENUGENDER = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400556E RID: 21870
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

		// Token: 0x0400556F RID: 21871
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

		// Token: 0x04005570 RID: 21872
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

		// Token: 0x04005571 RID: 21873
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

		// Token: 0x04005572 RID: 21874
		public static string[][] petMainTab2 = new string[][]
		{
			new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty
			}
		};

		// Token: 0x04005573 RID: 21875
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

		// Token: 0x04005574 RID: 21876
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

		// Token: 0x04005575 RID: 21877
		public static string SKILL_FAIL = string.Empty;

		// Token: 0x04005576 RID: 21878
		public static string HP_EMPTY = string.Empty;

		// Token: 0x04005577 RID: 21879
		public static string ZONE_HERE = string.Empty;

		// Token: 0x04005578 RID: 21880
		public static string[] DES_TASK = new string[]
		{
			" ",
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04005579 RID: 21881
		public static string[] DIES = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400557A RID: 21882
		public static string[] SYNTHESIS = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400557B RID: 21883
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

		// Token: 0x0400557C RID: 21884
		public static string TASK_INPUT_CLASS = string.Empty;

		// Token: 0x0400557D RID: 21885
		public static string SERI_NUM = string.Empty;

		// Token: 0x0400557E RID: 21886
		public static string CARD_CODE = string.Empty;

		// Token: 0x0400557F RID: 21887
		public static string pay_card = string.Empty;

		// Token: 0x04005580 RID: 21888
		public static string pay_card2 = string.Empty;

		// Token: 0x04005581 RID: 21889
		public static string serial_blank = string.Empty;

		// Token: 0x04005582 RID: 21890
		public static string card_code_blank = string.Empty;

		// Token: 0x04005583 RID: 21891
		public static string billion = string.Empty;

		// Token: 0x04005584 RID: 21892
		public static string million = string.Empty;

		// Token: 0x04005585 RID: 21893
		public static string MENU = string.Empty;

		// Token: 0x04005586 RID: 21894
		public static string CLOSE = string.Empty;

		// Token: 0x04005587 RID: 21895
		public static string ON = string.Empty;

		// Token: 0x04005588 RID: 21896
		public static string OFF = string.Empty;

		// Token: 0x04005589 RID: 21897
		public static string ENABLE = string.Empty;

		// Token: 0x0400558A RID: 21898
		public static string DELETE = string.Empty;

		// Token: 0x0400558B RID: 21899
		public static string VIEW = string.Empty;

		// Token: 0x0400558C RID: 21900
		public static string CONTINUE = string.Empty;

		// Token: 0x0400558D RID: 21901
		public static string NEXTSTEP = string.Empty;

		// Token: 0x0400558E RID: 21902
		public static string USE = string.Empty;

		// Token: 0x0400558F RID: 21903
		public static string SORT = string.Empty;

		// Token: 0x04005590 RID: 21904
		public static string YES = string.Empty;

		// Token: 0x04005591 RID: 21905
		public static string NO = string.Empty;

		// Token: 0x04005592 RID: 21906
		public static string EXIT = string.Empty;

		// Token: 0x04005593 RID: 21907
		public static string CHAT = string.Empty;

		// Token: 0x04005594 RID: 21908
		public static string REVENGE = string.Empty;

		// Token: 0x04005595 RID: 21909
		public static string OK = string.Empty;

		// Token: 0x04005596 RID: 21910
		public static string retry = string.Empty;

		// Token: 0x04005597 RID: 21911
		public static string uncheck = string.Empty;

		// Token: 0x04005598 RID: 21912
		public static string remember = string.Empty;

		// Token: 0x04005599 RID: 21913
		public static string ACCEPT = string.Empty;

		// Token: 0x0400559A RID: 21914
		public static string CANCEL = string.Empty;

		// Token: 0x0400559B RID: 21915
		public static string SELECT = string.Empty;

		// Token: 0x0400559C RID: 21916
		public static string enter = string.Empty;

		// Token: 0x0400559D RID: 21917
		public static string open_link = string.Empty;

		// Token: 0x0400559E RID: 21918
		public static string DOYOUWANTEXIT = string.Empty;

		// Token: 0x0400559F RID: 21919
		public static string NEWCHAR = string.Empty;

		// Token: 0x040055A0 RID: 21920
		public static string BACK = string.Empty;

		// Token: 0x040055A1 RID: 21921
		public static string LOCKED = string.Empty;

		// Token: 0x040055A2 RID: 21922
		public static string KILL = string.Empty;

		// Token: 0x040055A3 RID: 21923
		public static string KILLBOSS = string.Empty;

		// Token: 0x040055A4 RID: 21924
		public static string NOLOCK = string.Empty;

		// Token: 0x040055A5 RID: 21925
		public static string XU = string.Empty;

		// Token: 0x040055A6 RID: 21926
		public static string LUONG = string.Empty;

		// Token: 0x040055A7 RID: 21927
		public static string RUBY = string.Empty;

		// Token: 0x040055A8 RID: 21928
		public static string PK_NOW = string.Empty;

		// Token: 0x040055A9 RID: 21929
		public static string CUU_SAT = string.Empty;

		// Token: 0x040055AA RID: 21930
		public static string NOT_ENOUGH_MP = string.Empty;

		// Token: 0x040055AB RID: 21931
		public static string you_receive = string.Empty;

		// Token: 0x040055AC RID: 21932
		public static string MONTH = string.Empty;

		// Token: 0x040055AD RID: 21933
		public static string WEEK = string.Empty;

		// Token: 0x040055AE RID: 21934
		public static string DAY = string.Empty;

		// Token: 0x040055AF RID: 21935
		public static string HOUR = string.Empty;

		// Token: 0x040055B0 RID: 21936
		public static string SECOND = string.Empty;

		// Token: 0x040055B1 RID: 21937
		public static string MINUTE = string.Empty;

		// Token: 0x040055B2 RID: 21938
		public static string LEARN_SKILL = string.Empty;

		// Token: 0x040055B3 RID: 21939
		public static string rank = string.Empty;

		// Token: 0x040055B4 RID: 21940
		public static string active_point = string.Empty;

		// Token: 0x040055B5 RID: 21941
		public static string friend = string.Empty;

		// Token: 0x040055B6 RID: 21942
		public static string enemy = string.Empty;

		// Token: 0x040055B7 RID: 21943
		public static string no_friend = string.Empty;

		// Token: 0x040055B8 RID: 21944
		public static string chat_world = string.Empty;

		// Token: 0x040055B9 RID: 21945
		public static string change_flag = string.Empty;

		// Token: 0x040055BA RID: 21946
		public static string gameInfo = string.Empty;

		// Token: 0x040055BB RID: 21947
		public static string quayso = string.Empty;

		// Token: 0x040055BC RID: 21948
		public static string option = string.Empty;

		// Token: 0x040055BD RID: 21949
		public static string high = string.Empty;

		// Token: 0x040055BE RID: 21950
		public static string medium = string.Empty;

		// Token: 0x040055BF RID: 21951
		public static string low = string.Empty;

		// Token: 0x040055C0 RID: 21952
		public static string increase_vga = string.Empty;

		// Token: 0x040055C1 RID: 21953
		public static string decrease_vga = string.Empty;

		// Token: 0x040055C2 RID: 21954
		public static string serverchat_off = string.Empty;

		// Token: 0x040055C3 RID: 21955
		public static string serverchat_on = string.Empty;

		// Token: 0x040055C4 RID: 21956
		public static string x2Screen = string.Empty;

		// Token: 0x040055C5 RID: 21957
		public static string x1Screen = string.Empty;

		// Token: 0x040055C6 RID: 21958
		public static string changeSizeScreen = string.Empty;

		// Token: 0x040055C7 RID: 21959
		public static string aura_off = string.Empty;

		// Token: 0x040055C8 RID: 21960
		public static string aura_on = string.Empty;

		// Token: 0x040055C9 RID: 21961
		public static string aura_off_2 = string.Empty;

		// Token: 0x040055CA RID: 21962
		public static string aura_on_2 = string.Empty;

		// Token: 0x040055CB RID: 21963
		public static string hat_off = string.Empty;

		// Token: 0x040055CC RID: 21964
		public static string hat_on = string.Empty;

		// Token: 0x040055CD RID: 21965
		public static string chest = string.Empty;

		// Token: 0x040055CE RID: 21966
		public static string[] chestt = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040055CF RID: 21967
		public static string[] inventory = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040055D0 RID: 21968
		public static string[] combine = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040055D1 RID: 21969
		public static string[] mapp = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040055D2 RID: 21970
		public static string[] item_give = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040055D3 RID: 21971
		public static string[] item_receive = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040055D4 RID: 21972
		public static string[] zonee = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040055D5 RID: 21973
		public static string zone = string.Empty;

		// Token: 0x040055D6 RID: 21974
		public static string map = string.Empty;

		// Token: 0x040055D7 RID: 21975
		public static string item_receive2 = string.Empty;

		// Token: 0x040055D8 RID: 21976
		public static string item = string.Empty;

		// Token: 0x040055D9 RID: 21977
		public static string give_upper = string.Empty;

		// Token: 0x040055DA RID: 21978
		public static string receive_upper = string.Empty;

		// Token: 0x040055DB RID: 21979
		public static string receive_all = string.Empty;

		// Token: 0x040055DC RID: 21980
		public static string no_map = string.Empty;

		// Token: 0x040055DD RID: 21981
		public static string go_to_quest = string.Empty;

		// Token: 0x040055DE RID: 21982
		public static string from_earth = string.Empty;

		// Token: 0x040055DF RID: 21983
		public static string from_namec = string.Empty;

		// Token: 0x040055E0 RID: 21984
		public static string from_sayda = string.Empty;

		// Token: 0x040055E1 RID: 21985
		public static string expire = string.Empty;

		// Token: 0x040055E2 RID: 21986
		public static string pow_request = string.Empty;

		// Token: 0x040055E3 RID: 21987
		public static string your_pow = string.Empty;

		// Token: 0x040055E4 RID: 21988
		public static string used = string.Empty;

		// Token: 0x040055E5 RID: 21989
		public static string place = string.Empty;

		// Token: 0x040055E6 RID: 21990
		public static string FOREVER = string.Empty;

		// Token: 0x040055E7 RID: 21991
		public static string NOUPGRADE = string.Empty;

		// Token: 0x040055E8 RID: 21992
		public static string NOTUPGRADE = string.Empty;

		// Token: 0x040055E9 RID: 21993
		public static string UPGRADE = string.Empty;

		// Token: 0x040055EA RID: 21994
		public static string UPGRADING = string.Empty;

		// Token: 0x040055EB RID: 21995
		public static string make_shortcut = string.Empty;

		// Token: 0x040055EC RID: 21996
		public static string into_place = string.Empty;

		// Token: 0x040055ED RID: 21997
		public static string move_to_chest = string.Empty;

		// Token: 0x040055EE RID: 21998
		public static string move_to_chest2 = string.Empty;

		// Token: 0x040055EF RID: 21999
		public static string press_chat_querty = string.Empty;

		// Token: 0x040055F0 RID: 22000
		public static string press_chat = string.Empty;

		// Token: 0x040055F1 RID: 22001
		public static string saying = string.Empty;

		// Token: 0x040055F2 RID: 22002
		public static string miss = string.Empty;

		// Token: 0x040055F3 RID: 22003
		public static string donate = string.Empty;

		// Token: 0x040055F4 RID: 22004
		public static string receive = string.Empty;

		// Token: 0x040055F5 RID: 22005
		public static string press_twice = string.Empty;

		// Token: 0x040055F6 RID: 22006
		public static string can_harvest = string.Empty;

		// Token: 0x040055F7 RID: 22007
		public static string do_accept_qwerty = string.Empty;

		// Token: 0x040055F8 RID: 22008
		public static string do_accept = string.Empty;

		// Token: 0x040055F9 RID: 22009
		public static string plsRestartGame = string.Empty;

		// Token: 0x040055FA RID: 22010
		public static string is_online = string.Empty;

		// Token: 0x040055FB RID: 22011
		public static string is_offline = string.Empty;

		// Token: 0x040055FC RID: 22012
		public static string make_friend = string.Empty;

		// Token: 0x040055FD RID: 22013
		public static string chat_player = string.Empty;

		// Token: 0x040055FE RID: 22014
		public static string chat_with = string.Empty;

		// Token: 0x040055FF RID: 22015
		public static string clan_capsuledonate = string.Empty;

		// Token: 0x04005600 RID: 22016
		public static string clan_capsuleself = string.Empty;

		// Token: 0x04005601 RID: 22017
		public static string clan_point = string.Empty;

		// Token: 0x04005602 RID: 22018
		public static string give_pea = string.Empty;

		// Token: 0x04005603 RID: 22019
		public static string receive_pea = string.Empty;

		// Token: 0x04005604 RID: 22020
		public static string request_pea = string.Empty;

		// Token: 0x04005605 RID: 22021
		public static string time = string.Empty;

		// Token: 0x04005606 RID: 22022
		public static string received = string.Empty;

		// Token: 0x04005607 RID: 22023
		public static string power = string.Empty;

		// Token: 0x04005608 RID: 22024
		public static string join_date = string.Empty;

		// Token: 0x04005609 RID: 22025
		public static string clan_leader = string.Empty;

		// Token: 0x0400560A RID: 22026
		public static string clan_coleader = string.Empty;

		// Token: 0x0400560B RID: 22027
		public static string power_point = string.Empty;

		// Token: 0x0400560C RID: 22028
		public static string member = string.Empty;

		// Token: 0x0400560D RID: 22029
		public static string[] memberr = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x0400560E RID: 22030
		public static string[] chatClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x0400560F RID: 22031
		public static string[] leaveClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04005610 RID: 22032
		public static string[] createClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04005611 RID: 22033
		public static string[] findClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04005612 RID: 22034
		public static string[] khau_hieuu = new string[]
		{
			string.Empty
		};

		// Token: 0x04005613 RID: 22035
		public static string[] bieu_tuongg = new string[]
		{
			string.Empty
		};

		// Token: 0x04005614 RID: 22036
		public static string[] request_pea2 = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04005615 RID: 22037
		public static string level = string.Empty;

		// Token: 0x04005616 RID: 22038
		public static string clan_birthday = string.Empty;

		// Token: 0x04005617 RID: 22039
		public static string clan_list = string.Empty;

		// Token: 0x04005618 RID: 22040
		public static string create = string.Empty;

		// Token: 0x04005619 RID: 22041
		public static string find = string.Empty;

		// Token: 0x0400561A RID: 22042
		public static string leave = string.Empty;

		// Token: 0x0400561B RID: 22043
		public static string not_join_clan = string.Empty;

		// Token: 0x0400561C RID: 22044
		public static string[] clanEmpty = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400561D RID: 22045
		public static string input_clan_name = string.Empty;

		// Token: 0x0400561E RID: 22046
		public static string clan_name = string.Empty;

		// Token: 0x0400561F RID: 22047
		public static string chat_clan = string.Empty;

		// Token: 0x04005620 RID: 22048
		public static string input_clan_name_to_create = string.Empty;

		// Token: 0x04005621 RID: 22049
		public static string input_clan_slogan = string.Empty;

		// Token: 0x04005622 RID: 22050
		public static string do_u_want_join_clan = string.Empty;

		// Token: 0x04005623 RID: 22051
		public static string select_clan_icon = string.Empty;

		// Token: 0x04005624 RID: 22052
		public static string request_join_clan = string.Empty;

		// Token: 0x04005625 RID: 22053
		public static string view_clan_member = string.Empty;

		// Token: 0x04005626 RID: 22054
		public static string create_clan_co_leader = string.Empty;

		// Token: 0x04005627 RID: 22055
		public static string create_clan_leader = string.Empty;

		// Token: 0x04005628 RID: 22056
		public static string disable_clan_mastership = string.Empty;

		// Token: 0x04005629 RID: 22057
		public static string kick_clan_mem = string.Empty;

		// Token: 0x0400562A RID: 22058
		public static string clan_name_blank = string.Empty;

		// Token: 0x0400562B RID: 22059
		public static string clan_slogan_blank = string.Empty;

		// Token: 0x0400562C RID: 22060
		public static string cannot_find_clan = string.Empty;

		// Token: 0x0400562D RID: 22061
		public static string ago = string.Empty;

		// Token: 0x0400562E RID: 22062
		public static string findingClan = string.Empty;

		// Token: 0x0400562F RID: 22063
		public static string trade = string.Empty;

		// Token: 0x04005630 RID: 22064
		public static string not_lock_trade = string.Empty;

		// Token: 0x04005631 RID: 22065
		public static string not_lock_trade_upper = string.Empty;

		// Token: 0x04005632 RID: 22066
		public static string locked_trade = string.Empty;

		// Token: 0x04005633 RID: 22067
		public static string locked_trade_upper = string.Empty;

		// Token: 0x04005634 RID: 22068
		public static string lock_trade = string.Empty;

		// Token: 0x04005635 RID: 22069
		public static string wait_opp_lock_trade = string.Empty;

		// Token: 0x04005636 RID: 22070
		public static string press_done = string.Empty;

		// Token: 0x04005637 RID: 22071
		public static string THROW = string.Empty;

		// Token: 0x04005638 RID: 22072
		public static string SPLIT = string.Empty;

		// Token: 0x04005639 RID: 22073
		public static string done = string.Empty;

		// Token: 0x0400563A RID: 22074
		public static string opponent = string.Empty;

		// Token: 0x0400563B RID: 22075
		public static string you = string.Empty;

		// Token: 0x0400563C RID: 22076
		public static string mlock = string.Empty;

		// Token: 0x0400563D RID: 22077
		public static string money_trade = string.Empty;

		// Token: 0x0400563E RID: 22078
		public static string GETOUT = string.Empty;

		// Token: 0x0400563F RID: 22079
		public static string MOVEOUT = string.Empty;

		// Token: 0x04005640 RID: 22080
		public static string MOVEFORPET = string.Empty;

		// Token: 0x04005641 RID: 22081
		public static string GETOUTMONEY = string.Empty;

		// Token: 0x04005642 RID: 22082
		public static string GETINMONEY = string.Empty;

		// Token: 0x04005643 RID: 22083
		public static string SENDMONEY = string.Empty;

		// Token: 0x04005644 RID: 22084
		public static string GETIN = string.Empty;

		// Token: 0x04005645 RID: 22085
		public static string SALE = string.Empty;

		// Token: 0x04005646 RID: 22086
		public static string SALES = string.Empty;

		// Token: 0x04005647 RID: 22087
		public static string SALEALL = string.Empty;

		// Token: 0x04005648 RID: 22088
		public static string BUY = string.Empty;

		// Token: 0x04005649 RID: 22089
		public static string BUYS = string.Empty;

		// Token: 0x0400564A RID: 22090
		public static string input_money_to_trade = string.Empty;

		// Token: 0x0400564B RID: 22091
		public static string input_money = string.Empty;

		// Token: 0x0400564C RID: 22092
		public static string input_money_wrong = string.Empty;

		// Token: 0x0400564D RID: 22093
		public static string not_enough_money = string.Empty;

		// Token: 0x0400564E RID: 22094
		public static string input_quantity_to_trade = string.Empty;

		// Token: 0x0400564F RID: 22095
		public static string input_quantity = string.Empty;

		// Token: 0x04005650 RID: 22096
		public static string input_quantity_wrong = string.Empty;

		// Token: 0x04005651 RID: 22097
		public static string already_has_item = string.Empty;

		// Token: 0x04005652 RID: 22098
		public static string unlock_item_to_trade = string.Empty;

		// Token: 0x04005653 RID: 22099
		public static string root = string.Empty;

		// Token: 0x04005654 RID: 22100
		public static string need = string.Empty;

		// Token: 0x04005655 RID: 22101
		public static string need_upper = string.Empty;

		// Token: 0x04005656 RID: 22102
		public static string free = string.Empty;

		// Token: 0x04005657 RID: 22103
		public static string free1 = string.Empty;

		// Token: 0x04005658 RID: 22104
		public static string free2 = string.Empty;

		// Token: 0x04005659 RID: 22105
		public static string select_item = string.Empty;

		// Token: 0x0400565A RID: 22106
		public static string random = string.Empty;

		// Token: 0x0400565B RID: 22107
		public static string say_hello = string.Empty;

		// Token: 0x0400565C RID: 22108
		public static string say_wat_do_u_want_to_buy = string.Empty;

		// Token: 0x0400565D RID: 22109
		public static string say_wat_do_u_want_to_buy2 = string.Empty;

		// Token: 0x0400565E RID: 22110
		public static string do_u_sure_to_trade = string.Empty;

		// Token: 0x0400565F RID: 22111
		public static string learn_with = string.Empty;

		// Token: 0x04005660 RID: 22112
		public static string buy_with = string.Empty;

		// Token: 0x04005661 RID: 22113
		public static string can_not_do_when_die = string.Empty;

		// Token: 0x04005662 RID: 22114
		public static string use_for_combine = string.Empty;

		// Token: 0x04005663 RID: 22115
		public static string use_for_trade = string.Empty;

		// Token: 0x04005664 RID: 22116
		public static string not_enough_luong_world_channel = string.Empty;

		// Token: 0x04005665 RID: 22117
		public static string world_channel_5_luong = string.Empty;

		// Token: 0x04005666 RID: 22118
		public static string want_to_trade = string.Empty;

		// Token: 0x04005667 RID: 22119
		public static string hasJustUpgrade1 = string.Empty;

		// Token: 0x04005668 RID: 22120
		public static string hasJustUpgrade2 = string.Empty;

		// Token: 0x04005669 RID: 22121
		public static string potential_to_learn = string.Empty;

		// Token: 0x0400566A RID: 22122
		public static string potential_point = string.Empty;

		// Token: 0x0400566B RID: 22123
		public static string achievement_point = string.Empty;

		// Token: 0x0400566C RID: 22124
		public static string increase = string.Empty;

		// Token: 0x0400566D RID: 22125
		public static string increase_upper = string.Empty;

		// Token: 0x0400566E RID: 22126
		public static string not_enough_potential_point1 = string.Empty;

		// Token: 0x0400566F RID: 22127
		public static string not_enough_potential_point2 = string.Empty;

		// Token: 0x04005670 RID: 22128
		public static string use_potential_point_for1 = string.Empty;

		// Token: 0x04005671 RID: 22129
		public static string use_potential_point_for2 = string.Empty;

		// Token: 0x04005672 RID: 22130
		public static string for_HP = string.Empty;

		// Token: 0x04005673 RID: 22131
		public static string for_KI = string.Empty;

		// Token: 0x04005674 RID: 22132
		public static string for_hit_point = string.Empty;

		// Token: 0x04005675 RID: 22133
		public static string for_armor = string.Empty;

		// Token: 0x04005676 RID: 22134
		public static string for_crit = string.Empty;

		// Token: 0x04005677 RID: 22135
		public static string can_buy_from_Uron1 = string.Empty;

		// Token: 0x04005678 RID: 22136
		public static string can_buy_from_Uron2 = string.Empty;

		// Token: 0x04005679 RID: 22137
		public static string can_buy_from_Uron3 = string.Empty;

		// Token: 0x0400567A RID: 22138
		public static string HP = string.Empty;

		// Token: 0x0400567B RID: 22139
		public static string KI = string.Empty;

		// Token: 0x0400567C RID: 22140
		public static string hit_point = string.Empty;

		// Token: 0x0400567D RID: 22141
		public static string armor = string.Empty;

		// Token: 0x0400567E RID: 22142
		public static string vitality = string.Empty;

		// Token: 0x0400567F RID: 22143
		public static string critical = string.Empty;

		// Token: 0x04005680 RID: 22144
		public static string cap_do = string.Empty;

		// Token: 0x04005681 RID: 22145
		public static string KI_consume = string.Empty;

		// Token: 0x04005682 RID: 22146
		public static string cooldown = string.Empty;

		// Token: 0x04005683 RID: 22147
		public static string milisecond = string.Empty;

		// Token: 0x04005684 RID: 22148
		public static string max_level_reach = string.Empty;

		// Token: 0x04005685 RID: 22149
		public static string next_level_require = string.Empty;

		// Token: 0x04005686 RID: 22150
		public static string potential = string.Empty;

		// Token: 0x04005687 RID: 22151
		public static string potential2 = string.Empty;

		// Token: 0x04005688 RID: 22152
		public static string not_learn = string.Empty;

		// Token: 0x04005689 RID: 22153
		public static string learn_require = string.Empty;

		// Token: 0x0400568A RID: 22154
		public static string learn = string.Empty;

		// Token: 0x0400568B RID: 22155
		public static string to_gain_20hp = string.Empty;

		// Token: 0x0400568C RID: 22156
		public static string to_gain_20mp = string.Empty;

		// Token: 0x0400568D RID: 22157
		public static string to_gain_1pow = string.Empty;

		// Token: 0x0400568E RID: 22158
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

		// Token: 0x0400568F RID: 22159
		public static string hp_ki_full = string.Empty;

		// Token: 0x04005690 RID: 22160
		public static string quest_place = string.Empty;

		// Token: 0x04005691 RID: 22161
		public static string no_mission = string.Empty;

		// Token: 0x04005692 RID: 22162
		public static string reward_mission = string.Empty;

		// Token: 0x04005693 RID: 22163
		public static string achievement_mission = string.Empty;

		// Token: 0x04005694 RID: 22164
		public static string trangbi = string.Empty;

		// Token: 0x04005695 RID: 22165
		public static string wat_do_u_want = string.Empty;

		// Token: 0x04005696 RID: 22166
		public static string off = string.Empty;

		// Token: 0x04005697 RID: 22167
		public static string on = string.Empty;

		// Token: 0x04005698 RID: 22168
		public static string select_map = string.Empty;

		// Token: 0x04005699 RID: 22169
		public static string offPlease = string.Empty;

		// Token: 0x0400569A RID: 22170
		public static string onPlease = string.Empty;

		// Token: 0x0400569B RID: 22171
		public static sbyte language;

		// Token: 0x0400569C RID: 22172
		public static string choigame;

		// Token: 0x0400569D RID: 22173
		public static string no_enemy = string.Empty;

		// Token: 0x0400569E RID: 22174
		public static string kigui;

		// Token: 0x0400569F RID: 22175
		public static string kiguiXu;

		// Token: 0x040056A0 RID: 22176
		public static string kiguiLuong;

		// Token: 0x040056A1 RID: 22177
		public static string kiguiXuchat;

		// Token: 0x040056A2 RID: 22178
		public static string kiguiLuongchat;

		// Token: 0x040056A3 RID: 22179
		public static string huykigui;

		// Token: 0x040056A4 RID: 22180
		public static string nhantien;

		// Token: 0x040056A5 RID: 22181
		public static string dangban;

		// Token: 0x040056A6 RID: 22182
		public static string daban;

		// Token: 0x040056A7 RID: 22183
		public static string num;

		// Token: 0x040056A8 RID: 22184
		public static string upTop;

		// Token: 0x040056A9 RID: 22185
		public static string page;

		// Token: 0x040056AA RID: 22186
		public static string getDown;

		// Token: 0x040056AB RID: 22187
		public static string getUp;

		// Token: 0x040056AC RID: 22188
		public static string notYetSell;

		// Token: 0x040056AD RID: 22189
		public static string charger;

		// Token: 0x040056AE RID: 22190
		public static string finishBomong;

		// Token: 0x040056AF RID: 22191
		public static string note;

		// Token: 0x040056B0 RID: 22192
		public static string regNote;

		// Token: 0x040056B1 RID: 22193
		public static string remain;

		// Token: 0x040056B2 RID: 22194
		public static string faster;

		// Token: 0x040056B3 RID: 22195
		public static string fasterQuestion;

		// Token: 0x040056B4 RID: 22196
		public static string chuacotaikhoan;

		// Token: 0x040056B5 RID: 22197
		public static string taidulieudechoi;

		// Token: 0x040056B6 RID: 22198
		public static string huy;

		// Token: 0x040056B7 RID: 22199
		public static string taidulieu;

		// Token: 0x040056B8 RID: 22200
		public static string xoadulieu;

		// Token: 0x040056B9 RID: 22201
		public static string deletaDataNote;

		// Token: 0x040056BA RID: 22202
		public static string playNew;

		// Token: 0x040056BB RID: 22203
		public static string playAcc;

		// Token: 0x040056BC RID: 22204
		public static string vuilongnhapduthongtin;

		// Token: 0x040056BD RID: 22205
		public static string not_register_yet = string.Empty;

		// Token: 0x040056BE RID: 22206
		public static string nhanngoc;

		// Token: 0x040056BF RID: 22207
		public static string fusion;

		// Token: 0x040056C0 RID: 22208
		public static string sure_fusion;

		// Token: 0x040056C1 RID: 22209
		public static string fusionForever;

		// Token: 0x040056C2 RID: 22210
		public static string xinchucmung;

		// Token: 0x040056C3 RID: 22211
		public static string den;

		// Token: 0x040056C4 RID: 22212
		public static string nhatvatpham;
	}
}
