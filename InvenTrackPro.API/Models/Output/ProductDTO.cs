namespace InvenTrackPro.API.Models.Output
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public double Price { get; set; }
        public int AvailableStock { get; set; }
        public string CategoryName {  get; set; }
        public ICollection<ProductVariationDTO> Variations { get; set; }
    }
}
