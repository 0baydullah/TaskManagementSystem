using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data;
using DataAccessLayer.Models.Entity;
using BusinessLogicLayer.IService;
using log4net;

namespace ProjectManagementTool.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILog _log = LogManager.GetLogger(typeof(CategoryController));

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var categories = _categoryService.GetAllCategory();
             //   throw new NullReferenceException();

                return View(categories);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception","Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _categoryService.AddCategory(category);
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var category = _categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return RedirectToAction("Notfound", "Error");
                }
                return View(category);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return RedirectToAction("Notfound", "Error");
                }

                if (ModelState.IsValid)
                {
                    _categoryService.UpdateCategory(category);
                    return RedirectToAction("Index");
                }
                return View(category);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }
    }
}
