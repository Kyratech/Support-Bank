using System;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            int pounds = -123 / 100;
            int pence = -123 % 100;

            Console.Write(pounds + ", " + pence);

            string transactionPath = "C:/Work/Training/SupportBank/Transactions/Transactions2014.csv";
            SupportBank bank = new SupportBank();
            bank.UpdateRecords(transactionPath);
        }
    }
}
