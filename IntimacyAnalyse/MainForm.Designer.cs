namespace IntimacyAnalyse
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.数据预处理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.originDataGridView = new System.Windows.Forms.DataGridView();
            this.normalizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据预处理ToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(860, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // 数据预处理ToolStripMenuItem
            // 
            this.数据预处理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readDataMenuItem,
            this.normalizeMenuItem});
            this.数据预处理ToolStripMenuItem.Name = "数据预处理ToolStripMenuItem";
            this.数据预处理ToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.数据预处理ToolStripMenuItem.Text = "数据预处理";
            // 
            // readDataMenuItem
            // 
            this.readDataMenuItem.Name = "readDataMenuItem";
            this.readDataMenuItem.Size = new System.Drawing.Size(152, 22);
            this.readDataMenuItem.Text = "读取excel数据";
            this.readDataMenuItem.Click += new System.EventHandler(this.readDataMenuItem_Click);
            // 
            // originDataGridView
            // 
            this.originDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.originDataGridView.Location = new System.Drawing.Point(22, 40);
            this.originDataGridView.Name = "originDataGridView";
            this.originDataGridView.RowTemplate.Height = 23;
            this.originDataGridView.Size = new System.Drawing.Size(802, 336);
            this.originDataGridView.TabIndex = 1;
            // 
            // normalizeMenuItem
            // 
            this.normalizeMenuItem.Name = "normalizeMenuItem";
            this.normalizeMenuItem.Size = new System.Drawing.Size(152, 22);
            this.normalizeMenuItem.Text = "数据规范化";
            this.normalizeMenuItem.Click += new System.EventHandler(this.normalizeMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 454);
            this.Controls.Add(this.originDataGridView);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "亲密度分析系统";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.originDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem 数据预处理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readDataMenuItem;
        private System.Windows.Forms.DataGridView originDataGridView;
        private System.Windows.Forms.ToolStripMenuItem normalizeMenuItem;
    }
}

