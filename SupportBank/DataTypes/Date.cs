﻿using System;
using NLog;

namespace SupportBank.DataTypes
{
    public class Date
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private DateTime dateTime;

        public Date(string date)
        {
            char[] dateDelimiters = {'/'};
            string[] dateData = date.Split(dateDelimiters);

            try
            {
                int day = Int32.Parse(dateData[0]);
                int month = Int32.Parse(dateData[1]);
                int year = Int32.Parse(dateData[2]);

                dateTime = new DateTime(year, month, day);
            }
            catch (Exception)
            {
                throw new ArgumentException("The transaction date '" + date + "' is not in the correct date format: 'dd/mm/yyyy'.");
            }
        }

        public override string ToString()
        {
            return dateTime.ToString("yyyy-MM-dd");
        }
    }
}
