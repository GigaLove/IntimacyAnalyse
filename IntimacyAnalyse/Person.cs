using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IntimacyAnalyse
{
    class Person
    {

        int no;     //人员标号
        Hashtable connectPerson;    //用于存储关联的同学
        Hashtable otherPerson;  //用于存储非本班同学

        public Person(int number)
        {
            this.no = number;
            connectPerson = new Hashtable();
            otherPerson = new Hashtable();
        }

        public int No
        {
            get { return no; }
            set { no = value; }
        }

        public Hashtable ConnectPerson
        {
            get { return connectPerson; }
            set { connectPerson = value; }
        }

        public Hashtable OtherPerson
        {
            get { return otherPerson; }
            set { otherPerson = value; }
        }
        
    }
}
