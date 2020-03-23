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
using belajar_crud_wpf.Model;
using belajar_crud_wpf.MyContext;
using System.Net;
using System.Globalization;
using System.Net.Mail;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;

namespace belajar_crud_wpf
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {

        myContext conn = new myContext();

        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void sendNewPassToEmail(string email, string password, string name)
        {
            string from = "keepkarim@gmail.com";
            string to = email;
            MailMessage mm = new MailMessage(from, to);
            mm.Subject = "New reset password ";
            string isi_pesan = "hi " + name + " this is your request to reset password " + password + " Thank you";
            mm.Body = isi_pesan;
            mm.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new System.Net.NetworkCredential("keepkarim@gmail.com", "karimgmail9");
            smtp.EnableSsl = true;


            try
            {
                smtp.Send(mm);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to send email..." + e.ToString());
            }

        }

        private void Btn_Send_Request_Click(object sender, RoutedEventArgs e)
        {


            var cekemail = conn.Suppliers.Where(S => S.Email == txt_email_request.Text).FirstOrDefault();
            string newpassword = System.Guid.NewGuid().ToString();

            if (cekemail == null)
            {
                MessageBox.Show("Email tidak terdaftar...");
                txt_email_request.Focus();
            }
            else
            {
                sendNewPassToEmail(cekemail.Email, newpassword, cekemail.Name);
                MessageBox.Show("Request password terkirim...");

                // update password
                cekemail.Password = newpassword;
                conn.SaveChanges();
            }


        }
    }
}
