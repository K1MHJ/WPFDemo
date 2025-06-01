using System;
using System.Runtime.InteropServices;

namespace WpfApp1
{
    internal static class NativeMethods
    {
        public const int WS_CHILD = 0x40000000;
        public const int WS_VISIBLE = 0x10000000;
        public const int WS_BORDER = 0x00800000; // 테두리 스타일 추가

        public const int ES_LEFT = 0x0000;
        public const int ES_AUTOHSCROLL = 0x0080;
        public const int ES_MULTILINE = 0x0004; // 여러 줄 입력 가능하도록
        public const int ES_WANTRETURN = 0x1000; // Enter 키 입력 받도록

        // CreateWindowEx 함수
        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
        internal static extern IntPtr CreateWindowEx(
            int dwExStyle,
            string lpszClassName,
            string lpszWindowName,
            int style,
            int x, int y,
            int width, int height,
            IntPtr hwndParent,
            IntPtr hMenu,
            IntPtr hInstance,
            IntPtr lpParam);

        // DestroyWindow 함수
        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
        internal static extern bool DestroyWindow(IntPtr hwnd);

        // GetModuleHandle 함수
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr GetModuleHandle(string lpModuleName);

        // SetFocus 함수 (선택 사항: 초기 포커스 설정용)
        [DllImport("user32.dll")]
        internal static extern IntPtr SetFocus(IntPtr hWnd);

        // WM_GETTEXT 메시지 (텍스트 가져오기용, 선택 사항)
        public const int WM_GETTEXT = 0x000D;
        public const int WM_GETTEXTLENGTH = 0x000E;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, System.Text.StringBuilder lParam);

    }
}