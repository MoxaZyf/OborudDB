using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Utils;

namespace WPA_Mech.ViewModels.TreeViewTest
{
    public class Department : ViewModelBase
    {
        private List<Course> courses;

        public Department(string depname)
        {
            DepartmentName = depname;
            Courses = new List<Course>()
        {
            new Course("оборудование1"),
            new Course("оборудование2")
        };
        }

        public List<Course> Courses
        {
            get
            {
                return courses;
            }
            set
            {
                courses = value;
                RaisePropertyChanged("Courses");
            }
        }

        public string DepartmentName
        {
            get;
            set;
        }
    }
}
