using ApiFinance.Domain.Entities.DataBase;
using System.Collections.Generic;

namespace ApiFinance.Data.Contracts
{
    public interface ICreditCardRepository : IBaseRepository<CreditCard>
    {
        int Delete(int id);
        IEnumerable<CreditCard> GetAll();
        CreditCard GetById(int id);
        int Insert(CreditCard creditCard);
        int Update(CreditCard creditCard);
    }
}