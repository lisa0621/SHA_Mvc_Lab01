using MotoGP.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.Models.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;

        public DbContext Context
        {
            get
            {
                if (this.dbContext == null)
                {
                    this.dbContext = new MotoGPEntities();
                }

                return this.dbContext;
            }
        }

        public void Commit()
        {
            // Context.SaveChanges();
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //throw ex;
                var allErrors = new List<string>();

                foreach (DbEntityValidationResult re in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError err in re.ValidationErrors)
                    {
                        allErrors.Add(err.ErrorMessage);
                    }
                }
            }
        }

        public bool LazyLoadingEnabled
        {
            get { return Context.Configuration.LazyLoadingEnabled; }
            set { Context.Configuration.LazyLoadingEnabled = value; }
        }

        public bool ProxyCreationEnabled
        {
            get { return Context.Configuration.ProxyCreationEnabled; }
            set { Context.Configuration.ProxyCreationEnabled = value; }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                    this.dbContext = null;
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

