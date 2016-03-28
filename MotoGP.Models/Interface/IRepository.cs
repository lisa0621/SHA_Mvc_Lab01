using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.Models.Interface
{
    public interface IRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }

        void Create(T instance);

        void Delete(T instance);

        void DeleteById(int id);

        bool AnyData(int id);

        T GetDataById(int id);

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        IQueryable<T> All();

        void RemoveRange(IEnumerable<T> entities);
    }
}
