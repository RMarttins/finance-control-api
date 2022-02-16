using ApiFinance.App.Contracts.Validators;
using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using Microsoft.Extensions.Configuration;

namespace ApiFinance.App.Validators
{
    public class CreditCardValidator : BaseValidators<CreditCard>, ICreditCardValidator
    {
        private readonly ICreditCardRepository _iCreditCardRepository;
        private CreditCard creditCard;

        public CreditCardValidator(IConfiguration iConfiguration, ICreditCardRepository iCreditCardRepository) : base(iConfiguration) =>
            _iCreditCardRepository = iCreditCardRepository;

        protected override void ValidateBusinessRules()
        {
            ValidateIfIsChange();
        }

        protected override void ValidateProperties()
        {
            
        }

        private void ValidateIfIsChange()
        {
            creditCard = _iCreditCardRepository.GetById((int)Entity.Id);
            if (!creditCard.Equals(Entity))
                ValidateValuesForChange();
        }

        private void ValidateValuesForChange()
        {
            if (Entity.AccountPaymentId != creditCard.AccountPaymentId && (Entity.AccountPaymentId == null || Entity.AccountPaymentId == 0))
                Entity.AccountPaymentId = creditCard.AccountPaymentId;
            if (Entity.CardLimit != creditCard.CardLimit && Entity.CardLimit == null)
                Entity.CardLimit = creditCard.CardLimit;
            if (Entity.ClosingDay != creditCard.ClosingDay && (Entity.ClosingDay == null || Entity.ClosingDay == 0))
                Entity.ClosingDay = creditCard.ClosingDay;
            if (Entity.DueDay != creditCard.DueDay && (Entity.DueDay == null || Entity.DueDay == 0))
                Entity.DueDay = creditCard.DueDay;
            if (Entity.FinancialInstitutionId != creditCard.FinancialInstitutionId && (Entity.FinancialInstitutionId == null || Entity.FinancialInstitutionId == 0))
                Entity.FinancialInstitutionId = creditCard.FinancialInstitutionId;
            if (Entity.Name != creditCard.Name && string.IsNullOrEmpty(Entity.Name))
                Entity.Name = creditCard.Name;

            result = Entity;
        }
    }
}