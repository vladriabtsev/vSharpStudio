using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml.Linq;

namespace ViewModelBase
{
    public static class EventRaiser
    {
        public static void Raise(this EventHandler handler, object sender)
        {
            UIDispatcher.Invoke(() =>
            {
                handler?.Invoke(sender, EventArgs.Empty);
            });
        }
        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, T value)
        {
            UIDispatcher.Invoke(() =>
            {
                handler?.Invoke(sender, new EventArgs<T>(value));
            });
        }
        public static void Raise<T>(this EventHandler<T> handler, object sender, T value) where T : EventArgs
        {
            UIDispatcher.Invoke(() =>
            {
                handler?.Invoke(sender, value);
            });
        }
        public static void Raise<T>(this EventHandler<EventArgs<T>> handler, object sender, EventArgs<T> value)
        {
            UIDispatcher.Invoke(() =>
            {
                handler?.Invoke(sender, value);
            });
        }
    }
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}
