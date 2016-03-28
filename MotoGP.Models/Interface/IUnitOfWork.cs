using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoGP.Models.Interface
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }

        void Commit();

        bool LazyLoadingEnabled { get; set; }

        bool ProxyCreationEnabled { get; set; }
    }
}
