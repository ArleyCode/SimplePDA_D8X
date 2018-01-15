namespace BTLibSampleCECS
{
    partial class BTLibSampleCECS
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
            this.components = new System.ComponentModel.Container();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.TextBox();
            this.ListView1 = new System.Windows.Forms.ListView();
            this.SerialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.Button3 = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(-1, 219);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(115, 30);
            this.Button2.TabIndex = 41;
            this.Button2.Text = "Send";
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(-1, 159);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(115, 30);
            this.Button1.TabIndex = 42;
            this.Button1.Text = "Find BT Devices";
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Status
            // 
            this.Status.Enabled = false;
            this.Status.Location = new System.Drawing.Point(-1, 254);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(240, 23);
            this.Status.TabIndex = 43;
            // 
            // ListView1
            // 
            this.ListView1.Location = new System.Drawing.Point(-1, 28);
            this.ListView1.Name = "ListView1";
            this.ListView1.Size = new System.Drawing.Size(240, 125);
            this.ListView1.TabIndex = 44;
            // 
            // SerialPort1
            // 
            this.SerialPort1.PortName = "COM6";
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(124, 219);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(115, 30);
            this.Button3.TabIndex = 45;
            this.Button3.Text = "Exit";
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Label1
            // 
            this.Label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.Label1.Location = new System.Drawing.Point(15, 4);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(208, 24);
            this.Label1.Text = "Bluetooth Devices list";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(-1, 194);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(240, 23);
            this.TextBox1.TabIndex = 39;
            // 
            // Button5
            // 
            this.Button5.Location = new System.Drawing.Point(124, 159);
            this.Button5.Name = "Button5";
            this.Button5.Size = new System.Drawing.Size(115, 30);
            this.Button5.TabIndex = 40;
            this.Button5.Text = "Set Default device";
            this.Button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // BTLibSampleCECS
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.ListView1);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Button5);
            this.Name = "BTLibSampleCECS";
            this.Text = "BTLibSampleCECS";
            this.Load += new System.EventHandler(this.BTLibSampleCECS_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox Status;
        internal System.Windows.Forms.ListView ListView1;
        internal System.IO.Ports.SerialPort SerialPort1;
        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Button Button5;
    }
}

