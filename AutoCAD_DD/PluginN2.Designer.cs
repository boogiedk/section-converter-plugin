namespace AutoCAD_DD
{
    partial class PluginN2
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
            this.button_done = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDesc2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_done
            // 
            this.button_done.Location = new System.Drawing.Point(83, 123);
            this.button_done.Name = "button_done";
            this.button_done.Size = new System.Drawing.Size(75, 23);
            this.button_done.TabIndex = 0;
            this.button_done.Text = "Готово";
            this.button_done.UseVisualStyleBackColor = true;
            this.button_done.Click += new System.EventHandler(this.button_done_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Введите текст:";
            // 
            // textBoxDesc2
            // 
            this.textBoxDesc2.Location = new System.Drawing.Point(16, 29);
            this.textBoxDesc2.Multiline = true;
            this.textBoxDesc2.Name = "textBoxDesc2";
            this.textBoxDesc2.Size = new System.Drawing.Size(218, 88);
            this.textBoxDesc2.TabIndex = 2;
            // 
            // PluginN2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 158);
            this.Controls.Add(this.textBoxDesc2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_done);
            this.Name = "PluginN2";
            this.Text = "PluginN2";
            this.Load += new System.EventHandler(this.PluginN2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_done;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDesc2;
    }
}