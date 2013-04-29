using System;
using System.Collections;
using DxVBLib;

namespace ToyshopGame
{
	using DirectX=DxVBLib;
	/// <summary>
	/// CDirectX����˵����
	/// ��װ��CDirectX��ع���
	/// �����£�pyw 2004-2-11
	/// </summary>
	public class CDirectXBase
	{
		private DirectX.DirectX7 dx;
		private DirectX.DirectDraw7 DDraw;  
		private DirectX.DirectInput DInput;  
		private DirectX.DirectInputDevice DIDevice;
		private DirectX.DIKEYBOARDSTATE KBState;
		
		protected DirectX.DirectDrawSurface7 DDSScreen;//������
		protected DirectX.DirectDrawSurface7 DDSBackSurface;//��̨����
		protected DDSURFACEDESC2 descScreen;
		public CDirectXBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
		
		//InKey���أ���������
		public DxVBLib.DIKEYBOARDSTATE InKey()//�������м��̼�λ״̬
		{
            DIDevice.GetDeviceStateKeyboard(ref KBState);
			return KBState;
		}
		public bool InKey(DxVBLib.CONST_DIKEYFLAGS Key)//������Ҫ���ļ�λ�������Ƿ�ť��bool
		{
			DIDevice.GetDeviceStateKeyboard(ref KBState);
			return Convert.ToBoolean(KBState.key[(long)Key]);
		}
		//End InKey����

		protected int InitDirectX(IntPtr Handle,int W,int H)//��ʼ��DirectX
		{
			dx=new DirectX.DirectX7();
			
			//��ʼ����������
			InitDirectInput(Handle);

			DDraw=dx.DirectDrawCreate("");
			DDraw.SetCooperativeLevel(Handle.ToInt32(),	DxVBLib.CONST_DDSCLFLAGS.DDSCL_ALLOWMODEX | CONST_DDSCLFLAGS.DDSCL_ALLOWREBOOT | CONST_DDSCLFLAGS.DDSCL_FULLSCREEN | CONST_DDSCLFLAGS.DDSCL_EXCLUSIVE);
			DDraw.SetDisplayMode(W, H, 16, 0, CONST_DDSDMFLAGS.DDSDM_DEFAULT);//  '�ı���Ļ��ʽ
			
			//������
			descScreen=new DDSURFACEDESC2();
		    descScreen.lFlags = DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_CAPS | DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_BACKBUFFERCOUNT;
		    descScreen.ddsCaps.lCaps = CONST_DDSURFACECAPSFLAGS.DDSCAPS_PRIMARYSURFACE | CONST_DDSURFACECAPSFLAGS.DDSCAPS_FLIP | CONST_DDSURFACECAPSFLAGS.DDSCAPS_COMPLEX | CONST_DDSURFACECAPSFLAGS.DDSCAPS_SYSTEMMEMORY;
		    descScreen.lBackBufferCount = 1 ;//'���û���buffers����
		    DDSScreen = DDraw.CreateSurface(ref descScreen);//  '�������ý���������

			//������̨����
			DDSCAPS2 DDCaps=new DDSCAPS2();
			DDCaps.lCaps = DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_BACKBUFFER;//'����backbuffer��Ϣ'sets info about backbuffer
			DDSBackSurface = DDSScreen.GetAttachedSurface(ref DDCaps);
			DDSBackSurface.GetSurfaceDesc(ref descScreen);// 'set the info for the ships surface
			DDSBackSurface.SetForeColor(0);
			DDSBackSurface.SetFontBackColor(0);
			DDSBackSurface.SetFontTransparency(true);
			
			return 0;
		}
		private void InitDirectInput(IntPtr Handle)//��ʼ����������
		{
			DInput=dx.DirectInputCreate();//�����������
			DIDevice = DInput.CreateDevice("GUID_sysKeyboard");//�豸����
			DIDevice.SetCommonDataFormat(DxVBLib.CONST_DICOMMONDATAFORMATS.DIFORMAT_KEYBOARD); //����Ϊ������
			DIDevice.SetCooperativeLevel(Handle.ToInt32(),(DxVBLib.CONST_DISCLFLAGS)((int)DxVBLib.CONST_DISCLFLAGS.DISCL_BACKGROUND | (int)DxVBLib.CONST_DISCLFLAGS.DISCL_NONEXCLUSIVE));//sets it to act with our application
			DIDevice.Acquire(); //'allows reading of keys
			DxVBLib.DIKEYBOARDSTATE KBState=new DxVBLib.DIKEYBOARDSTATE();
		}
		
		//CreateSurface���أ���3����
		protected DxVBLib.DirectDrawSurface7 CreateSurface(string strFile,out DxVBLib.RECT Rect,out DxVBLib.DDSURFACEDESC2 DDesc)
		//���ļ���������
		{
			DxVBLib.DDCOLORKEY ddcColorKey;
			DxVBLib.RECT rect;
			DDesc=new DxVBLib.DDSURFACEDESC2();
			DxVBLib.DirectDrawSurface7 dDsurfaceRet;
			
			DDesc.ddsCaps.lCaps =DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_OFFSCREENPLAIN ; //����Ϊ��������
			dDsurfaceRet=DDraw.CreateSurfaceFromFile(strFile,ref DDesc);
			ddcColorKey.low = 0;//System.Drawing.Color.White.ToArgb();//Color:White
			ddcColorKey.high = 0;//16777215; 
			dDsurfaceRet.SetColorKey(DxVBLib.CONST_DDCKEYFLAGS.DDCKEY_SRCBLT,ref ddcColorKey); 
			rect.Top=0;
			rect.Left=0;
			rect.Right=DDesc.lWidth; //��ͼƬ�����С��ʾ
			rect.Bottom= DDesc.lHeight; 
			Rect=rect;
			return dDsurfaceRet;
		}
		protected DxVBLib.DirectDrawSurface7 CreateSurface(out DxVBLib.DirectDrawSurface7 DDSurfaceBack)
		//���������棨��Ļ�����������غ�̨����
		{
			DxVBLib.DDSURFACEDESC2 dDDesc=new DxVBLib.DDSURFACEDESC2();
			DxVBLib.DirectDrawSurface7 dDsurface;
			dDDesc.lFlags=DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_CAPS;
			dDDesc.ddsCaps.lCaps=DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_PRIMARYSURFACE;
			dDsurface=DDraw.CreateSurface(ref dDDesc);
	
			DxVBLib.DDSCAPS2 DDCaps=new DxVBLib.DDSCAPS2();
			DDCaps.lCaps = DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_BACKBUFFER; //����backbuffer��Ϣ'sets info about backbuffer
			DDSurfaceBack = dDsurface.GetAttachedSurface(ref DDCaps);
			DDSurfaceBack=null;
			return dDsurface;
		}
		protected DxVBLib.DirectDrawSurface7 CreateSurface(DxVBLib.CONST_DDSURFACECAPSFLAGS Caps)
		//����ָ�����͵ı��棨���ں�̨��
		{
			DxVBLib.DDSURFACEDESC2 dDDesc=new DxVBLib.DDSURFACEDESC2();
			DxVBLib.DirectDrawSurface7 DDSurfaceRet;
			dDDesc.lFlags=DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_CAPS;
			dDDesc.ddsCaps.lCaps=Caps;
			DDSurfaceRet=DDraw.CreateSurface(ref dDDesc);
			return DDSurfaceRet;
		}
		//CreateSurface���ؽ���

		public void BackToPrimary()
		{
			RECT rect=new RECT ();
			DDSScreen.BltFast(0,0,DDSBackSurface,ref rect,DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_WAIT);//��ͼ������Ļ����ʾ
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
		public bool RectHitTest(int ax,int ay,int bx,int by,RECT a,RECT b)//RECT�����غ��жϣ��غϷ���True�����򷵻�false
		{
			//��a����Ϊ�жϱ�׼
			int px,py;//�����жϵĵ�
			bool bRet=false;
			RECT rSwap=new RECT();
			int intpSwap=0;

			for (int i=1;i<=2;i++)
			{
				//���Ͻ���b�ķ�Χ��
				px=ax+a.Left;py=ay+a.Top;
				if (px>=b.Left+bx && px<=b.Right+bx && py>=b.Top+by && py<=b.Bottom+by) 
				{
					//Console.WriteLine("���Ͻ���b�ķ�Χ��");
					bRet=true;
				}
				//���Ͻ���b�ķ�Χ��
				px=ax+a.Left+a.Right;py=a.Top+ay;
				if (px>=b.Left+bx && px<=b.Right+bx && py>=b.Top+by && py<=b.Bottom+by) 
				{
					//Console.WriteLine("���Ͻ���b�ķ�Χ��");
					bRet=true;
				}
				//���½���b�ķ�Χ��
				px=ax+a.Left;py=ay+a.Top+a.Bottom;
				if (px>=b.Left+bx && px<=b.Right+bx && py>=b.Top+by && py<=b.Bottom+by) 
				{
					//Console.WriteLine("���½���b�ķ�Χ��");
					bRet=true;
				}
				//���½���b�ķ�Χ��
				px=ax+a.Left+a.Right;py=ay+a.Top+a.Bottom;
				if (px>=b.Left+bx && px<=b.Right+bx && py>=b.Top+by && py<=b.Bottom+by) 
				{
					//Console.WriteLine("���½���b�ķ�Χ��");
					bRet=true;
				}

				//���������ж�
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
				//����Ļ��

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
