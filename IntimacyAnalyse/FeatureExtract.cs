using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace IntimacyAnalyse
{
    class FeatureExtract
    {
        private Hashtable contactFeatureTable;

        public FeatureExtract()
        {
            contactFeatureTable = new Hashtable();
        }

        /// <summary>
        /// 数据特征提取
        /// </summary>
        /// <param name="dataList"></param>
        public void dataStatistic(List<string[]> dataList)
        {
            if (dataList == null || dataList.Count < 1)
            {
                return;
            }

            UserFeature uf = null;
            ContactFeature cf = null;
            Hashtable singleFeatureTable = null;

            foreach (string[] dataRow in dataList)
            {
                string localNumber = dataRow[0];
                string contactNumber = dataRow[1];
                int duration = Convert.ToInt32(dataRow[2]);
                string time = dataRow[4];

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

            writeFeature2File();
        }

        /// <summary>
        /// 将数据特征写入到csv文件中
        /// </summary>
        public void writeFeature2File()
        {
            UserFeature uf = null;
            ContactFeature cf = null;
            List<string[]> featureList = new List<string[]>();

            foreach (DictionaryEntry de in contactFeatureTable)
            {
                uf = de.Value as UserFeature;
                foreach (DictionaryEntry subDic in uf.FeatureTable)
                {
                    cf = subDic.Value as ContactFeature;
                    cf.PhoneCountRate = (double)cf.PhoneCount / uf.TotalCount;
                    cf.PhoneDurationRate = (double)cf.PhoneDuration / uf.TotalDuration;
                    cf.computeFrequency();
                    cf.TimeSlotPhoneCount[0] = cf.TimeSlotPhoneCount[0] / cf.PhoneCount;
                    cf.TimeSlotPhoneCount[1] = cf.TimeSlotPhoneCount[1] / cf.PhoneCount;
                    cf.TimeSlotPhoneCount[2] = cf.TimeSlotPhoneCount[2] / cf.PhoneCount;
                    featureList.Add(cf.convert2StrArray());
                }
            }

            CsvHandler.WriteCSV("data_feature.csv", true, featureList);
        }

    }
}
