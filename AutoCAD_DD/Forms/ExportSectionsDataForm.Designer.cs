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
            this.retb_ActuallyValue = new SectionConverterPlugin.CustomControls.RegExedTextBox();
            this.btn_Run = new System.Windows.Forms.Button();
            this.cb_ActualValueMistake = new System.Windows.Forms.CheckBox();
            this.label_mistake = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // retb_ActuallyValue
            // 
            this.retb_ActuallyValue.Enabled = false;
            this.retb_ActuallyValue.Location = new System.Drawing.Point(199, 0);
            this.retb_ActuallyValue.Margin = new System.Windows.Forms.Padding(0);
            this.retb_ActuallyValue.MatchOnKeyInput = false;
            this.retb_ActuallyValue.Name = "retb_ActuallyValue";
            this.retb_ActuallyValue.Silent = true;
            this.retb_ActuallyValue.Size = new System.Drawing.Size(101, 20);
            this.retb_ActuallyValue.TabIndex = 0;
            this.retb_ActuallyValue.Value = null;
            this.retb_ActuallyValue.ValueChanged += new System.EventHandler(this.retb_ActuallyValue_ValueChanged);
            // 
            // btn_Run
            // 
            this.btn_Run.Location = new System.Drawing.Point(3, 3);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(294, 27);
            this.btn_Run.TabIndex = 1;
            this.btn_Run.Text = "Run";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // cb_ActualValueMistake
            // 
            this.cb_ActualValueMistake.AutoSize = true;
            this.cb_ActualValueMistake.Location = new System.Drawing.Point(202, 82);
            this.cb_ActualValueMistake.Name = "cb_ActualValueMistake";
            this.cb_ActualValueMistake.Size = new System.Drawing.Size(15, 14);
            this.cb_ActualValueMistake.TabIndex = 2;
            this.cb_ActualValueMistake.UseVisualStyleBackColor = true;
            // 
            // label_mistake
            // 
            this.label_mistake.AutoSize = true;
            this.label_mistake.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_mistake.Location = new System.Drawing.Point(3, 79);
            this.label_mistake.Name = "label_mistake";
            this.label_mistake.Size = new System.Drawing.Size(156, 16);
            this.label_mistake.TabIndex = 3;
            this.label_mistake.Text = "Учитывать ошибку Ф.З";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ошибка фактических значений";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.cb_ActualValueMistake, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_mistake, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 239);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btn_Run, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 257);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(300, 33);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // ANewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 302);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ANewForm";
            this.Text = "Создание документации";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SectionConverterPlugin.CustomControls.RegExedTextBox retb_ActuallyValue;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.CheckBox cb_ActualValueMistake;
        private System.Windows.Forms.Label label_mistake;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}