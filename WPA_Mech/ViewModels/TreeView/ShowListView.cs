using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech.ViewModels//.TreeView
{
    public class ShowListView : ViewModelBase
    {
        private readonly Bez1Entities db = new Bez1Entities();


        public RelayCommand UPDCommand { get; set; }

        public UMRK_PPR order;

        private UMRK_PPR _order;
        public ShowListView()
        {
            UPDCommand = new RelayCommand(Update);
        }



        private DateTime _per;
        public DateTime Per
        {
            get { return _per; }
            set
            {
                _per = value;
                RaisePropertyChanged("Per");
            }
        }

        private string _oborud;
        public string Oborud
        {
            get { return _oborud; }
            set
            {
                _oborud = value;
                RaisePropertyChanged("Oborud");
            }
        }

        private string _type_oborud;
        public string Tyoe_oborud
        {
            get { return _type_oborud; }
            set
            {
                _type_oborud = value;
                RaisePropertyChanged("Tyoe_oborud");
            }
        }

        private string _type_work;
        public string Type_work
        {
            get { return _type_work; }
            set
            {
                _type_work = value;
                RaisePropertyChanged("Type_work");
            }
        }

        private float _t;
        public float T
        {
            get { return _t; }
            set
            {
                _t = value;
                RaisePropertyChanged("T");
            }
        }

        private float _tr;
        public float TR
        {
            get { return _tr; }
            set
            {
                _tr = value;
                RaisePropertyChanged("TR");
            }
        }

        private string _podrazd;
        public string Podrazd
        {
            get { return _podrazd; }
            set
            {
                _podrazd = value;
                RaisePropertyChanged("Podrazd");
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                RaisePropertyChanged("Comment");
                Update();
            }
        }


        private int _Id_PPR;
        public int Id_PPR
        {
            get => _Id_PPR;
            set
            {
                _Id_PPR = value;
                RaisePropertyChanged(nameof(Id_PPR));


            }
        }

        private ShowListView _selectedList;
        public ShowListView SelectedList
        {
            get => _selectedList;
            set
            {
                _selectedList = value;
                RaisePropertyChanged(nameof(SelectedList));
            }
        }

        public ObservableCollection<int> DataContext { get; internal set; }




        public ShowListView(UMRK_PPR order)
        {
            _order = order;
            _per = order.Per;
            _comment = order.Comment;
            Id_PPR = order.Id_PPR;
        }

        public void Update()
        {

            _order = new UMRK_PPR();
            var query = db.UMRK_PPR.Where(x => x.Id_PPR == Id_PPR).FirstOrDefault(n => n.Comment == Comment);
            db.SaveChanges();
            //   _order = order;
            _order.Id_PPR = _Id_PPR;
            /*  _order.Per = _per;
              _order.Oborud = _oborud;
              _order.Type_oborud = _type_oborud;
              _order.Podrazd = _podrazd;
              _order.Type_work = _type_work;
              _order.T = _t;
              _order.TR = _tr;*/
            _order.Comment = _comment;
            //   db.SaveChanges();




            //   MessageBox.Show("Данные изменены ShowListView ");


        }
    }
}
