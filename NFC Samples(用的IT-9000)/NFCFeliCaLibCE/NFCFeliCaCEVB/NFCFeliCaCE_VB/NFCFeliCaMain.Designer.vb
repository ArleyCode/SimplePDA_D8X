<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class NFCFeliCaMain
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
        Me.Label11 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TextBox4 = New System.Windows.Forms.TextBox
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.TextBox3 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(0, 178)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(70, 15)
        Me.Label11.Text = "Data (Hex)"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(77, 154)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(47, 23)
        Me.TextBox1.TabIndex = 107
        Me.TextBox1.Text = "1009"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(123, 155)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(36, 19)
        Me.Label10.Text = "(Hex)"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(0, 136)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 15)
        Me.Label9.Text = "Command"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(159, 136)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 15)
        Me.Label8.Text = "BlockNumber"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(76, 136)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 15)
        Me.Label5.Text = "ServiceCode"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(0, 196)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(237, 40)
        Me.TextBox4.TabIndex = 114
        '
        'ComboBox1
        '
        Me.ComboBox1.Items.Add("READ")
        Me.ComboBox1.Items.Add("WRITE")
        Me.ComboBox1.Location = New System.Drawing.Point(0, 154)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(74, 23)
        Me.ComboBox1.TabIndex = 113
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(164, 154)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(70, 23)
        Me.TextBox3.TabIndex = 108
        Me.TextBox3.Text = "1"
        '
        'TextBox2
        '
        Me.TextBox2.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.TextBox2.ForeColor = System.Drawing.Color.Black
        Me.TextBox2.Location = New System.Drawing.Point(0, 237)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox2.Size = New System.Drawing.Size(238, 56)
        Me.TextBox2.TabIndex = 106
        Me.TextBox2.TabStop = False
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(77, 114)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(158, 21)
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(0, 99)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(166, 20)
        Me.Label6.Text = "manufacture Parameter :"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(106, 20)
        Me.Label4.Text = "manufacture ID :"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(77, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(158, 21)
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(96, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(123, 19)
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 20)
        Me.Label1.Text = "System Code :"
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Button2.Location = New System.Drawing.Point(164, 1)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(74, 40)
        Me.Button2.TabIndex = 105
        Me.Button2.Text = "Exit"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Button1.Location = New System.Drawing.Point(0, 1)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(162, 40)
        Me.Button1.TabIndex = 104
        Me.Button1.Text = "NFC Start"
        '
        'NFCFeliCaMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(238, 295)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "NFCFeliCaMain"
        Me.Text = "NFCFeliCaCE_VB"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
