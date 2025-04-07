using BusinessLogicLayer.IService;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementTool.Controllers
{

    public class FilterController : Controller
    {
        private readonly IFilterService _filterService;

        public FilterController(IFilterService filterService)
        {
            _filterService = filterService;
        }
        public IActionResult Index(int projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
        }

        //[HttpGet("/progress/{status}")]
        public IActionResult Progress(string status, int projectId)
        {
            _filterService.InProgress(status, projectId);
            return View();
        }
    }
}
