using FoodREV.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace FoodREV.DataAccess.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryListDropDown();

        void Update(Category category);
    }
}
