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
using Microsoft.AspNetCore.Authorization;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class PriorityController : Controller
    {
        private readonly IPriorityService _priorityService;

        public PriorityController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var priority = _priorityService.GetAllPriority();

            return View(priority);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Priority priority)
        {
            if (ModelState.IsValid == true)
            {
                _priorityService.AddPriority(priority);

                return RedirectToAction("Index");
            }
            return View(priority);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var priority = _priorityService.GetPriorityById(id);

            if (priority == null)
            {
                return NotFound();
            }

            return View(priority);
        }

        [HttpPost]
        public IActionResult Edit(int id, Priority priority)
        {
            if (id != priority.PriorityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid == true)
            {
                _priorityService.UpdatePriority(priority);

                return RedirectToAction("Index");
            }

            return View(priority);
        }

        

        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _priorityService.DeletePriority(id);
            
            return RedirectToAction("Index");
        }

    }
}
