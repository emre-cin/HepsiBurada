using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HepsiBurada.Model.Model
{
    public class ProductModel
    {
        public ProductModel()
        {
            Campaigns = new List<CampaignModel>();
            Orders = new List<OrderModel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Product Code is required")]
        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Units In Stock is required")]
        [Display(Name = "Units In Stock")]
        public int UnitsInStock { get; set; }

        public List<CampaignModel> Campaigns { get; set; }

        public List<OrderModel> Orders { get; set; }
    }
}
