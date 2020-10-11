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
using WPA_Mech.ViewModels;
using WPA_Mech.ViewModels.TreeViewTest;

namespace WPA_Mech
{
    /// <summary>
    /// Логика взаимодействия для MyWindow.xaml
    /// </summary>
    public partial class MyWindow : Window
    {
        public MyWindow()
        {
            InitializeComponent(); this.Title = "Вы вошли как:  " + System.Environment.UserName;
           //this.ViewModel = new MainVM();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlanFact_Click(object sender, RoutedEventArgs e)
        {
            var vm = new PlanFactVM();
            var win = new PlanFactWin
            {
                DataContext = vm,
            };
            win.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
          /*  var vm = new TreeVM();
            var win = new TreeWin
            {
                DataContext = vm,
            };
            win.Show();*/
        }

        private void EditPrint(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void TreeView(object sender, RoutedEventArgs e)
        {
            var vm = new TreeVM();
            var win = new TreeWin
            {
                DataContext = vm,
            };
            win.Show();
        }

        private void TreeViewRedact(object sender, RoutedEventArgs e)
        {
            var vm = new TreeRedactVM();
            var win = new TreeRedactWin
            {
                DataContext = vm,
            };
            win.Show();
        }

        private void SutRaport(object sender, RoutedEventArgs e)
        {

        }

        private void TK_O(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void AddPers(object sender, RoutedEventArgs e)
        {

        }

        private void GrafikVixod(object sender, RoutedEventArgs e)
        {

        }

        private void AvtoHr(object sender, RoutedEventArgs e)
        {
            var vm = new AvtoHrVM();
            var win = new AvtoHrWin
            {
                DataContext = vm,
            };
            win.Show();
        }

        private void Bar(object sender, RoutedEventArgs e)
        {
            var vm = new TreeViewModel();
            var win = new BARWin
            {
                DataContext = vm,
            };
            win.Show();
        }
    }
    }
