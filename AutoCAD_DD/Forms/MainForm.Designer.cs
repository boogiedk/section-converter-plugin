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
            this.btn_createblock = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_createpoint = new System.Windows.Forms.Button();
            this.lbl_point = new System.Windows.Forms.Label();
            this.btn_execute = new System.Windows.Forms.Button();
            this.lbl_import = new System.Windows.Forms.Label();
            this.txb_fileName = new System.Windows.Forms.TextBox();
            this.txb_blockName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_createblock
            // 
            this.btn_createblock.Location = new System.Drawing.Point(168, 4);
            this.btn_createblock.Name = "btn_createblock";
            this.btn_createblock.Size = new System.Drawing.Size(75, 23);
            this.btn_createblock.TabIndex = 18;
            this.btn_createblock.Text = "Создать";
            this.btn_createblock.UseVisualStyleBackColor = true;
            this.btn_createblock.Click += new System.EventHandler(this.button3_Click);
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
            // btn_createpoint
            // 
            this.btn_createpoint.Location = new System.Drawing.Point(168, 33);
            this.btn_createpoint.Name = "btn_createpoint";
            this.btn_createpoint.Size = new System.Drawing.Size(75, 23);
            this.btn_createpoint.TabIndex = 20;
            this.btn_createpoint.Text = "Создать";
            this.btn_createpoint.UseVisualStyleBackColor = true;
            this.btn_createpoint.Click += new System.EventHandler(this.btn_createpoint_Click);
            // 
            // lbl_point
            // 
            this.lbl_point.AutoSize = true;
            this.lbl_point.Location = new System.Drawing.Point(11, 38);
            this.lbl_point.Name = "lbl_point";
            this.lbl_point.Size = new System.Drawing.Size(99, 13);
            this.lbl_point.TabIndex = 21;
            this.lbl_point.Text = "Создать отметку: ";
            // 
            // btn_execute
            // 
            this.btn_execute.Location = new System.Drawing.Point(168, 138);
            this.btn_execute.Name = "btn_execute";
            this.btn_execute.Size = new System.Drawing.Size(75, 23);
            this.btn_execute.TabIndex = 22;
            this.btn_execute.Text = "Выполнить";
            this.btn_execute.UseVisualStyleBackColor = true;
            this.btn_execute.Click += new System.EventHandler(this.btn_execute_Click);
            // 
            // lbl_import
            // 
            this.lbl_import.AutoSize = true;
            this.lbl_import.Location = new System.Drawing.Point(12, 67);
            this.lbl_import.Name = "lbl_import";
            this.lbl_import.Size = new System.Drawing.Size(150, 13);
            this.lbl_import.TabIndex = 23;
            this.lbl_import.Text = "Произвести импорт блоков:";
            // 
            // txb_fileName
            // 
            this.txb_fileName.Location = new System.Drawing.Point(143, 86);
            this.txb_fileName.Name = "txb_fileName";
            this.txb_fileName.Size = new System.Drawing.Size(100, 20);
            this.txb_fileName.TabIndex = 24;
            // 
            // txb_blockName
            // 
            this.txb_blockName.Location = new System.Drawing.Point(143, 112);
            this.txb_blockName.Name = "txb_blockName";
            this.txb_blockName.Size = new System.Drawing.Size(100, 20);
            this.txb_blockName.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Путь и имя файла:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Имя блока:";
            // 
            // PluginN1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 201);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txb_blockName);
            this.Controls.Add(this.txb_fileName);
            this.Controls.Add(this.lbl_import);
            this.Controls.Add(this.btn_execute);
            this.Controls.Add(this.lbl_point);
            this.Controls.Add(this.btn_createpoint);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btn_createblock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PluginN1";
            this.Text = "Plugin";
            this.Load += new System.EventHandler(this.PluginN1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_createblock;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btn_createpoint;
        private System.Windows.Forms.Label lbl_point;
        private System.Windows.Forms.Button btn_execute;
        private System.Windows.Forms.Label lbl_import;
        private System.Windows.Forms.TextBox txb_fileName;
        private System.Windows.Forms.TextBox txb_blockName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}