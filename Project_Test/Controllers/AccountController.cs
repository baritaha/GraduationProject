using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Test.Models;
using Project_Test.Models.Role;
using Project_Test.Models.ViewModels;
using Project_Test.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Controllers
{
    public class AccountController : Controller
    {
        private readonly Project_TestDBContext _db;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        public AccountController(Project_TestDBContext db,
                                 UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register_Donor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register_Donor(RegisterDonorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Donor 
                {
                    FirstName=model.FirstName,
                    LastName=model.LastName,
                    UserName=model.Email,
                    Email=model.Email,
                    Age=model.Age,
                    Gender=model.Gender,
                    BloodType=model.BloodType,
                    Address=model.Address,
                    NationalityNum=model.NationalityNum,
                    PhoneNumber=model.PhoneNumber
                };
                
                var Result = await _userManager.CreateAsync(user, model.Password);
                if (Result.Succeeded)
                {
                    _db.Donors.Add(user);
                    await _userManager.AddToRoleAsync(user, "Donor");
                    return RedirectToAction("Login_Donor");
                }
                foreach (var err in Result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login_Donor() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login_Donor(LoginDonorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Home_Donor", "Donor");
                }
                ModelState.AddModelError("", "Invalid User Or Password");
                return View(model);

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register_ReciptionBank()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register_ReciptionBank(RegisterBankViewModel model)
        {
            if (ModelState.IsValid)
            {
                ReciptionBank bank = new ReciptionBank
                {
                    MinUnits= model.MinUnits,
                    MaxUnits=model.MaxUnits,
                    HospitalName = model.HospitalName,
                    UserName = model.HospitalName,
                    Location = model.Location,
                    Current_Qty=model.Current_Qty
                };
                BloodStock stock = new BloodStock {BankName=bank.HospitalName,Qty=bank.Current_Qty};
                _db.Stocks.Add(stock);
                _db.SaveChanges();
                var Result = await _userManager.CreateAsync(bank, model.Password);
                if (Result.Succeeded)
                {
                    _db.Banks.Add(bank);             
                    return RedirectToAction("Login_ReciptionBank");
                }
                foreach (var err in Result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login_ReciptionBank()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login_ReciptionBank(LoginBankViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.HospitalName, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("ShowHospitalInfo", "ReciptionBank");
                }
                ModelState.AddModelError("", "Invalid User Or Password");
                return View(model);

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("MyTemp", "Home");
        }
        [HttpGet]
        public IActionResult RolesList()
        {
            return View(_roleManager.Roles);
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName,
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                ModelState.AddModelError("", "Role not Create");
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            if (id == null)
            {
                return RedirectToAction("RolesList");
            }
            var data = await _roleManager.FindByIdAsync(id);
            if (data == null)
            {
                return RedirectToAction("RolesList");
            }
            EditRoleViewModel model = new EditRoleViewModel
            {
                RoleID = data.Id,
                RoleName = data.Name
            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, model.RoleName))
                {
                    model.UsersNames.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleID);
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RolesList");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddRemoveUserRole(string id)
        {
            HttpContext.Session.SetString("roleId", id);
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("RolesList");
            }
            var UserRoleViewModel = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRoleVM = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleVM.IsSelected = true;
                }
                else
                {
                    userRoleVM.IsSelected = false;
                }
                UserRoleViewModel.Add(userRoleVM);
            }

            return View(UserRoleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddRemoveUserRole(List<UserRoleViewModel> model)
        {
            string roleId = HttpContext.Session.GetString("roleId");
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("RolesList");
            }
            IdentityResult res = null;
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    res = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    res = await _userManager.RemoveFromRoleAsync(user, role.Name);

                }
                else
                {
                    continue;
                }
                return RedirectToAction("RolesList");

            }
            return RedirectToAction("RolesList");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLogin model)
        {
            if (ModelState.IsValid)
            {

                if (model.UserName=="Admin" && model.Password=="0000")
                {
                    return RedirectToAction("RolesList", "Account");
                }
                ModelState.AddModelError("", "Invalid User Or Password");
                return View(model);

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login_Driver()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login_Driver(Login_Driver_ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("MyTemp", "Home");
                }
                ModelState.AddModelError("", "Invalid User Or Password");
                return View(model);

            }
            return View(model);
        }
    }
}
