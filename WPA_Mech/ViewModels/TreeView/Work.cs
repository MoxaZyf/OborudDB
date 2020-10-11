using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech
{
    public class Works : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();
        private string namename = string.Empty;


        private readonly TechKart _karts;
        public TechKart karts;

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
               // MessageBox.Show(NameName.ToString());
                NameName = NameName;
            }
        }





        private bool _isChecked;
        public bool IsChecked
        {
            get

            {
                return _isChecked;
            }

            set
            {
                _isChecked = value;
                RaisePropertyChanged("IsChecked");
                Work = NameName.ToString();
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
        private string _work;


        public string NameWork
        {
            get => _work;
            set
            {
                _work = value;
                RaisePropertyChanged(nameof(NameWork));
            }
        }

        public string КР { get; set; }

        public string КРу { get; set; }
        public string ТО { get; set; }
        public string Т { get; set; }
        public string ЕО { get; set; }
        public string А { get; set; }
        public string М { get; set; }
        public Works(TechKart kart)
        {
            _karts = kart;
            _id = kart.Id;
            _work = kart.Состав_работ;
            КР = kart.КР;
            КРу = kart.КРу;
            ТО = kart.ТО;
            ЕО = kart.ЕО;
            Т = kart.Т;
            А = kart.А;
            М = kart.М;

            NameName = _work;


        }

        public string NameName
        {
            get
            {
                return namename;
            }
            set
            {
                namename = value;
                RaisePropertyChanged("NameName");
            }
        }
        //  bool? IsChecked { get; set; }


    }
}
