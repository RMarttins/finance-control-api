using ApiFinance.App.Contracts.Services;
using ApiFinance.App.Contracts.Validators;
using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using ApiFinance.Domain.Reports;
using System;
using System.Collections.Generic;

namespace ApiFinance.App.Services
{
    public class BudgetRegisterService : BaseService<BudgetRegister>, IBudgetRegisterService
    {
        private readonly IBudgetRegisterRepository _iBudgetRegisterRepository;
        private readonly IBudgetRegisterInsertValidator _iBudgetRegisterInsertValidator;
        public BudgetRegisterService(IBudgetRegisterRepository iBudgetRegisterRepository, 
            IBudgetRegisterInsertValidator iBudgetRegisterInsertValidator) : base(iBudgetRegisterRepository)
        {
            _iBudgetRegisterRepository = iBudgetRegisterRepository;
            _iBudgetRegisterInsertValidator = iBudgetRegisterInsertValidator;
        }

        public int Delete(int id)
        {
            return _iBudgetRegisterRepository.Delete(id);
        }

        public IEnumerable<BudgetRegister> GetAll()
        {
            return _iBudgetRegisterRepository.GetAll();
        }

        public BudgetSummaryReport GetBudgetSummary(string year, string month)
        {
            if (string.IsNullOrEmpty(year?.Trim())) throw new ArgumentException($"Ano é obrigatório.", nameof(year));
            if (string.IsNullOrEmpty(month?.Trim())) throw new ArgumentException($"Mês é obrigatório.", nameof(month));

            var result =  _iBudgetRegisterRepository.GetBudgetSummary(year.Trim(), month.Trim());
            result.FinalBalance = result.TotalRevenue - result.TotalExpense;
            return result;
        }

        public IEnumerable<BudgetRegister> GetByDescription(string description, int movementId)
        {
            if (string.IsNullOrEmpty(description?.Trim())) throw new ArgumentException($"Descrição é obrigatório.", nameof(description));

            return _iBudgetRegisterRepository.GetByDescription(description.Trim(), movementId);
        }

        public BudgetRegister GetById(int id)
        {
            return _iBudgetRegisterRepository.GetById(id);
        }

        public IEnumerable<BudgetRegister> GetByMonthYear(string year, string month, int movementId)
        {
            if (string.IsNullOrEmpty(year?.Trim())) throw new ArgumentException($"Ano é obrigatório.", nameof(year));
            if (string.IsNullOrEmpty(month?.Trim())) throw new ArgumentException($"Mês é obrigatório.", nameof(month));

            return _iBudgetRegisterRepository.GetByMonthYear(year.Trim(), month.Trim(), movementId);
        }

        public IEnumerable<BudgetRegister> GetByMovementId(int movementId)
        {
            return _iBudgetRegisterRepository.GetByMovementId(movementId);
        }

        public int Insert(BudgetRegister budgetRegister)
        {
            _iBudgetRegisterInsertValidator.ValidateAllRules(budgetRegister);
            if (_iBudgetRegisterInsertValidator.Passed)
                return _iBudgetRegisterRepository.Insert(budgetRegister);
            else
                throw new Exception($"Uma ou mais regras consistidas no validator {nameof(IBudgetRegisterInsertValidator)} não passaram.");
        }

        public int Update(BudgetRegister budgetRegister)
        {
            //VALIDAR OS DADOS DO REQUEST
            return _iBudgetRegisterRepository.Update(budgetRegister);
        }
    }
}