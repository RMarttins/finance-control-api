namespace ApiFinance.Domain.Entities.DataBase
{
    public class Account : EntityBase
    {
        public decimal? AccountLimit { get; set; }
        public int? AccountTypeId { get; set; }
        public string AccountTypeName { get; set; }
        public decimal? BalanceAvailable { get; set; }
        public int? FinancialInstitutionId { get; set; }
        public string FinancialInstitutionName { get; set; }
        public string Name { get; set; }
        public decimal? OpeningBalance { get; set; }
    }
}