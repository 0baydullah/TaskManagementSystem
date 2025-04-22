using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IProjectInfoService _projectInfoService;
        private readonly IMemberService _memberService;
        private readonly UserManager<UserInfo> _userManager;
        private readonly ILog _log = LogManager.GetLogger(typeof(ActivityController));

        public ActivityController(IActivityService activityService, IProjectInfoService projectInfoService, IMemberService memberService,
           UserManager<UserInfo> userManager)
        {
            _activityService = activityService;
            _projectInfoService = projectInfoService;
            _memberService = memberService;
            _userManager = userManager;
        }
        public IActionResult Index(int projectId)
        {
            try
            {
                var project = _projectInfoService.GetProjectInfo(projectId);
                var data = _activityService.GetAllActivities(projectId).OrderByDescending( x => x.PerformAt);
                ViewBag.MemberList = _userManager.Users.ToDictionary(member => member.Id, member => member.Name);
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;


                return View(data);

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
