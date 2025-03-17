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

namespace ProjectManagementTool.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var statuses = _statusService.GetAllStatuses();

            return View(statuses);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Status status)
        {
            if (ModelState.IsValid == true)
            {
                _statusService.AddStatus(status);

                return RedirectToAction("Index");
            }
            return View(status);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = _statusService.GetStatusById;

            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        [HttpPost]
        public IActionResult Edit(int id, Status status)
        {
            if (id != status.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid == true)
            {
                _statusService.UpdateStatus(status);

                return RedirectToAction("Index");
            }

            return View(status);
        }

        

        
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            _statusService.DeleteStatus(id);
            
            return RedirectToAction("Index");
        }

    }
}
