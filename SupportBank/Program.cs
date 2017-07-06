using System;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            string transactionPath = "C:/Work/Training/SupportBank/Transactions/Transactions2014.csv";
            SupportBank bank = new SupportBank();
            bank.UpdateRecords(transactionPath);

            SupportBankConsoleParser consoleParser = new SupportBankConsoleParser();
            consoleParser.ProcessInput(bank);
        }
    }
}
