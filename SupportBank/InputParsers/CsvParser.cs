using System;
using System.IO;
using System.Text;
using NLog;
using SupportBank.Accounts;
using SupportBank.DataTypes;
using SupportBank.Transactions;

namespace SupportBank.InputParsers
{
    public class CsvParser : InputParser
    {
        private readonly StreamReader csvFile;

        private bool hasNext;
        private string currentLine;
        private int lineCounter;

        public CsvParser(string filepath, TransactionManager bankTransactions, AccountManager bankAccounts)
            : base(filepath, bankTransactions, bankAccounts)
        {
            csvFile = new StreamReader(filepath);
            
            //Discard the first line - it's just the column names
            csvFile.ReadLine();
            lineCounter = 1;
            ReadLineAndCloseIfFinished();
        }

        public override void ParseFile()
        {
            while (hasNext)
            {
                ParseLine(currentLine);
                ReadLineAndCloseIfFinished();
            }
        }

        private void ParseLine(string line)
        {
            TransactionStrings data = new TransactionStrings(splitCsv(line));

            AddAnyNewAccounts(data.GetSender(), data.GetRecipient());
            AddNewTransaction(data, lineCounter);
        }

        private void ReadLineAndCloseIfFinished()
        {
            currentLine = csvFile.ReadLine();

            if (currentLine == null)
            {
                hasNext = false;
                csvFile.Close();
            }
            else
            {
                hasNext = true;
                lineCounter++;
            }
        }

        private string[] splitCsv(string line)
        {
            char[] delimiters = { ',' };
            string[] transactionData = line.Split(delimiters);
            return transactionData;
        }
    }
}
