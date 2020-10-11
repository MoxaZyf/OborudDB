using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech.ViewModels
{

    
    public class TreeVM: ViewModelBase
    {

       private readonly Bez1Entities _db = new Bez1Entities();


        public RelayCommand SaveCommand { get; set; }
        public RelayCommand SaveTest { get; set; }
        public RelayCommand ShowSecondViewCommand { get; set; }
        public RelayCommand ShowFirstViewCommand { get; set; }
        public RelayCommand UPDCommand { get; set; }

        /*  private List<Personal> _personal = new List<Personal>();

          public List<Personal> Personal { get => _personal; }*/





        private List<UMRK_PPR> _treeViewSource;
        public List<UMRK_PPR> TreeViewSource
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

        private Personal _fio;
        public Personal FIO
        {
            get => _fio;
            set
            {
                _fio = value;
                RaisePropertyChanged("FIO");
            }
        }


        private UMRK_ISP _Fio;
        public UMRK_ISP Fio
        {
            get => _Fio;
            set
            {
                _Fio = value;
                RaisePropertyChanged("Fio");
            }
        }

        private DateTime? _datenar;
        public DateTime? DateNar
        {
            get => _datenar;
            set
            {
                _datenar = value;
                RaisePropertyChanged("DateNar");
                Rers();
            }
        }



        private int _smena;
        public int Smena
        {
            get => _smena;
            set
            {
                _smena = value;
                RaisePropertyChanged("Smena");
                if (Smena == 1 || Smena == 2)
                {
                    SmenaLong = 12;
                }
                else
                {
                    SmenaLong = 8;
                }
            }
        }

        private int _smenaLong;
        public int SmenaLong
        {
            get => _smenaLong;
            set
            {
                _smenaLong = value;
                RaisePropertyChanged("SmenaLong");

            }
        }

        private int _brigada;
        public int Brigada
        {
            get => _brigada;
            set
            {
                _brigada = value;
                RaisePropertyChanged("Brigada");
            }
        }

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

        private Comments _selectedOrder;
        public Comments SelectedItem
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                RaisePropertyChanged(nameof(SelectedItem));
                TypeWork = SelectedItem.Comment;
                Oborud = SelectedItem.TypeO;
                TimePlan = SelectedItem.T;
                Work = Work;
                TypeO = SelectedItem.TO;


                //  Work = SelectedItem.Works.Where(x=>x.IsChecked).ToString();

                var w = SelectedItem.Works.Where(x => x.IsChecked).Select(x => x.NameWork);//   TechKarts.Where(item => item.IsSelected).Select(item => item.Состав_работ);
                Work = "";
                foreach (string fib in w)
                {
                    //  Console.Write("asdf " + fib.ToString() + "\t" );

                    Work = Work + fib.ToString() + "||";
                }

            }
        }




        private List<Works> _IsChecked;
        public List<Works> IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked == value)
                    return;
                _IsChecked = value;
                RaisePropertyChanged("IsChecked");
                Work = Work;// IsSelected.NameWork;

            }
        }


        private string _Work;
        public string Work
        {
            get => _Work;
            set
            {
                _Work = value;
                RaisePropertyChanged(nameof(Work));
            }
        }
        private string _Oborud;
        public string Oborud
        {
            get => _Oborud;
            set
            {
                _Oborud = value;
                RaisePropertyChanged(nameof(Oborud));
            }
        }


        private string _TypeWork;
        public string TypeWork
        {
            get => _TypeWork;
            set
            {
                _TypeWork = value;
                RaisePropertyChanged(nameof(TypeWork));
            }
        }
        private string _TypeO;
        public string TypeO
        {
            get => _TypeO;
            set
            {
                _TypeO = value;
                RaisePropertyChanged(nameof(TypeO));
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
        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                RaisePropertyChanged(nameof(Comment));


            }
        }
        private string _t2Oborud;
        public string t2Oborud
        {
            get => _t2Oborud;
            set
            {
                _t2Oborud = value;
                RaisePropertyChanged(nameof(t2Oborud));


            }
        }
        public int Id_PPR { get; set; }
        public ObservableCollection<UMRK_PPR> OborudLists { get; }
        private List<Department> departments;



        public List<Personal> Personals { get; }

       // public List<GrafVix> PersVix { get; private set; }

        public void Rers()
        {

          //  PersVix = new List<GrafVix>();
           // PersVix = _db.GrafVix.Where(x => x.Date == DateNar && x.C_1_ == 8).ToList();
        }


        private int _sm;
        public int Sm
        {
            get { return _sm; }
            set
            {
                _sm = value;
                RaisePropertyChanged(nameof(Sm));
            }
        }

        private int _getsm;
        public int GetSm
        {
            get { return _getsm; }
            set
            {
                _getsm = value;
                RaisePropertyChanged(nameof(GetSm));
            }
        }

        private int _id_br;
        public int Id_Br
        {
            get { return _id_br; }
            set
            {
                _id_br = value;
                RaisePropertyChanged(nameof(Id_Br));
            }
        }


        private int _id_rem;
        public int Id_Rem
        {
            get { return _id_rem; }
            set
            {
                _id_rem = value;
                RaisePropertyChanged(nameof(Id_Rem));
            }
        }
        public TreeVM()
        {
            //

            // var pod = _db.UMRK_PPR.Where(p => p.Podrazd != null && p.Per.Year ==2019 && p.Per.Month==01 && p.Per.Day==17).GroupBy(p => p.Podrazd).Select(grp => grp.FirstOrDefault()).ToList().Select(r => new Department(r));
            //  Departments = new List<Department>(pod);
            Personals = new List<Personal>();
            Personals = _db.Personal.Where(p => p.Role == "VidNar").ToList();
            //закомечено, потому что надо брать персонал из одной таблицы и в одну записывать 
            /*   WorkPersonal = new ObservableCollection<UMRK_ISP>();
               var queryPersonal = _db.UMRK_ISP.ToList().Where(x => x.Prof != "Мастер" && x.Prof != "Мастер УМРК" && x.Prof != "Инженер по наладке и испытаниям");
               foreach (var personal in queryPersonal)
               {
                   WorkPersonal.Add(new UMRK_ISP { Fio = personal.Fio, Prof = personal.Prof });// + " ," + personal.Prof, Prof = personal.Prof });

               }
               */

            WorkPersonal = new ObservableCollection<UMRK_ISP>();
            var queryPersonal = _db.UMRK_ISP.ToList();
            foreach (var personal in queryPersonal)
            {
                WorkPersonal.Add(new UMRK_ISP { Fio = personal.Fio, Prof = personal.Prof });// + " ," + personal.Prof, Prof = personal.Prof });

            }

            // Sm = new ObservableCollection<UMRK_BR>();
            /*    var numsm = _db.UMRK_BR.OrderByDescending(x => x.sm).FirstOrDefault().ToString();
                foreach (var o in numsm)
                {
                    Sm = o + 1;
                }
                */
            // MyCollection.OrderByDescending(x => x.AddedDate).FirstOrDefault();
            /*Visit vt = (from v in context.Visits orderby v.VisitId descending select v).FirstOrDefault();
 
 */
            //  var  Smm = (from p in _db.UMRK_BR orderby p.sm descending select p.sm).FirstOrDefault();
            Sm = (from p in _db.UMRK_BR.OrderByDescending(p => p.sm) select p.sm).FirstOrDefault();

            GetSm = (from s in _db.UMRK_SM.OrderByDescending(s => s.Id_sm) select s.Id_sm).FirstOrDefault();

            Brigada = Sm;

         //   Id_Br = (from b in _db.UMRK_BR.OrderByDescending(p => p.sm).Where(b => b.sm == Sm) select b.Id_br).FirstOrDefault();

            MasterPersonal = new ObservableCollection<UMRK_ISP>();

            var queryMaster = _db.UMRK_ISP.ToList().Where(x => x.Prof == "Мастер" || x.Prof == "Мастер РМУ" || x.Prof.ToUpper().StartsWith("Н"));
            foreach (var master in queryMaster)
            {
                MasterPersonal.Add(new UMRK_ISP { Fio = master.Fio + " ," + master.Prof, Prof = master.Prof });
                //  MessageBox.Show("Master " + master.Fio);
            }
            SaveCommand = new RelayCommand(Save);
            SaveTest = new RelayCommand(SaveT);
            ShowSecondViewCommand = new RelayCommand(ShowSecondView);
            ShowFirstViewCommand = new RelayCommand(ShowFirstView);
            UPDCommand = new RelayCommand(Update);
        }
        public DateTime Per { get; set; }
        public DateTime Date { get; set; }
        public string t1Oborud { get; set; }

        private UMRK_PPR _order;
        public void Update(object parameter)
        {





            _order = (from p in _db.UMRK_PPR where p.Id_PPR == Id_PPR select p).FirstOrDefault();


            _order.Comment = Comment;
            _order.Per = Per;
            _db.SaveChanges();

            Window wnd = new Window();
            MessageBox.Show("Данные изменены");
            wnd.Close();



        }

        public void ShowFirstView(object parameter)
        {



            TreeVM vm = new TreeVM() { Comment = Comment, Per = Date, Oborud = t2Oborud, Id_PPR = Id_PPR };
            ViewShower.Show(0, vm, true, b => { if (b != null && b.Value) Oborud = t2Oborud; });

        }

        public void ShowSecondView(object parameter)
        {
            ViewShower.Show(1, JOINS, false, b => { });



        }
        #region Personal
        private ObservableCollection<UMRK_ISP> _Workpersonal;
        private ObservableCollection<UMRK_ISP> _MasterPersonal;



        public ObservableCollection<UMRK_ISP> WorkPersonal
        {
            get { return _Workpersonal; }
            set
            {
                _Workpersonal = value;
                RaisePropertyChanged(nameof(WorkPersonal));
            }
        }
        public ObservableCollection<UMRK_ISP> MasterPersonal
        {
            get { return _MasterPersonal; }
            set
            {
                _MasterPersonal = value;
                RaisePropertyChanged(nameof(MasterPersonal));
            }
        }


        public class UMRK_ISP : ViewModelBase
        {
            public UMRK_ISP() { }

            private string _fio;
            public string Fio
            {
                get
                {
                    return _fio;
                }
                set
                {
                    if (_fio == value)
                    {
                        return;
                    }
                    _fio = value;
                    RaisePropertyChanged("Fio");
                }
            }
            private string _prof;
            public string Prof
            {
                get
                {
                    return _prof;
                }
                set
                {
                    if (_prof == value)
                    {
                        return;
                    }
                    _prof = value;
                    RaisePropertyChanged("Prof");
                }
            }

            private int _id_br;
            public int Id_br
            {
                get
                {
                    return _id_br;
                }
                set
                {
                    if (_id_br == value)
                    {
                        return;
                    }
                    _id_br = value;
                    RaisePropertyChanged("Id_br");
                }
            }

            public UMRK_ISP(string Fio, string Prof, bool IsSelected) : this()
            {
                this._fio = Fio;
                this._prof = Prof;

                this._isSelected = IsSelected;
            }
            private bool _isSelected;
            public bool IsSelected
            {
                get { return _isSelected; }
                set
                {
                    if (_isSelected == value)
                        return;
                    _isSelected = value;
                    RaisePropertyChanged("IsSelected");


                    /*  var prof = Prof.ToString();//Fio.ToString();// Where(item => item.IsSelected).Select(item => item.Prof);
                      Prof = "";
                      Prof = Prof + prof.ToString() + "||";*/


                }
            }

            public ObservableCollection<UMRK_ISP> UMRK_ISPs { get; private set; }
        }
        public IEnumerable<string> GetPers => WorkPersonal.Where(item => item.IsSelected).Select(item => item.Fio);


        public IEnumerable<string> GetMaster => MasterPersonal.Where(item => item.IsSelected).Select(item => item.Fio);

        private string _pers;
        public string Pers
        {
            get { return _pers; }
            set
            {
                _pers = value;
                RaisePropertyChanged("Pers");
            }
        }

        private string _prof;
        public string Prof
        {
            get { return _prof; }
            set
            {
                _prof = value;
                RaisePropertyChanged("Prof");
            }
        }
        #endregion
        public void GetTime()
        {
            var pod = _db.UMRK_PPR.Where(p => p.Podrazd != null && p.Per == Data).GroupBy(p => p.Podrazd).Select(grp => grp.FirstOrDefault()).ToList().Select(r => new Department(r));
            Departments = new List<Department>(pod);
            var leftOuterJoin = (from t1 in _db.NaryadList
                                 join t2 in _db.UMRK_PPR on t1.Data equals t2.Per into temp
                                 from t2 in temp//.DefaultIfEmpty(new { t1.Data , Oborud = default(string) })
                                 where t1.Data == Data || t2.Per == Data

                                 select new TreeVM()
                                 {
                                     Date = t1.Data.Value,
                                     t1Oborud = t1.Oborud,
                                     t2Oborud = t2.Oborud,
                                     Comment = t2.Comment,
                                     Id_PPR = t2.Id_PPR,

                                 }

                                ).ToList().AsQueryable();

            var rightOuterJoin = (from t2 in _db.UMRK_PPR
                                  join t1 in _db.NaryadList on t2.Per equals t1.Data
                                  into temp
                                  from t1 in temp//.DefaultIfEmpty(new { t2.Per, Oborud = default(string) })
                                  where t1.Data == Data || t2.Per == Data
                                  select new TreeVM()
                                  {
                                      Per = t2.Per,
                                      t1Oborud = t1.Oborud,
                                      t2Oborud = t2.Oborud,
                                      Comment = t2.Comment,
                                      Id_PPR = t2.Id_PPR,


                                  }).ToList().AsQueryable();
            var fullOuterJoin = leftOuterJoin.Concat(rightOuterJoin).GroupBy(p => p.t2Oborud).Select(grp => grp.FirstOrDefault()).ToList();


            JOINS = new List<TreeVM>(fullOuterJoin.Where(x => x.t1Oborud != x.t2Oborud));
            int JC = JOINS.Count();
        }
        private List<TreeVM> join;
        public List<TreeVM> JOINS
        {
            get
            {
                return join;
            }
            set
            {
                join = value;
                RaisePropertyChanged("JOINS");
            }
        }
        public List<Department> Departments
        {
            get
            {
                return departments;
            }
            set
            {
                departments = value;
                RaisePropertyChanged("Departments");
            }
        }

        //       bool? IsChecked { get; set; }


        //   public List<Works> IsChecked { get; set; }

        public string d { get; set; }
        public string proff { get; set; }
        public void SaveT(object parameter)
        {
          /*  Bez1Entities db;
            db = new Bez1Entities();

            var cat1 = new TableTest { Name = "Кат1" };
          cat1.TableTest2 = new List<TableTest2> {
    new TableTest2 {Id=1, Name2 = cat1.Name.ToString() , TableTest = cat1 },
    new TableTest2 {Id=2, Name2 = cat1.Name.ToString() },
    new TableTest2 {Id=3, Name2 = cat1.Name.ToString() },
};
            // db.TableTest2.Add(TableTest2);
          //  db.TableTest2.AddRange(cat1.TableTest2);
            db.TableTest.Add(cat1);
            db.SaveChanges();
            MessageBox.Show("Проверяй");

            */


        }
            public void Save(object parameter)
        {
            Bez1Entities db;
            db = new Bez1Entities();
            //Одна запись в бд
            var sm = GetSm + 1;
            DateTime ndt = Convert.ToDateTime(DateNar);

          

          //  CONVERT(SMALLDATETIME, CONVERT(DATETIME, '" + ndt + "'))

            db.UMRK_SM.Add(new UMRK_SM
            {

                Date_nar = ndt,
               Num_sm = Smena,
                Prod = SmenaLong,
               Nar_vid = FIO.FIO.ToString(),


            });

            db.SaveChanges();

           

            //запись по кол-ву выбранного персонала 
            var q = WorkPersonal.ToList().Where(x => x.IsSelected).ToList();
            foreach (var qq in q)
            {




                proff = qq.Prof.ToString();
                d = qq.Fio.ToString();



                /* var prof = WorkPersonal.Where(x => x.IsSelected).Select(item => item.Fio );




                 var onlyprof = WorkPersonal.Where(x => x.IsSelected).Select(item => item.Prof);
                  proff= "";
                 foreach (string pr in onlyprof)
                 {


                     proff = pr.ToString();



                 d = "";
                 foreach (string fib in prof)
                 {


                     d = fib.ToString() ;

               */

                //сохранение по трем таблицам
                /*
                 BR - исполнители
                 REM - оборудование
                 OP - операции


                 UMRK_BR -Id_br автоматом брать == UMRK_OP.isp
                 UMRK_BR.sm автоматически  == UMRK_Rem.sm 
                 UMRK_OP id_rem == Umrk_REM.Id_Rem 


                Создается бригада, берется Id_br Записывается в операции id_br в isp 
                из UMRK_BR взять sm+1 

                UMRK_OP.id_rem == UMRK_BR.Id_br


                 a = Recordset.Open("SELECT distinct id_br FROM UMRK_Br WHERE sm = " + CStr(sm) + " and Num_br = " + CStr(Num_br) + " AND FIO = '" + CStr(ListBox1.List(ListBox1.ListIndex, 2)) + "'", objConnection)
                 запросом взять Id_br где sm==sm 



                 */
                //var sm = Sm + 1;
                

                db.UMRK_BR.Add(new UMRK_BR
                {
                    sm = GetSm+1,
                    Num_br = Brigada,
                    FIO = d,
                    Prof = proff,
                    Org = "ЭлектроремонтTEST",


                });

                db.SaveChanges();
            }
           
            int proverka = (from r in _db.UMRK_REM.Distinct().Where(r => r.sm == sm & r.Num_br == Brigada && r.Type_work == Work && Oborud == Oborud) select r.Id_rem).FirstOrDefault();
            if (proverka == 0)
            {
                db.UMRK_REM.Add(new UMRK_REM
                {
                    Num_br = Brigada,
                    sm = sm,
                    Podrazd = "Подразделение",
                    Oborud = Oborud,
                    Type_Oborud = _TypeO,
                    Type_work = TypeWork,//Work,
                    ind = false, //????
                    NR = DateNar,//начало
                    KR = DateNar,//конец
                                 //TR = ,// Общая продолжительность
                    ND = DateNar,
                    KD = DateNar,
                    TD = DateNar,
                    Komment = "Коммент",
                    Komment1 = "Коммент1",
                    Pr_rem = Id_PPR,

                });
                db.SaveChanges();

            }
            else
            {
                MessageBox.Show("Запись существует");
            }
            //по кол-ву выбранных работ для ремонта

             var workCount = SelectedItem.Works.Where(x => x.IsChecked).Select(x => x.NameWork).ToList();//   TechKarts.Where(item => item.IsSelected).Select(item => item.Состав_работ);
            //var workCount = WorkPersonal.ToList().Where(x => x.IsSelected);

            Work = "";
           

            foreach (string qqq in workCount)
            {

                //var w = qqq.
                Work = qqq.ToString();
                /*  Id_Br = (from b in _db.UMRK_BR.OrderByDescending(p => p.sm).Where(b => b.sm == Sm) select b.Id_br).FirstOrDefault();
                  db.UMRK_OP.Add(new UMRK_OP
                  {
                      Operation = Work,
                      Oper_tk_fk = 0,
                      Id_rem = 0,
                      isp = Id_Br,
                  });*/

                //  Save2();
                Id_Rem = (from b in _db.UMRK_REM.OrderByDescending(p => p.sm).Where(b => b.sm == sm) select b.Id_rem).FirstOrDefault();

                Id_Br = (from b in _db.UMRK_BR.OrderByDescending(p => p.sm).Where(b => b.sm == sm) select b.Id_br).FirstOrDefault();
                db.UMRK_OP.Add(new UMRK_OP
                {
                    Operation = Work,
                     Oper_tk_fk = 0,
                     Id_rem = Id_Rem,
                     isp = Id_Br,
                 });
               db.SaveChanges();


               
                

                /*    
  


            db.NaryadList.Add(new NaryadList
                   {

            
                       Data = DateNar,
                       Nar_vidal = FIO.FIO.ToString(),
                       Smena = Smena,
                       Master = Fio.Fio.ToString(),
                       FIO = d,
                       Post = proff,
                       Brigada = Brigada,
                       Oborud = Oborud,
                       TO_Type = TypeWork,
                       Time_Plan = Convert.ToInt32(TimePlan),
                       Work_list = Work,
                       Smena_long = SmenaLong

                   });*/
                //  }
            
           // db.SaveChanges();

            MessageBox.Show("Данные сохранены");

            }

        }

       
        
        
    }
}
