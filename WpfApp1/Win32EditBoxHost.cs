using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop; // HwndHost, HandleRef를 위해 필요

namespace WpfApp1
{


    public class Win32EditBoxHost : HwndHost
    {
        private IntPtr _hwndControl;
        private const string WindowClass_EDIT = "EDIT"; // Win32 EDIT 컨트롤 클래스 이름

        public Win32EditBoxHost()
        {
        }

        // Win32 컨트롤 생성
        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            _hwndControl = IntPtr.Zero;

            // EDIT 컨트롤 스타일
            int style = NativeMethods.WS_CHILD | NativeMethods.WS_VISIBLE |
                        NativeMethods.ES_LEFT | NativeMethods.ES_AUTOHSCROLL |
                        NativeMethods.ES_MULTILINE | NativeMethods.ES_WANTRETURN | // 여러 줄, Enter 키 입력
                        NativeMethods.WS_BORDER; // 테두리 추가

            // 현재 프로세스의 모듈 핸들 가져오기
            IntPtr hInstance = NativeMethods.GetModuleHandle(null);

            _hwndControl = NativeMethods.CreateWindowEx(
                0, // dwExStyle (확장 스타일 없음)
                WindowClass_EDIT, // lpszClassName
                "", // lpszWindowName (컨트롤의 텍스트, 초기값 없음)
                style, // style
                0, // x
                0, // y
                (int)this.ActualWidth,  // width (초기 너비, ArrangeOverride에서 조정 가능)
                (int)this.ActualHeight, // height (초기 높이, ArrangeOverride에서 조정 가능)
                hwndParent.Handle, // hwndParent
                IntPtr.Zero, // hMenu (컨트롤 ID, 여기서는 사용 안 함)
                hInstance, // hInstance
                IntPtr.Zero // lpParam
            );

            return new HandleRef(this, _hwndControl);
        }

        // Win32 컨트롤 파괴
        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            NativeMethods.DestroyWindow(hwnd.Handle);
        }

        // (선택 사항) 메시지 후킹이 필요하면 재정의
        // protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        // {
        //     // 예: 특정 메시지 처리
        //     return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
        // }

        // WPF 레이아웃 시스템에 크기 정보 제공
        protected override Size MeasureOverride(Size constraint)
        {
            // 컨트롤의 기본 크기를 반환하거나, 원하는 크기를 계산하여 반환
            // 여기서는 constraint를 그대로 사용하거나, 최소 크기를 지정할 수 있습니다.
            return constraint; // 또는 return new Size(100, 20);
        }

        // 호스트된 컨트롤의 크기와 위치 조정
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_hwndControl != IntPtr.Zero)
            {
                // HwndHost가 그려질 때 Win32 컨트롤의 크기를 업데이트합니다.
                // SetWindowPos 같은 Win32 API를 사용할 수도 있습니다.
                // 여기서는 HwndHost의 기본 동작으로도 크기가 잘 조절될 수 있습니다.
                // 명시적으로 하려면:
                // NativeMethods.SetWindowPos(_hwndControl, IntPtr.Zero, 0, 0, (int)finalSize.Width, (int)finalSize.Height, SWP_NOZORDER | SWP_NOACTIVATE);
            }
            return base.ArrangeOverride(finalSize); // 매우 중요!
        }

        // Win32 Edit 컨트롤의 텍스트를 가져오는 속성 (선택 사항)
        public string Text
        {
            get
            {
                if (_hwndControl != IntPtr.Zero)
                {
                    int length = (int)NativeMethods.SendMessage(_hwndControl, NativeMethods.WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
                    if (length > 0)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder(length + 1);
                        NativeMethods.SendMessage(_hwndControl, NativeMethods.WM_GETTEXT, (IntPtr)(length + 1), sb);
                        return sb.ToString();
                    }
                }
                return string.Empty;
            }
            // SetText도 유사하게 구현 가능
        }

        // 컨트롤에 포커스 설정 (선택 사항)
        public void FocusControl()
        {
            if (_hwndControl != IntPtr.Zero)
            {
                NativeMethods.SetFocus(_hwndControl);
            }
        }
    }
}