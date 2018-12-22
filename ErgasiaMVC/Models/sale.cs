namespace ErgasiaMVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class sale
    {
        [Required, Display(Name = "Store ID")]
        public string stor_id { get; set; }
        [Required, Display(Name = "Order #"), MaxLength(20)]
        public string ord_num { get; set; }
        [Required, DisplayName("Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public System.DateTime ord_date { get; set; }
        [Required, DisplayName("Quantity")]
        public short qty { get; set; }
        [Required, DisplayName("Payterms"), MaxLength(12)]
        public string payterms { get; set; }
        [Required, DisplayName("Title ID")]
        public string title_id { get; set; }
    
        public virtual store store { get; set; }
        public virtual title title { get; set; }
    }
}
