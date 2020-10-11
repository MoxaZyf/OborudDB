using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPA_Mech.Utils;
using Excel = Microsoft.Office.Interop.Excel;
//using GalaSoft.MvvmLight;
using System.Threading;

namespace WPA_Mech.ViewModels
{
    public class AvtoHrVM : ViewModelBase, IDisposable
    {


        private readonly BackgroundWorker worker = new BackgroundWorker();

        private RelayCommand _startCommand;
     /*   public ICommand StartCommand
        {
            get
            {
              //  return _startCommand ?? (_startCommand = new RelayCommand(param => Start(), param => CanStart));
            }
        }*/
     /*   set
            {
                collection = value;
                RaisePropertyChanged("Collection");
    }*/
    private int _progress;
        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
               // (ref _progress, value);
            }
        }
        public ICommand AvtoHrCommand { get; set; }

       // private readonly BackgroundWorker worker;
        private readonly ICommand instigateWorkCommand;
        private int currentProgress;

        public DateTime Date = new DateTime(2019, 07, 01);
        public AvtoHrVM()
        {
            //  AvtoHrCommand = new GalaSoft.MvvmLight.Command.RelayCommand(PlanFact, null);
           // AvtoHrCommand = new RelayCommand(DoWork,null);
           

            worker.WorkerReportsProgress = true;
            worker.DoWork += WorkerDoWork;
            worker.ProgressChanged += WorkerProgressChanged;


        }

        private void Start()
        {
            worker.RunWorkerAsync();
        }

        public bool CanStart
        {
            get
            {
                // return true only if certain condition is satisfied
                return false;
            }
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                worker.ReportProgress(i);
                Thread.Sleep(50);
            }
        }

        public ICommand InstigateWorkCommand
        {
            get { return this.instigateWorkCommand; }
        }

        public int CurrentProgress
        {
            get { return this.currentProgress; }
            private set
            {
                if (this.currentProgress != value)
                {
                    this.currentProgress = value;
                    this.RaisePropertyChanged("CurrentProgress");
                }
            }
        }



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


        public class MyTable : IComparable<MyTable>
        {
            public string oborud { get; set; }
            public DateTime date_nar { get; set; }
            public int id_rem { get; set; }
            public string type_work { get; set; }
            public double t_r { get; set; }
            public int tr { get; set; }
            public double t_regl { get; set; }
           // public int day { get; set; }
            public string oborud_type { get; set; }
            public int t { get; set; }
        


            public int CompareTo(MyTable other)
            {
                int res = oborud.CompareTo(other.oborud);
                return res != 0 ? res : date_nar.CompareTo(other.date_nar);
            }
        }

        public void DoWork(object sender, DoWorkEventArgs ee)
        {
            //var DataBegin = SelectedDataPrint.Value.Month;
            var day = (DateTime.DaysInMonth(Date.Year, SelectedDataPrint.Value.Month)); // последний день выбранного месяца
            DateTime DataBegin = new DateTime(Date.Year, SelectedDataPrint.Value.Month, 1);
            DateTime DataEnd = new DateTime(Date.Year, SelectedDataPrint.Value.Month, day);





            var resourceName = "WPA_Mech.AvtoHR.xlsx";

            var asm = Assembly.GetExecutingAssembly();


            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> embeddedResources = new List<string>(assembly.GetManifestResourceNames());



            using (Stream stream = asm.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
            }


            Assembly asml = Assembly.GetExecutingAssembly();
            string file = string.Format("{0}.AvtoHR.xlsx", asm.GetName().Name);
            Stream fileStream = asm.GetManifestResourceStream(file);
            SaveStreamToFile(Path.GetTempPath() + "AvtoHR.xlsx", fileStream);  //<--here is where to save to disk
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Path.GetTempPath() + "AvtoHR.xlsx");
            //  Excel.Worksheet worksheet;

            //  MessageBox.Show(DateBegin.ToString());


            int rowIndexBeginOrders = 3;
            int rowIndexCurrentRecord = rowIndexBeginOrders;

            int rowVidZabol = 1;
            int rowVidZabolRecord = rowVidZabol;

            int rowVidZabol8 = 0;
            int rowVidZabolRecord8 = rowVidZabol8;



            List<MyTable> resultQ;
            List<string> rezOborud;
            List<string> PlanrezOborud;
            List<MyTable> results;
            string oborudName;




            using (SqlConnection con = new SqlConnection(@"Data Source = zsudb\sccm; user id = alex; password = 123; Initial Catalog = Bez2019; "))
            {

                using (SqlCommand command = new SqlCommand("SELECT        TOP (100) PERCENT t1.Date_nar, t1.Nar_vid, t1.Id_rem, t1.Oborud, t1.Podrazd, t1.Type_Oborud, t1.ind, t1.Type_work, t1.Komment, " +
                    "t1.Komment1, t1.T_R, ISNULL(t3.t_regl, 0) AS t_regl, ISNULL(t2.t_n_regl, 0) AS t_n_regl,  dbo.UMRK_PPR.T, dbo.UMRK_PPR.TR " +
                    "FROM            (SELECT        dbo.UMRK_SM.Date_nar, dbo.UMRK_SM.Nar_vid, dbo.UMRK_REM.Id_rem, dbo.UMRK_REM.Oborud, dbo.UMRK_REM.Podrazd, " +
                    "dbo.UMRK_REM.Type_Oborud, dbo.UMRK_REM.ind, dbo.UMRK_REM.Type_work,  CASE WHEN SUM(isnull(CAST(dbo.UMRK_REM.TR AS float) + 2, 0)) = 0 THEN " +
                    "SUM(isnull(CAST(dbo.UMRK_REM.TD AS float) + 2, 0)) * 24 ELSE SUM(isnull(CAST(dbo.UMRK_REM.TR AS float) + 2, 0)) * 24 END AS T_R, " +
                    " CAST(dbo.UMRK_REM.Komment AS nvarchar(250)) AS Komment, CAST(dbo.UMRK_REM.Komment1 AS nvarchar(250)) AS Komment1  FROM            dbo.UMRK_SM INNER JOIN " +
                    " dbo.UMRK_REM ON dbo.UMRK_SM.Id_sm = dbo.UMRK_REM.sm  " +// WHERE        (dbo.UMRK_REM.Oborud = 'Печь регенерации углерода \"KEMIX\" {Поз. 114-1}') " +
                    " GROUP BY dbo.UMRK_REM.Oborud, dbo.UMRK_REM.Podrazd, dbo.UMRK_REM.Type_Oborud, dbo.UMRK_REM.ind, dbo.UMRK_SM.Date_nar, dbo.UMRK_SM.Nar_vid, " +
                    " dbo.UMRK_REM.Id_rem, dbo.UMRK_REM.Type_work, CAST(dbo.UMRK_REM.Komment AS nvarchar(250)), CAST(dbo.UMRK_REM.Komment1 AS nvarchar(250))) AS t1 INNER JOIN " +
                    " dbo.UMRK_PPR ON t1.Oborud = dbo.UMRK_PPR.Oborud AND t1.Date_nar = dbo.UMRK_PPR.Per LEFT OUTER JOIN (SELECT        Id_rem, 24 * SUM(ISNULL(CAST(time_op AS float), 0)) AS t_n_regl " +
                    " FROM            dbo.UMRK_OP    WHERE(Regl = 1) AND(Operation LIKE '%Итого%') GROUP BY Id_rem) AS t2 ON t1.Id_rem = t2.Id_rem LEFT OUTER JOIN (SELECT        Id_rem, 24 * SUM(ISNULL(CAST(time_op AS float), 0)) AS t_regl " +
                    " FROM            dbo.UMRK_OP AS UMRK_OP_1  WHERE(Regl = 0) AND(Operation LIKE '%Итого%') GROUP BY Id_rem) AS t3 ON t1.Id_rem = t3.Id_rem " +
                    "WHERE        (t1.Date_nar  >='" + DataBegin + "') AND  (t1.Date_nar <= '" + DataEnd + "')  ORDER BY t1.Date_nar, t1.Oborud", con))
                //>='"+ DataBegin+"') AND(dbo.UMRK_SM.Date_nar <= '"+DataEnd+"') "

                {
                    DataTable dt = new DataTable();
                    con.Open();
                    // SqlParameter nameParam = new SqlParameter("@DataBegin", DataBegin, "@DataEnd" , DataEnd);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                        //p.Date_nar == null ? "null" : p.Date_nar.ToString()
                        resultQ = dt.AsEnumerable().Select(se => new MyTable()
                        {
                            id_rem = se.Field<int>("id_rem"),
                            oborud = se.Field<string>("Oborud"),
                            oborud_type = se.Field<string>("Type_Oborud"),
                            type_work = se.Field<string>("type_work"),
                            date_nar = se.Field<DateTime>("date_nar"),
                            t_regl = Convert.ToDouble(se.Field<double>("t_regl") == 0 ? 0 : se.Field<double>("t_regl")),
                            tr = Convert.ToInt32(se.Field<Single>("TR") == 0 ? 0 : se.Field<Single>("TR")),
                            // tr = se.Field<int>("TR"),
                            t_r = Convert.ToDouble(se.Field<double>("T_R") == 0 ? 0 : se.Field<double>("T_R")),
                            t = Convert.ToInt32(se.Field<Single>("T") == 0 ? 0 : se.Field<Single>("T")),

                        }).ToList();
                        PlanrezOborud = dt.AsEnumerable().Distinct().Select(se => se.Field<string>("oborud")).ToList();

                        results = resultQ.GroupBy(x => x.oborud).Select(x => x.First()).ToList();
                    }


                }
            }




            // var planCount = PlanResults.Count() == 0 ? results.Count() : results.Count(); // Convert.ToDecimal(se.Field<decimal>("T_R") == 0 ? 0 : se.Field<decimal>("T_R"))

            //вывод плана 
            object[,] dataO = new object[results.Count(), 1];

            Excel.Range NamePodrazd = xlApp.get_Range("B4:B4");

            NamePodrazd.Value2 = Podrazd;

            Excel.Range NamePodrazd2 = xlApp.get_Range("O6:O6");

            NamePodrazd2.Value2 = Podrazd;


            Excel.Range DataMonth = xlApp.get_Range("S6:S6");
            string monthName = Date.ToMonthName();//.ToString("MMM", CultureInfo.CurrentCulture);

            //     DataMonth.Value2 = monthName + " " + Date.Year + "г.";




            int o;

            {
                for (o = 0; o < resultQ.Count(); o++)
                {
                    // dataO[o, 0] = resultQ[o];


                    //   Console.WriteLine("[INFO] Syncronisated PG->SQLite rows: {0}", results[i].oborud);

                    Excel.Range oborudname = (Excel.Range)xlApp.Cells[2 + o, 1];
                    Excel.Range oborudname2 = (Excel.Range)xlApp.Cells[2 + o, 1];


                    Excel.Range rangeOborud = xlApp.get_Range(oborudname, oborudname2);
                    //  xlApp.get_Range(oborudname, oborudname2).Interior.Color = Excel.XlRgbColor.rgbRed;
                    rangeOborud.Value2 = resultQ[o].date_nar;



                    Excel.Range oborudtype = (Excel.Range)xlApp.Cells[2 + o, 2];
                    Excel.Range oborudtype2 = (Excel.Range)xlApp.Cells[2 + o, 2];
                    Excel.Range rangeOborudtype = xlApp.get_Range(oborudtype, oborudtype2);

                    rangeOborudtype.Value2 = resultQ[o].id_rem;



                    Excel.Range oborud = (Excel.Range)xlApp.Cells[2 + o, 3];
                    Excel.Range oborud2 = (Excel.Range)xlApp.Cells[2 + o, 3];
                    Excel.Range rangeOborud2 = xlApp.get_Range(oborud, oborud2);

                    rangeOborud2.Value2 = resultQ[o].oborud;



                    Excel.Range oborudty = (Excel.Range)xlApp.Cells[2 + o, 4];
                    Excel.Range oborudty2 = (Excel.Range)xlApp.Cells[2 + o, 4];
                    Excel.Range rangeOborudty2 = xlApp.get_Range(oborudty, oborudty2);

                    rangeOborudty2.Value2 = resultQ[o].type_work;


                    Excel.Range oborudtyW = (Excel.Range)xlApp.Cells[2 + o, 6];
                    Excel.Range oborudtyW2 = (Excel.Range)xlApp.Cells[2 + o, 6];
                    Excel.Range rangeOborudtyW2 = xlApp.get_Range(oborudtyW, oborudtyW2);

                    rangeOborudtyW2.Value2 = resultQ[o].t_r;

                    Excel.Range t = (Excel.Range)xlApp.Cells[2 + o, 5];
                    Excel.Range t2 = (Excel.Range)xlApp.Cells[2 + o, 5];
                    Excel.Range ranget = xlApp.get_Range(t, t2);

                    ranget.Value2 = resultQ[o].t;


                    Excel.Range tregl = (Excel.Range)xlApp.Cells[2 + o, 8];
                    Excel.Range tregl2 = (Excel.Range)xlApp.Cells[2 + o, 8];
                    Excel.Range rangetregl = xlApp.get_Range(tregl, tregl2);

                    rangetregl.Value2 = resultQ[o].t_regl;

                    Excel.Range tr = (Excel.Range)xlApp.Cells[2 + o, 7];
                    Excel.Range tr2 = (Excel.Range)xlApp.Cells[2 + o, 7];
                    Excel.Range rangetr = xlApp.get_Range(tr, tr2);

                    rangetr.Value2 = resultQ[o].tr;

                    Excel.Range tnregl = (Excel.Range)xlApp.Cells[2 + o, 9];
                    Excel.Range tnregl2 = (Excel.Range)xlApp.Cells[2 + o, 9];
                    Excel.Range rangetnregl = xlApp.get_Range(tnregl, tnregl2);

                    rangetnregl.Value2 = rangetr.Value2 - rangetregl.Value2;
                    if (rangetnregl.Value2 <= 0)
                    {
                        xlApp.get_Range(tnregl, tnregl2).Interior.Color = Excel.XlRgbColor.rgbDarkRed;
                    }

                    if (rangetnregl.Value2 >= 0)
                    {
                        xlApp.get_Range(tnregl, tnregl2).Interior.Color = Excel.XlRgbColor.rgbLightSeaGreen;
                    }


                }
            }









            xlApp.Visible = true;

            if (xlWorkBook == null)
            {
                MessageBox.Show("Error: Unable to open Excel file.");
                return;
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.CurrentProgress = e.ProgressPercentage;
        }

        public void PlanFact(object sender, DoWorkEventArgs ee)
        {
            
            //var DataBegin = SelectedDataPrint.Value.Month;
            var day = (DateTime.DaysInMonth(Date.Year, SelectedDataPrint.Value.Month)); // последний день выбранного месяца
            DateTime DataBegin = new DateTime(Date.Year, SelectedDataPrint.Value.Month, 1);
            DateTime DataEnd = new DateTime(Date.Year, SelectedDataPrint.Value.Month, day);





            var resourceName = "WPA_Mech.AvtoHR.xlsx";

            var asm = Assembly.GetExecutingAssembly();


            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> embeddedResources = new List<string>(assembly.GetManifestResourceNames());



            using (Stream stream = asm.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
            }


            Assembly asml = Assembly.GetExecutingAssembly();
            string file = string.Format("{0}.AvtoHR.xlsx", asm.GetName().Name);
            Stream fileStream = asm.GetManifestResourceStream(file);
            SaveStreamToFile(Path.GetTempPath() + "AvtoHR.xlsx", fileStream);  //<--here is where to save to disk
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Path.GetTempPath() + "AvtoHR.xlsx");
            //  Excel.Worksheet worksheet;

            //  MessageBox.Show(DateBegin.ToString());


            int rowIndexBeginOrders = 3;
            int rowIndexCurrentRecord = rowIndexBeginOrders;

            int rowVidZabol = 1;
            int rowVidZabolRecord = rowVidZabol;

            int rowVidZabol8 = 0;
            int rowVidZabolRecord8 = rowVidZabol8;



           List<MyTable> resultQ;
            List<string> rezOborud;
            List<string> PlanrezOborud;
            List<MyTable> results;
            string oborudName;
        



            using (SqlConnection con = new SqlConnection(@"Data Source = zsudb\sccm; user id = alex; password = 123; Initial Catalog = Bez2019; "))
            {

                using (SqlCommand command = new SqlCommand("SELECT        TOP (100) PERCENT t1.Date_nar, t1.Nar_vid, t1.Id_rem, t1.Oborud, t1.Podrazd, t1.Type_Oborud, t1.ind, t1.Type_work, t1.Komment, " +
                    "t1.Komment1, t1.T_R, ISNULL(t3.t_regl, 0) AS t_regl, ISNULL(t2.t_n_regl, 0) AS t_n_regl,  dbo.UMRK_PPR.T, dbo.UMRK_PPR.TR " +
                    "FROM            (SELECT        dbo.UMRK_SM.Date_nar, dbo.UMRK_SM.Nar_vid, dbo.UMRK_REM.Id_rem, dbo.UMRK_REM.Oborud, dbo.UMRK_REM.Podrazd, " +
                    "dbo.UMRK_REM.Type_Oborud, dbo.UMRK_REM.ind, dbo.UMRK_REM.Type_work,  CASE WHEN SUM(isnull(CAST(dbo.UMRK_REM.TR AS float) + 2, 0)) = 0 THEN " +
                    "SUM(isnull(CAST(dbo.UMRK_REM.TD AS float) + 2, 0)) * 24 ELSE SUM(isnull(CAST(dbo.UMRK_REM.TR AS float) + 2, 0)) * 24 END AS T_R, " +
                    " CAST(dbo.UMRK_REM.Komment AS nvarchar(250)) AS Komment, CAST(dbo.UMRK_REM.Komment1 AS nvarchar(250)) AS Komment1  FROM            dbo.UMRK_SM INNER JOIN " +
                    " dbo.UMRK_REM ON dbo.UMRK_SM.Id_sm = dbo.UMRK_REM.sm  "+// WHERE        (dbo.UMRK_REM.Oborud = 'Печь регенерации углерода \"KEMIX\" {Поз. 114-1}') " +
                    " GROUP BY dbo.UMRK_REM.Oborud, dbo.UMRK_REM.Podrazd, dbo.UMRK_REM.Type_Oborud, dbo.UMRK_REM.ind, dbo.UMRK_SM.Date_nar, dbo.UMRK_SM.Nar_vid, " +
                    " dbo.UMRK_REM.Id_rem, dbo.UMRK_REM.Type_work, CAST(dbo.UMRK_REM.Komment AS nvarchar(250)), CAST(dbo.UMRK_REM.Komment1 AS nvarchar(250))) AS t1 INNER JOIN " +
                    " dbo.UMRK_PPR ON t1.Oborud = dbo.UMRK_PPR.Oborud AND t1.Date_nar = dbo.UMRK_PPR.Per LEFT OUTER JOIN (SELECT        Id_rem, 24 * SUM(ISNULL(CAST(time_op AS float), 0)) AS t_n_regl " +
                    " FROM            dbo.UMRK_OP    WHERE(Regl = 1) AND(Operation LIKE '%Итого%') GROUP BY Id_rem) AS t2 ON t1.Id_rem = t2.Id_rem LEFT OUTER JOIN (SELECT        Id_rem, 24 * SUM(ISNULL(CAST(time_op AS float), 0)) AS t_regl " +
                    " FROM            dbo.UMRK_OP AS UMRK_OP_1  WHERE(Regl = 0) AND(Operation LIKE '%Итого%') GROUP BY Id_rem) AS t3 ON t1.Id_rem = t3.Id_rem " +
                    "WHERE        (t1.Date_nar  >='" + DataBegin + "') AND  (t1.Date_nar <= '" + DataEnd + "')  ORDER BY t1.Date_nar, t1.Oborud", con))
                //>='"+ DataBegin+"') AND(dbo.UMRK_SM.Date_nar <= '"+DataEnd+"') "

                {
                    DataTable dt = new DataTable();
                    con.Open();
                    // SqlParameter nameParam = new SqlParameter("@DataBegin", DataBegin, "@DataEnd" , DataEnd);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                        //p.Date_nar == null ? "null" : p.Date_nar.ToString()
                        resultQ = dt.AsEnumerable().Select(se => new MyTable()
                        {
                            id_rem = se.Field<int>("id_rem"),
                            oborud = se.Field<string>("Oborud"),
                            oborud_type = se.Field<string>("Type_Oborud"),
                            type_work = se.Field<string>("type_work"),
                            date_nar = se.Field<DateTime>("date_nar"),
                            t_regl = Convert.ToDouble(se.Field<double>("t_regl") == 0 ? 0 : se.Field<double>("t_regl")),
                            tr = Convert.ToInt32(se.Field<Single>("TR") == 0 ? 0 : se.Field<Single>("TR")),
                            // tr = se.Field<int>("TR"),
                            t_r = Convert.ToDouble(se.Field<double>("T_R") == 0 ? 0 : se.Field<double>("T_R")),
                            t = Convert.ToInt32(se.Field<Single>("T") == 0 ? 0 : se.Field<Single>("T")),

                        }).ToList();
                        PlanrezOborud = dt.AsEnumerable().Distinct().Select(se => se.Field<string>("oborud")).ToList();

                        results = resultQ.GroupBy(x => x.oborud).Select(x => x.First()).ToList();
                    }


                }
            }


       

            // var planCount = PlanResults.Count() == 0 ? results.Count() : results.Count(); // Convert.ToDecimal(se.Field<decimal>("T_R") == 0 ? 0 : se.Field<decimal>("T_R"))

            //вывод плана 
            object[,] dataO = new object[results.Count(), 1];

            Excel.Range NamePodrazd = xlApp.get_Range("B4:B4");

            NamePodrazd.Value2 = Podrazd;

            Excel.Range NamePodrazd2 = xlApp.get_Range("O6:O6");

            NamePodrazd2.Value2 = Podrazd;


            Excel.Range DataMonth = xlApp.get_Range("S6:S6");
            string monthName = Date.ToMonthName();//.ToString("MMM", CultureInfo.CurrentCulture);

       //     DataMonth.Value2 = monthName + " " + Date.Year + "г.";




            int o;

            {
                for (o = 0; o < resultQ.Count(); o++)
                {
                    // dataO[o, 0] = resultQ[o];


                    //   Console.WriteLine("[INFO] Syncronisated PG->SQLite rows: {0}", results[i].oborud);

                    Excel.Range oborudname = (Excel.Range)xlApp.Cells[2 + o, 1];
                    Excel.Range oborudname2 = (Excel.Range)xlApp.Cells[2 + o, 1];


                    Excel.Range rangeOborud = xlApp.get_Range(oborudname, oborudname2);
                    //  xlApp.get_Range(oborudname, oborudname2).Interior.Color = Excel.XlRgbColor.rgbRed;
                    rangeOborud.Value2 = resultQ[o].date_nar;



                    Excel.Range oborudtype = (Excel.Range)xlApp.Cells[2 + o, 2];
                    Excel.Range oborudtype2 = (Excel.Range)xlApp.Cells[2 + o, 2];
                    Excel.Range rangeOborudtype = xlApp.get_Range(oborudtype, oborudtype2);

                    rangeOborudtype.Value2 = resultQ[o].id_rem;



                    Excel.Range oborud = (Excel.Range)xlApp.Cells[2 + o, 3];
                    Excel.Range oborud2 = (Excel.Range)xlApp.Cells[2 + o, 3];
                    Excel.Range rangeOborud2 = xlApp.get_Range(oborud, oborud2);

                    rangeOborud2.Value2 = resultQ[o].oborud;



                    Excel.Range oborudty = (Excel.Range)xlApp.Cells[2 + o, 4];
                    Excel.Range oborudty2 = (Excel.Range)xlApp.Cells[2 + o, 4];
                    Excel.Range rangeOborudty2 = xlApp.get_Range(oborudty, oborudty2);

                    rangeOborudty2.Value2 = resultQ[o].type_work;


                    Excel.Range oborudtyW = (Excel.Range)xlApp.Cells[2 + o, 6];
                    Excel.Range oborudtyW2 = (Excel.Range)xlApp.Cells[2 + o, 6];
                    Excel.Range rangeOborudtyW2 = xlApp.get_Range(oborudtyW, oborudtyW2);

                    rangeOborudtyW2.Value2 = resultQ[o].t_r;

                    Excel.Range t = (Excel.Range)xlApp.Cells[2 + o, 5];
                    Excel.Range t2 = (Excel.Range)xlApp.Cells[2 + o, 5];
                    Excel.Range ranget = xlApp.get_Range(t, t2);

                    ranget.Value2 = resultQ[o].t;


                    Excel.Range tregl = (Excel.Range)xlApp.Cells[2 + o, 8];
                    Excel.Range tregl2 = (Excel.Range)xlApp.Cells[2 + o, 8];
                    Excel.Range rangetregl = xlApp.get_Range(tregl, tregl2);

                    rangetregl.Value2 = resultQ[o].t_regl;

                    Excel.Range tr = (Excel.Range)xlApp.Cells[2 + o, 7];
                    Excel.Range tr2 = (Excel.Range)xlApp.Cells[2 + o, 7];
                    Excel.Range rangetr = xlApp.get_Range(tr, tr2);

                    rangetr.Value2 = resultQ[o].tr;

                    Excel.Range tnregl = (Excel.Range)xlApp.Cells[2 + o, 9];
                    Excel.Range tnregl2 = (Excel.Range)xlApp.Cells[2 + o, 9];
                    Excel.Range rangetnregl = xlApp.get_Range(tnregl, tnregl2);

                    rangetnregl.Value2 =   rangetr.Value2-rangetregl.Value2;
                    if (rangetnregl.Value2 <= 0)
                    {
                        xlApp.get_Range(tnregl, tnregl2).Interior.Color = Excel.XlRgbColor.rgbDarkRed;
                    }

                    if (rangetnregl.Value2 >= 0)
                    {
                        xlApp.get_Range(tnregl, tnregl2).Interior.Color = Excel.XlRgbColor.rgbLightSeaGreen;
                    }


                }
            }




           




            xlApp.Visible = true;

            if (xlWorkBook == null)
            {
                MessageBox.Show("Error: Unable to open Excel file.");
                return;
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
        public void Dispose()
        {
            worker.Dispose();
            GC.SuppressFinalize(this);
        }
    }



}
