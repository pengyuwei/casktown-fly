using System;
using System.Collections;

namespace ToyshopGame
{
	/// <summary>
	/// CCollection 的摘要说明。
	/// </summary>
	public class CCollection
	{
		protected SortedList CCol;
		protected int count;
		public CCollection()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			count=0;
			CCol=new SortedList(); 
		}
		public virtual object this [int index]   // Indexer declaration
		{
			get 
			{
				return CCol.GetByIndex(index-1);
			}
			set 
			{
				object objTmp=CCol.GetByIndex(index-1);
				objTmp= value;
			}
		}
		public object this [string key]   // Indexer declaration
		{
			get 
			{
				return CCol[key];
			}
			set 
			{
				CCol[key] = value;
			}
		}
		public object this [object Key]   // Indexer declaration
		{
			get 
			{
				return CCol[Key];
			}
			set 
			{
				CCol[Key] = value;
			}
		}
		public virtual void Add(object Value)
		{
			CCol.Add(++count,Value);
		}
		public virtual void Add(object Key,object Value)
		{
			++count;
			CCol.Add(Key,Value);
		}
		public virtual int Count()
		{
			return CCol.Count;
		}
		public object item(object key)
		{
			return CCol[key];
		}
		/// <summary>
		/// 删除一个元素，并自动排列剩下的元素，没有Key对应的元素不会引发异常
		/// </summary>
		/// <param name="Key"></param>
		public virtual void Remove(object Key)
		{
			CCol.Remove(Key);
		}
		public virtual void Remove(int index)
		{
			CCol.RemoveAt(index-1);
		}
		/// <summary>
		/// 清除集合中所有元素，Count置0
		/// </summary>
		public virtual void Clear()
		{
			CCol.Clear();
		}
	}
}
