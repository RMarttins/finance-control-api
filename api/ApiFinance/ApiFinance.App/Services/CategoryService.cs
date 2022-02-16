using ApiFinance.App.Contracts.Services;
using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using System;
using System.Collections.Generic;

namespace ApiFinance.App.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _iCategoryRepository;

        public CategoryService(ICategoryRepository iCategoryRepository) : base(iCategoryRepository) =>
            _iCategoryRepository = iCategoryRepository;

        public int Delete(int id)
        {
            return _iCategoryRepository.Delete(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _iCategoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _iCategoryRepository.GetById(id);
        }

        public IEnumerable<Category> GetByType(int typeId)
        {
            return _iCategoryRepository.GetByType(typeId);
        }

        public Category GetByTypeIdName(int typeId, string name)
        {
            if (string.IsNullOrEmpty(name?.Trim())) throw new ArgumentException($"Descrição é obrigatório.", nameof(name));
            return _iCategoryRepository.GetByTypeIdName(typeId, name);
        }

        public int Insert(Category category)
        {
            if (category.TypeId == null) throw new ArgumentException($"Tipo de categoria é obrigatório.", nameof(category.TypeId));
            if (string.IsNullOrEmpty(category.Name?.Trim())) throw new ArgumentException($"Descrição é obrigatório.", nameof(category.Name));
            return _iCategoryRepository.Insert(category);
        }

        public int Update(Category category)
        {
            if (string.IsNullOrEmpty(category.Name?.Trim())) throw new ArgumentException($"Descrição é obrigatório.", nameof(category.Name));
            return _iCategoryRepository.Update(category);
        }
    }
}