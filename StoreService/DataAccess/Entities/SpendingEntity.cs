namespace StoreService.DataAccess.Entities
{
    public class SpendingEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Emoji { get; set; }
        public int Value { get; set; }
        public int Factor { get; set; }
    }
}