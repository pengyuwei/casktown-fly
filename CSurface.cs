using System;
using DxVBLib;

namespace ToyshopGame
{
	/// <summary>
	/// CSurface ��ժҪ˵����
	/// </summary>
	public class CSurface
	{
		private DxVBLib.DirectDrawSurface7 mDDSurface;
		private DxVBLib.DDSURFACEDESC2 mDDesc;
		private DxVBLib.RECT mRect;
		private CPhase mcPhase;
		private int mintPhaseWidth;
		public CSurface()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			mRect=new RECT();
			mcPhase=new CPhase();
		}

		public DxVBLib.DirectDrawSurface7 Surface//Surface
		{
			get{return mDDSurface;}
			set{mDDSurface=value;}
		}
		public DxVBLib.DDSURFACEDESC2 DDesc//Surface��DDSURFACEDESC2��Ϣ
		{
			get{return mDDesc;}
			set{mDDesc=value;}
		}
		public DxVBLib.RECT Rect//Surface��RECT��Ϣ
		{
			get{return mRect;}
			set{mRect=value;}
		}
		public int SurfaceWidth//Surface�Ŀ����Ϣ������RECT��Ϣ
		{
			get	{return mRect.Right;}
			set	{mRect.Right=value;}
		}
		public int SurfaceHeight//Surface�ĸ߶���Ϣ������RECT��Ϣ
		{
			get {return mRect.Bottom;}
			set	{mRect.Bottom=value;}
		}
		public CPhase Phase
		{
			get	{return mcPhase;}
			set	{mcPhase=value;}
		}
		public int PhaseWidth
		{
			get	{return mintPhaseWidth;}
			set	{mintPhaseWidth=value;}
		}
		public RECT GetCurrentPhaseRect(int CurrentPhase)
		{
			RECT rRet=new RECT();
			int intPhaseWidth;
			intPhaseWidth=PhaseWidth;//һ֡�Ŀ��
			rRet.Top = 0;
			rRet.Left = intPhaseWidth * (CurrentPhase-1);
			rRet.Bottom = mRect.Bottom;
			rRet.Right = rRet.Left + intPhaseWidth-1;
			//Console.WriteLine("Left={0},Right={1}",rRet.Left ,rRet.Right);
			return rRet;
		}
	}
}
