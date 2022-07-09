using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public class EmployeeRepo
    {
        public readonly DbSet<Employee> _dbSet;
        public readonly DepartmentEmployeeContext _dbContext;
        public EmployeeRepo(DepartmentEmployeeContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<Employee>();
        }

        public DbSet<Employee> Get()
        {
            return _dbSet;
        }

        public Employee GetById(string id)
        {
            var data = _dbSet.Find(id);
            return data;
        }

        public void Add(Employee entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Employee entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(string id)
        {
            var entity = GetById(id);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
