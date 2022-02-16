using ApiFinance.App.Contracts.Services;
using ApiFinance.App.Contracts.Validators;
using ApiFinance.App.Services;
using ApiFinance.App.Validators;
using ApiFinance.Data.Context;
using ApiFinance.Data.Contracts;
using ApiFinance.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ApiFinance.Infra.Container
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Application
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IBudgetRegisterService, BudgetRegisterService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICreditCardService, CreditCardService>();

            //Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepositoy<>));
            services.AddScoped<IBudgetRegisterRepository, BudgetRegisterRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICreditCardRepository, CreditCardRepository>();

            //Context
            services.AddScoped<IDataContext, MySqlDataContext>();

            //Validators
            services.AddScoped<IBudgetRegisterInsertValidator, BudgetRegisterInsertValidator>();
            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<ICreditCardValidator, CreditCardValidator>();
        }
    }
}