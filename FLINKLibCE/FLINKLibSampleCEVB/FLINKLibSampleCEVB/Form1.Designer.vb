<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Form1
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
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Label5 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(89, 171)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(128, 20)
        Me.Button5.TabIndex = 13
        Me.Button5.Text = "Disconnect"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(17, 171)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(56, 20)
        Me.Button4.TabIndex = 14
        Me.Button4.Text = "Open"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(161, 139)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(56, 20)
        Me.Button3.TabIndex = 15
        Me.Button3.Text = "Recive"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(89, 139)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(56, 20)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "Send"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(17, 139)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(56, 20)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "Idle"
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Location = New System.Drawing.Point(49, 107)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(164, 20)
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(49, 83)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(164, 20)
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(193, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 16)
        Me.Label5.Text = "100%"
        '
        'Timer1
        '
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(49, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 16)
        Me.Label4.Text = "0%"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(9, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 16)
        Me.Label3.Text = "Total"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(9, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.Text = "File"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(25, 19)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(192, 32)
        Me.TextBox1.TabIndex = 24
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(25, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 16)
        Me.Label1.Text = "File name :"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(234, 199)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ProgressBar2)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar2 As System.Windows.Forms.ProgressBar
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
