
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class GenaricRepository<TEntity> : IGenaricRepository<TEntity> where TEntity : class
    {
        private DataContext _dbContext;
        protected DbSet<TEntity> _dbSet;
        public GenaricRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public int Create(TEntity entity)
        {
            _dbSet.Add(entity);
            return _dbContext.SaveChanges();   
        }

        public int Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return _dbContext.SaveChanges();
        }

        public TEntity? Get(int id)=> _dbSet.Find(id);  
       

        public IEnumerable<TEntity> GetAll() => _dbSet.ToList();
        

        public int Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
