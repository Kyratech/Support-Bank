using System;

namespace SupportBank.DataTypes
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
            value = value.Replace(".", "");
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

            return "£" + pounds.ToString("0") + "." + pence.ToString("00");
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
