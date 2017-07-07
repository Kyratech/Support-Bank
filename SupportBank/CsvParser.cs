using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;
using NLog;
using SupportBank.Accounts;
using SupportBank.DataTypes;
using SupportBank.Transactions;

namespace SupportBank
{
    public class CsvParser
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly TransactionManager transactions;
        private readonly AccountManager accounts;

        private readonly StreamReader csvFile;
        private readonly string path;

        private bool hasNext;
        private string currentLine;
        private int lineCounter;

        private bool hasNotWrittenErrorMessage;

        public CsvParser(string filepath, TransactionManager bankTransactions, AccountManager bankAccounts)
        {
            csvFile = new StreamReader(filepath);
            path = filepath;
            
            //Discard the first line - it's just the column names
            csvFile.ReadLine();
            lineCounter = 1;
            ReadAndCloseAtEnd();

            transactions = bankTransactions;
            accounts = bankAccounts;

            hasNotWrittenErrorMessage = true;

            logger.Info("Opened csv file '" + path + "' to read transactions.");
        }

        public bool HasNext()
        {
            return hasNext;
        }

        public void ParseNext()
        {
            if (hasNext)
            {
                ParseLine(currentLine);
                ReadAndCloseAtEnd();
            }
            else
            {
                throw new Exception("Reached end of file");
            }
        }

        private void ReadAndCloseAtEnd()
        {
            currentLine = csvFile.ReadLine();

            if (currentLine == null)
            {
                hasNext = false;
                csvFile.Close();
                logger.Info("Finished reading csv file '" + path + "', closing reader.");
            }
            else
            {
                hasNext = true;
                lineCounter++;
            }
        }

        private void ParseLine(string line)
        {
            TransactionStrings data = new TransactionStrings(splitCsv(line));

            AddAnyNewAccounts(data.GetSender(), data.GetRecipient());
            AddNewTransaction(data);
        }

        private string[] splitCsv(string line)
        {
            char[] delimiters = { ',' };
            string[] transactionData = line.Split(delimiters);
            return transactionData;
        }

        private void AddAnyNewAccounts(string sender, string recipient)
        {
            accounts.AddAccountIfNew(sender);
            accounts.AddAccountIfNew(recipient);
        }

        private void AddNewTransaction(TransactionStrings data)
        {
            try
            {
                Date date = new Date(data.GetDate());
                Account sender = accounts.GetAccount(data.GetSender());
                Account recipient = accounts.GetAccount(data.GetRecipient());
                Money amount = new Money(data.GetAmount());

                Transaction transaction = new Transaction(date, sender, recipient, data.GetNarrative(), amount);
                transactions.AddTransaction(transaction);
            }
            catch (ArgumentException ae)
            {
                WriteErrorToConsoleFirstTime();
                LogBadFormatTransaction(ae.Message);
            }
        }

        private void WriteErrorToConsoleFirstTime()
        {
            if (hasNotWrittenErrorMessage)
            {
                Console.WriteLine("There was some bad data in csv file: " + path);
                Console.WriteLine("Please refer to the system logs at: " + Program.LogDirectory);
                hasNotWrittenErrorMessage = false;
            }
        }

        private void LogBadFormatTransaction(string exceptionMessage)
        {
            StringBuilder errorString = new StringBuilder();
            errorString.Append("On line ");
            errorString.Append(lineCounter);
            errorString.Append(" of csv file '");
            errorString.Append(path);
            errorString.Append("': ");
            errorString.Append(exceptionMessage);
            errorString.Append(" This transaction has not been added to the database.");

            logger.Error(errorString.ToString);
        }
    }
}
