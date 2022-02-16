using ApiFinance.Domain.Entities.DataBase;
using System.Collections.Generic;

namespace ApiFinance.App.Contracts.Services
{
    public interface IAccountService : IBaseService<Account>
    {
        int Delete(int id);
        IEnumerable<Account> GetAll();
        Account GetById(int id);
        int Insert(Account account);
        int Update(Account account);
    }
}