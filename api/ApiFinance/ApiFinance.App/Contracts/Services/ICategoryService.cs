using ApiFinance.Domain.Entities.DataBase;
using System.Collections.Generic;

namespace ApiFinance.App.Contracts.Services
{
    public interface ICategoryService : IBaseService<Category>
    {
        int Delete(int id);
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        IEnumerable<Category> GetByType(int typeId);
        Category GetByTypeIdName(int typeId, string name);
        int Insert(Category category);
        int Update(Category category);
    }
}