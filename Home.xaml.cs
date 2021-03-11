using Atlas2_0ML.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        
        private void UncheckedBoxImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckedBoxImage.Visibility=Visibility.Visible;
            CheckedItemImage.Visibility = Visibility.Visible;
            UncheckedItemImage.Visibility = Visibility.Visible;
            MachineList.Visibility = Visibility.Visible;
            sfTimePicker.Visibility = Visibility.Hidden;
            calendarEdit.Visibility = Visibility.Hidden;
        }

        private void CheckedBoxImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckedBoxImage.Visibility = Visibility.Hidden;
            CheckedItemImage.Visibility = Visibility.Hidden;
        }


        private void CalendarIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            calendarEdit.Visibility = Visibility.Visible;
            sfTimePicker.Visibility = Visibility.Visible;
            MachineList.Visibility = Visibility.Hidden;
            Logo.Visibility = Visibility.Hidden;
            UncheckedItemImage.Visibility = Visibility.Hidden;
            CheckedItemImage.Visibility = Visibility.Hidden;


        }

        private void DatePicker()
        {
            //select multiple dates or even just one date
            //after selecting a date,select the time
            //
            //if(selection_IsNotNull)
            //Make time picker visible
            //if(Timepicker is not null)
            //get date and time and pass the variables to our Processor class.
            //Date and time will be class variables in Home
            //Run Processor class instance initialized in home
            
        }

        private void ShowTempData()
        {
            //After the Processor class has finished its job,get the utilization and temperature variables
            //render the variables on the screen
            //display the color pill on the right side of the variables
        }

        private void UncheckedItemImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckedItemImage.Visibility = Visibility.Visible;
            //UncheckedItemImage.Visibility = Visibility.Hidden;
        }

        private void CheckedItemImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //UncheckedItemImage.Visibility = Visibility.Visible;
            CheckedItemImage.Visibility = Visibility.Hidden;

        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            sfTimePicker.Visibility = Visibility.Hidden;
            UncheckedItemImage.Visibility = Visibility.Hidden;
            CheckedItemImage.Visibility = Visibility.Hidden;
            calendarEdit.Visibility = Visibility.Hidden;
            MachineList.Visibility = Visibility.Visible;
            Temp.Visibility = Visibility.Visible;

            TimeSelected.Visibility = Visibility.Visible;
            string Time = sfTimePicker.Value.ToString();
            TimeSelected.Text = Time;
            DateTime dt = DateTime.Parse(Time);
            string TBP = dt.ToString("HH:mm:ss");
            ModelOutput Out=Processor.Predict(TBP);

            NumberFormatInfo SetPrecision = new NumberFormatInfo();

            SetPrecision.NumberDecimalDigits = 2;
            if (Out.Score<=40)
            {
                Green.Visibility = Visibility.Visible;
            }
            else if(Out.Score<60 && Out.Score>40){
                Mixed.Visibility = Visibility.Visible;
            }
            else
            {
                Red.Visibility = Visibility.Visible;
            }

            PredictedValue.Text=Out.Score.ToString("N", SetPrecision); 
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            var LoginWindow = new Login();
            LoginWindow.Show();
            Close();
        }

        public void db_Delete_Record(string Email)
        {
            string SelectSQL = "DELETE FROM Users WHERE email='" + Email + "'";
            ClsDB.Execute_SQL(SelectSQL);
            var m = new MainWindow();
            m.Show();
            this.Close();

        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            var PromptWindow = new Prompt();
            PromptWindow.Show();
        }
    }
}
