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
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace lab1
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : Page
    {
        public static SKLAD_WPFEntities DataEntitiesSKLAD { get; set; } = new SKLAD_WPFEntities();
        public ObservableCollection<Owner> ListOwner { get; set; }
        public PageMain()
        {
            InitializeComponent();
            DataEntitiesSKLAD = new SKLAD_WPFEntities();
            ListOwner = new ObservableCollection<Owner>();
        }
        bool isDirty = true;

        private void GetOwners()
        {
            var queryOwners = (from owner in DataEntitiesSKLAD.Owners
                               orderby owner.Name
                               select owner).ToList();
            foreach (Owner own in queryOwners)
            {
                ListOwner.Add(own);

            }
            //DataGridItem.Items.Clear();
            DataGridItem.ItemsSource = ListOwner;
            foreach (var item in ListOwner)
            {
                Console.WriteLine(item.Name + " " + item.Email);
            }
        }
        private void RewriteOwner()
        {
            DataEntitiesSKLAD = new SKLAD_WPFEntities();
            ListOwner.Clear();
            GetOwners();
        }

        private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Отмена");
            RewriteOwner();
            DataGridItem.IsReadOnly = true;
            isDirty = true;
        }
        private void UndoCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;

        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Сохранено");
            DataEntitiesSKLAD.SaveChanges();

            isDirty = true;
            DataGridItem.IsReadOnly = true;
        }

        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;

        }
        private void EditCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGridItem.IsReadOnly = false;
            DataGridItem.BeginEdit();
            isDirty = true;
        }

        private void EditCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;

        }
        private void FindCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Поиск");
            isDirty = true;
        }

        private void FindCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;

        }
        private void AddCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Создание");
            Owner own = new Owner();
            own.Phone = "не задано";
            own.Email = "не задано";
            own.Name = "не задано";
            try
            {
                DataEntitiesSKLAD.Owners.Add(own);
                ListOwner.Add(own);
                isDirty = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                "Ошибка добавления данных" + ex.ToString());
            }

        }

        private void AddCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;

        }
        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Удаление");
            Owner emp = DataGridItem.SelectedItem as Owner;
            if (emp != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные ",
                "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesSKLAD.Owners.Remove(emp);
                    DataGridItem.SelectedIndex =
                    DataGridItem.SelectedIndex == 0 ? 1 : DataGridItem.SelectedIndex - 1;
                    ListOwner.Remove(emp);
                    //DataEntitiesSKLAD.SaveChanges();
                    isDirty = true;
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }

        private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetOwners();
            DataGridItem.SelectedIndex = 0;
        }
    }
}
