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
