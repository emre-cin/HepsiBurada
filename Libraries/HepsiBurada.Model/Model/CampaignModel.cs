using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HepsiBurada.Model.Model
{
    public class CampaignModel
    {
        public CampaignModel()
        {
            Orders = new List<OrderModel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Campaign Name is required")]
        [Display(Name = "Campaign Name")]
        public string CampaignName { get; set; }

        [Required(ErrorMessage = "Product Id is required")]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Code is required")]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

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

        [Required(ErrorMessage = "Duration is required")]
        [Display(Name = "Duration")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Is Active is required")]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }


        [Required(ErrorMessage = "Start Date is required")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public ProductModel Product { get; set; }

        public List<OrderModel> Orders { get; set; }
    }
}
