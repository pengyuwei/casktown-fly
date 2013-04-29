using System;

namespace ToyshopGame
{
	/// <summary>
	/// CPhase 类，关于动画帧的信息
	/// </summary>
	public class CPhase
	{
		private int mintCurrentPhase;//当前帧数，从1开始计算
		private int mintTotalPhase;//总帧数，从1开始计算
		private bool mbActive;//是否是激活状态
		private float mfloatPhaseChangeSpeed;//帧变的速率（毫秒）
		private DateTime mtimeLastPhaseChangeTime;//上次帧变的时间

		public CPhase()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			mintCurrentPhase=1;//当前帧数，从1开始计算
			mintTotalPhase=1;
			mbActive=true;
			mtimeLastPhaseChangeTime=DateTime.Now;
		}
		public int TotalPhase //总帧数
		{
			get	{return mintTotalPhase;}
			set	
			{
				int intValue=value;
				if (intValue>0)mintTotalPhase=intValue; else mintTotalPhase=1;//必须大于0
			}
		}
		public int CurrentPhase //当前帧数，从1开始计算
		{
			get	{return mintCurrentPhase;}
			set	
			{
				if (value<=TotalPhase)
				{
					mintCurrentPhase=value;
					mtimeLastPhaseChangeTime=DateTime.Now;
				}
				else
				{
					//mintCurrentPhase=1;
					//Console.WriteLine("CurrentPhase={0}",mintCurrentPhase.ToString());
					mbActive=false;
				}
			}
		}
		public float PhaseChangeSpeed
		{
			get	{return mfloatPhaseChangeSpeed;}
			set	{mfloatPhaseChangeSpeed=value;}
		}
		public void UpdatePhase()//根据帧变速率改变当前帧
		{
			if (DateTime.Now>mtimeLastPhaseChangeTime.AddMilliseconds(mfloatPhaseChangeSpeed))
			{
				CurrentPhase++;
			}
		}
		public bool IsActive
		{
			get	{return mbActive;}
			set	{mbActive=value;}
		}
	}
}
