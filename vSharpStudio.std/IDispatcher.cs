using System;

namespace ViewModelBase
{
	public interface IDispatcher
	{
		bool CheckAccess();
		void BeginInvoke(Action action);
	}
}
