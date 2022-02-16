using System;

namespace ApiFinance.App.Contracts.Validators
{
    public interface IBaseValidator<TEntity> : IDisposable
    {
        void ValidateAllRules(TEntity entity);
        Tr ValidateAllRules<Tr>(TEntity entity);

        TEntity Entity { get; }
        bool Passed { get; }
        object Result { get; }
    }
}