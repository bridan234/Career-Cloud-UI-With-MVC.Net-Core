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
    public class ApplicantResumeController : Controller
    {
        private readonly CareerCloudContext _context;
        private readonly ApplicantResumeLogic _logic;

        public ApplicantResumeController()
        {
            _logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>());
        }

        // GET: ApplicantResumePocoes
        public async Task<IActionResult> Index()
        {
            var careerCloudContext = _context.ApplicantResumes.Include(a => a.ApplicantProfile);
            return View(await careerCloudContext.ToListAsync());
        }

        // GET: ApplicantResumePocoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumePoco = await _context.ApplicantResumes
                .Include(a => a.ApplicantProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicantResumePoco == null)
            {
                return NotFound();
            }

            return View(applicantResumePoco);
        }

        // GET: ApplicantResumePocoes/Create
        public IActionResult Create(Guid? Applicant)
        {
            
            var appName = new EFGenericRepository<ApplicantProfilePoco>().GetList(c => c.Id == Applicant, i => i.SecurityLogin).First().SecurityLogin.FullName;
            ViewData["empID"] = Applicant;
            ViewData["empName"] = appName;
            return View();
        }

        void PopulateApplicantList(Guid? Selected = null)
        {
            var logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
            ViewData["Applicant"] = new SelectList(
                logic.GetAll(i => i.SecurityLogin)
                .OrderByDescending(i => i.SecurityLogin.Created)
                .Select(i => new {
                    Id = i.Id,
                    Name = i.SecurityLogin.FullName
                })
                , "Id", "Name", Selected);
        }

        // POST: ApplicantResumePocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicantResumePoco applicantResumePoco )
        {
            if (ModelState.IsValid)
            {
                applicantResumePoco.Id = Guid.NewGuid();

                _logic.Add(new ApplicantResumePoco[] { applicantResumePoco });

                return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = applicantResumePoco.Applicant });
            }
            PopulateApplicantList(applicantResumePoco.Applicant);
            return View(applicantResumePoco);
            
        }

        // GET: ApplicantResumePocoes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {


            var poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            PopulateApplicantList(poco.Applicant);
            return View(poco);
        }

        // POST: ApplicantResumePocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Edit")]
        public async Task<IActionResult> EditSave(Guid id)
        {
            var poco = _logic.Get(id);
            if (poco is null) return NotFound();
            poco.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (await TryUpdateModelAsync(poco,"",
                    i=>i.Resume,
                    i=>i.LastUpdated
                    ))
                {

                    try
                    {
                        _logic.Update( new ApplicantResumePoco[] { poco });
                        
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ApplicantResumePocoExists(poco.Id))
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
            PopulateApplicantList(poco.Applicant);
            return View(poco);
        }

        // GET: ApplicantResumePocoes/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicantResumePoco = _logic.GetList(c => c.Id == id, a => a.ApplicantProfile, a => a.ApplicantProfile.SecurityLogin).Single();

            if (applicantResumePoco == null)
            {
                return NotFound();
            }

            return View(applicantResumePoco);
        }

        // POST: ApplicantResumePocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var applicantResumePoco = _logic.Get(id);
            _logic.Delete(new ApplicantResumePoco[] { applicantResumePoco });

            return RedirectToAction(nameof(Details), "ApplicantProfile", new { Id = applicantResumePoco.Applicant });
        }

        private bool ApplicantResumePocoExists(Guid id)
        {
            return _logic.GetList(e => e.Id == id).Any();
        }
    }
}
