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
    public class PriorityController : Controller
    {
        private readonly IPriorityService _priorityService;
        private readonly ILog _log = LogManager.GetLogger(typeof(PriorityController));

        public PriorityController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var priority = _priorityService.GetAllPriority();

                return View(priority);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
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
        public IActionResult Create(Priority priority)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _priorityService.AddPriority(priority);
                    return RedirectToAction("Index");
                }
                return View(priority);
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
                var priority = _priorityService.GetPriorityById(id);

                if (priority == null)
                {
                    return RedirectToAction("Notfound", "Error");
                }

                return View(priority);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, Priority priority)
        {
            try
            {
                if (id != priority.PriorityId)
                {
                    return RedirectToAction("Notfound","Error");
                }

                if (ModelState.IsValid)
                {
                    _priorityService.UpdatePriority(priority);
                    
                    return RedirectToAction("Index");
                }
                return View(priority);
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
                _priorityService.DeletePriority(id);

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
