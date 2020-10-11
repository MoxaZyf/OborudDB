using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WPA_Mech
{
    /// <summary>
    /// Логика взаимодействия для AvtoHrWin.xaml
    /// </summary>
    public partial class AvtoHrWin : Window
    {
       

        public AvtoHrWin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var vm = DataContext as AvtoHrVM;

            //vm.DoWork(sender);
           
           

            //Thread th = new Thread(WorkMethod);
            //th.Start();
        }
    }
}
