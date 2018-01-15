/***************************************************************************
'* <Description>
'* IMGLibSampleCECS.cs (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* Image Scanner Barcode Capture Demo program
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

namespace IMGLibSampleCECS
{
    public partial class IMGLibSampleCECS : Form
    {
        public IMGLibSampleCECS()
        {
            InitializeComponent();
        }

        Int32 pError;
        string pMessage;
        string pCodeID = "  ";
        string pAimID = "   ";
        string pSymModifier = "   ";
        Int32 pLength;

        private void IMGLibSampleCECS_Load(object sender, EventArgs e)
        {
            // ini File default pass
            string pFileName = "\\FlashDisk\\System Settings\\IMGSet.ini";
            Calib.IMGLibNet.Api.IMGInit();						// IMGDRV open
            Calib.IMGLibNet.Api.IMGLoadConfigFile(pFileName);	// ini File read default value set
            Calib.IMGLibNet.Api.IMGConnect();					// IMGDRV mode will be ini File vallue 

            for (int i = 0; i < 2000; i++)
            {
                pMessage = pMessage + " ";  // allocate 512 pcs space for scanning data
            }

            textBox1.Focus();
        }

        private void IMGLibSampleCECS_Closing(object sender, CancelEventArgs e)
        {
            // IMGDRV Close
            Calib.IMGLibNet.Api.IMGDisconnect();
            Calib.IMGLibNet.Api.IMGDeinit();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if trigger key press
            if (e.KeyCode == System.Windows.Forms.Keys.F24)
            {	// read symbol
                pError = Calib.IMGLibNet.Api.IMGWaitForDecode(5000, pMessage, pCodeID, pAimID, pSymModifier, ref pLength, IntPtr.Zero);
                if (pError == Calib.IMGLibNet.Def.IMG_SUCCESS)
                {
                    textBox1.Text = pMessage;	// symbol data display
                    DispSymbol();				// symbol kind display
                }
            }
        }

        // search symbol kind
        private void DispSymbol()
        {
            switch (pCodeID)
            {
                case "A ":
                    label1.Text = "Australian Postal";
                    break;
                case "z ":
                    label1.Text = "Aztec";
                    break;
                case "B ":
                    label1.Text = "British Postal";
                    break;
                case "C ":
                    label1.Text = "Canadian Postal";
                    break;
                case "a ":
                    label1.Text = "Codabar";
                    break;
                case "q ":
                    label1.Text = "Codablock F";
                    break;
                case "h ":
                    label1.Text = "Code 11";
                    break;
                case "j ":
                    label1.Text = "Code 128 or ISBT";
                    break;
                case "b ":
                    label1.Text = "Code 39";
                    break;
                case "l ":
                    label1.Text = "Code 49 or EAN 128";
                    break;
                case "i ":
                    label1.Text = "Code 93";
                    break;
                case "y ":
                    label1.Text = "RSS or UCC/EAN Composite";
                    break;
                case "w ":
                    label1.Text = "DataMatrix";
                    break;
                case "K ":
                    label1.Text = "Dutch Postal";
                    break;
                case "d ":
                    label1.Text = "EAN13";
                    break;
                case "D ":
                    label1.Text = "EAN8";
                    break;
                case "f ":
                    label1.Text = "IATA 2of5";
                    break;
                case "e ":
                    label1.Text = "ITF(Interleaved 2of5)";
                    break;
                case "J ":
                    label1.Text = "Japanese Postal";
                    break;
                case "x ":
                    label1.Text = "Maxicode";
                    break;
                case "R ":
                    label1.Text = "MicroPDF";
                    break;
                case "g ":
                    label1.Text = "MSI";
                    break;
                case "r ":
                    label1.Text = "PDF417";
                    break;
                case "L ":
                    label1.Text = "Planet Code";
                    break;
                case "P ":
                    label1.Text = "Postnet";
                    break;
                case "O ":
                    label1.Text = "OCR";
                    break;
                case "s ":
                    label1.Text = "QR";
                    break;
                case "T ":
                    label1.Text = "TLC39";
                    break;
                case "c ":
                    label1.Text = "UPC Version A";
                    break;
                case "E ":
                    label1.Text = "UPC Version E0,E1";
                    break;

                default:
                    label1.Text = "Unknown Symbol";
                    break;
            }
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}