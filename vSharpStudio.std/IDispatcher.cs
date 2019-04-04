using System;
using System.Windows.Threading;

namespace ViewModelBase
{
	public interface IDispatcher
	{
		bool CheckAccess();
		void BeginInvoke(Action action);
    }
}
