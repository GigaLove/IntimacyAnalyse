using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntimacyAnalyse
{
    class Preprocess
    {
        /// <summary>
        /// 格式化通话时间
        /// </summary>
        /// <param name="time">原始通话时长数据格式，如“1分43秒”</param>
        /// <returns>格式化的秒数</returns>
        public static int timeFormat(String time)
        {
            int sec = 0;

            if (!time.Contains("分") && !time.Contains("秒"))
            {
                return Convert.ToInt32(time);
            }

            if (time != null && time.Length > 1)
            {
                time = time.Substring(0, time.Length - 1);
                String[] strs = time.Split('分');
                if (strs.Length == 1)
                {
                    sec = Convert.ToInt32(strs[0]);
                }
                else if (strs.Length == 2)
                {
                    sec = Convert.ToInt32(strs[0]) * 60 + Convert.ToInt32(strs[1]);
                }
                else { }
            }

            return sec;
        }
    }
}
