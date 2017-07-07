using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SupportBank.Accounts;
using SupportBank.Transactions;

namespace SupportBank.InputParsers
{
    public class JsonParser : InputParser
    {
        public JsonParser(string filepath, TransactionManager bankTransactions, AccountManager bankAccounts)
            : base(filepath, bankTransactions, bankAccounts)
        {
            
        }

        public override void ParseFile()
        {
            if (File.Exists(GetPath()))
            {
                string input = File.ReadAllText(GetPath());

                List<TransactionStrings> allTransactions =
                    JsonConvert.DeserializeObject<List<TransactionStrings>>(input, new JsonToAccountStringsConverter());

                for(int i = 0; i < allTransactions.Count; i++)
                {
                    ParseEntry(allTransactions[i], i + 1);
                }
            }
        }

        private void ParseEntry(TransactionStrings data, int entryNumber)
        {
            AddAnyNewAccounts(data.GetSender(), data.GetRecipient());
            AddNewTransaction(data, entryNumber);
        }
    }
}
