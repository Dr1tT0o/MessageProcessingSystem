using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Napier_Message_Service.Commands;
using Napier_Message_Service.Views;
using System.Windows.Input;
using System.Windows.Controls;

namespace Napier_Message_Service.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string InputMessageButtonContent { get; set; }
        public string ViewMessageButtonContent { get; set; }
        public string ViewStatsButtonContent { get; set; }

        public ICommand InputMessageButtonCommand { get; private set; }
        public ICommand ViewMessageButtonCommand { get; private set; }
        public ICommand ViewStatsButtonCommand { get; private set; }


        public UserControl ContentControlBinding { get; private set; }

        public MainWindowViewModel()
        {
            InputMessageButtonContent = "Input Messages";
            ViewMessageButtonContent = "View Messages";
            ViewStatsButtonContent = "View Stats";


            InputMessageButtonCommand = new RelayCommand(InputMessageButtonClick);
            ViewMessageButtonCommand = new RelayCommand(ViewMessageButtonClick);
            ViewStatsButtonCommand = new RelayCommand(TrendingButtonClick);


            ContentControlBinding = new InputMessageView();
        }

        public void InputMessageButtonClick()
        {
            ContentControlBinding = new InputMessageView();
            OnChanged(nameof(ContentControlBinding));
        }

        public void ViewMessageButtonClick()
        {
            ContentControlBinding = new ViewMessageView();
            OnChanged(nameof(ContentControlBinding));
        }

        public void TrendingButtonClick()
        {
            ContentControlBinding = new ViewStatsView();
            OnChanged(nameof(ContentControlBinding));
        }
    }
}
