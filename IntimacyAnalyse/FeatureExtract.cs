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

        public void dataStatistic(List<string[]> dataList)
        {
            if (dataList == null || dataList.Count < 1)
            {
                return;
            }

            List<string[]> featureList = new List<string[]>();
            UserFeature uf = null;
            ContactFeature cf = null;
            Hashtable singleFeatureTable = null;

            foreach (string[] dataRow in dataList)
            {
                string localNumber = dataRow[0];
                string contactNumber = dataRow[1];
                int duration = Convert.ToInt32(dataRow[2]);

                if (!contactFeatureTable.Contains(localNumber))
                {
                    uf = new UserFeature(localNumber);
                    contactFeatureTable.Add(localNumber, uf);
                }

                uf = contactFeatureTable[localNumber] as UserFeature;
                singleFeatureTable = uf.FeatureTable;

                if (!singleFeatureTable.Contains(dataRow[1]))
                {
                    cf = new ContactFeature(contactNumber);
                    singleFeatureTable.Add(contactNumber, cf);
                    cf.Intimacy = Convert.ToInt32(dataRow[5]);
                }

                cf = singleFeatureTable[contactNumber] as ContactFeature;
                cf.countRise();
                cf.durationRise(duration);
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
                    featureList.Add(cf.convert2StrArray());
                }
            }

            CsvHandler.WriteCSV("data_feature.csv", featureList);
        }

    }
}
