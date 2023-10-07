using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vSharpStudio.Util
{
    class WindowPostion
    {
        /// <summary>
        /// This method adjust the window position to avoid from it going 
        /// out of screen bounds.
        /// </summary>
        /// <param name="topLeft">The requiered position without its offset</param>
        /// <param name="maxSize">The max possible size of the window</param>
        /// <param name="offset">The offset of the topLeft postion</param>
        /// <param name="margin">The margin from the screen</param>
        /// <returns>The adjusted position of the window</returns>
        System.Drawing.Point Adjust(System.Drawing.Point topLeft, System.Drawing.Point maxSize, int offset, int margin)
        {
            Screen currentScreen = Screen.FromPoint(topLeft);
            System.Drawing.Rectangle rect = currentScreen.WorkingArea;

            // Set an offset from mouse position.
            topLeft.Offset(offset, offset);

            // Check if the window needs to go above the task bar, 
            // when the task bar shadows the HUD window.
            int totalHight = topLeft.Y + maxSize.Y + margin;

            if (totalHight > rect.Bottom)
            {
                topLeft.Y -= (totalHight - rect.Bottom);

                // If the screen dimensions exceed the hight of the window
                // set it just bellow the top bound.
                if (topLeft.Y < rect.Top)
                {
                    topLeft.Y = rect.Top + margin;
                }
            }

            int totalWidth = topLeft.X + maxSize.X + margin;
            // Check if the window needs to move to the left of the mouse, 
            // when the HUD exceeds the right window bounds.
            if (totalWidth > rect.Right)
            {
                // Since we already set an offset remove it and add the offset 
                // to the other side of the mouse (2x) in addition include the 
                // margin.
                topLeft.X -= (maxSize.X + (2 * offset + margin));

                // If the screen dimensions exceed the width of the window
                // don't exceed the left bound.
                if (topLeft.X < rect.Left)
                {
                    topLeft.X = rect.Left + margin;
                }
            }

            return topLeft;
        }
    }
}
