using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Utils;

namespace WPA_Mech//.ViewModels.TreeViewTest
{
    public interface ITreeItem
    {
        string Name { get; }
        IEnumerable Children { get; }
        object Parent { get; }
    }

    public abstract class BaseTreeItem<TChild, TParent> : ViewModelBase, ITreeItem
        where TChild : class
        where TParent : class
    {
        public string Name { get; protected set; }
        public ObservableCollection<TChild> Children { get; protected set; }
        public TParent Parent { get; protected set; }

        IEnumerable ITreeItem.Children
        {
            get
            {
                return Children;
            }
        }

        object ITreeItem.Parent
        {
            get
            {
                return Parent;
            }
        }


        protected BaseTreeItem(string name)
            : this(name, new ObservableCollection<TChild>())
        {
        }

        protected BaseTreeItem(string name, TParent parent)
            : this(name, new ObservableCollection<TChild>(), parent)
        {
        }

        protected BaseTreeItem(string name, ObservableCollection<TChild> children, TParent parent = null)
        {
            Name = name;
            Children = children;
            Parent = parent;
        }
    }

    public class RootTreeItem : BaseTreeItem<CategoryTreeItem, object>
    {
        public RootTreeItem(string name)
            : base(name)
        {
        }

        public RootTreeItem(string name, ObservableCollection<CategoryTreeItem> children)
            : base(name, children)
        {
        }
    }

    public class CategoryTreeItem : BaseTreeItem<LeafTreeItem, RootTreeItem>
    {
        public CategoryTreeItem(string name, RootTreeItem parent)
            : base(name, parent)
        {
        }
        public CategoryTreeItem(string name, ObservableCollection<LeafTreeItem> children, RootTreeItem parent)
            : base(name, children, parent)
        {
        }
    }

    public class LeafTreeItem : BaseTreeItem<object, CategoryTreeItem>
    {
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
                RaisePropertyChanged(nameof(IsChecked));
            }
        }

        public LeafTreeItem(string name, CategoryTreeItem parent, bool isChecked = false)
            : base(name, null, parent)
        {
            _isChecked = isChecked;
        }
    }
}
