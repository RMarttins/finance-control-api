using ApiFinance.App.Contracts.Validators;
using Microsoft.Extensions.Configuration;
using System;

namespace ApiFinance.App.Validators
{
    public abstract class BaseValidators<TEntity> : IBaseValidator<TEntity>
    {
        private bool _disposed;
        readonly IConfiguration _iConfiguration;
        private bool? _passed;
        protected object result;

        protected BaseValidators(IConfiguration iConfiguration) => _iConfiguration = iConfiguration;

        private bool GetStatus()
        {
            if (_passed is null)
                throw new AccessViolationException($"O método {nameof(ValidateAllRules)} deve ser executado antes de consultar a propriedade.");

            return (bool)_passed;
        }

        private void Run()
        {
            _passed = false;
            ValidateProperties();
            ValidateBusinessRules();
            _passed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }

        protected abstract void ValidateBusinessRules();

        protected abstract void ValidateProperties();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void ValidateAllRules(TEntity entity)
        {
            Entity = entity;
            Run();
        }

        public virtual Tr ValidateAllRules<Tr>(TEntity entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            Entity = entity;
            Run();
            return (Tr)Result;
        }

        public TEntity Entity { get; private set; }

        public bool Passed => GetStatus();

        public object Result => result;
    }
}
