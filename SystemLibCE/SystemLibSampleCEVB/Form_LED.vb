'=============================================================================
' CASIO Mobile Device SystemLib Sample                                       +
' Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
'=============================================================================
' Form_LED.vb : LED sample                                                        +
'-----------------------------------------------------------------------------
Public Class Form_LED
    Inherits System.Windows.Forms.Form
    Friend WithEvents textBox_Num As System.Windows.Forms.TextBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents textBox_OffTime As System.Windows.Forms.TextBox
    Friend WithEvents textBox_OnTime As System.Windows.Forms.TextBox
    Friend WithEvents button_Get As System.Windows.Forms.Button
    Friend WithEvents button_Off As System.Windows.Forms.Button
    Friend WithEvents button_Magenta As System.Windows.Forms.Button
    Friend WithEvents button_Cyan As System.Windows.Forms.Button
    Friend WithEvents button_Blue As System.Windows.Forms.Button
    Friend WithEvents button_Orange As System.Windows.Forms.Button
    Friend WithEvents button_Red As System.Windows.Forms.Button
    Friend WithEvents button_Green As System.Windows.Forms.Button


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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.textBox_Num = New System.Windows.Forms.TextBox
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.textBox_OffTime = New System.Windows.Forms.TextBox
        Me.textBox_OnTime = New System.Windows.Forms.TextBox
        Me.button_Get = New System.Windows.Forms.Button
        Me.button_Off = New System.Windows.Forms.Button
        Me.button_Magenta = New System.Windows.Forms.Button
        Me.button_Cyan = New System.Windows.Forms.Button
        Me.button_Blue = New System.Windows.Forms.Button
        Me.button_Orange = New System.Windows.Forms.Button
        Me.button_Red = New System.Windows.Forms.Button
        Me.button_Green = New System.Windows.Forms.Button
        '
        'textBox_Num
        '
        Me.textBox_Num.Location = New System.Drawing.Point(144, 8)
        Me.textBox_Num.Size = New System.Drawing.Size(88, 19)
        Me.textBox_Num.Text = "1"
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(8, 64)
        Me.label3.Size = New System.Drawing.Size(88, 16)
        Me.label3.Text = "LED OFF time"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(8, 40)
        Me.label2.Size = New System.Drawing.Size(88, 16)
        Me.label2.Text = "LED ON time"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(8, 16)
        Me.label1.Size = New System.Drawing.Size(88, 16)
        Me.label1.Text = "LED ON count"
        '
        'textBox_OffTime
        '
        Me.textBox_OffTime.Location = New System.Drawing.Point(144, 56)
        Me.textBox_OffTime.Size = New System.Drawing.Size(88, 19)
        Me.textBox_OffTime.Text = "16"
        '
        'textBox_OnTime
        '
        Me.textBox_OnTime.Location = New System.Drawing.Point(144, 32)
        Me.textBox_OnTime.Size = New System.Drawing.Size(88, 19)
        Me.textBox_OnTime.Text = "16"
        '
        'button_Get
        '
        Me.button_Get.Location = New System.Drawing.Point(16, 200)
        Me.button_Get.Size = New System.Drawing.Size(208, 24)
        Me.button_Get.Text = "Get status"
        '
        'button_Off
        '
        Me.button_Off.Location = New System.Drawing.Point(16, 168)
        Me.button_Off.Size = New System.Drawing.Size(208, 24)
        Me.button_Off.Text = "LED OFF"
        '
        'button_Magenta
        '
        Me.button_Magenta.Location = New System.Drawing.Point(160, 128)
        Me.button_Magenta.Size = New System.Drawing.Size(64, 24)
        Me.button_Magenta.Text = "Magenta"
        '
        'button_Cyan
        '
        Me.button_Cyan.Location = New System.Drawing.Point(88, 128)
        Me.button_Cyan.Size = New System.Drawing.Size(64, 24)
        Me.button_Cyan.Text = "Cyan"
        '
        'button_Blue
        '
        Me.button_Blue.Location = New System.Drawing.Point(16, 128)
        Me.button_Blue.Size = New System.Drawing.Size(64, 24)
        Me.button_Blue.Text = "Blue"
        '
        'button_Orange
        '
        Me.button_Orange.Location = New System.Drawing.Point(160, 96)
        Me.button_Orange.Size = New System.Drawing.Size(64, 24)
        Me.button_Orange.Text = "Orange"
        '
        'button_Red
        '
        Me.button_Red.Location = New System.Drawing.Point(88, 96)
        Me.button_Red.Size = New System.Drawing.Size(64, 24)
        Me.button_Red.Text = "Red"
        '
        'button_Green
        '
        Me.button_Green.Location = New System.Drawing.Point(16, 96)
        Me.button_Green.Size = New System.Drawing.Size(64, 24)
        Me.button_Green.Text = "Green"
        '
        'Form_LED
        '
        Me.ClientSize = New System.Drawing.Size(240, 238)
        Me.Controls.Add(Me.button_Get)
        Me.Controls.Add(Me.button_Off)
        Me.Controls.Add(Me.button_Magenta)
        Me.Controls.Add(Me.button_Cyan)
        Me.Controls.Add(Me.button_Blue)
        Me.Controls.Add(Me.button_Orange)
        Me.Controls.Add(Me.button_Red)
        Me.Controls.Add(Me.button_Green)
        Me.Controls.Add(Me.textBox_OffTime)
        Me.Controls.Add(Me.textBox_OnTime)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.textBox_Num)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Text = "Form_LED"

    End Sub

#End Region

    Private Sub button_Green_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Green.Click
        SetLED(Calib.SystemLibNet.Def.LED_GREEN)
    End Sub

    Private Sub button_Red_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Red.Click
        SetLED(Calib.SystemLibNet.Def.LED_RED)
    End Sub

    Private Sub button_Orange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Orange.Click
        SetLED(Calib.SystemLibNet.Def.LED_ORANGE)
    End Sub

    Private Sub button_Blue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Blue.Click
        SetLED(&HB)
    End Sub

    Private Sub button_Cyan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Cyan.Click
        SetLED(&HC)
    End Sub

    Private Sub button_Magenta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Magenta.Click
        SetLED(&HD)
    End Sub

    Private Sub button_Off_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Off.Click
        SetLED(Calib.SystemLibNet.Def.LED_OFF)
    End Sub

    Private Sub button_Get_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Get.Click
        Dim dwRet As Int32

        ' get LED ON/OFF condition
        dwRet = Calib.SystemLibNet.Api.SysGetLED()
        Select Case dwRet
            Case Calib.SystemLibNet.Def.LED_GREEN
                MessageBox.Show("Green", "SysGetLED", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
            Case Calib.SystemLibNet.Def.LED_RED
                MessageBox.Show("Red", "SysGetLED", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
            Case Calib.SystemLibNet.Def.LED_ORANGE
                MessageBox.Show("Orange", "SysGetLED", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
            Case &HB
                MessageBox.Show("Blue", "SysGetLED", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
            Case &HC
                MessageBox.Show("Cyan", "SysGetLED", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
            Case &HD
                MessageBox.Show("Magenta", "SysGetLED", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
        End Select

    End Sub

    Private Sub SetLED(ByVal dwMode As Int32)
        Dim dwNum As Int32, dwOnTime As Int32, dwOffTime As Int32

        ' LED ON count get
        dwNum = Convert.ToInt32(textBox_Num.Text)

        ' LED ON time get
        dwOnTime = Convert.ToInt32(textBox_OnTime.Text)

        ' LED OFF time get
        dwOffTime = Convert.ToInt32(textBox_OffTime.Text)

        ' LED ON/OFF
        Calib.SystemLibNet.Api.SysSetLED(dwMode, dwNum, dwOnTime, dwOffTime)

    End Sub

    Protected Overridable Sub Form_LED_OnClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed

		' LED OFF then close dialog
        Calib.SystemLibNet.Api.SysSetLED(Calib.SystemLibNet.Def.LED_OFF, 0, 0, 0)
        button_Off_Click(sender, e)
    End Sub
End Class
