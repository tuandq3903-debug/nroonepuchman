using System;
using Game4.Assets.src.g;

namespace Game4
{
	// Token: 0x02000250 RID: 592
	public class Service
	{
		// Token: 0x06001A94 RID: 6804 RVA: 0x001AB000 File Offset: 0x001A9200
		public static Service gI()
		{
			if (Service.instance == null)
			{
				Service.instance = new Service();
			}
			return Service.instance;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x001AB018 File Offset: 0x001A9218
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

		// Token: 0x06001A96 RID: 6806 RVA: 0x001AB07C File Offset: 0x001A927C
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

		// Token: 0x06001A97 RID: 6807 RVA: 0x001AB0EC File Offset: 0x001A92EC
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

		// Token: 0x06001A98 RID: 6808 RVA: 0x001AB1B4 File Offset: 0x001A93B4
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

		// Token: 0x06001A99 RID: 6809 RVA: 0x001AB258 File Offset: 0x001A9458
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

		// Token: 0x06001A9A RID: 6810 RVA: 0x001AB320 File Offset: 0x001A9520
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

		// Token: 0x06001A9B RID: 6811 RVA: 0x001AB41C File Offset: 0x001A961C
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

		// Token: 0x06001A9C RID: 6812 RVA: 0x001AB4BC File Offset: 0x001A96BC
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

		// Token: 0x06001A9D RID: 6813 RVA: 0x001AB520 File Offset: 0x001A9720
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

		// Token: 0x06001A9E RID: 6814 RVA: 0x001AB590 File Offset: 0x001A9790
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

		// Token: 0x06001A9F RID: 6815 RVA: 0x001AB618 File Offset: 0x001A9818
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

		// Token: 0x06001AA0 RID: 6816 RVA: 0x001AB690 File Offset: 0x001A9890
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

		// Token: 0x06001AA1 RID: 6817 RVA: 0x001AB6E8 File Offset: 0x001A98E8
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

		// Token: 0x06001AA2 RID: 6818 RVA: 0x001AB758 File Offset: 0x001A9958
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

		// Token: 0x06001AA3 RID: 6819 RVA: 0x001AB7C8 File Offset: 0x001A99C8
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

		// Token: 0x06001AA4 RID: 6820 RVA: 0x001AB838 File Offset: 0x001A9A38
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

		// Token: 0x06001AA5 RID: 6821 RVA: 0x001AB8C4 File Offset: 0x001A9AC4
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

		// Token: 0x06001AA6 RID: 6822 RVA: 0x001AB96C File Offset: 0x001A9B6C
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

		// Token: 0x06001AA7 RID: 6823 RVA: 0x001AB9E8 File Offset: 0x001A9BE8
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

		// Token: 0x06001AA8 RID: 6824 RVA: 0x001ABA58 File Offset: 0x001A9C58
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

		// Token: 0x06001AA9 RID: 6825 RVA: 0x001ABAC8 File Offset: 0x001A9CC8
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

		// Token: 0x06001AAA RID: 6826 RVA: 0x001ABB44 File Offset: 0x001A9D44
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

		// Token: 0x06001AAB RID: 6827 RVA: 0x001ABBA8 File Offset: 0x001A9DA8
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

		// Token: 0x06001AAC RID: 6828 RVA: 0x001ABC48 File Offset: 0x001A9E48
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

		// Token: 0x06001AAD RID: 6829 RVA: 0x001ABCD8 File Offset: 0x001A9ED8
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

		// Token: 0x06001AAE RID: 6830 RVA: 0x001ABD48 File Offset: 0x001A9F48
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

		// Token: 0x06001AAF RID: 6831 RVA: 0x001ABDC4 File Offset: 0x001A9FC4
		public Message messageNotLogin(sbyte command)
		{
			Message message = new Message(-29);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x001ABDD9 File Offset: 0x001A9FD9
		public Message messageNotMap(sbyte command)
		{
			Message message = new Message(-28);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x001ABDEE File Offset: 0x001A9FEE
		public static Message messageSubCommand(sbyte command)
		{
			Message message = new Message(-30);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x001ABE04 File Offset: 0x001AA004
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

		// Token: 0x06001AB3 RID: 6835 RVA: 0x001ABF80 File Offset: 0x001AA180
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

		// Token: 0x06001AB4 RID: 6836 RVA: 0x001AC128 File Offset: 0x001AA328
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

		// Token: 0x06001AB5 RID: 6837 RVA: 0x001AC180 File Offset: 0x001AA380
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

		// Token: 0x06001AB6 RID: 6838 RVA: 0x001AC1D8 File Offset: 0x001AA3D8
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

		// Token: 0x06001AB7 RID: 6839 RVA: 0x001AC25C File Offset: 0x001AA45C
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

		// Token: 0x06001AB8 RID: 6840 RVA: 0x001AC2F4 File Offset: 0x001AA4F4
		public void requestChangeMap()
		{
			Message message = new Message(-23);
			this.session.sendMessage(message);
			message.cleanup();
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x001AC31C File Offset: 0x001AA51C
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

		// Token: 0x06001ABA RID: 6842 RVA: 0x001AC364 File Offset: 0x001AA564
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

		// Token: 0x06001ABB RID: 6843 RVA: 0x001AC3AC File Offset: 0x001AA5AC
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

		// Token: 0x06001ABC RID: 6844 RVA: 0x001AC3F4 File Offset: 0x001AA5F4
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

		// Token: 0x06001ABD RID: 6845 RVA: 0x001AC600 File Offset: 0x001AA800
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

		// Token: 0x06001ABE RID: 6846 RVA: 0x001AC67C File Offset: 0x001AA87C
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

		// Token: 0x06001ABF RID: 6847 RVA: 0x001AC6EC File Offset: 0x001AA8EC
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

		// Token: 0x06001AC0 RID: 6848 RVA: 0x001AC770 File Offset: 0x001AA970
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

		// Token: 0x06001AC1 RID: 6849 RVA: 0x001AC7F8 File Offset: 0x001AA9F8
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

		// Token: 0x06001AC2 RID: 6850 RVA: 0x001AC888 File Offset: 0x001AAA88
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

		// Token: 0x06001AC3 RID: 6851 RVA: 0x001AC8F8 File Offset: 0x001AAAF8
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

		// Token: 0x06001AC4 RID: 6852 RVA: 0x001AC95C File Offset: 0x001AAB5C
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

		// Token: 0x06001AC5 RID: 6853 RVA: 0x001AC9D8 File Offset: 0x001AABD8
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

		// Token: 0x06001AC6 RID: 6854 RVA: 0x001ACA48 File Offset: 0x001AAC48
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

		// Token: 0x06001AC7 RID: 6855 RVA: 0x001ACAE4 File Offset: 0x001AACE4
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

		// Token: 0x06001AC8 RID: 6856 RVA: 0x001ACB54 File Offset: 0x001AAD54
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

		// Token: 0x06001AC9 RID: 6857 RVA: 0x001ACBD0 File Offset: 0x001AADD0
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

		// Token: 0x06001ACA RID: 6858 RVA: 0x001ACC64 File Offset: 0x001AAE64
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

		// Token: 0x06001ACB RID: 6859 RVA: 0x001ACCD4 File Offset: 0x001AAED4
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

		// Token: 0x06001ACC RID: 6860 RVA: 0x001ACD38 File Offset: 0x001AAF38
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

		// Token: 0x06001ACD RID: 6861 RVA: 0x001ACF0C File Offset: 0x001AB10C
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

		// Token: 0x06001ACE RID: 6862 RVA: 0x001ACF7C File Offset: 0x001AB17C
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

		// Token: 0x06001ACF RID: 6863 RVA: 0x001ACFE0 File Offset: 0x001AB1E0
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

		// Token: 0x06001AD0 RID: 6864 RVA: 0x001AD044 File Offset: 0x001AB244
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

		// Token: 0x06001AD1 RID: 6865 RVA: 0x001AD0B4 File Offset: 0x001AB2B4
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

		// Token: 0x06001AD2 RID: 6866 RVA: 0x001AD14C File Offset: 0x001AB34C
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

		// Token: 0x06001AD3 RID: 6867 RVA: 0x001AD1E4 File Offset: 0x001AB3E4
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

		// Token: 0x06001AD4 RID: 6868 RVA: 0x001AD270 File Offset: 0x001AB470
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

		// Token: 0x06001AD5 RID: 6869 RVA: 0x001AD2FC File Offset: 0x001AB4FC
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

		// Token: 0x06001AD6 RID: 6870 RVA: 0x001AD360 File Offset: 0x001AB560
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

		// Token: 0x06001AD7 RID: 6871 RVA: 0x001AD3D0 File Offset: 0x001AB5D0
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

		// Token: 0x06001AD8 RID: 6872 RVA: 0x001AD440 File Offset: 0x001AB640
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

		// Token: 0x06001AD9 RID: 6873 RVA: 0x001AD4B0 File Offset: 0x001AB6B0
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

		// Token: 0x06001ADA RID: 6874 RVA: 0x001AD538 File Offset: 0x001AB738
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

		// Token: 0x06001ADB RID: 6875 RVA: 0x001AD5A8 File Offset: 0x001AB7A8
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

		// Token: 0x06001ADC RID: 6876 RVA: 0x001AD618 File Offset: 0x001AB818
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

		// Token: 0x06001ADD RID: 6877 RVA: 0x001AD6A4 File Offset: 0x001AB8A4
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

		// Token: 0x06001ADE RID: 6878 RVA: 0x001AD714 File Offset: 0x001AB914
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

		// Token: 0x06001ADF RID: 6879 RVA: 0x001AD790 File Offset: 0x001AB990
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

		// Token: 0x06001AE0 RID: 6880 RVA: 0x001AD80C File Offset: 0x001ABA0C
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

		// Token: 0x06001AE1 RID: 6881 RVA: 0x001AD8B4 File Offset: 0x001ABAB4
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

		// Token: 0x06001AE2 RID: 6882 RVA: 0x001AD924 File Offset: 0x001ABB24
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

		// Token: 0x06001AE3 RID: 6883 RVA: 0x001AD994 File Offset: 0x001ABB94
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

		// Token: 0x06001AE4 RID: 6884 RVA: 0x001ADA04 File Offset: 0x001ABC04
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

		// Token: 0x06001AE5 RID: 6885 RVA: 0x001ADAA8 File Offset: 0x001ABCA8
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

		// Token: 0x06001AE6 RID: 6886 RVA: 0x001ADB0C File Offset: 0x001ABD0C
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

		// Token: 0x06001AE7 RID: 6887 RVA: 0x001ADB70 File Offset: 0x001ABD70
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

		// Token: 0x06001AE8 RID: 6888 RVA: 0x001ADBC8 File Offset: 0x001ABDC8
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

		// Token: 0x06001AE9 RID: 6889 RVA: 0x001ADC2C File Offset: 0x001ABE2C
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

		// Token: 0x06001AEA RID: 6890 RVA: 0x001ADC9C File Offset: 0x001ABE9C
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

		// Token: 0x06001AEB RID: 6891 RVA: 0x001ADD0C File Offset: 0x001ABF0C
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

		// Token: 0x06001AEC RID: 6892 RVA: 0x001ADD7C File Offset: 0x001ABF7C
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

		// Token: 0x06001AED RID: 6893 RVA: 0x001ADE00 File Offset: 0x001AC000
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

		// Token: 0x06001AEE RID: 6894 RVA: 0x001ADF04 File Offset: 0x001AC104
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

		// Token: 0x06001AEF RID: 6895 RVA: 0x001ADF68 File Offset: 0x001AC168
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

		// Token: 0x06001AF0 RID: 6896 RVA: 0x001ADFB4 File Offset: 0x001AC1B4
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

		// Token: 0x06001AF1 RID: 6897 RVA: 0x001AE00C File Offset: 0x001AC20C
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

		// Token: 0x06001AF2 RID: 6898 RVA: 0x001AE070 File Offset: 0x001AC270
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

		// Token: 0x06001AF3 RID: 6899 RVA: 0x001AE0E8 File Offset: 0x001AC2E8
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

		// Token: 0x06001AF4 RID: 6900 RVA: 0x001AE218 File Offset: 0x001AC418
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

		// Token: 0x06001AF5 RID: 6901 RVA: 0x001AE2A0 File Offset: 0x001AC4A0
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

		// Token: 0x06001AF6 RID: 6902 RVA: 0x001AE310 File Offset: 0x001AC510
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

		// Token: 0x06001AF7 RID: 6903 RVA: 0x001AE368 File Offset: 0x001AC568
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

		// Token: 0x06001AF8 RID: 6904 RVA: 0x001AE3CC File Offset: 0x001AC5CC
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

		// Token: 0x06001AF9 RID: 6905 RVA: 0x001AE424 File Offset: 0x001AC624
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

		// Token: 0x06001AFA RID: 6906 RVA: 0x001AE490 File Offset: 0x001AC690
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

		// Token: 0x06001AFB RID: 6907 RVA: 0x001AE5A8 File Offset: 0x001AC7A8
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

		// Token: 0x06001AFC RID: 6908 RVA: 0x001AE63C File Offset: 0x001AC83C
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

		// Token: 0x06001AFD RID: 6909 RVA: 0x001AE6B4 File Offset: 0x001AC8B4
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

		// Token: 0x06001AFE RID: 6910 RVA: 0x001AE70C File Offset: 0x001AC90C
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

		// Token: 0x06001AFF RID: 6911 RVA: 0x001AE778 File Offset: 0x001AC978
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

		// Token: 0x06001B00 RID: 6912 RVA: 0x001AE7E8 File Offset: 0x001AC9E8
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

		// Token: 0x06001B01 RID: 6913 RVA: 0x001AE864 File Offset: 0x001ACA64
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

		// Token: 0x06001B02 RID: 6914 RVA: 0x001AE8BC File Offset: 0x001ACABC
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

		// Token: 0x06001B03 RID: 6915 RVA: 0x001AE924 File Offset: 0x001ACB24
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

		// Token: 0x06001B04 RID: 6916 RVA: 0x001AE98C File Offset: 0x001ACB8C
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

		// Token: 0x06001B05 RID: 6917 RVA: 0x001AE9D8 File Offset: 0x001ACBD8
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

		// Token: 0x06001B06 RID: 6918 RVA: 0x000034B9 File Offset: 0x000016B9
		public void SendRemoteAddress()
		{
		}

		// Token: 0x040034BD RID: 13501
		private ISession session = Session_ME.gI();

		// Token: 0x040034BE RID: 13502
		protected static Service instance;

		// Token: 0x040034BF RID: 13503
		public static long curCheckController;

		// Token: 0x040034C0 RID: 13504
		public static long curCheckMap;

		// Token: 0x040034C1 RID: 13505
		public static long logController;

		// Token: 0x040034C2 RID: 13506
		public static long logMap;

		// Token: 0x040034C3 RID: 13507
		public static bool reciveFromMainSession;
	}
}
