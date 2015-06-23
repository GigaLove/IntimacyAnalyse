using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntimacyAnalyse
{
    public partial class MainForm : Form
    {
        private ExcelHandler excelHandler;
        private DataTable excelTable;
        private string fileName = "";
        private List<string[]> dataList;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取excel内容，显示到dataGridView中
        /// </summary>
        private void readDataMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "//";
            openFileDialog.Filter = "Microsoft Excel files(*.xls)|*.xls;*.xlsx";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                excelHandler = new ExcelHandler(fileName);
                excelTable = excelHandler.getTable();
                originDataGridView.DataSource = excelTable;
            }
        }

        /// <summary>
        /// 格式化原始数据
        /// </summary>
        private void normalizeMenuItem_Click(object sender, EventArgs e)
        {
            string normalizedFileName = fileName.Split('.')[0] + ".csv";
            foreach (DataRow row in excelTable.Rows)
            {
                int sec = Preprocess.timeFormat((String)row[3]);
                row[3] = sec;
            }
            originDataGridView.DataSource = excelTable;
            dataList = ExcelHandler.convert2List(excelTable);
            CsvHandler.WriteCSV(normalizedFileName, dataList);
            MessageBox.Show("数据规范化成功\n规范化后的数据已写入文件" + normalizedFileName);
        }
    }
}
