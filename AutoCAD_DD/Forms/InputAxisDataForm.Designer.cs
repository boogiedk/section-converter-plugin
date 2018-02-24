namespace SectionConverterPlugin.Forms
{
    partial class InputAxisDataForm
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
            this.maskedtb_main = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.firstDouble = new System.Windows.Forms.Label();
            this.secondDouble = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // maskedtb_main
            // 
            this.maskedtb_main.Location = new System.Drawing.Point(53, 12);
            this.maskedtb_main.Mask = "ПК_#99990+00,00";
            this.maskedtb_main.Name = "maskedtb_main";
            this.maskedtb_main.Size = new System.Drawing.Size(219, 20);
            this.maskedtb_main.TabIndex = 0;
            this.maskedtb_main.TextChanged += new System.EventHandler(this.maskedtb_main_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ввод:";
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(110, 109);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "ОК";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // firstDouble
            // 
            this.firstDouble.AutoSize = true;
            this.firstDouble.Location = new System.Drawing.Point(15, 64);
            this.firstDouble.Name = "firstDouble";
            this.firstDouble.Size = new System.Drawing.Size(13, 13);
            this.firstDouble.TabIndex = 3;
            this.firstDouble.Text = "0";
            // 
            // secondDouble
            // 
            this.secondDouble.AutoSize = true;
            this.secondDouble.Location = new System.Drawing.Point(218, 64);
            this.secondDouble.Name = "secondDouble";
            this.secondDouble.Size = new System.Drawing.Size(13, 13);
            this.secondDouble.TabIndex = 4;
            this.secondDouble.Text = "0";
            // 
            // InputAxisDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 144);
            this.Controls.Add(this.secondDouble);
            this.Controls.Add(this.firstDouble);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maskedtb_main);
            this.Name = "InputAxisDataForm";
            this.Text = "Введите информацию";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox maskedtb_main;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label firstDouble;
        private System.Windows.Forms.Label secondDouble;
    }
}