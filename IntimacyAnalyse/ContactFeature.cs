using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace IntimacyAnalyse
{
    class ContactFeature
    {
        private string contactNumber;
        private int phoneCount;
        private int phoneDuration;
        private double phoneCountRate;
        private double phoneDurationRate;
        private int contactFrequency;
        private int[] timeSlotPhoneCount;
        private int intimacy;
        private DateTimeFormatInfo dtFormat;

        public ContactFeature()
        {
        }

        public ContactFeature(string contactNumber)
        {
            this.contactNumber = contactNumber;
            this.phoneCount = 0;
            this.phoneDuration = 0;
            intimacy = -1;
            dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy/MM/dd hh:mm";
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

        public int[] TimeSlotPhoneCount
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
            //if (phoneCount == 1)
            //{
                
            //}
            //DateTime date1 = Convert.ToDateTime(firstRecordDate, dtFormat);
            //DateTime date2 = Convert.ToDateTime(lastRecordDate, dtFormat);

            //int daySpan = (date2 - date1).Days;
            contactFrequency = (int)Math.Ceiling((double)150 / phoneCount);
        }

        public string[] convert2StrArray()
        {
            string[] featureArray = {phoneCount.ToString(), String.Format("{0:N4}", phoneCountRate), phoneDuration.ToString(),
                                       String.Format("{0:N4}", phoneDurationRate), contactFrequency.ToString(), intimacy.ToString()};
            return featureArray;
        }
    }
}
