using BusinessLogicLayer.IService;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    public class FeatureController : Controller
    {
        private readonly IMemberService _memberService;
        public FeatureController(IMemberService memberService) 
        { 
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var members = _memberService.GetAllMember();
            // var releases = _releaseService.GetAllRelease();
            ViewBag.Members = new SelectList(members, "MemberId", "Name");
            //ViewBag.Releasea = new SelectList(members, "ReleaseId", "Name");
            ViewBag.Releases = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Release-1" },
                new SelectListItem { Value = "2", Text = "Release-2" }
            }, "Value", "Text");

            return View();
        }

        [HttpPost]
        public IActionResult Create(FeatureVM featureVM)
        {
            return View();
        }
    }
}
