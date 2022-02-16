using ApiFinance.Domain.Entities.DataBase;
using System.Collections.Generic;

namespace ApiFinance.Data.Contracts
{
    public interface ISubCategoryRepository : IBaseRepository<SubCategory>
    {
        int Delete(int id);
        IEnumerable<SubCategory> GetAll();
        SubCategory GetById(int id);
        IEnumerable<SubCategory> GetByCategoryId(int categoryId);
        int Insert(SubCategory subCategory);
        int Update(SubCategory subCategory);
    }
}