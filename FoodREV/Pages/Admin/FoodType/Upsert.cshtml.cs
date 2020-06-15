﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using FoodREV.Models;
using FoodREV.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodREV
{
    public class FoodTypeUpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public FoodTypeUpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [BindProperty]
        public Models.FoodType FoodTypeObj { get; set; }
        public IActionResult OnGet(int? id)
        {
            FoodTypeObj = new Models.FoodType();
            if (id != null)
            {
                FoodTypeObj = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
                if (FoodTypeObj == null)
                {
                    return NotFound();
                }
            }
            return Page();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (FoodTypeObj.Id == 0)
            {
                _unitOfWork.FoodType.Add(FoodTypeObj);
            }
            else
            {
                _unitOfWork.FoodType.Update(FoodTypeObj);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}