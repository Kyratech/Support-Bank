using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    class SupportBankConsoleParser
    {
        private string[] quitCommands = {"q", "quit", "exit"};

        public void ProcessInput(SupportBank bank)
        {
            string input;
            input = Console.ReadLine();
            while (ContinueProcessingInput(input))
            {
                if (input.StartsWith("List "))
                {
                    ParseCommand(input, bank);
                }
                else
                {
                    PrintHelpMessage();
                }

                input = Console.ReadLine();
            }
        }

        private bool ContinueProcessingInput(string input)
        {
            if (input != null)
            {
                foreach (string quitCommand in quitCommands)
                {
                    if (input.Equals(quitCommand, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private void ParseCommand(string input, SupportBank bank)
        {
            //Get the rest of input (after 'Last ') separately
            string query = input.Substring(5);

            if (query.Equals("All"))
            {
                bank.WriteAllAccounts();
            }
            else
            {
                bank.WriteTransactionsForAccount(query);
            }
        }

        private void PrintHelpMessage()
        {
            Console.WriteLine("You have not provided valid input.");
            Console.WriteLine("Enter 'List All' to see a list of all accounts, with balances.");
            Console.WriteLine("Enter 'List [Account]' to see a list of all transactions associated with that account.");
            Console.WriteLine("Enter 'q','quit' or 'exit' to end the program.");
            Console.WriteLine("Thank you! :)");
        }
    }
}
