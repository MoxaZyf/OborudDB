using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Utils;

namespace WPA_Mech.ViewModels.TreeViewTest
{


    public class TreeViewModel : ViewModelBase
    {
        public ObservableCollection<RootTreeItem> Tree { get; set; }

        public TreeViewModel()
        {
            Tree = new ObservableCollection<RootTreeItem>
        {
            new RootTreeItem("AAA"),
            new RootTreeItem("BBB"),
            new RootTreeItem("CCC"),
        };
            new List<CategoryTreeItem>
        {
            new CategoryTreeItem("B.1", Tree[1]),
            new CategoryTreeItem("B.2", Tree[1]),
            new CategoryTreeItem("B.3", Tree[1]),
        }.ForEach(item => Tree[1].Children.Add(item));

            new List<CategoryTreeItem>
        {
            new CategoryTreeItem("C.1", Tree[2]),
            new CategoryTreeItem("C.2", Tree[2]),
            new CategoryTreeItem("C.3", Tree[2]),
        }.ForEach(item => Tree[2].Children.Add(item));

            new List<LeafTreeItem>
        {
            new LeafTreeItem("C.1.1", Tree[2].Children[0]),
            new LeafTreeItem("C.1.2", Tree[2].Children[0]),
            new LeafTreeItem("C.1.3", Tree[2].Children[0]),
        }.ForEach(item => Tree[2].Children[0].Children.Add(item));
        }
    }



    //public class TreeViewModel : ViewModelBase
    //{
    //    private List<Department> departments;

    //    public TreeViewModel()
    //    {
    //        Departments = new List<Department>()
    //    {
    //        new Department("Department1"),
    //        new Department("Department2")
    //    };
    //    }

    //    public List<Department> Departments
    //    {
    //        get
    //        {
    //            return departments;
    //        }
    //        set
    //        {
    //            departments = value;
    //            RaisePropertyChanged("Departments");
    //        }
    //    }
    //}
}
