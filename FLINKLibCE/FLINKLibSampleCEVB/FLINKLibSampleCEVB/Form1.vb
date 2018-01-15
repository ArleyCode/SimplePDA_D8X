'***************************************************************************
'* <Description>
'* Form1.vb (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* FLINK Communication Demo program between PC and Windows CE Terminal
'*  via USB Cradle
'* -------------------------------------------------------------------------
'* <Language>
'* VB.NET
'* -------------------------------------------------------------------------
'* <Develop Environment>
'* VS 2005
'* -------------------------------------------------------------------------
'* <Target>
'* DT-5300 / DT-X8 CE model
'* -------------------------------------------------------------------------
'* Copyright(C)2010 CASIO COMPUTER CO.,LTD. All rights reserved.
'* -------------------------------------------------------------------------
'* <History>
'* Version  Date            Company     Keyword     Comment
'* 1.0.0    2010.02.24      CASIO       000000      Original      
'* 
'***************************************************************************/

Public Class Form1
    Dim port As IntPtr

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'Open Flink
        Dim cString(10) As Char
        Dim rsprm As New Calib.MoFlinkLibNet.FLK_RSPRM
        Dim errinfo As New Calib.MoFlinkLibNet.FLK_ERRINFO

        ' IT-3100
        'port = Calib.MoFlinkLibNet.Api.FLKOpen("IrDA", cString, rsprm, Calib.MoFlinkLibNet.Def.FLK_MODE_HT, IntPtr.Zero, 0)
        ' IT-600 DT-X7 DT-5300 IT-800
        port = Calib.MoFlinkLibNet.Api.FLKOpen("USB", cString, rsprm, Calib.MoFlinkLibNet.Def.FLK_MODE_HT, IntPtr.Zero, 0)
        If ((port.Equals(IntPtr.op_Explicit(Calib.MoFlinkLibNet.Def.FLK_NG)) = True) Or _
           (port.Equals(IntPtr.op_Explicit(Calib.MoFlinkLibNet.Def.FLK_PRM)) = True)) Then
            MessageBox.Show("FLKOpen Error")
            Calib.MoFlinkLibNet.Api.FLKReadErrorStatus(port, errinfo)
            Return
        End If

        ' Timer sets.
        Timer1.Enabled = True
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ' Disconnect
        Dim endKind As Int16
        endKind = Calib.MoFlinkLibNet.Def.FLK_CLOSE_NORMAL

        Calib.MoFlinkLibNet.Api.FLKClose(port, endKind)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' Idle : The LMWin application command of PC is executed.
        Dim iRet As Int32

        iRet = Calib.MoFlinkLibNet.Api.FLKIdle(port, IntPtr.Zero)
        If iRet <> Calib.MoFlinkLibNet.Def.FLK_OK Then
            MessageBox.Show("FLKIdle Error")
            Return
        End If

        ' Timer sets.
        Timer1.Enabled = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ' Send : The file that exists in the terminal is transmitted to PC. LMWin only has to start.
        Dim iRet As Int32

        ' All files under the temp folder on the terminal are transmitted to the C:\temp folder of PC.
        iRet = Calib.MoFlinkLibNet.Api.FLKSendFile(port, Calib.MoFlinkLibNet.Def.FLK_TRANS_NORMAL, "\temp\*.*", "C:\temp\", Calib.MoFlinkLibNet.Def.FLK_PROTECT_VALID)
        If iRet <> Calib.MoFlinkLibNet.Def.FLK_OK Then
            MessageBox.Show("FLKSendFile Error")
            Return
        End If

        ' Timer sets.
        Timer1.Enabled = True
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ' Receive : The file on PC is received to the terminal. LMWin only has to start.
        Dim iRet As Int32

        ' All files that exist in the C:\temp folder on PC are received to the temp folder on the terminal.
        iRet = Calib.MoFlinkLibNet.Api.FLKReceiveFile(port, Calib.MoFlinkLibNet.Def.FLK_TRANS_NORMAL, "C:\temp\*.*", "\temp\", Calib.MoFlinkLibNet.Def.FLK_PROTECT_VALID)
        If iRet <> Calib.MoFlinkLibNet.Def.FLK_OK Then
            MessageBox.Show("FLKReceiveFile Error")
            Return
        End If

        ' Timer sets.
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim flkstatus As New Calib.MoFlinkLibNet.FLK_STATUS

        ' Stop timer.
        Timer1.Enabled = False

        Calib.MoFlinkLibNet.Api.FLKReadStatus(port, flkstatus)
        If flkstatus.status = Calib.MoFlinkLibNet.Def.FLK_STATUS_ERROR Then
            ' Error occurred.
            MessageBox.Show("Command Error")
            Return
        End If

        If flkstatus.status = Calib.MoFlinkLibNet.Def.FLK_STATUS_END Then
            ' Complete operation.
            MessageBox.Show("Command complete")
            Return
        End If

        ' Update display.
        TextBox1.Text = New String(flkstatus.FileName)
        If flkstatus.file_size <> 0 Then
            ProgressBar1.Value = (flkstatus.file_count * 100 / flkstatus.file_size)
            ProgressBar2.Value = (flkstatus.total_count * 100 / flkstatus.total_size)
        End If

        ' Restart timer.
        Timer1.Enabled = True
    End Sub

    Private Sub Form1_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Dim endKind As Int16
        endKind = Calib.MoFlinkLibNet.Def.FLK_CLOSE_NORMAL

        Calib.MoFlinkLibNet.Api.FLKClose(port, endKind)
    End Sub
End Class
