using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareerCloud.UI.Controllers
{
    public class ApplicantProfileController : Controller
    {
        private readonly ApplicantProfileLogic _logic;
        public ApplicantProfileController()
        {
            _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
        }
        public IActionResult Index()
        {
            var pocos = _logic.GetAll(e=>e.ApplicantEducations,
                                        w=>w.ApplicantWorkHistories,
                                        s => s.ApplicantSkills,
                                        j=>j.ApplicantJobApplications,
                                        s=>s.SecurityLogin).OrderByDescending(o=>o.SecurityLogin.Created);

            if (pocos is null) return NotFound("No student found");
            
            return View(pocos);
        }

        public IActionResult Details (Guid Id)
        {
            if (Id.ToString() is null) return BadRequest();

            var poco = _logic.GetAll(e => e.ApplicantEducations,
                                        w => w.ApplicantWorkHistories,
                                        s => s.ApplicantSkills,
                                        r => r.ApplicantResume,
                                        j=>j.ApplicantJobApplications,
                                        s => s.SecurityLogin)
                .FirstOrDefault(k => k.Id == Id
                                        );
            
            if (poco is null) return NotFound("No student found");

            return View(poco);
        }

        public IActionResult Create()
        {
            PopulateLanguageDropDownList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(ApplicantProfilePoco applicant, SecurityLoginPoco securityLogin)
        {
            applicant.Id = Guid.NewGuid();
            applicant.SecurityLogin = securityLogin;
            applicant.Login = applicant.SecurityLogin.Id;
            applicant.SecurityLogin.ApplicantProfile = applicant;
            applicant.SecurityLogin.AgreementAccepted = DateTime.Now;
            securityLogin.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>()).Add(new SecurityLoginPoco[] { securityLogin });
                    _logic.Add(new ApplicantProfilePoco[] { applicant });

                    return RedirectToAction(nameof(Details), new { Id = applicant.Id });

            };
            PopulateLanguageDropDownList();
            return View();
        }

        public IActionResult Edit (Guid Id)
        {
            var poco = _logic.GetList(c => c.Id == Id, o => o.SecurityLogin).Single();
            if (poco is null) return NotFound();

            PopulateLanguageDropDownList(poco.SecurityLogin.PrefferredLanguage, poco.Country);
            
            return View(poco);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Edit")]
        public async Task<IActionResult> EditSaveAsync(Guid Id)
        {
            var ApplicantPoco = _logic.GetList(c=>c.Id==Id, c=>c.SecurityLogin).Single();
            if (ApplicantPoco is null) return NotFound();

            var secPoco = ApplicantPoco.SecurityLogin;

            if (ModelState.IsValid)
            {
                if (await TryUpdateModelAsync<ApplicantProfilePoco>(ApplicantPoco,"",
                        i=>i.City,
                        i=>i.Country,
                        i=>i.Currency,
                        i=>i.CurrentRate,
                        i=>i.CurrentSalary,
                        i=>i.PostalCode,
                        i=>i.Province,
                        i=>i.Street,
                        i=>i.SecurityLogin)) {

                    //secPoco = ApplicantPoco.SecurityLogin;
                    
                    try
                    {
                        new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>()).Update(new SecurityLoginPoco[] { secPoco });
                        _logic.Update(new ApplicantProfilePoco[] { ApplicantPoco });
                    }
                    catch(Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    
                }
                return RedirectToAction(nameof(Details), new { Id = ApplicantPoco.Id });
            }

            PopulateLanguageDropDownList(secPoco.PrefferredLanguage, ApplicantPoco.Country);
            return View(ApplicantPoco);
        }

        private void PopulateLanguageDropDownList(object selectedLanguage = null, object selectedCountry = null)
        {
            var SysLang = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>()).GetList(c=>c.LanguageID.Length <= 3).OrderBy(l=>l.Name);
            var SysCountry = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>()).GetList(c=>c.Code.Length <= 4).OrderBy(l=>l.Name);
            
            ViewBag.Language = new SelectList(SysLang, "LanguageID", "Name", selectedLanguage);
            ViewBag.Country = new SelectList(SysCountry, "Code", "Name", selectedCountry);
        }
    }
}