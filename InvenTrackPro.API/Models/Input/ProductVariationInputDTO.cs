namespace InvenTrackPro.API.Models.Input
{
    public class ProductVariationInputDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableStock { get; set; }
        public double PriceDelta { get; set; }
        public int ProductId { get; set; }
    }
}
