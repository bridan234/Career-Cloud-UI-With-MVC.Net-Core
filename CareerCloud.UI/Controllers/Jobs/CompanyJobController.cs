using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareerCloud.UI.Controllers.Jobs
{
    public class CompanyJobController : Controller
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobController()
        {
            _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }
        // GET: CompanyJob
        public ActionResult Index()
        {
            return View(_logic.GetAll());
        }

        // GET: CompanyJob/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyJob/Create
        public ActionResult Create(Guid? Company)
        {
            PopulateCompanyList(Company);
            return View();
        }

        private void PopulateCompanyList(Guid? Company = null)
        {
            var Companies = new EFGenericRepository<CompanyDescriptionPoco>().GetAll().Select(k=> new { Id = k.Company, Name = k.CompanyName is "" ? "<empty>" : k.CompanyName} );
            ViewData["Company"] = new SelectList(Companies, "Id", "Name", Company);
        }

        // POST: CompanyJob/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CompanyJobPoco Job, [Bind] CompanyJobDescriptionPoco JobDesc)
        {
            Job.Id = Guid.NewGuid();
            Job.ProfileCreated = DateTime.Now;

            //JobDesc.CompanyJob = Job;
            JobDesc = Job.CompanyJobDescription;
            JobDesc.Id = Guid.NewGuid();
            JobDesc.Job = Job.Id;
            
            if (ModelState.IsValid)
            {
                try
                {
                    _logic.Add(new CompanyJobPoco[] { Job });
                    new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>()).Add(new CompanyJobDescriptionPoco[] { JobDesc });
                    return RedirectToAction(nameof(Details), "companyprofile", new { Id = Job.Company });
                }
                catch
                {
                    return View();
                }
                
            }

            PopulateCompanyList(Job.Company);
            return View(Job);
        }

        // GET: CompanyJob/Edit/5
        public ActionResult Edit(Guid Id)
        {
            var Job = _logic.GetList(c=>c.Id==Id,g=>g.CompanyJobDescription).SingleOrDefault();
                if (Job is null) return NotFound();

            PopulateCompanyList(Job.Company);

            return View(Job);
        }

        // POST: CompanyJob/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Guid Id, CompanyJobPoco poco)
        {
            var Job = _logic.GetList(c=>c.Id==Id, i=>i.CompanyJobDescription).SingleOrDefault();
            if (Job is null) return NotFound();
            Job.IsCompanyHidden = poco.IsCompanyHidden;
            Job.IsInactive = poco.IsInactive;

            var JobDesc = Job.CompanyJobDescription;
            JobDesc.JobName = poco.CompanyJobDescription.JobName;
            JobDesc.JobDescriptions = poco.CompanyJobDescription.JobDescriptions;

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    _logic.Update( new CompanyJobPoco[] { Job });
                    new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>()).Update(new CompanyJobDescriptionPoco[] { JobDesc });
                    return RedirectToAction(nameof(Details), "companyprofile", new { Id = Job.Company });
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                };
            }
            PopulateCompanyList(Job.Company);
            return View(Job);
        }

        // GET: CompanyJob/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompanyJob/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}