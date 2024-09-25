
using BusinessLogicLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class GenaricRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : class
    {
        private DataContext _dataContext;
        protected DbSet<TEntity> _dbSet;
        public GenaricRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<TEntity>();
        }

		public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);
		public void Delete(TEntity entity) => _dbSet.Remove(entity);
		public async Task<TEntity?> GetAsync(int id) => await _dbSet.FindAsync(id);
		public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
		public void Update(TEntity entity) => _dbSet.Update(entity);


	}
}
