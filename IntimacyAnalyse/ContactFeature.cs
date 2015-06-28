using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace IntimacyAnalyse
{
    /// <summary>
    /// 联系人特征数据结构
    /// </summary>
    class ContactFeature
    {
        private const int TIME_RANGE = 150;

        private string localNumber;             // 本机号码
        private string contactNumber;           // 联系号码
        private int phoneCount;                 // 通话次数
        private int phoneDuration;              // 通话时长
        private double phoneCountRate;          // 通话次数比
        private double phoneDurationRate;       // 通话时长比
        private int contactFrequency;           // 通话频率
        private double[] timeSlotPhoneCount;    // 不同通话时间段的通话次数比，分为6：12点，12：18点，18：24点
        private int intimacy;                   // 亲密度
        private DateTimeFormatInfo dtFormat;

        public ContactFeature()
        {
        }

        public ContactFeature(string localNumber, string contactNumber)
        {
            this.localNumber = localNumber;
            this.contactNumber = contactNumber;
            this.phoneCount = 0;
            this.phoneDuration = 0;
            dtFormat = new DateTimeFormatInfo();
            //dtFormat.ShortDatePattern = "yyyy/MM/dd hh:mm";
            timeSlotPhoneCount = new double[3];
        }

        public string LocalNumber
        {
            get { return localNumber; }
            set { localNumber = value; }
        }

        public string ContactNumber
        {
            get { return contactNumber; }
            set { contactNumber = value; }
        }

        public int PhoneCount
        {
            get { return phoneCount; }
            set { phoneCount = value; }
        }

        public int PhoneDuration
        {
            get { return phoneDuration; }
            set { phoneDuration = value; }
        }

        public double PhoneCountRate
        {
            get { return phoneCountRate; }
            set { phoneCountRate = value; }
        }

        public double PhoneDurationRate
        {
            get { return phoneDurationRate; }
            set { phoneDurationRate = value; }
        }

        public double[] TimeSlotPhoneCount
        {
            get { return timeSlotPhoneCount; }
            set { timeSlotPhoneCount = value; }
        }

        public int ContactFrequency
        {
            get { return contactFrequency; }
            set { contactFrequency = value; }
        }

        public int Intimacy
        {
            get { return intimacy; }
            set { intimacy = value; }
        }
        /// <summary>
        /// 增加phoneCount计数
        /// </summary>
        public void countRise()
        {
            this.phoneCount++;
        }

        public void durationRise(int duration)
        {
            this.phoneDuration += duration;
        }

        public void computeFrequency()
        {
            contactFrequency = (int)Math.Ceiling((double)TIME_RANGE / phoneCount);
        }

        /// <summary>
        /// 通话时间段统计
        /// </summary>
        /// <param name="time"></param>
        public void timeSlotStat(String time)
        {
            DateTime dateTime = Convert.ToDateTime(time, dtFormat);
            int hour = dateTime.Hour;
            if (hour >= 6 && hour < 12)
            {
                timeSlotPhoneCount[0]++;
            }
            else if (hour >= 12 && hour < 18)
            {
                timeSlotPhoneCount[1]++;
            }
            else if (hour > 18 && hour <= 24)
            {
                timeSlotPhoneCount[2]++;
            }
            else
            {
            }
        }

        /// <summary>
        /// 将通话特征转换成string数组返回
        /// </summary>
        /// <returns>特征向量数组</returns>
        public string[] convert2StrArray()
        {
            string[] featureArray = {localNumber, contactNumber, phoneCount.ToString(), String.Format("{0:N4}", phoneCountRate), 
                                        phoneDuration.ToString(), String.Format("{0:N4}", phoneDurationRate), contactFrequency.ToString(),
                                        timeSlotPhoneCount[0].ToString(), timeSlotPhoneCount[1].ToString(), timeSlotPhoneCount[2].ToString()};
            return featureArray;
        }
    }
}
