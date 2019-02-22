using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Napier_Message_Service.Commands;
using System.Windows.Input;
using System.Windows;
using Napier_Message_Service.Views;
using System.Windows.Media;
using Napier_Message_Service.Models;

namespace Napier_Message_Service.ViewModels
{
    public class ViewMessageViewModel : BaseViewModel
    {
        //Reference for UI window
        ViewMessageView view;

        //OBJECT USED TO PROCESS AND STORE MESSAGES
        MessageProcessingService MPS;

        //Text Blocks
        public string SearchMessageTextBlock { get; private set; }
        public string ProcessedMessagesTextBlock { get; private set; }

        //Text Boxes
        public string SearchMessageTextBox { get; set; }

        //Commands
        public ICommand SearchButtonCommand { get; private set; }
        public ICommand FilterAllButtonCommand { get; private set; }
        public ICommand FilterTweetButtonCommand { get; private set; }
        public ICommand FilterSmsButtonCommand { get; private set; }
        public ICommand FilterEmailButtonCommand { get; private set; }
        public ICommand FilterSirButtonCommand { get; private set; }


        public ViewMessageViewModel( ViewMessageView view )
        {
            this.view = view;

            MPS = MessageProcessingService.Instance;

            //Text Blocks
            SearchMessageTextBlock = "Search message by id";
            ProcessedMessagesTextBlock = "Processed messages";

            //Text Boxes
            SearchMessageTextBox = string.Empty;

            //Commands
            SearchButtonCommand = new RelayCommand(SearchButtonClick);
            FilterAllButtonCommand = new RelayCommand(FilterAllButtonClick);
            FilterTweetButtonCommand = new RelayCommand(FilterTweetButtonClick);
            FilterSmsButtonCommand = new RelayCommand(FilterSmsButtonClick);
            FilterEmailButtonCommand = new RelayCommand(FilterEmailButtonClick);
            FilterSirButtonCommand = new RelayCommand(FilterSirButtonClick);
        }

        public void SearchButtonClick()
        {
            if (!string.IsNullOrEmpty(SearchMessageTextBox))
            {
                //Clearing existing elements in the panel
                view.DisplayMessagesPanel.Children.Clear();

                //Get message
                Message msg = MPS.GetMessageById(SearchMessageTextBox);

                //Display message
                if (msg != null)
                {
                    DisplayMessage(msg, view.DisplayMessagesPanel);
                    ResetOriginalButtonBackground();
                }
                else
                {
                    MessageBox.Show("Could not find a message with the specified id. Please enter a valid message id");
                }
            }
            else
            {
                MessageBox.Show("Empty or invalid message id. Enter a valid message id");
            }
        }

        public void FilterAllButtonClick()
        {
            //Change button background and set the background of the others buttons to the default value
            ResetOriginalButtonBackground();
            view.ButtonViewAll.Background = new SolidColorBrush(Color.FromRgb(50, 188, 55));

            //Get the list with all messages
            List<Message> allMessages = MPS.GetAllMessages();

            if (allMessages != null && allMessages.Count > 0)
            {
                //Clearing existing elements in the panel
                view.DisplayMessagesPanel.Children.Clear();

                foreach (Message msg in allMessages)
                {
                    //Display message
                    DisplayMessage(msg, view.DisplayMessagesPanel);
                }
            }
            else
            {
                MessageBox.Show("There are no processed messages to display");
            }

        }

        public void FilterTweetButtonClick()
        {
            //Change button background and set the background of the others buttons to the default value
            ResetOriginalButtonBackground();
            view.ButtonViewTweet.Background = new SolidColorBrush(Color.FromRgb(50, 188, 55));

            //Get the list with all Tweets
            List<Tweet> tweets = MPS.GetTweets();

            if (tweets != null && tweets.Count > 0)
            {
                //Clearing existing elements in the panel
                view.DisplayMessagesPanel.Children.Clear();

                foreach (Tweet tweet in tweets)
                {
                    //Display tweets
                    DisplayMessage(tweet, view.DisplayMessagesPanel);
                }
            }
            else
            {
                MessageBox.Show("There are no processed tweets to display");
            }
        }

        public void FilterSmsButtonClick()
        {
            //Change button background and set the background of the others buttons to the default value
            ResetOriginalButtonBackground();
            view.ButtonViewSms.Background = new SolidColorBrush(Color.FromRgb(50, 188, 55));

            //Get List of sms
            List<SMS> smss = MPS.GetSmss();

            if (smss != null && smss.Count > 0)
            {
                //Clearing existing elements in the panel
                view.DisplayMessagesPanel.Children.Clear();

                foreach (SMS sms in smss)
                {
                    //Display sms's
                    DisplayMessage(sms, view.DisplayMessagesPanel);
                }
            }
            else
            {
                MessageBox.Show("There are no processed sms to display");
            }
        }

        public void FilterEmailButtonClick()
        {
            //Change button background and set the background of the others buttons to the default value
            ResetOriginalButtonBackground();
            view.ButtonViewEmail.Background = new SolidColorBrush(Color.FromRgb(50, 188, 55));

            //Get List of emails
            List<Email> emails = MPS.GetEmails();

            if (emails != null && emails.Count > 0)
            {
                //Clearing existing elements in the panel
                view.DisplayMessagesPanel.Children.Clear();

                foreach (Email email in emails)
                {
                    //Display email
                    DisplayMessage(email, view.DisplayMessagesPanel);
                }
            }
            else
            {
                MessageBox.Show("There are no processed emails to display");
            }
        }

        public void FilterSirButtonClick()
        {
            //Change button background and set the background of the others buttons to the default value
            ResetOriginalButtonBackground();
            view.ButtonViewSir.Background = new SolidColorBrush(Color.FromRgb(50, 188, 55));

            //Get List of SIRs
            List<SIR> sirs = MPS.GetSirs();

            if (sirs != null && sirs.Count > 0)
            {
                //Clearing existing elements in the panel
                view.DisplayMessagesPanel.Children.Clear();

                foreach (SIR sir in sirs)
                {
                    //Display sir
                    DisplayMessage(sir, view.DisplayMessagesPanel);
                }
            }
            else
            {
                MessageBox.Show("There are no processed sirs to display");
            }

        }

        private void ResetOriginalButtonBackground()
        {
            view.ButtonViewAll.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
            view.ButtonViewTweet.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
            view.ButtonViewSms.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
            view.ButtonViewEmail.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
            view.ButtonViewSir.Background = new SolidColorBrush(Color.FromRgb(238, 238, 238));
        }
    }
}
