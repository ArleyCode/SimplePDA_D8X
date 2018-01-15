'***************************************************************************
'* <Description>
'* NFCFeliCaMain.vb (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* NFC FeliCa IC Card reading Demo program
'* -------------------------------------------------------------------------
'* <Language>
'* VB.NET
'* -------------------------------------------------------------------------
'* <Develop Environment>
'* VS 2005
'* -------------------------------------------------------------------------
'* <Target>
'* DT-5300 model
'* -------------------------------------------------------------------------
'* Copyright(C)2010 CASIO COMPUTER CO.,LTD. All rights reserved.
'* -------------------------------------------------------------------------
'* <History>
'* Version  Date            Company     Keyword     Comment
'* 1.0.0    2010.05.18      CASIO       000000      Original      
'* 1.0.1    2012.05.22      CASIO       000001      Modify        
'* 
'***************************************************************************/

Imports Calib.NFCFelicaLibNet.Api
Imports Calib.NFCFelicaLibNet.Def
Imports Calib.SystemLibNet.Api
Imports Calib.SystemLibNet.Def

Public Class NFCFeliCaMain
    Dim iRet As Integer
    Dim pSystemCode As Integer
    Dim pIDm(8) As Byte
    Dim pPMm(8) As Byte
    Dim pData(16) As Byte
    Dim strCommand As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button1.Enabled = False

        Label2.Text = ""
        Label3.Text = ""
        Label7.Text = ""
        TextBox2.Text = ""
        Application.DoEvents()

        ' NFC OPEN
        'iRet = NFCFelicaOpen(IntPtr.Zero)

        'TextBox2.Text = "NFCFelicaOpen...start." + ControlChars.NewLine + TextBox2.Text

        'If iRet <> NFC_OK Then
        '    strCommand = "NFCFelicaOpen"
        '    DispErrorMessage(iRet, strCommand)
        '    Exit Sub
        'End If
        'TextBox2.Text = "NFCFelicaOpen...OK." + ControlChars.NewLine + TextBox2.Text

        TextBox2.Text = "NFCFelicaPolling...start." + ControlChars.NewLine + TextBox2.Text

        ' NFCFelica POLLING
        iRet = NFCFelicaPolling(10000, IntPtr.Zero, &HFFFF, 0)

        If iRet <> NFC_OK Then
            strCommand = "NFCFelicaPolling"
            DispErrorMessage(iRet, strCommand)
            Exit Sub
        End If

        TextBox2.Text = "NFCFelicaPolling...OK." + ControlChars.NewLine + TextBox2.Text

        TextBox2.Text = "NFCFelicaGetCardResponse...start." + ControlChars.NewLine + TextBox2.Text

        ' NFCFelica GET CARD RESPONSE
        iRet = NFCFelicaGetCardResponse(pIDm, pPMm, pSystemCode, 0)

        If iRet <> NFC_OK Then
            strCommand = "NFCFelicaGetCardResponse"
            DispErrorMessage(iRet, strCommand)
            Exit Sub
        End If

        TextBox2.Text = "NFCFelicaGetCardResponse...OK." + ControlChars.NewLine + TextBox2.Text

        Label2.Text = pSystemCode.ToString

        Dim i As Integer

        For i = 0 To 7
            Label3.Text = Label3.Text + pIDm(i).ToString("X2") + " "
        Next

        For i = 0 To 7
            Label7.Text = Label7.Text + pPMm(i).ToString("X2") + " "
        Next

        If ComboBox1.SelectedIndex = 0 Then '0 = READ
            ' NFCFelica Read
            TextBox4.Text = ""
            TextBox2.Text = "NFCFelicaRead...start." + ControlChars.NewLine + TextBox2.Text

            iRet = NFCFelicaRead(CInt("&H" + TextBox1.Text.ToString), CInt(TextBox3.Text.ToString), pData, 0)

            If iRet <> NFC_OK Then
                strCommand = "NFCFelicaRead"
                DispErrorMessage(iRet, strCommand)
                Exit Sub
            End If

            TextBox2.Text = "NFCFelicaRead...OK." + ControlChars.NewLine + TextBox2.Text

            For i = 0 To 15
                TextBox4.Text = TextBox4.Text + pData(i).ToString("X2")
            Next
            '' For Security reason : WRITE routine is comment out 
            'ElseIf ComboBox1.SelectedIndex = 1 Then '1 = WRITE
            '    ' NFCFelica Write
            '    TextBox4.Text = TextBox4.Text.PadRight(32)

            '    For i = 0 To 15
            '        If Mid(TextBox4.Text, i * 2 + 1, 2) = "  " Then
            '            pData(i) = CByte(0)
            '        Else
            '            Try
            '                pData(i) = CByte(CInt("&H" + Mid(TextBox4.Text, i * 2 + 1, 2)))
            '            Catch ex As Exception
            '                TextBox2.Text = "Exception error. Input 16 bytes Hex value."
            '                SysSetBuzzerVolume(B_WARNING, BUZZERVOLUME_MAX)
            '                SysPlayBuzzer(B_WARNING, B_USERDEF, B_USERDEF)

            '                NFCFelicaClose()
            '                Button1.Enabled = True
            '                Exit Sub
            '            End Try

            '        End If
            '        'pData(i) = CByte(CInt("&H" + Mid(TextBox4.Text, i * 2 + 1, 2)))
            '    Next

            '    TextBox2.Text = "NFCFelicaWrite...start." + ControlChars.NewLine + TextBox2.Text

            '    iRet = NFCFelicaWrite(CInt("&H" + TextBox1.Text.ToString), CInt(TextBox3.Text.ToString), pData, 0)

            '    If iRet <> NFC_OK Then
            '        strCommand = "NFCFelicaWrite"
            '        DispErrorMessage(iRet, strCommand)
            '        Exit Sub
            '    End If

            '    TextBox2.Text = "NFCFelicaWrite...OK." + ControlChars.NewLine + TextBox2.Text

        End If

        TextBox2.Text = "NFCFelicaRadioOff...start." + ControlChars.NewLine + TextBox2.Text

        ' NFCFelica RADIO OFF
        iRet = NFCFelicaRadioOff()

        If iRet <> NFC_OK Then
            strCommand = "NFCFelicaRadioOff"
            DispErrorMessage(iRet, strCommand)
            Exit Sub
        End If
        TextBox2.Text = "NFCFelicaRadioOff...OK." + ControlChars.NewLine + TextBox2.Text

        ' NFC CLOSE
        'NFCFelicaClose()

        Button1.Enabled = True

        TextBox2.Text = "NFC scanning...OK." + ControlChars.NewLine + TextBox2.Text
        SysSetBuzzerVolume(B_SCANEND, BUZZERVOLUME_MAX)
        SysPlayBuzzer(B_SCANEND, B_USERDEF, B_USERDEF)
    End Sub

    Function DispErrorMessage(ByVal iRet As Integer, ByVal strCommand As String) As Boolean
        Select Case iRet
            Case NFC_PON
                TextBox2.Text = strCommand + " : NFC already opend." + ControlChars.NewLine + TextBox2.Text
            Case NFC_NOT_DEVICE
                TextBox2.Text = strCommand + " : NFC driver error." + ControlChars.NewLine + TextBox2.Text
            Case NFC_ERROR_INVALID_ACCESS
                TextBox2.Text = strCommand + " : Device exclusive error." + ControlChars.NewLine + TextBox2.Text
            Case NFC_ERROR_MODULE
                TextBox2.Text = strCommand + " : NFC module not responce error." + ControlChars.NewLine + TextBox2.Text
            Case NFC_POF
                TextBox2.Text = strCommand + " : NFC not open error." + ControlChars.NewLine + TextBox2.Text
            Case NFC_PRM
                TextBox2.Text = strCommand + " : NFC parameter error." + ControlChars.NewLine + TextBox2.Text
            Case NFC_ERROR_TIMEOUT
                TextBox2.Text = strCommand + " : Timeout error." + ControlChars.NewLine + TextBox2.Text
            Case NFC_ERROR_CALLBACK
                TextBox2.Text = strCommand + " : Call back function error." + ControlChars.NewLine + TextBox2.Text
            Case NFC_ERROR_STOP
                TextBox2.Text = strCommand + " : Stop error by stop function." + ControlChars.NewLine + TextBox2.Text
            Case NFC_NOT_ACTIVATION
                TextBox2.Text = strCommand + " : Card don't start error." + ControlChars.NewLine + TextBox2.Text
            Case NFC_ERROR_SUSPEND
                TextBox2.Text = strCommand + " : Terminal OFF occurred error." + ControlChars.NewLine + TextBox2.Text
            Case NFC_ERROR_AUTOOFF
                TextBox2.Text = strCommand + " : Radio Auto Stop error." + ControlChars.NewLine + TextBox2.Text
            Case Else
                TextBox2.Text = strCommand + " : Other error." + ControlChars.NewLine + TextBox2.Text
        End Select

        SysSetBuzzerVolume(B_WARNING, BUZZERVOLUME_MAX)
        SysPlayBuzzer(B_WARNING, B_USERDEF, B_USERDEF)

        'NFCFelicaClose()
        Button1.Enabled = True
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ShowTaskbar()
        Me.Close()
    End Sub

    Private Sub NFCFeliCaMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HideTaskbar()
        ComboBox1.SelectedIndex = 0

        ' 000001__	Modify
        ' NFC OPEN
        iRet = NFCFelicaOpen(IntPtr.Zero)

        TextBox2.Text = "NFCFelicaOpen...start." + ControlChars.NewLine + TextBox2.Text

        If iRet <> NFC_OK Then
            strCommand = "NFCFelicaOpen"
            DispErrorMessage(iRet, strCommand)
            Exit Sub
        End If
        TextBox2.Text = "NFCFelicaOpen...OK." + ControlChars.NewLine + TextBox2.Text
        ' __000001	Modify

    End Sub

    Private Sub NFCFeliCaMain_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' 000001__	Modify
        ' NFC CLOSE
        NFCFelicaClose()
        ' __000001	Modify

    End Sub
End Class
