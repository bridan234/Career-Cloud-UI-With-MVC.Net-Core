using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareerCloud.UI.Controllers
{
    public class CompanyProfileController : Controller
    {
        
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileController()
        {
            _logic = new CompanyProfileLogic( new EFGenericRepository<CompanyProfilePoco>());
        }
        public IActionResult Index()
        {
            var Companies = _logic.GetAll(i => i.CompanyDescriptions).OrderByDescending(c => c.RegistrationDate).Distinct();
            return View(Companies);
        }
         
        public IActionResult Details(Guid Id)
        {
            
            var Company = _logic.GetList(id => id.Id == Id
            , i => i.CompanyLocations,
            i => i.CompanyJobs
            , i => i.CompanyDescriptions
            ).Single();

            return View(Company);
        }

        public IActionResult Create()
        {
            PopulateLanguageDropDownList();
            PopulateCountryDropDownList();
            return View();
        }

        [AutoValidateAntiforgeryToken, HttpPost, ActionName("Create")]
        public IActionResult Save(CompanyProfilePoco company, [Bind] CompanyDescriptionPoco description,[Bind] CompanyLocationPoco location)
        {
            company.Id = Guid.NewGuid();
            company.RegistrationDate = DateTime.Now;
            company.CompanyLogo = new byte[] { 0, 0, 0, 25 };

            //CompanyDescriptionPoco description = company.CompanyDescriptions.Single();
            description.Id = Guid.NewGuid();
            //description.Company = company.Id;
            description.CompanyProfile = company;
            
            //CompanyLocationPoco location = company.CompanyLocations.Single();
            location.Id = Guid.NewGuid(); ;
            //location.Company = company.Id;
            location.CompanyProfile = company;

            if (ModelState.IsValid)
            {
                try
                {
                    _logic.Add(new CompanyProfilePoco[] { company });
                    new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>()).Add(new CompanyDescriptionPoco[] { description });
                    new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>()).Add(new CompanyLocationPoco[] { location });
                    
                    return RedirectToAction(nameof(Details), new { Id = company.Id });
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            PopulateCountryDropDownList(location.CountryCode);
            PopulateLanguageDropDownList(description.LanguageId);
            return View(company);
        }

        public IActionResult Edit (Guid Id)
        {
            var company = _logic.GetList(c=> c.Id == Id, o=>o.CompanyDescriptions).SingleOrDefault() ;
            if (company is null) return NotFound();

            return View(company);
        }

        [ActionName(nameof(Edit)), AutoValidateAntiforgeryToken, HttpPost]
        public async Task<IActionResult> EditConfirmedAsync(Guid Id)
        {
            var company = _logic.Get(Id);
            if (company is null) return NotFound();

            if(await TryUpdateModelAsync(company,"",i=>i.Id, i=>i.CompanyWebsite, i=>i.ContactName, i => i.ContactPhone))
            {
                try
                {
                    _logic.Update(new CompanyProfilePoco[] { company });
                    return RedirectToAction(nameof(Details), new { Id = company.Id });
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return View(company);
        }

        public IActionResult Delete (Guid Id)
        {
            var company = _logic.GetList(I=>I.Id ==Id, c=>c.CompanyDescriptions).SingleOrDefault();
            if (company is null) return NotFound();

            return View(company);
        }

        [HttpPost, ActionName(nameof(Delete))]
        public IActionResult DeleteConfirmed(Guid Id)
        {
            var company = _logic.Get(Id);
            if (company is null) return BadRequest();

            var compdescs = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>()).GetList(c=>c.Company == company.Id);
            if(compdescs.Count >0 ) new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>()).Delete( compdescs.ToArray() );

            var compLocs = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>()).GetList(c=>c.Company ==company.Id);
            if (compLocs.Count > 0)new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>()).Delete(compLocs.ToArray());

            var compJobs = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>()).GetList(c=>c.Company == company.Id);
            if (compJobs.Count > 0) {

                List<CompanyJobSkillPoco> skill = new List<CompanyJobSkillPoco>();
                List<CompanyJobEducationPoco> educs = new List<CompanyJobEducationPoco>();
                List<CompanyJobDescriptionPoco> jds = new List<CompanyJobDescriptionPoco>();
                List<ApplicantJobApplicationPoco> ajas = new List<ApplicantJobApplicationPoco>();

                compJobs.ForEach(j => 
                {
                    var jobskills = new EFGenericRepository<CompanyJobSkillPoco>().GetList(c => c.Job == j.Id).ToList();
                    skill.AddRange(jobskills.ToList());
                    
                    var jobedu = new EFGenericRepository<CompanyJobEducationPoco>().GetList(c => c.Job == j.Id).ToList();
                    educs.AddRange(jobedu.ToList());                                                                                                                               //= compJobs.ForEach(p=>p.Id == c.Job) }c.Job == compJobs.First().Id).ToArray());

                    var jobdesc = new EFGenericRepository<CompanyJobDescriptionPoco>().GetList(c => c.Job == j.Id).ToList();
                    jds.AddRange(jobdesc.ToList());

                    var appjobapp = new EFGenericRepository<ApplicantJobApplicationPoco>().GetList(c => c.Job == j.Id).ToList();
                    ajas.AddRange(appjobapp.ToList());
                });

                if (skill.Count > 0) new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>()).Delete(skill.ToArray());
                if (educs.Count > 0) new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>()).Delete(educs.ToArray());
                if (jds.Count > 0) new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>()).Delete(jds.ToArray());
                if (ajas.Count > 0) new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>()).Delete(ajas.ToArray());

                new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>()).Delete(compJobs.ToArray());
            }

            _logic.Delete(new CompanyProfilePoco[] { company });

            return RedirectToAction(nameof(Index));
        }
        private void PopulateLanguageDropDownList(object selectedLanguage = null)
        {
            var SysCountry = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>()).GetList(c => c.LanguageID.Length <= 4).OrderBy(l => l.Name);

            ViewBag.Language = new SelectList(SysCountry, "LanguageID", "Name", selectedLanguage);
        }

        private void PopulateCountryDropDownList(object selectedCountry = null)
        {
           var SysCountry = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>()).GetList(c => c.Code.Length <= 4).OrderBy(l => l.Name);

           ViewBag.Country = new SelectList(SysCountry, "Code", "Name", selectedCountry);
        }


    }
}