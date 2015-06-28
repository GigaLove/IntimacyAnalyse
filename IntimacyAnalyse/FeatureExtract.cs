using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IntimacyAnalyse
{
    /// <summary>
    /// 数据特征提取相关函数
    /// </summary>
    class FeatureExtract
    {
        /// <summary>
        /// 数据特征提取
        /// </summary>
        /// <param name="dataList"></param>
        public static List<string[]> dataStatistic(List<string[]> dataList, out Hashtable outContactFeatureTable)
        {
            Hashtable contactFeatureTable = new Hashtable();
            if (dataList == null || dataList.Count < 1)
            {
                outContactFeatureTable = null; 
                return null;
            }

            UserFeature uf = null;
            ContactFeature cf = null;
            Hashtable singleFeatureTable = null;
            List<string[]> featureList = new List<string[]>();

            // 迭代初始数据集
            foreach (string[] dataRow in dataList)
            {
                string localNumber = dataRow[0];
                string contactNumber = dataRow[1];
                int duration = Convert.ToInt32(dataRow[2]);
                string time = dataRow[4];

                // 过滤无效数据
                if (contactNumber.Length != 11 || contactNumber.StartsWith("0"))
                {
                    continue;
                }

                // 如果hashtable中，不存在创建新的用户特征 UserFeature
                if (!contactFeatureTable.Contains(localNumber))
                {
                    uf = new UserFeature(localNumber);
                    contactFeatureTable.Add(localNumber, uf);
                }

                uf = contactFeatureTable[localNumber] as UserFeature;
                singleFeatureTable = uf.FeatureTable;

                if (!singleFeatureTable.Contains(dataRow[1]))
                {
                    cf = new ContactFeature(localNumber, contactNumber);
                    singleFeatureTable.Add(contactNumber, cf);
                }

                cf = singleFeatureTable[contactNumber] as ContactFeature;
                cf.countRise();
                cf.durationRise(duration);
                cf.timeSlotStat(time);
                uf.totalCountRise();
                uf.totalDurationRise(duration);
            }

            foreach (DictionaryEntry de in contactFeatureTable)
            {
                uf = de.Value as UserFeature;
                foreach (DictionaryEntry subDic in uf.FeatureTable)
                {
                    cf = subDic.Value as ContactFeature;
                    cf.PhoneCountRate = (double)cf.PhoneCount / uf.TotalCount;
                    cf.PhoneDurationRate = (double)cf.PhoneDuration / uf.TotalDuration;
                    cf.computeFrequency();
                    // double类型数据保留小数点后四位
                    cf.TimeSlotPhoneCount[0] = Convert.ToDouble(String.Format("{0:N4}", cf.TimeSlotPhoneCount[0] / cf.PhoneCount));
                    cf.TimeSlotPhoneCount[1] = Convert.ToDouble(String.Format("{0:N4}", cf.TimeSlotPhoneCount[1] / cf.PhoneCount));
                    cf.TimeSlotPhoneCount[2] = Convert.ToDouble(String.Format("{0:N4}", cf.TimeSlotPhoneCount[2] / cf.PhoneCount));
                    featureList.Add(cf.convert2StrArray());
                }
            }

            outContactFeatureTable = contactFeatureTable;
            return featureList;
        }

        /// <summary>
        /// 读取特征数据csv文件，映射到double数组中
        /// </summary>
        /// <param name="fileName">特征数据文件</param>
        /// <returns></returns>
        public static double[][] featureStr2Double(List<string[]> fList)
        {
            double[][] featureData = new double[fList.Count][];

            for (int i = 0; i < fList.Count; i++ )
            {
                string[] strArray = fList[i];
                double[] featureRow = new double[strArray.Length - 2];
                for (int j = 0; j < featureRow.Length; j++)
                    featureRow[j] = Convert.ToDouble(strArray[2 + j]);
               
                featureData[i] = featureRow;
            }

            return featureData;
        }
    }
}
