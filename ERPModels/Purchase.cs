namespace ERPModels
{
    public class Purchase
    {
        public int id { get; set; }
        public DateTime purchase_date { get; set; }
        public string? address { get; set; }

        public ICollection<PurchaseDetail>? PurchaseDetails { get; set; }
    }
}
