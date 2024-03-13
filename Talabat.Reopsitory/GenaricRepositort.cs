using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;
using Talabat.Reopsitory.Date;

namespace Talabat.Reopsitory
{
    public class GenaricRepositort<T> : IGenaricRepositort<T> where T : BaseEntities
    {
        private readonly StoreContext dbContext;

        public GenaricRepositort(StoreContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #region Static
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
			if (typeof(T) == typeof(Product))
				return (IReadOnlyList<T>)await dbContext.Set<Product>().Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();

			return await dbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }
        #endregion

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(dbContext.Set<T>(), spec);
        }

        public async Task Add(T entitiy)
        => await dbContext.Set<T>().AddAsync(entitiy);

        public void update(T entitiy)
        =>  dbContext.Set<T>().Update(entitiy);
        public void delete(T entitiy)
         =>  dbContext.Set<T>().Remove(entitiy);
    }
}
