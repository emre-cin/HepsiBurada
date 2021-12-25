using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HepsiBurada.Model.DbEntity
{
    [Table("Campaign")]
    public class Campaign : BaseEntity
    {
        public Campaign()
        {
            this.Orders = new List<Order>();
            this.IsActive = true;
        }

        [MaxLength]
        [Required(ErrorMessage = "Campaign Name is required")]
        [Display(Name = "Campaign Name")]
        public string CampaignName { get; set; }

        [Required(ErrorMessage = "Product Id is required")]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Discount Percent is required")]
        [Display(Name = "Discount Percent")]
        public decimal DiscountPercent { get; set; }

        [Required(ErrorMessage = "Target Sales Count is required")]
        [Display(Name = "Target Sales Count")]
        public int TargetSalesCount { get; set; }

        [Required(ErrorMessage = "Limit is required")]
        [Display(Name = "Limit")]
        public decimal Limit { get; set; }

        [Required(ErrorMessage = "Is Active is required")]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public Product Product { get; set; }

        public List<Order> Orders { get; set; }
    }
}
