using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Date
    {
        private DateTime dateTime;

        public Date(string date)
        {
            char[] dateDelimiters = {'/'};
            string[] dateData = date.Split(dateDelimiters);

            int day = Int32.Parse(dateData[0]);
            int month = Int32.Parse(dateData[1]);
            int year = Int32.Parse(dateData[2]);

            dateTime = new DateTime(year, month, day);
        }
    }
}
