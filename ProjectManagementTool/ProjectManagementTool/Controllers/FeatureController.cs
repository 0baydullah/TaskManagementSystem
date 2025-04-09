using BusinessLogicLayer.IService;
using DataAccessLayer.Models.ViewModel;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace ProjectManagementTool.Controllers
{
    [Authorize]
    public class FeatureController : Controller
    {
        private readonly IMemberService _memberService;
        public readonly IFeatureService _featureService;
        public readonly IReleaseService _releaseService;
        private readonly IProjectInfoService _projectInfoService;
        private readonly ILog _log = LogManager.GetLogger(typeof(FeatureController));

        public FeatureController(IMemberService memberService, IFeatureService featureService, IReleaseService releaseService,
            IProjectInfoService projectInfoService) 
        { 
            _memberService = memberService;
            _featureService = featureService;
            _releaseService = releaseService;
            _projectInfoService = projectInfoService;
        }

        [HttpGet]
        public IActionResult Create(int projectId) 
        {
            try
            {

                var members = _memberService.GetAllMember().Where(m => m.ProjectId == projectId);
                var releases = _releaseService.GetAllReleases().Where(r => r.ProjectId == projectId);
                var project = _projectInfoService.GetProjectInfo(projectId);
                ViewBag.Members = new SelectList(members, "MemberId", "Name");
                ViewBag.Releases = new SelectList(releases, "ReleaseId", "ReleaseName");
                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;
                
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
                    return Ok(new { success = true, redirectUrl = @Url.Action("GetAll", "Feature", new { projectId = featureVM.ProjectId })});
                }
                else
                {
                    return Ok(new { success = false, errors =  "Feature already exist" });
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int projectId)
        {
            try
            {
                var features = await _featureService.GetAllFeature(projectId);
                ViewBag.ProjectId = projectId;
                return View(features);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            try
            {
                var feature = await _featureService.GetFeatureById(id);
                var members = _memberService.GetAllMember().Where(m => m.ProjectId == feature.ProjectId);
                var releases = _releaseService.GetAllReleases().Where(r => r.ProjectId == feature.ProjectId);
                var project = _projectInfoService.GetProjectInfo(members.FirstOrDefault().ProjectId);

                ViewBag.ProjectId = project.ProjectId;
                ViewBag.ProjectKey = project.Key;
                ViewBag.Members = new SelectList(members, "MemberId", "Name");
                ViewBag.Releases = new SelectList(releases, "ReleaseId", "ReleaseName");
               
                return View(feature);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FeatureVM featureVM)
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { success = false, errors });
            }
            try
            {
                var result = await _featureService.UpdateFeature(id, featureVM);

                if (result == true)
                {
                    return Ok(new { success = true, redirectUrl = @Url.Action("GetAll", "Feature", new { projectId = featureVM.ProjectId }) });
                }
                else
                {
                    return Ok(new { success = false, errors = new List<string> { "Feature already exist" } });
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
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
            catch (Exception ex)
            {
                _log.Error(ex.Message);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Exception", "Error");
            }
        }
    }
}
