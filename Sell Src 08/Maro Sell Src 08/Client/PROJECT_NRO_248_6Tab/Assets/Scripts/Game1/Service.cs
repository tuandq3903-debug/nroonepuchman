using System;
using Game1.Assets.src.g;

namespace Game1
{
	// Token: 0x020004D8 RID: 1240
	public class Service
	{
		// Token: 0x06003780 RID: 14208 RVA: 0x0036A1EC File Offset: 0x003683EC
		public static Service gI()
		{
			if (Service.instance == null)
			{
				Service.instance = new Service();
			}
			return Service.instance;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x0036A204 File Offset: 0x00368404
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

		// Token: 0x06003782 RID: 14210 RVA: 0x0036A268 File Offset: 0x00368468
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

		// Token: 0x06003783 RID: 14211 RVA: 0x0036A2D8 File Offset: 0x003684D8
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

		// Token: 0x06003784 RID: 14212 RVA: 0x0036A3A0 File Offset: 0x003685A0
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

		// Token: 0x06003785 RID: 14213 RVA: 0x0036A444 File Offset: 0x00368644
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

		// Token: 0x06003786 RID: 14214 RVA: 0x0036A50C File Offset: 0x0036870C
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

		// Token: 0x06003787 RID: 14215 RVA: 0x0036A608 File Offset: 0x00368808
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

		// Token: 0x06003788 RID: 14216 RVA: 0x0036A6A8 File Offset: 0x003688A8
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

		// Token: 0x06003789 RID: 14217 RVA: 0x0036A70C File Offset: 0x0036890C
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

		// Token: 0x0600378A RID: 14218 RVA: 0x0036A77C File Offset: 0x0036897C
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

		// Token: 0x0600378B RID: 14219 RVA: 0x0036A804 File Offset: 0x00368A04
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

		// Token: 0x0600378C RID: 14220 RVA: 0x0036A87C File Offset: 0x00368A7C
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

		// Token: 0x0600378D RID: 14221 RVA: 0x0036A8D4 File Offset: 0x00368AD4
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

		// Token: 0x0600378E RID: 14222 RVA: 0x0036A944 File Offset: 0x00368B44
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

		// Token: 0x0600378F RID: 14223 RVA: 0x0036A9B4 File Offset: 0x00368BB4
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

		// Token: 0x06003790 RID: 14224 RVA: 0x0036AA24 File Offset: 0x00368C24
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

		// Token: 0x06003791 RID: 14225 RVA: 0x0036AAB0 File Offset: 0x00368CB0
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

		// Token: 0x06003792 RID: 14226 RVA: 0x0036AB58 File Offset: 0x00368D58
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

		// Token: 0x06003793 RID: 14227 RVA: 0x0036ABD4 File Offset: 0x00368DD4
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

		// Token: 0x06003794 RID: 14228 RVA: 0x0036AC44 File Offset: 0x00368E44
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

		// Token: 0x06003795 RID: 14229 RVA: 0x0036ACB4 File Offset: 0x00368EB4
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

		// Token: 0x06003796 RID: 14230 RVA: 0x0036AD30 File Offset: 0x00368F30
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

		// Token: 0x06003797 RID: 14231 RVA: 0x0036AD94 File Offset: 0x00368F94
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

		// Token: 0x06003798 RID: 14232 RVA: 0x0036AE34 File Offset: 0x00369034
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

		// Token: 0x06003799 RID: 14233 RVA: 0x0036AEC4 File Offset: 0x003690C4
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

		// Token: 0x0600379A RID: 14234 RVA: 0x0036AF34 File Offset: 0x00369134
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

		// Token: 0x0600379B RID: 14235 RVA: 0x0036AFB0 File Offset: 0x003691B0
		public Message messageNotLogin(sbyte command)
		{
			Message message = new Message(-29);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x0036AFC5 File Offset: 0x003691C5
		public Message messageNotMap(sbyte command)
		{
			Message message = new Message(-28);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x0036AFDA File Offset: 0x003691DA
		public static Message messageSubCommand(sbyte command)
		{
			Message message = new Message(-30);
			message.writer().writeByte(command);
			return message;
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x0036AFF0 File Offset: 0x003691F0
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

		// Token: 0x0600379F RID: 14239 RVA: 0x0036B16C File Offset: 0x0036936C
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

		// Token: 0x060037A0 RID: 14240 RVA: 0x0036B314 File Offset: 0x00369514
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

		// Token: 0x060037A1 RID: 14241 RVA: 0x0036B36C File Offset: 0x0036956C
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

		// Token: 0x060037A2 RID: 14242 RVA: 0x0036B3C4 File Offset: 0x003695C4
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

		// Token: 0x060037A3 RID: 14243 RVA: 0x0036B448 File Offset: 0x00369648
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

		// Token: 0x060037A4 RID: 14244 RVA: 0x0036B4E0 File Offset: 0x003696E0
		public void requestChangeMap()
		{
			Message message = new Message(-23);
			this.session.sendMessage(message);
			message.cleanup();
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x0036B508 File Offset: 0x00369708
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

		// Token: 0x060037A6 RID: 14246 RVA: 0x0036B550 File Offset: 0x00369750
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

		// Token: 0x060037A7 RID: 14247 RVA: 0x0036B598 File Offset: 0x00369798
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

		// Token: 0x060037A8 RID: 14248 RVA: 0x0036B5E0 File Offset: 0x003697E0
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

		// Token: 0x060037A9 RID: 14249 RVA: 0x0036B7EC File Offset: 0x003699EC
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

		// Token: 0x060037AA RID: 14250 RVA: 0x0036B868 File Offset: 0x00369A68
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

		// Token: 0x060037AB RID: 14251 RVA: 0x0036B8D8 File Offset: 0x00369AD8
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

		// Token: 0x060037AC RID: 14252 RVA: 0x0036B95C File Offset: 0x00369B5C
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

		// Token: 0x060037AD RID: 14253 RVA: 0x0036B9E4 File Offset: 0x00369BE4
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

		// Token: 0x060037AE RID: 14254 RVA: 0x0036BA74 File Offset: 0x00369C74
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

		// Token: 0x060037AF RID: 14255 RVA: 0x0036BAE4 File Offset: 0x00369CE4
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

		// Token: 0x060037B0 RID: 14256 RVA: 0x0036BB48 File Offset: 0x00369D48
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

		// Token: 0x060037B1 RID: 14257 RVA: 0x0036BBC4 File Offset: 0x00369DC4
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

		// Token: 0x060037B2 RID: 14258 RVA: 0x0036BC34 File Offset: 0x00369E34
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

		// Token: 0x060037B3 RID: 14259 RVA: 0x0036BCD0 File Offset: 0x00369ED0
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

		// Token: 0x060037B4 RID: 14260 RVA: 0x0036BD40 File Offset: 0x00369F40
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

		// Token: 0x060037B5 RID: 14261 RVA: 0x0036BDBC File Offset: 0x00369FBC
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

		// Token: 0x060037B6 RID: 14262 RVA: 0x0036BE50 File Offset: 0x0036A050
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

		// Token: 0x060037B7 RID: 14263 RVA: 0x0036BEC0 File Offset: 0x0036A0C0
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

		// Token: 0x060037B8 RID: 14264 RVA: 0x0036BF24 File Offset: 0x0036A124
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

		// Token: 0x060037B9 RID: 14265 RVA: 0x0036C0F8 File Offset: 0x0036A2F8
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

		// Token: 0x060037BA RID: 14266 RVA: 0x0036C168 File Offset: 0x0036A368
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

		// Token: 0x060037BB RID: 14267 RVA: 0x0036C1CC File Offset: 0x0036A3CC
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

		// Token: 0x060037BC RID: 14268 RVA: 0x0036C230 File Offset: 0x0036A430
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

		// Token: 0x060037BD RID: 14269 RVA: 0x0036C2A0 File Offset: 0x0036A4A0
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

		// Token: 0x060037BE RID: 14270 RVA: 0x0036C338 File Offset: 0x0036A538
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

		// Token: 0x060037BF RID: 14271 RVA: 0x0036C3D0 File Offset: 0x0036A5D0
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

		// Token: 0x060037C0 RID: 14272 RVA: 0x0036C45C File Offset: 0x0036A65C
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

		// Token: 0x060037C1 RID: 14273 RVA: 0x0036C4E8 File Offset: 0x0036A6E8
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

		// Token: 0x060037C2 RID: 14274 RVA: 0x0036C54C File Offset: 0x0036A74C
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

		// Token: 0x060037C3 RID: 14275 RVA: 0x0036C5BC File Offset: 0x0036A7BC
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

		// Token: 0x060037C4 RID: 14276 RVA: 0x0036C62C File Offset: 0x0036A82C
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

		// Token: 0x060037C5 RID: 14277 RVA: 0x0036C69C File Offset: 0x0036A89C
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

		// Token: 0x060037C6 RID: 14278 RVA: 0x0036C724 File Offset: 0x0036A924
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

		// Token: 0x060037C7 RID: 14279 RVA: 0x0036C794 File Offset: 0x0036A994
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

		// Token: 0x060037C8 RID: 14280 RVA: 0x0036C804 File Offset: 0x0036AA04
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

		// Token: 0x060037C9 RID: 14281 RVA: 0x0036C890 File Offset: 0x0036AA90
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

		// Token: 0x060037CA RID: 14282 RVA: 0x0036C900 File Offset: 0x0036AB00
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

		// Token: 0x060037CB RID: 14283 RVA: 0x0036C97C File Offset: 0x0036AB7C
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

		// Token: 0x060037CC RID: 14284 RVA: 0x0036C9F8 File Offset: 0x0036ABF8
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

		// Token: 0x060037CD RID: 14285 RVA: 0x0036CAA0 File Offset: 0x0036ACA0
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

		// Token: 0x060037CE RID: 14286 RVA: 0x0036CB10 File Offset: 0x0036AD10
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

		// Token: 0x060037CF RID: 14287 RVA: 0x0036CB80 File Offset: 0x0036AD80
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

		// Token: 0x060037D0 RID: 14288 RVA: 0x0036CBF0 File Offset: 0x0036ADF0
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

		// Token: 0x060037D1 RID: 14289 RVA: 0x0036CC94 File Offset: 0x0036AE94
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

		// Token: 0x060037D2 RID: 14290 RVA: 0x0036CCF8 File Offset: 0x0036AEF8
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

		// Token: 0x060037D3 RID: 14291 RVA: 0x0036CD5C File Offset: 0x0036AF5C
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

		// Token: 0x060037D4 RID: 14292 RVA: 0x0036CDB4 File Offset: 0x0036AFB4
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

		// Token: 0x060037D5 RID: 14293 RVA: 0x0036CE18 File Offset: 0x0036B018
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

		// Token: 0x060037D6 RID: 14294 RVA: 0x0036CE88 File Offset: 0x0036B088
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

		// Token: 0x060037D7 RID: 14295 RVA: 0x0036CEF8 File Offset: 0x0036B0F8
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

		// Token: 0x060037D8 RID: 14296 RVA: 0x0036CF68 File Offset: 0x0036B168
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

		// Token: 0x060037D9 RID: 14297 RVA: 0x0036CFEC File Offset: 0x0036B1EC
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

		// Token: 0x060037DA RID: 14298 RVA: 0x0036D0F0 File Offset: 0x0036B2F0
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

		// Token: 0x060037DB RID: 14299 RVA: 0x0036D154 File Offset: 0x0036B354
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

		// Token: 0x060037DC RID: 14300 RVA: 0x0036D1A0 File Offset: 0x0036B3A0
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

		// Token: 0x060037DD RID: 14301 RVA: 0x0036D1F8 File Offset: 0x0036B3F8
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

		// Token: 0x060037DE RID: 14302 RVA: 0x0036D25C File Offset: 0x0036B45C
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

		// Token: 0x060037DF RID: 14303 RVA: 0x0036D2D4 File Offset: 0x0036B4D4
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

		// Token: 0x060037E0 RID: 14304 RVA: 0x0036D404 File Offset: 0x0036B604
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

		// Token: 0x060037E1 RID: 14305 RVA: 0x0036D48C File Offset: 0x0036B68C
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

		// Token: 0x060037E2 RID: 14306 RVA: 0x0036D4FC File Offset: 0x0036B6FC
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

		// Token: 0x060037E3 RID: 14307 RVA: 0x0036D554 File Offset: 0x0036B754
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

		// Token: 0x060037E4 RID: 14308 RVA: 0x0036D5B8 File Offset: 0x0036B7B8
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

		// Token: 0x060037E5 RID: 14309 RVA: 0x0036D610 File Offset: 0x0036B810
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

		// Token: 0x060037E6 RID: 14310 RVA: 0x0036D67C File Offset: 0x0036B87C
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

		// Token: 0x060037E7 RID: 14311 RVA: 0x0036D794 File Offset: 0x0036B994
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

		// Token: 0x060037E8 RID: 14312 RVA: 0x0036D828 File Offset: 0x0036BA28
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

		// Token: 0x060037E9 RID: 14313 RVA: 0x0036D8A0 File Offset: 0x0036BAA0
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

		// Token: 0x060037EA RID: 14314 RVA: 0x0036D8F8 File Offset: 0x0036BAF8
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

		// Token: 0x060037EB RID: 14315 RVA: 0x0036D964 File Offset: 0x0036BB64
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

		// Token: 0x060037EC RID: 14316 RVA: 0x0036D9D4 File Offset: 0x0036BBD4
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

		// Token: 0x060037ED RID: 14317 RVA: 0x0036DA50 File Offset: 0x0036BC50
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

		// Token: 0x060037EE RID: 14318 RVA: 0x0036DAA8 File Offset: 0x0036BCA8
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

		// Token: 0x060037EF RID: 14319 RVA: 0x0036DB10 File Offset: 0x0036BD10
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

		// Token: 0x060037F0 RID: 14320 RVA: 0x0036DB78 File Offset: 0x0036BD78
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

		// Token: 0x060037F1 RID: 14321 RVA: 0x0036DBC4 File Offset: 0x0036BDC4
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

		// Token: 0x060037F2 RID: 14322 RVA: 0x000034B9 File Offset: 0x000016B9
		public void SendRemoteAddress()
		{
		}

		// Token: 0x04006C3A RID: 27706
		private ISession session = Session_ME.gI();

		// Token: 0x04006C3B RID: 27707
		protected static Service instance;

		// Token: 0x04006C3C RID: 27708
		public static long curCheckController;

		// Token: 0x04006C3D RID: 27709
		public static long curCheckMap;

		// Token: 0x04006C3E RID: 27710
		public static long logController;

		// Token: 0x04006C3F RID: 27711
		public static long logMap;

		// Token: 0x04006C40 RID: 27712
		public static bool reciveFromMainSession;
	}
}
