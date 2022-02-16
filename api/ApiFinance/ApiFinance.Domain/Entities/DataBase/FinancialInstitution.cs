namespace ApiFinance.Domain.Entities.DataBase
{
    public class FinancialInstitution : EntityBase
    {
        public string CodeBank { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
    }
}