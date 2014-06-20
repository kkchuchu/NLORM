using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class PrimaryKeyAttribute : BaseAttribute
	{
		public PrimaryKeyAttribute()
		{
			this.YesorNo = true;
		}
		public bool YesorNo { get; set;}
	}
}
