'***************************************************************************
'* <Description>
'* NFCMifareMain.vb (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* NFC Mifare IC Card reading Demo program
'* -------------------------------------------------------------------------
'* <Language>
'* VB.NET
'* -------------------------------------------------------------------------
'* <Develop Environment>
'* VS 2005
'* -------------------------------------------------------------------------
'* <Target>
'* DT-5300 CE model
'* -------------------------------------------------------------------------
'* Copyright(C)2010 CASIO COMPUTER CO.,LTD. All rights reserved.
'* -------------------------------------------------------------------------
'* <History>
'* Version  Date            Company     Keyword     Comment
'* 1.0.0    2010.05.26      CASIO       000000      Original      
'* 1.0.1    2012.05.22      CASIO       000001      Modify        
'* 
'***************************************************************************/

Imports Calib.NFCMifareLibNet.Api
Imports Calib.NFCMifareLibNet.Def
Imports Calib.SystemLibNet.Api
Imports Calib.SystemLibNet.Def

Public Class NFCMifareMain
    Dim iRet As Integer
    Dim pATQ(2) As Byte
    Dim pSAK(1) As Byte
    Dim pUid(7) As Byte
    Dim pUidLen(1) As Byte
    Dim pData(16) As Byte
    Dim strCommand As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim i As Integer
        Dim dwMode As Integer

        Button1.Enabled = False

        Label2.Text = ""
        Label3.Text = ""
        Label7.Text = ""
        Label13.Text = ""
        TextBox2.Text = ""
        Application.DoEvents()

        ' NFC OPEN
        'iRet = NFCMifareOpen(IntPtr.Zero)

        'TextBox2.Text = "NFCMifareOpen...start." + ControlChars.NewLine + TextBox2.Text

        'If iRet <> NFC_OK Then
        '    strCommand = "NFCMifareOpen"
        '    DispErrorMessage(iRet, strCommand)
        '    Exit Sub
        'End If
        'TextBox2.Text = "NFCMifareOpen...OK." + ControlChars.NewLine + TextBox2.Text

        TextBox2.Text = "NFCMifarePolling...start." + ControlChars.NewLine + TextBox2.Text

        ' NFCMifare POLLING
        iRet = NFCMifarePolling(10000, IntPtr.Zero, 0)

        If iRet <> NFC_OK Then
            strCommand = "NFCMifarePolling"
            DispErrorMessage(iRet, strCommand)
            Exit Sub
        End If

        TextBox2.Text = "NFCMifarePolling...OK." + ControlChars.NewLine + TextBox2.Text

        TextBox2.Text = "NFCMifareGetCardResponse...start." + ControlChars.NewLine + TextBox2.Text

        ' NFCMifare GET CARD RESPONSE
        ' 000001__	Modify
        iRet = NFCMifareGetCardResponse(pATQ, pSAK, pUid, pUidLen(0), 0)
        ' __000001	Modify

        If iRet <> NFC_OK Then
            strCommand = "NFCMifareGetCardResponse"
            DispErrorMessage(iRet, strCommand)
            Exit Sub
        End If

        TextBox2.Text = "NFCMifareGetCardResponse...OK." + ControlChars.NewLine + TextBox2.Text

        For i = 0 To 1
            Label2.Text = Label2.Text + pATQ(i).ToString("X2") + " "
        Next
        'Label2.Text = pATQSystemCode.ToString



        For i = 0 To 0
            Label3.Text = Label3.Text + pSAK(i).ToString("X2") + " "
        Next

        For i = 0 To 6
            Label7.Text = Label7.Text + pUid(i).ToString("X2") + " "
        Next

        For i = 0 To 0
            Label13.Text = Label13.Text + pUidLen(i).ToString("X2") + " "
        Next


        If CInt(Label13.Text) = 4 Then  ' if Mifare is Standard card
            TextBox2.Text = "NFCMifareAuthentication...start." + ControlChars.NewLine + TextBox2.Text

            If ComboBox2.SelectedIndex = 0 Then
                dwMode = NFC_MIFARE_KEYA
            Else
                dwMode = NFC_MIFARE_KEYB
            End If

            TextBox5.Text = TextBox5.Text.PadRight(12)  'Authentication key

            For i = 0 To 5
                If Mid(TextBox5.Text, i * 2 + 1, 2) = "  " Then
                    pData(i) = CByte(0)
                Else
                    Try
                        pData(i) = CByte(CInt("&H" + Mid(TextBox5.Text, i * 2 + 1, 2)))
                    Catch ex As Exception
                        TextBox2.Text = "Exception error. Input 6 bytes Hex value." + ControlChars.NewLine + TextBox2.Text
                        SysSetBuzzerVolume(B_WARNING, BUZZERVOLUME_MAX)
                        SysPlayBuzzer(B_WARNING, B_USERDEF, B_USERDEF)

                        NFCMifareClose()
                        Button1.Enabled = True
                        Exit Sub
                    End Try

                End If
                'pData(i) = CByte(CInt("&H" + Mid(TextBox4.Text, i * 2 + 1, 2)))
            Next

            iRet = NFCMifareAuthentication(dwMode, pData, CInt(TextBox1.Text.ToString), 0)

            If iRet <> NFC_OK Then
                strCommand = "NFCMifareAuthentication"
                DispErrorMessage(iRet, strCommand)
                Exit Sub
            End If

            TextBox2.Text = "NFCMifareAuthentication...OK." + ControlChars.NewLine + TextBox2.Text
        End If

        If ComboBox1.SelectedIndex = 0 Then '0 = READ
            ' NFCMifare Read
            TextBox2.Text = "NFCMifareRead...start." + ControlChars.NewLine + TextBox2.Text
            TextBox4.Text = ""
            iRet = NFCMifareRead(CInt(TextBox3.Text.ToString), pData, 0)

            If iRet <> NFC_OK Then
                strCommand = "NFCMifareRead"
                DispErrorMessage(iRet, strCommand)
                Exit Sub
            End If

            TextBox2.Text = "NFCMifareRead...OK." + ControlChars.NewLine + TextBox2.Text

            For i = 0 To 15
                TextBox4.Text = TextBox4.Text + pData(i).ToString("X2")
            Next

        ElseIf ComboBox1.SelectedIndex = 1 Then '1 = WRITE
            ' NFCMifare Write
            TextBox2.Text = "NFCMifareWrite...start." + ControlChars.NewLine + TextBox2.Text
            TextBox4.Text = TextBox4.Text.PadRight(32)

            For i = 0 To 15
                If Mid(TextBox4.Text, i * 2 + 1, 2) = "  " Then
                    pData(i) = CByte(0)
                Else
                    Try
                        pData(i) = CByte(CInt("&H" + Mid(TextBox4.Text, i * 2 + 1, 2)))
                    Catch ex As Exception
                        TextBox2.Text = "Exception error. Input 16 bytes Hex value." + ControlChars.NewLine + TextBox2.Text
                        SysSetBuzzerVolume(B_WARNING, BUZZERVOLUME_MAX)
                        SysPlayBuzzer(B_WARNING, B_USERDEF, B_USERDEF)

                        NFCMifareClose()
                        Button1.Enabled = True
                        Exit Sub
                    End Try

                End If
                'pData(i) = CByte(CInt("&H" + Mid(TextBox4.Text, i * 2 + 1, 2)))
            Next

            iRet = NFCMifareWrite(CInt(TextBox3.Text.ToString), pData, 0)

            If iRet <> NFC_OK Then
                strCommand = "NFCMifareWrite"
                DispErrorMessage(iRet, strCommand)
                Exit Sub
            End If

            TextBox2.Text = "NFCMifareWrite...OK." + ControlChars.NewLine + TextBox2.Text
        ElseIf ComboBox1.SelectedIndex = 2 Then '2 = WRITE Ultralight
            ' NFCMifare4 Write
            TextBox2.Text = "NFCMifareWrite4...start." + ControlChars.NewLine + TextBox2.Text
            TextBox4.Text = TextBox4.Text.PadRight(8)

            For i = 0 To 3
                If Mid(TextBox4.Text, i * 2 + 1, 2) = "  " Then
                    pData(i) = CByte(0)
                Else
                    Try
                        pData(i) = CByte(CInt("&H" + Mid(TextBox4.Text, i * 2 + 1, 2)))
                    Catch ex As Exception
                        TextBox2.Text = "Exception error. Input 16 bytes Hex value." + ControlChars.NewLine + TextBox2.Text
                        SysSetBuzzerVolume(B_WARNING, BUZZERVOLUME_MAX)
                        SysPlayBuzzer(B_WARNING, B_USERDEF, B_USERDEF)

                        NFCMifareClose()
                        Button1.Enabled = True
                        Exit Sub
                    End Try

                End If
                'pData(i) = CByte(CInt("&H" + Mid(TextBox4.Text, i * 2 + 1, 2)))
            Next

            iRet = NFCMifareWrite4(CInt(TextBox3.Text.ToString), pData, 0)

            If iRet <> NFC_OK Then
                strCommand = "NFCMifareWrite4"
                DispErrorMessage(iRet, strCommand)
                Exit Sub
            End If

            TextBox2.Text = "NFCMifareWrite4...OK." + ControlChars.NewLine + TextBox2.Text

        End If



        TextBox2.Text = "NFCMifareRadioOff...start." + ControlChars.NewLine + TextBox2.Text

        ' NFCMifare RADIO OFF
        iRet = NFCMifareRadioOff()

        If iRet <> NFC_OK Then
            strCommand = "NFCMifareRadioOff"
            DispErrorMessage(iRet, strCommand)
            Exit Sub
        End If
        TextBox2.Text = "NFCMifareRadioOff...OK." + ControlChars.NewLine + TextBox2.Text

        ' NFC CLOSE
        'NFCMifareClose()

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

        'NFCMifareClose()
        Button1.Enabled = True
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ShowTaskbar()
        Me.Close()
    End Sub

    Private Sub NFCMifareMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HideTaskbar()
        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0

        ' 000001__	Modify
        ' NFC OPEN
        iRet = NFCMifareOpen(IntPtr.Zero)

        TextBox2.Text = "NFCMifareOpen...start." + ControlChars.NewLine + TextBox2.Text

        If iRet <> NFC_OK Then
            strCommand = "NFCMifareOpen"
            DispErrorMessage(iRet, strCommand)
            Exit Sub
        End If
        TextBox2.Text = "NFCMifareOpen...OK." + ControlChars.NewLine + TextBox2.Text
        ' __000001	Modify
    End Sub

    Private Sub NFCMifareMain_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        ' 000001__	Modify
        ' NFC CLOSE
        NFCMifareClose()
        ' __000001	Modify
    End Sub
End Class
