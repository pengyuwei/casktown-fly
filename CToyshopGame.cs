using System;
using System.Windows.Forms;
using DxVBLib;
using ToyshopGame;

namespace ToyshopGame
{
	/// <summary>
	/// CToyshopGame 的摘要说明:
	/// V0.0.8
	/// 最后更新：pyw 2004-4-9
	/// </summary>
	public class CToyshopGame:CGameFrame
	{

		public CToyshopGame(string strAppPath):base(strAppPath)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static void Main()//程序入口
		{
			CToyshopGame CGame=new CToyshopGame(Application.StartupPath + "\\");
			frmMain cFrmMain=new frmMain(CGame);
			cFrmMain.Show();  
			
			CGame.InitGameData();

			#region 临时测试代码
			//
//			RECT a=new RECT(),b=new RECT();
//			a.Bottom=30;a.Right=30;//物体
//			b.Bottom=10;b.Right=15;//子弹
//			Console.WriteLine(CGame.DX.RectHitTest(477,189,495,202,a,b).ToString());
			//
			#endregion

			if (!CGame.DX.InitDX(cFrmMain.Handle)) return;
			
			//CGame.NewGameMovie();
			
			do
			{
				
			}while (CGame.GameLoop()!=0);

			CGame.DX.ReleaseDX(); 
			CGame.DX=null;
			cFrmMain.Dispose(); 
			cFrmMain=null;
			CGame=null; 
			Application.Exit();
		}
		private void GameLogo()
		{
		
		}
		private void NewGameMovie()
		{
			CPlane Player=(CPlane)cPlayers[CConst.C_SURFACEKEY_PLAYER1];
			p_RefreshFrame pRefreshFrame=new p_RefreshFrame(RefreshFrame);
			DX.KeyOff();
			Wait(1800);
			for (int i=0;i<=15;i++)
			{
				Player.MoveUp();
				
				Wait(40);
			}
			for (int i=0;i<=5;i++)
			{
				Player.MoveDown();
				
				Wait(150);
			}
			Wait(3000);
			DX.KeyOn();

			RPGSay("AKASA：EVAN，你看前方！那么多的敌机...",1,"1",pRefreshFrame);
			RPGSay("EVAN：看我的吧！",1,"2",pRefreshFrame);
		}

		private int GameLoop()
		{
			int intRet=0;
			DateTime LastKeyPressTime;//上次按射击键的时间
			int intBulletXAdd;//子弹发射位置的偏移
			int intMaxY,intMaxX,intMinY,intMinX;

			DX.Cls();

			#region 背景图(已经取消)
			//CSurface Map=DX.Surfaces[CConst.C_SURFACEKEY_MAP];
			//mddsBackMap=Map.Surface;
			#endregion
			
			CPlane Player=(CPlane)cPlayers[CConst.C_SURFACEKEY_PLAYER1];
			LastKeyPressTime=DateTime.Now;
			
			CSurface a=DX.Surfaces[Player.Picture];
			CSurface b=DX.Surfaces[CConst.C_SURFACEKEY_BULLET1];
			intBulletXAdd=a.Rect.Right/2-b.Rect.Right/2;//子弹发射位置的偏移坐标
			
			//玩家的移动范围
			intMaxX=CConst.C_SCREENWIDTH-a.Rect.Right-10;
			intMaxY=CConst.C_SCREENHEIGHT-a.Rect.Bottom;
			intMinX=10;
			intMinY=10;

			DX.Cls();
			
			HitTestBulletsThread.Start(); 
			HitTestEnemysThread.Start();
			//程序开始
			do 
			{
				DX.Cls();
				if (DX.InKey(CONST_DIKEYFLAGS.DIK_P))
				{
					while(!DX.InKey(CONST_DIKEYFLAGS.DIK_RETURN))
					{
						DX.BackToPrimary();
					}
				}
				if (DX.InKey(CONST_DIKEYFLAGS.DIK_RCONTROL))
				{
					DebugMode=true;
				}
				if (DX.InKey(CONST_DIKEYFLAGS.DIK_1))
				{
					DebugObjectsCanMove=!DebugObjectsCanMove;
				}
				if (DX.InKey(CONST_DIKEYFLAGS.DIK_2))
				{
					DebugBulletsCanMove=!DebugBulletsCanMove;
				}
				if (DX.InKey(CONST_DIKEYFLAGS.DIK_ESCAPE))
				{
					break;
				}
				if (DX.InKey(CONST_DIKEYFLAGS.DIK_RIGHT) || DX.InKey(CONST_DIKEYFLAGS.DIK_D))
				{
					if (Player.X <intMaxX) {Player.X+=Player.Speed;}
				}
				if(DX.InKey(CONST_DIKEYFLAGS.DIK_LEFT) || DX.InKey(CONST_DIKEYFLAGS.DIK_A))
				{
					if (Player.X>intMinX) {Player.X-=Player.Speed;}
				}
				if(DX.InKey(CONST_DIKEYFLAGS.DIK_UP) || DX.InKey(CONST_DIKEYFLAGS.DIK_W))
				{
					if (Player.Y>intMinY){Player.Y-=Player.Speed;}
				}
				if(DX.InKey(CONST_DIKEYFLAGS.DIK_DOWN) || DX.InKey(CONST_DIKEYFLAGS.DIK_S))
				{
					if (Player.Y<intMaxY) {Player.Y+=Player.Speed;}
				}
				if(DX.InKey(CONST_DIKEYFLAGS.DIK_H) || DX.InKey(CONST_DIKEYFLAGS.DIK_LCONTROL))
				{
					if (DateTime.Now>LastKeyPressTime.AddMilliseconds(100))
					{
						LastKeyPressTime=DateTime.Now;
						AddBullet(Player.X+intBulletXAdd,Player.Y-20,15,CConst.C_BULLETTYPE_PLAYER1,Player);
						if (Player.Score>100)
						{
							AddBullet(Player.X+intBulletXAdd*2,Player.Y,15,CConst.C_BULLETTYPE_PLAYER1,Player);
							AddBullet(Player.X,Player.Y,15,CConst.C_BULLETTYPE_PLAYER1,Player);
						}
						if (Player.Score>300)
						{
							AddBullet(Player.X+intBulletXAdd*3,Player.Y,15,CConst.C_BULLETTYPE_PLAYER1,Player);
							AddBullet(Player.X-intBulletXAdd,Player.Y,15,CConst.C_BULLETTYPE_PLAYER1,Player);
						}
					}
				}
//				if (DX.InKey(CONST_DIKEYFLAGS.DIK_SPACE))
//				{
//					RPGSay("AKASA：好棒好棒，我也要开枪！",1,"1",pRefreshFrame);
//				}
				if (DX.InKey(CONST_DIKEYFLAGS.DIK_J))
				{
					AddHBoom(1);
				}
				if (this.Running==false) break;
				if (Player.Life<=0) 
				{
					intRet=1;	
					break;
				}
				UpdateFrame();
				DX.BackToPrimary();
			}while(this.Running);
			
			if (intRet!=0)
			{
				HitTestBulletsThread.Abort(); 
				HitTestEnemysThread.Abort();
				ShowGameOver();
				//HitTestBulletsThread.Start(); 
				//HitTestEnemysThread.Start();
			}
			return intRet;
		}
		private void ShowGameOver()
		{
			DX.KeyOff();
			cPlayers.Clear();
			p_RefreshFrame pRefreshFrame=new p_RefreshFrame(UpdateFrame);

			Wait(200,pRefreshFrame);
			DX.KeyOn();
			RPGSay("EVAN：不行了，俺扛不住了.",1,"2",pRefreshFrame);
			RPGSay("AKASA：真白痴...",1,"1",pRefreshFrame);
			RPGSay("EVAN：...",1,"2",pRefreshFrame);
			RPGSay("AKASA：按回车键重新来过吧.",1,"1",pRefreshFrame);
			do 
			{
				DX.Cls();
				UpdateFrame();
				DX.DrawText(CConst.C_SCREENWIDTH/2-100,CConst.C_SCREENHEIGHT/2," G a m e  O v e r !");
				DX.BackToPrimary();
			}
			while (!DX.InKey(CONST_DIKEYFLAGS.DIK_RETURN));
			InitGameData();
		}
	}
}
