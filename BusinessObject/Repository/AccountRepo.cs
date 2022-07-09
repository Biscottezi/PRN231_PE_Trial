using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class AccountRepo
    {
        public readonly DbSet<AccountDb> _dbSet;
        public readonly DepartmentEmployeeContext _dbContext;
        public AccountRepo(DepartmentEmployeeContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<AccountDb>();
        }

        public DbSet<AccountDb> Get()
        {
            return _dbSet;
        }

        public AccountDb GetById(string id)
        {
            var data = _dbSet.Find(id);
            return data;
        }

        public void Add(AccountDb entity)
        {
            _dbSet.Add(entity);

        }

        public void Update(AccountDb entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(string id)
        {
            var entity = GetById(id);
            _dbSet.Remove(entity);
        }
    }
}
