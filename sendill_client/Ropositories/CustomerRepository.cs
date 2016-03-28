using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace sendill_client.Ropositories
{
    public class CustomerRepository
    {
        private dbSendillEntities context = null;

        public CustomerRepository()
        {
            this.context = new dbSendillEntities();
        }

        public CustomerRepository(dbSendillEntities db)
        {
            this.context = db;
        }

        public IEnumerable<tbl_customers> SelectAll()
        {

            return context.tbl_customers.ToList();

        }

        public tbl_customers SelectById(int id)
        {
            return context.tbl_customers.Find(id);
        }

        public void Update(tbl_customers obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }

        public void Insert(tbl_customers obj)
        {
            context.tbl_customers.Add(obj);
        }

        public void Delete(int id)
        {

            tbl_customers existing = context.tbl_customers.Find(id);
            context.tbl_customers.Remove(existing);

        }

        public void Save()
        {
            context.SaveChanges();
        }



    }
}