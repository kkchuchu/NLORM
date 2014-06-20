using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NLORM.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class IndexAttribute : BaseAttribute
	{
		public IndexAttribute()
		{
			this.YesorNo = true;
		}
		public bool YesorNo { get; set;}
	}
}
