using System;

namespace ToyshopGame
{
	/// <summary>
	/// CPhase �࣬���ڶ���֡����Ϣ
	/// </summary>
	public class CPhase
	{
		private int mintCurrentPhase;//��ǰ֡������1��ʼ����
		private int mintTotalPhase;//��֡������1��ʼ����
		private bool mbActive;//�Ƿ��Ǽ���״̬
		private float mfloatPhaseChangeSpeed;//֡������ʣ����룩
		private DateTime mtimeLastPhaseChangeTime;//�ϴ�֡���ʱ��

		public CPhase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			mintCurrentPhase=1;//��ǰ֡������1��ʼ����
			mintTotalPhase=1;
			mbActive=true;
			mtimeLastPhaseChangeTime=DateTime.Now;
		}
		public int TotalPhase //��֡��
		{
			get	{return mintTotalPhase;}
			set	
			{
				int intValue=value;
				if (intValue>0)mintTotalPhase=intValue; else mintTotalPhase=1;//�������0
			}
		}
		public int CurrentPhase //��ǰ֡������1��ʼ����
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
		public void UpdatePhase()//����֡�����ʸı䵱ǰ֡
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
