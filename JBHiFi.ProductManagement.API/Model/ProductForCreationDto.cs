using System.ComponentModel.DataAnnotations;

namespace JBHiFi.ProductManagement.API.Model
{
    public class ProductForCreationDto
    {
        public string Id { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Model name is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Brand name is required")]
        public string Brand { get; set; }
    }
}
