using System;

namespace ToyshopGame
{
	/// <summary>
	/// CPlane 玩家控制的飞机。
	/// </summary>
	public class CPlane:CObj
	{
		private int mintScore;
		private bool mbInvincibleState;//是否是无敌模式
		private DateTime mLastWoundedTime;

		public CPlane()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			mLastWoundedTime=DateTime.Now;
			mbInvincibleState=false;
		}
		public CPlane(int intLife)
		{
			//
			// TODO: 在此处添加构造函数逻辑
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
		{//生命值
			get	
			{
				if (mintLife>0)
					return mintLife;
				else
					return 0;
			}
			set	
			{
				if (DateTime.Now>mLastWoundedTime.AddSeconds(1))//1秒内不会连续受伤
				{
					mintLife=value;
					mLastWoundedTime=DateTime.Now;
				}
			}
		}
		public void SetLife(int Life)//强制设置生命力
		{
			mintLife=Life;
		}
		public bool IsInvincibleState()//当前是否是无敌模式？
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
