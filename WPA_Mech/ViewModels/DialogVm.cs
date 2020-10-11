using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech.ViewModels
{
    public class DialogVm : DialogVmBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();
        private PlanPPRVM planPPR;
   

        // private readonly OborudVm oborudVm = new OborudVm();


        public override string WindowTitle
        {
            get
            {
                return IsCreate ? "Добавление оборудования" : "Изменить";
            }
        }

      

        private DateTime _per;
        public DateTime Per
        {
            get { return _per; }
            set
            {
                _per = value;
                RaisePropertyChanged();
                ApplyCommand.RaiseCanExecuteChanged();
            }
        }

        private string _oborud;
        public string Oborud
        {
            get
            { return _oborud; }
            set
            {
                _oborud = value;
                RaisePropertyChanged();
                ApplyCommand.RaiseCanExecuteChanged();
            }
        }

        private string _type_oborud;
        public string Type_oborud
        {
            get
            { return _type_oborud; }
            set
            {
                _type_oborud = value;
                RaisePropertyChanged();
                ApplyCommand.RaiseCanExecuteChanged();
            }
        }

        private string _podrazd;
        public string Podrazd
        {
            get
            { return _podrazd; }
            set
            {
                _podrazd = value;
                RaisePropertyChanged();
                ApplyCommand.RaiseCanExecuteChanged();
            }
        }

        private string _type_work;
        public string Type_work
        {
            get
            { return _type_work; }
            set
            {
                _type_work = value;
                RaisePropertyChanged();
                ApplyCommand.RaiseCanExecuteChanged();
            }
        }


        private float? _t;
        public float? T
        {
            get
            { return _t; }
            set
            {
                _t = value;
                RaisePropertyChanged();
                ApplyCommand.RaiseCanExecuteChanged();

            }
        }

        private float? _tr;
        public float? TR
        {
            get
            { return _tr; }
            set
            {
                _tr = value;
                RaisePropertyChanged(nameof(TR));
            }
        }


        private ObservableCollection<PlanPPRVM> collection;


        public ObservableCollection<PlanPPRVM> Collection

        {
            get { return collection; }
            set
            {
                collection = value;
                RaisePropertyChanged("Collection");
            }
        }
        public DialogVm(PlanPPRVM planPPRs = null)
        {
            IsCreate = planPPRs == null;
            if (planPPRs != null)
            {
                planPPR = planPPRs;
                _per = planPPR.Per;
                _oborud = planPPRs.Oborud;
                _type_oborud = planPPR.Type_oborud;
                _podrazd = planPPRs.Podrazd;
                _type_work = planPPR.Type_work;
                _t = planPPR.T;
                _tr = planPPR.TR;
            }

            var viewmodels = _db.UMRK_PPR.ToList().Select(model => new PlanPPRVM(model));
            Collection = new ObservableCollection<PlanPPRVM>(viewmodels);
        }


        public PlanPPRVM GetOborud()
        {



            Bez1Entities db;
            db = new Bez1Entities();
            Collection.Add(planPPR);

            db.UMRK_PPR.Add(new UMRK_PPR
            {
                Per = Per,
                Oborud = Oborud,
                Type_oborud = Type_oborud,
                Podrazd = Podrazd,
                Type_work=Type_work,
                T=T,
                TR=TR

            });
            db.SaveChanges();
            return planPPR;



        }

        protected override void Apply()
        {
            if (!IsCreate)
            {
                planPPR.Per = Per;
                planPPR.Oborud = Oborud;
                planPPR.Type_oborud = Type_oborud;
                planPPR.Podrazd = Podrazd;
                planPPR.Type_work = Type_work;
                planPPR.T = T;
                planPPR.TR = TR;
              

            }
            base.Apply();
        }

       /* protected override bool CanApply()
        {
            return !string.IsNullOrWhiteSpace(NameOborud)
                && !string.IsNullOrWhiteSpace(Podrazd) && !string.IsNullOrWhiteSpace(Uchastok);
        }
        */

    }
}