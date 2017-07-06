using System;

namespace SupportBank.DataTypes
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

        public override string ToString()
        {
            return dateTime.ToString("YYYY-MM-DD");
        }
    }
}
