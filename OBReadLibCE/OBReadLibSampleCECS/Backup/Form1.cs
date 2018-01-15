/***************************************************************************
'* <Description>
'* Form1.cs (Casio Computer Co., Ltd.)
'* -------------------------------------------------------------------------
'* <Function outline>
'* OBR Library Sample program
'* -------------------------------------------------------------------------
'* <Language>
'* C#.NET
'* -------------------------------------------------------------------------
'* <Develop Environment>
'* VS 2005
'* -------------------------------------------------------------------------
'* <Target>
'* JPN : DT-5300CE / DT-X7 / DT-5200 / DT-X8
'* ENG : DT-X30CE / DT-X7 / DT-X11 / IT-600 / DT-X8
'* -------------------------------------------------------------------------
'* Copyright(C)2010 CASIO COMPUTER CO.,LTD. All rights reserved.
'* -------------------------------------------------------------------------
'* <History>
'* Version  Date            Company     Keyword     Comment
'* 1.0.0    2010.02.17      CASIO       000000      Original      
'* 
'***************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
using Calib;

namespace OBRLibSampleCS
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox2;
	
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.Label label1;
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 8);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(208, 104);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(120, 136);
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(96, 23);
            this.textBox2.TabIndex = 5;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(120, 168);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(96, 23);
            this.textBox3.TabIndex = 4;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(120, 200);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(96, 23);
            this.textBox4.TabIndex = 3;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(16, 144);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(72, 16);
            label1.Text = "cài đặt";
            label1.ParentChanged += new System.EventHandler(this.label1_ParentChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.Text = "Data length";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.Text = "Terminate code";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(242, 271);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(label1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "OBRLibSampleCS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closed += new System.EventHandler(this.Form1_Closed);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{

            OBReadLibNet.Api.OBRClose();				//OBRDRV Close
			Application.Run(new Form1());
		}

		[DllImport("coredll.dll")]
		public static extern IntPtr GetForegroundWindow();

		public static IntPtr HWND;
        private System.Threading.Thread thread;
		//Barcode type
		static int[] DecodeNum = {
									  OBReadLibNet.Def.OBR_NONDT,
									  OBReadLibNet.Def.OBR_CD39,
									  OBReadLibNet.Def.OBR_NW_7,
									  OBReadLibNet.Def.OBR_WPCA,
									  OBReadLibNet.Def.OBR_WPC,
									  OBReadLibNet.Def.OBR_UPEA,
									  OBReadLibNet.Def.OBR_UPE,
									  OBReadLibNet.Def.OBR_IDF,
									  OBReadLibNet.Def.OBR_ITF,
									  OBReadLibNet.Def.OBR_CD93,
									  OBReadLibNet.Def.OBR_CD128,
									  OBReadLibNet.Def.OBR_MSI,
									  OBReadLibNet.Def.OBR_IATA
								  };

		static string[] DecodeName = {
										 "          ",
										 "OBR_CD39  ",
										 "OBR_NW_7  ",
										 "OBR_WPCA  ",
										 "OBR_WPC   ",
										 "OBR_UPEA  ",
										 "OBR_UPE   ",
										 "OBR_IDF   ",
										 "OBR_ITF   ",
										 "OBR_CD93  ",
										 "OBR_CD128 ",
										 "OBR_MSI   ",
										 "OBR_IATA  "
									 };

		private void Form1_Load(object sender, System.EventArgs e)
		{
			int iRet;

            HWND = GetForegroundWindow();
			iRet = OBReadLibNet.Api.OBRLoadConfigFile();		//ini File read default value set
			iRet = OBReadLibNet.Api.OBRSetDefaultSymbology();	//1D(OBR) driver mode will be ini File vallue
            iRet = OBReadLibNet.Api.OBRSetScanningKey(OBReadLibNet.Def.OBR_TRIGGERKEY_L | OBReadLibNet.Def.OBR_TRIGGERKEY_R | OBReadLibNet.Def.OBR_CENTERTRIGGER);
			iRet = OBReadLibNet.Api.OBRSetScanningCode(OBReadLibNet.Def.OBR_ALL);
			iRet = OBReadLibNet.Api.OBRSetBuffType(OBReadLibNet.Def.OBR_BUFOBR);	//1D(OBR) driver mode will be OBR_BUFOBR
			iRet = OBReadLibNet.Api.OBRSetScanningNotification(OBReadLibNet.Def.OBR_EVENT, IntPtr.Zero);	//1D(OBR) driver mode will be OBR_EVENT
			iRet = OBReadLibNet.Api.OBROpen(HWND, 0);			//OBRDRV open

			if(iRet == OBReadLibNet.Def.OBR_ERROR_INVALID_ACCESS)
			{
				MessageBox.Show("Failed to connect to the scanner. Please exit a scanner application.", "OBRLibSampleCS");
				return;
			}
			else if(iRet == OBReadLibNet.Def.OBR_ERROR_HOTKEY)
			{
				MessageBox.Show("Trigger keys are being used. Please quit the program which is using the trigger keys.", "OBRLibSampleCS");
				return;
			}
			else if(iRet != OBReadLibNet.Def.OBR_OK)
			{
				MessageBox.Show("Failed to connect to the scanner.", "OBRLibSampleCS");
				return;
			}
			
			iRet = OBReadLibNet.Api.OBRClearBuff();
			thread = new Thread( new ThreadStart(start));
            thread.Start();     //Start start thread
		}

		delegate void SetLabelText();

		private void SetText()
		{
			int leng = new int();	//digit number
			byte leng2 = new byte();//digit number
			int dwrcd = new int();	//barcode type
			int ret;
			byte lcnt = new byte();
			byte[] buff = new byte[1024];
			string str;
			int i;

			if( HWND !=  IntPtr.Zero )
			{	// check OBRBuffer state
				ret = OBReadLibNet.Api.OBRGetStatus(  ref leng, ref lcnt );
				if( leng != 0 )
				{
					// get OBRBuffer data
					ret = OBReadLibNet.Api.OBRGets( buff, ref dwrcd, ref leng2 );
					Encoding ASCII = Encoding.GetEncoding("ascii");
					textBox1.Text = ASCII.GetString(buff, 0, leng2);	//scan barcode type display

					str = "----------";
					for( i=0; i<13; i++ )
					{
						if( DecodeNum[i] == dwrcd )
						{
							str = DecodeName[i];
							break;
						}
					}
					textBox2.Text = str;				//scan barcode type display
					textBox3.Text = leng2.ToString();	//digit number display
					textBox4.Text = ret.ToString();		//end information display
				}
				OBReadLibNet.Api.OBRClearBuff();
			}
		}

        private void start()
        {
            while (true)
            {
                SystemLibNet.Api.SysWaitForEvent(IntPtr.Zero, OBReadLibNet.Def.OBR_NAME_EVENT, SystemLibNet.Def.INFINITE);  //Wait event
                Invoke(new SetLabelText(SetText));      //Display OBRBuffer data
            }
        }

		private void Form1_Closed(object sender, System.EventArgs e)
		{
			OBReadLibNet.Api.OBRClose();				//OBRDRV Close
			SystemLibNet.Api.SysTerminateWaitEvent();	//End SysWaitForEvent function
			HWND = IntPtr.Zero;
			textBox1.Text = "";
			textBox2.Text = "";
			textBox3.Text = "";
			textBox4.Text = "";
			thread.Abort();								//Abort start thread
		}

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Tiếng Việt Bài viết này giới thiệu và hướng dẫn bạn cách cài đặt và sử dụng Font chữ tiếng Việt, bộ gõ, các từ điển tiếng Việt, Âm lịch và các chương trình đọc eBook trên các thiết bị dùng.txt.", "OBRLibSampleCS");
        }
	}
}
