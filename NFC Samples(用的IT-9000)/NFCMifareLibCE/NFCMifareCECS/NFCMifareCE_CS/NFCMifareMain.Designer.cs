namespace NFCMifareCE_CS
{
    partial class NFCMifareMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ComboBox2 = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.TextBox5 = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.TextBox4 = new System.Windows.Forms.TextBox();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.Label14 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox2
            // 
            this.ComboBox2.Items.Add("KEY_A");
            this.ComboBox2.Items.Add("KEY_B");
            this.ComboBox2.Location = new System.Drawing.Point(125, 113);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(60, 23);
            this.ComboBox2.TabIndex = 165;
            // 
            // Label5
            // 
            this.Label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label5.Location = new System.Drawing.Point(188, 98);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(50, 15);
            this.Label5.Text = "Block No.";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(191, 113);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(47, 23);
            this.TextBox1.TabIndex = 164;
            this.TextBox1.Text = "1";
            // 
            // TextBox5
            // 
            this.TextBox5.Location = new System.Drawing.Point(0, 113);
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.Size = new System.Drawing.Size(122, 23);
            this.TextBox5.TabIndex = 163;
            this.TextBox5.Text = "A0A1A2A3A4A5";
            // 
            // Label15
            // 
            this.Label15.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label15.Location = new System.Drawing.Point(0, 98);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(125, 15);
            this.Label15.Text = "Authentication Key (Hex)";
            // 
            // Label13
            // 
            this.Label13.Location = new System.Drawing.Point(176, 40);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(59, 20);
            // 
            // Label12
            // 
            this.Label12.Location = new System.Drawing.Point(121, 41);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(51, 20);
            this.Label12.Text = "UidLen :";
            // 
            // Label3
            // 
            this.Label3.Location = new System.Drawing.Point(50, 60);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(127, 21);
            // 
            // Label11
            // 
            this.Label11.Location = new System.Drawing.Point(2, 179);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(70, 15);
            this.Label11.Text = "Data (Hex)";
            // 
            // Label9
            // 
            this.Label9.Location = new System.Drawing.Point(0, 138);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(70, 15);
            this.Label9.Text = "Command";
            // 
            // Label8
            // 
            this.Label8.Location = new System.Drawing.Point(122, 138);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(56, 15);
            this.Label8.Text = "Block No.";
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new System.Drawing.Point(0, 195);
            this.TextBox4.Multiline = true;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new System.Drawing.Size(238, 40);
            this.TextBox4.TabIndex = 162;
            // 
            // ComboBox1
            // 
            this.ComboBox1.Items.Add("READ");
            this.ComboBox1.Items.Add("WRITE_Standard");
            this.ComboBox1.Items.Add("WRITE_Ultralight");
            this.ComboBox1.Location = new System.Drawing.Point(0, 155);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(118, 23);
            this.ComboBox1.TabIndex = 161;
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new System.Drawing.Point(122, 155);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(55, 23);
            this.TextBox3.TabIndex = 160;
            this.TextBox3.Text = "1";
            // 
            // TextBox2
            // 
            this.TextBox2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.TextBox2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.TextBox2.Location = new System.Drawing.Point(0, 237);
            this.TextBox2.Multiline = true;
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.ReadOnly = true;
            this.TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox2.Size = new System.Drawing.Size(238, 58);
            this.TextBox2.TabIndex = 159;
            this.TextBox2.TabStop = false;
            // 
            // Label7
            // 
            this.Label7.Location = new System.Drawing.Point(50, 78);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(158, 21);
            // 
            // Label6
            // 
            this.Label6.Location = new System.Drawing.Point(2, 78);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(51, 20);
            this.Label6.Text = "Uid :";
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(2, 60);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(68, 20);
            this.Label4.Text = "SAK :";
            // 
            // Label2
            // 
            this.Label2.Location = new System.Drawing.Point(50, 41);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(55, 19);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(2, 40);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(51, 20);
            this.Label1.Text = "ATQ :";
            // 
            // Button2
            // 
            this.Button2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Button2.Location = new System.Drawing.Point(168, 0);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(68, 40);
            this.Button2.TabIndex = 158;
            this.Button2.Text = "Exit";
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Button1.Location = new System.Drawing.Point(0, 0);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(159, 40);
            this.Button1.TabIndex = 157;
            this.Button1.Text = "NFC Start";
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label14
            // 
            this.Label14.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.Label14.Location = new System.Drawing.Point(126, 98);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(26, 14);
            this.Label14.Text = "Key";
            // 
            // NFCMifareMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.ComboBox2);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.TextBox5);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.TextBox4);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.TextBox3);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Label14);
            this.Name = "NFCMifareMain";
            this.Text = "NFCMifareCE_CS";
            this.Closed += new System.EventHandler(this.NFCMifareMain_Closed);
            this.Load += new System.EventHandler(this.NFCMifareMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ComboBox ComboBox2;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.TextBox TextBox5;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.TextBox TextBox4;
        internal System.Windows.Forms.ComboBox ComboBox1;
        internal System.Windows.Forms.TextBox TextBox3;
        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Label Label14;
    }
}

