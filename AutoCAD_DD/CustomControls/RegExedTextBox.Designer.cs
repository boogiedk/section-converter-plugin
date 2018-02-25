namespace CivilToolsGUI.CustomControls
{
    partial class RegExedTextBox
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb_TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_TextBox
            // 
            this.tb_TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_TextBox.Location = new System.Drawing.Point(0, 0);
            this.tb_TextBox.Name = "tb_TextBox";
            this.tb_TextBox.Size = new System.Drawing.Size(150, 20);
            this.tb_TextBox.TabIndex = 0;
            this.tb_TextBox.TextChanged += new System.EventHandler(this.tb_TextBox_TextChanged);
            this.tb_TextBox.Leave += new System.EventHandler(this.tb_TextBox_Leave);
            // 
            // RegExedTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tb_TextBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RegExedTextBox";
            this.Size = new System.Drawing.Size(150, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_TextBox;
    }
}
