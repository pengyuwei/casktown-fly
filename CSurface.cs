using System;
using DxVBLib;

namespace ToyshopGame
{
	/// <summary>
	/// CSurface 的摘要说明。
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
			// TODO: 在此处添加构造函数逻辑
			//
			mRect=new RECT();
			mcPhase=new CPhase();
		}

		public DxVBLib.DirectDrawSurface7 Surface//Surface
		{
			get{return mDDSurface;}
			set{mDDSurface=value;}
		}
		public DxVBLib.DDSURFACEDESC2 DDesc//Surface的DDSURFACEDESC2信息
		{
			get{return mDDesc;}
			set{mDDesc=value;}
		}
		public DxVBLib.RECT Rect//Surface的RECT信息
		{
			get{return mRect;}
			set{mRect=value;}
		}
		public int SurfaceWidth//Surface的宽度信息，关联RECT信息
		{
			get	{return mRect.Right;}
			set	{mRect.Right=value;}
		}
		public int SurfaceHeight//Surface的高度信息，关联RECT信息
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
			intPhaseWidth=PhaseWidth;//一帧的宽度
			rRet.Top = 0;
			rRet.Left = intPhaseWidth * (CurrentPhase-1);
			rRet.Bottom = mRect.Bottom;
			rRet.Right = rRet.Left + intPhaseWidth-1;
			//Console.WriteLine("Left={0},Right={1}",rRet.Left ,rRet.Right);
			return rRet;
		}
	}
}
