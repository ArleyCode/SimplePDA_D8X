using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace NFCFeliCaCE_CS
{
    public class Common
    {
        //        '*** WIN32API constants ***
        //'ShowWindow() function
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;

        //'************************************************************
        //'   WIN32API inport
        //'************************************************************

        [DllImport("CoreDll.dll")]
        public static extern int FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("CoreDll.dll")]
        public static extern int ShowWindow(int hwnd,
            int nCmdShow);

        //'************************************************************
        //'   Hide Taskbar
        //'************************************************************
        public static void HideTaskbar()
        {
            int lpTaskBarHwnd;

            lpTaskBarHwnd = FindWindow("HHTaskBar", "");
            ShowWindow(lpTaskBarHwnd, SW_HIDE);
        }

        //'************************************************************
        //'   Show Taskbar
        //'************************************************************
        public static void ShowTaskbar()
        {
            int lpTaskBarHwnd;

            lpTaskBarHwnd = FindWindow("HHTaskBar", "");
            ShowWindow(lpTaskBarHwnd, SW_SHOW);
        }
    }
}