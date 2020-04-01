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
    public class CompanyJobDescriptionController : Controller
    {
        private readonly CompanyJobDescriptionLogic _logic;

        public CompanyJobDescriptionController()
        {
            _logic = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>());
        }
        public IActionResult Index()
        {
            return View(_logic.GetAll()); 
        }

        public IActionResult Details(Guid Id)
        {
            var Job = _logic.GetList(c => c.Id == Id, 
                i=>i.CompanyJob, 
                i=>i.CompanyJob.CompanyJobSkills,
                i=>i.CompanyJob.CompanyJobEducation,
                i=>i.CompanyJob.CompanyProfile, 
                i=>i.CompanyJob.CompanyProfile.CompanyLocations).SingleOrDefault();

            var applicants = new EFGenericRepository<ApplicantProfilePoco>()
                        .GetAll(i => i.SecurityLogin)
                            .OrderByDescending(i=>i.SecurityLogin.Created)
                        .Select(o=>new {Id = o.Id, Names = o.SecurityLogin.FullName });

            ViewBag.Applicants = new SelectList(applicants, "Id", "Names");//applicants.First(i=>i.Login == i.SecurityLogin.Id).SecurityLogin.FullName);

            if (Job is null) return NotFound();

            return View(Job);
        }
    }
}