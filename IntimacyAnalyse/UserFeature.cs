using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IntimacyAnalyse
{
    /// <summary>
    /// 用户的通话特征
    /// </summary>
    class UserFeature
    {


        private string localNumber;
        private int totalCount;     // 通话总次数
        private int totalDuration;      // 通话总时长
        private Hashtable featureTable;

        public UserFeature(string localNumber)
        {
            this.localNumber = localNumber;
            featureTable = new Hashtable();
            totalCount = 0;
            totalDuration = 0;
        }

        public string LocalNumber
        {
            get { return localNumber; }
            set { localNumber = value; }
        }
        
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; }
        }
        
        public int TotalDuration
        {
            get { return totalDuration; }
            set { totalDuration = value; }
        }

        public Hashtable FeatureTable
        {
            get { return featureTable; }
            set { featureTable = value; }
        }

        public void totalCountRise()
        {
            totalCount++;
        }

        public void totalDurationRise(int duration)
        {
            totalDuration += duration;
        }
    }
}
