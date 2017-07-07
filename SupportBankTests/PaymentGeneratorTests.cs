using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupportBank;
using SupportBank.Accounts;
using SupportBank.DataTypes;
using SupportBank.Transactions;

namespace SupportBankTests
{
    [TestClass]
    public class PaymentGeneratorTests
    {
        [TestMethod]
        public void ParseCorrectAccountsCsv()
        {
            string testPath = "C:/Work/Training/SupportBank/SupportBankTests/testFiles/testCSV.csv";
            TransactionManager transactionManager = new TransactionManager();
            AccountManager accountManager = new AccountManager();
            CsvParser reader = new CsvParser(testPath, transactionManager, accountManager);

            ParseCorrectAccounts(reader, accountManager);
        }

        private void ParseCorrectAccounts(CsvParser reader, AccountManager accountManager)
        {
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
        public void ParseCorrectTransactionsCsv()
        {
            string testPath = "C:/Work/Training/SupportBank/SupportBankTests/testFiles/testCSV.csv";
            TransactionManager transactionManager = new TransactionManager();
            AccountManager accountManager = new AccountManager();
            CsvParser reader = new CsvParser(testPath, transactionManager, accountManager);

            ParseCorrectTransactions(reader, transactionManager);
        }

        private void ParseCorrectTransactions(CsvParser reader, TransactionManager transactionManager)
        {
            while (reader.HasNext())
            {
                reader.ParseNext();
            }

            Assert.AreEqual<int>(2, transactionManager.GetCount());

            List<Transaction> transactionsA = transactionManager.GetTransactionsWithAccount("Alpha A");
            List<Transaction> transactionsO = transactionManager.GetTransactionsWithAccount("Omega O");
            List<Transaction> transactionsZ = transactionManager.GetTransactionsWithAccount("Zeta Z");

            Assert.AreEqual(2, transactionsA.Count);
            Assert.AreEqual(1, transactionsO.Count);
            Assert.AreEqual(1, transactionsZ.Count);
        }

        [TestMethod]
        public void ListOfAccountsCorrectCsv()
        {
            string testPath = "C:/Work/Training/SupportBank/SupportBankTests/testFiles/testCSV.csv";
            TransactionManager transactionManager = new TransactionManager();
            AccountManager accountManager = new AccountManager();
            CsvParser reader = new CsvParser(testPath, transactionManager, accountManager);

            ListOfAccountsCorrect(reader, accountManager);
        }

        private void ListOfAccountsCorrect(CsvParser reader, AccountManager accountManager)
        {
            while (reader.HasNext())
            {
                reader.ParseNext();
            }

            List<Account> accounts = accountManager.GetAccounts();

            Assert.AreEqual<int>(3, accounts.Count);
        }

        [TestMethod]
        public void ListOfTransactionsCorrectCsv()
        {
            string testPath = "C:/Work/Training/SupportBank/SupportBankTests/testFiles/testCSV.csv";
            TransactionManager transactionManager = new TransactionManager();
            AccountManager accountManager = new AccountManager();
            CsvParser reader = new CsvParser(testPath, transactionManager, accountManager);

            ListOfTransactionsCorrect(reader, transactionManager);
        }

        private void ListOfTransactionsCorrect(CsvParser reader, TransactionManager transactionManager)
        {
            while (reader.HasNext())
            {
                reader.ParseNext();
            }

            string accountNameA = "Alpha A";
            string accountNameB = "Beta B";
            string accountNameO = "Omega O";
            string accountNameZ = "Zeta Z";

            List<Transaction> transactionsA = transactionManager.GetTransactionsWithAccount(accountNameA);
            List<Transaction> transactionsB = transactionManager.GetTransactionsWithAccount(accountNameB);
            List<Transaction> transactionsO = transactionManager.GetTransactionsWithAccount(accountNameO);
            List<Transaction> transactionsZ = transactionManager.GetTransactionsWithAccount(accountNameZ);

            Assert.AreEqual<int>(2, transactionsA.Count);
            Assert.AreEqual<int>(0, transactionsB.Count);
            Assert.AreEqual<int>(1, transactionsO.Count);
            Assert.AreEqual<int>(1, transactionsZ.Count);
        }

        [TestMethod]
        public void AccountOutputTest()
        {
            Account account = new Account("Alpha A");

            Money profit = new Money(333);
            Money loss = new Money(-444);

            account.ApplyTransaction(profit);
            account.ApplyTransaction(loss);

            StringAssert.Contains(account.ToString(), "[Alpha A, Balance: -£1.11]");
        }

        [TestMethod]
        public void TransactionOutputTest()
        {
            Date date = new Date("01/01/2000");
            Account accountA = new Account("Alpha A");
            Account accountB = new Account("Beta B");
            string narrative = "Food";
            Money amount = new Money(123);

            Transaction transaction = new Transaction(date, accountA, accountB, narrative, amount);

            StringAssert.Contains(transaction.ToString(), "[2000-01-01, Alpha A->Beta B, Food, £1.23]");
        }

        [TestMethod]
        public void MoneyParsingTest()
        {
            Money[] four = new Money[2];
            four[0] = new Money("0.04");
            four[1] = new Money(".04");

            Money[] ten = new Money[4];
            ten[0] = new Money("0.10");
            ten[1] = new Money(".10");
            ten[2] = new Money("0.1");
            ten[3] = new Money(".1");

            Money[] thirteen = new Money[2];
            thirteen[0] = new Money("0.13");
            thirteen[1] = new Money(".13");

            Money[] hundred = new Money[3];
            hundred[0] = new Money("1.00");
            hundred[1] = new Money("1.");
            hundred[2] = new Money("1");


            Money hundredFour = new Money("1.04");

            Money[] hundredTen = new Money[2];
            hundredTen[0] = new Money("1.10");
            hundredTen[1] = new Money("1.1");

            Money hundredThirteen = new Money("1.13");

            foreach (Money money in four)
            {
                Assert.AreEqual(4, money.GetAmount());
                StringAssert.Contains(money.ToString(), "£0.04");
            }

            foreach (Money money in ten)
            {
                Assert.AreEqual(10, money.GetAmount());
                StringAssert.Contains(money.ToString(), "£0.10");
            }

            foreach (Money money in thirteen)
            {
                Assert.AreEqual(13, money.GetAmount());
                StringAssert.Contains(money.ToString(), "£0.13");
            }

            foreach (Money money in hundred)
            {
                Assert.AreEqual(100, money.GetAmount());
                StringAssert.Contains(money.ToString(), "£1.00");
            }

            Assert.AreEqual(104, hundredFour.GetAmount());
            StringAssert.Contains(hundredFour.ToString(), "£1.04");

            foreach (Money money in hundredTen)
            {
                Assert.AreEqual(110, money.GetAmount());
                StringAssert.Contains(money.ToString(), "£1.10");
            }

            Assert.AreEqual(113, hundredThirteen.GetAmount());
            StringAssert.Contains(hundredThirteen.ToString(), "£1.13");
        }
    }
}
