//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kurs
{
    using System;
    using System.Collections.Generic;
    
    public partial class Avto
    {
        public Avto()
        {
            this.Orders = new HashSet<Orders>();
        }
    
        public int Id_avto { get; set; }
        public string Car_brand { get; set; }
        public string Model { get; set; }
    
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
