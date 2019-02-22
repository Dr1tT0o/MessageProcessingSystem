using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Message_Service.Models
{
    public class MessageProcessingService
    {
        private static MessageProcessingService instance;
        public List<Message> MessageList { get;  }
        public List<string> UnprocessedMessages { get;  }
        public int UnprocessedMessageCounter { get; set; }
        public List<string> HyperlinkList { get;  }
        public Dictionary<string, string> SRIList { get; }
        public Dictionary<string, int> TrendingList { get; }
        public List<string> ListOfMentions { get; }


        //CONSTRUCTOR
        private MessageProcessingService()
        {
            MessageList = new List<Message>();
            UnprocessedMessages = new List<string>();
            HyperlinkList = new List<string>();
            SRIList = new Dictionary<string, string>();
            TrendingList = new Dictionary<string, int>();
            ListOfMentions = new List<string>();
            UnprocessedMessageCounter = 0;
        }


        //GET INSTANCE OF CLASS
        public static MessageProcessingService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MessageProcessingService();
                }
                return instance;
            }
        }


        //PROCESS MESSAGE
        public Message ProcessMessage(string id, string body)
        {

            Message message = MessageFactory.CreateMessage(id, body);

            if (message != null)
            {
                //IF MESSAGE BEING PROCESSED IS IN UNPROCESSED LIST THEN REMOVE IT FROM THE LIST
                if (IsUnprocessedMessage(id))
                {
                    RemoveFromUnprocessedList(id);
                    UnprocessedMessageCounter--;
                }

                if (message is SMS sms)
                {
                    //REPLACE TEXTSPEAK ABBREVIATIONS
                    sms.ProcessAbbreviations();
                }
                else if (message is Tweet tweet)
                {
                    //REPLACE TEXTSPEAK ABBREVIATIONS
                    tweet.ProcessAbbreviations();
                    //PROCESS TWEET MENTIONS
                    tweet.ProcessMentions();
                    //PROCESS TWEET HASHTAGS
                    tweet.ProcessHashtags();
                }
                else if (message is Email email)
                {
                    //PROCESS EMAIL HYPERLINKS
                    email.ProcessHyperlinks();
                }

                //ADD MESSAGE TO LIST
                MessageList.Add(message);

                //ADD MESSAGE TO JSON FILE
                IOService IO = new IOService();
                IO.SaveMessageToJSON(message);
            }

            return message;
        }

        //LOAD MESSAGES FROM FILE INTO THE UNPROCESSED MESSAGES LIST
        public void LoadUnprocessedMessagesFromFile(string filepath)
        {
            List<string> messages = null;
            try
            {
                IOService IO = new IOService();
                messages = IO.GetMessagesFromFile(filepath);
                foreach (string message in messages)
                {
                    if (message != null && !string.IsNullOrEmpty(message))
                    {
                        if (!UnprocessedMessages.Contains(message))
                        {
                            UnprocessedMessages.Add(message);
                        }
                    }
                }
            }
            catch (Exception e){
                //PRINT EXCEPTION TO THE CONSOLE
                Console.WriteLine(e.ToString());
            }
        }


        //LOAD MESSAGE FROM UNPROCESSED LIST
        public string LoadNextMessage()
        {
            if (UnprocessedMessageCounter == UnprocessedMessages.Count)
            {
                UnprocessedMessageCounter = 0;
            }
            string unprocessedMessage = UnprocessedMessages[UnprocessedMessageCounter];
            UnprocessedMessageCounter++;
            return unprocessedMessage;
        }


        //CHECK IF MESSAGE IS IN THE UNPROCESSED LIST
        public bool IsUnprocessedMessage(string id)
        {
            foreach (string message in UnprocessedMessages)
            {
                if (message.Split('\n')[0] == id)
                {
                    return true;
                }
            }
            return false;
        }


        //REMOVE MESSAGE FROM THE UNPROCESSED LIST
        public void RemoveFromUnprocessedList(string id)
        {
            for (int i = UnprocessedMessages.Count - 1; i >= 0; i--)
            {
                string message = UnprocessedMessages[i];
                if (message.Split('\n')[0] == id)
                {
                    UnprocessedMessages.RemoveAt(i);
                }
            }
        }


        //RETURN MESSAGE BY ID
        public Message GetMessageById(string id)
        {
            foreach (Message message in MessageList)
            {
                if (message.ID == id)
                {
                    return message;
                }
            }
            return null;
        }


        //RETURN LIST WITH ALL MESSAGES
        public List<Message> GetAllMessages()
        {
            return MessageList;
        }


        //RETURN LIST OF TWEETS
        public List<Tweet> GetTweets()
        {
            List<Tweet> tweets = new List<Tweet>();
            foreach (Message message in MessageList)
            {
                if (message is Tweet tweet)
                {
                    tweets.Add(tweet);
                }
            }
            return tweets;
        }


        //RETURN LIST OF SMS
        public List<SMS> GetSmss()
        {
            List<SMS> smss = new List<SMS>();
            foreach (Message message in MessageList)
            {
                if (message is SMS sms)
                {
                    smss.Add(sms);
                }
            }
            return smss;
        }


        //RETURN LIST OF EMAILS
        public List<Email> GetEmails()
        {
            List<Email> emails = new List<Email>();
            foreach (Message message in MessageList)
            {
                if (message is Email email && !(message is SIR))
                {
                    emails.Add(email);
                }
            }
            return emails;
        }


        //RETURN LIST OF SIRs
        public List<SIR> GetSirs()
        {
            List<SIR> sirs = new List<SIR>();
            foreach (Message message in MessageList)
            {
                if (message is SIR sir)
                {
                    sirs.Add(sir);
                }
            }
            return sirs;
        }


        //RETURN LIST OF MENTIONS
        public List<string> GetListOfMentions()
        {
            List<string> mentions = new List<string>();

            foreach (Message message in MessageList)
            {
                if (message is Tweet tweet)
                {
                    foreach (string mention in tweet.Mentions)
                    {
                        if (!mentions.Contains(mention))
                        {
                            mentions.Add(mention);
                        }
                    }
                }
            }

            return mentions;
        }


        //RETURN THE TRENDING LIST
        public Dictionary<string, int> GetTrendingList()
        {
            var trending = new Dictionary<string, int>();

            foreach (Message message in MessageList)
            {
                if (message is Tweet tweet)
                {
                    foreach (string hashtag in tweet.Hashtags)
                    {
                        if (!trending.ContainsKey(hashtag))
                        {
                            trending.Add(hashtag, 1);
                        }
                        else
                        {
                            trending[hashtag]++;
                        }
                    }
                }
            }
            return trending;
        }
    }
}
