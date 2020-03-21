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

namespace belajar_crud_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        myContext conn = new myContext(); // ceclare new obj myContext

        public int cb_sup; // declare combo box supplir

        public MainWindow()
        {
            InitializeComponent();

            tbl_supplier.ItemsSource = conn.Suppliers.ToList(); // refresh table supplier

            tbl_item.ItemsSource = conn.Items.ToList(); // refresh table Item

            combo_supplier.ItemsSource = conn.Suppliers.ToList(); // refresh combo suppliers
           
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }


        // Insert Suplier 
        private void Btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            var input = new Supplier (txt_name.Text , txt_address.Text);

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
            }
            else
            {
                // push to database
                conn.Suppliers.Add(input);
                conn.SaveChanges();
                txt_name.Text = string.Empty;
                txt_address.Text = string.Empty;
                MessageBox.Show("Data Berhasil ditambahkan");
            }

            tbl_supplier.ItemsSource = conn.Suppliers.ToList(); // refresh table

        }

        private void txt_id_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

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
            MessageBox.Show(update + " has been update");

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

        private void txt_name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txt_address_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }


        // Insert Item
        private void btn_item_insert_Click(object sender, RoutedEventArgs e)
        {
            
            var inPrice = Convert.ToInt32(txt_item_price.Text);
            var inStock = Convert.ToInt32(txt_item_stock.Text);
            var inSupp = conn.Suppliers.Where(S => S.Id == cb_sup).FirstOrDefault();

            var inputItem = new Item(txt_item_name.Text, inPrice, inStock, inSupp);
            conn.Items.Add(inputItem);
            var insert = conn.SaveChanges();

            MessageBox.Show(insert + "Item has been inserted");
            txt_item_name.Text= string.Empty;
            txt_item_price.Text = string.Empty;
            txt_item_stock.Text = string.Empty;

            tbl_item.ItemsSource = conn.Items.ToList();
        }


        // Set up combo supplier
        private void combo_supplier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb_sup = Convert.ToInt32(combo_supplier.SelectedValue); // handle load supplier input in Item menu
        }

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
    }
}
