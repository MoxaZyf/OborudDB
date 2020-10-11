using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Utils;

namespace WPA_Mech.ViewModels.TreeViewTest
{
    public class Course : ViewModelBase
    {
        private List<Book> books;

        public Course(string coursename)
        {
            CourseName = coursename;
            Books = new List<Book>()
        {
            new Book("работа 1"),
            new Book("работа 2"),
            new Book("работа 3")
        };


            
        }

        public List<Book> Books
        {
            get
            {
                return books;
            }
            set
            {
                books = value;
                RaisePropertyChanged("Books");
            }
        }

        public string CourseName
        {
            get;
            set;
        }
    }
}
