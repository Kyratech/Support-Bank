using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SupportBank.Accounts;
using SupportBank.DataTypes;
using SupportBank.Transactions;

namespace SupportBank.InputParsers
{
    public abstract class InputParser
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly TransactionManager transactions;
        private readonly AccountManager accounts;

        private readonly string path;

        private bool hasNotWrittenErrorMessage;

        protected InputParser(string filepath, TransactionManager bankTransactions, AccountManager bankAccounts)
        {
            path = filepath;

            transactions = bankTransactions;
            accounts = bankAccounts;

            hasNotWrittenErrorMessage = true;

            logger.Info("Opened file '" + path + "' to read transactions.");
        }

        public static InputParser ChooseParser(string recordFilepath, TransactionManager transactions, AccountManager accounts)
        {
            if (recordFilepath.EndsWith(".csv"))
            {
                return new CsvParser(recordFilepath, transactions, accounts);
            }
            else if (recordFilepath.EndsWith(".json"))
            {
                return new JsonParser(recordFilepath, transactions, accounts);
            }
            else
            {
                logger.Error("Attempted to read unsupported file: '" + recordFilepath + "'.");
                throw new ArgumentException("The file at '" + recordFilepath + "' is not a supported type (.csv, .json)");
            }
        }

        public abstract void ParseFile();

        protected void AddAnyNewAccounts(string sender, string recipient)
        {
            accounts.AddAccountIfNew(sender);
            accounts.AddAccountIfNew(recipient);
        }

        protected void AddNewTransaction(TransactionStrings data, int entry)
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
                LogBadFormatTransaction(entry, ae.Message);
            }
        }

        protected void WriteErrorToConsoleFirstTime()
        {
            if (hasNotWrittenErrorMessage)
            {
                Console.WriteLine("There was some bad data in file: " + path);
                Console.WriteLine("Please refer to the system logs at: " + Program.LogDirectory);
                hasNotWrittenErrorMessage = false;
            }
        }

        protected void LogBadFormatTransaction(int entry, string exceptionMessage)
        {
            StringBuilder errorString = new StringBuilder();
            errorString.Append("In entry ");
            errorString.Append(entry);
            errorString.Append(" of file '");
            errorString.Append(path);
            errorString.Append("': ");
            errorString.Append(exceptionMessage);
            errorString.Append(" This transaction has not been added to the database.");

            logger.Error(errorString.ToString);
        }

        protected string GetPath()
        {
            return path;
        }
    }
}
