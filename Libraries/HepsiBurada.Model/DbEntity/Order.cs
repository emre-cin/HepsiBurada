using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HepsiBurada.Model.DbEntity
{
    [Table("Order")]
    public class Order : BaseEntity
    {
        [Required(ErrorMessage = "Product Id is required")]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Display(Name = "Campaign Id")]
        public int? CampaignId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Discount is required")]
        [Display(Name = "Discount")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Total Price is required")]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "Is Active is required")]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public Product Product { get; set; }

        public Campaign Campaign { get; set; }
    }
}
