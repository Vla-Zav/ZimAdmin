//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZimAdmin.Entitys
{
    using System;
    using System.Collections.Generic;
    
    public partial class Conclusions
    {
        public int id_Counclusion { get; set; }
        public int id_Patient { get; set; }
        public int id_Doctor { get; set; }
        public System.DateTime DataTime_Conclusion { get; set; }
        public string Complaint { get; set; }
        public string Life_History { get; set; }
        public string Objective_Status { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
    
        public virtual Doctors Doctors { get; set; }
        public virtual Patients Patients { get; set; }
    }
}