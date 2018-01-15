/***************************************************************************
 * <Description>
 * NFCFeliCaMain.cs (Casio Computer Co., Ltd.)
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
 * 1.0.0    2010.05.20      CASIO       000000      Original      
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

namespace NFCFeliCaCE_CS
{
    public partial class NFCFeliCaMain : Form
    {
        public NFCFeliCaMain()
        {
            InitializeComponent();
        }

        int iRet;
        byte[] pIDm = new byte[8];
        byte[] pPMm = new byte[8];
        byte[] pData = new byte[16];
        string strCommand;
        int pSystemCode;

        private void Button1_Click(object sender, EventArgs e)
        {
            Button1.Enabled = false;

            Label2.Text = "";
            Label3.Text = "";
            Label7.Text = "";
            TextBox2.Text = "";
            Application.DoEvents();

            // NFC OPEN
            //iRet = NFCFelicaLibNet.Api.NFCFelicaOpen(IntPtr.Zero);

            //TextBox2.Text = "NFCFelicaOpen...start." + ControlChars.NewLine + TextBox2.Text;

            //if (iRet != NFCFelicaLibNet.Def.NFC_OK)
            //{
            //    strCommand = "NFCFelicaOpen";
            //    DispErrorMessage(iRet, strCommand);
            //    return;
            //}
            //TextBox2.Text = "NFCFelicaOpen...OK." + ControlChars.NewLine + TextBox2.Text;

            TextBox2.Text = "NFCFelicaPolling...start." + ControlChars.NewLine + TextBox2.Text;

            // NFC POLLING
            iRet = NFCFelicaLibNet.Api.NFCFelicaPolling(10000, IntPtr.Zero, 0xffff, 0);
            if (iRet != NFCFelicaLibNet.Def.NFC_OK)
            {
                strCommand = "NFCFelicaPolling";
                DispErrorMessage(iRet, strCommand);

                return;
            }

            TextBox2.Text = "NFCFelicaPolling...OK." + ControlChars.NewLine + TextBox2.Text;

            TextBox2.Text = "NFCFelicaGetCardResponse...start." + ControlChars.NewLine + TextBox2.Text;

            // NFC GET CARD RESPONSE
            iRet = NFCFelicaLibNet.Api.NFCFelicaGetCardResponse(pIDm, pPMm, ref pSystemCode, 0);

            if (iRet != NFCFelicaLibNet.Def.NFC_OK)
            {
                strCommand = "NFCFelicaGetCardResponse";
                DispErrorMessage(iRet, strCommand);

                return;
            }

            TextBox2.Text = "NFCFelicaGetCardResponse...OK." + ControlChars.NewLine + TextBox2.Text;
            Label2.Text = pSystemCode.ToString();

            int i;

            for (i = 0; i < 8; i++)
            {
                Label3.Text = Label3.Text + pIDm[i].ToString("X2") + " ";
            }

            for (i = 0; i < 8; i++)
            {
                Label7.Text = Label7.Text + pPMm[i].ToString("X2") + " ";
            }

            //NFCFelicaRead routine

            if (ComboBox1.SelectedIndex == 0) // Read case
            {
                TextBox4.Text = "";
                TextBox2.Text = "NFCFelicaRead...start." + ControlChars.NewLine + TextBox2.Text;

                iRet = NFCFelicaLibNet.Api.NFCFelicaRead(Convert.ToInt32(TextBox1.Text.ToString(), 16), Convert.ToInt32(TextBox3.Text.ToString()), pData, 0);
                if (iRet != NFCFelicaLibNet.Def.NFC_OK)
                {
                    strCommand = "NFCFelicaRead";
                    DispErrorMessage(iRet, strCommand);
                    return;
                }

                TextBox2.Text = "NFCFelicaRead...OK." + ControlChars.NewLine + TextBox2.Text;
                for (i = 0; i < 16; i++)
                {
                    TextBox4.Text = TextBox4.Text + pData[i].ToString("X2");
                }
            }
            //// For Security reason : WRITE routine is comment out
            //else if (ComboBox1.SelectedIndex == 1) // Write case
            //{
            //    TextBox4.Text = TextBox4.Text.PadRight(32);

            //    for (i = 0; i < 16; i++)
            //    {
            //        if (TextBox4.Text.Substring(i * 2, 2) == "  ")
            //        {
            //            pData[i] = Convert.ToByte(0);
            //        }
            //        else
            //        {
            //            try
            //            {
            //                pData[i] = Convert.ToByte(Convert.ToInt32(TextBox4.Text.Substring(i * 2, 2), 16));
            //            }
            //            catch
            //            {
            //                TextBox2.Text = "Exception error. Input 16 bytes Hex value.";
            //                SystemLibNet.Api.SysSetBuzzerVolume(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.BUZZERVOLUME_MAX);
            //                SystemLibNet.Api.SysPlayBuzzer(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.B_USERDEF, SystemLibNet.Def.B_USERDEF);

            //                NFCFelicaLibNet.Api.NFCFelicaClose();
            //                Button1.Enabled = true;
            //                return;
            //            }
            //        }
            //    }
            //    TextBox2.Text = "NFCFelicaWrite...start." + ControlChars.NewLine + TextBox2.Text;

            //    iRet = NFCFelicaLibNet.Api.NFCFelicaWrite(Convert.ToInt32(TextBox1.Text.ToString(), 16), Convert.ToInt32(TextBox3.Text.ToString()), pData, 0);

            //    if (iRet != NFCFelicaLibNet.Def.NFC_OK)
            //    {
            //        strCommand = "NFCFelicaWrite";
            //        DispErrorMessage(iRet, strCommand);
            //        return;
            //    }
            //    TextBox2.Text = "NFCFelicaWrite...OK." + ControlChars.NewLine + TextBox2.Text;

            //}

            TextBox2.Text = "NFCFelicaRadioOff...start." + ControlChars.NewLine + TextBox2.Text;

            // NFC RADIO OFF
            iRet = NFCFelicaLibNet.Api.NFCFelicaRadioOff();

            if (iRet != NFCFelicaLibNet.Def.NFC_OK)
            {
                strCommand = "NFCFelicaRadioOff";
                DispErrorMessage(iRet, strCommand);
                return;
            }

            TextBox2.Text = "NFCFelicaRadioOff...OK." + ControlChars.NewLine + TextBox2.Text;

            // NFC CLOSE
            //NFCFelicaLibNet.Api.NFCFelicaClose();

            Button1.Enabled = true;
            TextBox2.Text = "NFC scanning...OK." + ControlChars.NewLine + TextBox2.Text;
            SystemLibNet.Api.SysSetBuzzerVolume(SystemLibNet.Def.B_SCANEND, SystemLibNet.Def.BUZZERVOLUME_MAX);
            SystemLibNet.Api.SysPlayBuzzer(SystemLibNet.Def.B_SCANEND, SystemLibNet.Def.B_USERDEF, SystemLibNet.Def.B_USERDEF);

        }

        private void DispErrorMessage(int iRet, string strCommand)
        {
            switch (iRet)
            {
                case NFCFelicaLibNet.Def.NFC_PON:
                    TextBox2.Text = strCommand + " : NFC already opend." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCFelicaLibNet.Def.NFC_NOT_DEVICE:
                    TextBox2.Text = strCommand + " : NFC driver error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCFelicaLibNet.Def.NFC_ERROR_INVALID_ACCESS:
                    TextBox2.Text = strCommand + " : Device exclusive error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCFelicaLibNet.Def.NFC_ERROR_MODULE:
                    TextBox2.Text = strCommand + " : NFC module not responce error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCFelicaLibNet.Def.NFC_POF:
                    TextBox2.Text = strCommand + " : NFC not open error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCFelicaLibNet.Def.NFC_PRM:
                    TextBox2.Text = strCommand + " : NFC parameter error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCFelicaLibNet.Def.NFC_ERROR_TIMEOUT:
                    TextBox2.Text = strCommand + " : Timeout error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCFelicaLibNet.Def.NFC_ERROR_CALLBACK:
                    TextBox2.Text = strCommand + " : Call back function error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCFelicaLibNet.Def.NFC_ERROR_STOP:
                    TextBox2.Text = strCommand + " : Stop error by stop function." + ControlChars.NewLine + TextBox2.Text;
                    break;
                case NFCFelicaLibNet.Def.NFC_NOT_ACTIVATION:
                    TextBox2.Text = strCommand + " : Card don't start error." + ControlChars.NewLine + TextBox2.Text;
                    break;
                default:
                    TextBox2.Text = strCommand + " : Other error." + ControlChars.NewLine + TextBox2.Text;
                    break;
            }
            SystemLibNet.Api.SysSetBuzzerVolume(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.BUZZERVOLUME_MAX);
            SystemLibNet.Api.SysPlayBuzzer(SystemLibNet.Def.B_WARNING, SystemLibNet.Def.B_USERDEF, SystemLibNet.Def.B_USERDEF);

            //NFCFelicaLibNet.Api.NFCFelicaClose();
            Button1.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Common.ShowTaskbar();
            this.Close();
        }

        private void NFCFeliCaMain_Load(object sender, EventArgs e)
        {
            Common.HideTaskbar();
            ComboBox1.SelectedIndex = 0;

            //	000001__	Modify
            // NFCFeliCa OPEN
            iRet = NFCFelicaLibNet.Api.NFCFelicaOpen(IntPtr.Zero);

            TextBox2.Text = "NFCFelicaOpen...start." + ControlChars.NewLine + TextBox2.Text;

            if (iRet != NFCFelicaLibNet.Def.NFC_OK)
            {
                strCommand = "NFCFelicaOpen";
                DispErrorMessage(iRet, strCommand);
                return;
            }
            TextBox2.Text = "NFCFelicaOpen...OK." + ControlChars.NewLine + TextBox2.Text;
            //	__000001	Modify
        }

        private void NFCFeliCaMain_Closed(object sender, EventArgs e)
        {
            //	000001__	Modify
            // NFC CLOSE
            NFCFelicaLibNet.Api.NFCFelicaClose();
            //	__000001	Modify

        }

    }
}