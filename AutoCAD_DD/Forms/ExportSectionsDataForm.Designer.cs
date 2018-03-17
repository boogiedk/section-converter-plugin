namespace SectionConverterPlugin.Forms
{
    partial class ExportSectionsDataForm
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
            this.retb__FactPosNoizeLowerBound = new SectionConverterPlugin.CustomControls.RegExedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_mistake = new System.Windows.Forms.Label();
            this.cb_ActualValueMistake = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_ExportPoints = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.retb__FactPosNoizeUpperBound = new SectionConverterPlugin.CustomControls.RegExedTextBox();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // retb__FactPosNoizeLowerBound
            // 
            this.retb__FactPosNoizeLowerBound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.retb__FactPosNoizeLowerBound.Enabled = false;
            this.retb__FactPosNoizeLowerBound.Location = new System.Drawing.Point(180, 63);
            this.retb__FactPosNoizeLowerBound.Margin = new System.Windows.Forms.Padding(0);
            this.retb__FactPosNoizeLowerBound.MatchOnKeyInput = false;
            this.retb__FactPosNoizeLowerBound.Name = "retb__FactPosNoizeLowerBound";
            this.retb__FactPosNoizeLowerBound.Silent = true;
            this.retb__FactPosNoizeLowerBound.Size = new System.Drawing.Size(88, 24);
            this.retb__FactPosNoizeLowerBound.TabIndex = 0;
            this.retb__FactPosNoizeLowerBound.Value = null;
            this.retb__FactPosNoizeLowerBound.ValueChanged += new System.EventHandler(this.retb__FactPosNoizeLowerBound_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.Location = new System.Drawing.Point(33, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Нижняя граница";
            // 
            // label_mistake
            // 
            this.label_mistake.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label_mistake.AutoSize = true;
            this.label_mistake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label_mistake.Location = new System.Drawing.Point(33, 7);
            this.label_mistake.Name = "label_mistake";
            this.label_mistake.Size = new System.Drawing.Size(91, 15);
            this.label_mistake.TabIndex = 3;
            this.label_mistake.Text = "Генерировать";
            // 
            // cb_ActualValueMistake
            // 
            this.cb_ActualValueMistake.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_ActualValueMistake.AutoSize = true;
            this.cb_ActualValueMistake.Location = new System.Drawing.Point(183, 8);
            this.cb_ActualValueMistake.Name = "cb_ActualValueMistake";
            this.cb_ActualValueMistake.Size = new System.Drawing.Size(15, 14);
            this.cb_ActualValueMistake.TabIndex = 2;
            this.cb_ActualValueMistake.UseVisualStyleBackColor = true;
            this.cb_ActualValueMistake.CheckedChanged += new System.EventHandler(this.cb_FactValueMistake_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "м";
            // 
            // btn_ExportPoints
            // 
            this.btn_ExportPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_ExportPoints.Location = new System.Drawing.Point(3, 175);
            this.btn_ExportPoints.Name = "btn_ExportPoints";
            this.btn_ExportPoints.Size = new System.Drawing.Size(298, 24);
            this.btn_ExportPoints.TabIndex = 7;
            this.btn_ExportPoints.Text = "Экспорт";
            this.btn_ExportPoints.UseVisualStyleBackColor = true;
            this.btn_ExportPoints.Click += new System.EventHandler(this.btn_ExportPoints_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(33, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Верхняя граница";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Фактические значения:";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.btn_ExportPoints, 0, 2);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(304, 202);
            this.tableLayoutPanel5.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.cb_ActualValueMistake, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_mistake, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.retb__FactPosNoizeLowerBound, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.retb__FactPosNoizeUpperBound, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(298, 136);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(271, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "м";
            // 
            // retb__FactPosNoizeUpperBound
            // 
            this.retb__FactPosNoizeUpperBound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.retb__FactPosNoizeUpperBound.Enabled = false;
            this.retb__FactPosNoizeUpperBound.Location = new System.Drawing.Point(180, 31);
            this.retb__FactPosNoizeUpperBound.Margin = new System.Windows.Forms.Padding(0);
            this.retb__FactPosNoizeUpperBound.MatchOnKeyInput = false;
            this.retb__FactPosNoizeUpperBound.Name = "retb__FactPosNoizeUpperBound";
            this.retb__FactPosNoizeUpperBound.Silent = true;
            this.retb__FactPosNoizeUpperBound.Size = new System.Drawing.Size(88, 28);
            this.retb__FactPosNoizeUpperBound.TabIndex = 0;
            this.retb__FactPosNoizeUpperBound.Value = null;
            this.retb__FactPosNoizeUpperBound.ValueChanged += new System.EventHandler(this.retb__FactPosNoizeUpperBound_ValueChanged);
            // 
            // ExportSectionsDataForm
            // 
            this.AcceptButton = this.btn_ExportPoints;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 202);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportSectionsDataForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Экспорт сечений";
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.RegExedTextBox retb__FactPosNoizeLowerBound;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_mistake;
        private System.Windows.Forms.CheckBox cb_ActualValueMistake;
        private System.Windows.Forms.Button btn_ExportPoints;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private CustomControls.RegExedTextBox retb__FactPosNoizeUpperBound;
    }
}