using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelBase
{
	public interface IValidatable
	{
		bool Validate();
		void ValidateAsync();
	}
}
