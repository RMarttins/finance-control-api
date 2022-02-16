using ApiFinance.App.Contracts.Services;
using ApiFinance.App.Contracts.Validators;
using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using System;
using System.Collections.Generic;

namespace ApiFinance.App.Services
{
    public class CreditCardService : BaseService<CreditCard>, ICreditCardService
    {
        private readonly ICreditCardRepository _iCreditCardRepository;
        private readonly ICreditCardValidator _iCreditCardValidator;

        public CreditCardService(ICreditCardRepository iCreditCardRepository, ICreditCardValidator iCreditCardValidator) : base(iCreditCardRepository)
        {
            _iCreditCardRepository = iCreditCardRepository;
            _iCreditCardValidator = iCreditCardValidator;
        }

        public int Delete(int id)
        {
            return _iCreditCardRepository.Delete(id);
        }

        public IEnumerable<CreditCard> GetAll()
        {
            return _iCreditCardRepository.GetAll();
        }

        public CreditCard GetById(int id)
        {
            return _iCreditCardRepository.GetById(id);
        }

        public int Insert(CreditCard creditCard)
        {
            if (creditCard.Name == null) throw new ArgumentException($"Nome do cartão é obrigatório.", nameof(creditCard.Name));
            return _iCreditCardRepository.Insert(creditCard);
        }

        public int Update(CreditCard creditCard)
        {
            _iCreditCardValidator.ValidateAllRules(creditCard);
            if(_iCreditCardValidator.Passed)
                return _iCreditCardRepository.Update(creditCard);
            else
                throw new Exception($"Uma ou mais regras consistidas no validator {nameof(ICreditCardValidator)} não passaram.");
        }
    }
}