using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntimacyAnalyse
{
    /// <summary>
    /// 亲密度分析结果
    /// </summary>
    public class IntiRes
    {
        private string localNum;    // 本机号码
        private string name;    // 用户姓名
        private List<IntiScore> intiList;   // 亲密关系联系人list
        private List<IntiScore> nIntiList;  // 非亲密关系联系人list

        public IntiRes(string localNum)
        {
            this.localNum = localNum;
            intiList = new List<IntiScore>();
            nIntiList = new List<IntiScore>();
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

        public List<IntiScore> IntiList
        {
            get { return intiList; }
            set { intiList = value; }
        }

        public List<IntiScore> NIntiList
        {
            get { return nIntiList; }
            set { nIntiList = value; }
        }
    }
}
