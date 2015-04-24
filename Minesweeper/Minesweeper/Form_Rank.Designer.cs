namespace Minesweeper
{
    partial class Form_Rank
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Rank));
            this.Label_Beginer = new System.Windows.Forms.Label();
            this.Label_Intermediate = new System.Windows.Forms.Label();
            this.Label_Expert = new System.Windows.Forms.Label();
            this.Button_Reset = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Label_Beginer
            // 
            this.Label_Beginer.AutoSize = true;
            this.Label_Beginer.Location = new System.Drawing.Point(12, 9);
            this.Label_Beginer.Name = "Label_Beginer";
            this.Label_Beginer.Size = new System.Drawing.Size(71, 15);
            this.Label_Beginer.TabIndex = 0;
            this.Label_Beginer.Text = "Beginner";
            // 
            // Label_Intermediate
            // 
            this.Label_Intermediate.AutoSize = true;
            this.Label_Intermediate.Location = new System.Drawing.Point(12, 42);
            this.Label_Intermediate.Name = "Label_Intermediate";
            this.Label_Intermediate.Size = new System.Drawing.Size(103, 15);
            this.Label_Intermediate.TabIndex = 1;
            this.Label_Intermediate.Text = "Intermediate";
            // 
            // Label_Expert
            // 
            this.Label_Expert.AutoSize = true;
            this.Label_Expert.Location = new System.Drawing.Point(12, 74);
            this.Label_Expert.Name = "Label_Expert";
            this.Label_Expert.Size = new System.Drawing.Size(55, 15);
            this.Label_Expert.TabIndex = 2;
            this.Label_Expert.Text = "Expert";
            // 
            // Button_Reset
            // 
            this.Button_Reset.Location = new System.Drawing.Point(30, 104);
            this.Button_Reset.Name = "Button_Reset";
            this.Button_Reset.Size = new System.Drawing.Size(75, 23);
            this.Button_Reset.TabIndex = 3;
            this.Button_Reset.Text = "Reset";
            this.Button_Reset.UseVisualStyleBackColor = true;
            this.Button_Reset.Click += new System.EventHandler(this.Button_Reset_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(146, 104);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 4;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Form_Rank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(247, 139);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Button_Reset);
            this.Controls.Add(this.Label_Expert);
            this.Controls.Add(this.Label_Intermediate);
            this.Controls.Add(this.Label_Beginer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_Rank";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Minesweeper";
            this.Load += new System.EventHandler(this.Form_Rank_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Beginer;
        private System.Windows.Forms.Label Label_Intermediate;
        private System.Windows.Forms.Label Label_Expert;
        private System.Windows.Forms.Button Button_Reset;
        private System.Windows.Forms.Button Button_OK;
    }
}