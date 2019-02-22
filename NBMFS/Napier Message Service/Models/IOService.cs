using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Napier_Message_Service.Models
{
    public class IOService
    {
        private string jsonFilePath;
        private string abbreviationsFilePath;

        public IOService()
        {
            jsonFilePath = Directory.GetCurrentDirectory() + "\\messages.txt";
            abbreviationsFilePath = Directory.GetCurrentDirectory() + "\\textwords.csv";
        }

        public void SaveMessageToJSON(Message msg)
        {
            string json = JsonConvert.SerializeObject(msg);
            File.AppendAllText(jsonFilePath, json);
        }

        public Dictionary<string, string> GetAbbriviations()
        {
            Dictionary<string, string> abbreviations = new Dictionary<string, string>();
            var reader = new StreamReader(abbreviationsFilePath);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine().Split(',');
                abbreviations.Add(line[0], line[1]);
            }
            return abbreviations;
        }

        public List<string> GetMessagesFromFile(string filepath)
        {
            StringBuilder message = new StringBuilder();
            List<string> messages = new ArrayList<string>();

            var reader = new StreamReader(filepath);

            //CHECK IF LINE IS A MESSAGE ID
            Regex reg = new Regex("^[EST]{1}[0-9]{9}$");
            Match m; 
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                m = reg.Match(line);
                //NEW MESSAGE
                if (m.Success)
                {
                    if (message.ToString() != string.Empty)
                    {
                        messages.Add(message.ToString());
                        message.Clear();
                    }
                }
                line += "\n";
                message.Append(line);
            }
            messages.Add(message.ToString());
            return messages;
        }
    }
}
