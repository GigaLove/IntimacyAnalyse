namespace IntimacyAnalyse
{
    partial class GraphShowForm
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
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("");
            this.nonClassmateListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.classmateListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.choseButton = new System.Windows.Forms.Button();
            this.studentIDComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // nonClassmateListView
            // 
            this.nonClassmateListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.nonClassmateListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3});
            this.nonClassmateListView.Location = new System.Drawing.Point(208, 64);
            this.nonClassmateListView.Name = "nonClassmateListView";
            this.nonClassmateListView.Size = new System.Drawing.Size(177, 323);
            this.nonClassmateListView.TabIndex = 12;
            this.nonClassmateListView.UseCompatibleStateImageBehavior = false;
            this.nonClassmateListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "非本班同学";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "亲密度";
            this.columnHeader4.Width = 50;
            // 
            // classmateListView
            // 
            this.classmateListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.classmateListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4});
            this.classmateListView.Location = new System.Drawing.Point(40, 64);
            this.classmateListView.Name = "classmateListView";
            this.classmateListView.Size = new System.Drawing.Size(150, 323);
            this.classmateListView.TabIndex = 11;
            this.classmateListView.UseCompatibleStateImageBehavior = false;
            this.classmateListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "本班同学姓名";
            this.columnHeader1.Width = 88;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "亲密度";
            this.columnHeader2.Width = 57;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Location = new System.Drawing.Point(403, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 447);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "显示";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(580, 421);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // choseButton
            // 
            this.choseButton.Location = new System.Drawing.Point(208, 35);
            this.choseButton.Name = "choseButton";
            this.choseButton.Size = new System.Drawing.Size(75, 23);
            this.choseButton.TabIndex = 9;
            this.choseButton.Text = "选择";
            this.choseButton.UseVisualStyleBackColor = true;
            this.choseButton.Click += new System.EventHandler(this.choseButton_Click);
            // 
            // studentIDComboBox
            // 
            this.studentIDComboBox.FormattingEnabled = true;
            this.studentIDComboBox.Location = new System.Drawing.Point(40, 37);
            this.studentIDComboBox.Name = "studentIDComboBox";
            this.studentIDComboBox.Size = new System.Drawing.Size(150, 20);
            this.studentIDComboBox.TabIndex = 8;
            this.studentIDComboBox.Text = "请选择学生姓名";
            // 
            // GraphShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 492);
            this.Controls.Add(this.nonClassmateListView);
            this.Controls.Add(this.classmateListView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.choseButton);
            this.Controls.Add(this.studentIDComboBox);
            this.Name = "GraphShowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "亲密度分析图形化展示";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView nonClassmateListView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView classmateListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button choseButton;
        private System.Windows.Forms.ComboBox studentIDComboBox;
    }
}