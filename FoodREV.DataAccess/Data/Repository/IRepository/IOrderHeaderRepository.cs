using System;
using System.Collections.Generic;
using System.Text;
using FoodREV.DataAccess.Data.Repository.IRepository;
using FoodREV.Models;

namespace Taste.DataAccess.Data.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
    }
}
