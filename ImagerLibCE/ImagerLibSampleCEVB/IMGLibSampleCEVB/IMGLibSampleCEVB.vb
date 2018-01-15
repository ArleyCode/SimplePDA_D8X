'***************************************************************************
'* <Description>
'* IMGLibSampleCEVB.vb (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* Image Scanner Barcode Capture Demo program
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

Public Class IMGLibSampleCEVB
    Dim IMG As New Calib.IMGLibNet.Api
    Dim IMGDef As New Calib.IMGLibNet.Def
    Dim pMessage As String
    Dim pCodeID As String
    Dim pAimID As String
    Dim pSymModifier As String
    Dim pLength As Int32
    Dim pError As Int32

    Private Sub IMGLibSampleCEVB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' ini File default pass
        Dim pFileName As String = "\FlashDisk\System Settings\IMGSet.ini"

        Calib.IMGLibNet.Api.IMGInit()                     ' IMGDRV open
        Calib.IMGLibNet.Api.IMGLoadConfigFile(pFileName)  ' ini File read default value set
        Calib.IMGLibNet.Api.IMGConnect()                  ' IMGDRV mode will be ini File vallue 

        TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Dim i As Int32
        pMessage = ""
        For i = 0 To 511
            pMessage = pMessage & " "
        Next
        pCodeID = "    "
        pAimID = "  "
        pSymModifier = "  "
        pLength = 0
        pError = Calib.IMGLibNet.Def.IMG_ERR_DRIVER

        'if trigger key press
        If e.KeyCode = Keys.F24 Then
            'If e.KeyCode = 234 Or e.KeyCode = 230 Or e.KeyCode = 233 Then
            ' read symbol
            pError = Calib.IMGLibNet.Api.IMGWaitForDecode(5000, pMessage, pCodeID, pAimID, pSymModifier, pLength, IntPtr.Zero)
            If pError = Calib.IMGLibNet.Def.IMG_SUCCESS Then
                TextBox1.Text = pMessage  ' symbol data display
                SymbolDisp()              ' symbol kind display
            End If
        End If
    End Sub

    Private Sub IMGLibSampleCEVB_Closing(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        ' IMGDRV Close
        Calib.IMGLibNet.Api.IMGDisconnect()
        Calib.IMGLibNet.Api.IMGDeinit()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()

    End Sub
    ' search symbol kind
    Private Sub SymbolDisp()
        Select Case pCodeID
            Case "A   "
                Label1.Text = "Australian Postal"
            Case "z   "
                Label1.Text = "Aztec"
            Case "B   "
                Label1.Text = "British Postal"
            Case "C   "
                Label1.Text = "Canadian Postal"
            Case "a   "
                Label1.Text = "Codabar"
            Case "q   "
                Label1.Text = "Codablock F"
            Case "h   "
                Label1.Text = "Code 11"
            Case "j   "
                Label1.Text = "Code 128 or ISBT"
            Case "b   "
                Label1.Text = "Code 39"
            Case "l   "
                Label1.Text = "Code 49 or EAN 128"
            Case "i   "
                Label1.Text = "Code 93"
            Case "y   "
                Label1.Text = "RSS or UCC/EAN Composite"
            Case "w   "
                Label1.Text = "DataMatrix"
            Case "K   "
                Label1.Text = "Dutch Postal"
            Case "d   "
                Label1.Text = "EAN13"
            Case "D   "
                Label1.Text = "EAN8"
            Case "f   "
                Label1.Text = "IATA 2of5"
            Case "e   "
                Label1.Text = "ITF(Interleaved 2of5)"
            Case "J   "
                Label1.Text = "Japanese Postal"
            Case "x   "
                Label1.Text = "Maxicode"
            Case "R   "
                Label1.Text = "MicroPDF"
            Case "g   "
                Label1.Text = "MSI"
            Case "r   "
                Label1.Text = "PDF417"
            Case "L   "
                Label1.Text = "Planet Code"
            Case "P   "
                Label1.Text = "Postnet"
            Case "O   "
                Label1.Text = "OCR"
            Case "s   "
                Label1.Text = "QR"
            Case "T   "
                Label1.Text = "TLC39"
            Case "c   "
                Label1.Text = "UPC Version A"
            Case "E   "
                Label1.Text = "UPC Version E0,E1"

            Case Else
                Label1.Text = "Unknown Symbol"
        End Select
    End Sub
End Class
