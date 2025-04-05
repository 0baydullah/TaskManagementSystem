using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    public class BugController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IUserStoryService _userStoryService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;
        private readonly IBugService _bugService;

        private readonly ILog _log = LogManager.GetLogger(typeof(BugController));

        public BugController(IMemberService memberService, IUserStoryService userStoryService, IStatusService statusService,
            IPriorityService prioriyService, IBugService bugService)
        {
            _memberService = memberService;
            _userStoryService = userStoryService;
            _statusService = statusService;
            _priorityService = prioriyService;
            _bugService = bugService;
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            try
            {
                var story = _userStoryService.GetUserStory(id);
                ViewBag.ProjectId = story.ProjectId;
                ViewBag.UserStoryId = id;

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                var priorities = _priorityService.GetAllPriority();
                ViewBag.Priority = new SelectList(priorities, "PriorityId", "Name");

                var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);
                ViewBag.Members = new SelectList(members, "MemberId", "Name");

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
        public IActionResult Create(int id, BugVM bugVM)
        {
            try
            {
                if (bugVM == null )
                {
                    return Json(new { success = false, messgage = "Story Not Found" });
                }
                
                _bugService.AddBug(id,bugVM);

                

                return Json(new { success = true });
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
                var bug = _bugService.GetBug(id);
                if (bug == null)
                {
                    return RedirectToAction("Notfound","Error");
                }

                _bugService.DeleteBug(bug);
                return Ok();
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
                var bug = _bugService.GetBug(id);

                var story = _userStoryService.GetUserStory(bug.UserStoryId);
                ViewBag.ProjectId = story.ProjectId;
                ViewBag.Id = id;

                var statuses = _statusService.GetAllStatuses();
                ViewBag.Status = new SelectList(statuses, "StatusId", "Name");

                var priorities = _priorityService.GetAllPriority();
                ViewBag.Priority = new SelectList(priorities, "PriorityId", "Name");

                var members = _memberService.GetAllMember().Where(m => m.ProjectId == story.ProjectId);
                ViewBag.Members = new SelectList(members, "MemberId", "Name");

                ViewBag.UserStoryId = bug.UserStoryId;

                if (bug == null)
                {
                    return RedirectToAction("Notfound","Error");
                }

                return View(bug);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
                TempData["Error"] = ex.Message;

                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, BugVM bug)
        {
            try
            {
                var existingBug = _bugService.GetBug(id);

                if (existingBug == null)
                {
                    return RedirectToAction("Notfound", "Error");
                }

                _bugService.UpdateBug(id,bug);

                return Json(new { success = true });
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
