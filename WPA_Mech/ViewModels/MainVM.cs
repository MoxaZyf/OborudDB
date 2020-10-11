using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech.ViewModels
{
  public  class MainVM: ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();

       


        public MainVM()
        {





           

            var Succ = _db.Users.Where(x=>x.UserName == System.Environment.UserName).Select(x=>x.UserName).ToList();
            foreach (var i in Succ)
            {

            
           

                if (i == System.Environment.UserName)
                    MessageBox.Show("Вы авторизованы");
                var myWindow = new MyWindow();
                myWindow.Show();

                // закрываем текущее окно логина
                var window = Application.Current.Windows[0];
                if (window != null)
                    window.Close();
               
             }
            
        

        }

    }
}
