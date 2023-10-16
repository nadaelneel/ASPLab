using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Manger<T> where T : class
    {
        private readonly MyDBContext myDB;
        private readonly DbSet<T> myDbSet;
        public Manger(MyDBContext db) { 
            myDB = db;
            myDbSet = myDB.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return myDbSet.AsQueryable();
        }
        public IQueryable<T> search(
            Expression<Func<T,bool>> filter ,
            string orderBy ,
            bool isAssending ,
            int PageSize ,
            int Pageindex )
        {
            var query = myDbSet.AsQueryable();

            if(filter !=null)
            {
                query = query.Where(filter);
            }
            query = query.OrderBy(isAssending, orderBy);
            int toSkip = (Pageindex - 1) * PageSize;
            query = query.Skip(toSkip).Take(PageSize);
            return query;
        }
        public EntityEntry<T> Add(T item)
        {
            return myDbSet.Add(item);
        }

        public EntityEntry<T> Delete(T item)
        {
           return  myDbSet.Remove(item);
        }

        public EntityEntry<T> Update(T item)
        {
           return myDbSet.Update(item);
        }
    }
}
