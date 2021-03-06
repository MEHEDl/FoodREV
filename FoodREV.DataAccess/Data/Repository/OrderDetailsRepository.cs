﻿using System;
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
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetails orderDetails)
        {
            var orderDetailsFromDb = _db.OrderDetails.FirstOrDefault(m => m.Id == orderDetails.Id);
            _db.OrderDetails.Update(orderDetailsFromDb);

            _db.SaveChanges();

        }
    }
}
