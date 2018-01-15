//=============================================================================
// CASIO Mobile Device SystemLib Sample                                       +
// Copyright (C) 2006 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
//=============================================================================
// Form_Main.cs : main routine                                                        +
//-----------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;

namespace SystemLibSampleCS
{
	/// <summary>
	/// This is a form of the main window. 
	/// </summary>
	public class Form_Main : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuLED;
		private System.Windows.Forms.MenuItem menuBUZZER;
		private System.Windows.Forms.MenuItem menuVIBRATOR;
		private System.Windows.Forms.MenuItem menuItem1;
	
		public Form_Main()
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuLED = new System.Windows.Forms.MenuItem();
			this.menuBUZZER = new System.Windows.Forms.MenuItem();
			this.menuVIBRATOR = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// menuItem1
			// 
			this.menuItem1.MenuItems.Add(this.menuLED);
			this.menuItem1.MenuItems.Add(this.menuBUZZER);
			this.menuItem1.MenuItems.Add(this.menuVIBRATOR);
			this.menuItem1.Text = "Test";
			// 
			// menuLED
			// 
			this.menuLED.Text = "LED";
			this.menuLED.Click += new System.EventHandler(this.menuLED_Click);
			// 
			// menuBUZZER
			// 
			this.menuBUZZER.Text = "Buzzer";
			this.menuBUZZER.Click += new System.EventHandler(this.menuBUZZER_Click);
			// 
			// menuVIBRATOR
			// 
			this.menuVIBRATOR.Text = "Vibrator";
			this.menuVIBRATOR.Click += new System.EventHandler(this.menuVIBRATOR_Click);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			// 
			// Form_Main
			// 
			this.ClientSize = new System.Drawing.Size(238, 267);
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Text = "SystemLibSampleCS";

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new Form_Main());
		}

		private void menuLED_Click(object sender, System.EventArgs e)
		{
			Form f1 = new Form_LED();
			f1.ShowDialog();
		}

		private void menuBUZZER_Click(object sender, System.EventArgs e)
		{
			Form f1 = new Form_BUZZER();
			f1.ShowDialog();
		}

		private void menuVIBRATOR_Click(object sender, System.EventArgs e)
		{
			Form f1 = new Form_VIBRATOR();
			f1.ShowDialog();
		}
	}
}
