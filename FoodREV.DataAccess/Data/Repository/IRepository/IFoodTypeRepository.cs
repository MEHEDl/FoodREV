using FoodREV.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace FoodREV.DataAccess.Data.Repository.IRepository
{
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        IEnumerable<SelectListItem> GetFoodTypeDropDown();

        void Update(FoodType foodType);
    }
}
