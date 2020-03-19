﻿using belajar_crud_wpf.Model;
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
        myContext conn = new myContext();

        public MainWindow()
        {
            InitializeComponent();
            tbl_supplier.ItemsSource = conn.Suppliers.ToList(); // refresh table
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

        private void Btn_Insert_Click(object sender, RoutedEventArgs e)
        {
            var input = new Supplier (txt_name.Text , txt_address.Text);

            // validasi input
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
                // push ke database
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

        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txt_id.Text); // menangkap id dari textbox id
            var cekId = conn.Suppliers.Where(S => S.Id == id).FirstOrDefault(); // s -> objek dari tbl_supplier

            cekId.Name = txt_name.Text;
            cekId.Address = txt_address.Text;
            var update = conn.SaveChanges();
            MessageBox.Show(update + " telah di update");
            tbl_supplier.ItemsSource = conn.Suppliers.ToList(); // refresh table
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txt_id.Text); // menangkap id dari textbox id
            var cekId = conn.Suppliers.Where(S => S.Id == id).FirstOrDefault(); // s -> objek dari tbl_supplier
            conn.Suppliers.Remove(cekId);
            var delete = conn.SaveChanges();
            MessageBox.Show(delete + " telah dihapus");
            txt_id.Text = string.Empty;
            txt_name.Text = string.Empty;
            txt_address.Text = string.Empty;
            tbl_supplier.ItemsSource = conn.Suppliers.ToList(); 
        }

        private void txt_name_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txt_address_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
