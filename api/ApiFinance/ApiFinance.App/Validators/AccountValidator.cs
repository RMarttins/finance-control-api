using ApiFinance.App.Contracts.Validators;
using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using Microsoft.Extensions.Configuration;

namespace ApiFinance.App.Validators
{
    public class AccountValidator : BaseValidators<Account>, IAccountValidator
    {
        private readonly IAccountRepository _iAccountRepository;
        private Account account;

        public AccountValidator(IConfiguration iConfiguration, IAccountRepository iAccountRepository) : base(iConfiguration) =>
            _iAccountRepository = iAccountRepository;

        protected override void ValidateBusinessRules()
        {
            ValidateIfIsChange();
        }

        protected override void ValidateProperties()
        {

        }

        private void ValidateIfIsChange()
        {
            account = _iAccountRepository.GetById((int)Entity.Id);
            if (!account.Equals(Entity))
                ValidateValuesForChange();
        }
       
        private void ValidateValuesForChange()
        {
            if (Entity.AccountLimit != account.AccountLimit && Entity.AccountLimit == null)
                Entity.AccountLimit = account.AccountLimit;
            if (Entity.FinancialInstitutionId != account.FinancialInstitutionId && (Entity.FinancialInstitutionId == null || Entity.FinancialInstitutionId == 0))
                Entity.FinancialInstitutionId = account.FinancialInstitutionId;
            if (Entity.Name != account.Name && string.IsNullOrEmpty(Entity.Name))
                Entity.Name = account.Name;
            if (Entity.OpeningBalance != account.OpeningBalance && Entity.OpeningBalance == null)
                Entity.OpeningBalance = account.OpeningBalance;

            result = Entity;
        }
    }
}