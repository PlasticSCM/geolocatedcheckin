namespace geolocation
{
    partial class AskNewLocationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AskNewLocationForm));
            this.InfoPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.CoordsPanel = new System.Windows.Forms.Panel();
            this.AddressTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LongitudeTextbox = new System.Windows.Forms.TextBox();
            this.LatitudeTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.OkButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.NewNameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.InfoPanel.SuspendLayout();
            this.CoordsPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfoPanel
            // 
            this.InfoPanel.BackColor = System.Drawing.Color.White;
            this.InfoPanel.Controls.Add(this.label1);
            this.InfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.InfoPanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.InfoPanel.Location = new System.Drawing.Point(0, 0);
            this.InfoPanel.Name = "InfoPanel";
            this.InfoPanel.Size = new System.Drawing.Size(549, 48);
            this.InfoPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "This location is not close to any previous location you used before.";
            // 
            // CoordsPanel
            // 
            this.CoordsPanel.Controls.Add(this.AddressTextbox);
            this.CoordsPanel.Controls.Add(this.label4);
            this.CoordsPanel.Controls.Add(this.LongitudeTextbox);
            this.CoordsPanel.Controls.Add(this.LatitudeTextbox);
            this.CoordsPanel.Controls.Add(this.label3);
            this.CoordsPanel.Controls.Add(this.label2);
            this.CoordsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CoordsPanel.Location = new System.Drawing.Point(0, 48);
            this.CoordsPanel.Name = "CoordsPanel";
            this.CoordsPanel.Size = new System.Drawing.Size(549, 89);
            this.CoordsPanel.TabIndex = 1;
            // 
            // AddressTextbox
            // 
            this.AddressTextbox.Location = new System.Drawing.Point(78, 48);
            this.AddressTextbox.Name = "AddressTextbox";
            this.AddressTextbox.ReadOnly = true;
            this.AddressTextbox.Size = new System.Drawing.Size(448, 23);
            this.AddressTextbox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Address";
            // 
            // LongitudeTextbox
            // 
            this.LongitudeTextbox.Location = new System.Drawing.Point(350, 15);
            this.LongitudeTextbox.Name = "LongitudeTextbox";
            this.LongitudeTextbox.ReadOnly = true;
            this.LongitudeTextbox.Size = new System.Drawing.Size(177, 23);
            this.LongitudeTextbox.TabIndex = 3;
            // 
            // LatitudeTextbox
            // 
            this.LatitudeTextbox.Location = new System.Drawing.Point(78, 15);
            this.LatitudeTextbox.Name = "LatitudeTextbox";
            this.LatitudeTextbox.ReadOnly = true;
            this.LatitudeTextbox.Size = new System.Drawing.Size(177, 23);
            this.LatitudeTextbox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Longitude";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Latitude";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.OkButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 215);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 47);
            this.panel1.TabIndex = 3;
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(440, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(87, 27);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.NewNameTextBox);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 137);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(549, 78);
            this.panel2.TabIndex = 2;
            // 
            // NewNameTextBox
            // 
            this.NewNameTextBox.Location = new System.Drawing.Point(22, 36);
            this.NewNameTextBox.Name = "NewNameTextBox";
            this.NewNameTextBox.Size = new System.Drawing.Size(504, 23);
            this.NewNameTextBox.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(226, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Enter a meaningful name for this location";
            // 
            // AskNewLocationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 262);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CoordsPanel);
            this.Controls.Add(this.InfoPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AskNewLocationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Geolocated checkins - enter a name for this location";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.AskNewLocationForm_Activated);
            this.InfoPanel.ResumeLayout(false);
            this.InfoPanel.PerformLayout();
            this.CoordsPanel.ResumeLayout(false);
            this.CoordsPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel InfoPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel CoordsPanel;
        private System.Windows.Forms.TextBox LongitudeTextbox;
        private System.Windows.Forms.TextBox LatitudeTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox AddressTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox NewNameTextBox;
        private System.Windows.Forms.Label label5;
    }
}