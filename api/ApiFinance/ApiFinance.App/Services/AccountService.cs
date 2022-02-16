using ApiFinance.App.Contracts.Services;
using ApiFinance.App.Contracts.Validators;
using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using System;
using System.Collections.Generic;

namespace ApiFinance.App.Services
{
    public class AccountService : BaseService<Account>, IAccountService
    {
        private readonly IAccountRepository _iAccountRepository;
        private readonly IAccountValidator _iAccountValidator;

        public AccountService(IAccountRepository iAccountRepository, IAccountValidator iAccountValidator) : base(iAccountRepository)
        { 
            _iAccountRepository = iAccountRepository;
            _iAccountValidator = iAccountValidator;
        }

        public int Delete(int id)
        {
            return _iAccountRepository.Delete(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return _iAccountRepository.GetAll();
        }

        public Account GetById(int id)
        {
            return _iAccountRepository.GetById(id);
        }

        public int Insert(Account account)
        {
            if (account.Name == null) throw new ArgumentException($"Nome da conta é obrigatório.", nameof(account.Name));
            return _iAccountRepository.Insert(account);
        }

        public int Update(Account account)
        {
            _iAccountValidator.ValidateAllRules(account);
            if (_iAccountValidator.Passed)
                return _iAccountRepository.Update(account);
            else
                throw new Exception($"Uma ou mais regras consistidas no validator {nameof(IAccountValidator)} não passaram.");
        }
    }
}