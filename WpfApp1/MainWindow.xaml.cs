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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ShowActivated = false;
        }

        private void GetTextButton_Click(object sender, RoutedEventArgs e)
        {
            // Win32EditBoxHost의 Text 속성을 통해 텍스트 가져오기
            string textFromWin32Edit = MyWin32EditHost.Text;
            MessageBox.Show($"Text from Win32 Edit Box: \n\n{textFromWin32Edit}", "Win32 Control Text");
        }

        private void FocusEditButton_Click(object sender, RoutedEventArgs e)
        {
            MyWin32EditHost.FocusControl();
        }
    }
}