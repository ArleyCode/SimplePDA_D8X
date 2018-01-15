/***************************************************************************
'* <Description>
'* BTLibSampleCECS.cs (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* Bluetooth Library Sample Demo program for Windows CE
'* This program is used CASIO Bluetooth Library
'* -------------------------------------------------------------------------
'* <Language>
'* C#.NET
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Calib;

namespace BTLibSampleCECS
{
    public partial class BTLibSampleCECS : Form
    {
        public BTLibSampleCECS()
        {
            InitializeComponent();
        }

        const int BTDEF_MAX_INQUIRY_NUM = 16;
        BluetoothLibNet.BTST_LOCALINFO bt_li = new BluetoothLibNet.BTST_LOCALINFO();
        BluetoothLibNet.BTST_DEVICEINFO[] bt_di = new BluetoothLibNet.BTST_DEVICEINFO[16];
        string[,] strListView;

        IntPtr[] bt_hdev = new IntPtr[BTDEF_MAX_INQUIRY_NUM + 1];

        int bt_dmax;
        int BtRet;
        DialogResult Result;
        bool PrinterFound;
        int i,j;

        public class ColHeader : ColumnHeader
        {
            public Boolean ascending;
            public ColHeader(string text, int width, HorizontalAlignment align, Boolean asc)
            {
                this.Text = text;
                this.Width = width;
                this.TextAlign = align;
                this.ascending = asc;
            }
        }

        private void BTLibSampleCECS_Load(object sender, EventArgs e)
        {
            this.ListView1.View = View.Details;

            ListView1.Columns.Add(new ColHeader("Dev Name", 100, HorizontalAlignment.Left, true));
            ListView1.Columns.Add(new ColHeader("Dev Address", 137, HorizontalAlignment.Left, true));

            Button1.Enabled = true;
            Button5.Enabled = false;

            Application.DoEvents();

            BluetoothLibNet.Api.BTDeInitialize();
            BluetoothLibNet.Api.BTInitialize();

            string swork = new string(' ',82);
            bt_li.LocalName = swork.ToCharArray();
            bt_li.LocalAddress = "                  ".ToCharArray();
            bt_li.LocalDeviceMode = 0;
            bt_li.LocalClass1 = 0;
            bt_li.LocalClass2 = 0;
            bt_li.LocalClass3 = 0;
            bt_li.Authentication = false;
            bt_li.Encryption = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ListView1.Items.Clear();
            Application.DoEvents();

            Status.Text = "get local device info...";
            BtRet = BluetoothLibNet.Api.BTGetLocalInfo(bt_li);
            if (BtRet != BluetoothLibNet.Def.BTERR_SUCCESS)
            {
                Status.Text = "";
                Result = MessageBox.Show("BT get local info Error", "Error");
                BluetoothLibNet.Api.BTDeInitialize();
                Status.Text = "deinitialize bluetooth completed !!!";
                return;
            }
            bt_li.LocalDeviceMode = BluetoothLibNet.Def.BTMODE_BOTH_ENABLED;
            bt_li.Authentication = false;
            bt_li.Encryption = false;
            Status.Text = "set new local device info...";
            BtRet = BluetoothLibNet.Api.BTSetLocalInfo(bt_li);
            if (BtRet != BluetoothLibNet.Def.BTERR_SUCCESS)
            {
                Status.Text = "";
                Result = MessageBox.Show("BT set local info Error", "Error");
                BluetoothLibNet.Api.BTDeInitialize();
                Status.Text = "deinitialize bluetooth completed !!!";
                return;
            }
            BtRet = BluetoothLibNet.Api.BTRegisterLocalInfo();
            if (BtRet != BluetoothLibNet.Def.BTERR_SUCCESS)
            {
                Status.Text = "";
                Result = MessageBox.Show("BT register local info Error", "Error");
                BluetoothLibNet.Api.BTDeInitialize();
                Status.Text = "deinitialize bluetooth completed !!!";
                return;
                

            }
            bt_dmax = BluetoothLibNet.Def.BTDEF_MAX_INQUIRY_NUM;
            Status.Text = "searching bluetooth devices...";
            Cursor.Current = Cursors.WaitCursor;
            BtRet = BluetoothLibNet.Api.BTInquiry(IntPtr.Zero, ref bt_dmax, 5000);

            Cursor.Current = Cursors.Default;
            if (BtRet != BluetoothLibNet.Def.BTERR_SUCCESS)
            {
                Status.Text = "";
                Result = MessageBox.Show("BT Inquiry Error", "Error");
                BluetoothLibNet.Api.BTDeInitialize();
                Status.Text = "deinitialize bluetooth completed !!!";
                return;
            }
            Status.Text = "found " + bt_dmax.ToString() + " bluetooth devices!";
            PrinterFound = false;
            if (bt_dmax > 0)
                PrinterFound = true;

            string swork = new string(' ',82);

            strListView = new string[bt_dmax, 2];

            for (j = 0; j < bt_dmax; j++)
            {
                bt_di[j] = new BluetoothLibNet.BTST_DEVICEINFO();
                bt_di[j].DeviceErrorFlag = 0;
                bt_di[j].DeviceHandle = 0;
                bt_di[j].DeviceName = swork.ToCharArray();
                bt_di[j].DeviceAddress = swork.Substring(1,18).ToCharArray();
                bt_di[j].LocalClass1 = 0;
                bt_di[j].LocalClass2 = 0;
                bt_di[j].LocalClass3 = 0;
                bt_di[j].ProfileNumber = 0;
                for (i = 0; i < 16; i++)
                    bt_di[j].ProfileUUID[i] = 0;
            }
            BtRet = BluetoothLibNet.Api.BTGetDeviceInfo(bt_di, bt_dmax, 0);

            if (BtRet == BluetoothLibNet.Def.BTERR_SUCCESS | bt_dmax != 0)
            {
                for (i=0; i < bt_dmax ; i++)
                {
                    string xxx1 = "";
                    for (int ia1 = 0; ia1 < 82; ia1++)
                    {
                        xxx1 = xxx1 + bt_di[i].DeviceName[ia1].ToString();
                    }

                    string xxx2 = "";
                    for (int ia1 = 0; ia1 < 18; ia1++)
                    {
                        xxx2 = xxx2 + bt_di[i].DeviceAddress[ia1].ToString();
                    }
                  
                    ListView1.Items.Add(new ListViewItem(new string[] {xxx1, xxx2}));
                    strListView[i,0] = xxx1;
                    strListView[i,1] = xxx2;
                }
            }
            else
            {
                Result = MessageBox.Show("BT Get device info error", "Error");
                BluetoothLibNet.Api.BTDeInitialize();
                Status.Text = "deinitialize bluetooth completed !!!";
                return;
            }
            Button5.Enabled = true;
            
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox1.Text = strListView[ListView1.FocusedItem.Index, 1];
               
            }
            catch 
            {
                TextBox1.Text = "Please select BT Device!";
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            Status.Text = "BTGetServiceInfo executing...";
            BtRet = BluetoothLibNet.Api.BTGetServiceInfo(bt_di[ListView1.FocusedItem.Index]);
            Status.Text = "BTRegisterDeviceInfo executing...";
            BtRet = BluetoothLibNet.Api.BTRegisterDeviceInfo(bt_di[ListView1.FocusedItem.Index]);
            if (BtRet != BluetoothLibNet.Def.BTERR_SUCCESS)
            {
                Status.Text = "";
                Result = MessageBox.Show("BT register printer info Error", "Error");
                BluetoothLibNet.Api.BTDeInitialize();
                Status.Text = "deinitialize bluetooth completed !!!";
                return;
            }

            Status.Text = "BTSetDefaultDevice executing...";
            BtRet = BluetoothLibNet.Api.BTSetDefaultDevice(bt_di[ListView1.FocusedItem.Index], BluetoothLibNet.Def.BTPORT_SERIAL);
            if (BtRet != BluetoothLibNet.Def.BTERR_SUCCESS)
            {
                Status.Text = "";
                Result = MessageBox.Show("BT Printer set default device Error", "Error");
                BluetoothLibNet.Api.BTDeInitialize();

                Status.Text = "deinitialize bluetooth completed !!!";
                return;
            }
            Status.Text = "BTSetDefaultDevice completed !!!";
            Cursor.Current = Cursors.Default;

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string prnlabel = "BT test " + dt.ToString();
            try
            {
                SerialPort1.Open();

                SerialPort1.Write(prnlabel);

                MessageBox.Show("Print OK");

                SerialPort1.Close();
            }
            catch
            {
                MessageBox.Show("Exception error");
                SerialPort1.Close();
            }


        }

        private void Button3_Click(object sender, EventArgs e)
        {
            BluetoothLibNet.Api.BTDeInitialize();
            this.Close();
        }


    }
}