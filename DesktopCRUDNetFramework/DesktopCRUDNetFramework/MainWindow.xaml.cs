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
using MySql.Data.MySqlClient;//Adding the Mysql Library

namespace DesktopCRUDNetFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DbClass db = new DbClass();

        public MainWindow()
        {
            SLV = new ListView();
            id_tbox = new TextBox();
            part_tbox = new TextBox();
            customer_tbox = new TextBox();
            retailer_tbox = new TextBox();
            price_tbox = new TextBox();
            InitializeComponent();
        }
        private void Fetch_2(object sender, RoutedEventArgs e)
        {

            List<part> slv = DbClass.Fetch_2(int.Parse(id_tbox.Text));
            SLV.ItemsSource = slv;
            System.Windows.MessageBox.Show("Fetch Command");
        }

        private void Fetch(object sender, RoutedEventArgs e)
        {
            List<part> slv = DbClass.Fetch();
            SLV.ItemsSource=slv;
            System.Windows.MessageBox.Show("Fetch Command");
        }
        private void Insert(object sender, RoutedEventArgs e)
        {
            db.Insert(int.Parse(id_tbox.Text),(string)part_tbox.Text, (string)customer_tbox.Text, (string)retailer_tbox.Text, (string)price_tbox.Text);
            System.Windows.MessageBox.Show("Insert Command");
        }
        private void Remove(object sender, RoutedEventArgs e)
        {
            db.Delete(int.Parse(id_tbox.Text));
            System.Windows.MessageBox.Show("Remove Command");
        }
        private void Update(object sender, RoutedEventArgs e)
        {
            db.Update(int.Parse(id_tbox.Text), (string)part_tbox.Text, (string)customer_tbox.Text, (string)retailer_tbox.Text, (string)price_tbox.Text);
            System.Windows.MessageBox.Show("Update Command");
        }

       
    }

    
}
