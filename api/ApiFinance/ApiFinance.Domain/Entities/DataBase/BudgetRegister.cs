using System;

namespace ApiFinance.Domain.Entities.DataBase
{
    public class BudgetRegister : EntityBase
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime? DtRegister { get; set; }
        public string Description { get; set; }
        public DateTime? DtDue { get; set; }
        public string Note { get; set; }
        public int? PaymentAccountId { get; set; }
        public int? PaymentStatus { get; set; }
        public int? SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int? TypeId { get; set; }
        public string TypeName { get; set; }
        public decimal? Value { get; set; }
    }
}