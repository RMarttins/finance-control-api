using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ApiFinance.Domain.Reports
{
    public class BudgetSummaryReport
    {
        public BudgetSummaryReport()
        {
            AmountSpentByCategory = new Collection<SpentCategoryReport>();
        }

        public decimal? TotalRevenue { get; set; }
        public decimal? TotalExpense { get; set; }
        public decimal? FinalBalance { get; set; }
        public ICollection<SpentCategoryReport> AmountSpentByCategory { get; set; }
    }
}