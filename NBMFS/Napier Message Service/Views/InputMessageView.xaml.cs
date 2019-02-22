using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Napier_Message_Service.ViewModels;


namespace Napier_Message_Service.Views
{
    /// <summary>
    /// Interaction logic for InputMessageView.xaml
    /// </summary>
    public partial class InputMessageView : UserControl
    {
        public InputMessageView()
        {
            InitializeComponent();

            DataContext = new InputMessageViewModel(this);
        }
    }
}
