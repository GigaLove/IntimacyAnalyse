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
    public partial class GraphShowForm : Form
    {
        private string[] studentID;
        private Hashtable intiResTable;
        private Hashtable contactPerson;            // 联系人hashtable
        List<Person> ps = new List<Person>();
        Point[] po = { new Point(215, 31), new Point(270,38), new Point(318,60), new Point(360,95), new Point(390,138), 
            new Point(403,186), new Point(404,240), new Point(390,290), new Point(363,335), new Point(323, 371), 
            new Point(275,394), new Point(223,403), new Point(170,396), new Point(122,375), new Point(80,342), 
            new Point(52,298), new Point(36,246), new Point(35,194), new Point(48,142), new Point(76,98), 
            new Point(116,62), new Point(163,39)};//初始化展示的22个人的坐标

        public GraphShowForm(Hashtable intiResTable)
        {
            InitializeComponent();
            this.intiResTable = intiResTable;
            initContact();
            readPerson();
        }

        private int getStudentID(string number)
        {
            int index = 0;
            for (int i = 0; i < studentID.Length; i++ )
            {
                if (studentID[i].Equals(number))
                {
                    index = i;
                    break;
                }
            }
            return index + 1;
        }
        public void initContact()
        {
            // 基于同学电话excel填充联系人Hashtable
            ExcelHandler excelHandler = new ExcelHandler("同学联系方式.xls");
            DataTable  excelTable = excelHandler.getTable();

            studentID = new string[excelTable.Rows.Count];
            for(int i = 0; i < studentID.Length; i++)
            {
                studentID[i] = excelTable.Rows[i]["手机号"].ToString();
                studentIDComboBox.Items.Add("" + (i + 1));
            }

            // 基于同学电话excel填充联系人Hashtable
            excelHandler = new ExcelHandler("同学电话号码列表.xls");
            excelTable = excelHandler.getTable();

            contactPerson = new Hashtable();
            foreach (DataRow row in excelTable.Rows)
            {
                contactPerson.Add(row["手机号"].ToString(), row["姓名"].ToString());
            }

            
        }

        public void readPerson()
        {
            for (int i = 0; i < studentID.Length; i++)
            {
                string number = studentID[i];
                IntiRes intiRes = intiResTable[number] as IntiRes;
                if (intiRes == null)
                {
                    continue;
                }
                Person p = new Person(i);
                Hashtable classIntiTable = new Hashtable();
                Hashtable nClassIntiTable = new Hashtable();
                for (int j = 0; j < intiRes.IntiList.Count; j++ )
                {
                    IntiScore intiSocre = intiRes.IntiList[j];
                    int studentNum = getStudentID(intiSocre.Number);
                    if (contactPerson.Contains(intiSocre.Number) && !classIntiTable.Contains(studentNum))
                        classIntiTable.Add(studentNum, Math.Ceiling(intiSocre.Score));
                    if (!contactPerson.Contains(intiSocre.Number) && nClassIntiTable.Count < 4)
                    {
                        nClassIntiTable.Add(nClassIntiTable.Count + 1, Math.Ceiling(intiSocre.Score));
                    }
                }
                p.ConnectPerson = classIntiTable;
                p.OtherPerson = nClassIntiTable;
                ps.Add(p);
            }
        }

        //显示结果函数
        private void drawresult(int i)
        {
            List<DictionaryEntry> cp = new List<DictionaryEntry>();//用于存储与选中点相关联的点
            List<DictionaryEntry> op = new List<DictionaryEntry>();//用于存储与选中点相关联非本班的点
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);//用于存储画板结果
            Graphics gph = Graphics.FromImage(bmp);//画板
            //Pen redPen = new Pen(Color.Red, 2);
            Pen greenPen = new Pen(Color.Green, 2f); //用于标记与被选中的人关联的本班同学           
            Pen bluePen = new Pen(Color.DarkGray, 0.01f);//用于标记所有本班人，画线
            Pen pinkPen = new Pen(Color.Blue, 2f);//用于标记被选中展示的person
            //根据传递的所显示的persong号码，动态在该点为中心，生成矩形四个点，用于表示与选中person关联非本班关联度强的四个人
            Point[] poi = {new Point(po[i-1].X - 25, po[i-1].Y - 25),
                           new Point(po[i-1].X + 25, po[i-1].Y - 25),
                           new Point(po[i-1].X + 25, po[i-1].Y + 25),
                           new Point(po[i-1].X - 25, po[i-1].Y + 25)};
            //标记被选中的person对应的点
            gph.DrawEllipse(pinkPen, po[i - 1].X - 4f, po[i - 1].Y - 4f, 8f, 8f);
            for (int j = 0; j < 22; j++)
            {
                gph.DrawEllipse(bluePen, po[j].X - 2f, po[j].Y - 2f, 4f, 4f);//给每个本班人点画圈
                //给每个点标号
                if (j <= 4)
                {
                    gph.DrawString((j + 1).ToString(), new Font("宋体", 9), Brushes.Black, po[j].X, po[j].Y - 10);
                }
                if (j > 4 && j <= 14)
                {
                    gph.DrawString((j + 1).ToString(), new Font("宋体", 9), Brushes.Black, po[j].X, po[j].Y + 5);
                }
                if (j > 14 && j <= 19)
                {
                    gph.DrawString((j + 1).ToString(), new Font("宋体", 9), Brushes.Black, po[j].X - 15, po[j].Y - 6);
                }
                if (j > 19 && j < 22)
                {
                    gph.DrawString((j + 1).ToString(), new Font("宋体", 9), Brushes.Black, po[j].X - 10, po[j].Y - 10);
                }
            }
            //将本班同学的点用灰色的线连起来
            for (int k = 0; k < 21; k++)
            {
                for (int l = k + 1; l < 22; l++)
                {
                    gph.DrawLine(bluePen, po[k], po[l]);
                }
            }
            //将每个与被选中的人关联的本班同学哈希表值添加到cp list中
            foreach (DictionaryEntry ht in ps[i - 1].ConnectPerson)
            {
                cp.Add(ht);
            }
            //将每个与被选中的人关联的非本班同学哈希表值添加到op list中
            foreach (DictionaryEntry ht in ps[i - 1].OtherPerson)
            {
                op.Add(ht);
            }
            //动态根据本班的哈希表的值，绘制关联的线
            foreach (DictionaryEntry cpp in cp)
            {
                float w = float.Parse(cpp.Value.ToString());//w为哈希表的值，表示关联度，也是线的粗细
                float[] val = { 3, 1 };
                Pen blackPen = new Pen(Color.Black, w);
                blackPen.DashPattern = val;

                gph.DrawEllipse(greenPen, po[(int)cpp.Key - 1].X - 3f, po[(int)cpp.Key - 1].Y - 3f, 6f, 6f);//标记关联的本班同学
                gph.DrawLine(blackPen, po[(int)cpp.Key - 1], po[i - 1]);//绘制关联线
                //将关联列表与listview1中
                ListViewItem lv = new ListViewItem(cpp.Key.ToString());
                lv.SubItems.Add(cpp.Value.ToString());
                classmateListView.Items.Add(lv);
            }
            //标记非本班同学四个点
            for (int j = 0; j < 4; j++)
            {
                gph.DrawEllipse(greenPen, poi[j].X - 3f, poi[j].Y - 3f, 6f, 6f);
            }
            //动态根据非本班的哈希表的值，绘制关联的线
            foreach (DictionaryEntry opp in op)
            
            {
                float w = float.Parse(opp.Value.ToString());
                float[] val = { 2, 1 };
                Pen blackPen2 = new Pen(Color.DarkBlue, w);  //w为哈希表的值，表示关联度，也是线的粗细
                blackPen2.DashPattern = val;
               // for(int a = 0;a<op.Count;a++)
                gph.DrawLine(blackPen2, poi[(int)opp.Key - 1], po[i - 1]);//绘制关联线

                //将关联列表与listview1中
                ListViewItem lv = new ListViewItem(opp.Key.ToString());
                lv.SubItems.Add(opp.Value.ToString());
                nonClassmateListView.Items.Add(lv);
            }

            //显示绘制的图像
            pictureBox1.Image = bmp;
        }

        private void choseButton_Click(object sender, EventArgs e)
        {
            classmateListView.Items.Clear();//清空本班有联系的同学列表
            nonClassmateListView.Items.Clear();//清空非本班有联系的同学列表
            int no = System.Int32.Parse(studentIDComboBox.Text);//获取下拉列表中号码
            drawresult(no);//将号码传递给显示结果函数
        }
    }
}
