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
    
    public partial class Work_shift
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Work_shift()
        {
            this.Doctors = new HashSet<Doctors>();
        }
    
        public int id_Shift { get; set; }
        public System.TimeSpan Start_Work_Day { get; set; }
        public System.TimeSpan End_Work_Day { get; set; }
        public string Number { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Doctors> Doctors { get; set; }
    }
}
