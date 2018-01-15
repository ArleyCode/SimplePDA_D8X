/***************************************************************************
'* <Description>
'* Form1.cs (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* FLINK Communication Demo program between PC and Windows CE Terminal
'*  via USB Cradle
'* 
'* <Send>   : DT-5300CE / DT-X8 (\\temp\*.*) --> PC (C:\\temp)
'* <Recive> : PC (C:\\temp)          --> DT-5300CE / DT-X8 (\\temp\*.*)
'* 
'* PC side : LMWIN utility
'* -------------------------------------------------------------------------
'* <Language>
'* C#.NET
'* -------------------------------------------------------------------------
'* <Develop Environment>
'* VS 2005
'* -------------------------------------------------------------------------
'* <Target>
'* DT-5300 CE model
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

namespace FLINKLibSampleCECS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IntPtr port = new IntPtr();

        private void button1_Click(object sender, EventArgs e)
        {   // Idle : The LMWin application command of PC is executed.
            Int32 iRet;

            iRet = MoFlinkLibNet.Api.FLKIdle(port, IntPtr.Zero);
            if (iRet != MoFlinkLibNet.Def.FLK_OK)
            {
                MessageBox.Show("FLKIdle Error");
                return;
            }
            // Timer sets.
            timer1.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {   // Send : The file that exists in the terminal is transmitted to PC. LMWin only has to start.
            Int32 iRet = new Int32();

            // All files under the temp folder on the terminal are transmitted to the C:\temp folder of PC.
            iRet = MoFlinkLibNet.Api.FLKSendFile(port, MoFlinkLibNet.Def.FLK_TRANS_NORMAL, "\\temp\\*.*", "C:\\temp\\", MoFlinkLibNet.Def.FLK_PROTECT_VALID);
            if (iRet != MoFlinkLibNet.Def.FLK_OK)
            {
                MessageBox.Show("FLKSendFile Error");
                return;
            }
            // Timer sets.
            timer1.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {   // Receive : The file on PC is received to the terminal. LMWin only has to start.
            Int32 iRet = new Int32();

            // All files that exist in the C:\temp folder on PC are received to the temp folder on the terminal.
            iRet = MoFlinkLibNet.Api.FLKReceiveFile(port, MoFlinkLibNet.Def.FLK_TRANS_NORMAL, "C:\\temp\\*.*", "\\temp\\", MoFlinkLibNet.Def.FLK_PROTECT_VALID);
            if (iRet != MoFlinkLibNet.Def.FLK_OK)
            {
                MessageBox.Show("FLKReceiveFile Error");
                return;
            }
            // Timer sets.
            timer1.Enabled = true;

        }

        private void button4_Click(object sender, EventArgs e)
        {   //Disconnect
            Int16 endKind = new Int16();
            endKind = MoFlinkLibNet.Def.FLK_CLOSE_NORMAL;

            MoFlinkLibNet.Api.FLKClose(port, endKind);

        }

        private void button5_Click(object sender, EventArgs e)
        {   // Open Flink
            char[] cString = new char[10];
            MoFlinkLibNet.FLK_RSPRM rsprm = new MoFlinkLibNet.FLK_RSPRM();

            // IT-3100
            //port = MoFlinkLibNet.Api.FLKOpen("IrDA", cString, rsprm, MoFlinkLibNet.Def.FLK_MODE_HT, IntPtr.Zero, 0);
            // IT-600 DT-X7 DT-5300 IT-800
            port = MoFlinkLibNet.Api.FLKOpen("USB", cString, rsprm, MoFlinkLibNet.Def.FLK_MODE_HT, IntPtr.Zero, 0);

            if ((port == (IntPtr)MoFlinkLibNet.Def.FLK_NG) || (port == (IntPtr)MoFlinkLibNet.Def.FLK_PRM))
            {
                MessageBox.Show("FLKOpen Error");
                return;
            }

            // Timer sets.
            timer1.Enabled = true;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoFlinkLibNet.FLK_STATUS flkstatus = new MoFlinkLibNet.FLK_STATUS();

            // Stop timer.
            timer1.Enabled = false;

            MoFlinkLibNet.Api.FLKReadStatus(port, flkstatus);
            if (flkstatus.status == MoFlinkLibNet.Def.FLK_STATUS_ERROR)
            {	// Error occurred.
                MessageBox.Show("Command Error");
                return;
            }
            if (flkstatus.status == MoFlinkLibNet.Def.FLK_STATUS_END)
            {	// Complete operation.
                MessageBox.Show("Command complete");
                return;
            }
            // Update display.
            textBox1.Text = new string(flkstatus.FileName);
            if (flkstatus.file_size != 0)
            {
                progressBar1.Value = (flkstatus.file_count * 100 / flkstatus.file_size);
                progressBar2.Value = (flkstatus.total_count * 100 / flkstatus.total_size);
            }

            // Restart timer.
            timer1.Enabled = true;

        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {   // Disconnect Flink
            Int16 endKind = new Int16();
            endKind = MoFlinkLibNet.Def.FLK_CLOSE_NORMAL;

            MoFlinkLibNet.Api.FLKClose(port, endKind);

        }



    }
}