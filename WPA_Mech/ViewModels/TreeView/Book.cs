using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech
{
    public class Book : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();

        private string bookname = string.Empty;

        public UMRK_PPR oborud;
        private readonly UMRK_PPR _oborud;

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));

            }
        }
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
        private List<Comments> comments;
        public Book(UMRK_PPR oborud)
        {
            _oborud = oborud;
            _name = oborud.Oborud;
            NameName = _name;
            Data = oborud.Per;
            var naim = _db.UMRK_PPR.Where(p => p.Type_work != null && p.Per == Data).Where(x => x.Oborud == _name).GroupBy(p => p.Oborud).Select(grp => grp.FirstOrDefault()).ToList().Select(r => new Comments(r));
            Comments = new List<Comments>(naim);
        }
        public List<Comments> Comments
        {
            get
            {
                return comments;
            }
            set
            {
                comments = value;
                RaisePropertyChanged("Comments");
            }
        }


        public DateTime Data
        {
            get; set;
        }
        public string NameName
        { get; set; }


        private bool _isSelected;
        public Boolean IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

    }
}

