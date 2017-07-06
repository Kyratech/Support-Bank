using System.Text;
using SupportBank.Accounts;
using SupportBank.DataTypes;

namespace SupportBank.Transactions
{
    public class Transaction
    {
        private Date date;
        private Account sender;
        private Account recipient;
        private string narrative;
        private Money amount;

        public Transaction(Date date, Account sender, Account recipient, string narrative, Money amount)
        {
            this.date = date;
            this.sender = sender;
            this.recipient = recipient;
            this.narrative = narrative;
            this.amount = amount;

            UpdateAccounts();
        }

        private void UpdateAccounts()
        {
            sender.ApplyTransaction(new Money(-amount.GetAmount()));
            recipient.ApplyTransaction(amount);
        }

        public bool InvolvesAccount(string name)
        {
            return (sender.GetName().Equals(name) || recipient.GetName().Equals(name));
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            stringBuilder.Append(date);
            stringBuilder.Append(", ");
            stringBuilder.Append(sender.GetName());
            stringBuilder.Append("->");
            stringBuilder.Append(recipient.GetName());
            stringBuilder.Append(", ");
            stringBuilder.Append(narrative);
            stringBuilder.Append(", ");
            stringBuilder.Append(amount);
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }
    }
}
