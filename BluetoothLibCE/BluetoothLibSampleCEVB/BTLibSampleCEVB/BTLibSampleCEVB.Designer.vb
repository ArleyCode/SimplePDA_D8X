<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class BTLibSampleCEVB
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
        Me.components = New System.ComponentModel.Container
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Button5 = New System.Windows.Forms.Button
        Me.SerialPort1 = New System.IO.Ports.SerialPort(Me.components)
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Status = New System.Windows.Forms.TextBox
        Me.ListView1 = New System.Windows.Forms.ListView
        Me.SuspendLayout()
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(123, 218)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(115, 30)
        Me.Button3.TabIndex = 37
        Me.Button3.Text = "Exit"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(14, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(208, 24)
        Me.Label1.Text = "Bluetooth Devices list"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(-2, 193)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(240, 23)
        Me.TextBox1.TabIndex = 31
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(123, 158)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(115, 30)
        Me.Button5.TabIndex = 32
        Me.Button5.Text = "Set Default device"
        '
        'SerialPort1
        '
        Me.SerialPort1.PortName = "COM6"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(-2, 218)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(115, 30)
        Me.Button2.TabIndex = 33
        Me.Button2.Text = "Send"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(-2, 158)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 30)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "Find BT Devices"
        '
        'Status
        '
        Me.Status.Enabled = False
        Me.Status.Location = New System.Drawing.Point(-2, 253)
        Me.Status.Name = "Status"
        Me.Status.Size = New System.Drawing.Size(240, 23)
        Me.Status.TabIndex = 35
        '
        'ListView1
        '
        Me.ListView1.Location = New System.Drawing.Point(-2, 27)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(240, 125)
        Me.ListView1.TabIndex = 36
        '
        'BTLibSampleCEVB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(238, 295)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Status)
        Me.Controls.Add(Me.ListView1)
        Me.Name = "BTLibSampleCEVB"
        Me.Text = "BTLibSampleCEVB"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents SerialPort1 As System.IO.Ports.SerialPort
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents ListView1 As System.Windows.Forms.ListView

End Class
