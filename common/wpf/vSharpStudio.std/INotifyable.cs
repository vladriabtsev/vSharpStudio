using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModelBase
{
  public interface INotifyable
  {
    void Notify(string token, object sender, NotificationEventArgs e);
  }
}
