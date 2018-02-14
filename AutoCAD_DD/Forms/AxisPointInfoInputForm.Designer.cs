namespace SectionConverterPlugin
{
    partial class AxisPointInfoInputForm
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
            this.btn_Ok = new System.Windows.Forms.Button();
            this.lbl_Description = new System.Windows.Forms.Label();
            this.tb_Description = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(83, 123);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 0;
            this.btn_Ok.Text = "OK";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // lbl_Description
            // 
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.Location = new System.Drawing.Point(13, 13);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(83, 13);
            this.lbl_Description.TabIndex = 1;
            this.lbl_Description.Text = "Введите текст:";
            // 
            // tb_Description
            // 
            this.tb_Description.Location = new System.Drawing.Point(16, 29);
            this.tb_Description.Multiline = true;
            this.tb_Description.Name = "tb_Description";
            this.tb_Description.Size = new System.Drawing.Size(218, 88);
            this.tb_Description.TabIndex = 2;
            this.tb_Description.Text = " ";
            // 
            // AxisPointInfoInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 158);
            this.Controls.Add(this.tb_Description);
            this.Controls.Add(this.lbl_Description);
            this.Controls.Add(this.btn_Ok);
            this.Name = "AxisPointInfoInputForm";
            this.Text = "Ввод информации о точке";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.TextBox tb_Description;
    }
}