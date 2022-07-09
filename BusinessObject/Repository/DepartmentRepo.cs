using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class DepartmentRepo
    {
        public readonly DbSet<Department> _dbSet;
        public readonly DepartmentEmployeeContext _dbContext;
        public DepartmentRepo(DepartmentEmployeeContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Department>();
        }

        public DbSet<Department> Get()
        {
            return _dbSet;
        }

        public Department GetById(string id)
        {
            var data = _dbSet.Find(id);
            return data;
        }

        public void Add(Department entity)
        {
            _dbSet.Add(entity);

        }

        public void Update(Department entity)
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
