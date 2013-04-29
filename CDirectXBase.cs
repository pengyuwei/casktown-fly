using System;
using System.Collections;
using DxVBLib;

namespace ToyshopGame
{
	using DirectX=DxVBLib;
	/// <summary>
	/// CDirectX基类说明：
	/// 封装了CDirectX相关功能
	/// 最后更新：pyw 2004-2-11
	/// </summary>
	public class CDirectXBase
	{
		private DirectX.DirectX7 dx;
		private DirectX.DirectDraw7 DDraw;  
		private DirectX.DirectInput DInput;  
		private DirectX.DirectInputDevice DIDevice;
		private DirectX.DIKEYBOARDSTATE KBState;
		
		protected DirectX.DirectDrawSurface7 DDSScreen;//主表面
		protected DirectX.DirectDrawSurface7 DDSBackSurface;//后台缓冲
		protected DDSURFACEDESC2 descScreen;
		public CDirectXBase()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public void KeyOn()
		{
			DIDevice.Acquire();
		}
		public void KeyOff()
		{
			DIDevice.Unacquire();
		}
		
		//InKey重载，共两个：
		public DxVBLib.DIKEYBOARDSTATE InKey()//返回所有键盘键位状态
		{
            DIDevice.GetDeviceStateKeyboard(ref KBState);
			return KBState;
		}
		public bool InKey(DxVBLib.CONST_DIKEYFLAGS Key)//传来需要检测的键位，返回是否按钮的bool
		{
			DIDevice.GetDeviceStateKeyboard(ref KBState);
			return Convert.ToBoolean(KBState.key[(long)Key]);
		}
		//End InKey重载

		protected int InitDirectX(IntPtr Handle,int W,int H)//初始化DirectX
		{
			dx=new DirectX.DirectX7();
			
			//初始化键盘输入
			InitDirectInput(Handle);

			DDraw=dx.DirectDrawCreate("");
			DDraw.SetCooperativeLevel(Handle.ToInt32(),	DxVBLib.CONST_DDSCLFLAGS.DDSCL_ALLOWMODEX | CONST_DDSCLFLAGS.DDSCL_ALLOWREBOOT | CONST_DDSCLFLAGS.DDSCL_FULLSCREEN | CONST_DDSCLFLAGS.DDSCL_EXCLUSIVE);
			DDraw.SetDisplayMode(W, H, 16, 0, CONST_DDSDMFLAGS.DDSDM_DEFAULT);//  '改变屏幕方式
			
			//主表面
			descScreen=new DDSURFACEDESC2();
		    descScreen.lFlags = DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_CAPS | DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_BACKBUFFERCOUNT;
		    descScreen.ddsCaps.lCaps = CONST_DDSURFACECAPSFLAGS.DDSCAPS_PRIMARYSURFACE | CONST_DDSURFACECAPSFLAGS.DDSCAPS_FLIP | CONST_DDSURFACECAPSFLAGS.DDSCAPS_COMPLEX | CONST_DDSURFACECAPSFLAGS.DDSCAPS_SYSTEMMEMORY;
		    descScreen.lBackBufferCount = 1 ;//'设置缓存buffers数量
		    DDSScreen = DDraw.CreateSurface(ref descScreen);//  '根据设置建立主层面

			//建立后台缓冲
			DDSCAPS2 DDCaps=new DDSCAPS2();
			DDCaps.lCaps = DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_BACKBUFFER;//'设置backbuffer信息'sets info about backbuffer
			DDSBackSurface = DDSScreen.GetAttachedSurface(ref DDCaps);
			DDSBackSurface.GetSurfaceDesc(ref descScreen);// 'set the info for the ships surface
			DDSBackSurface.SetForeColor(0);
			DDSBackSurface.SetFontBackColor(0);
			DDSBackSurface.SetFontTransparency(true);
			
			return 0;
		}
		private void InitDirectInput(IntPtr Handle)//初始化键盘输入
		{
			DInput=dx.DirectInputCreate();//键盘输入对象
			DIDevice = DInput.CreateDevice("GUID_sysKeyboard");//设备对象
			DIDevice.SetCommonDataFormat(DxVBLib.CONST_DICOMMONDATAFORMATS.DIFORMAT_KEYBOARD); //设置为读键盘
			DIDevice.SetCooperativeLevel(Handle.ToInt32(),(DxVBLib.CONST_DISCLFLAGS)((int)DxVBLib.CONST_DISCLFLAGS.DISCL_BACKGROUND | (int)DxVBLib.CONST_DISCLFLAGS.DISCL_NONEXCLUSIVE));//sets it to act with our application
			DIDevice.Acquire(); //'allows reading of keys
			DxVBLib.DIKEYBOARDSTATE KBState=new DxVBLib.DIKEYBOARDSTATE();
		}
		
		//CreateSurface重载，共3个：
		protected DxVBLib.DirectDrawSurface7 CreateSurface(string strFile,out DxVBLib.RECT Rect,out DxVBLib.DDSURFACEDESC2 DDesc)
		//从文件建立表面
		{
			DxVBLib.DDCOLORKEY ddcColorKey;
			DxVBLib.RECT rect;
			DDesc=new DxVBLib.DDSURFACEDESC2();
			DxVBLib.DirectDrawSurface7 dDsurfaceRet;
			
			DDesc.ddsCaps.lCaps =DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_OFFSCREENPLAIN ; //设置为离屏表面
			dDsurfaceRet=DDraw.CreateSurfaceFromFile(strFile,ref DDesc);
			ddcColorKey.low = 0;//System.Drawing.Color.White.ToArgb();//Color:White
			ddcColorKey.high = 0;//16777215; 
			dDsurfaceRet.SetColorKey(DxVBLib.CONST_DDCKEYFLAGS.DDCKEY_SRCBLT,ref ddcColorKey); 
			rect.Top=0;
			rect.Left=0;
			rect.Right=DDesc.lWidth; //按图片本身大小显示
			rect.Bottom= DDesc.lHeight; 
			Rect=rect;
			return dDsurfaceRet;
		}
		protected DxVBLib.DirectDrawSurface7 CreateSurface(out DxVBLib.DirectDrawSurface7 DDSurfaceBack)
		//返回主表面（屏幕），参数返回后台缓冲
		{
			DxVBLib.DDSURFACEDESC2 dDDesc=new DxVBLib.DDSURFACEDESC2();
			DxVBLib.DirectDrawSurface7 dDsurface;
			dDDesc.lFlags=DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_CAPS;
			dDDesc.ddsCaps.lCaps=DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_PRIMARYSURFACE;
			dDsurface=DDraw.CreateSurface(ref dDDesc);
	
			DxVBLib.DDSCAPS2 DDCaps=new DxVBLib.DDSCAPS2();
			DDCaps.lCaps = DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_BACKBUFFER; //设置backbuffer信息'sets info about backbuffer
			DDSurfaceBack = dDsurface.GetAttachedSurface(ref DDCaps);
			DDSurfaceBack=null;
			return dDsurface;
		}
		protected DxVBLib.DirectDrawSurface7 CreateSurface(DxVBLib.CONST_DDSURFACECAPSFLAGS Caps)
		//返回指定类型的表面（用于后台）
		{
			DxVBLib.DDSURFACEDESC2 dDDesc=new DxVBLib.DDSURFACEDESC2();
			DxVBLib.DirectDrawSurface7 DDSurfaceRet;
			dDDesc.lFlags=DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_CAPS;
			dDDesc.ddsCaps.lCaps=Caps;
			DDSurfaceRet=DDraw.CreateSurface(ref dDDesc);
			return DDSurfaceRet;
		}
		//CreateSurface重载结束

		public void BackToPrimary()
		{
			RECT rect=new RECT ();
			DDSScreen.BltFast(0,0,DDSBackSurface,ref rect,DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_WAIT);//将图象在屏幕上显示
		}
		public void Cls()
		{
			RECT rect=new RECT ();
			DDSBackSurface.BltColorFill(ref rect,0);
			//DDSScreen.BltColorFill(ref rect,0);
		}
		public void ReleaseDX()
		{
			DDraw.RestoreAllSurfaces();
			DDraw.RestoreDisplayMode();
			DDraw=null;
		}
		public bool RectHitTest(int ax,int ay,int bx,int by,RECT a,RECT b)//RECT区域重合判断，重合返回True，否则返回false
		{
			//以a物体为判断标准
			int px,py;//保存判断的点
			bool bRet=false;
			RECT rSwap=new RECT();
			int intpSwap=0;

			for (int i=1;i<=2;i++)
			{
				//左上角在b的范围内
				px=ax+a.Left;py=ay+a.Top;
				if (px>=b.Left+bx && px<=b.Right+bx && py>=b.Top+by && py<=b.Bottom+by) 
				{
					//Console.WriteLine("左上角在b的范围内");
					bRet=true;
				}
				//右上角在b的范围内
				px=ax+a.Left+a.Right;py=a.Top+ay;
				if (px>=b.Left+bx && px<=b.Right+bx && py>=b.Top+by && py<=b.Bottom+by) 
				{
					//Console.WriteLine("右上角在b的范围内");
					bRet=true;
				}
				//左下角在b的范围内
				px=ax+a.Left;py=ay+a.Top+a.Bottom;
				if (px>=b.Left+bx && px<=b.Right+bx && py>=b.Top+by && py<=b.Bottom+by) 
				{
					//Console.WriteLine("左下角在b的范围内");
					bRet=true;
				}
				//右下角在b的范围内
				px=ax+a.Left+a.Right;py=ay+a.Top+a.Bottom;
				if (px>=b.Left+bx && px<=b.Right+bx && py>=b.Top+by && py<=b.Bottom+by) 
				{
					//Console.WriteLine("右下角在b的范围内");
					bRet=true;
				}

				//交换数据判断
				rSwap=a;a=b;b=rSwap;
				intpSwap=ax;ax=bx;bx=intpSwap;
				intpSwap=ay;ay=by;by=intpSwap;
			}
			return bRet;
		}
		public RECT GetDisplayRect(int x,int y,RECT scrRect)
		{
			RECT rRet=new RECT();
			if (RectHitTest(0,0,x,y,CConst.C_SCREENRECT,scrRect))
			{
				//在屏幕内

			}
			return rRet;
		}
		public void GetRGB(int Colour, ref int Red,ref int Green,ref int Blue)
		{
			Red = Colour % 256;
			Green = (Colour % 65636) / 256;
			Blue = Colour / 65536;
		}
	}
}
