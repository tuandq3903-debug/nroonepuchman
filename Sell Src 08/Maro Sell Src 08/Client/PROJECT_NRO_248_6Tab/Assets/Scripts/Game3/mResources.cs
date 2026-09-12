using System;

namespace Game3
{
	// Token: 0x02000300 RID: 768
	public class mResources
	{
		// Token: 0x06002213 RID: 8723 RVA: 0x00217C56 File Offset: 0x00215E56
		public static void loadLanguague(sbyte newLanguage)
		{
			mResources.language = newLanguage;
			T1.load();
			GameCanvas.isLoadRes = true;
		}

		// Token: 0x04004284 RID: 17028
		public static string confirmChangeServer = string.Empty;

		// Token: 0x04004285 RID: 17029
		public static string chooseDefaultsv = string.Empty;

		// Token: 0x04004286 RID: 17030
		public static string winLose = string.Empty;

		// Token: 0x04004287 RID: 17031
		public static string learnSkill = string.Empty;

		// Token: 0x04004288 RID: 17032
		public static string updSkill = string.Empty;

		// Token: 0x04004289 RID: 17033
		public static string proficiency = string.Empty;

		// Token: 0x0400428A RID: 17034
		public static string delacc = string.Empty;

		// Token: 0x0400428B RID: 17035
		public static string notiINAPP = string.Empty;

		// Token: 0x0400428C RID: 17036
		public static string notiRuby = string.Empty;

		// Token: 0x0400428D RID: 17037
		public static string equip = string.Empty;

		// Token: 0x0400428E RID: 17038
		public static string unlock = string.Empty;

		// Token: 0x0400428F RID: 17039
		public static string radaCard = string.Empty;

		// Token: 0x04004290 RID: 17040
		public static string not_enough_money_1 = string.Empty;

		// Token: 0x04004291 RID: 17041
		public static string napngoc = string.Empty;

		// Token: 0x04004292 RID: 17042
		public static string functionMaintain1 = string.Empty;

		// Token: 0x04004293 RID: 17043
		public static string tang;

		// Token: 0x04004294 RID: 17044
		public static string kquaVongQuay;

		// Token: 0x04004295 RID: 17045
		public static string useGem;

		// Token: 0x04004296 RID: 17046
		public static string autoFunction;

		// Token: 0x04004297 RID: 17047
		public static string choitiep;

		// Token: 0x04004298 RID: 17048
		public static string attack;

		// Token: 0x04004299 RID: 17049
		public static string defend;

		// Token: 0x0400429A RID: 17050
		public static string follow;

		// Token: 0x0400429B RID: 17051
		public static string status;

		// Token: 0x0400429C RID: 17052
		public static string gohome;

		// Token: 0x0400429D RID: 17053
		public static string pet;

		// Token: 0x0400429E RID: 17054
		public static string maychutathoacmatsong;

		// Token: 0x0400429F RID: 17055
		public static string cauhinhthap;

		// Token: 0x040042A0 RID: 17056
		public static string cauhinhcao;

		// Token: 0x040042A1 RID: 17057
		public static string combineSpell;

		// Token: 0x040042A2 RID: 17058
		public static string combineFail;

		// Token: 0x040042A3 RID: 17059
		public static string combineSuccess;

		// Token: 0x040042A4 RID: 17060
		public static string turnOnAnalog;

		// Token: 0x040042A5 RID: 17061
		public static string turnOffAnalog;

		// Token: 0x040042A6 RID: 17062
		public static string analog;

		// Token: 0x040042A7 RID: 17063
		public static string inventory_Pass;

		// Token: 0x040042A8 RID: 17064
		public static string input_Inventory_Pass;

		// Token: 0x040042A9 RID: 17065
		public static string input_Inventory_Pass_wrong = string.Empty;

		// Token: 0x040042AA RID: 17066
		public static string REGISTOPROTECT = string.Empty;

		// Token: 0x040042AB RID: 17067
		public static string turnOnSound = string.Empty;

		// Token: 0x040042AC RID: 17068
		public static string turnOffSound = string.Empty;

		// Token: 0x040042AD RID: 17069
		public static string REGISTERING = string.Empty;

		// Token: 0x040042AE RID: 17070
		public static string SENDINGMSG = string.Empty;

		// Token: 0x040042AF RID: 17071
		public static string SENTMSG = string.Empty;

		// Token: 0x040042B0 RID: 17072
		public static string NOSENDMSG = string.Empty;

		// Token: 0x040042B1 RID: 17073
		public static string sendMsgSuccess = string.Empty;

		// Token: 0x040042B2 RID: 17074
		public static string cannotSendMsg = string.Empty;

		// Token: 0x040042B3 RID: 17075
		public static string sendGuessMsgSuccess = string.Empty;

		// Token: 0x040042B4 RID: 17076
		public static string sendMsgFail = string.Empty;

		// Token: 0x040042B5 RID: 17077
		public static string ALERT_PRIVATE_PASS_1 = string.Empty;

		// Token: 0x040042B6 RID: 17078
		public static string ALERT_PRIVATE_PASS_2 = string.Empty;

		// Token: 0x040042B7 RID: 17079
		public static string INPUT_PRIVATE_PASS = string.Empty;

		// Token: 0x040042B8 RID: 17080
		public static string change_account = string.Empty;

		// Token: 0x040042B9 RID: 17081
		public static string alreadyHadAccount1 = string.Empty;

		// Token: 0x040042BA RID: 17082
		public static string alreadyHadAccount2 = string.Empty;

		// Token: 0x040042BB RID: 17083
		public static string userBlank = string.Empty;

		// Token: 0x040042BC RID: 17084
		public static string passwordBlank = string.Empty;

		// Token: 0x040042BD RID: 17085
		public static string accTooShort = string.Empty;

		// Token: 0x040042BE RID: 17086
		public static string phoneInvalid = string.Empty;

		// Token: 0x040042BF RID: 17087
		public static string emailInvalid = string.Empty;

		// Token: 0x040042C0 RID: 17088
		public static string registerNewAcc = string.Empty;

		// Token: 0x040042C1 RID: 17089
		public static string selectServer = string.Empty;

		// Token: 0x040042C2 RID: 17090
		public static string selectServer2 = string.Empty;

		// Token: 0x040042C3 RID: 17091
		public static string forgetPass = string.Empty;

		// Token: 0x040042C4 RID: 17092
		public static string password = string.Empty;

		// Token: 0x040042C5 RID: 17093
		public static string[] LOGINLABELS = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040042C6 RID: 17094
		public static string msg = string.Empty;

		// Token: 0x040042C7 RID: 17095
		public static string[] msgg = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x040042C8 RID: 17096
		public static string no_msg = string.Empty;

		// Token: 0x040042C9 RID: 17097
		public static string cancelAccountProtection = string.Empty;

		// Token: 0x040042CA RID: 17098
		public static string plsCheckAcc = string.Empty;

		// Token: 0x040042CB RID: 17099
		public static string phone = string.Empty;

		// Token: 0x040042CC RID: 17100
		public static string email = string.Empty;

		// Token: 0x040042CD RID: 17101
		public static string acc = string.Empty;

		// Token: 0x040042CE RID: 17102
		public static string pwd = string.Empty;

		// Token: 0x040042CF RID: 17103
		public static string goToWebForPassword = string.Empty;

		// Token: 0x040042D0 RID: 17104
		public static string dragon_ball = string.Empty;

		// Token: 0x040042D1 RID: 17105
		public static string character = string.Empty;

		// Token: 0x040042D2 RID: 17106
		public static string account = string.Empty;

		// Token: 0x040042D3 RID: 17107
		public static string account_server = string.Empty;

		// Token: 0x040042D4 RID: 17108
		public static string char_name_blank = string.Empty;

		// Token: 0x040042D5 RID: 17109
		public static string char_name_short = string.Empty;

		// Token: 0x040042D6 RID: 17110
		public static string char_name_long = string.Empty;

		// Token: 0x040042D7 RID: 17111
		public static string changeNameChar = string.Empty;

		// Token: 0x040042D8 RID: 17112
		public static string char_name = string.Empty;

		// Token: 0x040042D9 RID: 17113
		public static string login = string.Empty;

		// Token: 0x040042DA RID: 17114
		public static string login2 = string.Empty;

		// Token: 0x040042DB RID: 17115
		public static string register = string.Empty;

		// Token: 0x040042DC RID: 17116
		public static string WAIT = string.Empty;

		// Token: 0x040042DD RID: 17117
		public static string PLEASEWAIT = string.Empty;

		// Token: 0x040042DE RID: 17118
		public static string CONNECTING = string.Empty;

		// Token: 0x040042DF RID: 17119
		public static string LOGGING = string.Empty;

		// Token: 0x040042E0 RID: 17120
		public static string LOADING = string.Empty;

		// Token: 0x040042E1 RID: 17121
		public static string downloading_data = string.Empty;

		// Token: 0x040042E2 RID: 17122
		public static string select_server = string.Empty;

		// Token: 0x040042E3 RID: 17123
		public static string pls_restart_game_error = string.Empty;

		// Token: 0x040042E4 RID: 17124
		public static string pls_restart_game_error2 = string.Empty;

		// Token: 0x040042E5 RID: 17125
		public static string lost_connection = string.Empty;

		// Token: 0x040042E6 RID: 17126
		public static string check_3G = string.Empty;

		// Token: 0x040042E7 RID: 17127
		public static string UPDATE = string.Empty;

		// Token: 0x040042E8 RID: 17128
		public static string change_zone = string.Empty;

		// Token: 0x040042E9 RID: 17129
		public static string select_zone = string.Empty;

		// Token: 0x040042EA RID: 17130
		public static string website = string.Empty;

		// Token: 0x040042EB RID: 17131
		public static string server = string.Empty;

		// Token: 0x040042EC RID: 17132
		public static string planet = string.Empty;

		// Token: 0x040042ED RID: 17133
		public static string[] MENUME = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x040042EE RID: 17134
		public static string[] MENUGENDER = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x040042EF RID: 17135
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

		// Token: 0x040042F0 RID: 17136
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

		// Token: 0x040042F1 RID: 17137
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

		// Token: 0x040042F2 RID: 17138
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

		// Token: 0x040042F3 RID: 17139
		public static string[][] petMainTab2 = new string[][]
		{
			new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty
			}
		};

		// Token: 0x040042F4 RID: 17140
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

		// Token: 0x040042F5 RID: 17141
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

		// Token: 0x040042F6 RID: 17142
		public static string SKILL_FAIL = string.Empty;

		// Token: 0x040042F7 RID: 17143
		public static string HP_EMPTY = string.Empty;

		// Token: 0x040042F8 RID: 17144
		public static string ZONE_HERE = string.Empty;

		// Token: 0x040042F9 RID: 17145
		public static string[] DES_TASK = new string[]
		{
			" ",
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x040042FA RID: 17146
		public static string[] DIES = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x040042FB RID: 17147
		public static string[] SYNTHESIS = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x040042FC RID: 17148
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

		// Token: 0x040042FD RID: 17149
		public static string TASK_INPUT_CLASS = string.Empty;

		// Token: 0x040042FE RID: 17150
		public static string SERI_NUM = string.Empty;

		// Token: 0x040042FF RID: 17151
		public static string CARD_CODE = string.Empty;

		// Token: 0x04004300 RID: 17152
		public static string pay_card = string.Empty;

		// Token: 0x04004301 RID: 17153
		public static string pay_card2 = string.Empty;

		// Token: 0x04004302 RID: 17154
		public static string serial_blank = string.Empty;

		// Token: 0x04004303 RID: 17155
		public static string card_code_blank = string.Empty;

		// Token: 0x04004304 RID: 17156
		public static string billion = string.Empty;

		// Token: 0x04004305 RID: 17157
		public static string million = string.Empty;

		// Token: 0x04004306 RID: 17158
		public static string MENU = string.Empty;

		// Token: 0x04004307 RID: 17159
		public static string CLOSE = string.Empty;

		// Token: 0x04004308 RID: 17160
		public static string ON = string.Empty;

		// Token: 0x04004309 RID: 17161
		public static string OFF = string.Empty;

		// Token: 0x0400430A RID: 17162
		public static string ENABLE = string.Empty;

		// Token: 0x0400430B RID: 17163
		public static string DELETE = string.Empty;

		// Token: 0x0400430C RID: 17164
		public static string VIEW = string.Empty;

		// Token: 0x0400430D RID: 17165
		public static string CONTINUE = string.Empty;

		// Token: 0x0400430E RID: 17166
		public static string NEXTSTEP = string.Empty;

		// Token: 0x0400430F RID: 17167
		public static string USE = string.Empty;

		// Token: 0x04004310 RID: 17168
		public static string SORT = string.Empty;

		// Token: 0x04004311 RID: 17169
		public static string YES = string.Empty;

		// Token: 0x04004312 RID: 17170
		public static string NO = string.Empty;

		// Token: 0x04004313 RID: 17171
		public static string EXIT = string.Empty;

		// Token: 0x04004314 RID: 17172
		public static string CHAT = string.Empty;

		// Token: 0x04004315 RID: 17173
		public static string REVENGE = string.Empty;

		// Token: 0x04004316 RID: 17174
		public static string OK = string.Empty;

		// Token: 0x04004317 RID: 17175
		public static string retry = string.Empty;

		// Token: 0x04004318 RID: 17176
		public static string uncheck = string.Empty;

		// Token: 0x04004319 RID: 17177
		public static string remember = string.Empty;

		// Token: 0x0400431A RID: 17178
		public static string ACCEPT = string.Empty;

		// Token: 0x0400431B RID: 17179
		public static string CANCEL = string.Empty;

		// Token: 0x0400431C RID: 17180
		public static string SELECT = string.Empty;

		// Token: 0x0400431D RID: 17181
		public static string enter = string.Empty;

		// Token: 0x0400431E RID: 17182
		public static string open_link = string.Empty;

		// Token: 0x0400431F RID: 17183
		public static string DOYOUWANTEXIT = string.Empty;

		// Token: 0x04004320 RID: 17184
		public static string NEWCHAR = string.Empty;

		// Token: 0x04004321 RID: 17185
		public static string BACK = string.Empty;

		// Token: 0x04004322 RID: 17186
		public static string LOCKED = string.Empty;

		// Token: 0x04004323 RID: 17187
		public static string KILL = string.Empty;

		// Token: 0x04004324 RID: 17188
		public static string KILLBOSS = string.Empty;

		// Token: 0x04004325 RID: 17189
		public static string NOLOCK = string.Empty;

		// Token: 0x04004326 RID: 17190
		public static string XU = string.Empty;

		// Token: 0x04004327 RID: 17191
		public static string LUONG = string.Empty;

		// Token: 0x04004328 RID: 17192
		public static string RUBY = string.Empty;

		// Token: 0x04004329 RID: 17193
		public static string PK_NOW = string.Empty;

		// Token: 0x0400432A RID: 17194
		public static string CUU_SAT = string.Empty;

		// Token: 0x0400432B RID: 17195
		public static string NOT_ENOUGH_MP = string.Empty;

		// Token: 0x0400432C RID: 17196
		public static string you_receive = string.Empty;

		// Token: 0x0400432D RID: 17197
		public static string MONTH = string.Empty;

		// Token: 0x0400432E RID: 17198
		public static string WEEK = string.Empty;

		// Token: 0x0400432F RID: 17199
		public static string DAY = string.Empty;

		// Token: 0x04004330 RID: 17200
		public static string HOUR = string.Empty;

		// Token: 0x04004331 RID: 17201
		public static string SECOND = string.Empty;

		// Token: 0x04004332 RID: 17202
		public static string MINUTE = string.Empty;

		// Token: 0x04004333 RID: 17203
		public static string LEARN_SKILL = string.Empty;

		// Token: 0x04004334 RID: 17204
		public static string rank = string.Empty;

		// Token: 0x04004335 RID: 17205
		public static string active_point = string.Empty;

		// Token: 0x04004336 RID: 17206
		public static string friend = string.Empty;

		// Token: 0x04004337 RID: 17207
		public static string enemy = string.Empty;

		// Token: 0x04004338 RID: 17208
		public static string no_friend = string.Empty;

		// Token: 0x04004339 RID: 17209
		public static string chat_world = string.Empty;

		// Token: 0x0400433A RID: 17210
		public static string change_flag = string.Empty;

		// Token: 0x0400433B RID: 17211
		public static string gameInfo = string.Empty;

		// Token: 0x0400433C RID: 17212
		public static string quayso = string.Empty;

		// Token: 0x0400433D RID: 17213
		public static string option = string.Empty;

		// Token: 0x0400433E RID: 17214
		public static string high = string.Empty;

		// Token: 0x0400433F RID: 17215
		public static string medium = string.Empty;

		// Token: 0x04004340 RID: 17216
		public static string low = string.Empty;

		// Token: 0x04004341 RID: 17217
		public static string increase_vga = string.Empty;

		// Token: 0x04004342 RID: 17218
		public static string decrease_vga = string.Empty;

		// Token: 0x04004343 RID: 17219
		public static string serverchat_off = string.Empty;

		// Token: 0x04004344 RID: 17220
		public static string serverchat_on = string.Empty;

		// Token: 0x04004345 RID: 17221
		public static string x2Screen = string.Empty;

		// Token: 0x04004346 RID: 17222
		public static string x1Screen = string.Empty;

		// Token: 0x04004347 RID: 17223
		public static string changeSizeScreen = string.Empty;

		// Token: 0x04004348 RID: 17224
		public static string aura_off = string.Empty;

		// Token: 0x04004349 RID: 17225
		public static string aura_on = string.Empty;

		// Token: 0x0400434A RID: 17226
		public static string aura_off_2 = string.Empty;

		// Token: 0x0400434B RID: 17227
		public static string aura_on_2 = string.Empty;

		// Token: 0x0400434C RID: 17228
		public static string hat_off = string.Empty;

		// Token: 0x0400434D RID: 17229
		public static string hat_on = string.Empty;

		// Token: 0x0400434E RID: 17230
		public static string chest = string.Empty;

		// Token: 0x0400434F RID: 17231
		public static string[] chestt = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004350 RID: 17232
		public static string[] inventory = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004351 RID: 17233
		public static string[] combine = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004352 RID: 17234
		public static string[] mapp = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004353 RID: 17235
		public static string[] item_give = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004354 RID: 17236
		public static string[] item_receive = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004355 RID: 17237
		public static string[] zonee = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004356 RID: 17238
		public static string zone = string.Empty;

		// Token: 0x04004357 RID: 17239
		public static string map = string.Empty;

		// Token: 0x04004358 RID: 17240
		public static string item_receive2 = string.Empty;

		// Token: 0x04004359 RID: 17241
		public static string item = string.Empty;

		// Token: 0x0400435A RID: 17242
		public static string give_upper = string.Empty;

		// Token: 0x0400435B RID: 17243
		public static string receive_upper = string.Empty;

		// Token: 0x0400435C RID: 17244
		public static string receive_all = string.Empty;

		// Token: 0x0400435D RID: 17245
		public static string no_map = string.Empty;

		// Token: 0x0400435E RID: 17246
		public static string go_to_quest = string.Empty;

		// Token: 0x0400435F RID: 17247
		public static string from_earth = string.Empty;

		// Token: 0x04004360 RID: 17248
		public static string from_namec = string.Empty;

		// Token: 0x04004361 RID: 17249
		public static string from_sayda = string.Empty;

		// Token: 0x04004362 RID: 17250
		public static string expire = string.Empty;

		// Token: 0x04004363 RID: 17251
		public static string pow_request = string.Empty;

		// Token: 0x04004364 RID: 17252
		public static string your_pow = string.Empty;

		// Token: 0x04004365 RID: 17253
		public static string used = string.Empty;

		// Token: 0x04004366 RID: 17254
		public static string place = string.Empty;

		// Token: 0x04004367 RID: 17255
		public static string FOREVER = string.Empty;

		// Token: 0x04004368 RID: 17256
		public static string NOUPGRADE = string.Empty;

		// Token: 0x04004369 RID: 17257
		public static string NOTUPGRADE = string.Empty;

		// Token: 0x0400436A RID: 17258
		public static string UPGRADE = string.Empty;

		// Token: 0x0400436B RID: 17259
		public static string UPGRADING = string.Empty;

		// Token: 0x0400436C RID: 17260
		public static string make_shortcut = string.Empty;

		// Token: 0x0400436D RID: 17261
		public static string into_place = string.Empty;

		// Token: 0x0400436E RID: 17262
		public static string move_to_chest = string.Empty;

		// Token: 0x0400436F RID: 17263
		public static string move_to_chest2 = string.Empty;

		// Token: 0x04004370 RID: 17264
		public static string press_chat_querty = string.Empty;

		// Token: 0x04004371 RID: 17265
		public static string press_chat = string.Empty;

		// Token: 0x04004372 RID: 17266
		public static string saying = string.Empty;

		// Token: 0x04004373 RID: 17267
		public static string miss = string.Empty;

		// Token: 0x04004374 RID: 17268
		public static string donate = string.Empty;

		// Token: 0x04004375 RID: 17269
		public static string receive = string.Empty;

		// Token: 0x04004376 RID: 17270
		public static string press_twice = string.Empty;

		// Token: 0x04004377 RID: 17271
		public static string can_harvest = string.Empty;

		// Token: 0x04004378 RID: 17272
		public static string do_accept_qwerty = string.Empty;

		// Token: 0x04004379 RID: 17273
		public static string do_accept = string.Empty;

		// Token: 0x0400437A RID: 17274
		public static string plsRestartGame = string.Empty;

		// Token: 0x0400437B RID: 17275
		public static string is_online = string.Empty;

		// Token: 0x0400437C RID: 17276
		public static string is_offline = string.Empty;

		// Token: 0x0400437D RID: 17277
		public static string make_friend = string.Empty;

		// Token: 0x0400437E RID: 17278
		public static string chat_player = string.Empty;

		// Token: 0x0400437F RID: 17279
		public static string chat_with = string.Empty;

		// Token: 0x04004380 RID: 17280
		public static string clan_capsuledonate = string.Empty;

		// Token: 0x04004381 RID: 17281
		public static string clan_capsuleself = string.Empty;

		// Token: 0x04004382 RID: 17282
		public static string clan_point = string.Empty;

		// Token: 0x04004383 RID: 17283
		public static string give_pea = string.Empty;

		// Token: 0x04004384 RID: 17284
		public static string receive_pea = string.Empty;

		// Token: 0x04004385 RID: 17285
		public static string request_pea = string.Empty;

		// Token: 0x04004386 RID: 17286
		public static string time = string.Empty;

		// Token: 0x04004387 RID: 17287
		public static string received = string.Empty;

		// Token: 0x04004388 RID: 17288
		public static string power = string.Empty;

		// Token: 0x04004389 RID: 17289
		public static string join_date = string.Empty;

		// Token: 0x0400438A RID: 17290
		public static string clan_leader = string.Empty;

		// Token: 0x0400438B RID: 17291
		public static string clan_coleader = string.Empty;

		// Token: 0x0400438C RID: 17292
		public static string power_point = string.Empty;

		// Token: 0x0400438D RID: 17293
		public static string member = string.Empty;

		// Token: 0x0400438E RID: 17294
		public static string[] memberr = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x0400438F RID: 17295
		public static string[] chatClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004390 RID: 17296
		public static string[] leaveClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004391 RID: 17297
		public static string[] createClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004392 RID: 17298
		public static string[] findClan = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004393 RID: 17299
		public static string[] khau_hieuu = new string[]
		{
			string.Empty
		};

		// Token: 0x04004394 RID: 17300
		public static string[] bieu_tuongg = new string[]
		{
			string.Empty
		};

		// Token: 0x04004395 RID: 17301
		public static string[] request_pea2 = new string[]
		{
			string.Empty,
			string.Empty
		};

		// Token: 0x04004396 RID: 17302
		public static string level = string.Empty;

		// Token: 0x04004397 RID: 17303
		public static string clan_birthday = string.Empty;

		// Token: 0x04004398 RID: 17304
		public static string clan_list = string.Empty;

		// Token: 0x04004399 RID: 17305
		public static string create = string.Empty;

		// Token: 0x0400439A RID: 17306
		public static string find = string.Empty;

		// Token: 0x0400439B RID: 17307
		public static string leave = string.Empty;

		// Token: 0x0400439C RID: 17308
		public static string not_join_clan = string.Empty;

		// Token: 0x0400439D RID: 17309
		public static string[] clanEmpty = new string[]
		{
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x0400439E RID: 17310
		public static string input_clan_name = string.Empty;

		// Token: 0x0400439F RID: 17311
		public static string clan_name = string.Empty;

		// Token: 0x040043A0 RID: 17312
		public static string chat_clan = string.Empty;

		// Token: 0x040043A1 RID: 17313
		public static string input_clan_name_to_create = string.Empty;

		// Token: 0x040043A2 RID: 17314
		public static string input_clan_slogan = string.Empty;

		// Token: 0x040043A3 RID: 17315
		public static string do_u_want_join_clan = string.Empty;

		// Token: 0x040043A4 RID: 17316
		public static string select_clan_icon = string.Empty;

		// Token: 0x040043A5 RID: 17317
		public static string request_join_clan = string.Empty;

		// Token: 0x040043A6 RID: 17318
		public static string view_clan_member = string.Empty;

		// Token: 0x040043A7 RID: 17319
		public static string create_clan_co_leader = string.Empty;

		// Token: 0x040043A8 RID: 17320
		public static string create_clan_leader = string.Empty;

		// Token: 0x040043A9 RID: 17321
		public static string disable_clan_mastership = string.Empty;

		// Token: 0x040043AA RID: 17322
		public static string kick_clan_mem = string.Empty;

		// Token: 0x040043AB RID: 17323
		public static string clan_name_blank = string.Empty;

		// Token: 0x040043AC RID: 17324
		public static string clan_slogan_blank = string.Empty;

		// Token: 0x040043AD RID: 17325
		public static string cannot_find_clan = string.Empty;

		// Token: 0x040043AE RID: 17326
		public static string ago = string.Empty;

		// Token: 0x040043AF RID: 17327
		public static string findingClan = string.Empty;

		// Token: 0x040043B0 RID: 17328
		public static string trade = string.Empty;

		// Token: 0x040043B1 RID: 17329
		public static string not_lock_trade = string.Empty;

		// Token: 0x040043B2 RID: 17330
		public static string not_lock_trade_upper = string.Empty;

		// Token: 0x040043B3 RID: 17331
		public static string locked_trade = string.Empty;

		// Token: 0x040043B4 RID: 17332
		public static string locked_trade_upper = string.Empty;

		// Token: 0x040043B5 RID: 17333
		public static string lock_trade = string.Empty;

		// Token: 0x040043B6 RID: 17334
		public static string wait_opp_lock_trade = string.Empty;

		// Token: 0x040043B7 RID: 17335
		public static string press_done = string.Empty;

		// Token: 0x040043B8 RID: 17336
		public static string THROW = string.Empty;

		// Token: 0x040043B9 RID: 17337
		public static string SPLIT = string.Empty;

		// Token: 0x040043BA RID: 17338
		public static string done = string.Empty;

		// Token: 0x040043BB RID: 17339
		public static string opponent = string.Empty;

		// Token: 0x040043BC RID: 17340
		public static string you = string.Empty;

		// Token: 0x040043BD RID: 17341
		public static string mlock = string.Empty;

		// Token: 0x040043BE RID: 17342
		public static string money_trade = string.Empty;

		// Token: 0x040043BF RID: 17343
		public static string GETOUT = string.Empty;

		// Token: 0x040043C0 RID: 17344
		public static string MOVEOUT = string.Empty;

		// Token: 0x040043C1 RID: 17345
		public static string MOVEFORPET = string.Empty;

		// Token: 0x040043C2 RID: 17346
		public static string GETOUTMONEY = string.Empty;

		// Token: 0x040043C3 RID: 17347
		public static string GETINMONEY = string.Empty;

		// Token: 0x040043C4 RID: 17348
		public static string SENDMONEY = string.Empty;

		// Token: 0x040043C5 RID: 17349
		public static string GETIN = string.Empty;

		// Token: 0x040043C6 RID: 17350
		public static string SALE = string.Empty;

		// Token: 0x040043C7 RID: 17351
		public static string SALES = string.Empty;

		// Token: 0x040043C8 RID: 17352
		public static string SALEALL = string.Empty;

		// Token: 0x040043C9 RID: 17353
		public static string BUY = string.Empty;

		// Token: 0x040043CA RID: 17354
		public static string BUYS = string.Empty;

		// Token: 0x040043CB RID: 17355
		public static string input_money_to_trade = string.Empty;

		// Token: 0x040043CC RID: 17356
		public static string input_money = string.Empty;

		// Token: 0x040043CD RID: 17357
		public static string input_money_wrong = string.Empty;

		// Token: 0x040043CE RID: 17358
		public static string not_enough_money = string.Empty;

		// Token: 0x040043CF RID: 17359
		public static string input_quantity_to_trade = string.Empty;

		// Token: 0x040043D0 RID: 17360
		public static string input_quantity = string.Empty;

		// Token: 0x040043D1 RID: 17361
		public static string input_quantity_wrong = string.Empty;

		// Token: 0x040043D2 RID: 17362
		public static string already_has_item = string.Empty;

		// Token: 0x040043D3 RID: 17363
		public static string unlock_item_to_trade = string.Empty;

		// Token: 0x040043D4 RID: 17364
		public static string root = string.Empty;

		// Token: 0x040043D5 RID: 17365
		public static string need = string.Empty;

		// Token: 0x040043D6 RID: 17366
		public static string need_upper = string.Empty;

		// Token: 0x040043D7 RID: 17367
		public static string free = string.Empty;

		// Token: 0x040043D8 RID: 17368
		public static string free1 = string.Empty;

		// Token: 0x040043D9 RID: 17369
		public static string free2 = string.Empty;

		// Token: 0x040043DA RID: 17370
		public static string select_item = string.Empty;

		// Token: 0x040043DB RID: 17371
		public static string random = string.Empty;

		// Token: 0x040043DC RID: 17372
		public static string say_hello = string.Empty;

		// Token: 0x040043DD RID: 17373
		public static string say_wat_do_u_want_to_buy = string.Empty;

		// Token: 0x040043DE RID: 17374
		public static string say_wat_do_u_want_to_buy2 = string.Empty;

		// Token: 0x040043DF RID: 17375
		public static string do_u_sure_to_trade = string.Empty;

		// Token: 0x040043E0 RID: 17376
		public static string learn_with = string.Empty;

		// Token: 0x040043E1 RID: 17377
		public static string buy_with = string.Empty;

		// Token: 0x040043E2 RID: 17378
		public static string can_not_do_when_die = string.Empty;

		// Token: 0x040043E3 RID: 17379
		public static string use_for_combine = string.Empty;

		// Token: 0x040043E4 RID: 17380
		public static string use_for_trade = string.Empty;

		// Token: 0x040043E5 RID: 17381
		public static string not_enough_luong_world_channel = string.Empty;

		// Token: 0x040043E6 RID: 17382
		public static string world_channel_5_luong = string.Empty;

		// Token: 0x040043E7 RID: 17383
		public static string want_to_trade = string.Empty;

		// Token: 0x040043E8 RID: 17384
		public static string hasJustUpgrade1 = string.Empty;

		// Token: 0x040043E9 RID: 17385
		public static string hasJustUpgrade2 = string.Empty;

		// Token: 0x040043EA RID: 17386
		public static string potential_to_learn = string.Empty;

		// Token: 0x040043EB RID: 17387
		public static string potential_point = string.Empty;

		// Token: 0x040043EC RID: 17388
		public static string achievement_point = string.Empty;

		// Token: 0x040043ED RID: 17389
		public static string increase = string.Empty;

		// Token: 0x040043EE RID: 17390
		public static string increase_upper = string.Empty;

		// Token: 0x040043EF RID: 17391
		public static string not_enough_potential_point1 = string.Empty;

		// Token: 0x040043F0 RID: 17392
		public static string not_enough_potential_point2 = string.Empty;

		// Token: 0x040043F1 RID: 17393
		public static string use_potential_point_for1 = string.Empty;

		// Token: 0x040043F2 RID: 17394
		public static string use_potential_point_for2 = string.Empty;

		// Token: 0x040043F3 RID: 17395
		public static string for_HP = string.Empty;

		// Token: 0x040043F4 RID: 17396
		public static string for_KI = string.Empty;

		// Token: 0x040043F5 RID: 17397
		public static string for_hit_point = string.Empty;

		// Token: 0x040043F6 RID: 17398
		public static string for_armor = string.Empty;

		// Token: 0x040043F7 RID: 17399
		public static string for_crit = string.Empty;

		// Token: 0x040043F8 RID: 17400
		public static string can_buy_from_Uron1 = string.Empty;

		// Token: 0x040043F9 RID: 17401
		public static string can_buy_from_Uron2 = string.Empty;

		// Token: 0x040043FA RID: 17402
		public static string can_buy_from_Uron3 = string.Empty;

		// Token: 0x040043FB RID: 17403
		public static string HP = string.Empty;

		// Token: 0x040043FC RID: 17404
		public static string KI = string.Empty;

		// Token: 0x040043FD RID: 17405
		public static string hit_point = string.Empty;

		// Token: 0x040043FE RID: 17406
		public static string armor = string.Empty;

		// Token: 0x040043FF RID: 17407
		public static string vitality = string.Empty;

		// Token: 0x04004400 RID: 17408
		public static string critical = string.Empty;

		// Token: 0x04004401 RID: 17409
		public static string cap_do = string.Empty;

		// Token: 0x04004402 RID: 17410
		public static string KI_consume = string.Empty;

		// Token: 0x04004403 RID: 17411
		public static string cooldown = string.Empty;

		// Token: 0x04004404 RID: 17412
		public static string milisecond = string.Empty;

		// Token: 0x04004405 RID: 17413
		public static string max_level_reach = string.Empty;

		// Token: 0x04004406 RID: 17414
		public static string next_level_require = string.Empty;

		// Token: 0x04004407 RID: 17415
		public static string potential = string.Empty;

		// Token: 0x04004408 RID: 17416
		public static string potential2 = string.Empty;

		// Token: 0x04004409 RID: 17417
		public static string not_learn = string.Empty;

		// Token: 0x0400440A RID: 17418
		public static string learn_require = string.Empty;

		// Token: 0x0400440B RID: 17419
		public static string learn = string.Empty;

		// Token: 0x0400440C RID: 17420
		public static string to_gain_20hp = string.Empty;

		// Token: 0x0400440D RID: 17421
		public static string to_gain_20mp = string.Empty;

		// Token: 0x0400440E RID: 17422
		public static string to_gain_1pow = string.Empty;

		// Token: 0x0400440F RID: 17423
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

		// Token: 0x04004410 RID: 17424
		public static string hp_ki_full = string.Empty;

		// Token: 0x04004411 RID: 17425
		public static string quest_place = string.Empty;

		// Token: 0x04004412 RID: 17426
		public static string no_mission = string.Empty;

		// Token: 0x04004413 RID: 17427
		public static string reward_mission = string.Empty;

		// Token: 0x04004414 RID: 17428
		public static string achievement_mission = string.Empty;

		// Token: 0x04004415 RID: 17429
		public static string trangbi = string.Empty;

		// Token: 0x04004416 RID: 17430
		public static string wat_do_u_want = string.Empty;

		// Token: 0x04004417 RID: 17431
		public static string off = string.Empty;

		// Token: 0x04004418 RID: 17432
		public static string on = string.Empty;

		// Token: 0x04004419 RID: 17433
		public static string select_map = string.Empty;

		// Token: 0x0400441A RID: 17434
		public static string offPlease = string.Empty;

		// Token: 0x0400441B RID: 17435
		public static string onPlease = string.Empty;

		// Token: 0x0400441C RID: 17436
		public static sbyte language;

		// Token: 0x0400441D RID: 17437
		public static string choigame;

		// Token: 0x0400441E RID: 17438
		public static string no_enemy = string.Empty;

		// Token: 0x0400441F RID: 17439
		public static string kigui;

		// Token: 0x04004420 RID: 17440
		public static string kiguiXu;

		// Token: 0x04004421 RID: 17441
		public static string kiguiLuong;

		// Token: 0x04004422 RID: 17442
		public static string kiguiXuchat;

		// Token: 0x04004423 RID: 17443
		public static string kiguiLuongchat;

		// Token: 0x04004424 RID: 17444
		public static string huykigui;

		// Token: 0x04004425 RID: 17445
		public static string nhantien;

		// Token: 0x04004426 RID: 17446
		public static string dangban;

		// Token: 0x04004427 RID: 17447
		public static string daban;

		// Token: 0x04004428 RID: 17448
		public static string num;

		// Token: 0x04004429 RID: 17449
		public static string upTop;

		// Token: 0x0400442A RID: 17450
		public static string page;

		// Token: 0x0400442B RID: 17451
		public static string getDown;

		// Token: 0x0400442C RID: 17452
		public static string getUp;

		// Token: 0x0400442D RID: 17453
		public static string notYetSell;

		// Token: 0x0400442E RID: 17454
		public static string charger;

		// Token: 0x0400442F RID: 17455
		public static string finishBomong;

		// Token: 0x04004430 RID: 17456
		public static string note;

		// Token: 0x04004431 RID: 17457
		public static string regNote;

		// Token: 0x04004432 RID: 17458
		public static string remain;

		// Token: 0x04004433 RID: 17459
		public static string faster;

		// Token: 0x04004434 RID: 17460
		public static string fasterQuestion;

		// Token: 0x04004435 RID: 17461
		public static string chuacotaikhoan;

		// Token: 0x04004436 RID: 17462
		public static string taidulieudechoi;

		// Token: 0x04004437 RID: 17463
		public static string huy;

		// Token: 0x04004438 RID: 17464
		public static string taidulieu;

		// Token: 0x04004439 RID: 17465
		public static string xoadulieu;

		// Token: 0x0400443A RID: 17466
		public static string deletaDataNote;

		// Token: 0x0400443B RID: 17467
		public static string playNew;

		// Token: 0x0400443C RID: 17468
		public static string playAcc;

		// Token: 0x0400443D RID: 17469
		public static string vuilongnhapduthongtin;

		// Token: 0x0400443E RID: 17470
		public static string not_register_yet = string.Empty;

		// Token: 0x0400443F RID: 17471
		public static string nhanngoc;

		// Token: 0x04004440 RID: 17472
		public static string fusion;

		// Token: 0x04004441 RID: 17473
		public static string sure_fusion;

		// Token: 0x04004442 RID: 17474
		public static string fusionForever;

		// Token: 0x04004443 RID: 17475
		public static string xinchucmung;

		// Token: 0x04004444 RID: 17476
		public static string den;

		// Token: 0x04004445 RID: 17477
		public static string nhatvatpham;
	}
}
