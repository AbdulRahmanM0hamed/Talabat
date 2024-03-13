using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Reopsitory.Date;

namespace Talabat.Reopsitory
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;
        private readonly Hashtable _Repository;
        public UnitOfWork(StoreContext context)
        {
            this.context = context;
            this._Repository = new Hashtable();
        }

        public IGenaricRepositort<TEntity> Repositort<TEntity>() where TEntity : BaseEntities
        {
            var Type = typeof(TEntity).Name;
            if (!_Repository.ContainsKey(Type))
            {
                var repository = new GenaricRepositort<TEntity>(context);
                _Repository.Add(Type, repository);

            }
            return _Repository[Type] as IGenaricRepositort<TEntity>;    
        
        }


        public async Task<int> Complete()
        => await context.SaveChangesAsync();
        public async ValueTask DisposeAsync()
        => await context.DisposeAsync();


    }
}
