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
    
    public partial class Appointments
    {
        public int id_Appointmaent { get; set; }
        public int id_Doctor { get; set; }
        public int id_Patient { get; set; }
        public System.DateTime DataTime_Appointment { get; set; }
    
        public virtual Doctors Doctors { get; set; }
        public virtual Patients Patients { get; set; }
    }
}
