using System;
using System.Collections;

namespace ToyshopGame
{
	/// <summary>
	/// CSurfaces ��ժҪ˵����
	/// </summary>
	public class CSurfaces:CCollection
	{
		public CSurfaces():base()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			count=0;
			CCol=new SortedList(); 
		}
		public new CSurface this [int index]   // Indexer declaration
		{
			get 
			{
				return (CSurface)CCol[index];
			}
			set 
			{
				CCol[index] = value;
			}
		}
		public new CSurface this [string key]   // Indexer declaration
		{
			get 
			{
				return (CSurface)CCol[key];
			}
			set 
			{
				CCol[key] = value;
			}
		}
		public void Add(CSurface Value)
		{
			CCol.Add(++count,Value);
		}
		public void Add(string Key,CSurface Value)
		{
			CCol.Add(Key,Value);
		}
	}
}
