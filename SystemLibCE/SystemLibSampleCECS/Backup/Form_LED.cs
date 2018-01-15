//=============================================================================
// CASIO Mobile Device SystemLib Sample                                       +
// Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
//=============================================================================
// Form_LED.cs : LED sample                                                        +
//-----------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Calib;


namespace SystemLibSampleCS
{
	/// <summary>
	/// This is a form of the LED sample.
	/// </summary>
	public class Form_LED : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_Num;
		private System.Windows.Forms.TextBox textBox_OnTime;
		private System.Windows.Forms.TextBox textBox_OffTime;
		private System.Windows.Forms.Button button_Green;
		private System.Windows.Forms.Button button_Red;
		private System.Windows.Forms.Button button_Orange;
		private System.Windows.Forms.Button button_Magenta;
		private System.Windows.Forms.Button button_Cyan;
		private System.Windows.Forms.Button button_Blue;
		private System.Windows.Forms.Button button_Off;
		private System.Windows.Forms.Button button_Get;
	
		public Form_LED()
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_Num = new System.Windows.Forms.TextBox();
			this.textBox_OnTime = new System.Windows.Forms.TextBox();
			this.textBox_OffTime = new System.Windows.Forms.TextBox();
			this.button_Green = new System.Windows.Forms.Button();
			this.button_Red = new System.Windows.Forms.Button();
			this.button_Orange = new System.Windows.Forms.Button();
			this.button_Magenta = new System.Windows.Forms.Button();
			this.button_Cyan = new System.Windows.Forms.Button();
			this.button_Blue = new System.Windows.Forms.Button();
			this.button_Off = new System.Windows.Forms.Button();
			this.button_Get = new System.Windows.Forms.Button();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.Text = "LED ON count";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.Text = "LED ON time";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 80);
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.Text = "LED OFF Time";
			// 
			// textBox_Num
			// 
			this.textBox_Num.Location = new System.Drawing.Point(136, 8);
			this.textBox_Num.Size = new System.Drawing.Size(88, 19);
			this.textBox_Num.Text = "1";
			// 
			// textBox_OnTime
			// 
			this.textBox_OnTime.Location = new System.Drawing.Point(136, 40);
			this.textBox_OnTime.Size = new System.Drawing.Size(88, 19);
			this.textBox_OnTime.Text = "16";
			// 
			// textBox_OffTime
			// 
			this.textBox_OffTime.Location = new System.Drawing.Point(136, 72);
			this.textBox_OffTime.Size = new System.Drawing.Size(88, 19);
			this.textBox_OffTime.Text = "16";
			// 
			// button_Green
			// 
			this.button_Green.Location = new System.Drawing.Point(16, 104);
			this.button_Green.Size = new System.Drawing.Size(64, 24);
			this.button_Green.Text = "Green";
			this.button_Green.Click += new System.EventHandler(this.button_Green_Click);
			// 
			// button_Red
			// 
			this.button_Red.Location = new System.Drawing.Point(88, 104);
			this.button_Red.Size = new System.Drawing.Size(64, 24);
			this.button_Red.Text = "Red";
			this.button_Red.Click += new System.EventHandler(this.button_Red_Click);
			// 
			// button_Orange
			// 
			this.button_Orange.Location = new System.Drawing.Point(160, 104);
			this.button_Orange.Size = new System.Drawing.Size(64, 24);
			this.button_Orange.Text = "Orange";
			this.button_Orange.Click += new System.EventHandler(this.button_Orange_Click);
			// 
			// button_Magenta
			// 
			this.button_Magenta.Location = new System.Drawing.Point(160, 136);
			this.button_Magenta.Size = new System.Drawing.Size(64, 24);
			this.button_Magenta.Text = "Magenta";
			this.button_Magenta.Click += new System.EventHandler(this.button_Magenta_Click);
			// 
			// button_Cyan
			// 
			this.button_Cyan.Location = new System.Drawing.Point(88, 136);
			this.button_Cyan.Size = new System.Drawing.Size(64, 24);
			this.button_Cyan.Text = "Cyan";
			this.button_Cyan.Click += new System.EventHandler(this.button_Cyan_Click);
			// 
			// button_Blue
			// 
			this.button_Blue.Location = new System.Drawing.Point(16, 136);
			this.button_Blue.Size = new System.Drawing.Size(64, 24);
			this.button_Blue.Text = "Blue";
			this.button_Blue.Click += new System.EventHandler(this.button_Blue_Click);
			// 
			// button_Off
			// 
			this.button_Off.Location = new System.Drawing.Point(16, 176);
			this.button_Off.Size = new System.Drawing.Size(208, 24);
			this.button_Off.Text = "LED OFF";
			this.button_Off.Click += new System.EventHandler(this.button_Off_Click);
			// 
			// button_Get
			// 
			this.button_Get.Location = new System.Drawing.Point(16, 208);
			this.button_Get.Size = new System.Drawing.Size(208, 24);
			this.button_Get.Text = "Get status";
			this.button_Get.Click += new System.EventHandler(this.button_Get_Click);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			// 
			// Form_LED
			// 
			this.ClientSize = new System.Drawing.Size(240, 246);
			this.Controls.Add(this.button_Get);
			this.Controls.Add(this.button_Off);
			this.Controls.Add(this.button_Magenta);
			this.Controls.Add(this.button_Cyan);
			this.Controls.Add(this.button_Blue);
			this.Controls.Add(this.button_Orange);
			this.Controls.Add(this.button_Red);
			this.Controls.Add(this.button_Green);
			this.Controls.Add(this.textBox_OffTime);
			this.Controls.Add(this.textBox_OnTime);
			this.Controls.Add(this.textBox_Num);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "LED";
			this.Closed += new System.EventHandler(this.button_Off_Click);

		}
		#endregion

		private void button_Green_Click(object sender, System.EventArgs e)
		{
			SetLED(Calib.SystemLibNet.Def.LED_GREEN);
		}

		private void button_Red_Click(object sender, System.EventArgs e)
		{
			SetLED(Calib.SystemLibNet.Def.LED_RED);		
		}

		private void button_Orange_Click(object sender, System.EventArgs e)
		{
			SetLED(Calib.SystemLibNet.Def.LED_ORANGE);		
		}

		private void button_Blue_Click(object sender, System.EventArgs e)
		{
			SetLED(0x0000000B);
		}

		private void button_Cyan_Click(object sender, System.EventArgs e)
		{
			SetLED(0x0000000C);		
		}

		private void button_Magenta_Click(object sender, System.EventArgs e)
		{
			SetLED(0x0000000D);
		}

		private void button_Off_Click(object sender, System.EventArgs e)
		{
			SetLED(Calib.SystemLibNet.Def.LED_OFF);
		}

		private void button_Get_Click(object sender, System.EventArgs e)
		{
			int	dwRet;

			// get LED ON/OFF condition
			dwRet = Calib.SystemLibNet.Api.SysGetLED();
			switch(dwRet) 
			{
				case Calib.SystemLibNet.Def.LED_GREEN:
					MessageBox.Show("Green","SysGetLED",MessageBoxButtons.OK,MessageBoxIcon.None,MessageBoxDefaultButton.Button1);
					break;
				case Calib.SystemLibNet.Def.LED_RED:
					MessageBox.Show("Red","SysGetLED",MessageBoxButtons.OK,MessageBoxIcon.None,MessageBoxDefaultButton.Button1);
					break;
				case Calib.SystemLibNet.Def.LED_ORANGE:
					MessageBox.Show("Orange","SysGetLED",MessageBoxButtons.OK,MessageBoxIcon.None,MessageBoxDefaultButton.Button1);
					break;
				case 0x0000000B:
					MessageBox.Show("Blue","SysGetLED",MessageBoxButtons.OK,MessageBoxIcon.None,MessageBoxDefaultButton.Button1);
					break;
				case 0x0000000C:
					MessageBox.Show("Cyan","SysGetLED",MessageBoxButtons.OK,MessageBoxIcon.None,MessageBoxDefaultButton.Button1);
					break;
				case 0x0000000D:
					MessageBox.Show("Magenta","SysGetLED",MessageBoxButtons.OK,MessageBoxIcon.None,MessageBoxDefaultButton.Button1);
					break;
			}
		}

		private void SetLED(int dwMode)
		{
			int	dwNum, dwOnTime, dwOffTime;

			// LED ON count get
			dwNum = Convert.ToInt32(textBox_Num.Text);

			// LED ON time get
			dwOnTime = Convert.ToInt32(textBox_OnTime.Text);

			// LED OFF time get
			dwOffTime = Convert.ToInt32(textBox_OffTime.Text);

			// LED ON/OFF
			Calib.SystemLibNet.Api.SysSetLED(dwMode,dwNum, dwOnTime, dwOffTime);
		}

	}
}

