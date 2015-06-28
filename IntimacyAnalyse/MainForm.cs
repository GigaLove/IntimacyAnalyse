using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace IntimacyAnalyse
{
    public partial class MainForm : Form
    {
        private const int CLUSTER_COUNT = 2;

        private ExcelHandler excelHandler;
        private DataTable excelTable;
        private string fileName = "";
        private List<string[]> dataList;
        private List<string[]> csvList;
        private List<string[]> selectedFeatureList;
        private List<string[]> clusterFeatureList;
        private List<string[]> markedDataList;
        private Boolean isFormated = false;
        private OpenFileDialog openFileDialog;
        private double[][] means;
        private double[] intiMeans;
        private double[] nIntiMeans;
        private Hashtable contactPerson;

        public MainForm()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < featureCheckedListBox.Items.Count; i++)
            {
                featureCheckedListBox.SetItemChecked(i, true);
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
                int sec = Preprocess.timeFormat((String)row["通话时长"]);
                row["通话时长"] = sec;
            }
            originDataGridView.DataSource = excelTable;
            dataList = ExcelHandler.convert2List(excelTable);
            CsvHandler.WriteCSV(normalizedFileName, dataList);
            MessageBox.Show("数据规范化成功\n规范化后的数据已写入文件" + normalizedFileName);
        }

        private void readFeatureMenuItem_Click(object sender, EventArgs e)
        {
            //featureData = FeatureExtract.featureStr2Double("feature.csv");
            //int[] clustering = KMeans.Cluster(featureData, 2);
            //double[][] means = KMeans.getMeans(featureData, clustering, 2);

        }

        private void readExcelMenuItem_Click(object sender, EventArgs e)
        {
            readExcelData(originDataGridView);
        }

        /// <summary>
        /// 基于width调整数据表格宽度
        /// </summary>
        /// <param name="width"></param>
        private void adapteDataGridViewWidth(int width)
        {
            for (int i = 0; i < originDataGridView.Columns.Count; i++)
            {
                originDataGridView.Columns[i].Width = width;
            }
        }

        private void readExcelButton_Click(object sender, EventArgs e)
        {
            readExcelData(originDataGridView);
        }

        /// <summary>
        /// 读取excel数据，显示到datagridview中
        /// </summary>
        private void readExcelData(DataGridView dgv)
        {
            // 设置filedialog相关属性
            openFileDialog.InitialDirectory = "//";
            openFileDialog.Filter = "Microsoft Excel files(*.xls)|*.xls;*.xlsx";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                excelHandler = new ExcelHandler(fileName);
                excelTable = excelHandler.getTable();
                dgv.DataSource = excelTable;
                adapteDataGridViewWidth(128);
            }
        }

        /// <summary>
        /// 数据格式化事件处理函数
        /// </summary>
        private void dataFormatButton_Click(object sender, EventArgs e)
        {
            if (excelTable == null || excelTable.Rows.Count == 0)
            {
                return;
            }

            foreach (DataRow row in excelTable.Rows)
            {
                int sec = Preprocess.timeFormat((String)row["通话时长"]);
                row["通话时长"] = sec;
            }
            MessageBox.Show("数据格式化成功");
            originDataGridView.DataSource = excelTable;
            isFormated = true;
        }

        /// <summary>
        /// CSV文件写入事件处理函数
        /// </summary>
        private void writeCSVButton_Click(object sender, EventArgs e)
        {
            if (excelTable == null)
            {
                MessageBox.Show("请先读取数据文件");
                return;
            }
            if (!isFormated)
            {
                MessageBox.Show("请先进行数据格式化");
            }
            else
            {
                string normalizedFileName = fileName.Split('.')[0] + ".csv";
                dataList = ExcelHandler.convert2List(excelTable);
                CsvHandler.WriteCSV(normalizedFileName, dataList);
                MessageBox.Show("规范化数据已写入文件" + normalizedFileName);
                isFormated = false;
            }
        }

        /// <summary>
        /// 读取CSV文件事件处理函数
        /// </summary>
        private void readCSVButton_Click(object sender, EventArgs e)
        {
            // 设置filedialog相关属性
            openFileDialog.InitialDirectory = "//";
            openFileDialog.Filter = "CSV文件|*.CSV";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string csvFileName = openFileDialog.FileName;
                csvList = CsvHandler.ReadCSV(csvFileName);

                foreach (string[] csvArray in csvList)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.CreateCells(csvDataGridView, csvArray);
                    csvDataGridView.Rows.Add(dr);
                }
            }
        }


        /// <summary>
        /// 基于特征选取checkboxlist，进行特征提取
        /// </summary>
        private void featureExtractButton_Click(object sender, EventArgs e)
        {
            if (csvList == null || csvList.Count == 0)
            {
                MessageBox.Show("请先导入csv文件");
                return;
            }

            // 获取选定数据特征
            int[] selectedFeature = new int[8];
            int selectedCount = 0;
            selectedFeatureList = new List<string[]>();
            for (int i = 0; i < featureCheckedListBox.Items.Count; i++)
            {
                if (featureCheckedListBox.GetItemChecked(i))
                {
                    selectedFeature[i] = 1;
                    selectedCount++;
                }
            }

            Hashtable contactFeatureTable;
            List<string[]> featureList = FeatureExtract.dataStatistic(csvList, out contactFeatureTable);
            UserFeature uf = contactFeatureTable[featureList[0][0]] as UserFeature;

            MessageBox.Show("特征提取成功\n本机号码：" + uf.LocalNumber + "\n总通话次数：" + uf.TotalCount + "\n总通话时长：" +
                uf.TotalDuration + "\n通话联系人个数：" + uf.FeatureTable.Count);
            for (int i = 0; i < selectedCount + 2; i++)
                featureDataGridView.Columns.Add(new DataGridViewTextBoxColumn());

            foreach (string[] featureArray in featureList)
            {
                DataGridViewRow dr = new DataGridViewRow();
                string[] temp = new string[selectedCount + 2];
                temp[0] = featureArray[0];
                temp[1] = featureArray[1];
                int index = 2;
                for (int i = 0; i < selectedFeature.Length; i++)
                {
                    if (selectedFeature[i] == 1)
                    {
                        temp[index++] = featureArray[2 + i];
                    }
                }

                dr.CreateCells(featureDataGridView, temp);
                featureDataGridView.Rows.Add(dr);
                selectedFeatureList.Add(temp);
            }
        }

        /// <summary>
        /// 写特征数据事件处理函数
        /// </summary>
        private void saveFeatureButton_Click(object sender, EventArgs e)
        {
            if (selectedFeatureList == null)
            {
                MessageBox.Show("请先进行特征提取！");
            }
            CsvHandler.WriteCSV("data_feature.csv", true, selectedFeatureList);
            MessageBox.Show("数据特征文件已经写入data_feature.csv");
        }

        /// <summary>
        /// 读取特征数据事件处理函数
        /// </summary>
        private void readFDataButton_Click(object sender, EventArgs e)
        {
            clusterDataGridView.Rows.Clear();
            clusterDataGridView.Columns.Clear();
            if (readCSVFileByDialog(clusterDataGridView))
                clusterFeatureList = new List<string[]>(csvList);
        }

        /// <summary>
        /// 读csv文件，将内容添加到对应的datagridview中
        /// </summary>
        /// <param name="gridView"></param>
        private Boolean readCSVFileByDialog(DataGridView gridView)
        {
            // 设置filedialog相关属性
            openFileDialog.InitialDirectory = "//";
            openFileDialog.Filter = "CSV文件|*.CSV";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string csvFileName = openFileDialog.FileName;
                csvList = CsvHandler.ReadCSV(csvFileName);

                for (int i = 0; i < csvList[0].Length; i++)
                    gridView.Columns.Add(new DataGridViewTextBoxColumn());

                foreach (string[] csvArray in csvList)
                {
                    DataGridViewRow dr = new DataGridViewRow();
                    dr.CreateCells(gridView, csvArray);
                    gridView.Rows.Add(dr);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 聚类分析按钮事件处理函数
        /// </summary>
        private void clusterAnalyseButton_Click(object sender, EventArgs e)
        {
            if (clusterFeatureList == null)
            {
                return;
            }

            double[][] featureDoubleArray = FeatureExtract.featureStr2Double(clusterFeatureList);
            int[] clustering = KMeans.Cluster(featureDoubleArray, 2);
            means = KMeans.getMeans(featureDoubleArray, clustering, 2);

            for (int i = 0; i < clusterFeatureList.Count; i++)
            {
                string[] temp = { clusterFeatureList[i][0], clusterFeatureList[i][1], clustering[i].ToString() };
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(clusterResDataGridView, temp);
                clusterResDataGridView.Rows.Add(dr);
            }

            if (means[0][0] > means[1][0])
            {
                intiRichTextBox.Text = String.Join("\n", means[1]);
                nIntiRichTextBox.Text = String.Join("\n", means[0]);
                iRichTextBox.Text = String.Join("\n", means[1]);
                nRichTextBox.Text = String.Join("\n", means[0]);
                intiMeans = means[1];
                nIntiMeans = means[0];
            }
            else
            {
                intiRichTextBox.Text = String.Join("\n", means[0]);
                nIntiRichTextBox.Text = String.Join("\n", means[1]);
                iRichTextBox.Text = String.Join("\n", means[0]);
                nRichTextBox.Text = String.Join("\n", means[1]);
                intiMeans = means[0];
                nIntiMeans = means[1];
            }
        }

        /// <summary>
        /// 读取标号数据集
        /// </summary>
        private void readMarkDataButton_Click(object sender, EventArgs e)
        {
            markDataGridView.Rows.Clear();
            markDataGridView.Columns.Clear();
            if (readCSVFileByDialog(markDataGridView))
                markedDataList = new List<string[]>(csvList);
        }

        /// <summary>
        /// 聚类验证分析事件处理函数
        /// </summary>
        private void clusterValidateButton_Click(object sender, EventArgs e)
        {
            if (means == null)
            {
                MessageBox.Show("请先训练样本集");
                mainTabControl.SelectedTab = clusterTabPage;
                return;
            }

            int[] clustering = new int[markedDataList.Count];
            double[][] data = new double[markedDataList.Count][];
            for (int i = 0; i < markedDataList.Count; i++)
            {
                string[] markedRow = markedDataList[i];
                double[] tempData = new double[markedRow.Length - 3];

                for (int j = 2; j < markedRow.Length - 1; j++)
                {
                    tempData[j - 2] = Convert.ToDouble(markedRow[j]);
                }
                data[i] = tempData;
            }

            data = KMeans.Normalized(data);

            for (int i = 0; i < markedDataList.Count; i++)
            {
                // 计算分类
                clustering[i] = classify(data[i]);
                string[] temp = { markedDataList[i][0], markedDataList[i][1], clustering[i].ToString() };
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(validateResDataGridView, temp);
                validateResDataGridView.Rows.Add(dr);
            }

            int correctCount = 0;
            for (int i = 0; i < markedDataList.Count; i++)
            {
                int mark = Convert.ToInt32(markedDataList[i][markedDataList[i].Length - 1]);
                if (clustering[i] == mark)
                {
                    correctCount++;
                }
            }
            double correctRatio = (double)correctCount / markedDataList.Count * 100;
            string corectRatioStr = String.Format("{0:N2}", correctRatio) + "%";
            correctRatioTextBox.Text = corectRatioStr;

        }

        /// <summary>
        /// 基于分类中心进行数据分类
        /// </summary>
        private int classify(double[] data)
        {
            if (data.Length != means[0].Length)
            {
                return 0;
            }

            double intiDistance = 0.0;
            double nIntiDistance = 0.0;

            for (int i = 0; i < data.Length; i++)
            {
                intiDistance += (data[i] - intiMeans[i]) * (data[i] - intiMeans[i]);
                nIntiDistance += (data[i] - nIntiMeans[i]) * (data[i] - nIntiMeans[i]);
            }

            if (intiDistance > nIntiDistance)
            {
                return 1;
            }

            return -1;
        }

        private void readContactExcelMenuItem_Click(object sender, EventArgs e)
        {
            readExcelData(contacterDataGridView);
            contactPerson = new Hashtable();
            foreach (DataRow row in excelTable.Rows)
            {
                contactPerson.Add(row["姓名"], row["手机号"]);
            }
            MessageBox.Show("联系方式读取成功，共计" + contactPerson.Count + "个联系人信息");
        }

        private void readContacterButton_Click(object sender, EventArgs e)
        {
            readExcelData(contacterDataGridView);
            contactPerson = new Hashtable();
            foreach (DataRow row in excelTable.Rows)
            {
                contactPerson.Add(row["姓名"], row["手机号"]);
            }
            MessageBox.Show("联系方式读取成功，共计" + contactPerson.Count + "个联系人信息");
        }
    }
}
