using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    class Program
    {
        public const string LogDirectory = @"C:/Work/Logs/SupportBank.log";
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Program.SetupLogger();
            logger.Info("SupportBank program started");

            //string transactions2013 = "C:/Work/Training/SupportBank/Transactions/Transactions2013.json";
            //string transactions2014 = "C:/Work/Training/SupportBank/Transactions/Transactions2014.csv";
            //string transactions2015 = "C:/Work/Training/SupportBank/Transactions/DodgyTransactions2015.csv";
            SupportBank bank = new SupportBank();
            //bank.UpdateRecords(transactions2013);
            //bank.UpdateRecords(transactions2014);
            //bank.UpdateRecords(transactions2015);

            SupportBankConsoleParser consoleParser = new SupportBankConsoleParser();
            consoleParser.ProcessInput(bank);

            logger.Info("SupportBank program shutting down.");
        }

        private static void SetupLogger()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = LogDirectory, Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
        }
    }
}
