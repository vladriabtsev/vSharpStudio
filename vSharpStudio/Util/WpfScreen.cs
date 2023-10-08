using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows;
using System;
using System.Drawing;
using Point = System.Windows.Point;

class WpfScreen
{
    public static IEnumerable<WpfScreen> AllScreens()
    {
        foreach (Screen screen in Screen.AllScreens)
        {
            yield return new WpfScreen(screen);
        }
    }

    public static WpfScreen GetScreenFrom(Window window)
    {
        WindowInteropHelper windowInteropHelper = new WindowInteropHelper(window);
        Screen screen = Screen.FromHandle(windowInteropHelper.Handle);
        WpfScreen wpfScreen = new WpfScreen(screen);
        return wpfScreen;
    }

    public static WpfScreen GetScreenFrom(Point point)
    {
        int x = (int)Math.Round(point.X);
        int y = (int)Math.Round(point.Y);

        // are x,y device-independent-pixels ??
        System.Drawing.Point drawingPoint = new System.Drawing.Point(x, y);
        Screen screen = Screen.FromPoint(drawingPoint);
        WpfScreen wpfScreen = new WpfScreen(screen);

        return wpfScreen;
    }

    public static WpfScreen Primary
    {
        get 
        {   if (Screen.PrimaryScreen == null) throw new NullReferenceException($"{nameof(Screen)} is null");
            return new WpfScreen(Screen.PrimaryScreen); 
        }
    }

    private readonly Screen screen;

    internal WpfScreen(Screen screen)
    {
        this.screen = screen;
    }

    public Rect DeviceBounds
    {
        get { return this.GetRect(this.screen.Bounds); }
    }

    public Rect WorkingAreaPx
    {
        get { return this.GetRect(this.screen.WorkingArea); }
    }

    private Rect GetRect(Rectangle value)
    {
        // should x, y, width, height be device-independent-pixels ??
        return new Rect
        {
            X = value.X,
            Y = value.Y,
            Width = value.Width,
            Height = value.Height
        };
    }

    public bool IsPrimary
    {
        get { return this.screen.Primary; }
    }

    public string DeviceName
    {
        get { return this.screen.DeviceName; }
    }

    //IDisposable using only
    public static Graphics CreateConverter() => Graphics.FromHwnd(IntPtr.Zero);

    public static double XUnitsToPixels(Graphics g, double unitX) => g.DpiX / 96.0 * unitX;
    public static double YUnitsToPixels(Graphics g, double unitY) => g.DpiY / 96.0 * unitY;

    public static double XPixelsToUnits(Graphics g, double pixelX) => pixelX * 96.0 / g.DpiX;
    public static double YPixelsToUnits(Graphics g, double pixelY) => pixelY * 96.0 / g.DpiY;

}
