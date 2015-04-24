namespace Minesweeper
{
    partial class Form_Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Setting));
            this.Label_Width = new System.Windows.Forms.Label();
            this.NumericUpDown_Width = new System.Windows.Forms.NumericUpDown();
            this.NumericUpDown_Mine = new System.Windows.Forms.NumericUpDown();
            this.Label_Mine = new System.Windows.Forms.Label();
            this.NumericUpDown_Height = new System.Windows.Forms.NumericUpDown();
            this.Label_Height = new System.Windows.Forms.Label();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Mine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Height)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_Width
            // 
            this.Label_Width.AutoSize = true;
            this.Label_Width.Location = new System.Drawing.Point(12, 9);
            this.Label_Width.Name = "Label_Width";
            this.Label_Width.Size = new System.Drawing.Size(55, 15);
            this.Label_Width.TabIndex = 0;
            this.Label_Width.Text = "Width:";
            // 
            // NumericUpDown_Width
            // 
            this.NumericUpDown_Width.Location = new System.Drawing.Point(81, 7);
            this.NumericUpDown_Width.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.NumericUpDown_Width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown_Width.Name = "NumericUpDown_Width";
            this.NumericUpDown_Width.Size = new System.Drawing.Size(120, 25);
            this.NumericUpDown_Width.TabIndex = 1;
            this.NumericUpDown_Width.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumericUpDown_Width.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // NumericUpDown_Mine
            // 
            this.NumericUpDown_Mine.Location = new System.Drawing.Point(81, 69);
            this.NumericUpDown_Mine.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.NumericUpDown_Mine.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown_Mine.Name = "NumericUpDown_Mine";
            this.NumericUpDown_Mine.Size = new System.Drawing.Size(120, 25);
            this.NumericUpDown_Mine.TabIndex = 9;
            this.NumericUpDown_Mine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumericUpDown_Mine.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label_Mine
            // 
            this.Label_Mine.AutoSize = true;
            this.Label_Mine.Location = new System.Drawing.Point(12, 71);
            this.Label_Mine.Name = "Label_Mine";
            this.Label_Mine.Size = new System.Drawing.Size(47, 15);
            this.Label_Mine.TabIndex = 8;
            this.Label_Mine.Text = "Mine:";
            // 
            // NumericUpDown_Height
            // 
            this.NumericUpDown_Height.Location = new System.Drawing.Point(81, 38);
            this.NumericUpDown_Height.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.NumericUpDown_Height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDown_Height.Name = "NumericUpDown_Height";
            this.NumericUpDown_Height.Size = new System.Drawing.Size(120, 25);
            this.NumericUpDown_Height.TabIndex = 7;
            this.NumericUpDown_Height.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumericUpDown_Height.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label_Height
            // 
            this.Label_Height.AutoSize = true;
            this.Label_Height.Location = new System.Drawing.Point(12, 40);
            this.Label_Height.Name = "Label_Height";
            this.Label_Height.Size = new System.Drawing.Size(63, 15);
            this.Label_Height.TabIndex = 6;
            this.Label_Height.Text = "Height:";
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(15, 100);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 10;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(112, 100);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 11;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Form_Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(208, 132);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.NumericUpDown_Mine);
            this.Controls.Add(this.Label_Mine);
            this.Controls.Add(this.NumericUpDown_Height);
            this.Controls.Add(this.Label_Height);
            this.Controls.Add(this.NumericUpDown_Width);
            this.Controls.Add(this.Label_Width);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Minesweeper";
            this.Load += new System.EventHandler(this.Form_Setting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Mine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Width;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Width;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Mine;
        private System.Windows.Forms.Label Label_Mine;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Height;
        private System.Windows.Forms.Label Label_Height;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
    }
}