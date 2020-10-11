using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPA_Mech.Models;
using WPA_Mech.Utils;

namespace WPA_Mech
{
    public class Comments : ViewModelBase
    {
        private readonly Bez1Entities _db = new Bez1Entities();

        private string commentname = string.Empty;
        public UMRK_PPR oborud;
        private readonly UMRK_PPR _oborud;


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

        private string _typeO;
        public string TypeO
        {
            get => _typeO;
            set
            {
                _typeO = value;
                RaisePropertyChanged(nameof(TypeO));
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

        private string _to;
        public string TO
        {
            get => _to;
            set
            {
                _to = value;
                RaisePropertyChanged(nameof(TO));
            }
        }

        private List<Works> works;
        public Comments(UMRK_PPR oborud)
        {
            _oborud = oborud;
            _comment = oborud.Type_work;
            _t = oborud.T;
            _to = oborud.Type_oborud;
            _typeO = oborud.Oborud;

            _id = oborud.Id_PPR;
            CommentName = _comment;

            var type = _db.TechKart.Where(p => p.Состав_работ != null).Where(/*x=>x.Name == _typeO &&*/ x => x.TypeOborud == _to).GroupBy(p => p.Состав_работ).Select(grp => grp.FirstOrDefault()).ToList().Select(r => new Works(r));

            if (_comment.ToString() == "КР")
                type = type.Where(tt => tt.КР == "КР");

            if (_comment.ToString() == "КРу")
                type = type.Where(tt => tt.КРу == "КРу");
            if (_comment.ToString() == "ТО")
                type = type.Where(tt => tt.ТО == "ТО");
            if (_comment.ToString() == "Т")
                type = type.Where(tt => tt.Т == "Т");
            if (_comment.ToString() == "ЕО")
                type = type.Where(tt => tt.ЕО == "ЕО");
            if (_comment.ToString() == "А")
                type = type.Where(tt => tt.А == "А");
            if (_comment.ToString() == "М")
                type = type.Where(tt => tt.М == "М");


            Works = new List<Works>(type);


        }
        public List<Works> Works
        {
            get
            {
                return works;
            }
            set
            {
                works = value;
                RaisePropertyChanged("Works");
            }
        }

        public string CommentName
        {
            get
            {
                return commentname;
            }
            set
            {
                commentname = value;
                RaisePropertyChanged("CommentName");
            }
        }



    }
}