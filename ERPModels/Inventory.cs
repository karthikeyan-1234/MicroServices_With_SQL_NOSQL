namespace ERPModels
{
    public class Inventory
    {
        public int id { get; set; }
        public int item_id { get; set; }
        public float qty { get; set; }
        public DateTime last_edit_at { get; set; }
    }
}
