Module Common
    '*** WIN32API constants ***
    'ShowWindow() function
    Public Const SW_HIDE As Integer = 0
    Public Const SW_SHOWNORMAL As Integer = 1
    Public Const SW_NORMAL As Integer = 1
    Public Const SW_SHOWMINIMIZED As Integer = 2
    Public Const SW_SHOWMAXIMIZED As Integer = 3
    Public Const SW_MAXIMIZE As Integer = 3
    Public Const SW_SHOWNOACTIVATE As Integer = 4
    Public Const SW_SHOW As Integer = 5
    Public Const SW_MINIMIZE As Integer = 6
    Public Const SW_SHOWMINNOACTIVE As Integer = 7
    Public Const SW_SHOWNA As Integer = 8
    Public Const SW_RESTORE As Integer = 9
    Public Const SW_SHOWDEFAULT As Integer = 10
    Public Const SW_FORCEMINIMIZE As Integer = 11
    Public Const SW_MAX As Integer = 11

    '************************************************************
    '   WIN32API inport
    '************************************************************
    <System.Runtime.InteropServices.DllImport("CoreDll")> _
    Public Function FindWindow( _
                        ByVal className As String, _
                        ByVal WindowsName As String) As IntPtr
    End Function

    <System.Runtime.InteropServices.DllImport("CoreDll")> _
    Public Function ShowWindow( _
                        ByVal hwnd As IntPtr, _
                        ByVal nCmdShow As Integer) As Boolean
    End Function

    '************************************************************
    '   Hide Taskbar
    '************************************************************
    Public Sub HideTaskbar()
        Dim lpTaskBarHwnd As IntPtr

        '** Hiding Taskbar
        lpTaskBarHwnd = FindWindow("HHTaskBar", vbNullString)
        ShowWindow(lpTaskBarHwnd, SW_HIDE)

    End Sub

    '************************************************************
    '   Show Taskbar
    '************************************************************
    Public Sub ShowTaskbar()
        Dim lpTaskBarHwnd As IntPtr

        '** Showing Taskbar
        lpTaskBarHwnd = FindWindow("HHTaskBar", vbNullString)
        ShowWindow(lpTaskBarHwnd, SW_SHOW)

    End Sub
End Module
