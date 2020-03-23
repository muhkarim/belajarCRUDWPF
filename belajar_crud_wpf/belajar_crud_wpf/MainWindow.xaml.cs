using belajar_crud_wpf.Model;
using belajar_crud_wpf.MyContext;
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
using System.Text.RegularExpressions;
using System.Net;
using System.Globalization;
using System.Net.Mail;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;


namespace belajar_crud_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        myContext conn = new myContext(); // ceclare new obj myContext

        public int cb_sup; // menampung supplier_id

        public int role_id = 1;

        public MainWindow()
        {
            InitializeComponent();

            tbl_supplier.ItemsSource = conn.Suppliers.ToList(); // refresh table supplier

            tbl_item.ItemsSource = conn.Items.ToList(); // refresh table Item

            combo_supplier.ItemsSource = conn.Suppliers.ToList(); // refresh combo suppliers
           
        }



        

        // Insert Supplier 
        private void Btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            string password = System.Guid.NewGuid().ToString();

            
            var input = new Supplier(txt_name.Text, txt_address.Text, txt_email.Text, password);
            
            
            // patern email
            string pattern = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9[\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";

            // validation
            if (txt_name.Text == "")
            {
                MessageBox.Show("Nama tidak boleh kosong...");
                txt_name.Focus();
            }
            else if (txt_address.Text == "")
            {
                MessageBox.Show("Address tidak boleh kosong...");
                txt_address.Focus();
            } else if (txt_email.Text == "")
            {
                MessageBox.Show("Email tidak boleh kosong...");
                txt_email.Focus();

            } else if (!Regex.Match(txt_email.Text, pattern).Success)
            {
                MessageBox.Show("format email salah...");
                txt_email.Focus();
            } else
            {
                // push to database
                conn.Suppliers.Add(input);
               
                conn.SaveChanges();
                sendPassToEmail(txt_email.Text, password, txt_name.Text); // method send password to email
                txt_name.Text = string.Empty;
                txt_address.Text = string.Empty;
                txt_email.Text = string.Empty;
                MessageBox.Show("Data Berhasil ditambahkan");
            }

            tbl_supplier.ItemsSource = conn.Suppliers.ToList(); // refresh table

        }

        // Data Grid Supplier
        // Using Selection
        private void tbl_supplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
            var data = tbl_supplier.SelectedItem;

            if (data == null) {
                tbl_supplier.ItemsSource = conn.Suppliers.ToList();
            } else
            {
                string id = (tbl_supplier.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                txt_id.Text = id;

                string name = (tbl_supplier.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
                txt_name.Text = name;

                string address = (tbl_supplier.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
                txt_address.Text = address;

                string email = (tbl_supplier.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
                txt_email.Text = email;
            }

        }

        // Update Supplier
        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            int inputId = Convert.ToInt32(txt_id.Text); // menangkap id dari textbox id
            var cekId = conn.Suppliers.Where(S => S.Id == inputId).FirstOrDefault(); // s -> objek dari tbl_supplier

            cekId.Name = txt_name.Text;
            cekId.Address = txt_address.Text;
            var update = conn.SaveChanges();
            MessageBox.Show(update + " Data berhasil di update");

            tbl_supplier.ItemsSource = conn.Suppliers.ToList(); // refresh table
        }


        // Delete supplier
        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            int inId = Convert.ToInt32(txt_id.Text); // menangkap id dari textbox id
            var cekId = conn.Suppliers.Where(S => S.Id == inId).FirstOrDefault(); // s -> objek dari tbl_supplier

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                conn.Suppliers.Remove(cekId);
                var delete = conn.SaveChanges();
                txt_id.Text = string.Empty;
                txt_name.Text = string.Empty;
                txt_address.Text = string.Empty;
                tbl_supplier.ItemsSource = conn.Suppliers.ToList();
            }
             
        }

       



        // Set up combo supplier
        private void combo_supplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_sup = Convert.ToInt32(combo_supplier.SelectedValue); // handle load supplier input in Item menu
        }

        // Insert Item
        private void btn_item_insert_Click(object sender, RoutedEventArgs e)
        {
            string item_pattern = "[^a-zA-Z0-9]";

            try
            {
               
                // validasi
                if (txt_item_name.Text == "")
                {
                    MessageBox.Show("Nama item tidak boleh kosong...");
                    txt_item_name.Focus();
                }
                else if (Regex.IsMatch(txt_item_name.Text, item_pattern))
                {
                    MessageBox.Show("Format nama item salah");
                    txt_item_name.Focus();
                }
                
                else if (txt_item_price.Text == "")
                {
                    MessageBox.Show("Price tidak boleh kosong...");
                    txt_item_price.Focus();
                }
                else if (txt_item_stock.Text == "")
                {
                    MessageBox.Show("Stok tidak boleh kosong...");
                    txt_item_stock.Focus();
                }
                else if (combo_supplier.Text == "")
                {
                    MessageBox.Show("Supplier tidak boleh kosong...");
                    combo_supplier.Focus();
                }
                else
                {
                    var inPrice = Convert.ToInt32(txt_item_price.Text);
                    var inStock = Convert.ToInt32(txt_item_stock.Text);
                    var inSupp = conn.Suppliers.Where(S => S.Id == cb_sup).FirstOrDefault();

                    var inputItem = new Item(txt_item_name.Text, inPrice, inStock, inSupp);
                    conn.Items.Add(inputItem);
                    var insert = conn.SaveChanges();

                    MessageBox.Show(insert + "Data telah ditambahkan...");
                    txt_item_name.Text = string.Empty;
                    txt_item_price.Text = string.Empty;
                    txt_item_stock.Text = string.Empty;
                    combo_supplier.Text = string.Empty;
                }



                tbl_item.ItemsSource = conn.Items.ToList(); // refresh table item
            }
            catch
            {

            }

           
        }

        // Select datagrid table item
        private void tbl_item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var data = tbl_item.SelectedItem;

            if (data == null)
            {
                tbl_item.ItemsSource = conn.Items.ToList();
            }
            else
            {
                string id = (tbl_item.SelectedCells[0].Column.GetCellContent(data) as TextBlock).Text;
                txt_item_id.Text = id;

                string name = (tbl_item.SelectedCells[1].Column.GetCellContent(data) as TextBlock).Text;
                txt_item_name.Text = name;

                string price = (tbl_item.SelectedCells[2].Column.GetCellContent(data) as TextBlock).Text;
                txt_item_price.Text = price;

                string stock = (tbl_item.SelectedCells[3].Column.GetCellContent(data) as TextBlock).Text;
                txt_item_stock.Text = stock;

                string supplier = (tbl_item.SelectedCells[4].Column.GetCellContent(data) as TextBlock).Text;
                combo_supplier.Text = supplier;

            }
        }

        // Update Item
        private void btn_item_update_Click(object sender, RoutedEventArgs e)
        {

            int inputId = Convert.ToInt32(txt_item_id.Text);
            var cekId = conn.Items.Where(S => S.Id == inputId).FirstOrDefault();
            var inPrice = Convert.ToInt32(txt_item_price.Text);
            var inStock = Convert.ToInt32(txt_item_stock.Text);
            var inSupp = conn.Suppliers.Where(S => S.Id == cb_sup).FirstOrDefault();

            cekId.Name = txt_item_name.Text;
            cekId.Price = inPrice;
            cekId.Stock = inStock;
            cekId.Supplier = inSupp;
            var update = conn.SaveChanges();
            MessageBox.Show(update + " has been update");

            txt_item_id.Text = string.Empty;
            txt_item_name.Text = string.Empty;
            txt_item_price.Text = string.Empty;
            txt_item_stock.Text = string.Empty;
            combo_supplier.Text = string.Empty;

            tbl_item.ItemsSource = conn.Items.ToList();
        }

        // Delete Item
        private void btn_item_delete_Click(object sender, RoutedEventArgs e)
        {
            int inId = Convert.ToInt32(txt_item_id.Text); // menangkap id dari textbox id
            var cekId = conn.Items.Where(S => S.Id == inId).FirstOrDefault(); // s -> objek dari tbl_supplier

            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                conn.Items.Remove(cekId);
                var delete = conn.SaveChanges();
                txt_item_id.Text = string.Empty;
                txt_item_name.Text = string.Empty;
                txt_item_price.Text = string.Empty;
                txt_item_stock.Text = string.Empty;
                combo_supplier.Text = string.Empty;

                tbl_item.ItemsSource = conn.Items.ToList();
            }
        }

        // Function send password to email
        private void sendPassToEmail(string email, string password, string name)
        {
            string from = "keepkarim@gmail.com";
            string to = email;
            MailMessage mm = new MailMessage(from, to);
            mm.Subject = "New Password to Login Application ";
            string isi_pesan = "hi " + name + " this is your password " + password + " Thank you";
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
            catch(Exception e)
            {
                MessageBox.Show("Failed to send email..." + e.ToString());
            }

        }













        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txt_name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txt_address_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
