using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Message_Service.Models
{
    public class SMS : Message
    {
        public SMS(string id, string sender, string text)
        {
            ID = id;
            Sender = sender;
            MessageText = text;
        }

        public void ProcessAbbreviations()
        {
            IOService io = new IOService();
            Dictionary<string, string> abbreviations = io.GetAbbriviations();
            foreach (string key in abbreviations.Keys)
            {
                if (MessageText.Contains(key))
                {
                    string textspeak = key + "<" + abbreviations[key] + ">";
                    MessageText = MessageText.Replace(key, textspeak);
                }
            }
        }
    }
}
