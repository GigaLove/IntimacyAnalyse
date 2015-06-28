using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntimacyAnalyse
{
    /// <summary>
    /// 亲密度打分类
    /// </summary>
    class IntiScore
    {
        private string number;  // 对方电话
        private double score;   // 打分

        public IntiScore(string number, double score)
        {
            this.number = number;
            this.score = score;
        }

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public double Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}
