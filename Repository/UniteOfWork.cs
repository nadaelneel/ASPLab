using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UniteOfWork
    {
        private MyDBContext MyDBContext { get; set; }

        public UniteOfWork(MyDBContext db) {
            MyDBContext = db;
        }

        public void Save()
        {
            MyDBContext.SaveChanges();
        }
    }
}
