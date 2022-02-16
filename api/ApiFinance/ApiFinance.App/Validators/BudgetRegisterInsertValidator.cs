using ApiFinance.App.Contracts.Services;
using ApiFinance.App.Contracts.Validators;
using ApiFinance.Domain.Entities.DataBase;
using Microsoft.Extensions.Configuration;
using System;

namespace ApiFinance.App.Validators
{
    public class BudgetRegisterInsertValidator : BaseValidators<BudgetRegister>, IBudgetRegisterInsertValidator
    {
        private ICategoryService _iCategoryService;

        public BudgetRegisterInsertValidator(IConfiguration iConfiguration, ICategoryService iCategoryService) : base(iConfiguration) 
            => _iCategoryService = iCategoryService;

        protected override void ValidateBusinessRules()
        {
            ValidateCategory();
        }

        private void ValidateCategory()
        {
            if (Entity.CategoryId == null || Entity.CategoryId == 0)
            {
                var category = _iCategoryService.GetByTypeIdName((int)Entity.TypeId, "Outros");
                Entity.CategoryId = category.Id;
            }

            result = Entity;
        }

        protected override void ValidateProperties()
        {
            if (string.IsNullOrEmpty(Entity.Description?.Trim())) throw new FieldAccessException($"A descrição é obrigatório.");
            if (Entity.TypeId == null || Entity.TypeId == 0) throw new FieldAccessException($"O Tipo de movimentação é obrigatório.");
            if (Entity.Value == 0 || Entity.Value < 0) throw new FieldAccessException($"O valor do registro deve ser maior que 0.");
            if (Entity.DtRegister == default) throw new FieldAccessException($"Data de registro é obrigatório.");
        }

    }
}