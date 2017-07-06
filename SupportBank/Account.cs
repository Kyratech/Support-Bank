using System;
using System.Collections.Generic;

namespace SupportBank
{
    public class Account : IEquatable<Account>
    {
        private readonly string name;

        public Account(string name)
        {
            this.name = name;
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
