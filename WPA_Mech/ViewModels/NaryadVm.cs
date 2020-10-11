using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech//.ViewModels
{
    public class NaryadVm : ViewModelBase//, IDataErrorInfo
    {

        public static int Errors { get; set; }

        public RelayCommand SaveCommand { get; set; }

        public NaryadList naryad;

        private readonly NaryadList _naryad;




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

        private string _Podrazd;
        public string Podrazd
        {
            get => _Podrazd;
            set
            {
                _Podrazd = value;
                RaisePropertyChanged(nameof(Podrazd));
            }
        }
        private string _oborud;
        public string Oborud
        {
            get => _oborud;
            set
            {
                _oborud = value;
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
        private int? _time_Plan;
        public int? Time_Plan
        {
            get => _time_Plan;
            set
            {
                _time_Plan = value;
                RaisePropertyChanged(nameof(Time_Plan));

            }

        }

        private int? _time_Fakt;
        public int? Time_Fakt
        {
            get => _time_Fakt;
            set
            {
                _time_Fakt = value;
                RaisePropertyChanged(nameof(Time_Fakt));
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


        public NaryadVm(NaryadList naryad)
        {
            _naryad = naryad;
            _data = naryad.Data;
            _nar_vidal = naryad.Nar_vidal;
            _smena = naryad.Smena;
            _smena_long = naryad.Smena_long;
            _oborud = naryad.Oborud;
            _fio = naryad.FIO;
            _post = naryad.Post;
            _Podrazd = naryad.Podrazd;

            _TO_Type = naryad.TO_Type;
            _work_list = naryad.Work_list;
            _time_Plan = naryad.Time_Plan;
            _time_Fakt = naryad.Time_Fakt;
            _statusNar = naryad.StatusNar;
            _comment = naryad.Comment;
            _master = naryad.Master;

        }


        public NaryadVm()
        {
            SaveCommand = new RelayCommand(Save, CanSave);
        }

        public void Save(object parameter)
        {
            if (_naryad != null)
            {

                _naryad.Data = _data;
                _naryad.Nar_vidal = _nar_vidal;
                _naryad.Smena = _smena;
                _naryad.Smena_long = _smena_long;
                _naryad.FIO = _fio;
                _naryad.Post = _post;
                _naryad.Podrazd = _Podrazd;
                _naryad.Oborud = _oborud;
                _naryad.TO_Type = _TO_Type;
                _naryad.Work_list = _work_list;
                _naryad.Time_Plan = _time_Plan;
                _naryad.Time_Fakt = _time_Fakt;
                _naryad.Data = _data;
                _naryad.StatusNar = _statusNar;
                _naryad.Comment = _comment;
                _naryad.Master = _master;




                MessageBox.Show("Данные изменены VM");
            }
        }

        public bool CanSave(object parameter)
        {
            if (Errors == 0)
                return true;
            else
                return false;
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

        public void UpdateModel()
        {

            if (_naryad != null)
            {

                _naryad.Data = _data;
                _naryad.Nar_vidal = _nar_vidal;
                _naryad.Smena = _smena;
                _naryad.Smena_long = _smena_long;
                _naryad.Oborud = _oborud;
                _naryad.FIO = _fio;
                _naryad.Post = _post;
                _naryad.Podrazd = _Podrazd;

                _naryad.TO_Type = _TO_Type;
                _naryad.Work_list = _work_list;
                _naryad.Time_Plan = _time_Plan;
                _naryad.Time_Fakt = _time_Fakt;
                _naryad.StatusNar = _statusNar;
                _naryad.Comment = _comment;
                _naryad.Master = _master;

                MessageBox.Show("Данные изменены ");
            }

        }


        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }







    }
}

