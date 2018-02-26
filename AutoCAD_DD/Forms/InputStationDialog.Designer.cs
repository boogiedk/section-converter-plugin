namespace SectionConverterPlugin.Forms
{
    partial class InputStationDialog
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
            this.lbl_prefix = new System.Windows.Forms.Label();
            this.lbl_plus = new System.Windows.Forms.Label();
            this.lbl_input = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.retb_firstDouble = new CivilToolsGUI.CustomControls.RegExedTextBox();
            this.retb_secondDouble = new CivilToolsGUI.CustomControls.RegExedTextBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_prefix
            // 
            this.lbl_prefix.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_prefix.AutoSize = true;
            this.lbl_prefix.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_prefix.Location = new System.Drawing.Point(3, 7);
            this.lbl_prefix.Name = "lbl_prefix";
            this.lbl_prefix.Size = new System.Drawing.Size(26, 16);
            this.lbl_prefix.TabIndex = 1;
            this.lbl_prefix.Text = "ПК";
            // 
            // lbl_plus
            // 
            this.lbl_plus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_plus.AutoSize = true;
            this.lbl_plus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_plus.Location = new System.Drawing.Point(143, 7);
            this.lbl_plus.Name = "lbl_plus";
            this.lbl_plus.Size = new System.Drawing.Size(14, 16);
            this.lbl_plus.TabIndex = 5;
            this.lbl_plus.Text = "+";
            // 
            // lbl_input
            // 
            this.lbl_input.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_input.AutoSize = true;
            this.lbl_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_input.Location = new System.Drawing.Point(3, 7);
            this.lbl_input.Name = "lbl_input";
            this.lbl_input.Size = new System.Drawing.Size(108, 16);
            this.lbl_input.TabIndex = 6;
            this.lbl_input.Text = "Введите пикет:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.retb_firstDouble, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_prefix, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_plus, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.retb_secondDouble, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(298, 24);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // retb_firstDouble
            // 
            this.retb_firstDouble.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.retb_firstDouble.Enabled = false;
            this.retb_firstDouble.Location = new System.Drawing.Point(40, 5);
            this.retb_firstDouble.Margin = new System.Windows.Forms.Padding(0);
            this.retb_firstDouble.MatchOnKeyInput = false;
            this.retb_firstDouble.Name = "retb_firstDouble";
            this.retb_firstDouble.Size = new System.Drawing.Size(100, 20);
            this.retb_firstDouble.TabIndex = 7;
            this.retb_firstDouble.Value = null;
            this.retb_firstDouble.ValueChanged += new System.EventHandler(this.regExedTb_NumberOne_ValueChanged);
            // 
            // retb_secondDouble
            // 
            this.retb_secondDouble.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.retb_secondDouble.Enabled = false;
            this.retb_secondDouble.Location = new System.Drawing.Point(160, 5);
            this.retb_secondDouble.Margin = new System.Windows.Forms.Padding(0);
            this.retb_secondDouble.MatchOnKeyInput = false;
            this.retb_secondDouble.Name = "retb_secondDouble";
            this.retb_secondDouble.Size = new System.Drawing.Size(100, 20);
            this.retb_secondDouble.TabIndex = 8;
            this.retb_secondDouble.Value = null;
            this.retb_secondDouble.ValueChanged += new System.EventHandler(this.regExedTb_NumberTwo_ValueChanged);
            // 
            // btn_Ok
            // 
            this.btn_Ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Ok.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Ok.Location = new System.Drawing.Point(3, 175);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(298, 24);
            this.btn_Ok.TabIndex = 2;
            this.btn_Ok.Text = "ОК";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.btn_Ok, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lbl_input, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(304, 202);
            this.tableLayoutPanel3.TabIndex = 11;
            // 
            // InputStationDialog
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 202);
            this.Controls.Add(this.tableLayoutPanel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputStationDialog";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RightToLeftLayout = true;
            this.Text = "Ввод информации";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_prefix;
        private System.Windows.Forms.Label lbl_plus;
        private System.Windows.Forms.Label lbl_input;
        private CivilToolsGUI.CustomControls.RegExedTextBox retb_firstDouble;
        private CivilToolsGUI.CustomControls.RegExedTextBox retb_secondDouble;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}