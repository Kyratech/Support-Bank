using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;
using SupportBank.Accounts;
using SupportBank.DataTypes;
using SupportBank.Transactions;

namespace SupportBank
{
    public class CsvParser
    {
        private TransactionManager transactions;
        private AccountManager accounts;

        private readonly StreamReader csvFile;
        private string currentLine;

        public CsvParser(string filepath, TransactionManager bankTransactions, AccountManager bankAccounts)
        {
            csvFile = new StreamReader(filepath);
            //Discard the first line - it's just the column names
            csvFile.ReadLine();
            currentLine = csvFile.ReadLine();

            transactions = bankTransactions;
            accounts = bankAccounts;
        }

        public bool HasNext()
        {
            if (currentLine == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ParseNext()
        {
            if (HasNext())
            {
                ParseLine(currentLine);
                currentLine = csvFile.ReadLine();
            }
            else
            {
                throw new Exception("Reached end of file");
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
            Date date = new Date(data.GetDate());
            Account sender = accounts.GetAccount(data.GetSender());
            Account recipient = accounts.GetAccount(data.GetRecipient());
            Money amount = new Money(data.GetAmount());

            Transaction transaction = new Transaction(date, sender, recipient, data.GetNarrative(), amount);
            transactions.AddTransaction(transaction);
        }
    }
}
