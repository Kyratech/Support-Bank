using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class Money
    {
        private int amount;

        public Money(int value)
        {
            amount = value;
        }

        public Money(string value)
        {
            //Remove deciaml point
            value.Replace(".", "");
            amount = Int32.Parse(value);
        }

        public int GetAmount()
        {
            return amount;
        }

        public override string ToString()
        {
            int pounds = amount / 100;
            int pence = amount % 100;

            return "£" + pounds + pence.ToString("00");
        }

        public static Money operator +(Money m1, Money m2)
        {
            return new Money(m1.GetAmount() + m2.GetAmount());
        }

        public static Money operator -(Money m1, Money m2)
        {
            return new Money(m1.GetAmount() - m2.GetAmount());
        }
    }
}
