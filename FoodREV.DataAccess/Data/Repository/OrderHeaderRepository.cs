using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FoodREV.DataAccess;
using FoodREV.DataAccess.Data.Repository;
using FoodREV.DataAccess.Data.Repository.IRepository;
using FoodREV.Models;
using Taste.DataAccess.Data.Repository.IRepository;

namespace Taste.DataAccess.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
            var orderHeaderFromDb = _db.OrderHeader.FirstOrDefault(m => m.Id == orderHeader.Id);
            _db.OrderHeader.Update(orderHeaderFromDb);          
            
            _db.SaveChanges();

        }
    }
}
