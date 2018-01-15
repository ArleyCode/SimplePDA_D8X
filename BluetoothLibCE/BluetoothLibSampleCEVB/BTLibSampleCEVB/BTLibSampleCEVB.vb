'***************************************************************************
'* <Description>
'* BTLibSampleCEVB.vb (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* Bluetooth Library Sample Demo program for Windows CE
'* This program is used CASIO Bluetooth Library
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
Imports Calib.BluetoothLibNet.Api
Imports Calib.BluetoothLibNet.Def

Public Class BTLibSampleCEVB
    Const BTDEF_MAX_INQUIRY_NUM = 16

    'Set new wrap objects for libraries

    'Set some variables used by bluetooth
    Dim bt_li As New Calib.BluetoothLibNet.BTST_LOCALINFO

    Dim bt_di() As Calib.BluetoothLibNet.BTST_DEVICEINFO
    Dim strListView(,) As String

    Dim bt_hdev(BTDEF_MAX_INQUIRY_NUM + 1) As IntPtr
    Dim bt_dmax As Int32
    Dim BtRet As Int32
    Dim Result As DialogResult
    Dim PrinterFound As Boolean
    Dim i, j, ii As Int32

    ' The ColHeader class is a ColumnHeader object with an 
    ' added property for determining an ascending or descending sort.
    ' True specifies an ascending order, false specifies a descending order.
    Public Class ColHeader
        Inherits ColumnHeader
        Public ascending As Boolean

        Public Sub New(ByVal [text] As String, ByVal width As Integer, ByVal align As HorizontalAlignment, ByVal asc As Boolean)
            Me.Text = [text]
            Me.Width = width
            Me.TextAlign = align
            Me.ascending = asc
        End Sub
    End Class

    Private Sub BTLibSampleCEVB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ListView1.View = View.Details

        ' Add columns using the ColHeader class. The fourth    
        ' parameter specifies true for an ascending sort order.
        ListView1.Columns.Add(New ColHeader("Dev Name", 100, HorizontalAlignment.Left, True))
        ListView1.Columns.Add(New ColHeader("Dev Address", 137, HorizontalAlignment.Left, True))

        Button1.Enabled = True
        Button5.Enabled = False

        Application.DoEvents()

        BTDeInitialize()
        BTInitialize()

        bt_li.LocalName = Space(87)
        bt_li.LocalAddress = Space(18)
        bt_li.LocalDeviceMode = 0
        bt_li.LocalClass1 = 0
        bt_li.LocalClass2 = 0
        bt_li.LocalClass3 = 0
        bt_li.Authentication = False
        bt_li.Encryption = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListView1.Items.Clear()
        Application.DoEvents()

        Status.Text = "get local device info..."
        BtRet = BTGetLocalInfo(bt_li)
        If BtRet <> BTERR_SUCCESS Then
            Status.Text = ""
            Result = MessageBox.Show("BT get local info Error", "Error")
            BTDeInitialize()
            Status.Text = "deinitialize bluetooth completed !!!"
            Exit Sub
        End If
        bt_li.LocalDeviceMode = BTMODE_BOTH_ENABLED
        bt_li.Authentication = False
        bt_li.Encryption = False
        Status.Text = "set new local device info..."
        BtRet = BTSetLocalInfo(bt_li)
        If BtRet <> BTERR_SUCCESS Then
            Status.Text = ""
            Result = MessageBox.Show("BT set local info Error", "Error")
            BTDeInitialize()
            Status.Text = "deinitialize bluetooth completed !!!"
            Exit Sub
        End If
        BtRet = BTRegisterLocalInfo()
        If BtRet <> BTERR_SUCCESS Then
            Status.Text = ""
            Result = MessageBox.Show("BT register local info Error", "Error")
            BTDeInitialize()
            Status.Text = "deinitialize bluetooth completed !!!"
            Exit Sub
        End If
        bt_dmax = BTDEF_MAX_INQUIRY_NUM
        Status.Text = "searching bluetooth devices..."
        Cursor.Current = Cursors.WaitCursor
        BtRet = BTInquiry(IntPtr.Zero, bt_dmax, 5000)

        Cursor.Current = Cursors.Default
        If BtRet <> BTERR_SUCCESS Then
            Status.Text = ""
            Result = MessageBox.Show("BT Inquiry Error", "Error")
            BTDeInitialize()
            Status.Text = "deinitialize bluetooth completed !!!"
            Exit Sub
        End If
        Status.Text = "found " + bt_dmax.ToString + " bluetooth devices!"
        PrinterFound = False
        If bt_dmax >= 1 Then
            PrinterFound = True
        End If

        ReDim bt_di(bt_dmax)
        ReDim strListView(bt_dmax - 1, 1)

        For j = 0 To bt_dmax - 1

            bt_di(j) = New Calib.BluetoothLibNet.BTST_DEVICEINFO

            bt_di(j).DeviceErrorFlag = 0
            bt_di(j).DeviceHandle = 0
            bt_di(j).DeviceName = Space(82)
            bt_di(j).DeviceAddress = Space(18)
            bt_di(j).LocalClass1 = 0
            bt_di(j).LocalClass2 = 0
            bt_di(j).LocalClass3 = 0
            bt_di(j).ProfileNumber = 0
            For i = 0 To 15
                bt_di(j).ProfileUUID(i) = 0
            Next
        Next

        BtRet = BTGetDeviceInfo(bt_di, bt_dmax, 0)

        If BtRet = BTERR_SUCCESS Or bt_dmax <> 0 Then

            For i = 0 To bt_dmax - 1
                ' Add the data.
                ListView1.Items.Add(New ListViewItem(New String() _
                                    {bt_di(i).DeviceName, _
                                    bt_di(i).DeviceAddress}))

                strListView(i, 0) = bt_di(i).DeviceName
                strListView(i, 1) = bt_di(i).DeviceAddress
            Next
        Else
            Result = MessageBox.Show("BT Get device inro error", "Error")
            BTDeInitialize()

            Status.Text = "deinitialize bluetooth completed !!!"
            Exit Sub

        End If
        Button5.Enabled = True
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            TextBox1.Text = strListView(ListView1.FocusedItem.Index, 1)
        Catch ex As Exception
            TextBox1.Text = "Please select BT Device!"
            Exit Sub
        End Try
        Cursor.Current = Cursors.WaitCursor
        Status.Text = "BTGetServiceInfo executing..."
        BtRet = BTGetServiceInfo(bt_di(ListView1.FocusedItem.Index))
        Status.Text = "BTRegisterDeviceInfo executing..."
        BtRet = BTRegisterDeviceInfo(bt_di(ListView1.FocusedItem.Index))
        If BtRet <> BTERR_SUCCESS Then
            Status.Text = ""
            Result = MessageBox.Show("BT register printer info Error", "Error")
            BTDeInitialize()

            Status.Text = "deinitialize bluetooth completed !!!"
            Exit Sub
        End If

        Status.Text = "BTSetDefaultDevice executing..."
        BtRet = BTSetDefaultDevice(bt_di(ListView1.FocusedItem.Index), BTPORT_SERIAL)
        If BtRet <> BTERR_SUCCESS Then
            Status.Text = ""
            Result = MessageBox.Show("BT Printer set default device Error", "Error")
            BTDeInitialize()

            Status.Text = "deinitialize bluetooth completed !!!"
            Exit Sub
        End If
        Status.Text = "BTSetDefaultDevice completed !!!"

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim dt As DateTime = DateTime.Now
        Dim prnlabel As String = "BT test " + dt.ToString

        SerialPort1.Open()

        SerialPort1.Write(prnlabel)
        MessageBox.Show("Print OK")

        SerialPort1.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        BTDeInitialize()
        Me.Close()
    End Sub
End Class
