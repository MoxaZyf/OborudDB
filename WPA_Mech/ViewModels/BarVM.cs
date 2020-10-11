using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;
using WPA_Mech.Utils;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace WPA_Mech.ViewModels
{
    public class BarVM : INotifyPropertyChanged
    {
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

    public ICommand RunCommand { get; private set; }
    public ICommand CancelCommand { get; private set; }

    public BarVM()
    {
        RunCommand = new RelayCommand(Run);
        CancelCommand = new RelayCommand(Cancel);
    }

    private void Run()
    {
        if (IsStarted) return;
        _runCts = new CancellationTokenSource();
        Task.Run(() => DoWork(Start, Stop));
    }

    private void Cancel()
    {
        if (_runCts != null) _runCts.Cancel();
    }

    private void DoWork(int start, int stop)
    {
            var resourceName = "WPA_Mech.AvtoHR.xlsx";

            var asm = Assembly.GetExecutingAssembly();


            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> embeddedResources = new List<string>(assembly.GetManifestResourceNames());



          /*  using (Stream stream = asm.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
            }
            */

            Assembly asml = Assembly.GetExecutingAssembly();
            string file = string.Format("{0}.AvtoHR.xlsx", asm.GetName().Name);
            Stream fileStream = asm.GetManifestResourceStream(file);
            SaveStreamToFile(Path.GetTempPath() + "AvtoHR.xlsx", fileStream);  //<--here is where to save to disk
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(Path.GetTempPath() + "AvtoHR.xlsx");
           

            if (xlWorkBook == null)
            {
                MessageBox.Show("Error: Unable to open Excel file.");
                return;
            }
            IsStarted = true;
            Current = start;
            var length = fileStream.Length;

            var pos = 0;
            var buffer = new byte[1024];
      //      BinaryReader reader = new BinaryReader(fileStream);
          /*  while (pos < length)
            {
                int read = reader.Read(buffer, 0, buffer.Length);
                pos = pos + read;
                DoOneWork(Convert.ToInt32(pos / length) * 100);
                Current++;
                
            }*/

           
        for (int i = start; i < 100; i++)
        {
                

                if (_runCts != null && _runCts.IsCancellationRequested   )  break;
                
              //  DoOneWork(i/10);
            Current++;

                List<MyTable> resultQ;
                List<MyTable> results;
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
                            //  PlanrezOborud = dt.AsEnumerable().Distinct().Select(se => se.Field<string>("oborud")).ToList();

                            results = resultQ.GroupBy(x => x.oborud).Select(x => x.First()).ToList();
                        }


                    }
                }
            //    Excel.Application xlApp = new Excel.Application();
                // var planCount = PlanResults.Count() == 0 ? results.Count() : results.Count(); // Convert.ToDecimal(se.Field<decimal>("T_R") == 0 ? 0 : se.Field<decimal>("T_R"))

                //вывод плана 
                object[,] dataO = new object[results.Count(), 1];

                Excel.Range NamePodrazd = xlApp.get_Range("B4:B4");



                Excel.Range NamePodrazd2 = xlApp.get_Range("O6:O6");




                Excel.Range DataMonth = xlApp.get_Range("S6:S6");
                //  string monthName = Date.ToMonthName();//.ToString("MMM", CultureInfo.CurrentCulture);

                //     DataMonth.Value2 = monthName + " " + Date.Year + "г.";




                int o;





            }
         
            if (_runCts != null)
        {
            _runCts.Dispose();
            _runCts = null;
               
            }
        IsStarted = false;
            MessageBox.Show("End3");
            xlApp.Visible = true;
        }

        public DateTime DataBegin = new DateTime(2019, 07, 01);
        public DateTime DataEnd = new DateTime(2019, 07, 31);
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

        private void DoOneWork(int i)
    {
           
       




           // Thread.Sleep(20);
            
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
        private void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;
        public class RelayCommand : ICommand
        {
            private readonly Action _action;

            public RelayCommand(Action action)
            {
                _action = action;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _action();
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}