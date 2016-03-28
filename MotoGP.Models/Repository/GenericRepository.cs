using MotoGP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.Models.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        public GenericRepository(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; private set; }

        private IDbSet<T> objectset;

        public IDbSet<T> ObjectSet
        {
            get { return objectset ?? (objectset = UnitOfWork.Context.Set<T>()); }
        }

        public void Create(T instance)
        {
            ObjectSet.Add(instance);
        }

        public void Delete(T instance)
        {
            ObjectSet.Remove(instance);
        }

        public void DeleteById(int id)
        {
            var entity = this.GetDataById(id);
            if (entity != null)
            {
                this.ObjectSet.Remove(entity);
            }
        }

        public T GetDataById(int id)
        {
            return this.ObjectSet.Find(id);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return this.objectset.Where(predicate);
        }

        public IQueryable<T> All()
        {
            return this.ObjectSet.AsQueryable();
        }

        public bool AnyData(int id)
        {
            return this.GetDataById(id) != null;
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            UnitOfWork.Context.Set<T>().RemoveRange(entities);
        }
    }
}

