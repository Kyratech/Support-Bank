using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportBank.Accounts;
using SupportBank.Transactions;

namespace SupportBank
{
    class SupportBankWriter
    {
        public static void WriteAllAccounts(AccountManager accounts)
        {
            foreach (Account account in accounts.GetAccounts())
            {
                Console.WriteLine(account);
            }
        }

        public static void WriteAllTransactionsForAccount(TransactionManager transactions, string accountName)
        {
            foreach (Transaction transaction in transactions.GetTransactionsWithAccount(accountName))
            {
                Console.WriteLine(transaction);
            }
        }
    }
}
