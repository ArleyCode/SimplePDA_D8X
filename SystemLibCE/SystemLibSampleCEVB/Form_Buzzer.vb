'=============================================================================
' CASIO Mobile Device SystemLib Sample                                       +
' Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
'=============================================================================
' Form_BUZZER.vb : Buzzer sample                                            +
'-----------------------------------------------------------------------------
Public Class Form_Buzzer
    Inherits System.Windows.Forms.Form
    Friend WithEvents comboBox_Volume As System.Windows.Forms.ComboBox
    Friend WithEvents comboBox_Type As System.Windows.Forms.ComboBox
    Friend WithEvents checkBox_Mute As System.Windows.Forms.CheckBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents button_Stop As System.Windows.Forms.Button
    Friend WithEvents button_Play As System.Windows.Forms.Button


    Private CalibDef As Calib.SystemLibNet.Def
    Private CalibApi As Calib.SystemLibNet.Api

    ' structure definition
    Private Structure tagBUZZERSETTING
        Public bMute As Boolean
        Public dwVol As Int32
    End Structure

    'MAX number
    Private Const TYPE_MAXNUM As Int32 = 6

    'Type List
    Private dwTypeList(TYPE_MAXNUM) As Int32

    ' setting before change
    Private OldSetting(TYPE_MAXNUM) As tagBUZZERSETTING

    ' setting after change
    Private NewSetting(TYPE_MAXNUM) As tagBUZZERSETTING


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
        Me.comboBox_Volume = New System.Windows.Forms.ComboBox
        Me.comboBox_Type = New System.Windows.Forms.ComboBox
        Me.checkBox_Mute = New System.Windows.Forms.CheckBox
        Me.label1 = New System.Windows.Forms.Label
        Me.button_Stop = New System.Windows.Forms.Button
        Me.button_Play = New System.Windows.Forms.Button
        '
        'comboBox_Volume
        '
        Me.comboBox_Volume.Items.Add("Min")
        Me.comboBox_Volume.Items.Add("Middle")
        Me.comboBox_Volume.Items.Add("Max")
        Me.comboBox_Volume.Location = New System.Drawing.Point(128, 64)
        Me.comboBox_Volume.Size = New System.Drawing.Size(96, 20)
        '
        'comboBox_Type
        '
        Me.comboBox_Type.Location = New System.Drawing.Point(16, 8)
        Me.comboBox_Type.Size = New System.Drawing.Size(208, 20)
        '
        'checkBox_Mute
        '
        Me.checkBox_Mute.Location = New System.Drawing.Point(16, 40)
        Me.checkBox_Mute.Text = "Mute"
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(16, 72)
        Me.label1.Size = New System.Drawing.Size(48, 16)
        Me.label1.Text = "Volume"
        '
        'button_Stop
        '
        Me.button_Stop.Location = New System.Drawing.Point(127, 106)
        Me.button_Stop.Size = New System.Drawing.Size(104, 24)
        Me.button_Stop.Text = "Stop"
        '
        'button_Play
        '
        Me.button_Play.Location = New System.Drawing.Point(7, 106)
        Me.button_Play.Size = New System.Drawing.Size(104, 24)
        Me.button_Play.Text = "Play"
        '
        'Form_Buzzer
        '
        Me.ClientSize = New System.Drawing.Size(238, 236)
        Me.Controls.Add(Me.button_Stop)
        Me.Controls.Add(Me.button_Play)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.checkBox_Mute)
        Me.Controls.Add(Me.comboBox_Volume)
        Me.Controls.Add(Me.comboBox_Type)
        Me.Text = "Form_Buzzer"

    End Sub

#End Region

    Private Sub button_Play_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Play.Click
        Dim nSelType As Int32, dwRet As Int32

        nSelType = comboBox_Type.SelectedIndex

        ' Play
        dwRet = CalibApi.SysPlayBuzzer(dwTypeList(nSelType), 0, 0)
        CommonFunc.ShowErrorMessage("SysPlayBuzzer", dwRet)
    End Sub

    Private Sub button_Stop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_Stop.Click
        Dim dwRet As Int32

        ' Stop
        dwRet = CalibApi.SysStopBuzzer()
        CommonFunc.ShowErrorMessage("SysStopBuzzer", dwRet)
    End Sub

    Private Sub Form_Buzzer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' buzzer type combo box initialize
        comboBox_Type.Items.Add("Click")
        dwTypeList(0) = CalibDef.B_CLICK

        comboBox_Type.Items.Add("Alarm")
        dwTypeList(1) = CalibDef.B_ALARM

        comboBox_Type.Items.Add("Warning")
        dwTypeList(2) = CalibDef.B_WARNING

        comboBox_Type.Items.Add("Scanner")
        dwTypeList(3) = CalibDef.B_SCANEND

        comboBox_Type.Items.Add("Tap")
        dwTypeList(4) = CalibDef.B_TAP

        comboBox_Type.Items.Add("All Buzzer")
        dwTypeList(5) = CalibDef.B_ALL

        ' buzzer setting get
        GetBuzzerSetting()

        comboBox_Type.SelectedIndex = 0

    End Sub

    Private Sub GetBuzzerSetting()
        Dim dwMute As Int32
        Dim dwVol As Int32

        Dim i As Int32

        For i = 0 To TYPE_MAXNUM
            ' mute setting get
            dwMute = CalibApi.SysGetBuzzerMute(dwTypeList(i))
            ShowErrorMessage("SysGetBuzzerMute", dwMute)
            If (dwMute <> 0) Then
                NewSetting(i).bMute = True
                OldSetting(i).bMute = True
            Else
                NewSetting(i).bMute = False
                OldSetting(i).bMute = False
            End If

            ' volume setting get
            dwVol = CalibApi.SysGetBuzzerVolume(dwTypeList(i))
            ShowErrorMessage("SysGetBuzzerVolume", dwVol)

            If (dwVol = CalibDef.BUZZERVOLUME_MIN) Then
                NewSetting(i).dwVol = 0
                OldSetting(i).dwVol = 0
            ElseIf (dwVol = CalibDef.BUZZERVOLUME_MID) Then
                NewSetting(i).dwVol = 1
                OldSetting(i).dwVol = 1
            Else
                NewSetting(i).dwVol = 2
                OldSetting(i).dwVol = 2
            End If
        Next i

    End Sub

    Private Sub comboBox_Type_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboBox_Type.SelectedIndexChanged
        Dim nNewSel As Int32

        nNewSel = comboBox_Type.SelectedIndex

        ' change mute setting
        If (NewSetting(nNewSel).bMute = True) Then
            checkBox_Mute.Checked = True
        Else
            checkBox_Mute.Checked = False
        End If

        ' change sound setting
        comboBox_Volume.SelectedIndex = NewSetting(nNewSel).dwVol


        ' judge selected buzzer type is max sound or not
        If (dwTypeList(nNewSel) = CalibDef.B_ALL) Then
            comboBox_Volume.Enabled = False
            button_Play.Enabled = False
        Else
            comboBox_Volume.Enabled = True
            button_Play.Enabled = True
        End If
    End Sub

    Private Sub checkBox_Mute_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkBox_Mute.CheckStateChanged
        Dim dwRet As Int32
        Dim nSel As Int32

        nSel = comboBox_Type.SelectedIndex

        NewSetting(nSel).bMute = checkBox_Mute.Checked

        ' change mute setting
        dwRet = CalibApi.SysSetBuzzerMute(dwTypeList(nSel), NewSetting(nSel).bMute)
        CommonFunc.ShowErrorMessage("SysSetBuzzerMute", dwRet)
    End Sub

    Private Sub comboBox_Volume_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboBox_Volume.SelectedIndexChanged
        Dim dwRet As Int32
        Dim dwVol As Int32
        Dim nSel As Int32

        nSel = comboBox_Type.SelectedIndex

        NewSetting(nSel).dwVol = comboBox_Volume.SelectedIndex

        ' change sound setting
        If (NewSetting(nSel).dwVol = 0) Then
            dwVol = CalibDef.BUZZERVOLUME_MIN
        ElseIf (NewSetting(nSel).dwVol = 1) Then
            dwVol = CalibDef.BUZZERVOLUME_MID
        Else
            dwVol = CalibDef.BUZZERVOLUME_MAX
        End If

        dwRet = CalibApi.SysSetBuzzerVolume(dwTypeList(nSel), dwVol)
        ShowErrorMessage("SysSetBuzzerVolume", dwRet)
    End Sub

    Protected Overridable Sub Form_Buzzer_OnClosed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Dim dwRet As Int32
        Dim dwVol As Int32
        Dim i As Int32

        ' return to before setting
        For i = 0 To TYPE_MAXNUM

            ' change mute setting
            dwRet = CalibApi.SysSetBuzzerMute(dwTypeList(i), OldSetting(i).bMute)
            ShowErrorMessage("SysSetBuzzerMute", dwRet)

            ' change sound setting
            If (OldSetting(i).dwVol = 0) Then
                dwVol = CalibDef.BUZZERVOLUME_MIN
            ElseIf (OldSetting(i).dwVol = 1) Then
                dwVol = CalibDef.BUZZERVOLUME_MID
            Else
                dwVol = CalibDef.BUZZERVOLUME_MAX
            End If
            dwRet = CalibApi.SysSetBuzzerVolume(dwTypeList(i), dwVol)
            ShowErrorMessage("SysSetBuzzerVolume", dwRet)
        Next

    End Sub
End Class
