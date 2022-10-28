using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelBase
{
	public interface IEditableObjectExt : IEditableObject
	{
		bool IsChanged { get; set; }
		bool IsInEdit { get; }
	}
}
