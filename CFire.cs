using System;

namespace ToyshopGame
{
	/// <summary>
	/// CFire ��ժҪ˵����
	/// </summary>
	public class CFire: CObj
	{
		private int mintDamage;
		private CObj mobjOwner;
		public CFire()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
