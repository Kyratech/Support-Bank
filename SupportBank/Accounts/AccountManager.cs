using System.Collections.Generic;

namespace SupportBank.Accounts
{
    public class AccountManager
    {
        private Dictionary<string, Account> accounts;

        public AccountManager()
        {
            accounts = new Dictionary<string, Account>();
        }

        public void AddAccountIfNew(string name)
        {
            if (!accounts.ContainsKey(name))
            {
                accounts.Add(name, new Account(name));
            }
        }

        public Account GetAccount(string name)
        {
            return accounts[name];
        }

        public int GetCount()
        {
            return accounts.Count;
        }
    }
}
