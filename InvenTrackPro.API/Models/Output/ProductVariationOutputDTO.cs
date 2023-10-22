namespace InvenTrackPro.API.Models.Output
{
    public class ProductVariationOutputDTO
    {
        public int VariationId { get; set; }
        public string VariationName { get; set; }
        public string VariationDescription { get; set; }
        public int AvailableStock { get; set; }
        public double VariationPrice { get; set; }
    }
}
