using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntimacyAnalyse
{
    public class ConnectPerson
    {
        int number;//标号
        string tel;//电话
        string name;//姓名
        float score;//亲密度打分

        public ConnectPerson()
        {
        }

        public ConnectPerson(int number, string tel, string name, float score)
        {
            this.number = number;
            this.tel = tel;
            this.name = name;
            this.score = score;
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public float Score
        {
            get { return score; }
            set { score = value; }
        }
        
    }
}
