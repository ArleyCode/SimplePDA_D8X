/***************************************************************************
 * <Description>
 * NFCMifareMain.cs (Casio Computer Co., Ltd.)
 * -------------------------------------------------------------------------
 * <Function outline>
 * NFC FeliCa IC Card reading Demo program
 * -------------------------------------------------------------------------
 * <Language>
 * C#.NET
 * -------------------------------------------------------------------------
 * <Develop Environment>
 * VS 2005
 * -------------------------------------------------------------------------
 * <Target>
 * DT-5300 CE model
 * -------------------------------------------------------------------------
 * Copyright(C)2010 CASIO COMPUTER CO.,LTD. All rights reserved.
 * -------------------------------------------------------------------------
 * <History>
 * Version  Date            Company     Keyword     Comment
 * 1.0.0    2010.05.27      CASIO       000000      Original      
 * 1.0.1    2012.05.22      CASIO       000001      Modify        
 * 
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

using Calib;

namespace NFCMifareCE_CS
{
    public partial class NFCMifareMain : Form
    {
        public NFCMifareMain()
        {
            InitializeComponent();
        }

        int iRet;
        byte[] pATQ = new byte[2];
        byte[] pSAK = new byte[1];
        byte[] pUid = new byte[7];
        byte[] pUidLen = new byte[1];
        byte[] pData = new byte[16];
        string strCommand;

        private void Button1_Click(object sender, EventArgs e)
        {
            int i;
            int dwMode;

            Button1.Enabled = false;

            Label2.Text = "";
            Label3.Text = "";
            Label7.Text = "";
            Label13.Text = "";
            TextBox2.Text = "";
            Application.DoEvents();

            // NFC OPEN
            //iRet = NFCMifareLibNet.Api.NFCMifareOpen(IntPtr.Zero);

            //TextBox2.Text = "NFCMifareOpen...start." + ControlChars.NewLine + TextBox2.Text;

            //if (iRet != NFCMifareLibNet.Def.NFC_OK)
            //{
            //    strCommand = "NFCMifareOpen";
            //    DispErrorMessage(iRet, strCommand);
            //    return;
            //}
            //TextBox2.Text = "NFCMifareOpen...OK." + ControlChars.NewLine + TextBox2.Text;

            TextBox2.Text = "NFCMifarePolling...start." + ControlChars.NewLine + TextBox2.Text;

            // NFCMifare POLLING
            iRet = NFCMifareLibNet.Api.NFCMifarePolling(10000, IntPtr.Zero, 0);

            if (iRet != NFCMifareLibNet.Def.NFC_OK)
            {
                strCommand = "NFCMifarePolling";
                DispErrorMessage(iRet, strCommand);
                return;
            }
            TextBox2.Text = "NFCMifarePolling...OK." + ControlChars.NewLine + TextBox2.Text;

            TextBox2.Text = "NFCMifareGetCardResponse...start." + ControlChars.NewLine + TextBox2.Text;

            // NFCMifare GET CARD RESPONSE
            //	000001__	Modify
            iRet = NFCMifareLibNet.Api.NFCMifareGetCardResponse(pATQ, pSAK, pUid, ref pUidLen[0], 0);
            //	__000001	Modify

            if (iRet != NFCMifareLibNet.Def.NFC_OK)
            {
                strCommand = "NFCMifareGetCardResponse";
                DispErrorMessage(iRet, strCommand);
                return;
            }

            TextBox2.Text = "NFCMifareGetCardResponse...OK." + ControlChars.NewLine + TextBox2.Text;

            for (i = 0; i < 2; i++)
            {
                Label2.Text = Label2.Text + pATQ[i].ToString("X2") + " ";
            }

            for (i = 0; i < 1; i++)
            {
                Label3.Text = Label3.Text + pSAK[i].ToString("X2") + " ";
            }

            for (i = 0; i < 7; i++)
            {
                Label7.Text = Label7.Text + pUid[i].ToString("X2") + " ";
            }

            for (i = 0; i < 1; i++)
            {
                Label13.Text = Label13.Text + pUidLen[i].ToString("X2") + " ";
            }

            if (Convert.ToInt32(Label13.Text) == 4)
            {
                TextBox2.Text = "NFCMifareAuthentication...start." + ControlChars.NewLine + TextBox2.Text;
                if (ComboBox2.SelectedIndex == 0)
                {
                    dwMode = NFCMifareLibNet.Def.NFC_MIFARE_KEYA;
                }
                else
                {
                    dwMode = NFCMifareLibNet.Def.NFC_MIFARE_KEYB;
                }

                TextBox5.Text = TextBox5.Text.PadRight(12);

                for (i = 0; i < 6; i++)
                {
                    if (TextBox5.Text.Substring(i * 2, 2) == " ")
                    {
                        pData[i] = Convert.ToByte(0);
                    }
                    else
                    {
                        try
                        {
                            pData[i] = Convert.ToByte(Convert.ToInt32(TextBox5.Text.Substring(i * 2, 2), 16));
                        }
                        catch
                        {
                            TextBox2.Text = "Exception error. Input 6 bytes Hex value." + ControlChars.NewLine + TextBox2.Text;
                            SystemLibNet.Api.SysSetBuzzerVolume(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.BUZZERVOLUME_MAX);
                            SystemLibNet.Api.SysPlayBuzzer(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.B_USERDEF, SystemLibNet.Def.B_USERDEF);

                            NFCMifareLibNet.Api.NFCMifareClose();
                            Button1.Enabled = true;
                            return;
                        }
                    }
                }
                iRet = NFCMifareLibNet.Api.NFCMifareAuthentication(dwMode, pData, Convert.ToInt32(TextBox1.Text.ToString()), 0);

                if (iRet != NFCMifareLibNet.Def.NFC_OK)
                {
                    strCommand = "NFCMifareAuthentication";
                    DispErrorMessage(iRet, strCommand);
                    return;
                }

                TextBox2.Text = "NFCMifareAuthentication...OK." + ControlChars.NewLine + TextBox2.Text;
            }

            if (ComboBox1.SelectedIndex == 0)
            {
                TextBox2.Text = "NFCMifareRead...start." + ControlChars.NewLine + TextBox2.Text;
                TextBox4.Text = "";
                iRet = NFCMifareLibNet.Api.NFCMifareRead(Convert.ToInt32(TextBox3.Text.ToString()), pData, 0);

                if (iRet != NFCMifareLibNet.Def.NFC_OK)
                {
                    strCommand = "NFCMifareRead";
                    DispErrorMessage(iRet, strCommand);
                    return;
                }

                TextBox2.Text = "NFCMifareRead...OK." + ControlChars.NewLine + TextBox2.Text;

                for (i = 0; i < 16; i++)
                {
                    TextBox4.Text = TextBox4.Text + pData[i].ToString("X2");
                }
            }
            else if (ComboBox1.SelectedIndex == 1)
            {
                // NFCMifare Write
                TextBox2.Text = "NFCMifareWrite...start." + ControlChars.NewLine + TextBox2.Text;
                TextBox4.Text = TextBox4.Text.PadRight(32);

                for (i = 0; i < 16; i++)
                {
                    if (TextBox4.Text.Substring(i * 2, 2) == " ")
                    {
                        pData[i] = Convert.ToByte(0);
                    }
                    else
                    {
                        try
                        {
                            pData[i] = Convert.ToByte(Convert.ToInt32(TextBox4.Text.Substring(i + 2, 2), 16));
                        }
                        catch
                        {
                            TextBox2.Text = "Exception error. Input 16 bytes Hex value." + ControlChars.NewLine + TextBox2.Text;
                            SystemLibNet.Api.SysSetBuzzerVolume(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.BUZZERVOLUME_MAX);
                            SystemLibNet.Api.SysPlayBuzzer(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.B_USERDEF, SystemLibNet.Def.B_USERDEF);

                            NFCMifareLibNet.Api.NFCMifareClose();
                            Button1.Enabled = true;
                            return;
                        }
                    }
                }
                iRet = NFCMifareLibNet.Api.NFCMifareWrite(Convert.ToInt32(TextBox3.Text.ToString()), pData, 0);

                if (iRet != NFCMifareLibNet.Def.NFC_OK)
                {
                    strCommand = "NFCMifareWrite";
                    DispErrorMessage(iRet, strCommand);
                    return;
                }
                TextBox2.Text = "NFCMifareWrite...OK." + ControlChars.NewLine + TextBox2.Text;
            }
            else if (ComboBox1.SelectedIndex == 2)
            {
                //NFCMifare4 Write
                TextBox2.Text = "NFCMifareWrite4...start." + ControlChars.NewLine + TextBox2.Text;
                TextBox4.Text = TextBox4.Text.PadRight(8);

                for (i = 0; i < 4; i++)
                {
                    if (TextBox4.Text.Substring(i * 2, 2) == " ")
                    {
                        pData[i] = Convert.ToByte(0);
                    }
                    else
                    {
                        try
                        {
                            pData[i] = Convert.ToByte(Convert.ToInt32(TextBox4.Text.Substring(i * 2, 2), 16));

                        }
                        catch
                        {
                            TextBox2.Text = "Exception error. Input 16 bytes Hex value." + ControlChars.NewLine + TextBox2.Text;
                            SystemLibNet.Api.SysSetBuzzerVolume(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.BUZZERVOLUME_MAX);
                            SystemLibNet.Api.SysPlayBuzzer(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.B_USERDEF, SystemLibNet.Def.B_USERDEF);
                            NFCMifareLibNet.Api.NFCMifareClose();
                            Button1.Enabled = true;
                            return;
                        }
                    }
                }
                iRet = NFCMifareLibNet.Api.NFCMifareWrite4(Convert.ToInt32(TextBox3.Text.ToString()),pData,0);

                if (iRet != NFCMifareLibNet.Def.NFC_OK)
                {
                    strCommand = "NFCMifareWrite4";
                    DispErrorMessage(iRet, strCommand);
                    return;
                }

                TextBox2.Text = "NFCMifareWrite4...OK." + ControlChars.NewLine + TextBox2.Text;
            }

            TextBox2.Text = "NFCMifareRadioOff...start." + ControlChars.NewLine + TextBox2.Text;

            // NFCMifare RADIO OFF
            iRet = NFCMifareLibNet.Api.NFCMifareRadioOff();

            if (iRet != NFCMifareLibNet.Def.NFC_OK)
            {
                strCommand = "NFCMifareRadioOff";
                DispErrorMessage(iRet, strCommand);
                return;
            }

            TextBox2.Text = "NFCMifareRadioOff...OK." + ControlChars.NewLine + TextBox2.Text;

            // NFC CLOSE
            //NFCMifareLibNet.Api.NFCMifareClose();

            Button1.Enabled = true;
            TextBox2.Text = "NFC scanning...OK." + ControlChars.NewLine + TextBox2.Text;
            SystemLibNet.Api.SysSetBuzzerVolume(SystemLibNet.Def.B_SCANEND, SystemLibNet.Def.BUZZERVOLUME_MAX);
            SystemLibNet.Api.SysPlayBuzzer(SystemLibNet.Def.B_SCANEND, SystemLibNet.Def.B_USERDEF, SystemLibNet.Def.B_USERDEF);
        }

        private void DispErrorMessage(int iRet, string strCommand)
        {
            switch (iRet)
            {
                case NFCMifareLibNet.Def.NFC_PON:
                    TextBox2.Text = strCommand + " : NFC already opend." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCMifareLibNet.Def.NFC_NOT_DEVICE:
                    TextBox2.Text = strCommand + " : NFC driver error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCMifareLibNet.Def.NFC_ERROR_INVALID_ACCESS:
                    TextBox2.Text = strCommand + " : Device exclusive error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCMifareLibNet.Def.NFC_ERROR_MODULE:
                    TextBox2.Text = strCommand + " : NFC module not responce error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCMifareLibNet.Def.NFC_POF:
                    TextBox2.Text = strCommand + " : NFC not open error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCMifareLibNet.Def.NFC_PRM:
                    TextBox2.Text = strCommand + " : NFC parameter error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCMifareLibNet.Def.NFC_ERROR_TIMEOUT:
                    TextBox2.Text = strCommand + " : Timeout error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCMifareLibNet.Def.NFC_ERROR_CALLBACK:
                    TextBox2.Text = strCommand + " : Call back function error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCMifareLibNet.Def.NFC_ERROR_STOP:
                    TextBox2.Text = strCommand + " : Stop error by stop function." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCMifareLibNet.Def.NFC_NOT_ACTIVATION:
                    TextBox2.Text = strCommand + " : Card don't start error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                default:
                    TextBox2.Text = strCommand + " : Other error." + ControlChars.NewLine + TextBox2.Text;
                    break;
            }
            SystemLibNet.Api.SysSetBuzzerVolume(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.BUZZERVOLUME_MAX);
            SystemLibNet.Api.SysPlayBuzzer(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.B_USERDEF, SystemLibNet.Def.B_USERDEF);

            //NFCMifareLibNet.Api.NFCMifareClose();
            Button1.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Common.ShowTaskbar();
            this.Close();
        }

        private void NFCMifareMain_Load(object sender, EventArgs e)
        {
            Common.HideTaskbar();
            ComboBox1.SelectedIndex = 0;
            ComboBox2.SelectedIndex = 0;

            //	000001__	Modify
            // NFC OPEN
            iRet = NFCMifareLibNet.Api.NFCMifareOpen(IntPtr.Zero);

            TextBox2.Text = "NFCMifareOpen...start." + ControlChars.NewLine + TextBox2.Text;

            if (iRet != NFCMifareLibNet.Def.NFC_OK)
            {
                strCommand = "NFCMifareOpen";
                DispErrorMessage(iRet, strCommand);
                return;
            }
            TextBox2.Text = "NFCMifareOpen...OK." + ControlChars.NewLine + TextBox2.Text;
            //	__000001	Modify
        }

        private void NFCMifareMain_Closed(object sender, EventArgs e)
        {
            //	000001__	Modify
            // NFC CLOSE
            NFCMifareLibNet.Api.NFCMifareClose();
            //	__000001	Modify
        }
    }
}