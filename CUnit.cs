using System;

namespace ToyshopGame
{
	/// <summary>
	/// CUnit ��λ���ࣨ��Ϸ����С��λ����
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
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			Random Rnd=new Random((int)DateTime.Now.Millisecond);
			mintXP=Rnd.Next(-5,5);
			mintYP=Rnd.Next(1,5);
		}
		public int X//�ƶ��ٶ�
		{
			get	{return mintX;}
			set	{mintX=value;}
		}
		public int Y//�ƶ��ٶ�
		{
			get	{return mintY;}
			set	{mintY=value;}
		}
		public int Speed//�ƶ��ٶ�
		{
			get	{return mintSpeed;}
			set	{mintSpeed=value;}
		}
		public int XP//X������ƶ���λ
		{
			get	{return mintXP;}
			set	{mintXP=value;}
		}
		public int YP//Y������ƶ���λ
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
		public int Type//��λ������
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
