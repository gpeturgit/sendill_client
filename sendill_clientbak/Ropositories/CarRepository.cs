using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace sendill_client.Ropositories
{
    public class CarRepository
    {
        private dbSendillEntities context = null;

        public CarRepository()
        {
            this.context = new dbSendillEntities();
        }

        public CarRepository(dbSendillEntities db)
        {
            this.context = db;
        }

        public IEnumerable<tbl_cars> SelectAll()
        {
            return context.tbl_cars.ToList();
        }

        public tbl_cars SelectById(int id)
        {
            return context.tbl_cars.Find(id);
        }

        public void Update(tbl_cars obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }

        public void Insert(tbl_cars obj)
        {
            context.tbl_cars.Add(obj);
        }

        public void Delete(int id)
        {
            tbl_cars existing = context.tbl_cars.Find(id);
            context.tbl_cars.Remove(existing);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}