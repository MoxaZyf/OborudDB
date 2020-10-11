using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Utils;

namespace WPA_Mech.ViewModels.TreeViewTest
{
    public class Book : ViewModelBase
    {
        private string bookname = string.Empty;

        public string BookName
        {
            get
            {
                return bookname;
            }
            set
            {
                bookname = value;
                RaisePropertyChanged("BookName");
            }
        }

        public Book(string bookname)
        {
            BookName = bookname;
        }

        
        public Book()
        {

        }

       
        private bool _isChecked;
        public bool IsChecked
        {
            get

            {
                return _isChecked;
            }

            set
            {
                _isChecked = value;
                RaisePropertyChanged("IsChecked");
               string Books = BookName.ToString();
            }
        }
    }
}
