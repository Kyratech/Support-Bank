using System;
using NLog;

namespace SupportBank.DataTypes
{
    public class Date
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private DateTime dateTime;

        public Date(string date)
        {
            char[] dateDelimiters = {'/', '-', 'T', ' '};
            string[] dateData = date.Split(dateDelimiters);

            try
            {
                if (dateData[0].Length == 4)
                {
                    ParseInternational(dateData);
                }
                else
                {
                    ParseEnglish(dateData);
                }
                
            }
            catch (Exception)
            {
                throw new ArgumentException("The transaction date '" + date + "' is not in the correct date format: 'dd/mm/yyyy'.");
            }
        }

        private void ParseInternational(string[] dateData)
        {
            int day = Int32.Parse(dateData[2]);
            int month = Int32.Parse(dateData[1]);
            int year = Int32.Parse(dateData[0]);

            dateTime = new DateTime(year, month, day);
        }

        private void ParseEnglish(string[] dateData)
        {
            int day = Int32.Parse(dateData[0]);
            int month = Int32.Parse(dateData[1]);
            int year = Int32.Parse(dateData[2]);

            dateTime = new DateTime(year, month, day);
        }

        public override string ToString()
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
    }
}
