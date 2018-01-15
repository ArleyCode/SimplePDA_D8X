'***************************************************************************
'* <Description>
'* Form1.vb (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* OBR Library Sample program
'* -------------------------------------------------------------------------
'* <Language>
'* VB.NET
'* -------------------------------------------------------------------------
'* <Develop Environment>
'* VS 2005
'* -------------------------------------------------------------------------
'* <Target>
'* JPN : DT-5300CE / DT-X7 / DT-5200 / DT-X8
'* ENG : DT-X30CE / DT-X7 / DT-X11 / IT-600 / DT-X8
'* -------------------------------------------------------------------------
'* Copyright(C)2010 CASIO COMPUTER CO.,LTD. All rights reserved.
'* -------------------------------------------------------------------------
'* <History>
'* Version  Date            Company     Keyword     Comment
'* 1.0.0    2010.02.17      CASIO       000000      Original      
'* 
'***************************************************************************/

Imports Calib.OBReadLibNet.Api
Imports Calib.OBReadLibNet.Def
Imports Calib.SystemLibNet.Api
Imports Calib.SystemLibNet.Def

Public Class Form1
    Inherits System.Windows.Forms.Form
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label

#Region " Windows Form Designer generated code "

    Public Sub New()

        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(16, 8)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(208, 88)
        Me.TextBox1.TabIndex = 6
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(120, 120)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(104, 23)
        Me.TextBox2.TabIndex = 5
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(120, 152)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(104, 23)
        Me.TextBox3.TabIndex = 4
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(120, 184)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(104, 23)
        Me.TextBox4.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 120)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 16)
        Me.Label1.Text = "Barcode type"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 152)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 16)
        Me.Label2.Text = "Data length"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 184)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.Text = "Terminate code"
        '
        'Form1
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.ClientSize = New System.Drawing.Size(242, 271)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "Form1"
        Me.Text = "OBRLibSampleVB"
        Me.ResumeLayout(False)

    End Sub

    Public Shared Sub Main()
        Application.Run(New Form1())
    End Sub

#End Region

    Declare Function GetForegroundWindow Lib "coredll" () As IntPtr
    Dim HWND As IntPtr
    Dim thread As System.Threading.Thread

    'Barcode type
    Dim DecodeNum() As Integer = New Integer() { _
           OBR_NONDT, _
           OBR_CD39, _
           OBR_NW_7, _
           OBR_WPCA, _
           OBR_WPC, _
           OBR_UPEA, _
           OBR_UPE, _
           OBR_IDF, _
           OBR_ITF, _
           OBR_CD93, _
           OBR_CD128, _
           OBR_MSI, _
           OBR_IATA}

    Dim DecodeName() As String = New String() { _
           "          ", _
           "OBR_CD39  ", _
           "OBR_NW_7  ", _
           "OBR_WPCA  ", _
           "OBR_WPC   ", _
           "OBR_UPEA  ", _
           "OBR_UPE   ", _
           "OBR_IDF   ", _
           "OBR_ITF   ", _
           "OBR_CD93  ", _
           "OBR_CD128 ", _
           "OBR_MSI   ", _
           "OBR_IATA  "}



    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim iRet As Integer

        HWND = GetForegroundWindow()
        iRet = OBRLoadConfigFile()              'ini File read default value set
        iRet = OBRSetDefaultSymbology()         '1D(OBR) driver mode will be ini File vallue
        iRet = OBRSetScanningKey(OBR_TRIGGERKEY_L Or OBR_TRIGGERKEY_R Or OBR_CENTERTRIGGER)
        iRet = OBRSetScanningCode(OBR_ALL)
        iRet = OBRSetBuffType(OBR_BUFOBR)   '1D(OBR) driver mode will be OBR_BUFOBR
        iRet = OBRSetScanningNotification(OBR_EVENT, IntPtr.Zero) '1D(OBR) driver mode will be OBR_EVENT
        iRet = OBROpen(HWND, 0)                 'OBRDRV open

        If (iRet = OBR_ERROR_INVALID_ACCESS) Then
            MessageBox.Show("Failed to connect to the scanner. Please exit a scanner application.", "OBRLibSampleCS")
            Exit Sub
        End If

        If (iRet = OBR_ERROR_HOTKEY) Then
            MessageBox.Show("Trigger keys are being used. Please quit the program which is using the trigger keys.", "OBRLibSampleCS")
            Exit Sub
        End If

        If (iRet <> OBR_OK) Then
            MessageBox.Show("Failed to connect to the scanner.", "OBRLibSampleCS")
            Exit Sub
        End If

        iRet = OBRClearBuff()
        thread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf start))
        thread.Start()    'Start start thread
    End Sub

    Delegate Sub SetLabelText()

    Private Sub SetText()
        Dim leng As Integer    'digit number
        Dim leng2 As Byte      'digit number
        Dim dwrcd As Integer    'barcode type
        Dim ret As Integer
        Dim lcnt As Byte
        Dim buff(1024) As Byte
        Dim str As String
        Dim i As Integer


        If (IntPtr.op_Inequality(HWND, IntPtr.Zero)) Then
            'check OBRBuffer state
            ret = OBRGetStatus(leng, lcnt)
            If (leng <> 0) Then
                ' get OBRBuffer data
                ret = OBRGets(buff, dwrcd, leng2)
                Dim ASCII As System.Text.Encoding
                ASCII = System.Text.Encoding.GetEncoding("ascii")
                TextBox1.Text = ASCII.GetString(buff, 0, leng2)    'scan barcode type display

                str = "----------"
                For i = 0 To 12
                    If (DecodeNum(i) = dwrcd) Then
                        str = DecodeName(i)
                        Exit For
                    End If
                Next

                TextBox2.Text = str                 'scan barcode type display
                TextBox3.Text = leng2.ToString()    'digit number display
                TextBox4.Text = ret.ToString()      'end information display
            End If
            OBRClearBuff()
        End If

    End Sub

    Private Sub start()
        While (True)
            SysWaitForEvent(IntPtr.Zero, OBR_NAME_EVENT, INFINITE)    'Wait event
            Invoke(New SetLabelText(AddressOf SetText))    'Display OBRBuffer data
        End While

    End Sub

    Private Sub Form1_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        OBRClose()  'OBRDRV Close
        SysTerminateWaitEvent() 'End SysWaitForEvent function
        HWND = IntPtr.Zero
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        thread.Abort()                                 'Abort start thread
    End Sub
End Class
