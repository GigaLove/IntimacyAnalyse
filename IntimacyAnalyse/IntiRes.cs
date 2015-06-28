using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntimacyAnalyse
{
    class IntiRes
    {
        private string localNum;
        private string name;
        private List<string> intiList;
        private List<string> nIntiList;

        public IntiRes(string localNum)
        {
            this.localNum = localNum;
            intiList = new List<string>();
            nIntiList = new List<string>();
        }

        public string LocalNum
        {
            get { return localNum; }
            set { localNum = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<string> IntiList
        {
            get { return intiList; }
            set { intiList = value; }
        }

        public List<string> NIntiList
        {
            get { return nIntiList; }
            set { nIntiList = value; }
        }
    }
}
