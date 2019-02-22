using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Message_Service.Models
{
    public class Tweet : Message
    {
        public List<string> Mentions { get; }
        public List<string> Hashtags { get; }


        public Tweet(string id, string sender, string text)
        {
            ID = id;
            Sender = sender;
            MessageText = text;
            Mentions = new List<string>();
            Hashtags = new List<string>();
        }

        public void ProcessHashtags()
        {
            char[] separator = { ' ', '\n', '.', ',', '!', '?', '\"'};
            string[] words = MessageText.Split(separator);
            foreach (string word in words)
            {
                if (word.StartsWith("#"))
                {
                    Hashtags.Add(word);
                }
            }
        }

        public void ProcessMentions()
        {
            char[] separator = { ' ', '\n', '.', ',', '!', '?', '\"' };
            string[] words = MessageText.Split(separator);
            foreach (string word in words)
            {
                if (word.StartsWith("@") && word.Length <= 16)
                {
                    Mentions.Add(word);
                }
            }
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
