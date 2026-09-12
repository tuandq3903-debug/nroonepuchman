using System;
using Game6.Assets.src.g;

namespace Game6
{
	// Token: 0x020000A0 RID: 160
	public class Service
	{
		// Token: 0x0600074C RID: 1868 RVA: 0x00080E48 File Offset: 0x0007F048
		public static Service gI()
		{
			if (Service.instance == null)
			{
				Service.instance = new Service();
			}
			return Service.instance;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00080E60 File Offset: 0x0007F060
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

		// Token: 0x0600074E RID: 1870 RVA: 0x00080EC4 File Offset: 0x0007F0C4
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

		// Token: 0x0600074F RID: 1871 RVA: 0x00080F34 File Offset: 0x0007F134
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

		// Token: 0x06000750 RID: 1872 RVA: 0x00080FFC File Offset: 0x0007F1FC
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

		// Token: 0x06000751 RID: 1873 RVA: 0x000810A0 File Offset: 0x0007F2A0
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

		// Token: 0x06000752 RID: 1874 RVA: 0x00081168 File Offset: 0x0007F368
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

		// Token: 0x06000753 RID: 1875 RVA: 0x00081264 File Offset: 0x0007F464
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

		// Token: 0x06000754 RID: 1876 RVA: 0x00081304 File Offset: 0x0007F504
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

		// Token: 0x06000755 RID: 1877 RVA: 0x00081368 File Offset: 0x0007F568
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

		// Token: 0x06000756 RID: 1878 RVA: 0x000813D8 File Offset: 0x0007F5D8
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

		// Token: 0x06000757 RID: 1879 RVA: 0x00081460 File Offset: 0x0007F660
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

		// Token: 0x06000758 RID: 1880 RVA: 0x000814D8 File Offset: 0x0007F6D8
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

		// Token: 0x06000759 RID: 1881 RVA: 0x00081530 File Offset: 0x0007F730
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

		// Token: 0x0600075A RID: 1882 RVA: 0x000815A0 File Offset: 0x0007F7A0
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

		// Token: 0x0600075B RID: 1883 RVA: 0x00081610 File Offset: 0x0007F810
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

		// Token: 0x0600075C RID: 1884 RVA: 0x00081680 File Offset: 0x0007F880
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

		// Token: 0x0600075D RID: 1885 RVA: 0x0008170C File Offset: 0x0007F90C
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

		// Token: 0x0600075E RID: 1886 RVA: 0x000817B4 File Offset: 0x0007F9B4
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

		// Token: 0x0600075F RID: 1887 RVA: 0x00081830 File Offset: 0x0007FA30
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

		// Token: 0x06000760 RID: 1888 RVA: 0x000818A0 File Offset: 0x0007FAA0
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

		// Token: 0x06000761 RID: 1889 RVA: 0x00081910 File Offset: 0x0007FB10
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

		// Token: 0x06000762 RID: 1890 RVA: 0x0008198C File Offset: 0x0007FB8C
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

		// Token: 0x06000763 RID: 1891 RVA: 0x000819F0 File Offset: 0x0007FBF0
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

		// Token: 0x06000764 RID: 1892 RVA: 0x00081A90 File Offset: 0x0007FC90
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

		// Token: 0x06000765 RID: 1893 RVA: 0x00081B20 File Offset: 0x0007FD20
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

		// Token: 0x06000766 RID: 1894 RVA: 0x00081B90 File Offset: 0x0007FD90
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

		// Token: 0x06000767 RID: 1895 RVA: 0x00081C0C File Offset: 0x0007FE0C
		public Message messageNotLogin(sbyte command)
		{
			Message message = new Message(-29);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00081C21 File Offset: 0x0007FE21
		public Message messageNotMap(sbyte command)
		{
			Message message = new Message(-28);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00081C36 File Offset: 0x0007FE36
		public static Message messageSubCommand(sbyte command)
		{
			Message message = new Message(-30);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00081C4C File Offset: 0x0007FE4C
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

		// Token: 0x0600076B RID: 1899 RVA: 0x00081DC8 File Offset: 0x0007FFC8
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

		// Token: 0x0600076C RID: 1900 RVA: 0x00081F70 File Offset: 0x00080170
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

		// Token: 0x0600076D RID: 1901 RVA: 0x00081FC8 File Offset: 0x000801C8
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

		// Token: 0x0600076E RID: 1902 RVA: 0x00082020 File Offset: 0x00080220
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

		// Token: 0x0600076F RID: 1903 RVA: 0x000820A4 File Offset: 0x000802A4
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

		// Token: 0x06000770 RID: 1904 RVA: 0x0008213C File Offset: 0x0008033C
		public void requestChangeMap()
		{
			Message message = new Message(-23);
			this.session.sendMessage(message);
			message.cleanup();
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00082164 File Offset: 0x00080364
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

		// Token: 0x06000772 RID: 1906 RVA: 0x000821AC File Offset: 0x000803AC
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

		// Token: 0x06000773 RID: 1907 RVA: 0x000821F4 File Offset: 0x000803F4
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

		// Token: 0x06000774 RID: 1908 RVA: 0x0008223C File Offset: 0x0008043C
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

		// Token: 0x06000775 RID: 1909 RVA: 0x00082448 File Offset: 0x00080648
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

		// Token: 0x06000776 RID: 1910 RVA: 0x000824C4 File Offset: 0x000806C4
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

		// Token: 0x06000777 RID: 1911 RVA: 0x00082534 File Offset: 0x00080734
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

		// Token: 0x06000778 RID: 1912 RVA: 0x000825B8 File Offset: 0x000807B8
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

		// Token: 0x06000779 RID: 1913 RVA: 0x00082640 File Offset: 0x00080840
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

		// Token: 0x0600077A RID: 1914 RVA: 0x000826D0 File Offset: 0x000808D0
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

		// Token: 0x0600077B RID: 1915 RVA: 0x00082740 File Offset: 0x00080940
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

		// Token: 0x0600077C RID: 1916 RVA: 0x000827A4 File Offset: 0x000809A4
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

		// Token: 0x0600077D RID: 1917 RVA: 0x00082820 File Offset: 0x00080A20
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

		// Token: 0x0600077E RID: 1918 RVA: 0x00082890 File Offset: 0x00080A90
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

		// Token: 0x0600077F RID: 1919 RVA: 0x0008292C File Offset: 0x00080B2C
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

		// Token: 0x06000780 RID: 1920 RVA: 0x0008299C File Offset: 0x00080B9C
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

		// Token: 0x06000781 RID: 1921 RVA: 0x00082A18 File Offset: 0x00080C18
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

		// Token: 0x06000782 RID: 1922 RVA: 0x00082AAC File Offset: 0x00080CAC
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

		// Token: 0x06000783 RID: 1923 RVA: 0x00082B1C File Offset: 0x00080D1C
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

		// Token: 0x06000784 RID: 1924 RVA: 0x00082B80 File Offset: 0x00080D80
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

		// Token: 0x06000785 RID: 1925 RVA: 0x00082D54 File Offset: 0x00080F54
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

		// Token: 0x06000786 RID: 1926 RVA: 0x00082DC4 File Offset: 0x00080FC4
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

		// Token: 0x06000787 RID: 1927 RVA: 0x00082E28 File Offset: 0x00081028
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

		// Token: 0x06000788 RID: 1928 RVA: 0x00082E8C File Offset: 0x0008108C
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

		// Token: 0x06000789 RID: 1929 RVA: 0x00082EFC File Offset: 0x000810FC
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

		// Token: 0x0600078A RID: 1930 RVA: 0x00082F94 File Offset: 0x00081194
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

		// Token: 0x0600078B RID: 1931 RVA: 0x0008302C File Offset: 0x0008122C
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

		// Token: 0x0600078C RID: 1932 RVA: 0x000830B8 File Offset: 0x000812B8
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

		// Token: 0x0600078D RID: 1933 RVA: 0x00083144 File Offset: 0x00081344
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

		// Token: 0x0600078E RID: 1934 RVA: 0x000831A8 File Offset: 0x000813A8
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

		// Token: 0x0600078F RID: 1935 RVA: 0x00083218 File Offset: 0x00081418
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

		// Token: 0x06000790 RID: 1936 RVA: 0x00083288 File Offset: 0x00081488
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

		// Token: 0x06000791 RID: 1937 RVA: 0x000832F8 File Offset: 0x000814F8
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

		// Token: 0x06000792 RID: 1938 RVA: 0x00083380 File Offset: 0x00081580
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

		// Token: 0x06000793 RID: 1939 RVA: 0x000833F0 File Offset: 0x000815F0
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

		// Token: 0x06000794 RID: 1940 RVA: 0x00083460 File Offset: 0x00081660
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

		// Token: 0x06000795 RID: 1941 RVA: 0x000834EC File Offset: 0x000816EC
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

		// Token: 0x06000796 RID: 1942 RVA: 0x0008355C File Offset: 0x0008175C
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

		// Token: 0x06000797 RID: 1943 RVA: 0x000835D8 File Offset: 0x000817D8
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

		// Token: 0x06000798 RID: 1944 RVA: 0x00083654 File Offset: 0x00081854
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

		// Token: 0x06000799 RID: 1945 RVA: 0x000836FC File Offset: 0x000818FC
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

		// Token: 0x0600079A RID: 1946 RVA: 0x0008376C File Offset: 0x0008196C
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

		// Token: 0x0600079B RID: 1947 RVA: 0x000837DC File Offset: 0x000819DC
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

		// Token: 0x0600079C RID: 1948 RVA: 0x0008384C File Offset: 0x00081A4C
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

		// Token: 0x0600079D RID: 1949 RVA: 0x000838F0 File Offset: 0x00081AF0
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

		// Token: 0x0600079E RID: 1950 RVA: 0x00083954 File Offset: 0x00081B54
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

		// Token: 0x0600079F RID: 1951 RVA: 0x000839B8 File Offset: 0x00081BB8
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

		// Token: 0x060007A0 RID: 1952 RVA: 0x00083A10 File Offset: 0x00081C10
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

		// Token: 0x060007A1 RID: 1953 RVA: 0x00083A74 File Offset: 0x00081C74
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

		// Token: 0x060007A2 RID: 1954 RVA: 0x00083AE4 File Offset: 0x00081CE4
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

		// Token: 0x060007A3 RID: 1955 RVA: 0x00083B54 File Offset: 0x00081D54
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

		// Token: 0x060007A4 RID: 1956 RVA: 0x00083BC4 File Offset: 0x00081DC4
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

		// Token: 0x060007A5 RID: 1957 RVA: 0x00083C48 File Offset: 0x00081E48
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

		// Token: 0x060007A6 RID: 1958 RVA: 0x00083D4C File Offset: 0x00081F4C
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

		// Token: 0x060007A7 RID: 1959 RVA: 0x00083DB0 File Offset: 0x00081FB0
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

		// Token: 0x060007A8 RID: 1960 RVA: 0x00083DFC File Offset: 0x00081FFC
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

		// Token: 0x060007A9 RID: 1961 RVA: 0x00083E54 File Offset: 0x00082054
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

		// Token: 0x060007AA RID: 1962 RVA: 0x00083EB8 File Offset: 0x000820B8
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

		// Token: 0x060007AB RID: 1963 RVA: 0x00083F30 File Offset: 0x00082130
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

		// Token: 0x060007AC RID: 1964 RVA: 0x00084060 File Offset: 0x00082260
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

		// Token: 0x060007AD RID: 1965 RVA: 0x000840E8 File Offset: 0x000822E8
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

		// Token: 0x060007AE RID: 1966 RVA: 0x00084158 File Offset: 0x00082358
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

		// Token: 0x060007AF RID: 1967 RVA: 0x000841B0 File Offset: 0x000823B0
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

		// Token: 0x060007B0 RID: 1968 RVA: 0x00084214 File Offset: 0x00082414
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

		// Token: 0x060007B1 RID: 1969 RVA: 0x0008426C File Offset: 0x0008246C
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

		// Token: 0x060007B2 RID: 1970 RVA: 0x000842D8 File Offset: 0x000824D8
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

		// Token: 0x060007B3 RID: 1971 RVA: 0x000843F0 File Offset: 0x000825F0
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

		// Token: 0x060007B4 RID: 1972 RVA: 0x00084484 File Offset: 0x00082684
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

		// Token: 0x060007B5 RID: 1973 RVA: 0x000844FC File Offset: 0x000826FC
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

		// Token: 0x060007B6 RID: 1974 RVA: 0x00084554 File Offset: 0x00082754
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

		// Token: 0x060007B7 RID: 1975 RVA: 0x000845C0 File Offset: 0x000827C0
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

		// Token: 0x060007B8 RID: 1976 RVA: 0x00084630 File Offset: 0x00082830
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

		// Token: 0x060007B9 RID: 1977 RVA: 0x000846AC File Offset: 0x000828AC
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

		// Token: 0x060007BA RID: 1978 RVA: 0x00084704 File Offset: 0x00082904
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

		// Token: 0x060007BB RID: 1979 RVA: 0x0008476C File Offset: 0x0008296C
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

		// Token: 0x060007BC RID: 1980 RVA: 0x000847D4 File Offset: 0x000829D4
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

		// Token: 0x060007BD RID: 1981 RVA: 0x00084820 File Offset: 0x00082A20
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

		// Token: 0x060007BE RID: 1982 RVA: 0x000034B9 File Offset: 0x000016B9
		public void SendRemoteAddress()
		{
		}

		// Token: 0x04000FBF RID: 4031
		private ISession session = Session_ME.gI();

		// Token: 0x04000FC0 RID: 4032
		protected static Service instance;

		// Token: 0x04000FC1 RID: 4033
		public static long curCheckController;

		// Token: 0x04000FC2 RID: 4034
		public static long curCheckMap;

		// Token: 0x04000FC3 RID: 4035
		public static long logController;

		// Token: 0x04000FC4 RID: 4036
		public static long logMap;

		// Token: 0x04000FC5 RID: 4037
		public static bool reciveFromMainSession;
	}
}
