using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Game3.Mod;
using Game3.Mod.XMAP;
using UnityEngine;
using UnityEngine.Networking;

namespace Game3
{
	// Token: 0x020004A7 RID: 1191
	public class ModFunc : IActionListener
	{
		// Token: 0x060034D4 RID: 13524 RVA: 0x0033A22C File Offset: 0x0033842C
		public static void InitButtonPositions()
		{
			if (ModFunc.buttonPositions.Count != 0)
			{
				return;
			}
			foreach (KeyValuePair<string, ModFunc.Point> kvp in ModFunc.defaultButtonPositions)
			{
				ModFunc.buttonPositions[kvp.Key] = new ModFunc.Point(kvp.Value.x, kvp.Value.y);
			}
		}

		// Token: 0x060034D5 RID: 13525 RVA: 0x0033A2B4 File Offset: 0x003384B4
		public static ModFunc GI()
		{
			return ModFunc.Instance ?? new ModFunc();
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x0033A2C4 File Offset: 0x003384C4
		public void OpenMenu()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Bản đồ", 883));
			myVector.addElement(new Command("Luyện tập", 45));
			myVector.addElement(new Command("Nhặt đồ", 89));
			myVector.addElement(new Command("Đệ tử", 16));
			myVector.addElement(new Command("BOSS", 32));
			myVector.addElement(new Command("Khác", 53));
			GameCanvas.menu.startAt(myVector, 4);
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x0033A354 File Offset: 0x00338554
		public static Color GetColor()
		{
			string[] array = ModFunc.backgroundColor.Split(' ', StringSplitOptions.None);
			return new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x0033A38C File Offset: 0x0033858C
		public bool UpdateKey(int key)
		{
			switch (key)
			{
				case 97:
					this.MoveTo(Char.myCharz().cx - 100, Char.myCharz().cy);
					return true;
				case 99:
					this.UseItem(194);
					return true;
				case 100:
					this.MoveTo(Char.myCharz().cx + 100, Char.myCharz().cy);
					return true;
				case 101:
					Service.gI().friend(0, -1);
					InfoDlg.showWait();
					return true;
				case 102:
					this.UsePorata();
					return true;
				case 103:
					if (Char.myCharz().charFocus != null)
					{
						Service.gI().giaodich(0, Char.myCharz().charFocus.charID, -1, -1);
						GameScr.info1.addInfo("Đã gửi lời mời giao dịch đến " + Char.myCharz().charFocus.cName, 0);
						return true;
					}
					return true;
				case 104:
					GameScr.gI().onChatFromMe("ukhu", string.Empty);
					return true;
				case 106:
					ManualXmap.GI().LoadMapLeft();
					return true;
				case 107:
					ManualXmap.GI().LoadMapCenter();
					return true;
				case 108:
					ManualXmap.GI().LoadMapRight();
					return true;
				case 109:
					this.userOpenZones = true;
					Service.gI().openUIZone();
					return true;
				case 110:
					PickMob.IsAutoPickItems = !PickMob.IsAutoPickItems;
					GameScr.info1.addInfo("Tự động nhặt: " + (PickMob.IsAutoPickItems ? "Bật" : "Tắt"), 0);
					return true;
				case 115:
					this.MoveTo(Char.myCharz().cx, Char.myCharz().cy + 100);
					return true;
				case 116:
					this.UseItem(521);
					return true;
				case 117:
					this.perform(42, null);
					return true;
				case 119:
					this.MoveTo(Char.myCharz().cx, Char.myCharz().cy - 100);
					return true;
				case 120:
					this.OpenMenu();
					return true;
			}
			return false;
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x0033A5C4 File Offset: 0x003387C4
		public void LoadGame()
		{
			if (!ModFunc.loadedMusic)
			{
				ModFunc.InitMusic();
				ModFunc.loadedMusic = true;
			}
			Time.timeScale = 1.5f;
			this.listSkillsAuto.Clear();
			this.listItemAuto.Clear();
			this.isHighFps = (Rms.loadRMSInt("isHighFps") != 0);
			ModFunc.isInventory = (Rms.loadRMSInt("inventory") == 1);
			ModFunc.isEffectInven = (Rms.loadRMSInt("effectinven") == 1);
			ModFunc.GiamDungLuong = (Rms.loadRMSInt("background") == 1);
			ModFunc.AnPlayer = (Rms.loadRMSInt("anplayer") == 1);
			this.autoWakeUp = (Rms.loadRMSInt("autoWakeUp") == 1);
			if (Rms.loadRMSInt("new logo") != 1)
			{
				Rms.saveRMSInt("logoGif", 1);
				Rms.saveRMSInt("logo", 1);
				Rms.saveRMSInt("new logo", 1);
			}
			if (!ModFunc.ModNotLogo)
			{
				ModFunc.isLogo = (Rms.loadRMSInt("logo") == 1);
				ModFunc.isLogoGif = (Rms.loadRMSInt("logoGif") == 1);
				if (ModFunc.isLogo)
				{
					if (ModFunc.isLogoGif)
					{
						ModFunc.LoadLogoGif();
					}
					else
					{
						ModFunc.LoadLogoImages();
					}
				}
			}
			this.ChangeFPSTarget();
			if (this.autoWakeUp)
			{
				GameScr.info1.addInfo("Tự động hồi sinh [Bật]", 0);
			}
			this.LoadButtonPositions();
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x0033A708 File Offset: 0x00338908
		public void MoveTo(int x, int y)
		{
			Char.myCharz().cx = x;
			Char.myCharz().cy = y;
			Service.gI().charMove();
			if (!ItemTime.isExistItem(4387))
			{
				Char.myCharz().cx = x;
				Char.myCharz().cy = y + 1;
				Service.gI().charMove();
				Char.myCharz().cx = x;
				Char.myCharz().cy = y;
				Service.gI().charMove();
			}
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x0033A784 File Offset: 0x00338984
		public void GotoNpc(int npcID)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == npcID && Math.abs(npc.cx - Char.myCharz().cx) >= 50)
				{
					this.MoveTo(npc.cx, npc.cy - 1);
					Char.myCharz().FocusManualTo(npc);
					return;
				}
			}
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x0033A800 File Offset: 0x00338A00
		public int FindItemIndex(int idItem)
		{
			if (Char.myCharz().arrItemBag == null)
			{
				return -1;
			}
			for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
			{
				if (Char.myCharz().arrItemBag[i] != null && (int)Char.myCharz().arrItemBag[i].template.id == idItem)
				{
					return Char.myCharz().arrItemBag[i].indexUI;
				}
			}
			return -1;
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x0033A86C File Offset: 0x00338A6C
		private void AttackChar()
		{
			try
			{
				MyVector myVector = new MyVector();
				myVector.addElement(Char.myCharz().charFocus);
				Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
			}
			catch
			{
			}
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x0033A8B8 File Offset: 0x00338AB8
		public void AttackMob(Mob mob)
		{
			try
			{
				MyVector myVector = new MyVector();
				myVector.addElement(mob);
				Service.gI().sendPlayerAttack(myVector, new MyVector(), 1);
			}
			catch
			{
			}
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x0033A8F8 File Offset: 0x00338AF8
		public void AutoAttack()
		{
			Char @char = Char.myCharz();
			if (!Char.isLoadingMap && !@char.stone && !@char.meDead && @char.statusMe != 14 && @char.statusMe != 5 && @char.myskill.template.type == 1 && @char.myskill.template.id != 10 && @char.myskill.template.id != 11 && !@char.myskill.paintCanNotUseSkill && mSystem.currentTimeMillis() - this.lastAutoAttack > 500L)
			{
				if (GameScr.gI().isMeCanAttackMob(@char.mobFocus) && Res.abs(@char.mobFocus.xFirst - @char.cx) < @char.myskill.dx * 2)
				{
					this.AttackMob(@char.mobFocus);
					this.SetUsedSkill(@char.myskill);
				}
				else if (@char.isMeCanAttackOtherPlayer(@char.charFocus) && Res.abs(@char.charFocus.cx - @char.cx) < @char.myskill.dx * 2)
				{
					this.AttackChar();
					this.SetUsedSkill(@char.myskill);
				}
				this.lastAutoAttack = mSystem.currentTimeMillis();
			}
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x0033AA52 File Offset: 0x00338C52
		public void SetUsedSkill(Skill skill)
		{
			skill.paintCanNotUseSkill = true;
			skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x0033AA68 File Offset: 0x00338C68
		public void UsePorata()
		{
			foreach (int num in new int[]
			{
				454,
				921,
				1155,
				1156,
				1162
			})
			{
				int index = this.FindItemIndex(num);
				if (index != -1)
				{
					Service.gI().useItem(0, 1, (sbyte)index, -1);
					Service.gI().petStatus(3);
					return;
				}
			}
			GameScr.info1.addInfo("Bạn không có bông tai", 0);
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x0033AAD0 File Offset: 0x00338CD0
		public void AutoFocusBoss()
		{
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.charID < 0 && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ"))
				{
					Char.myCharz().FocusManualTo(@char);
					return;
				}
			}
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x0033AB38 File Offset: 0x00338D38
		public int GetMapID(string mapName)
		{
			int result = -1;
			for (int i = 0; i < XmapController.mapNames.Length; i++)
			{
				if (XmapController.mapNames[i].Trim().ToLower().Equals(mapName.Trim().ToLower()))
				{
					result = i;
				}
			}
			return result;
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x0033AB80 File Offset: 0x00338D80
		private string CharGender(Char @char)
		{
			if (@char.cTypePk == 5)
			{
				return "BOSS";
			}
			if (@char.cgender == 0)
			{
				return "TĐ";
			}
			if (@char.cgender == 1)
			{
				return "NM";
			}
			if (@char.cgender == 2)
			{
				return "XD";
			}
			return "";
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x0033ABD0 File Offset: 0x00338DD0
		public void UseItem(int itemId)
		{
			int index = this.FindItemIndex(itemId);
			if (index != -1)
			{
				Service.gI().useItem(0, 1, (sbyte)index, -1);
				return;
			}
			GameScr.info1.addInfo("Không tìm thấy vật phẩm", 0);
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x0033AC0C File Offset: 0x00338E0C
		public void UseItemAuto()
		{
			if (!ModFunc.startAutoItem)
			{
				// Task.Delay(10000).ContinueWith<bool>((Task t) => ModFunc.startAutoItem = true);
				return;
			}
			if (this.listItemAuto.Count > 0 && ModFunc.startAutoItem)
			{
				for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
				{
					Item item = Char.myCharz().arrItemBag[i];
					foreach (ItemAuto itemAuto in this.listItemAuto)
					{
						if (item != null && (int)item.template.iconID == itemAuto.iconID && (int)item.template.id == itemAuto.id && !ItemTime.isExistItem((int)item.template.iconID))
						{
							Service.gI().useItem(0, 1, (sbyte)this.FindItemIndex((int)item.template.id), -1);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x0033AD30 File Offset: 0x00338F30
		private void AutoHoiSinh()
		{
			if (Char.myCharz().cHP <= 0L || Char.myCharz().meDead || Char.myCharz().statusMe == 14)
			{
				Service.gI().wakeUpFromDead();
			}
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x0033AD64 File Offset: 0x00338F64
		public static int GetCurrPhaLe(Item item)
		{
			for (int i = 0; i < item.itemOption.Length; i++)
			{
				if (item.itemOption[i].optionTemplate.id == 107)
				{
					return item.itemOption[i].param;
				}
			}
			return 0;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x0033ADAC File Offset: 0x00338FAC
		public void AutoPhaLe()
		{
			while (this.isAutoPhaLe)
			{
				if (TileMap.mapID != 5)
				{
					GameScr.info1.addInfo("Cần đến Đảo Kame để sử dụng Tự động Pha lê hóa", 0);
					Thread.Sleep(500);
					return;
				}
				if (this.currPhale >= this.maxPhale && this.itemPhale != null && this.currPhale >= 0 && this.maxPhale > 0)
				{
					Sound.start(1f, Sound.l1);
					GameScr.info1.addInfo("Đã đạt đến số sao yêu cầu", 0);
					this.maxPhale = -1;
					this.itemPhale = null;
				}
				if (Char.myCharz().xu > 10000000000L)
				{
					this.GotoNpc(21);
					if (this.itemPhale != null && this.maxPhale > 0)
					{
						while (!GameCanvas.menu.showMenu)
						{
							Service.gI().combine(1, GameCanvas.panel.vItemCombine);
							Thread.Sleep(100);
						}
						Service.gI().confirmMenu(21, 0);
						GameCanvas.menu.doCloseMenu();
						GameCanvas.panel.currItem = null;
						GameCanvas.panel.chatTField.isShow = false;
					}
				}
				else if (this.itemPhale != null)
				{
					this.BanVang();
				}
				Thread.Sleep(500);
			}
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x0033AEE8 File Offset: 0x003390E8
		private void BanVang()
		{
			if (TileMap.mapID != 5)
			{
				GameScr.info1.addInfo("Cần đến Đảo Kame để Tự động bán vàng", 0);
				Thread.Sleep(1000);
				return;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				GameScr.info1.addInfo("Dừng bán vàng", 0);
				return;
			}
			while (Char.myCharz().xu <= 60000000000L && !Input.GetKey(KeyCode.Q))
			{
				if (this.FindItemIndex(457) == -1)
				{
					GameScr.info1.addInfo("Không tìm thấy thỏi vàng", 0);
					if (this.isAutoPhaLe)
					{
						this.isAutoPhaLe = false;
						GameScr.info1.addInfo("Vàng không đủ, đã tắt Tự động Pha lê hóa", 0);
					}
					return;
				}
				Service.gI().useItem(0, 1, (sbyte)this.FindItemIndex(457), -1);
				GameScr.info1.addInfo("Đang bán thỏi vàng", 0);
				Thread.Sleep(500);
			}
			GameScr.info1.addInfo("Đã bán xong", 0);
			Thread.Sleep(500);
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x0033AFE4 File Offset: 0x003391E4
		public static Item FindItemBagWithIndexUI(int index)
		{
			foreach (Item item in Char.myCharz().arrItemBag)
			{
				if (item != null && item.indexUI == index)
				{
					return item;
				}
			}
			return null;
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x0033B020 File Offset: 0x00339220
		public void CollectAllThuongDe()
		{
			this.isCollectAll = true;
			Service.gI().openMenu(19);
			Service.gI().confirmMenu(19, 2);
			Service.gI().confirmMenu(19, 1);
			Service.gI().buyItem(2, 0, 0);
			Thread.Sleep(2000);
			this.isCollectAll = false;
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x0033B078 File Offset: 0x00339278
		private void OpenMenuThuongDe()
		{
			this.isOpenThuongDe = true;
			Service.gI().openMenu(19);
			Service.gI().confirmMenu(19, 2);
			Service.gI().confirmMenu(19, 0);
			this.isOpenThuongDe = false;
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x0033B0B0 File Offset: 0x003392B0
		public void quayThuongDe()
		{
			if (this.isCollectAll || this.isOpenThuongDe)
			{
				return;
			}
			if (!this.isPaintThuongDe && TileMap.mapID == 45)
			{
				this.OpenMenuThuongDe();
				return;
			}
			if (TileMap.mapID == 45)
			{
				if (Input.GetKey("q") || Char.myCharz().xu <= 200000000L)
				{
					GameScr.info1.addInfo("Đã tắt Auto VQMM (2)", 0);
					this.isAutoVQMM = false;
					return;
				}
				Service.gI().openMenu(19);
				Service.gI().SendCrackBall(2, 7);
			}
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x0033B140 File Offset: 0x00339340
		public bool Chat(string text)
		{
			switch (text)
			{
				case "htl":
					isInventory = !isInventory;
					GameScr.info1.addInfo("Hành Trang Lưới: " + (isInventory ? "ON" : "OFF"), 0);
					return true;
				case "loadskill":
					perform(57, null);
					return true;
				case "ak":
					perform(42, null);
					return true;
				case "ts":
					perform(44, null);
					return true;
				case "tsnguoi":
					perform(48, null);
					return true;
				case "vqmm":
					isPaintThuongDe = false;
					isAutoVQMM = !isAutoVQMM;
					GameScr.info1.addInfo("Auto VQMM: " + (isAutoVQMM ? "Bật" : "Tắt"), 0);
					return true;
				case "ukhu":
					isUpdateZones = !isUpdateZones;
					GameScr.info1.addInfo("Tự động cập nhật khu: " + (isUpdateZones ? "Bật" : "Tắt"), 0);
					return true;
				default:
					if (text.StartsWith("k "))
					{
						if (int.TryParse(text.Replace("k ", ""), out var khu) && khu >= 0)
						{
							Service.gI().requestChangeZone(khu, -1);
						}
						return true;
					}
					if (text.StartsWith("s "))
					{
						ChangeGameSpeed(text.Replace("s ", ""));
						return true;
					}
					if (text.StartsWith("atc "))
					{
						textAutoChat = text.Replace("atc ", "");
						return true;
					}
					if (text.StartsWith("atctg "))
					{
						textAutoChatTG = text.Replace("atctg ", "");
						return true;
					}
					if (text.StartsWith("do "))
					{
						bossCanDo = text.Replace("do ", "");
						GameScr.info1.addInfo("Boss cần dò: " + bossCanDo, 0);
						return true;
					}
					if (text == "dbx")
					{
						isdoBoss = !isdoBoss;
						GameScr.info1.addInfo("Tự động dò boss: " + (isdoBoss ? "Bật" : "Tắt"), 0);
						return true;
					}
					if (text == "gtv")
					{
						isVietnamese = !isVietnamese;
						GameScr.info1.addInfo("Gõ Tiếng Việt: " + (isVietnamese ? "Bật" : "Tắt"), 0);
						return true;
					}
					return false;
			}
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x0033B48C File Offset: 0x0033968C
		private void UpdateTouch()
		{
			if (GameScr.gI().isNotPaintTouchControl())
			{
				return;
			}
			if (GameCanvas.isPointerHoldIn(GameScr.imgPanel.getWidth() + 3, 10, GameScr.imgArrow.getWidth() + 2, GameScr.imgArrow.getHeight() + 2) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				ModFunc.isMenuVisible = true;
				ModFunc.targetMenuX = 22f;
				ModFunc.targetArrowRotation = 180f;
				mGraphics.isFlipping = false;
				SoundMn.gI().buttonClick();
				GameCanvas.clearAllPointerEvent();
				return;
			}
			if (GameCanvas.isPointerHoldIn(GameScr.imgPanel.getWidth() + 65, 10, GameScr.imgArrow2.getWidth() + 2, GameScr.imgArrow2.getHeight() + 2) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				ModFunc.isMenuVisible = false;
				ModFunc.targetMenuX = 0f;
				ModFunc.targetArrowRotation = 0f;
				SoundMn.gI().buttonClick();
				GameCanvas.clearAllPointerEvent();
				return;
			}
			if (ModFunc.isMenuVisible)
			{
				if (GameCanvas.isPointerHoldIn(GameScr.imgPanel.getWidth() + 8, 3, GameScr.imgModFunc.getWidth() + 2, GameScr.imgModFunc.getHeight() + 2) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					this.OpenMenu();
					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
					return;
				}
				if (GameCanvas.isPointerHoldIn(GameScr.imgPanel.getWidth() + 30, 3, GameScr.imgCommandChat.getWidth() + 3, GameScr.imgCommandChat.getHeight() + 2) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					ModFunc.isShowMenuChat = true;
					SoundMn.gI().buttonClick();
					GameCanvas.clearAllPointerEvent();
					return;
				}
			}
			if (!ModFunc.isEditButton)
			{
				foreach (KeyValuePair<string, ModFunc.Point> kvp2 in ModFunc.buttonPositions)
				{
					int num = ModFunc.modKeyPosX + kvp2.Value.x;
					int buttonY2 = ModFunc.modKeyPosY + kvp2.Value.y;
					if (GameCanvas.isPointerHoldIn(num - 16, buttonY2 - 16, 32, 32) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						string key = kvp2.Key;
						if (!(key == "Capsule"))
						{
							if (!(key == "Fusion"))
							{
								if (!(key == "Zone"))
								{
									if (!(key == "MapLeft"))
									{
										if (!(key == "MapCenter"))
										{
											if (key == "MapRight")
											{
												ManualXmap.GI().LoadMapRight();
											}
										}
										else
										{
											ManualXmap.GI().LoadMapCenter();
										}
									}
									else
									{
										ManualXmap.GI().LoadMapLeft();
									}
								}
								else
								{
									this.userOpenZones = true;
									Service.gI().openUIZone();
								}
							}
							else
							{
								this.UsePorata();
							}
						}
						else
						{
							this.UseItem(194);
						}
						GameCanvas.clearAllPointerEvent();
						break;
					}
				}
				return;
			}
			int buttonWidth = 60;
			int buttonHeight = 24;
			int padding = 10;
			int y = 40;
			int x = GameCanvas.w / 2 - buttonWidth - padding / 2;
			int resetX = GameCanvas.w / 2 + padding / 2;
			if (GameCanvas.isPointerHoldIn(x, y, buttonWidth, buttonHeight) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				this.SaveButtonPositions();
				ModFunc.isEditButton = false;
				GameScr.info1.addInfo("Đã lưu vị trí các nút", 0);
				GameCanvas.clearAllPointerEvent();
				return;
			}
			if (GameCanvas.isPointerHoldIn(resetX, y, buttonWidth, buttonHeight) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				ModFunc.buttonPositions.Clear();
				ModFunc.InitButtonPositions();
				this.SaveButtonPositions();
				GameScr.info1.addInfo("Đã reset vị trí các nút về mặc định", 0);
				GameCanvas.clearAllPointerEvent();
				return;
			}
			if (GameCanvas.isPointerDown)
			{
				if (!ModFunc.isDragging)
				{
					foreach (KeyValuePair<string, ModFunc.Point> kvp3 in ModFunc.buttonPositions)
					{
						int buttonX = ModFunc.modKeyPosX + kvp3.Value.x;
						int buttonY3 = ModFunc.modKeyPosY + kvp3.Value.y;
						if (GameCanvas.isPointerHoldIn(buttonX - 16, buttonY3 - 16, 32, 32))
						{
							ModFunc.selectedButton = kvp3.Key;
							ModFunc.dragStart = new ModFunc.Point(GameCanvas.px - buttonX, GameCanvas.py - buttonY3);
							ModFunc.isDragging = true;
							GameCanvas.isPointerJustDown = false;
							break;
						}
					}
					return;
				}
				if (ModFunc.selectedButton != null)
				{
					int newX = GameCanvas.px - ModFunc.modKeyPosX - ModFunc.dragStart.x;
					int newY = GameCanvas.py - ModFunc.modKeyPosY - ModFunc.dragStart.y;
					newX = System.Math.Max(-modKeyPosX + 20, System.Math.Min(GameCanvas.w - modKeyPosX - 25, newX));
					newY = System.Math.Max(-modKeyPosY + 20, System.Math.Min(GameCanvas.h - modKeyPosY - 25, newY));
					ModFunc.buttonPositions[ModFunc.selectedButton] = new ModFunc.Point(newX, newY);
					return;
				}
			}
			else if (ModFunc.isDragging)
			{
				this.SaveButtonPositions();
				ModFunc.isDragging = false;
				ModFunc.selectedButton = null;
				ModFunc.dragStart = null;
				GameCanvas.clearAllPointerEvent();
			}
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x0033B9B8 File Offset: 0x00339BB8
		public void Update()
		{
			this.UpdateTouch();
			AutoItem.Update();
			long currentTime = mSystem.currentTimeMillis();
			if (this.isPeanPet && currentTime - this.lastPeanPet >= 3000L)
			{
				Char pet = Char.myPetz();
				if (!pet.isDie && (pet.cStamina <= (int)(pet.cMaxStamina * 20 / 100) || pet.cHP < pet.cHPFull * 20L / 100L || pet.cMP < pet.cMPFull * 20L / 100L))
				{
					GameScr.gI().doUseHP();
					this.lastPeanPet = currentTime;
				}
			}
			if (this.isAutoPhaLe && this.itemPhale != null)
			{
				this.currPhale = ModFunc.GetCurrPhaLe(ModFunc.FindItemBagWithIndexUI(this.itemPhale.indexUI));
			}
			else
			{
				this.currPhale = -1;
			}
			if (ModFunc.isAutoChat && currentTime - this.lastAutoChat >= 4000L)
			{
				this.AutoChat();
				this.lastAutoChat = currentTime;
			}
			if (ModFunc.isAutoChatTG && currentTime - this.lastAutoChatTG >= 30000L)
			{
				this.AutoChatTG();
				this.lastAutoChatTG = currentTime;
			}
			if (!TileMap.isOfflineMap() && mSystem.currentTimeMillis() - this.lastUpdateZones >= 1000L)
			{
				this.UseItemAuto();
				if (this.isUpdateZones)
				{
					Service.gI().openUIZone();
				}
				this.lastUpdateZones = mSystem.currentTimeMillis();
			}
			if (this.isAutoVQMM && currentTime - this.lastVQMM >= 1000L)
			{
				this.quayThuongDe();
				this.lastVQMM = currentTime;
			}
			if (this.autoWakeUp && currentTime - this.lastAutoWakeUp >= 1000L)
			{
				this.AutoHoiSinh();
				this.lastAutoWakeUp = currentTime;
			}
			if (this.focusBoss && currentTime - this.lastFocusBoss >= 500L)
			{
				this.AutoFocusBoss();
				this.lastFocusBoss = currentTime;
			}
			if (this.autoAttack)
			{
				this.AutoAttack();
			}
			this.UpdateNotifTichXanh();
			if (ModFunc.isAutoNoitai && Input.GetKey("q"))
			{
				ModFunc.isAutoNoitai = false;
				this.ChiSoNoiTai = -1;
				this.curSelectIntrinsic = "";
				GameScr.info1.addInfo("Đã dừng auto mở nội tại", 0);
			}
			if (ModFunc.isAutoFilterItem && currentTime - this.lastFilterTime >= 500L)
			{
				this.DoFilter();
				this.lastFilterTime = currentTime;
			}
			if (ModFunc.isdoBoss && mSystem.currentTimeMillis() - ModFunc.currDoBoss >= 1000L)
			{
				ModFunc.DoBoss();
				ModFunc.currDoBoss = mSystem.currentTimeMillis();
			}
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x0033BC14 File Offset: 0x00339E14
		public void PaintButton(mGraphics g, int xAnchor, int yAnchor)
		{
			if (!Main.isIPhone || !this.isShowButton || GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameScr.gI().isPaintPopup() || GameCanvas.panel.isShow || Char.myCharz().taskMaint.taskId == 0 || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance)
			{
				return;
			}
			ModFunc.modKeyPosX = xAnchor;
			ModFunc.modKeyPosY = yAnchor;
			ModFunc.InitButtonPositions();
			foreach (KeyValuePair<string, ModFunc.Point> kvp in ModFunc.buttonPositions)
			{
				string buttonName = kvp.Key;
				ModFunc.Point pos = kvp.Value;
				int buttonX = xAnchor + pos.x;
				int buttonY = yAnchor + pos.y;
				if (!(buttonName == "Capsule"))
				{
					if (!(buttonName == "Fusion"))
					{
						if (!(buttonName == "Zone"))
						{
							if (!(buttonName == "MapLeft"))
							{
								if (!(buttonName == "MapCenter"))
								{
									if (buttonName == "MapRight")
									{
										g.drawImage(GameScr.imgNextRight, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
										if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
										{
											g.drawImage(GameScr.imgNextRightF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
										}
									}
								}
								else
								{
									g.drawImage(GameScr.imgNextCenter, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
									if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
									{
										g.drawImage(GameScr.imgNextCenterF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
									}
								}
							}
							else
							{
								g.drawImage(GameScr.imgNextLeft, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
								if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
								{
									g.drawImage(GameScr.imgNextLeftF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
								}
							}
						}
						else
						{
							g.drawImage(GameScr.imgChangeZone, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
							if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
							{
								g.drawImage(GameScr.imgChangeZoneF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
							}
						}
					}
					else
					{
						g.drawImage(GameScr.imgFusion, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
						if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
						{
							g.drawImage(GameScr.imgFusionF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
						}
					}
				}
				else
				{
					g.drawImage(GameScr.imgCapsule, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					if (GameCanvas.isPointerHoldIn(buttonX - 15, buttonY - 15, 30, 30))
					{
						g.drawImage(GameScr.imgCapsuleF, buttonX, buttonY, mGraphics.HCENTER | mGraphics.VCENTER);
					}
				}
				if (ModFunc.isEditButton)
				{
					g.setColor((ModFunc.selectedButton == buttonName) ? 16711680 : 16776960);
					g.drawRect(buttonX - 16, buttonY - 16, 32, 32);
					mFont.tahoma_7_yellow.drawStringBorder(g, buttonName, buttonX, buttonY - 30, mFont.CENTER, mFont.tahoma_7_grey);
					ModFunc.Point poss = ModFunc.buttonPositions[buttonName];
					string coords = string.Format("({0},{1})", poss.x, poss.y);
					mFont.tahoma_7_yellow.drawStringBorder(g, coords, buttonX, buttonY + 20, mFont.CENTER, mFont.tahoma_7_grey);
				}
			}
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x0033BFF4 File Offset: 0x0033A1F4
		public void Paint(mGraphics g)
		{
			int imgHPWidth = mGraphics.getImageWidth(GameScr.imgHP);
			int imgMPWidth = mGraphics.getImageWidth(GameScr.imgMP);
			mFont.tahoma_7_red.drawStringBorder(g, NinjaUtil.getMoneys(Char.myCharz().cHP), 84 + imgHPWidth / 2, 4, mFont.CENTER, mFont.tahoma_7_grey);
			mFont.tahoma_7_blue1.drawStringBorder(g, NinjaUtil.getMoneys(Char.myCharz().cMP), 84 + imgMPWidth / 2, 17, mFont.CENTER, mFont.tahoma_7_grey);
			int xText = 90;
			int yText = GameScr.gI().cmdMenu.y - 20;
			if (!this.showInfoMe && !ModFunc.isEditButton && !ModFunc.isAutoNoitai)
			{
				mFont.tahoma_7_yellow.drawStringBorder(g, "Time: " + DateTime.Now.ToString("dd/MM/yyyy | HH:mm:ss"), xText, yText + 20, mFont.LEFT, mFont.tahoma_7_grey);
				int num2 = 0;
				if (ModFunc.isShowID)
				{
					mFont.tahoma_7_red.drawStringBorder(g, string.Concat(new string[]
					{
						TileMap.mapName,
						" [",
						TileMap.mapID.ToString(),
						"]  - Khu: ",
						TileMap.zoneID.ToString()
					}), xText, yText + num2, mFont.LEFT, mFont.tahoma_7_grey);
				}
				else
				{
					mFont.tahoma_7_red.drawStringBorder(g, TileMap.mapName + "  - Khu: " + TileMap.zoneID.ToString(), xText, yText + num2, mFont.LEFT, mFont.tahoma_7_grey);
				}
				num2 += 10;
				mFont.tahoma_7_red.drawStringBorder(g, "X: " + Char.myCharz().cx.ToString() + " - Y: " + Char.myCharz().cy.ToString(), xText, yText + num2, mFont.LEFT, mFont.tahoma_7_grey);
			}
			if (this.isAutoPhaLe && !ModFunc.isEditButton)
			{
				mFont.tahoma_7b_red.drawString(g, (this.itemPhale != null) ? this.itemPhale.template.name : "Chưa Có", GameCanvas.w / 2, 72, mFont.CENTER);
				mFont.tahoma_7b_red.drawString(g, (this.itemPhale != null) ? ("Số Sao : " + this.currPhale.ToString()) : "Số Sao : -1", GameCanvas.w / 2, 82, mFont.CENTER);
				mFont.tahoma_7b_red.drawString(g, "Số Sao Cần Đập : " + this.maxPhale.ToString() + " Sao", GameCanvas.w / 2, 92, mFont.CENTER);
			}
			if ((this.isAutoPhaLe || this.isAutoVQMM) && !ModFunc.isEditButton)
			{
				Item tv = ModFunc.FindItemBagWithIndexUI(this.FindItemIndex(457));
				mFont.tahoma_7b_red.drawString(g, "Ngọc Xanh : " + NinjaUtil.getMoneys((long)Char.myCharz().luong) + " Ngọc Hồng : " + NinjaUtil.getMoneys((long)Char.myCharz().luongKhoa), GameCanvas.w / 2, 102, mFont.CENTER);
				mFont.tahoma_7b_red.drawString(g, "Vàng : " + NinjaUtil.getMoneys(Char.myCharz().xu) + " Thỏi Vàng : " + ((tv != null) ? tv.quantity : 0).ToString(), GameCanvas.w / 2, 112, mFont.CENTER);
			}
			if (this.showInfoMe && !ModFunc.isEditButton)
			{
				this.PaintInfoMe(g, xText, yText);
			}
			if (ModFunc.notifBoss && !ModFunc.isEditButton && !ModFunc.isFilterItem && !ModFunc.isAutoNoitai)
			{
				int numX = 38;
				for (int i = 0; i < ModFunc.activeBossNotif.size(); i++)
				{
					((ShowBoss)ModFunc.activeBossNotif.elementAt(i)).PaintBoss(g, GameCanvas.w - 2, numX, mFont.RIGHT);
					numX += 10;
				}
			}
			if (ModFunc.notifKillBoss && !ModFunc.isEditButton && !this.showInfoMe && !ModFunc.isFilterItem && !ModFunc.isAutoNoitai)
			{
				int numXKilledBoss = 65;
				for (int j = 0; j < ModFunc.killedBossNotif.size(); j++)
				{
					((ShowBoss)ModFunc.killedBossNotif.elementAt(j)).PaintBoss(g, 100, numXKilledBoss, mFont.LEFT);
				}
			}
			if (this.showCharsInMap && !ModFunc.isEditButton)
			{
				this.PaintCharInMap(g);
			}
			this.PaintListInfo(g);
			if (this.lineToBoss)
			{
				for (int k = 0; k < GameScr.vCharInMap.size(); k++)
				{
					Char @char = (Char)GameScr.vCharInMap.elementAt(k);
					if (@char != null && @char.cTypePk == 5 && !@char.cName.StartsWith("Đ"))
					{
						g.setColor(Color.red);
						g.drawLine(Char.myCharz().cx - GameScr.cmx, Char.myCharz().cy - GameScr.cmy, @char.cx - GameScr.cmx, @char.cy - GameScr.cmy);
					}
				}
			}
			if (TileMap.mapID != 51 && TileMap.mapID != 52 && TileMap.mapID != 113 && TileMap.mapID != 112 && TileMap.mapID != 129 && TileMap.mapID != 165)
			{
				ModFunc.PaintLogoGif(g, GameCanvas.hw, GameCanvas.hh / 7, 3);
			}
			if (ModFunc.isMenuVisible)
			{
				ModFunc.menuX += (ModFunc.targetMenuX - ModFunc.menuX) * ModFunc.ANIMATION_SPEED;
				ModFunc.arrowRotation += (ModFunc.targetArrowRotation - ModFunc.arrowRotation) * ModFunc.ANIMATION_SPEED;
				g.drawImageFlipped(GameScr.imgArrow2, (float)(GameScr.imgPanel.getWidth() + 65), 10f);
				g.drawImage(GameScr.imgModFunc, (float)GameScr.imgPanel.getWidth() + ModFunc.menuX - 15f, 3f, 0);
				g.drawImage(GameScr.imgCommandChat, (float)GameScr.imgPanel.getWidth() + ModFunc.menuX + 5f, 3f, 0);
			}
			else
			{
				ModFunc.menuX += (0f - ModFunc.menuX) * ModFunc.ANIMATION_SPEED;
				ModFunc.arrowRotation += (0f - ModFunc.arrowRotation) * ModFunc.ANIMATION_SPEED;
				g.drawRegion(GameScr.imgArrow, 0, 0, GameScr.imgArrow.getWidth(), GameScr.imgArrow.getHeight(), (int)ModFunc.arrowRotation, GameScr.imgPanel.getWidth() + 8, 15, 3);
			}
			this.PaintPlayerTichXanh(g);
			if (ModFunc.isEditButton)
			{
				this.PaintEditButton(g);
			}
			if (ModFunc.isShowFilterList)
			{
				this.ShowFilterList(g);
			}
			if (ModFunc.isShowMenuChat)
			{
				ModFunc.PaintMenuChat(g);
			}
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x0033C684 File Offset: 0x0033A884
		private void PaintEditButton(mGraphics g)
		{
			int buttonWidth = 60;
			int buttonHeight = 24;
			int padding = 10;
			int y = 40;
			int saveX = GameCanvas.w / 2 - buttonWidth - padding / 2;
			g.setColor(0, 0.7f);
			g.fillRect(saveX, y, buttonWidth, buttonHeight);
			g.setColor(65280);
			g.drawRect(saveX, y, buttonWidth, buttonHeight);
			mFont.tahoma_7b_white.drawString(g, "Lưu", saveX + buttonWidth / 2, y + 5, mFont.CENTER);
			int resetX = GameCanvas.w / 2 + padding / 2;
			g.setColor(0, 0.7f);
			g.fillRect(resetX, y, buttonWidth, buttonHeight);
			g.setColor(16711680);
			g.drawRect(resetX, y, buttonWidth, buttonHeight);
			mFont.tahoma_7b_white.drawString(g, "Reset", resetX + buttonWidth / 2, y + 5, mFont.CENTER);
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x0033C750 File Offset: 0x0033A950
		private void PaintCharInMap(mGraphics g)
		{
			int numX = GameCanvas.w - 2;
			int numY = ModFunc.notifBoss ? 92 : 50;
			this.charsInMap.removeAllElements();
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				if (i > 15 || numY > GameScr.yHP - 20)
				{
					g.fillRect(numX - 150, numY + 1, 150, 10, 2721889, 90);
					mFont.tahoma_7_white.drawStringBorder(g, string.Concat(new object[]
					{
						i + 1,
						" ..."
					}), numX, numY, mFont.RIGHT, mFont.tahoma_7_grey);
					return;
				}
				Char char6 = (Char)GameScr.vCharInMap.elementAt(i);
				if (char6 != null && char6.cName != null && char6.cName.Length > 0 && !char6.isMiniPet && char6.cName.ToLower() != "trọng tài")
				{
					g.fillRect(numX - 150, numY + 1, 150, 10, 2721889, 90);
					string[] str = new string[]
					{
						(i + 1 < 10) ? "0" : "",
						(i + 1).ToString(),
						". [",
						this.CharGender(char6),
						"] ",
						char6.cName,
						" [ ",
						NinjaUtil.getMoneys(char6.cHP).ToString(),
						" ]"
					};
					if (char6 == Char.myCharz().charFocus)
					{
						mFont.tahoma_7_yellow.drawStringBorder(g, string.Concat(str), numX, numY, mFont.RIGHT, mFont.tahoma_7_grey);
					}
					else if (char6.charID < 0 && char6.charID > -1000 && char6.charID != -114)
					{
						mFont.tahoma_7_red.drawStringBorder(g, string.Concat(str), numX, numY, mFont.RIGHT, mFont.tahoma_7_grey);
					}
					else if (Char.myCharz().clan != null && char6.clanID == Char.myCharz().clan.ID)
					{
						mFont.tahoma_7_green.drawStringBorder(g, string.Concat(str), numX, numY, mFont.RIGHT, mFont.tahoma_7_grey);
					}
					else
					{
						mFont.tahoma_7_white.drawStringBorder(g, string.Concat(str), numX, numY, mFont.RIGHT, mFont.tahoma_7_grey);
					}
					this.charsInMap.addElement(char6);
					numY += 10;
				}
			}
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x0033C9CC File Offset: 0x0033ABCC
		private void PaintListInfo(mGraphics g)
		{
			int num4 = 70;
			Char charFocus = Char.myCharz().charFocus;
			if (charFocus != null && Char.myCharz().isMeCanAttackOtherPlayer(charFocus))
			{
				int healthBarWidth = 150;
				int healthBarHeight = 12;
				int healthBarX = GameCanvas.w / 2 - healthBarWidth / 2;
				int healthBarY = num4;
				g.setColor(8421504);
				g.fillRect(healthBarX - 3, healthBarY - 3, healthBarWidth + 6, healthBarHeight + 6, 12);
				g.setColor(2829099);
				g.fillRect(healthBarX - 1, healthBarY - 1, healthBarWidth + 2, healthBarHeight + 2, 10);
				float healthPercentage = (float)charFocus.cHP / (float)charFocus.cHPFull;
				int currentHealthBarWidth = (int)((float)healthBarWidth * healthPercentage);
				if (healthPercentage > 0.5f)
				{
					g.setColor(65280);
				}
				else if (healthPercentage > 0.25f)
				{
					g.setColor(16776960);
				}
				else
				{
					g.setColor(16711680);
				}
				g.fillRect(healthBarX, healthBarY, currentHealthBarWidth, healthBarHeight, 8);
				string hpText = NinjaUtil.getMoneys(charFocus.cHP) + "/" + NinjaUtil.getMoneys(charFocus.cHPFull);
				mFont.tahoma_7b_white.drawStringBorder(g, hpText, GameCanvas.w / 2 + 1, healthBarY + healthBarHeight / 2 - 6, mFont.CENTER, mFont.tahoma_7_grey);
				num4 += 17;
				if (charFocus.protectEff)
				{
					mFont.tahoma_7b_red.drawString(g, "Đang khiên năng lượng", GameCanvas.w / 2, num4, mFont.CENTER);
					num4 += 10;
				}
				if (charFocus.isMonkey == 1)
				{
					mFont.tahoma_7b_red.drawString(g, "Đang biến khỉ", GameCanvas.w / 2, num4, mFont.CENTER);
					num4 += 10;
				}
				if (charFocus.sleepEff)
				{
					mFont.tahoma_7b_red.drawString(g, "Bị thôi miên", GameCanvas.w / 2, num4, mFont.CENTER);
					num4 += 10;
				}
				if (charFocus.holdEffID != 0)
				{
					mFont.tahoma_7b_red.drawString(g, "Bị trói", GameCanvas.w / 2, num4, mFont.CENTER);
					num4 += 10;
				}
				if (charFocus.isFreez)
				{
					mFont.tahoma_7b_red.drawString(g, "Bị TDHS: " + charFocus.freezSeconds.ToString(), GameCanvas.w / 2, num4, mFont.CENTER);
					num4 += 10;
				}
				if (charFocus.blindEff)
				{
					mFont.tahoma_7b_red.drawString(g, "Bị choáng", GameCanvas.w / 2, num4, mFont.CENTER);
				}
			}
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x0033CC10 File Offset: 0x0033AE10
		private void PaintInfoMe(mGraphics g, int xText, int yText)
		{
			if (mSystem.currentTimeMillis() - this.lastUpdateInfoMe > 3000L)
			{
				Service.gI().petInfo();
				this.lastUpdateInfoMe = mSystem.currentTimeMillis();
			}
			int num = 10;
			int numy = 64;
			mFont.tahoma_7b_yellow.drawStringBorder(g, "Sư Phụ :", xText, yText, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBorder(g, "SM: " + NinjaUtil.getMoneys(Char.myCharz().cPower), xText, yText + num, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBorder(g, "TN: " + NinjaUtil.getMoneys(Char.myCharz().cTiemNang), xText, yText + 2 * num, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBorder(g, "SĐ: " + NinjaUtil.getMoneys(Char.myCharz().cDamFull), xText, yText + 3 * num, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBorder(g, "Giáp: " + NinjaUtil.getMoneys(Char.myCharz().cDefull), xText, yText + 4 * num, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7b_yellow.drawStringBorder(g, "Đệ Tử :", xText, yText + numy, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBorder(g, "SM: " + NinjaUtil.getMoneys(Char.myPetz().cPower), xText, yText + num + numy, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBorder(g, "TN: " + NinjaUtil.getMoneys(Char.myPetz().cTiemNang), xText, yText + 2 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBorder(g, "SĐ: " + NinjaUtil.getMoneys(Char.myPetz().cDamFull), xText, yText + 3 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBorder(g, "HP : " + NinjaUtil.getMoneys(Char.myPetz().cHP), xText, yText + 4 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBorder(g, "MP : " + NinjaUtil.getMoneys(Char.myPetz().cMP), xText, yText + 5 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_white.drawStringBorder(g, "Giáp: " + NinjaUtil.getMoneys(Char.myPetz().cDefull), xText, yText + 6 * num + numy, mFont.LEFT, mFont.tahoma_7_grey);
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x0033CE98 File Offset: 0x0033B098
		public void perform(int idAction, object p)
		{
			if (idAction > 60)
			{
				if (idAction <= 104)
				{
					if (idAction == 76)
					{
						PickMob.vuotDiaHinh = !PickMob.vuotDiaHinh;
						GameScr.info1.addInfo("Vượt địa hình " + (PickMob.vuotDiaHinh ? "[Bật]" : "[Tắt]"), 0);
						return;
					}
					if (idAction == 80)
					{
						PickMob.telePem = !PickMob.telePem;
						GameScr.info1.addInfo("Dịch chuyển đến quái\n" + (PickMob.telePem ? "[Bật]" : "[Tắt]"), 0);
						return;
					}
					switch (idAction)
					{
						case 89:
							{
								MyVector menuAutoPick = new MyVector();
								menuAutoPick.addElement(new Command("Tự động nhặt " + (PickMob.IsAutoPickItems ? "[Bật]" : "[Tắt]"), 90));
								menuAutoPick.addElement(new Command("Nhặt tất cả " + (PickMob.IsPickItemsAll ? "[Bật]" : "[Tắt]"), 91));
								menuAutoPick.addElement(new Command("Nhặt xa\n" + (PickMob.IsPickItemsDis ? "[Bật]" : "[Tắt]"), 92));
								menuAutoPick.addElement(new Command("Xem DS lọc đồ", 93));
								menuAutoPick.addElement(new Command("Tự động lọc đồ", 94));
								GameCanvas.menu.startAt(menuAutoPick, 4);
								return;
							}
						case 90:
							PickMob.IsAutoPickItems = !PickMob.IsAutoPickItems;
							GameScr.info1.addInfo("Tự động nhặt " + (PickMob.IsAutoPickItems ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 91:
							PickMob.IsPickItemsAll = !PickMob.IsPickItemsAll;
							GameScr.info1.addInfo("Nhặt tất cả " + (PickMob.IsPickItemsAll ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 92:
							PickMob.IsPickItemsDis = !PickMob.IsPickItemsDis;
							GameScr.info1.addInfo("Nhặt xa " + (PickMob.IsPickItemsDis ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 93:
							ModFunc.isShowFilterList = !ModFunc.isShowFilterList;
							GameScr.info1.addInfo("Đã mở danh sách lọc đồ", 0);
							return;
						case 94:
							ModFunc.isAutoFilterItem = !ModFunc.isAutoFilterItem;
							GameScr.info1.addInfo("Tự động lọc đồ " + (ModFunc.isAutoFilterItem ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 95:
						case 96:
						case 97:
						case 98:
						case 99:
							break;
						case 100:
							{
								string text = (string)p;
								int.TryParse(text.Split("-", StringSplitOptions.None)[0], out ModFunc.indexAutoPoint);
								bool.TryParse(text.Split("-", StringSplitOptions.None)[1], out ModFunc.autoPointForPet);
								GameCanvas.panel.hideNow();
								this.MyChatTextField(ChatTextField.gI(), "Tăng đến mức", "VD: 220000");
								return;
							}
						case 101:
							ModFunc.isOpenAccMAnager = true;
							return;
						case 102:
							{
								Account account = (Account)p;
								Rms.saveRMSString("acc", account.getUsername());
								Rms.saveRMSString("pass", account.getPassword());
								if (GameCanvas.loginScr != null && GameCanvas.currentScreen == GameCanvas.loginScr)
								{
									GameCanvas.loginScr.setUserPass();
								}
								ModFunc.isOpenAccMAnager = false;
								return;
							}
						case 103:
							{
								int index = ModFunc.accounts.IndexOf((Account)p);
								ModFunc.accounts.RemoveAt(index);
								this.cmdsChooseAcc.RemoveAt(index);
								this.cmdsDelAcc.RemoveAt(index);
								this.SaveAcc();
								return;
							}
						case 104:
							ModFunc.isOpenAccMAnager = false;
							return;
						default:
							return;
					}
				}
				else
				{
					if (idAction - 500 <= 1)
					{
						this.AddOrRemoveAutoItem((Item)p, idAction == 500);
						return;
					}
					if (idAction - 502 > 1)
					{
						if (idAction != 883)
						{
							return;
						}
						XmapController.ShowXmapMenu();
						return;
					}
					else
					{
						this.AddOrRemoveFilterItem((Item)p, idAction == 502);
					}
				}
				return;
			}
			if (idAction <= 8)
			{
				if (idAction == 1)
				{
					int mapId;
					string notif;
					if (int.TryParse((string)p, out mapId))
					{
						XmapController.StartRunToMapId(mapId);
						notif = "Di chuyển đến boss ở MAP " + mapId.ToString();
					}
					else
					{
						notif = "Địa điểm không hợp lệ!";
					}
					GameScr.info1.addInfo(notif, 0);
					return;
				}
				if (idAction != 2)
				{
					return;
				}
				GameScr.info1.addInfo("Đã huỷ di chuyển đến Boss", 0);
				return;
			}
			else
			{
				if (idAction == 16)
				{
					MyVector menuPet = new MyVector();
					menuPet.addElement(new Command(this.isPeanPet ? "Buff đậu cho đệ [Bật]" : "Buff đậu cho đệ [Tắt]", 17));
					GameCanvas.menu.startAt(menuPet, 4);
					return;
				}
				if (idAction != 17)
				{
					switch (idAction)
					{
						case 32:
							{
								MyVector myVector2 = new MyVector();
								myVector2.addElement(new Command(ModFunc.notifBoss ? "Thông báo BOSS [Bật]" : "Thông báo BOSS [Tắt]", 46));
								myVector2.addElement(new Command(ModFunc.notifKillBoss ? "Thông báo tiêu diệt BOSS [Bật]" : "Thông báo tiêu diệt BOSS [Tắt]", 58));
								myVector2.addElement(new Command(this.lineToBoss ? "Kẻ đường tới BOSS [Bật]" : "Đường kẻ tới BOSS [Tắt]", 47));
								myVector2.addElement(new Command(this.focusBoss ? "Focus BOSS [Bật]" : "Focus BOSS [Tắt]", 52));
								GameCanvas.menu.startAt(myVector2, 4);
								return;
							}
						case 33:
						case 34:
						case 35:
						case 36:
						case 37:
						case 39:
						case 40:
						case 41:
						case 50:
						case 59:
							break;
						case 38:
							PickMob.mapGoback = TileMap.mapID;
							PickMob.zoneGoback = TileMap.zoneID;
							PickMob.xGoback = Char.myCharz().cx;
							PickMob.yGoback = Char.myCharz().cy;
							PickMob.isGoBack = !PickMob.isGoBack;
							if (PickMob.isGoBack)
							{
								GameScr.info1.addInfo("Map Goback: " + TileMap.mapName + " | Khu: " + TileMap.zoneID.ToString(), 0);
								GameScr.info1.addInfo("Tọa độ X: " + PickMob.xGoback.ToString() + " | Y: " + PickMob.yGoback.ToString(), 0);
								if (Char.myCharz().cHP <= 0L || Char.myCharz().statusMe == 14)
								{
									Service.gI().returnTownFromDead();
									new Thread(new ThreadStart(PickMob.GoBack)).Start();
								}
							}
							GameScr.info1.addInfo("Goback tọa độ " + (PickMob.isGoBack ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 42:
							this.autoAttack = !this.autoAttack;
							GameScr.info1.addInfo("Tự đánh " + (this.autoAttack ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 43:
							PickMob.neSieuQuai = !PickMob.neSieuQuai;
							GameScr.info1.addInfo("Né siêu quái " + (PickMob.neSieuQuai ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 44:
							PickMob.tsPlayer = false;
							PickMob.tanSat = ((p != null) ? ((bool)p) : (!PickMob.tanSat));
							GameScr.info1.addInfo("Tàn sát " + (PickMob.tanSat ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 45:
							{
								MyVector myVector3 = new MyVector();
								MyVector mobIds = new MyVector();
								for (int i = 0; i < GameScr.vMob.size(); i++)
								{
									Mob mob = (Mob)GameScr.vMob.elementAt(i);
									if (GameScr.gI().isMeCanAttackMob(mob) && !mobIds.contains(mob.templateId) && !PickMob.TypeMobsTanSat.Contains(mob.templateId))
									{
										mobIds.addElement(mob.templateId);
										myVector3.addElement(new Command("Tàn sát " + mob.getTemplate().name, 49, mob));
									}
								}
								myVector3.addElement(new Command(PickMob.tanSat ? "Tàn sát [Bật]" : "Tàn sát [Tắt]", 44));
								myVector3.addElement(new Command(PickMob.tsPlayer ? "Tàn sát\nngười [Bật]" : "Tàn sát\nngười [Tắt]", 48));
								myVector3.addElement(new Command(this.autoAttack ? "Tự đánh [Bật]" : "Tự đánh [Tắt]", 42));
								myVector3.addElement(new Command(PickMob.neSieuQuai ? "Né siêu quái [Bật]" : "Né siêu quái [Tắt]", 43));
								myVector3.addElement(new Command(PickMob.vuotDiaHinh ? "Vượt địa hình [Bật]" : "Vượt địa hình [Tắt]", 76));
								myVector3.addElement(new Command(PickMob.telePem ? "Dịch chuyển\n[Bật]" : "Dịch chuyển\n[Tắt]", 80));
								myVector3.addElement(new Command(PickMob.isGoBack ? "Goback Tọa Độ [Bật]" : "Goback Tọa Độ [Tắt]", 38));
								myVector3.addElement(new Command("Xoá danh sách tàn sát", 51));
								GameCanvas.menu.startAt(myVector3, 4);
								return;
							}
						case 46:
							ModFunc.notifBoss = !ModFunc.notifBoss;
							GameScr.info1.addInfo("Thông báo BOSS " + (ModFunc.notifBoss ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 47:
							this.lineToBoss = !this.lineToBoss;
							GameScr.info1.addInfo("Kẻ đường tới BOSS " + (this.lineToBoss ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 48:
							PickMob.tanSat = false;
							PickMob.tsPlayer = ((p != null) ? ((bool)p) : (!PickMob.tsPlayer));
							GameScr.info1.addInfo("Tàn sát người " + (PickMob.tsPlayer ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 49:
							{
								Mob mobType = (Mob)p;
								if (!PickMob.TypeMobsTanSat.Contains(mobType.templateId))
								{
									PickMob.TypeMobsTanSat.Add(mobType.templateId);
								}
								GameScr.info1.addInfo("Tàn sát " + mobType.getTemplate().name, 0);
								this.perform(44, true);
								return;
							}
						case 51:
							PickMob.TypeMobsTanSat.Clear();
							GameScr.info1.addInfo("Đã xoá danh sách quái tàn sát!", 0);
							return;
						case 52:
							this.focusBoss = !this.focusBoss;
							GameScr.info1.addInfo("Focus BOSS " + (this.focusBoss ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 53:
							{
								MyVector menuOthers = new MyVector();
								menuOthers.addElement(new Command("Tốc độ\nGame", 54));
								menuOthers.addElement(new Command("Tự động\nChat " + (ModFunc.isAutoChat ? "[Bật]" : "[Tắt]"), 55));
								menuOthers.addElement(new Command("Tự động\nChat Thế\nGiới " + (ModFunc.isAutoChatTG ? "[Bật]" : "[Tắt]"), 56));
								menuOthers.addElement(new Command("Load ô\nskill", 57));
								menuOthers.addElement(new Command(ModFunc.isPlayingMusic ? "Tắt nhạc" : "Bật nhạc", 60));
								GameCanvas.menu.startAt(menuOthers, 4);
								return;
							}
						case 54:
							this.MyChatTextField(ChatTextField.gI(), "Nhập tốc độ game", "1 đến 10");
							return;
						case 55:
							ModFunc.isAutoChat = !ModFunc.isAutoChat;
							GameScr.info1.addInfo("Tự động chat " + (ModFunc.isAutoChat ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 56:
							ModFunc.isAutoChatTG = !ModFunc.isAutoChatTG;
							GameScr.info1.addInfo("Tự động chat thế giới " + (ModFunc.isAutoChatTG ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 57:
							this.LoadSkillToScreen();
							GameScr.info1.addInfo("Đã load ô skill", 0);
							return;
						case 58:
							ModFunc.notifKillBoss = !ModFunc.notifKillBoss;
							GameScr.info1.addInfo("Thông báo tiêu diệt BOSS " + (ModFunc.notifKillBoss ? "[Bật]" : "[Tắt]"), 0);
							return;
						case 60:
							Sound.play(UnityEngine.Random.Range(0, 3), 1f);
							Debug.Log("Music " + ModFunc.musics.Count.ToString());
							GameScr.info1.addInfo("Đã bật trình phát nhạc", 0);
							break;
						default:
							return;
					}
					return;
				}
				this.isPeanPet = !this.isPeanPet;
				GameScr.info1.addInfo("Buff đậu cho đệ " + (this.isPeanPet ? "[Bật]" : "[Tắt]"), 0);
				return;
			}
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x0033DAF6 File Offset: 0x0033BCF6
		public void AutoBuyItem(int num, Item itemBuy)
		{
			new Thread(delegate ()
			{
				for (int i = 0; i < num; i++)
				{
					Service.gI().buyItem(3, (int)itemBuy.template.id, 0);
					Thread.Sleep(200);
				}
				GameScr.info1.addInfo("Đã mua xong " + num.ToString() + " " + itemBuy.template.name, 0);
			}).Start();
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x0033DB20 File Offset: 0x0033BD20
		private void AddOrRemoveAutoItem(Item item, bool isAdd)
		{
			if (isAdd)
			{
				this.listItemAuto.Add(new ItemAuto((int)item.template.iconID, (int)item.template.id));
				GameScr.info1.addInfo("Đã thêm " + item.template.name + " vào Auto Item", 0);
				return;
			}
			foreach (ItemAuto itemAuto in this.listItemAuto)
			{
				if (itemAuto.iconID == (int)item.template.iconID && itemAuto.id == (int)item.template.id)
				{
					this.listItemAuto.Remove(itemAuto);
					GameScr.info1.addInfo("Đã xóa " + item.template.name + " khỏi Auto Item", 0);
					break;
				}
			}
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x0033DC18 File Offset: 0x0033BE18
		public void DoDoubleClickToObj(IMapObject obj)
		{
			if ((obj.Equals(Char.myCharz().npcFocus) || GameScr.gI().mobCapcha == null) && !GameScr.gI().checkClickToBotton(obj))
			{
				GameScr.gI().checkEffToObj(obj, false);
				Char.myCharz().cancelAttack();
				Char.myCharz().currentMovePoint = null;
				Char.myCharz().cvx = (Char.myCharz().cvy = 0);
				obj.stopMoving();
				GameScr.gI().auto = 10;
				GameScr.gI().doFire(false, true);
				GameScr.gI().clickToX = obj.getX();
				GameScr.gI().clickToY = obj.getY();
				GameScr.gI().clickOnTileTop = false;
				GameScr.gI().clickMoving = true;
				GameScr.gI().clickMovingRed = true;
				GameScr.gI().clickMovingTimeOut = 20;
				GameScr.gI().clickMovingP1 = 30;
			}
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x0033DD08 File Offset: 0x0033BF08
		public void MyChatTextField(ChatTextField chatTField, string strChat, string strName)
		{
			chatTField.strChat = strChat;
			chatTField.tfChat.name = strName;
			chatTField.to = string.Empty;
			chatTField.isShow = true;
			chatTField.tfChat.isFocus = true;
			chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			chatTField.tfChat.setMaxTextLenght(10);
			if (!Main.isPC)
			{
				chatTField.startChat(GameCanvas.panel, string.Empty);
				return;
			}
			if (GameCanvas.isTouch)
			{
				chatTField.tfChat.doChangeToTextBox();
			}
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x0033DD90 File Offset: 0x0033BF90
		public void ChangeGameSpeed(string strSpeed)
		{
			int speed;
			if (int.TryParse(strSpeed, out speed) && speed > 0 && speed <= 10)
			{
				Time.timeScale = (float)speed;
				GameScr.info1.addInfo("Tốc độ game: " + speed.ToString(), 0);
				return;
			}
			GameScr.info1.addInfo("Chỉ nhập số từ 1 đến 10", 0);
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x0033DDE4 File Offset: 0x0033BFE4
		public void TeleportToPlayer(int charID)
		{
			Service.gI().gotoPlayer(charID);
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x0033DDF4 File Offset: 0x0033BFF4
		public void AddNotifTichXanh(string notif)
		{
			this.listNotifTichXanh.addElement(notif);
			if (!this.startChat)
			{
				int halfW = GameCanvas.w / 2;
				this.startChat = true;
				this.xNotif = halfW + halfW / 2;
				this.lastUpdateNotif = mSystem.currentTimeMillis();
			}
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x0033DE3C File Offset: 0x0033C03C
		private void PaintPlayerTichXanh(mGraphics g)
		{
			if (this.listNotifTichXanh.size() != 0)
			{
				string st = (string)this.listNotifTichXanh.elementAt(0);
				int halfW = GameCanvas.w / 2;
				g.setClip(halfW - halfW / 3, 50, halfW / 3 * 2, 12);
				g.fillRect(halfW - halfW / 3, 50, halfW / 3 * 2, 12, 0, 60);
				mFont.tahoma_7_yellow.drawStringBorder(g, st, this.xNotif, 50, 0, mFont.tahoma_7_grey);
				ModFunc.PaintTicks(g, this.xNotif - 12, 51);
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x0033DEC8 File Offset: 0x0033C0C8
		private void UpdateNotifTichXanh()
		{
			if (!this.startChat || mSystem.currentTimeMillis() - this.lastUpdateNotif < 10L)
			{
				return;
			}
			this.xNotif--;
			string strChat = (string)this.listNotifTichXanh.elementAt(0);
			this.lastUpdateNotif = mSystem.currentTimeMillis();
			if (this.xNotif < GameCanvas.w / 2 - 100 - mFont.tahoma_7_yellow.getWidth(strChat))
			{
				this.xNotif = GameCanvas.w / 2 + 100;
				this.listNotifTichXanh.removeElementAt(0);
				if (this.listNotifTichXanh.size() == 0)
				{
					this.startChat = false;
				}
			}
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x0033DF68 File Offset: 0x0033C168
		public void SetIncreasePoint(string strPoint)
		{
			int point;
			if (int.TryParse(strPoint, out point) && ModFunc.indexAutoPoint != -1 && point > 0)
			{
				ModFunc.pointIncrease = point;
				new Thread(new ThreadStart(this.DoAutoIncreasePoint)).Start();
				GameScr.info1.addInfo("Tự động tăng " + ModFunc.strPointTypes[ModFunc.indexAutoPoint] + " đến " + point.ToString(), 0);
				return;
			}
			GameScr.info1.addInfo("Có lỗi xảy ra (100)", 0);
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x0033DFE4 File Offset: 0x0033C1E4
		private void DoAutoIncreasePoint()
		{
			while (ModFunc.indexAutoPoint != -1 && ModFunc.pointIncrease > 0)
			{
				Char @char = ModFunc.autoPointForPet ? Char.myPetz() : Char.myCharz();
				long num;
				switch (ModFunc.indexAutoPoint)
				{
					case 0:
						num = @char.cHPGoc;
						break;
					case 1:
						num = @char.cMPGoc;
						break;
					case 2:
						num = @char.cDamGoc;
						break;
					case 3:
						num = (long)@char.cDefGoc;
						break;
					case 4:
						num = (long)@char.cCriticalGoc;
						break;
					default:
						num = 0L;
						break;
				}
				if (num >= (long)ModFunc.pointIncrease)
				{
					ModFunc.indexAutoPoint = -1;
					ModFunc.pointIncrease = 0;
					GameScr.info1.addInfo("Đã đạt chỉ số yêu cầu", 0);
					return;
				}
				Service.gI().upPotential(ModFunc.autoPointForPet, ModFunc.indexAutoPoint, 100);
				Thread.Sleep(500);
			}
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x0033E0B8 File Offset: 0x0033C2B8
		public void LoadAcc()
		{
			string text = Rms.loadRMSString("accManager");
			if (text != null && !(text.Trim('|') == string.Empty))
			{
				ModFunc.accounts.Clear();
				this.cmdsChooseAcc.Clear();
				this.cmdsDelAcc.Clear();
				string[] accs = text.Trim('|').Split('|', StringSplitOptions.None);
				for (int i = 0; i < accs.Length; i++)
				{
					string[] acc = accs[i].Split('$', StringSplitOptions.None);
					Account account = new Account(acc[0], acc[1]);
					ModFunc.accounts.Add(account);
					Command cmd = new Command(account.getUsername(), this, 102, account);
					cmd.setType();
					this.cmdsChooseAcc.Add(cmd);
					Command cmdDel = new Command("Xoá", this, 103, account);
					cmdDel.setTypeDelete();
					this.cmdsDelAcc.Add(cmdDel);
				}
			}
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x0033E1AC File Offset: 0x0033C3AC
		public void AddAccount(string user, string pass)
		{
			Account account = new Account(user, pass);
			int index = ModFunc.accounts.IndexOf(account);
			if (index != -1)
			{
				ModFunc.accounts.RemoveAt(index);
			}
			ModFunc.accounts.Insert(0, account);
			for (int i = 5; i < ModFunc.accounts.Count; i++)
			{
				ModFunc.accounts.RemoveAt(i);
			}
			this.SaveAcc();
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x0033E210 File Offset: 0x0033C410
		private void SaveAcc()
		{
			string text = "";
			foreach (Account acc in ModFunc.accounts)
			{
				text += string.Join('$', new string[]
				{
					acc.getUsername(),
					acc.getPassword()
				});
				text += "|";
			}
			Rms.saveRMSString("accManager", text.Trim('|'));
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x0033E2A8 File Offset: 0x0033C4A8
		private void AutoChat()
		{
			if (string.IsNullOrEmpty(ModFunc.textAutoChat))
			{
				GameScr.info1.addInfo("Chưa cài nội dung tự động chat", 0);
				return;
			}
			Service.gI().chat(ModFunc.textAutoChat);
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x0033E2D6 File Offset: 0x0033C4D6
		private void AutoChatTG()
		{
			if (string.IsNullOrEmpty(ModFunc.textAutoChatTG))
			{
				GameScr.info1.addInfo("Chưa cài nội dung tự động chat thế giới", 0);
				return;
			}
			Service.gI().chatGlobal(ModFunc.textAutoChatTG);
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x0033E304 File Offset: 0x0033C504
		public static string EncodeStringToByteArrayString(string inputString, string key)
		{
			string byteArrayString = BitConverter.ToString(ModFunc.EncodeToBytes(inputString, key)).Replace("-", "");
			return string.Join("-", ModFunc.SplitByLength(byteArrayString, 2));
		}

		// Token: 0x0600350A RID: 13578 RVA: 0x0033E340 File Offset: 0x0033C540
		private static byte[] EncodeToBytes(string inputString, string key)
		{
			byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
			byte[] keyBytes = Encoding.UTF8.GetBytes(key);
			byte[] encodedBytes = new byte[inputBytes.Length];
			for (int i = 0; i < inputBytes.Length; i++)
			{
				encodedBytes[i] = (byte)(inputBytes[i] ^ keyBytes[i % keyBytes.Length]);
			}
			return encodedBytes;
		}

		// Token: 0x0600350B RID: 13579 RVA: 0x0033E38C File Offset: 0x0033C58C
		private static string[] SplitByLength(string str, int length)
		{
			int strLength = str.Length;
			int numSegments = (strLength + length - 1) / length;
			string[] segments = new string[numSegments];
			for (int i = 0; i < numSegments; i++)
			{
				int startIndex = i * length;
				int segmentLength = Math.min(length, strLength - startIndex);
				segments[i] = str.Substring(startIndex, segmentLength);
			}
			return segments;
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x0033E3DC File Offset: 0x0033C5DC
		public static string DecodeByteArrayString(string byteArrayString, string key)
		{
			string result;
			try
			{
				string[] hexValues = byteArrayString.Split('-', StringSplitOptions.None);
				string concatenatedHex = string.Join("", hexValues);
				byte[] encodedBytes = new byte[concatenatedHex.Length / 2];
				for (int i = 0; i < encodedBytes.Length; i++)
				{
					encodedBytes[i] = Convert.ToByte(concatenatedHex.Substring(i * 2, 2), 16);
				}
				result = ModFunc.DecodeToString(encodedBytes, key);
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x0033E458 File Offset: 0x0033C658
		private static string DecodeToString(byte[] encodedBytes, string key)
		{
			byte[] keyBytes = Encoding.UTF8.GetBytes(key);
			byte[] decodedBytes = new byte[encodedBytes.Length];
			for (int i = 0; i < encodedBytes.Length; i++)
			{
				decodedBytes[i] = (byte)(encodedBytes[i] ^ keyBytes[i % keyBytes.Length]);
			}
			return Encoding.UTF8.GetString(decodedBytes);
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x0033E4A2 File Offset: 0x0033C6A2
		public static void Log(string text)
		{
			if (ModFunc.isDebugEnable)
			{
				Debug.Log(text);
			}
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x0033E4B4 File Offset: 0x0033C6B4
		public static void WriteLog(string message)
		{
			if (!ModFunc.isDebugEnable)
			{
				return;
			}
			try
			{
				StreamWriter streamWriter = new StreamWriter(new FileStream("log_" + DateTime.Today.ToString("yyyyMMdd") + ".txt", FileMode.OpenOrCreate));
				streamWriter.WriteLine(DateTime.Today.ToString("HH:mm:ss") + ": " + message);
				streamWriter.Flush();
				streamWriter.Close();
			}
			catch (Exception ex)
			{
				ModFunc.Log(ex.Message);
			}
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x0033E544 File Offset: 0x0033C744
		private void LoadSkillToScreen()
		{
			for (int i = 0; i < Char.myCharz().vSkill.size(); i++)
			{
				Skill skill = (Skill)Char.myCharz().vSkill.elementAt(i);
				if (GameCanvas.isTouch && !Main.isPC)
				{
					for (int j = 0; j < GameScr.onScreenSkill.Length; j++)
					{
						if (GameScr.onScreenSkill[j] == skill)
						{
							GameScr.onScreenSkill[j] = null;
						}
					}
					GameScr.onScreenSkill[i] = skill;
					GameScr.gI().saveonScreenSkillToRMS();
				}
				else
				{
					for (int k = 0; k < GameScr.keySkill.Length; k++)
					{
						if (GameScr.keySkill[k] == skill)
						{
							GameScr.keySkill[k] = null;
						}
					}
					GameScr.keySkill[i] = skill;
					GameScr.gI().saveKeySkillToRMS();
				}
			}
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x0033E604 File Offset: 0x0033C804
		public static void DoChatGlobal()
		{
			GameCanvas.endDlg();
			if (Char.myCharz().checkLuong() < 5)
			{
				GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
				return;
			}
			if (GameCanvas.panel.chatTField == null)
			{
				GameCanvas.panel.chatTField = new ChatTextField();
				GameCanvas.panel.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				GameCanvas.panel.chatTField.initChatTextField();
				GameCanvas.panel.chatTField.parentScreen = GameCanvas.panel;
			}
			GameCanvas.panel.chatTField.strChat = mResources.world_channel_5_luong;
			GameCanvas.panel.chatTField.tfChat.name = mResources.CHAT;
			GameCanvas.panel.chatTField.to = string.Empty;
			GameCanvas.panel.chatTField.isShow = true;
			GameCanvas.panel.chatTField.tfChat.isFocus = true;
			GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			if (Main.isWindowsPhone)
			{
				GameCanvas.panel.chatTField.tfChat.strInfo = GameCanvas.panel.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				GameCanvas.panel.chatTField.startChat(GameCanvas.panel, string.Empty);
				return;
			}
			if (GameCanvas.isTouch)
			{
				GameCanvas.panel.chatTField.tfChat.doChangeToTextBox();
			}
		}

		// Token: 0x06003512 RID: 13586 RVA: 0x0033E780 File Offset: 0x0033C980
		public void GoToBoss(int mapId)
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Đi tới\nMAP " + mapId.ToString(), this, 1, mapId.ToString()));
			myVector.addElement(new Command("Huỷ", this, 2, null));
			GameCanvas.menu.startAt(myVector, 4);
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x0033E7D7 File Offset: 0x0033C9D7
		public void ChangeFPSTarget()
		{
			Rms.saveRMSInt("isHighFps", this.isHighFps ? 1 : 0);
			if (this.isHighFps)
			{
				Application.targetFrameRate = 60;
				return;
			}
			Application.targetFrameRate = 30;
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x0033E806 File Offset: 0x0033CA06
		public static void changeStatusEffectInven()
		{
			if (ModFunc.isEffectInven)
			{
				ModFunc.isEffectInven = false;
				Rms.saveRMSInt("effectinven", ModFunc.isEffectInven ? 1 : 0);
				return;
			}
			ModFunc.isEffectInven = true;
			Rms.saveRMSInt("effectinven", ModFunc.isEffectInven ? 1 : 0);
		}

		// Token: 0x06003515 RID: 13589 RVA: 0x0033E848 File Offset: 0x0033CA48
		public static void chanegStatusInventory()
		{
			if (ModFunc.isInventory)
			{
				ModFunc.isInventory = false;
				Rms.saveRMSInt("inventory", ModFunc.isInventory ? 1 : 0);
				GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
				return;
			}
			ModFunc.isInventory = true;
			Rms.saveRMSInt("inventory", ModFunc.isInventory ? 1 : 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}

		// Token: 0x06003516 RID: 13590 RVA: 0x0033E8B4 File Offset: 0x0033CAB4
		public static void changeStatusLogo()
		{
			if (ModFunc.isLogo)
			{
				ModFunc.isLogo = false;
				ModFunc.imgLogoBig = null;
				ModFunc.logo = null;
				Rms.saveRMSInt("logo", 0);
				if (ModFunc.isLogoGif)
				{
					ModFunc.isLogoGif = false;
					Rms.saveRMSInt("logogif", 0);
					return;
				}
			}
			else
			{
				Rms.saveRMSInt("logo", 1);
				ModFunc.isLogo = true;
				if (ModFunc.isLogoGif)
				{
					ModFunc.LoadLogoGif();
					Rms.saveRMSInt("logogif", 1);
					return;
				}
				ModFunc.LoadLogoImages();
			}
		}

		// Token: 0x06003517 RID: 13591 RVA: 0x0033E92C File Offset: 0x0033CB2C
		public static void changeStatusBackground()
		{
			if (ModFunc.GiamDungLuong)
			{
				ModFunc.GiamDungLuong = false;
				Rms.saveRMSInt("background", ModFunc.GiamDungLuong ? 1 : 0);
				return;
			}
			ModFunc.GiamDungLuong = true;
			Rms.saveRMSInt("background", ModFunc.GiamDungLuong ? 1 : 0);
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x0033E96C File Offset: 0x0033CB6C
		public static void changeStatusAnPlayer()
		{
			if (ModFunc.AnPlayer)
			{
				ModFunc.AnPlayer = false;
				Rms.saveRMSInt("anplayer", ModFunc.AnPlayer ? 1 : 0);
				return;
			}
			ModFunc.AnPlayer = true;
			Rms.saveRMSInt("anplayer", ModFunc.AnPlayer ? 1 : 0);
		}

		// Token: 0x06003519 RID: 13593 RVA: 0x0033E9AC File Offset: 0x0033CBAC
		public static void changeStatusShowID()
		{
			if (ModFunc.isShowID)
			{
				ModFunc.isShowID = false;
				Rms.saveRMSInt("showid", ModFunc.isShowID ? 1 : 0);
				return;
			}
			ModFunc.isShowID = true;
			Rms.saveRMSInt("showid", ModFunc.isShowID ? 1 : 0);
		}

		// Token: 0x0600351A RID: 13594 RVA: 0x0033E9EC File Offset: 0x0033CBEC
		public static void changeStatusLogoGif()
		{
			if (ModFunc.isLogoGif)
			{
				ModFunc.isLogoGif = false;
				Rms.saveRMSInt("logogif", 0);
				return;
			}
			ModFunc.isLogoGif = true;
			Rms.saveRMSInt("logogif", 1);
		}

		// Token: 0x0600351B RID: 13595 RVA: 0x0033EA18 File Offset: 0x0033CC18
		public static Npc GetNpcByTempId(int tempId)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == tempId)
				{
					return npc;
				}
			}
			return null;
		}

		// Token: 0x0600351C RID: 13596 RVA: 0x0033EA5C File Offset: 0x0033CC5C
		public static void LoadLogoImages()
		{
			ModFunc.imgBg = GameCanvas.LoadImageFromRoot("/bg/bg.png");
			ModFunc.imgLogoBig = GameCanvas.LoadImageFromRoot("/bg/logo.png");
			ModFunc.imgLogoBig = GameCanvas.loadImage("/logoNormal/logo.png");
			if (ModFunc.imgLogoBig == null)
			{
				GameScr.info1.addInfo("Không thể load logo!", 0);
				ModFunc.isLogo = true;
				Rms.saveRMSInt("logo", 0);
			}
		}

		// Token: 0x0600351D RID: 13597 RVA: 0x0033EAC0 File Offset: 0x0033CCC0
		public static void LoadLogoGif()
		{
			for (int i = 0; i < ModFunc.FrameGif; i++)
			{
				ModFunc.logos[i] = GameCanvas.loadImage("/logoNormal/" + i.ToString() + ".png");
			}
		}

		// Token: 0x0600351E RID: 13598 RVA: 0x0033EB00 File Offset: 0x0033CD00
		public static void PaintLogoGif(mGraphics g, int x, int y, int anchor)
		{
			if (!ModFunc.isLogo)
			{
				return;
			}
			if (ModFunc.isLogoGif)
			{
				int id = GameCanvas.gameTick / 2 % ModFunc.FrameGif;
				if (ModFunc.logos[id] != null)
				{
					g.drawImage(ModFunc.logos[id], x, y, anchor);
					return;
				}
			}
			else if (ModFunc.imgLogoBig != null)
			{
				g.drawImage(ModFunc.imgLogoBig, x, y, anchor);
			}
		}

		// Token: 0x0600351F RID: 13599 RVA: 0x0033EB5C File Offset: 0x0033CD5C
		public static void LoadLogoGifMenu()
		{
			for (int i = 0; i < ModFunc.FrameGifMenu; i++)
			{
				ModFunc.logosMenu[i] = GameCanvas.loadImage("/GifMenu/love-" + i.ToString() + ".png");
			}
		}

		// Token: 0x06003520 RID: 13600 RVA: 0x0033EB9C File Offset: 0x0033CD9C
		public static void PaintLogoGifMenu(mGraphics g, int x, int y, int anchor)
		{
			int id = GameCanvas.gameTick / 2 % ModFunc.FrameGifMenu;
			if (ModFunc.logosMenu[id] != null)
			{
				g.drawImage(ModFunc.logosMenu[id], x, y, anchor);
			}
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x0033EBD0 File Offset: 0x0033CDD0
		public static void LoadTickImages()
		{
			for (int i = 0; i < 20; i++)
			{
				ModFunc.ticks[i] = GameCanvas.loadImage("/tick/tick_" + i.ToString());
			}
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x0033EC08 File Offset: 0x0033CE08
		public static void PaintTicks(mGraphics g, int x, int y)
		{
			int id = GameCanvas.gameTick / 4 % 20;
			if (ModFunc.ticks[id] != null)
			{
				g.drawImage(ModFunc.ticks[id], x, y);
			}
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x0033EC38 File Offset: 0x0033CE38
		private static IEnumerator LoadFile(string fullPath)
		{
			string fileUri = "file://" + fullPath;
			using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(fileUri, AudioType.OGGVORBIS);
			www.certificateHandler = new BypassCertificateHandler();
			yield return www.SendWebRequest();
			if (www.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError(www.error);
				yield break;
			}
			AudioClip temp = DownloadHandlerAudioClip.GetContent(www);
			musics.Add(temp);
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x0033EC48 File Offset: 0x0033CE48
		public static void InitMusic()
		{
			int fromRms = Rms.loadRMSInt("musicSize");
			ModFunc.musicCount = ((fromRms != -1) ? fromRms : 0);
			for (int i = 0; i < ModFunc.musicCount; i++)
			{
				string fullPath = Rms.GetiPhoneDocumentsPath() + "/music_" + i.ToString() + ".ogg";
				if (File.Exists(fullPath))
				{
					CoroutineRunner.Instance.RunCoroutine(ModFunc.LoadFile(fullPath));
				}
				else
				{
					Debug.LogWarning("File does not exist: " + fullPath);
				}
			}
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x0033ECC4 File Offset: 0x0033CEC4
		public static string Decrypt(string encryptedText, int keys)
		{
			Debug.Log("Chuỗi nhận được để giải mã: " + encryptedText);
			if (string.IsNullOrEmpty(encryptedText))
			{
				return string.Empty;
			}
			int padding = 0;
			while ((encryptedText.Length + padding) % 5 != 0)
			{
				padding++;
			}
			if (padding > 0)
			{
				encryptedText = encryptedText.PadRight(encryptedText.Length + padding, 'u');
			}
			List<byte> result = new List<byte>();
			for (int i = 0; i < encryptedText.Length; i += 5)
			{
				ulong value = 0UL;
				for (int j = 0; j < 5; j++)
				{
					int charIndex = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!#$%&()*+-;<=>?@^_`{|}~".IndexOf(encryptedText[i + j]);
					if (charIndex == -1)
					{
						Debug.LogError(string.Format("Ký tự không hợp lệ trong chuỗi mã hóa: {0} tại vị trí {1}", encryptedText[i + j], i + j));
						throw new Exception(string.Format("Ký tự không hợp lệ trong chuỗi mã hóa: {0}", encryptedText[i + j]));
					}
					value = value * 85UL + (ulong)charIndex;
				}
				result.Add((byte)(value >> 24));
				result.Add((byte)(value >> 16));
				result.Add((byte)(value >> 8));
				result.Add((byte)value);
			}
			if (padding > 0)
			{
				result.RemoveRange(result.Count - padding, padding);
			}
			byte[] array = result.ToArray();
			byte[] salt = new byte[16];
			byte[] iv = new byte[16];
			byte[] cipherText = new byte[array.Length - 32];
			Buffer.BlockCopy(array, 0, salt, 0, 16);
			Buffer.BlockCopy(array, 16, iv, 0, 16);
			Buffer.BlockCopy(array, 32, cipherText, 0, cipherText.Length);
			string keysStr = keys.ToString();
			byte[] keyBytes = Encoding.UTF8.GetBytes(keysStr).Concat(salt).ToArray<byte>();
			byte[] key = new byte[32];
			for (int k = 0; k < 32; k++)
			{
				key[k] = keyBytes[k % keyBytes.Length];
			}
			string result2;
			using (Aes aes = Aes.Create())
			{
				aes.Key = key;
				aes.IV = iv;
				aes.Mode = CipherMode.CBC;
				aes.Padding = PaddingMode.PKCS7;
				using (ICryptoTransform decryptor = aes.CreateDecryptor())
				{
					using (MemoryStream msDecrypt = new MemoryStream(cipherText))
					{
						using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
						{
							using (StreamReader srDecrypt = new StreamReader(csDecrypt))
							{
								result2 = srDecrypt.ReadToEnd();
							}
						}
					}
				}
			}
			return result2;
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x0033EF6C File Offset: 0x0033D16C
		public static bool AutoLogin()
		{
			if (ModFunc.autoLogin == null || ModFunc.autoLogin.waitToNextLogin)
			{
				return false;
			}
			if (!Util.CanDoWithTime(ModFunc.autoLogin.lastTimeWait, 500L))
			{
				return false;
			}
			if (ServerListScreen.ipSelect < 0 || ServerListScreen.ipSelect >= ServerListScreen.address.Length || string.IsNullOrEmpty(ServerListScreen.address[ServerListScreen.ipSelect]) || ServerListScreen.testConnect != 2)
			{
				ServerListScreen.LoadIP();
				if (GameCanvas.serverScreen == null)
				{
					GameCanvas.serverScreen = new ServerListScreen();
				}
				GameCanvas.serverScreen.switchToMe();
				ModFunc.autoLogin.lastTimeWait = mSystem.currentTimeMillis();
				return false;
			}
			if (GameCanvas.currentScreen != GameCanvas.loginScr)
			{
				if (GameCanvas.loginScr == null)
				{
					GameCanvas.loginScr = new LoginScr();
				}
				GameCanvas.loginScr.switchToMe();
				ModFunc.autoLogin.lastTimeWait = mSystem.currentTimeMillis();
				return false;
			}
			if (!ModFunc.autoLogin.hasSetUserPass)
			{
				Account account = ModFunc.autoLogin.GetAccWithUsername(ModFunc.accounts);
				if (account.getUsername().Length > 0)
				{
					Rms.saveRMSString("acc", account.getUsername());
					Rms.saveRMSString("pass", account.getPassword());
					GameCanvas.loginScr.setUserPass();
					ModFunc.autoLogin.hasSetUserPass = true;
				}
				ModFunc.autoLogin.lastTimeWait = mSystem.currentTimeMillis();
			}
			GameCanvas.loginScr.doLogin();
			ModFunc.autoLogin.waitToNextLogin = true;
			return true;
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x0033F0C8 File Offset: 0x0033D2C8
		private void SaveButtonPositions()
		{
			string posData = "";
			foreach (KeyValuePair<string, ModFunc.Point> kvp in ModFunc.buttonPositions)
			{
				posData += string.Format("{0},{1},{2};", kvp.Key, kvp.Value.x, kvp.Value.y);
			}
			Rms.saveRMSString("buttonPositions", posData);
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x0033F160 File Offset: 0x0033D360
		private void LoadButtonPositions()
		{
			string posData = Rms.loadRMSString("buttonPositions");
			if (!string.IsNullOrEmpty(posData))
			{
				ModFunc.buttonPositions.Clear();
				foreach (string button in posData.Split(';', StringSplitOptions.None))
				{
					if (!string.IsNullOrEmpty(button))
					{
						string[] parts = button.Split(',', StringSplitOptions.None);
						if (parts.Length == 3)
						{
							string name = parts[0];
							int x;
							int y;
							if (int.TryParse(parts[1], out x) && int.TryParse(parts[2], out y))
							{
								ModFunc.buttonPositions[name] = new ModFunc.Point(x, y);
							}
						}
					}
				}
				return;
			}
			ModFunc.InitButtonPositions();
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x0033F1FC File Offset: 0x0033D3FC
		public static void changeStatusEditButton()
		{
			if (ModFunc.isEditButton)
			{
				ModFunc.isEditButton = false;
				Rms.saveRMSInt("editbutton", 0);
				GameScr.info1.addInfo("Đã tắt chế độ chỉnh sửa nút", 0);
				return;
			}
			ModFunc.isEditButton = true;
			GameCanvas.panel.isShow = false;
			Rms.saveRMSInt("editbutton", 1);
			GameScr.info1.addInfo("Đã bật chế độ chỉnh sửa nút", 0);
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x0033F260 File Offset: 0x0033D460
		private void AddOrRemoveFilterItem(Item item, bool isAdd)
		{
			if (isAdd)
			{
				ModFunc.listFilterItems.Add(new ItemAutoFilter((int)item.template.iconID, (int)item.template.id, item.template.name));
				GameScr.info1.addInfo("Đã thêm " + item.template.name + " vào DS lọc đồ", 0);
				return;
			}
			foreach (ItemAutoFilter itemFilter in ModFunc.listFilterItems)
			{
				if (itemFilter.iconID == (int)item.template.iconID && itemFilter.id == (int)item.template.id && itemFilter.name == item.template.name)
				{
					ModFunc.listFilterItems.Remove(itemFilter);
					GameScr.info1.addInfo("Đã xóa " + item.template.name + " khỏi DS lọc đồ", 0);
					break;
				}
			}
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x0033F378 File Offset: 0x0033D578
		private void ShowFilterList(mGraphics g)
		{
			int itemHeight = 25;
			int padding = 10;
			int headerHeight = 30;
			int resizeHandleSize = 40;
			int resizeX = ModFunc.panelX + ModFunc.panelW - resizeHandleSize;
			int resizeY = ModFunc.panelY + ModFunc.panelH - resizeHandleSize;
			if (GameCanvas.isPointerDown && GameCanvas.isPointerHoldIn(ModFunc.panelX, ModFunc.panelY, ModFunc.panelW, headerHeight))
			{
				GameCanvas.isPointerJustDown = false;
				if (!ModFunc.isDragging)
				{
					ModFunc.isDragging = true;
					this.lastMouseX = GameCanvas.px;
					this.lastMouseY = GameCanvas.py;
					this.lastPanelX = ModFunc.panelX;
					this.lastPanelY = ModFunc.panelY;
				}
				else
				{
					float smoothFactor = 1f;
					int deltaX = GameCanvas.px - this.lastMouseX;
					int deltaY = GameCanvas.py - this.lastMouseY;
					int targetX = this.lastPanelX + deltaX;
					int targetY = this.lastPanelY + deltaY;
					ModFunc.panelX = (int)((float)ModFunc.panelX + (float)(targetX - ModFunc.panelX) * smoothFactor);
					ModFunc.panelY = (int)((float)ModFunc.panelY + (float)(targetY - ModFunc.panelY) * smoothFactor);
					this.lastMouseX = GameCanvas.px;
					this.lastMouseY = GameCanvas.py;
					this.lastPanelX = ModFunc.panelX;
					this.lastPanelY = ModFunc.panelY;
				}
				ModFunc.panelX = System.Math.Max(0, System.Math.Min(GameCanvas.w - ModFunc.panelW, ModFunc.panelX));
				ModFunc.panelY = System.Math.Max(0, System.Math.Min(GameCanvas.h - ModFunc.panelH, ModFunc.panelY));
			}
			else
			{
				ModFunc.isDragging = false;
			}
			if (GameCanvas.isPointerDown && !ModFunc.isDragging)
			{
				bool flag = GameCanvas.px >= ModFunc.panelX + ModFunc.panelW - resizeHandleSize && GameCanvas.px <= ModFunc.panelX + ModFunc.panelW;
				bool isInResizeZoneY = GameCanvas.py >= ModFunc.panelY + ModFunc.panelH - resizeHandleSize && GameCanvas.py <= ModFunc.panelY + ModFunc.panelH;
				if ((flag && GameCanvas.py >= ModFunc.panelY + ModFunc.panelH - resizeHandleSize) || (isInResizeZoneY && GameCanvas.px >= ModFunc.panelX + ModFunc.panelW - resizeHandleSize))
				{
					GameCanvas.isPointerJustDown = false;
					if (!this.isResizing)
					{
						this.isResizing = true;
						this.lastMouseX = GameCanvas.px;
						this.lastMouseY = GameCanvas.py;
						this.lastPanelW = ModFunc.panelW;
						this.lastPanelH = ModFunc.panelH;
					}
					else
					{
						float smoothFactor2 = 1f;
						int deltaX2 = GameCanvas.px - this.lastMouseX;
						int deltaY2 = GameCanvas.py - this.lastMouseY;
						int targetW = this.lastPanelW + deltaX2;
						int targetH = this.lastPanelH + deltaY2;
						ModFunc.panelW = (int)((float)ModFunc.panelW + (float)(targetW - ModFunc.panelW) * smoothFactor2);
						ModFunc.panelH = (int)((float)ModFunc.panelH + (float)(targetH - ModFunc.panelH) * smoothFactor2);
						this.lastMouseX = GameCanvas.px;
						this.lastMouseY = GameCanvas.py;
						this.lastPanelW = ModFunc.panelW;
						this.lastPanelH = ModFunc.panelH;
						ModFunc.panelW = System.Math.Max(180, System.Math.Min(GameCanvas.w - ModFunc.panelX, ModFunc.panelW));
						ModFunc.panelH = System.Math.Max(120, System.Math.Min(GameCanvas.h - ModFunc.panelY, ModFunc.panelH));
					}
				}
			}
			else if (!GameCanvas.isPointerDown)
			{
				this.isResizing = false;
			}
			g.setColor(0, 0.7f);
			g.fillRect(ModFunc.panelX, ModFunc.panelY, ModFunc.panelW, ModFunc.panelH, 5);
			for (int i = 0; i < 3; i++)
			{
				g.setColor(16777215, 0.2f - (float)i * 0.05f);
				g.drawRect(ModFunc.panelX + i, ModFunc.panelY + i, ModFunc.panelW - i * 2, ModFunc.panelH - i * 2);
			}
			g.setColor(16777215, 0.8f);
			for (int j = 0; j < 3; j++)
			{
				g.drawLine(resizeX + 5, ModFunc.panelY + ModFunc.panelH - 10 - j * 5, ModFunc.panelX + ModFunc.panelW - 5, ModFunc.panelY + ModFunc.panelH - 10 - j * 5);
			}
			for (int k = 0; k < 3; k++)
			{
				g.drawLine(ModFunc.panelX + ModFunc.panelW - 10 - k * 5, resizeY + 5, ModFunc.panelX + ModFunc.panelW - 10 - k * 5, ModFunc.panelY + ModFunc.panelH - 5);
			}
			int btnSize = 16;
			g.setColor(16733525);
			g.fillRect(ModFunc.panelX + ModFunc.panelW - btnSize - 5, ModFunc.panelY + 5, btnSize, btnSize, 5);
			mFont.tahoma_7b_white.drawString(g, "X", ModFunc.panelX + ModFunc.panelW - btnSize / 2 - 5, ModFunc.panelY + 7, mFont.CENTER);
			int autoFilterBtnW = 80;
			int autoFilterBtnH = 20;
			int autoFilterBtnX = ModFunc.panelX + ModFunc.panelW - 80;
			int autoFilterBtnY = ModFunc.panelY + ModFunc.panelH - autoFilterBtnH + 25;
			g.setColor(ModFunc.isAutoFilterItem ? 65280 : 16711680);
			g.fillRect(autoFilterBtnX, autoFilterBtnY, autoFilterBtnW, autoFilterBtnH, 8);
			string btnText = ModFunc.isAutoFilterItem ? "Auto: Bật" : "Auto: Tắt";
			mFont.tahoma_7b_white.drawStringBorder(g, btnText, autoFilterBtnX + autoFilterBtnW / 2 + 1, autoFilterBtnY + 6 + 1, mFont.CENTER, mFont.tahoma_7_grey);
			if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && GameCanvas.isPointerHoldIn(autoFilterBtnX, autoFilterBtnY, autoFilterBtnW, autoFilterBtnH))
			{
				ModFunc.isAutoFilterItem = !ModFunc.isAutoFilterItem;
				GameScr.info1.addInfo(ModFunc.isAutoFilterItem ? "Đã bật auto lọc đồ" : "Đã tắt auto lọc đồ", 0);
				GameCanvas.clearAllPointerEvent();
			}
			string title = "Danh sách vật phẩm lọc";
			int titleY = ModFunc.panelY + padding;
			mFont.tahoma_7b_white.drawString(g, title, ModFunc.panelX + ModFunc.panelW / 2, titleY, mFont.CENTER);
			g.setColor(5987163);
			g.fillRect(ModFunc.panelX + padding, titleY + 12, ModFunc.panelW - padding * 2, 1, 5);
			g.setClip(ModFunc.panelX, ModFunc.panelY + 35, ModFunc.panelW, ModFunc.panelH - 45);
			int contentHeight = ModFunc.listFilterItems.Count * itemHeight;
			int visibleHeight = ModFunc.panelH - 45;
			int maxScroll = System.Math.Max(0, contentHeight - visibleHeight);
			if (GameCanvas.isPointerDown && !ModFunc.isDragging && !this.isResizing)
			{
				GameCanvas.isPointerJustDown = false;
				if (!this.isScrolling)
				{
					this.isScrolling = true;
					this.lastMouseY = GameCanvas.py;
					this.lastScrollY = this.scrollY;
				}
				else
				{
					float smoothFactor3 = 1f;
					int deltaY3 = this.lastMouseY - GameCanvas.py;
					int targetScroll = this.lastScrollY + deltaY3;
					this.scrollY = (int)((float)this.scrollY + (float)(targetScroll - this.scrollY) * smoothFactor3);
					this.lastMouseY = GameCanvas.py;
					this.lastScrollY = this.scrollY;
				}
				this.scrollY = System.Math.Max(0, System.Math.Min(maxScroll, this.scrollY));
			}
			else if (!GameCanvas.isPointerDown)
			{
				this.isScrolling = false;
			}
			int num = this.scrollY / itemHeight;
			int endIndex = System.Math.Min(num + this.MAX_ITEMS_VISIBLE, ModFunc.listFilterItems.Count);
			for (int l = num; l < endIndex; l++)
			{
				ItemAutoFilter item = ModFunc.listFilterItems[l];
				int itemY = ModFunc.panelY + 35 + l * itemHeight - this.scrollY;
				if (l % 2 == 0)
				{
					g.setColor(2105376, 0.3f);
					g.fillRect(ModFunc.panelX + 5, itemY, ModFunc.panelW - 10, itemHeight - 2, 5);
				}
				string info = item.name ?? "";
				mFont.tahoma_7_white.drawString(g, info, ModFunc.panelX + padding, itemY + 5, 0);
				mFont.tahoma_7_red.drawString(g, string.Format("ID: {0}", item.id), ModFunc.panelX + padding, itemY + 15, 0);
				int delBtnW = 35;
				int delBtnH = 18;
				int delBtnX = ModFunc.panelX + ModFunc.panelW - delBtnW - padding;
				int delBtnY = itemY + 3;
				g.setColor(16724787);
				g.fillRect(delBtnX, delBtnY, delBtnW, delBtnH, 5);
				mFont.tahoma_7b_white.drawString(g, "Xóa", delBtnX + delBtnW / 2, delBtnY + 4, mFont.CENTER);
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					GameCanvas.isPointerJustDown = false;
					if (GameCanvas.isPointerHoldIn(delBtnX, delBtnY, delBtnW, delBtnH))
					{
						ModFunc.listFilterItems.RemoveAt(l);
						GameScr.info1.addInfo("Đã xóa vật phẩm khỏi danh sách lọc", 0);
						GameCanvas.clearAllPointerEvent();
					}
				}
			}
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			int scrollBarX = ModFunc.panelX + ModFunc.panelW - 8;
			int scrollBarY = ModFunc.panelY + 35;
			int scrollBarH = ModFunc.panelH - 45;
			int scrollBarW = 4;
			g.setColor(3355443);
			g.fillRect(scrollBarX, scrollBarY, scrollBarW, scrollBarH, 5);
			g.setColor(6710886);
			g.drawRect(scrollBarX, scrollBarY, scrollBarW, scrollBarH);
			if (contentHeight > visibleHeight)
			{
				float scrollRatio = (float)visibleHeight / (float)contentHeight;
				int scrollThumbH = (int)((float)scrollBarH * scrollRatio);
				int scrollThumbY = scrollBarY;
				if (this.scrollY > 0)
				{
					float scrollPercent = (float)this.scrollY / (float)maxScroll;
					scrollThumbY = scrollBarY + (int)((float)(scrollBarH - scrollThumbH) * scrollPercent);
				}
				scrollThumbY = System.Math.Max(scrollBarY, System.Math.Min(scrollBarY + scrollBarH - scrollThumbH, scrollThumbY));
				g.setColor(8947848);
				g.fillRect(scrollBarX + 1, scrollThumbY, scrollBarW - 2, scrollThumbH, 5);
				g.setColor(11184810);
				g.fillRect(scrollBarX + 1, scrollThumbY, scrollBarW - 2, 2, 5);
				g.setColor(6710886);
				g.fillRect(scrollBarX + 1, scrollThumbY + scrollThumbH - 2, scrollBarW - 2, 2, 5);
			}
			if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				GameCanvas.isPointerJustDown = false;
				if (GameCanvas.isPointerHoldIn(ModFunc.panelX + ModFunc.panelW - btnSize - 5, ModFunc.panelY + 5, btnSize, btnSize))
				{
					ModFunc.isShowFilterList = false;
					this.scrollY = 0;
					GameCanvas.clearAllPointerEvent();
				}
			}
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x0033FD80 File Offset: 0x0033DF80
		private void DoFilter()
		{
			if (!ModFunc.isAutoFilterItem)
			{
				return;
			}
			try
			{
				for (int i = 0; i < Char.myCharz().arrItemBag.Length; i++)
				{
					Item item = Char.myCharz().arrItemBag[i];
					if (item != null)
					{
						using (List<ItemAutoFilter>.Enumerator enumerator = ModFunc.listFilterItems.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.id == (int)item.template.id)
								{
									Service.gI().useItem(1, 1, (sbyte)item.indexUI, -1);
									Thread.Sleep(50);
									return;
								}
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x0033FE40 File Offset: 0x0033E040
		public static void DoBoss()
		{
			if (string.IsNullOrEmpty(ModFunc.bossCanDo))
			{
				GameScr.info1.addInfo("Chưa nhập boss cần tìm", 0);
				ModFunc.zoneMacDinh = 0;
				ModFunc.isdoBoss = false;
				return;
			}
			if (Input.GetKey("q"))
			{
				GameScr.info1.addInfo("Đã tắt auto dò boss", 0);
				ModFunc.isdoBoss = false;
				return;
			}
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				Char @char = (Char)GameScr.vCharInMap.elementAt(i);
				if (@char != null && @char.cName.ToLower().Contains(ModFunc.bossCanDo.ToLower()) && @char.cTypePk == 5)
				{
					Sound.start(1f, Sound.l1);
					GameScr.info1.addInfo("Đã tìm thấy boss", 0);
					ModFunc.zoneMacDinh = 0;
					ModFunc.isdoBoss = false;
					return;
				}
			}
			if (GameScr.gI().numPlayer == null || GameScr.gI().numPlayer.Length == 0)
			{
				Service.gI().openUIZone();
				return;
			}
			Service.gI().requestChangeZone(ModFunc.zoneMacDinh, -1);
			if (!Char.isLoadingMap && TileMap.zoneID == ModFunc.zoneMacDinh)
			{
				ModFunc.zoneMacDinh++;
				if (ModFunc.zoneMacDinh >= GameScr.gI().numPlayer.Length)
				{
					ModFunc.zoneMacDinh = 0;
				}
			}
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x0033FF80 File Offset: 0x0033E180
		public static void LoadImgMenuChat()
		{
			ModFunc.imgMenuChat = GameCanvas.loadImage("/mainImage/MenuChat.png");
			ModFunc.imgCloseButton = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
			ModFunc.imgNextPage = GameCanvas.loadImage("/mainImage/myTexture2dbtnl.png");
			ModFunc.imgNextPage2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf.png");
			ModFunc.imgPrevPage = GameCanvas.loadImage("/mainImage/myTexture2dbtnl.png");
			ModFunc.imgPrevPage2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf.png");
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x0033FFE8 File Offset: 0x0033E1E8
		public static void PaintMenuChat(mGraphics g)
		{
			if (!ModFunc.isShowMenuChat)
			{
				return;
			}
			int menuChatX = (GameCanvas.w - ModFunc.imgMenuChat.getWidth()) / 2;
			int menuChatY = (GameCanvas.h - ModFunc.imgMenuChat.getHeight()) / 2;
			g.drawImage(ModFunc.imgMenuChat, menuChatX, menuChatY);
			g.drawImage(ModFunc.imgCloseButton, menuChatX + ModFunc.imgMenuChat.getWidth() - ModFunc.imgCloseButton.getWidth(), menuChatY);
			string chatInfo = "Nhập lệnh chat tại đây:";
			mFont.tahoma_7b_red.drawString(g, chatInfo, menuChatX + 30, menuChatY + 10, mFont.LEFT);
			Dictionary<string, string> chatCommands = new Dictionary<string, string>
			{
				{
					"htl",
					"Bật/tắt hành trang lưới"
				},
				{
					"loadskill",
					"Tải lại ô skill"
				},
				{
					"ak",
					"Bật/tắt tự động tấn công"
				},
				{
					"ts",
					"Bật/tắt chế độ tàn sát"
				},
				{
					"tsnguoi",
					"Bật/tắt chế độ tàn sát người"
				},
				{
					"vqmm",
					"Bật/tắt tự động VQMM"
				},
				{
					"ukhu",
					"Bật/tắt cập nhật khu tự động"
				},
				{
					"k X",
					"Chuyển đến khu X (VD: k 5)"
				},
				{
					"s X",
					"Thay đổi tốc độ game (1-10)"
				},
				{
					"atc text",
					"Thiết lập tin nhắn tự động"
				},
				{
					"atctg text",
					"Thiết lập tin nhắn tự động thế giới"
				},
				{
					"do text",
					"Thiết lập boss cần dò"
				},
				{
					"dbx",
					"Bật/tắt tự động dò boss"
				},
				{
					"gtv",
					"Bật/tắt gõ Tiếng Việt"
				}
			};
			int totalPages = (int)System.Math.Ceiling((double)chatCommands.Count / 8.0);
			int num = ModFunc.currentPage * 8;
			int endIndex = System.Math.Min(num + 8, chatCommands.Count);
			int commandY = menuChatY + 30;
			for (int i = num; i < endIndex; i++)
			{
				KeyValuePair<string, string> command = chatCommands.ElementAt(i);
				string commandText = command.Key + ": " + command.Value;
				mFont.tahoma_7_yellow.drawStringBorder(g, commandText, menuChatX + 35, commandY, mFont.LEFT, mFont.tahoma_7_grey);
				commandY += 15;
			}
			if (ModFunc.currentPage > 0)
			{
				g.drawImage(ModFunc.imgPrevPage, menuChatX + 30, menuChatY + ModFunc.imgMenuChat.getHeight() - 30);
				mFont.tahoma_7b_white.drawString(g, "Trang trước", menuChatX + 40, menuChatY + ModFunc.imgMenuChat.getHeight() - 22, mFont.LEFT);
			}
			if (ModFunc.currentPage < totalPages - 1)
			{
				g.drawImage(ModFunc.imgNextPage, menuChatX + ModFunc.imgMenuChat.getWidth() - 100, menuChatY + ModFunc.imgMenuChat.getHeight() - 30);
				mFont.tahoma_7b_white.drawString(g, "Trang sau", menuChatX + ModFunc.imgMenuChat.getWidth() - 43, menuChatY + ModFunc.imgMenuChat.getHeight() - 22, mFont.RIGHT);
			}
			ModFunc.PaintLogoGifMenu(g, menuChatX + ModFunc.imgMenuChat.getWidth() - 150, menuChatY + (ModFunc.imgMenuChat.getHeight() - ModFunc.FrameGifMenu) / 2, mFont.CENTER);
			if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				if (GameCanvas.isPointerHoldIn(menuChatX + ModFunc.imgMenuChat.getWidth() - ModFunc.imgCloseButton.getWidth(), menuChatY, ModFunc.imgCloseButton.getWidth(), ModFunc.imgCloseButton.getHeight()))
				{
					ModFunc.isShowMenuChat = false;
					GameCanvas.clearAllPointerEvent();
				}
				if (ModFunc.currentPage > 0 && GameCanvas.isPointerHoldIn(menuChatX + 10, menuChatY + ModFunc.imgMenuChat.getHeight() - 30, 80, 20))
				{
					ModFunc.currentPage--;
				}
				if (ModFunc.currentPage < totalPages - 1 && GameCanvas.isPointerHoldIn(menuChatX + ModFunc.imgMenuChat.getWidth() - 80, menuChatY + ModFunc.imgMenuChat.getHeight() - 30, 80, 20))
				{
					ModFunc.currentPage++;
				}
			}
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x00340390 File Offset: 0x0033E590
		public void SetAutoIntrinsic(int param)
		{
			if (this.curSelectIntrinsic.Length <= 0)
			{
				GameScr.info1.addInfo("Chỉ số đã chọn không đúng! (1)", 0);
				return;
			}
			int maxParam;
			if (int.TryParse(this.curSelectIntrinsic.Split("đến ", StringSplitOptions.None)[1].Split("%", StringSplitOptions.None)[0], out maxParam) && param > 0 && param <= maxParam)
			{
				this.paramIntrinsic = param;
				if (this.curSelectIntrinsic.Contains("+"))
				{
					this.curSelectIntrinsic = this.curSelectIntrinsic.Split("+", StringSplitOptions.None)[0].Trim();
				}
				else
				{
					if (!this.curSelectIntrinsic.Contains("dưới"))
					{
						this.paramIntrinsic = -1;
						this.curSelectIntrinsic = "";
						GameScr.info1.addInfo("Có lỗi xảy ra, vui lòng liên hệ ADMIN!", 0);
						return;
					}
					this.curSelectIntrinsic = this.curSelectIntrinsic.Split("dưới ", StringSplitOptions.None)[0].Trim();
				}
				new Thread(new ThreadStart(this.DoAutoIntrinsic)).Start();
				return;
			}
			GameScr.info1.addInfo("Chỉ số đã chọn không đúng! (0)", 0);
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x003404B0 File Offset: 0x0033E6B0
		private void DoAutoIntrinsic()
		{
			while (this.paramIntrinsic != -1)
			{
				Service.gI().speacialSkill(0);
				Thread.Sleep(500);
				Service.gI().confirmMenu(5, 2);
				Thread.Sleep(500);
				Service.gI().confirmMenu(5, 0);
				Thread.Sleep(500);
			}
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x0034050C File Offset: 0x0033E70C
		public void CheckAutoIntrinsic(string info)
		{
			if (info.Contains("+"))
			{
				string[] array = info.Split("+", StringSplitOptions.None);
				string recvName = array[0].Trim();
				int recvParam;
				if (int.TryParse(array[1].Split("%", StringSplitOptions.None)[0], out recvParam) && this.curSelectIntrinsic == recvName && recvParam >= this.paramIntrinsic)
				{
					GameScr.info1.addInfo(string.Concat(new string[]
					{
						"Mở nội tại ",
						this.curSelectIntrinsic,
						" ",
						this.paramIntrinsic.ToString(),
						"% thành công!"
					}), 0);
					this.paramIntrinsic = -1;
					this.curSelectIntrinsic = "";
					GameCanvas.menu.menuSelectedItem = GameCanvas.menu.menuItems.size() - 1;
					GameCanvas.menu.performSelect();
					GameCanvas.menu.doCloseMenu();
					return;
				}
			}
			else if (info.Contains("dưới"))
			{
				string[] array2 = info.Split("dưới ", StringSplitOptions.None);
				string recvName2 = array2[0].Trim();
				int recvParam2;
				if (int.TryParse(array2[1].Split("%", StringSplitOptions.None)[0], out recvParam2) && this.curSelectIntrinsic == recvName2 && recvParam2 >= this.paramIntrinsic)
				{
					GameScr.info1.addInfo(string.Concat(new string[]
					{
						"Mở nội tại ",
						this.curSelectIntrinsic,
						" ",
						this.paramIntrinsic.ToString(),
						"% thành công!"
					}), 0);
					this.paramIntrinsic = -1;
					this.curSelectIntrinsic = "";
					GameCanvas.menu.menuSelectedItem = GameCanvas.menu.menuItems.size() - 1;
					GameCanvas.menu.performSelect();
					GameCanvas.menu.doCloseMenu();
					return;
				}
			}
			else
			{
				this.paramIntrinsic = -1;
				this.curSelectIntrinsic = "";
				GameCanvas.menu.doCloseMenu();
			}
		}

		// Token: 0x04006699 RID: 26265
		private static readonly ModFunc Instance = new ModFunc();

		// Token: 0x0400669A RID: 26266
		public static string homeUrl = "Mod6tab đồng bộ";

		// Token: 0x0400669B RID: 26267
		public static bool ModNotLogo = false;

		// Token: 0x0400669C RID: 26268
		public static bool ModNotLogoGif = false;

		// Token: 0x0400669D RID: 26269
		public static bool isReadInt = true;

		// Token: 0x0400669E RID: 26270
		public static bool isVietnamese = false;

		// Token: 0x0400669F RID: 26271
		public static bool isShowMenuChat = false;

		// Token: 0x040066A0 RID: 26272
		public static bool isMenuVisible = false;

		// Token: 0x040066A1 RID: 26273
		public static float arrowRotation = 0f;

		// Token: 0x040066A2 RID: 26274
		public static float menuX = 0f;

		// Token: 0x040066A3 RID: 26275
		public static float targetMenuX = 0f;

		// Token: 0x040066A4 RID: 26276
		public static float targetArrowRotation = 0f;

		// Token: 0x040066A5 RID: 26277
		public static float ANIMATION_SPEED = 0.1f;

		// Token: 0x040066A6 RID: 26278
		private static bool isDebugEnable = false;

		// Token: 0x040066A7 RID: 26279
		private static long lastTimeLog = 0L;

		// Token: 0x040066A8 RID: 26280
		public bool canUpdate;

		// Token: 0x040066A9 RID: 26281
		public static Command cmdAccManager;

		// Token: 0x040066AA RID: 26282
		public static bool isOpenAccMAnager = false;

		// Token: 0x040066AB RID: 26283
		public static List<Account> accounts = new List<Account>();

		// Token: 0x040066AC RID: 26284
		public List<Command> cmdsChooseAcc = new List<Command>();

		// Token: 0x040066AD RID: 26285
		public List<Command> cmdsDelAcc = new List<Command>();

		// Token: 0x040066AE RID: 26286
		public static Command cmdCloseAccManager;

		// Token: 0x040066AF RID: 26287
		private static int modKeyPosX;

		// Token: 0x040066B0 RID: 26288
		private static int modKeyPosY;

		// Token: 0x040066B1 RID: 26289
		public static bool isAutoLogin = false;

		// Token: 0x040066B2 RID: 26290
		public static bool dangLogin = false;

		// Token: 0x040066B3 RID: 26291
		public static AutoLogin autoLogin;

		// Token: 0x040066B4 RID: 26292
		public static bool isAutoNoitai = false;

		// Token: 0x040066B5 RID: 26293
		public bool autoAttack;

		// Token: 0x040066B6 RID: 26294
		public bool autoWakeUp;

		// Token: 0x040066B7 RID: 26295
		public long lastAutoWakeUp;

		// Token: 0x040066B8 RID: 26296
		public bool isAutoPhaLe;

		// Token: 0x040066B9 RID: 26297
		public bool isAutoVQMM;

		// Token: 0x040066BA RID: 26298
		public long lastVQMM;

		// Token: 0x040066BB RID: 26299
		private long lastAutoAttack;

		// Token: 0x040066BC RID: 26300
		private int paramIntrinsic = -1;

		// Token: 0x040066BD RID: 26301
		private readonly List<Skill> listSkillsAuto = new List<Skill>();

		// Token: 0x040066BE RID: 26302
		public List<ItemAuto> listItemAuto = new List<ItemAuto>();

		// Token: 0x040066BF RID: 26303
		private static bool isAutoChat = false;

		// Token: 0x040066C0 RID: 26304
		private static string textAutoChat = string.Empty;

		// Token: 0x040066C1 RID: 26305
		private static bool isAutoChatTG = false;

		// Token: 0x040066C2 RID: 26306
		private static string textAutoChatTG = string.Empty;

		// Token: 0x040066C3 RID: 26307
		public static bool startAutoItem = false;

		// Token: 0x040066C4 RID: 26308
		private long lastAutoChat;

		// Token: 0x040066C5 RID: 26309
		private long lastAutoChatTG;

		// Token: 0x040066C6 RID: 26310
		public static bool isFilterItem = false;

		// Token: 0x040066C7 RID: 26311
		public static bool isAutoFilterItem = false;

		// Token: 0x040066C8 RID: 26312
		public static List<ItemAutoFilter> listFilterItems = new List<ItemAutoFilter>();

		// Token: 0x040066C9 RID: 26313
		public static bool isShowFilterList = false;

		// Token: 0x040066CA RID: 26314
		private bool isResizing;

		// Token: 0x040066CB RID: 26315
		private int lastMouseX;

		// Token: 0x040066CC RID: 26316
		private int lastMouseY;

		// Token: 0x040066CD RID: 26317
		private int lastPanelX;

		// Token: 0x040066CE RID: 26318
		private int lastPanelY;

		// Token: 0x040066CF RID: 26319
		private int lastPanelW;

		// Token: 0x040066D0 RID: 26320
		private int lastPanelH;

		// Token: 0x040066D1 RID: 26321
		private int lastScrollY;

		// Token: 0x040066D2 RID: 26322
		private bool isScrolling;

		// Token: 0x040066D3 RID: 26323
		private int scrollY;

		// Token: 0x040066D4 RID: 26324
		private readonly int MAX_ITEMS_VISIBLE = 10;

		// Token: 0x040066D5 RID: 26325
		private long lastFilterTime;

		// Token: 0x040066D6 RID: 26326
		public static bool notifBoss = true;

		// Token: 0x040066D7 RID: 26327
		public static bool notifKillBoss = true;

		// Token: 0x040066D8 RID: 26328
		private bool lineToBoss;

		// Token: 0x040066D9 RID: 26329
		private bool focusBoss;

		// Token: 0x040066DA RID: 26330
		private long lastFocusBoss;

		// Token: 0x040066DB RID: 26331
		public static MyVector activeBossNotif = new MyVector();

		// Token: 0x040066DC RID: 26332
		public static MyVector killedBossNotif = new MyVector();

		// Token: 0x040066DD RID: 26333
		public bool showCharsInMap = true;

		// Token: 0x040066DE RID: 26334
		public bool userOpenZones;

		// Token: 0x040066DF RID: 26335
		public bool isUpdateZones;

		// Token: 0x040066E0 RID: 26336
		private long lastUpdateZones;

		// Token: 0x040066E1 RID: 26337
		public MyVector charsInMap = new MyVector();

		// Token: 0x040066E2 RID: 26338
		public static int zoneMacDinh;

		// Token: 0x040066E3 RID: 26339
		public static bool isdoBoss;

		// Token: 0x040066E4 RID: 26340
		private static long currDoBoss;

		// Token: 0x040066E5 RID: 26341
		public static string bossCanDo;

		// Token: 0x040066E6 RID: 26342
		public Item itemPhale;

		// Token: 0x040066E7 RID: 26343
		public int maxPhale = -1;

		// Token: 0x040066E8 RID: 26344
		public int currPhale = -1;

		// Token: 0x040066E9 RID: 26345
		public bool isCollectAll;

		// Token: 0x040066EA RID: 26346
		public bool isPaintThuongDe;

		// Token: 0x040066EB RID: 26347
		public bool isOpenThuongDe;

		// Token: 0x040066EC RID: 26348
		private static int currentPage = 0;

		// Token: 0x040066ED RID: 26349
		private int ChiSoNoiTai = -1;

		// Token: 0x040066EE RID: 26350
		public string curSelectIntrinsic = "";

		// Token: 0x040066EF RID: 26351
		private string CurrentNoiTai = "";

		// Token: 0x040066F0 RID: 26352
		private string currentPlayerNoiTai = "";

		// Token: 0x040066F1 RID: 26353
		public MyVector listNotifTichXanh = new MyVector();

		// Token: 0x040066F2 RID: 26354
		private bool startChat;

		// Token: 0x040066F3 RID: 26355
		private int xNotif;

		// Token: 0x040066F4 RID: 26356
		private long lastUpdateNotif;

		// Token: 0x040066F5 RID: 26357
		public bool isPeanPet;

		// Token: 0x040066F6 RID: 26358
		private long lastPeanPet;

		// Token: 0x040066F7 RID: 26359
		public static bool autoPointForPet = false;

		// Token: 0x040066F8 RID: 26360
		public static bool userOpenPet = false;

		// Token: 0x040066F9 RID: 26361
		public static int indexAutoPoint = -1;

		// Token: 0x040066FA RID: 26362
		public static int pointIncrease = 0;

		// Token: 0x040066FB RID: 26363
		public bool showInfoMe;

		// Token: 0x040066FC RID: 26364
		private long lastUpdateInfoMe;

		// Token: 0x040066FD RID: 26365
		public bool isShowButton = true;

		// Token: 0x040066FE RID: 26366
		public bool isIntroOff;

		// Token: 0x040066FF RID: 26367
		public static bool isInventory = true;

		// Token: 0x04006700 RID: 26368
		public static bool isEffectInven = false;

		// Token: 0x04006701 RID: 26369
		public static bool isLogo = true;

		// Token: 0x04006702 RID: 26370
		public static bool isLogoGif = true;

		// Token: 0x04006703 RID: 26371
		public static bool GiamDungLuong = false;

		// Token: 0x04006704 RID: 26372
		public static bool AnPlayer = false;

		// Token: 0x04006705 RID: 26373
		public bool isHighFps;

		// Token: 0x04006706 RID: 26374
		public static bool isShowID = false;

		// Token: 0x04006707 RID: 26375
		private static int FrameGif = 58;

		// Token: 0x04006708 RID: 26376
		private static int FrameGifMenu = 16;

		// Token: 0x04006709 RID: 26377
		public static Image[] ticks = new Image[20];

		// Token: 0x0400670A RID: 26378
		private static Image logo = new Image();

		// Token: 0x0400670B RID: 26379
		private static Image[] logos = new Image[ModFunc.FrameGif];

		// Token: 0x0400670C RID: 26380
		private static Image[] logosMenu = new Image[ModFunc.FrameGifMenu];

		// Token: 0x0400670D RID: 26381
		public static Image imgLogoBig = null;

		// Token: 0x0400670E RID: 26382
		public static Image imgBg = null;

		// Token: 0x0400670F RID: 26383
		public static bool isShortOptionTemp = false;

		// Token: 0x04006710 RID: 26384
		public static Image imgMenuChat = null;

		// Token: 0x04006711 RID: 26385
		public static Image imgCloseButton = null;

		// Token: 0x04006712 RID: 26386
		public static Image imgNextPage = null;

		// Token: 0x04006713 RID: 26387
		public static Image imgNextPage2 = null;

		// Token: 0x04006714 RID: 26388
		public static Image imgPrevPage = null;

		// Token: 0x04006715 RID: 26389
		public static Image imgPrevPage2 = null;

		// Token: 0x04006716 RID: 26390
		public static int musicCount = 0;

		// Token: 0x04006717 RID: 26391
		public static bool loadedMusic = false;

		// Token: 0x04006718 RID: 26392
		public static bool isPlayingMusic = false;

		// Token: 0x04006719 RID: 26393
		public static List<AudioClip> musics = new List<AudioClip>();

		// Token: 0x0400671A RID: 26394
		private static string backgroundColor = "0.6 0.8 0.9";

		// Token: 0x0400671B RID: 26395
		public static bool isEditButton = false;

		// Token: 0x0400671C RID: 26396
		private static Dictionary<string, ModFunc.Point> buttonPositions = new Dictionary<string, ModFunc.Point>();

		// Token: 0x0400671D RID: 26397
		private static string selectedButton = null;

		// Token: 0x0400671E RID: 26398
		private static ModFunc.Point dragStart = null;

		// Token: 0x0400671F RID: 26399
		private static bool isDragging = false;

		// Token: 0x04006720 RID: 26400
		public static string ipServer = "Đổi IP";

		// Token: 0x04006721 RID: 26401
		public static bool isLockFocus = false;

		// Token: 0x04006722 RID: 26402
		public static string strAddAutoItem = "Thêm vào\nAutoItem";

		// Token: 0x04006723 RID: 26403
		public static string strRemoveAutoItem = "Xoá khỏi\nAutoItem";

		// Token: 0x04006724 RID: 26404
		public static string strAddFilterItem = "Thêm vào\nDS lọc";

		// Token: 0x04006725 RID: 26405
		public static string strRemoveFilterItem = "Xóa khỏi\nDS lọc";

		// Token: 0x04006726 RID: 26406
		public static string strTeleportTo = "Dịch\nchuyển tới";

		// Token: 0x04006727 RID: 26407
		public static string strAutoBuy = "Mua 50 lần";

		// Token: 0x04006728 RID: 26408
		public static string strAutoBuy200 = "Mua 1000 lần";

		// Token: 0x04006729 RID: 26409
		public static string strChooseIntrinsic = "Chọn chỉ số";

		// Token: 0x0400672A RID: 26410
		public static string strInCrease = "Tăng\ntới\nmức";

		// Token: 0x0400672B RID: 26411
		public static string[] strPointTypes = new string[]
		{
			"HP",
			"MP",
			"Sức Đánh",
			"Giáp",
			"Chí mạng"
		};

		// Token: 0x0400672C RID: 26412
		public static string strAccManager = "Q.L.T.K";

		// Token: 0x0400672D RID: 26413
		public static string strModFunc = "Chức Năng MOD";

		// Token: 0x0400672E RID: 26414
		public static string strUpdateZones = "Cập Nhật Khu";

		// Token: 0x0400672F RID: 26415
		public static string strCharsInMap = "Nhân Vật Trong Khu";

		// Token: 0x04006730 RID: 26416
		public static string strInfoMe = "Thông Tin Bản Thân";

		// Token: 0x04006731 RID: 26417
		public static string strAutoPhaLe = "Tự Động Pha Lê Hóa";

		// Token: 0x04006732 RID: 26418
		public static string strAutoVQMM = "Tự Động VQMM";

		// Token: 0x04006733 RID: 26419
		public static string strAutoWakeUp = "Tự Động Hồi Sinh";

		// Token: 0x04006734 RID: 26420
		public static string strAutoLogin = "Tự Động Đăng Nhập";

		// Token: 0x04006735 RID: 26421
		public static string strShowButton = "Hiện Nút Trợ Năng";

		// Token: 0x04006736 RID: 26422
		public static string strIntroOff = "Tắt Intro";

		// Token: 0x04006737 RID: 26423
		public static string strInventoryOFF = "Hiện Hành Trang Lưới";

		// Token: 0x04006738 RID: 26424
		public static string strEffectOff = "Tắt Hiệu Ứng Hành Trang";

		// Token: 0x04006739 RID: 26425
		public static string strHighFps = "FPS Cao";

		// Token: 0x0400673A RID: 26426
		public static string strClickToChat = " [Ấn để chat]";

		// Token: 0x0400673B RID: 26427
		public static string strPlayerInfo = "Thông tin player";

		// Token: 0x0400673C RID: 26428
		public static string strPet2 = "Người iuu";

		// Token: 0x0400673D RID: 26429
		public static string strUseForPet2 = "Sử dụng\ncho\nNg.iuu";

		// Token: 0x0400673E RID: 26430
		public static string strLogo = "Ẩn / hiện Logo";

		// Token: 0x0400673F RID: 26431
		public static string strGiamDungLuong = "Giảm Dung Lượng";

		// Token: 0x04006740 RID: 26432
		public static string strAnPlayer = "Ẩn Player";

		// Token: 0x04006741 RID: 26433
		public static string strLogoGif = "Logo động";

		// Token: 0x04006742 RID: 26434
		public static string strShowID = "Hiện ID Item/NPC";

		// Token: 0x04006743 RID: 26435
		public static string strEditButton = "Chỉnh sửa nút";

		// Token: 0x04006744 RID: 26436
		public static string strVietnamese = "Gõ Tiếng Việt";

		// Token: 0x04006745 RID: 26437
		public static string strShowMenuChat = "Thông Tin Lệnh Chat";

		// Token: 0x04006746 RID: 26438
		private static readonly Dictionary<string, ModFunc.Point> defaultButtonPositions = new Dictionary<string, ModFunc.Point>
		{
			{
				"Capsule",
				new ModFunc.Point(20, -26)
			},
			{
				"Fusion",
				new ModFunc.Point(-21, 21)
			},
			{
				"Zone",
				new ModFunc.Point(-66, 62)
			},
			{
				"MapLeft",
				new ModFunc.Point(-106, 62)
			},
			{
				"MapCenter",
				new ModFunc.Point(-66, 21)
			},
			{
				"MapRight",
				new ModFunc.Point(-21, -26)
			}
		};

		// Token: 0x04006747 RID: 26439
		private static int panelX = GameCanvas.w / 3 + 25;

		// Token: 0x04006748 RID: 26440
		private static int panelY = 15;

		// Token: 0x04006749 RID: 26441
		private static int panelW = 200;

		// Token: 0x0400674A RID: 26442
		private static int panelH = 170;

		// Token: 0x020004A8 RID: 1192
		public class Point
		{
			// Token: 0x06003535 RID: 13621 RVA: 0x00340C05 File Offset: 0x0033EE05
			public Point(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			// Token: 0x0400674B RID: 26443
			public int x;

			// Token: 0x0400674C RID: 26444
			public int y;
		}
	}
}
