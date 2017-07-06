using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank.Transactions
{
    public class TransactionStrings
    {
        private readonly string date;
        private readonly string sender;
        private readonly string recipient;
        private readonly string narrative;
        private readonly string amount;

        public TransactionStrings(string[] data)
        {
            if (data.Length != 5)
            {
                throw new ArgumentException("The input should have 5 csvs, but instead had: " + data.Length);
            }
            else
            {
                date = data[0];
                sender = data[1];
                recipient = data[2];
                narrative = data[3];
                amount = data[4];
            }
        }

        public string GetDate()
        {
            return date;
        }

        public string GetSender()
        {
            return sender;
        }

        public string GetRecipient()
        {
            return recipient;
        }

        public string GetNarrative()
        {
            return narrative;
        }

        public string GetAmount()
        {
            return amount;
        }
    }
}
