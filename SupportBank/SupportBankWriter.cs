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
            List<Transaction> transactionsForAccount = transactions.GetTransactionsWithAccount(accountName);

            if (transactionsForAccount.Count > 0)
            {
                WriteTransactionList(transactionsForAccount);
            }
            else
            {
                Console.WriteLine("There are no transactions associated with that user.");
            }
        }

        private static void WriteTransactionList(List<Transaction> transactionList)
        {
            foreach (Transaction transaction in transactionList)
            {
                Console.WriteLine(transaction);
            }
        }

        public static void WriteInvalidAccountMessage(string invalidName)
        {
            Console.WriteLine("'" + invalidName + "' does not have an active account.");
        }
    }
}
