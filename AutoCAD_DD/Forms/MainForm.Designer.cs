namespace SectionConverterPlugin
{
    partial class PluginN1
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
            this.label9 = new System.Windows.Forms.Label();
            this.btn_execute = new System.Windows.Forms.Button();
            this.btn_Height = new System.Windows.Forms.Button();
            this.btn_Bottom = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(150, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Создать блок с описанием: ";
            // 
            // btn_execute
            // 
            this.btn_execute.Location = new System.Drawing.Point(168, 4);
            this.btn_execute.Name = "btn_execute";
            this.btn_execute.Size = new System.Drawing.Size(75, 23);
            this.btn_execute.TabIndex = 22;
            this.btn_execute.Text = "Создать";
            this.btn_execute.UseVisualStyleBackColor = true;
            this.btn_execute.Click += new System.EventHandler(this.btn_execute_Click);
            // 
            // btn_Height
            // 
            this.btn_Height.Location = new System.Drawing.Point(2, 188);
            this.btn_Height.Name = "btn_Height";
            this.btn_Height.Size = new System.Drawing.Size(75, 23);
            this.btn_Height.TabIndex = 30;
            this.btn_Height.Text = "Point Height";
            this.btn_Height.UseVisualStyleBackColor = true;
            this.btn_Height.Click += new System.EventHandler(this.btn_Height_Click);
            // 
            // btn_Bottom
            // 
            this.btn_Bottom.Location = new System.Drawing.Point(83, 188);
            this.btn_Bottom.Name = "btn_Bottom";
            this.btn_Bottom.Size = new System.Drawing.Size(75, 23);
            this.btn_Bottom.TabIndex = 31;
            this.btn_Bottom.Text = "Point Bottom";
            this.btn_Bottom.UseVisualStyleBackColor = true;
            this.btn_Bottom.Click += new System.EventHandler(this.btn_Bottom_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(164, 188);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 32;
            this.button3.Text = "Point 3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(168, 113);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(75, 23);
            this.btn_update.TabIndex = 33;
            this.btn_update.Text = "Обновить";
            this.btn_update.UseVisualStyleBackColor = true;
            // 
            // PluginN1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 270);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btn_Bottom);
            this.Controls.Add(this.btn_Height);
            this.Controls.Add(this.btn_execute);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PluginN1";
            this.Text = "Plugin";
            this.Load += new System.EventHandler(this.PluginN1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_execute;
        private System.Windows.Forms.Button btn_Height;
        private System.Windows.Forms.Button btn_Bottom;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btn_update;
    }
}