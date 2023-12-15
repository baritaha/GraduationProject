using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Project_Test.Models;
using Project_Test.Models.ViewModels;
using Project_Test.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Test.Controllers
{
    public class ReciptionBankController : Controller
    {
        private readonly Project_TestDBContext _db;
        private UserManager<IdentityUser> _userManager;
        private readonly IToastNotification _toastNotification;
        public ReciptionBankController(Project_TestDBContext db, UserManager<IdentityUser> userManager, IToastNotification toastNotification)
        {
            _db = db;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public IActionResult Home_Bank()
        {
            ////bari tahahghvhv
            //var lowStocks = _db.Stocks.Where(d=>d.Qty <200).ToList();
            //try
            //{
            //    foreach (var stock in lowStocks)
            //    {
            //        var message = "The stock is very low in " + stock.BankName;

            //        var banks = _db.Banks.ToList(); // Retrieve all blood banks

            //        foreach (var bank in banks)
            //        {
            //            _toastNotification.AddWarningToastMessage(message);

            //        }
            //    }
            //}
            //catch (Exception x) { return View(); }

            return View();
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public async Task<IActionResult> AllDonations()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _db.Donations.Where(db => db.Donor_Location == user.UserName).Select(db => db).ToList();
            return View(result);
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public async Task<IActionResult> Donations()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _db.Donations.Where(db=>db.Donor_Location==user.UserName).Select(db=>db).ToList();
            return View(result);
        }
        [HttpPost]
        public IActionResult Accept(int id)
        {
            var donation = _db.Donations.FirstOrDefault(d => d.DonationID == id);
            donation.Donation_Status = Models.Donation_Box.Status.Accepted;
            var stock_id = _db.Stocks.Where(d => d.BankName == donation.Donor_Location).Select(d=>d).FirstOrDefault();
            var bloodstock = _db.Stocks.Include(s => s.Bloods).FirstOrDefault(s => s.BloodStockID == stock_id.BloodStockID);
            //Generate Random code for blood
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 10)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            Blood blood = new Blood { Type=donation.Donor_BloodType,Code=result,Start_Date=DateTime.Now,};
            blood.Expire_Date = blood.Start_Date.AddDays(14);
            bloodstock.Qty++;
            bloodstock.Bloods.Add(blood);
            _db.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Donation has been accepted");
            return RedirectToAction("Donations");
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public IActionResult Reject()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "BloodBank")]
        public IActionResult Reject(int id, Donation_Box model)
        {

            if (ModelState.IsValid)
            {

                var donation = _db.Donations.FirstOrDefault(d => d.DonationID == id);
                donation.Rejection_Reason = model.Rejection_Reason;
                donation.Donation_Status = Models.Donation_Box.Status.Rejected;
                _db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Donations");
            }

            return RedirectToAction("Donations");
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public async Task<IActionResult> Stock()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _db.Stocks.Where(s => s.BankName == user.UserName).Select(s=>s).FirstOrDefault();
            //int stock_id = result.BloodStockID;
            var bloodstock = _db.Stocks.Include(s => s.Bloods).FirstOrDefault(s => s.BloodStockID == result.BloodStockID);
            var blood = bloodstock.Bloods.ToList();
            var currentDate = DateTime.Today;
            var expired_Units = _db.Bloods.Where(s => s.Expire_Date < currentDate).Select(s => s).ToList();
            foreach (var unit in expired_Units)
            {
                _db.Bloods.Remove(unit);
            }
            _db.SaveChanges();
            return View(blood);
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "BloodBank")]
        public async Task<IActionResult> Add(Blood model)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _db.Stocks.Where(s => s.BankName == user.UserName).Select(s => s).FirstOrDefault();
            var bloodstock = _db.Stocks.Include(s => s.Bloods).FirstOrDefault(s => s.BloodStockID == result.BloodStockID);
            if (ModelState.IsValid)
            {
                var blood = new Blood
                {
                    Type = model.Type,
                    Start_Date = model.Start_Date,
                    Expire_Date = model.Start_Date.AddDays(14),
                    Code=model.Code
                };
                bloodstock.Bloods.Add(blood);
                bloodstock.Qty++;
                _db.SaveChanges();
                return RedirectToAction("Stock");
            }
            else
            {
                ModelState.AddModelError("", "Invalid");

            }
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public IActionResult EditBloodUnit(int id)
        {
            var unit = _db.Bloods.FirstOrDefault(d => d.BloodID == id);
            return View(unit);
        }
        [HttpPost]
        [Authorize(Roles = "BloodBank")]
        public async Task<IActionResult> EditBloodUnit(Blood model,int id)
        {
            var unit =_db.Bloods.FirstOrDefault(d => d.BloodID == id);
            if (ModelState.IsValid)
            {
                unit.Code = model.Code;
                unit.Start_Date = model.Start_Date;
                unit.Type = model.Type;
                unit.Expire_Date = unit.Start_Date.AddDays(14);
                _db.SaveChanges();
                _db.Update(unit);
                return RedirectToAction("Stock");
            }
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> DeleteBloodUnit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _db.Stocks.Where(s => s.BankName == user.UserName).Select(s => s).FirstOrDefault();
            var bloodstock = _db.Stocks.Include(s => s.Bloods).FirstOrDefault(s => s.BloodStockID == result.BloodStockID);
            var unit = _db.Bloods.FirstOrDefault(d => d.BloodID == id);
            bloodstock.Qty--;
            bloodstock.Bloods.Remove(unit);        
            _db.SaveChanges();
            return RedirectToAction("Stock");
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public IActionResult AllDonors()
        {
            var donors = _db.Donors.ToList();
            return View(donors);
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public async Task<IActionResult> Accepted_Donation()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _db.Donations.Where(db => db.Donor_Location == user.UserName).Select(db => db).ToList();
            var accept_status=result.FindAll(d => d.Donation_Status == Donation_Box.Status.Accepted).Select(d=>d).ToList();

            return View(accept_status);
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public async Task<IActionResult> Rejected_Donation()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = _db.Donations.Where(db => db.Donor_Location == user.UserName).Select(db => db).ToList();
            var reject_status = result.FindAll(d => d.Donation_Status == Donation_Box.Status.Rejected).Select(d => d).ToList();

            return View(reject_status);
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public IActionResult Rejected_Donation_Details(int id)
        {
            var donation = _db.Donations.FirstOrDefault(d => d.DonationID == id);
            return View(donation);
        }

        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public IActionResult ShowHospitalInfo()
        {
            var hospitals = _db.Banks.ToList(); // Retrieve all hospitals from the database
            var banks = _db.Banks.ToList();

            foreach (var bank in banks)
            {
                var stock = _db.Stocks.Where(d => d.BankName == bank.HospitalName).ToList();
                foreach (var sto in stock)
                {
                    if (bank.HospitalName == sto.BankName && sto.Qty <= bank.MinUnits)
                    {
                        var message = "The stock is very low in " + sto.BankName;
                        _toastNotification.AddWarningToastMessage(message);
                    }
                }
            }
            return View(hospitals);
        }
        [HttpGet]
        [Authorize(Roles = "BloodBank")]
        public IActionResult Register_Driver()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "BloodBank")]
        public async Task<IActionResult> Register_Driver(RegisterDriverViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                var driver = new Driver
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    Age = model.Age,
                    Gender = model.Gender,
                    NationalityNum = model.NationalityNum,
                    PhoneNumber = model.PhoneNumber,
                    BankName = user.Email
                };

                var Result = await _userManager.CreateAsync(driver, model.Password);
                if (Result.Succeeded)
                {
                    _db.Drivers.Add(driver);
                    await _userManager.AddToRoleAsync(driver, "Driver");
                    return RedirectToAction("ShowHospitalInfo", "ReciptionBank");
                }
                foreach (var err in Result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View();
            }
            return View();
        }


    }
}
