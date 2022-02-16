namespace ApiFinance.Domain.Reports
{
    public class SpentCategoryReport
    {
        public int IdCategory { get; set; }
        public string NomeCategory { get; set; }
        public decimal? AmountCategory { get; set; }
    }
}