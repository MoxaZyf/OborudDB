using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech
{
    public class Course : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();

        public UMRK_PPR oborud;

        private string _oboruds;
        public string Oborud
        {
            get => _oboruds;
            set
            {
                _oboruds = value;
                RaisePropertyChanged(nameof(Oborud));
            }
        }

        private readonly UMRK_PPR _oborud;

        private List<Book> books;

        public Course(string coursename)
        {
            CourseName = coursename;
            Books = new List<Book>()
            {
                new Book("JJJJ"),
                new Book("KKKK"),
                new Book("OOOOO")
            };
        }

        public Course(UMRK_PPR oborud)
        {
            _oborud = oborud;
            _oboruds = oborud.Type_oborud;
            CourseName = _oboruds;

            Data = oborud.Per;

            var naim = _db.UMRK_PPR.Where(p => p.Oborud != null && p.Per == Data/*.Year == 2019 && p.Per.Month == 01 && p.Per.Day == 17*/).Where(x => x.Type_oborud == _oboruds).ToList().Select(r => new Book(r));
            Books = new List<Book>(naim);

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

        public DateTime Data
        {
            get; set;
        }


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
