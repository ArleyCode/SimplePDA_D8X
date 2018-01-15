<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class NFCMifareMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TextBox5 = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ComboBox2
        '
        Me.ComboBox2.Items.Add("KEY_A")
        Me.ComboBox2.Items.Add("KEY_B")
        Me.ComboBox2.Location = New System.Drawing.Point(125, 113)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(60, 23)
        Me.ComboBox2.TabIndex = 142
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label5.Location = New System.Drawing.Point(188, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 15)
        Me.Label5.Text = "Block No."
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(191, 113)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(47, 23)
        Me.TextBox1.TabIndex = 141
        Me.TextBox1.Text = "1"
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(0, 113)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(122, 23)
        Me.TextBox5.TabIndex = 140
        Me.TextBox5.Text = "A0A1A2A3A4A5"
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label15.Location = New System.Drawing.Point(0, 98)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(125, 15)
        Me.Label15.Text = "Authentication Key (Hex)"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(176, 40)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(59, 20)
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(121, 41)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(51, 20)
        Me.Label12.Text = "UidLen :"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(50, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(127, 21)
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(2, 179)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 15)
        Me.Label11.Text = "Data (Hex)"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(0, 138)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 15)
        Me.Label9.Text = "Command"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(122, 138)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 15)
        Me.Label8.Text = "Block No."
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(0, 195)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(238, 40)
        Me.TextBox4.TabIndex = 139
        '
        'ComboBox1
        '
        Me.ComboBox1.Items.Add("READ")
        Me.ComboBox1.Items.Add("WRITE_Standard")
        Me.ComboBox1.Items.Add("WRITE_Ultralight")
        Me.ComboBox1.Location = New System.Drawing.Point(0, 155)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(118, 23)
        Me.ComboBox1.TabIndex = 138
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(122, 155)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(55, 23)
        Me.TextBox3.TabIndex = 137
        Me.TextBox3.Text = "1"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.TextBox2.Location = New System.Drawing.Point(0, 237)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox2.Size = New System.Drawing.Size(238, 58)
        Me.TextBox2.TabIndex = 136
        Me.TextBox2.TabStop = False
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(50, 78)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(158, 21)
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(2, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(51, 20)
        Me.Label6.Text = "Uid :"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(2, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 20)
        Me.Label4.Text = "SAK :"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(50, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 19)
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(2, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 20)
        Me.Label1.Text = "ATQ :"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Button2.Location = New System.Drawing.Point(168, 0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(68, 40)
        Me.Button2.TabIndex = 135
        Me.Button2.Text = "Exit"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Button1.Location = New System.Drawing.Point(0, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(159, 40)
        Me.Button1.TabIndex = 134
        Me.Button1.Text = "NFC Start"
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label14.Location = New System.Drawing.Point(126, 98)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(26, 14)
        Me.Label14.Text = "Key"
        '
        'NFCMifareMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(238, 295)
        Me.ControlBox = False
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label14)
        Me.Name = "NFCMifareMain"
        Me.Text = "NFCMifareCE_VB"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label

End Class
