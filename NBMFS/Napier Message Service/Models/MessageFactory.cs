using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Napier_Message_Service.Models
{
    public static class MessageFactory
    {
        public static string ProcessingError { get; set; }

        static MessageFactory() {

        }

        //RETURN CORRECT MESSAGE OBJECT DEPENDING ON MESSAGE TYPE
        public static Message CreateMessage(string id, string body)
        {
            //RESET ERROR TO NULL
            ProcessingError = null;


            //VALIDATE ID
            if (null == id || id.Length != 10)
            {
                ProcessingError = "Invalid message id. Please enter a valid message id.";
                return null;
            }
            else
            {
                try
                {
                    Int32.Parse(id.Substring(1));
                }
                catch
                {
                    ProcessingError = "Invalid message id. Please enter a valid message id.";
                    return null;
                }
            }

            //IF ID IS VALID
            if (!string.IsNullOrEmpty(body))
            {
                if (id.StartsWith("E"))
                {
                    return CreateEmail(id, body);
                }
                else if (id.StartsWith("S"))
                {
                    return CreateSms(id, body);
                }
                else if (id.StartsWith("T"))
                {
                    return CreateTweet(id, body);
                }
                else
                {
                    ProcessingError = "Invalid message id. Message id must start with t for tweets, s for sms and e for emails";
                    return null;
                }
            }
            else
            {
                ProcessingError = "Message body is empty. Please enter a message body";
                return null;
            }
        }

        //RETURNS A TWEET OBJECT OR NULL IF INPUT IS INVALID
        private static Tweet CreateTweet(string id, string body)
        {

            //DECLARE MESSAGE ATTRIBUTES
            string[] messageAttributes = body.Split('\n');
            string sender = string.Empty;
            string text = string.Empty;

            // VALIDATE TWITTER SENDER
            try
            {
                sender = messageAttributes[0];
                if (string.IsNullOrEmpty(sender) || !sender.StartsWith("@"))
                {
                    ProcessingError = "Invalid tweet sender. tweet sender must be a valid tweeter ID (starting with @)";
                    return null;
                }
                else
                {
                    if (sender.Length > 16)
                    {
                        ProcessingError = "Invalid tweet sender. Tweet sender must be no longer than 15 characters.";
                        return null;
                    }
                }
            }
            catch
            {
                ProcessingError = "Tweet sender is blank. Please enter the tweet sender in the form of a valid tweeter id (starting with @)";
                return null;
            }

            //VALIDATE TWEET TEXT
            StringBuilder tweetText = new StringBuilder();
            for (int i = 1; i < messageAttributes.Length; i++)
            {
                string line = messageAttributes[i] + "\n";
                tweetText.Append(line);
            }

            text = tweetText.ToString().Substring(0, tweetText.Length - 1);

            if (string.IsNullOrEmpty(text))
            {
                ProcessingError = "Tweet text is empty. Please enter the tweet text";
                return null;
            }
            else
            {
                if (text.Length > 140)
                {
                    ProcessingError = "Tweets must be no longer than 140 characters.";
                    return null;
                }
            }

            return new Tweet(id, sender, text);
        }


        //RETURNS A SMS OBJECT OR NULL IF INPUT IS INVALID
        private static SMS CreateSms(string id, string body)
        {
            //DECLARE MESSAGE ATTRIBUTES
            string[] messageAttributes = body.Split('\n');
            string sender = string.Empty;
            string text = string.Empty;

            //VALIDATE SMS SENDER
            try
            {
                sender = messageAttributes[0];
                if (string.IsNullOrEmpty(sender) || !sender.StartsWith("+"))
                {
                    ProcessingError = "Invalid SMS sender. Please enter a valid SMS sender in the form of a valid international phone number (starting with +).";
                    return null;
                }
                else
                {
                    try
                    {
                        long.Parse(sender.Substring(1));
                    }
                    catch
                    {
                        ProcessingError = "Invalid SMS sender. Please enter a valid SMS sender in the form of a valid international phone number (starting with +).";
                        return null;
                    }
                }
            }
            catch
            {
                ProcessingError = "SMS sender is blank. Please enter the sms sender in the form of an international phone number (starting with +)";
                return null;
            }

            //VALIDATE SMS TEXT
            StringBuilder smsText = new StringBuilder();
            for (int i = 1; i < messageAttributes.Length; i++)
            {
                string line = messageAttributes[i] + "\n";
                smsText.Append(line);
            }

            text = smsText.ToString().Substring(0, smsText.Length - 1);

            if (string.IsNullOrEmpty(text))
            {
                ProcessingError = "SMS text is empty. Please enter the message text";
                return null;
            }
            else
            {
                if (text.Length > 140)
                {
                    ProcessingError = "SMS text must be no longer than 140 characters.";
                    return null;
                }
            }

            return new SMS(id, sender, text);
        }

        //RETURNS AN EMAIL OR SIR OBJECT OR NULL IF INPUT IS INVALID
        private static Email CreateEmail(string id, string body)
        {
            //DECLARE MESSAGE ATTRIBUTES
            string[] messageAttributes = body.Split('\n');
            string sender = string.Empty;
            string subject = string.Empty;
            string sortcode = string.Empty;
            string noi = string.Empty;
            string text = string.Empty;

            //VALIDATE SENDER
            try
            {
                messageAttributes = body.Split('\n');
                sender = messageAttributes[0].Trim();
                Regex reg = new Regex(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$");
                Match match = reg.Match(sender);
                if (!match.Success)
                {
                    ProcessingError = "Invalid email sender. The email sender must be a valid email address.";
                    return null;
                }
            }
            catch
            {
                ProcessingError = "Invalid email sender. The email sender must be a valid email address.";
                return null;
            }

            //VALIDATE SUBJECT
            try
            {
                subject = messageAttributes[1].Trim();
                if (subject.Length > 20)
                {
                    ProcessingError = "Email subject must be no longer than 20 characters";
                    return null;
                }
            }
            catch
            {
                ProcessingError = "Email messages must have a subject. Please enter an email subject";
                return null;
            }

            //CHECK IF EMAIL SUBJECT IS SIR (SIR EMAIL HAVE A SUBJECT WITH A DATE WITH FORMAT DD/MM/YY
            if (subject.StartsWith("SIR") && subject.Length > 3 && DateTime.TryParseExact(subject.Substring(4), "dd/mm/yy", null, System.Globalization.DateTimeStyles.None, out DateTime res))
            {
                try
                {
                    //VALIDATE SORT CODE
                    sortcode = messageAttributes[2].Trim();
                    Regex reg = new Regex(@"^[0-9]{2}-[0-9]{2}-[0-9]{2}$");
                    Match match = reg.Match(sortcode);
                    if (!match.Success || sortcode.Length != 8)
                    {
                        ProcessingError = "Invalid sort-code in SIR. Please enter a valid sort-code";
                        return null;
                    }
                }
                catch
                {
                    ProcessingError = "Invalid sort-code in SIR. Please enter a valid sort-code";
                    return null;
                }

                //VALIDATE NATURE OF INCIDENT
                string[] natureOfIncident = { "theft", "staff attack", "atm theft", "raid", "customer attack", "staff abuse", "bomb threat", "terrorism", "suspicious incident", "intelligence", "cash loss" };
                try
                {
                    noi = messageAttributes[3].Trim();
                    if (!natureOfIncident.Contains(noi.ToLower()))
                    {
                        ProcessingError = "SIR nature of incident unknown. Please enter a valid nature of incident.";
                        return null;
                    }
                }
                catch
                {
                    ProcessingError = "SIR nature of incident unknown. Please enter a valid nature of incident.";
                    return null;
                }


                //VALIDATE SIR TEXT
                StringBuilder sriText = new StringBuilder();
                for (int i = 4; i < messageAttributes.Length; i++)
                {
                    string line = messageAttributes[i] + "\n";
                    sriText.Append(line);
                }

                text = sriText.ToString().Substring(0, sriText.Length - 1);

                if (string.IsNullOrEmpty(text))
                {
                    ProcessingError = "SIR message text is empty. Please enter the message text";
                    return null;
                }
                else
                {
                    if (text.Length > 1024)
                    {
                        ProcessingError = "SIR message text must be no longer than 1024 characters.";
                        return null;
                    }
                }
                return new SIR(id, sender, subject, sortcode, noi, text);
            }

            //VALIDATE EMAIL TEXT
            StringBuilder emailText = new StringBuilder();
            for (int i = 2; i < messageAttributes.Length; i++)
            {
                string line = messageAttributes[i] + "\n";
                emailText.Append(line);
            }

            text = emailText.ToString().Substring(0, emailText.Length - 1);

            if (string.IsNullOrEmpty(text))
            {
                ProcessingError = "Email message text is empty. Please enter the message text";
                return null;
            }
            else
            {
                if (text.Length > 1024)
                {
                    ProcessingError = "Email messages must be no longer than 1024 characters.";
                    return null;
                }
            }

            return new Email(id, sender, subject, text);
        }
    }
}
