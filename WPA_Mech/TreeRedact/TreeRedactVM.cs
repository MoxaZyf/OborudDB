using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Utils;
using WPA_Mech.Models;
using System.Data.Entity;
using System.Windows;
using System.Collections.ObjectModel;

namespace WPA_Mech//.TreeRedact
{
  public  class TreeRedactVM : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();

        public RelayCommand SaveCommand { get; set; }

        public RelayCommand UpdateCommand { get; set; }

        public RelayCommand PrintCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }
















        private List<UMRK_SM> _treeViewSource;
        public List<UMRK_SM> TreeViewSource
        {
            get { return _treeViewSource; }
            set
            {
                if (_treeViewSource != value)
                {
                    _treeViewSource = value;
                    RaisePropertyChanged("TreeViewSource");
                }
            }
        }


        /*  private List<UMRK_REM> _treeViewSource;
          public List<UMRK_REM> TreeViewSource
          {
              get { return _treeViewSource; }
              set
              {
                  if (_treeViewSource != value)
                  {
                      _treeViewSource = value;
                      RaisePropertyChanged("TreeViewSource");
                  }
              }
          }*/
        private DateTime? _data;
        public DateTime? Data
        {
            get => _data;
            set
            {
                _data = value;
                RaisePropertyChanged("Data");
                GetTime();
            }
        }
         private BookRedact _selectedOrder;
         public BookRedact SelectedItem
         {
             get => _selectedOrder;
             set
             {
                 _selectedOrder = value;
                 RaisePropertyChanged(nameof(SelectedItem));
                 
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



        bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        private float? _TimePlan;
        public float? TimePlan
        {
            get => _TimePlan;
            set
            {
                _TimePlan = value;
                RaisePropertyChanged(nameof(TimePlan));
            }
        }


        private int? _TimeFakt;
        public int? TimeFakt
        {
            get => _TimeFakt;
            set
            {
                _TimeFakt = value;
                RaisePropertyChanged(nameof(TimeFakt));
            }
        }



        private TreeRedactVM _Oborud;
        public TreeRedactVM Oborud
        {
            get => _Oborud;
            set
            {
                _Oborud = value;
                RaisePropertyChanged(nameof(Oborud));
            }
        }
        private List<DepartmentRedact> departmentRedact;

        public void GetTime()
        {
           // var parent = _db.UMRK_REM.Where(p => p.NR == Data).GroupBy(p => p.sm).Select(grp => grp.FirstOrDefault()).ToList().Select(r => new DepartmentRedact(r));
           var parent = _db.UMRK_SM.Where(p=>p.Date_nar == Data).GroupBy(p=>p.Nar_vid).Select (grp => grp.FirstOrDefault()).ToList().Select(r => new DepartmentRedact(r));
            DepartmentRedact = new List<DepartmentRedact>(parent);

            Oborud = Oborud;
     
            _db.UMRK_SM.Load();
            UMRK_SM = _db.UMRK_SM.Local;
          //  _db.NaryadList.Load();
           // NaryadLists = _db.NaryadList.Local;
        }

        private readonly UMRK_SM _naryad;
        public UMRK_SM naryad;

        public TreeRedactVM(UMRK_SM naryad)
        {
            _naryad = naryad;
         //   _TimeFakt = naryad.Time_Fakt;


        }


        /*    private readonly NaryadList _naryad;
            public NaryadList naryad;

            public TreeRedactVM(NaryadList naryad)
            {
                _naryad = naryad;
                _TimeFakt = naryad.Time_Fakt;


            }
            */
        public TreeRedactVM()
        {
            SaveCommand = new RelayCommand(Save);
            UpdateCommand = new RelayCommand(Upd);
            PrintCommand = new RelayCommand(PrintC);
            DeleteCommand = new RelayCommand(Delete);
            //  DeleteCommand = new RelayCommand(o=> Delete((Collection<object>)o));
            // _db.NaryadList.Load();
            // NaryadLists = _db.NaryadList.Local;
            _db.UMRK_SM.Load();
            UMRK_SM = _db.UMRK_SM.Local;
        }

        ObservableCollection<UMRK_SM> autos;
        // public ObservableCollection<NaryadList> NaryadLists
        public ObservableCollection<UMRK_SM> UMRK_SM
        {
            get { return autos; }
            set
            {
                autos = value;
                RaisePropertyChanged("UMRK_SM");
            }
        }




        public void Delete(object parameter)
        {
            if (SelectedItem != null)
            {

                SelectedItem.Delete();

            }
        }

        public void PrintC(object parameter)
        {


            if (SelectedItem != null)
            {

                SelectedItem.Print();

            }
        }




        public void Upd(object parameter)
        {


            if (SelectedItem != null)
            {

                SelectedItem.UpdateModel();



                _db.SaveChanges();

            }
        }
        public void Save()
        {

            if (SelectedItem != null)
            {

                SelectedItem.UpdateModel();







                _db.SaveChanges();

                MessageBox.Show("Данные изменены ");
            }





        }







        public List<DepartmentRedact> DepartmentRedact
        {
            get
            {
                return departmentRedact;
            }
            set
            {
                departmentRedact = value;
                RaisePropertyChanged("DepartmentRedact");
            }
        }
    }
}

