using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportBank
{
    public class CsvReader
    {
        private StreamReader csvFile;

        public CsvReader(string filepath)
        {
            csvFile = new StreamReader(filepath);
            //Discard the first line - it's just the column names
            csvFile.ReadLine();
        }

        public string NextLine()
        {
            return csvFile.ReadLine();
        }
    }
}
