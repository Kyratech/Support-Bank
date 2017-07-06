using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportBank.Accounts;

namespace SupportBank.Transactions
{
    public class TransactionManager
    {
        private List<Transaction> transactions;

        public TransactionManager()
        {
            transactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction newTransaction)
        {
            transactions.Add(newTransaction);
        }

        public List<Transaction> GetTransactionsWithAccount(Account account)
        {
            return GetTransactionsWithAccount(account.GetName());
        }

        public List<Transaction> GetTransactionsWithAccount(string accountName)
        {
            List<Transaction> transactionsWithAccount = new List<Transaction>();

            foreach (Transaction transaction in transactions)
            {
                if (transaction.InvolvesAccount(accountName))
                {
                    transactionsWithAccount.Add(transaction);
                }
            }

            return transactionsWithAccount;
        }

        public int GetCount()
        {
            return transactions.Count;
        }
    }
}
