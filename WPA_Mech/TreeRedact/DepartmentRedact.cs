using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech//.TreeRedact
{
    public class DepartmentRedact : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();

        private readonly UMRK_SM _oborud;

        private readonly UMRK_PPR _ppr;


        private List<CourseRedact> courseRedact;
        private List<TreeRedactVM> treeRedact;

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

        public DepartmentRedact(string depname)
        {
            DepartmentNameRedact = depname;





        }

        bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
                if (_isSelected == true)
                {
                    MessageBox.Show(this.NameVid);

                }

                NameVid = NameVid;
                Podrazd = DepartmentNameRedact.ToString();

                SetName();
            }
        }


        public void SetName()
        {
            if (_isSelected == true)
            {
                MessageBox.Show("SetName " + this._podrazd);
               
            }

        }




        /* private bool _isChecked;
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
                 Podrazd = DepartmentNameRedact.ToString();
             }
         }*/

        public DepartmentRedact(UMRK_SM oborud)
        {
            //выдал наряд 
           _oborud = oborud;
            //  _podrazd = oborud.Nar_vidal;//.Oborud;//.Podrazd;

            _podrazd = oborud.Nar_vid;//.Oborud;//.Podrazd;

            Data = oborud.Date_nar;

            DepartmentNameRedact = _podrazd;
            var cour = _db.UMRK_REM.Where(p => p.NR == Data)/*.Where(x => x.Nar_vidal == _podrazd)*/.GroupBy(p => p.Oborud).Select(grp => grp.FirstOrDefault()).ToList().Select(r => new CourseRedact(r));

            CoursesRedact = new List<CourseRedact>(cour);

            NameVid = _podrazd;
            

        }


        public void SetTime()
        {
            var time = _db.UMRK_PPR.Where(t => t.Per == Data & t.Oborud == _podrazd).ToString();
        }
        public DepartmentRedact (UMRK_PPR ppr)
        {
            _ppr = ppr;
            var time = _db.UMRK_PPR.Where(t => t.Per == Data & t.Oborud == _podrazd).ToString();
        }

        private string _nameVid;
        public string NameVid
        {
            get
            {
                return _nameVid;
            }
            set
            {
                _nameVid = value;
                RaisePropertyChanged("NameVid");
            }
        }
        public List<TreeRedactVM> TreeRedact
        {
            get
            {
                return treeRedact;
            }
            set
            {
                treeRedact = value;
                RaisePropertyChanged("TreeRedact");
            }
        }


        public List<CourseRedact> CoursesRedact
        {
            get
            {
                return courseRedact;
            }
            set
            {
                courseRedact = value;
                RaisePropertyChanged("CoursesRedact");
            }
        }

        public string DepartmentNameRedact
        {
            get;
            set;
        }

        public DateTime? Data
        {
            get; set;
        }
    }
}

