using ApiFinance.Domain.Entities.DataBase;
using System.Collections.Generic;

namespace ApiFinance.App.Contracts.Services
{
    public interface ISubCategoryService : IBaseService<SubCategory>
    {
        int Delete(int id);
        IEnumerable<SubCategory> GetAll();
        SubCategory GetById(int id);
        IEnumerable<SubCategory> GetByCategoryId(int categoryId);
        int Insert(SubCategory subCategory);
        int Update(SubCategory subCategory);
    }
}