using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupportBank;

namespace SupportBankTests
{
    [TestClass]
    public class PaymentGeneratorTests
    {
        [TestMethod]
        public void GenerateCorrectAccounts()
        {
            CsvReader reader = new CsvReader("C:/Work/Training/SupportBank/SupportBankTests/testCSV/testCSV.csv");
            AccountManager accountManager = new AccountManager();

            string line;
            while ((line = reader.NextLine()) != null)
            {
                accountManager.AddAccount(AccountGenerator.GenerateAccountNameFromSender(line));
                accountManager.AddAccount(AccountGenerator.GenerateAccountNameFromRecipient(line));
            }

            Assert.AreEqual<int>(3, accountManager.GetCount());
        }
    }
}
