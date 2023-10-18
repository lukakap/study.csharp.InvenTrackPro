namespace InvenTrackPro.API.Models
{
    public class ProductVariation
    {
        public int ProductVariationId { get; set; }
        public string ProductVariationName { get; set; }
        public string ProductVariationDescription { get; set; }
        public int AvailableStock { get; set; }
        public double PriceDelta { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
