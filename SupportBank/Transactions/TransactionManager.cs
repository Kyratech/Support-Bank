using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int GetCount()
        {
            return transactions.Count;
        }
    }
}
