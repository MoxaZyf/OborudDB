//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPA_Mech.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UMRK_PPR
    {
        public int Id_PPR { get; set; }
        public System.DateTime Per { get; set; }
        public string Oborud { get; set; }
        public string Type_oborud { get; set; }
        public string Podrazd { get; set; }
        public string Type_work { get; set; }
        public Nullable<float> T { get; set; }
        public Nullable<float> TR { get; set; }
        public string Comment { get; set; }
    }
}
