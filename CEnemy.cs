using System;

namespace ToyshopGame
{
	/// <summary>
	/// CEnemy ��ժҪ˵����
	/// </summary>
	public class CEnemy: CObj
	{
		private int mintScore;
		private int mintDamage;
		private int mintAmmoCount;
		public CEnemy()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			Random Rnd=new Random((int)DateTime.Now.Millisecond);
			X=Rnd.Next(100,500);
			Y=Rnd.Next(-1250,-500);
		}
		public int Score //����
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
