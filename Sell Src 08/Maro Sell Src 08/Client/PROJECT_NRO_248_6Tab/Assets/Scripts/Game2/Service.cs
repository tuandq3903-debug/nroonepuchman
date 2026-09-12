using System;
using Game2.Assets.src.g;

namespace Game2
{
	// Token: 0x02000400 RID: 1024
	public class Service
	{
		// Token: 0x06002DDC RID: 11740 RVA: 0x002D5148 File Offset: 0x002D3348
		public static Service gI()
		{
			if (Service.instance == null)
			{
				Service.instance = new Service();
			}
			return Service.instance;
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x002D5160 File Offset: 0x002D3360
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

		// Token: 0x06002DDE RID: 11742 RVA: 0x002D51C4 File Offset: 0x002D33C4
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

		// Token: 0x06002DDF RID: 11743 RVA: 0x002D5234 File Offset: 0x002D3434
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

		// Token: 0x06002DE0 RID: 11744 RVA: 0x002D52FC File Offset: 0x002D34FC
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

		// Token: 0x06002DE1 RID: 11745 RVA: 0x002D53A0 File Offset: 0x002D35A0
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

		// Token: 0x06002DE2 RID: 11746 RVA: 0x002D5468 File Offset: 0x002D3668
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

		// Token: 0x06002DE3 RID: 11747 RVA: 0x002D5564 File Offset: 0x002D3764
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

		// Token: 0x06002DE4 RID: 11748 RVA: 0x002D5604 File Offset: 0x002D3804
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

		// Token: 0x06002DE5 RID: 11749 RVA: 0x002D5668 File Offset: 0x002D3868
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

		// Token: 0x06002DE6 RID: 11750 RVA: 0x002D56D8 File Offset: 0x002D38D8
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

		// Token: 0x06002DE7 RID: 11751 RVA: 0x002D5760 File Offset: 0x002D3960
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

		// Token: 0x06002DE8 RID: 11752 RVA: 0x002D57D8 File Offset: 0x002D39D8
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

		// Token: 0x06002DE9 RID: 11753 RVA: 0x002D5830 File Offset: 0x002D3A30
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

		// Token: 0x06002DEA RID: 11754 RVA: 0x002D58A0 File Offset: 0x002D3AA0
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

		// Token: 0x06002DEB RID: 11755 RVA: 0x002D5910 File Offset: 0x002D3B10
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

		// Token: 0x06002DEC RID: 11756 RVA: 0x002D5980 File Offset: 0x002D3B80
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

		// Token: 0x06002DED RID: 11757 RVA: 0x002D5A0C File Offset: 0x002D3C0C
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

		// Token: 0x06002DEE RID: 11758 RVA: 0x002D5AB4 File Offset: 0x002D3CB4
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

		// Token: 0x06002DEF RID: 11759 RVA: 0x002D5B30 File Offset: 0x002D3D30
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

		// Token: 0x06002DF0 RID: 11760 RVA: 0x002D5BA0 File Offset: 0x002D3DA0
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

		// Token: 0x06002DF1 RID: 11761 RVA: 0x002D5C10 File Offset: 0x002D3E10
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

		// Token: 0x06002DF2 RID: 11762 RVA: 0x002D5C8C File Offset: 0x002D3E8C
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

		// Token: 0x06002DF3 RID: 11763 RVA: 0x002D5CF0 File Offset: 0x002D3EF0
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

		// Token: 0x06002DF4 RID: 11764 RVA: 0x002D5D90 File Offset: 0x002D3F90
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

		// Token: 0x06002DF5 RID: 11765 RVA: 0x002D5E20 File Offset: 0x002D4020
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

		// Token: 0x06002DF6 RID: 11766 RVA: 0x002D5E90 File Offset: 0x002D4090
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

		// Token: 0x06002DF7 RID: 11767 RVA: 0x002D5F0C File Offset: 0x002D410C
		public Message messageNotLogin(sbyte command)
		{
			Message message = new Message(-29);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x002D5F21 File Offset: 0x002D4121
		public Message messageNotMap(sbyte command)
		{
			Message message = new Message(-28);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x002D5F36 File Offset: 0x002D4136
		public static Message messageSubCommand(sbyte command)
		{
			Message message = new Message(-30);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x002D5F4C File Offset: 0x002D414C
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

		// Token: 0x06002DFB RID: 11771 RVA: 0x002D60C8 File Offset: 0x002D42C8
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

		// Token: 0x06002DFC RID: 11772 RVA: 0x002D6270 File Offset: 0x002D4470
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

		// Token: 0x06002DFD RID: 11773 RVA: 0x002D62C8 File Offset: 0x002D44C8
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

		// Token: 0x06002DFE RID: 11774 RVA: 0x002D6320 File Offset: 0x002D4520
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

		// Token: 0x06002DFF RID: 11775 RVA: 0x002D63A4 File Offset: 0x002D45A4
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

		// Token: 0x06002E00 RID: 11776 RVA: 0x002D643C File Offset: 0x002D463C
		public void requestChangeMap()
		{
			Message message = new Message(-23);
			this.session.sendMessage(message);
			message.cleanup();
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x002D6464 File Offset: 0x002D4664
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

		// Token: 0x06002E02 RID: 11778 RVA: 0x002D64AC File Offset: 0x002D46AC
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

		// Token: 0x06002E03 RID: 11779 RVA: 0x002D64F4 File Offset: 0x002D46F4
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

		// Token: 0x06002E04 RID: 11780 RVA: 0x002D653C File Offset: 0x002D473C
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

		// Token: 0x06002E05 RID: 11781 RVA: 0x002D6748 File Offset: 0x002D4948
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

		// Token: 0x06002E06 RID: 11782 RVA: 0x002D67C4 File Offset: 0x002D49C4
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

		// Token: 0x06002E07 RID: 11783 RVA: 0x002D6834 File Offset: 0x002D4A34
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

		// Token: 0x06002E08 RID: 11784 RVA: 0x002D68B8 File Offset: 0x002D4AB8
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

		// Token: 0x06002E09 RID: 11785 RVA: 0x002D6940 File Offset: 0x002D4B40
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

		// Token: 0x06002E0A RID: 11786 RVA: 0x002D69D0 File Offset: 0x002D4BD0
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

		// Token: 0x06002E0B RID: 11787 RVA: 0x002D6A40 File Offset: 0x002D4C40
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

		// Token: 0x06002E0C RID: 11788 RVA: 0x002D6AA4 File Offset: 0x002D4CA4
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

		// Token: 0x06002E0D RID: 11789 RVA: 0x002D6B20 File Offset: 0x002D4D20
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

		// Token: 0x06002E0E RID: 11790 RVA: 0x002D6B90 File Offset: 0x002D4D90
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

		// Token: 0x06002E0F RID: 11791 RVA: 0x002D6C2C File Offset: 0x002D4E2C
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

		// Token: 0x06002E10 RID: 11792 RVA: 0x002D6C9C File Offset: 0x002D4E9C
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

		// Token: 0x06002E11 RID: 11793 RVA: 0x002D6D18 File Offset: 0x002D4F18
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

		// Token: 0x06002E12 RID: 11794 RVA: 0x002D6DAC File Offset: 0x002D4FAC
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

		// Token: 0x06002E13 RID: 11795 RVA: 0x002D6E1C File Offset: 0x002D501C
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

		// Token: 0x06002E14 RID: 11796 RVA: 0x002D6E80 File Offset: 0x002D5080
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

		// Token: 0x06002E15 RID: 11797 RVA: 0x002D7054 File Offset: 0x002D5254
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

		// Token: 0x06002E16 RID: 11798 RVA: 0x002D70C4 File Offset: 0x002D52C4
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

		// Token: 0x06002E17 RID: 11799 RVA: 0x002D7128 File Offset: 0x002D5328
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

		// Token: 0x06002E18 RID: 11800 RVA: 0x002D718C File Offset: 0x002D538C
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

		// Token: 0x06002E19 RID: 11801 RVA: 0x002D71FC File Offset: 0x002D53FC
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

		// Token: 0x06002E1A RID: 11802 RVA: 0x002D7294 File Offset: 0x002D5494
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

		// Token: 0x06002E1B RID: 11803 RVA: 0x002D732C File Offset: 0x002D552C
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

		// Token: 0x06002E1C RID: 11804 RVA: 0x002D73B8 File Offset: 0x002D55B8
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

		// Token: 0x06002E1D RID: 11805 RVA: 0x002D7444 File Offset: 0x002D5644
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

		// Token: 0x06002E1E RID: 11806 RVA: 0x002D74A8 File Offset: 0x002D56A8
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

		// Token: 0x06002E1F RID: 11807 RVA: 0x002D7518 File Offset: 0x002D5718
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

		// Token: 0x06002E20 RID: 11808 RVA: 0x002D7588 File Offset: 0x002D5788
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

		// Token: 0x06002E21 RID: 11809 RVA: 0x002D75F8 File Offset: 0x002D57F8
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

		// Token: 0x06002E22 RID: 11810 RVA: 0x002D7680 File Offset: 0x002D5880
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

		// Token: 0x06002E23 RID: 11811 RVA: 0x002D76F0 File Offset: 0x002D58F0
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

		// Token: 0x06002E24 RID: 11812 RVA: 0x002D7760 File Offset: 0x002D5960
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

		// Token: 0x06002E25 RID: 11813 RVA: 0x002D77EC File Offset: 0x002D59EC
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

		// Token: 0x06002E26 RID: 11814 RVA: 0x002D785C File Offset: 0x002D5A5C
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

		// Token: 0x06002E27 RID: 11815 RVA: 0x002D78D8 File Offset: 0x002D5AD8
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

		// Token: 0x06002E28 RID: 11816 RVA: 0x002D7954 File Offset: 0x002D5B54
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

		// Token: 0x06002E29 RID: 11817 RVA: 0x002D79FC File Offset: 0x002D5BFC
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

		// Token: 0x06002E2A RID: 11818 RVA: 0x002D7A6C File Offset: 0x002D5C6C
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

		// Token: 0x06002E2B RID: 11819 RVA: 0x002D7ADC File Offset: 0x002D5CDC
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

		// Token: 0x06002E2C RID: 11820 RVA: 0x002D7B4C File Offset: 0x002D5D4C
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

		// Token: 0x06002E2D RID: 11821 RVA: 0x002D7BF0 File Offset: 0x002D5DF0
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

		// Token: 0x06002E2E RID: 11822 RVA: 0x002D7C54 File Offset: 0x002D5E54
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

		// Token: 0x06002E2F RID: 11823 RVA: 0x002D7CB8 File Offset: 0x002D5EB8
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

		// Token: 0x06002E30 RID: 11824 RVA: 0x002D7D10 File Offset: 0x002D5F10
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

		// Token: 0x06002E31 RID: 11825 RVA: 0x002D7D74 File Offset: 0x002D5F74
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

		// Token: 0x06002E32 RID: 11826 RVA: 0x002D7DE4 File Offset: 0x002D5FE4
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

		// Token: 0x06002E33 RID: 11827 RVA: 0x002D7E54 File Offset: 0x002D6054
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

		// Token: 0x06002E34 RID: 11828 RVA: 0x002D7EC4 File Offset: 0x002D60C4
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

		// Token: 0x06002E35 RID: 11829 RVA: 0x002D7F48 File Offset: 0x002D6148
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

		// Token: 0x06002E36 RID: 11830 RVA: 0x002D804C File Offset: 0x002D624C
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

		// Token: 0x06002E37 RID: 11831 RVA: 0x002D80B0 File Offset: 0x002D62B0
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

		// Token: 0x06002E38 RID: 11832 RVA: 0x002D80FC File Offset: 0x002D62FC
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

		// Token: 0x06002E39 RID: 11833 RVA: 0x002D8154 File Offset: 0x002D6354
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

		// Token: 0x06002E3A RID: 11834 RVA: 0x002D81B8 File Offset: 0x002D63B8
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

		// Token: 0x06002E3B RID: 11835 RVA: 0x002D8230 File Offset: 0x002D6430
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

		// Token: 0x06002E3C RID: 11836 RVA: 0x002D8360 File Offset: 0x002D6560
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

		// Token: 0x06002E3D RID: 11837 RVA: 0x002D83E8 File Offset: 0x002D65E8
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

		// Token: 0x06002E3E RID: 11838 RVA: 0x002D8458 File Offset: 0x002D6658
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

		// Token: 0x06002E3F RID: 11839 RVA: 0x002D84B0 File Offset: 0x002D66B0
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

		// Token: 0x06002E40 RID: 11840 RVA: 0x002D8514 File Offset: 0x002D6714
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

		// Token: 0x06002E41 RID: 11841 RVA: 0x002D856C File Offset: 0x002D676C
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

		// Token: 0x06002E42 RID: 11842 RVA: 0x002D85D8 File Offset: 0x002D67D8
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

		// Token: 0x06002E43 RID: 11843 RVA: 0x002D86F0 File Offset: 0x002D68F0
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

		// Token: 0x06002E44 RID: 11844 RVA: 0x002D8784 File Offset: 0x002D6984
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

		// Token: 0x06002E45 RID: 11845 RVA: 0x002D87FC File Offset: 0x002D69FC
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

		// Token: 0x06002E46 RID: 11846 RVA: 0x002D8854 File Offset: 0x002D6A54
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

		// Token: 0x06002E47 RID: 11847 RVA: 0x002D88C0 File Offset: 0x002D6AC0
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

		// Token: 0x06002E48 RID: 11848 RVA: 0x002D8930 File Offset: 0x002D6B30
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

		// Token: 0x06002E49 RID: 11849 RVA: 0x002D89AC File Offset: 0x002D6BAC
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

		// Token: 0x06002E4A RID: 11850 RVA: 0x002D8A04 File Offset: 0x002D6C04
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

		// Token: 0x06002E4B RID: 11851 RVA: 0x002D8A6C File Offset: 0x002D6C6C
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

		// Token: 0x06002E4C RID: 11852 RVA: 0x002D8AD4 File Offset: 0x002D6CD4
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

		// Token: 0x06002E4D RID: 11853 RVA: 0x002D8B20 File Offset: 0x002D6D20
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

		// Token: 0x06002E4E RID: 11854 RVA: 0x000034B9 File Offset: 0x000016B9
		public void SendRemoteAddress()
		{
		}

		// Token: 0x040059BB RID: 22971
		private ISession session = Session_ME.gI();

		// Token: 0x040059BC RID: 22972
		protected static Service instance;

		// Token: 0x040059BD RID: 22973
		public static long curCheckController;

		// Token: 0x040059BE RID: 22974
		public static long curCheckMap;

		// Token: 0x040059BF RID: 22975
		public static long logController;

		// Token: 0x040059C0 RID: 22976
		public static long logMap;

		// Token: 0x040059C1 RID: 22977
		public static bool reciveFromMainSession;
	}
}
