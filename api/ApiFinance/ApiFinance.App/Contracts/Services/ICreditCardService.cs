using ApiFinance.Domain.Entities.DataBase;
using System.Collections.Generic;

namespace ApiFinance.App.Contracts.Services
{
    public interface ICreditCardService : IBaseService<CreditCard>
    {
        int Delete(int id);
        IEnumerable<CreditCard> GetAll();
        CreditCard GetById(int id);
        int Insert(CreditCard creditCard);
        int Update(CreditCard creditCard);
    }
}