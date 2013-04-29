using System;
using System.Drawing;
using System.IO;
using DxVBLib;

namespace ToyshopGame
{
	/// <summary>
	/// CDirectX 的摘要说明。Instance
	/// </summary>
	public class CDirectX:CDirectXBase
	{
		public CSurfaces Surfaces;//存放所有离屏表面的集合
		protected string AppPath;

		public CDirectX(string strAppPath)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this.AppPath =strAppPath;
		}
		public bool InitDX(IntPtr Handle)//初始化DirectX
		{
			if (!CheckFiles())
			{
				return false;
			}
			base.InitDirectX(Handle,CConst.C_SCREENWIDTH,CConst.C_SCREENHEIGHT) ;
			this.DDSBackSurface.SetForeColor(0xffffff);
			LoadAllSurfaces();
			return true;
		}
		private bool CheckFiles()
		{
			bool bRet=true;
			string[] strFiles=new string[10];
			strFiles[0]="Resource\\Plane.bmp";
			strFiles[1]="Resource\\playerhead1.bmp";
			strFiles[2]="Resource\\playerhead2.bmp";
			strFiles[3]="Resource\\enemy1.bmp";
			strFiles[4]="Resource\\enemy2.bmp";
			strFiles[5]="Resource\\Obj.bmp";
			strFiles[6]="Resource\\bullet1.bmp";
			strFiles[7]="Resource\\bullet2.bmp";
			strFiles[8]="Resource\\explode1.bmp";
			strFiles[9]="Resource\\explode2.bmp";
			//strFiles[10]="Resource\\map.bmp";
			for (int i=0;i<=strFiles.GetUpperBound(0);i++)
			{
				if (!File.Exists(this.AppPath + strFiles[i])) 
				{
					bRet=false;
					System.Windows.Forms.MessageBox.Show("File not found:\n"+strFiles[i]);
					break;
				}
			}
			return bRet;
		}
		private void LoadAllSurfaces()//读入表面并存入集合
		{
			//建立表面集合
			DxVBLib.DDSURFACEDESC2 DDesc;
			DxVBLib.RECT rect;
			CSurface cSurface; 

			Surfaces=new CSurfaces();//所有离屏表面的集合
			
			#region 临时代码
//			DxVBLib.DirectDrawSurface7 DDSurfaceScr;

			//主表面：
			//屏幕及后台缓冲
//			DxVBLib.DirectDrawSurface7 DDSurfaceTemp;
//			cSurface=new CSurface();
//			mDDSurfaceScreen =this.CreateSurface(out mDDSurfaceBackSurface);
//			cSurface.DDSurface=DDSurfaceScr;
//			CSurfaces.Add(cSurface);//0
//			cSurface=new CSurface();
//			CSurfaces.Add(DDSurfaceTemp);//1

			//离屏表面:
			//原始屏幕拷贝
//			cSurface=new CSurface();
//			cSurface.DDSurface =this.CreateSurface(DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_OFFSCREENPLAIN);
//			rect=new DxVBLib.RECT();
//			rect.Top =0;rect.Left=0;rect.Right=1024;rect.Bottom=768;   
//			cSurface.DDSurface.BltFast(0,0,mDDSurfaceScreen,ref rect,DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_WAIT);//将图象在屏幕上显示
//			CSurfaces.Add(cSurface);//1
			#endregion

			//飞机
			cSurface=new CSurface(); 
			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\Plane.bmp",out rect,out DDesc);
			cSurface.Rect =rect; 
			Surfaces.Add(CConst.C_SURFACEKEY_PLAYER1,cSurface);//1
			cSurface=null;
			//玩家1头像
			cSurface=new CSurface(); 
			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\playerhead1.bmp",out rect,out DDesc);
			cSurface.Rect =rect; 
			Surfaces.Add(CConst.C_SURFACEKEY_PLAYERHEAD1,cSurface);//1
			cSurface=null;
			//玩家2头像
			cSurface=new CSurface(); 
			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\playerhead2.bmp",out rect,out DDesc);
			cSurface.Rect =rect; 
			Surfaces.Add(CConst.C_SURFACEKEY_PLAYERHEAD2,cSurface);//1
			cSurface=null;
			//敌人1
			cSurface=new CSurface(); 
			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\enemy1.bmp",out rect,out DDesc);
			cSurface.Rect =rect; 
			Surfaces.Add(CConst.C_SURFACEKEY_ENEMY1,cSurface);//2
			cSurface=null;
			//敌人2
			cSurface=new CSurface(); 
			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\enemy2.bmp",out rect,out DDesc);
			cSurface.Rect =rect; 
			Surfaces.Add(CConst.C_SURFACEKEY_ENEMY2,cSurface);//
			cSurface=null;
			//物体1
			cSurface=new CSurface(); 
			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\Obj.bmp",out rect,out DDesc);
			cSurface.Rect =rect; 
			Surfaces.Add(CConst.C_SURFACEKEY_OBJECT1,cSurface);//
			cSurface=null;
			//子弹bullet/weapon武器
			cSurface=new CSurface(); 
			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\bullet1.bmp",out rect,out DDesc);
			cSurface.Rect =rect; 
			Surfaces.Add(CConst.C_SURFACEKEY_BULLET1,cSurface);//
			cSurface=null;
			//子弹bullet/weapon武器2
			cSurface=new CSurface(); 
			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\bullet2.bmp",out rect,out DDesc);
			cSurface.Rect =rect; 
			Surfaces.Add(CConst.C_SURFACEKEY_BULLET2,cSurface);//
			cSurface=null;
			//爆炸explode1
			cSurface=new CSurface(); 
			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\explode1.bmp",out rect,out DDesc);
			cSurface.Rect =rect; 
			cSurface.Phase.TotalPhase=15;//总帧数
			cSurface.PhaseWidth=64;//一帧的宽度=图片宽/总帧数
			Surfaces.Add(CConst.C_SURFACEKEY_EXPLODE1,cSurface);//
			cSurface=null;
			//爆炸explode2
			cSurface=new CSurface(); 
			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\explode2.bmp",out rect,out DDesc);
			cSurface.Rect =rect; 
			cSurface.Phase.TotalPhase=8;//总帧数
			cSurface.PhaseWidth=133;//一帧的宽度=图片宽/总帧数
			Surfaces.Add(CConst.C_SURFACEKEY_EXPLODE2,cSurface);//
			cSurface=null;
			//boss
			//cSurface=new CSurface(); 
			//cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\boss.bmp",out rect);
			//cSurface.Rect =rect; 
			//Surfaces.Add(CConst.C_SURFACEKEY_ENEMYBOSS,cSurface);//
			//cSurface=null;
//			//背景图片
//			cSurface=new CSurface(); 
//			cSurface.Surface =this.CreateSurface(this.AppPath + "Resource\\map.bmp",out rect,out DDesc);
//			cSurface.Rect =rect; 
//			Surfaces.Add(CConst.C_SURFACEKEY_MAP,cSurface);//
//			cSurface=null;

			return ;
		}

		public void test(ref int X,ref int Y)
		{
			//用于测试的方法
			int x=0,y=0;
			DxVBLib.DirectDrawSurface7 DDSurfaceBack;
			DxVBLib.DirectDrawSurface7 DDSurface;
			CSurface cSurface;
			DxVBLib.RECT rect;

			x=X;y=Y;
			if (x<0) x=0;
			if (y<0) y=0;
			if (x>CConst.C_SCREENWIDTH) x=CConst.C_SCREENWIDTH;
			if (y>CConst.C_SCREENHEIGHT) y=CConst.C_SCREENHEIGHT;
			//cSurface=(CSurface)this.CSurfaces.Item(PRIMARYSURFACE);
			DDSurfaceBack=this.DDSBackSurface;//cSurface.DDSurface;
			
			cSurface=(CSurface)this.Surfaces[CConst.C_SURFACEKEY_PLAYER1];
			DDSurface=cSurface.Surface;
			rect=cSurface.Rect;
			if (x+rect.Right>CConst.C_SCREENWIDTH) x=CConst.C_SCREENWIDTH-rect.Right;
			if (y+rect.Bottom>CConst.C_SCREENHEIGHT) y=CConst.C_SCREENHEIGHT-rect.Bottom;
			//DDSurfaceScr.restore();
			//base.Cls();
			DDSurfaceBack.BltFast(x,y,DDSurface,ref rect,(DxVBLib.CONST_DDBLTFASTFLAGS)((int)DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_SRCCOLORKEY | (int)DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_WAIT));//将图象在屏幕上显示
			this.BackToPrimary();
			X=x;Y=y;
			return;
		}

		/// <summary>
		/// 绘制图形+3次重载
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="DDSurface"></param>
		public void DrawPicture(int x,int y,DxVBLib.DirectDrawSurface7 DDSurface)
		{
			RECT r=new RECT();
			this.DDSBackSurface.BltFast(x,y,DDSurface,ref r,(DxVBLib.CONST_DDBLTFASTFLAGS)((int)DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_SRCCOLORKEY | (int)DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_WAIT));//将图象在屏幕上显示
		}
		public void DrawPicture(int x,int y,string SurfaceKey)
		{
			RECT r=new RECT();
			CSurface cs=Surfaces[SurfaceKey];
			DxVBLib.DirectDrawSurface7 DDSurface=cs.Surface;
			this.DDSBackSurface.BltFast(x,y,DDSurface,ref r,(DxVBLib.CONST_DDBLTFASTFLAGS)((int)DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_SRCCOLORKEY | (int)DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_WAIT));//将图象在屏幕上显示
		}
		public void DrawPicture(CObj ObjectN)
		{
			RECT scrRect=new RECT();
			CSurface DDSurface=Surfaces[ObjectN.Picture];
			if (DDSurface.Phase.TotalPhase>1)
			{
				//多帧的情况
				scrRect=DDSurface.GetCurrentPhaseRect(ObjectN.Phase.CurrentPhase);
			}
			else
			{
				scrRect=DDSurface.Rect;
			}
			DrawPicture(ObjectN.X,ObjectN.Y,DDSurface.Surface,scrRect);
		}
		public void DrawPicture(int x,int y,DxVBLib.DirectDrawSurface7 DDSurface,RECT scrRect)
		{
			RECT drawRect;
//			scrRect=DDSurface.Rect;
//			if (DDSurface.Phase.TotalPhase>0)
//			{
//				//多帧的情况
//				scrRect=DDSurface.GetCurrentPhaseRect();
//			}
			if (x>=CConst.C_SCREENWIDTH || y>=CConst.C_SCREENHEIGHT) return;//已经在屏幕外了

			if (x+(scrRect.Right-scrRect.Left)<CConst.C_SCREENWIDTH && y+scrRect.Bottom-scrRect.Top<CConst.C_SCREENHEIGHT && y>0 && x>0)
			{
				//物体完全在屏幕内
				this.DDSBackSurface.BltFast(x,y,DDSurface,ref scrRect,(DxVBLib.CONST_DDBLTFASTFLAGS)((int)DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_SRCCOLORKEY | (int)DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_WAIT));//将图象在屏幕上显示
			}
			else
			{
				//物体部分在屏幕内
				drawRect=scrRect;
				drawRect.Left=0;
				drawRect.Top=0;

				//////////////////////////////////////////////////////////////////////////////////////
				//下方出屏幕
				if (x+scrRect.Right-scrRect.Left<CConst.C_SCREENWIDTH && y+scrRect.Bottom-scrRect.Top>CConst.C_SCREENHEIGHT)	{
					drawRect.Bottom=CConst.C_SCREENHEIGHT-y;
				}
				//右方出屏幕
				if (y+scrRect.Bottom-scrRect.Top<CConst.C_SCREENHEIGHT && x+scrRect.Right-scrRect.Left>CConst.C_SCREENWIDTH)	{
					drawRect.Right=CConst.C_SCREENWIDTH-x;
				}
				//下方和右方都出屏幕
				if (y+scrRect.Bottom-scrRect.Top>CConst.C_SCREENHEIGHT && x+scrRect.Right-scrRect.Left>CConst.C_SCREENWIDTH)	{
					drawRect.Right=CConst.C_SCREENWIDTH-x;
					drawRect.Bottom=CConst.C_SCREENHEIGHT-y;
				}
				//左方出屏幕
				//上方出屏幕
				if (y<0 && y+scrRect.Bottom-scrRect.Top>0)	
				{
					drawRect.Top=-y;
					//Console.WriteLine(drawRect.Top.ToString());
					y=0;
				}
				//左上方出屏幕
				/////////////////////////////////////////////////////////////////////////////////////
				this.DDSBackSurface.BltFast(x,y,DDSurface,ref drawRect,(DxVBLib.CONST_DDBLTFASTFLAGS)((int)DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_SRCCOLORKEY | (int)DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_WAIT));//将图象在屏幕上显示
			}
			return;
		}
		//DrawPicture重载结束

		/// <summary>
		/// 显示文字
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="strText"></param>
		public void DrawText(int x,int y,string strText)
		{
			this.DDSBackSurface.SetForeColor(0xFFFFFF);
			this.DDSBackSurface.DrawText(x,y,strText,false);
			return;
		}
		public void DrawPoint(int x,int y,int c)
		{
			this.DDSBackSurface.SetForeColor(c);
			this.DDSBackSurface.DrawCircle(x,y,1);
			return;
		}
		public void DrawBox(RECT r)
		{
			//public void DrawBox(int x1,int y1,int x2,int y2,int c,int mode,RECT r)
			//this.DDSBackSurface.SetForeColor(c);
			//this.DDSBackSurface.setDrawStyle(mode);
			//this.DDSBackSurface.DrawBox(x1,y1,x2,y2);
			this.DDSBackSurface.SetFillStyle(6);
			this.DDSBackSurface.BltColorFill(ref r,Color.Blue.ToArgb());
			this.DDSBackSurface.SetFillStyle(0);
			return;
		}
		//半透明一个区域
		public void TransparenceRect(int X,int Y,RECT R)
		{
			double alpha = 0.99;
			int lR=0,  lG=0,  lB=0,  color;
			int lR2=0,  lG2=0,  lB2=0;
			int lR3=0,  lG3=0,  lB3=0;
			DDSBackSurface.Lock(ref R, ref descScreen, CONST_DDLOCKFLAGS.DDLOCK_WAIT, 0);
			//objDDSayBox.Lock R, ddsdSayBox, DDLOCK_WAIT, 0
			for (int x=1;x<CConst.C_SCREENWIDTH;x++)
			{
				for (int y=Y;y<Y + R.Bottom;y++)
				{
					color = DDSBackSurface.GetLockedPixel(x, y);
					//'根据返回得Long值取得RGB值:00000 000000 00000-->RGB
					GetRGB (color,ref lR,ref lG,ref lB);
					//color2 = DDSBackSurface.GetLockedPixel(x, y - CConst.C_RpgTalkBoxTop + 1);// '640,135
					//GetRGB (color2,ref lR2,ref lG2,ref lB2);
					lR2 = 100;
					lG2 = 100;
					lB2 = 200;
					//'        'alpha混合
					lR3 = Convert.ToInt32(alpha * lR2 + (1 - alpha) * lR);
					lG3 =Convert.ToInt32(alpha * lG2 + (1 - alpha) * lG);
					lB3 = Convert.ToInt32(alpha * lB2 + (1 - alpha) * lB);
					DDSBackSurface.SetLockedPixel(x, y, Color.FromArgb(lR3, lG3, lB3).ToArgb());
			}
		}
		//objDDSayBox.Unlock R
		DDSBackSurface.Unlock(ref R);
		}
	}
}
