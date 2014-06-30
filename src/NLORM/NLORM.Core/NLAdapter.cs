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

		public NLAdapter( INLORMDb db, string selectstring)
		{
			this.db = db;
			this.selectstr = selectstring;
			this.items = db.Query<T>(selectstr);

			ArrayList arrli = new ArrayList();
			arrli.AddRange( items.ToArray());
			this.collection = new NLCollection<T>( arrli);
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
		public NLCollection( ArrayList items)
		{
			this._list = items;
			multiscri = new List<Dapper.CommandDefinition>();
		}
		public int Count 
		{ 
			get
			{
				return this._list.Count;
			} 
		}
		private List<Dapper.CommandDefinition> multiscri;
		private ArrayList _list;
		public virtual void CopyTo(Array ar, int index)
		{
		}

		public T this[ int index]
		{
			get
			{
				multiscri.Add( new Dapper.CommandDefinition( @""));
				return (T)_list[index];
			}
		}
		public int Add( T item)
		{
			_list.Add( item);

			multiscri.Add( new Dapper.CommandDefinition( @""));

			return this.Count;
		}

		public bool Remove(T item)
		{
			bool succedtag = true;
			try
			{
				for ( int i = 0; i < _list.Count; i++)
				{
					if ( this.PublicInstancePropertiesEqual( _list[i], item))
					{
						_list.RemoveAt(i);
						break;
					}
				}
				multiscri.Add( new Dapper.CommandDefinition( @""));// todo
			}
			catch ( ArgumentNullException)
			{
				succedtag = false;
			}
			return succedtag;
		}

		private bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore)
		{
			if (self != null && to != null)
			{
				Type type = typeof(T);
				List<string> ignoreList = new List<string>(ignore);
				foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
				{
					if (!ignoreList.Contains(pi.Name))
					{
						object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
						object toValue = type.GetProperty(pi.Name).GetValue(to, null);

						if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
						{
							return false;
						}
					}
				}
				return true;
			}
			if ( self == null && to == null)
			{
				return true;
			}
			return false;
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
			//update all
			return new NLCollectionEnum<T>(_list.ToArray( typeof(T)) as T[]);
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
