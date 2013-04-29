using System;
using DxVBLib;

namespace ToyshopGame
{
	/// <summary>
	/// CObj �������(�̳���CUnit ��λ�ܻ���)
	/// </summary>
	public class CObj:CUnit {
		protected int mintLife;
		protected int mintLevel;
		private string mstrPic;
		private CPhase mcPhase;

		public CObj() {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			mcPhase=new CPhase();
			mintLife=0;
		}
		public virtual int Level//����ĵȼ�
		{
			get{return mintLevel;}
			set{mintLevel=value;}
		}
		public virtual int Life {//����ֵ
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
