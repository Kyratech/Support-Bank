using SupportBank.Accounts;

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
    }
}
