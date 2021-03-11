using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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

namespace Atlas2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //< db oeffnen >

        static string cn_String = Properties.Settings.Default.connection_String;

        SqlConnection cn = new SqlConnection(cn_String);

        //</ db oeffnen >

        string _passwordSalt;
        private string _passwordHash;
        

        public MainWindow()
        {
            InitializeComponent();
            Hack();
        }

        public void Hack()
        {
            if (PassHack.IsMouseDirectlyOver)
                PassHack.Content = "";
            if (ConfirmPassHack.IsMouseDirectlyOver)
                ConfirmPassHack.Content = "";

        }

        private void PBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            PassHack.Content = "";

        }

        private void CPBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ConfirmPassHack.Content = "";
        }



        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if(IsValidEmail(Email.Text)&& IsFieldNotEmpty(Email.Text) && 
               IsFieldNotEmpty(PBox.Password) && IsFieldNotEmpty(CPBox.Password))
            {
                _passwordSalt = Email.Text;
                _passwordHash = CalculateHash(PBox.Password,_passwordSalt);
                if (ConfirmPassword(CPBox.Password))
                {
                    db_Update_Add_Record(Email.Text, _passwordHash);
                    var HomeWindow = new Home();
                    HomeWindow.LoggedInEmail.Text = Email.Text;
                    HomeWindow.Show();
                    Close();
                }                
            }
            else
            {
                MessageBox.Show("Please key in your details correctly", "Error");
            }

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
          
            var LoginWindow = new Login();
            LoginWindow.Show();
            Close();

        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        bool IsFieldNotEmpty(string Field)
        {
            return !(Field == "");
        }

        

        private string CalculateHash(string clearTextPassword, string salt)
        {
            // Convert the salted password to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(clearTextPassword + salt);
            // Use the hash algorithm to calculate the hash
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // Return the hash as a base64 encoded string that can be compared to the stored password
            return Convert.ToBase64String(hash);
        }

        public bool ConfirmPassword(string password)
        {
            string passwordHash = CalculateHash(password, _passwordSalt);

            return _passwordHash==passwordHash;
        }

        private void db_Update_Add_Record(string Email,string Hash)

        {

            //--------< db_Update_Add_Record() >--------

            //*Update or add Record

            //< correct>

            Name = Name.Replace("'", "''");

            Email = Email.Replace("'", "''");

            //</ correct>



            //< find record >

            string sSQL = "SELECT TOP 1 * FROM Users WHERE [email] Like '" + Email + "'";

            DataTable tbl = ClsDB.Get_DataTable(sSQL);

            //</ find record >



            if (tbl.Rows.Count == 0)

            {

                //< add >

                string sql_Add = "INSERT INTO Users ([email],[hash]) VALUES('" + Email + "', '" + Hash + "')";
                
                
                ClsDB.Execute_SQL(sql_Add);

                //</ add >

            }

            else

            {

                //< update >

                string ID = tbl.Rows[0]["IDDetail"].ToString();

                string sql_Update = "UPDATE Users SET [dtScan] = SYSDATETIME() WHERE IDDetail = " + ID;

                ClsDB.Execute_SQL(sql_Update);

                //</ update >

            }

            //--------</ db_Update_Add_Record() >--------

        }
    }
}
