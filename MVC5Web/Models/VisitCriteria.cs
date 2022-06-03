using Microsoft.Ajax.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC5Web.Models
{
    public class VisitCriteria : BaseCriteria<Visit>
    {
		private int? _id;
		public int? ID
		{
			get { return _id; }
			set
			{
				TryAddCriteria(nameof(this.ID), (object)value, item => item.ID == value);

				_id = value;
			}
		}

		private string _petName;
		public string PetName
		{
			get { return _petName; }
			set
			{
				TryAddCriteria(nameof(this.PetName), (object)value, item => item.PetName.Contains(value));

				_petName = value;
			}
		}
	}
}
