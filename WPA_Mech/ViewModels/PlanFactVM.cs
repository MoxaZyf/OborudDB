using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Models;
using WPA_Mech.Utils;
using System.Windows.Input;
using System.Windows;
using System.IO;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using GalaSoft.MvvmLight.Command;
using System.Globalization;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace WPA_Mech.ViewModels
{
   
    public class PlanFactVM : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();



        DataTable _manualDataTable;
        public DataTable ManualDataTable
        {
            get
            {
                return _manualDataTable;
            }
            set
            {
                _manualDataTable = value;
                RaisePropertyChanged("ManualDataTable");
            }
        }

        private ICommand _testCommand;



        private ICommand _openfile;
        private ICommand _saveInDB;


     












        public ICommand PlanFactCommand { get; set; }

        private DateTime? _selectedDataPrint;

        public DateTime? SelectedDataPrint
        {
            get { return _selectedDataPrint; }
            set
            {
                if (_selectedDataPrint == value)
                    return;
                _selectedDataPrint = value;
                RaisePropertyChanged("SelectedDataPrint");

            }
        }
        private string _podrazd;


        public string Podrazd
        {
            get => _podrazd;
            set
            {
                _podrazd = value;
                RaisePropertyChanged("Podrazd");
            }
        }
        public  class MyTable :IComparable<MyTable>
        {
            public string oborud { get; set; }
            public DateTime date_nar { get; set; }
            public int id_rem { get; set; }
            public string type_work { get; set; }
            public decimal t_r { get; set; }
            public decimal t_regl { get; set; }
            public int day { get; set; }
            public string oborud_type { get; set; }
           

            public int CompareTo(MyTable other)
            {
                int res = oborud.CompareTo(other.oborud);
                return res != 0 ? res : date_nar.CompareTo(other.date_nar);
            }
        }
     
        public class PlanTable :IComparable<PlanTable>
        {
            public string oborud { get; set; }
            public DateTime Per { get; set; }
            public string type_oborud { get; set; }
            public string podrazd { get; set; }
            public string type_work { get; set; }
            public Single t { get; set; }
            public Single tr { get; set; }
            public int day { get; set; }



            public int CompareTo (PlanTable other)
            {
                int res = oborud.CompareTo(other.oborud);
                return res != 0 ? res : Per.CompareTo(other.Per);
            }
        }



        private int _start;
        private int _stop = 100;
        private int _current;

        private bool _isStarted;

        private CancellationTokenSource _runCts;
        public int Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
                RaisePropertyChanged("Start");
            }
        }

        public int Stop
        {
            get
            {
                return _stop;
            }
            set
            {
                _stop = value;
                RaisePropertyChanged("Stop");
            }
        }

        public int Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
                RaisePropertyChanged("Current");
            }
        }

        public bool IsStarted
        {
            get
            {
                return _isStarted;
            }
            set
            {
                _isStarted = value;
                RaisePropertyChanged("IsStarted");
            }
        }


        public PlanFactVM()
        {
            PlanFactCommand = new GalaSoft.MvvmLight.Command.RelayCommand(PlanFact, null);

           

        }
       

        
       

       
        

       

        public void PlanFact()
        {
            //var DataBegin = SelectedDataPrint.Value.Month;
            var day = (DateTime.DaysInMonth(SelectedDataPrint.Value.Year, SelectedDataPrint.Value.Month)); // последний день выбранного месяца
            DateTime DataBegin = new DateTime(SelectedDataPrint.Value.Year, SelectedDataPrint.Value.Month, 1);
            DateTime DataEnd = new DateTime(SelectedDataPrint.Value.Year, SelectedDataPrint.Value.Month, day);

            



            var resourceName = "WPA_Mech.PlanFact.xlsx";

            var asm = Assembly.GetExecutingAssembly();


            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> embeddedResources = new List<string>(assembly.GetManifestResourceNames());



            using (Stream stream = asm.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
            }


            Assembly asml = Assembly.GetExecutingAssembly();
            string file = string.Format("{0}.PlanFact.xlsx", asm.GetName().Name);
            Stream fileStream = asm.GetManifestResourceStream(file);
            SaveStreamToFile(Path.GetTempPath() + "PlanFact.xlsx", fileStream);  //<--here is where to save to disk
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Path.GetTempPath() + "PlanFact.xlsx");
            //  Excel.Worksheet worksheet;

            //  MessageBox.Show(DateBegin.ToString());


            int rowIndexBeginOrders = 3;
            int rowIndexCurrentRecord = rowIndexBeginOrders;

            int rowVidZabol = 1;
            int rowVidZabolRecord = rowVidZabol;

            int rowVidZabol8 = 0;
            int rowVidZabolRecord8 = rowVidZabol8;


        
            List<MyTable> resultQ;
            List<string>  rezOborud;
            List<string> PlanrezOborud;
            List<MyTable> results;
            string oborudName;
            List<PlanTable> PlanResultQ;
            List<PlanTable> PlanResults;



       /*    int[,] arrQ = new int[8, 31];
            Random random = new Random();
            Console.WriteLine("Time1 " + DateTime.Now);
            for (int iz = 0; iz < 8; iz++)
            {
                for (int j = 0; j < 31; j++)
                {

                    Excel.Range werty = (Excel.Range)xlApp.Cells[25+iz, 4+j ];
                    werty.Value2 = random.Next(100);
                    arrQ[iz, j] = random.Next(100);
                   // Console.Write("{0,4}", arrQ[iz, j]);
                }
                //Console.WriteLine();
            }
            Console.WriteLine("Time2 " + DateTime.Now);
            Excel.Range testArr = xlApp.get_Range("E25:AI32");

             testArr.Value2 = arrQ;
            Console.WriteLine("Time3 " + DateTime.Now);
            */
            using (SqlConnection con = new SqlConnection(@"Data Source = zsudb\sccm; user id = alex; password = 123; Initial Catalog = Bez2019; "))
            {

                using (SqlCommand command = new SqlCommand("                    SELECT        t1.Date_nar, t1.Id_rem,t1.Type_Oborud, t1.Oborud, t1.Type_work, " +
                "ISNULL(CAST(ROUND(t1.T_R, 2) AS decimal(10, 2)), 0) AS T_R, " +
                "ISNULL(CAST(ROUND(t3.t_regl, 2) AS decimal(10, 2)), 0) AS t_regl, " +
                "DAY(t1.Date_nar) AS DaY " +
                "FROM(SELECT        dbo.UMRK_SM.Date_nar, dbo.UMRK_REM.Id_rem, dbo.UMRK_REM.Oborud,dbo.UMRK_REM.Type_Oborud, dbo.UMRK_REM.Type_work," +
                " CASE WHEN SUM(isnull(CAST(dbo.UMRK_REM.TR AS float) + 2, 0))  = 0 THEN SUM(isnull(CAST(dbo.UMRK_REM.TD AS float) + 2, 0)) * 24 " +
                "ELSE SUM(isnull(CAST(dbo.UMRK_REM.TR AS float) + 2, 0)) * 24 END AS T_R                          " +
                "FROM dbo.UMRK_SM INNER JOIN dbo.UMRK_REM ON dbo.UMRK_SM.Id_sm = dbo.UMRK_REM.sm                        " +
                "  WHERE(dbo.UMRK_SM.Date_nar >='"+ DataBegin+"') AND(dbo.UMRK_SM.Date_nar <= '"+DataEnd+"') " +
                //   "AND(dbo.UMRK_REM.Oborud = 'Печь регенерации углерода `KEMIX` {Поз. 114-1}')                         " +
                 //"OR(dbo.UMRK_REM.Oborud = 'Фабрика ЗИФ-УВП (Комплекс оборудования основной технологической линии)')   " +
                 //"AND   (dbo.UMRK_SM.Date_nar >= '01.06.2019') AND(dbo.UMRK_SM.Date_nar <= '30.06.2019')                    " +
                 "AND (dbo.UMRK_REM.Podrazd ='"+Podrazd +"')                      " +
                " GROUP BY dbo.UMRK_REM.Oborud, dbo.UMRK_SM.Date_nar, dbo.UMRK_REM.Id_rem, dbo.UMRK_REM.Type_work,dbo.UMRK_REM.Type_Oborud ) AS t1 " +
                "LEFT OUTER JOIN  (SELECT Id_rem, 24 * SUM(ISNULL(CAST(time_op AS float), 0)) AS t_n_regl " +
                " FROM   dbo.UMRK_OP WHERE(Regl = 1) AND(Operation LIKE '%Итого%')   GROUP BY Id_rem) AS t2 ON t1.Id_rem = t2.Id_rem " +
                "LEFT OUTER JOIN  (SELECT        Id_rem, 24 * SUM(ISNULL(CAST(time_op AS float), 0)) AS t_regl " +
                " FROM  dbo.UMRK_OP AS UMRK_OP_1  WHERE(Regl = 0) AND(Operation LIKE '%Итого%')  GROUP BY Id_rem) AS t3 ON t1.Id_rem = t3.Id_rem order by t1.Oborud", con))
                               
                {
                    DataTable dt = new DataTable();
                    con.Open();
                   // SqlParameter nameParam = new SqlParameter("@DataBegin", DataBegin, "@DataEnd" , DataEnd);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                        //p.Date_nar == null ? "null" : p.Date_nar.ToString()
                        resultQ = dt.AsEnumerable().Select(se => new MyTable() { id_rem = se.Field<int>("id_rem"), oborud = se.Field<string>("Oborud"), oborud_type = se.Field<string>("Type_Oborud"), type_work = se.Field<string>("type_work"), date_nar = se.Field<DateTime>("date_nar"), day = se.Field<int>("DaY"), t_regl = Convert.ToDecimal(se.Field<decimal>("t_regl") == 0 ? 0 : se.Field<decimal>("t_regl")), t_r = Convert.ToDecimal(se.Field<decimal>("T_R") == 0 ? 0 : se.Field<decimal>("T_R")) }).ToList();
                        PlanrezOborud = dt.AsEnumerable().Distinct().Select(se => se.Field<string>("oborud")).ToList();

                        results = resultQ.GroupBy(x => x.oborud).Select(x => x.First()).ToList();
                    }
                   

                }
            }


            using (SqlConnection Plancon = new SqlConnection(@"Data Source = zsudb\sccm; user id = alex; password = 123; Initial Catalog = Bez2019; "))
            {
                using (SqlCommand planCommand = new SqlCommand(" SELECT * ,DAY(dbo.UMRK_PPR.Per)as DaY FROM UMRK_PPR WHERE " +
                    "dbo.UMRK_PPR.Per >='"+ DataBegin +"' and dbo.UMRK_PPR.Per <= '"+DataEnd+"'" +
                   " AND (dbo.UMRK_PPR.Podrazd ='" + Podrazd + "')   " +
                 // "    AND(dbo.UMRK_PPR.Oborud = 'Печь регенерации углерода `KEMIX` {Поз. 114-1}')  " +
                   "  order by dbo.UMRK_PPR.Oborud                ", Plancon))

                {
                    DataTable Plandt = new DataTable();
                      Plancon.Open();
                    using (SqlDataReader Planreader = planCommand.ExecuteReader())
                    {
                        Plandt.Load(Planreader);
                        //p.Date_nar == null ? "null" : p.Date_nar.ToString()
                        PlanResultQ = Plandt.AsEnumerable().Select(se => new PlanTable() { oborud = se.Field<string>("Oborud"), type_oborud = se.Field<string>("Type_Oborud"), type_work = se.Field<string>("type_work"), Per = se.Field<DateTime>("Per"), day = se.Field<int>("DaY"), t = se.Field<Single>("t"), tr =se.Field<Single>("TR")  }).ToList();
                        rezOborud = Plandt.AsEnumerable().Distinct().Select(se => se.Field<string>("oborud")).ToList();

                        PlanResults = PlanResultQ.GroupBy(x => x.oborud).Select(x => x.First()).ToList();
                    }
                }
            }

            // var planCount = PlanResults.Count() == 0 ? results.Count() : results.Count(); // Convert.ToDecimal(se.Field<decimal>("T_R") == 0 ? 0 : se.Field<decimal>("T_R"))

            //вывод плана 
            object[,] dataO = new object[PlanResults.Count(), 1];
           
                Excel.Range NamePodrazd = xlApp.get_Range("B4:B4");

                NamePodrazd.Value2 = Podrazd;

                Excel.Range NamePodrazd2 = xlApp.get_Range("O6:O6");

                NamePodrazd2.Value2 = Podrazd;


                Excel.Range DataMonth = xlApp.get_Range("S6:S6");
                string monthName = SelectedDataPrint.Value.ToMonthName();//.ToString("MMM", CultureInfo.CurrentCulture);

                DataMonth.Value2 = monthName + " " + SelectedDataPrint.Value.Year + "г.";

                int o;
                
                {
                    for (o = 0; o < PlanResults.Count(); o++)
                    {
                        dataO[o, 0] = PlanResults[o];


                        //   Console.WriteLine("[INFO] Syncronisated PG->SQLite rows: {0}", results[i].oborud);

                        Excel.Range oborudname = (Excel.Range)xlApp.Cells[9 + o * 8, 3 ];
                        Excel.Range oborudname2 = (Excel.Range)xlApp.Cells[9 + o * 8, 3 ];


                        Excel.Range rangeOborud = xlApp.get_Range(oborudname, oborudname2);
                        //  xlApp.get_Range(oborudname, oborudname2).Interior.Color = Excel.XlRgbColor.rgbRed;
                        rangeOborud.Value2 = PlanResults[o].oborud;

                        Excel.Range oborudtype = (Excel.Range)xlApp.Cells[9 + o * 8, 2 ];
                        Excel.Range oborudtype2 = (Excel.Range)xlApp.Cells[9 + o * 8, 2 ];


                        Excel.Range rangeOborudtype = xlApp.get_Range(oborudtype, oborudtype2);

                        rangeOborudtype.Value2 = PlanResults[o].type_oborud;
                        // xlApp.get_Range(oborudtype, oborudtype2).Interior.Color = Excel.XlRgbColor.rgbRed;

                        oborudName = PlanResults[o].oborud;


                    }
                }
                for (var xx = 0; xx < PlanResults.Count(); xx++)
                {
                    var found = PlanResultQ.FindAll(p => p.oborud == PlanResults[xx].oborud);
                    foreach (var xxx in found)
                    {

                        // Console.WriteLine("Оборудование PLAN {0} и  ", xxx.oborud);//, xxx.type_work, xxx.day);

                        Excel.Range tw11 = (Excel.Range)xlApp.Cells[9 + xx * 8, 4 + xxx.day];
                        Excel.Range tw22 = (Excel.Range)xlApp.Cells[9 + xx * 8, 4 + xxx.day];
                        Excel.Range rangetw1 = xlApp.get_Range(tw11, tw22);
                        rangetw1.Value2 = "[" + xxx.type_work + "]";




                        Excel.Range t_r1 = (Excel.Range)xlApp.Cells[11 + xx * 8, 4 + xxx.day];
                        Excel.Range t_r2 = (Excel.Range)xlApp.Cells[11 + xx * 8, 4 + xxx.day];
                        Excel.Range r_r1 = xlApp.get_Range(t_r1, t_r2);
                        r_r1.Value2 = xxx.t;




                        Excel.Range t_regl1 = (Excel.Range)xlApp.Cells[13 + xx * 8, 4 + xxx.day];
                        Excel.Range t_regl2 = (Excel.Range)xlApp.Cells[13 + xx * 8, 4 + xxx.day];
                        Excel.Range r_regl1 = xlApp.get_Range(t_regl1, t_regl2);
                        r_regl1.Value2 = xxx.tr;



                    }
                }
            
            //Проверка PlanResults.Count() на нолЬ

           var planCount = PlanResults.Count() == 0 ? results.Count() : results.Count(); // Convert.ToDecimal(se.Field<decimal>("T_R") == 0 ? 0 : se.Field<decimal>("T_R"))
            //вывод факта

            int ofakt;
                object[,] dataOfakt = new object[results.Count(), 1];
                {
                    for (ofakt = 0; ofakt < results.Count(); ofakt++)
                    {
                        dataO[ofakt, 0] = results[ofakt];


                        //  Console.WriteLine("[INFO] Syncronisated PG->SQLite rows: {0}", results[i].oborud);

                        Excel.Range oborudname = (Excel.Range)xlApp.Cells[9 + ofakt * 8, 2 ];
                        Excel.Range oborudname2 = (Excel.Range)xlApp.Cells[9 + ofakt * 8, 2 ];


                        Excel.Range rangeOborud = xlApp.get_Range(oborudname, oborudname2);

                        //   rangeOborud.Value2 = results[ofakt].oborud;

                        /*   Excel.Range oborudtype = (Excel.Range)xlApp.Cells[9 + ofakt * 8, 2 + data];
                           Excel.Range oborudtype2 = (Excel.Range)xlApp.Cells[9 + ofakt * 8, 2 + data];


                           Excel.Range rangeOborudtype = xlApp.get_Range(oborudtype, oborudtype2);

                          // rangeOborudtype.Value2 = results[o].oborud_type;


                           oborudName = results[ofakt].oborud;
                           */

                    }
                }
                // Console.WriteLine("TimeBegin {0} " , DateTime.Now );
                for (var xx = 0; xx < planCount; xx++)  // results
                {//PlanResults

                    /*   var message = string.Join(Environment.NewLine,
                    PlanrezOborud.GroupJoin(rezOborud, x => x, y => y, (x, y) => new { number = x, count = y.Count() })
                        .Where(n => n.count > 0)
                        .Select(n => n.number + " встречается " + (n.count + 1) + " раза"));

                       Console.WriteLine(message);*/
                    var found = resultQ.FindAll(p => p.oborud == PlanResults[xx].oborud);


                    foreach (var xxx in found)
                    {
                        //  Console.WriteLine("resultQ {0}  /// PlanResultQ {1} ", resultQ[xx].oborud, PlanResultQ[xx].oborud );
                        //  Console.WriteLine("Оборудование {0} и тип работ {1} и дата {2} ", xxx.oborud, xxx.type_work, xxx.day);
                        // Console.WriteLine("Оборудование FAKT {0} ", xxx.oborud);


                        /*   Excel.Range oborudname = (Excel.Range)xlApp.Cells[9 + xx * 8, 2 ];
                           Excel.Range oborudname2 = (Excel.Range)xlApp.Cells[9 + xx * 8, 2 ];


                           Excel.Range rangeOborud = xlApp.get_Range(oborudname, oborudname2);

                              rangeOborud.Value2 =  xxx.oborud;*/



                        Excel.Range tw11 = (Excel.Range)xlApp.Cells[10 + xx * 8, 4 + xxx.day];
                        Excel.Range tw22 = (Excel.Range)xlApp.Cells[10 + xx * 8, 4 + xxx.day];
                        Excel.Range rangetw1 = xlApp.get_Range(tw11, tw22);
                        // rangetw1.Value2 = "[" + xxx.type_work + "]";
                        if (rangetw1.Value2 == null)
                        {
                            rangetw1.Value2 = " ";

                        }

                        if (rangetw1.Value2 != null)
                        {

                        string range = xxx.type_work;
                         rangetw1.Value2 = rangetw1.Value2 == xxx.type_work ? "" : xxx.type_work + "[" + xxx.type_work + "]";
                         //  rangetw1.Value2 = rangetw1.Value2 + "[" + xxx.type_work + "]";
                        // Convert.ToDecimal(se.Field<decimal>("T_R") == 0 ? 0 : se.Field<decimal>("T_R"))

                    }

                   /* if (xxx.type_work == xxx.type_work)
                    {
                        rangetw1.Value2 = xxx.type_work;// rangetw1.Value2 + "[" + xxx.type_work + "]";
                       // Console.WriteLine("88888888 " + rangetw1.Value2);

                    }*/





                    Excel.Range t_r1 = (Excel.Range)xlApp.Cells[12 + xx * 8, 4 + xxx.day];
                        Excel.Range t_r2 = (Excel.Range)xlApp.Cells[12 + xx * 8, 4 + xxx.day];
                        Excel.Range r_r1 = xlApp.get_Range(t_r1, t_r2);

                        if (r_r1.Value2 == null)
                        {
                            r_r1.Value2 = 0;

                        }

                        if (r_r1.Value2 != null)
                        {
                            r_r1.Value2 = Convert.ToDecimal(r_r1.Value2) + xxx.t_r;

                        }



                        Excel.Range t_regl1 = (Excel.Range)xlApp.Cells[14 + xx * 8, 4 + xxx.day];
                        Excel.Range t_regl2 = (Excel.Range)xlApp.Cells[14 + xx * 8, 4 + xxx.day];
                        Excel.Range r_regl1 = xlApp.get_Range(t_regl1, t_regl2);


                        if (r_regl1.Value2 == null)
                        {
                            r_regl1.Value2 = 0;

                        }

                        if (r_regl1.Value2 != null)
                        {
                            r_regl1.Value2 = Convert.ToDecimal(r_regl1.Value2) + xxx.t_regl;

                        }

                    }
                }
                //   Console.WriteLine("TimeEND {0} ", DateTime.Now);

           


            xlApp.Visible = true;

            if (xlWorkBook == null)
            {
                MessageBox.Show("Error: Unable to open Excel file.");
                return;
            }
        
    }
  
        #region загрузка плана 

        public ICommand Openfile
        {
            get
            {
                if (_openfile == null)
                {
                    _openfile = new OpenCommand(this);
                }
                return _openfile;
            }
        }

        abstract class MyCommand : ICommand
        {
            protected PlanFactVM _cvm;
            public MyCommand(PlanFactVM cvm)
            {
                _cvm = cvm;
            }
            public event EventHandler CanExecuteChanged;
            public abstract bool CanExecute(object parameter);
            public abstract void Execute(object parameter);
        }
        public static string _selectedPath;
        public static string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                _selectedPath = value;
                // RaisePropertyChanged("SelectedPath");
            }
        }
        class OpenCommand : MyCommand
        {


            public OpenCommand(PlanFactVM cvm) : base(cvm)
            {

            }



            public override bool CanExecute(object parameter)
            {
                return true;
            }

            public override void Execute(object parameter)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                    //    MessageBox.Show("ура " + openFileDialog.FileName);
                    SelectedPath = openFileDialog.FileName;
            }
        }


        public class Metric
        {
            public DateTime Per { get; set; }
            public string Oborud { get; set; }
            public string Type_oborud { get; set; }
            public string Podrazd { get; set; }
            public string Type_work { get; set; }
            public double? T { get; set; }
            public double? TR { get; set; }


        }

       public  ICommand TestCommand
        {
            get
            {
                return _testCommand ?? (_testCommand = new ActionCommand( Run));
            }

        }

        private void Run()
        {
            if (IsStarted) return;
            _runCts = new CancellationTokenSource();
            Task.Run(() => ExecuteTest(Start, Stop));
        }


        private void ExecuteTest(int start, int stop)
        {
            IsStarted = true;
            Current = start;
            for (int i = start; i < stop; i++)
            {
                if (_runCts != null && _runCts.IsCancellationRequested) break;
                Execute(i);

                Current++;

            }
            if (_runCts != null)
            {
                _runCts.Dispose();
                _runCts = null;
            }
        }
            private void Execute(int i)
        {
           
           
            Microsoft.Office.Interop.Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook;
            Excel.Worksheet worksheet;
            Excel.Range range;
            workbook = excelApp.Workbooks.Open(SelectedPath/*@"D:\tk.xlsx"*/);
            //  MessageBox.Show("SelectedPath " + SelectedPath);


        

                worksheet = (Excel.Worksheet)workbook.Sheets["PPR"];

            

            range = worksheet.UsedRange;
            /*запись в бд*/

          /*  DataTable dt = new DataTable();
       

            dt.Columns.Add("Per");

            dt.Columns.Add("Oborud");
            dt.Columns.Add("Type_oborud");
            dt.Columns.Add("Podrazd");
            dt.Columns.Add("Type_work");
            dt.Columns.Add("T");
            dt.Columns.Add("TR");*/
            DateTime DataBegin = new DateTime(2019, 12, 01);
            DateTime Data_end;
            var day = (DateTime.DaysInMonth(DataBegin.Year, DataBegin.Month)); // последний день выбранного месяца
            Data_end = new DateTime(2019, 12, day);
           
         //   string delstr;
          //  delstr = @"delete from UMRK_PPR WHERE dbo.UMRK_PPR.Per >= '" + DataBegin + "' and dbo.UMRK_PPR.Per <= '" + Data_end + "' and dbo.UMRK_PPR.Oborud = '" + oborud + "' and dbo.UMRK_PPR.Podrazd = '" + PodrazdList + "'";


            for (int rrow = 1; rrow <= day; rrow++)
            {
                DateTime dat = new DateTime(2019, 12, 01);
                for ( i =0; i<2;i++)
                { 
                DateTime Date_tek = dat.AddDays(rrow);
                var metric = new Metric
                {
                    Per = Date_tek, //worksheet.Cells[9, rrow + 4].Value2,
                    Podrazd = worksheet.Cells[4, 1].Value2,
                    Oborud = worksheet.Cells[11+i*8, 2].Value2,
                    Type_oborud = worksheet.Cells[11+i*8, 1].Value2,
                    Type_work = worksheet.Cells[11+i*8, rrow + 4].Value2,
                    T = Convert.ToDouble(worksheet.Cells[13+i*8, rrow + 4].Value2),
                    TR = Convert.ToDouble(worksheet.Cells[15 + i * 8, rrow + 4].Value2),
                    
                    // String.IsNullOrEmpty(tokens[21]) ? (double?)null : (double?)Convert.ToDouble(tokens[21]);
                };

                    SqlConnection Con = new SqlConnection(@"Data Source=ZSUDB\SCCM;Initial Catalog=Bez2019;Integrated Security=False;User ID=Alex;Password=123");
                    SqlCommand com;

                    string delstr;
                    Con.Open();
                    delstr = @"delete from UMRK_PPR WHERE dbo.UMRK_PPR.Per >= '" + DataBegin + "' and dbo.UMRK_PPR.Per <= '" + Data_end + "' and dbo.UMRK_PPR.Oborud = '" + metric.Oborud + "' and dbo.UMRK_PPR.Podrazd = '" + metric.Podrazd + "'";
                    com = new SqlCommand(delstr, Con);



                   

                    
                      
                    
                    

                    if (metric.Type_work != null && metric.T != 0 && metric.TR != 0)
                    {
                        string str = @"insert into UMRK_PPR (Per, Oborud, Type_oborud, Podrazd, Type_work, T, TR) values ('" + Date_tek + "','" + metric.Oborud + "','" + metric.Type_oborud + "','" + metric.Podrazd + "','" + metric.Type_work + "','" + metric.T + "','" + metric.TR + "') ";
                        Console.WriteLine("DELSTR " + str.ToString());
                    com = new SqlCommand(str, Con);
                    com.ExecuteNonQuery();
                }
                  Con.Close();
                   // Console.WriteLine("METRICA " + metric.Oborud.ToString());
                }
            }

            /*     for (row = 2; row <= range.Rows.Count; row++)
                 {
                     //Получение одной ячейки как ранга
                     Excel.Range forYach = worksheet.Cells[row, 1] as Excel.Range;
                     //Получаем значение из ячейки и преобразуем в строку
                     if ((worksheet.Cells[row, 1]).Value != null)
                     {
                         string work = forYach.Value2.ToString();

                         //чтение нулевых ячеек
                         // Excel.Range forI = Convert.ToString(worksheet.Cells[row, 2].Value2) as Excel.Range;

                         ///раскидать по строчкам. наверное добавить цикл проверки на конец заполнения листа
                         ///
                          DataBegin = new DateTime(2019, 12, 01);// worksheet.Cells[2, 1].Value2;// new DateTime(2019, 01, 12);
                         string strings = worksheet.Cells[3, 3].Value2;
                         //MessageBox.Show(worksheet.Cells[2, 1].Value2.ToString());
                         // DateTime data = DataBegin;
                          day = (DateTime.DaysInMonth(DataBegin.Year, DataBegin.Month)); // последний день выбранного месяца

                         Data_end = new DateTime(2019, 12, day);

                        // DateTime Date_tek;//= new DateTime();
                         int dj = 0;
                         int i = 0;
                         for ( dj = 0; dj <= day; dj++)
                         {                        
                             DateTime dat = new DateTime(2019, 12, 01);

                             DateTime Date_tek = dat.AddDays(dj-1);                      

                             string PodrazdList = Convert.ToString(worksheet.Cells[4, 1].Value2);

                         for (i=0; i<2; i++)
                         { 
                         string type_oborud = Convert.ToString(worksheet.Cells[11+ i*8, 1].Value2);
                         string oborud = Convert.ToString(worksheet.Cells[11 + i * 8, 2].Value2);    
                         string type_work =Convert.ToString(worksheet.Cells[11 + i * 8, 4+dj].Value2);    
                         double t = Convert.ToDouble(worksheet.Cells[13 + i * 8, 4 + dj].Value2);
                         double Tr = Convert.ToDouble(worksheet.Cells[15 + i * 8, 4 + dj].Value2);


                            /*     SqlConnection Con = new SqlConnection(@"Data Source=ZSUDB\SCCM;Initial Catalog=Bez2019;Integrated Security=False;User ID=Alex;Password=123");
                                 SqlCommand com;

                                 string delstr;
                                   Con.Open();
                                 delstr = @"delete from UMRK_PPR WHERE dbo.UMRK_PPR.Per >= '" + DataBegin + "' and dbo.UMRK_PPR.Per <= '" + Data_end + "' and dbo.UMRK_PPR.Oborud = '" + oborud + "' and dbo.UMRK_PPR.Podrazd = '" + PodrazdList + "'";
                                  com = new SqlCommand(delstr, Con);



                                 string str;

                             //  Console.WriteLine("DELSTR " + delstr.ToString());
                                 if (type_work != null && t != 0 && Tr!= 0)
                                 { 
                               str = @"insert into UMRK_PPR (Per, Oborud, Type_oborud, Podrazd, Type_work, T, TR) values ('" +Date_tek + "','" + oborud + "','" + type_oborud + "','" + PodrazdList + "','" + type_work + "','" + t + "','" + Tr + "') ";
                                    // Console.WriteLine("DELSTR " + str.ToString());
                                     com = new SqlCommand(str, Con);
                                     com.ExecuteNonQuery();
                                  }
                         Con.Close();
        } }
                  
                }
            }*/


          //  dt.AcceptChanges();
            workbook.Close(true, Missing.Value, Missing.Value);
            excelApp.Quit();

                // ManualDataTable = dt;

              


             //   MessageBox.Show("Данные сохранены");
               

           
            IsStarted = false;
          

        }
        private void DoOneWork(int i)
        {
            //Thread.Sleep(100);
           
        }
        public ICommand SaveInDB
        {

            get
            {
                if (_saveInDB == null)
                {
                    _saveInDB = new SaveCommand(this);
                }
                return _saveInDB;
            }

        }
        class SaveCommand : MyCommand
        {


            public SaveCommand(PlanFactVM cvm) : base(cvm)
            {
            }

            public DataTable ManualDataTable { get; private set; }

            //public ObservableCollection<Car> Phones { get; private set; }
            //public ObservableCollection<Car> Cars { get; private set; }

            public override bool CanExecute(object parameter)
            {
                return true;
            }







            public override void Execute(object parameter)
            {

                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook;
                Excel.Worksheet worksheet;
                Excel.Range range;
                workbook = excelApp.Workbooks.Open(SelectedPath);
                worksheet = (Excel.Worksheet)workbook.Sheets["Test Sheet"];

                int column = 0;
                int row = 0;

                range = worksheet.UsedRange;
                DataTable dt = new DataTable();

                dt.Columns.Add("ID");
                dt.Columns.Add("Name");
                dt.Columns.Add("TO");

                for (row = 2; row <= range.Rows.Count; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (column = 1; column <= range.Columns.Count; column++)
                    {
                        dr[column - 1] = (range.Cells[row, column] as Excel.Range).Value2.ToString();
                    }
                    dt.Rows.Add(dr);



                }
                dt.AcceptChanges();
                workbook.Close(true, Missing.Value, Missing.Value);
                excelApp.Quit();


                ManualDataTable = dt;//.DefaultView;
                MessageBox.Show(ManualDataTable.Columns.Count.ToString());
            }

        }




        private DelegateCommand<object> _editCommand;


        public DelegateCommand<object> EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new DelegateCommand<object>(Edit, CanEdit));
            }
        }



        private bool CanEdit(object obj)
        {
            return obj != null;
        }

        private void Edit(object obj)
        {
           /* var type = obj as string ?? obj.GetType().Name;
            DialogWindow window;
            switch (type)
            {
                default: return;
                case "Oboruds":
                    var dialogVm = new DialogVm(obj as PlanPPRVM);
                    window = new Dialog(dialogVm);
                    window.ShowDialog();
                    if (dialogVm.Result && dialogVm.IsCreate)
                    {
                        // _oboruds.Add(dialogVm.GetOborud());
                        ol.Add(dialogVm.GetOborud());

                    }
                    break;



            }*/
        }










        #endregion


       


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
    }


    static class DateTimeExtensions
    {
        public static string ToMonthName(this DateTime SelectedDataPrint)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(SelectedDataPrint.Month);
        }

        public static string ToShortMonthName(this DateTime SelectedDataPrint)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(SelectedDataPrint.Month);
        }
    }





}
