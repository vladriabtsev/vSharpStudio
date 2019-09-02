using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace vSharpStudio.wpf.Controls
{
    public class DialogBox : Xceed.Wpf.Toolkit.MessageBox
    {
        public static MessageBoxResult Show(UserControl userControl, string caption, MessageBoxButton button)
        {
            return ShowCore(null, IntPtr.Zero, userControl, caption, button, MessageBoxImage.None, MessageBoxResult.None, (Style)null);
        }
        /// <summary>
        /// Shows the MessageBox.
        /// </summary>
        /// <param name="messageText">The message text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="defaultResult">The default result.</param>
        /// <returns></returns>
        private static MessageBoxResult ShowCore(Window owner, IntPtr ownerHandle, UserControl userControl, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, Style messageBoxStyle)
        {
            if (System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted)
            {
                throw new InvalidOperationException("Static methods for MessageBoxes are not available in XBAP. Use the instance ShowMessageBox methods instead.");
            }

            if ((owner != null) && (ownerHandle != IntPtr.Zero))
            {
                throw new NotSupportedException("The owner of a MessageBox can't be both a Window and a WindowHandle.");
            }

            var msgBox = new DialogBox();
            msgBox.InitializeMessageBox(owner, ownerHandle, null, caption, button, icon, defaultResult);

            // Setting the style to null will inhibit any implicit styles      
            if (messageBoxStyle != null)
            {
                msgBox.Style = messageBoxStyle;
            }

            msgBox.ShowDialog();
            return msgBox.MessageBoxResult;
        }
        /// <summary>
        /// Display the MessageBox window and returns only when this MessageBox closes.
        /// </summary>
        public bool? ShowDialog()
        {
            if (this.Parent != null)
                throw new InvalidOperationException(
                  "This method is not intended to be called while displaying a Message Box inside a WindowContainer. Use 'ShowMessageBox()' instead.");

            _dialogResult = System.Windows.MessageBoxResult.None;
            this.Visibility = Visibility.Visible;
            this.CreateContainer();

            return this.Container.ShowDialog();
        }
        /// <summary>
        /// Creates the container which will host the MessageBox control.
        /// </summary>
        /// <returns></returns>
        private Window CreateContainer()
        {
            var newWindow = new Window();
            newWindow.AllowsTransparency = true;
            newWindow.Background = Brushes.Transparent;
            newWindow.Content = this;

            if (_ownerHandle != IntPtr.Zero)
            {
                var windowHelper = new WindowInteropHelper(newWindow) { Owner = _ownerHandle };
                newWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            }
            else
            {
                newWindow.Owner = _owner ?? ComputeOwnerWindow();
                if (newWindow.Owner != null)
                    newWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                else
                    newWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            }

            newWindow.ShowInTaskbar = false;
            newWindow.SizeToContent = System.Windows.SizeToContent.WidthAndHeight;
            newWindow.ResizeMode = System.Windows.ResizeMode.NoResize;
            newWindow.WindowStyle = System.Windows.WindowStyle.None;
            newWindow.Closed += new EventHandler(OnContainerClosed);
            return newWindow;
        }
        private MessageBoxResult _dialogResult = MessageBoxResult.None;
    }
}
