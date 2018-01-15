Imports Calib.OBReadLibNet.Api



Public Class Form1
    Declare Function GetForegroundWindow Lib "coredll" () As IntPtr

    Dim HWND As IntPtr

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OBRStartScanning(Calib.OBReadLibNet.Def.OBR_INFINITE)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OBRStopScanning()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HWND = GetForegroundWindow()
        OBROpen(HWND, 0)

    End Sub

    Private Sub Form1_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        OBRClose()  'OBRDRV Close
        Me.Close()
    End Sub
End Class
