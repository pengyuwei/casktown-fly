using System;

namespace ToyshopGame
{
	/// <summary>
	/// CPlane ��ҿ��Ƶķɻ���
	/// </summary>
	public class CPlane:CObj
	{
		private int mintScore;
		private bool mbInvincibleState;//�Ƿ����޵�ģʽ
		private DateTime mLastWoundedTime;

		public CPlane()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			mLastWoundedTime=DateTime.Now;
			mbInvincibleState=false;
		}
		public CPlane(int intLife)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			mLastWoundedTime=DateTime.Now.AddSeconds(-10);
			mintLife=intLife;
		}
		public int Score
		{
			get	{return mintScore;}
			set	{mintScore=value;}
		}
		public override int Life 
		{//����ֵ
			get	
			{
				if (mintLife>0)
					return mintLife;
				else
					return 0;
			}
			set	
			{
				if (DateTime.Now>mLastWoundedTime.AddSeconds(1))//1���ڲ�����������
				{
					mintLife=value;
					mLastWoundedTime=DateTime.Now;
				}
			}
		}
		public void SetLife(int Life)//ǿ������������
		{
			mintLife=Life;
		}
		public bool IsInvincibleState()//��ǰ�Ƿ����޵�ģʽ��
		{
			bool bRet=false;
			bRet=mbInvincibleState;
			if (DateTime.Now>mLastWoundedTime.AddSeconds(1))
			{
				bRet=false;
			}
			else
			{
				bRet=true;
			}
			return bRet;
		}

	}
}
