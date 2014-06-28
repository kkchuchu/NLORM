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
		private int count;
		private List<string> script;
		private ISqlBuilder sqlBuilder;
		public NLAdapter( INLORMDb db, string selectstring)
		{
			this.db = db;
			this.selectstr = selectstring;

			items = db.Query<T>(selectstr);
			this.count = items.Count();
		}

		public int Add( T item)
		{
			items = items.Concat( new[] {item});

			script.Add( sqlBuilder.GenInsertSql<T>());
			count++;

			return count;
		}

		public bool Remove(T item)
		{
			bool succedtag = true;
			try
			{
				items = items.Except( new[] {item});
				script.Add( "");// todo
			}
			catch ( ArgumentNullException)
			{
				succedtag = false;
			}

			return succedtag;
		}

		public IEnumerable<T> Items
		{
			get
			{
				return items;
			}
		}
	}

	public class NLItem<T> : IEnumerable<T>, ICollection, IEnumerable
	{
		
	}

}
