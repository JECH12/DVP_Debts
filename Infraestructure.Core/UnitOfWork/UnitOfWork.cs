using Infraestructure.Core.Context;
using Infraestructure.Core.Repository;
using Infraestructure.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoreContext _coreContext;
        private bool _disposed;
        private Repository<User> _userRepository;
        private Repository<Payment> _paymentRepository;
        private Repository<State> _stateRepository;
        private Repository<Debt> _debtRepository;

        public UnitOfWork(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task<int> SaveAsync() => await _coreContext.SaveChangesAsync();

        public Repository<State> StateRepository
        {
            get
            {
                if (_stateRepository == null)
                    _stateRepository = new Repository<State>(_coreContext);

                return _stateRepository;
            }
        }

        public Repository<User> UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new Repository<User>(_coreContext);

                return _userRepository;
            }
        }

        public Repository<Payment> PaymentRepository
        {
            get
            {
                if (_paymentRepository == null)
                    _paymentRepository = new Repository<Payment>(_coreContext);

                return _paymentRepository;
            }
        }

        public Repository<Debt> DebtRepository
        {
            get
            {
                if (_debtRepository == null)
                    _debtRepository = new Repository<Debt>(_coreContext);

                return _debtRepository;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _coreContext.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
