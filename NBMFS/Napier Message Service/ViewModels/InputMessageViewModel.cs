using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Napier_Message_Service.Commands;
using System.Windows.Input;
using System.Windows;
using Napier_Message_Service.Models;
using System.Windows.Controls;
using Napier_Message_Service.Views;
using System.Windows.Media;

namespace Napier_Message_Service.ViewModels
{
    public class InputMessageViewModel : BaseViewModel
    {
        //OBJECT USED TO PROCESS AND STORE MESSAGES
        MessageProcessingService MPS;
        //REFERENT TO THE UI
        InputMessageView view;

        //TEXTBLOCKS
        public string MessageIdTextBlock { get; private set; }
        public string MessageIdErrorTextBlock { get;  private set; }
        public string MessageBodyTextBlock { get; private set; }
        public string MessageBodyErrorTextBlock { get; private set; }
        public string BrowseFileTextBlock { get; private set; }
        public string UnprocessedMessagesTextBlock { get; private set; }
        public string ProcessedMessagesTextBlock { get; private set; }

        //TEXTBOXES
        public string MessageIdTextBox { get; set; }
        public string MessageBodyTextBox { get; set; }
        public string BrowseFileTextBox { get; set; }

        //BUTTONCONTENTS
        public string SubmitButtonContent { get; private set; }
        public string BrowseButtonContent { get; private set; }
        public string LoadMessageButtonContent { get; private set; }
        public string ProcessNextButtonContent { get; private set; }


        //COMMANDS
        public ICommand SubmitButtonCommand { get; private set; }
        public ICommand BrowseButtonCommand { get; private set; }
        public ICommand LoadMessageButtonCommand { get; private set; }
        public ICommand LoadNextButtonCommand { get; private set; }

        //PANEL USED TO DISPLAY PROCESSED MESSAGES
        public StackPanel DisplayMessagesPanel { get; set; }


        //CONTRUCTOR
        public InputMessageViewModel(InputMessageView view)
        {
            //GET REFERENCE FOR THE UI 
            this.view = view;

            //GET REFERENCE FOR THE MESSAGE PROCESSING SERVICE
            MPS = MessageProcessingService.Instance;

            //Text blocks
            MessageIdTextBlock = "Message ID";
            MessageIdErrorTextBlock = string.Empty;
            MessageBodyTextBlock = "Message body";
            MessageBodyErrorTextBlock = string.Empty;
            BrowseFileTextBlock = "Provide a file path to load messages from file";
            if (MPS.UnprocessedMessages.Count == 0)
            {
                UnprocessedMessagesTextBlock = "There is no unprocessed messages";
            }
            else
            {
                UnprocessedMessagesTextBlock = $"There is {MPS.UnprocessedMessages.Count} unprocessed messages";
            }
            ProcessedMessagesTextBlock = "Processed message";

            //Text boxes
            MessageIdTextBox = string.Empty;
            MessageBodyTextBox = string.Empty;
            BrowseFileTextBox = string.Empty;

            //Button Content
            SubmitButtonContent = "Process";
            BrowseButtonContent = "Browse";
            ProcessNextButtonContent = "Load next";
            LoadMessageButtonContent = "Load messages";

            //Commands
            SubmitButtonCommand = new RelayCommand(SubmitButtonClick);
            BrowseButtonCommand = new RelayCommand(BrowseButtonClick);
            LoadMessageButtonCommand = new RelayCommand(LoadMessageButtonClick);
            LoadNextButtonCommand = new RelayCommand(LoadNextButtonClick);
        }

        //SUBMITS MESSAGE FOR PROCESSING
        public void SubmitButtonClick()
        {
            Message msg = null;

            bool isUnprocessedMessage = MPS.IsUnprocessedMessage(MessageIdTextBox);

            try
            {
                msg = MPS.ProcessMessage(MessageIdTextBox, MessageBodyTextBox);
            }
            catch(Exception e)
            {
                MessageBox.Show("Exception: " + e.ToString());
            }

            if (null == msg)
            {
                if (MessageFactory.ProcessingError != null)
                {
                    MessageBox.Show(MessageFactory.ProcessingError);
                    return;
                }
            }
            else
            {
                if (isUnprocessedMessage)
                {
                    UnprocessedMessagesTextBlock = $"There is {MPS.UnprocessedMessages.Count} unprocessed messages";
                    OnChanged(nameof(UnprocessedMessagesTextBlock));
                }
                MessageBox.Show("Message was processed successfuly");
                ClearInput();
                view.DisplayMessages.Children.Clear();
                DisplayMessage(msg, view.DisplayMessages);
            }
        }


        //LOADS THE NEXT UNPROCESSED MESSAGE INTO THE UI TEXTBOXES
        public void LoadNextButtonClick()
        {
            //If there is no unprocessed messages
            if (MPS.UnprocessedMessages.Count == 0)
            {
                MessageBox.Show("There are no unprocessed messages");
            }
            else
            {
                string message = MPS.LoadNextMessage();


                var m = message.Split(new[] { '\n' }, 2);
                string id = m[0];
                string body = m[1];
                MessageIdTextBox = id;
                OnChanged(nameof(MessageIdTextBox));
                MessageBodyTextBox = body;
                OnChanged(nameof(MessageBodyTextBox));
            }
        }


        //OPENS A FILE BROWSE DIALOG TO GET THE FILE PATH WITH UNPROCESSED MESSAGES
        public void BrowseButtonClick()
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt";
            //dlg.Filter = "Text documents (.txt)|*.txt";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                BrowseFileTextBox = filename;

                // Notify UI property has changed 
                OnChanged(nameof(BrowseFileTextBox));
            }
            else
            {
                MessageBox.Show("Failed to load file!");
            }
        }


        //LOAD UNPROCESSED MESSAGES FROM TEXT FILE
        public void LoadMessageButtonClick()
        {
            MPS.LoadUnprocessedMessagesFromFile(BrowseFileTextBox);
            UnprocessedMessagesTextBlock = $"There is {MPS.UnprocessedMessages.Count} unprocessed messages";
            OnChanged(nameof(UnprocessedMessagesTextBlock));
            BrowseFileTextBox = string.Empty;
            OnChanged(nameof(BrowseFileTextBox));
        }


        //CLEAR INPUT TEXTBOXES
        public void ClearInput()
        {
            MessageIdTextBox = string.Empty;
            OnChanged(nameof(MessageIdTextBox));
            MessageBodyTextBox = string.Empty;
            OnChanged(nameof(MessageBodyTextBox));
        }
    }
}
