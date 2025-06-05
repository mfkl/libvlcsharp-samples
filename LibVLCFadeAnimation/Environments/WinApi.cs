using System;
using System.Runtime.InteropServices;

namespace LibVLCFadeAnimation.Environments
{
    internal class WinApi
    {
        #region Opacity
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, byte crKey, byte bAlpha, int dwFlags);

        private const int WS_EX_LAYERED = 0x00080000;
        private const int GWL_EXSTYLE = -20;
        private const int LWA_ALPHA = 0x2;

        public static void SetWindowOpacity(IntPtr hWnd, double opacity)
        {
            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, exStyle | WS_EX_LAYERED);

            // Set window opacity (0 = fully transparent, 255 = fully opaque)
            SetLayeredWindowAttributes(hWnd, 0, Convert.ToByte(opacity * 255), LWA_ALPHA);
        }
        #endregion
    }
}
