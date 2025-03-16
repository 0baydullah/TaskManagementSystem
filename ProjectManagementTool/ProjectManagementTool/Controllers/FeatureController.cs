using BusinessLogicLayer.IService;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManagementTool.Controllers
{
    public class FeatureController : Controller
    {
        private readonly IMemberService _memberService;
        public readonly IFeatureService _featureService;
        public FeatureController(IMemberService memberService, IFeatureService featureService) 
        { 
            _memberService = memberService;
            _featureService = featureService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var members = _memberService.GetAllMember();
            // var releases = _releaseService.GetAllRelease();
            ViewBag.Members = new SelectList(members, "MemberId", "Name");
            //ViewBag.Releases = new SelectList(members, "ReleaseId", "Name");
            ViewBag.Releases = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Release-1" },
                new SelectListItem { Value = "2", Text = "Release-2" },
                new SelectListItem { Value = "7", Text = "Release-7" }
            }, "Value", "Text");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FeatureVM featureVM)
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { success = false, errors });
            }

            try
            {
                var result =  await _featureService.CreateFeature(featureVM);

                if (result == true)
                {
                    return Ok(new { success = true, redirectUrl = Url.Action("GetAll", "Feature") });
                }
                else
                {
                    return BadRequest(new { success = false, errors = new List<string> { "Failed" } });
                }
            }
            catch (Exception)
            {
                return View(featureVM);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var features = await _featureService.GetAllFeature();
                return View(features);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            try
            {
                var feature = await _featureService.GetFeatureById(id);
                var members = _memberService.GetAllMember();
                // var releases = _releaseService.GetAllRelease();
                ViewBag.Members = new SelectList(members, "MemberId", "Name");
                //ViewBag.Releasea = new SelectList(members, "ReleaseId", "Name");
                ViewBag.Releases = new SelectList(new List<SelectListItem>
                {
                    new SelectListItem { Value = "1", Text = "Release-1" },
                    new SelectListItem { Value = "2", Text = "Release-2" },
                    new SelectListItem { Value = "7", Text = "Release-7" }
                }, "Value", "Text");

                return View(feature);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FeatureVM featureVM)
        {
            try
            {
                var result = await _featureService.UpdateFeature(id, featureVM);

                if (result == true)
                {
                    return Ok(new { success = true, redirectUrl = Url.Action("GetAll", "Feature") });
                }
                else
                {
                    return BadRequest(new { success = false, errors = new List<string> { "Failed" } });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _featureService.DeleteFeature(id);

                if (result == true)
                {
                    return Ok(new { success = true, redirectUrl = Url.Action("GetAll", "Feature") });
                }
                else
                {
                    return BadRequest(new { success = false, errors = new List<string> { "Failed" } });
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
