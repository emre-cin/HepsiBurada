using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HepsiBurada.Model.DbEntity
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        public Product()
        {
            this.Campaigns = new List<Campaign>();
            this.Orders = new List<Order>();
        }

        [MaxLength]
        [Required(ErrorMessage = "Product Code is required")]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Units In Stock is required")]
        [Display(Name = "Units In Stock")]
        public int UnitsInStock { get; set; }

        public List<Campaign> Campaigns { get; set; }

        public List<Order> Orders { get; set; }
    }
}
