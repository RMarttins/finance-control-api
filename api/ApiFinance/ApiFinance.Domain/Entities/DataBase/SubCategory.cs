namespace ApiFinance.Domain.Entities.DataBase
{
    public class SubCategory : EntityBase
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
    }
}