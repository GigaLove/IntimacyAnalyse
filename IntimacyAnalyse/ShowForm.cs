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
    public partial class ShowForm : Form
    {
        private const int TOP_COUNT = 5;        // 选择亲密度的top值
        List<Person> personList = new List<Person>();//用于存储所有Person类，用于下拉列表框选中后展示方便
        private Hashtable intiResTable;

        public ShowForm(Hashtable intiResTable)
        {
            InitializeComponent();
            this.intiResTable = intiResTable;
            initContact();
            initIntimacy();

            //load_Person();//加载所有Person类
        }

        public void initContact()
        {
            // 基于同学电话excel填充联系人Hashtable
            ExcelHandler excelHandler = new ExcelHandler("同学电话号码列表.xls");
            DataTable excelTable = excelHandler.getTable();

            for (int i = 0; i < excelTable.Rows.Count; i++)
            {
                int number = Convert.ToInt32(excelTable.Rows[i]["学号"]);
                string name = excelTable.Rows[i]["姓名"].ToString();
                string tele = excelTable.Rows[i]["手机号"].ToString();
                Person p = new Person(number, name, tele);
                personList.Add(p);

                studentIDComboBox.Items.Add("" + number);  // 更新combobox
            }
        }

        private Person getPerson(string number)
        {
            foreach (Person p in personList)
            {
                if (p.Tel.Equals(number))
                {
                    return p;
                }
            }
            return null;
        }

        private string getClassmateName(string number)
        {
            string name = "";
            foreach (Person p in personList)
            {
                if (p.Tel.Equals(number))
                {
                    name = p.Name;
                    break;
                }
            }
            return name;
        }

        public void initIntimacy()
        {
            if (intiResTable != null)
            {
                foreach (DictionaryEntry de in intiResTable)
                {
                    Person p = getPerson(de.Key as string);
                    if (p != null)
                    {
                        IntiRes intiRes = de.Value as IntiRes;
                        p.Classmate = getTopIntiClassmate(intiRes);
                        p.OtherPerson = getTopIntiNClassmate(intiRes);
                    }
                }
            }
        }

        public List<ConnectPerson> getTopIntiClassmate(IntiRes intiRes)
        {
            List<ConnectPerson> connectPersonList = new List<ConnectPerson>();

            List<IntiScore> list = new List<IntiScore>(intiRes.IntiList);
            list.AddRange(intiRes.NIntiList);
            list.Sort();
            int count = 0;

            foreach (IntiScore intiScore in list)
            {
                Person p = getPerson(intiScore.Number);
                if (p != null)
                {
                    ConnectPerson cp = new ConnectPerson(p.No, p.Tel, p.Name, (float)intiScore.Score);
                    connectPersonList.Add(cp);
                    count++;
                    if (count >= TOP_COUNT)
                    {
                        break;
                    }
                }
            }
            return connectPersonList;
        }

        public List<ConnectPerson> getTopIntiNClassmate(IntiRes intiRes)
        {
            List<ConnectPerson> connectPersonList = new List<ConnectPerson>();

            List<IntiScore> list = new List<IntiScore>(intiRes.IntiList);
            list.AddRange(intiRes.NIntiList);
            list.Sort();
            int count = 0;

            foreach (IntiScore intiScore in list)
            {
                Person p = getPerson(intiScore.Number);
                if (p == null)
                {
                    count++;
                    ConnectPerson cp = new ConnectPerson(count, intiScore.Number, "", (float)intiScore.Score);
                    connectPersonList.Add(cp);
                    if (count >= TOP_COUNT)
                    {
                        break;
                    }
                }
            }
            return connectPersonList;
        }


        //加载数据集
        private void load_Person()
        {
            //实例化22个person
            for (int i = 0; i < 24; i++)
            {
                Person p = new Person();
                p.No = i;
                personList.Add(p);
            }
            personList[1].Name = "陈泽阳";
            personList[1].Tel = "123456";
            ConnectPerson classm0 = new ConnectPerson();
            ConnectPerson classm1 = new ConnectPerson();
            ConnectPerson classm2 = new ConnectPerson();
            classm0.Name = "陈华尚";
            classm0.Number = 2;
            classm0.Tel = "0000011";
            classm0.Score = 0.01f;
            classm1.Name = "代凯尧";
            classm1.Number = 3;
            classm1.Tel = "0023231";
            classm1.Score = 1.8f;
            classm2.Name = "王江波";
            classm2.Number = 4;
            classm2.Tel = "00112231";
            classm2.Score = 9f;
            ConnectPerson othe0 = new ConnectPerson();
            ConnectPerson othe1 = new ConnectPerson();

            othe0.Number = 2;
            othe0.Tel = "0000011";
            othe0.Score = 1.3f;

            othe1.Number = 3;
            othe1.Tel = "0023231";
            othe1.Score = 1.8f;

            personList[1].Classmate.Add(classm0);
            personList[1].Classmate.Add(classm1);
            personList[1].Classmate.Add(classm2);
            personList[1].OtherPerson.Add(othe0);
            personList[1].OtherPerson.Add(othe1);
        }

        //显示结果函数
        private void drawresult(int studentID)
        {

            Bitmap bmp = new Bitmap(intiShowPictureBox.Width, intiShowPictureBox.Height);//用于存储画板结果
            Graphics gph = Graphics.FromImage(bmp);//画板

            Pen greenPen = new Pen(Color.Green, 2f); //用于标记与被选中的人关联的本班同学           
            Pen darkGrayPen = new Pen(Color.DarkGray, 0.01f);//用于标记所有本班人，画线
            Pen bluePen = new Pen(Color.Blue, 2f);//用于标记被选中展示的person
            Point checkedPoint = new Point(220, 220);//用于画线
            Point checkedPoint0 = new Point(550, 220);//用于画线
            gph.DrawEllipse(bluePen, checkedPoint.X - 4f, checkedPoint.Y - 4f, 8f, 8f);//标记选中的点
            gph.DrawEllipse(bluePen, checkedPoint0.X - 4f, checkedPoint0.Y - 4f, 8f, 8f);//标记选中的点
            gph.DrawString(personList[studentID].Name, new Font("宋体", 9), Brushes.Black, checkedPoint.X, checkedPoint.Y + 5);//选中的点标名字
            gph.DrawString(personList[studentID].Name, new Font("宋体", 9), Brushes.Black, checkedPoint0.X, checkedPoint0.Y + 5);//选中的点标名字
            Point[] classPoint = new Point[personList[studentID].Classmate.Count];//班级相关点集
            Point[] otherPoint = new Point[personList[studentID].OtherPerson.Count];//非本班同学点集
            double leng = 150;//于展示的每条连线的长度
            double degree1 = Math.PI / (classPoint.Length * 0.5);
            double degree2 = Math.PI / (otherPoint.Length * 0.5);
            for (int j = 0; j < classPoint.Length; j++)
            {
                Pen yellowPen = new Pen(Color.YellowGreen, personList[studentID].Classmate[j].Score);
                float[] val = { 3, 1 };
                yellowPen.DashPattern = val;//定义画笔及画笔各项参数，画笔粗细为得分；
                double angle = j * degree1;
                double cos = Math.Sin(angle);
                double sin = Math.Cos(angle);
                int xx = (int)(leng * cos);
                int yy = (int)(leng * sin);
                classPoint[j].X = checkedPoint.X + xx;
                classPoint[j].Y = checkedPoint.Y + yy;

                gph.DrawEllipse(bluePen, classPoint[j].X - 2f, classPoint[j].Y - 2f, 4f, 4f);//给每个本班人点画圈
                gph.DrawString(personList[studentID].Classmate[j].Name, new Font("宋体", 9), Brushes.Black, classPoint[j].X, classPoint[j].Y + 5);
                gph.DrawLine(yellowPen, checkedPoint, classPoint[j]);
            }
            for (int j = 0; j < otherPoint.Length; j++)
            {
                Pen skyBluePen = new Pen(Color.LightSkyBlue, personList[studentID].OtherPerson[j].Score);
                float[] val = { 3, 1 };
                skyBluePen.DashPattern = val;//定义画笔及画笔各项参数，画笔粗细为得分；
                double angle = j * degree2;
                double cos = Math.Sin(angle);
                double sin = Math.Cos(angle);
                int xx = (int)(leng * cos);
                int yy = (int)(leng * sin);
                otherPoint[j].X = checkedPoint0.X + xx;
                otherPoint[j].Y = checkedPoint0.Y + yy;

                gph.DrawEllipse(bluePen, otherPoint[j].X - 2f, otherPoint[j].Y - 2f, 4f, 4f);//给每个非本班人点画圈
                gph.DrawString(personList[studentID].OtherPerson[j].Tel, new Font("宋体", 8), Brushes.Black, otherPoint[j].X, otherPoint[j].Y + 5);
                gph.DrawLine(skyBluePen, checkedPoint0, otherPoint[j]);
            }

            intiShowPictureBox.Image = bmp;


        }

        private void choseButton_Click(object sender, EventArgs e)
        {
            //classIntiDataGridView.Rows.Clear();//清空本班有联系的同学列表
            //nClassIntiDataGridView.Rows.Clear();//清空非本班有联系的同学列表

            int no = System.Int32.Parse(studentIDComboBox.Text) - 1;//获取下拉列表中号码
            classIntiDataGridView.DataSource = personList[no].Classmate;
            nClassIntiDataGridView.DataSource = personList[no].OtherPerson;
            classIntiDataGridView.Show();
            nClassIntiDataGridView.Show();
            drawresult(no);//将号码传递给显示结果函数
        }
    }
}
