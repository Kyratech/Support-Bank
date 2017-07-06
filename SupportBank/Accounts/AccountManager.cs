using System;
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

        public bool AccountExists(string name)
        {
            return accounts.ContainsKey(name);
        }

        public List<Account> GetAccounts()
        {
            List<Account> accountList = new List<Account>();
            foreach (Account account in accounts.Values)
            {
                accountList.Add(account);
            }
            return accountList;
        }
    }
}
