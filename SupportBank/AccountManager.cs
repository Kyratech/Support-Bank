using System.Collections.Generic;

namespace SupportBank
{
    public class AccountManager
    {
        private HashSet<Account> accounts;

        public AccountManager()
        {
            accounts = new HashSet<Account>();
        }

        public void AddAccount(Account newAccount)
        {
            accounts.Add(newAccount);
        }

        public int GetCount()
        {
            return accounts.Count;
        }
    }
}
