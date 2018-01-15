//=============================================================================
// CASIO Mobile Device SystemLib Sample                                       +
// Copyright (C) 2005 CASIO COMPUTER CO., LTD. All Rights Reserved.           +
//=============================================================================
// CommonFunc.cs : common function                                           +
//-----------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using Calib;


namespace SystemLibSampleCS
{
	/// <summary>
	/// Summary description for CommonFunc.
	/// </summary>
	public class CommonFunc : System.ComponentModel.Component
	{
		public CommonFunc()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		// error message display
		public static void ShowErrorMessage(string strCaption, Int32 dwStatus)
		{
			string strTitle;
			string	strMsg;


			if( dwStatus == Calib.SystemLibNet.Def.SYS_PARAMERR ) 
			{
				strMsg = "Invalid parameter specified.";
			}
			else if( dwStatus == Calib.SystemLibNet.Def.FUNCTION_UNSUPPORT )
			{
				strMsg = "Do not support function.";
			} 
			else 
			{
				return;
			}

			if( strCaption.Length > 0 ) 
			{
				strTitle = strCaption;
			} 
			else 
			{
				strTitle = "SystemLibSampleCS";
			}
			MessageBox.Show(strMsg,strTitle,MessageBoxButtons.OK,MessageBoxIcon.None,MessageBoxDefaultButton.Button1);
		}
	}
}
