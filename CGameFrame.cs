using System;
using System.Windows.Forms;
using System.Threading; 
using DxVBLib;
using ToyshopGame;

namespace ToyshopGame
{
	/// <summary>
	/// CGameFrame 的摘要说明:
	/// V0.0.8
	/// 最后更新：pyw 2004-4-12
	/// </summary>
	public class CGameFrame
	{
		public delegate void p_RefreshFrame();
		public Thread HitTestBulletsThread;
		public Thread HitTestEnemysThread;
		public string AppPath;
		public bool Running;

		protected bool DebugMode=false;
		protected bool DebugObjectsCanMove=true;
		protected bool DebugBulletsCanMove=true;
		protected string  DebugMsg="";
		protected CCollection cPlayers;//玩家集合，存储所有玩家的信息
		protected CCollection cObjects;//物体集合，存储所有场景物体的信息
		protected CCollection cEnemys;//敌人集合，存储所有场景中敌人的信息
		protected CCollection cbullets;//子弹集合，存储所有的子弹
		protected CCollection cExplodes;//爆炸集合，存储所有场景中正在发生的爆炸的信息
		protected CUnit[] Stars=new CUnit[300];//背景星空
		//public CRPG cRpg;
		protected CDirectX DX;
		
		protected DxVBLib.DirectDrawSurface7 mddsBackMap;
		protected Random Rnd;
		
		public CGameFrame(string strAppPath)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			Running=true;
			this.AppPath =strAppPath;
			//p_RefreshFrame pRefreshFrame=new p_RefreshFrame(RefreshFrame);
			//cRpg=new CRPG();
			DX=new CDirectX(this.AppPath); 

			DebugMode=false;DebugObjectsCanMove=true;DebugBulletsCanMove=true;
			HitTestBulletsThread=new Thread(new ThreadStart(this.DoHitTestAllBullets));
			HitTestEnemysThread=new Thread(new ThreadStart(this.DoHitTestAllEnemys));
		}
		public void InitGameData() 
		{
			Rnd=new Random((int)DateTime.Now.Millisecond);
			//初始化星星
			for (int i=0;i<Stars.GetUpperBound(0);i++)
			{
				Stars[i]=new CUnit();
				Stars[i].X=Rnd.Next(1,800);
				if (Rnd.Next(1,11)>3)
				{
					Stars[i].X=Rnd.Next(600,700);
				}
				Stars[i].Y=Rnd.Next(1,600);
				Stars[i].Speed=Rnd.Next(1,5);
			}
			//玩家集合
			cPlayers=new CCollection();
			CPlane Player1=new CPlane();
			Player1.SetLife(1000);//直接赋值会有2秒延时的限制
			Player1.Score=0;
			Player1.Speed=6;
			Player1.X=400;
			Player1.Y=580;
			Player1.Picture=CConst.C_SURFACEKEY_PLAYER1;
			cPlayers.Add(CConst.C_SURFACEKEY_PLAYER1,Player1);
			
			Player1=null;
			
			//敌人集合
			//CEnemy ObjectN;
			cEnemys=new CCollection();
			for (int i=1;i<=20;i++) 
			{
				AddEnemy(Rnd.Next(1,3));
			}
			
			//子弹集合
			cbullets=new CCollection();
			//爆炸集合
			cExplodes=new CCollection();
			return;
		}
		public void AddEnemy(int Type)
		{
			CEnemy ObjectN;
			ObjectN=new CEnemy();
			ObjectN.Life=Type*50;
			ObjectN.Damage=Rnd.Next(1,10);
			ObjectN.Score=Rnd.Next(1,10);
			ObjectN.AmmoCount=2;
			ObjectN.Speed=5;
			ObjectN.X=Rnd.Next(100,500);
			ObjectN.Y=Rnd.Next(-1250,-500);
			ObjectN.Type=Type;
			ObjectN.Picture=CConst.C_SURFACEKEY_ENEMY+Type.ToString();
			cEnemys.Add(ObjectN);
			ObjectN=null;
		}
		/// <summary>
		/// 子弹相关函数：
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="Type"></param>
		/// <param name="Speed"></param>
		/// <param name="Owner"></param>
		public void AddBullet(int x,int y,int Speed,int Type,CObj Owner)
		{
			CFire objX=new CFire();
			objX.Type=Type;
			objX.Owner=Owner;
			objX.Damage=60;  
			objX.X=x;objX.Y=y;
			if (Type==CConst.C_BULLETTYPE_ENEMY)
			{
				//敌人发射的子弹
				//DebugMsg=Owner.GetType().ToString();
				objX.Picture=CConst.C_SURFACEKEY_BULLET2;
				objX.Speed=Speed;
				objX.XP=0;objX.YP=Speed;
			}
			else
			{
				//DebugMsg=Owner.GetType().ToString();
				objX.Picture=CConst.C_SURFACEKEY_BULLET1;
				objX.Speed=-Speed;
				objX.XP=0;objX.YP=-Speed;
			}
			cbullets.Add(objX);
			return;
		}
		public void DoMoveAllBullet()
		{
			CFire objX;
			for(int i=1;i<=cbullets.Count();i++)
			{
				objX=(CFire)cbullets[i];
				if (true)//objX.Type==CConst.C_BULLETTPYE_PLAYER1)
				{
					//objX.Y-=objX.Speed;
					objX.UpdateMove();
					if (objX.Y<=0)
					{
						cbullets.Remove(i);
					}
				}
			}
		}
		public void DrawAllBullet()
		{
			CFire objX;
			for(int i=1;i<=cbullets.Count();i++)
			{
				objX=(CFire)cbullets[i];	
				try
				{
					DX.DrawPicture((CObj)objX);

					objX=null;
				}
				catch
				{
					//Console.WriteLine(objX.Picture);
				}
			}
		}
		/// <summary>
		/// 玩家相关方法：
		/// </summary>
		public void DrawAllPlayers()
		{
			CPlane Player=(CPlane)cPlayers[CConst.C_SURFACEKEY_PLAYER1];
			for (int i=1;i<=cPlayers.Count();i++)
			{
				
				//DX.DrawPicture(Player.X,Player.Y,DX.Surfaces[Player.Picture]);
				DX.DrawPicture((CObj)Player);

				Player=null;
			}
		}
		/// <summary>
		/// 敌人相关方法
		/// </summary>
		public void DrawAllEnemys()
		{
			for (int i=1;i<=cEnemys.Count();i++)
			{
				CObj ObjectN=(CObj)cEnemys[i];//CConst.C_SURFACEKEY_ENEMY+i.ToString()];
				if (ObjectN.Y>-15)
				{
					//DX.DrawPicture(ObjectN.X,ObjectN.Y,DX.Surfaces[ObjectN.Picture]);
					DX.DrawPicture(ObjectN);
				}
				ObjectN=null;
			}
		}
		public void DoMoveAllEnemys()
		{
			Random rnd;
			rnd=new Random((int)DateTime.Now.Millisecond);
			for (int i=1;i<=cEnemys.Count();i++)
			{
				CEnemy ObjectN=(CEnemy)cEnemys[i];//CConst.C_SURFACEKEY_OBJECT+i.ToString()];
				ObjectN.UpdateMove();
				if (ObjectN.Type==1 && ObjectN.Y>0 && ObjectN.AmmoCount>0 && Rnd.Next(1,11)>7)
				{
					//敌人飞机开枪
					AddBullet(ObjectN.X,ObjectN.Y,Rnd.Next(5,7),CConst.C_BULLETTYPE_ENEMY,ObjectN);
					ObjectN.AmmoCount--; 
				}
				ObjectN.UpdateMove();
				if (ObjectN.Y>CConst.C_SCREENHEIGHT || ObjectN.X>CConst.C_SCREENWIDTH || ObjectN.X<0)
				{
					cEnemys.Remove(i);
					AddEnemy(Rnd.Next(1,3));
				}
				ObjectN=null;
			}
		}
		/// <summary>
		/// 爆炸相关
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void AddHBoom(int Type)//增加一个氢弹爆炸,要指定爆炸的类型
		{
			CFire objX;
			for(int i=1;i<=cbullets.Count();i++)
			{
				objX=(CFire)cbullets[i];
				if (objX.Type==CConst.C_BULLETTYPE_ENEMY)
				{
					cbullets.Remove(i);
				}
			}
			for (int i=1;i<=20;i++)
			{
				AddExplode(Rnd.Next(150,550),Rnd.Next(100,200),Rnd.Next(1,3));
			}
		}
		public void AddExplode(int x,int y,int Type)//增加一个爆炸,要指定爆炸的类型
		{
			CExplode objX=new CExplode();
			objX.Picture=CConst.C_SURFACEKEY_EXPLODE+Type.ToString();
			CSurface a=DX.Surfaces[objX.Picture];
			objX.Phase.TotalPhase=a.Phase.TotalPhase; 
			objX.Phase.PhaseChangeSpeed=20;
			objX.Phase.CurrentPhase=1;
			objX.X=x;objX.Y=y;
			cExplodes.Add(objX);
			return;
		}
		public void UpdateAllExplodes()//更新爆炸
		{
			for (int i=1;i<=cExplodes.Count();i++)
			{
				CObj ObjectN=(CObj)cExplodes[i];//CConst.C_SURFACEKEY_OBJECT+i.ToString()];
				ObjectN.Phase.UpdatePhase();  
				if (!ObjectN.Phase.IsActive)
				{
					cExplodes.Remove(i); 
				}
				ObjectN=null;
			}
		}
		public void DrawAllExplodes()//绘制爆炸
		{
			for (int i=1;i<=cExplodes.Count();i++)
			{
				CObj ObjectN=(CObj)cExplodes[i];//CConst.C_SURFACEKEY_ENEMY+i.ToString()];
				if (ObjectN.Phase.IsActive)
				{
					DX.DrawPicture(ObjectN);
					//DebugMsg=ObjectN.Phase.CurrentPhase.ToString();
				}
				ObjectN=null;
			}
		}
		/// <summary>
		/// 检测
		/// </summary>
		public void DoHitTestAllEnemys()//检测敌人和玩家的碰撞
		{
			Random rnd;
			rnd=new Random((int)DateTime.Now.Millisecond);
			do
			{
				for (int j=1;j<=cEnemys.Count();j++)
				{
					CEnemy ObjectN=(CEnemy)cEnemys[j];//CConst.C_SURFACEKEY_OBJECT+j.ToString()];
					if (ObjectN.Y>0)
					{
						//判断和玩家的接触情况
						for (int i=1;i<=cPlayers.Count();i++)
						{
							CPlane Player=(CPlane)cPlayers[CConst.C_SURFACEKEY_PLAYER+i.ToString()];
							//DX.DrawText(1,560,"Player X="+Player.X.ToString()+",Y="+Player.Y.ToString());
							if (HitTest(ObjectN,Player))
							{
								//接触
								Player.Life-=ObjectN.Damage;
								ObjectN.Life-=ObjectN.Damage;
								if (ObjectN.Life<=0)
								{
									Player.Score+=ObjectN.Score;
									cEnemys.Remove(j);
									AddEnemy(Rnd.Next(1,3));
								}
								AddExplode(ObjectN.X,ObjectN.Y,rnd.Next(1,3));
							}
						}
					}
				}
				Thread.Sleep(50);
			}while(true);
		}
		public void DoHitTestAllBullets()//检测子弹和物体（敌人和玩家）的碰撞
		{
			CFire objX;
			do
			{
				for (int i=1;i<=cbullets.Count();i++)
				{
					objX=(CFire)cbullets[i];//其中一发子弹
				
					if (objX.Type==CConst.C_BULLETTYPE_ENEMY)
					{
						//判断和玩家的碰撞情况
						for (int j=1;j<=cPlayers.Count();j++)
						{
							CPlane Player=(CPlane)cPlayers[CConst.C_SURFACEKEY_PLAYER+j.ToString()];
							if (HitTest(objX,Player))
							{
								Player.Life-=objX.Damage;
								cbullets.Remove(i);
								AddExplode(objX.X,objX.Y,Rnd.Next(1,3));
							}
						}
					
					}
					else
					{
						//判断和敌人的碰撞情况
						for (int j=1;j<=cEnemys.Count();j++)
						{
							CEnemy ObjectN=(CEnemy)cEnemys[j];
							if (HitTest(ObjectN,objX))
							{
								//子弹击中
								ObjectN.Life-=objX.Damage; 
								AddExplode(ObjectN.X,ObjectN.Y,Rnd.Next(1,3));
								if (ObjectN.Life<=0)
								{
									CPlane c=(CPlane)objX.Owner;
									c.Score+=ObjectN.Score;
									cEnemys.Remove(j);
									AddEnemy(Rnd.Next(1,3));
								}
								cbullets.Remove(i);
							}
						}
					}
				}
				Thread.Sleep(30);
			}while(true);
		}
		public bool HitTest(CObj a,CObj b)//物体接触检测
		{
			CSurface face1,face2;
			RECT r1,r2;
			face1=DX.Surfaces[a.Picture];
			r1=face1.Rect;
			face2=DX.Surfaces[b.Picture];
			r2=face2.Rect;
			if (DX.RectHitTest(a.X,a.Y,b.X,b.Y,r1,r2))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 帧更新
		/// </summary>
		public void UpdateFrame()
		{
			UpdateStarts();
			if (cPlayers.Count()>0)
			{
				CPlane c=(CPlane)cPlayers[CConst.C_SURFACEKEY_PLAYER1];
				DX.DrawText(1,1,"经验值:"+c.Score.ToString()+"  生命力:"+c.Life.ToString()); 
			}
			DX.DrawText(40,40,DebugMsg); 
			if (!DebugMode && DebugBulletsCanMove)
			{
				DoMoveAllBullet();
			}
			DrawAllBullet();
			if (!DebugMode && DebugObjectsCanMove)
			{
				DoMoveAllEnemys();
			}
			DrawAllEnemys();
			DrawAllPlayers();
			DrawAllExplodes();UpdateAllExplodes();
			
			//DoHitTestAllBullets();
			//DoHitTestAllEnemys();
		}
		public void UpdateFrame(bool bUpdatePrimaryScreen)
		{
			UpdateFrame();
			if (bUpdatePrimaryScreen)
			{
				DX.BackToPrimary();
			}
		}
		public void RefreshFrame()
		{
			DX.Cls();
			UpdateStarts();
			DrawAllBullet();DrawAllPlayers();DrawAllExplodes();DrawAllEnemys();
		}
		public void RefreshFrame(bool bRefreshScreen)
		{
			DX.Cls();
			UpdateStarts();
			DrawAllBullet();DrawAllPlayers();DrawAllExplodes();DrawAllEnemys();
			if (bRefreshScreen==true)
			{
					DX.BackToPrimary();
			}
		}
		public void UpdateStarts()
		{
			for (int i=0;i<Stars.GetUpperBound(0);i++)
			{
				DX.DrawPoint(Stars[i].X,Stars[i].Y,Rnd.Next(0,0xFFFFFF));
				Stars[i].Y+=Stars[i].Speed;
				if (Stars[i].Y>CConst.C_SCREENHEIGHT)
				{
					Stars[i].X=Rnd.Next(1,800);
					if (Rnd.Next(1,11)>6)
					{
						Stars[i].X=Rnd.Next(500,700);
					}
					Stars[i].Y=Rnd.Next(-100,0);
					Stars[i].Speed=Rnd.Next(1,9);
				}
			}
		}

		#region 绘制背景图（已经取消）
//		public void DrawBackMap() 
//		{
//			//C_STRING_MAP
//			DX.DrawPicture(0,0,mddsBackMap);
//			return;
//		}
		#endregion

		///RPG相关功能
		///
		public void RPGSay(string TalkText,int Mode,string Head,p_RefreshFrame bkgfun)
		{
			bool bBreakLoop=false;
			int HeadX=CConst.C_RpgTalkBoxHeadX1;
			int HeadY=CConst.C_RpgTalkBoxHeadY1;
			RECT rRPGSay=new RECT();
			rRPGSay.Left=0;
			rRPGSay.Top=CConst.C_RpgTalkBoxTop1;
			rRPGSay.Right=CConst.C_RpgTalkBoxWidth;
			rRPGSay.Bottom=rRPGSay.Top+CConst.C_RpgTalkBoxHeight;
			DX.KeyOff();
			DX.KeyOn();
			DateTime delaytime=DateTime.Now;
			do
			{
				if (DateTime.Now>delaytime.AddMilliseconds(500))
				{
					if(DX.InKey(CONST_DIKEYFLAGS.DIK_RETURN) || DX.InKey(CONST_DIKEYFLAGS.DIK_SPACE) || DX.InKey(CONST_DIKEYFLAGS.DIK_ESCAPE))
					{
						bBreakLoop=true;
					}
				}
				DX.Cls();
				bkgfun();
				//DX.TransparenceRect(1,CConst.C_RpgTalkBoxTop1,rRPGSay);
				DX.DrawBox(rRPGSay);
				DX.DrawPicture(HeadX,HeadY,CConst.C_SURFACEKEY_PLAYERHEAD+Head);
				DX.DrawText(250, CConst.C_RpgTalkBoxTop1 + 20,TalkText);
				DX.BackToPrimary();
			}
			while(!bBreakLoop);
			DX.KeyOn();
		}
		/// <summary>
		/// 延时方法，+2重载
		/// </summary>
		/// <param name="Milliseconds"></param>
		public void Wait(int Milliseconds)
		{
			DateTime delaytime=DateTime.Now;
			do 
			{
				RefreshFrame(true);
			}
			while (DateTime.Now<delaytime.AddMilliseconds(Milliseconds));
		}
		public void Wait(int Milliseconds,p_RefreshFrame bkgfun)
		{
			DateTime delaytime=DateTime.Now;
			do 
			{
				bkgfun();
			}
			while (DateTime.Now<delaytime.AddMilliseconds(Milliseconds));
		}
	}
}
