using System;

namespace ToyshopGame
{
	/// <summary>
	/// CFire 的摘要说明。
	/// </summary>
	public class CFire: CObj
	{
		private int mintDamage;
		private CObj mobjOwner;
		public CFire()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			mintDamage=0;
			mobjOwner=null;
		}
		public int Damage
		{
			get	{return mintDamage;}
			set	{mintDamage=value;}
		}
		public CObj Owner
		{
			get	{return mobjOwner;}
			set	{mobjOwner=value;}
		}
	}
}
