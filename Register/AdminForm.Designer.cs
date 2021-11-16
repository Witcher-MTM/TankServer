
namespace WinFormsApp1
{
    partial class AdminForm
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
            this.ListMms = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.FilterBtn = new System.Windows.Forms.Button();
            this.DataFilterBtn = new System.Windows.Forms.Button();
            this.FilterByName = new System.Windows.Forms.Button();
            this.textBoxByName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ListMms
            // 
            this.ListMms.FormattingEnabled = true;
            this.ListMms.ItemHeight = 15;
            this.ListMms.Location = new System.Drawing.Point(460, 95);
            this.ListMms.Name = "ListMms";
            this.ListMms.Size = new System.Drawing.Size(328, 319);
            this.ListMms.TabIndex = 0;
            this.ListMms.SelectedIndexChanged += new System.EventHandler(this.ListMms_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 140);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox1.Size = new System.Drawing.Size(441, 274);
            this.textBox1.TabIndex = 3;
            // 
            // FilterBtn
            // 
            this.FilterBtn.Location = new System.Drawing.Point(13, 13);
            this.FilterBtn.Name = "FilterBtn";
            this.FilterBtn.Size = new System.Drawing.Size(75, 23);
            this.FilterBtn.TabIndex = 4;
            this.FilterBtn.Text = "Filter";
            this.FilterBtn.UseVisualStyleBackColor = true;
            this.FilterBtn.Click += new System.EventHandler(this.FilterBtn_Click);
            // 
            // DataFilterBtn
            // 
            this.DataFilterBtn.Location = new System.Drawing.Point(13, 43);
            this.DataFilterBtn.Name = "DataFilterBtn";
            this.DataFilterBtn.Size = new System.Drawing.Size(105, 23);
            this.DataFilterBtn.TabIndex = 5;
            this.DataFilterBtn.Text = "Filter by Data";
            this.DataFilterBtn.UseVisualStyleBackColor = true;
            this.DataFilterBtn.Visible = false;
            this.DataFilterBtn.Click += new System.EventHandler(this.DataFilterBtn_Click);
            // 
            // FilterByName
            // 
            this.FilterByName.Location = new System.Drawing.Point(124, 43);
            this.FilterByName.Name = "FilterByName";
            this.FilterByName.Size = new System.Drawing.Size(105, 23);
            this.FilterByName.TabIndex = 6;
            this.FilterByName.Text = "Filter by Name";
            this.FilterByName.UseVisualStyleBackColor = true;
            this.FilterByName.Visible = false;
            this.FilterByName.Click += new System.EventHandler(this.FilterByName_Click);
            // 
            // textBoxByName
            // 
            this.textBoxByName.Location = new System.Drawing.Point(92, 72);
            this.textBoxByName.Name = "textBoxByName";
            this.textBoxByName.Size = new System.Drawing.Size(172, 23);
            this.textBoxByName.TabIndex = 7;
            this.textBoxByName.Text = "Enter from who ";
            this.textBoxByName.Visible = false;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBoxByName);
            this.Controls.Add(this.FilterByName);
            this.Controls.Add(this.DataFilterBtn);
            this.Controls.Add(this.FilterBtn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ListMms);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListMms;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button FilterBtn;
        private System.Windows.Forms.Button DataFilterBtn;
        private System.Windows.Forms.Button FilterByName;
        private System.Windows.Forms.TextBox textBoxByName;
    }
}