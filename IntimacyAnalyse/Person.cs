using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IntimacyAnalyse
{
    class Person
    {
        int no;//人员标号
        string tel;//电话
        string name;//姓名
        List<ConnectPerson> classmate;
        List<ConnectPerson> otherPerson;

        public Person()
        {
        }

        public Person(int no,string name, string tel)
        {
            this.no = no;
            this.name = name;
            this.tel = tel;
            classmate = new List<ConnectPerson>();
            otherPerson = new List<ConnectPerson>();
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        public int No
        {
            get { return no; }
            set { no = value; }
        }

        public List<ConnectPerson> Classmate
        {
            get { return classmate; }
            set { classmate = value; }
        }

        public List<ConnectPerson> OtherPerson
        {
            get { return otherPerson; }
            set { otherPerson = value; }
        }
    }
}
