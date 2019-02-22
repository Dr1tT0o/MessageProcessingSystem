using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using Napier_Message_Service.Models;
using System.Windows.Controls;
using System.Windows;

namespace Napier_Message_Service.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void OnChanged(string name)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        //Displays message on screen
        public void DisplayMessage(Message msg, StackPanel panel)
        {
            //Declare UI elements
            TextBlock messageType = new TextBlock();
            TextBlock messageSender = new TextBlock();
            TextBlock messageSubject = new TextBlock();
            TextBlock messageSortCode = new TextBlock();
            TextBlock messageNatureOfIncident = new TextBlock();
            TextBlock messageText = new TextBlock();
            TextBlock quarantinedHyperlinks = new TextBlock();
            StackPanel messagePanel = new StackPanel();

            //Set properties for stack panel
            messagePanel.Margin = GetMargin(5, 5, 5, 5);
            messagePanel.Background = new SolidColorBrush(Color.FromRgb(68, 68, 68));

            //Set properties for messageType
            messageType.FontSize = 9;
            messageType.FontWeight = FontWeights.Bold;
            messageType.Margin = GetMargin(5, 5, 5, 0);

            //Set properties for messageSender
            messageSender.FontSize = 9;
            messageSender.Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            messageSender.FontWeight = FontWeights.Bold;
            messageSender.Margin = GetMargin(5, 0, 5, 0);
            messageSender.Text = "Sender: " + msg.Sender;

            //Set properties for messageSubject
            messageSubject.FontSize = 9;
            messageSubject.FontWeight = FontWeights.Bold;
            messageSubject.Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            messageSubject.Margin = GetMargin(5, 0, 5, 0);

            //Set properties for messageSortCode
            messageSortCode.FontSize = 9;
            messageSortCode.FontWeight = FontWeights.Bold;
            messageSortCode.Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            messageSortCode.Margin = GetMargin(5, 0, 5, 0);

            //Set properties for messageNatureOfIncident
            messageNatureOfIncident.FontSize = 9;
            messageNatureOfIncident.FontWeight = FontWeights.Bold;
            messageNatureOfIncident.Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            messageNatureOfIncident.Margin = GetMargin(5, 0, 5, 0);


            //Set properties for message text
            messageText.FontSize = 9;
            messageText.Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            messageText.Margin = GetMargin(5, 2, 5, 0);
            messageText.Text = msg.MessageText;
            messageText.TextWrapping = System.Windows.TextWrapping.Wrap;


            //Set properties for quarantined hyperlinks
            quarantinedHyperlinks.FontSize = 9;
            quarantinedHyperlinks.FontWeight = FontWeights.Bold;
            quarantinedHyperlinks.Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            quarantinedHyperlinks.Margin = GetMargin(5, 2, 5, 5);
            quarantinedHyperlinks.TextWrapping = System.Windows.TextWrapping.Wrap;


            //Display Email
            if (msg is Email email)
            {
                messageSubject.Text = "Subject: " + email.Subject;

                string urls = "Hyperlinks quarantined: [";
                foreach (string hyperlink in email.URLs)
                {
                    urls += hyperlink + " ";
                }

                urls.Trim();
                urls += "]";
                quarantinedHyperlinks.Text = urls;

                if (email is SIR sir)
                {
                    messageType.Text = "#" + msg.ID + " (SIR)";
                    messageType.Foreground = new SolidColorBrush(Color.FromRgb(255, 38, 70));
                    messageSortCode.Text = "Sort code: " + sir.SortCode;
                    messageNatureOfIncident.Text = "Nature of incident: " + sir.NatureOfIncident;

                    messagePanel.Children.Add(messageType);
                    messagePanel.Children.Add(messageSender);
                    messagePanel.Children.Add(messageSubject);
                    messagePanel.Children.Add(messageSortCode);
                    messagePanel.Children.Add(messageNatureOfIncident);
                    messagePanel.Children.Add(messageText);
                    messagePanel.Children.Add(quarantinedHyperlinks);
                }
                else
                {
                    messageType.Text = "#" + msg.ID + " (email)";
                    messageType.Foreground = new SolidColorBrush(Color.FromRgb(237, 127, 87));
                    messagePanel.Children.Add(messageType);
                    messagePanel.Children.Add(messageSender);
                    messagePanel.Children.Add(messageSubject);
                    messagePanel.Children.Add(messageText);
                    messagePanel.Children.Add(quarantinedHyperlinks);
                }
            }
            //Display SMS
            else if (msg is SMS sms)
            {
                messageType.Text = "#" +msg.ID + " (sms)";
                messageType.Foreground = new SolidColorBrush(Color.FromRgb(115, 224, 109));

                messagePanel.Children.Add(messageType);
                messagePanel.Children.Add(messageSender);
                messagePanel.Children.Add(messageText);
            }
            //Display Tweet 
            else if (msg is Tweet)
            {
                messageType.Text = "#" + msg.ID + " (tweet)"; ;
                messageType.Foreground = new SolidColorBrush(Color.FromRgb(113, 215, 238));

                messagePanel.Children.Add(messageType);
                messagePanel.Children.Add(messageSender);
                messagePanel.Children.Add(messageText);
            }

            panel.Children.Insert(0, messagePanel);
        }

        public Thickness GetMargin(int left, int top, int right, int bottom)
        {
            Thickness margin = new Thickness
            {
                Left = left,
                Top = top,
                Right = right,
                Bottom = bottom
            };
            return margin;
        }
    }
}
