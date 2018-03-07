namespace SectionConverterPlugin.Forms
{
    partial class InputSizeForWindowDialog
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
            this.retb_heightWindow = new CivilToolsGUI.CustomControls.RegExedTextBox();
            this.retb_widthWindow = new CivilToolsGUI.CustomControls.RegExedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // retb_heightWindow
            // 
            this.retb_heightWindow.Enabled = false;
            this.retb_heightWindow.Location = new System.Drawing.Point(66, 27);
            this.retb_heightWindow.Margin = new System.Windows.Forms.Padding(0);
            this.retb_heightWindow.MatchOnKeyInput = false;
            this.retb_heightWindow.Name = "retb_heightWindow";
            this.retb_heightWindow.Silent = true;
            this.retb_heightWindow.Size = new System.Drawing.Size(114, 20);
            this.retb_heightWindow.TabIndex = 0;
            this.retb_heightWindow.Value = null;
            this.retb_heightWindow.ValueChanged += new System.EventHandler(this.retb_heightWindow_ValueChanged);
            // 
            // retb_widthWindow
            // 
            this.retb_widthWindow.Enabled = false;
            this.retb_widthWindow.Location = new System.Drawing.Point(64, 71);
            this.retb_widthWindow.Margin = new System.Windows.Forms.Padding(0);
            this.retb_widthWindow.MatchOnKeyInput = false;
            this.retb_widthWindow.Name = "retb_widthWindow";
            this.retb_widthWindow.Silent = true;
            this.retb_widthWindow.Size = new System.Drawing.Size(114, 20);
            this.retb_widthWindow.TabIndex = 1;
            this.retb_widthWindow.Value = null;
            this.retb_widthWindow.ValueChanged += new System.EventHandler(this.retb_widthtWindow_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Высота: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ширина:";
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(103, 167);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 4;
            this.btn_Ok.Text = "Ок";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // InputSizeForWindow
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 202);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.retb_widthWindow);
            this.Controls.Add(this.retb_heightWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputSizeForWindow";
            this.ShowIcon = false;
            this.Text = "Ввод информации";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CivilToolsGUI.CustomControls.RegExedTextBox retb_heightWindow;
        private CivilToolsGUI.CustomControls.RegExedTextBox retb_widthWindow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Ok;
    }
}