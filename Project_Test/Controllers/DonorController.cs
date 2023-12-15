using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Test.Models;
using Project_Test.Models.ViewModels;
using Project_Test.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Controllers
{
    public class DonorController : Controller
    {
        private readonly Project_TestDBContext _db;
        private UserManager<IdentityUser> _userManager;
        public DonorController(UserManager<IdentityUser> userManager, Project_TestDBContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        [HttpGet]
        [Authorize(Roles = "Donor")]
        public IActionResult Home_Donor()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Donor")]
        public async Task<IActionResult> Profile(Donor donor)
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }
        [HttpGet]
        [Authorize(Roles = "Donor")]
        public IActionResult Edit_Profile(int id)
        {
          
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Donor")]
        public async Task<IActionResult> Edit_Profile(Edit_ProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            Donor donor = _db.Donors.FirstOrDefault(d => d.Id == id);
            if (ModelState.IsValid)
            {
                donor.FirstName = model.FirstName;
                donor.LastName = model.LastName;
                donor.Age = model.Age;
                donor.Gender = model.Gender;
                donor.BloodType = model.BloodType;
                donor.Address = model.Address;
                donor.NationalityNum = model.NationalityNum;
                donor.PhoneNumber = model.PhoneNumber;
                var result = await _userManager.UpdateAsync(donor);
                if (result.Succeeded)
                {
                    return RedirectToAction("Home_Donor", "Donor");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View();
            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Donor")]
        public async Task<IActionResult> Page()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }
        [HttpGet]
        [Authorize(Roles = "Donor")]
        public IActionResult Donate()
        {
            var model = new DonateViewModel();
            model.BloodBanks = _db.Banks.ToList();
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Donor")]
        public async Task<IActionResult> Donate(DonateViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            var id = user.Id;
            Donor donor = _db.Donors.FirstOrDefault(d => d.Id == id);
            var donation = new Donation_Box
            {
                Donor_Email = donor.Email,
                Donor_BloodType = donor.BloodType,
                Donor_Location = model.Donor_Location,
                Patient_Name = model.Patient_Name,
                Patient_Age = model.Patient_Age,
                Patient_BloodType = model.Patient_BloodType,
                Patient_Gender = model.Patient_Gender,
                Patient_Hospital = model.Patient_Hospital,
                Patient_NationalityNum = model.Patient_NationalityNum,
                Diseases = model.Diseases,
                Donation_Status = Donation_Box.Status.Pending
                
            };
            if (ModelState.IsValid)
            {
                _db.Donations.Add(donation);
                _db.SaveChanges();
                return RedirectToAction("Page", "Donor");
            }
            else
            {
                    ModelState.AddModelError("", "Invalid Input");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> View_Donations()
        {
            var donor = await _userManager.GetUserAsync(User);
            var data = _db.Donations.Where(s => s.Donor_Email == donor.Email).Select(s=>s).ToList();
            return View(data);
        }
    }
}
