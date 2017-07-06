namespace SupportBank
{
    public class AccountGenerator
    {
        public static Account GenerateAccountNameFromSender(string line)
        {
            string[] data = SplitCsv(line);
            Account sender = new Account(data[1]);
            return sender;
        }

        public static Account GenerateAccountNameFromRecipient(string line)
        {
            string[] data = SplitCsv(line);
            Account recipient = new Account(data[2]);
            return recipient;
        }

        private static string[] SplitCsv(string line)
        {
            char[] delimiters = {','};
            string[] transactionData = line.Split(delimiters);
            return transactionData;
        }
    }
}
