using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech//.TreeRedact
{
  public  class PlanPPRGet : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();

        private string commentnameRedact = string.Empty;
        //   public NaryadList oborud;
        // private readonly NaryadList _oborud;

        public UMRK_PPR oborud;
        private readonly UMRK_PPR _oborud;
        private string _commentRedact;


        public string CommentRedact
        {
            get => _commentRedact;
            set
            {
                _commentRedact = value;
                RaisePropertyChanged(nameof(CommentRedact));
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

        private float? _timePlan;
        public float? Time_Plan
        {
            get => _timePlan;
            set
            {
                _timePlan = value;
                RaisePropertyChanged(nameof(Time_Plan));
            }
        }


        public PlanPPRGet(UMRK_PPR oborud)
        {
            _oborud = oborud;

          //_fio = _oborud.FIO;
              _timePlan = _oborud.T;

            //  _id = oborud.Id_rem;
            CommentNameRedact = _commentRedact;




        }

        public string CommentNameRedact
        {
            get
            {
                return commentnameRedact;
            }
            set
            {
                commentnameRedact = value;
                RaisePropertyChanged("CommentNameRedact");
            }
        }



    }
}
