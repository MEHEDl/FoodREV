﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FoodREV.DataAccess.Data.Repository.IRepository;
using FoodREV.Models.ViewModels;
using FoodREV.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodREV.Pages.Admin.MenuItem
{
    [Authorize(Roles = SD.ManagerRole)]
    public class MenuItemUpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MenuItemUpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public MenuItemVM MenuItemObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            MenuItemObj = new MenuItemVM
            {
                CategoryList = _unitOfWork.Category.GetCategoryListDropDown(),
                FoodTypeList = _unitOfWork.FoodType.GetFoodTypeDropDown(),
                MenuItem = new Models.MenuItem()
            };
            if (id != null)
            {
                MenuItemObj.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id.GetValueOrDefault());
                if (MenuItemObj == null)
                {
                    return NotFound();
                }
            }
            return Page();

        }

        public IActionResult OnPost()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (MenuItemObj.MenuItem.Id == 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\menuItems");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                MenuItemObj.MenuItem.Image = @"\images\menuItems\" + fileName + extension;

                _unitOfWork.MenuItem.Add(MenuItemObj.MenuItem);
            }
            else
            {
                //Edit Menu Item
                var objFromDb = _unitOfWork.MenuItem.Get(MenuItemObj.MenuItem.Id);
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuItems");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }


                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    MenuItemObj.MenuItem.Image = @"\images\menuItems\" + fileName + extension;
                }
                else
                {
                    MenuItemObj.MenuItem.Image = objFromDb.Image;
                }


                _unitOfWork.MenuItem.Update(MenuItemObj.MenuItem);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}