
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Utils;
using WPA_Mech.Models;

namespace WPA_Mech.ViewModels
{
   public class PlanPPRVM : ViewModelBase
    {
        private UMRK_PPR  _UMRK_PPRs;

        public PlanPPRVM()
        {

        }

        public UMRK_PPR UMRK_PPR;
        private readonly UMRK_PPR _UMRK_PPR;

        private DateTime _per;
        public DateTime Per
        {
            get => _per;
            set
            {
                _per = value;
                RaisePropertyChanged(nameof(Per));
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

        private string _type_oborud;
        public string Type_oborud
        {
            get => _type_oborud;
            set
            {
                _type_oborud = value;
                RaisePropertyChanged(nameof(Type_oborud));
            }
        }


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
        private string _type_work;
        public string Type_work
        {
            get => _type_work;
            set
            {
                _type_work = value;
                RaisePropertyChanged(nameof(Type_work));
            }
        }


        private float? _t;
        public float? T
        {
            get => _t;
            set
            {
                _t = value;
                RaisePropertyChanged(nameof(T));

            }
        }

        private float? _tr;
        public float? TR
        {
            get => _tr;
            set
            {
                _tr = value;
                RaisePropertyChanged(nameof(TR));
            }
        }


        public PlanPPRVM(UMRK_PPR umrk_ppr)
        {
            _UMRK_PPR = umrk_ppr;
            _per = umrk_ppr.Per;
            _oborud = umrk_ppr.Oborud;
            _type_oborud = umrk_ppr.Type_oborud;
            _podrazd = umrk_ppr.Podrazd;
            _type_work = umrk_ppr.Type_work;
            _t = umrk_ppr.T;
            _tr = umrk_ppr.TR;
        }


        public void UpdateModel()
        {
            if(_UMRK_PPR !=null)
            {
                _UMRK_PPR.Per = _per;
                _UMRK_PPR.Oborud = _oborud;
                _UMRK_PPR.Type_oborud = _type_oborud;
                _UMRK_PPR.Podrazd = _podrazd;
                _UMRK_PPR.Type_work = _type_work;
                _UMRK_PPR.T = _t;
                _UMRK_PPR.TR = _tr;
            }
        }


    }
}
