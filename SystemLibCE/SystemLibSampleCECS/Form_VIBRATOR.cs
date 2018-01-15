//=============================================================================
// CASIO Mobile Device SystemLib Sample                                       +
// Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
//=============================================================================
// Form_VIBRATOR.cs : vibration sample                                        +
//-----------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Calib;
using CalibApi = Calib.SystemLibNet.Api;
using CalibDef = Calib.SystemLibNet.Def;

namespace SystemLibSampleCS
{
	/// <summary>
	/// This is a form of the vibration sample.
	/// </summary>
	public class Form_VIBRATOR : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBox_Type;
		private System.Windows.Forms.CheckBox checkBox_Mute;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox_Num;
		private System.Windows.Forms.TextBox textBox_OnTime;
		private System.Windows.Forms.TextBox textBox_OffTime;
		private System.Windows.Forms.Button button_Play;
		private System.Windows.Forms.Button button_Stop;

		private static int dwTypeNum = 6;
		private int[] dwTypeList = new int[dwTypeNum];
		private bool[] OldSetting = new bool[dwTypeNum];
	
		public Form_VIBRATOR()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			// vibration type combobox initialize
			comboBox_Type.Items.Add("Alarm");
			dwTypeList[0] = CalibDef.B_ALARM;

			comboBox_Type.Items.Add("Warning");
			dwTypeList[1] = CalibDef.B_WARNING;

			comboBox_Type.Items.Add("Scanner");
			dwTypeList[2] = CalibDef.B_SCANEND;

			comboBox_Type.Items.Add("Wireless read");
			dwTypeList[3] = CalibDef.B_WIREREAD;

			comboBox_Type.Items.Add("User Define");
			dwTypeList[4] = CalibDef.B_USERDEF;

			comboBox_Type.Items.Add("All Vibrator");
			dwTypeList[5] = CalibDef.B_ALL;

   		    // get vibration setting
			GetVibratorSetting();

			comboBox_Type.SelectedIndex = 0;
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
			this.comboBox_Type = new System.Windows.Forms.ComboBox();
			this.checkBox_Mute = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_Num = new System.Windows.Forms.TextBox();
			this.textBox_OnTime = new System.Windows.Forms.TextBox();
			this.textBox_OffTime = new System.Windows.Forms.TextBox();
			this.button_Play = new System.Windows.Forms.Button();
			this.button_Stop = new System.Windows.Forms.Button();
			// 
			// comboBox_Type
			// 
			this.comboBox_Type.Location = new System.Drawing.Point(8, 16);
			this.comboBox_Type.Size = new System.Drawing.Size(216, 20);
			this.comboBox_Type.SelectedIndexChanged += new System.EventHandler(this.ChangeType);
			// 
			// checkBox_Mute
			// 
			this.checkBox_Mute.Location = new System.Drawing.Point(8, 48);
			this.checkBox_Mute.Text = "Mute";
			this.checkBox_Mute.CheckStateChanged += new System.EventHandler(this.ChangeMute);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 80);
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.Text = "Vibrator Count";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 112);
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.Text = "Vibrator ON Time";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 144);
			this.label3.Size = new System.Drawing.Size(80, 16);
			this.label3.Text = "Vibrator OFF Time";
			// 
			// textBox_Num
			// 
			this.textBox_Num.Location = new System.Drawing.Point(160, 72);
			this.textBox_Num.Size = new System.Drawing.Size(64, 19);
			this.textBox_Num.Text = "";
			// 
			// textBox_OnTime
			// 
			this.textBox_OnTime.Location = new System.Drawing.Point(160, 104);
			this.textBox_OnTime.Size = new System.Drawing.Size(64, 19);
			this.textBox_OnTime.Text = "";
			// 
			// textBox_OffTime
			// 
			this.textBox_OffTime.Location = new System.Drawing.Point(160, 136);
			this.textBox_OffTime.Size = new System.Drawing.Size(64, 19);
			this.textBox_OffTime.Text = "";
			// 
			// button_Play
			// 
			this.button_Play.Location = new System.Drawing.Point(8, 176);
			this.button_Play.Size = new System.Drawing.Size(104, 24);
			this.button_Play.Text = "Play";
			this.button_Play.Click += new System.EventHandler(this.button_Play_Click);
			// 
			// button_Stop
			// 
			this.button_Stop.Location = new System.Drawing.Point(120, 176);
			this.button_Stop.Size = new System.Drawing.Size(104, 24);
			this.button_Stop.Text = "Stop";
			this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			// 
			// Form_VIBRATOR
			// 
			this.ClientSize = new System.Drawing.Size(236, 242);
			this.Controls.Add(this.button_Stop);
			this.Controls.Add(this.button_Play);
			this.Controls.Add(this.textBox_OffTime);
			this.Controls.Add(this.textBox_OnTime);
			this.Controls.Add(this.textBox_Num);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBox_Mute);
			this.Controls.Add(this.comboBox_Type);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "VIBRATOR";
			this.Closed += new System.EventHandler(this.ResetSetting);

		}
		#endregion

		private void button_Play_Click(object sender, System.EventArgs e)
		{
			int		dwRet;
			int		nSelType;
			int		dwNum = 0;
			int		dwOnTime = 0;
			int		dwOffTime = 0;

			nSelType = comboBox_Type.SelectedIndex;
			if(dwTypeList[nSelType] == CalibDef.B_USERDEF)
			{
				// vibration count
				dwNum = Convert.ToInt32(textBox_Num.Text);

				// vibration on time
				dwOnTime = Convert.ToInt32(textBox_OnTime.Text);

				// vibration off time
				dwOffTime = Convert.ToInt32(textBox_OffTime.Text);
			}

			// Play
			dwRet = CalibApi.SysPlayVibrator(
				dwTypeList[nSelType], dwNum, dwOnTime, dwOffTime);
			CommonFunc.ShowErrorMessage("SysPlayVibrator", dwRet);
		}

		private void button_Stop_Click(object sender, System.EventArgs e)
		{
			// Stop
			CalibApi.SysStopVibrator();
		}

		private void GetVibratorSetting()
		{
			int dwMute;
			int i;


			for(i = 0 ; i < dwTypeNum; i++)
			{
				// get mute setting
				dwMute = CalibApi.SysGetVibratorMute(dwTypeList[i]);
				CommonFunc.ShowErrorMessage("SysGetVibratorMute", dwMute);
				if( dwMute != 0)
				{
					OldSetting[i] = true;
				}
				else
				{
					OldSetting[i] = false;
				}
			}
		}

		// vibration type change
		private void ChangeType(object sender, System.EventArgs e)
		{
			int nSelType = comboBox_Type.SelectedIndex;


			// get mute setting
			if(CalibApi.SysGetVibratorMute(dwTypeList[nSelType]) != 0)
			{
				checkBox_Mute.Checked = true;
			}
			else
			{
				checkBox_Mute.Checked = false;
			}

			// judge selected vibration type is user setting or not
			if(dwTypeList[nSelType] == CalibDef.B_USERDEF)
			{
				// vibration count
				textBox_Num.Text = "2";
				textBox_Num.Enabled = true;

				// vibration on time
				textBox_OnTime.Text = "100";
				textBox_OnTime.Enabled = true;

				// vibration off time
				textBox_OffTime.Text = "100";
				textBox_OffTime.Enabled = true;
			}
			else
			{
				// disable vibration count
				textBox_Num.Text = "";
				textBox_Num.Enabled = false;
		
				// disable vibration on time
				textBox_OnTime.Text = "";
				textBox_OnTime.Enabled = false;

				// disable vibration off time
				textBox_OffTime.Text = "";
				textBox_OffTime.Enabled = false;
			}

    		// judge selected vibration type is full vibration or not
			if(dwTypeList[nSelType] == CalibDef.B_ALL)
			{
				button_Play.Enabled = false;
			}
			else
			{
				button_Play.Enabled = true;
			}
		
		}

		private void ChangeMute(object sender, System.EventArgs e)
		{
			int		dwRet;
			int		nSelType = comboBox_Type.SelectedIndex;


			// change mute setting
			dwRet = CalibApi.SysSetVibratorMute(dwTypeList[nSelType], checkBox_Mute.Checked);
			CommonFunc.ShowErrorMessage("SysSetVibratorMute", dwRet);		
		}

		private void ResetSetting(object sender, System.EventArgs e)
		{
			Int32 dwRet;
			int i;

			// return before setting
			for(i = 0 ; i < dwTypeNum; i++)
			{
				// change vibration setting
				dwRet = CalibApi.SysSetVibratorMute(dwTypeList[i],OldSetting[i]);
				CommonFunc.ShowErrorMessage("SysGetVibratorMute", dwRet);
			}
		}
	}
}
