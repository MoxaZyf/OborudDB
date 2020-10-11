using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPA_Mech.Models;
using WPA_Mech.Utils;
using Excel = Microsoft.Office.Interop.Excel;

namespace WPA_Mech//.TreeRedact
{
    public class BookRedact : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();

        private string booknameRedact = string.Empty;

        public NaryadList oborud;
        private readonly NaryadList _oborud;

        public UMRK_REM umrk_rem;
        private readonly UMRK_REM _umrk_rem;

        public UMRK_BR umrk_br;
        private readonly UMRK_BR _umrk_br;


        public UMRK_PPR ppr;
        private readonly UMRK_PPR _ppr;

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

        public string _oborudname { get; set; }
        public string BookNameRedact
        {
            get
            {
                return booknameRedact;
            }
            set
            {
                booknameRedact = value;
                RaisePropertyChanged("BookNameRedact");
            }
        }

      //  private readonly UMRK_PPR _ppr;
        private string _podrazd;
        public string Podrazd
        {
            get => _podrazd;
            set
            {
                _podrazd = value;
                RaisePropertyChanged(nameof(Podrazd));
            }

        }
        public void SetName()
        {
            if (_isSelected == true)
            {
                MessageBox.Show("SetName " + this._podrazd);

            }

        }
        public BookRedact(string booknameRedact)
        {
            BookNameRedact = booknameRedact;
        }
        private float? _timePlan;
        public float? TimePlan
        {
            get => _timePlan;
            set
            {
                _timePlan = value;
                RaisePropertyChanged(nameof(TimePlan));
            }
        }

        private DateTime? _timeFakt;
        public DateTime? TimeFakt
        {
            get => _timeFakt;
            set
            {
                _timeFakt = value;
                RaisePropertyChanged(nameof(TimeFakt));
            }
        }


        private int? _brigada;
        public int? Brigada
        {
            get => _brigada;
            set
            {
                _brigada = value;
                RaisePropertyChanged(nameof(Brigada));
            }
        }
        private int? _sm;
        public int? sm
        {
            get => _sm;
            set
            {
                _sm = value;
                RaisePropertyChanged(nameof(sm));
            }
        }

        

        private List<CommentsRedact> commentsRedact;
        public BookRedact(UMRK_BR umrk_br)
        {
             _umrk_br = umrk_br;
              _name = umrk_br.FIO;//.Oborud;
            NameNameRedact = _name;
            sm = umrk_br.sm;
            Data = Data;

            //  br взять sm, запросить в rem дату по sm и в ппр взять t из даты rem

            var orders = _db.UMRK_PPR.Join(_db.UMRK_REM, k => k.Id_PPR, l => l.Pr_rem, (k, l) => new
            {
                
                smena = l.sm,
                tplan = k.T,
                pprID = k.Id_PPR,
                PPRID2 = l.Pr_rem

            }).Where(x => x.smena == sm ).ToList();
            foreach (var something in orders)
            {
                _timePlan = something.tplan;
            }

            //   var parent = _db.UMRK_SM.Where(p => p.Date_nar == Data).GroupBy(p => p.Nar_vid).Select(grp => grp.FirstOrDefault()).ToList().Select(r => new DepartmentRedact(r));


            // var timeP = _db.UMRK_PPR.Where(t => t.Per == rem.Value).Select(t=>t.T).ToList();

            /* var orders = _db.UMRK_REM.Join(_db.UMRK_BR, k => k.sm, l => l.sm, (k, l) => new
             {
                 Date = k.NR,
                 smena = l.sm,
                 oborud = k.Oborud,
                 typeOborud = k.Type_Oborud

             }).Where(x=>x.smena ==sm).ToList();
             foreach (var something in orders)
             {
                 _timePlan = something.smena;
             }*/

            //  _timePlan = 210388;
            //план из плана ППР
            //  _timePlan = commentsRedact.
            /* _oborudname = oborud.Nar_vidal;//.Oborud;
            NameNameRedact = _name;
            _timePlan = _oborud.Time_Plan;
          //  _timeFakt = _oborud.Time_Fakt;
            _brigada = _oborud.Brigada;

            _data = oborud.Data;
            _nar_vidal = oborud.Nar_vidal;
            _smena = oborud.Smena;
            _smena_long = oborud.Smena_long;

            _fio = oborud.FIO;
            _post = oborud.Post;
            _Podrazd = oborud.Podrazd;

            _TO_Type = oborud.TO_Type;
            _work_list = oborud.Work_list;
            _timePlan = oborud.Time_Plan;
         //   _timeFakt = oborud.Time_Fakt;
            _statusNar = oborud.StatusNar;
            _comment = oborud.Comment;
            _master = oborud.Master;


            Data = oborud.Data;*/
            // Data = umrk_rem.sm;


            var rem = _db.UMRK_REM.Where(r => r.sm == sm).ToList().FirstOrDefault();
            //  var naim = _db.NaryadList.Where(p => p.Data == Data).Where(x => x.FIO == _name && x.Nar_vidal == _oborudname)/*.GroupBy(p => p.Brigada).Select(grp => grp.FirstOrDefault())*/.ToList().Select(r => new CommentsRedact(r));

            // var naim = _db.UMRK_BR.Where (p=>p.sm==sm).ToList().Select(r => new CommentsRedact(r));
            var naim = _db.UMRK_BR.Where(p => p.sm == sm).Where(x => x.FIO == _name)/*.GroupBy(p => p.Brigada).Select(grp => grp.FirstOrDefault())*/.ToList().Select(r => new CommentsRedact(r));

            CommentsRedact = new List<CommentsRedact>(naim);
        
            

        }

        public BookRedact(UMRK_REM rem)
        {
            _ppr = ppr;
          //  _timePlan = ppr.T;
            var time = _db.UMRK_PPR.Where(t => t.Per == Data & t.Oborud == _podrazd).ToList().Select(r => new PlanPPRGet(r));
            PlanPPRGet = new List<PlanPPRGet>(time);
        }

        public BookRedact(UMRK_PPR ppr)
        {
            _ppr = ppr;
            _timePlan = ppr.T;
            var time = _db.UMRK_PPR.Where(t => t.Per == Data & t.Oborud == _podrazd).ToList().Select(r => new PlanPPRGet(r));
            PlanPPRGet = new List<PlanPPRGet>(time);
        }
        private List<PlanPPRGet>  _planppr;
        public List<PlanPPRGet> PlanPPRGet
        {
            get
            {
                return _planppr;
            }
            set
            {
                _planppr = value;
                RaisePropertyChanged("PlanPPRGet");
            }
        }
        public List<CommentsRedact> CommentsRedact
        {
            get
            {
                return commentsRedact;
            }
            set
            {
                commentsRedact = value;
                RaisePropertyChanged("CommentsRedact");
            }
        }

       


        /*  public DateTime? Data
          {
              get; set;
          }*/
        public string NameNameRedact
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

        private readonly NaryadVm nar = new NaryadVm();
        public NaryadVm Nar
        {
            get { return nar; }

        }
        private NaryadVm __nar;
        public NaryadVm _Nar

        {
            get { return __nar; }
            set
            {
                __nar = value;
                RaisePropertyChanged(nameof(_Nar));

            }


        }


        private NaryadVm collection;
        public NaryadVm Collection
        {
            get { return collection; }
            set
            {
                collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        private DateTime? _data;
        public DateTime? Data
        {
            get => _data;
            set
            {

                _data = value;
                RaisePropertyChanged(nameof(Data));


            }
        }



        private string _nar_vidal;
        public string Nar_vidal
        {
            get => _nar_vidal;
            set
            {
                _nar_vidal = value;
                RaisePropertyChanged(nameof(Nar_vidal));
            }
        }
        private int? _smena;
        public int? Smena
        {
            get => _smena;
            set
            {
                _smena = value;
                RaisePropertyChanged(nameof(Smena));
            }
        }

        private int? _smena_long;
        public int? Smena_long
        {
            get => _smena_long;
            set
            {
                _smena_long = value;
                RaisePropertyChanged(nameof(Smena_long));
            }
        }

        private string _fio;
        public string FIO
        {
            get => _fio;
            set
            {
                _fio = value;
                RaisePropertyChanged(nameof(FIO));
            }
        }

        private string _post;
        public string Post
        {
            get => _post;
            set
            {
                _post = value;
                RaisePropertyChanged(nameof(Post));
            }
        }

     /*   private string _Podrazd;
        public string Podrazd
        {
            get => _Podrazd;
            set
            {
                _Podrazd = value;
                RaisePropertyChanged(nameof(Podrazd));
            }
        }*/
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

        private string _TO_Type;
        public string TO_Type
        {
            get => _TO_Type;
            set
            {
                _TO_Type = value;
                RaisePropertyChanged(nameof(TO_Type));
            }
        }

        private string _work_list;
        public string Work_list
        {
            get => _work_list;
            set
            {
                _work_list = value;
                RaisePropertyChanged(nameof(Work_list));
            }
        }


        private string _statusNar;
        public string StatusNar
        {
            get => _statusNar;
            set
            {
                _statusNar = value;
                RaisePropertyChanged(nameof(StatusNar));
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
        private string _master;
        public string Master
        {
            get => _master;
            set
            {
                _master = value;
                RaisePropertyChanged(nameof(Master));
            }

        }

        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                RaisePropertyChanged(nameof(Id));
            }

        }

        private NaryadVm _selectedOrder;
        public NaryadVm SelectedItem
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                RaisePropertyChanged(nameof(SelectedItem));

                //  TimePlan = SelectedItem.Time_Plan;
                //     TimeFakt = SelectedItem.Time_Fakt;
            }
        }



        public void Print()
        {
            if (_oborud != null)
            {
                //  MessageBox.Show(_oborud.Oborud + " + Data" + _oborud.Data);




                //    List<NaryadList> lista = _selectedNar;//.Select(e => (NaryadList)e).ToList();
                /*  lista.ForEach(_nareditV => _db.NaryadList.ToList());*/
                var list = _oborud.FIO;


                var resourceName = "WPA_Mech.NarForm.xlsx";
                var asm = Assembly.GetExecutingAssembly();


                Assembly assembly = Assembly.GetExecutingAssembly();
                List<string> embeddedResources = new List<string>(assembly.GetManifestResourceNames());

                using (Stream stream = asm.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                }


                Assembly asml = Assembly.GetExecutingAssembly();
                string file = string.Format("{0}.NarForm.xlsx", asm.GetName().Name);
                Stream fileStream = asm.GetManifestResourceStream(file);
                SaveStreamToFile(Path.GetTempPath() + "NarForm.xlsx", fileStream);  //<--here is where to save to disk
                Microsoft.Office.Interop.Excel.Application xlApp = new Excel.Application();
                Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Path.GetTempPath() + "NarForm.xlsx");
                // Excel.Worksheet worksheet;
                Excel.Worksheet sheet = xlWorkBook.Worksheets["Лист2"] as Excel.Worksheet;





                var orders = _db.UMRK_PPR.ToList().ToList().Where(x => x.Per == Data);


                int rowIndexBeginOrdersi = 3;

                int rowIndexCurrentRecordi = rowIndexBeginOrdersi;
                foreach (var podr in orders)
                {
                    (sheet.Cells[17, 17] as Excel.Range).Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;


                    //   xlApp.Cells[rowIndexCurrentRecord, 1].Value = rowIndexCurrentRecord - 2;
                    sheet.Cells[rowIndexCurrentRecordi, 14].Value = podr.Oborud;//списко оборудования 
                    sheet.Cells[rowIndexCurrentRecordi, 15].Value = podr.Type_work;//тип работ
                    sheet.Cells[rowIndexCurrentRecordi, 16].Value = podr.T;//время




                    rowIndexCurrentRecordi++;
                }







                int rowIndexBeginOrders = 6;

                int rowBrigada = 5;

                int rowIndexCurrentRecord = rowIndexBeginOrders;
                int brigafaRecord = rowBrigada;



                int rowWork = 20;
                int workRecord = rowWork;
                var Work = "";
              //  string Worktest = _oborud.Work_list;
              //  string[] resulwork = Worktest.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
              /*  foreach (string s in resulwork)
                {
                    Work = s.ToString();
                    // Console.WriteLine(Work);
                    xlApp.Cells[workRecord, 3].Value = Work;



                    workRecord++;

                }*/
                //97?5
                int cellsPersonal = 6;
                int rowPersonal = 5;
                int rowIndexPerosnal = rowPersonal;
                int cellsIndexPersonal = cellsPersonal;
              //  var personal = _db.NaryadList.ToList().ToList().Where(x => x.Data == _oborud.Data && x.Nar_vidal == _oborud.Nar_vidal && x.Brigada == _oborud.Brigada);
            /*    foreach (var pers in personal)
                {

                    var Per = "";
                    string test = pers.FIO;
                    string[] resul = test.Split(new string[] { "||" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in resul)
                    {
                        Per = s.ToString();
                        //Console.WriteLine(Per);
                        xlApp.Cells[rowIndexCurrentRecord + 1, 3].Value = Per;

                        xlApp.Cells[19, brigafaRecord].Value = Per;

                        var Fakt = pers.Time_Fakt;
                        if (Fakt != null)
                        {
                            // xlApp.Range[xlApp.Cells[96, brigafaRecord], xlApp.Cells[97, brigafaRecord]].NumberFormat = "@";
                            xlApp.Cells[93, brigafaRecord].Value = Convert.ToInt32(pers.Time_Fakt);

                        }
                        rowIndexCurrentRecord++;
                        brigafaRecord++;
                    }


                }
                */
                /*
                if (_oborud.Smena == 1)
                {
                    sheet.Cells[6, 5].Value = "08:00 ";
                    sheet.Cells[8, 5].Value = "20:00 ";
                }
                if (_oborud.Smena == 2)
                {
                    sheet.Cells[6, 5].Value = "20:00 ";
                    sheet.Cells[8, 5].Value = "08:00 ";
                }
                if (_oborud.Smena == 3)
                {
                    sheet.Cells[6, 5].Value = "08:00 ";
                    sheet.Cells[8, 5].Value = "07:00 ";
                }


                sheet.Cells[4, 3].Value = _oborud.Master;
                sheet.Cells[6, 3].Value = _oborud.Master;

                sheet.Cells[19, 4].Value = _oborud.Master;


                sheet.Cells[104, 3].Value = "Наряд задание выдал " + _oborud.Nar_vidal + " (______________________________________)";

                sheet.Cells[104, 4].Value = "Наряд задание получил " + _oborud.Master + " (______________________________________)";
                sheet.Cells[109, 3].Value = "Наряд задание сдал " + _oborud.Master + " (______________________________________)";

                sheet.Cells[109, 4].Value = "Наряд задание принял " + _oborud.Nar_vidal + " (______________________________________)";

                sheet.Cells[3, 4].Value = _oborud.Data;//дата
                sheet.Cells[6, 4].Value = _oborud.Data;//дата
                sheet.Cells[8, 4].Value = _oborud.Data;//дата


                sheet.Cells[16, 3].Value = "Наряд задание на выполнение тезнического облсуживания оборудования " + _oborud.Oborud;//подпись к наряду
                */

                //оборудование из наряда














                xlApp.Visible = true;

                if (xlWorkBook == null)
                {
                    MessageBox.Show("Error: Unable to open Excel file.");
                    return;
                }
            }
        }



        public void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }


        public void Delete()
        {
            if (_oborud != null)
            {

                /*   var existingBlox = new NaryadList
                   {
                       Id = _oborud.Id,
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
                       Master = _oborud.Master,



                   };*/
                var existingBlox = (from nlist in _db.NaryadList where nlist.Brigada == Brigada select nlist);
                using (var context = new Bez1Entities())
                {

                    foreach (var item in existingBlox)
                    {
                        context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        context.SaveChanges();
                    }
                    //  context.Entry(existingBlox).State = System.Data.Entity.EntityState.Deleted;
                    //  context.SaveChanges();



                    MessageBox.Show("Данные удалены ");


                }
            }
        }


        public void UpdateModel()
        {


            if (_umrk_rem != null)
            {

                /* var existingBlox = new NaryadList
                 {
                     Id = _oborud.Id,
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
                     Master = _oborud.Master,


                 };*/

                var existingBlox = new UMRK_REM
                {
                    Num_br = _umrk_rem.Num_br,
                    sm = _umrk_rem.sm,
                    Podrazd = "Подразделение",
                    Oborud = _umrk_rem.Oborud,
                    Type_Oborud = "Тип оборудования",
                    Type_work = _umrk_rem.Type_work,
                    ind = false, //????
                    NR = _umrk_rem.NR,//начало
                    KR = _umrk_rem.KR,//конец
                    TR = _timeFakt,             //TR = ,// Общая продолжительность
                    ND = _umrk_rem.ND,
                    KD = _umrk_rem.KD,
                    TD = _umrk_rem.TD,
                    Komment = "КомментRedact",
                    Komment1 = "Коммент1Redact",
                };

                using (var context = new Bez1Entities())
                {
                    context.Entry(existingBlox).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();



                    MessageBox.Show("Данные изменены ");
                }
            }

        }

        public static implicit operator BookRedact(TreeRedactVM v)
        {
            throw new NotImplementedException();
        }
    }
}

