using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace sendill_client.Ropositories
{
    public class TourRepository
    {
        private dbSendillEntities context = null;

        public TourRepository()
        {
            this.context = new dbSendillEntities();
        }

        public TourRepository(dbSendillEntities db)
        {
            this.context = db;
        }

        public IEnumerable<tbl_tours> SelectAll()
        {

            return context.tbl_tours.ToList();

        }

        public tbl_tours SelectById(int id)
        {
            return context.tbl_tours.Find(id);
        }

        public void Update(tbl_tours obj)
        {
            context.Entry(obj).State = EntityState.Modified;
        }

        public void Insert(tbl_tours obj)
        {
            context.tbl_tours.Add(obj);
        }

        public void Delete(int id)
        {

            tbl_tours existing = context.tbl_tours.Find(id);
            context.tbl_tours.Remove(existing);

        }



    }
}

//public class CustomerRepository : ICustomerRepository
//{
//    private NorthwindEntities db = null;

//    public CustomerRepository()
//    {
//        this.db = new NorthwindEntities();
//    }

//    public CustomerRepository(NorthwindEntities db)
//    {
//        this.db = db;
//    }

//    public IEnumerable<Customer> SelectAll()
//    {
//        return db.Customers.ToList();
//    }

//    public Customer SelectByID(string id)
//    {
//        return db.Customers.Find(id);
//    }

//    public void Insert(Customer obj)
//    {
//        db.Customers.Add(obj);
//    }

//    public void Update(Customer obj)
//    {
//        db.Entry(obj).State = EntityState.Modified;
//    }

//    public void Delete(string id)
//    {
//        Customer existing = db.Customers.Find(id);
//        db.Customers.Remove(existing);
//    }

//    public void Save()
//    {
//        db.SaveChanges();
//    }
//}