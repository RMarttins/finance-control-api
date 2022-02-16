using ApiFinance.Domain.Entities.DataBase;
using System.Collections.Generic;

namespace ApiFinance.Data.Contracts
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        int Delete(int id);
        IEnumerable<Account> GetAll();
        Account GetById(int id);
        int Insert(Account account);
        int Update(Account account);
    }
}