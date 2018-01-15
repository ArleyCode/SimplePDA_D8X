'=============================================================================
' CASIO Mobile Device SystemLib Sample                                       +
' Copyright (C) 2005 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
'=============================================================================
' Form_Vibrator.vb : vibration sample                                          +
'-----------------------------------------------------------------------------
Public Class Form_Vibrator
    Inherits System.Windows.Forms.Form
    Friend WithEvents comboBox_Type As System.Windows.Forms.ComboBox
    Friend WithEvents textBox_OffTime As System.Windows.Forms.TextBox
    Friend WithEvents textBox_OnTime As System.Windows.Forms.TextBox
    Friend WithEvents textBox_Num As System.Windows.Forms.TextBox
    Friend WithEvents checkBox_Mute As System.Windows.Forms.CheckBox
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents button_Stop As System.Windows.Forms.Button
    Friend WithEvents button_Play As System.Windows.Forms.Button

    Private CalibDef As Calib.SystemLibNet.Def
    Private CalibApi As Calib.SystemLibNet.Api

    Private Const dwTypeNum As Int32 = 6
    Private dwTypeList(dwTypeNum) As Int32
    Private OldSetting(dwTypeNum) As Boolean


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
        Me.comboBox_Type = New System.Windows.Forms.ComboBox
        Me.textBox_OffTime = New System.Windows.Forms.TextBox
        Me.textBox_OnTime = New System.Windows.Forms.TextBox
        Me.textBox_Num = New System.Windows.Forms.TextBox
        Me.checkBox_Mute = New System.Windows.Forms.CheckBox
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.button_Stop = New System.Windows.Forms.Button
        Me.button_Play = New System.Windows.Forms.Button
        '
        'comboBox_Type
        '
        Me.comboBox_Type.Location = New System.Drawing.Point(8, 8)
        Me.comboBox_Type.Size = New System.Drawing.Size(216, 20)
        '
        'textBox_OffTime
        '
        Me.textBox_OffTime.Location = New System.Drawing.Point(160, 128)
        Me.textBox_OffTime.Size = New System.Drawing.Size(64, 19)
        Me.textBox_OffTime.Text = ""
        '
        'textBox_OnTime
        '
        Me.textBox_OnTime.Location = New System.Drawing.Point(160, 96)
        Me.textBox_OnTime.Size = New System.Drawing.Size(64, 19)
        Me.textBox_OnTime.Text = ""
        '
        'textBox_Num
        '
        Me.textBox_Num.Location = New System.Drawing.Point(160, 64)
        Me.textBox_Num.Size = New System.Drawing.Size(64, 19)
        Me.textBox_Num.Text = ""
        '
        'checkBox_Mute
        '
        Me.checkBox_Mute.Location = New System.Drawing.Point(8, 40)
        Me.checkBox_Mute.Text = "Mute"
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(8, 128)
        Me.label3.Size = New System.Drawing.Size(104, 16)
        Me.label3.Text = "Vibrator OFF Time"
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(8, 96)
        Me.label2.Size = New System.Drawing.Size(112, 16)
        Me.label2.Text = "Vibrator ON Time"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(8, 64)
        Me.label1.Size = New System.Drawing.Size(120, 16)
        Me.label1.Text = "Vibrator Count"
        '
        'button_Stop
        '
        Me.button_Stop.Location = New System.Drawing.Point(120, 160)
        Me.button_Stop.Size = New System.Drawing.Size(104, 24)
        Me.button_Stop.Text = "Stop"
        '
        'button_Play
        '
        Me.button_Play.Location = New System.Drawing.Point(8, 160)
        Me.button_Play.Size = New System.Drawing.Size(104, 24)
        Me.button_Play.Text = "Play"
        '
        'Form_Vibrator
        '
        Me.ClientSize = New System.Drawing.Size(236, 234)
        Me.Controls.Add(Me.button_Stop)
        Me.Controls.Add(Me.button_Play)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.checkBox_Mute)
        Me.Controls.Add(Me.textBox_OffTime)
        Me.Controls.Add(Me.textBox_OnTime)
        Me.Controls.Add(Me.textBox_Num)
        Me.Controls.Add(Me.comboBox_Type)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Text = "Form_Vibrator"

    End Sub

#End Region

    Private Sub button_Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Play.Click
        Dim dwRet As Int32
        Dim nSelType As Int32
        Dim dwNum As Int32 = 0
        Dim dwOnTime As Int32 = 0
        Dim dwOffTime As Int32 = 0

        nSelType = comboBox_Type.SelectedIndex
        If (dwTypeList(nSelType) = CalibDef.B_USERDEF) Then
            ' get count
            dwNum = Convert.ToInt32(textBox_Num.Text)

            ' get vibration on time
            dwOnTime = Convert.ToInt32(textBox_OnTime.Text)

            ' get vibration off time
            dwOffTime = Convert.ToInt32(textBox_OffTime.Text)
        End If

        ' Play
        dwRet = CalibApi.SysPlayVibrator(dwTypeList(nSelType), dwNum, dwOnTime, dwOffTime)
        ShowErrorMessage("SysPlayVibrator", dwRet)
    End Sub

    Private Sub button_Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Stop.Click
        ' Stop
        CalibApi.SysStopVibrator()
    End Sub

    Private Sub Form_Vibrator_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' vibration type combobox initialize
        comboBox_Type.Items.Add("Alarm")
        dwTypeList(0) = CalibDef.B_ALARM

        comboBox_Type.Items.Add("Warning")
        dwTypeList(1) = CalibDef.B_WARNING

        comboBox_Type.Items.Add("Scanner")
        dwTypeList(2) = CalibDef.B_SCANEND

        comboBox_Type.Items.Add("Wireless read")
        dwTypeList(3) = CalibDef.B_WIREREAD

        comboBox_Type.Items.Add("User Define")
        dwTypeList(4) = CalibDef.B_USERDEF

        comboBox_Type.Items.Add("All Vibrator")
        dwTypeList(5) = CalibDef.B_ALL

        ' get vibration setting
        GetVibratorSetting()

        comboBox_Type.SelectedIndex = 0

    End Sub

    Private Sub GetVibratorSetting()
        Dim dwMute As Int32
        Dim i As Int32

        For i = 0 To dwTypeNum
            ' get mute setting
            dwMute = CalibApi.SysGetVibratorMute(dwTypeList(i))
            ShowErrorMessage("SysGetVibratorMute", dwMute)
            If (dwMute <> 0) Then
                OldSetting(i) = True
            Else
                OldSetting(i) = False
            End If
        Next
    End Sub

    Private Sub comboBox_Type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboBox_Type.SelectedIndexChanged
        Dim nSelType As Int32 = comboBox_Type.SelectedIndex

        ' get mute setting
        If (CalibApi.SysGetVibratorMute(dwTypeList(nSelType)) <> 0) Then
            checkBox_Mute.Checked = True
        Else
            checkBox_Mute.Checked = False
        End If

        ' judge selected vibration type is user setting or not
        If (dwTypeList(nSelType) = CalibDef.B_USERDEF) Then
            ' vibration count
            textBox_Num.Text = "2"
            textBox_Num.Enabled = True

            ' vibration on time
            textBox_OnTime.Text = "100"
            textBox_OnTime.Enabled = True

            ' vibration off time
            textBox_OffTime.Text = "100"
            textBox_OffTime.Enabled = True

        Else
            ' disable vibration count
            textBox_Num.Text = ""
            textBox_Num.Enabled = False

            ' disable vibration on time
            textBox_OnTime.Text = ""
            textBox_OnTime.Enabled = False

            ' disable vibration off time
            textBox_OffTime.Text = ""
            textBox_OffTime.Enabled = False
        End If

        ' judge selected vibration type is full vibration or not
        If (dwTypeList(nSelType) = CalibDef.B_ALL) Then
            button_Play.Enabled = False
        Else
            button_Play.Enabled = True
        End If

    End Sub

    Private Sub checkBox_Mute_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkBox_Mute.CheckStateChanged
        Dim dwRet As Int32
        Dim nSelType As Int32 = comboBox_Type.SelectedIndex


        ' change mute setting
        dwRet = CalibApi.SysSetVibratorMute(dwTypeList(nSelType), checkBox_Mute.Checked)
        ShowErrorMessage("SysSetVibratorMute", dwRet)
    End Sub

    Protected Overridable Sub Form_Vibrator_OnClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Dim dwRet As Int32
        Dim i As Int32

        ' return before setting
        For i = 0 To dwTypeNum

            ' change vibration setting
            dwRet = CalibApi.SysSetVibratorMute(dwTypeList(i), OldSetting(i))
            ShowErrorMessage("SysGetVibratorMute", dwRet)
        Next

    End Sub
End Class
