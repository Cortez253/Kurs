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
    
    public partial class Services
    {
        public Services()
        {
            this.Orders = new HashSet<Orders>();
        }
    
        public int Id_service { get; set; }
        public string Name_service { get; set; }
        public decimal Price { get; set; }
    
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
