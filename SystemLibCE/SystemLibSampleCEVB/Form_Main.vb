'=============================================================================
' CASIO Mobile Device SystemLib Sample                                       +
' Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
'=============================================================================
' Form_Main.vb : main                                                        +
'-----------------------------------------------------------------------------
Public Class Form_Main
    Inherits System.Windows.Forms.Form
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem

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
    Friend WithEvents MenuItem_LED As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem_Buzzer As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem_Bibrator As System.Windows.Forms.MenuItem
    Private Sub InitializeComponent()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem_LED = New System.Windows.Forms.MenuItem
        Me.MenuItem_Buzzer = New System.Windows.Forms.MenuItem
        Me.MenuItem_Bibrator = New System.Windows.Forms.MenuItem
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.Add(Me.MenuItem1)
        '
        'MenuItem1
        '
        Me.MenuItem1.MenuItems.Add(Me.MenuItem_LED)
        Me.MenuItem1.MenuItems.Add(Me.MenuItem_Buzzer)
        Me.MenuItem1.MenuItems.Add(Me.MenuItem_Bibrator)
        Me.MenuItem1.Text = "Test"
        '
        'MenuItem_LED
        '
        Me.MenuItem_LED.Text = "LED"
        '
        'MenuItem_Buzzer
        '
        Me.MenuItem_Buzzer.Text = "Buzzer"
        '
        'MenuItem_Bibrator
        '
        Me.MenuItem_Bibrator.Text = "Vibrator"
        '
        'Form_Main
        '
        Me.ClientSize = New System.Drawing.Size(240, 238)
        Me.MaximizeBox = False
        Me.Menu = Me.MainMenu1
        Me.MinimizeBox = False
        Me.Text = "SystemLibSampleVB"

    End Sub

    Public Shared Sub Main()
        Application.Run(New Form_Main)
    End Sub

#End Region

    Private Sub MenuItem_LED_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem_LED.Click
        Dim f1 As New Form_LED
        f1.ShowDialog()
    End Sub

    Private Sub MenuItem_Buzzer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem_Buzzer.Click
        Dim f1 As New Form_Buzzer
        f1.ShowDialog()
    End Sub

    Private Sub MenuItem_Vibrator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem_Bibrator.Click
        Dim f1 As New Form_Vibrator
        f1.ShowDialog()
    End Sub
End Class
