using System;
using SupportBank.DataTypes;

namespace SupportBank.Accounts
{
    public class Account : IEquatable<Account>
    {
        private readonly string name;
        private Money balance;

        public Account(string name)
        {
            this.name = name;
            balance = new Money(0);
        }

        public void ApplyTransaction(Money transaction)
        {
            balance = balance + transaction;
        }

        public Money GetBalance()
        {
            return GetBalance();
        }

        public bool Equals(Account other)
        {
            return name.Equals(other.GetName());
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public string GetName()
        {
            return name;
        }
    }
}
