using System;

namespace ToyshopGame
{
	/// <summary>
	/// CEnemy 的摘要说明。
	/// </summary>
	public class CEnemy: CObj
	{
		private int mintScore;
		private int mintDamage;
		private int mintAmmoCount;
		public CEnemy()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			Random Rnd=new Random((int)DateTime.Now.Millisecond);
			X=Rnd.Next(100,500);
			Y=Rnd.Next(-1250,-500);
		}
		public int Score //分数
		{
			get	{return mintScore;}
			set	{mintScore=value;}
		}
		public int Damage
		{
			get	{return mintDamage;}
			set	{mintDamage=value;}
		}
		public int AmmoCount
		{
			get	{return mintAmmoCount;}
			set	{mintAmmoCount=value;}
		}
	}
}
