using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportBank.Accounts;
using SupportBank.InputParsers;
using SupportBank.Transactions;

namespace SupportBank
{
    public class SupportBank
    {
        private readonly TransactionManager transactions;
        private readonly AccountManager accounts;

        public SupportBank()
        {
            transactions = new TransactionManager();
            accounts = new AccountManager();
        }

        public void UpdateRecords(string recordFilepath)
        {
            CsvParser reader = new CsvParser(recordFilepath, transactions, accounts);

            reader.ParseFile();
        }

        public void WriteAllAccounts()
        {
            SupportBankWriter.WriteAllAccounts(accounts);
        }

        public void WriteTransactionsForAccount(string accountName)
        {
            if (accounts.AccountExists(accountName))
            {
                SupportBankWriter.WriteAllTransactionsForAccount(transactions, accountName);
            }
            else
            {
                SupportBankWriter.WriteInvalidAccountMessage(accountName);
            }
        }
    }
}
