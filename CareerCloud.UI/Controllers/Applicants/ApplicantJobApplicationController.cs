using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;

namespace CareerCloud.UI.Controllers.Applicants
{
    public class ApplicantJobApplicationController : Controller
    {
        private readonly ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationController()
        {
            _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
            
        }

        // GET: ApplicantJobApplication
        public async Task<IActionResult> Index(Guid? Applicant)
        {
            var Jobs = _logic.GetAll(a => a.ApplicantProfile, s => s.ApplicantProfile.SecurityLogin);

            if (Applicant != null)
                Jobs = Jobs.Where(a => a.Applicant == Applicant).ToList();
            else
                return NotFound();

            ViewData["Id"] = Applicant;
            return View(Jobs.ToList());
        }

        // GET: ApplicantJobApplication/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            return null;
        }

        // GET: ApplicantJobApplication/Create
        public IActionResult Create(Guid? Applicant, Guid? Job)
        {
            PopulateApplicantList(Applicant);
            PopulateJobSelectList(Job);
            return View();
        }

        void PopulateApplicantList(Guid? Selected = null)
        {
            var logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
            ViewData["Applicant"] = new SelectList(
                logic.GetAll(i => i.SecurityLogin)
                .OrderByDescending(i=>i.SecurityLogin.Created)
                .Select(i => new { 
                    Id = i.Id, Name = i.SecurityLogin.FullName })
                , "Id", "Name", Selected);
        }
        // POST: ApplicantJobApplication/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Applicant,Job,ApplicationDate,TimeStamp")] ApplicantJobApplicationPoco applicantJobApplicationPoco)
        {
            if (_logic.GetList(j => j.CompanyJob.Id == applicantJobApplicationPoco.Job) == null) return BadRequest("Sorry This Job was not found \nOr have expired, please check the number and try again");
            if (ModelState.IsValid)
            {
                applicantJobApplicationPoco.Id = Guid.NewGuid();
                applicantJobApplicationPoco.ApplicationDate = DateTime.Now;

                _logic.Add(new ApplicantJobApplicationPoco[] { applicantJobApplicationPoco });
                return RedirectToAction(nameof(Details),"ApplicantProfile",new { Id = applicantJobApplicationPoco.Applicant });
            }
            //ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantJobApplicationPoco.Applicant);
            PopulateApplicantList(applicantJobApplicationPoco.Applicant);
            PopulateJobSelectList(applicantJobApplicationPoco.Job);
            return View(applicantJobApplicationPoco);
        }

        // GET: ApplicantJobApplication/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var poco = _logic.GetAll(a => a.ApplicantProfile.SecurityLogin, i => i.CompanyJob, i => i.CompanyJob.CompanyJobDescription).FirstOrDefault(i => i.Id == id); ;

            if (poco == null)
            {
                return NotFound();
            }

            //ViewData["Applicant"] = new SelectList(_context.ApplicantProfiles, "Id", "Id", applicantJobApplicationPoco.Applicant);

            PopulateJobSelectList(poco.Job);
            return View(poco);
        }
        public void PopulateJobSelectList(Guid? selected = null)
        {
            var logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
            var Poco = logic.GetAll(i => i.CompanyJobDescription, i=>i.CompanyProfile.CompanyDescriptions);

            var compJobs = from d in Poco.Select(j => new { Job = j.Id, Name = j.CompanyJobDescription.JobName, comp = j.CompanyProfile.CompanyDescriptions }) select d;

            ViewData["Job"] = new SelectList(compJobs.Select(s => new { Id = s.Job, JobName = string.Concat(s.Name," <--|--> ",s.comp.First().CompanyName) }), "Id", "JobName", selected);

        }
        // POST: ApplicantJobApplication/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken,ActionName("Edit")]
        public async Task<IActionResult> EditSave(Guid id)
        {
            var poco = _logic.Get(id);
            if (poco is null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(poco,"",i=> i.Job, i=> i.ApplicationDate))
            {

                if (ModelState.IsValid)
                {
                    try
                    {
                        _logic.Update(new ApplicantJobApplicationPoco[] { poco });
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ApplicantJobApplicationPocoExists(poco.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = poco.Applicant });
            }

            PopulateJobSelectList(poco.Job);
            return View(poco);
        }

        // GET: ApplicantJobApplication/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantJobApplicationPoco = _logic.GetAll(a => a.ApplicantProfile.SecurityLogin, a => a.CompanyJob.CompanyJobDescription).FirstOrDefault(m => m.Id == id); //await _context.ApplicantJobApplications

            if (applicantJobApplicationPoco == null)
            {
                return NotFound("Not Found");
            }

            return View(applicantJobApplicationPoco);
        }

        // POST: ApplicantJobApplication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var poco = _logic.Get(id);
            _logic.Delete(new ApplicantJobApplicationPoco[] { poco });
            return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = poco.Applicant });
        }

        private bool ApplicantJobApplicationPocoExists(Guid id)
        {
            return _logic.GetList(i => i.Id ==id).Any();
        }
    }
}
