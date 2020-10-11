using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech//.ViewModels.TreeView
{
    public class Department : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();

    private readonly UMRK_PPR _oborud;


    private List<Course> courses;

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
    public Department(string depname)
    {
        DepartmentName = depname;




        Courses = new List<Course>()
            {
                new Course("Course1"),
                new Course("Course2")
            };
    }



    public Department(UMRK_PPR oborud)
    {
        _oborud = oborud;
        _podrazd = oborud.Podrazd;

        Data = oborud.Per;

        DepartmentName = _podrazd;
        var cour = _db.UMRK_PPR.Where(p => p.Type_oborud != null && p.Per == Data/*.Year == 2019 && p.Per.Month == 01 && p.Per.Day == 17*/).Where(x => x.Podrazd == _podrazd)
            .GroupBy(p => p.Type_oborud).Select(grp => grp.FirstOrDefault()).ToList().Select(r => new Course(r));

        Courses = new List<Course>(cour);


    }

    public List<Course> Courses
    {
        get
        {
            return courses;
        }
        set
        {
            courses = value;
            RaisePropertyChanged("Courses");
        }
    }

    public string DepartmentName
    {
        get;
        set;
    }

    public DateTime Data
    {
        get; set;
    }
}
}
