namespace IntimacyAnalyse
{
    partial class ShowForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nClassIntiDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.classIntiDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.intiShowPictureBox = new System.Windows.Forms.PictureBox();
            this.choseButton = new System.Windows.Forms.Button();
            this.studentIDComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nClassIntiDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.classIntiDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intiShowPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nClassIntiDataGridView);
            this.groupBox3.Location = new System.Drawing.Point(16, 277);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(457, 229);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "非本班同学";
            // 
            // nClassIntiDataGridView
            // 
            this.nClassIntiDataGridView.AllowUserToOrderColumns = true;
            this.nClassIntiDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.nClassIntiDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nClassIntiDataGridView.Location = new System.Drawing.Point(7, 18);
            this.nClassIntiDataGridView.Name = "nClassIntiDataGridView";
            this.nClassIntiDataGridView.RowTemplate.Height = 23;
            this.nClassIntiDataGridView.Size = new System.Drawing.Size(444, 203);
            this.nClassIntiDataGridView.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.classIntiDataGridView);
            this.groupBox2.Location = new System.Drawing.Point(16, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(457, 229);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "本班同学";
            // 
            // classIntiDataGridView
            // 
            this.classIntiDataGridView.AllowUserToOrderColumns = true;
            this.classIntiDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.classIntiDataGridView.Cursor = System.Windows.Forms.Cursors.Default;
            this.classIntiDataGridView.Location = new System.Drawing.Point(6, 16);
            this.classIntiDataGridView.Name = "classIntiDataGridView";
            this.classIntiDataGridView.RowTemplate.Height = 23;
            this.classIntiDataGridView.Size = new System.Drawing.Size(445, 207);
            this.classIntiDataGridView.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.intiShowPictureBox);
            this.groupBox1.Location = new System.Drawing.Point(479, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(669, 464);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "亲密度展示";
            // 
            // intiShowPictureBox
            // 
            this.intiShowPictureBox.Location = new System.Drawing.Point(6, 20);
            this.intiShowPictureBox.Name = "intiShowPictureBox";
            this.intiShowPictureBox.Size = new System.Drawing.Size(657, 436);
            this.intiShowPictureBox.TabIndex = 2;
            this.intiShowPictureBox.TabStop = false;
            // 
            // choseButton
            // 
            this.choseButton.Location = new System.Drawing.Point(160, 14);
            this.choseButton.Name = "choseButton";
            this.choseButton.Size = new System.Drawing.Size(75, 23);
            this.choseButton.TabIndex = 11;
            this.choseButton.Text = "选择";
            this.choseButton.UseVisualStyleBackColor = true;
            this.choseButton.Click += new System.EventHandler(this.choseButton_Click);
            // 
            // studentIDComboBox
            // 
            this.studentIDComboBox.FormattingEnabled = true;
            this.studentIDComboBox.Location = new System.Drawing.Point(16, 14);
            this.studentIDComboBox.Name = "studentIDComboBox";
            this.studentIDComboBox.Size = new System.Drawing.Size(121, 20);
            this.studentIDComboBox.TabIndex = 10;
            this.studentIDComboBox.Text = "请选择学号";
            // 
            // ShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 520);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.choseButton);
            this.Controls.Add(this.studentIDComboBox);
            this.Name = "ShowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图形化展现";
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nClassIntiDataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.classIntiDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intiShowPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView nClassIntiDataGridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView classIntiDataGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox intiShowPictureBox;
        private System.Windows.Forms.Button choseButton;
        private System.Windows.Forms.ComboBox studentIDComboBox;
    }
}