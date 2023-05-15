namespace ERPModels
{
    public class PurchaseDetail
    {
        public int id { get; set; }
        public int purchase_id { get; set; }
        public int item_id { get; set; }
        public float qty { get; set; }
        public float rate { get; set; }

        public Purchase? Purchase { get; set; }
    }
}
