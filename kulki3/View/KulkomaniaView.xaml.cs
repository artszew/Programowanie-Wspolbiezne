using Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace View
{
    /// <summary>
    /// Interaction logic for KulkomaniaView.xaml
    /// </summary>
    public partial class KulkomaniaView : Window
    {
        public KulkomaniaView()
        {
            InitializeComponent();
            DataContext = new ViewModel.KulkomaniaViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}