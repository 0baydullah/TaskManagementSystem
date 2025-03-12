using BusinessLogicLayer.IService;
using BusinessLogicLayer.Service;
using DataAccessLayer.Models.Entity;
using DataAccessLayer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementTool.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IRoleService _roleService;
        public MemberController(IMemberService memberService, IRoleService roleService)
        {
            _memberService = memberService;
            _roleService = roleService;
        }
       
        [HttpGet]
        public IActionResult Index()
        {
            var members = _memberService.GetAllMember();
            return View(members);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var roles = _roleService.GetAllRole();

            ViewData["RoleId"] = new SelectList(roles, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Member model)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted! ";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    message += error + " ";
                    Console.WriteLine(error);
                }

            }
            else
            {
                _memberService.AddMember(model);
                isSuccess = true;
                message = "Member added successfully!";
            }

            return Json( new {isSuccess, message});
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var member = _memberService.GetMember(id);
            if (member == null)
            {
                return NotFound("Member not found!");
            }

            var model = new MemberVM
            {
                MemberId = member.MemberId,
                Email = member.Email,
                RoleId = member.RoleId
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit( MemberVM model, int id)
        {
            bool isSuccess = false;
            string message = "Invalid data submitted! ";

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    message += error + " ";
                    Console.WriteLine(error);
                }

            }

            else
            {
                var member = _memberService.GetMember(id);
                if (member == null)
                {
                    return NotFound("Member not found!");
                }
                member.Email = model.Email;
                member.RoleId = model.RoleId;
                _memberService.UpdateMember(member);
                isSuccess = true;
                message = "Member updated successfully!";
            }

            return Json(new { isSuccess, message });
        }

        [HttpPost]
        public IActionResult Delete(int id )
        {
            var member = _memberService.GetMember(id);

            if (member == null)
            {
                return NotFound("Member not found!");
            }

            _memberService.DeleteMember(member);

            return Json(new { success = true });

        }
    }
}
