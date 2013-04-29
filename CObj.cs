using System;
using DxVBLib;

namespace ToyshopGame
{
	/// <summary>
	/// CObj 物体基类(继承自CUnit 单位总基类)
	/// </summary>
	public class CObj:CUnit {
		protected int mintLife;
		protected int mintLevel;
		private string mstrPic;
		private CPhase mcPhase;

		public CObj() {
			//
			// TODO: 在此处添加构造函数逻辑
			//
			mcPhase=new CPhase();
			mintLife=0;
		}
		public virtual int Level//物体的等级
		{
			get{return mintLevel;}
			set{mintLevel=value;}
		}
		public virtual int Life {//生命值
			get	
			{
				if (mintLife>0)
				return mintLife;
				else
				return 0;
			}
			set	{mintLife=value;}
		}
		public string Picture {
			get	{return mstrPic;}
			set	{mstrPic=value;}
		}
		public CPhase Phase
		{
			get	{return mcPhase;}
			set	{mcPhase=value;}
		}

	}
}
