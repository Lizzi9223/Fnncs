//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Finances.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Income
    {
        public int ID_Income { get; set; }
        public string UserLogin { get; set; }
        public string CategoryName { get; set; }
        public string Descrip { get; set; }
        public System.DateTime date_when { get; set; }
        public double sum { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
    }
}
