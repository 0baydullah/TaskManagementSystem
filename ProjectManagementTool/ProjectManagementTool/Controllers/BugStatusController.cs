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
using Microsoft.AspNetCore.Authorization;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class BugStatusController : Controller
    {
        private readonly IBugStatusService _statusService;
        private readonly ILog _log = LogManager.GetLogger(typeof(StatusController));

        public BugStatusController(IBugStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var statuses = _statusService.GetAllStatuses();

                return View(statuses);
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
        public IActionResult Create(BugStatus status)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _statusService.AddStatus(status);

                    return RedirectToAction("Index");
                }
                return View(status);
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
                var status = _statusService.GetStatusById(id);
                if (status == null)
                {
                    return RedirectToAction("Notfound", "Error");
                }
                return View(status);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id,  BugStatus status)
        {
            try
            {
                if (id != status.Id)
                {
                    return RedirectToAction("Notfound", "Error");
                }

                if (ModelState.IsValid)
                {
                    _statusService.UpdateStatus(status);

                    return RedirectToAction("Index");
                }

                return View(status);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _statusService.DeleteStatus(id);

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
