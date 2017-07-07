using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SupportBank.Transactions;

namespace SupportBank.InputParsers
{
    class JsonToAccountStringsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(TransactionStrings))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            reader.DateParseHandling = DateParseHandling.None;
            JObject jObject = JObject.Load(reader);

            string[] inputStrings = new string[5];
            inputStrings[0] = (string) jObject["Date"];
            inputStrings[1] = (string) jObject["FromAccount"];
            inputStrings[2] = (string) jObject["ToAccount"];
            inputStrings[3] = (string) jObject["Narrative"];
            inputStrings[4] = (string) jObject["Amount"];

            TransactionStrings transactionStrings = new TransactionStrings(inputStrings);

            return transactionStrings;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
