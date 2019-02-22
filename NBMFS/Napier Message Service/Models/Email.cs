using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Napier_Message_Service.Models
{
    public class Email : Message
    {
        public string Subject { get; set; }
        public List<string> URLs { get; }

        public Email()
        {
            URLs = new List<string>();
        }

        public Email(string id, string sender, string subject, string text)
        {
            ID = id;
            Sender = sender;
            Subject = subject;
            MessageText = text;
            URLs = new List<string>();
        }

        public void ProcessHyperlinks()
        {
            char[] separator = { ' ', '\n', ',', '!', '?', '"' };
            string[] words = MessageText.Split(separator);

            Regex reg = new Regex(@"(((http|ftp|https):\/\/)?[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");

            foreach (string word in words)
            {
                Match match = reg.Match(word);
                if (match.Success)
                {
                    if (word.Length > 0)
                    {
                        if (word.EndsWith(".") || word.EndsWith(","))
                        {
                            MessageText = MessageText.Replace(word.Substring(0, word.Length - 1), "<URL Quarantined>");
                            URLs.Add(word.Substring(0, word.Length - 1));
                        }
                        else
                        {
                            MessageText = MessageText.Replace(word, "<URL Quarantined>");
                            URLs.Add(word);
                        }
                    }
                }
            }
        }
    }
}
