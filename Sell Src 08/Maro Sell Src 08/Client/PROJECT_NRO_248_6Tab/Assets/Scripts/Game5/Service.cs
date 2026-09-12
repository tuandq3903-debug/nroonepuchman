using System;
using Game5.Assets.src.g;

namespace Game5
{
	// Token: 0x02000178 RID: 376
	public class Service
	{
		// Token: 0x060010F0 RID: 4336 RVA: 0x00115F5C File Offset: 0x0011415C
		public static Service gI()
		{
			if (Service.instance == null)
			{
				Service.instance = new Service();
			}
			return Service.instance;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00115F74 File Offset: 0x00114174
		public void gotoPlayer(int id)
		{
			Message message = null;
			try
			{
				message = new Message(18);
				message.writer().writeInt(id);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x00115FD8 File Offset: 0x001141D8
		public void androidPack()
		{
			if (mSystem.android_pack == null)
			{
				return;
			}
			Message message = null;
			try
			{
				message = new Message(126);
				message.writer().writeUTF(mSystem.android_pack);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00116048 File Offset: 0x00114248
		public void charInfo(string day, string month, string year, string address, string cmnd, string dayCmnd, string noiCapCmnd, string sdt, string name)
		{
			Message message = null;
			try
			{
				message = new Message(42);
				message.writer().writeUTF(day);
				message.writer().writeUTF(month);
				message.writer().writeUTF(year);
				message.writer().writeUTF(address);
				message.writer().writeUTF(cmnd);
				message.writer().writeUTF(dayCmnd);
				message.writer().writeUTF(noiCapCmnd);
				message.writer().writeUTF(sdt);
				message.writer().writeUTF(name);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00116110 File Offset: 0x00114310
		public void androidPack2()
		{
			if (mSystem.android_pack == null)
			{
				return;
			}
			Message message = null;
			try
			{
				message = new Message(126);
				message.writer().writeUTF(mSystem.android_pack);
				if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					this.session = Session_ME.gI();
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x001161B4 File Offset: 0x001143B4
		public void combine(sbyte action, MyVector id)
		{
			Res.outz("combine");
			Message message = null;
			try
			{
				message = new Message(-81);
				message.writer().writeByte(action);
				if (action == 1)
				{
					message.writer().writeByte(id.size());
					for (int i = 0; i < id.size(); i++)
					{
						message.writer().writeByte(((Item)id.elementAt(i)).indexUI);
						Res.outz("gui id " + ((Item)id.elementAt(i)).indexUI.ToString());
					}
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0011627C File Offset: 0x0011447C
		public void giaodich(sbyte action, int playerID, sbyte index, int num)
		{
			Res.outz2("giao dich action = " + action.ToString());
			Message message = null;
			try
			{
				message = new Message(-86);
				message.writer().writeByte(action);
				if (action == 0 || action == 1)
				{
					Res.outz2(">>>> len playerID =" + playerID.ToString());
					message.writer().writeInt(playerID);
				}
				if (action == 2)
				{
					Res.outz2("gui len index =" + index.ToString() + " num= " + num.ToString());
					message.writer().writeByte(index);
					message.writer().writeInt(num);
				}
				if (action == 4)
				{
					Res.outz2(">>>> len index =" + index.ToString());
					message.writer().writeByte(index);
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00116378 File Offset: 0x00114578
		public void sendClientInput(TField[] t)
		{
			Message message = null;
			try
			{
				Res.outz(" gui input ");
				message = new Message(-125);
				Res.outz("byte lent = " + t.Length.ToString());
				message.writer().writeByte(t.Length);
				for (int i = 0; i < t.Length; i++)
				{
					message.writer().writeUTF(t[i].getText());
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00116418 File Offset: 0x00114618
		public void speacialSkill(sbyte index)
		{
			Message message = null;
			try
			{
				message = new Message(112);
				message.writer().writeByte(index);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0011647C File Offset: 0x0011467C
		public void mobCapcha(char ch)
		{
			Res.outz("cap char c= " + ch.ToString());
			Message message = null;
			try
			{
				message = new Message(-85);
				message.writer().writeChar(ch);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x001164EC File Offset: 0x001146EC
		public void friend(sbyte action, int playerId)
		{
			Res.outz("add friend");
			Message message = null;
			try
			{
				message = new Message(-80);
				message.writer().writeByte(action);
				if (playerId != -1)
				{
					message.writer().writeInt(playerId);
				}
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x00116574 File Offset: 0x00114774
		public void getArchivemnt(int index)
		{
			Res.outz("get ngoc");
			Message message = null;
			try
			{
				message = new Message(-76);
				message.writer().writeByte(index);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x001165EC File Offset: 0x001147EC
		public void getPlayerMenu(int playerID)
		{
			Message message = null;
			try
			{
				message = new Message(-79);
				message.writer().writeInt(playerID);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00116644 File Offset: 0x00114844
		public void clanImage(sbyte id)
		{
			Message message = null;
			try
			{
				message = new Message(-62);
				message.writer().writeByte(id);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x001166B4 File Offset: 0x001148B4
		public void skill_not_focus(sbyte status)
		{
			Message message = null;
			try
			{
				message = new Message(-45);
				message.writer().writeByte(status);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00116724 File Offset: 0x00114924
		public void clanDonate(int id)
		{
			Message message = null;
			try
			{
				message = new Message(-54);
				message.writer().writeInt(id);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00116794 File Offset: 0x00114994
		public void clanMessage(int type, string text, int clanID)
		{
			Message message = null;
			try
			{
				message = new Message(-51);
				message.writer().writeByte(type);
				if (type == 0)
				{
					message.writer().writeUTF(text);
				}
				if (type == 2)
				{
					message.writer().writeInt(clanID);
				}
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00116820 File Offset: 0x00114A20
		public void useItem(sbyte type, sbyte where, sbyte index, short template)
		{
			Cout.println("USE ITEM! " + type.ToString());
			if (Char.myCharz().statusMe == 14)
			{
				return;
			}
			Message message = null;
			try
			{
				message = new Message(-43);
				message.writer().writeByte(type);
				message.writer().writeByte(where);
				message.writer().writeByte(index);
				if (index == -1)
				{
					message.writer().writeShort(template);
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x001168C8 File Offset: 0x00114AC8
		public void joinClan(int id, sbyte action)
		{
			Message message = null;
			try
			{
				message = new Message(-49);
				message.writer().writeInt(id);
				message.writer().writeByte(action);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00116944 File Offset: 0x00114B44
		public void clanMember(int id)
		{
			Message message = null;
			try
			{
				message = new Message(-50);
				message.writer().writeInt(id);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x001169B4 File Offset: 0x00114BB4
		public void searchClan(string text)
		{
			Message message = null;
			try
			{
				message = new Message(-47);
				message.writer().writeUTF(text);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00116A24 File Offset: 0x00114C24
		public void clanRemote(int id, sbyte role)
		{
			Message message = null;
			try
			{
				message = new Message(-56);
				message.writer().writeInt(id);
				message.writer().writeByte(role);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00116AA0 File Offset: 0x00114CA0
		public void leaveClan()
		{
			Message message = null;
			try
			{
				message = new Message(-55);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00116B04 File Offset: 0x00114D04
		public void clanInvite(sbyte action, int playerID, int clanID, int code)
		{
			Message message = null;
			try
			{
				message = new Message(-57);
				message.writer().writeByte(action);
				if (action == 0)
				{
					message.writer().writeInt(playerID);
				}
				if (action == 1 || action == 2)
				{
					message.writer().writeInt(clanID);
					message.writer().writeInt(code);
				}
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x00116BA4 File Offset: 0x00114DA4
		public void getClan(sbyte action, sbyte id, string text)
		{
			Message message = null;
			try
			{
				message = new Message(-46);
				message.writer().writeByte(action);
				if (action == 2 || action == 4)
				{
					message.writer().writeByte(id);
					message.writer().writeUTF(text);
				}
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x00116C34 File Offset: 0x00114E34
		public void updateCaption(sbyte gender)
		{
			Message message = null;
			try
			{
				message = new Message(-41);
				message.writer().writeByte(gender);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00116CA4 File Offset: 0x00114EA4
		public void getItem(sbyte type, sbyte id)
		{
			Message message = null;
			try
			{
				message = new Message(-40);
				message.writer().writeByte(type);
				message.writer().writeByte(id);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x00116D20 File Offset: 0x00114F20
		public Message messageNotLogin(sbyte command)
		{
			Message message = new Message(-29);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00116D35 File Offset: 0x00114F35
		public Message messageNotMap(sbyte command)
		{
			Message message = new Message(-28);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00116D4A File Offset: 0x00114F4A
		public static Message messageSubCommand(sbyte command)
		{
			Message message = new Message(-30);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00116D60 File Offset: 0x00114F60
		public void setClientType()
		{
			if (Rms.loadRMSInt("clienttype") != -1)
			{
				Main.typeClient = Rms.loadRMSInt("clienttype");
			}
			try
			{
				Message message = this.messageNotLogin(2);
				message.writer().writeByte(Main.typeClient);
				message.writer().writeByte(mGraphics.zoomLevel);
				message.writer().writeBoolean(false);
				message.writer().writeInt(GameCanvas.w);
				message.writer().writeInt(GameCanvas.h);
				message.writer().writeBoolean(TField.isQwerty);
				message.writer().writeBoolean(GameCanvas.isTouch);
				message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
				DataInputStream dataInputStream = MyStream.readFile("/info");
				if (dataInputStream != null)
				{
					sbyte[] data = new sbyte[dataInputStream.r.buffer.Length];
					dataInputStream.read(ref data);
					if (data != null)
					{
						message.writer().writeShort(data.Length);
						message.writer().write(data);
						Res.err("write " + data.Length.ToString() + "|" + GameMidlet.VERSION);
					}
				}
				this.session.sendMessage(message);
				message.cleanup();
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			this.SendRemoteAddress();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00116EDC File Offset: 0x001150DC
		public void setClientType2()
		{
			Res.outz("SET CLIENT TYPE");
			if (Rms.loadRMSInt("clienttype") != -1)
			{
				mSystem.clientType = Rms.loadRMSInt("clienttype");
			}
			try
			{
				Res.outz("setType");
				Message message = this.messageNotLogin(2);
				message.writer().writeByte(mSystem.clientType);
				message.writer().writeByte(mGraphics.zoomLevel);
				Res.outz("gui zoomlevel = " + mGraphics.zoomLevel.ToString());
				message.writer().writeBoolean(false);
				message.writer().writeInt(GameCanvas.w);
				message.writer().writeInt(GameCanvas.h);
				message.writer().writeBoolean(TField.isQwerty);
				message.writer().writeBoolean(GameCanvas.isTouch);
				message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
				DataInputStream dataInputStream = MyStream.readFile("/info");
				if (dataInputStream != null)
				{
					sbyte[] data = new sbyte[dataInputStream.r.buffer.Length];
					dataInputStream.read(ref data);
					if (data != null)
					{
						message.writer().writeShort(data.Length);
						message.writer().write(data);
						Res.err("write " + data.Length.ToString() + "|" + GameMidlet.VERSION);
					}
				}
				this.session = Session_ME2.gI();
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
				message.cleanup();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x00117084 File Offset: 0x00115284
		public void sendCheckController()
		{
			Message message = null;
			try
			{
				message = new Message(-120);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				Service.curCheckController = mSystem.currentTimeMillis();
				message.cleanup();
			}
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x001170DC File Offset: 0x001152DC
		public void sendCheckMap()
		{
			Message message = null;
			try
			{
				message = new Message(-121);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				Service.curCheckMap = mSystem.currentTimeMillis();
				message.cleanup();
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00117134 File Offset: 0x00115334
		public void login(string username, string pass, string version, sbyte type)
		{
			try
			{
				Message message = this.messageNotLogin(0);
				message.writer().writeUTF(username);
				message.writer().writeUTF(pass);
				message.writer().writeUTF(version);
				message.writer().writeByte(type);
				this.session.sendMessage(message);
				message.cleanup();
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x001171B8 File Offset: 0x001153B8
		public void requestRegister(string username, string pass, string usernameAo, string passAo, string version)
		{
			try
			{
				Message message = this.messageNotLogin(1);
				message.writer().writeUTF(username);
				message.writer().writeUTF(pass);
				if (usernameAo != null && !usernameAo.Equals(string.Empty))
				{
					message.writer().writeUTF(usernameAo);
					message.writer().writeUTF("a");
				}
				this.session.sendMessage(message);
				message.cleanup();
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00117250 File Offset: 0x00115450
		public void requestChangeMap()
		{
			Message message = new Message(-23);
			this.session.sendMessage(message);
			message.cleanup();
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00117278 File Offset: 0x00115478
		public void magicTree(sbyte type)
		{
			Message message = new Message(-34);
			try
			{
				message.writer().writeByte(type);
				this.session.sendMessage(message);
				message.cleanup();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x001172C0 File Offset: 0x001154C0
		public void requestChangeZone(int zoneId, int indexUI)
		{
			Message message = new Message(21);
			try
			{
				message.writer().writeByte(zoneId);
				this.session.sendMessage(message);
				message.cleanup();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00117308 File Offset: 0x00115508
		public void checkMMove(int second)
		{
			Message message = new Message(-78);
			try
			{
				message.writer().writeInt(second);
				this.session.sendMessage(message);
				message.cleanup();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00117350 File Offset: 0x00115550
		public void charMove()
		{
			int num = Char.myCharz().cx - Char.myCharz().cxSend;
			int num2 = Char.myCharz().cy - Char.myCharz().cySend;
			if (Char.ischangingMap || (num == 0 && num2 == 0) || Controller.isStopReadMessage || Char.myCharz().isTeleport || Char.myCharz().cy <= 0 || Char.myCharz().telePortSkill)
			{
				return;
			}
			try
			{
				Message message = new Message(-7);
				Char.myCharz().cxSend = Char.myCharz().cx;
				Char.myCharz().cySend = Char.myCharz().cy;
				Char.myCharz().cdirSend = Char.myCharz().cdir;
				Char.myCharz().cactFirst = Char.myCharz().statusMe;
				if (TileMap.tileTypeAt(Char.myCharz().cx / (int)TileMap.size, Char.myCharz().cy / (int)TileMap.size) == 0)
				{
					message.writer().writeByte(1);
					if (Char.myCharz().canFly)
					{
						if (!Char.myCharz().isHaveMount)
						{
							Char.myCharz().cMP -= Char.myCharz().cMPGoc / 100L * ((Char.myCharz().isMonkey != 1) ? 1L : 2L);
						}
						if (Char.myCharz().cMP < 0L)
						{
							Char.myCharz().cMP = 0L;
						}
						GameScr.gI().isInjureMp = true;
						GameScr.gI().twMp = 0;
					}
				}
				else
				{
					message.writer().writeByte(0);
				}
				message.writer().writeShort(Char.myCharz().cx);
				if (num2 != 0)
				{
					message.writer().writeShort(Char.myCharz().cy);
				}
				this.session.sendMessage(message);
				GameScr.tickMove++;
				message.cleanup();
			}
			catch (Exception ex)
			{
				Cout.LogError("LOI CHAR MOVE " + ex.ToString());
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0011755C File Offset: 0x0011575C
		public void createChar(string name, int gender, int hair)
		{
			Message message = new Message(-28);
			try
			{
				message.writer().writeByte(2);
				message.writer().writeUTF(name);
				message.writer().writeByte(gender);
				message.writer().writeByte(hair);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			this.session.sendMessage(message);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x001175D8 File Offset: 0x001157D8
		public void requestModTemplate(int modTemplateId)
		{
			Message message = null;
			try
			{
				message = new Message(11);
				message.writer().writeByte(modTemplateId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00117648 File Offset: 0x00115848
		public void saleItem(sbyte action, sbyte type, short id)
		{
			Message message = null;
			try
			{
				message = new Message(7);
				message.writer().writeByte(action);
				message.writer().writeByte(type);
				message.writer().writeShort(id);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x001176CC File Offset: 0x001158CC
		public void buyItem(sbyte type, int id, int quantity)
		{
			Message message = null;
			try
			{
				message = new Message(6);
				message.writer().writeByte(type);
				message.writer().writeShort(id);
				if (quantity > 1)
				{
					message.writer().writeShort(quantity);
				}
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00117754 File Offset: 0x00115954
		public void selectSkill(int skillTemplateId)
		{
			Cout.println(Char.myCharz().cName + " SELECT SKILL " + skillTemplateId.ToString());
			Message message = null;
			try
			{
				message = new Message(34);
				message.writer().writeShort(skillTemplateId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x001177E4 File Offset: 0x001159E4
		public void getEffData(short id)
		{
			Message message = null;
			try
			{
				message = new Message(-66);
				message.writer().writeShort(id);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00117854 File Offset: 0x00115A54
		public void openUIZone()
		{
			Message message = null;
			try
			{
				message = new Message(29);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x001178B8 File Offset: 0x00115AB8
		public void confirmMenu(short npcID, sbyte select)
		{
			Message message = null;
			try
			{
				message = new Message(32);
				message.writer().writeShort(npcID);
				message.writer().writeByte(select);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00117934 File Offset: 0x00115B34
		public void openMenu(int npcId)
		{
			Message message = null;
			try
			{
				message = new Message(33);
				message.writer().writeShort(npcId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x001179A4 File Offset: 0x00115BA4
		public void menu(int npcId, int menuId, int optionId)
		{
			Cout.println("menuid: " + menuId.ToString());
			Message message = null;
			try
			{
				message = new Message(22);
				message.writer().writeByte(npcId);
				message.writer().writeByte(menuId);
				message.writer().writeByte(optionId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00117A40 File Offset: 0x00115C40
		public void menuId(short menuId)
		{
			Message message = null;
			try
			{
				message = new Message(27);
				message.writer().writeShort(menuId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00117AB0 File Offset: 0x00115CB0
		public void textBoxId(short menuId, string str)
		{
			Message message = null;
			try
			{
				message = new Message(88);
				message.writer().writeShort(menuId);
				message.writer().writeUTF(str);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00117B2C File Offset: 0x00115D2C
		public void crystalCollectLock(Item[] items)
		{
			GameCanvas.msgdlg.pleasewait();
			Message message = null;
			try
			{
				message = new Message(13);
				for (int i = 0; i < items.Length; i++)
				{
					if (items[i] != null)
					{
						message.writer().writeByte(items[i].indexUI);
					}
				}
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00117BC0 File Offset: 0x00115DC0
		public void acceptInviteTrade(int playerMapId)
		{
			Message message = null;
			try
			{
				message = new Message(37);
				message.writer().writeInt(playerMapId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00117C30 File Offset: 0x00115E30
		public void cancelInviteTrade()
		{
			Message message = null;
			try
			{
				message = new Message(50);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00117C94 File Offset: 0x00115E94
		public void sendPlayerAttack(MyVector vMob, MyVector vChar, int type)
		{
			try
			{
				Message message = null;
				if (type != 0)
				{
					if (vMob.size() > 0 && vChar.size() > 0)
					{
						if (type != 1)
						{
							if (type == 2)
							{
								message = new Message(67);
							}
						}
						else
						{
							message = new Message(-4);
						}
						message.writer().writeByte(vMob.size());
						for (int i = 0; i < vMob.size(); i++)
						{
							Mob mob = (Mob)vMob.elementAt(i);
							message.writer().writeByte(mob.mobId);
						}
						for (int j = 0; j < vChar.size(); j++)
						{
							Char @char = (Char)vChar.elementAt(j);
							if (@char != null)
							{
								message.writer().writeInt(@char.charID);
							}
							else
							{
								message.writer().writeInt(-1);
							}
						}
					}
					else if (vMob.size() > 0)
					{
						message = new Message(54);
						for (int k = 0; k < vMob.size(); k++)
						{
							Mob mob2 = (Mob)vMob.elementAt(k);
							if (!mob2.isMobMe)
							{
								message.writer().writeByte(mob2.mobId);
							}
							else
							{
								message.writer().writeByte(-1);
								message.writer().writeInt(mob2.mobId);
							}
						}
					}
					else if (vChar.size() > 0)
					{
						message = new Message(-60);
						for (int l = 0; l < vChar.size(); l++)
						{
							Char char2 = (Char)vChar.elementAt(l);
							message.writer().writeInt(char2.charID);
						}
					}
					message.writer().writeSByte((sbyte)Char.myCharz().cdir);
					if (message != null)
					{
						this.session.sendMessage(message);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00117E68 File Offset: 0x00116068
		public void pickItem(int itemMapId)
		{
			Message message = null;
			try
			{
				message = new Message(-20);
				message.writer().writeShort(itemMapId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00117ED8 File Offset: 0x001160D8
		public void returnTownFromDead()
		{
			Message message = null;
			try
			{
				message = new Message(-15);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00117F3C File Offset: 0x0011613C
		public void wakeUpFromDead()
		{
			Message message = null;
			try
			{
				message = new Message(-16);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00117FA0 File Offset: 0x001161A0
		public void chat(string text)
		{
			Message message = null;
			try
			{
				message = new Message(44);
				message.writer().writeUTF(text);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00118010 File Offset: 0x00116210
		public void updateData()
		{
			Message message = null;
			try
			{
				message = new Message(-87);
				if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					this.session = Session_ME.gI();
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x001180A8 File Offset: 0x001162A8
		public void updateMap()
		{
			Message message = null;
			try
			{
				message = this.messageNotMap(6);
				if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					this.session = Session_ME.gI();
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00118140 File Offset: 0x00116340
		public void updateSkill()
		{
			Message message = null;
			try
			{
				message = this.messageNotMap(7);
				if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					this.session = Session_ME.gI();
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x001181CC File Offset: 0x001163CC
		public void updateItem()
		{
			Message message = null;
			try
			{
				message = this.messageNotMap(8);
				if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					this.session = Session_ME.gI();
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00118258 File Offset: 0x00116458
		public void clientOk()
		{
			Message message = null;
			try
			{
				message = this.messageNotMap(13);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x001182BC File Offset: 0x001164BC
		public void addFriend(string name)
		{
			Message message = null;
			try
			{
				message = new Message(53);
				message.writer().writeUTF(name);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0011832C File Offset: 0x0011652C
		public void addPartyAccept(int charId)
		{
			Message message = null;
			try
			{
				message = new Message(76);
				message.writer().writeInt(charId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0011839C File Offset: 0x0011659C
		public void addPartyCancel(int charId)
		{
			Message message = null;
			try
			{
				message = new Message(77);
				message.writer().writeInt(charId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0011840C File Offset: 0x0011660C
		public void player_vs_player(sbyte action, sbyte type, int playerId)
		{
			Message message = null;
			try
			{
				message = new Message(-59);
				message.writer().writeByte(action);
				message.writer().writeByte(type);
				message.writer().writeInt(playerId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00118494 File Offset: 0x00116694
		public void requestMaptemplate(int maptemplateId)
		{
			Message message = null;
			try
			{
				message = this.messageNotMap(10);
				message.writer().writeByte(maptemplateId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00118504 File Offset: 0x00116704
		public void acceptPleaseParty(string str)
		{
			Message message = null;
			try
			{
				message = new Message(17);
				message.writer().writeUTF(str);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00118574 File Offset: 0x00116774
		public void chatPlayer(string text, int id)
		{
			Res.outz("chat player text = " + text);
			Message message = null;
			try
			{
				message = new Message(-72);
				message.writer().writeInt(id);
				message.writer().writeUTF(text);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00118600 File Offset: 0x00116800
		public void chatGlobal(string text)
		{
			Message message = null;
			try
			{
				message = new Message(-71);
				message.writer().writeUTF(text);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00118670 File Offset: 0x00116870
		public void sendCardInfo(string NAP, string PIN)
		{
			Message message = null;
			try
			{
				message = this.messageNotMap(16);
				message.writer().writeUTF(NAP);
				message.writer().writeUTF(PIN);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x001186EC File Offset: 0x001168EC
		public void changeName(string name, int id)
		{
			Message message = null;
			try
			{
				message = this.messageNotMap(18);
				message.writer().writeInt(id);
				message.writer().writeUTF(name);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00118768 File Offset: 0x00116968
		public void requestIcon(int id)
		{
			GameCanvas.connect();
			Message message = null;
			try
			{
				message = new Message(-67);
				message.writer().writeInt(id);
				if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					this.session = Session_ME.gI();
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00118810 File Offset: 0x00116A10
		public void activeAccProtect(int pass)
		{
			Message message = null;
			try
			{
				message = this.messageNotMap(37);
				message.writer().writeInt(pass);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00118880 File Offset: 0x00116A80
		public void clearAccProtect(int pass)
		{
			Message message = null;
			try
			{
				message = this.messageNotMap(41);
				message.writer().writeInt(pass);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x001188F0 File Offset: 0x00116AF0
		public void openLockAccProtect(int pass2)
		{
			Message message = null;
			try
			{
				message = this.messageNotMap(39);
				message.writer().writeInt(pass2);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00118960 File Offset: 0x00116B60
		public void getBgTemplate(short id)
		{
			Message message = null;
			try
			{
				message = new Message(-32);
				message.writer().writeShort(id);
				if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					this.session = Session_ME.gI();
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00118A04 File Offset: 0x00116C04
		public void getMapOffline()
		{
			Message message = null;
			try
			{
				message = new Message(-33);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00118A68 File Offset: 0x00116C68
		public void finishUpdate()
		{
			Message message = null;
			try
			{
				message = new Message(-38);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00118ACC File Offset: 0x00116CCC
		public void finishUpdate(int playerID)
		{
			Message message = null;
			try
			{
				message = new Message(-38);
				message.writer().writeInt(playerID);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00118B24 File Offset: 0x00116D24
		public void finishLoadMap()
		{
			Message message = null;
			try
			{
				message = new Message(-39);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00118B88 File Offset: 0x00116D88
		public void requestBagImage(sbyte ID)
		{
			Message message = null;
			try
			{
				message = new Message(-63);
				message.writer().writeByte(ID);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00118BF8 File Offset: 0x00116DF8
		public void login2(string user)
		{
			Res.outz("Login 2");
			Message message = null;
			try
			{
				message = new Message(-101);
				message.writer().writeUTF(user);
				message.writer().writeByte(1);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00118C68 File Offset: 0x00116E68
		public void getMagicTree(sbyte action)
		{
			Message message = null;
			try
			{
				message = new Message(-34);
				message.writer().writeByte(action);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00118CD8 File Offset: 0x00116ED8
		public void upPotential(bool forPet, int typePotential, int num)
		{
			Message message = null;
			try
			{
				message = messageSubCommand((sbyte)(forPet ? 18 : 16));
				message.writer().writeByte(typePotential);
				message.writer().writeShort(num);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00118D5C File Offset: 0x00116F5C
		public void getResource(sbyte action, MyVector vResourceIndex)
		{
			Res.outz("request resource action= " + action.ToString());
			Message message = null;
			try
			{
				message = new Message(-74);
				message.writer().writeByte(action);
				if (action == 2 && vResourceIndex != null)
				{
					message.writer().writeShort(vResourceIndex.size());
					for (int i = 0; i < vResourceIndex.size(); i++)
					{
						message.writer().writeShort(short.Parse((string)vResourceIndex.elementAt(i)));
					}
				}
				if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					Service.reciveFromMainSession = true;
					this.session = Session_ME.gI();
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00118E60 File Offset: 0x00117060
		public void requestMapSelect(int selected)
		{
			Res.outz("request magic tree");
			Message message = null;
			try
			{
				message = new Message(-91);
				message.writer().writeByte(selected);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00118EC4 File Offset: 0x001170C4
		public void petInfo()
		{
			Message message = null;
			try
			{
				message = new Message(-107);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00118F10 File Offset: 0x00117110
		public void PetInfo2()
		{
			Message message = null;
			try
			{
				message = new Message(3);
				message.writer().writeByte(0);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00118F68 File Offset: 0x00117168
		public void sendTop(string topName, sbyte selected)
		{
			Message message = null;
			try
			{
				message = new Message(-96);
				message.writer().writeUTF(topName);
				message.writer().writeByte(selected);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00118FCC File Offset: 0x001171CC
		public void enemy(sbyte b, int charID)
		{
			Message message = null;
			Res.outz("add enemy");
			try
			{
				message = new Message(-99);
				message.writer().writeByte(b);
				if (b == 1 || b == 2)
				{
					message.writer().writeInt(charID);
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00119044 File Offset: 0x00117244
		public void kigui(sbyte action, int itemId, sbyte moneyType, int money, int quaintly)
		{
			Message message = null;
			try
			{
				Res.outz("ki gui action= " + action.ToString());
				message = new Message(-100);
				message.writer().writeByte(action);
				if (action == 0)
				{
					message.writer().writeShort(itemId);
					message.writer().writeByte(moneyType);
					message.writer().writeInt(money);
					message.writer().writeInt(quaintly);
				}
				if (action == 1 || action == 2)
				{
					message.writer().writeShort(itemId);
				}
				if (action == 3)
				{
					message.writer().writeShort(itemId);
					message.writer().writeByte(moneyType);
					message.writer().writeInt(money);
				}
				if (action == 4)
				{
					message.writer().writeByte(moneyType);
					message.writer().writeByte(money);
					Res.outz("currTab= " + moneyType.ToString() + " page= " + money.ToString());
				}
				if (action == 5)
				{
					message.writer().writeShort(itemId);
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00119174 File Offset: 0x00117374
		public void getFlag(sbyte action, sbyte flagType)
		{
			Message message = null;
			try
			{
				message = new Message(-103);
				message.writer().writeByte(action);
				Res.outz("------------service--  " + action.ToString() + "   " + flagType.ToString());
				if (action != 0)
				{
					message.writer().writeByte(flagType);
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x001191FC File Offset: 0x001173FC
		public void setLockInventory(int pass)
		{
			Message message = null;
			try
			{
				Res.outz("------------setLockInventory:     " + pass.ToString());
				message = new Message(-104);
				message.writer().writeInt(pass);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0011926C File Offset: 0x0011746C
		public void petStatus(sbyte status)
		{
			Message message = null;
			try
			{
				message = new Message(-108);
				message.writer().writeByte(status);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x001192C4 File Offset: 0x001174C4
		public void pet2Status(sbyte status)
		{
			Message message = null;
			try
			{
				message = new Message(3);
				message.writer().writeByte(1);
				message.writer().writeByte(status);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00119328 File Offset: 0x00117528
		public void transportNow()
		{
			Message message = null;
			try
			{
				Res.outz("------------transportNow  ");
				message = new Message(-105);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00119380 File Offset: 0x00117580
		public void funsion(sbyte type)
		{
			Message message = null;
			try
			{
				Res.outz("FUNSION");
				message = new Message(125);
				message.writer().writeByte(type);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x001193EC File Offset: 0x001175EC
		public void imageSource(MyVector vID)
		{
			Message message = null;
			try
			{
				Res.outz("IMAGE SOURCE size= " + vID.size().ToString());
				message = new Message(-111);
				message.writer().writeShort(vID.size());
				if (vID.size() > 0)
				{
					for (int i = 0; i < vID.size(); i++)
					{
						Res.outz("gui len str " + ((ImageSource)vID.elementAt(i)).id);
						message.writer().writeUTF(((ImageSource)vID.elementAt(i)).id);
					}
				}
				if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
				{
					this.session = Session_ME2.gI();
				}
				else
				{
					this.session = Session_ME.gI();
					Service.reciveFromMainSession = true;
				}
				this.session.sendMessage(message);
				this.session = Session_ME.gI();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00119504 File Offset: 0x00117704
		public void sendServerData(sbyte action, int id, sbyte[] data)
		{
			Message message = null;
			try
			{
				Res.outz("SERVER DATA");
				message = new Message(-110);
				message.writer().writeByte(action);
				if (action == 1)
				{
					message.writer().writeInt(id);
					if (data != null)
					{
						int num = data.Length;
						message.writer().writeShort(num);
						message.writer().write(ref data, 0, num);
					}
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00119598 File Offset: 0x00117798
		public void changeOnKeyScr(sbyte[] skill)
		{
			Message message = null;
			try
			{
				message = new Message(-113);
				for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
				{
					message.writer().writeByte(skill[i]);
				}
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00119610 File Offset: 0x00117810
		public void requestPean()
		{
			Message message = null;
			try
			{
				message = new Message(-114);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00119668 File Offset: 0x00117868
		public void sendThachDau(int id)
		{
			Res.outz("GUI THACH DAU");
			Message message = null;
			try
			{
				message = new Message(-118);
				message.writer().writeInt(id);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x001196D4 File Offset: 0x001178D4
		public void messagePlayerMenu(int charId)
		{
			Message message = null;
			try
			{
				message = new Message(-30);
				message.writer().writeByte(63);
				message.writer().writeInt(charId);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00119744 File Offset: 0x00117944
		public void playerMenuAction(int charId, short select)
		{
			Message message = null;
			try
			{
				message = new Message(-30);
				message.writer().writeByte(64);
				message.writer().writeInt(charId);
				message.writer().writeShort(select);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x001197C0 File Offset: 0x001179C0
		public void getImgByName(string nameImg)
		{
			Message message = null;
			try
			{
				message = new Message(66);
				message.writer().writeUTF(nameImg);
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00119818 File Offset: 0x00117A18
		public void SendCrackBall(byte type, byte soluong)
		{
			Message message = new Message(-127);
			try
			{
				message.writer().writeByte((int)type);
				if (soluong > 0)
				{
					message.writer().writeByte((int)soluong);
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00119880 File Offset: 0x00117A80
		public void SendRada(int i, int id)
		{
			Message message = new Message(sbyte.MaxValue);
			try
			{
				message.writer().writeByte(i);
				if (id != -1)
				{
					message.writer().writeShort(id);
				}
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x001198E8 File Offset: 0x00117AE8
		public void sendDelAcc()
		{
			Message message = new Message(69);
			try
			{
				this.session.sendMessage(message);
			}
			catch (Exception)
			{
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00119934 File Offset: 0x00117B34
		public void new_skill_not_focus(sbyte idTemplateSkill, sbyte dir, short x, short y)
		{
			Message message = null;
			try
			{
				message = new Message(-45);
				message.writer().writeSByte(20);
				message.writer().writeSByte(idTemplateSkill);
				message.writer().writeShort(Char.myCharz().cx);
				message.writer().writeShort(Char.myCharz().cy);
				message.writer().writeSByte(dir);
				message.writer().writeShort(x);
				message.writer().writeShort(y);
				this.session.sendMessage(message);
			}
			catch (Exception ex)
			{
				Cout.println(ex.Message + ex.StackTrace);
			}
			finally
			{
				message.cleanup();
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000034B9 File Offset: 0x000016B9
		public void SendRemoteAddress()
		{
		}

		// Token: 0x0400223E RID: 8766
		private ISession session = Session_ME.gI();

		// Token: 0x0400223F RID: 8767
		protected static Service instance;

		// Token: 0x04002240 RID: 8768
		public static long curCheckController;

		// Token: 0x04002241 RID: 8769
		public static long curCheckMap;

		// Token: 0x04002242 RID: 8770
		public static long logController;

		// Token: 0x04002243 RID: 8771
		public static long logMap;

		// Token: 0x04002244 RID: 8772
		public static bool reciveFromMainSession;
	}
}
