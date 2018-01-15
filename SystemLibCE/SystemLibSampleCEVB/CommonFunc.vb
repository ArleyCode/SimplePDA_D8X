'=============================================================================
' CASIO Mobile Device SystemLib Sample                                       +
' Copyright (C) 2005 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
'=============================================================================
' CommonFunc.vb : common function                                           +
'-----------------------------------------------------------------------------
Module CommonFunc
    Public Sub ShowErrorMessage(ByVal strCaption As String, ByVal dwStatus As Int32)
        Dim strTitle As String
        Dim strMsg As String

        If (dwStatus = Calib.SystemLibNet.Def.SYS_PARAMERR) Then
            strMsg = "The invalid parameter was set up."

        ElseIf (dwStatus = Calib.SystemLibNet.Def.FUNCTION_UNSUPPORT) Then
            strMsg = "Function unsupport"

        Else
            Return
        End If

        If (strCaption.Length > 0) Then
            strTitle = strCaption

        Else
            strTitle = "SystemLibSampleVB"
        End If
        MessageBox.Show(strMsg, strTitle, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
    End Sub

End Module
