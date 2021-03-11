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
using System.Windows.Shapes;

namespace Atlas2._0
{
    /// <summary>
    /// Interaction logic for Prompt.xaml
    /// </summary>
    public partial class Prompt : Window
    {
        public Prompt()
        {
            InitializeComponent();
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Home h = (Home)Application.Current.Windows[0];
            h.db_Delete_Record(h.LoggedInEmail.Text);
            Close();
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
