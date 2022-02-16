using ApiFinance.App.Contracts.Services;
using ApiFinance.Data.Contracts;
using ApiFinance.Domain.Entities.DataBase;
using System;
using System.Collections.Generic;

namespace ApiFinance.App.Services
{
    public class SubCategoryService : BaseService<SubCategory>, ISubCategoryService
    {
        private readonly ISubCategoryRepository _iSubCategoryRepository;

        public SubCategoryService(ISubCategoryRepository iSubCategoryRepository) : base(iSubCategoryRepository) =>
            _iSubCategoryRepository = iSubCategoryRepository;

        public int Delete(int id)
        {
            return _iSubCategoryRepository.Delete(id);
        }

        public IEnumerable<SubCategory> GetAll()
        {
            return _iSubCategoryRepository.GetAll();
        }

        public IEnumerable<SubCategory> GetByCategoryId(int categoryId)
        {
            return _iSubCategoryRepository.GetByCategoryId(categoryId);
        }

        public SubCategory GetById(int id)
        {
            return _iSubCategoryRepository.GetById(id);
        }

        public int Insert(SubCategory subCategory)
        {
            if (subCategory.CategoryId == null) throw new ArgumentException($"Tipo de categoria é obrigatório.", nameof(subCategory.CategoryId));
            if (string.IsNullOrEmpty(subCategory.Name?.Trim())) throw new ArgumentException($"Descrição é obrigatório.", nameof(subCategory.Name));
            return _iSubCategoryRepository.Insert(subCategory);
        }

        public int Update(SubCategory subCategory)
        {
            if (string.IsNullOrEmpty(subCategory.Name?.Trim())) throw new ArgumentException($"Descrição é obrigatório.", nameof(subCategory.Name));
            return _iSubCategoryRepository.Update(subCategory);
        }
    }
}