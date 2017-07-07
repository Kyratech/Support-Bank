using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace SupportBank
{
    class SupportBankConsoleParser
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private string[] quitCommands = {"q", "quit", "exit"};

        private const string ListCommand = "List ";
        private const string ImportCommand = "Import File ";

        public void ProcessInput(SupportBank bank)
        {
            Console.WriteLine("Please enter your query below:");
            string input;
            input = Console.ReadLine();
            while (ShouldContinueProcessingInput(input))
            {
                input = input.Trim();
                if (input.StartsWith(ListCommand))
                {
                    ParseListCommand(input, bank);
                }
                else if (input.StartsWith(ImportCommand))
                {
                    ParseImport(input, bank);
                }
                else
                {
                    PrintHelpMessage();
                }

                input = Console.ReadLine();
            }
        }

        private bool ShouldContinueProcessingInput(string input)
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

        private void ParseListCommand(string input, SupportBank bank)
        {
            string query = input.Substring(ListCommand.Length);

            if (query.Equals("All"))
            {
                bank.WriteAllAccounts();
                logger.Info("User has requested list of all Accounts.");
            }
            else
            {
                bank.WriteTransactionsForAccount(query);
                logger.Info("User has requested list of transactions for account: " + query);
            }
        }

        private void ParseImport(string input, SupportBank bank)
        {
            string path = input.Substring(ImportCommand.Length);

            try
            {
                bank.UpdateRecords(path);
                Console.WriteLine("Records updated successfully!");
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
                logger.Warn("User attempted to load invalid file: '" + path + "'.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Cannot find a valid file at: '" + path + "'.");
                logger.Warn("User attempted to load invalid file: '" + path + "'.");
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
