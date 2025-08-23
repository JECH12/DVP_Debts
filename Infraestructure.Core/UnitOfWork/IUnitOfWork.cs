using Infraestructure.Core.Repository;
using Infraestructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Repository<User> UserRepository { get; }
        Repository<Payment> PaymentRepository { get; }
        Repository<State> StateRepository { get; }
        Repository<Debt> DebtRepository { get; }

        Task<int> SaveAsync();
    }
}
