using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Atlas2._0
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string _passwordSalt;
        private string _passwordHash;

        public Login()
        {
            InitializeComponent();
        }

        private void PBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            PassHack.Content = "";

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

            return _passwordHash == passwordHash;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidEmail(Email.Text) && IsFieldNotEmpty(Email.Text) &&
               IsFieldNotEmpty(PBox.Password))
            {
                _passwordSalt = Email.Text;
                _passwordHash = CalculateHash(PBox.Password, _passwordSalt);
                if (ConfirmPassword(PBox.Password))
                {
                    if(db_Check_Record(Email.Text, _passwordHash))
                    {
                        var HomeWindow = new Home();
                        HomeWindow.Show();
                        HomeWindow.LoggedInEmail.Text = Email.Text;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Sign Up", "Error");
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Please key in your details correctly", "Error");
            }
            
        }

        private bool db_Check_Record(string Email, string _passwordHash)
        {
            string SelectSQL = "SELECT * from Users WHERE email='" + Email + "'";
            ClsDB.GetData(SelectSQL);

            return _passwordHash == ClsDB.Hash;
        }
    }
}
