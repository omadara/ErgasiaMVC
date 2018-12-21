namespace ErgasiaMVC.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;

    public class ModelReports : DbContext
    {
        public ModelReports() : base("name=pubsEntities") { }
    }

    public class TopAuthorsReportInput {
        [DisplayName("Number of bestseller books")]
        [Range(1, 1000)]
        public int num { get; set; }

        [DisplayName("Start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [DisplayName("End date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }
    }

    public class SalesReportInput
    {
        [DisplayName("Start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime startDate { get; set; }

        [DisplayName("End date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }

        [DisplayName("Store name start")]
        public string storeNameStart { get; set; }

        [DisplayName("Store name end")]
        public string storeNameEnd { get; set; }
    }
}