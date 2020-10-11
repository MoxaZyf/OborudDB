using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech//.TreeRedact
{
    public class CourseRedact : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();

        public UMRK_REM oborud;

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

        private readonly UMRK_REM _oborud;

        private List<BookRedact> booksRedact;

        public CourseRedact(string coursenameRedact)
        {
            CourseNameRedact = coursenameRedact;

        }


        public int? Sm
        {
            get; set;
        }
        public DateTime Time
        {
            get;set;
        }
        public CourseRedact(UMRK_REM oborud)
        {
            //оборудование
            _oborud = oborud;
            _oboruds = oborud.Oborud;
            CourseNameRedact = _oboruds;
            
           // Vidal = _oborud.Nar_vidal;

            Data = oborud.NR;
            Sm = oborud.sm;

            // var naim = _db.NaryadList.Where(p =>/* p.Oborud != null &&*/ p.Data == Data).Where(x => x.Oborud == _oboruds && x.Nar_vidal == Vidal)/*.GroupBy(p => p.Brigada).Select(grp => grp.FirstOrDefault())*/.ToList().Select(r => new BookRedact(r));
            //  var naim = _db.UMRK_REM.Where(p =>/* p.Oborud != null &&*/ p.NR == Data).Where(x => x.Oborud == _oboruds)/*.GroupBy(p => p.Brigada).Select(grp => grp.FirstOrDefault())*/.ToList().Select(r => new BookRedact(r));
              var naim = _db.UMRK_BR.Where (p=>p.sm == Sm ).ToList().Select(r => new BookRedact(r));

            //br взять sm, запросить в rem дату по sm и в ппр взять t из даты rem

            var q = _db.UMRK_REM.Where(r => r.sm == Sm).ToList().Select(e => new BookRedact(e));

            
            

            BooksRedact = new List<BookRedact>(naim);

           // BooksRedact = new List<BookRedact>(q);

        }

        /*
         * from phone in db.Phones
             join company in db.Companies on phone.CompanyId equals company.Id
             join country in db.Countries on company.CountryId equals country.Id
             select new
             { 
                Name = phone.Name, 
                Company = company.Name, 
                Price = phone.Price, 
                Country = country.Name 
             };
         * 
         * 
         *  var orders = _db.KanzOrders.Join(_db.KLists, k => k.ListID, l => l.ListId, (k, l) => new
            {
                Date = k.Date,
                Count = k.Count,
                Id1C = k.Id1C,
                NameList = k.NameList,
                Articul = l.Articul,



            }).Where(x => x.Count > 0);


 _list = new ObservableCollection<KList>(_db.KLists);
            _order = new ObservableCollection<KanzOrder>(_db.KanzOrders.Include(_list=>_list.KList));
      

            ListItems = new ObservableCollection<KList>(_db.KLists.Include(ListItems=>ListItems.KanzOrder));

         */

        public List<BookRedact> BooksRedact
        {
            get
            {
                return booksRedact;
            }
            set
            {
                booksRedact = value;
                RaisePropertyChanged("BooksRedact");
            }
        }

        public string CourseNameRedact
        {
            get;
            set;
        }

        public string Vidal { get; set; }
        public DateTime? Data
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
                if (_isSelected == true)
                {
                    MessageBox.Show("** " + this.Oborud);
                    Oborud = this.Oborud;
                }

            }
        }

        private CourseRedact _coursesRedact;
        public CourseRedact CoursesRedact
        {
            get => _coursesRedact;
            set
            {
                _coursesRedact = value;
                RaisePropertyChanged(nameof(CoursesRedact));

            }
        }

        public void Delete()
        {
            MessageBox.Show("Данные изменены ");
            if (_oborud != null)
            {
                MessageBox.Show("Данные изменены ");
                var existingBlox = new NaryadList
                {
                    /*   Id = _oborud.Id,
                       Data = _oborud.Data,
                       Nar_vidal = _oborud.Nar_vidal,
                       Smena = _oborud.Smena,
                       Smena_long = _oborud.Smena_long,

                       FIO = _oborud.FIO,
                       Post = _oborud.Post,
                       Podrazd = _oborud.Podrazd,
                       Oborud = _oborud.Oborud,
                       Brigada = _oborud.Brigada,

                       TO_Type = _oborud.TO_Type,
                       Work_list = _oborud.Work_list,
                       Time_Plan = _oborud.Time_Plan,
                       Time_Fakt = _timeFakt,
                       StatusNar = _oborud.StatusNar,
                       Comment = _oborud.Comment,
                       Master = _oborud.Master,*/


                };
                using (var context = new Bez1Entities())
                {
                    context.Entry(existingBlox).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();



                    MessageBox.Show("Данные изменены ");
                }
            }
        }

    }
}