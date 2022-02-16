namespace ApiFinance.Domain.Entities.DataBase
{
    public class CreditCard : EntityBase
    {
        public int? AccountPaymentId { get; set; }
        public string AccountPaymentName { get; set; }
        public decimal? AvailableLimit { get; set; }
        public decimal? CardLimit { get; set; }
        public int? ClosingDay { get; set; }
        public int? DueDay { get; set; }
        public int? FinancialInstitutionId { get; set; }
        public string FinancialInstitutionName { get; set; }
        public string Name { get; set; }
    }
}