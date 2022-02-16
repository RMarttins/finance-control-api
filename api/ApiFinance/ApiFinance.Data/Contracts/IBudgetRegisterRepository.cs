using ApiFinance.Domain.Entities.DataBase;
using ApiFinance.Domain.Reports;
using System.Collections.Generic;

namespace ApiFinance.Data.Contracts
{
    public interface IBudgetRegisterRepository : IBaseRepository<BudgetRegister>
    {
        int Delete(int id);
        int Insert(BudgetRegister budgetRegister);
        IEnumerable<BudgetRegister> GetAll();
        BudgetRegister GetById(int id);
        IEnumerable<BudgetRegister> GetByMonthYear(string year, string month, int movementId);
        IEnumerable<BudgetRegister> GetByMovementId(int movementId);
        IEnumerable<BudgetRegister> GetByDescription(string description, int movementId);
        BudgetSummaryReport GetBudgetSummary(string year, string month);
        int Update(BudgetRegister budgetRegister);
    }
}