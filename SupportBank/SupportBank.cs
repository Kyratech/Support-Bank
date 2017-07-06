﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportBank.Accounts;
using SupportBank.Transactions;

namespace SupportBank
{
    class SupportBank
    {
        private TransactionManager transactions;
        private AccountManager accounts;

        public SupportBank()
        {
            transactions = new TransactionManager();
            accounts = new AccountManager();
        }

        public void UpdateRecords(string recordFilepath)
        {
            CsvParser reader = new CsvParser(recordFilepath, transactions, accounts);

            while (reader.HasNext())
            {
                reader.ParseNext();
            }
        }
    }
}