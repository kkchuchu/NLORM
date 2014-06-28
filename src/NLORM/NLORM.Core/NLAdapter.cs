using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NLORM.Core
{
	public class NLAdapter<T>
	{		
		private INLORMDb db;
		private string selectstr;
		private IEnumerable<T> items;
		private NLCollection<T> collection;
		private int count;
		private ISqlBuilder sqlBuilder;
		public NLAdapter( INLORMDb db, string selectstring)
		{
			this.db = db;
			this.selectstr = selectstring;
			this.items = db.Query<T>(selectstr);
			this.collection.ToNLC( items);
			this.count = items.Count();
		}

		public NLCollection<T> Collection
		{
			get
			{
				return collection;
			}
		}
	}

	public class NLCollection<T> : IEnumerable, IEnumerable<T>
	{
		public int Count 
		{ 
			get
			{
				return this.List.Count;
			} 
		}
		protected ArrayList List 
		{ 
			get
			{
				return List;
			} 
		}
		private List< Dapper.CommandDefinition> multiscripts;
		public virtual void CopyTo(Array ar, int index)
		{
		}
		public void  ToNLC( IEnumerable<T> coll)
		{
			this.List.AddRange( coll as ICollection);
		}

		public T this[ int index]
		{
			get
			{
				List.Add( new Dapper.CommandDefinition( @""));
				return (T)List[index];
			}
		}
		public int Add( T item)
		{
			List.Add( item);

			List.Add( new Dapper.CommandDefinition( @""));

			return this.Count;
		}

		public bool Remove(T item)
		{
			bool succedtag = true;
			try
			{
				List.Remove( item);
				List.Add( new Dapper.CommandDefinition( @""));// todo
			}
			catch ( ArgumentNullException)
			{
				succedtag = false;
			}

			return succedtag;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
		   return (IEnumerator) GetEnumerator();
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return (IEnumerator<T>) GetEnumerator();
		}

		public NLCollectionEnum<T> GetEnumerator()
		{
			return new NLCollectionEnum<T>(List.ToArray() as T[]);
		}
	}

	public class NLCollectionEnum<T> : IEnumerator, IEnumerator<T>
	{
		public T[] _collect;

		// Enumerators are positioned before the first element
		// until the first MoveNext() call.
		int position = -1;

		public NLCollectionEnum(T[] list)
		{
			_collect = list;
		}

		public bool MoveNext()
		{
			position++;
			return (position < _collect.Length);
		}

		public void Reset()
		{
			position = -1;
		}

		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}

		public T Current
		{
			get
			{
				try
				{
					return _collect[position];
				}
				catch (IndexOutOfRangeException)
				{
					throw new InvalidOperationException();
				}
			}
		}

		public void Dispose()
		{
			this._collect = null;
		}
	}
}
