using System.Collections.Generic;

namespace ApiFinance.Domain.Entities.DataBase
{
    public class Category : EntityBase
    {
        public int? TypeId { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}