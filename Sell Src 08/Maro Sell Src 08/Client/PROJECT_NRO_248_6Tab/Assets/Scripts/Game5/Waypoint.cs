using System;

namespace Game5
{
	// Token: 0x0200019D RID: 413
	public class Waypoint : IActionListener
	{
		// Token: 0x06001280 RID: 4736 RVA: 0x001236B8 File Offset: 0x001218B8
		public Waypoint(short minX, short minY, short maxX, short maxY, bool isEnter, bool isOffline, string name)
		{
			this.minX = minX;
			this.minY = minY;
			this.maxX = maxX;
			this.maxY = maxY;
			name = Res.changeString(name);
			this.isEnter = isEnter;
			this.isOffline = isOffline;
			if (((TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && this.minX >= 0 && this.minX <= 24) || (((TileMap.mapID == 0 && Char.myCharz().cgender != 0) || (TileMap.mapID == 7 && Char.myCharz().cgender != 1) || (TileMap.mapID == 14 && Char.myCharz().cgender != 2)) && isOffline))
			{
				return;
			}
			if (TileMap.isInAirMap() || TileMap.mapID == 47)
			{
				if (minY <= 150 || !TileMap.isInAirMap())
				{
					this.popup = new PopUp(name, (int)(minX + (maxX - minX) / 2), (int)(maxY - ((minX <= 100) ? 48 : 24)));
					this.popup.command = new Command(null, this, 1, this);
					this.popup.isWayPoint = true;
					this.popup.isPaint = false;
					PopUp.addPopUp(this.popup);
					TileMap.vGo.addElement(this);
				}
				return;
			}
			if (!isEnter && !isOffline)
			{
				this.popup = new PopUp(name, (int)minX, (int)(minY - 24));
				this.popup.command = new Command(null, this, 1, this);
				this.popup.isWayPoint = true;
				this.popup.isPaint = false;
				PopUp.addPopUp(this.popup);
			}
			else
			{
				if (TileMap.isTrainingMap())
				{
					this.popup = new PopUp(name, (int)minX, (int)(minY - 16));
				}
				else
				{
					int x = (int)(minX + (maxX - minX) / 2);
					this.popup = new PopUp(name, x, (int)(minY - ((minY == 0) ? -32 : 16)));
				}
				this.popup.command = new Command(null, this, 2, this);
				this.popup.isWayPoint = true;
				this.popup.isPaint = false;
				PopUp.addPopUp(this.popup);
			}
			TileMap.vGo.addElement(this);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x001238D4 File Offset: 0x00121AD4
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				int xEnd2 = (int)((this.minX + this.maxX) / 2);
				int yEnd2 = (int)this.maxY;
				if (this.maxY > this.minY + 24)
				{
					yEnd2 = (int)((this.minY + this.maxY) / 2);
				}
				GameScr.gI().auto = 0;
				Char.myCharz().currentMovePoint = new MovePoint(xEnd2, yEnd2);
				Char.myCharz().cdir = ((Char.myCharz().cx - Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
				Service.gI().charMove();
				return;
			}
			if (idAction != 2)
			{
				return;
			}
			GameScr.gI().auto = 0;
			if (Char.myCharz().isInEnterOfflinePoint() != null)
			{
				Service.gI().charMove();
				InfoDlg.showWait();
				Service.gI().getMapOffline();
				Char.ischangingMap = true;
				return;
			}
			if (Char.myCharz().isInEnterOnlinePoint() != null)
			{
				Service.gI().charMove();
				Service.gI().requestChangeMap();
				Char.isLockKey = true;
				Char.ischangingMap = true;
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				InfoDlg.showWait();
				return;
			}
			int xEnd3 = (int)((this.minX + this.maxX) / 2);
			int yEnd3 = (int)this.maxY;
			Char.myCharz().currentMovePoint = new MovePoint(xEnd3, yEnd3);
			Char.myCharz().cdir = ((Char.myCharz().cx - Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : -1);
			Char.myCharz().endMovePointCommand = new Command(null, this, 2, null);
		}

		// Token: 0x04002428 RID: 9256
		public short minX;

		// Token: 0x04002429 RID: 9257
		public short minY;

		// Token: 0x0400242A RID: 9258
		public short maxX;

		// Token: 0x0400242B RID: 9259
		public short maxY;

		// Token: 0x0400242C RID: 9260
		public bool isEnter;

		// Token: 0x0400242D RID: 9261
		public bool isOffline;

		// Token: 0x0400242E RID: 9262
		public PopUp popup;
	}
}
