using System;

namespace ToyshopGame
{
	/// <summary>
	/// CUnit 单位基类（游戏中最小单位）。
	/// </summary>
	public class CUnit
	{
		private int mintType;
		private int mintX;
		private int mintY;
		private int mintSpeed;
		private int mintXP;
		private int mintYP;

		public CUnit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			Random Rnd=new Random((int)DateTime.Now.Millisecond);
			mintXP=Rnd.Next(-5,5);
			mintYP=Rnd.Next(1,5);
		}
		public int X//移动速度
		{
			get	{return mintX;}
			set	{mintX=value;}
		}
		public int Y//移动速度
		{
			get	{return mintY;}
			set	{mintY=value;}
		}
		public int Speed//移动速度
		{
			get	{return mintSpeed;}
			set	{mintSpeed=value;}
		}
		public int XP//X方向的移动单位
		{
			get	{return mintXP;}
			set	{mintXP=value;}
		}
		public int YP//Y方向的移动单位
		{
			get	{return mintYP;}
			set	{mintYP=value;}
		}
		public void UpdateMove()
		{
			if (Y>0)
			{
				X+=XP;
				Y+=YP;
			}
			else
			{
				Y+=Speed;
			}
		}
		public int Type//单位的类型
		{
			get	{return mintType;}
			set	{mintType=value;}
		}
		public void MoveUp()
		{
			Y-=Speed;
		}
		public void MoveDown()
		{
			Y+=Speed;
		}
		public void MoveLeft()
		{
			X-=Speed;
		}
		public void MoveRight()
		{
			X+=Speed;
		}
	}
}
