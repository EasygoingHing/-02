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

namespace AccidentGraph
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void btnAutorization_Click(object sender, RoutedEventArgs e)
        {
            DataAutorization.Password = tbPassword.Text;
            DataAutorization.Login = tblogin.Text;

            if (DataAutorization.Login == "Admin" && DataAutorization.Password == "Admin"
                || DataAutorization.Login == "User" && DataAutorization.Password == "User")
            {
                this.Hide();                
                MainWindow main = new MainWindow();     
                main.Owner = this;
                main.Show();                
            }
            else
            {
                tblogin.Text = null;
                tbPassword.Text = null;
                MessageBox.Show("Данные для входа неверные");
                tblogin.Focus();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
           Environment.Exit(0);
        }
    }
}
