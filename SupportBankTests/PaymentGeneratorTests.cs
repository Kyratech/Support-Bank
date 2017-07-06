using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupportBank;
using SupportBank.Accounts;
using SupportBank.Transactions;

namespace SupportBankTests
{
    [TestClass]
    public class PaymentGeneratorTests
    {
        [TestMethod]
        public void GenerateCorrectAccounts()
        {
            string testPath = "C:/Work/Training/SupportBank/SupportBankTests/testCSV/testCSV.csv";
            TransactionManager transactionManager = new TransactionManager();
            AccountManager accountManager = new AccountManager();
            CsvParser reader = new CsvParser(testPath, transactionManager, accountManager);

            string line;
            while (reader.HasNext())
            {
                reader.ParseNext();
            }

            Assert.AreEqual<int>(3, accountManager.GetCount());

            string accountNameA = "Alpha A";
            string accountNameO = "Omega O";
            string accountNameZ = "Zeta Z";

            Account accountA = accountManager.GetAccount(accountNameA);
            Account accountO = accountManager.GetAccount(accountNameO);
            Account accountZ = accountManager.GetAccount(accountNameZ);

            StringAssert.Contains(accountA.GetName(), accountNameA);
            StringAssert.Contains(accountO.GetName(), accountNameO);
            StringAssert.Contains(accountZ.GetName(), accountNameZ);
        }

        [TestMethod]
        public void GenerateCorrectTransactions()
        {
            string testPath = "C:/Work/Training/SupportBank/SupportBankTests/testCSV/testCSV.csv";
            TransactionManager transactionManager = new TransactionManager();
            AccountManager accountManager = new AccountManager();
            CsvParser reader = new CsvParser(testPath, transactionManager, accountManager);

            string line;
            while (reader.HasNext())
            {
                reader.ParseNext();
            }

            Assert.AreEqual<int>(2, transactionManager.GetCount());
        }
    }
}
