using System.ComponentModel.DataAnnotations;

namespace FastFood.Web.ViewModels.Items
{
    public class ItemsAllViewModels
    {
        [Required]
        [MinLength(3),MaxLength(20)]
        public string Name { get; set; }

        [Range(typeof(decimal),"0.00","5000")]
        public decimal Price { get; set; }

        public string Category { get; set; }
    }
}
