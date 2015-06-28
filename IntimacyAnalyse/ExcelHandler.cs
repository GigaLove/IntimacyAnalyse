using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace IntimacyAnalyse
{
    class ExcelHandler
    {
        private string connectStr;          //链接字符串
        private string fullPath;            //文件路径
        private DataSet ds;
        private OleDbConnection conn;
        private OleDbDataAdapter adapter;

        /// <summary>
        /// 方法说明：构造函数，构造链接字符串
        /// </summary>
        /// <param name="path">excel文件路径</param>
        public ExcelHandler(string path)
        {
            fullPath = path;
            connectStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullPath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        }

        public void serFullPath(string fullPath)
        {
            this.fullPath = fullPath;
            connectStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullPath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
        }

        /// <summary>
        /// 方法说明：加载excel中的数据
        /// </summary>
        /// <returns>DataSet数据集</returns>
        private DataSet loadExcelData(string sqlStr)
        {
            conn = new OleDbConnection(connectStr);
            conn.Open();
            adapter = new OleDbDataAdapter(sqlStr, conn);
            ds = new DataSet();
            adapter.Fill(ds, "Sheet1");
            conn.Dispose();
            return ds;
        }

        /// <summary>
        /// 方法说明：返回excel中表明，用于书写sql语句
        /// </summary>
        /// <returns>表名</returns>
        private string getTableName()
        {
            string tableName = "Sheet1$";       //默认情况下表名为Sheet1$
            if (File.Exists(fullPath))
            {
                conn = new OleDbConnection(connectStr);
                conn.Open();
                DataTable sheetNames = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                conn.Dispose();
                tableName = (string)sheetNames.Rows[0][2];
            }
            return tableName;
        }

        /// <summary>
        /// 方法说明：根据datatable，返回嵌套的list
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>嵌套list</returns>
        public static List<string[]> convert2List(DataTable table)
        {
            List<string[]> dataList = new List<string[]>();
            int len = table.Rows[0].ItemArray.Length;       //获取每一行的列数

            foreach (DataRow row in table.Rows)
            {
                string[] temp = new string[len];
                for (int i = 0; i < len; i++)
                {
                    temp[i] = row[i].ToString();
                }
                dataList.Add(temp);
            }

            return dataList;
        }

        public DataTable getTable()
        {
            string sqlStr = "select * from [" + getTableName() + "];";       //sql语句
            System.Data.DataTable table = loadExcelData(sqlStr).Tables[0];
            return table;
        }
    }
}
