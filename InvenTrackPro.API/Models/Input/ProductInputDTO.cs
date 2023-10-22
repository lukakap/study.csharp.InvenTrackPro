using InvenTrackPro.API.Models.Output;

namespace InvenTrackPro.API.Models.Input
{
    public class ProductInputDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double Price { get; set; }
        public int AvailableStock { get; set; }
        public int? CategoryId { get; set; }
    }
}
