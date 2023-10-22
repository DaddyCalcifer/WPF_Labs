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
using System.Data.Entity;
using System.Collections.ObjectModel;
namespace lab1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static SKLAD_WPFEntities DataEntitiesSKLAD { get; set; }
        //public static ObservableCollection<Owner> ListOwner { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //DataEntitiesSKLAD = new SKLAD_WPFEntities();
            //ListOwner = new ObservableCollection<Owner>();
        }
        private void frame1_Loaded(object sender, RoutedEventArgs e)
        {
            var page = new PageMain();
            this.Content = page;
           
            //var queryOwners = (from owner in DataEntitiesSKLAD.Owners
            //                  orderby owner.Name
            //                  select owner).ToList();
            //foreach (Owner own in queryOwners)
            //{
            //    ListOwner.Add(own);
               
            //}
            //page.DataGridItem.Items.Clear();
            //page.DataGridItem.ItemsSource = ListOwner;
            ////page.DataGridItem.Items.Add(ListOwner[0]);
            //foreach(var item in ListOwner)
            //{
            //    Console.WriteLine(item.Name + " " + item.Email);
            //}
        }

        private void frame1_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
