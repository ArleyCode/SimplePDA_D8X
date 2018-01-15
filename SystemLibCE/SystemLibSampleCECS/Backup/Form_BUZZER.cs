//=============================================================================
// CASIO Mobile Device SystemLib Sample                                       +
// Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
//=============================================================================
// Form_BUZZER.cs : Buzzer sample                                            +
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
	/// This is a form of the Buzzer sample.
	/// </summary>
	public class Form_BUZZER : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBox_Type;
		private System.Windows.Forms.CheckBox checkBox_Mute;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button_Play;
		private System.Windows.Forms.Button button_Stop;
		private System.Windows.Forms.ComboBox comboBox_Volume;


	    // structure definition
		private struct tagBUZZERSETTING 
		{
			public bool		bMute;
			public int		dwVol;
			tagBUZZERSETTING(
				bool bMute,
				int	dwVol)
			{
				this.bMute = bMute;
				this.dwVol = dwVol;
			}
		}

  		// MAX number
		private static int TYPE_MAXNUM = 6;

	    // Type List
		private int[] dwTypeList = new int[TYPE_MAXNUM]; 

  		// setting before change
		private tagBUZZERSETTING[] OldSetting = new tagBUZZERSETTING[TYPE_MAXNUM]; 

	    // setting after change
		private tagBUZZERSETTING[] NewSetting = new tagBUZZERSETTING[TYPE_MAXNUM]; 
		

		public Form_BUZZER()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
			// buzzer type combo box initialize
			comboBox_Type.Items.Add("Click");
			dwTypeList[0] = CalibDef.B_CLICK;

			comboBox_Type.Items.Add("Alarm");
			dwTypeList[1] = CalibDef.B_ALARM;

			comboBox_Type.Items.Add("Warning");
			dwTypeList[2] = CalibDef.B_WARNING;

			comboBox_Type.Items.Add("Scanner");
			dwTypeList[3] = CalibDef.B_SCANEND;

			comboBox_Type.Items.Add("Tap");
			dwTypeList[4] = CalibDef.B_TAP;

			comboBox_Type.Items.Add("All Buzzer");
			dwTypeList[5] = CalibDef.B_ALL;


			// A present setting is preserved. 
			GetBuzzerSetting();

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
			this.button_Play = new System.Windows.Forms.Button();
			this.button_Stop = new System.Windows.Forms.Button();
			this.comboBox_Volume = new System.Windows.Forms.ComboBox();
			// 
			// comboBox_Type
			// 
			this.comboBox_Type.Location = new System.Drawing.Point(16, 8);
			this.comboBox_Type.Size = new System.Drawing.Size(208, 20);
			this.comboBox_Type.SelectedIndexChanged += new System.EventHandler(this.ChangeType);
			// 
			// checkBox_Mute
			// 
			this.checkBox_Mute.Location = new System.Drawing.Point(16, 40);
			this.checkBox_Mute.Text = "Mute";
			this.checkBox_Mute.CheckStateChanged += new System.EventHandler(this.ChangeMute);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 72);
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.Text = "Volume";
			// 
			// button_Play
			// 
			this.button_Play.Location = new System.Drawing.Point(8, 112);
			this.button_Play.Size = new System.Drawing.Size(104, 24);
			this.button_Play.Text = "Play";
			this.button_Play.Click += new System.EventHandler(this.button_Play_Click);
			// 
			// button_Stop
			// 
			this.button_Stop.Location = new System.Drawing.Point(128, 112);
			this.button_Stop.Size = new System.Drawing.Size(104, 24);
			this.button_Stop.Text = "Stop";
			this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
			// 
			// comboBox_Volume
			// 
			this.comboBox_Volume.Items.Add("Min");
			this.comboBox_Volume.Items.Add("Middle");
			this.comboBox_Volume.Items.Add("Max");
			this.comboBox_Volume.Location = new System.Drawing.Point(128, 64);
			this.comboBox_Volume.Size = new System.Drawing.Size(96, 20);
			this.comboBox_Volume.SelectedIndexChanged += new System.EventHandler(this.ChangeVolume);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			// 
			// Form_BUZZER
			// 
			this.ClientSize = new System.Drawing.Size(238, 244);
			this.Controls.Add(this.comboBox_Volume);
			this.Controls.Add(this.button_Stop);
			this.Controls.Add(this.button_Play);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBox_Mute);
			this.Controls.Add(this.comboBox_Type);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "BUZZER";
			this.Closed += new System.EventHandler(this.ResetSetting);

		}
		#endregion

		private void button_Play_Click(object sender, System.EventArgs e)
		{
			int	nSelType, dwRet;

			nSelType = comboBox_Type.SelectedIndex;

			//play
			dwRet = CalibApi.SysPlayBuzzer(
						dwTypeList[nSelType], 0, 0);
            CommonFunc.ShowErrorMessage("ֹͣSysPlayBuzzer", dwRet);
		}

		private void button_Stop_Click(object sender, System.EventArgs e)
		{
			int dwRet;

			//stop
			dwRet = CalibApi.SysStopBuzzer();
			CommonFunc.ShowErrorMessage("SysStopBuzzer", dwRet);		
		}

		// change buzzer type
		private void ChangeType(object sender, System.EventArgs e)
		{
			int	nNewSel;

			nNewSel = comboBox_Type.SelectedIndex;

    	    // change mute setting
			if(NewSetting[nNewSel].bMute == true) 
			{
				checkBox_Mute.Checked = true;
			} 
			else 
			{
				checkBox_Mute.Checked = false;
			}

    	    // change volume setting
			comboBox_Volume.SelectedIndex = NewSetting[nNewSel].dwVol;


      		// judge selected buzzer type is max sound or not
			if(dwTypeList[nNewSel] == CalibDef.B_ALL) 
			{
				comboBox_Volume.Enabled = false;
				button_Play.Enabled = false;
			} 
			else 
			{
				comboBox_Volume.Enabled = true;
				button_Play.Enabled = true;
			}
		}

		private void ChangeMute(object sender, System.EventArgs e)
		{
			Int32	dwRet;
			int		nSel;

			nSel = comboBox_Type.SelectedIndex;

			NewSetting[nSel].bMute = checkBox_Mute.Checked;

			// mute setting set
			dwRet = CalibApi.SysSetBuzzerMute(dwTypeList[nSel], NewSetting[nSel].bMute);			
			CommonFunc.ShowErrorMessage("SysSetBuzzerMute", dwRet);
		}

		private void ChangeVolume(object sender, System.EventArgs e)
		{
		
			Int32		dwRet;
			int		dwVol;
			int		nSel;

			nSel = comboBox_Type.SelectedIndex;

			NewSetting[nSel].dwVol = comboBox_Volume.SelectedIndex;

    	    // change Volume setting
			if(NewSetting[nSel].dwVol == 0)		dwVol = CalibDef.BUZZERVOLUME_MIN;
			else if(NewSetting[nSel].dwVol == 1)	dwVol = CalibDef.BUZZERVOLUME_MID;
			else dwVol = CalibDef.BUZZERVOLUME_MAX;
			dwRet = CalibApi.SysSetBuzzerVolume(dwTypeList[nSel], dwVol);
			CommonFunc.ShowErrorMessage("SysSetBuzzerVolume", dwRet);
		}

		// buzzer setting get
		private void GetBuzzerSetting()
		{
			Int32 dwMute;
			int dwVol;
			int i;

			for(i = 0 ; i < TYPE_MAXNUM; i++)
			{
				// mute setting get
				dwMute = CalibApi.SysGetBuzzerMute(dwTypeList[i]);
				CommonFunc.ShowErrorMessage("SysGetBuzzerMute", dwMute);
				if( dwMute != 0 ) 
				{
					NewSetting[i].bMute = OldSetting[i].bMute = true;
				} 
				else 
				{
					NewSetting[i].bMute = OldSetting[i].bMute = false;
				}

				// volume get
				dwVol = CalibApi.SysGetBuzzerVolume(dwTypeList[i]);
				CommonFunc.ShowErrorMessage("SysGetBuzzerVolume", dwVol);

				if(dwVol == CalibDef.BUZZERVOLUME_MIN) 
				{
					NewSetting[i].dwVol = OldSetting[i].dwVol = 0;
				}
				else if(dwVol == CalibDef.BUZZERVOLUME_MID) 
				{
					NewSetting[i].dwVol = OldSetting[i].dwVol = 1;
				}
				else 
				{
					NewSetting[i].dwVol = OldSetting[i].dwVol = 2;
				}
			}

			return;
		}


		private void ResetSetting(object sender, System.EventArgs e)
		{
			int		dwRet;
			int		dwVol;
			int		i;

			// return to before setting
			for( i=0 ; i < TYPE_MAXNUM ; i++ ) 
			{
				// mute setting change
				dwRet = CalibApi.SysSetBuzzerMute(dwTypeList[i], OldSetting[i].bMute);			
				CommonFunc.ShowErrorMessage("SysSetBuzzerMute", dwRet);

    		    // change Volume setting
				if(OldSetting[i].dwVol == 0)		dwVol = CalibDef.BUZZERVOLUME_MIN;
				else if(OldSetting[i].dwVol == 1)	dwVol = CalibDef.BUZZERVOLUME_MID;
				else dwVol = CalibDef.BUZZERVOLUME_MAX;
				dwRet = CalibApi.SysSetBuzzerVolume(dwTypeList[i], dwVol);
				CommonFunc.ShowErrorMessage("SysSetBuzzerVolume", dwRet);
			}
		}
	}
}
